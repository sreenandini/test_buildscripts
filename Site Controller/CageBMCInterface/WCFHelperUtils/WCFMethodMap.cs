using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.Xml;
using System.ServiceModel.Channels;

namespace WCFHelperUtils
{
    /// <summary>
    /// WCF Method Map
    /// </summary>
    public class WCFMethodMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WCFMethodMap"/> class.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        public WCFMethodMap(ServiceDescription serviceDescription)
        {
            this.PrepareMap(serviceDescription);
        }

        /// <summary>
        /// Prepares the map.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        private void PrepareMap(ServiceDescription serviceDescription)
        {
            IDictionary<string, WCFMethodMessage> methods = new SortedDictionary<string, WCFMethodMessage>();
            IList<string> actions = new List<string>();

            foreach (ServiceEndpoint epDesc in serviceDescription.Endpoints)
            {
                ContractDescription ctDesc = epDesc.Contract;
                if (ctDesc.ContractType == typeof(IMetadataExchange)) continue;
                string nsContract = ctDesc.Namespace;

                foreach (OperationDescription opDesc in ctDesc.Operations)
                {
                    string action = string.Empty;
                    string name = opDesc.Name;
                    WCFMethodMessage methodMessage = new WCFMethodMessage(name);
                    bool isMessageContract = false;

                    foreach (MessageDescription mDesc in opDesc.Messages)
                    {
                        string paramName = name;

                        if (mDesc.MessageType != null &&
                            mDesc.Body != null)
                        {
                            bool isWrapped = (!string.IsNullOrEmpty(mDesc.Body.WrapperNamespace)
                                                || !string.IsNullOrEmpty(mDesc.Body.WrapperName));
                            if (isWrapped)
                            {
                                paramName = mDesc.Body.WrapperName;
                                isMessageContract = true;
                            }
                        }

                        if (mDesc.Direction == MessageDirection.Input)
                        {
                            methodMessage.Input = paramName;
                            action = mDesc.Action;
                        }
                        else if (mDesc.Direction == MessageDirection.Output)
                        {
                            methodMessage.Output = mDesc.Action;
                        }
                    }

                    if (string.IsNullOrEmpty(action))
                    {
                        action = nsContract + "/" + name;
                    }

                    actions.Add(action);
                    methodMessage.Action = action;
                    methodMessage.IsMessageContract = isMessageContract;
                    methods.Add(action, methodMessage);
                }
            }

            // empty action for message routing
            actions.Add(string.Empty);

            // methods and actions
            this.Methods = methods;
            this.Actions = actions.ToArray();
        }

        /// <summary>
        /// Gets the method message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>WCF Method Message</returns>
        public WCFMethodMessage GetMethodMessage(ref System.ServiceModel.Channels.Message message)
        {
            string action = message.Headers.Action;
            WCFMethodMessage methodMessage = null;

            if (!string.IsNullOrEmpty(action))
            {
                if (this.Methods.ContainsKey(action))
                {
                    methodMessage = this.Methods[action];
                }
            }

            if (methodMessage == null)
            {
                try
                {
                    XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
                    string root = bodyReader.LocalName;
                    WCFMethodMessage methodMessage2 = this.Methods.Values.FirstOrDefault(s => string.Compare(s.Input, root, true) == 0);
                    if (methodMessage2 != null)
                    {
                        methodMessage = methodMessage2;
                        Message message2 = Message.CreateMessage(message.Version, methodMessage2.Action, bodyReader);
                        message = message2;
                    }
                }
                finally
                {
                }
            }

            return methodMessage;
        }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>The actions.</value>
        public string[] Actions { get; private set; }

        /// <summary>
        /// Gets or sets the methods.
        /// </summary>
        /// <value>The methods.</value>
        public IDictionary<string, WCFMethodMessage> Methods { get; private set; }
    }
}
