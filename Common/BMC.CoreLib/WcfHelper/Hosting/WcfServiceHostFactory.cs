using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    /// <summary>
    /// Wcf Service Host Factory
    /// </summary>
    public class WcfServiceHostFactory 
        : ServiceHostFactory, IWcfServiceHostFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WcfServiceHostFactory"/> class.
        /// </summary>
        public WcfServiceHostFactory() { }

        #region IWcfServiceHostFactory Members

        /// <summary>
        /// Creates the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="hostConfig">The host config.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <returns>Service host instance.</returns>
        public virtual System.ServiceModel.ServiceHost Create(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses)
        {
            return new WcfServiceHost(serviceType, knownTypes, baseAddresses);
        }

        /// <summary>
        /// Creates the specified singleton instance.
        /// </summary>
        /// <param name="singletonInstance">The singleton instance.</param>
        /// <param name="hostConfig">The host config.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <returns>Service host instance.</returns>
        public virtual System.ServiceModel.ServiceHost Create(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses)
        {
            return new WcfServiceHost(singletonInstance, knownTypes, baseAddresses);
        }

        #endregion
    }
}
