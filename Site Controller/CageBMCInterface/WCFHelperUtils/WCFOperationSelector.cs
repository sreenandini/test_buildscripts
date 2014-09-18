using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;

namespace WCFHelperUtils
{
    public class WCFOperationSelector : IDispatchOperationSelector
    {
        private WCFMethodMap _map = null;

        /// <summary>
        /// Initializes the <see cref="WCFActionFilter"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public WCFOperationSelector(WCFMethodMap map)
        {
            _map = map;
        }

        /// <summary>
        /// Associates a local operation with the incoming method.
        /// </summary>
        /// <param name="message">The incoming <see cref="T:System.ServiceModel.Channels.Message"/> to be associated with an operation.</param>
        /// <returns>
        /// The name of the operation to be associated with the <paramref name="message"/>.
        /// </returns>
        public string SelectOperation(ref System.ServiceModel.Channels.Message message)
        {
            WCFMethodMessage methodMessage = _map.GetMethodMessage(ref message);
            if (methodMessage != null) return methodMessage.MethodName;
            return string.Empty;
        }
    }
}