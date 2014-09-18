using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public interface IFreeformEncryptionFactory
        : IDisposable
    {
        List<byte> Encrypt(IFreeformEntity_Msg message, IFreeformEntity_MsgTgt target, List<byte> buffer);
        byte[] Decrypt(IFreeformEntity_Msg message, byte[] buffer);
    }
}
