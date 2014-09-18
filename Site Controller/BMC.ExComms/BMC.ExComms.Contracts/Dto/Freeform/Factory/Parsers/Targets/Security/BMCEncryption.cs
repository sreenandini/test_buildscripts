using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    
    internal class FFParser_Tgt_Generic_BMCEncryption : FFTgtParser
    {
        internal FFParser_Tgt_Generic_BMCEncryption()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_BMCEncryption : FFParser_Tgt_Generic_BMCEncryption
    {
        internal FFParser_Tgt_MC300_BMCEncryption()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_BMCEncryption_GmuInit_G2H : FFParser_Tgt_MC300_BMCEncryption
    {
        internal FFParser_Tgt_MC300_BMCEncryption_GmuInit_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Security_BMCEncryption tgt = new FFTgt_G2H_Security_BMCEncryption();
            tgt.EntityData = buffer;//Want to Decrypt
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_BMCEncryption_GmuInit_H2G : FFParser_Tgt_MC300_BMCEncryption
    {
        internal FFParser_Tgt_MC300_BMCEncryption_GmuInit_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_Security_BMCEncryption_Response tgt = new FFTgt_H2G_Security_BMCEncryption_Response();
            tgt.Status = (FF_AppId_ResponseStatus_Types)buffer[0];
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_BMCEncryption_HostInit_H2G : FFParser_Tgt_MC300_BMCEncryption
    {
        internal FFParser_Tgt_MC300_BMCEncryption_HostInit_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_Security_BMCEncryption tgt = new FFTgt_H2G_Security_BMCEncryption();
            tgt.EntityData = buffer;//Want to Decrypt
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_BMCEncryption_HostInit_G2H : FFParser_Tgt_MC300_BMCEncryption
    {
        internal FFParser_Tgt_MC300_BMCEncryption_HostInit_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_Security_BMCEncryption_Response tgt = new FFTgt_G2H_Security_BMCEncryption_Response();
            tgt.Status = (FF_AppId_ResponseStatus_Types)buffer[0];
            return tgt;
        }
    } 
}
