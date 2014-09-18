using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DataXChangeEndPointService.Service
{
    [ServiceContract(Namespace = "http://www.ballytech.com/sds/data"),]
    public interface DataXChangeEndPoint
    {
        // Methods
        [OperationContract(Action = "", ReplyAction = "*"), XmlSerializerFormat]
        sendResponseResponse1 sendResponse(sendResponseRequest request);
    }

    [Serializable, XmlType(Namespace = "http://www.ballytech.com/sds/data"),]
    public class sendResponse
    {
        // Fields
        private string responseMessageField;
        private int siteNumberField;
        private string sourceField;

        // Properties
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public string responseMessage
        {
            get
            {
                return this.responseMessageField;
            }
            set
            {
                this.responseMessageField = value;
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int siteNumber
        {
            get
            {
                return this.siteNumberField;
            }
            set
            {
                this.siteNumberField = value;
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }
    }

    [Serializable, MessageContract(IsWrapped = false)]
    public class sendResponseRequest
    {
        // Fields
        [MessageBodyMember(Namespace = "http://www.ballytech.com/sds/data", Order = 0)]
        public sendResponse sendResponse;

        // Methods
        public sendResponseRequest()
        {
        }

        public sendResponseRequest(sendResponse sendResponse)
        {
            this.sendResponse = sendResponse;
        }
    }

    [Serializable, XmlType(Namespace = "http://www.ballytech.com/sds/data")]
    public class sendResponseResponse
    {
    }

    [Serializable, MessageContract(IsWrapped = false)]
    public class sendResponseResponse1
    {
        // Fields
        [MessageBodyMember(Namespace = "http://www.ballytech.com/sds/data", Order = 0)]
        public sendResponseResponse sendResponseResponse;

        // Methods
        public sendResponseResponse1()
        {
        }

        public sendResponseResponse1(sendResponseResponse sendResponseResponse)
        {
            this.sendResponseResponse = sendResponseResponse;
        }
    }
}
