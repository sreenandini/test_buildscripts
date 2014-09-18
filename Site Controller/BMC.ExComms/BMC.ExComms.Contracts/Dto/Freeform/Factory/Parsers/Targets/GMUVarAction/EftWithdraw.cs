using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region EFT Withdraw Request
    internal class FFParser_Tgt_MC300_GVA_EftWithdraw_Req_G2H : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EftWithdraw_Req_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GVA_EFT_Withdraw_Request tgt = new FFTgt_G2H_GVA_EFT_Withdraw_Request();
            return tgt;
        }
    }
    #endregion 

    #region EFT Withdraw Response 
    internal class FFParser_Tgt_MC300_GVA_EFTWithdraw_Resp_H2G
           : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EFTWithdraw_Resp_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_GVA_EFT_Withdraw_Response tgt = new FFTgt_H2G_GVA_EFT_Withdraw_Response();
            tgt.WithdrawalAmount_option1 = FreeformHelper.GetBCDValue<Double>(buffer, 0, 4);
            tgt.WithdrawalAmount_option2 = FreeformHelper.GetBCDValue<Double>(buffer, 4, 4);
            tgt.WithdrawalAmount_option3 = FreeformHelper.GetBCDValue<Double>(buffer, 8, 4);
            tgt.WithdrawalAmount_option4 = FreeformHelper.GetBCDValue<Double>(buffer, 12, 4);

            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GVA_EFT_Withdraw_Response tgt2 = tgt as FFTgt_H2G_GVA_EFT_Withdraw_Response;
            buffer.AddRange(tgt2.WithdrawalAmount_option1.GetBCDToBytes(4));
            buffer.AddRange(tgt2.WithdrawalAmount_option2.GetBCDToBytes(4));
            buffer.AddRange(tgt2.WithdrawalAmount_option3.GetBCDToBytes(4));
            buffer.AddRange(tgt2.WithdrawalAmount_option4.GetBCDToBytes(4));
        }
    }
    #endregion

#region EFT Withdraw Status
    

    internal class FFParser_Tgt_MC300_GVA_EFTWithdraw_Status_H2G : FFParser_Tgt_MC300_GVA_EFT
    {
        internal FFParser_Tgt_MC300_GVA_EFTWithdraw_Status_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_Withdraw_Status tgt = new FFTgt_G2H_EFT_Withdraw_Status();
            tgt.Status = buffer[0].GetAppId<FF_GmuId_ResponseStatus_Types, FF_AppId_ResponseStatus_Types>();
            return tgt;
        }
    }
  
#endregion
}
