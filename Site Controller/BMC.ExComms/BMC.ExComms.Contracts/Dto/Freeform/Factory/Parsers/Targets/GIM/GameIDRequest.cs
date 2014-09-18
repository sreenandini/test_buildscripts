using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GIM_GameIDRequest : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GIM_GameIDRequest()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GIM_GameIDRequest tgt = new FFTgt_H2G_GIM_GameIDRequest();
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GIM_GameIDRequest : FFParser_Tgt_Generic_GIM_GameIDRequest
    {
        internal FFParser_Tgt_MC300_GIM_GameIDRequest()
            : base() { }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GIM_GameIDRequest tgt2 = tgt as FFTgt_H2G_GIM_GameIDRequest;
        }
    }
}
