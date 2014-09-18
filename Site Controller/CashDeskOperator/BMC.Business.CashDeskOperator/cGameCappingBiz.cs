using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Security;
using BMC.Transport;
using ComExchangeLib;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace BMC.Business.CashDeskOperator
{
    public class GameCappingBiz
    {
        private DAGameCapping _DAGameCapping = new DAGameCapping(CommonDataAccess.ExchangeConnectionString);

        public List<GameCapDetails> GetGameCapDetails()
        {
            List<GameCapDetails> rsltGameCapDetails = null;
            ISingleResult<rsp_GetGameCapDetailsResult> result = null;

            try
            {
                result = _DAGameCapping.GetGameCapDetails();

                rsltGameCapDetails = (from o in result
                                      select new GameCapDetails()
                                      {
                                          GameCappingID = o.GameCappingID,
                                          InstallationNo = o.InstallationNo,
                                          Position = o.Position,
                                          ReservedBy = o.ReservedBy,
                                          ReservedFor = o.ReservedFor,
                                          SessionStartTime = o.SessionStartTime,
                                          ElapsedSec = o.ElapsedSec,
                                          AlertCame = o.AlertCame,
                                          AlertUnCapSec = o.AlertUnCapSec,
                                          IsEnabled = true,
                                          Status = string.Empty
                                      }).ToList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltGameCapDetails;
        }

        public GameCapResult UpdateGameCapDetails(int GameCappingID, string UserName)
        {
            GameCapResult rsltGameCapResult = null;
            ISingleResult<usp_UpdateGameCapDetailsResult> result = null;

            try
            {
                result = _DAGameCapping.UpdateGameCapDetails(0, GameCappingID, "", UserName);

                rsltGameCapResult = (from o in result
                                     select new GameCapResult()
                                     {
                                         Message =o.Message,
                                         ReserveGameAsset = o.ReserveGameAsset,
                                         MaxCapNotExceeded = o.MaxCapNotExceeded,
                                         SelfReserve = o.SelfReserve,
                                         AllowReserve = o.AllowReserve,
                                         TimeOption = o.TimeOption,
                                         AutoRelease = o.AutoRelease,
                                         ExpireMinstoAlert = o.ExpireMinstoAlert
                                     }).First();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltGameCapResult;
        }

        public GameCapResult ValidateGameCapDetails(int InstallationNo)
        {
            GameCapResult rsltGameCapResult = null;
            ISingleResult<rsp_ValidateGameCapInformationResult> result = null;

            try
            {
                result = _DAGameCapping.ValidateGameCapInformation(InstallationNo,String.Empty,true);

                rsltGameCapResult = (from o in result
                                     select new GameCapResult()
                                     {
                                         Message = o.Message,
                                         ReserveGameAsset = o.ReserveGameAsset,
                                         MaxCapNotExceeded = o.MaxCapNotExceeded,
                                         SelfReserve = o.SelfReserve,
                                         AllowReserve = o.AllowReserve,
                                         TimeOption = o.TimeOption,
                                         AutoRelease = o.AutoRelease,
                                         ExpireMinstoAlert = o.ExpireMinstoAlert
                                     }).First();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltGameCapResult;
        }        
    }
    
    public class RleaseGameCap : ObjectStateObserver, IDisposable
    {
        public ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        Sector203Data m_SectorData = null;

        public IDictionary<int, EnableMachineThreadData> _requestCollection = null;

        private List<GameCapDetails> _lstGameCapDetails = null;

        private GameCappingBiz oGameCappingBiz = null;

        private bool _disposed;
        private object _lockRes = new object();

        private ThreadDispatcher<EnableMachineThreadDataResponse> _thAckResponse = null;
        private Thread _thRequest = null;

        ManualResetEvent mEvent = new ManualResetEvent(false);
        
        ManualResetEvent mRefreshEvent = new ManualResetEvent(false);

        public ManualResetEvent StopRelease { get { return this.mEvent; } }

        public ManualResetEvent PauseRelease { get { return this.mRefreshEvent; } }

        public List<GameCapDetails> GameCapRelease
        { 
            get { return this._lstGameCapDetails; } 
            
            set 
            { 
                if (this._lstGameCapDetails != value) 
                { this._lstGameCapDetails = value; }
            } 
        }

        public RleaseGameCap()
        {
            _exchangeClient = new ExchangeClient();
            _exchangeClient.MACEnDisAck += new _IExchangeClientEvents_MACEnDisAckEventHandler(_exchangeClient_MacEnable_DATA_ACK);
            _exchangeClient.InitialiseExchange(0);

            if (_requestCollection == null)
                _requestCollection = new SortedDictionary<int, EnableMachineThreadData>();

            if (m_SectorData == null)
                m_SectorData = new Sector203Data();
            
            if(oGameCappingBiz == null)
                oGameCappingBiz= new GameCappingBiz();
            
            if (_lstGameCapDetails == null)
                _lstGameCapDetails = new List<GameCapDetails>();
            
            _thRequest = new Thread(new ThreadStart(ProcessRequest));

            _thAckResponse = new ThreadDispatcher<EnableMachineThreadDataResponse>(1, "_thAckResponse_EnableMachine");
            _thAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<EnableMachineThreadDataResponse>(this.ProcessResponse));
            _thAckResponse.Initialize();

            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);

            _thRequest.Start();
        }

        void _exchangeClient_MacEnable_DATA_ACK(MessageAck messageACK)
        {
            _thAckResponse.AddThreadData(new EnableMachineThreadDataResponse()
            {
                MessageID = messageACK.MessageID,
                Ack = messageACK.ACK,
            });

            LogManager.WriteLog("ExchangeClientAck(Machine Enable event) | MessageID: " +
                messageACK.MessageID.ToString() + ", ACK Status: " + messageACK.ACK.ToString()
                                     , LogManager.enumLogLevel.Info);
        }

        public override void NotifyState(ObjectState state)
        {
            base.NotifyState(state);
            if (state == ObjectState.Deactivated) mEvent.Set();
        }

        private void ProcessRequest()
        {
            try
            {
                int Message_ID = 0;

                lock (_lockRes)
                {
                    _requestCollection.Clear();
                }

                while (true)
                {
                    //Exit thread when screen unloaded
                    if (mEvent.WaitOne(100))
                        break;

                    //Waits for new game cap release data from UI
                    PauseRelease.WaitOne();

                    //Skip release command if there is no session to uncap
                    if (_lstGameCapDetails.Count == 0)
                        continue;

                    List<GameCapDetails> lstTmp = _lstGameCapDetails.Where(item => item.Status == "Processing").ToList();

                    foreach (var item in lstTmp)
                    {
                        EnableMachineThreadData threadData = new EnableMachineThreadData()
                        {
                            InstallationNo = item.InstallationNo,
                            GameCapID = item.GameCappingID
                        };

                        Message_ID = SendEnableMachine(threadData.InstallationNo);
                        LogManager.WriteLog("Enable machine is sent to Installation: " + threadData.InstallationNo.ToString()
                                            + ", MessageID: " + Message_ID.ToString()
                                            , LogManager.enumLogLevel.Info);
                        item.Status = "Send";

                        threadData.MessageID = Message_ID;
                        if (!_requestCollection.ContainsKey(Message_ID))
                            _requestCollection.Add(Message_ID, threadData);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void ProcessResponse(EnableMachineThreadDataResponse threadData)
        {
            if (_requestCollection.Count <= 0)
                return;

            lock (_lockRes)
            {
                if (_requestCollection.ContainsKey(threadData.MessageID))
                {
                    EnableMachineThreadData Requestitem = _requestCollection[threadData.MessageID];

                    if (threadData.Ack)
                    {
                        LogManager.WriteLog("ProcessResponse_EnableMachine  |   ACK Updated for Installation No " + Requestitem.InstallationNo.ToString()
                                                , LogManager.enumLogLevel.Info);
                        
                        GameCapResult oGameCapResult = oGameCappingBiz.UpdateGameCapDetails(Requestitem.GameCapID, SecurityHelper.CurrentUser.UserName);
                        LogManager.WriteLog("Game capping session closed Status [Installation No : " + Requestitem.InstallationNo.ToString() + "] : " + oGameCapResult.Message, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("ProcessResponse_EnableMachine  |   NACK received for Installation No " + Requestitem.InstallationNo.ToString()
                                               , LogManager.enumLogLevel.Info);
                    }

                    _requestCollection.Remove(threadData.MessageID);
                }
            }
        }

        public int SendEnableMachine(int InstallationNo)
        {
            try
            {
                m_SectorData.Command = 0x80;

                byte[] _bCmdData = new byte[1];
                _bCmdData[0] = 2;

                m_SectorData.PutCommandDataVB(_bCmdData);

                _exchangeClient.RequestExWriteSector(InstallationNo, 203, m_SectorData);
                return _iExchangeAdmin.LastMessageID;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }

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
        ~RleaseGameCap()
        {
            Dispose();
            _disposed = true;
        }

        #endregion


        public void Shutdown()
        {
            _thAckResponse.WaitAll(false, null);
        }
    }

    public class EnableMachineThreadDataResponse : ThreadData
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

    public class EnableMachineThreadData : ThreadData
    {        
        public int MessageID { get; set; }
        public int GameCapID { get; set; }
        public int InstallationNo { get; set; }


        #region IThreadData Members

        public override string UniqueKey
        {
            get { return GameCapID.ToString(); }
        }
        
        #endregion
    }
}
