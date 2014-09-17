using System;
using System.Collections.Generic;
using BMC.DataAccess;
using BMC.DBInterface.NetworkService;
using Microsoft.Win32;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport.NetworkService;
using System.Data;
using System.Linq;
using BMC.Business.CashDeskOperator.WebServices;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using ComExchangeLib;
using System.Threading;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;


namespace BMC.Business.NetworkService
{
    public class AutoEnableDisable : ObjectStateObserver, IDisposable
    {
        #region Declaration

        public static bool? isAutoEnableDisableFeatureExists;
        Dictionary<string, string> dBarPositions;
        Dictionary<string, string> dFaultEvents;
        List<string> lFaults;
        string SiteCode = string.Empty;
        private bool _disposed;

        public bool _IsRunning;

        public static List<MessageStore> MessageStore;
        public static List<AFTMessages> AFTMessages;

        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;

        public readonly object HoldingObject = new object();
        public readonly object sectorHoldingobject = new object();

        private ThreadDispatcher<MachineConfigThreadData> _machineDispatcher = null;

        private int nMachineThreadComplete = 0;
        private int nMachineTotal = 0;

        #endregion

        #region Entry

        public AutoEnableDisable()
        {
            _exchangeClient = new ExchangeClient();

            _exchangeClient.ExchangeSectorUpdate += ExchangeClientExchangeSectorUpdate;
            _exchangeClient.ACK += ExchangeClientAck;
            _exchangeClient.UDPUpdate += ExchangeClientUDPUpdate;
            _exchangeClient.ServerUpdate += ExchangeClientServerUpdate;

            _exchangeClient.InitialiseExchange(0);
            lock (HoldingObject)
            {
                if (MessageStore == null)
                    MessageStore = new List<MessageStore>();

                if (AFTMessages == null)
                    AFTMessages = new List<AFTMessages>();
            }
            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);
        }

        public AutoEnableDisable(bool temp)
        {

        }


        public AutoEnableDisable(ThreadDispatcher<MachineConfigThreadData> machineDispatcher)
        {
            _machineDispatcher = machineDispatcher;
            _machineDispatcher.AddProcessThreadData(new ProcessThreadDataHandler<MachineConfigThreadData>(this.OnMachineConfigAction));
            ObjectStateNotifier.AddObserver(this);
        }

        public void EntryPoint()
        {

            LogManager.WriteLog("Entered EntryPoint", LogManager.enumLogLevel.Info);
            EnableDisableVLTBasedonVerfication();
        }

        #endregion
        //
        #region Get Fault parameters
        private Dictionary<string, string> GetFaults(List<string> Faults)
        {
            Dictionary<string, string> dFaults = new Dictionary<string, string>();
            try
            {


                string strDateTime = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                dFaults.Add(DBConstants.CONST_SP_PARAM_INSTALLATIONID, Faults[0]);
                dFaults.Add(DBConstants.CONST_SP_PARAM_FAULTSOURCE, Faults[1]);
                dFaults.Add(DBConstants.CONST_SP_PARAM_FAULTDESC, Faults[2]);
                dFaults.Add(DBConstants.CONST_SP_PARAM_POLLED, Faults[3]);
                dFaults.Add(DBConstants.CONST_SP_PARAM_EVENTFAULT, Faults[4]);
                dFaults.Add(DBConstants.CONST_SP_PARAM_EVENTDATE, strDateTime);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dFaults;
        }
        #endregion Get Fault parameters
        //


        #region MachineConfig

        /// <summary>
        /// Called when [Machine config action].
        /// </summary>
        /// <param name="threadData">The thread data.</param>
        private void OnMachineConfigAction(MachineConfigThreadData threadData)
        {
            
            if (!threadData.CheckAndWriteLog("OnMachineConfigAction", "Method Invoked successfully.")) return;

#if DEBUG
                //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                //watch.Start();
                //if (!threadData.CheckAndWriteLog("OnMachineConfigAction", "Method Started at : " + DateTime.Now.ToString())) return;
#endif

            try
            {
                if (threadData.Installation_Float_Status == 1 && DBBuilder.GetSettingFromDB("DISABLE_MACHINE_ON_DROP", "FALSE").ToUpper() == "TRUE")
                {
                    LogManager.WriteLog("Disabling the Installation - " + threadData.Installation_No.ToString(), LogManager.enumLogLevel.Info);
                    threadData.Enable = false; threadData.datapakCurrentState = 0;

                    EnableDisableMachine(threadData);

                    //ThreadPool.QueueUserWorkItem(new WaitCallback(EnableDisableMachine), _autoEnableDisable);
                    string strComment = "Machine Floated and Setting DISABLE_MACHINE_ON_DROP {True} hence disabling.";
                    LogManager.WriteLog(strComment, LogManager.enumLogLevel.Info);

                    DBBuilder.UpdateCommentsForAAMS(threadData.badId.ToString(), strComment, 3, 0);
                }
                else
                {
                    if (threadData.enterprisestatus)
                    {
                        threadData.Enable = true; threadData.datapakCurrentState = 1;
                        EnableDisableMachine(threadData);

                        LogManager.WriteLog("Enabling the Installation - " + threadData.Installation_No.ToString(), LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        threadData.Enable = false; threadData.datapakCurrentState = 0;
                        EnableDisableMachine(threadData);
                        string strComment = "Disabling the Installation  - " + threadData.Installation_No.ToString();
                        LogManager.WriteLog(strComment, LogManager.enumLogLevel.Info);
                        DBBuilder.UpdateCommentsForAAMS(threadData.Installation_No.ToString(), strComment, 2, 0);
                    }
                }
            }
            finally
            {
#if DEBUG
                //watch.Stop();
                //threadData.CheckAndWriteLog("OnMachineConfigAction", "Method Ended at : " + DateTime.Now.ToString());
                //threadData.CheckAndWriteLog("OnMachineConfigAction", "Time Taken to execute : " + watch.Elapsed.ToString());
#endif
                 Interlocked.Increment(ref nMachineThreadComplete);
            }
        }

        //
        public void MachineConfig()
        {

            if (nMachineThreadComplete != nMachineTotal)
            {
                LogManager.WriteLog("MachineConfig pending items to be processed.Returning to Main.", LogManager.enumLogLevel.Info);
                return;
            }

            nMachineThreadComplete = 0;

            string PROC = "|=> MachineConfig() : ";
            if (this.IsObjectInactive)
            {
                LogManager.WriteLog(PROC + "Service was instructed to stop.", LogManager.enumLogLevel.Info);
                return;
            }
                      

            try
            {
                DataTable getMachineDetails;
                LogManager.WriteLog("Inside MachineConfig method", LogManager.enumLogLevel.Info);

                getMachineDetails = DBBuilder.GetAAMSDetails(3);

                
                if (getMachineDetails.Rows.Count <= 0)
                    return;
                else
                {
                    nMachineTotal = getMachineDetails.Rows.Count;
                }

                foreach (DataRow row in getMachineDetails.Rows)
                {
                    if (this.IsObjectInactive)
                    {
                        LogManager.WriteLog(PROC + "Service was instructed to stop.", LogManager.enumLogLevel.Info);
                        return;
                    }

                    MachineConfigThreadData threadData = new MachineConfigThreadData()
                    {
                        Installation_No = row["BAD_Reference_ID"] != DBNull.Value ? Convert.ToInt32(row["BAD_Reference_ID"].ToString()) : 0,
                        Enable = false,
                        badId = Convert.ToInt32(row["BAD_ID"]),
                        datapakCurrentState = 0,
                        entityType = 3,
                        Installation_Float_Status = row["Installation_Float_Status"] != DBNull.Value ? Convert.ToInt32(row["Installation_Float_Status"]) : 0,
                        enterprisestatus = row["BMC_Enterprise_Status"] != DBNull.Value
                                                   ? Convert.ToBoolean(row["BMC_Enterprise_Status"].ToString())
                                                   : true,
                        updateDate = Convert.ToDateTime(row["BAD_Updated_Date"])
                    };
#if DEBUG
                    if (_machineDispatcher == null)
                    {
                        LogManager.WriteLog("|==> DANGER: _machineDispatcher is null.", LogManager.enumLogLevel.Info);
                    }
#endif
                    _machineDispatcher.AddThreadData(threadData);
                }
            }
            catch (Exception Ex)
            {
                LogManager.WriteLog("MachineConfig Error " + Ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(Ex);
            }
        }

        #endregion



        #region Check AAMS Table

        public double EnableDisableVLTBasedonVerfication()
        {
            DataTable AAMSTable;
            bool IsVerified;

            LogManager.WriteLog("EnableDisableVLTBasedonVerfication - Started.", LogManager.enumLogLevel.Info);

            if (!Convert.ToBoolean(ConfigManager.Read("EnableDisableVLTBasedonVerfication")))
                return 60 * 1000;

            double dTimerDelay = 60 * 1000;
            double dBlockTimeOut = 10;

            AAMSTable = DBBuilder.GetAAMSDetails(3);

            try
            {
                if (AAMSTable.Rows.Count > 0)
                {
                    ConfigManager.Read("CommsRegistryPath");
                   // var key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(ConfigManager.Read("CommsRegistryPath"));
                    //if (key != null)
                    // dBlockTimeOut = Convert.ToDouble(key.GetValue("BlockingCallTimeOut"));
                    dBlockTimeOut = Convert.ToDouble(BMCRegistryHelper.GetRegKeyValue(ConfigManager.Read("CommsRegistryPath"),"BlockingCallTimeOut"));
                    int nMaxThreads = 0;
                    nMaxThreads=Convert.ToInt32(ConfigManager.Read("MaxThreadPoolSize"));

                    LogManager.WriteLog("[EnableDisableVLTBasedonVerfication]- MaxThreads: " + nMaxThreads.ToString(), LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("[EnableDisableVLTBasedonVerfication]- BlockingCallTimeOutValue: " + dBlockTimeOut.ToString(), LogManager.enumLogLevel.Info);
                    dTimerDelay = ((AAMSTable.Rows.Count / nMaxThreads) * dBlockTimeOut) +10000;
                    LogManager.WriteLog("[EnableDisableVLTBasedonVerfication]: RowCount: " + AAMSTable.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                    foreach (DataRow row in AAMSTable.Rows)
                    {
                        try
                        {
                            MachineEnableDisable _autoEnableDisable;
                            string EntityCommand = string.Empty;
                            int iCurrentStatus = 0;
                            int iInstallationNo = 0;
                            string strSerialno = string.Empty;
                            string strComment = string.Empty;
                            bool enterpriseStatus = false;
                            int Installation_Float_Status;

                            iCurrentStatus = row["BAD_Entity_Current_Status"] != DBNull.Value ? Convert.ToInt32(row["BAD_Entity_Current_Status"].ToString()) : 0;
                            strSerialno = row["BAD_Asset_Serial_No"] == DBNull.Value ? string.Empty : row["BAD_Asset_Serial_No"].ToString();
                            enterpriseStatus = row["BMC_Enterprise_Status"] != DBNull.Value
                                                   ? Convert.ToBoolean(row["BMC_Enterprise_Status"].ToString())
                                                   : true;

                            Installation_Float_Status = row["Installation_Float_Status"] != DBNull.Value ? Convert.ToInt32(row["Installation_Float_Status"]) : 0;
                            iInstallationNo = row["BAD_Reference_ID"] != DBNull.Value ? Convert.ToInt32(row["BAD_Reference_ID"].ToString()) : 0;

                            LogManager.WriteLog("-- Enable Disable Status for AssetSerialNumber - " + strSerialno + ", " + "- Enterprise Status :" + enterpriseStatus.ToString() + "Inside EnableDisableVLTBasedonVerfication - ", LogManager.enumLogLevel.Info);

                            _autoEnableDisable = new MachineEnableDisable()
                            {
                                Installation_No = iInstallationNo,
                                Enable = false,
                                badId = Convert.ToInt32(row["BAD_ID"]),
                                datapakCurrentState = 0,
                                entityType = 3,
                                updateDate = Convert.ToDateTime(row["BAD_Updated_Date"])
                            };

                            if (Installation_Float_Status == 1 && DBBuilder.GetSettingFromDB("DISABLE_MACHINE_ON_DROP", "FALSE").ToUpper() == "TRUE")
                            {
                                LogManager.WriteLog("Disabling the machine - " + strSerialno, LogManager.enumLogLevel.Info);
                                _autoEnableDisable.Enable = false; _autoEnableDisable.datapakCurrentState = 0;
                                ThreadPool.QueueUserWorkItem(new WaitCallback(EnableDisableMachine), _autoEnableDisable);
                                strComment = "Machine Floated and Setting DISABLE_MACHINE_ON_DROP {True} hence disabling.";
                                LogManager.WriteLog(strComment, LogManager.enumLogLevel.Info);
                                DBBuilder.UpdateCommentsForAAMS(row["BAD_ID"].ToString(), strComment, 3, 0);
                            }
                            else
                            {
                                if (enterpriseStatus)
                                {
                                    _autoEnableDisable.Enable = true; _autoEnableDisable.datapakCurrentState = 1;
                                    ThreadPool.QueueUserWorkItem(new WaitCallback(EnableDisableMachine), _autoEnableDisable);
                                    LogManager.WriteLog("Enabling the machine - " + strSerialno, LogManager.enumLogLevel.Info);
                                }
                                else
                                {
                                    _autoEnableDisable.Enable = false; _autoEnableDisable.datapakCurrentState = 0;
                                    ThreadPool.QueueUserWorkItem(new WaitCallback(EnableDisableMachine), _autoEnableDisable);
                                    strComment = "Disabling the machine  - " + strSerialno;
                                    LogManager.WriteLog(strComment, LogManager.enumLogLevel.Info);
                                    DBBuilder.UpdateCommentsForAAMS(row["BAD_ID"].ToString(), strComment, 2, 0);
                                }
                            }

                            LogManager.WriteLog("EnableDisableVLTBasedonVerfication - Completed.", LogManager.enumLogLevel.Info);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.Publish(ex);
                        }
                    }
                }
                else
                {
                    LogManager.WriteLog("No Machine to be enabled or disabled.", LogManager.enumLogLevel.Info);
                }

                return dTimerDelay;
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return 60 * 1000;
            }
        }
        //
        public void EnableDisableMachine(object _machineConfig )
        {
            int result;
            MachineConfigThreadData objMachineConfigThreadData = _machineConfig as MachineConfigThreadData;
            try
            {
#if DEBUG
                //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                //watch.Start();
                //LogManager.WriteLog("EnableMachineFromUI | "+"Method Started at : " + DateTime.Now.ToString(), LogManager.enumLogLevel.Info);
#endif
                result = objMachineConfigThreadData.Enable ? EnableMachineFromUI(objMachineConfigThreadData.Installation_No) : DisableMachineFromUI(objMachineConfigThreadData.Installation_No);
#if DEBUG
                
                //watch.Stop();
                //LogManager.WriteLog("EnableMachineFromUI | "+ "Method Ended at : " + DateTime.Now.ToString(), LogManager.enumLogLevel.Info);
                //LogManager.WriteLog("EnableMachineFromUI | " + "Time Taken : "+ watch.Elapsed.ToString(), LogManager.enumLogLevel.Info);
#endif

                if (result == 0)
                {
                    DBBuilder.UpdateAAMSStatus(objMachineConfigThreadData.badId, objMachineConfigThreadData.datapakCurrentState.ToString(), "",
                           objMachineConfigThreadData.entityType, objMachineConfigThreadData.Installation_No,
                           objMachineConfigThreadData.updateDate);

                    LogManager.WriteLog("Update Bar Position machine status", LogManager.enumLogLevel.Info);
                    DBBuilder.UpdateBarPosition(objMachineConfigThreadData.Installation_No, objMachineConfigThreadData.Enable);
                    LogManager.WriteLog(
                        "Installation No: " + objMachineConfigThreadData.Installation_No.ToString() + " --- ACK Status was True",
                        LogManager.enumLogLevel.Info);
                    
                }
                LogManager.WriteLog("Requesting command result: " + result + " --- DataPak Number:" + objMachineConfigThreadData.Installation_No, LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Enable/Disable Command Failed, DataPak Number:" + objMachineConfigThreadData.Installation_No, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }
        //
        public class MachineEnableDisable
        {
            public int Installation_No { get; set; } 
            public bool Enable { get; set; } 
            public int badId { get; set; } 
            public int datapakCurrentState { get; set; } 
            public int entityType  { get; set; } 
            public DateTime updateDate { get; set; } 
        }
        //
        public int DisableMachineFromUI(int installationNo)
        {
            ExchangeClient _ExchangeClient_DisableMachineFromUI = new ExchangeClient();
            try
            {
                _ExchangeClient_DisableMachineFromUI.InitialiseExchange(0);
                return _ExchangeClient_DisableMachineFromUI.EnableDisableMachine(1, installationNo);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("DisableMachineFromUI Failed for Installation:" + installationNo.ToString(), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }
            finally
            {
                var i = Marshal.ReleaseComObject(_ExchangeClient_DisableMachineFromUI);
                while (i > 0)
                {
                    LogManager.WriteLog("[DisableMachineFromUI]Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_ExchangeClient_DisableMachineFromUI);
                }
                LogManager.WriteLog("|=>[DisableMachineFromUI] _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);
            }

        }
        //
        public int EnableMachineFromUI(int installationNo)
        {
#if DEBUG
            //System.Diagnostics.Stopwatch watchobj = new System.Diagnostics.Stopwatch();
            //watchobj.Start();
            //LogManager.WriteLog("EnableMachineFromUI | " + "ExchangeClient Started at : " + DateTime.Now.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
#endif
            ExchangeClient _ExchangeClient_EnableMachineFromUI = new ExchangeClient();
#if DEBUG

            //watchobj.Stop();
            //LogManager.WriteLog("EnableMachineFromUI | " + "ExchangeClient Ended at : " + DateTime.Now.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
            //LogManager.WriteLog("EnableMachineFromUI | " + "ExchangeClient Time Taken : " + watchobj.Elapsed.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
#endif
            try
            {
#if DEBUG
                //System.Diagnostics.Stopwatch watchinit = new System.Diagnostics.Stopwatch();
                //watchinit.Start();
                LogManager.WriteLog("EnableMachineFromUI | " + "InitialiseExchange Started at : " + DateTime.Now.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
#endif
                _ExchangeClient_EnableMachineFromUI.InitialiseExchange(0);
#if DEBUG

                //watchinit.Stop();
                LogManager.WriteLog("EnableMachineFromUI | " + "InitialiseExchange Ended at : " + DateTime.Now.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
                //LogManager.WriteLog("EnableMachineFromUI | " + "InitialiseExchange Time Taken : " + watchobj.Elapsed.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
#endif

#if DEBUG
                //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                //watch.Start();
                LogManager.WriteLog("EnableMachineFromUI | " + "EnableDisableMachine Started at : " + DateTime.Now.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
#endif
                int nReturn = -5;
                nReturn = _ExchangeClient_EnableMachineFromUI.EnableDisableMachine(2, installationNo);
#if DEBUG

                //watch.Stop();
                LogManager.WriteLog("EnableMachineFromUI | " + "EnableDisableMachine Ended at : " + DateTime.Now.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
                //LogManager.WriteLog("EnableMachineFromUI | " + "EnableDisableMachine Time Taken : " + watch.Elapsed.ToString() + " for Installation- " + installationNo.ToString(), LogManager.enumLogLevel.Info);
#endif
                return nReturn;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EnableMachineFromUI Failed for Installation:" + installationNo.ToString(), LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return -1;
            }
            finally
            {
                var i = Marshal.ReleaseComObject(_ExchangeClient_EnableMachineFromUI);
                while (i > 0)
                {
                    LogManager.WriteLog("[EnableMachineFromUI]Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_ExchangeClient_EnableMachineFromUI);
                }
                LogManager.WriteLog("|=>[EnableMachineFromUI] _exchangeClient was released successfully for Installation: " + installationNo.ToString(), LogManager.enumLogLevel.Info);
            }
        }
        //
        internal int DisableMachine(int datapak)
        {
            int messageId;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageId = SendSector201Comexchange(datapak, 0x1, byteArray);
            }
            return messageId;
        }

        internal int EnableMachine(int datapak)
        {
            int messageId;
            lock (HoldingObject)
            {
                var byteArray = new byte[1];
                byteArray[0] = 0;
                messageId = SendSector201Comexchange(datapak, 0x2, byteArray);
                LogManager.WriteLog("Datapak = " + datapak.ToString() + " MessageID = " + messageId.ToString(),
                                    LogManager.enumLogLevel.Info);
            }
            return messageId;
        }

        private int SendSector201Comexchange(int datapak, int command, byte[] byteArray)
        {
            var sector201Data = new Sector201Data { Command = Convert.ToByte(command) };
            try
            {

                sector201Data.PutCommandDataVB(byteArray);
                _exchangeClient.RequestExWriteSector(datapak, 201, sector201Data);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1000;
            }
            finally
            {
                Marshal.ReleaseComObject(sector201Data);

            }

            return _iExchangeAdmin.LastMessageID;
        }

        #endregion

        #region eventHandlers

        void ExchangeClientServerUpdate()
        {
            _exchangeClient.RefreshActiveUPDs("");
        }

        void ExchangeClientUDPUpdate()
        {

        }

        void ExchangeClientAck(MessageAck messageAck)
        {
            try
            {

                LogManager.WriteLog("MessageID " + messageAck.MessageID + "  MessageAckResult = " + messageAck.ACK, LogManager.enumLogLevel.Info);
                lock (HoldingObject)
                {
                    var memoryList = MessageStore.Where(message => message.messageId == messageAck.MessageID);
                    LogManager.WriteLog("Memory List" + MessageStore.Count, LogManager.enumLogLevel.Info);
                    //below is for AFT
                    var aftMemoryList = AFTMessages.Where(aftmessage => aftmessage.MessageID == messageAck.MessageID);
                    LogManager.WriteLog("AFT Memory List" + AFTMessages.Count, LogManager.enumLogLevel.Info);

                    if (memoryList != null)
                    {
                        foreach (var store in memoryList)
                        {
                            if (store != null)
                            {
                                LogManager.WriteLog("store.GamePosition = " + store.GamePosition.ToString(),
                                                    LogManager.enumLogLevel.Info);
                                if (Int32.Parse(store.GamePosition) == 0)
                                {
                                    if (messageAck.ACK)
                                    {
                                        if (store.Reason == NetworkService.MessageStore.ReasonType.AAMS)
                                        {
                                            //if (store.ExportHistoryID != 0)
                                            //{
                                            //    var _proxy = GetWebService();
                                            //    _proxy.UpdateBarPositionCentralStatusBySiteID(store.ExportHistoryID);
                                            //}

                                            DBBuilder.UpdateAAMSStatus(store.badId, store.datapakCurrentState.ToString(), "",
                                                                       store.entityType, store.installationNo,
                                                                       store.updateDate);
                                            LogManager.WriteLog(
                                                "Message ID: " + messageAck.MessageID + " --- ACK Status was True",
                                                LogManager.enumLogLevel.Info);
                                        }
                                        else
                                        {
                                            LogManager.WriteLog(
                                                "This enable/disable was called due to enterprise connectivity.",
                                                LogManager.enumLogLevel.Info);
                                            dBarPositions = new Dictionary<string, string>
                                                            {
                                                                {"BarPosName", store.barpositionName},
                                                                {"isMachine", "true"}
                                                            };

                                            LogManager.WriteLog(
                                                "datapack number and Message ID : " + store.installationNo + " " +
                                                store.messageId, LogManager.enumLogLevel.Info);
                                            LogManager.WriteLog("Current machine Status " + store.datapakCurrentState,
                                                                LogManager.enumLogLevel.Info);


                                            if (store.datapakCurrentState == 1)
                                                dBarPositions.Add("Status", "1");
                                            else
                                                dBarPositions.Add("Status", "0");

                                            LogManager.WriteLog("Enabled Machine " + dBarPositions["isMachine"],
                                                                LogManager.enumLogLevel.Info);
                                            LogManager.WriteLog("Enabled Position " + dBarPositions["BarPosName"],
                                                                LogManager.enumLogLevel.Info);
                                            LogManager.WriteLog("Set Status " + dBarPositions["Status"],
                                                                LogManager.enumLogLevel.Info);

                                            var bUpdatedResult = DBBuilder.UpdateBarPosition(dBarPositions);

                                            if (bUpdatedResult)
                                            {
                                                LogManager.WriteLog("Updated Bar Position status",
                                                                    LogManager.enumLogLevel.Info);
                                                lFaults = new List<string> { store.installationNo.ToString(), "300" };

                                                if (store.datapakCurrentState == 1)
                                                    lFaults.Add("Machine Auto Enabled");
                                                else
                                                    lFaults.Add("Machine Auto Disabled");
                                                lFaults.Add("true");
                                                if (store.datapakCurrentState == 1)
                                                    lFaults.Add("101");
                                                else
                                                    lFaults.Add("100");

                                                dFaultEvents = GetFaults(lFaults);

                                                DBBuilder.CreateFaultEvents(dFaultEvents);
                                                if (store.datapakCurrentState == 1)
                                                    LogManager.WriteLog("Machine Auto Enabled -  fault event created",
                                                                        LogManager.enumLogLevel.Info);
                                                else
                                                    LogManager.WriteLog("Machine Auto Disabled -  fault event created",
                                                                        LogManager.enumLogLevel.Info);
                                            }
                                        }
                                    }
                                    else
                                        LogManager.WriteLog(
                                            "Message ID: " + messageAck.MessageID + " --- ACK Status was Failure",
                                            LogManager.enumLogLevel.Info);
                                }
                                else
                                {
                                    LogManager.WriteLog(" Before Updating Store Nack or ack. store ",
                                                        LogManager.enumLogLevel.Info);
                                    store.GameACKorNACK = messageAck.ACK;

                                    LogManager.WriteLog(
                                        " Update GameAcK or Nack to Message ID: " + messageAck.MessageID +
                                        "Game Position = " + store.GamePosition + " Installation No=" + store.installationNo,
                                        LogManager.enumLogLevel.Info);
                                }
                            }
                            else
                            {
                                LogManager.WriteLog("Store value is null", LogManager.enumLogLevel.Info);
                            }
                        }
                    }
                    if (aftMemoryList != null)
                    {
                        LogManager.WriteLog("Inside aftmemory list check", LogManager.enumLogLevel.Info);
                        foreach (var store in aftMemoryList)
                        {
                            LogManager.WriteLog("Ack status " + messageAck.ACK, LogManager.enumLogLevel.Info);
                            if (messageAck.ACK)
                            {
                                DBBuilder.UpdateAFTPolling(store.Installation_No);
                                LogManager.WriteLog("AFT enabled/disabled for the installation no :- " + store.Installation_No.ToString(), LogManager.enumLogLevel.Info);
                            }
                            else
                                LogManager.WriteLog("Message ID: " + messageAck.MessageID + " --- ACK Status was Failure for AFT Enable/Disable", LogManager.enumLogLevel.Info);
                        }
                    }
                    else
                    {
                        LogManager.WriteLog(" MessageId " + messageAck.MessageID + "Could not be found",
                                            LogManager.enumLogLevel.Info);
                    }

                    LogManager.WriteLog("Removing Messages. MessageAck = " + messageAck.MessageID,
                                                    LogManager.enumLogLevel.Info);

                    MessageStore.RemoveAll(x => x.messageId == messageAck.MessageID);
                    AFTMessages.RemoveAll(x => x.MessageID == messageAck.MessageID);

                    LogManager.WriteLog("Removed Sucessfully. MessageAck = " + messageAck.MessageID,
                                            LogManager.enumLogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void ExchangeClientExchangeSectorUpdate()
        {
            LogManager.WriteLog("Inside Exchange Sector update", LogManager.enumLogLevel.Info);

            lock (sectorHoldingobject)
            {
                object punk;
                _exchangeClient.ExchangeReadSector(out punk);

                var udPinfo = (IUDPinfo)punk;
                var udpNo = udPinfo.UDPNo;
                int MessageID = 0;
                var memoryList = MessageStore.Where(message => message.installationNo == udpNo);
                foreach (var store in memoryList)
                {
                    LogManager.WriteLog("Inside for each = " + store.installationNo.ToString(), LogManager.enumLogLevel.Info);

                    if (Int32.Parse(store.GamePosition) != 0)
                    {
                        if (punk.GetType() == typeof(Sector205Data))
                        {
                            var sector205Data = (Sector205Data)punk;
                            var counter205 = sector205Data.Get205Data;
                            var returnObjectLength = sector205Data.CommandLength;
                            if (returnObjectLength == 2)
                            {
                                DBBuilder.UpdateAAMSStatus(store.badId, "1", "Game Enable/Disable Completed for Game - " + store.GamePosition + " Installation No - " + store.installationNo.ToString(), 4, store.installationNo, DateTime.Now);
                            }
                            MessageID = store.messageId;
                        }
                    }
                }
                MessageStore.RemoveAll(x => x.messageId == MessageID);

            }
        }

        private int SendSector205_Comexchange(int lUDP, string Message)
        {
            byte cmd;
            string[] strArray;
            string StrCommand;
            int iloop;
            byte[] Data = new byte[50];
            var sector205 = new Sector205Data();
            int Lent;
            try
            {

                Message = Message + ",0";
                strArray = Message.Split(',');
                Lent = strArray.GetUpperBound(0);
                var objNumeric = new Regex("[^0-9]");
                for (iloop = 0; iloop < Lent - 1; iloop++)
                {

                    StrCommand = strArray[iloop];
                    if (objNumeric.IsMatch(StrCommand))
                    {
                        if (Int32.Parse(StrCommand) < 256)
                        {
                            cmd = byte.Parse(StrCommand);
                            Data[iloop] = cmd;
                        }
                        else
                        {
                            return -1000;
                        }
                    }
                    else
                    {
                        return -1000;
                    }

                }

                sector205.CommandLength = (byte)iloop;
                sector205.Command = Data[0];
                sector205.PutCommandDataVB(Data);

                LogManager.WriteLog("Sending sector 205", LogManager.enumLogLevel.Info);

                _exchangeClient.RequestExWriteSector(lUDP, 205, sector205);
                return _iExchangeAdmin.LastMessageID;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1000;
            }

        }

        private int SendSector203Comexchange(int datapak, byte command)
        {
            var sector203Data = new Sector203Data { Command = command };
            _exchangeClient.RequestExWriteSector(datapak, 203, sector203Data);
            return _iExchangeAdmin.LastMessageID;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_disposed) return;
            ObjectStateNotifier.RemoveObserver(this);

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
        ~AutoEnableDisable()
        {
            Dispose();
            _disposed = true;
        }

        #endregion

        public bool IsAFTEnabled()
        {
            try
            {
                return Convert.ToBoolean(DBBuilder.GetSettingFromDB("IsAFTEnabledForSite", "false"));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return false;
            }
        }

        public void GetDataforAFT()
        {
            LogManager.WriteLog("[GetDataforAFT]- Start", LogManager.enumLogLevel.Info);
            
            DataTable dtAFT;
            int Message_ID;

            try
            {
                dtAFT = DBBuilder.GetAFTPollingData();

                LogManager.WriteLog("[GetDataforAFT]- ResultSetCount: " + dtAFT.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if ((dtAFT == null) || (dtAFT.Rows.Count == 0))
                {
                    LogManager.WriteLog("No machine to be enabled/disabled for AFT", LogManager.enumLogLevel.Info);
                    return;
                }


                if (AFTMessages == null) { AFTMessages = new List<AFTMessages>(); }

                foreach (DataRow row in dtAFT.Rows)
                {
                    switch (Convert.ToInt32(row["AFT_ED_Type"]))
                    {
                        case 1:
                            Message_ID = SendSector203Comexchange(Convert.ToInt32(row["Installation_No"]), 76);
                            AFTMessages.Add(new AFTMessages() { Installation_No = Convert.ToInt32(row["Installation_No"]), MessageID = Message_ID });
                            break;
                        case 0:
                            Message_ID = SendSector203Comexchange(Convert.ToInt32(row["Installation_No"]), 77);
                            AFTMessages.Add(new AFTMessages() { Installation_No = Convert.ToInt32(row["Installation_No"]), MessageID = Message_ID });
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(10);
                }
                LogManager.WriteLog("[GetDataforAFT]- Start", LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
    }

    public class MessageStore
    {

        public int badId;
        public int messageId;
        public int datapakCurrentState;
        public int installationNo;
        public int entityType;
        public bool shouldUpdateEnterprise;
        public int ExportHistoryID;
        public DateTime InsertedDate = DateTime.Now;
        public ReasonType Reason;
        public string barpositionName = "000";
        public string GamePosition = "0000";
        public bool GameACKorNACK;
        public DateTime updateDate;
        public CommandType Command;

        public enum ReasonType
        {
            AAMS = 1,
            Connection = 2
        }

        public enum CommandType
        {
            MachineEnableCommand = 1,
            MachineDisableCommand = 2,
            GameEnableCommand = 3,
            GameDisableCommand = 4

        }
    }

    public class AFTMessages
    {
        public int MessageID;
        public int Installation_No;
    }

    public class MachineConfigThreadData : ThreadData
    {
        public int Installation_No { get; set; }
        public bool Enable { get; set; }
        public int badId { get; set; }
        public int datapakCurrentState { get; set; }
        public int entityType { get; set; }
        public DateTime updateDate { get; set; }
        public int Installation_Float_Status { get; set; }
        public bool enterprisestatus { get; set; }


        #region IThreadData Members

        public override string UniqueKey
        {
            get { return Installation_No.ToString(); }
        }

        #endregion
    }
}


