using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets
{
    internal abstract class FreeformHandler_Encrypted
        : FreeformHandler_Generic
    {
        protected override void OnModifyMessage(ILogMethod method, FFMsg_G2H message)
        {
            FFTgt_B2B_Encrypted target = message.GetTarget<FFTgt_B2B_Encrypted>();
            if (target == null ||
                target.EntityData == null)
            {
                throw new ArgumentNullException("No valid encrypted targets found");
            }
        }
    }

    [FreeformHandler((int)FF_AppId_SessionIds.EncryptedECash)]
    internal sealed class FreeformHandler_EncryptedECash
        : FreeformHandler_Encrypted
    {
    }

    [FreeformHandler((int)FF_AppId_SessionIds.Ticket)]
    internal sealed class FreeformHandler_EncryptedTicket
        : FreeformHandler_Encrypted
    {
    }
}
