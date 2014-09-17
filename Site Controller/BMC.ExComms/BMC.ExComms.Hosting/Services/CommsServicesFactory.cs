using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using BMC.CoreLib.Services;
using BMC.ExComms.Contracts.Hosting;

namespace BMC.ExComms.Hosting.Services
{
    public interface IExCommsServiceInfo : IDisposable
    {
        Guid ServiceGuid { get; }

        string ServiceName { get; }

        string DisplayName { get; }

        string Description { get; }

        ServiceBase CreateService();

        IServiceHost Factory { get; }
    }

    public abstract class ExCommsServiceInfo
        : ExecutorServiceBase, IExCommsServiceInfo
    {
        private readonly IServiceHost _factory = null;

        protected ExCommsServiceInfo(IExecutorService executorService,
            ExCommsHostingModuleType moduleType,
            IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService)
        {
            _factory = new ExCommsServerHostFactoryActivatorFactory(executorService, moduleType, activators);
        }

        public abstract Guid ServiceGuid { get; }

        public abstract string ServiceName { get; }

        public abstract string DisplayName { get; }

        public virtual string Description
        {
            get { return this.DisplayName; }
        }

        public ServiceBase CreateService()
        {
            return new ExCommsServiceBase(_factory, this.ServiceName);
        }

        public IServiceHost Factory
        {
            get { return _factory; }
        }
    }

    public sealed class ExCommsServiceInfo_All
        : ExCommsServiceInfo
    {
        public ExCommsServiceInfo_All(IExecutorService executorService, ExCommsHostingModuleType moduleType, IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService, moduleType, activators)
        {
        }

        public override Guid ServiceGuid
        {
            get { return new Guid("77BB59D4-6A1E-491B-8BBE-34439CB33764"); }
        }

        public override string ServiceName
        {
            get { return "bmc_excomms_svc"; }
        }

        public override string DisplayName
        {
            get { return "BMC ExComms Service"; }
        }
    }

    public sealed class ExCommsServiceInfo_CommunicationServer
        : ExCommsServiceInfo
    {
        public ExCommsServiceInfo_CommunicationServer(IExecutorService executorService, ExCommsHostingModuleType moduleType, IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService, moduleType, activators)
        {
        }

        public override Guid ServiceGuid
        {
            get { return new Guid("7852C354-75E7-4D0E-BA36-D0C2C2A38AD4"); }
        }

        public override string ServiceName
        {
            get { return "bmc_excomms_svc_comms"; }
        }

        public override string DisplayName
        {
            get { return "BMC ExComms Service (Communication Server)"; }
        }
    }

    public sealed class ExCommsServiceInfo_MonitorServer
        : ExCommsServiceInfo
    {
        public ExCommsServiceInfo_MonitorServer(IExecutorService executorService, ExCommsHostingModuleType moduleType, IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService, moduleType, activators)
        {
        }

        public override Guid ServiceGuid
        {
            get { return new Guid("4D320C60-92AE-4BAF-A34B-2CE56B89F5CC"); }
        }

        public override string ServiceName
        {
            get { return "bmc_excomms_svc_mon"; }
        }

        public override string DisplayName
        {
            get { return "BMC ExComms Service (Monitor Server)"; }
        }
    }

    public sealed class ExCommsServiceInfo_MonitorServer_Router
        : ExCommsServiceInfo
    {
        public ExCommsServiceInfo_MonitorServer_Router(IExecutorService executorService, ExCommsHostingModuleType moduleType, IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService, moduleType, activators)
        {
        }

        public override Guid ServiceGuid
        {
            get { return new Guid("7EEE81D1-76B8-4AF4-8802-FDAFDFD4DF2C"); }
        }

        public override string ServiceName
        {
            get { return "bmc_excomms_svc_mon_route"; }
        }

        public override string DisplayName
        {
            get { return "BMC ExComms Service (Monitor Server Router)"; }
        }
    }

    public sealed class ExCommsServiceInfo_MonitorServer_Processor
        : ExCommsServiceInfo
    {
        public ExCommsServiceInfo_MonitorServer_Processor(IExecutorService executorService, ExCommsHostingModuleType moduleType, IEnumerable<IExCommsServerHostFactoryActivator> activators)
            : base(executorService, moduleType, activators)
        {
        }

        public override Guid ServiceGuid
        {
            get { return new Guid("4247C88D-260D-41F5-84F7-85564C0B70ED"); }
        }

        public override string ServiceName
        {
            get { return "bmc_excomms_svc_mon_proc"; }
        }

        public override string DisplayName
        {
            get { return "BMC ExComms Service (Monitor Server Processor)"; }
        }
    }

    internal delegate IExCommsServiceInfo CreateExCommsServiceInfoHandler(
        IExecutorService executorService,
        IEnumerable<IExCommsServerHostFactoryActivator> activators);

    public static class ExCommsServicesFactrory
    {
        private static readonly IDictionary<string, CreateExCommsServiceInfoHandler> _servicesInfo =
            new Dictionary<string, CreateExCommsServiceInfoHandler>()
            {
                { "/all", (e, a) => new ExCommsServiceInfo_All(e,
                                                                ExCommsHostingModuleType.CommunicationServer |
                                                                ExCommsHostingModuleType.MonitorServer4CommsServer |
                                                                ExCommsHostingModuleType.MonitorServer4MonProcessor,
                                                                a)},
                { "/commsserver", (e, a) => new ExCommsServiceInfo_CommunicationServer(e,
                                                                ExCommsHostingModuleType.CommunicationServer,
                                                                a)},
                { "/monitorserver", (e, a) => new ExCommsServiceInfo_MonitorServer(e,
                                                                ExCommsHostingModuleType.MonitorServer4CommsServer |
                                                                ExCommsHostingModuleType.MonitorServer4MonProcessor,
                                                                a)},
                { "/monitorserver,router", (e, a) => new ExCommsServiceInfo_MonitorServer_Router(e,
                                                                ExCommsHostingModuleType.MonitorServer4CommsServer,
                                                                a)},
                { "/monitorserver,processor", (e, a) => new ExCommsServiceInfo_MonitorServer_Processor(e,
                                                                ExCommsHostingModuleType.MonitorServer4MonProcessor,
                                                                a)},
            };

        public static IExCommsServiceInfo Run(IExecutorService executorService, string[] args)
        {
            using (ILogMethod method = Log.LogMethod("CommsServicesFactrory", "Run"))
            {
                IExCommsServiceInfo result = null;

                try
                {
                    var activators = MEFHelper.GetExportedValues<IExCommsServerHostFactoryActivator>();
                    string key = args.Length == 0 ? "/all" :
                                ((args[0] == "/debug") ? 
                                ((args.Length == 1) ? "/all" : args[1]) : args[0]);

                    if (_servicesInfo.ContainsKey(key))
                    {
                        result = _servicesInfo[key](executorService, activators);
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
