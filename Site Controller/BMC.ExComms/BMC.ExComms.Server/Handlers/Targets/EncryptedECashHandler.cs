using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets
{
    [FreeformHandler(FF_AppId_SessionIds.EncryptedECash)]
    internal sealed class FreeformHandler_EncryptedECash
        : FreeformHandler_Generic
    {
    }
}
