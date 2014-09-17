using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Tickets;

namespace BMC.ExComms.Server.Handlers.Targets.Redemption
{
    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Redemption_Request) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Request_G2H_SIM
        : FFTgtHandler
    {
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_Ticket_Redemption_Response) },
        Response = new Type[] { typeof(FFTgt_G2H_Ticket_Redemption_Close) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Response_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            FFTgt_H2G_Ticket_Redemption_Response tgtSrc = target as FFTgt_H2G_Ticket_Redemption_Response;
            FF_AppId_TicketRedemption_Close_Status status = (tgtSrc.Amount > 0 ? FF_AppId_TicketRedemption_Close_Status.Success : FF_AppId_TicketRedemption_Close_Status.CouponRejectedbySystem);
            context.FreeformTargets.Add(TicketsHelper.RedeemTicketComplete(context.SourceMessage.IpAddress,
                tgtSrc.Barcode, tgtSrc.Amount, tgtSrc.Type, status));
            return true;
        }
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Redemption_Close) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Close_G2H_SIM
        : FFTgtHandler
    {
    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_Ticket_Redemption_Close) }
        )]
    internal class FFTgtHandler_Ticket_Redemption_Close_H2G_SIM
        : FFTgtHandler
    {
    }
}
