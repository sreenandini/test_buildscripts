using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_Encrypted
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_Encrypted()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_Encrypted
        : FFParser_Tgt_Generic_Encrypted
    {
        internal FFParser_Tgt_MC300_Encrypted()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            byte[] buffer2 = FreeformEntityFactory.EncryptionFactory.Decrypt(rootEntity as IFreeformEntity_Msg, buffer);
            return new FFTgt_B2B_Encrypted()
            {
                DecryptedData = buffer2,
            };
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            List<byte> buffer2 = FreeformEntityFactory.EncryptionFactory.Encrypt(tgt.Parent as IFreeformEntity_Msg, tgt, buffer);
            buffer.Clear();
            buffer.AddRange(buffer2);
        }
    }

    internal class FFParser_Tgt_MC300_Encrypted_G2H
        : FFParser_Tgt_MC300_Encrypted
    {
        internal FFParser_Tgt_MC300_Encrypted_G2H()
            : base()
        {
        }
    }    

    internal class FFParser_Tgt_MC300_Encrypted_H2G 
        : FFParser_Tgt_MC300_Encrypted
    {
        internal FFParser_Tgt_MC300_Encrypted_H2G()
            : base()
        {
        }
    }
}
