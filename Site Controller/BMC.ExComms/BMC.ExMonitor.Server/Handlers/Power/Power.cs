using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.DataLayer.MSSQL;
using Audit.Transport;
using Audit.BusinessClasses;
using BMC.Common.ExceptionManagement;

namespace BMC.ExMonitor.Server.Handlers.Power
{
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.GamePowerUp)]
    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.PowerReset)]
    internal class MonitorHandler_PowerUpandReset
        : MonitorHandler_PowerHandler_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }
    }

    [MonitorHandlerMapping((int)FaultSource.NonPriorityEvent, (int)FaultType_NonPriorityEvent.GamePowerDown)]
    internal class MonitorHandler_PowerDown
        : MonitorHandler_PowerHandler_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return base.OnExecuteInternal(context, target);
        }

        protected override bool OnPowerUp(int installationNo)
        {
            return true;
        }
    }
}
