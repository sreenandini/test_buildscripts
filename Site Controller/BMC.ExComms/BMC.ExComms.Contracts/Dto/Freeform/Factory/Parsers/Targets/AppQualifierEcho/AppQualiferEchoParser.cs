using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_App_Echo : FFTgtParser
    {
        internal FFParser_Tgt_Generic_App_Echo()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_AppResponseEcho tgt = new FFTgt_B2B_AppResponseEcho();
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_App_Echo : FFParser_Tgt_Generic_App_Echo
    {
        internal FFParser_Tgt_MC300_App_Echo()
            : base() { }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            //  FFTgt_G2H_AppQualifier_Unknown tgt2 = tgt as FFTgt_G2H_AppQualifier_Unknown ;
        }
    }
}
