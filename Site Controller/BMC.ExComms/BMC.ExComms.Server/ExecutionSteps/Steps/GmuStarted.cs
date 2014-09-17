using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal abstract class ExecStep_GmuStarted
        : ExecutionStep
    {
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_GIM_Request_G2H_GMU),
                        typeof(ExecStep_KeyExchange_Start_G2H_GMU) 
                    },
                    PostTypeG2H = ExecutionStepPostTypes.ProcessInCurrentChannel,
                    PostTypeH2G = ExecutionStepPostTypes.ProcessInCurrentChannel)]
    internal class ExecStep_GmuStarted_GMU
        : ExecStep_GmuStarted
    {
        public override bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request)
        {
            return true;
        }

        protected override void PrepareMessageToProcessInCurrentChannel(IFreeformEntity_Msg request, ref IFreeformEntity_Msg response)
        {
            if (request.EntityPrimaryTarget is FFTgt_H2G_GIM_GameIDInfo)
            {
                //response = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                //          new FFCreateEntityRequest_G2H_Secured()
                //          {
                //              Command = FF_AppId_G2H_Commands.ResponseRequest,
                //              MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                //              SessionID = FF_AppId_SessionIds.Security,
                //              TransactionID = 0xB1,
                //              IPAddress = request.IpAddress,
                //          });
                //FFTgt_B2B_Security tgt = new FFTgt_B2B_Security_Secured()
                //{
                //    SecurityData = new FFTgt_B2B_Security_KeyExchange_Request(),
                //};
                //response.AddTarget(tgt);
                // initiate a key exchange (ticket)
                //var msgTicket = FreeformEncryptionHelper.InitKeyExchangeStartG2H(request.IpAddress, FF_AppId_SessionIds.Security, this.NewTransactionId);
                //var msgEFT = FreeformEncryptionHelper.InitKeyExchangeStartG2H(request.IpAddress, FF_AppId_SessionIds.ECash, this.NewTransactionId);
                //this.ExecuteCurrentRequests(msgTicket, msgEFT);
            }

            response = null;
        }
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_GIM_Request_G2H_SIM),
                                typeof(ExecStep_KeyExchange_Start_G2H_SIM) 
                            },
                            PostTypeG2H = ExecutionStepPostTypes.ProcessCustom,
                            PostTypeH2G = ExecutionStepPostTypes.ProcessCustom)]
    internal class ExecStep_GmuStarted_SIM
        : ExecStep_GmuStarted
    {
        public override bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request)
        {
            return request.EntityPrimaryTarget is FFTgt_H2G_GIM_GameIDInfo;
        }

        protected override bool OnProcessMessageCustom(CoreLib.ILogMethod method, IFreeformEntity_Msg request)
        {
            // initiate a key exchange (ticket)
            var msgTicket = this.Factory.SecurityTables.InitKeyExchangeStartG2H_SIM(request.IpAddress, FF_AppId_SessionIds.Security, this.NewTransactionId);
            var msgEFT = this.Factory.SecurityTables.InitKeyExchangeStartG2H_SIM(request.IpAddress, FF_AppId_SessionIds.ECash, this.NewTransactionId);
            this.ExecuteCurrentRequests(msgTicket, msgEFT);
            return true;
        }
    }
}
