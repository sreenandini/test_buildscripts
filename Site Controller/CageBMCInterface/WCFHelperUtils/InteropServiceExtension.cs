using System;
using System.Collections.Generic;
using System.Text;

namespace WCFHelperUtils
{
    /* 
        <extensions>
            <behaviorExtensions>
                <add name="InteropServiceExtension" type="WCFHelperUtils.InteropServiceExtension, WCFHelperUtils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
            </behaviorExtensions>
        </extensions>
    */
    /// <summary>
    /// InteropServiceExtension
    /// </summary>
    public class InteropServiceExtension : WCFServiceExtensionElement
    {
        private WCFMethodMap _map = null;

        /// <summary>
        /// Creates the extension object.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected override WCFServiceExtensionElement CreateExtensionObject()
        {
            return new InteropServiceExtension();
        }

        /// <summary>
        /// Called when [process dispatch behavior].
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host base.</param>
        protected override void OnProcessDispatchBehavior(System.ServiceModel.Description.ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            _map = new WCFMethodMap(serviceDescription);
        }

        /// <summary>
        /// Creates the action filter.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns>The message filter.</returns>
        protected override System.ServiceModel.Dispatcher.MessageFilter CreateActionFilter(System.ServiceModel.Dispatcher.EndpointDispatcher dispatcher)
        {
            return new WCFActionFilter(_map);
        }

        /// <summary>
        /// Creates the operation selector.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <returns>The dispatch operation selector.</returns>
        protected override System.ServiceModel.Dispatcher.IDispatchOperationSelector CreateOperationSelector(System.ServiceModel.Dispatcher.EndpointDispatcher dispatcher)
        {
            return new WCFOperationSelector(_map);
        }
    }
}
