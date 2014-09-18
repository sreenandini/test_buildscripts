using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_AD_ATU 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_AD_ATU()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_AD_ATU 
        : FFParser_Tgt_Generic_EFT_AD_ATU
    {
        internal FFParser_Tgt_MC300_EFT_AD_ATU()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_AD_Amount_Request_G2H 
        : FFParser_Tgt_MC300_EFT_AD_ATU
    {
        internal FFParser_Tgt_MC300_EFT_AD_Amount_Request_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, byte[] buffer)
        {
            FFTgt_G2H_AD_Amount_Request tgt = new FFTgt_G2H_AD_Amount_Request();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.Status = (FF_GmuId_EFT_AutoDownload_Status)buffer[5];
            tgt.AccountType = (FF_GmuId_EFT_AutoDownload_TopUp_AccountTypes)buffer[6];
            tgt.AutoDownloadAmount = FreeformHelper.GetBCDValue<double>(buffer, 0, 7, 4);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_EFT_AD_Amount_Response_H2G 
        : FFParser_Tgt_MC300_EFT_AD_ATU
    {
        internal FFParser_Tgt_MC300_EFT_AD_Amount_Response_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, byte[] buffer)
        {
            FFTgt_H2G_AD_Amount_Request tgt = new FFTgt_H2G_EFT_WithdrawalAuthorization();
            tgt.ErrorCode = buffer[0];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 1, 5);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToNumberInt32(buffer, 6, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 7, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_WithdrawalAuthorization2 tgt2 = tgt as FFTgt_H2G_EFT_WithdrawalAuthorization2;
            buffer.Add(tgt2.AccountType.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.NonCashableAmount, 4);
            buffer.SetBCDValue(tgt2.CashableAmount, 4);
            buffer.SetBCDValue(tgt2.NonCashableBalanceAmount, 4);
            buffer.SetBCDValue(tgt2.CashableBalanceAmount, 4);
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetBooleanArrayValues(tgt2.PlayerFlags, 3);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }

    internal class FFParser_Tgt_MC300_EFT_ATU_Amount_Request_G2H
        : FFParser_Tgt_MC300_EFT_AD_ATU
    {
        internal FFParser_Tgt_MC300_EFT_ATU_Amount_Request_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, byte[] buffer)
        {
            FFTgt_G2H_ATU_AmountRequest tgt = new FFTgt_G2H_ATU_AmountRequest();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.Status = (FF_GmuId_EFT_AutoTopUp_Status)buffer[5];
            tgt.AccountType = (FF_GmuId_EFT_AutoDownload_TopUp_AccountTypes)buffer[6];
            tgt.AutoDownloadMaxAmount = FreeformHelper.GetBCDValue<double>(buffer, 0, 7, 4);
            tgt.AutoTopUpTrigger = FreeformHelper.GetBCDValue<double>(buffer, 0, 11, 4);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_EFT_ATU_Amount_Response_H2G 
        : FFParser_Tgt_MC300_EFT_AD_ATU
    {
        internal FFParser_Tgt_MC300_EFT_ATU_Amount_Response_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, byte[] buffer)
        {
            FFTgt_H2G_ATU_Amount_Response tgt = new FFTgt_H2G_ATU_Amount_Response();
            tgt.ErrorCode = buffer[0];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 1, 5);
            tgt.DispMsgLength = FreeformHelper.GetBytesToNumberInt32(buffer, 6, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 7, 128);
            return tgt;
        }
    }
}
