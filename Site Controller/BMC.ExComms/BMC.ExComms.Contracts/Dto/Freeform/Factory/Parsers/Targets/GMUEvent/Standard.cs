using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GMUEvent_Standard : FFTgtParser
    {
        internal FFParser_Tgt_MC300_GMUEvent_Standard()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent_StdData tgt = new FFTgt_G2H_GMUEvent_StdData();
            tgt.ExceptionCode = buffer[0].GetAppId<FF_GmuId_GMUEvent_XCodes, FF_AppId_GMUEvent_XCodes>();
            tgt.JackpotID = buffer[1].GetAppId<FF_GmuId_GMUEvent_JackpotIDs, FF_AppId_GMUEvent_JackpotIDs>();
            tgt.EmployeeCardID = FreeformHelper.GetHexStringValue(buffer, 2, 2);
            tgt.LastBet = FreeformHelper.GetBCDValue<short>(buffer,  4, 2);
            tgt.DoorStatus = FreeformHelper.GetBytesToNumberUInt8(buffer, 6, 1);
            tgt.OptionByte = FreeformHelper.GetBytesToNumberUInt8(buffer, 7, 1);
            tgt.JackpotAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 8, 6);
            tgt.PlayerCard = FreeformHelper.GetBCDValueString(buffer, 0, 14, 5);
            tgt.BonusPoints = FreeformHelper.GetBCDValue<short>(buffer, 19, 2);
            tgt.LastBill = FreeformHelper.GetBytesToNumberUInt8(buffer, 21, 1);
            tgt.SMICode = FreeformHelper.GetASCIIStringValueTrim(buffer, 22, 8);
            tgt.GameDenomination = FreeformHelper.GetBCDValue<int>(buffer, 30, 4);
            tgt.CasinoID = FreeformHelper.GetASCIIStringValueTrim(buffer, 34, 3);
            tgt.BonusCountdown = FreeformHelper.GetBCDValue<short>(buffer, 37, 2);
            tgt.BonusPoints = FreeformHelper.GetBCDValue<short>(buffer, 39, 2);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_GMUEvent_StdData tgt2 = tgt as FFTgt_G2H_GMUEvent_StdData;
            buffer.Add(tgt2.ExceptionCode.GetGmuIdInt8<FF_AppId_GMUEvent_XCodes>());
            buffer.Add(tgt2.JackpotID.GetGmuIdInt8<FF_AppId_GMUEvent_JackpotIDs>());
            buffer.AddRange(tgt2.EmployeeCardID.GetHexBytesValue(2));
            buffer.AddRange(tgt2.LastBet.GetBCDToBytes(2));
            buffer.Add(tgt2.DoorStatus);
            buffer.Add(tgt2.OptionByte);
            buffer.AddRange(tgt2.JackpotAmount.GetBCDToBytes(6));
            buffer.AddRange(tgt2.PlayerCard.GetBCDToBytes(5));
            buffer.AddRange(tgt2.BonusPoints.GetBCDToBytes(2));
            buffer.Add(tgt2.LastBill);
            buffer.AddRange(tgt2.SMICode.GetASCIIBytesValueSpace(8));
            buffer.AddRange(tgt2.GameDenomination.GetBCDToBytes(4));
            buffer.AddRange(tgt2.CasinoID.GetASCIIBytesValueSpace(3));
            buffer.AddRange(tgt2.BonusCountdown.GetBCDToBytes(2));
            buffer.AddRange(tgt2.BonusPoints.GetBCDToBytes(2));
        }
    }
}
