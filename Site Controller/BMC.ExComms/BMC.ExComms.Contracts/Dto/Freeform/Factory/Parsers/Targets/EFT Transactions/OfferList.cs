using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_OfferList 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_OfferList()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_OfferList 
        : FFParser_Tgt_Generic_EFT_OfferList
    {
        internal FFParser_Tgt_MC300_EFT_OfferList()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_OfferListReq_G2H 
        : FFParser_Tgt_MC300_EFT_OfferList
    {
        internal FFParser_Tgt_MC300_EFT_OfferListReq_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_OfferListRequest tgt = new FFTgt_G2H_EFT_OfferListRequest();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_OfferListRequest tgt2 = tgt as FFTgt_G2H_EFT_OfferListRequest;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_OfferList_H2G
        : FFParser_Tgt_MC300_EFT_OfferList
    {
        internal FFParser_Tgt_MC300_EFT_OfferList_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_OfferList tgt = new FFTgt_H2G_EFT_OfferList();
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 0, 5);
            tgt.Pin = FreeformHelper.GetBCDValueString(buffer, 0, 5, 2);
            tgt.NonCashableBalance = FreeformHelper.GetBytesToBCDDouble(buffer, 7, 5);
            tgt.CashableBalance = FreeformHelper.GetBytesToBCDDouble(buffer, 12, 5);
            tgt.OfferId1 = FreeformHelper.GetBytesToBCDDouble(buffer, 17, 5);
            tgt.OfferId2 = FreeformHelper.GetBytesToBCDDouble(buffer, 22, 5);
            tgt.OfferId3 = FreeformHelper.GetBytesToBCDDouble(buffer, 27, 5);
            tgt.OfferId4 = FreeformHelper.GetBytesToBCDDouble(buffer, 32, 5);
            tgt.OfferId5 = FreeformHelper.GetBytesToBCDDouble(buffer, 37, 5);
            tgt.OfferId6 = FreeformHelper.GetBytesToBCDDouble(buffer, 42, 5);
            tgt.OfferId7 = FreeformHelper.GetBytesToBCDDouble(buffer, 47, 5);
            tgt.OfferId8 = FreeformHelper.GetBytesToBCDDouble(buffer, 52, 5);
            tgt.OfferId9 = FreeformHelper.GetBytesToBCDDouble(buffer, 57, 5);
            tgt.OfferId10 = FreeformHelper.GetBytesToBCDDouble(buffer, 62, 5);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_OfferList tgt2 = tgt as FFTgt_H2G_EFT_OfferList;
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetBCDValue(tgt2.Pin, 2);
            buffer.SetBCDValue(tgt2.NonCashableBalance, 5);
            buffer.SetBCDValue(tgt2.CashableBalance, 5);
            buffer.SetBCDValue(tgt2.OfferId1, 5);
            buffer.SetBCDValue(tgt2.OfferId2, 5);
            buffer.SetBCDValue(tgt2.OfferId3, 5);
            buffer.SetBCDValue(tgt2.OfferId4, 5);
            buffer.SetBCDValue(tgt2.OfferId5, 5);
            buffer.SetBCDValue(tgt2.OfferId6, 5);
            buffer.SetBCDValue(tgt2.OfferId7, 5);
            buffer.SetBCDValue(tgt2.OfferId8, 5);
            buffer.SetBCDValue(tgt2.OfferId9, 5);
            buffer.SetBCDValue(tgt2.OfferId10, 5);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_OfferReq_G2H 
        : FFParser_Tgt_MC300_EFT_OfferList
    {
        internal FFParser_Tgt_MC300_EFT_OfferReq_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_OfferRequest tgt = new FFTgt_G2H_EFT_OfferRequest();
            tgt.AccountType = buffer[0].GetAppId<FF_GmuId_EFT_AccountTypes, FF_AppId_EFT_AccountTypes>();
            tgt.PlayerAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 1, 5);
            tgt.Pin = FreeformHelper.GetBCDValueString(buffer, 0, 6, 2);
            tgt.OfferId = FreeformHelper.GetBytesToBCDDouble(buffer, 8, 5);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_OfferRequest tgt2 = tgt as FFTgt_G2H_EFT_OfferRequest;
            buffer.Add(tgt2.AccountType.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.PlayerAccountNumber, 5);
            buffer.SetBCDValue(tgt2.Pin, 2);
            buffer.SetBCDValue(tgt2.OfferId, 5);
        }
    }
}
