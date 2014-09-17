using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.GMUEvent
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.A1,
        Request = new Type[] { typeof(FFTgt_G2H_GMUEvent_StdData) }
        )]
    internal class FFTgtHandler_GMUEvent_StdData_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.A1,
        Request = new Type[] { typeof(FFTgt_G2H_GMUEvent_StdData) }
        )]
    internal class FFTgtHandler_GMUEvent_StdData_G2H_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_SIM(context, target);
        }
    }
}
