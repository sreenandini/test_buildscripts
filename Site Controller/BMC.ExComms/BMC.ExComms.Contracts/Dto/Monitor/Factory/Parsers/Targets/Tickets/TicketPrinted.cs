using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_Ticket_Printed_Request),
                typeof(MonTgt_G2H_Ticket_Printed_Request),
                (int)FaultSource.TicketEvent,
                (int)FaultType_TicketEvent.TicketPrinted)]
    internal class MonTgtParser_Ticket_Printed_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_Ticket_Printed_Request tgtSrc = request as FFTgt_G2H_Ticket_Printed_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_Ticket_Printed_Request tgtDest = new MonTgt_G2H_Ticket_Printed_Request()
                {
                    BarCode = tgtSrc.BarCode,
                    Amount = tgtSrc.Amount,
                    Type = tgtSrc.Type,
                    SequenceNo = tgtSrc.SequenceNo,
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
               typeof(FFTgt_H2G_Ticket_Printed_Response),
               typeof(MonTgt_H2G_Ticket_Printed_Response),
               (int)FaultSource.TicketEvent,
               (int)FaultType_TicketEvent.TicketPrintedResponse,
               FF_AppId_H2G_PollCodes.FreeformNoResponse,
               FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_Ticket_Printed_Response_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Ticket_Printed_Response tgtSrc = request as MonTgt_H2G_Ticket_Printed_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_TicketInfo tgtDest = new FFTgt_B2B_TicketInfo()
                {
                    SubTargetData = new FFTgt_H2G_Ticket_Printed_Response()
                    {
                        Status = tgtSrc.Status,
                    }
                };
                return tgtDest;
            }
            return null;
        }
    }

    //[MonTgtParserMappingG2H(FF_FlowDirection.G2H,
    //                typeof(FFTgt_G2H_TicketPrint_ResultStatus),
    //                (int)FaultSource.TicketEvent,
    //                (int)FaultType_TicketEvent.TicketPrintStatus,
    //                new int[]
    //                {
    //                    (int)FF_AppId_SessionIds.Ticket,
    //                    (int)FF_AppId_TargetIds.Tickets,
    //                    (int)FF_AppId_TicketMessageTypes.TicketPrintStatusResult
    //                })]
    //internal class MonTgtParser_TicketPrint_ResultStatus_G2H
    //    : MonTgtParser_G2H
    //{
    //    protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
    //    {
    //        FFTgt_G2H_TicketPrint_ResultStatus ffTgt = request as FFTgt_G2H_TicketPrint_ResultStatus;
    //        if (ffTgt == null) return null;

    //        MonTgt_G2H_TicketPrint_ResultStatus monTgt = new MonTgt_G2H_TicketPrint_ResultStatus()
    //        {
    //            GameTicketSequence = ffTgt.GameTicketSequence,
    //            PrintStatus = ffTgt.PrintStatus
    //        };

    //        return monTgt;
    //    }
    //}
}
