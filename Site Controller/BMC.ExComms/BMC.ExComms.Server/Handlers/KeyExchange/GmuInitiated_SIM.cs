using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.Handlers.Targets.KeyExchange
{
    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Request) }
        )]
    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Request) }
        )]
    internal class FFTgtHandler_KeyExchange_Start_G2H_SIM
        : FFTgtHandler
    {
    }

    [FFTgtHandler_Internal_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) },
        Response = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) }
        )]
    [FFTgtHandler_Internal_SIM(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) },
        Response = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) }
        )]
    internal class FFTgtHandler_KeyExchange_PartialKey_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GInternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            context.FreeformTargets.Add(this.MessageHandlerFactory.SecurityTables.InitKeyExchangeEndG2H_SIM(context.SourceMessage));
            return true;
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) }
        )]
    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) }
        )]
    internal class FFTgtHandler_KeyExchange_End_G2H_SIM
        : FFTgtHandler
    {
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) }
        )]
    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Security,
        Request = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) }
        )]
    internal class FFTgtHandler_KeyExchange_Status_H2G_SIM
        : FFTgtHandler
    {
    }
}
