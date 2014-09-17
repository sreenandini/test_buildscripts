using System.Collections.Concurrent;
using BMC.CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.Hosting;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server.Handlers
{
    internal class _MonitorHandlerFactory
        : MonitorHandlerBase
    {
        private static IDictionary<string, Type> _handlerMappingTypes = null;
        private IDictionary<string, IMonitorHandler> _handlerMappings = null;
        private IMonitorHandler _faultHandler = null;

        static _MonitorHandlerFactory()
        {
            InitializeMappingTypes();
        }

        public _MonitorHandlerFactory()
        {
            this.InitializeMappings();
        }

        private static void InitializeMappingTypes()
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandlerFactory", "Initialize"))
            {
                try
                {
                    _handlerMappingTypes = new StringConcurrentDictionary<Type>();
                    var handlerMappingTypes = (from t in typeof(_MonitorHandlerFactory).Assembly.GetTypes()
                                               let j = t.GetCustomAttributes(typeof(MonitorHandlerMappingAttribute), true).OfType<MonitorHandlerMappingAttribute>().ToArray()
                                               where j.Length > 0
                                               orderby t.Name
                                               select new
                                               {
                                                   Name = t.Name,
                                                   Type = t,
                                                   Attribute = j
                                               }).ToArray();

                    const string keyFormat = "{0:D}_{1:D}";
                    foreach (var handlerMappingType in handlerMappingTypes.AsParallel())
                    {
                        Type classType = handlerMappingType.Type;

                        foreach (var mappingAttribute in handlerMappingType.Attribute)
                        {
                            if (mappingAttribute.FaultEnumType != null)
                            {
                                Array enumValues = Enum.GetValues(mappingAttribute.FaultEnumType);
                                foreach (var enumValue in enumValues)
                                {
                                    string key = string.Format(keyFormat, mappingAttribute.FaultSource, (int)enumValue);
                                    _handlerMappingTypes.Add(key, classType);
                                }
                            }
                            else
                            {
                                string key = string.Format(keyFormat, mappingAttribute.FaultSource, mappingAttribute.FaultType);
                                _handlerMappingTypes.Add(key, classType);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private void InitializeMappings()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "InitializeMappings"))
            {
                IDictionary<string, IMonitorHandler> handlerInstances = null;

                try
                {
                    _handlerMappings = new StringDictionary<IMonitorHandler>();
                    handlerInstances = new StringDictionary<IMonitorHandler>();

                    foreach (var mappingType in _handlerMappingTypes.AsParallel())
                    {
                        MonitorHandlerBase handler = null;
                        string mappingTypeKey = mappingType.Value.FullName;

                        if (handlerInstances.ContainsKey(mappingTypeKey))
                        {
                            handler = handlerInstances[mappingTypeKey] as MonitorHandlerBase;
                        }
                        else
                        {
                            handler = Activator.CreateInstance(mappingType.Value, true) as MonitorHandlerBase;
                            handlerInstances.Add(mappingTypeKey, handler);
                        }

                        if (handler == null) continue;
                        if (!_handlerMappings.ContainsKey(mappingType.Key))
                        {
                            _handlerMappings.Add(mappingType.Key, handler);
                        }
                    }

                    const string faultKey = "-1_-1";
                    _faultHandler = _handlerMappings[faultKey];
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    if (handlerInstances != null)
                    {
                        handlerInstances.Clear();
                        handlerInstances = null;
                    }
                }
            }
        }

        protected override bool ProcessG2HMessageInternal(MonMsg_G2H request)
        {
            if (request == null) return false;

            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "ProcessG2HMessageInternal"))
            {
                bool result = false;
                string key = request.FaultSourceTypeKey;

                try
                {
                    method.Info("!&! HANDLER STARTED FOR : " + key);

                    result = _handlerMappings.ContainsKey(key) ?
                            _handlerMappings[key].ProcessG2HMessage(request) :
                            _faultHandler.ProcessG2HMessage(request);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    method.Info("!&! HANDLER COMPLETED FOR : " + key);
                }

                return result;
            }
        }

        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "OnExecuteInternal"))
            {
                bool result = default(bool);
                int installationNo = 0;
                MonMsg_H2G h2gMessage = null;
                Lazy<IList<MonitorEntity_MsgTgt>> invalidTargets = new Lazy<IList<MonitorEntity_MsgTgt>>(
                    () => { return new List<MonitorEntity_MsgTgt>(); });

                try
                {
                    if (context.G2HMessage != null)
                    {
                        MonMsg_G2H request = context.G2HMessage;
                        installationNo = request.InstallationNo;
                        foreach (var target2 in request.Targets)
                        {
                            if (target2 is IMonTgt_Secondary) continue;

                            string key = target2.FaultSourceTypeKey;
                            if (_handlerMappings.ContainsKey(key))
                            {
                                request.FaultSource = target2.FaultSource;
                                request.FaultType = target2.FaultType;
                                result |= _handlerMappings[key].Execute(context, target2);
                            }
                            else
                            {
                                invalidTargets.Value.Add(target2);
                            }
                        }
                        if (installationNo <= 0)
                            installationNo = context.G2HMessage.InstallationNo;
                    }

                    // unmapped targets
                    if (invalidTargets.Value != null &&
                        invalidTargets.Value.Count > 0)
                    {
                        foreach (var target2 in invalidTargets.Value)
                        {
                            result |= _faultHandler.Execute(context, target2);
                        }
                    }

                    if (context.H2GMessage != null)
                    {
                        h2gMessage = context.H2GMessage;
                    }
                    else if (context.H2GTargets != null &&
                        context.H2GTargets.Count > 0)
                    {
                        h2gMessage = new MonMsg_H2G()
                        {
                            InstallationNo = installationNo,
                        };
                        h2gMessage.Targets.AddRange(context.H2GTargets);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                finally
                {
                    if (h2gMessage != null)
                    {
                        if (h2gMessage.InstallationNo <= 0)
                            h2gMessage.InstallationNo = installationNo;
                        (ExMonitorServerImpl.Current as IExMonServer4CommsServer2).ProcessH2GMessage(h2gMessage);
                    }
                }

                return result;
            }
        }
    }

    internal class MonitorHandlerFactory
        : ExecutorServiceBase
    {
        private static readonly StringConcurrentDictionary<bool> _gmuHandlersExecuting = null;

        #region Single Thread Helper (MonitorProcessor)

        private static SingletonHelperBase<_MonitorHandlerFactory> _factoryHelper = null;
        private static SingletonHelper<MonitorHandlerFactory> _currentHelper = null;

        #endregion

        private readonly IThreadPoolExecutor<MonitorExecutionContext> _executorDelayLoading = null;
        private readonly ThinEventSlim _teWait = new ThinEventSlim(false);
        private readonly TimeSpan _tsWait = TimeSpan.FromMilliseconds(100);

        static MonitorHandlerFactory()
        {
            _factoryHelper = new SingletonThreadHelper<_MonitorHandlerFactory>(
                                    new Lazy<_MonitorHandlerFactory>(() =>
                                       { return new _MonitorHandlerFactory(); }));
        }

        internal MonitorHandlerFactory(IExecutorService executorService)
            : base(executorService)
        {
            _executorDelayLoading = ThreadPoolExecutorFactory.CreateThreadPool<MonitorExecutionContext>(
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

        void OnExecutorDelayLoading_ProcessItem(MonitorExecutionContext context)
        {
            var factory = GetFactory(context);
            do
            {
                if (!factory.IsExecuting)
                    break;
            } while (!_teWait.Wait(_tsWait));

            factory.Execute(context, null);
        }

        public static void Initialize(IExecutorService executorService)
        {
            _currentHelper = new SingletonHelper<MonitorHandlerFactory>(
                                    new Lazy<MonitorHandlerFactory>(
                                        () => { return new MonitorHandlerFactory(executorService); }));
        }

        public static MonitorHandlerFactory Current
        {
            get { return _currentHelper.Current; }
        }

        internal _MonitorHandlerFactory GetFactory(MonitorExecutionContext request)
        {
            return _factoryHelper.Current;
        }

        internal bool Execute(MonitorExecutionContext context)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandlerFactory", "Execute"))
            {
                bool result = default(bool);

                try
                {
                    _MonitorHandlerFactory factory = GetFactory(context);
                    if (factory.IsExecuting)
                    {
                        _executorDelayLoading.QueueWorkerItem(context);
                        result = true;
                    }
                    else
                    {
                        result = factory.Execute(context, null);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }
    }
}
