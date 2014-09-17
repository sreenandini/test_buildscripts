using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BMC.EBSComms.Contracts.Dto.EBS2SMS
{
    [DataContract(Namespace = "http://www.gamingstandards.com/s2s/wsdl/v1.1/S2SMessageService.wsdl",
        Name = "RequestType")]
    public class RequestType
    {
        [DataMember(Name = "request", IsRequired = true)]
        public string request { get; set; }
    }

    [DataContract(Namespace = "http://www.gamingstandards.com/s2s/wsdl/v1.1/S2SMessageService.wsdl",
        Name = "ResponseType")]
    public class ResponseType
    {
        [DataMember(Name = "response", IsRequired = true)]
        public string response { get; set; }
    }

    [DataContract(Namespace = "http://www.ballytech.com/sds/s2s",
        Name = "processS2SMessage")]
    [Serializable()]
    public class processS2SMessage
    {
        [DataMember(Name = "s2sMessage", IsRequired = true)]
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string s2sMessage { get; set; }
    }

    [DataContract(Namespace = "http://www.ballytech.com/sds/s2s",
        Name = "processS2SMessageResponse")]
    [Serializable()]
    public class processS2SMessageResponse
    {
        [DataMember(Name = "return", IsRequired = true)]
        [XmlElement(Form = XmlSchemaForm.Qualified)]
        public string @return { get; set; }
    }
}
