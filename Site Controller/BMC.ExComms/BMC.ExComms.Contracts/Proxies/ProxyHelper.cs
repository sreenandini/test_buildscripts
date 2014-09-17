using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.CoreLib.Comparers;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.CoreLib.WcfHelper.Helpers;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib.WcfHelper.Proxies;
using BMC.ExComms.Contracts.Configuration;
using BMC.ExComms.Contracts.DTO;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Hosting;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExComms.Contracts.Proxies
{
    public abstract class ExCommsCallbackServiceChannelFactory<T, C>
        : WcfCallbackServiceChannelFactory<T>
        where T : class, IServiceContractBase
        where C : class, IServiceCallbackContractBase
    {
        protected C _callbackInstance = null;

        internal ExCommsCallbackServiceChannelFactory(IExecutorService executorService, C callbackInstance,
                                                int timeoutInMilliseconds, WaitHandle canListen)
            : base(executorService, timeoutInMilliseconds, canListen)
        {
            _callbackInstance = callbackInstance;
        }

        protected static S FillSubscriptionEntity<S>(S entity)
            where S : SubscriptionRequestEntity
        {
            using (ILogMethod method = Log.LogMethod("ExMonitorServerCallbackProxy", "FillSubscriptionEntity"))
            {
                try
                {
                    entity.IPAddress = Extensions.GetIpAddressString(-1);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return entity;
            }
        }
    }

    public static class ExCommServerWcfHelper
    {
        public static Binding CreateTcpBinding()
        {
            NetTcpBinding binding = ContractBindingsHelper.CreateTcpBinding();
            return binding;
        }

        public static Binding CreateBasicHttpBinding()
        {
            BasicHttpBinding binding = ContractBindingsHelper.CreateBasicHttpBinding();
            return binding;
        }

        public static Binding CreateDualHttpBinding()
        {
            WSDualHttpBinding binding = ContractBindingsHelper.CreateDualHttpBinding();
            return binding;
        }
    }

    public static class ExCommsGenericProxy
    {
        private static readonly IDictionary<string, object> _localInstances = null;

        private delegate IExCommsServerBase GetServerHandler(ExCommsServerHostFactoryActivatorFactory activatorFactory);
        private static readonly IDictionary<Type, GetServerHandler> _serverInstances = null;

        static ExCommsGenericProxy()
        {
            _localInstances = new SortedDictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            _serverInstances = new SortedDictionary<Type, GetServerHandler>(new TypeComparer())
            {
                {typeof (IExCommsServer), GetCommsServerInstance},
                {typeof (IExMonServer4CommsServer), GetExMonitorServerInstance4CommsServer},
                {typeof (IExMonServer4MonProcessor), GetExMonitorServerInstance4MonProcessor},
            };
        }

        private static IExCommsServerBase GetCommsServerInstance(ExCommsServerHostFactoryActivatorFactory activatorFactory)
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasCommServer &&
                activatorFactory.CommunicationServer != null)
            {
                return activatorFactory.CommunicationServer;
            }
            return null;
        }

        private static IExCommsServerBase GetExMonitorServerInstance4CommsServer(ExCommsServerHostFactoryActivatorFactory activatorFactory)
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4CommsServer &&
                activatorFactory.MonitorServer != null)
            {
                return activatorFactory.MonitorServer;
            }
            return null;
        }

        private static IExCommsServerBase GetExMonitorServerInstance4MonProcessor(ExCommsServerHostFactoryActivatorFactory activatorFactory)
        {
            if (ExCommsHostingModuleTypeHelper.Current.HasMonitorServer4MonProcessor &&
                activatorFactory.MonitorServer != null)
            {
                return activatorFactory.MonitorServer;
            }
            return null;
        }

        public static TClient GetService<TClient, TChannel>(Func<TChannel, TClient> createLocalChannel,
                                                            Func<Binding, string, TClient> createRemoteChannel)
            where TClient : WcfClientChannel<TChannel>
            where TChannel : IServiceContractBase
        {
            ModuleProc PROC = new ModuleProc("ExCommsGenericProxy", "GetService<" + typeof(TChannel).Name + ">");
            TClient result = null;
            Type serviceType = null;
            Type channelType = typeof(TChannel);

            try
            {
                // Check the interface from available service host instances
                ExCommsServerHostFactoryActivatorFactory activatorFactory =
                    ExCommsServerHostFactoryActivatorFactory.Current;
                if (activatorFactory != null &&
                    _serverInstances.ContainsKey(channelType))
                {
                    try
                    {
                        TChannel channel = (TChannel)_serverInstances[channelType](activatorFactory);
                        result = createLocalChannel(channel);
                    }
                    catch
                    {
                    }
                }

                // Remote Uri
                if (result == null)
                {
                    result = createRemoteChannel(ProxyHelper.Binding, ProxyHelper.ServerUri);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    public static class ProxyHelper
    {
        private static readonly string _hostIpAddress = string.Empty;
        private static Binding _binding = null;
        private static string _serverUri = string.Empty;

        static ProxyHelper()
        {
            _hostIpAddress = Extensions.GetIpAddressString(-1);
            _binding = ExCommServerWcfHelper.CreateTcpBinding();
            _serverUri = "net.tcp://{0}:{1:D}/" + BaseUrl;
        }

        private static string GetServerName()
        {
            return BMCRegistryHelper.GetRegKeyValue(@"Cashmaster\Exchange", "Default_Server_Ip");
        }

        public static string BaseUrl
        {
            get { return "BMC/Exchange"; }
        }

        public static string HostIpAddress
        {
            get { return _hostIpAddress; }
        }

        public static Binding Binding
        {
            get { return _binding; }
        }

        public static string ServerUri
        {
            get
            {
                return string.Format(_serverUri, GetServerName(),
                    ExMonitorServerConfigStoreFactory.Store.MonitorServerTcpPort);
            }
        }
    }
}
