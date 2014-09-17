using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.Hosting;

namespace BMC.ExComms.Server.Executor
{
    internal class FreeformExecutor_NonPriorityMessages 
        : FreeformExecutorBase, IFreeformExecutor_NonPriority
    {
        public FreeformExecutor_NonPriorityMessages() 
            : base()
        {
        }
    }
}
