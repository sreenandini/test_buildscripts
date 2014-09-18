using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_Phishing 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_Phishing()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_Phishing 
        : FFParser_Tgt_Generic_EFT_Phishing
    {
        internal FFParser_Tgt_MC300_EFT_Phishing()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_Phishing_G2H 
        : FFParser_Tgt_MC300_EFT_Phishing
    {
        internal FFParser_Tgt_MC300_EFT_Phishing_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_Phishing tgt = new FFTgt_G2H_EFT_Phishing();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.Type = buffer[5];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_Phishing tgt2 = tgt as FFTgt_G2H_EFT_Phishing;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.Add(tgt2.Type);
        }
    }
}
