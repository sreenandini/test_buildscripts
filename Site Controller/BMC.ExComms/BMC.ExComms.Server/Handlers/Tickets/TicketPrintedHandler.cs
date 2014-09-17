using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Tickets
{
    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Printed_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_Ticket_Printed_Response) }
        )]
    internal class FFTgtHandler_Ticket_Printed_Request_G2H_GMU
        : FFTgtHandler
    {
        protected override bool OnProcessMessageG2HExternal_GMU(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            FFTgt_G2H_Ticket_Printed_Request tgtSrc = target as FFTgt_G2H_Ticket_Printed_Request;
            using (TicketIDInfo idInfo = new TicketIDInfo(tgtSrc.BarCode))
            {
                tgtSrc.SequenceNo = idInfo.SequenceNumber;
            }
            return base.OnProcessMessageG2HExternal_GMU(context, target);
        }
    }

    [FFTgtHandler_External_GMU(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_Ticket_Printed_Response) }
        )]
    internal class FFTgtHandler_Ticket_Printed_Response_H2G_GMU
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_G2H_Ticket_Printed_Request) },
        Response = new Type[] { typeof(FFTgt_H2G_Ticket_Printed_Response) }
        )]
    internal class FFTgtHandler_Ticket_Printed_Request_G2H_SIM
        : FFTgtHandler
    {

    }

    [FFTgtHandler_External_SIM(
        SessionId = (int)FF_AppId_SessionIds.Tickets,
        Request = new Type[] { typeof(FFTgt_H2G_Ticket_Printed_Response) }
        )]
    internal class FFTgtHandler_Ticket_Printed_Response_H2G_SIM
        : FFTgtHandler
    {
        protected override bool OnProcessMessageH2GExternal_SIM(FFTgtExecutionContext context, IFreeformEntity_MsgTgt target)
        {
            return true;
        }
    }
}
