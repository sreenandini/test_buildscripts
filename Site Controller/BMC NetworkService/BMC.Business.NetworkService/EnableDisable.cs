using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComExchangeLib;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.Runtime.InteropServices;
using System.Data;
using BMC.DBInterface.NetworkService;
using System.Timers;
using BMC.Common.ConfigurationManagement;
using BMC.Transport.NetworkService;
using System.Threading;
#if NEW_EXCOMMS
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Proxies;
#endif
//using System.Threading;

namespace BMC.Business.NetworkService
{
    public class EnableDisable : ObjectStateObserver, IDisposable
    {
        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        Sector203Data sector203Data = new Sector203Data();

        public IDictionary<int, EnableDisableThreadData> dEnableDisableRequest = null;
        private ThreadDispatcher<EnableDisableThreadDataResponse> _thAckResponse = null;

        private bool _disposed;
        private object _lockRes = new object();
        private System.Timers.Timer _tmrRequest = null;

        System.Threading.ManualResetEvent mEvent = new System.Threading.ManualResetEvent(false);

        public EnableDisable()
        {
            _exchangeClient = new ExchangeClient();
            _exchangeClient.MACEnDisAck += ExchangeClientAck;
            _exchangeClient.InitialiseExchange(0);

            if (dEnableDisableRequest == null)
                dEnableDisableRequest = new SortedDictionary<int, EnableDisableThreadData>();

            _tmrRequest = new System.Timers.Timer(Int32.Parse(ConfigManager.Read("MachineConfigInterval")) * 1000);
            _tmrRequest.Elapsed += new ElapsedEventHandler(ProcessRequest);

            _thAckResponse = new ThreadDispatcher<EnableDisableThreadDataResponse>(1, "_thAckResponse");
            _thAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<EnableDisableThreadDataResponse>(this.ProcessResponse));
            _thAckResponse.Initialize();

            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);

            _tmrRequest.Start();
        }

        private void ExchangeClientAck(MessageAck messageACK)
        {
            try
            {
                _thAckResponse.AddThreadData(new EnableDisableThreadDataResponse()
                {
                    MessageID = messageACK.MessageID,
                    Ack = messageACK.ACK,
                });
                LogManager.WriteLog("ExchangeClientAck | MessageID: " + messageACK.MessageID.ToString() + ", ACK Status: " + messageACK.ACK.ToString()
                                         , LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExchangeClientAck  |   Exception Occurred", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        public override void NotifyState(ObjectState state)
        {
            base.NotifyState(state);
            if (state == ObjectState.Deactivated) mEvent.Set();
        }

        private void ProcessRequest(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;

            try
            {
                timer.Stop();
                lock (_lockRes)
                {
                    dEnableDisableRequest.Clear();
                }

                // Database hit and store this value in this list.
                this.EnableDisableMachine();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (!this.IsObjectInactive)
                {
                    timer.Start();
                }
            }
        }

        public void ProcessResponse(BMC.Business.NetworkService.EnableDisableThreadDataResponse threadData)
        {
            if (dEnableDisableRequest.Count <= 0)
                return;

            try
            {
                if (mEvent.WaitOne(NetworkServiceSettings.DBHitWaitTime))
                {
                    return;
                }

                lock (_lockRes)
                {
                    if (dEnableDisableRequest.ContainsKey(threadData.MessageID))
                    {
                        EnableDisableThreadData Requestitem = dEnableDisableRequest[threadData.MessageID];

                        //Update DB and remove from both lists
                        if (threadData.Ack == true)
                        {
                            DBBuilder.UpdateAAMSStatus(Requestitem.badID, Requestitem.datapakCurrentState.ToString(), "",
                                   3, Requestitem.InstallationNo, Requestitem.updateDate);

                            // LogManager.WriteLog("Update Bar Position machine status", LogManager.enumLogLevel.Info);
                            DBBuilder.UpdateBarPosition(Requestitem.InstallationNo, Requestitem.Enable);
                            LogManager.WriteLog("ProcessResponse_EnableDisable  |   ACK Updated for Installation:"
                                                + Requestitem.InstallationNo.ToString()
                                                +", Command:"+Requestitem.command.ToString()
                                                , LogManager.enumLogLevel.Info);

                        }
                        else
                        {
                            LogManager.WriteLog("ProcessResponse_EnableDisable  |   NACK received for Installation:"
                                                + Requestitem.InstallationNo.ToString()
                                                + ", Command:" + Requestitem.command.ToString()
                                                , LogManager.enumLogLevel.Info);
                        }
                        //LogManager.WriteLog("Requesting command result: " + Responseitem.Value.ToString() + " - Installation Number: " + Requestitem.Value.ToString(), LogManager.enumLogLevel.Info);
                        dEnableDisableRequest.Remove(threadData.MessageID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ProcessResponse_EnableDisable | Exception Occured." + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

      
        /// <summary>
        /// Entry Method for Enable/Disable Machines
        /// </summary>
        private void EnableDisableMachine()
        {
            if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
            {
                return;
            }

            DataTable dtInsatllationsToProcess;

            dtInsatllationsToProcess = DBBuilder.GetAAMSDetails(3);

            LogManager.WriteLog("EnableDisableMachine | Number of Installation to Process: " + dtInsatllationsToProcess.Rows.Count.ToString()
                                 , LogManager.enumLogLevel.Info);

            try
            {
                if (dtInsatllationsToProcess.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dtInsatllationsToProcess.Rows)
                    {
                        if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
                        {
                            break;
                        }

                        EnableDisableThreadData threadData = new EnableDisableThreadData()
                        {
                            InstallationNo = dRow["BAD_Reference_ID"] != DBNull.Value ? Convert.ToInt32(dRow["BAD_Reference_ID"].ToString()) : 0,
                            Enable = false,
                            badID = Convert.ToInt32(dRow["BAD_ID"]),
                            datapakCurrentState = 0,
                            entityType = 3,
                            Installation_Float_Status = dRow["Installation_Float_Status"] != DBNull.Value ? Convert.ToInt32(dRow["Installation_Float_Status"]) : 0,
                            enterprisestatus = dRow["BMC_Enterprise_Status"] != DBNull.Value
                                                       ? Convert.ToBoolean(dRow["BMC_Enterprise_Status"].ToString())
                                                       : true,
                            updateDate = Convert.ToDateTime(dRow["BAD_Updated_Date"])
                        };


#if DEBUG
                        LogManager.WriteLog("EnableDisableMachine | InstallationNo:" + threadData.InstallationNo.ToString() + ", EnterpriseStatus:" + threadData.enterprisestatus.ToString(), LogManager.enumLogLevel.Info);
                        
#endif
                        if (threadData.Installation_Float_Status == 1 && DBBuilder.GetSettingFromDB("DISABLE_MACHINE_ON_DROP", "FALSE").ToUpper() == "TRUE")
                        {
                            int messageID = DisableMachine(threadData.InstallationNo);

                            if (!dEnableDisableRequest.ContainsKey(messageID))
                                dEnableDisableRequest.Add(messageID, threadData);

                            threadData.Enable = false; threadData.datapakCurrentState = 0;
                            threadData.command = eCommand.Disable;

                            LogManager.WriteLog("EnableDisableMachine | Disable Request For Installation: " 
                                                + threadData.InstallationNo.ToString() + ", MessageID:" + messageID.ToString()
                                                + " ,CASE:DISABLE_MACHINE_ON_DROP"
                                                , LogManager.enumLogLevel.Info);
                            //var strComment = "Machine Floated and Setting DISABLE_MACHINE_ON_DROP {True} hence disabling.";
                            //LogManager.WriteLog(strComment, LogManager.enumLogLevel.Info);
                            //DBBuilder.UpdateCommentsForAAMS(dRow["BAD_ID"].ToString(), strComment, 3, 0);
                        }
                        else if (threadData.IsSiteLicensingEnabled && threadData.SiteLicensing_DisableGames)
                        {
                            int messageID = DisableMachine(threadData.InstallationNo);

                            if (!dEnableDisableRequest.ContainsKey(messageID))
                                dEnableDisableRequest.Add(messageID, threadData);

                            threadData.datapakCurrentState = 0;
                            threadData.command = eCommand.Disable;
                            LogManager.WriteLog("EnableDisableMachine | Disable Request For Installation: "
                                                + threadData.InstallationNo.ToString() + ", MessageID:" + messageID.ToString()
                                                + " ,CASE:SITELICENSING_DISABLEGAMES"
                                                , LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            if (threadData.enterprisestatus)
                            {
                                int messageID = EnableMachine(threadData.InstallationNo);
                                if (!dEnableDisableRequest.ContainsKey(messageID))
                                    dEnableDisableRequest.Add(messageID, threadData);

                                threadData.Enable = true; threadData.datapakCurrentState = 1;
                                threadData.command = eCommand.Enable;
                                LogManager.WriteLog("EnableDisableMachine | Enable Request for Installation: " 
                                                    + threadData.InstallationNo.ToString() + ", MessageID:" + messageID.ToString()
                                                    + " ,CASE:ENTERPRISESTATUS"
                                                    , LogManager.enumLogLevel.Info);
                            }
                            else
                            {

                                int messageID = DisableMachine(threadData.InstallationNo);
                                if (!dEnableDisableRequest.ContainsKey(messageID))
                                    dEnableDisableRequest.Add(messageID, threadData);

                                threadData.Enable = false; threadData.datapakCurrentState = 0;
                                threadData.command = eCommand.Disable;
                                LogManager.WriteLog("EnableDisableMachine | Disable Request for Installation: " 
                                                    + threadData.InstallationNo.ToString() + ", MessageID:" + messageID.ToString()
                                                    + " ,CASE:ENTERPRISESTATUS"
                                                    , LogManager.enumLogLevel.Info);

                                //var strComment = "Disabling the Installation : " + threadData.InstallationNo.ToString();
                                //DBBuilder.UpdateCommentsForAAMS(dRow["BAD_ID"].ToString(), strComment, 2, 0);
                            }
                        }

                        
                       
                    }

                }
                else
                {
                    LogManager.WriteLog("No Machines to be enabled or disabled.", LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("EnableDisableMachine | Exception Occured.", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Enable Machine- Call to Disable a Specific Installation
        /// </summary>
        /// <param name="datapak">Installation No/Datapak No</param>
        /// <returns>Unique Message ID</returns>
        /// 
        internal int EnableMachine(int InstallationNo)
        {
            

            byte[] bData = { 2 };

            sector203Data.Command = Convert.ToByte(0x80);
            sector203Data.PutCommandDataVB(bData);

            #if !NEW_EXCOMMS
            _exchangeClient.RequestExWriteSector(InstallationNo, 203, sector203Data);
            return _iExchangeAdmin.LastMessageID;
            #else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = InstallationNo,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_EnableDisableMachineNW { });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
            #endif
        }

        /// <summary>
        /// Disable Machine- Call to Disable a Specific Installation
        /// </summary>
        /// <param name="datapak">Installation No/Datapak No</param>
        /// <returns>Unique Message ID</returns>
        /// 
        internal int DisableMachine(int InstallationNo)
        {
            byte[] bData = { 1 };

            sector203Data.Command = Convert.ToByte(0x80);
            sector203Data.PutCommandDataVB(bData);

            #if !NEW_EXCOMMS
            _exchangeClient.RequestExWriteSector(InstallationNo, 203, sector203Data);
            return _iExchangeAdmin.LastMessageID;
            #else
                        MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = InstallationNo,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_EnableDisableMachineNW { });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
            #endif

        }


        #region IDisposable Members

        public void Dispose()
        {
            if (_disposed) return;
            ObjectStateNotifier.RemoveObserver(this);

            try
            {
                var i = Marshal.ReleaseComObject(_exchangeClient);
                Thread.Sleep(10);
                while (i > 0)
                {
                    LogManager.WriteLog("Number of objects in _exchangeClient = " + i, LogManager.enumLogLevel.Info);
                    i = Marshal.ReleaseComObject(_exchangeClient);
                }
                LogManager.WriteLog("|=> _exchangeClient was released successfully.", LogManager.enumLogLevel.Info);

                i = Marshal.ReleaseComObject(_iExchangeAdmin);
                Thread.Sleep(10);
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
        ~EnableDisable()
        {
            Dispose();
            _disposed = true;
        }

        #endregion



    }

    public class EnableDisableThreadDataResponse : ThreadData
    {
        //public int InstallationNo { get; set; }
        public int MessageID { get; set; }
        public bool Ack { get; set; }

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return MessageID.ToString(); }
        }

        #endregion
    }

    public class EnableDisableThreadData : ThreadData
    {
        public int badID { get; set; }
        public int InstallationNo { get; set; }
        public int messageID { get; set; }
        public bool Enable { get; set; }
        public eCommand command;
        public int datapakCurrentState { get; set; }
        public int entityType { get; set; }
        public DateTime updateDate { get; set; }
        public int Installation_Float_Status { get; set; }
        public bool enterprisestatus { get; set; }
        public bool Ack { get; set; }
        public bool IsSiteLicensingEnabled { get; set; }
        public bool SiteLicensing_DisableGames { get; set; }

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return InstallationNo.ToString(); }
        }

        #endregion
    }
}
