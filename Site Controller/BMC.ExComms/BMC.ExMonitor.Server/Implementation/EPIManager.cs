using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExMonitor.Server.Handlers.EPI;
using System.Threading.Tasks;
using BMC.ExComms.DataLayer.MSSQL;
using System.Data;
using BMC.ExMonitor.Server.Handlers;

namespace BMC.ExMonitor.Server
{
    public enum EPIProcessType
    {
        Process = 0,
        TimeOut = 1,
        IntervalRating = 2,
    }

    enum EPIMeterTypes
    {
        GamesPlayed = 0,
        GamesWon,
        GamesLost,
        CoinsIn,
        CoinsOut,
        Handpay,
        Jackpot,
        BillsIn,
        VouchersIn,
        VouchersOut,
        NonCashableEFTIn,
        NonCashableEFTOut,
        CashableEFTIn,
        CashableEFTOut,
    }

    enum EPIMeterValueTypes
    {
        Start = 0,
        Current = 1,
        Diff = 2,
    }

    class EPITimeout
        : DisposableObject
    {
        public EPITimeout() { }

        public EPITimeout(int installationNo, EPIProcessType processType, DateTime lastUpdatedTime)
        {
            this.InstallationNo = installationNo;
            this.ProcessType = processType;
            this.LastUpdatedTime = lastUpdatedTime;
        }

        public int InstallationNo;

        public string _Type;

        public EPIProcessType ProcessType { get; set; }

        public DateTime LastUpdatedTime;

        public long IsInProcess;
    }

    class EPIMeterValue
        : DisposableObject
    {
        private EPIMeterTypes _meterType = EPIMeterTypes.GamesPlayed;

        internal EPIMeterValue(EPIMeterTypes meterType)
        {
            _meterType = meterType;
        }

        public long this[EPIMeterValueTypes valueType]
        {
            get
            {
                if (valueType == EPIMeterValueTypes.Current) return this.ValueCurrent;
                else if (valueType == EPIMeterValueTypes.Diff) return this.ValueDiff;
                else return this.ValueStart;
            }
            set
            {
                if (valueType == EPIMeterValueTypes.Current) this.ValueCurrent = value;
                else if (valueType == EPIMeterValueTypes.Diff) this.ValueDiff = value;
                else this.ValueStart = value;
            }
        }

        private long _valueStart = 0;
        public long ValueStart
        {
            get { return _valueStart; }
            set
            {
                _valueStart = value;
                _valueCurrent = value;
                _valueDiff = 0;
            }
        }

        private long _valueCurrent = 0;
        public long ValueCurrent
        {
            get { return _valueCurrent; }
            set { _valueCurrent = value; }
        }

        private long _valueDiff = 0;
        public long ValueDiff
        {
            get { return _valueDiff; }
            set { _valueDiff = value; }
        }

        public void Clear()
        {
            this.ValueStart = 0;
        }

        public override string ToString()
        {
            return string.Format("[({0}) S=>{1:D}, C=>{2:D}, D=>{3:D}]", _meterType.ToString(), _valueStart, _valueCurrent, _valueDiff);
        }
    }

    class EPIMeterValueDictionary
        : SortedDictionary<EPIMeterTypes, EPIMeterValue>, IDisposable
    {
        private readonly string DYN_MODULE_NAME = "EPIMeterValueDictionary";

        internal EPIMeterValueDictionary()
        {
            this.InitEPIMeterValues();
        }

        private void InitEPIMeterValues()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "InitEPIMeterValues"))
            {
                try
                {
                    Array enumValues = Enum.GetValues(typeof(EPIMeterTypes));
                    foreach (var enumValue in enumValues)
                    {
                        this.Add((EPIMeterTypes)enumValue, new EPIMeterValue((EPIMeterTypes)enumValue));
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public long this[EPIMeterValueTypes valueType, EPIMeterTypes meterType]
        {
            get
            {
                return this[meterType][valueType];
            }
            set { this[meterType][valueType] = value; }
        }

        public long GetMeterValue(EPIMeterTypes meterType)
        {
            return this[EPIMeterValueTypes.Start, meterType];
        }

        public void CopyMeterValuesFromDB(FloorFinancialsResult floorFinancial, EPIMeterValueTypes valueType)
        {
            this[valueType, EPIMeterTypes.GamesPlayed] = (long)floorFinancial.Games_Bet.SafeValue();
            this[valueType, EPIMeterTypes.GamesWon] = (long)floorFinancial.Games_Won.SafeValue();
            this[valueType, EPIMeterTypes.GamesLost] = (long)floorFinancial.Games_Lost.SafeValue();
            this[valueType, EPIMeterTypes.CoinsIn] = (long)floorFinancial.Coins_In.SafeValue();
            this[valueType, EPIMeterTypes.CoinsOut] = (long)floorFinancial.Coins_Out.SafeValue();
            this[valueType, EPIMeterTypes.Handpay] = (long)floorFinancial.Handpay.SafeValue();
            this[valueType, EPIMeterTypes.Jackpot] = (long)floorFinancial.Jackpot.SafeValue();
            this[valueType, EPIMeterTypes.BillsIn] = (long)floorFinancial.BillsIn.SafeValue();
            this[valueType, EPIMeterTypes.VouchersIn] = (long)floorFinancial.VouchersIn.SafeValue();
            this[valueType, EPIMeterTypes.VouchersOut] = (long)floorFinancial.TicketOut.SafeValue();
            this[valueType, EPIMeterTypes.NonCashableEFTIn] = (long)floorFinancial.NoncashableEFTIN.SafeValue();
            this[valueType, EPIMeterTypes.NonCashableEFTOut] = (long)floorFinancial.NonCashableEFTOut.SafeValue();
            this[valueType, EPIMeterTypes.CashableEFTIn] = (long)floorFinancial.CashableEFTIn.SafeValue();
            this[valueType, EPIMeterTypes.CashableEFTIn] = (long)floorFinancial.CashableEFTOut.SafeValue();
        }

        public void CopyMeterValuesFromDB(DataRow floorFinancial, EPIMeterValueTypes valueType)
        {
            this[valueType, EPIMeterTypes.GamesPlayed] = floorFinancial.Field<long>("GamesPlayed");
            this[valueType, EPIMeterTypes.GamesWon] = floorFinancial.Field<long>("GamesWon");
            this[valueType, EPIMeterTypes.GamesLost] = floorFinancial.Field<long>("GamesLost");
            this[valueType, EPIMeterTypes.CoinsIn] = floorFinancial.Field<long>("CoinsIn");
            this[valueType, EPIMeterTypes.CoinsOut] = floorFinancial.Field<long>("CoinsOut");
            this[valueType, EPIMeterTypes.Jackpot] = floorFinancial.Field<long>("Jackpot");
            this[valueType, EPIMeterTypes.BillsIn] = floorFinancial.Field<long>("BillsIn");
            this[valueType, EPIMeterTypes.VouchersIn] = floorFinancial.Field<long>("VouchersIn");
            this[valueType, EPIMeterTypes.NonCashableEFTIn] = floorFinancial.Field<long>("NonCashableEFTIn");
            this[valueType, EPIMeterTypes.NonCashableEFTOut] = floorFinancial.Field<long>("NonCashableEFTOut");
            this[valueType, EPIMeterTypes.CashableEFTIn] = floorFinancial.Field<long>("CashableEFTIn");
            this[valueType, EPIMeterTypes.CashableEFTOut] = floorFinancial.Field<long>("CashableEFTOut");
        }

        public bool GetLatestMeters(int installationNo, EPIMeterValueTypes valueType)
        {
            // get the floor financial
            var dbFloorFinancial = ExCommsDataContext.Current.GetFloorFinancials(installationNo).FirstOrDefault();
            if (dbFloorFinancial == null) return false;
            this.CopyMeterValuesFromDB(dbFloorFinancial, valueType);
            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    class EPICardDetail
        : DisposableObject
    {
        private string _lastCardNo = string.Empty;
        private string _cardNo = string.Empty;
        private bool _isFirstCardIn = false;
        private bool _isCardIn = false;
        private bool _isCardOut = false;

        private readonly EPIMeterValueDictionary _epiMeterValues = new EPIMeterValueDictionary();

        internal EPICardDetail(int installationNo)
        {
            this.InstallationNo = installationNo;
        }

        public long this[EPIMeterValueTypes valueType, EPIMeterTypes meterType]
        {
            get { return _epiMeterValues[valueType, meterType]; }
            set { _epiMeterValues[valueType, meterType] = value; }
        }

        public int InstallationNo { get; set; }

        public string CardNo
        {
            get { return _cardNo; }
            set
            {
                if (_cardNo.IsEmpty())
                {
                    _isFirstCardIn = true;
                }

                if (value != _cardNo &&
                    !value.IsEmpty())
                {
                    _cardNo = value;
                    _isCardIn = !value.IsEmpty();
                    _isCardOut = value.IsEmpty();
                }
            }
        }

        public bool IsFirstCardIn
        {
            get { return _isFirstCardIn; }
            set { _isFirstCardIn = value; }
        }

        public bool IsCardIn
        {
            get { return _isCardIn; }
        }

        public bool IsCardOut
        {
            get { return _isCardOut; }
        }

        public DateTime EPISessionStart { get; set; }

        public string EPICMPGameCode { get; set; }

        public void Clear()
        {
            this.CardNo = string.Empty;
            this.EPISessionStart = DateTime.MinValue;
            this.EPICMPGameCode = string.Empty;
        }

        public bool GetLatestMeters(int installationNo, EPIMeterValueTypes valueType)
        {
            return _epiMeterValues.GetLatestMeters(installationNo, valueType);
        }

        public bool IsValid
        {
            get { return this.IsCardIn; }
        }

        public void CopyMeterValuesFromDB(FloorFinancialsResult floorFinancial, EPIMeterValueTypes valueType)
        {
            _epiMeterValues.CopyMeterValuesFromDB(floorFinancial, valueType);
        }

        public void CopyMeterValuesFromDB(DataRow floorFinancial, EPIMeterValueTypes valueType)
        {
            _epiMeterValues.CopyMeterValuesFromDB(floorFinancial, valueType);
        }

        protected override void ToString(StringBuilder sb)
        {
            base.ToString(sb);
            sb.AppendFormat("\tCard No: {0}", this.CardNo);
            Parallel.ForEach<EPIMeterValue>(_epiMeterValues.Values, (m) =>
            {
                sb.AppendFormat(",\t" + m.ToString());
            });
        }
    }

    class EPICardDetails :
        DoubleKeyConcurrentDictionary<int, string, EPICardDetail>
    {
        public EPICardDetails()
            : base((e) =>
            {
                return e.CardNo;
            }) { }
    }

    public class EPIManager
        : DisposableObject
    {

        private readonly StringConcurrentDictionary<EPITimeout> _collection = new StringConcurrentDictionary<EPITimeout>();
        private readonly IExMonitorServerConfigStore _configStore = ExMonitorServerConfigStoreFactory.Store;
        private readonly EPICardDetails _playerCardInDetails = new EPICardDetails();

        #region Single Thread Helper (Current)

        private static readonly SingletonThreadHelper<EPIManager> _currentHelper = new SingletonThreadHelper<EPIManager>(
                                    new Lazy<EPIManager>(() => new EPIManager()));

        public static EPIManager Current
        {
            get { return _currentHelper.Current; }
        }

        #endregion

        private EPIManager() { }

        internal EPICardDetail GetCardInDetail(int installationNo)
        {
            return _playerCardInDetails.AddOrGet(installationNo, () => { return new EPICardDetail(installationNo); });
        }

        internal EPICardDetail GetCardInDetailByCardNo(string cardNo)
        {
            return _playerCardInDetails.GetValueFromKey2(cardNo);
        }

        internal EPICardDetail AddOrUpdateCardInDetail(int installationNo, string cardNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddOrUpdateTimeout"))
            {
                EPICardDetail result = null;

                try
                {
                    (result = this.GetCardInDetail(installationNo)).CardNo = cardNo;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        internal void ClearCardDetails(EPICardDetail cardDetail)
        {
            _playerCardInDetails.Update(cardDetail.CardNo, 0);
            cardDetail.Clear();
        }

        public void PlayerCardIn(int installationNo, string cardNo)
        {
            this.AddOrUpdateCardInDetail(installationNo, cardNo);
        }

        public void PlayerCardOut(int installationNo, string cardNo)
        {
            this.AddOrUpdateCardInDetail(installationNo, string.Empty);
        }

        public void UpdatePlayerFirstCardIn(int installationNo, string cardNo, bool isFirstCardIn)
        {
            this.GetCardInDetail(installationNo).IsFirstCardIn = isFirstCardIn;
        }

        public string PrepareKey(int installationNo, EPIProcessType processType)
        {
            return string.Format("{0:D},{1:D}", installationNo, (int)processType);
        }

        private EPITimeout AddOrUpdateTimeout(int installationNo, EPIProcessType processType, DateTime lastUpdatedTime)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "AddOrUpdateTimeout"))
            {
                EPITimeout result = null;

                try
                {
                    string fullKey = this.PrepareKey(installationNo, processType);
                    result = _collection.AddOrGet(fullKey, () => { return new EPITimeout(); });
                    result.InstallationNo = installationNo;
                    result.ProcessType = processType;
                    result.LastUpdatedTime = lastUpdatedTime;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private EPITimeout GetTimeout(int installationNo, EPIProcessType processType)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveTimeout"))
            {
                EPITimeout result = null;

                try
                {
                    string fullKey = this.PrepareKey(installationNo, processType);
                    _collection.TryGetValue(fullKey, out result);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private EPITimeout RemoveTimeout(int installationNo, EPIProcessType processType)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveTimeout"))
            {
                EPITimeout result = null;

                try
                {
                    string fullKey = this.PrepareKey(installationNo, processType);
                    _collection.TryRemove(fullKey, out result);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public void CreateProcess(int installationNo)
        {
            this.AddOrUpdateTimeout(installationNo, EPIProcessType.Process, DateTime.Now);
        }

        public void CreateInactivityTimeout(int installationNo)
        {
            this.AddOrUpdateTimeout(installationNo, EPIProcessType.TimeOut, DateTime.Now);
        }

        public void UpdateInactivityTimeout(int installationNo)
        {
            this.AddOrUpdateTimeout(installationNo, EPIProcessType.TimeOut, DateTime.Now);
        }

        public void CreateIntervalRatingTimer(int installationNo)
        {
            this.AddOrUpdateTimeout(installationNo, EPIProcessType.IntervalRating, DateTime.Now);
        }

        public bool EPIProcessExists(int installationNo)
        {
            return _collection.ContainsKey(this.PrepareKey(installationNo, EPIProcessType.Process));
        }

        public void RemoveTimeoutsIfExists(int installationNo)
        {
            if (this.EPIProcessExists(installationNo))
            {
                this.RemoveTimeouts(installationNo);
            }
        }

        public void RemoveTimeouts(int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "RemoveTimeouts"))
            {
                try
                {
                    this.RemoveTimeout(installationNo, EPIProcessType.Process);
                    this.RemoveTimeout(installationNo, EPIProcessType.IntervalRating);
                    this.RemoveTimeout(installationNo, EPIProcessType.TimeOut);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public bool AnyEPITimeouts(int installationNo)
        {
            return EPIProcessExists(installationNo);
        }

        public void CheckEPITimeouts(int installationNo)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CheckEPITimeouts"))
            {
                try
                {
                    EPITimeout timeout = this.GetTimeout(installationNo, EPIProcessType.IntervalRating);
                    if (timeout == null) return;

                    if ((timeout.LastUpdatedTime.AddMinutes(_configStore.EPICardInMeterUpdate) - DateTime.Now).Seconds >= 0)
                    {
                        method.Info("Running EPI Interval Rating");

                        //  This happens due to two threads are executing this method at the same time.
                        if ((Interlocked.Read(ref timeout.IsInProcess) == 1))
                        {
                            method.Info("Some other thread is already generating the Interval Rating, So i am skipping now.");
                            return;
                        }
                        //  I am executing now
                        if ((Interlocked.CompareExchange(ref timeout.IsInProcess, 1, 0) == 1))
                        {
                            method.Info("Some other thread has already taken control, So i am skipping now.");
                            return;
                        }

                        EPICardDetail cardDetail = this.GetCardInDetail(installationNo);
                        SDTMessages.Instance.ProcessPlayerCardSessionRatings(installationNo);
                        timeout.LastUpdatedTime = DateTime.Now;
                        Interlocked.Exchange(ref timeout.IsInProcess, 0);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }

    internal class EPIMsgProcessor
    {
        private ConcurrentDictionary<int, ConcurrentDictionary<string, myEPIMessage>> _epiMsgsNormalPriorityColl = null;
        private ConcurrentDictionary<int, ConcurrentDictionary<string, myEPIMessage>> _epiMsgsHighPriorityColl = null;

        private bool _taskRunning;
        private AutoResetEvent resetevent = new AutoResetEvent(false);

        #region Single Thread Helper (Current)

        private static readonly SingletonHelper<EPIMsgProcessor> _currentHelper = new SingletonHelper<EPIMsgProcessor>(
                                    new Lazy<EPIMsgProcessor>(() => new EPIMsgProcessor()));

        public static EPIMsgProcessor Current
        {
            get { return _currentHelper.Current; }
        }

        #endregion

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The module name.</value>
        protected virtual string DYN_MODULE_NAME
        {
            get
            {
                return this.GetType().Name;
            }
        }

        private EPIMsgProcessor()
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "EPIMsgProcessor"))
            {
                _epiMsgsNormalPriorityColl = new ConcurrentDictionary<int, ConcurrentDictionary<string, myEPIMessage>>();
                _epiMsgsHighPriorityColl = new ConcurrentDictionary<int, ConcurrentDictionary<string, myEPIMessage>>();

                Log.Info(PROC, "Starting EPI message send processor ..");
                FreeformTaskHelper.CreateAndExecuteTask(ProcessHighPriorityEPIMessages, System.Threading.Tasks.TaskCreationOptions.LongRunning);
                FreeformTaskHelper.CreateAndExecuteTask(ProcessNormalPriorityEPIMessages, System.Threading.Tasks.TaskCreationOptions.LongRunning);
                Log.Info(PROC, "Started EPI message send processor ..");
            }
        }

        public void DisplayBallyWelcomeMsg(int Datapack, string sBarposName, DateTime dWhen, MessagePriority Priority = MessagePriority.NORMAL)
        {
            SendEPIMessage(Datapack, sBarposName, " WELCOME ", dWhen, 0, Priority);
        }

        public void DisplayPleaseWaitMsg(int Datapack, string sBarposName, DateTime dWhen, MessagePriority Priority = MessagePriority.NORMAL)
        {
            SendEPIMessage(Datapack, sBarposName, " STANDBY ", dWhen, 0, Priority);
        }

        public void DisplayInvalidCardMsg(int Datapack, string sBarposName, DateTime dWhen, MessagePriority Priority = MessagePriority.NORMAL)
        {
            SendEPIMessage(Datapack, sBarposName, " INVALID ", dWhen, 0, Priority);
        }

        public void DisplayReinsertCardMsg(int Datapack, string sBarposName, DateTime dWhen, MessagePriority Priority = MessagePriority.NORMAL)
        {
            SendEPIMessage(Datapack, sBarposName, " REINSERT ", dWhen, 0, Priority);
        }

        public void DisplayCardRemovedMsg(int Datapack, string sBarposName, DateTime dWhen, MessagePriority Priority = MessagePriority.NORMAL)
        {
            SendEPIMessage(Datapack, sBarposName, " CARD REMOVED ", dWhen, 0, Priority);
        }

        public void DisplayCouldnotCommunicateMsg(int Datapack, string sBarposName, DateTime dWhen, MessagePriority Priority = MessagePriority.NORMAL)
        {
            SendEPIMessage(Datapack, sBarposName, " COMMUNICATION PROBLEM ", dWhen, 0, Priority);
        }

        public void DeleteEPIMessage(int installationNo)
        {
            this.DeleteEPIMessage(installationNo, string.Empty);
        }

        public void DeleteEPIMessage(int installationNo, string monitorKey)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "DeleteEPIMessage"))
            {
                Log.Info(PROC, "Deleting messages for " + installationNo);
                try
                {
                    lock (_epiMsgsHighPriorityColl)
                    {
                        if (_epiMsgsHighPriorityColl.Count > 0)
                        {
                            if (_epiMsgsHighPriorityColl[installationNo].ContainsKey(monitorKey))
                                _epiMsgsHighPriorityColl[installationNo][monitorKey]._isSendPending = false;
                        }
                    }

                    lock (_epiMsgsNormalPriorityColl)
                    {
                        if (_epiMsgsNormalPriorityColl.Count > 0)
                        {
                            if (_epiMsgsNormalPriorityColl[installationNo].ContainsKey(monitorKey))
                                _epiMsgsNormalPriorityColl[installationNo][monitorKey]._isSendPending = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public void SendEPIMessage(int installationNo, MessagePriority priority, IMonitorEntity_MsgTgt monitorTarget)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SendEPIMessage"))
            {
                Log.Info(PROC, "Started");
                try
                {

                    myEPIMessage response = new myEPIMessage()
                    {
                        //myRequest._ShowTime = dWhen;
                        _UDP = installationNo,
                        _MSG = string.Empty,
                        //myRequest._BarPos = sBarposName;
                        //myRequest._Timeout = nTimeout;
                        _Priority = priority,
                        MonitorTarget = monitorTarget,
                        _CMD = 79,
                        _isSendPending = true
                    };
                    
                    if (priority == MessagePriority.HIGH)
                    {
                        if (_epiMsgsHighPriorityColl[installationNo].ContainsKey(monitorTarget.ToString()))
                            _epiMsgsHighPriorityColl[installationNo][monitorTarget.ToString()] = response;
                        else
                            _epiMsgsHighPriorityColl[installationNo].TryAdd(monitorTarget.ToString(), response);
                    }
                    else if (priority == MessagePriority.NORMAL)
                    {
                        if (_epiMsgsNormalPriorityColl[installationNo].ContainsKey(monitorTarget.ToString()))
                            _epiMsgsNormalPriorityColl[installationNo][monitorTarget.ToString()] = response;
                        else
                            _epiMsgsNormalPriorityColl[installationNo].TryAdd(monitorTarget.ToString(), response);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        public void SendEPIMessage(int installationNo, string barposName, string message, DateTime dWhen, int nTimeout, MessagePriority priority, int CMD = 78)
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SendEPIMessage"))
            {
                Log.Info(PROC, "Started");
                try
                {
                    MonTgt_H2G_EPI_Info monitorTarget = new MonTgt_H2G_EPI_Info()
                    {
                        EPICommand = Convert.ToByte(CMD),
                        EPIMessages = ASCIIEncoding.UTF8.GetBytes(message)
                    };

                    myEPIMessage response = new myEPIMessage()
                    {
                        //myRequest._ShowTime = dWhen;
                        _UDP = installationNo,
                        _MSG = message,
                        _BarPos = barposName,
                        _Timeout = nTimeout,
                        _Priority = priority,
                        MonitorTarget = monitorTarget,
                        _CMD = 79,
                        _isSendPending = true
                    };

                    if (priority == MessagePriority.HIGH)
                    {
                        if (_epiMsgsHighPriorityColl[installationNo].ContainsKey(monitorTarget.ToString()))
                            _epiMsgsHighPriorityColl[installationNo][monitorTarget.ToString()] = response;
                        else
                            _epiMsgsHighPriorityColl[installationNo].TryAdd(monitorTarget.ToString(), response);
                    }
                    else if (priority == MessagePriority.NORMAL)
                    {
                        if (_epiMsgsNormalPriorityColl[installationNo].ContainsKey(monitorTarget.ToString()))
                            _epiMsgsNormalPriorityColl[installationNo][monitorTarget.ToString()] = response;
                        else
                            _epiMsgsNormalPriorityColl[installationNo].TryAdd(monitorTarget.ToString(), response);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        /// <summary>
        /// To Process High Priority items
        /// </summary>
        private void ProcessHighPriorityEPIMessages()
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ProcessHighPriorityEPIMessages"))
            {
                Log.Info(PROC, "TO Process High Priority EPI Messages");

                while (!resetevent.WaitOne(1000))
                {
                    try
                    {
                        if (_epiMsgsHighPriorityColl.Count > 0)
                        {
                            _epiMsgsHighPriorityColl.ForEachItem(item =>
                                {
                                    item.Value.ForEachItem(subItem =>
                                        {
                                            if (subItem.Value._isSendPending)
                                            {
                                                EPIMessage(item.Key, subItem.Value.MonitorTarget);
                                                subItem.Value._isSendPending = false;
                                            }
                                        });
                                });
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
        }

        /// <summary>
        /// To Process Normal Priority items
        /// </summary>
        private void ProcessNormalPriorityEPIMessages()
        {
            using (ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "ProcessNormalPriorityEPIMessages"))
            {
                Log.Info(PROC, "TO Process Normal Priority EPI Messages");
                
                while (!resetevent.WaitOne(1000))
                {
                    try
                    {
                        if (_epiMsgsNormalPriorityColl.Count > 0)
                        {
                            _epiMsgsNormalPriorityColl.ForEachItem(item =>
                            {
                                item.Value.ForEachItem(subItem =>
                                {
                                    if (subItem.Value._isSendPending)
                                    {
                                        EPIMessage(item.Key, subItem.Value.MonitorTarget);
                                        subItem.Value._isSendPending = false;
                                    }
                                });
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
        }

        public void EPIMessage(int installationNo, IMonitorEntity_MsgTgt target)
        {
            SendCommand(installationNo, target as MonitorEntity_MsgTgt);
        }

        public void SendCommand(int installationNo, MonitorEntity_MsgTgt target)
        {
            try
            {
                target.InstallationNo = installationNo;
                MonMsg_H2G msg = new MonMsg_H2G();
                msg.AddTarget(target);

                MonitorExecutionContext ctx = new MonitorExecutionContext()
                {
                    H2GMessage = msg
                };

                MonitorHandlerFactory.Current.Execute(ctx);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void SendCommand(int installationNo, int command, string message)
        {
        }
    }

    public enum MessagePriority : int
    {
        HIGH = 3,
        NORMAL = 2,
        LOW = 1,
    }

    class myEPIMessage
    {

        public int _UDP;

        public string _MSG;

        public byte[] _FreeformMsg;

        public DateTime _ShowTime;

        public string _BarPos;

        public int _Timeout;

        public int _CMD;

        public MessagePriority _Priority;

        public IMonitorEntity_MsgTgt MonitorTarget { get; set; }

        public bool _isSendPending;
    }
}
