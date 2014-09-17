using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal enum ExecutionStepResult
    {
        None = 0,
        Success = 1,
        Failed = 2,
    }

    public enum ExecutionStepPostTypes
    {
        ProcessInExternalChannel = 0,
        ProcessInCurrentChannel = 1,
        PrepareAndProcessInExternalChannel = 2,
        ProcessCustom = 3,
    }

    internal struct ExecutionStepKeyValue
    {
        private string _key;
        private FF_FlowDirection _value;
        private string _fullKey;

        public static ExecutionStepKeyValue Empty = new ExecutionStepKeyValue();

        public ExecutionStepKeyValue(string key, FF_FlowDirection value)
        {
            _key = key;
            _value = value;
            _fullKey = string.Format("{0}, {1}", _key, _value);
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public FF_FlowDirection Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string FullKey
        {
            get { return _fullKey; }
        }

        public override string ToString()
        {
            return _fullKey;
        }
    }

    internal interface IExecutionStep
        : IDisposable, IExecutorKey
    {
        _ExecutionStepFactory Factory { get; set; }

        IExecutionStep PrevStep { get; set; }

        IExecutionStep NextStep { get; set; }

        ExecutionStepResult ExecutionResult { get; set; }

        ExecutionStepPostTypes PostTypeG2H { get; set; }

        ExecutionStepPostTypes PostTypeH2G { get; set; }

        bool SetNextStep { get; set; }

        bool MoveToNextStep { get; set; }

        int Level { get; set; }

        ExecutionStepCollection NextSteps { get; }

        ExecutionStepCollection PrevSteps { get; }

        IDictionary<ExecutionStepKeyValue, byte> AllowedMessages { get; }

        bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request);

        IExecutionStep Execute(IFreeformEntity_Msg request);
    }

    internal delegate bool ExecStepProcessMessageG2HHandler(ILogMethod method, FFMsg_G2H message);
    internal delegate bool ExecStepProcessMessageH2GHandler(ILogMethod method, FFMsg_H2G message);

    internal interface IExecutionStepExecutor
        : IDisposable
    {
        bool ProcessMessageG2H(ILogMethod method, FFMsg_G2H message);
        bool ProcessMessageH2G(ILogMethod method, FFMsg_H2G message);
    }

    internal sealed class ExecutionStepCollection
        : List<IExecutionStep>
    {
        internal ExecutionStepCollection() { }

        internal ExecutionStepCollection(IEnumerable<IExecutionStep> collection)
            : base(collection) { }

        public override string ToString()
        {
            return string.Format("Total Items : {0:D}", this.Count);
        }
    }

    internal sealed class ExecutionStepGroupedDictionary
        : IntDictionary<ExecutionStepCollection>
    {
        internal ExecutionStepGroupedDictionary() { }
    }

    internal sealed class ExecutionStepDictionary
        : StringDictionary<IExecutionStep>
    {
        internal ExecutionStepDictionary()
        {
            this.Entity = new ExCommsExecutionStepEntity();
        }

        public IExecutionStep Start { get; set; }

        public IExecutionStep End { get; set; }

        public IExecutionStep Current { get; set; }

        public ExCommsExecutionStepEntity Entity { get; private set; }

        public override string ToString()
        {
            return string.Format("Total Items : {0:D}", this.Count);
        }
    }

    internal abstract class ExecutionStep
        : DisposableObject, IExecutionStep
    {
        private ExecutionStepStates _state = ExecutionStepStates.HostStarted;
        private _ExecutionStepFactory _factory = null;

        private bool _hasNextSteps = false;
        protected IExecutionStep _nextStep = null;
        protected readonly Lazy<ExecutionStepCollection> _nextSteps = null;
        protected StringDictionary<ExecutionStep> _nextStepsCached = null;

        private bool _hasPrevSteps = false;
        protected IExecutionStep _prevStep = null;
        protected readonly Lazy<ExecutionStepCollection> _prevSteps = null;
        protected StringDictionary<ExecutionStep> _prevStepsCached = null;

        protected ExecStepProcessMessageG2HHandler _processMessageG2H = null;
        protected ExecStepProcessMessageH2GHandler _processMessageH2G = null;

        protected ExecutionStepPostTypes _postG2H = ExecutionStepPostTypes.ProcessInExternalChannel;
        protected ExecutionStepPostTypes _postH2G = ExecutionStepPostTypes.ProcessInCurrentChannel;

        private readonly IDictionary<ExecutionStepKeyValue, byte> _allowedMessages = null;
        private readonly FreeformSecurityTableCollection _securityTables = null;
        internal readonly ExCommsServerImpl _serverInstance = null;

        private class KeyValueComparer :
            IComparer<ExecutionStepKeyValue>
        {
            public int Compare(ExecutionStepKeyValue x, ExecutionStepKeyValue y)
            {
                return string.CompareOrdinal(x.ToString(), y.ToString());
            }
        }

        #region Single Thread Helper (MonitorProcessor)

        private readonly static SingletonThreadHelper<IExCommsMonitorProcessor> _monitorProcessorHelper = null;

        static ExecutionStep()
        {
            _monitorProcessorHelper = new SingletonThreadHelper<IExCommsMonitorProcessor>(
                new Lazy<IExCommsMonitorProcessor>(() => new ExCommsMonitorProcessor(ExCommsServerImpl.Current)));
        }

        internal IExCommsMonitorProcessor MonitorProcessor
        {
            get { return _monitorProcessorHelper.Current; }
        }

        #endregion

        internal ExecutionStep()
        {
            _serverInstance = ExCommsServerImpl.Current;

            _nextSteps = new Lazy<ExecutionStepCollection>(() =>
            {
                _hasNextSteps = true;
                _nextStepsCached = new StringDictionary<ExecutionStep>();
                return new ExecutionStepCollection();
            });
            _prevSteps = new Lazy<ExecutionStepCollection>(() =>
            {
                _hasPrevSteps = true;
                _prevStepsCached = new StringDictionary<ExecutionStep>();
                return new ExecutionStepCollection();
            });

            if (ExecutionStepFactory.Current.ExecutionStepExecutor != null)
            {
                _processMessageG2H = ExecutionStepFactory.Current.ExecutionStepExecutor.ProcessMessageG2H;
                _processMessageH2G = ExecutionStepFactory.Current.ExecutionStepExecutor.ProcessMessageH2G;
            }
            else
            {
                _processMessageG2H = this.OnProcessMessageG2H;
                _processMessageH2G = this.OnProcessMessageH2G;
            }
            this.PostTypeG2H = ExecutionStepPostTypes.ProcessInExternalChannel;
            this.PostTypeH2G = ExecutionStepPostTypes.ProcessInCurrentChannel;

            _allowedMessages = new SortedDictionary<ExecutionStepKeyValue, byte>(new KeyValueComparer());
        }

        //internal ExecutionStep(IExecutionStep nextStep)
        //    : this()
        //{
        //    _nextStep = nextStep;
        //}

        //internal ExecutionStep(IEnumerable<IExecutionStep> nextSteps)
        //    : this()
        //{
        //    this.NextSteps.AddRange(nextSteps);
        //}

        public override string ToString()
        {
            return string.Format("{0} ({1:D})", this.GetType().Name, this.GetHashCode());
        }

        public _ExecutionStepFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public IExecutionStep NextStep
        {
            get { return _nextStep; }
            set { _nextStep = value; }
        }

        public string UniqueKey { get { return this.GetType().Name; } }

        public IExecutionStep PrevStep { get; set; }

        public ExecutionStepResult ExecutionResult { get; set; }

        public ExecutionStepPostTypes PostTypeG2H { get; set; }

        public ExecutionStepPostTypes PostTypeH2G { get; set; }

        public bool SetNextStep { get; set; }

        public bool MoveToNextStep { get; set; }

        public int Level { get; set; }

        public ExecutionStepCollection NextSteps
        {
            get { return _nextSteps.Value; }
        }

        public ExecutionStepCollection PrevSteps
        {
            get { return _prevSteps.Value; }
        }

        public IDictionary<ExecutionStepKeyValue, byte> AllowedMessages
        {
            get
            {
                return _allowedMessages;
            }
        }

        public virtual bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request)
        {
            return _allowedMessages.ContainsKey(pair);
        }

        public IExecutionStep Execute(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessMessage"))
            {
                try
                {
                    ExecutionStep step = null;
                    this.ExecutionResult = ExecutionStepResult.None;
                    ExecutionStepKeyValue pair =
                        new ExecutionStepKeyValue(request.EntityUniqueKeyString, request.FlowDirection);
                    string uniqueKey = pair.FullKey;

                    // current execution step
                    if (this.CanExecute(pair, request))
                    {
                        step = this;
                    }
                    // next step is responsible
                    else if (_nextStep != null &&
                            _nextStep.CanExecute(pair, request))
                    {
                        step = _nextStep as ExecutionStep;
                    }
                    // any of the next steps is responsible
                    else if (_hasNextSteps)
                    {
                        if (_nextStepsCached.ContainsKey(uniqueKey))
                        {
                            step = _nextStepsCached[uniqueKey];
                        }
                        else
                        {
                            foreach (var nextStep in this.NextSteps)
                            {
                                if (nextStep.CanExecute(pair, request))
                                {
                                    step = nextStep as ExecutionStep;
                                    _nextStepsCached.Add(uniqueKey, step);
                                    break;
                                }
                            }
                        }
                    }

                    // any thing is responsible?
                    if (step != null)
                    {
                        return step.ExecuteInternal(method, request);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return null;
            }
        }

        internal virtual ExecutionStep ExecuteInternal(ILogMethod method, Contracts.DTO.Freeform.IFreeformEntity_Msg request)
        {
            bool result = false;
            ExecutionStepPostTypes postType = ExecutionStepPostTypes.ProcessInExternalChannel;

            if (request is FFMsg_G2H)
            {
                postType = this.PostTypeG2H;
                if (postType == ExecutionStepPostTypes.ProcessInExternalChannel)
                {
                    result = _processMessageG2H(method, request as FFMsg_G2H);
                }
            }
            else if (request is FFMsg_H2G)
            {
                postType = this.PostTypeH2G;
                if (postType == ExecutionStepPostTypes.ProcessInExternalChannel)
                {
                    result = _processMessageH2G(method, request as FFMsg_H2G);
                }
            }

            if (postType != ExecutionStepPostTypes.ProcessInExternalChannel)
            {
                if (postType == ExecutionStepPostTypes.ProcessInCurrentChannel)
                {
                    result = this.PostMessageToProcessInCurrentChannel(method, request);
                }
                else if (postType == ExecutionStepPostTypes.PrepareAndProcessInExternalChannel)
                {
                    result = this.PostMessageToProcessInExternalChannel(method, request);
                }
                else if (postType == ExecutionStepPostTypes.ProcessCustom)
                {
                    result = this.OnProcessMessageCustom(method, request);
                }
            }

            this.ExecutionResult = result ? ExecutionStepResult.Success : ExecutionStepResult.Failed;
            return this;
        }

        private bool PostMessageToProcessInCurrentChannel(ILogMethod method, IFreeformEntity_Msg request)
        {
            IFreeformEntity_Msg response = null;
            this.PrepareMessageToProcessInCurrentChannel(request, ref response);
            if (response != null)
                return ExecutionStepFactory.Current.Execute(response);
            return false;
        }

        protected virtual void PrepareMessageToProcessInCurrentChannel(IFreeformEntity_Msg request, ref IFreeformEntity_Msg response)
        {
            response = null;
        }

        private bool PostMessageToProcessInExternalChannel(ILogMethod method, IFreeformEntity_Msg request)
        {
            IFreeformEntity_Msg response = null;
            this.PrepareMessageToProcessInExternalChannel(request, ref response);
            if (response != null)
            {
                if (response is FFMsg_G2H)
                    return _processMessageG2H(method, response as FFMsg_G2H);
                else if (response is FFMsg_H2G)
                    return _processMessageH2G(method, response as FFMsg_H2G);
            }
            return false;
        }

        protected virtual bool OnProcessMessageCustom(ILogMethod method, IFreeformEntity_Msg request) { return true; }

        protected void ExecuteCurrentRequest(IFreeformEntity_Msg request)
        {
            ExecutionStepFactory.Current.Execute(request);
        }

        protected void ExecuteCurrentRequests(params IFreeformEntity_Msg[] requests)
        {
            foreach (var request in requests)
            {
                ExecutionStepFactory.Current.Execute(request);
                Thread.Sleep(10);
            }
        }

        protected void ExecuteNextStep(IFreeformEntity_Msg request)
        {
            if (this.NextStep != null)
            {
                this.NextStep.Execute(request);
            }
        }

        protected virtual void PrepareMessageToProcessInExternalChannel(IFreeformEntity_Msg request, ref IFreeformEntity_Msg response)
        {
            response = request;
        }

        protected virtual bool OnProcessMessageG2H(ILogMethod method, FFMsg_G2H message)
        {
            if (message == null)
            {
                method.Info("Freeform message (G2H) was null.");
                return false;
            }

            // further modifiy the message
            if (this.OnModifyMessageG2H(method, message))
            {
                return this.PostMessageToMonitorServer(method, message);
            }

            return false;
        }

        protected virtual bool PostMessageToMonitorServer(ILogMethod method, FFMsg_G2H message)
        {
            // convert the monitor message from freeform message
            MonMsg_G2H monMsg = MonitorEntityFactory.CreateEntity(message);
            if (monMsg == null)
            {
                method.Info("Unable to convert the monitor message from freeform message.");
                return false;
            }

            // post the monitor message into monitor processor
            if (!this.MonitorProcessor.ProcessG2HMessage(monMsg))
            {
                method.Info("Unable to post the message to monitor processor.");
                return false;
            }

            return true;
        }

        protected virtual bool OnModifyMessageG2H(ILogMethod method, FFMsg_G2H message) { return true; }

        protected virtual bool OnProcessMessageH2G(ILogMethod method, FFMsg_H2G message)
        {
            if (message == null)
            {
                method.Info("Freeform message (H2G) was null.");
                return false;
            }

            // further modifiy the message
            if (this.OnModifyMessageH2G(method, message))
            {
                // post the monitor message into transceiver
                return _serverInstance.PostMessageToTransceiver(message);
            }

            return false;
        }

        protected virtual bool OnModifyMessageH2G(ILogMethod method, FFMsg_H2G message) { return true; }

        //protected SECURITY_KEY_INDEX GetSecurityKeyIndex(IFreeformEntity_Msg message)
        //{
        //    int sessionId = (int)message.SessionID;
        //    SECURITY_KEY_INDEX keyIndex = SECURITY_KEY_INDEX.NO_KEY;
        //    if (_securityKeys.ContainsKey(sessionId)) keyIndex = _securityKeys[sessionId];
        //    return keyIndex;
        //}

        private byte[] CreatePartialKey(IFreeformEntity_Msg message, Func<FreeformSecurityTableCollection, SECURITY_KEY_INDEX, byte[]> create)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CreatePartialKey"))
            {
                byte[] result = default(byte[]);

                try
                {
                    result = create(this.Factory.SecurityTables, message.GetSecurityKeyIndex());
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        protected byte[] CreatePartialKeyGmu(IFreeformEntity_Msg message)
        {
            return this.CreatePartialKey(message,
                (s, ki) =>
                {
                    return s.CreatePartialKeyGmu(ki);
                });
        }

        protected byte[] CreatePartialKeyHost(IFreeformEntity_Msg message)
        {
            return this.CreatePartialKey(message,
                (s, ki) =>
                {
                    return s.CreatePartialKeyHost(ki);
                });
        }

        public int TransactionId
        {
            get { return ExecutionStepFactory.TransactionId; }
        }

        public int NewTransactionId
        {
            get { return ExecutionStepFactory.NewTransactionId; }
        }
    }
}
