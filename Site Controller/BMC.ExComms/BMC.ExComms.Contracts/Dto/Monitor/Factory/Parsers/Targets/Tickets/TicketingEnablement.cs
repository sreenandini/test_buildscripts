using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.H2G,
                    typeof(MonTgt_H2G_TicketingEnablement_Request),
                    (int)FaultSource.TicketEvent,
                    (int)FaultType_TicketEvent.TicketEnablementRequest,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Ticket,
                        (int)FF_AppId_TargetIds.Tickets,
                        (int)FF_AppId_TicketMessageTypes.EnablementRequest
                    })]
    internal class MonTgtParser_TTicketingEnablement_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IFreeformEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_TicketingEnablement_Request monTgt = request as MonTgt_H2G_TicketingEnablement_Request;
            if (monTgt == null) return null;

            FFTgt_H2G_TicketingEnablement_Request ffTgt = new FFTgt_H2G_TicketingEnablement_Request()
            {
                Command = monTgt.Command
            };
            return ffTgt;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_TicketingEnablement),
                    (int)FaultSource.TicketEvent,
                    (int)FaultType_TicketEvent.TicketEnablementResponse,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Ticket,
                        (int)FF_AppId_TargetIds.Tickets,
                        (int)FF_AppId_TicketMessageTypes.EnablementResponse
                    })]
    internal class MonTgtParser_TicketingEnablement_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_TicketingEnablement ffTgt = request as FFTgt_G2H_TicketingEnablement;
            if (ffTgt == null) return null;

            MonTgt_G2H_TicketingEnablement monTgt = new MonTgt_G2H_TicketingEnablement()
            {
                Status = ffTgt.Status,
            };

            return monTgt;
        }
    }
}
