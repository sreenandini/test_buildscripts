using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
     [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_Ticket_Redemption_Request),
                typeof(MonTgt_G2H_Ticket_Redemption_Request),
                (int)FaultSource.TicketEvent,
                (int)FaultType_TicketEvent.TicketRedemptionRequest)]
    internal class MonTgtParser_Ticket_Redemption_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_Ticket_Redemption_Request tgtSrc = request as FFTgt_G2H_Ticket_Redemption_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_Ticket_Redemption_Request tgtDest = new MonTgt_G2H_Ticket_Redemption_Request()
                {
                    Barcode = tgtSrc.Barcode,
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
               typeof(FFTgt_H2G_Ticket_Redemption_Response),
               typeof(MonTgt_H2G_Ticket_Redemption_Response),
               (int)FaultSource.TicketEvent,
               (int)FaultType_TicketEvent.TicketRedemptionResponse,
               FF_AppId_H2G_PollCodes.FreeformResponse,
               FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_Ticket_Redemption_Response_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Ticket_Redemption_Response tgtSrc = request as MonTgt_H2G_Ticket_Redemption_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_TicketInfo tgtDest = new FFTgt_B2B_TicketInfo()
                {
                    SubTargetData = new FFTgt_H2G_Ticket_Redemption_Response()
                    {
                        Barcode = tgtSrc.Barcode,
                        Amount = tgtSrc.Amount,
                        Type = tgtSrc.Type
                    }
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_Ticket_Redemption_Close),
                typeof(MonTgt_G2H_Ticket_Redemption_Close),
                (int)FaultSource.TicketEvent,
                (int)FaultType_TicketEvent.TicketRedemptionComplete)]
    internal class MonTgtParser_Ticket_Redemption_Close_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_Ticket_Redemption_Close tgtSrc = request as FFTgt_G2H_Ticket_Redemption_Close;
            if (tgtSrc != null)
            {
                MonTgt_G2H_Ticket_Redemption_Close tgtDest = new MonTgt_G2H_Ticket_Redemption_Close()
                {
                    Barcode = tgtSrc.Barcode,
                    Amount = tgtSrc.Amount,
                    Type = tgtSrc.Type,
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
               typeof(FFTgt_H2G_Ticket_Redemption_Close),
               typeof(MonTgt_H2G_Ticket_Redemption_Close),
               (int)FaultSource.TicketEvent,
               (int)FaultType_TicketEvent.TicketRedemptionCompleteResponse,
               FF_AppId_H2G_PollCodes.FreeformNoResponse,
               FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_Ticket_Redemption_Close_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Ticket_Redemption_Close tgtSrc = request as MonTgt_H2G_Ticket_Redemption_Close;
            if (tgtSrc != null)
            {
                FFTgt_B2B_TicketInfo tgtDest = new FFTgt_B2B_TicketInfo()
                {
                    SubTargetData = new FFTgt_H2G_Ticket_Redemption_Close()
                    {
                        Status = tgtSrc.Status,
                    }
                };
                return tgtDest;
            }
            return null;
        }
    }
}
