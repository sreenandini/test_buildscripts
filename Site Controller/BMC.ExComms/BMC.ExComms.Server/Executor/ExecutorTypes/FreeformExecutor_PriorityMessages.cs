using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.Hosting;

namespace BMC.ExComms.Server.Executor
{
    internal class FreeformExecutor_PriorityMessages :
        FreeformExecutorBase, IFreeformExecutor_Priority
    {
        public FreeformExecutor_PriorityMessages() 
            : base()
        {
        }
    }
}
