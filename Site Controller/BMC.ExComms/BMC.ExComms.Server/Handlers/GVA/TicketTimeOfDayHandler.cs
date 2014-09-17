using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets.GVA
{
    [FFTgtHandler_Internal_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_GVA_TimeOfDay_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_GVA_TimeOfDay_Response) }
        )]
    internal class FFTgtHandler_GVA_TimeOfDay_Request_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HInternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction()
            {
                ActionData = new FFTgt_H2G_GVA_TimeOfDay_Response()
                {
                    TimeOfDay = DateTime.Now.TimeOfDay,
                }
            };
            context.FreeformTargets.Add(tgtDest);
            return true;
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_GVA_TimeOfDay_Response) }
        )]
    internal class FFTgtHandler_GVA_TimeOfDay_Response_H2G_GMU
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_GVA_TimeOfDay_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_GVA_TimeOfDay_Response) }
        )]
    internal class FFTgtHandler_GVA_TimeOfDay_Request_G2H_SIM
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_GVA_TimeOfDay_Response) }
        )]
    internal class FFTgtHandler_GVA_TimeOfDay_Response_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            FFTgt_H2G_GVA_TimeOfDay_Response tgtSrc = target as FFTgt_H2G_GVA_TimeOfDay_Response;
            ModuleHelper.Current.TicketGlobals.UpdateTimeOfDay(context.SourceMessage.IpAddress, tgtSrc.TimeOfDay);
            return true;
        }
    }
}
