using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.ECash
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_G2H_EFT_BalanceRequest) },
        Response = new Type[] { typeof(FFTgt_H2G_EFT_BalanceResponse) }
        )]
    internal class FFTgtHandler_ECash_BalanceRequest_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_H2G_EFT_BalanceResponse) }
        )]
    internal class FFTgtHandler_ECash_BalanceResponse_H2G_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageH2GExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_G2H_EFT_BalanceRequest) },
        Response = new Type[] { typeof(FFTgt_H2G_EFT_BalanceResponse) }
        )]
    internal class FFTgtHandler_ECash_BalanceRequest_G2H_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_SIM(context, target);
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.ECash,
        Request = new Type[] { typeof(FFTgt_H2G_EFT_BalanceResponse) }
        )]
    internal class FFTgtHandler_ECash_BalanceResponse_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageH2GExternal_SIM(context, target);
        }
    }
}
