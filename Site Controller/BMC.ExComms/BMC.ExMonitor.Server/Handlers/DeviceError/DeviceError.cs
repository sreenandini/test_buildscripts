using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.DeviceError
{
    [MonitorHandlerMapping((int)FaultSource.DeviceFaultEvent, typeof(FaultType_DeviceFaultEvent))]
    internal class MonitorHandler_DeviceError
        : GeneralEventsBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }
}
