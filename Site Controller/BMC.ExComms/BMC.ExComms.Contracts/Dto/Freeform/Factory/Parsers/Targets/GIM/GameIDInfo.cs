using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BMC.CoreLib;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GIM_GameIDInfo
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GIM_GameIDInfo()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GIM_GameIDInfo tgt = new FFTgt_G2H_GIM_GameIDInfo();
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GIM_GameIDInfo_G2H
        : FFParser_Tgt_Generic_GIM_GameIDInfo
    {
        internal FFParser_Tgt_MC300_GIM_GameIDInfo_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.GMUGameNumber, (int)FF_AppId_GIM_GameIDInfoTags.GMUGameNumber,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).GMUNumber = buf.GetASCIIStringValue();
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.GameGameNumber, (int)FF_AppId_GIM_GameIDInfoTags.GameGameNumber,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).AssetNumber = buf.GetASCIIStringValue();
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.ManufacturerID, (int)FF_AppId_GIM_GameIDInfoTags.ManufacturerID,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).ManufacturerID = buf.GetHexStringValue();
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.SerialNumber, (int)FF_AppId_GIM_GameIDInfoTags.SerialNumber,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).SerialNumber = buf.GetASCIIStringValue();
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.MACAddress, (int)FF_AppId_GIM_GameIDInfoTags.MACAddress,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).MACAddress = buf.GetHexStringValue(':');
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.SASVersion, (int)FF_AppId_GIM_GameIDInfoTags.SASVersion,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).SASVersion = buf.GetASCIIStringValue();
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfoTags.GMUVersion, (int)FF_AppId_GIM_GameIDInfoTags.GMUVersion,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).GMUVersion = buf.GetASCIIStringValue();
                    return null;
                });
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_GIM_GameIDInfo tgt2 = tgt as FFTgt_G2H_GIM_GameIDInfo;
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.GMUGameNumber, tgt2.GMUNumber.GetASCIIBytesValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.GameGameNumber, tgt2.AssetNumber.GetASCIIBytesValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.ManufacturerID, tgt2.ManufacturerID.GetHexBytesValue(2));
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.SerialNumber, tgt2.SerialNumber.GetASCIIBytesValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.MACAddress, tgt2.MACAddress.GetMACAddressBytesValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.SASVersion, tgt2.SASVersion.GetASCIIBytesValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GmuId_GIM_GameIDInfoTags.GMUVersion, tgt2.GMUVersion.GetASCIIBytesValue());
        }
    }

    internal class FFParser_Tgt_MC300_GIM_GameIDInfo_H2G
        : FFParser_Tgt_Generic_GIM_GameIDInfo
    {
        internal FFParser_Tgt_MC300_GIM_GameIDInfo_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        public override bool SkipTargetInfo
        {
            get { return true; }
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfo_H2G.DisplayMessage,
                (int)FF_AppId_GIM_GameIDInfo_H2G.DisplayMessage,
                (tgt, idx, len, buf) =>
                {
                    FFTgt_H2G_GIM_GameIDInfo tgt2 = tgt as FFTgt_H2G_GIM_GameIDInfo;
                    tgt2.EnableNetworkMessaging = buf[0].ConvertToBoolean();
                    tgt2.DisplayMessage = buf.GetASCIIStringValue(1, -1);
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfo_H2G.IPAddress,
                 (int)FF_AppId_GIM_GameIDInfo_H2G.IPAddress,
                 (tgt, idx, len, buf) =>
                 {
                     FFTgt_H2G_GIM_GameIDInfo tgt2 = tgt as FFTgt_H2G_GIM_GameIDInfo;
                     tgt2.SourceAddress = new IPAddress(buf);
                     return null;
                 });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfo_H2G.AssetNumber,
                 (int)FF_AppId_GIM_GameIDInfo_H2G.AssetNumber,
                 (tgt, idx, len, buf) =>
                 {
                     FFTgt_H2G_GIM_GameIDInfo tgt2 = tgt as FFTgt_H2G_GIM_GameIDInfo;
                     tgt2.AssetNumberInt = buf.GetBytesToBCDInt32(0, len);
                     //tgt2.HasAssetNumber &= (tgt2.AssetNumber.Length > 0);
                     return null;
                 });
            this.AddBufferEntityParser((int)FF_GmuId_GIM_GameIDInfo_H2G.PokerGamePrefix,
                 (int)FF_AppId_GIM_GameIDInfo_H2G.PokerGamePrefix,
                 (tgt, idx, len, buf) =>
                 {
                     FFTgt_H2G_GIM_GameIDInfo tgt2 = tgt as FFTgt_H2G_GIM_GameIDInfo;
                     tgt2.PokerGamePrefix = buf.GetASCIIStringValue();
                     //tgt2.HasAssetNumber &= !tgt2.PokerGamePrefix.IsEmpty();
                     return null;
                 });
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_GIM_GameIDInfo tgt2 = tgt as FFTgt_H2G_GIM_GameIDInfo;

            // display message length & message
            byte[] display = tgt2.DisplayMessage.GetASCIIBytesValue();
            this.AddTargetToBuffer(ref buffer, 2,
                new byte[][]
                {
                    new byte[] { tgt2.EnableNetworkMessaging.ConvertToByte() },
                    display,
                });

            // source ip address
            IPAddress ipAddr = tgt2.SourceAddress;
            this.AddTargetToBuffer(ref buffer, 3, ipAddr.GetAddressBytes());

            // asset number and game prefix
            if (tgt2.AssetNumberInt > 0)
            {
                this.AddTargetToBuffer(ref buffer, 4, tgt2.AssetNumberInt.GetBCDToBytes(4));
                byte[] pokerGamePrefix = tgt2.PokerGamePrefix.GetASCIIBytesValue();
                this.AddTargetToBuffer(ref buffer, 5, pokerGamePrefix);
            }
        }
    }
}
