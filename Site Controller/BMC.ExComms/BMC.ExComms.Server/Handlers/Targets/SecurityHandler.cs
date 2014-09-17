using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers.Targets
{
    [FreeformHandler((int)FF_AppId_SessionIds.Security)]
    internal sealed class FreeformHandler_Security
        : FreeformHandler_Generic
    {
        protected override void OnModifyMessage(ILogMethod method, FFMsg_G2H message)
        {
            FFTgt_B2B_Security target = message.GetTarget<FFTgt_B2B_Security>();
            if (target == null ||
                target.Targets.Count == 0)
            {
                throw new ArgumentNullException("No valid encrypted targets found");
            }
        }
    }
}
