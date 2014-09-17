using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.WcfHelper.Hosting;
using System.ServiceModel.Description;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BMC.EBSComms.Contracts.Configuration;
using BMC.CoreLib.WcfHelper.Behaviors;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using BMC.EBSComms.Server;
using BMC.CoreLib.WcfHelper.Helpers;
using BMC.EBSComms.DataLayer.Dto;
using BMC.EBSComms.DataLayer;
using BMC.CoreLib.Services;

namespace BMC.EBSComms.Hosting
{
    public class EBSCommServerHostFactory : WcfExecutorServiceHostFactory
    {
        private static bool IS_EBS_13_0 = false;

        static EBSCommServerHostFactory()
        {
            FindEBSVersion();
        }

        private static void FindEBSVersion()
        {
            ModuleProc PROC = new ModuleProc("", "FindEBSVersion");

            try
            {
                DLSettingDto entity = DataInterfaceFactory.GetInterface().GetSettings();
                if (entity != null)
                {
                    IS_EBS_13_0 = entity.EBSVersion.IgnoreCaseCompare("13.0");
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public EBSCommServerHostFactory(IExecutorService executorService)
            : base(executorService, (IS_EBS_13_0 ? "S2S/S2SPlayerInfo.asmx" : "SDS/S2S"),
            0,
            EBSCommServerConfigStoreFactory.Store.EBSCommServerHttpPort,
            0) { }

        protected override IWcfExecutorServiceFactory OnCreateExecutorService()
        {
            return new EBSCommServerFactory();
        }

        protected override WcfServiceHostBase OnCreateServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
        {
            return new EBSCommServerHost(singletonInstance, knownTypes, baseAddresses);
        }

        protected override Binding OnCreateHttpBinding()
        {
            BasicHttpBinding binding = ContractBindingsHelper.CreateBasicHttpBinding() as BasicHttpBinding;
            if (IS_EBS_13_0)
            {
                binding.Name = "S2SPlayerInfoSoap";
                binding.Namespace = NamesHelper.NS_EBS2SMS_13_0;
            }
            else
            {
                binding.Name = "S2SEndPointBinding";
                binding.Namespace = NamesHelper.NS_EBS2SMS_13_1;
            }
            return binding;
        }
    }

    public class EBSCommServerNoHostFactory : ExecutorServiceBase, IServiceHost
    {
        public EBSCommServerNoHostFactory(IExecutorService executorService)
            : base(executorService) { }

        public bool Start()
        {
            Log.Info("BMC EBS Communication Feature is disabled (Start).");
            return true;
        }

        public bool Stop()
        {
            Log.Info("BMC EBS Communication Feature is disabled (Stop).");
            return true;
        }
    }

    public static class EBSCommServerHostFactoryFactory
    {
        static EBSCommServerHostFactoryFactory() { }

        public static IServiceHost CreateHost(IExecutorService executorService)
        {
            ModuleProc PROC = new ModuleProc("", "CreateHost");

            try
            {
#if EBS_NOHOST_SUPPORT
                bool isEnabled = DataInterfaceFactory.GetInterface().GetSettings().IsEnabled;
                if (isEnabled)
                    return new EBSCommServerHostFactory(executorService);
#else
                return new EBSCommServerHostFactory(executorService);
#endif
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return new EBSCommServerNoHostFactory(executorService);
        }
    }
    public class EBSCommServerBehavior : WcfCustomBehavior
    {
        public EBSCommServerBehavior() { }

        public override bool LogRequestMessage
        {
            get
            {
                return EBSCommServerConfigStoreFactory.Store.LogIncomingMessages;
            }
            set
            {
                EBSCommServerConfigStoreFactory.Store.LogIncomingMessages = value;
            }
        }

        public override bool LogResponseMessage
        {
            get
            {
                return EBSCommServerConfigStoreFactory.Store.LogOutgoingMessages;
            }
            set
            {
                EBSCommServerConfigStoreFactory.Store.LogOutgoingMessages = value;
            }
        }
    }

    public sealed class EBSCommServerHost : WcfExecutorServiceHost
    {
        public EBSCommServerHost(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, knownTypes, baseAddresses) { }

        public EBSCommServerHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, knownTypes, baseAddresses) { }

        protected override WcfCustomBehaviorBase CreateCustomBehavior()
        {
            return new EBSCommServerBehavior();
        }
    }
}
