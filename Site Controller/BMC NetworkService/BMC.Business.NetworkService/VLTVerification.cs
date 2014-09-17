using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.NetworkService;
using System.Data;
using BMC.Business.CashDeskOperator.WebServices;
using System.Configuration;
using ComExchangeLib;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;
using BMC.Common.Utilities;

namespace BMC.Business.NetworkService
{
    public class VLTVerification : ObjectStateObserver, IDisposable
    {
        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        private bool _disposed;

        //private volatile List<string> _udpCollection;
        public List<Sector205MessageStore> LP21Store;
        public List<Sector203MessageStore> sector203MessageStore;

        public static object HoldingObject = new object();
        public static object sectorHoldingobject = new object();

        private ThreadDispatcher<TITOConfigThreadData> _titoDispatcher = null;
        private bool _isDisposeInitiated = false;

        public VLTVerification()
        {
            

            _exchangeClient = new ExchangeClient();
                        
            //_exchangeClient.ExchangeSectorUpdate += ExchangeClientExchangeSectorUpdate;
            _exchangeClient.ExchangeSectorUpdate += _exchangeClient_ExchangeSectorUpdate;
            //_exchangeClient.ACK += ExchangeClientAck; ------------------------------------ this will be used in case of LP21
            _exchangeClient.ACK += Exchange203Reply;
            //_exchangeClient.UDPUpdate += ExchangeClientUDPUpdate;
            //_exchangeClient.ServerUpdate += ExchangeClientServerUpdate;

            _exchangeClient.InitialiseExchange(0);

            LP21Store = new List<Sector205MessageStore>();
            sector203MessageStore = new List<Sector203MessageStore>();

            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);
        }

        public VLTVerification(bool temp)
        {
            ObjectStateNotifier.AddObserver(this);
        }

        public VLTVerification(ThreadDispatcher<TITOConfigThreadData> titoDispatcher)
            : this()
        {
            _titoDispatcher = titoDispatcher;
            _titoDispatcher.AddProcessThreadData(new ProcessThreadDataHandler<TITOConfigThreadData>(this.OnTITOConfigAction));
        }



        void _exchangeClient_ExchangeSectorUpdate()
        {
            LogManager.WriteLog("Inside Exchange Sector update", LogManager.enumLogLevel.Info);
            string VLTXML = string.Empty;

            try
            {
                Proxy webProxy = new Proxy(DBBuilder.GetSiteCode(), DBBuilder.GetExchangeKey(), DBBuilder.GetEnterpriseKey());

                lock (sectorHoldingobject)
                {
                    object punk;
                    _exchangeClient.ExchangeReadSector(out punk);

                    var udPinfo = (IUDPinfo)punk;
                    var udpNo = udPinfo.UDPNo;
                    int messageID203;

                    //if (punk.GetType() == typeof(Sector205Data))
                    //{
                    var sector205Data = (Sector205Data)punk;
                    var counter205 = sector205Data.Get205Data;
                    var returnObjectLength = sector205Data.CommandLength;

                    if (counter205 == null && counter205.Length < 3) return;
                    //string cRC = counter205.GetValue(1).ToString().PadLeft(2, '0') + counter205.GetValue(2).ToString().PadLeft(2, '0');
                    string cRC = ((Convert.ToInt32(counter205.GetValue(1)) << 8) | (Convert.ToInt32(counter205.GetValue(2)))).ToString();

                    LogManager.WriteLog("The received CRC is:" + cRC, LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("The Message ID for Sector 205 received is:" + sector205Data.MessageID.ToString(), LogManager.enumLogLevel.Info);

                    //var memoryList = LP21Store.Where(message => message.messageId == sector205Data.MessageID);
                    var memoryList = LP21Store.Where(message => message.messageId == sector205Data.MessageID);

                    if (memoryList != null)
                    {
                        foreach (var store in memoryList)
                        {
                            if (store.Type != "04" && store.Type != "05") //not for verification process, 04 - Daily Verification, 05 - On Demand
                            {
                                if (cRC == store.cRC)
                                {
                                    LogManager.WriteLog("CRCs match, verified.", LogManager.enumLogLevel.Info);
                                    VerificationDBBuilder.UpdateLP21Status(store.referenceId, "Enabled", store.Type);
                                }
                                else
                                {
                                    LogManager.WriteLog("CRCs did not match, Expected:" + store.cRC, LogManager.enumLogLevel.Info);
                                    LogManager.WriteLog("CRCs did not match, Received:" + cRC, LogManager.enumLogLevel.Info);
                                    if (store.Type == "03") //Installation
                                    {
                                        if (VerificationDBBuilder.UpdateInstallationcRC(store.Installation_No, cRC))
                                        {
                                            VLTXML = VerificationDBBuilder.GetVLTDetailsinXML(store.Serial, "");
                                            try
                                            {
                                                if (!webProxy.UpdateDetailsFromXML("VLTVERIFICATION", VLTXML))
                                                    VerificationDBBuilder.InsertExportHistory("VLTVERIFICATION", store.referenceId.ToString());
                                            }
                                            catch (Exception Ex)
                                            {
                                                ExceptionManager.Publish(Ex);
                                                VerificationDBBuilder.InsertExportHistory("VLTVERIFICATION", store.referenceId.ToString());
                                            }

                                            LogManager.WriteLog("verification for installation updated in enterprise", LogManager.enumLogLevel.Info);
                                        }
                                    }
                                    else
                                    {
                                        LogManager.WriteLog("Either Power up or Ram Clear happened, send 203 command.", LogManager.enumLogLevel.Info);
                                        messageID203 = SendSector203Comexchange(store.Installation_No, 112);
                                        sector203MessageStore.Add(new Sector203MessageStore() { InstallationNo = store.Installation_No, Serial = store.Serial, cRC = cRC, updateType = store.Type, MessageId = messageID203, MessageID205 = store.messageId });
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                LogManager.WriteLog("As a part of verification process.", LogManager.enumLogLevel.Info);
                                int Status = cRC == store.cRC ? 1 : 0;
                                VerificationDBBuilder.UpdateVLTVerificationStatus(store.Serial, Status);

                                VLTXML = VerificationDBBuilder.GetVLTDetailsinXML(store.Serial, store.AAMSMessageID);

                                VerificationDBBuilder.UpdateAAMSVerification(store.referenceId, 100);

                                try
                                {
                                    if (!webProxy.UpdateDetailsFromXML("VLTVERIFICATION", VLTXML))
                                        VerificationDBBuilder.InsertExportHistory("AAMSVERIFICATION", store.referenceId.ToString());
                                }
                                catch (Exception Ex)
                                {
                                    ExceptionManager.Publish(Ex);
                                    VerificationDBBuilder.InsertExportHistory("AAMSVERIFICATION", store.referenceId.ToString());
                                }
                            }
                        }

                        LP21Store.RemoveAll(x => x.messageId == sector205Data.MessageID);
                    }
                    //}

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public bool IsRegulatoryEnabled()
        {
            try
            {
                string IsRegulatoryEnabled = DBBuilder.GetSettingFromDB("IsRegulatoryEnabled", "False");
                string IsRegulatoryAAMS = DBBuilder.GetSettingFromDB("RegulatoryType", "A");

                LogManager.WriteLog(string.Format("{0} - {1}, {2} - {3}", "Regulatory Enabled", IsRegulatoryEnabled.ToString().ToUpper(),
                                    "Regulatory Type", IsRegulatoryAAMS.ToUpper()), LogManager.enumLogLevel.Info);

                if (IsRegulatoryEnabled.ToUpper() == "TRUE" && IsRegulatoryAAMS.ToUpper() == "AAMS")
                    return true;

                return false;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public void GetDataforLP21()
        {
            string message = string.Empty;
            string Seed;
            DataTable LP21Data;
            int messageID;
            //AutoEnableDisable auto = new AutoEnableDisable();

            Seed = DBBuilder.GetSettingFromDB("LP21_Seed", "00,01");
            message = "33," + Seed;

            LP21Data = VerificationDBBuilder.GetLP21Details();

            if ((LP21Data == null) || (LP21Data.Rows.Count == 0))
            {
                LogManager.WriteLog("No data to process", LogManager.enumLogLevel.Info);
                return;
            }

            foreach (DataRow row in LP21Data.Rows)
            {
                messageID = SendSector205Comexchange(int.Parse(row["Installation_No"].ToString()), message);

                LogManager.WriteLog("Sector 205 - LP21 issued with Message ID:" + messageID.ToString() + " for installation:" + row["Installation_No"].ToString(), LogManager.enumLogLevel.Info);

                lock (HoldingObject)
                {
                    LP21Store.Add(new Sector205MessageStore()
                    {
                        referenceId = int.Parse(row["ReferenceID"].ToString()),
                        Serial = row["Serial"].ToString(),
                        Installation_No = Convert.ToInt32(row["Installation_No"]),
                        messageId = messageID,
                        AddDate = DateTime.Now,
                        cRC = row["Installation_cRC"].ToString(),
                        Type = row["UpdateType"].ToString()
                    });

                }
            }
        }

        public void GetVLTforB5Request()
        {
            DataTable B5Req;
            int MessageID;
            int InstallationNo;
            try
            {
                B5Req = VerificationDBBuilder.GetB5ReqDetails();

                if ((B5Req == null) || (B5Req.Rows.Count == 0))
                {
                    LogManager.WriteLog("No data to process", LogManager.enumLogLevel.Info);
                    return;
                }

                foreach (DataRow row in B5Req.Rows)
                {
                    InstallationNo = Convert.ToInt32(row["Installation_No"]);
                    lock (sectorHoldingobject)
                    {
                        LogManager.WriteLog("Sending B5 Request: Installation No" + InstallationNo, LogManager.enumLogLevel.Info);    
                        MessageID = SendSector203Comexchange(InstallationNo, 112);
                        
                        sector203MessageStore.Add(new Sector203MessageStore() 
                                                        { InstallationNo = Convert.ToInt32(row["Installation_No"]), 
                                                          cRC="", 
                                                          MessageId = MessageID, 
                                                          MessageID205 = 0,
                                                          ReferenceId = Convert.ToInt32(row["BAD_ID"]), 
                                                          Serial = "", 
                                                          updateType = ""
                                                        });
                        LogManager.WriteLog("B5 Request Sent: Installation No: " + InstallationNo + "Message ID: " + MessageID.ToString(), LogManager.enumLogLevel.Info);    
                    }
                }

                
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("GetVLTforB5Request Error " + Ex.Message, LogManager.enumLogLevel.Info);    
                ExceptionManager.Publish(Ex);
            }
        }

        public void SyncGMUDateTime(char syncStatus)
        {
            DataTable activeInstallations;
            int MessageID;
            int InstallationNo;
            try
            {
                activeInstallations = VerificationDBBuilder.GetNoSyncInstallations(syncStatus);

                if ((activeInstallations == null) || (activeInstallations.Rows.Count == 0))
                {
                    LogManager.WriteLog("No data to process", LogManager.enumLogLevel.Info);
                    return;
                }

                foreach (DataRow row in activeInstallations.Rows)
                {
                    InstallationNo = Convert.ToInt32(row["Installation_No"]);
                    lock (HoldingObject)
                    {
                        LogManager.WriteLog("Sending Sync GMU datetime: Installation No" + InstallationNo, LogManager.enumLogLevel.Info);
                        MessageID = SendSector203Comexchange(InstallationNo, 96);

                        sector203MessageStore.Add(new Sector203MessageStore()
                        {
                            InstallationNo = Convert.ToInt32(row["Installation_No"]),
                            cRC = "",
                            MessageId = MessageID,
                            MessageID205 = 0,
                            ReferenceId = 0,
                            Serial = "",
                            updateType = "",
                            Type = "SyncTime"
                        });
                        LogManager.WriteLog("Sync GMU datetime: Installation No: " + InstallationNo + "Message ID: " + MessageID.ToString(), LogManager.enumLogLevel.Info);
                    }
                }


            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("SyncGMUDateTime Error " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }

        public bool CheckSiteStatus()
        {
            LogManager.WriteLog("Inside CheckSiteStatus method", LogManager.enumLogLevel.Info);

            return DBBuilder.GetSiteStatus();
        }

        public void VoucherExpire()
        {
            //try
            //{
            //    DataTable getInstallations;
            //    int InstallationNo;
            //    int TicketExpire = 0;
            //    LogManager.WriteLog("Inside VoucherExpire method", LogManager.enumLogLevel.Info);
            //    getInstallations = DBBuilder.GetInstallationCountAfterMeter();
            //    TicketExpire = Convert.ToInt32(DBBuilder.GetSettingFromDB("TICKET_EXPIRE", "10"));
            //    foreach (DataRow row in getInstallations.Rows)
            //    {
            //        InstallationNo = Convert.ToInt32(row["Installation_No"]);
            //        TITOParams titoParams = new TITOParams() { Installation_No = InstallationNo, TicketExpireDays = TicketExpire };
            //        ThreadPool.QueueUserWorkItem(new WaitCallback(CallTITOParams),titoParams);
            //        LogManager.WriteLog("VoucherExpire: Installation No: " + InstallationNo.ToString() , LogManager.enumLogLevel.Info);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    LogManager.WriteLog("VoucherExpire Error " + Ex.Message, LogManager.enumLogLevel.Info);
            //    ExceptionManager.Publish(Ex);
            //}
        }
        //
        public void CallTITOParams(object _titoParams)
        {
            //TITOParams titoParams = _titoParams as TITOParams;
            //ExchangeTITOParams(titoParams.Installation_No, titoParams.TicketExpireDays);
        }
        //
        public class TITOParams
        {
            public int Installation_No { get; set; }
            public int TicketExpireDays { get; set; }
        }
        //
        public bool ExchangeTITOParams(int installation_no, int TicketExpireDays)
        {
            ExchangeClient _ExchangeClient_ExchangeTITOParams = new ExchangeClient();
            try
            {
                
                _ExchangeClient_ExchangeTITOParams.InitialiseExchange(0);

                //lock (HoldingObject)
                //{
                    LogManager.WriteLog("Updated ExchangeTITOParams: Installation No" + installation_no, LogManager.enumLogLevel.Info);

                    DBBuilder.UpdateTicketExpire(TicketExpireDays);
                    if (_ExchangeClient_ExchangeTITOParams.SetTicketParameters(installation_no, TicketExpireDays) == 0)
                    {
                        DBBuilder.UpdateInstallationCountAfterMeter(installation_no);
                        LogManager.WriteLog("ExchangeTITOParams: Installation No: " + installation_no + " Success.", LogManager.enumLogLevel.Info);
                        return true;
                    }
                    else
                    {
                        LogManager.WriteLog("ExchangeTITOParams: Installation No: " + installation_no + " Failure.", LogManager.enumLogLevel.Info);
                        return false;
                    }
               
                //}
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeTITOParams Error " + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return false;
            }
            finally
            {
                var i = Marshal.ReleaseComObject(_ExchangeClient_ExchangeTITOParams);
                while (i > 0)
                {
                    LogManager.WriteLog("[ExchangeTITOParams] Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_ExchangeClient_ExchangeTITOParams);
                }
                LogManager.WriteLog("|=>[ExchangeTITOParams] _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);
            }
        }

        /// <summary>
        /// Called when [TITO config action].
        /// </summary>
        /// <param name="threadData">The thread data.</param>
        private void OnTITOConfigAction(TITOConfigThreadData threadData)
        {
            if (!threadData.CheckAndWriteLog("OnTITOConfigAction", "Method Invoked successfully.")) return;

            if (threadData.TITOEnabled == 1)
            {
                if (EnableTITO(threadData.InstallationNo) == 0)
                {
                    LogManager.WriteLog("Success TITO Enable: Installation No " + threadData.InstallationNo, LogManager.enumLogLevel.Info);
                    if (ExchangeTITOParams(threadData.InstallationNo, threadData.TicketExpireDays))
                    {
                        //
                        DBBuilder.UpdatedTITOConfig(threadData.InstallationNo, threadData.SiteTITOEnabled,
                            threadData.SiteNonCashEnabled, threadData.MachineTITOEnabled, threadData.MachineNonCashEnabled);
                        //
                    }
                }
                else
                {
                    LogManager.WriteLog("Failure TITO Enable: Installation No " + threadData.InstallationNo, LogManager.enumLogLevel.Info);
                }
            }
            else
            {
                if (DisableTITO(threadData.InstallationNo) == 0)
                {
                    //
                    DBBuilder.UpdatedTITOConfig(threadData.InstallationNo, threadData.SiteTITOEnabled,
                        threadData.SiteNonCashEnabled, threadData.MachineTITOEnabled, threadData.MachineNonCashEnabled);
                    //
                    LogManager.WriteLog("Success TITO Disable: Installation No " + threadData.InstallationNo, LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("Failure TITO Disable: Installation No " + threadData.InstallationNo, LogManager.enumLogLevel.Info);
                }
            }
        }

        //
        public void TITOConfig()
        {
            string PROC = "|=> TITOConfig() : " ;
            if (this.IsObjectInactive)
            {
                LogManager.WriteLog(PROC + "Service was instructed to stop.", LogManager.enumLogLevel.Info);
                return;
            }

            try
            {
                DataTable getTITOConig;
                LogManager.WriteLog("Inside TITOConfig method", LogManager.enumLogLevel.Info);
                getTITOConig = DBBuilder.GetTITOConfig();
                int TickerExpire = Convert.ToInt32(DBBuilder.GetSettingFromDB("TICKET_EXPIRE", "10"));
                foreach (DataRow row in getTITOConig.Rows) 
                {
                    if (this.IsObjectInactive)
                    {
                        LogManager.WriteLog(PROC + "Service was instructed to stop.", LogManager.enumLogLevel.Info);
                        return;
                    }
            
                    TITOConfigThreadData threadData = new TITOConfigThreadData()
                    {
                        InstallationNo = Convert.ToInt32(row["Installation_No"]),
                        TITOEnabled = Convert.ToInt32(row["TITOEnabled"]),
                        SiteTITOEnabled = Convert.ToInt32(row["SiteTITOEnabled"]),
                        SiteNonCashEnabled = Convert.ToInt32(row["SiteNonCashEnabled"]),
                        MachineTITOEnabled = Convert.ToInt32(row["MachineTITOEnabled"]),
                        MachineNonCashEnabled = Convert.ToInt32(row["MachineNonCashEnabled"]),
                        TicketExpireDays = TickerExpire,
                    };
                    _titoDispatcher.AddThreadData(threadData);
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("TITOConfig Error " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }
        //
        public void DisableNoTimeSyncGMUs()
        {
            DataTable allGmus;
            int InstallationNo;
            int MessageID;
            bool DisableAll;
            try
            {
                Proxy webProxy = new Proxy(DBBuilder.GetSiteCode(), DBBuilder.GetConnectionString());
                DisableAll = Math.Abs(System.DateTime.Now.Subtract(webProxy.GetSystemLocalDateTime()).Minutes) >= 
                    Convert.ToInt32(ConfigManager.Read("GMUTimeDiffOffset"))? true:false;
                allGmus = DBBuilder.GetAllGmus('Y', 0, 0);
                LogManager.WriteLog("Get all Gmus", LogManager.enumLogLevel.Info);
                if ((allGmus == null) || (allGmus.Rows.Count == 0))
                {
                    LogManager.WriteLog("No data to process", LogManager.enumLogLevel.Info);
                    return;
                }
                foreach (DataRow row in allGmus.Rows)
                {
                    InstallationNo = Convert.ToInt32(row["Installation_No"]);
                    LogManager.WriteLog("Sending Disable Machine: Installation No" + InstallationNo, LogManager.enumLogLevel.Info);
                    if (DisableAll)
                    {
                        AutoEnableDisable _disableMachine = new AutoEnableDisable();
                        _disableMachine.DisableMachine(InstallationNo);
                        _disableMachine.Dispose();
                        _disableMachine = null;
                    }
                    else
                    {
                        lock (HoldingObject)
                        {
                            LogManager.WriteLog("Sending Sync GMU datetime: Installation No" + InstallationNo, LogManager.enumLogLevel.Info);
                            MessageID = SendSector203Comexchange(InstallationNo, 116);

                            sector203MessageStore.Add(new Sector203MessageStore()
                            {
                                InstallationNo = InstallationNo,
                                cRC = "",
                                MessageId = MessageID,
                                MessageID205 = 0,
                                ReferenceId = 0,
                                Serial = "",
                                updateType = "",
                                Type = "GMUTimeDiff"
                            });
                            LogManager.WriteLog("GMU Time Difference: Installation No: " + InstallationNo + "Message ID: " + MessageID.ToString(), LogManager.enumLogLevel.Info);
                        }
                    }
                    LogManager.WriteLog("Disable Machine: Installation No: " + InstallationNo , LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("DisableNoTimeSyncGMUs Error " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }
        //
        public void VerifyToDisableMachine(int InstallationNo)
        {
            DataTable GmuTimeDiff;
            try
            {
                GmuTimeDiff = DBBuilder.GetAllGmus('N', Convert.ToInt32(ConfigManager.Read("GMUTimeDiffOffset")), InstallationNo);
                if ((GmuTimeDiff == null) || (GmuTimeDiff.Rows.Count == 0))
                {
                    LogManager.WriteLog("No data to process", LogManager.enumLogLevel.Info);
                    return;
                }

                AutoEnableDisable _disableMachine = new AutoEnableDisable();
                _disableMachine.DisableMachine(InstallationNo);
                _disableMachine.Dispose();
                _disableMachine = null;
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("VerifyToDisableMachine Error " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }
        //
        public void DisableSlotPorts() 
        {
            int installationNo;
            int MessageID;
            byte command;
            byte[] data                 =   null;
            string strMessage           =   string.Empty;
            string[] strMessageArray    =   null;
            
            try
            {
                LogManager.WriteLog("Inside DisableSlotPorts method", LogManager.enumLogLevel.Info);

                strMessage      =   "1,1,1";
                command         =   Convert.ToByte(115);

                DataTable installationDetails = VerificationDBBuilder.GetInstallationDetailsForPortBlocking();

                foreach (DataRow dataRow in installationDetails.Rows)
                {
                    try
                    {
                        installationNo      =   Convert.ToInt32(dataRow["Installation_No"]);
                        strMessageArray     =   strMessage.Split(',');
                        data                =   new byte[3];

                        for (int i = 0; i < strMessageArray.Length; i++)
                        {
                            data[i] = Convert.ToByte(strMessageArray[i]);
                        }

                        LogManager.WriteLog("Sending Sector203 Command 115 to Disable Ports for Installation - " + installationNo + "...", 
                            LogManager.enumLogLevel.Info);

                        MessageID = SendSector203Comexchange(installationNo, command, data);

                        LogManager.WriteLog("Sector203 Command 115 to Disable Ports sent successfully for Installation - " + installationNo + ".",
                            LogManager.enumLogLevel.Info);

                        lock (HoldingObject)
                        {
                            sector203MessageStore.Add(new Sector203MessageStore()
                            {
                                InstallationNo = installationNo,
                                cRC = "",
                                Serial = "",
                                updateType = "",
                                MessageId = MessageID,
                                MessageID205 = 0,
                                ReferenceId = 0,
                                Type = "DisableSlotPorts"

                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Auto Configure the Site Code in the GMU.
        /// </summary>
        public double UpdateGMUForSiteCode()
        {

            double dBlockTimeOut=10;
            double dTimerDelay = 60 * 1000;
            
            try
            {
                DataTable dtUnprocessedRecords = VerificationDBBuilder.GetInstallationsForGMUSiteCodeUpdate();

                if (dtUnprocessedRecords.Rows.Count > 0)
                {
                    ConfigManager.Read("CommsRegistryPath");
                   // var key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("CommsRegistryPath"));
                   // if (key != null)
                   //     dBlockTimeOut = Convert.ToDouble(key.GetValue("BlockingCallTimeOut"));
                    dBlockTimeOut = Convert.ToDouble(BMCRegistryHelper.GetRegKeyValue( ConfigManager.Read("CommsRegistryPath"), "BlockingCallTimeOut");
                    int nMaxThreads = 0;
                    nMaxThreads = Convert.ToInt32(ConfigManager.Read("MaxThreadPoolSize"));

                    LogManager.WriteLog("[UpdateGMUForSiteCode]- BlockingCallTimeOutValue: " + dBlockTimeOut.ToString(), LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("[UpdateGMUForSiteCode]- MaxThreads: " + nMaxThreads.ToString(), LogManager.enumLogLevel.Info);

                    dTimerDelay = ((dtUnprocessedRecords.Rows.Count/nMaxThreads )* dBlockTimeOut)+10000;

                    LogManager.WriteLog("[UpdateGMUForSiteCode]: RowCount: " + dtUnprocessedRecords.Rows.Count.ToString(), LogManager.enumLogLevel.Info);
                    foreach (DataRow dr in dtUnprocessedRecords.Rows)
                    {
                        LogManager.WriteLog("Spawning Thread for installation : " + dr["Installation_No"].ToString()+" | "+DateTime.Now.ToString(), LogManager.enumLogLevel.Info);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(SendSiteCode), dr["Installation_No"].ToString());
                    }
                }
                else
                {
                    LogManager.WriteLog("UpdateGMU - No pending data to process.", LogManager.enumLogLevel.Info);
                }

                return dTimerDelay;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdateGMU - Error Occured.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 60 * 1000;
            }
        }

        public void SendSiteCode(object InstallationNo)
        {
            int Installation_No = Convert.ToInt32(InstallationNo);
            ExchangeClient _ExchangeClient_SendSiteCode = new ExchangeClient();
            try
            {

                _ExchangeClient_SendSiteCode.InitialiseExchange(0);

                LogManager.WriteLog("Sending Site Code for Installation No :  " + Installation_No.ToString()+" | "+DateTime.Now.ToString(), LogManager.enumLogLevel.Info);

                int iRet = _ExchangeClient_SendSiteCode.SendOptionFileParam(Installation_No);

                if (iRet == 0)
                {
                    VerificationDBBuilder.UpdateGMUSiteCodeStatus(Installation_No, 0);
                    LogManager.WriteLog("Updated Site Code for Installation No : " + Installation_No.ToString() + " | " + DateTime.Now.ToString(), LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("Site Code updation failed for Installation No : " + Installation_No.ToString() + " | " + DateTime.Now.ToString(), LogManager.enumLogLevel.Info);
                }
                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdateGMU - Error Occured for Installation - " + Installation_No.ToString(), LogManager.enumLogLevel.Info);
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Info);
            }
            finally
            {
                var i = Marshal.ReleaseComObject(_ExchangeClient_SendSiteCode);
                while (i > 0)
                {
                    LogManager.WriteLog("[SendSiteCode]Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_ExchangeClient_SendSiteCode);
                }
                LogManager.WriteLog("|=>[SendSiteCode] _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);
                
                int nAvThreads,nPortThreads;
                ThreadPool.GetAvailableThreads(out nAvThreads, out nPortThreads);

                LogManager.WriteLog("[SendSiteCode]: AvailableThreads: " + nAvThreads.ToString() + " PortThreads: " + nPortThreads.ToString(), LogManager.enumLogLevel.Info); 
            }

             
        }
        #region Verification

        public void UpdateInProgressRecords()
        {
            VerificationDBBuilder.UpdatePendingRecords();
        }
        public void InserVLTtoAAMSVerify()
        {
            string dNow;
            string[] VerificationTimes;
            try
            {
                string LastRunTime = DBBuilder.GetSettingFromDB("LastVerificationRun", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                int VerifyInterval = Int32.Parse(ConfigManager.Read("VerificationTimeInterval")) * 1000;
                VerificationTimes = DBBuilder.GetSettingFromDB("VerificationTimes", "10,17").Split(',');
                foreach (string item in VerificationTimes)
                {
                    dNow = DateTime.Now.ToShortDateString() + " " + item + ":00";
                    if (Convert.ToDateTime(LastRunTime) < Convert.ToDateTime(dNow))
                    {
                        if (Convert.ToInt16(item) == DateTime.Now.Hour && (DateTime.Now.Minute * 60 * 1000) <= VerifyInterval)
                        {
                            VerificationDBBuilder.InsertVLTDetails();
                            DateTime var = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
                            VerificationDBBuilder.UpdateLastRuninSetting(var.ToShortDateString() + " " + var.TimeOfDay);
                            return;
                        }    
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ProcessVLTVerification()
        {
            string message;
            string Seed;
            int MessageID;
            string VerifyType;

            try
            {
                Seed = DBBuilder.GetSettingFromDB("LP21_Seed", "00,01");
                //message = "33,00," + Seed;
                message = "33," + Seed;

                DataTable VLTTable = VerificationDBBuilder.GetUnprocessedVLTDetails();
                foreach (DataRow dr in VLTTable.Rows)
                {
                    LogManager.WriteLog("To verify either due to daily or on demand.", LogManager.enumLogLevel.Info);

                    VerifyType = dr["AAMS_Message_ID"].ToString() == string.Empty ? "04" : "05";

                    MessageID = SendSector205Comexchange(int.Parse(dr["Installation_No"].ToString()), message);
                    LogManager.WriteLog("The new Message ID issued is:" + MessageID.ToString(), LogManager.enumLogLevel.Info);
                    lock (HoldingObject)
                    {
                        LP21Store.Add(new Sector205MessageStore()
                        {
                            AAMSMessageID = dr["AAMS_Message_ID"].ToString(),
                            AddDate = DateTime.Now,
                            cRC = dr["Installation_cRC"].ToString(),
                            Installation_No = Convert.ToInt32(dr["Installation_No"]),
                            messageId = MessageID,
                            referenceId = Convert.ToInt32(dr["IH_ID"]),
                            Serial = dr["VLT_Serial"].ToString(),
                            Type = VerifyType
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                VerificationDBBuilder.ResetAAMSVerificationRecords();
            }
        }
        //verifys whether the Last scheduled verification has been run. 
        //if not call it
        public void CheckUnRunVerifications()
        {
            try
            {
                string LastRunTime = DBBuilder.GetSettingFromDB("LastVerificationRun", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                DateTime ShouldHaveRunTime = new DateTime();
                if (LastRunTime == "00/00/00 00:00:00")
                {
                    return;
                }
                DateTime LastRunDate = Convert.ToDateTime(LastRunTime);
                TimeSpan diff = DateTime.Now.Subtract(LastRunDate);
                string[] VerificationTimes = DBBuilder.GetSettingFromDB("VerificationTimes", "10,17").Split(',');
                int LowerBound = int.Parse(VerificationTimes[0]);
                int HigherBound = int.Parse(VerificationTimes[VerificationTimes.Length - 1]);
                int CurrentHour = DateTime.Now.Hour;
                if (CurrentHour < LowerBound)
                {
                    ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, HigherBound, 0, 0);
                }
                else if (CurrentHour > HigherBound)
                {
                    ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, HigherBound, 0, 0);
                }
                else
                {
                    for (int i = 0; i < VerificationTimes.Length - 1; i++)
                    {
                        if (CurrentHour > int.Parse(VerificationTimes[i]) && CurrentHour < int.Parse(VerificationTimes[i + 1]))
                        {
                            ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(VerificationTimes[i]), 0, 0);
                        }
                    }
                }

                if (ShouldHaveRunTime != null)
                {
                    if (ShouldHaveRunTime > Convert.ToDateTime(LastRunTime))
                    {
                        //InserVLTtoAAMSVerify();
                        VerificationDBBuilder.InsertVLTDetails();
                        DateTime var = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
                        VerificationDBBuilder.UpdateLastRuninSetting(var.ToShortDateString() + " " + var.ToShortTimeString());
                    }
                }

                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        #region Component Verification       

        public void GetVLTComponentCount()
        {            
            var bcommand = new byte();

            try
            {
                LogManager.WriteLog("CV - Inside Get Component Count Method - GetVLTComponentCount.", LogManager.enumLogLevel.Info);

                DataTable dtUnprocessedRecords = VerificationDBBuilder.GetCVInstallationDetails(1);

                if (dtUnprocessedRecords.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUnprocessedRecords.Rows)
                    {
                        int MessageID;
                        int iInstallationID = dr["InstallationNo"] != DBNull.Value ? Convert.ToInt32(dr["InstallationNo"]) : 0;

                        bcommand = Convert.ToByte(97);

                        LogManager.WriteLog("CV - Get Component Count - Installation No - " + iInstallationID.ToString(), LogManager.enumLogLevel.Info);

                        MessageID = SendSector203Comexchange(iInstallationID, bcommand);

                        LogManager.WriteLog("CV - Get Component Count - The new Message ID issued is:" + MessageID.ToString(), LogManager.enumLogLevel.Info);

                        lock (HoldingObject)
                        {
                            sector203MessageStore.Add(new Sector203MessageStore()
                            {
                                InstallationNo = iInstallationID,
                                cRC = "",
                                Serial = "",
                                updateType = "",
                                MessageId = MessageID,
                                MessageID205 = 0,
                                ReferenceId = 0,
                                Type = "ComponentCount"

                            });
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("CV - Get Component Count - No pending data to process.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CV - Get Component Count - Error Occured.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void GetVLTComponentDetails()
        {
            var bcommand = new byte();
            var data = new byte[1];;

            try
            {
                LogManager.WriteLog("CV - Inside Get VLT Component Details method - GetVLTComponentDetails.", LogManager.enumLogLevel.Info);
                
                DataTable dtCVInstallationRecords = VerificationDBBuilder.GetCVInstallationDetails(2);                

                if (dtCVInstallationRecords.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCVInstallationRecords.Rows)
                    {
                        int iInstallationID = dr["InstallationNo"] != DBNull.Value ? Convert.ToInt32(dr["InstallationNo"]) : 0;
                        DataTable dtComponentTypeRecords = VerificationDBBuilder.GetComponentTypes(iInstallationID);

                        foreach (DataRow drRow in dtComponentTypeRecords.Rows)
                        {
                            int MessageID;                            
                            int iCompID = drRow["CompID"] != DBNull.Value ? Convert.ToInt32(drRow["CompID"]) : 0;

                            bcommand = Convert.ToByte(98);
                            data[0] = Convert.ToByte(iCompID);

                            LogManager.WriteLog("CV - Get VLT Component Details - Installation No - " + iInstallationID.ToString() + " - Component ID - " + iCompID.ToString(), LogManager.enumLogLevel.Info);

                            string strValue = string.Empty;

                            try
                            {
                                for (int i = 0; i < data.Length; i++)
                                {
                                    strValue = data[0].ToString() + "," + bcommand.ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                            }

                            LogManager.WriteLog("CV - Get VLT Component Details - The Data Sent is:" + strValue, LogManager.enumLogLevel.Info);

                            MessageID = SendSector203Comexchange(iInstallationID, bcommand, data);
                            
                            LogManager.WriteLog("CV - Get VLT Component Details - The Message ID issued is:" + MessageID.ToString(), LogManager.enumLogLevel.Info);

                            lock (HoldingObject)
                            {
                                sector203MessageStore.Add(new Sector203MessageStore()
                                {
                                    InstallationNo = iInstallationID,
                                    cRC = "",
                                    Serial = "",
                                    updateType = "",
                                    MessageId = MessageID,
                                    MessageID205 = 0,
                                    ReferenceId = iCompID,
                                    Type = "ComponentDetails"
                                });
                            }
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("CV - Get VLT Component Details - No pending installation data to process.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CV - Get VLT Component Details - Error Occured.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void ProcessVLTComponentVerification()
        {
            string strSeed = string.Empty;
            string strSerialNo = string.Empty;

            var bcommand = new byte();

            try
            {
                LogManager.WriteLog("CV - Inside Component Verification method - ProcessVLTComponentVerification.", LogManager.enumLogLevel.Info);

                DataTable dtUnprocessedRecords = VerificationDBBuilder.GetUnprocessedCompVerDetails();

                if (dtUnprocessedRecords.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtUnprocessedRecords.Rows)
                    {
                        int MessageID;
                        strSeed = dr["Seed"] != DBNull.Value ? dr["Seed"].ToString() : string.Empty;
                        int iAlgorithm = dr["AlgorithmID"] != DBNull.Value ? Convert.ToInt32(dr["AlgorithmID"]) : 0;
                        int iInstallationID = dr["InstallationNo"] != DBNull.Value ? Convert.ToInt32(dr["InstallationNo"]) : 0;
                        int iCompID = dr["CompID"] != DBNull.Value ? Convert.ToInt32(dr["CompID"]) : 0;
                        strSerialNo = dr["SerialNo"] != DBNull.Value ? dr["SerialNo"].ToString() : string.Empty;

                        //Generate the data.
                        var data = new byte[5 + strSeed.Length];
                        int i, j;

                        data[0] = Convert.ToByte(iCompID);

                        data[1] = Convert.ToByte((iAlgorithm & 0xFF000000) >> 24);
                        data[2] = Convert.ToByte((iAlgorithm & 0x00FF0000) >> 16);
                        data[3] = Convert.ToByte((iAlgorithm & 0x0000FF00) >> 8);
                        data[4] = Convert.ToByte(iAlgorithm & 0x000000FF);

                        for (i = 0, j = 5; i < strSeed.Length; i++, j++)
                            data[j] = Convert.ToByte(strSeed[i]);

                        bcommand = Convert.ToByte(99);

                        string strValue = string.Empty;

                        try
                        {
                            for (i = 0; i < data.Length; i++)
                            {
                                strValue = strValue + data[i].ToString() + ",";
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }

                        LogManager.WriteLog("CV - Component Verification - Serial No - " + strSerialNo + ". Data - " + strValue, LogManager.enumLogLevel.Info);

                        MessageID = SendSector203Comexchange(iInstallationID, bcommand, data);

                        LogManager.WriteLog("CV - Component Verification - The new Message ID issued is:" + MessageID.ToString(), LogManager.enumLogLevel.Info);

                        lock (HoldingObject)
                        {
                            sector203MessageStore.Add(new Sector203MessageStore()
                            {
                                InstallationNo = iInstallationID,
                                cRC = "",
                                Serial = strSerialNo,
                                updateType = "",
                                MessageId = MessageID,
                                MessageID205 = 0,
                                ReferenceId = iCompID,
                                Type = "ComponentVerification"
                            });
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("CV - Component Verification - No pending data to process.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CV - Component Verification - Error Occured.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public void ProcessMC300Verification()
        {
            string strAlgorithm;
            LogManager.WriteLog("CV - Inside MC300 Verification method - ProcessMC300Verification.", LogManager.enumLogLevel.Info);
            var bcommand = new byte();
            int MessageID;
            int iInstallationID;
            string strSeed = string.Empty;

            DataTable dtUnprocessedRecords = VerificationDBBuilder.GetUnprocessedMC300VerDetails();
            foreach (DataRow dr in dtUnprocessedRecords.Rows)
            {
                iInstallationID = dr["InstallationNo"] != DBNull.Value ? Convert.ToInt32(dr["InstallationNo"]) : 0;
                strAlgorithm = dr["AlgorithmType"] != DBNull.Value ? dr["AlgorithmType"].ToString() : string.Empty;
                strSeed = dr["Seed"] != DBNull.Value ? dr["Seed"].ToString() : string.Empty;
                var data = new byte[1 + strSeed.Length];
                int i, j;

                if (strAlgorithm.Contains("SHA1"))
                    data[0] = Convert.ToByte(1);
                else
                    data[0] = Convert.ToByte(2);

                for (i = 0, j = 1; i < strSeed.Length; i++, j++)
                    data[j] = Convert.ToByte(strSeed[i]);

                bcommand = Convert.ToByte(100);

                string strValue = string.Empty;

                try
                {
                    for (i = 0; i < data.Length; i++)
                    {
                        strValue = strValue + data[i].ToString() + ",";
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }

                LogManager.WriteLog("CV - Inside MC300 Verification for Installation No - " + iInstallationID.ToString() + "," + strValue, LogManager.enumLogLevel.Info);

                MessageID = SendSector203Comexchange(iInstallationID, bcommand, data);

                LogManager.WriteLog("CV - Inside MC300 Verification ,The new Message ID issued is:" + MessageID.ToString(), LogManager.enumLogLevel.Info);

                lock (HoldingObject)
                {
                    sector203MessageStore.Add(new Sector203MessageStore()
                    {
                        InstallationNo = iInstallationID,
                        cRC = "",
                        Serial = "",
                        updateType = "",
                        MessageId = MessageID,
                        MessageID205 = 0,
                        ReferenceId = 0,
                        Type = "MC300Verification"
                    });
                }

            }
        }

        public void DailyCompVerification()
        {
            try
            {
                string LastRunTime = DBBuilder.GetSettingFromDB("LastVerificationRun", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                DateTime ShouldHaveRunTime = DateTime.Now;

                if (LastRunTime == "00/00/00 00:00:00")
                {
                    LogManager.WriteLog("CV - Daily Verification Not triggered. LastRunTime - " + LastRunTime, LogManager.enumLogLevel.Info);
                    return;
                }

                DateTime LastRunDate = Convert.ToDateTime(LastRunTime);
                TimeSpan diff = DateTime.Now.Subtract(LastRunDate);
                string[] VerificationTimes = DBBuilder.GetSettingFromDB("VerificationTimes", "10,18").Split(',');
                int LowerBound = int.Parse(VerificationTimes[0]);
                int HigherBound = int.Parse(VerificationTimes[VerificationTimes.Length - 1]);
                int CurrentHour = DateTime.Now.Hour;

                if (CurrentHour == LowerBound || CurrentHour == HigherBound)
                {
                    ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, CurrentHour, 0, 0);
                }

                if (CurrentHour < LowerBound)
                {
                    ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, HigherBound, 0, 0);
                }
                else if (CurrentHour > HigherBound)
                {
                    ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, HigherBound, 0, 0);
                }
                else
                {
                    for (int i = 0; i < VerificationTimes.Length - 1; i++)
                    {
                        if (CurrentHour > int.Parse(VerificationTimes[i]) && CurrentHour < int.Parse(VerificationTimes[i + 1]))
                        {
                            ShouldHaveRunTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(VerificationTimes[i]), 0, 0);
                        }
                    }
                }

                if (ShouldHaveRunTime > Convert.ToDateTime(LastRunTime))
                {
                    //Insert data for Daily Verification;
                    string strSerialNo = string.Empty;

                    var dtUnprocessedRecords = VerificationDBBuilder.GetActiveMachinesForCompVerification();

                    if (dtUnprocessedRecords.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUnprocessedRecords.Rows)
                        {                            
                            strSerialNo = dr["Machine_Serial_No"].ToString();
                            LogManager.WriteLog("CV - Daily Verification Starting for Serial No - " + strSerialNo + " . ShouldHaveRunTime - " + ShouldHaveRunTime.ToString() + " . LastRunTime - " + LastRunTime, LogManager.enumLogLevel.Info);
                            VerificationDBBuilder.UpdateDailyVerificationData(strSerialNo);
                            LogManager.WriteLog("CV - Daily Verification Completed for Serial No - " + strSerialNo , LogManager.enumLogLevel.Info);
                        }
                    }
                    else
                    {
                        LogManager.WriteLog("CV - Daily Verification. No active installations.", LogManager.enumLogLevel.Info);
                    }

                    DateTime var = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
                    VerificationDBBuilder.UpdateLastRuninSetting(var.ToShortDateString() + " " + var.ToShortTimeString());
                }
                else
                {
                    LogManager.WriteLog("CV - Daily Verification Not triggered. ShouldHaveRunTime - " + ShouldHaveRunTime.ToString() + " . LastRunTime - " + LastRunTime, LogManager.enumLogLevel.Info);
                }                
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("CV - Error in Request for DailyVerification ErrorMessage: {0}}", ex.Message), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        #endregion Component Verification        

        #endregion

        #region Com layer code

        /// <summary>
        /// Checks the COM object state and return.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="successCode">The success code.</param>
        /// <param name="errorCode">The error code.</param>
        /// <returns>Success or failure code.</returns>
        private T CheckCOMObjectStateAndReturn<T>(string functionName, T successCode, T errorCode)
        {
            if (this.IsObjectInactive)
            {
                LogManager.WriteLog("|=> " + functionName + "() : Thread was instructed to close.",
                    LogManager.enumLogLevel.Info);
                return errorCode;
            }
            else if (_isDisposeInitiated)
            {
                LogManager.WriteLog("|=> " + functionName + "() : Object is instructed to dispose.",
                    LogManager.enumLogLevel.Info);
                return errorCode;
            }
            else if (_exchangeClient == null)
            {
                LogManager.WriteLog("|=> " + functionName + "() : COM Object already disposed.",
                    LogManager.enumLogLevel.Info);
                return errorCode;
            }
            return successCode;
        }

        private int EnableTITO(int installation_no)
        {
            ExchangeClient _ExchangeClient_EnableTITO = new ExchangeClient();

            try
            {
                _ExchangeClient_EnableTITO.InitialiseExchange(0);
                //if (this.CheckCOMObjectStateAndReturn<int>("EnableTITO", 0, 1) == 1) return 1;
                return _exchangeClient.EnableCashOut(installation_no);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed TITO enable", ex.Message), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 1;
            }
            finally
            {
                var i = Marshal.ReleaseComObject(_ExchangeClient_EnableTITO);
                while (i > 0)
                {
                    LogManager.WriteLog("[EnableTITO]Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_ExchangeClient_EnableTITO);
                }
                LogManager.WriteLog("|=>[EnableTITO] _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);
            }
        }
        //
        private int DisableTITO(int installation_no)
        {
            ExchangeClient _ExchangeClient_DisableTITO = new ExchangeClient();

            try
            {
                _ExchangeClient_DisableTITO.InitialiseExchange(0);

                //if (this.CheckCOMObjectStateAndReturn<int>("DisableTITO", 0, 1) == 1) return 1;
                return _ExchangeClient_DisableTITO.DisableCashOut(installation_no);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(string.Format("Failed TITO disable", ex.Message), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return 1;
            }
            finally
            {
                var i = Marshal.ReleaseComObject(_ExchangeClient_DisableTITO);
                while (i > 0)
                {
                    LogManager.WriteLog("[DisableTITO]Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_ExchangeClient_DisableTITO);
                }
                LogManager.WriteLog("|=>[DisableTITO] _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);
            }
        }
        //
        private int SendSector205Comexchange(int udp, string message)
        {
            var sector205Data = new Sector205Data();
            var data = new byte[50];
            //var stringArray = (message + ",0").Split(',');
            var stringArray = message.Split(',');

            int i;
            for (i = 0; i < stringArray.Length; i++)
                data[i] = Convert.ToByte(stringArray[i]);

            sector205Data.CommandLength = Convert.ToByte(i);
            sector205Data.Command = Convert.ToByte(data[0]);
            sector205Data.PutCommandDataVB(data);

            _exchangeClient.RequestExWriteSector(udp, 205, sector205Data);
            return _iExchangeAdmin.LastMessageID;
        }

        private int SendSector203Comexchange(int datapak, byte command)
        {
            var sector203Data = new Sector203Data { Command = command };
            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);
            return _iExchangeAdmin.LastMessageID;
        }

        private int SendSector203Comexchange(int datapak, byte command, byte[] data)
        {
            var sector203Data = new Sector203Data();

            sector203Data.Command = command;
            sector203Data.PutCommandDataVB(data);

            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);
            return _iExchangeAdmin.LastMessageID;
        }

        void ExchangeClientExchangeSectorUpdate()
        {
            LogManager.WriteLog("Inside Exchange Sector update", LogManager.enumLogLevel.Info);
            string VLTXML = string.Empty;

            try
            {
                Proxy webProxy = new Proxy(DBBuilder.GetSiteCode(), DBBuilder.GetExchangeKey(), DBBuilder.GetEnterpriseKey());

                lock (sectorHoldingobject)
                {
                    object punk;
                    _exchangeClient.ExchangeReadSector(out punk);

                    var udPinfo = (IUDPinfo)punk;
                    var udpNo = udPinfo.UDPNo;
                    int messageID203;

                    //if (punk.GetType() == typeof(Sector205Data))
                    //{
                    var sector205Data = (Sector205Data)punk;
                    var counter205 = sector205Data.Get205Data;
                    var returnObjectLength = sector205Data.CommandLength;

                    if (counter205 == null && counter205.Length < 3) return;
                    //int iCRC = (((int)(counter205.GetValue(1)) << 8) | ((int)(counter205.GetValue(2)))).ToString();
                    //string cRC = counter205.GetValue(1).ToString().PadLeft(2, '0') + counter205.GetValue(2).ToString().PadLeft(2, '0');
                    string cRC = ((Convert.ToInt32(counter205.GetValue(1)) << 8) | (Convert.ToInt32(counter205.GetValue(2)))).ToString();

                    LogManager.WriteLog("The received CRC is:" + cRC, LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("The Message ID for Sector 205 received is:" + sector205Data.MessageID.ToString(), LogManager.enumLogLevel.Info);

                    //var memoryList = LP21Store.Where(message => message.messageId == sector205Data.MessageID);
                    var memoryList = LP21Store.Where(message => message.messageId == sector205Data.MessageID);

                    if (memoryList != null)
                    {
                        foreach (var store in memoryList)
                        {
                            if (store.Type != "04" && store.Type != "05") //not for verification process, 04 - Daily Verification, 05 - On Demand
                            {
                                if (cRC == store.cRC)
                                {
                                    LogManager.WriteLog("CRCs match, verified.", LogManager.enumLogLevel.Info);
                                    VerificationDBBuilder.UpdateLP21Status(store.referenceId, "Enabled", store.Type);
                                }
                                else
                                {
                                    LogManager.WriteLog("CRCs did not match, Expected:" + store.cRC, LogManager.enumLogLevel.Info);
                                    LogManager.WriteLog("CRCs did not match, Received:" + cRC, LogManager.enumLogLevel.Info);
                                    if (store.Type == "03") //Installation
                                    {
                                        if (VerificationDBBuilder.UpdateInstallationcRC(store.Installation_No, cRC))
                                        {
                                            VLTXML = VerificationDBBuilder.GetVLTDetailsinXML(store.Serial, "");
                                            if (!webProxy.UpdateDetailsFromXML("VLTVERIFICATION", VLTXML))
                                                VerificationDBBuilder.InsertExportHistory("VLTVERIFICATION", store.referenceId.ToString());

                                            LogManager.WriteLog("verification for installation updated in enterprise", LogManager.enumLogLevel.Info);
                                        }
                                    }
                                    else
                                    {
                                        LogManager.WriteLog("Either Power up or Ram Clear happened, send 203 command.", LogManager.enumLogLevel.Info);
                                        messageID203 = SendSector203Comexchange(store.Installation_No, 112);
                                        sector203MessageStore.Add(new Sector203MessageStore() { InstallationNo = store.Installation_No, Serial = store.Serial, cRC = cRC, updateType = store.Type, MessageId = messageID203, MessageID205 = store.messageId, ReferenceId = store.referenceId });
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                LogManager.WriteLog("As a part of verification process.", LogManager.enumLogLevel.Info);
                                int Status = cRC == store.cRC ? 1 : 0;
                                VerificationDBBuilder.UpdateVLTVerificationStatus(store.Serial, Status);

                                VLTXML = VerificationDBBuilder.GetVLTDetailsinXML(store.Serial, store.AAMSMessageID);

                                VerificationDBBuilder.UpdateAAMSVerification(store.referenceId, 100);
                                if (!webProxy.UpdateDetailsFromXML("VLTVERIFICATION", VLTXML))
                                    VerificationDBBuilder.InsertExportHistory("AAMSVERIFICATION", store.referenceId.ToString());
                            }
                        }

                        LP21Store.RemoveAll(x => x.messageId == sector205Data.MessageID);
                    }
                    //}

                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void ExchangeClientAck(MessageAck messageAck)
        {
            string VLTXML;

            try
            {
                Proxy webProxy = new Proxy(DBBuilder.GetSiteCode(), DBBuilder.GetExchangeKey(), DBBuilder.GetEnterpriseKey());

                lock (HoldingObject)
                {
                    var memoryList = sector203MessageStore.Where(message => message.MessageId == messageAck.MessageID);

                    if (memoryList != null)
                    {
                        foreach (var store in memoryList)
                        {
                            if (messageAck.ACK)
                            {
                                VerificationDBBuilder.UpdateSector203(store.InstallationNo, store.cRC, store.updateType);

                                VLTXML = VerificationDBBuilder.GetVLTDetailsinXML(store.Serial, "");

                                try
                                {
                                    if (!webProxy.UpdateDetailsFromXML("VLTVERIFICATION", VLTXML))
                                        VerificationDBBuilder.InsertExportHistory("VLTVERIFICATION", store.ReferenceId.ToString());
                                }
                                catch (Exception Ex)
                                {
                                    ExceptionManager.Publish(Ex);
                                    VerificationDBBuilder.InsertExportHistory("VLTVERIFICATION", store.ReferenceId.ToString());
                                }

                                LogManager.WriteLog("203 Ack'ed, updated the local table as well as in Enterprise.", LogManager.enumLogLevel.Info);
                            }
                            else
                                LogManager.WriteLog("Ack failure for 203 Sector call..", LogManager.enumLogLevel.Info);

                            LP21Store.RemoveAll(x => x.messageId == store.MessageID205);
                        }
                        sector203MessageStore.RemoveAll(x => x.MessageId == messageAck.MessageID);
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void Exchange203Reply(MessageAck messageAck)
        {
            lock (HoldingObject)
            {
                var memoryList = sector203MessageStore.Where(message => message.MessageId == messageAck.MessageID);
                string strStatus = string.Empty;

                if (memoryList != null)
                {
                    foreach (var store in memoryList)
                    {
                        if (messageAck.ACK)
                        {
                            switch (store.Type)
                            {
                                case "ComponentCount":
                                    LogManager.WriteLog("CV - ComponentCount - 203 Ack'ed for Message ID - " + store.MessageId + ". Recieved Component Count.", LogManager.enumLogLevel.Info);
                                    GetVLTComponentDetails();
                                    break;
                                case "ComponentDetails":
                                    LogManager.WriteLog("CV - ComponentDetails - 203 Ack'ed for Message ID - " + store.MessageId + ". Recieved Component Details.", LogManager.enumLogLevel.Info);
                                    break;
                                case "ComponentVerification":
                                    LogManager.WriteLog("CV - ComponentVerification - 203 Ack'ed for Message ID - " + store.MessageId + ". Updated the verification details.", LogManager.enumLogLevel.Info);
                                    ProcessVLTComponentVerification();
                                    break;
                                case "MC300Verification":
                                    LogManager.WriteLog("CV - MC300 Verification - 203 Ack'ed for Message ID - " + store.MessageId + ". Updated the MC300 details.", LogManager.enumLogLevel.Info);
                                    break;
                                case "SyncTime":
                                    VerificationDBBuilder.SyncInstallationStatus(store.InstallationNo,'Y');
                                    LogManager.WriteLog("CV - SyncTime - 203 Ack'ed for Message ID - " + store.MessageId + ". Installation - " + store.InstallationNo + ". GMU Time is in sync.", LogManager.enumLogLevel.Info);
                                    break;
                                case "GMUTimeDiff":
                                    VerifyToDisableMachine(store.InstallationNo);
                                    LogManager.WriteLog("CV - GMUTimeDiff - 203 Ack'ed for Message ID - " + store.MessageId + ". Installation - " + store.InstallationNo + ". GMU Time Diff is verified.", LogManager.enumLogLevel.Info);
                                    break;
                                case "DisableSlotPorts":
                                    LogManager.WriteLog("Disable Ports for Installation - " + store.InstallationNo + ". 203 Ack'ed for Message ID - " + store.MessageId + ". Slot Ports Disabled Successfully.", LogManager.enumLogLevel.Info);
                                    LogManager.WriteLog("Updating Port Disabled Status for Installation - " + store.InstallationNo + "...", LogManager.enumLogLevel.Info);
                                    if (VerificationDBBuilder.UpdatePortDisabledStatusForPortBlocking(store.InstallationNo))
                                        LogManager.WriteLog("Updated Port Disabled Status for Installation - " + store.InstallationNo + " successfully.", LogManager.enumLogLevel.Info);
                                    break;
                                case "VoucherExpire":
                                    LogManager.WriteLog("CV - VoucherExpire - 203 Ack'ed for Message ID - " + store.MessageId + ". Installation - " + store.InstallationNo + ". GMU Time is in sync.", LogManager.enumLogLevel.Info);
                                    break;
                                case "ExchangeTITO":
                                    LogManager.WriteLog("CV - ExchangeTITO - 203 Ack'ed for Message ID - " + store.MessageId + ". Installation - " + store.InstallationNo + ". GMU Time is in sync.", LogManager.enumLogLevel.Info);
                                    break;
                                case "UpdateGMU":
                                    VerificationDBBuilder.UpdateGMUSiteCodeStatus(store.InstallationNo, 0);
                                    LogManager.WriteLog("UpdateGMU - 203 Ack'ed for Message ID - " + store.MessageId + ". Installation - " + store.InstallationNo, LogManager.enumLogLevel.Info);
                                    break;
                                default:
                                    if (VerificationDBBuilder.UpdateB5ReqStatus(store.ReferenceId))
                                        LogManager.WriteLog("203 Ack'ed for Message ID - " + store.MessageId + " , updated the BMC AAMS Details table.", LogManager.enumLogLevel.Info);
                                    else
                                        LogManager.WriteLog("203 Ack'ed for Message ID - " + store.MessageId + " , but failed to update the DB", LogManager.enumLogLevel.Info);
                                    break;
                            }
                        }
                        else
                        {
                            switch (store.Type)
                            {
                                case "ComponentCount":
                                case "ComponentDetails":
                                case "ComponentVerification":
                                case "GMUTimeDiff":
                                case "VoucherExpire":
                                case "ExchangeTITO":
                                case "UpdateGMU":
                                    LogManager.WriteLog("Negative Ack for " + store.Type + " for the installation - " + store.InstallationNo + ". Message ID - " + store.MessageId + ". ", LogManager.enumLogLevel.Info);
                                    break;
                                case "MC300Verification":
                                    LogManager.WriteLog("CV - " + store.Type + " Ack failure for 203 Sector call. Message ID - " + store.MessageId + ". ", LogManager.enumLogLevel.Info);
                                    break;
                                case "SyncTime":
                                    VerificationDBBuilder.SyncInstallationStatus(store.InstallationNo,'N');
                                    LogManager.WriteLog("CV - " + store.Type + " Ack failure for 203 Sector call. Message ID - " + store.MessageId + ". ", LogManager.enumLogLevel.Info);
                                    break;
                                case "DisableSlotPorts":
                                    LogManager.WriteLog("Disable Ports for Installation - " + store.InstallationNo + ". Ack Failure for 203 Sector Call. Message ID - " + store.MessageId + ". ", LogManager.enumLogLevel.Info);                                    
                                    break;
                                default:
                                    LogManager.WriteLog("Ack failure for 203 Sector call. Message ID - " + store.MessageId + ". ", LogManager.enumLogLevel.Info);
                                    break;
                            }
                        }
                    }
                    sector203MessageStore.RemoveAll(x => x.MessageId == messageAck.MessageID);
                }
            }
        }

        public bool IsDisableOnExchangeNotInSyncEnabled()
        {
            try
            {
                return Convert.ToBoolean(DBBuilder.GetSettingFromDB("DisableOnExchangeTimeNotInSync", "false"));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public bool IsSyncDateTimeEnabled()
        {
            try
            {
                return Convert.ToBoolean(DBBuilder.GetSettingFromDB("SyncDateTimeEnabled", "false"));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }
        
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_disposed) return;
            ObjectStateNotifier.RemoveObserver(this);
            _isDisposeInitiated = true;

            try
            {
                if (_titoDispatcher != null)
                {
                    _titoDispatcher.RemoveProcessThreadData(this.OnTITOConfigAction);
                    LogManager.WriteLog("|=> ThreadDispatcher.RemoveProcessThreadData called.", LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            try
            {
                var i = Marshal.ReleaseComObject(_exchangeClient);
                while (i > 0)
                {
                    LogManager.WriteLog("Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_exchangeClient);
                }
                LogManager.WriteLog("|=> _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);

                i = Marshal.ReleaseComObject(_iExchangeAdmin);
                while (i > 0)
                {
                    LogManager.WriteLog("Number of objects in _iExchangeAdmin = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_iExchangeAdmin);
                }
                LogManager.WriteLog("|=> _iExchangeAdmin was released successfully.", LogManager.enumLogLevel.Info);
            }
            catch
            { }

            _iExchangeAdmin = null;
            _exchangeClient = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AutoEnableDisable"/> is reclaimed by garbage collection.
        /// </summary>
        ~VLTVerification()
        {
            Dispose();
            _disposed = true;
        }

        #endregion
    }

    public class Sector205MessageStore
    {
        public int referenceId;
        public string Serial;
        public int Installation_No;
        public int messageId;
        public string AAMSMessageID = "";
        public DateTime AddDate;
        public string cRC;
        public string Type;
        //public int IH_ID = 0;
    }

    public class Sector203MessageStore
    {
        public int InstallationNo { get; set; }
        public string cRC { get; set; }
        public string Serial { get; set; }
        public string updateType { get; set; }
        public int MessageId { get; set; }
        public int MessageID205 { get; set; }
        public int ReferenceId { get; set; }
        public string Type = "";
    }

    public class TITOConfigThreadData : ThreadData
    {
        public int InstallationNo { get; set; }
        public int TITOEnabled { get; set; }
        public int SiteTITOEnabled { get; set; }
        public int SiteNonCashEnabled { get; set; }
        public int MachineTITOEnabled { get; set; }
        public int MachineNonCashEnabled { get; set; }
        public int TicketExpireDays { get; set; }

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return InstallationNo.ToString(); }
        }

        #endregion
    }

  

}
