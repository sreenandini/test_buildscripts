using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;

namespace BMC.ExMonitor.Server.Handlers.Error
{
    [MonitorHandlerMapping((int)FaultSource.ErrorEvent, typeof(FaultType_ErrorEvent))]
    internal class MonitorHandler_Error
        : GeneralEventsBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
