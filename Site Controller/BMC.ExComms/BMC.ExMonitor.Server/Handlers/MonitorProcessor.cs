using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExMonitor.Server.Handlers
{
    public interface IMonitorProcessor : IDisposable
    {
        bool ProcessG2HMessage(MonMsg_G2H request);
        bool ProcessH2GMessage(MonMsg_H2G request);
    }
}
