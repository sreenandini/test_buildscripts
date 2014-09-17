using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.ECash
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_H2G_EFT_SystemEnable) },
        Response = new Type[] { typeof(FFTgt_G2H_EFT_SystemEnable) }
        )]
    internal class FFTgtHandler_ECash_SystemEnable_H2G_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageH2GExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_G2H_EFT_SystemEnable) }
        )]
    internal class FFTgtHandler_ECash_SystemEnable_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_H2G_EFT_SystemEnable) },
        Response = new Type[] { typeof(FFTgt_G2H_EFT_SystemEnable) }
        )]
    internal class FFTgtHandler_ECash_SystemEnable_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            context.FreeformTargets.Add(ECashHelper.SystemEnableResponse(context.SourceMessage.IpAddress, FF_AppId_EFT_ResponseTypes.Ack));
            return true;
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_G2H_EFT_SystemEnable) }
        )]
    internal class FFTgtHandler_ECash_SystemEnable_G2H_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_SIM(context, target);
        }
    }
}
