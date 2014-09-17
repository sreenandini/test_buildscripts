using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.GMUVarAction
{
    internal class MonitorHandler_GVA_Base : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            return false;
        }
    }
}
