using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GIM_AuxNetworkEnableDisable : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GIM_AuxNetworkEnableDisable()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GIM_AuxNetworkEnableDisable tgt = new FFTgt_H2G_GIM_AuxNetworkEnableDisable();
            tgt.EnableDisable = buffer[0].ConvertToBoolean();
            tgt.Display = FreeformHelper.GetASCIIStringValue(buffer, 1, 0);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GIM_AuxNetworkEnableDisable : FFParser_Tgt_Generic_GIM_AuxNetworkEnableDisable
    {
        internal FFParser_Tgt_MC300_GIM_AuxNetworkEnableDisable()
            : base() { }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GIM_AuxNetworkEnableDisable tgt2 = tgt as FFTgt_H2G_GIM_AuxNetworkEnableDisable;
            buffer.Add(tgt2.EnableDisable.ConvertToByte());
            buffer.AddRange(tgt2.Display.GetASCIIBytesValue(50));
        }
    }
}
