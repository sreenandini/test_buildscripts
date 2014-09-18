using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_LP_InstantPeriodic
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_LP_InstantPeriodic()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_LP_InstantPeriodic
        : FFParser_Tgt_Generic_LP_InstantPeriodic
    {
        internal FFParser_Tgt_MC300_LP_InstantPeriodic()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_LP_InstantPeriodic_G2H
        : FFParser_Tgt_MC300_LP_InstantPeriodic
    {
        internal FFParser_Tgt_MC300_LP_InstantPeriodic_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_LP_InstantPeriodic tgt = new FFTgt_H2G_LP_InstantPeriodic();
            tgt.ConfiguredInterval = buffer[0];
            tgt.LowerOrderInterval = buffer[1];
            tgt.HigherOrderInterval = buffer[2];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_LP_InstantPeriodic tgt2 = tgt as FFTgt_H2G_LP_InstantPeriodic;
            buffer.SetBCDValue(tgt2.ConfiguredInterval, 1);
            buffer.SetBCDValue(tgt2.LowerOrderInterval, 1);
            buffer.SetBCDValue(tgt2.HigherOrderInterval, 1);
        }
    }

    internal class FFParser_Tgt_MC300_LP_InstantPeriodic_H2G
        : FFParser_Tgt_MC300_LP_ForcePeriodic
    {
        internal FFParser_Tgt_MC300_LP_InstantPeriodic_H2G()
            : base() { }

        //internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        //{

        //}

        //public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        //{

        //}
    }
}
