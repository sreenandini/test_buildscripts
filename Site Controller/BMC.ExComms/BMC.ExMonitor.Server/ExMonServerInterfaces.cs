using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExMonitor.Server
{
    internal interface IExMonServer4MonProcessorProxy
        : IDisposable
    {
        bool ProcessH2GMessage(MonMsg_H2G request);
    }
}
