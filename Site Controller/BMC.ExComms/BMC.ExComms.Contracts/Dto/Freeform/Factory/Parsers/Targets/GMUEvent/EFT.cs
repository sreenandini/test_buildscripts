using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GMUEvent_EFT : FFTgtParser
    {
        internal FFParser_Tgt_MC300_GMUEvent_EFT()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent_EFTData tgt = new FFTgt_G2H_GMUEvent_EFTData();
            tgt.TransactionID = buffer[0];
            tgt.ErrorCode = buffer[1];
            return tgt;
        }
    }
}
