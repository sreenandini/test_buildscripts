using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GVA_EftMaxWithdraw_G2H : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EftMaxWithdraw_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EFT_MaxWithdraw_Request tgt = new FFTgt_G2H_GVA_EFT_MaxWithdraw_Request();
            return tgt;
        }
    }
    internal class FFParser_Tgt_MC300_GVA_EftMaxWithdrawResp_H2G
          : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EftMaxWithdrawResp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_EFT_MaxWithdraw_Response tgt = new FFTgt_H2G_GVA_EFT_MaxWithdraw_Response();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_EFT_MaxWithdraw_Response tgt2 = tgt as FFTgt_H2G_GVA_EFT_MaxWithdraw_Response;
            buffer.AddRange(tgt2.MaxElectronicWithdrawalAmount.GetBCDToBytes(4));
        }
    }
}
