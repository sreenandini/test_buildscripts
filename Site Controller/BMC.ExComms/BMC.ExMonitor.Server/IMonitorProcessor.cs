using BMC.ExComms.Contracts.DTO.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server
{
    public interface IMonitorProcessor : IDisposable
    {
        bool ProcessG2HMessage(MonMsg_G2H request);
        bool ProcessH2GMessage(MonMsg_H2G response);
    }
}
