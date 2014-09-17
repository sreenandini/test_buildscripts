using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.Handlers.Targets.KeyExchange
{
    [FFTgtHandler_Internal_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Request) },
        Response = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) }
        )]
    [FFTgtHandler_Internal_GMU(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Request) },
        Response = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) }
        )]
    internal class FFTgtHandler_KeyExchange_Start_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HInternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            context.FreeformTargets.Add(this.MessageHandlerFactory.SecurityTables.InitKeyExchangePartialKeyH2G_GMU(context.SourceMessage));
            return true;
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) }
        )]
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) }
        )]
    internal class FFTgtHandler_KeyExchange_PartialKey_H2G_GMU
        : FFTgtHandler
    {
    }

    [FFTgtHandler_Internal_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) },
        Response = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) }
        )]
    [FFTgtHandler_Internal_GMU(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) },
        Response = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) }
        )]
    internal class FFTgtHandler_KeyExchange_End_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HInternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            context.FreeformTargets.Add(this.MessageHandlerFactory.SecurityTables.InitKeyExchangeStatusH2G_GMU(context.SourceMessage));
            return true;
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) }
        )]
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) }
        )]
    internal class FFTgtHandler_KeyExchange_Status_H2G_GMU
        : FFTgtHandler
    {
    }
}
