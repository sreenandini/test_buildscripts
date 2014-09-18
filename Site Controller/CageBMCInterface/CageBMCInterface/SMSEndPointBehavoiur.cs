using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace VoucherEngine
{
    public class SMSEndPointBehavoiur : BehaviorExtensionElement, IEndpointBehavior, IDispatchOperationSelector, IDispatchMessageInspector
    {
        public override Type BehaviorType
        {
            get { return typeof(SMSEndPointBehavoiur); }
        }

        protected override object CreateBehavior()
        {
            return new SMSEndPointBehavoiur();
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.OperationSelector = this;

            ActionMessageFilter mf = endpointDispatcher.ContractFilter as ActionMessageFilter;

            List<string> strList = new List<string>(mf.Actions);
            strList.Add(string.Empty);

            endpointDispatcher.ContractFilter = new ActionMessageFilter(strList.ToArray());
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion IEndpointBehavior Members

        #region IDispatchOperationSelector Members

        public string SelectOperation(ref System.ServiceModel.Channels.Message message)
        {
            XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
            message = CreateMessageCopy(message, bodyReader);
            message.Headers.Action = bodyReader.NamespaceURI + @"/" + bodyReader.LocalName;
            return bodyReader.LocalName;
        }

        #endregion IDispatchOperationSelector Members

        private Message CreateMessageCopy(Message message, XmlDictionaryReader body)
        {
            Message copy = Message.CreateMessage(message.Version, message.Headers.Action, body);
            return copy;
        }

        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            
        }

        #endregion
    }
}