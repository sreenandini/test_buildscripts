using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.Handlers
{
    class _FFMsgHandlerFactory
       : DisposableObject
    {
        private readonly string _key = string.Empty;
        private bool _isExecuting = false;
        private readonly List<ExCommsExecutionStepEntity> _entities = null;
        private readonly FreeformSecurityTableCollection _securityTables = null;

        private readonly IFFMsgHandler _msgHandler = null;

        internal _FFMsgHandlerFactory(string key, IFFMsgTransmitter msgTransmitter, FFTgtHandlerDeviceTypes deviceType,
            List<ExCommsExecutionStepEntity> entities, FreeformSecurityTableCollection securityTables)
        {
            _key = key;
            _msgHandler = new FFMsgHandler(this, deviceType, msgTransmitter);
            _entities = entities;
            _securityTables = securityTables;
        }

        internal FreeformSecurityTableCollection SecurityTables
        {
            get { return _securityTables; }
        }

        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
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
                    _isExecuting = true;
                    result = _msgHandler.Execute(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    _isExecuting = false;
                }

                return result;
            }
        }
    }

    internal class FFMsgHandlerFactory
        : ExecutorServiceBase
    {
        private const string DYN_MODULE_NAME = "FFMsgHandlerFactory";
        private static readonly List<ExCommsExecutionStepEntity> _entities = null;
        private static readonly StringConcurrentDictionary<FreeformSecurityTableCollection> _gmuSecurityTables = null;
        //private static readonly StringConcurrentDictionary<IList<FFMsgHandlerMessageMapping>> _messageFFMsgHandlers = null;
        private static readonly StringConcurrentDictionary<_FFMsgHandlerFactory> _gmuFactories = null;
        private volatile static int _transactionId = 0;

        #region Single Thread Helper (Factories)

        private static SingletonHelperBase<_FFMsgHandlerFactory> _factoryHelper = null;
        private static SingletonHelper<FFMsgHandlerFactory> _currentHelper = null;

        #endregion

        private readonly IThreadPoolExecutor<IFreeformEntity_Msg> _executorDelayLoading = null;
        private readonly ThinEventSlim _teWait = new ThinEventSlim(false);
        private readonly TimeSpan _tsWait = TimeSpan.FromMilliseconds(100);
        private Func<IFFMsgTransmitter> _createMessageHandler = null;

        static FFMsgHandlerFactory()
        {
            //_entities = new List<ExCommsExecutionStepEntity>();
            _gmuSecurityTables = new StringConcurrentDictionary<FreeformSecurityTableCollection>();
            _gmuFactories = new StringConcurrentDictionary<_FFMsgHandlerFactory>();
        }

        internal FFMsgHandlerFactory(IExecutorService executorService, Func<IFFMsgTransmitter> createMessageHandler)
            : base(executorService)
        {
            _createMessageHandler = createMessageHandler;
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

        public static void Initialize(IExecutorService executorService, FFTgtHandlerDeviceTypes deviceType,
            Func<IFFMsgTransmitter> createMessageHandler)
        {
            _currentHelper = new SingletonHelper<FFMsgHandlerFactory>(
                                    new Lazy<FFMsgHandlerFactory>(
                                        () => new FFMsgHandlerFactory(executorService, createMessageHandler)
                                        {
                                            DeviceType = deviceType,
                                        }));
        }

        public static FFMsgHandlerFactory Current
        {
            get { return _currentHelper.Current; }
        }

        //public IFFMsgHandlerExecutor FFMsgHandlerExecutor { get; set; }

        internal _FFMsgHandlerFactory GetFactory(IFreeformEntity_Msg request)
        {
            return _gmuFactories.GetOrAdd(request.IpAddress,
                (k) =>
                {
                    return new _FFMsgHandlerFactory(request.IpAddress, _createMessageHandler(),  this.DeviceType,
                        _entities, GetSecurityTables(request.IpAddress));
                });
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
                    _FFMsgHandlerFactory factory = GetFactory(request);

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

        public FFTgtHandlerDeviceTypes DeviceType { get; private set; }

        public FreeformSecurityTableCollection GetSecurityTables(string key)
        {
            return _gmuSecurityTables.GetOrAdd(key, (k) => { return new FreeformSecurityTableCollection(); });
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
