using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_LP_ForcePeriodic
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_LP_ForcePeriodic()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_LP_ForcePeriodic
        : FFParser_Tgt_Generic_LP_ForcePeriodic
    {
        internal FFParser_Tgt_MC300_LP_ForcePeriodic()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_LP_ForcePeriodic_G2H
        : FFParser_Tgt_MC300_LP_ForcePeriodic
    {
        internal FFParser_Tgt_MC300_LP_ForcePeriodic_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_LP_ForcePeriodic tgt = new FFTgt_H2G_LP_ForcePeriodic();
            tgt.Data1 = buffer[0];
            tgt.Data2 = buffer[1];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_LP_ForcePeriodic tgt2 = tgt as FFTgt_H2G_LP_ForcePeriodic;
            buffer.SetBCDValue(tgt2.Data1, 1);
            buffer.SetBCDValue(tgt2.Data2, 1);
        }
    }

    internal class FFParser_Tgt_MC300_LP_ForcePeriodic_H2G
        : FFParser_Tgt_MC300_LP_ForcePeriodic
    {
        internal FFParser_Tgt_MC300_LP_ForcePeriodic_H2G()
            : base() { }

        //internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        //{
            
        //}

        //public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        //{
            
        //}
    }
}
