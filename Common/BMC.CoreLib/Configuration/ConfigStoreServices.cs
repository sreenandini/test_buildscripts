using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Hosting;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BMC.CoreLib.Concurrent;
using System.Diagnostics;
using System.ServiceModel.Channels;
using BMC.CoreLib.WcfHelper.Contracts;
using System.Net;

namespace BMC.CoreLib.Configuration
{
    [ServiceContract(Namespace = "BMC.CoreLib.Configuration",
        Name = "ConfigStoreService",
        SessionMode = SessionMode.Allowed)]
    public interface IConfigStoreService : IServiceContractBase
    {
        #region Users
        [OperationContract(Action = "BMC.CoreLib.Configuration.ConfigStoreService.SendMessage",
            ReplyAction = "BMC.CoreLib.Configuration.ConfigStoreService.SendMessageResponse")]
        ConfigStoreResponse SendMessage(ConfigStoreRequest request);
        #endregion
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ConfigStoreService : DisposableObject, IConfigStoreService
    {
        public ConfigStoreService() { }

        #region IConfigStoreService Members

        public ConfigStoreResponse SendMessage(ConfigStoreRequest request)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SendMessage");
            ConfigStoreResponse response = new ConfigStoreResponse();

            try
            {
                ConfigStoreManager.PushValues(request.Request);
                response.Response = true;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return response;
        }

        #endregion
    }

    internal class ConfigStoreServiceHostFactory : DisposableObject, IWcfServiceHost
    {
        private WcfServiceHost _host = null;
        private object _lock = new object();

        private Type[] _knownTypes = null;

        public ConfigStoreServiceHostFactory(Type[] knownTypes)
        {
            _knownTypes = knownTypes;
        }

        #region IWcfServiceHost Members

        internal static Binding CreateNamedPipeBinding()
        {
            NetNamedPipeBinding binding = new NetNamedPipeBinding()
            {
                CloseTimeout = new TimeSpan(0, 10, 0),
                OpenTimeout = new TimeSpan(0, 10, 0),
                SendTimeout = new TimeSpan(0, 10, 0),
                ReceiveTimeout = new TimeSpan(0, 10, 0),
                TransactionFlow = false,
                TransferMode = TransferMode.Buffered,
                TransactionProtocol = TransactionProtocol.OleTransactions,
                HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
                MaxBufferPoolSize = 524288,
                MaxBufferSize = 65536,
                MaxConnections = 10,
                MaxReceivedMessageSize = 65536
            };
            binding.Security.Mode = NetNamedPipeSecurityMode.Transport;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            return binding;
        }

        public bool Start()
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

                            // Add the base address
                            Type serviceType = typeof(ConfigStoreService);

                            Uri pipeUri = new Uri(ConfigStoreManager.GetPipeName(Process.GetCurrentProcess().Id));
                            Log.Info(PROC, pipeUri.AbsoluteUri);

                            // default servuce
                            if (true)
                            {
                                _host = new WcfServiceHost(serviceType, null, new Uri[] { pipeUri });
                                Type[] interfaces = serviceType.GetInterfaces();
                                string serviceContractAttrString = typeof(ServiceContractAttribute).ToString();
                                if (interfaces != null)
                                {
                                    foreach (Type iface in interfaces)
                                    {
                                        ServiceContractAttribute serviceContractAttr = (from c in iface.GetCustomAttributes(false)
                                                                                        where c.ToString() == serviceContractAttrString
                                                                                        select c).FirstOrDefault() as ServiceContractAttribute;
                                        if (serviceContractAttr != null)
                                        {
                                            // named pipe
                                            _host.AddServiceEndpoint(iface, CreateNamedPipeBinding(),
                                                pipeUri);
                                        }
                                    }
                                }
                            }

                            // Mex endpoings
                            _host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                "mex");
                            Log.Info(PROC, pipeUri.AbsoluteUri + "/mex");
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
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool Stop()
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
                            _host.Close();
                            _host = null;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                _host = null;
            }

            return false;
        }

        #endregion
    }

    public class ConfigStoreServiceProxy
       : WcfClientChannel<IConfigStoreService>,
       IConfigStoreService
    {
        #region Constructors
        public ConfigStoreServiceProxy(int processId)
            : base(ConfigStoreServiceHostFactory.CreateNamedPipeBinding(), ConfigStoreManager.GetPipeName(processId)) { }
        #endregion

        #region IConfigStoreService Members

        public ConfigStoreResponse SendMessage(ConfigStoreRequest request)
        {
            return this.InvokeMethod((c) => { return c.SendMessage(request); });
        }

        #endregion
    }
}
