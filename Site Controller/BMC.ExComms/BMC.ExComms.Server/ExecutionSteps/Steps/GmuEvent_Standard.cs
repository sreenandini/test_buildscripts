using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal abstract class ExecStep_GMUEvent_Request_G2H
        : ExecutionStep
    {
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_NoStep) 
                    },
                    PostTypeG2H = ExecutionStepPostTypes.ProcessInExternalChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                    AllowedMessages = new Type[] { typeof(FFTgt_G2H_GMUEvent_StdData) })]
    internal class ExecStep_GMUEvent_Request_G2H_GMU
        : ExecStep_GMUEvent_Request_G2H
    {
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_NoStep) 
                            },
                            PostTypeG2H = ExecutionStepPostTypes.ProcessInExternalChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                            AllowedMessages = new Type[] { typeof(FFTgt_G2H_GMUEvent_StdData) })]
    internal class ExecStep_GMUEvent_Request_G2H_SIM
        : ExecStep_GMUEvent_Request_G2H
    {
        internal override ExecutionStep ExecuteInternal(ILogMethod method, IFreeformEntity_Msg request)
        {
            return base.ExecuteInternal(method, request);
        }
    }
}
