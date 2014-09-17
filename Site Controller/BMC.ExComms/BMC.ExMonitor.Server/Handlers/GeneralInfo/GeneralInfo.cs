using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.GeneralInfo
{
    [MonitorHandlerMapping((int)FaultSource.GeneralEvent, typeof(FaultType_GeneralEvent))]
    internal class MonitorHandler_GeneralInfo
        : GeneralEventsBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.GeneralEvent2, typeof(FaultType_GeneralEvent))]
    internal class MonitorHandler_GeneralInfo2
        : GeneralEventsBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
