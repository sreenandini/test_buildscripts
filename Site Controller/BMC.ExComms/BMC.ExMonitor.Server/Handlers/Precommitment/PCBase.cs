using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.Precommitment
{
    internal class MonitorHandler_PC_Base : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            return false;
        }
    }
}
