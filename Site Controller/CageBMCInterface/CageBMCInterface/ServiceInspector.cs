using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ServiceModel.Dispatcher;
using BMC.Common.LogManagement;
using System.ServiceModel.Description;
using System.Collections.Generic;
using System.Xml;


namespace CageBMCInterface
{
    public class ServiceInspector : IDispatchMessageInspector
    {
        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            LogManager.WriteLog("Cage Request:" + request.ToString() , BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
            //request.Headers.Action = "createVoucher";
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            LogManager.WriteLog("BMC Response :" + reply.ToString(), BMC.Common.LogManagement.LogManager.enumLogLevel.Info);
        }

        #endregion

        //#region IEndpointBehavior Members

        //public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        //{
        //}

        //public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        //{
        //}

        //public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        //{
        //    endpointDispatcher.DispatchRuntime.OperationSelector = this;

        //    ActionMessageFilter mf = endpointDispatcher.ContractFilter as ActionMessageFilter;

        //    List<string> strList = new List<string>(mf.Actions);
        //    strList.Add(string.Empty);

        //    endpointDispatcher.ContractFilter = new ActionMessageFilter(strList.ToArray());
        //}

        //public void Validate(ServiceEndpoint endpoint)
        //{
        //}

        //#endregion IEndpointBehavior Members

        //#region IDispatchOperationSelector Members

        //public string SelectOperation(ref System.ServiceModel.Channels.Message message)
        //{
        //    XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
        //    message = CreateMessageCopy(message, bodyReader);
        //    message.Headers.Action = bodyReader.NamespaceURI + @"/" + bodyReader.LocalName;
        //    return bodyReader.LocalName;
        //}
        //private System.ServiceModel.Channels.Message CreateMessageCopy(System.ServiceModel.Channels.Message message, XmlDictionaryReader body)
        //{
        //    System.ServiceModel.Channels.Message copy = System.ServiceModel.Channels.Message.CreateMessage(message.Version, message.Headers.Action, body);
        //    return copy;
        //}

        //#endregion
    }
}
