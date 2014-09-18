using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFHelperUtils
{
    /// <summary>
    /// WCFServiceExtensionElement
    /// </summary>
    public abstract class WCFServiceExtensionElement : BehaviorExtensionElement, IServiceBehavior
    {
        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/>.</returns>
        public override Type BehaviorType
        {
            get { return typeof(WCFServiceExtensionElement); }
        }

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected override object CreateBehavior()
        {
            return CreateExtensionObject();
        }

        /// <summary>
        /// Creates the extension object.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected abstract WCFServiceExtensionElement CreateExtensionObject();

        /// <summary>
        /// Creates the action filter.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns>The message filter.</returns>
        protected virtual MessageFilter CreateActionFilter(EndpointDispatcher dispatcher) { return null; }

        /// <summary>
        /// Creates the operation selector.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns>The dispatch operation selector.</returns>
        protected virtual IDispatchOperationSelector CreateOperationSelector(EndpointDispatcher dispatcher) { return null; }

        /// <summary>
        /// Called when [process dispatch behavior].
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host base.</param>
        protected virtual void OnProcessDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase) { }

        /// <summary>
        /// Called when [iterate channel dispatcher].
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        protected virtual void OnIterateChannelDispatcher(ChannelDispatcher dispatcher) { }

        /// <summary>
        /// Called when [iterate endpoint dispatcher].
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        protected virtual void OnIterateEndpointDispatcher(EndpointDispatcher dispatcher) { }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public virtual void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The host that is currently being built.</param>
        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            this.OnProcessDispatchBehavior(serviceDescription, serviceHostBase);

            foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
            {
                // on each channel dispatcher
                this.OnIterateChannelDispatcher(cd);

                foreach (EndpointDispatcher ep in cd.Endpoints)
                {
                    // Action Filter
                    MessageFilter actionFilter = this.CreateActionFilter(ep);
                    if (actionFilter != null) ep.ContractFilter = actionFilter;

                    // Operation Selector
                    IDispatchOperationSelector operationSelector = this.CreateOperationSelector(ep);
                    if (operationSelector != null) ep.DispatchRuntime.OperationSelector = operationSelector;

                    // on each endpoint dispatcher
                    this.OnIterateEndpointDispatcher(ep);
                }
            }
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public virtual void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase) { }
    }
}
