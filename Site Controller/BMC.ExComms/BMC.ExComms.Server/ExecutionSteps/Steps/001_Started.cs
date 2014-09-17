using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal abstract class ExecStep_HostStarted
        : ExecutionStep
    {
    }

    [ExecutionStep(IsStart = true,
                   NextSteps = new Type[] 
                   { 
                       typeof(ExecStep_GIM_Request_G2H_GMU) 
                   })]
    internal class ExecStep_HostStarted_GMU
        : ExecStep_HostStarted
    {
    }

    [ExecutionStepSimuator(IsStart = true,
                   NextSteps = new Type[] 
                   { 
                       typeof(ExecStep_GIM_Request_G2H_SIM) 
                   })]
    internal class ExecStep_HostStarted_SIM
        : ExecStep_HostStarted
    {
    }
}
