using System;
using System.IO;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.Contracts.Dto;
using BMC.EBSComms.Contracts.Messages;
using BMC.EBSComms.Contracts.Messages.EBS2SMS;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        private const string LASTMSGID_RECV = "EBSLastMessageId_Recv";

        public S2SMessagePostOperationSoapOut S2SMessagePostOperation(S2SMessagePostOperationSoapIn request)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "S2SMessagePostOperation");
            S2SMessagePostOperationSoapOut result = new S2SMessagePostOperationSoapOut();

            try
            {
                string rawMessage = string.Empty;

                if (request != null &&
                    request.Request != null &&
                    !request.Request.request.IsEmpty())
                {
                    rawMessage = request.Request.request;
                }

                result = this.ProcessMessage<S2SMessagePostOperationSoapOut>(result, rawMessage);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public S2SEndPoint_processS2SMessageResponse processS2SMessage(S2SEndPoint_processS2SMessage request)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "S2SMessagePostOperation");
            S2SEndPoint_processS2SMessageResponse result = new S2SEndPoint_processS2SMessageResponse();

            try
            {
                string rawMessage = string.Empty;

                if (request != null &&
                    request.Request != null &&
                    !request.Request.s2sMessage.IsEmpty())
                {
                    rawMessage = request.Request.s2sMessage;
                }

                result = this.ProcessMessage<S2SEndPoint_processS2SMessageResponse>(result, rawMessage);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private S ProcessMessage<S>(S result, string rawMessage)
            where S : IBMCRawMessage
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "S2SMessagePostOperation");
            long messageId = _di.GetSettingValue<long>(LASTMSGID_RECV, 0);
            string siteCode = string.Empty;

            try
            {
                s2sMessage msgs2s = null;

                Log.Info(PROC, "[EBS->BMC] Received raw message from EBS : " + rawMessage);
                if (!rawMessage.IsEmpty())
                {
                    msgs2s = BMC.CoreLib.Extensions.ReadXmlObject<s2sMessage>(rawMessage, string.Empty);
                }

                if (msgs2s == null ||
                    msgs2s.Items.Length != 2)
                {
                    Log.Warning("[EBS->BMC] Unable to convert or Invalid message received from EBS.");
                    result = (S)this.CreateS2SErrorMessageAndReturn(result, ref messageId, ERR_INVALID_MSG,
                        "Unable to convert or Invalid message received from EBS", true);
                    return result;
                }

                // prepare the target
                s2sMessage target = this.CreateS2SMessage(ref messageId);
                
                // process the message
                this.InvokeWork_FromEBS(target, msgs2s);
                siteCode = target.p_propertyId;

                // update the timestamp now and sent
                this.FillS2SMessageToResponse(result, target);

                // update the last message id
                _di.UpdateSettingValue(LASTMSGID_RECV, messageId.ToString());
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                result = (S)this.CreateS2SErrorMessageAndReturn(result, ref messageId, ERR_EXCEPTION,
                    ex.GetAllExceptions(), true);
            }
            finally
            {
                if (result != null)
                {
                    // log the incoming message                
                    _di.UpdateMessageHistory(_configStore.LogRawMessages,
                                             (int)EBSSystemNames.EBS, (int)EBSSystemNames.BMC,
                                             siteCode, DateTime.Now, 0, rawMessage, result.GetRawMessage());
                }
            }

            return result;
        }
    }
}
