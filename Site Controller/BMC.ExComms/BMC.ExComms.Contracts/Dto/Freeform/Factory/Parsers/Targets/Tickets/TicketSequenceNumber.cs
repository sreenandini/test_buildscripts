using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_Ticket_SequenceNumber 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_SequenceNumber()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_SequenceNumber 
        : FFParser_Tgt_Generic_Ticket_SequenceNumber
    {
        internal FFParser_Tgt_MC300_Ticket_SequenceNumber()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_SequenceNumber_G2H 
        : FFParser_Tgt_MC300_Ticket_SequenceNumber
    {
        internal FFParser_Tgt_MC300_Ticket_SequenceNumber_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_TicketSeqNoUpdate tgt = new FFTgt_G2H_TicketSeqNoUpdate();
            tgt.Barcode = buffer.GetBCDValueString(0, 0, 9);
            tgt.Error = buffer[9].GetAppId<FF_GmuId_TicketSeqNoUpdate_Error, FF_AppId_TicketSeqNoUpdate_Error>();
            tgt.GameTicketSequence = buffer.GetBytesToBCDInt16(10, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_TicketSeqNoUpdate tgt2 = tgt as FFTgt_G2H_TicketSeqNoUpdate;
            buffer.SetBCDValue(tgt2.Barcode, 9);
            buffer.SetValue(tgt2.Error.GetGmuIdInt8());
            buffer.SetValue(tgt2.GameTicketSequence, 2);
        }
    }
}
