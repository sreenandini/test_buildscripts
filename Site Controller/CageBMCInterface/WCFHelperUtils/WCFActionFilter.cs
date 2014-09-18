using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Xml;
using System.ServiceModel;
using System.IO;
using System.Text;
using System.Globalization;
using System.Reflection;

namespace WCFHelperUtils
{
    /// <summary>
    /// WCFActionFilter
    /// </summary>
    public class WCFActionFilter : ActionMessageFilter
    {
        /// <summary>
        /// Initializes the <see cref="WCFActionFilter"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public WCFActionFilter(WCFMethodMap map)
            : base(map.Actions) { }

        /// <summary>
        /// Tests whether a message's action matches one of the actions specified in this <see cref="T:System.ServiceModel.Dispatcher.ActionMessageFilter"/>.
        /// </summary>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> to test.</param>
        /// <returns>
        /// true if the <see cref="T:System.ServiceModel.Channels.Message"/> action header matches one of the specified actions; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="message"/> is null.</exception>
        public override bool Match(Message message)
        {
            return base.Match(message);
        }

        /// <summary>
        /// Tests whether a buffered message's action header matches one of the actions specified in this <see cref="T:System.ServiceModel.Dispatcher.ActionMessageFilter"/>.
        /// </summary>
        /// <param name="messageBuffer">The <see cref="T:System.ServiceModel.Channels.MessageBuffer"/> to test.</param>
        /// <returns>
        /// true if the <see cref="T:System.ServiceModel.Channels.MessageBuffer"/> action header matches one of the specified actions; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="messageBuffer"/> is null.</exception>
        public override bool Match(MessageBuffer messageBuffer)
        {
            return base.Match(messageBuffer);
        }
    }
}