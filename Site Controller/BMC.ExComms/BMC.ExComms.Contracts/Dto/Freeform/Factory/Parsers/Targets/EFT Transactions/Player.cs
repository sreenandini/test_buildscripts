using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_PlayerEnable
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_PlayerEnable()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_PlayerEnabl
        : FFParser_Tgt_Generic_EFT_PlayerEnable
    {
        internal FFParser_Tgt_MC300_EFT_PlayerEnabl()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_PlayerEnable_H2G 
        : FFParser_Tgt_MC300_EFT_PlayerEnabl
    {
        internal FFParser_Tgt_MC300_EFT_PlayerEnable_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_PlayerEnable tgt = new FFTgt_H2G_EFT_PlayerEnable();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 5, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToNumberUInt8(buffer, 8, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 9, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_PlayerEnable tgt2 = tgt as FFTgt_H2G_EFT_PlayerEnable;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }
}
