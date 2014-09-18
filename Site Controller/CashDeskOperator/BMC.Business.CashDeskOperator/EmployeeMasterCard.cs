using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComExchangeLib;
using BMC.Common.ConfigurationManagement;
using System.Timers;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.Transport;

using System.Data;

using System.Runtime.InteropServices;
using System.Threading;
using BMC.DBInterface.CashDeskOperator;

namespace BMC.Business.CashDeskOperator
{
    public class EmployeeMasterCard : ObjectStateObserver, IDisposable
    {
        private ExchangeClient _exchangeClient;
        private IExchangeAdmin _iExchangeAdmin;
        Sector203Data m_SectorData = new Sector203Data();

        public IDictionary<int, EmployeeMasterCardThreadData> _requestCollection = null;

        public List<Employeecarddata> _emppollingCollection = null;


        private bool _disposed;
        private object _lockRes = new object();

        private ThreadDispatcher<EmployeeMasterCardThreadDataResponse> _thAckResponse = null;
        private Thread _thRequest = null;

        System.Threading.ManualResetEvent mEvent = new System.Threading.ManualResetEvent(false);

        public EmployeeMasterCard()
        {
            _exchangeClient = new ExchangeClient();
            _exchangeClient.EMP_DATA_ACK += new _IExchangeClientEvents_EMP_DATA_ACKEventHandler(_exchangeClient_EMP_DATA_ACK);
            _exchangeClient.InitialiseExchange(0);

            if (_requestCollection == null)
                _requestCollection = new SortedDictionary<int, EmployeeMasterCardThreadData>();

            if (_emppollingCollection == null)
                _emppollingCollection = new List<Employeecarddata>();


            _thRequest = new Thread(new ThreadStart(ProcessRequest));
            _thRequest.Start();


            _thAckResponse = new ThreadDispatcher<EmployeeMasterCardThreadDataResponse>(1, "_thAckResponse_EmployeeMasterCard");
            _thAckResponse.AddProcessThreadData(new ProcessThreadDataHandler<EmployeeMasterCardThreadDataResponse>(this.ProcessResponse));
            _thAckResponse.Initialize();

            _iExchangeAdmin = (IExchangeAdmin)_exchangeClient;
            ObjectStateNotifier.AddObserver(this);

        }

        void _exchangeClient_EMP_DATA_ACK(MessageAck messageACK)
        {
            _thAckResponse.AddThreadData(new EmployeeMasterCardThreadDataResponse()
            {
                MessageID = messageACK.MessageID,
                Ack = messageACK.ACK,
            });
            LogManager.WriteLog("ExchangeClientAck(Employee card event) | MessageID: " +
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
                lock (_lockRes)
                {
                    _requestCollection.Clear();
                    _emppollingCollection.Clear();
                }

                // Database hit and store this value in this list.
                this.GetDataforEmployeecard();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
            }
        }

        public void ProcessResponse(EmployeeMasterCardThreadDataResponse threadData)
        {
            if (_requestCollection.Count <= 0)
                return;

            lock (_lockRes)
            {
              
                if (_requestCollection.ContainsKey(threadData.MessageID))
                {
                    EmployeeMasterCardThreadData Requestitem = _requestCollection[threadData.MessageID];

                    if (threadData.Ack)
                    {
                        CommonDataAccess.UpdateEmployeeCardPolling(Requestitem.EmployeeCardNo, Requestitem.InstallationNo);
                        LogManager.WriteLog("ProcessResponse_EmployeeMasterCard  |   ACK Updated for EmployeeCard:"
                                                + Requestitem.EmployeeCardNo.ToString()
                                                + "| in Installation No " + Requestitem.InstallationNo.ToString()
                                                , LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        LogManager.WriteLog("ProcessResponse_EmployeeMasterCard  |   NACK received for EmployeeCard:"
                                                + Requestitem.EmployeeCardNo.ToString()
                                                + "| in Installation No " + Requestitem.InstallationNo.ToString()
                                               , LogManager.enumLogLevel.Info);
                    }
                    _emppollingCollection.Add(new Employeecarddata
                    {
                        EmployeeCard = Requestitem.EmployeeCardNo,
                        Installation_No = Requestitem.InstallationNo,
                        PollingStatus = threadData.Ack
                    });

                    _requestCollection.Remove(threadData.MessageID);
                }
            }
        }

        public void GetDataforEmployeecard()
        {  

            LogManager.WriteLog("[GetDataforEmployeecard]- Start", LogManager.enumLogLevel.Info);

            DataTable dtEMPCard;
            int Message_ID = 0;

            try
            {
                dtEMPCard = CommonDataAccess.GetEmployeeCardPollingData();

                LogManager.WriteLog("[GetDataforEmployeecard] | Number of Employeecards to Process: " +
                    dtEMPCard.Rows.Count.ToString(), LogManager.enumLogLevel.Info);

                if ((dtEMPCard == null) || (dtEMPCard.Rows.Count == 0))
                {
                    LogManager.WriteLog("No cards to be broadcasted", LogManager.enumLogLevel.Info);
                    return;
                }

                foreach (DataRow row in dtEMPCard.Rows)
                {
                    EmployeeMasterCardThreadData threadData = new EmployeeMasterCardThreadData()
                    {
                        EmployeeCardNo = row["EmployeeCard"].ToString(),
                        EmployeeFlags = row["EmployeeFlags"].ToString(),
                        InstallationNo = Convert.ToInt32(row["Installation_No"])
                    };

                    bool canAdd = true;


                    Message_ID = EmployeecardSend(threadData.EmployeeCardNo, threadData.EmployeeFlags, threadData.InstallationNo);
                    LogManager.WriteLog("GetDataforEmployeecard | Master card Information " + threadData.EmployeeCardNo.ToString()
                            + "sent to Installation: " + threadData.InstallationNo.ToString()
                                        + ", MessageID: " + Message_ID.ToString()
                                        , LogManager.enumLogLevel.Info);


                    if (canAdd)
                    {
                        threadData.MessageID = Message_ID;
                        if (!_requestCollection.ContainsKey(Message_ID))
                            _requestCollection.Add(Message_ID, threadData);
                    }
                }
                LogManager.WriteLog("[GetDataforEmployeecard]- End", LogManager.enumLogLevel.Info);
            }
            catch (Exception Ex)

            {
                ExceptionManager.Publish(Ex);
            }
        }




        public int EmployeecardSend(string EmpCardNo, string EmployeeFlags, int InstallationNo)
        {
            try
            {
                m_SectorData.Command = 0x84;
                string EmpFlags = EmployeeFlags.Substring(2);
                List<byte> enumver = Enumerable.Range(0, EmpFlags.Length)
                       .Where(x => x % 2 == 0)
                       .Select(x => Convert.ToByte(EmpFlags.Substring(x, 2), 16)).ToList();

                List<byte> cardno = Enumerable.Range(0, EmpCardNo.PadLeft(10, '0').Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(EmpCardNo.PadLeft(10, '0').Substring(x, 2), 16)).ToList();

                enumver.Insert(0, Convert.ToByte(EmployeeFlags.Substring(0, 1)));
                enumver.Insert(1, Convert.ToByte(EmployeeFlags.Substring(1, 1)));
                enumver.InsertRange(0, cardno);

                byte[] bData = enumver.ToArray();

                m_SectorData.PutCommandDataVB(bData);

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
        ~EmployeeMasterCard()
        {
            Dispose();
            _disposed = true;
        }

        #endregion



    }

    public class EmployeeMasterCardThreadDataResponse : ThreadData
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

    public class EmployeeMasterCardThreadData : ThreadData
    {
        public string EmployeeCardNo { get; set; }
        public int MessageID { get; set; }
        public string EmployeeFlags { get; set; }
        public int InstallationNo { get; set; }

        
        #region IThreadData Members

        public override string UniqueKey
        {
            get { return EmployeeCardNo.ToString(); }
        }



        #endregion
    }


}

