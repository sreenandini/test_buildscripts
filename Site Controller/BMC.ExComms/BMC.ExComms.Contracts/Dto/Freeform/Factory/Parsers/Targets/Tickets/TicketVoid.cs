using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_Ticket_Void 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_Void()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Void 
        : FFParser_Tgt_Generic_Ticket_Void
    {
        internal FFParser_Tgt_MC300_Ticket_Void()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Void_G2H 
        : FFParser_Tgt_MC300_Ticket_Void
    {
        internal FFParser_Tgt_MC300_Ticket_Void_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Ticket_Void tgt = new FFTgt_G2H_Ticket_Void();
            tgt.Barcode = buffer.GetBCDValueString(0, 0, 9);
            tgt.Error = buffer[9].GetAppId<FF_GmuId_TicketPrintStatus, FF_AppId_TicketPrintStatus>();         
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_Ticket_Void tgt2 = tgt as FFTgt_G2H_Ticket_Void;
            buffer.SetBCDValue(tgt2.Barcode, 9);
            buffer.SetValue(tgt2.Error.GetGmuIdInt8());
        }
    }
}
