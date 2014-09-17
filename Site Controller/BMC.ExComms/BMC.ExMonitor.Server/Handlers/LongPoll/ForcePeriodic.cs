using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.LongPoll
{
    [MonitorHandlerMapping((int)FaultSource.LongPoll, (int)FaultType_LongPollCode.LPC_Send_ForcePeriodic)]
    internal class MonitorHandler_LPC_103_7
        : MonitorHandlerBase_H2G
    {
        public void ForcePeriodicMeter(MonMsg_H2G response)
        {
            this.ProcessH2GMessage(response);
        }
    }
}
