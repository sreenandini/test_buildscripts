using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_SysEnableDisable 
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_SysEnableDisable()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_SysEnableDisable 
        : FFParser_Tgt_Generic_EFT_SysEnableDisable
    {
        internal FFParser_Tgt_MC300_EFT_SysEnableDisable()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_SysEnable_H2G 
        : FFParser_Tgt_MC300_EFT_SysEnableDisable
    {
        internal FFParser_Tgt_MC300_EFT_SysEnable_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_SystemEnable tgt = new FFTgt_H2G_EFT_SystemEnable();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            base.GetTargetData(tgt, ref buffer);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SysEnable_G2H
        : FFParser_Tgt_MC300_EFT_SysEnableDisable
    {
        internal FFParser_Tgt_MC300_EFT_SysEnable_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_SystemEnable tgt = new FFTgt_G2H_EFT_SystemEnable();
            tgt.ResponseType = buffer[0].GetAppId<FF_GmuId_EFT_ResponseTypes, FF_AppId_EFT_ResponseTypes>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_SystemEnable tgtSrc = tgt as FFTgt_G2H_EFT_SystemEnable;
            buffer.Add(tgtSrc.ResponseType.GetGmuIdInt8());
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SysDisable_H2G 
        : FFParser_Tgt_MC300_EFT_SysEnableDisable
    {
        internal FFParser_Tgt_MC300_EFT_SysDisable_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_SystemDisable tgt = new FFTgt_H2G_EFT_SystemDisable();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            base.GetTargetData(tgt, ref buffer);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SysDisable_G2H
        : FFParser_Tgt_MC300_EFT_SysEnableDisable
    {
        internal FFParser_Tgt_MC300_EFT_SysDisable_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_SystemDisable tgt = new FFTgt_G2H_EFT_SystemDisable();
            tgt.ResponseType = buffer[0].GetAppId<FF_GmuId_EFT_ResponseTypes, FF_AppId_EFT_ResponseTypes>();
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_SystemDisable tgtSrc = tgt as FFTgt_G2H_EFT_SystemDisable;
            buffer.Add(tgtSrc.ResponseType.GetGmuIdInt8());
        }
    }
}
