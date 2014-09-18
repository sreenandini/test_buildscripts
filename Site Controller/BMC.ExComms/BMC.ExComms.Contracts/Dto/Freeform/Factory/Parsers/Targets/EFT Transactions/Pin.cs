using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_Pin 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_Pin()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_Pin 
        : FFParser_Tgt_Generic_EFT_Pin
    {
        internal FFParser_Tgt_MC300_EFT_Pin()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_PinChkReq_G2H 
        : FFParser_Tgt_MC300_EFT_Pin
    {
        internal FFParser_Tgt_MC300_EFT_PinChkReq_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_PinCheck_Request tgt = new FFTgt_G2H_EFT_PinCheck_Request();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.Pin = FreeformHelper.GetBCDValueString(buffer, 0, 5, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_PinCheck_Request tgt2 = tgt as FFTgt_G2H_EFT_PinCheck_Request;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetBCDValue(tgt2.Pin, 2);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_PinChkReply_H2G 
        : FFParser_Tgt_MC300_EFT_Pin
    {
        internal FFParser_Tgt_MC300_EFT_PinChkReply_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_PinCheckReply tgt = new FFTgt_H2G_EFT_PinCheckReply();
            tgt.ErrorCode = buffer[0];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 1, 5);
            tgt.PlayerFlags.BytesValue = FreeformHelper.GetRange(buffer, 5, 3);
            tgt.DisplayMessageLength = FreeformHelper.GetBytesToNumberUInt8(buffer, 8, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 9, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_PinCheckReply tgt2 = tgt as FFTgt_H2G_EFT_PinCheckReply;
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.AddRange(tgt2.PlayerFlags.BytesValue);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }

    internal class FFParser_Tgt_MC300_EFT_PinChangeReq_G2H 
        : FFParser_Tgt_MC300_EFT_Pin
    {
        internal FFParser_Tgt_MC300_EFT_PinChangeReq_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_PIN_ChangeRequest tgt = new FFTgt_G2H_EFT_PIN_ChangeRequest();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.CurrentPIN = FreeformHelper.GetBCDValueString(buffer, 0, 5, 2);
            tgt.NewPIN = FreeformHelper.GetBCDValueString(buffer, 0, 7, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_PIN_ChangeRequest tgt2 = tgt as FFTgt_G2H_EFT_PIN_ChangeRequest;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetBCDValue(tgt2.CurrentPIN, 2);
            buffer.SetBCDValue(tgt2.NewPIN, 2);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_PinChangeResponse_H2G
        : FFParser_Tgt_MC300_EFT_Pin
    {
        internal FFParser_Tgt_MC300_EFT_PinChangeResponse_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_PIN_ChangeResponse tgt = new FFTgt_H2G_EFT_PIN_ChangeResponse();
            tgt.ErrorCode = buffer[0];
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 1, 5);
            tgt.DisplayMessageLength= FreeformHelper.GetBytesToNumberInt32(buffer, 6, 1);
            tgt.DisplayMessage = FreeformHelper.GetBCDValueString(buffer, 0, 7, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_PIN_ChangeResponse tgt2 = tgt as FFTgt_H2G_EFT_PIN_ChangeResponse;
            buffer.Add(tgt2.ErrorCode);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.Add((byte)tgt2.DisplayMessageLength);
            buffer.AddRange(tgt2.DisplayMessage.GetASCIIBytesValue(tgt2.DisplayMessageLength));
        }
    }
}
