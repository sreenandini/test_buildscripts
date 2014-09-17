using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using BMC.CoreLib.Diagnostics;
using System.ServiceModel.Web;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    /// <summary>
    /// Wcf Service Host Base Class
    /// </summary>
    public abstract class WcfWebServiceHostBase
        : WebServiceHost, IWcfServiceHost
    {
        /// <summary>
        /// Behavior entries collection.
        /// </summary>
        //private WcfBehaviorEntryCollection _behaviorEntries = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfWebServiceHostBase"/> class.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public WcfWebServiceHostBase(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WcfWebServiceHostBase"/> class.
        /// </summary>
        /// <param name="singletonInstance">The instance of the hosted service.</param>
        /// <param name="baseAddresses">An <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses for the hosted service.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="singletonInstance"/> is null.</exception>
        public WcfWebServiceHostBase(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses) { }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            //_behaviorEntries = new WcfBehaviorEntryCollection();
        }

        /// <summary>
        /// Loads the service description information from the configuration file and applies it to the runtime being constructed.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The description of the service hosted is null.</exception>
        protected override void ApplyConfiguration()
        {
            this.Initialize();
            base.ApplyConfiguration();
            //this.AddBehaviorEntries(_behaviorEntries);
            this.ApplyServiceBehavior();
            this.InitEvents();
        }

        /// <summary>
        /// Inits the events.
        /// </summary>
        private void InitEvents()
        {
            this.Opened += new EventHandler(OnWebServiceHost_Opened);
            this.Closed += new EventHandler(OnWebServiceHost_Closed);
        }

        /// <summary>
        /// Called when service host opened.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnWebServiceHost_Opened(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("WcfWebServiceHostBase", "OnWebServiceHost_Opened");

            ServiceHostBase host = sender as ServiceHostBase;
            if (host != null &&
                host.ChannelDispatchers != null)
            {
                foreach (ChannelDispatcher cdisp in host.ChannelDispatchers)
                {
                    Log.Info(PROC, "Listen Uri : " + cdisp.Listener.Uri.AbsoluteUri);
                }
            }
        }

        /// <summary>
        /// Called when service host closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnWebServiceHost_Closed(object sender, EventArgs e)
        {

        }

        ///// <summary>
        ///// Gets the behavior entries.
        ///// </summary>
        ///// <value>The behavior entries.</value>
        //internal WcfBehaviorEntryCollection BehaviorEntries
        //{
        //    get
        //    {
        //        return _behaviorEntries;
        //    }
        //}

        /// <summary>
        /// Applies the service behavior.
        /// </summary>
        private void ApplyServiceBehavior()
        {
            ModuleProc PROC = new ModuleProc("WcfWebServiceHostBase", "ApplyServiceBehavior");

            try
            {
                this.ApplyServiceBehaviorInternal();
                ServiceDescription desc = this.Description;
                if (desc != null)
                {
                    //WcfServiceBehavior defaultBehavior = desc.Behaviors.Find<WcfServiceBehavior>();
                    //if (defaultBehavior == null &&
                    //    !_behaviorEntries.HasCustomServiceBehavior)
                    //{
                    //    defaultBehavior = new WcfServiceBehavior();
                    //    defaultBehavior.BehaviorEntries = _behaviorEntries;
                    //    desc.Behaviors.Add(defaultBehavior);
                    //    Log.Info(PROC,
                    //        string.Format("Default service behavior [{0}] was successfully added.",
                    //        defaultBehavior.GetType().Name));
                    //}
                    //foreach (WcfBehaviorEntry behaviorEntry in _behaviorEntries)
                    //{
                    //    // service behavior (either default or custom)
                    //    WcfServiceBehavior customBehavior = defaultBehavior;
                    //    if (behaviorEntry.ServiceBehavior != null)
                    //    {
                    //        customBehavior = behaviorEntry.ServiceBehavior;
                    //        if (customBehavior.GetType() != typeof(WcfServiceBehavior))
                    //        {
                    //            customBehavior.BehaviorEntries = _behaviorEntries;
                    //            desc.Behaviors.Add(customBehavior);
                    //            Log.Info(PROC,
                    //                string.Format("Custom service behavior [{0}] was successfully added.",
                    //                customBehavior.GetType().Name));
                    //        }
                    //    }

                    //    // update the service behavior
                    //    behaviorEntry.ServiceBehavior = customBehavior;
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        protected virtual void ApplyServiceBehaviorInternal() { }

        #region Overridden Methods

        /// <summary>
        /// Adds the behavior entries.
        /// </summary>
        /// <param name="entries">The entries.</param>
        //protected virtual void AddBehaviorEntries(WcfBehaviorEntryCollection entries) { }
        protected override void OnOpening()
        {
            base.OnOpening();
            this.AddDefaultBehaviors();
            this.ModifyBehaviorsOnOpening();
        }

        private void AddDefaultBehaviors()
        {
            ModuleProc PROC = new ModuleProc("WcfWebServiceHostBase", "AddDefaultBehaviors");

            try
            {
                foreach (ServiceEndpoint sep in this.Description.Endpoints)
                {
                    try
                    {
                        if (sep.Binding.GetType() == typeof(WebHttpBinding))
                        {
                            WebHttpBehavior httpBehavior = sep.Behaviors.Find<WebHttpBehavior>();
                            if (httpBehavior == null)
                            {
                                httpBehavior = new WebHttpBehavior();
                                sep.Behaviors.Add(httpBehavior);
                            }
                            httpBehavior.DefaultOutgoingResponseFormat = WebMessageFormat.Json;
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

        }

        protected virtual void ModifyBehaviorsOnOpening() { }

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

        #endregion
    }
}
