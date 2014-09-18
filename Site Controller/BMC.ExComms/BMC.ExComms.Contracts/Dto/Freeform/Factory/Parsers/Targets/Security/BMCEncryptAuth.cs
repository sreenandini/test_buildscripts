using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
   
    internal class FFParser_Tgt_Generic_BMCEncryptAuth : FFTgtParser
    {
        internal FFParser_Tgt_Generic_BMCEncryptAuth()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_BMCEncryptAuth : FFParser_Tgt_Generic_BMCEncryptAuth
    {
        internal FFParser_Tgt_MC300_BMCEncryptAuth()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_BMCEncryptAuth_G2H : FFParser_Tgt_MC300_BMCEncryptAuth
    {
        internal FFParser_Tgt_MC300_BMCEncryptAuth_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_EncryptAuthentication tgt = new FFTgt_B2B_Security_EncryptAuthentication();
            tgt.EncryptionAlgorithm = buffer[0];
            tgt.KeyIndex = buffer[1];
            tgt.EntityData = FreeformHelper.CopyToBuffer(buffer, 2, buffer.Length);
            return tgt;
        }
    }


    internal class FFParser_Tgt_MC300_BMCEncryptAuth_H2G : FFParser_Tgt_MC300_BMCEncryptAuth
    {
        internal FFParser_Tgt_MC300_BMCEncryptAuth_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_Security_EncryptAuthentication tgt = new FFTgt_B2B_Security_EncryptAuthentication();
            tgt.EncryptionAlgorithm = buffer[0];
            tgt.KeyIndex = buffer[1];
            tgt.EntityData = FreeformHelper.CopyToBuffer(buffer, 2, buffer.Length);
            return tgt;
        }
    }
}
