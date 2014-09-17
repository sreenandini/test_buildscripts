using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExMonitor.Server
{
    public  class MonitorProcessor 
        : ExecutorServiceBase, IMonitorProcessor
    {
        public MonitorProcessor(IExecutorService executorService)
            : base(executorService) { }

        public bool ProcessG2HMessage(BMC.ExComms.Contracts.DTO.Monitor.MonMsg_G2H request)
        {
            throw new NotImplementedException();
        }

        public bool ProcessH2GMessage(BMC.ExComms.Contracts.DTO.Monitor.MonMsg_H2G response)
        {
            throw new NotImplementedException();
        }
    }
}
