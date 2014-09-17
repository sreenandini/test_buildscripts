using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.CoreLib;
using System.ServiceModel;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Concurrent;
using System.ServiceModel.Description;
using System.IO;
using System.ServiceModel.Channels;
using BMC.CoreLib.IoC;
using BMC.CoreLib.WcfHelper.Helpers;
using System.Net;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.CoreLib.WcfHelper.Behaviors;
using BMC.CoreLib.Services;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class RESTFulAttribute : Attribute
    {
        public RESTFulAttribute() { }
    }

    public class ServiceCallbackAttribute : Attribute
    {
        public ServiceCallbackAttribute() { }
    }

    public interface IWcfExecutorServiceFactory : IDisposable
    {
        Type ServiceType { get; }

        object CreateSingletonInstance(IExecutorService executorService);

        Type[] KnownTypes { get; }
    }

    public abstract class WcfExecutorServiceFactoryBase : DisposableObject, IWcfExecutorServiceFactory
    {
        private object _singletonInstance = null;
        private object _singletonInstanceLock = new object();

        protected WcfExecutorServiceFactoryBase() { }

        public abstract Type ServiceType { get; }

        public object CreateSingletonInstance(IExecutorService executorService)
        {
            if (_singletonInstance == null)
            {
                lock (_singletonInstanceLock)
                {
                    if (_singletonInstance == null)
                    {
                        _singletonInstance = this.OnCreateSingletonInstance(executorService);
                    }
                }
            }

            return _singletonInstance;
        }

        public abstract object OnCreateSingletonInstance(IExecutorService executorService);

        public virtual Type[] KnownTypes
        {
            get { return null; }
        }
    }

    public partial interface IWcfExecutorService : IServiceContractBase { }

    [RESTFul]
    public partial interface IWcfExecutorServiceRest : IServiceContractBase { }

    public abstract class WcfExecutorServiceHostFactory : ExecutorBase, IWcfServiceHost
    {
        private WcfServiceHostBase _host = null;
        private WcfWebServiceHostBase _hostWeb = null;
        private object _lock = new object();

        private IWcfExecutorService _serverInstance = null;
        private IWcfExecutorServiceRest _serverInstanceRest = null;
        private string _basePath = string.Empty;
        private int _tcpPort = 0;
        private int _httpPort = 0;
        private int _webHttpPort = 0;


        public WcfExecutorServiceHostFactory(IExecutorService executorService, string basePath,
            int tcpPort, int httpPort, int webHttpPort)
            : base(executorService)
        {
            _basePath = basePath;
            _tcpPort = tcpPort;
            _httpPort = httpPort;
            _webHttpPort = webHttpPort;
        }

        #region IWcfServiceHost Members

        protected abstract IWcfExecutorServiceFactory OnCreateExecutorService();

        protected virtual IWcfExecutorServiceFactory OnCreateExecutorServiceRest() { return null; }

        protected abstract WcfServiceHostBase OnCreateServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses);

        protected virtual WcfWebServiceHostBase OnCreateWebServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses) { return null; }

        protected virtual Binding OnCreateHttpBinding()
        {
            return ContractBindingsHelper.CreateDualHttpBinding();
        }

        public virtual bool Start()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Start");
            bool result = false;

            try
            {
                if (_host == null)
                {
                    lock (_lock)
                    {
                        if (_host == null)
                        {
                            // Host
                            string hostName = Dns.GetHostName();

                            // Service implementation factory instances
                            IWcfExecutorServiceFactory executorServiceFactory = this.OnCreateExecutorService();
                            IWcfExecutorServiceFactory executorServiceFactoryRest = this.OnCreateExecutorServiceRest();
                            if (executorServiceFactory == null) return false;
                            Type[] knownTypes = executorServiceFactory.KnownTypes;

                            // Service implementation types
                            _serverInstance = executorServiceFactory.CreateSingletonInstance(this.ExecutorService) as IWcfExecutorService;
                            Type serviceType = executorServiceFactory.ServiceType;
                            Type serviceTypeRest = null;
                            Type[] knownTypesRest = null;

                            if (executorServiceFactoryRest != null)
                            {
                                _serverInstanceRest = executorServiceFactoryRest.CreateSingletonInstance(this.ExecutorService) as IWcfExecutorServiceRest;
                                serviceTypeRest = executorServiceFactoryRest.ServiceType;
                                knownTypesRest = executorServiceFactoryRest.KnownTypes;
                            }

                            // Base address and bindings
                            AddressHelper.SetBasePath(_basePath);
                            Uri tcpUri = null, httpUri = null, httpUriWeb = null;
                            bool hasTcp = false, hasHttp = false, hasWebHttp = false;
                            IList<Uri> hostAddresses = new List<Uri>();
                            Binding bindingTcp = null, bindingHttp = null, bindingWebHttp = null;

                            // uris
                            if (_tcpPort > 0)
                            {
                                tcpUri = AddressHelper.CreateTcpAddress(hostName, _tcpPort);
                                bindingTcp = ContractBindingsHelper.CreateTcpBinding();
                                hostAddresses.Add(tcpUri);
                                hasTcp = true;
                            }
                            if (_httpPort > 0)
                            {
                                httpUri = AddressHelper.CreateHttpAddress(hostName, _httpPort);
                                bindingHttp = this.OnCreateHttpBinding();
                                hostAddresses.Add(httpUri);
                                hasHttp = true;
                            }
                            if (_webHttpPort > 0)
                            {
                                httpUriWeb = AddressHelper.CreateHttpAddress(hostName, _webHttpPort);
                                bindingWebHttp = ContractBindingsHelper.CreateWebHttpBinding();
                                hasWebHttp = true;
                            }

                            // default service
                            if ((hasTcp || hasHttp) &&
                                (serviceType != null))
                            {
                                _host = this.OnCreateServiceHost(_serverInstance, knownTypes, hostAddresses.ToArray());
                                var ifaceInfos = this.OnGetServiceContractInterfaces(serviceType);

                                foreach (var ifaceInfo in ifaceInfos)
                                {
                                    // web service host
                                    RESTFulAttribute restFul = (from c in ifaceInfo.ContractType.GetCustomAttributes(true)
                                                                where c.ToString() == typeof(RESTFulAttribute).ToString()
                                                                select c).FirstOrDefault() as RESTFulAttribute;
                                    if (restFul == null)
                                    {
                                        // tcp
                                        if (hasTcp)
                                        {
                                            _host.AddServiceEndpoint(ifaceInfo.ContractType, bindingTcp, string.Empty);
                                            Log.Info("Service Endpoint (Tcp) added for " + ifaceInfo.ContractType.FullName);
                                        }

                                        // http
                                        if (hasHttp)
                                        {
                                            _host.AddServiceEndpoint(ifaceInfo.ContractType, bindingHttp, string.Empty);
                                            Log.Info("Service Endpoint (Http) added for " + ifaceInfo.ContractType.FullName);
                                        }
                                    }
                                }

                                if (hasHttp)
                                {
                                    ServiceMetadataBehavior metadataBehavior = _host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                                    if (metadataBehavior != null)
                                    {
                                        metadataBehavior.HttpGetEnabled = true;
                                        metadataBehavior.HttpGetUrl = httpUri;
                                    }
                                }
                            }

                            // rest service
                            if (hasWebHttp &&
                                (serviceTypeRest != null))
                            {
                                var ifaceInfos = this.OnGetServiceContractInterfaces(serviceTypeRest);

                                foreach (var ifaceInfo in ifaceInfos)
                                {
                                    // web service host
                                    RESTFulAttribute restFul = (from c in ifaceInfo.ContractType.GetCustomAttributes(true)
                                                                where c.ToString() == typeof(RESTFulAttribute).ToString()
                                                                select c).FirstOrDefault() as RESTFulAttribute;
                                    if (restFul == null)
                                    {
                                        restFul = (from a in ifaceInfo.ContractType.GetInterfaces()
                                                   from c in a.GetCustomAttributes(true)
                                                   where c.ToString() == typeof(RESTFulAttribute).ToString()
                                                   select c).FirstOrDefault() as RESTFulAttribute;
                                    }

                                    if (restFul != null)
                                    {
                                        // web service host
                                        if (_hostWeb == null)
                                        {
                                            _hostWeb = this.OnCreateWebServiceHost(_serverInstanceRest, knownTypesRest, new Uri[] { });
                                        }

                                        // http
                                        Uri webUri = new Uri(Path.Combine(httpUriWeb.AbsoluteUri, ifaceInfo.ContractAttribute.Name));
                                        _hostWeb.AddServiceEndpoint(ifaceInfo.ContractType, bindingWebHttp, webUri);
                                        Log.Info("Service Endpoint (WebHttp) added for " + ifaceInfo.ContractType.FullName);
                                    }
                                }
                            }

                            // Mex endpoings
                            if (hasTcp)
                            {
                                _host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                                Log.Info("Mex Service Endpoint (Tcp) added");
                            }
                            if (hasHttp)
                            {
                                _host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                                Log.Info("Mex Service Endpoint (Http) added");
                            }

                            // mex endpoint is not required for web http, so please don't uncomment this line
                            if (_hostWeb != null)
                            {
                                //_hostWeb.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(),
                                //   "mex");
                            }
                        }
                    }
                }

                // default service host
                try
                {
                    if (_host != null &&
                        _host.State == CommunicationState.Created)
                    {
                        _host.Open();
                        _mreShutdown.Reset();
                        if (_serverInstance is IListener)
                        {
                            ((IListener)_serverInstance).Start();
                        }
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }

                // web service host
                if (result &&
                    (_hostWeb != null))
                {
                    try
                    {
                        if (_hostWeb != null &&
                            _hostWeb.State == CommunicationState.Created)
                        {
                            _hostWeb.Open();
                            if (_serverInstanceRest is IListener)
                            {
                                ((IListener)_serverInstanceRest).Start();
                            }
                            result = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        protected class ServiceContractInterfaceInfo : DisposableObject
        {
            public ServiceContractInterfaceInfo(Type contractType, ServiceContractAttribute contractAttribute)
            {
                this.ContractType = contractType;
                this.ContractAttribute = contractAttribute;
            }
            public Type ContractType { get; private set; }
            public ServiceContractAttribute ContractAttribute { get; private set; }
        }

        protected virtual ServiceContractInterfaceInfo[] OnGetServiceContractInterfaces(Type serviceType)
        {
            string serviceContractAttrString = typeof(ServiceContractAttribute).ToString();
            return (from i in serviceType.GetInterfaces()
                    from c in i.GetCustomAttributes(true)
                    where c.ToString() == serviceContractAttrString &&
                            this.IsHostingTypeDefined(i)
                    select new ServiceContractInterfaceInfo(i, c as ServiceContractAttribute)).ToArray();
        }

        protected virtual bool IsHostingTypeDefined(Type type)
        {
            return true;
        }

        public virtual bool Stop()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Stop");

            try
            {
                if (_host != null)
                {
                    lock (_lock)
                    {
                        if (_host != null)
                        {
                            if (_serverInstance is IListener)
                            {
                                ((IListener)_serverInstance).Stop();
                            }
                            _host.Close();
                            _host = null;

                            try
                            {
                                if (_hostWeb != null)
                                {

                                    try
                                    {
                                        if (_hostWeb != null)
                                        {
                                            if (_serverInstanceRest is IListener)
                                            {
                                                ((IListener)_serverInstanceRest).Stop();
                                            }
                                            _hostWeb.Close();
                                            _hostWeb = null;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Exception(PROC, ex);
                                    }
                                    _hostWeb.Close();
                                    _hostWeb = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }

                            this.Shutdown();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _host = null;
                _hostWeb = null;
            }

            return false;
        }

        #endregion
    }

    public class WcfExecutorServiceHost : WcfServiceHost
    {
        public WcfExecutorServiceHost(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, knownTypes, baseAddresses) { }

        public WcfExecutorServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, knownTypes, baseAddresses) { }

        protected override void ApplyServiceBehaviorInternal(WcfServiceHostParam param)
        {
            base.ApplyServiceBehaviorInternal(param);
            param.SingleWsdl = true;
        }
    }

    public class WcfExecutorWebServiceHost : WcfWebServiceHost
    {
        public WcfExecutorWebServiceHost(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, knownTypes, baseAddresses) { }

        public WcfExecutorWebServiceHost(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, knownTypes, baseAddresses) { }
    }
}
