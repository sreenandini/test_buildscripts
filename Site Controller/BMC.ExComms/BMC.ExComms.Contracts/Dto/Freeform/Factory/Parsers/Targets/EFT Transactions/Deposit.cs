using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_Deposit
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_Deposit()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_Deposit
        : FFParser_Tgt_Generic_EFT_Deposit
    {
        internal FFParser_Tgt_MC300_EFT_Deposit()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_DepositReq_G2H
        : FFParser_Tgt_MC300_EFT_Withdrawal
    {
        internal FFParser_Tgt_MC300_EFT_DepositReq_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_DepositRequest tgt = new FFTgt_G2H_EFT_DepositRequest();
            tgt.NonCashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 0, 4);
            tgt.CashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 4, 4);
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 8, 5);
            tgt.Pin = FreeformHelper.GetBCDValueString(buffer, 0, 13, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_DepositRequest tgt2 = tgt as FFTgt_G2H_EFT_DepositRequest;
            buffer.SetBCDValue(tgt2.NonCashableAmount, 4);
            buffer.SetBCDValue(tgt2.CashableAmount, 4);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetBCDValue(tgt2.Pin, 2);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_DepositAuth_H2G
        : FFParser_Tgt_MC300_EFT_Deposit
    {
        internal FFParser_Tgt_MC300_EFT_DepositAuth_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_DepositAuthorization tgt = new FFTgt_H2G_EFT_DepositAuthorization();
            tgt.ErrorCode = buffer[0];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 1, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 6, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToNumberUInt8(buffer, 9, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 10, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_DepositAuthorization tgt2 = tgt as FFTgt_H2G_EFT_DepositAuthorization;
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }

    internal class FFParser_Tgt_MC300_EFT_DepositComplete_G2H
        : FFParser_Tgt_MC300_EFT_Deposit
    {
        internal FFParser_Tgt_MC300_EFT_DepositComplete_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_DepositComplete tgt = new FFTgt_G2H_EFT_DepositComplete();
            tgt.NonCashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 0, 4);
            tgt.CashableAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 4, 4);
            tgt.ErrorCode = buffer[8];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 9, 5);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_DepositComplete tgt2 = tgt as FFTgt_G2H_EFT_DepositComplete;
            buffer.SetBCDValue(tgt2.NonCashableAmount, 4);
            buffer.SetBCDValue(tgt2.CashableAmount, 4);
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_DepositAck_H2G
        : FFParser_Tgt_MC300_EFT_Deposit
    {
        internal FFParser_Tgt_MC300_EFT_DepositAck_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_DepositAcknowledgement tgt = new FFTgt_H2G_EFT_DepositAcknowledgement();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 5, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToNumberUInt8(buffer, 8, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 9, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_DepositAcknowledgement tgt2 = tgt as FFTgt_H2G_EFT_DepositAcknowledgement;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }
}
