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
    public class AFTEnableDisable : ObjectStateObserver, IDisposable
    {
        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        Sector203Data m_SectorData = new Sector203Data();

        public IDictionary<int, AFTEnableDisableThreadData> _requestCollection = null;

        private bool _disposed;
        private object _lockRes = new object();

        private ThreadDispatcher<AFTEnableDisableThreadDataResponse> _thAckResponse = null;
        private System.Timers.Timer _tmrRequest = null;

        System.Threading.ManualResetEvent mEvent = new System.Threading.ManualResetEvent(false);

        public AFTEnableDisable()
        {
            _exchangeClient = new ExchangeClient();
            _exchangeClient.AFTAck += ExchangeClientAck;
            _exchangeClient.InitialiseExchange(0);

            if (_requestCollection == null)
                _requestCollection = new SortedDictionary<int, AFTEnableDisableThreadData>();

            _tmrRequest = new System.Timers.Timer(Int32.Parse(ConfigManager.Read("AFTTimeInterval")) * 1000);
            _tmrRequest.Elapsed += new ElapsedEventHandler(ProcessRequest);

            _thAckResponse = new ThreadDispatcher<AFTEnableDisableThreadDataResponse>(1, "_thAckResponse_AFTEnableDisable");
            _thAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<AFTEnableDisableThreadDataResponse>(this.ProcessResponse));
            _thAckResponse.Initialize();

            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);

            _tmrRequest.Start();
        }

        private void ExchangeClientAck(MessageAck messageACK)
        {
            _thAckResponse.AddThreadData(new AFTEnableDisableThreadDataResponse()
            {
                MessageID = messageACK.MessageID,
                Ack = messageACK.ACK,
            });
            LogManager.WriteLog("ExchangeClientAck(AFT) | MessageID: " + messageACK.MessageID.ToString() + ", ACK Status: " + messageACK.ACK.ToString()
                                     , LogManager.enumLogLevel.Info);
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
                    _requestCollection.Clear();
                }

                // Database hit and store this value in this list.
                this.GetDataforAFT();
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

        public void ProcessResponse(BMC.Business.NetworkService.AFTEnableDisableThreadDataResponse threadData)
        {
            if (_requestCollection.Count <= 0)
                return;

            lock (_lockRes)
            {
                if (mEvent.WaitOne(NetworkServiceSettings.DBHitWaitTime))
                {
                    return;
                }
                if (_requestCollection.ContainsKey(threadData.MessageID))
                {
                    AFTEnableDisableThreadData Requestitem = _requestCollection[threadData.MessageID];
                  

                    if (threadData.Ack)
                    {
                        DBBuilder.UpdateAFTPolling(Requestitem.InstallationNo);

                        LogManager.WriteLog("ProcessResponse_AFTEnableDisable  |   ACK Updated for Installation:"
                                                + Requestitem.InstallationNo.ToString()
                                                +", Command:"+Requestitem.Command.ToString()
                                                , LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("ProcessResponse_AFTEnableDisable  |   NACK received for Installation:"
                                               + Requestitem.InstallationNo.ToString()
                                               + ", Command:" + Requestitem.Command.ToString()
                                               , LogManager.enumLogLevel.Info);
                    }

                    _requestCollection.Remove(threadData.MessageID);
                }
            }
        }

        public void GetDataforAFT()
        {
            if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
            {
                return;
            }

            LogManager.WriteLog("[GetDataforAFT]- Start", LogManager.enumLogLevel.Info);

            DataTable dtAFT;
            int Message_ID = 0;

            try
            {
                dtAFT = DBBuilder.GetAFTPollingData();

                LogManager.WriteLog("[GetDataforAFT] | Number of Installation to Process: " + dtAFT.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if ((dtAFT == null) || (dtAFT.Rows.Count == 0))
                {
                    LogManager.WriteLog("No machine to be enabled/disabled for AFT", LogManager.enumLogLevel.Info);
                    return;
                }

                foreach (DataRow row in dtAFT.Rows)
                {
                    if (mEvent.WaitOne(NetworkServiceSettings.RequestWaitTime))
                    {
                        break;
                    }

                    AFTEnableDisableThreadData threadData = new AFTEnableDisableThreadData()
                    {
                        InstallationNo = Convert.ToInt32(row["Installation_No"])
                    };

                    bool canAdd = true;
                    
                    switch (Convert.ToInt32(row["AFT_ED_Type"]))
                    {
                        case 1:
                            threadData.Command = eCommand.Enable;
                            Message_ID = AFTEnable(threadData.InstallationNo);
                            LogManager.WriteLog("GetDataforAFT | AFT Enable Request for Installation: " 
                                                + threadData.InstallationNo.ToString()
                                                + ", MessageID: " + Message_ID.ToString()
                                                , LogManager.enumLogLevel.Info);
                            break;
                        case 0:
                            threadData.Command = eCommand.Disable;
                            Message_ID = AFTDisable(threadData.InstallationNo);
                            LogManager.WriteLog("GetDataforAFT | AFT Disable Request for Installation: " 
                                                + threadData.InstallationNo.ToString()
                                                + ", MessageID: " + Message_ID.ToString()
                                                , LogManager.enumLogLevel.Info);
                            break;

                        default:
                            canAdd = false;
                            break;
                    }

                    if (canAdd)
                    {
                        threadData.MessageID = Message_ID;
                        if (!_requestCollection.ContainsKey(Message_ID))
                            _requestCollection.Add(Message_ID, threadData);
                    }
                }
                LogManager.WriteLog("[GetDataforAFT]- End", LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        


        public int AFTEnable(int _installation_no)
        {

            m_SectorData.Command = 0x4C;
            byte[] bData = { };
            m_SectorData.PutCommandDataVB(bData);
          
            #if !NEW_EXCOMMS
            _exchangeClient.RequestExWriteSector(_installation_no, 203, m_SectorData);
            return _iExchangeAdmin.LastMessageID;
            #else      
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = _installation_no,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_SystemEnable { });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(monMsg_H2G) ? 0 : -1;
            #endif
        }

        public int AFTDisable(int _installation_no)
        {

            m_SectorData.Command = 0x4D;
            byte[] bData = { };
            m_SectorData.PutCommandDataVB(bData);

            #if !NEW_EXCOMMS
            _exchangeClient.RequestExWriteSector(_installation_no, 203, m_SectorData);
            return _iExchangeAdmin.LastMessageID;
            #else
            MonMsg_H2G monMsg_H2G = new MonMsg_H2G()
            {
                InstallationNo = _installation_no,
            };
            monMsg_H2G.Targets.Add(new MonTgt_H2G_SystemDisbale { });
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
        ~AFTEnableDisable()
        {
            Dispose();
            _disposed = true;
        }

        #endregion



    }

    public class AFTEnableDisableThreadDataResponse : ThreadData
    {
        public int MessageID { get; set; }
        public bool Ack { get; set; }

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return MessageID.ToString(); }
        }

        #endregion
    }

    public class AFTEnableDisableThreadData : ThreadData
    {
        public int InstallationNo { get; set; }
        public int MessageID { get; set; }
        //public bool Enable { get; set; }
        public eCommand Command;

        #region IThreadData Members

        public override string UniqueKey
        {
            get { return InstallationNo.ToString(); }
        }

        #endregion
    }

  
}
