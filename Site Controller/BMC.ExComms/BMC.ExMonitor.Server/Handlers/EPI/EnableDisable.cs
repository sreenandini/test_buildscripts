using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers.EPI
{
    [MonitorHandlerMapping((int)FaultSource.ECash, (int)FaultType_ECash.EnableEFT)]
    internal class MonitorHandler_EPI_SystemEnable_G2H :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return true;
        }
    }

    [MonitorHandlerMapping((int)FaultSource.ECash, (int)FaultType_ECash.DisableEFT)]
    internal class MonitorHandler_EPI_SystemDisable_G2H :
        MonitorHandler_EPI_Base
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            return true;
        }
    }
}
