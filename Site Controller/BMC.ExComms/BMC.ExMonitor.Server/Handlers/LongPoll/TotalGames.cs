using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers.LongPoll
{
    [MonitorHandlerMapping((int)FaultSource.LongPoll, (int)FaultType_LongPollCode.LPC_Receive_51)]
    internal class MonitorHandler_LPC_TotalGames
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
