using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server.Handlers.Tickets
{
    internal class MonitorHandler_Ticket_Base 
        : MonitorHandlerBase
    {
        protected override bool ProcessG2HMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            return false;
        }

        protected override bool ProcessH2GMessageInternal(ExComms.Contracts.DTO.Monitor.MonMsg_H2G response)
        {
            return false;
        }
    }
}
