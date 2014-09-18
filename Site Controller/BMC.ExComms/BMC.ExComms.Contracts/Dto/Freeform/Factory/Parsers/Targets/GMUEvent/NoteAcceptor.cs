using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GMUEvent_NoteAcceptor : FFTgtParser
    {
        internal FFParser_Tgt_MC300_GMUEvent_NoteAcceptor()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent_NoteAcceptorData tgt = new FFTgt_G2H_GMUEvent_NoteAcceptorData();
            tgt.EventData = FreeformHelper.GetASCIIStringValue(buffer, 0, 32);
            return tgt;
        }    
    }
}
