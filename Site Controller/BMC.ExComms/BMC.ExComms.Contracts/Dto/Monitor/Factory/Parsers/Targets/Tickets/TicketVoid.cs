using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
               typeof(FFTgt_G2H_Ticket_Void),
               (int)FaultSource.TicketEvent,
               (int)FaultType_TicketEvent.TicketVoid)]
    internal class MonTgtParser_Ticket_Void_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_Ticket_Void tgtSrc = request as FFTgt_G2H_Ticket_Void;
            if (tgtSrc != null)
            {
                MonTgt_G2H_Ticket_Void tgtDest = new MonTgt_G2H_Ticket_Void()
                {
                    Barcode = tgtSrc.Barcode,
                    Error = tgtSrc.Error,
                };
                return tgtDest;
            }
            return null;
        }
    }
}
