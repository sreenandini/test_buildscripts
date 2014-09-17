using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers.LongPoll
{
    [MonitorHandlerMapping((int)FaultSource.LongPoll, (int)FaultType_LongPollCode.LPC_EnableMachine)]
    internal class MonitorHandler_LPC_EnableMachine
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.LongPoll, (int)FaultType_LongPollCode.LPC_DisableMachine)]
    internal class MonitorHandler_LPC_DisableMachine
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
