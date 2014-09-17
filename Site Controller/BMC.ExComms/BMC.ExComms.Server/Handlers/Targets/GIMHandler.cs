using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets
{
    [FreeformHandler((int)FF_AppId_SessionIds.GIM)]
    internal sealed class FreeformHandler_GIM
        : FreeformHandler_Generic
    {
    }
}
