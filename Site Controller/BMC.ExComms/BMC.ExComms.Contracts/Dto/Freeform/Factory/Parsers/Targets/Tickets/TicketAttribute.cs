using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    internal class FFParser_Tgt_Generic_Ticket_Attribute
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Ticket_Attribute()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Attribute
        : FFParser_Tgt_Generic_Ticket_Attribute
    {
        internal FFParser_Tgt_MC300_Ticket_Attribute()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Ticket_Attribute_G2H
        : FFParser_Tgt_MC300_Ticket_Attribute
    {
        internal FFParser_Tgt_MC300_Ticket_Attribute_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_TicketAttribute tgt = new FFTgt_G2H_TicketAttribute();
            tgt.TicketPrintDateTime = buffer.GetBytesToBCDDateTime(0, 6);
            tgt.OfflineStatus = buffer[6].GetAppId<FF_GmuId_TicketOfflineStatus, FF_AppId_TicketOfflineStatus>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_TicketAttribute tgt2 = tgt as FFTgt_G2H_TicketAttribute;
            buffer.SetBCDValue(tgt2.TicketPrintDateTime, 6);
            buffer.SetValue(tgt2.OfflineStatus.GetGmuIdInt8());
        }
    }
}
