﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets.GVA
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_GVA_TED_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_GVA_TED_Response) }
        )]
    internal class FFTgtHandler_GVA_TED_Request_G2H_GMU
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_GVA_TED_Response) }
        )]
    internal class FFTgtHandler_GVA_TED_Response_H2G_GMU
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_GVA_TED_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_GVA_TED_Response) }
        )]
    internal class FFTgtHandler_GVA_TED_Request_G2H_SIM
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_GVA_TED_Response) }
        )]
    internal class FFTgtHandler_GVA_TED_Response_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            FFTgt_H2G_GVA_TED_Response tgtSrc = target as FFTgt_H2G_GVA_TED_Response;
            ModuleHelper.Current.TicketGlobals.UpdateExpiryDate(context.SourceMessage.IpAddress, tgtSrc.ExipreDays, tgtSrc.Date);
            return true;
        }
    }
}
