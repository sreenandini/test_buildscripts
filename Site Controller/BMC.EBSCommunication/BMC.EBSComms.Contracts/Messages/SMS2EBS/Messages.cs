using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.EBSComms.Contracts.Configuration;
using BMC.EBSComms.Contracts.Dto.SMS2EBS;

namespace BMC.EBSComms.Contracts.Messages.SMS2EBS
{
    [MessageContract(IsWrapped = true,
                     WrapperName = "S2SMessagePostOperation")]
    public class S2SMessagePostOperationSoapIn : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Name = "request")]
        public BMC.EBSComms.Contracts.Dto.SMS2EBS.RequestType_13_0 Request { get; set; }

        public string GetRawMessage()
        {
            ModuleProc PROC = new ModuleProc("S2SMessagePostOperationSoapIn", "GetRawMessage");
            string result = default(string);

            try
            {
                if (this.Request != null)
                {
                    result = this.Request.request;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    [MessageContract(IsWrapped = true,
                     WrapperName = "S2SMessagePostOperationResponse")]
    public class S2SMessagePostOperationSoapOut : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Name = "response")]
        public BMC.EBSComms.Contracts.Dto.SMS2EBS.ResponseType_13_0 Response { get; set; }

        public string GetRawMessage()
        {
            ModuleProc PROC = new ModuleProc("S2SMessagePostOperationSoapOut", "GetRawMessage");
            string result = default(string);

            try
            {
                if (this.Response != null)
                {
                    result = this.Response.response;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    [MessageContract(IsWrapped = true,
                     WrapperName = "S2SMessagePostOperation", 
                     WrapperNamespace = NamesHelper.NS_SMS2EBS_13_1)]
    public class S2SMessagePostOperationRequest : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Name = "request", 
                           Namespace = NamesHelper.NS_SMS2EBS_13_1)]
        public BMC.EBSComms.Contracts.Dto.SMS2EBS.RequestType_13_1 Request { get; set; }

        public string GetRawMessage()
        {
            ModuleProc PROC = new ModuleProc("S2SMessagePostOperationRequest", "GetRawMessage");
            string result = default(string);

            try
            {
                if (this.Request != null)
                {
                    result = this.Request.Request;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    [MessageContract(IsWrapped = true,
                     WrapperName = "S2SMessagePostOperationResponse",
                     WrapperNamespace = NamesHelper.NS_SMS2EBS_13_1)]
    public class S2SMessagePostOperationResponse : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Name = "S2SMessagePostOperationResult", 
                           Namespace = NamesHelper.NS_SMS2EBS_13_1)]
        public BMC.EBSComms.Contracts.Dto.SMS2EBS.ResponseType_13_1 Response { get; set; }

        public string GetRawMessage()
        {
            ModuleProc PROC = new ModuleProc("S2SMessagePostOperationResponse", "GetRawMessage");
            string result = default(string);

            try
            {
                if (this.Response != null)
                {
                    result = this.Response.Response;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
