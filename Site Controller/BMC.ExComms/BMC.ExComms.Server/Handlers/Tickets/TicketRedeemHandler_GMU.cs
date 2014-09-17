using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Tickets;

namespace BMC.ExComms.Server.Handlers.Targets.Redemption
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Redemption_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_Ticket_Redemption_Response) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Request_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return base.OnProcessMessageG2HExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_Ticket_Redemption_Response) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Response_H2G_GMU
        : FFTgtHandler
    {
        
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Redemption_Close) },
        Response = new Type[] { typeof(FFTgt_H2G_Ticket_Redemption_Close) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Close_G2H_GMU
        : FFTgtHandler
    {
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_Ticket_Redemption_Close) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Close_H2G_GMU
        : FFTgtHandler
    {
    }
}
