using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using BMC.EBSComms.Contracts.Configuration;

namespace BMC.EBSComms.Contracts.Dto.SMS2EBS
{
    [DataContract(Namespace = NamesHelper.NS_SMS2EBS_DC_13_0,
                  Name = "RequestType")]
    public class RequestType_13_0
    {
        [DataMember(Name = "Request", IsRequired = true)]
        public string request { get; set; }
    }

    [DataContract(Namespace = NamesHelper.NS_SMS2EBS_DC_13_0,
                  Name = "ResponseType")]
    public class ResponseType_13_0
    {
        [DataMember(Name = "Response", IsRequired = true)]
        public string response { get; set; }
    }

    [DataContract(Namespace = NamesHelper.NS_SMS2EBS_13_1,
                  Name = "RequestType")]
    public class RequestType_13_1
    {
        [DataMember(Name = "Request", IsRequired = true)]
        public string Request { get; set; }
    }

    [DataContract(Namespace = NamesHelper.NS_SMS2EBS_13_1,
                  Name = "ResponseType")]
    public class ResponseType_13_1
    {
        [DataMember(Name = "Response", IsRequired = true)]
        public string Response { get; set; }
    }
}
