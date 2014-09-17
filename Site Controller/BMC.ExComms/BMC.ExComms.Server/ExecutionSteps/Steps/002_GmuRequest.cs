using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal abstract class ExecStep_GIM_Request_G2H
        : ExecutionStep
    {
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_GIM_Response_H2G_GMU) 
                    },
                    AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                    AllowedMessages = new Type[] 
                    { 
                        typeof(FFTgt_G2H_GIM_GameIDInfo) 
                    },
                    AllowedReplyMessages = new Type[] 
                    { 
                        typeof(FFTgt_H2G_GIM_GameIDInfo) 
                    })]
    internal class ExecStep_GIM_Request_G2H_GMU
        : ExecStep_GIM_Request_G2H
    {
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_GIM_Response_H2G_SIM) 
                    },
                    AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                    AllowedMessages = new Type[] 
                    { 
                        typeof(FFTgt_G2H_GIM_GameIDInfo) 
                    })]
    internal class ExecStep_GIM_Request_G2H_SIM
        : ExecStep_GIM_Request_G2H
    {
    }
}
