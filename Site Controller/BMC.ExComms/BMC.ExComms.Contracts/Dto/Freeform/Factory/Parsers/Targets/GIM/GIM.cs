using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GIM
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GIM()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_GIM tgt = new FFTgt_B2B_GIM();
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GIM
        : FFParser_Tgt_Generic_GIM
    {
        internal FFParser_Tgt_MC300_GIM()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_GIM_G2H
        : FFParser_Tgt_Generic_GIM
    {
        internal FFParser_Tgt_MC300_GIM_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GIM_SubTargets.GameIDInfo, (int)FF_AppId_GIM_SubTargets.GameIDInfo, new FFParser_Tgt_MC300_GIM_GameIDInfo_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GIM_SubTargets.AuxNetworkEnableDisable, (int)FF_AppId_GIM_SubTargets.AuxNetworkEnableDisable, new FFParser_Tgt_MC300_GIM_AuxNetworkEnableDisable());
            this.AddBufferEntityParser((int)FF_GmuId_GIM_SubTargets.GameIDRequest, (int)FF_AppId_GIM_SubTargets.GameIDRequest, new FFParser_Tgt_MC300_GIM_GameIDRequest());
        }
    }

    internal class FFParser_Tgt_MC300_GIM_H2G
        : FFParser_Tgt_Generic_GIM
    {
        internal FFParser_Tgt_MC300_GIM_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GIM_SubTargets.GameIDInfo, (int)FF_AppId_GIM_SubTargets.GameIDInfo, new FFParser_Tgt_MC300_GIM_GameIDInfo_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GIM_SubTargets.AuxNetworkEnableDisable, (int)FF_AppId_GIM_SubTargets.AuxNetworkEnableDisable, new FFParser_Tgt_MC300_GIM_GameIDInfo_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GIM_SubTargets.GameIDRequest, (int)FF_AppId_GIM_SubTargets.GameIDRequest, new FFParser_Tgt_MC300_GIM_GameIDInfo_H2G());
        }

        protected override IFreeformEntity ParseBufferInternal(IFreeformEntity parent, IFreeformEntity rootEntity, ref IFreeformEntity entity, byte[] buffer, int offset,
            int length)
        {
            FFTgt_H2G_GIM_GameIDInfo tgt = new FFTgt_H2G_GIM_GameIDInfo();
            parent.Targets.Add(tgt);
            this.GetParserFromAppId((int)FF_AppId_GIM_SubTargets.GameIDInfo)
                .ParseBuffer(tgt, rootEntity, buffer, offset, length);
            return tgt;
        }
    }
}
