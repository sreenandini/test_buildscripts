using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.EBSComms.Contracts.Configuration;
using BMC.EBSComms.Contracts.Dto.EBS2SMS;

namespace BMC.EBSComms.Contracts.Messages
{
    public interface IBMCRawMessage
    {
        string GetRawMessage();
    }
}

namespace BMC.EBSComms.Contracts.Messages.EBS2SMS
{
    [MessageContract(IsWrapped = false)]
    public class S2SMessagePostOperationSoapIn : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Namespace = NamesHelper.NS_EBS2SMS_13_0,
                           Name = "s2sRequest")]
        public RequestType Request { get; set; }

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

    [MessageContract(IsWrapped = false)]
    public class S2SMessagePostOperationSoapOut : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Namespace = NamesHelper.NS_EBS2SMS_13_0,
                           Name = "s2sResponse")]
        public ResponseType Response { get; set; }

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

    [MessageContract(IsWrapped = false)]
    public class S2SEndPoint_processS2SMessage : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Namespace = NamesHelper.NS_EBS2SMS_13_1,
                           Name = "processS2SMessage")]
        public processS2SMessage Request { get; set; }

        public string GetRawMessage()
        {
            ModuleProc PROC = new ModuleProc("S2SMessagePostOperationSoapIn", "GetRawMessage");
            string result = default(string);

            try
            {
                if (this.Request != null)
                {
                    result = this.Request.s2sMessage;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }

    [MessageContract(IsWrapped = false)]
    public class S2SEndPoint_processS2SMessageResponse : WcfMessageContextBase, IBMCRawMessage
    {
        [MessageBodyMember(Namespace = NamesHelper.NS_EBS2SMS_13_1,
                           Name = "processS2SMessageResponse")]
        public processS2SMessageResponse Response { get; set; }

        public string GetRawMessage()
        {
            ModuleProc PROC = new ModuleProc("S2SMessagePostOperationSoapIn", "GetRawMessage");
            string result = default(string);

            try
            {
                if (this.Response != null)
                {
                    result = this.Response.@return;
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
