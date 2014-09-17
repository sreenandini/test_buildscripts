using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_TicketSeqNoUpdate),
                    (int)FaultSource.TicketEvent,
                    (int)FaultType_TicketEvent.GameTicketSequenceNumberUpdate,
                    new int[]
                    {
                        (int)FF_AppId_SessionIds.Ticket,
                        (int)FF_AppId_TargetIds.Tickets,
                        (int)FF_AppId_TicketMessageTypes.TicketSequenceNumberUpdate
                    })]
    internal class MonTgtParser_TicketSeqNoUpdate_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_TicketSeqNoUpdate ffTgt = request as FFTgt_G2H_TicketSeqNoUpdate;
            if (ffTgt == null) return null;

            MonTgt_G2H_TicketSeqNoUpdate monTgt = new MonTgt_G2H_TicketSeqNoUpdate()
            {
                Barcode = ffTgt.Barcode,
                Error = ffTgt.Error,
                GameTicketSequence = ffTgt.GameTicketSequence
            };

            return monTgt;
        }
    }
}
