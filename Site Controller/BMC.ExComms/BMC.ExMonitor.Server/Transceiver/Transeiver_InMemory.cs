using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server.Transceiver
{
    internal class MonitorTransceiver_InMemory
        : ExMonitorTransceiverBase
    {
        public MonitorTransceiver_InMemory(IExecutorService executorService, ExMonitorServerImpl monitorServer) 
            : base(executorService, monitorServer)
        {
        }
    }
}
