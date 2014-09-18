using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{   

    internal class FFParser_Tgt_Generic_Roulette : FFTgtParser
    {
        internal FFParser_Tgt_Generic_Roulette()
            : base()
        {
        }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Roulette tgt = new FFTgt_G2H_Roulette();
            entity = tgt;
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }
}
