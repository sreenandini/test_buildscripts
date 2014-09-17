using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Behaviors;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    /// <summary>
    /// Wcf Service Host Base Class
    /// </summary>
    public abstract class WcfServiceHostBase : ServiceHost, IWcfServiceHost
    {
        protected Type _serviceType = null;
        protected Type[] _knownTypes = null;
        protected ServiceMetadataBehavior _serviceMetadata = null;

        /// <summary>
        /// Behavior entries collection.
        /// </summary>
        //private WcfBehaviorEntryCollection _behaviorEntries = null;
        private WcfServiceHostParam _hostParam = new WcfServiceHostParam();

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfServiceHostBase"/> class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public WcfServiceHostBase(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _serviceType = serviceType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfServiceHostBase"/> class.
        /// </summary>
        /// <param name="singletonInstance">The instance of the hosted service.</param>
        /// <param name="baseAddresses">An <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the hosted service.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="singletonInstance"/> is null.</exception>
        public WcfServiceHostBase(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses)
        {
            _serviceType = singletonInstance.GetType();
        }

        public WcfServiceHostBase(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _serviceType = serviceType;
            _knownTypes = knownTypes;
        }

        public WcfServiceHostBase(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses)
        {
            _serviceType = singletonInstance.GetType();
            _knownTypes = knownTypes;
        }

        /// <summary>
        /// Loads the service description information from the configuration file and applies it to the runtime being constructed.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The description of the service hosted is null.</exception>
        protected override void ApplyConfiguration()
        {
            this.Initialize();
            base.ApplyConfiguration();
            this.ApplyServiceBehavior();
            this.InitEvents();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            OnHostInitialized(this);
        }

        /// <summary>
        /// Applies the service behavior.
        /// </summary>
        protected virtual void ApplyServiceBehavior()
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "ApplyServiceBehavior");

            try
            {
                this.ApplyServiceBehaviorInternal(_hostParam);

                // add the service behaviors
                _hostParam.ServiceBehaviors.ForEachItem((s) =>
                {
                    this.Description.Behaviors.Add(s);
                });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void ApplyServiceBehaviorInternal(WcfServiceHostParam param) { }

        private void ApplyServiceBehaviorOnOpening()
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "ApplyServiceBehaviorOnOpening");

            try
            {
                this.ApplyServiceBehaviorOnOpeningInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void ApplyServiceBehaviorOnOpeningInternal()
        {
            ServiceBehaviorAttribute attrService = (from a in _serviceType.GetCustomAttributes(typeof(ServiceBehaviorAttribute), true).OfType<ServiceBehaviorAttribute>()
                                                    select a).FirstOrDefault();
            if (attrService != null &&
                !attrService.Namespace.IsEmpty())
            {
                foreach (ServiceEndpoint sep in this.Description.Endpoints)
                {
                    if (sep.Contract.ContractType != typeof(IMetadataExchange))
                    {
                        sep.Binding.Namespace = attrService.Namespace;
                    }
                }
            }
        }

        /// <summary>
        /// Inits the events.
        /// </summary>
        private void InitEvents()
        {
            this.Opened += new EventHandler(OnServiceHost_Opened);
            this.Closed += new EventHandler(OnServiceHost_Closed);
        }

        private void LogMessage(ModuleProc PROC, string message)
        {
            string fullMessage = string.Empty;
            if (this.Description != null)
            {
                if (this.Description.ServiceType != null)
                {
                    fullMessage = this.Description.ServiceType.Name + " => ";
                }
            }

            fullMessage += message;
            Log.Info(PROC, fullMessage);
        }

        /// <summary>
        /// Called when service host opened.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnServiceHost_Opened(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "OnServiceHost_Opened");

            try
            {                
                this.ApplyServiceBehaviorOnOpening();

                ServiceHostBase host = sender as ServiceHostBase;
                if (host != null &&
                    host.ChannelDispatchers != null)
                {
                    foreach (ChannelDispatcher cdisp in host.ChannelDispatchers)
                    {
                        string logMessage = "::: CHANNEL URI : " + cdisp.Listener.Uri.AbsoluteUri + " (" + cdisp.BindingName + ") is opened.";
                        LogMessage(PROC, logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        /// <summary>
        /// Called when service host closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnServiceHost_Closed(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "OnServiceHost_Closed");

            try
            {
                ServiceHostBase host = sender as ServiceHostBase;
                if (host != null &&
                    host.ChannelDispatchers != null)
                {
                    foreach (ChannelDispatcher cdisp in host.ChannelDispatchers)
                    {
                        string logMessage = "::: CHANNEL URI : " + cdisp.Listener.Uri.AbsoluteUri + " (" + cdisp.BindingName + ") is closed.";
                        LogMessage(PROC, logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static event WcfServiceHostInitializedHandler HostInitialized = null;

        private static void OnHostInitialized(WcfServiceHostBase host)
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "OnHostInitialized");

            try
            {
                if (HostInitialized != null)
                {
                    HostInitialized(host);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #region Overridden Methods

        protected override void OnOpening()
        {
            base.OnOpening();
            this.AddOperationKnownTypes();
            this.ModifyBehaviorsOnOpening();
        }

        private void AddOperationKnownTypes()
        {
            ModuleProc PROC = new ModuleProc("TISWinServiceHost", "AddOperationKnownTypes");

            try
            {
                if (_knownTypes != null)
                {
                    foreach (ServiceEndpoint ep in this.Description.Endpoints)
                    {
                        ContractDescription cd = ep.Contract;                        
                        if (cd.ContractType != typeof(IMetadataExchange))
                        {
                            foreach (OperationDescription od in cd.Operations)
                            {
                                foreach (Type knownType in _knownTypes)
                                {
                                    if (od.KnownTypes.IndexOf(knownType) == -1)
                                    {
                                        od.KnownTypes.Add(knownType);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void ModifyBehaviorsOnOpening() { }

        protected virtual void AddDefaultServiceBehaviors()
        {
            ModuleProc PROC = new ModuleProc("TISWinServiceHost", "AddDefaultServiceBehaviors");

            try
            {
                // ServiceMetadataBehavior
                _serviceMetadata = this.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (_serviceMetadata == null)
                {
                    _serviceMetadata = new ServiceMetadataBehavior();
                    this.Description.Behaviors.Add(_serviceMetadata);
                }
                _serviceMetadata.HttpGetEnabled = false;
                //bServiceMetadata.HttpGetUrl = uriHttp;                            

                // ServiceDebugBehavior
                ServiceDebugBehavior bServiceDebug = this.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (bServiceDebug == null)
                {
                    bServiceDebug = new ServiceDebugBehavior();
                    this.Description.Behaviors.Add(bServiceDebug);
                }
                bServiceDebug.IncludeExceptionDetailInFaults = true;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        #endregion

        #region IWcfServiceHost Members

        /// <summary>
        /// Starts the customizaed Wcf Service Host.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        public virtual bool Start()
        {
            ModuleProc PROC = new ModuleProc("WindowsWcfServiceHostFactory", "Start");
            bool result = false;

            try
            {
                Log.Info(PROC, "Inside Start...");
                this.OnStart();
                
                this.Open();
                result = (this.State == CommunicationState.Opening ||
                            this.State == CommunicationState.Opened);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (result)
                {
                    Log.Info(PROC, "Service host was started successfully.");
                }
                else
                {
                    Log.Info(PROC, "Unable to start the service host.");
                }
            }
            return result;
        }

        private void OnStart()
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "OnStart");

            try
            {
                this.OnStartInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void OnStartInternal() { }

        /// <summary>
        /// Stops the customizaed Wcf Service Host.
        /// </summary>
        /// <returns>True if succeeded; otherwise false.</returns>
        public virtual bool Stop()
        {
            ModuleProc PROC = new ModuleProc("HostingServiceWrapper", "Stop");
            bool result = false;

            try
            {               
                Log.Info(PROC, "Inside Stop...");
                this.OnStop();

                this.Close();
                result = (this.State == CommunicationState.Closing ||
                            this.State == CommunicationState.Closed);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (result)
                {
                    Log.Info(PROC, "Service host was stopped successfully.");
                }
                else
                {
                    Log.Info(PROC, "Unable to stop the service host.");
                }
            }
            return result;
        }

        private void OnStop()
        {
            ModuleProc PROC = new ModuleProc("WcfServiceHostBase", "OnStop");

            try
            {
                this.OnStopInternal();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void OnStopInternal() { }

        #endregion
    }

    public class WcfServiceHostParam : DisposableObject
    {
        public WcfServiceHostParam()
        {
            this.ServiceBehaviors = new List<IServiceBehavior>();
        }

        public IList<IServiceBehavior> ServiceBehaviors { get; private set; }

        public bool SingleWsdl { get; set; }
    }

    public delegate void WcfServiceHostInitializedHandler(WcfServiceHostBase host);
}
