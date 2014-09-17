using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.WcfHelper.Contracts;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.Contracts.Messages.EBS2SMS;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        private const string ERR_INVALID_MSG = "BMC100";
        private const string ERR_EXCEPTION = "BMC101";

        private s2sMessage CreateS2SMessage(ref long messageId)
        {
            // prepare the target
            s2sMessage target = new s2sMessage();
            string FromSystem = BMC.CoreLib.Extensions.GetAppSettingValue("FromSystem", "BMC");
            string ToSystem = BMC.CoreLib.Extensions.GetAppSettingValue("ToSystem", "EBS");
            s2sHeader header = new s2sHeader()
            {
                
                fromSystem = FromSystem,
                toSystem = ToSystem,
                messageId = ++messageId,
            };
            s2sBody body = new s2sBody();
            target.p_header = header;
            target.p_body = body;
            target.Items = new object[] {
                    header, body
                };
            return target;
        }

        private s2sMessage CreateS2SErrorMessage(ref long messageId, string errorCode, string message)
        {
            // prepare the target
            s2sMessage target = new s2sMessage();
            string FromSystem = BMC.CoreLib.Extensions.GetAppSettingValue("FromSystem", "BMC");
            string ToSystem = BMC.CoreLib.Extensions.GetAppSettingValue("ToSystem", "EBS");
            s2sAck ack = new s2sAck()
            {
                fromSystem = FromSystem,
                toSystem = ToSystem,
                messageId = ++messageId,
                errorCode = errorCode,
                errorText = message,
            };
            target.p_ack = ack;
            target.Items = new object[] {
                    ack
                };

            return target;
        }

        private object CreateS2SErrorMessageAndReturn(object result, ref long messageId,
            string errorCode, string message, bool updateMessageId)
        {
            if (result.GetType() == typeof(S2SMessagePostOperationSoapOut))
                return CreateS2SErrorMessageAndReturn(result as S2SMessagePostOperationSoapOut, ref messageId, errorCode, message, updateMessageId);
            else 
                return CreateS2SErrorMessageAndReturn(result as S2SEndPoint_processS2SMessageResponse, ref messageId, errorCode, message, updateMessageId);
        }

        private S2SMessagePostOperationSoapOut CreateS2SErrorMessageAndReturn(S2SMessagePostOperationSoapOut result, ref long messageId,
            string errorCode, string message, bool updateMessageId)
        {
            var msg = this.CreateS2SErrorMessage(ref messageId, errorCode, message);
            if (updateMessageId) _di.UpdateSettingValue(LASTMSGID_RECV, messageId.ToString());
            return this.FillS2SMessageToResponse(result, msg);
        }

        private S2SEndPoint_processS2SMessageResponse CreateS2SErrorMessageAndReturn(S2SEndPoint_processS2SMessageResponse result, ref long messageId,
            string errorCode, string message, bool updateMessageId)
        {
            var msg = this.CreateS2SErrorMessage(ref messageId, errorCode, message);
            if (updateMessageId) _di.UpdateSettingValue(LASTMSGID_RECV, messageId.ToString());
            return this.FillS2SMessageToResponse(result, msg);
        }

        private string GetS2SMessageResponse(s2sMessage target)
        {
            // update the timestamp now and sent
            if (target.p_header != null)
                target.p_header.dateTimeSent = DateTime.Now;
            else if (target.p_ack != null)
                target.p_ack.dateTimeSent = DateTime.Now;

            string response = this.ConvertObjectToXml(target, false, false); ;
            Log.Info("[BMC->EBS] Response to be sent : " + response);
            return response;
        }

        private object FillS2SMessageToResponse(object result, s2sMessage target)
        {
            if (result.GetType() == typeof(S2SMessagePostOperationSoapOut))
                return FillS2SMessageToResponse(result as S2SMessagePostOperationSoapOut, target);
            else
                return FillS2SMessageToResponse(result as S2SEndPoint_processS2SMessageResponse, target);
        }

        private S2SMessagePostOperationSoapOut FillS2SMessageToResponse(S2SMessagePostOperationSoapOut result, s2sMessage target)
        {
            string response = this.GetS2SMessageResponse(target);
            result.Response = new Contracts.Dto.EBS2SMS.ResponseType()
            {
                response = response
            };
            return result;
        }

        private S2SEndPoint_processS2SMessageResponse FillS2SMessageToResponse(S2SEndPoint_processS2SMessageResponse result, s2sMessage target)
        {
            string response = this.GetS2SMessageResponse(target);
            result.Response = new Contracts.Dto.EBS2SMS.processS2SMessageResponse()
            {
                @return = response
            };
            return result;
        }
    }
}
