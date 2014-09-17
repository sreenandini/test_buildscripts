using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server
{
    [Export(typeof(IExCommsServerHostFactoryActivator))]
    public sealed class ExMonitorServerServiceHostFactoryActivator :
        IExCommsServerHostFactoryActivator
    {
        public ExCommsHostingServerType ServerType
        {
            get
            {
                return ExCommsHostingServerType.MonitorServer;
            }
        }

        public ExCommsHostingModuleType SupportedModuleTypes
        {
            get { return ExCommsHostingModuleType.MonitorServer4CommsServer | ExCommsHostingModuleType.MonitorServer4MonProcessor; }
        }

        public ExCommsServerHostFactory Create(IExecutorService executorService)
        {
            return new ExMonitorServerServiceHostFactory(executorService);
        }
    }

    internal class ExMonitorServerServiceHostFactory
        : ExCommsServerHostFactory
    {
        private readonly Type _typeCommsServer = typeof(IExMonServer4CommsServer);
        private readonly Type _typeProcessor = typeof(IExMonServer4MonProcessor);
        private readonly Type _typeClient = typeof(IExMonServer4MonClient);

        public ExMonitorServerServiceHostFactory(IExecutorService executorService)
            : base(executorService, ExMonitorServerConfigStoreFactory.Store, BMC.ExComms.Contracts.Proxies.ProxyHelper.BaseUrl,
                ExMonitorServerConfigStoreFactory.Store.MonitorServerTcpPort,
                ExMonitorServerConfigStoreFactory.Store.MonitorServerHttpPort,
                0)
        {
        }

        protected override IWcfExecutorServiceFactory OnCreateExecutorService()
        {
            return new ExMonitorServerServiceFactory();
        }

        public override bool Start()
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4CommsServer)
                return base.Start();
            else
                return true;
        }

        public override bool Stop()
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4CommsServer)
                return base.Stop();
            else
                return true;
        }

        protected override bool IsHostingTypeDefined(Type type)
        {
            // only monitor processor alone
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4CommsServer)
            {
                if ((type == _typeClient) ||
                    (type == _typeCommsServer) ||
                    (type == _typeProcessor))
                {
                    return true;
                }
            }
            return false;
        }
    }

    internal class ExMonitorServerServiceFactory
        : WcfExecutorServiceFactoryBase
    {
        internal ExMonitorServerServiceFactory()
        {
        }

        public override Type ServiceType
        {
            get { return typeof(ExMonitorServerImpl); }
        }

        public override object OnCreateSingletonInstance(IExecutorService executorService)
        {
            return new ExMonitorServerImpl(executorService);
        }

        public override Type[] KnownTypes
        {
            get
            {
                return ExCommsMessageKnownTypeFactory.KnownTypes;
            }
        }
    }
}
