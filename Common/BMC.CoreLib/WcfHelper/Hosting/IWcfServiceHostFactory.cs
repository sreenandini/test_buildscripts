using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace BMC.CoreLib.WcfHelper.Hosting
{
    /// <summary>
    /// Wcf Service Host Factory
    /// </summary>
    public interface IWcfServiceHostFactory
    {
        /// <summary>
        /// Creates the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <returns>Service host instance.</returns>
        ServiceHost Create(Type serviceType, Type[] knownTypes, params Uri[] baseAddresses);

        /// <summary>
        /// Creates the specified singleton instance.
        /// </summary>
        /// <param name="singletonInstance">The singleton instance.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        /// <returns>Service host instance.</returns>
        ServiceHost Create(object singletonInstance, Type[] knownTypes, params Uri[] baseAddresses);
    }
}
