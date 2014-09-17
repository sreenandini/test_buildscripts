using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal abstract class ExecStep_GIM_Response_H2G
        : ExecutionStep
    {
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_GmuStarted_GMU) 
                    },
                    PostTypeH2G = ExecutionStepPostTypes.ProcessInExternalChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                    AllowedMessages = new Type[] { typeof(FFTgt_H2G_GIM_GameIDInfo) })]
    internal class ExecStep_GIM_Response_H2G_GMU
        : ExecStep_GIM_Response_H2G
    {
        internal override ExecutionStep ExecuteInternal(CoreLib.ILogMethod method, IFreeformEntity_Msg request)
        {
            return base.ExecuteInternal(method, request);
        }
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_GmuStarted_SIM) 
                            },
                            PostTypeH2G = ExecutionStepPostTypes.ProcessInCurrentChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                            AllowedMessages = new Type[] { typeof(FFTgt_H2G_GIM_GameIDInfo) })]
    internal class ExecStep_GIM_Response_H2G_SIM
        : ExecStep_GIM_Response_H2G
    {
        internal override ExecutionStep ExecuteInternal(CoreLib.ILogMethod method, IFreeformEntity_Msg request)
        {
            this.ExecuteNextStep(request);
            return this;
        }
    }
}
