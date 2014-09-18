using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    internal class FFParser_Tgt_Generic_BMCAuthentication : FFTgtParser
    {
        internal FFParser_Tgt_Generic_BMCAuthentication()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_BMCAuthentication : FFParser_Tgt_Generic_BMCAuthentication
    {
        internal FFParser_Tgt_MC300_BMCAuthentication()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_BMCAuthentication_G2H : FFParser_Tgt_MC300_BMCAuthentication
    {
        internal FFParser_Tgt_MC300_BMCAuthentication_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_BMCAuthentication tgt = new FFTgt_B2B_Security_BMCAuthentication();
            tgt.AuthenticationByte = buffer[0];
            tgt.EntityData = FreeformHelper.CopyToBuffer(buffer, 1, buffer.Length);//want to decrypt
            return tgt;
        }
    }


    internal class FFParser_Tgt_MC300_BMCAuthentication_H2G : FFParser_Tgt_MC300_BMCAuthentication
    {
        internal FFParser_Tgt_MC300_BMCAuthentication_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_BMCAuthentication tgt = new FFTgt_B2B_Security_BMCAuthentication();
            tgt.AuthenticationByte = buffer[0];
            tgt.EntityData = FreeformHelper.CopyToBuffer(buffer, 1, buffer.Length);//want to decrypt
            return tgt;
        }
    }
}
