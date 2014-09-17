using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Server.Executor
{
    internal interface IFreeformExecutor
    {      
        bool ProcessMessage(FFMsg_G2H G2H_Msg);
        bool ProcessMessage(FFMsg_H2G H2G_Msg);
    }

    internal interface IFreeformExecutor_Priority : IFreeformExecutor
    {
        
    }

    internal interface IFreeformExecutor_GIM : IFreeformExecutor
    {

    }

    internal interface IFreeformExecutor_NonPriority : IFreeformExecutor
    {

    }
}
