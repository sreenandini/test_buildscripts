using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_Withdrawal
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_Withdrawal()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_Withdrawal
        : FFParser_Tgt_Generic_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_Withdrawal()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_WithdrawalReq_G2H
        : FFParser_Tgt_MC300_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_WithdrawalReq_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_WithdrawalRequest tgt = new FFTgt_G2H_EFT_WithdrawalRequest();
            tgt.AccountType = buffer[0].GetAppId<FF_GmuId_EFT_AccountTypes, FF_AppId_EFT_AccountTypes>();
            tgt.AmountRequested = FreeformHelper.GetBytesToBCDDouble(buffer, 1, 4);
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 5);
            tgt.Pin = FreeformHelper.GetBCDValueString(buffer, 0, 10, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_WithdrawalRequest tgt2 = tgt as FFTgt_G2H_EFT_WithdrawalRequest;
            buffer.Add(tgt2.AccountType.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.AmountRequested, 4);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetBCDValue(tgt2.Pin, 2);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_WithdrawalAuth_H2G
        : FFParser_Tgt_MC300_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_WithdrawalAuth_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_WithdrawalAuthorization tgt = new FFTgt_H2G_EFT_WithdrawalAuthorization();
            tgt.NonCashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 0, 4);
            tgt.CashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 4, 4);
            tgt.ErrorCode = buffer[8];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 9, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 14, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToBCDUInt8(buffer, 17, 1);
            tgt.DisplayMessage = FreeformHelper.GetASCIIStringValue(buffer, 18, tgt.DisplayMessageLength);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_WithdrawalAuthorization tgt2 = tgt as FFTgt_H2G_EFT_WithdrawalAuthorization;
            buffer.SetBCDValue(tgt2.NonCashableAmount, 4);
            buffer.SetBCDValue(tgt2.CashableAmount, 4);
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }

    internal class FFParser_Tgt_MC300_EFT_WithdrawalComplete_G2H : FFParser_Tgt_MC300_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_WithdrawalComplete_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_WithdrawalComplete tgt = new FFTgt_G2H_EFT_WithdrawalComplete();
            tgt.NonCashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 0, 4);
            tgt.CashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 4, 4);
            tgt.ErrorCode = buffer[8];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 9, 5);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_WithdrawalComplete tgt2 = tgt as FFTgt_G2H_EFT_WithdrawalComplete;
            buffer.SetBCDValue(tgt2.NonCashableAmount, 4);
            buffer.SetBCDValue(tgt2.CashableAmount, 4);
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_WithdrawalAck_H2G : FFParser_Tgt_MC300_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_WithdrawalAck_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_WithdrawalAcknowledgement tgt = new FFTgt_H2G_EFT_WithdrawalAcknowledgement();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 5, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToBCDUInt8(buffer, 8, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 9, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_WithdrawalAcknowledgement tgt2 = tgt as FFTgt_H2G_EFT_WithdrawalAcknowledgement;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }

    internal class FFParser_Tgt_MC300_EFT_WithdrawalAuth2_H2G : FFParser_Tgt_MC300_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_WithdrawalAuth2_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_WithdrawalAuthorization2 tgt = new FFTgt_H2G_EFT_WithdrawalAuthorization2();
            tgt.AccountType = (FF_AppId_EFT_AccountTypes)buffer[0];
            tgt.NonCashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 1, 4);
            tgt.CashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 5, 4);
            tgt.NonCashableBalanceAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 9, 4);
            tgt.CashableBalanceAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 13, 4);
            tgt.ErrorCode = buffer[17];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 18, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 23, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToBCDUInt8(buffer, 26, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 27, 128);
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
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }
}
