using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_CodeDownload_ChangeVersion
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_CodeDownload_ChangeVersion()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_ChangeVersion
        : FFParser_Tgt_Generic_CodeDownload_ChangeVersion
    {
        internal FFParser_Tgt_MC300_CodeDownload_ChangeVersion()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_CodeDownload_ChangeVersion_H2G
        : FFParser_Tgt_MC300_CodeDownload_ChangeVersion
    {
        internal FFParser_Tgt_MC300_CodeDownload_ChangeVersion_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_CodeDownload_ChangeVersion tgt = new FFTgt_H2G_CodeDownload_ChangeVersion();
            tgt.NewVersion = FreeformHelper.GetASCIIStringValue(buffer, 0, 7);
            tgt.OldVersion = FreeformHelper.GetASCIIStringValue(buffer, 7, 7);
            return tgt;
        }
    }
}
