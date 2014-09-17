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

namespace BMC.ExComms.Server
{
    [Export(typeof(IExCommsServerHostFactoryActivator))]
    public sealed class ExCommsServerServiceHostFactoryActivator :
        IExCommsServerHostFactoryActivator
    {
        public ExCommsHostingServerType ServerType
        {
            get
            {
                return ExCommsHostingServerType.CommunicationServer;
            }
        }

        public ExCommsHostingModuleType SupportedModuleTypes
        {
            get { return ExCommsHostingModuleType.CommunicationServer; }
        }

        public ExCommsServerHostFactory Create(IExecutorService executorService)
        {
            return new ExCommsServerServiceHostFactory(executorService);
        }
    }

    internal class ExCommsServerServiceHostFactory
        : ExCommsServerHostFactory
    {
        public ExCommsServerServiceHostFactory(IExecutorService executorService)
            : base(executorService, ExCommsServerConfigStoreFactory.Store, BMC.ExComms.Contracts.Proxies.ProxyHelper.BaseUrl,
                ExCommsServerConfigStoreFactory.Store.ExCommServerTcpPort,
                ExCommsServerConfigStoreFactory.Store.ExCommServerHttpPort,
                0)
        {
        }

        protected override IWcfExecutorServiceFactory OnCreateExecutorService()
        {
            return new ExCommsServerServiceFactory();
        }

        public override bool Start()
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasCommServer)
                return base.Start();
            else
                return true;
        }

        public override bool Stop()
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasCommServer)
                return base.Stop();
            else
                return true;
        }

        protected override bool IsHostingTypeDefined(Type type)
        {
            return true;
        }
    }

    internal class ExCommsServerServiceFactory
        : WcfExecutorServiceFactoryBase
    {
        internal ExCommsServerServiceFactory()
        {
        }

        public override Type ServiceType
        {
            get { return typeof(ExCommsServerImpl); }
        }

        public override object OnCreateSingletonInstance(IExecutorService executorService)
        {
            return new ExCommsServerImpl(executorService);
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
