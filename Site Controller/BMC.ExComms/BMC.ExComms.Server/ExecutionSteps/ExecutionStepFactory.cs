using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.ExecutionSteps
{    
    class _ExecutionStepFactory
        : DisposableObject
    {
        //private readonly IDictionary<string, ExecutionStepDictionary> _gmuExecutionSteps = null;
        //private readonly StringConcurrentDictionary<bool> _gmuExecutionStepsExecuting = null;

        private readonly string _key = string.Empty;
        private bool _isExecuting = false;
        private readonly List<ExCommsExecutionStepEntity> _entities = null;
        private readonly FreeformSecurityTableCollection _securityTables = null;

        private readonly ExecutionStepDictionary _executionSteps = null;
        private readonly ExecutionStepGroupedDictionary _groupedSteps = null;

        private readonly StringConcurrentDictionary<ExecutionStepCollection> _messageWiseSteps = null;
        private readonly DoubleKeyConcurrentDictionary<string, string, RequestResponseMapItem> _requestResponseMappings = null;


        //internal _ExecutionStepFactory(List<ExCommsExecutionStepEntity> entities,
        //    StringConcurrentDictionary<bool> gmuExecutionStepsExecuting)
        internal _ExecutionStepFactory(string key, List<ExCommsExecutionStepEntity> entities, FreeformSecurityTableCollection securityTables)
        {
            //_gmuExecutionSteps = new StringConcurrentDictionary<ExecutionStepDictionary>();
            //_gmuExecutionStepsExecuting = gmuExecutionStepsExecuting;

            _key = key;
            _entities = entities;
            _securityTables = securityTables;
            _executionSteps = new ExecutionStepDictionary();
            _executionSteps.Entity.GmuIpAddress = key;
            _messageWiseSteps = new StringConcurrentDictionary<ExecutionStepCollection>();
            _requestResponseMappings = new DoubleKeyConcurrentDictionary<string, string, RequestResponseMapItem>(
                null, StringComparer.OrdinalIgnoreCase, StringComparer.OrdinalIgnoreCase);
            _groupedSteps = new ExecutionStepGroupedDictionary();
            this.CreateExecutionSteps();
        }

        internal ExecutionStepDictionary ExecutionSteps
        {
            get { return _executionSteps; }
        }

        internal ExecutionStepGroupedDictionary GroupedSteps
        {
            get { return _groupedSteps; }
        }

        internal StringConcurrentDictionary<ExecutionStepCollection> MessageWiseSteps
        {
            get { return _messageWiseSteps; }
        }

        internal DoubleKeyConcurrentDictionary<string, string, RequestResponseMapItem> RequestResponseMappings
        {
            get { return _requestResponseMappings; }
        }

        internal FreeformSecurityTableCollection SecurityTables
        {
            get { return _securityTables; }
        }

        public bool IsExecuting
        {
            get
            {
                //string key = request.IpAddress;
                //if (_gmuExecutionStepsExecuting.ContainsKey(key))
                //    return _gmuExecutionStepsExecuting[key];
                //return false;
                return _isExecuting;
            }
        }

        internal bool PersistRequestResponseMapItem(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "PersistRequestResponseMapItem"))
            {
                try
                {
                    if (request.EntityPrimaryTarget == null) return false;
                    string typeName = request.EntityPrimaryTarget.GetType().Name;
                    RequestResponseMapItem mapItem = null;
                    bool copyTo = false;

                    // save the request/response map item
                    if (request is FFMsg_G2H &&
                        _requestResponseMappings.IsKey1Exists(typeName))
                    {
                        mapItem = _requestResponseMappings.GetValueFromKey1(typeName);
                    }
                    // get the request/response map item
                    else if (request is FFMsg_H2G &&
                            _requestResponseMappings.IsKey2Exists(typeName))
                    {
                        mapItem = _requestResponseMappings.GetValueFromKey2(typeName);
                        copyTo = true;
                    }

                    // copy from/to request/response map item
                    if (mapItem != null)
                    {
                        if (copyTo)
                        {
                            mapItem.CopyTo(request);
                        }
                        else
                        {
                            mapItem.CopyFrom(request);
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return false;
            }
        }

        public bool Execute(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Execute"))
            {
                bool result = default(bool);
                string key = string.Empty;

                try
                {
                    key = request.IpAddress;
                    ExecutionStepDictionary steps = _executionSteps;
                    _isExecuting = true;

                    //_gmuExecutionStepsExecuting.AddOrUpdate(key, true, (k, b) => { return true; });

                    //if (!_gmuExecutionSteps.ContainsKey(key))
                    //{
                    //    steps = new ExecutionStepDictionary();
                    //    steps.Entity.GmuIpAddress = key;
                    //    steps.CreateExecutionSteps(ExecutionStepFactory.Current.DeviceType, _messageWiseSteps, _groupedSteps);
                    //    _gmuExecutionSteps.Add(key, steps);
                    //    _entities.Add(steps.Entity);
                    //}
                    //else
                    //{
                    //    steps = _gmuExecutionSteps[key];
                    //}

                    if (steps != null)
                    {
                        // has clients
                        bool hasClients = false;
                        ExCommsExecutionStepEntity entity = steps.Entity;
                        IExecutionStep execStepCurrent = null;
                        IExecutionStep execStepLatest = null;
                        entity.Steps.Clear();

                        if (ExCommsServerImpl.Current != null)
                            hasClients = ExCommsServerImpl.Current.HasStepChangedClients;

#if !OLD_CODE
                        // 1. find the execution step by message 
                        string messageKey = request.EntityUniqueKeyDirection;
                        if (_messageWiseSteps.ContainsKey(messageKey))
                        {
                            var stepList = _messageWiseSteps[messageKey];
                            if (stepList.Count == 1)
                            {
                                execStepCurrent = stepList[0];
                            }
                        }

                        // execute the step
                        if (execStepCurrent != null)
                        {
                            // last step
                            IExecutionStep execStepLast = (steps.Current ?? steps.Start);
                            if (hasClients && execStepLast != null)
                                entity.Steps.Add(execStepLast.GetType().Name);

                            // execute the current step
                            execStepLatest = execStepCurrent.Execute(request);

                            //// move to or set to next step
                            //if (execStepLatest != null &&
                            //    execStepLatest.ExecutionResult == ExecutionStepResult.Success)
                            //{
                            //    if (execStepLatest.NextStep != null)
                            //    {
                            //        if (execStepLatest.MoveToNextStep)
                            //        {
                            //            if (hasClients && execStepLatest.NextStep != null)
                            //                entity.Steps.Add(execStepLatest.NextStep.GetType().Name);
                            //            execStepLatest = execStepLatest.NextStep.Execute(request);
                            //        }

                            //        //if (execStepLatest.SetNextStep)
                            //        //{
                            //        //    if (hasClients)
                            //        //        entity.Steps.Add(execStepLatest.GetType().Name);
                            //        //    execStepLatest = execStepLatest.NextStep;
                            //        //    execStepLatest.ExecutionResult = ExecutionStepResult.Success;
                            //        //}
                            //    }
                            //}

                            if (execStepLatest != null &&
                                execStepLatest.ExecutionResult == ExecutionStepResult.Success &&
                                !Object.ReferenceEquals(steps.Current, execStepLatest))
                            {
                                steps.Current = execStepLatest;
                            }
                        }
#else
                        // last step
                        IExecutionStep step = (steps.Current ?? steps.Start);
                        if (hasClients && step != null)
                            entity.Steps.Add(step.GetType().Name);

                        // previous step
                        if (step.ExecutionResult == ExecutionStepResult.Failed &&
                            step.PrevStep != null) step = step.PrevStep;

                        // execute the current step
                        if (hasClients && step != null)
                            entity.Steps.Add(step.GetType().Name);
                        IExecutionStep step2 = step.Execute(request);

                        // move to or set to next step
                        if (step2 != null &&
                            step2.ExecutionResult == ExecutionStepResult.Success)
                        {
                            if (step2.NextStep != null)
                            {
                                if (step2.MoveToNextStep)
                                {
                                    if (hasClients && step2.NextStep != null)
                                        entity.Steps.Add(step2.NextStep.GetType().Name);
                                    step2 = step2.NextStep.Execute(request);
                                }

                                if (step2.SetNextStep)
                                {
                                    if (hasClients)
                                        entity.Steps.Add(step2.GetType().Name);
                                    step2 = step2.NextStep;
                                    step2.ExecutionResult = ExecutionStepResult.Success;
                                }
                            }
                        }

                        if (step2 != null &&
                            step2.ExecutionResult == ExecutionStepResult.Success &&
                            !Object.ReferenceEquals(steps.Current, step2))
                        {
                            steps.Current = step2;
                        }
#endif

                        if (hasClients)
                        {
                            if (execStepLatest != null)
                                entity.Steps.Add(execStepLatest.UniqueKey);
                            ExCommsServerImpl.Current.OnNotifyExecutionStepChanged(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    //_gmuExecutionStepsExecuting.AddOrUpdate(key, false, (k, b) => { return false; });
                    _isExecuting = false;
                }

                return result;
            }
        }
    }

    internal class ExecutionStepMessageMapping
        : DisposableObject
    {
        public FF_FlowDirection Direction { get; set; }
        public IExecutionStep ExecutionStep { get; set; }
    }

    internal class ExecutionStepFactory
        : ExecutorServiceBase
    {
        private const string DYN_MODULE_NAME = "ExecutionStepFactory";
        private static readonly List<ExCommsExecutionStepEntity> _entities = null;
        private static readonly StringConcurrentDictionary<bool> _gmuExecutionStepsExecuting = null;
        private static readonly StringConcurrentDictionary<FreeformSecurityTableCollection> _gmuSecurityTables = null;
        private static readonly StringConcurrentDictionary<IList<ExecutionStepMessageMapping>> _messageExecutionSteps = null;
        private static readonly StringConcurrentDictionary<_ExecutionStepFactory> _gmuFactories = null;
        private volatile static int _transactionId = 0;

        #region Single Thread Helper (Factories)

        private static SingletonHelperBase<_ExecutionStepFactory> _factoryHelper = null;
        private static SingletonHelper<ExecutionStepFactory> _currentHelper = null;

        #endregion

        private readonly IThreadPoolExecutor<IFreeformEntity_Msg> _executorDelayLoading = null;
        private readonly ThinEventSlim _teWait = new ThinEventSlim(false);
        private readonly TimeSpan _tsWait = TimeSpan.FromMilliseconds(100);

        static ExecutionStepFactory()
        {
            _entities = new List<ExCommsExecutionStepEntity>();
            _gmuExecutionStepsExecuting = new StringConcurrentDictionary<bool>();
            _gmuSecurityTables = new StringConcurrentDictionary<FreeformSecurityTableCollection>();
            _gmuFactories = new StringConcurrentDictionary<_ExecutionStepFactory>();
        }

        internal ExecutionStepFactory(IExecutorService executorService)
            : base(executorService)
        {
            _executorDelayLoading = ThreadPoolExecutorFactory.CreateThreadPool<IFreeformEntity_Msg>(
                new ThreadPoolExecutorArg()
                {
                    ExecutorService = executorService,
                    KernelModeQueue = false,
                    PoolType = ThreadPoolType.AsyncTaskQueue,
                    ThreadCount = 5, // Configurable
                    FlushItemsBeforeClose = true,
                    ThreadQueueCount = -1,
                });
            _executorDelayLoading.ProcessItem += OnExecutorDelayLoading_ProcessItem;
        }

        void OnExecutorDelayLoading_ProcessItem(IFreeformEntity_Msg request)
        {
            var factory = GetFactory(request);
            do
            {
                if (!factory.IsExecuting)
                    break;
            } while (!_teWait.Wait(_tsWait));

            factory.Execute(request);
        }

        public static void Initialize(IExecutorService executorService, bool perThreadFactory, ExecutionStepDeviceTypes deviceType)
        {
            _currentHelper = new SingletonHelper<ExecutionStepFactory>(
                                    new Lazy<ExecutionStepFactory>(
                                        () => new ExecutionStepFactory(executorService)
                                                {
                                                    DeviceType = deviceType,
                                                }));

            //var lazyFactory = new Lazy<_ExecutionStepFactory>(() => new _ExecutionStepFactory(_entities, _gmuExecutionStepsExecuting));
            //if (perThreadFactory)
            //    _factoryHelper = new SingletonThreadHelper<_ExecutionStepFactory>(lazyFactory);
            //else
            //    _factoryHelper = new SingletonHelper<_ExecutionStepFactory>(lazyFactory);
        }

        public static ExecutionStepFactory Current
        {
            get { return _currentHelper.Current; }
        }

        public IExecutionStepExecutor ExecutionStepExecutor { get; set; }

        internal _ExecutionStepFactory GetFactory(IFreeformEntity_Msg request)
        {
            return _gmuFactories.AddOrGet(request.IpAddress,
                () => { return new _ExecutionStepFactory(request.IpAddress, _entities, GetSecurityTables(request.IpAddress)); });
        }

        public bool Execute(IFreeformEntity_Msg request)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "ProcessMessage"))
            {
                bool result = default(bool);

                try
                {
                    if (request == null ||
                        request.IpAddress.IsEmpty())
                    {
                        return false;
                    }

                    string key = request.IpAddress;
                    _ExecutionStepFactory factory = GetFactory(request);

                    // new or existing transaction id
                    if (!factory.PersistRequestResponseMapItem(request)
                        && request.TransactionID <= 0)
                    {
                        request.TransactionID = NewTransactionId;
                    }

                    // immediate execution or delayed execution
                    if (factory.IsExecuting)
                    {
                        _executorDelayLoading.QueueWorkerItem(request);
                        result = true;
                    }
                    else
                    {
                        result = factory.Execute(request);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public List<ExCommsExecutionStepEntity> Entities
        {
            get
            {
                return _entities;
            }
        }

        public ExecutionStepDeviceTypes DeviceType { get; private set; }

        public FreeformSecurityTableCollection GetSecurityTables(string key)
        {
            return _gmuSecurityTables.AddOrGet(key, () => { return new FreeformSecurityTableCollection(); });
        }

        public static int TransactionId
        {
            get { return _transactionId; }
        }

        public static int NewTransactionId
        {
            get { return Interlocked.Increment(ref _transactionId); }
        }
    }
}
