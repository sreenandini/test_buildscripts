using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
 
    internal class FFParser_Tgt_MC300_GMUEvent_Ticket : FFTgtParser
    {
        internal FFParser_Tgt_MC300_GMUEvent_Ticket()
            : base() { }


        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent_TicketData tgt = new FFTgt_G2H_GMUEvent_TicketData();
            tgt.BarcodeNumber = FreeformHelper.GetBytesToBCDDouble(buffer, 0, 9);
            tgt.ErrorCode = FreeformHelper.GetBytesToNumberUInt8(buffer, 9, 1);
            return tgt;
        }
    }
}
