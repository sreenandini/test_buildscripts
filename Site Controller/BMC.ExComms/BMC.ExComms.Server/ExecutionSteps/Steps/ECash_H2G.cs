using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.ExecutionSteps.Steps
{
    internal abstract class ExecStep_ECash_Response_H2G
        : ExecutionStep
    {
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_NoStep) 
                    },
                    PostTypeH2G = ExecutionStepPostTypes.ProcessInExternalChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                    AllowedMessages = new Type[] 
                    { 
                        typeof(FFTgt_H2G_EFT_BalanceResponse) 
                    })]
    internal class ExecStep_ECash_Response_H2G_GMU
        : ExecStep_ECash_Response_H2G
    {
        internal override ExecutionStep ExecuteInternal(ILogMethod method, IFreeformEntity_Msg request)
        {
            return base.ExecuteInternal(method, request);
        }
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_NoStep) 
                            },
                            PostTypeH2G = ExecutionStepPostTypes.ProcessInExternalChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                            AllowedMessages = new Type[] 
                            {
                                typeof(FFTgt_H2G_EFT_BalanceResponse) 
                            })]
    internal class ExecStep_ECash_Response_H2G_SIM
        : ExecStep_ECash_Response_H2G
    {
        internal override ExecutionStep ExecuteInternal(ILogMethod method, IFreeformEntity_Msg request)
        {
            return base.ExecuteInternal(method, request);
        }
    }
}
