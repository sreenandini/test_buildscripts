using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib.WcfHelper.Hosting;
using BMC.EBSComms.Contracts.Messages.EBS2SMS;

namespace BMC.EBSComms.Contracts.Interfaces.EBS2SMS
{
    [ServiceContract(Namespace = "http://www.gamingstandards.com/s2s/wsdl/v1.1/S2SMessageService.wsdl",
        Name = "S2SPlayerInfoSoap",
        SessionMode = SessionMode.Allowed)]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
    public interface IEBSCommServer : IWcfExecutorService
    {
        [OperationContract(Action = "http://www.gamingstandards.com:8080/S2SWebsite/services/SlotSystemHost",
            ReplyAction = "http://www.gamingstandards.com:8080/S2SWebsite/services/SlotSystemHost")]
        [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gamingstandards.com:8080/S2SWebsite/services/SlotSystemHost", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        S2SMessagePostOperationSoapOut S2SMessagePostOperation(S2SMessagePostOperationSoapIn request);
    }

    [ServiceContract(Namespace = "http://www.ballytech.com/sds/s2s",
        Name = "S2SEndPoint",
        SessionMode = SessionMode.Allowed)]
    [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
    public interface IEBSCommServer_13_1 : IWcfExecutorService
    {
        [OperationContract(Action = "*",
            ReplyAction = "*")]
        [XmlSerializerFormat(Style = OperationFormatStyle.Document, Use = OperationFormatUse.Literal)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        S2SEndPoint_processS2SMessageResponse processS2SMessage(S2SEndPoint_processS2SMessage request);
    }
}
