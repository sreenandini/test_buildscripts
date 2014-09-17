using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Server.ExecutionSteps
{
    [ExecutionStep(IsEnd = true)]
    [ExecutionStepSimuator(IsEnd = true)]
    internal class ExecStep_NoStep
        : ExecutionStep
    {
        public override bool CanExecute(ExecutionStepKeyValue pair, Contracts.DTO.Freeform.IFreeformEntity_Msg request)
        {
            return false;
        }
    }
}
