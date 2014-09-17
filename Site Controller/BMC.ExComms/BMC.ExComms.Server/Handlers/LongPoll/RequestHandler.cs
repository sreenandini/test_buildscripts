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
            typeof(FFTgt_H2G_GM_SAS_EnableDisable), 
            typeof(FFTgt_H2G_GM_SAS_CurrentCredits),
            typeof(FFTgt_H2G_GM_SAS_HandpayInfo),
            typeof(FFTgt_H2G_GM_SAS_GameMachineInfo),
            typeof(FFTgt_H2G_GM_SAS_TotalGames),
            typeof(FFTgt_H2G_GM_SAS_GetGameInfo),
            typeof(FFTgt_H2G_GM_SAS_GetExtendedGameInfo),
            typeof(FFTgt_H2G_GM_SAS_ClearHandpayLock)
        }
        )]
    internal sealed class FFTgtHandler_GameMessage_MessageRequest_H2G_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageH2GExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.GameMessage,
        Request = new Type[] 
        { 
            typeof(FFTgt_H2G_GM_SAS_EnableDisable), 
            typeof(FFTgt_H2G_GM_SAS_CurrentCredits),
            typeof(FFTgt_H2G_GM_SAS_HandpayInfo),
            typeof(FFTgt_H2G_GM_SAS_GameMachineInfo),
            typeof(FFTgt_H2G_GM_SAS_TotalGames),
            typeof(FFTgt_H2G_GM_SAS_GetGameInfo),
            typeof(FFTgt_H2G_GM_SAS_GetExtendedGameInfo),
            typeof(FFTgt_H2G_GM_SAS_ClearHandpayLock)
        }
        )]
    internal sealed class FFTgtHandler_GameMessage_MessageRequest_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageH2GExternal_SIM(context, target);
        }
    }
}
