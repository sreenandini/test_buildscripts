using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using BMC.CoreLib.Services;
using BMC.CoreLib.WcfHelper.Behaviors;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExComms.Contracts.Hosting
{
    public interface IExCommsServerBase : IDisposable
    {
    }

    public interface IExCommsServerImpl
        : IExCommsServerBase, IExMonServer4CommsServerCallback
    {
        IExecutorService ExecutorService { get; }
        bool ProcessH2GMessage(FFMsg_G2H request);
        bool ProcessH2GMessage(IFFTgt_H2G request);
        bool PostMessageToTransceiver(IFreeformEntity_Msg request);
    }

    public interface IExMonitorServerImpl
        : IExCommsServerBase
    {
        IExecutorService ExecutorService { get; }
        bool ProcessH2GMessage(MonMsg_H2G request);
    }

    public interface IMonitorProcessor : IDisposable
    {
        bool ProcessG2HMessage(MonMsg_G2H request);
        bool ProcessH2GMessage(MonMsg_H2G request);
    }

    public abstract class ExCommsServerHostFactory
        : WcfExecutorServiceHostFactory
    {
        private readonly IExchangeConfigStore _configStore = null;

        protected ExCommsServerHostFactory(IExecutorService executorService, IExchangeConfigStore configStore, string basePath, int tcpPort, int httpPort, int webHttpPort)
            : base(executorService, basePath, tcpPort, httpPort, webHttpPort)
        {
            _configStore = configStore;
        }

        protected override WcfServiceHostBase OnCreateServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
        {
            this.SingletonInstance = singletonInstance;
            return new ExCommsServerHost(_configStore, singletonInstance, knownTypes, baseAddresses);
        }

        public virtual object SingletonInstance { get; set; }
    }

    public interface IExCommsServerHostFactoryActivator
    {
        ExCommsHostingServerType ServerType { get; }

        ExCommsHostingModuleType SupportedModuleTypes { get; }

        ExCommsServerHostFactory Create(IExecutorService executorService);
    }

    public class ExCommsServerBehavior
        : WcfCustomBehavior
    {
        private readonly IExchangeConfigStore _configStore = null;

        public ExCommsServerBehavior(IExchangeConfigStore configStore)
        {
            _configStore = configStore ?? ExCommsServerConfigStoreFactory.Store;
        }

        public override bool LogRequestMessage
        {
            get
            {
                return _configStore.LogIncomingMessages;
            }
            set
            {
                _configStore.LogIncomingMessages = value;
            }
        }

        public override bool LogResponseMessage
        {
            get
            {
                return _configStore.LogOutgoingMessages;
            }
            set
            {
                _configStore.LogOutgoingMessages = value;
            }
        }
    }

    public sealed class ExCommsServerHost
        : WcfExecutorServiceHost
    {
        private readonly IExchangeConfigStore _configStore = null;

        public ExCommsServerHost(IExchangeConfigStore configStore, Type serviceType, Type[] knownTypes,
            params Uri[] baseAddresses)
            : base(serviceType, knownTypes, baseAddresses)
        {
            _configStore = configStore;
        }

        public ExCommsServerHost(IExchangeConfigStore configStore, object singletonInstance, Type[] knownTypes,
            params Uri[] baseAddresses)
            : base(singletonInstance, knownTypes, baseAddresses)
        {
            _configStore = configStore;
        }

        protected override WcfCustomBehaviorBase CreateCustomBehavior()
        {
            return new ExCommsServerBehavior(_configStore);
        }
    }

    public sealed class ExCommsServerHostFactoryActivatorFactory
        : ExecutorServiceBase, IServiceHost
    {
        private readonly IEnumerable<IExCommsServerHostFactoryActivator> _activators = null;
        private readonly IList<ExCommsServerHostFactory> _hosts = new List<ExCommsServerHostFactory>();

        private static ExCommsServerHostFactoryActivatorFactory _current = null;

        public ExCommsServerHostFactoryActivatorFactory(IExecutorService executorService,
            ExCommsHostingModuleType moduleType,
            IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService)
        {
            ExCommsHostingModuleTypeHelper.Current.ModuleType = moduleType;
            _activators = activators;
            _current = this;
        }

        public static ExCommsServerHostFactoryActivatorFactory Current
        {
            get { return _current; }
        }

        public IExCommsServerImpl CommunicationServer { get; private set; }

        public IExMonitorServerImpl MonitorServer { get; private set; }

        public bool Start()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Start"))
            {
                bool result = default(bool);

                try
                {
                    ExCommsHostingModuleTypeHelper moduleType = ExCommsHostingModuleTypeHelper.Current;
                    Array values = Enum.GetValues(typeof(ExCommsHostingModuleType));

                    foreach (var activator in _activators)
                    {
                        using (ExCommsHostingModuleTypeHelper moduleType2 =
                                new ExCommsHostingModuleTypeHelper(activator.SupportedModuleTypes))
                        {
                            if (!moduleType2.Equals(moduleType)) continue;

                            var host = activator.Create(this.ExecutorService);
                            if (host != null)
                            {
                                _hosts.Add(host);
                                result |= host.Start();

                                if (host.SingletonInstance is IExCommsServerImpl)
                                    this.CommunicationServer = host.SingletonInstance as IExCommsServerImpl;
                                if (host.SingletonInstance is IExMonitorServerImpl)
                                    this.MonitorServer = host.SingletonInstance as IExMonitorServerImpl;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public bool Stop()
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "Start"))
            {
                bool result = default(bool);

                try
                {
                    this.ExecutorService.Shutdown();
                    foreach (var host in _hosts)
                    {
                        result |= host.Stop();
                    }
                    this.ExecutorService.AwaitTermination(TimeSpan.FromMinutes(2));
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
