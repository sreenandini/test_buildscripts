using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Concurrent;
using BMC.ExComms.Contracts.Interfaces;

namespace BMC.ExComms.Server.Executors
{
    internal class FFExecutor_InMemory
        : FFExecutorBase
    {
        public FFExecutor_InMemory(IExecutorService executorService)
            : base(executorService) { }
    }
}
