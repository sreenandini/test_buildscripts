using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GIM : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GIM()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, byte[] buffer)
        {
            FFTgt_B2B_GIM tgt = new FFTgt_B2B_GIM();
            entity = tgt;
            tgt.SubTargetID = (FF_GIM_SubTargets)buffer[0];
            this.ParseBuffer(entity, buffer, 0, 0);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GIM : FFParser_Tgt_Generic_GIM
    {
        internal FFParser_Tgt_MC300_GIM()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GIM_SubTargets.GameIDInfo, (int)FF_EntityId_Targets_GIM.GameIDInfo, new FFParser_Tgt_MC300_GIM_GameIDInfo());
        }
    }

    internal class FFParser_Tgt_Generic_GIM_GameIDInfo : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GIM_GameIDInfo()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, byte[] buffer)
        {
            FFTgt_G2H_GIM_GameIDInfo tgt = new FFTgt_G2H_GIM_GameIDInfo();
            this.ParseBuffer(tgt, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GIM_GameIDInfo : FFParser_Tgt_Generic_GIM_GameIDInfo
    {
        internal FFParser_Tgt_MC300_GIM_GameIDInfo()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.GMUGameNumber, (int)FF_EntityId_Targets_GIM_GameIDInfo.GMUGameNumber, 
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).GMUNumber = FreeformHelper.GetASCIIStringValue(buf);
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.GameGameNumber, (int)FF_EntityId_Targets_GIM_GameIDInfo.GameGameNumber,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).AssetNumber = FreeformHelper.GetASCIIStringValue(buf);
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.ManufacturerID, (int)FF_EntityId_Targets_GIM_GameIDInfo.ManufacturerID,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).ManufacturerID = FreeformHelper.GetHexStringValue(buf);
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.SerialNumber, (int)FF_EntityId_Targets_GIM_GameIDInfo.SerialNumber,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).SerialNumber = FreeformHelper.GetASCIIStringValue(buf);
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.MACAddress, (int)FF_EntityId_Targets_GIM_GameIDInfo.MACAddress,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).MACAddress = FreeformHelper.GetHexStringValue(buf, ':');
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.SASVersion, (int)FF_EntityId_Targets_GIM_GameIDInfo.SASVersion,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).SASVersion = FreeformHelper.GetASCIIStringValue(buf);
                    return null;
                });
            this.AddBufferEntityParser((int)FF_GIM_GameIDInfoTags.GMUVersion, (int)FF_EntityId_Targets_GIM_GameIDInfo.GMUVersion,
                (tgt, idx, len, buf) =>
                {
                    ((FFTgt_G2H_GIM_GameIDInfo)tgt).GMUVersion = FreeformHelper.GetASCIIStringValue(buf);
                    return null;
                });
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_GIM_GameIDInfo tgt2 = tgt as FFTgt_G2H_GIM_GameIDInfo;
            this.AddTargetToBuffer(ref buffer, (int)FF_GIM_GameIDInfoTags.GMUGameNumber, tgt2.GMUNumber.GetASCIIByteValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GIM_GameIDInfoTags.GameGameNumber, tgt2.AssetNumber.GetASCIIByteValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GIM_GameIDInfoTags.SASVersion, tgt2.SASVersion.GetASCIIByteValue());
            this.AddTargetToBuffer(ref buffer, (int)FF_GIM_GameIDInfoTags.GMUVersion, tgt2.GMUVersion.GetASCIIByteValue());
        }
    }
}
