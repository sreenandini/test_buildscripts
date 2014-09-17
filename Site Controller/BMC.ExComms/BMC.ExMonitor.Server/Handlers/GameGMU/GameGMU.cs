using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.GameGMU
{
    [MonitorHandlerMapping((int)FaultSource.GameGmuRequest, typeof(FaultType_GameGmuRequest))]
    internal class MonitorHandler_GameGMU
        : GeneralEventsBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
