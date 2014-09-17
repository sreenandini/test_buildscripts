using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.Hopper
{
    [MonitorHandlerMapping((int)FaultSource.HopperEvent, typeof(FaultType_HopperEvent))]
    internal class MonitorHandler_Hopper
        : GeneralEventsBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
