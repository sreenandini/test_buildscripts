using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;
using BMC.ExComms.Server.Handlers.Targets.GVA;

namespace BMC.ExComms.Server.Handlers.Client
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.GameMessage,
        Request = new Type[] 
        { 
            typeof(FFTgt_G2H_GameMessage_SASCommand),
            typeof(FFTgt_G2H_GM_SAS_TotalGames),
            typeof(FFTgt_G2H_GameMessage_MessageNotSent),
            typeof(FFTgt_G2H_GameMessage_MessageResponseTimeout),
        }
        )]
    internal sealed class FFTgtHandler_GameMessage_MessageResponse_G2G_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_GMU(context, target);
        }
    }
}
