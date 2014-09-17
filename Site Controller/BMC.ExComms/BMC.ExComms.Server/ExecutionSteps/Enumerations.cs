using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal enum ExecutionStepStates
    {
        HostStarted = 0,

        GmuRequest,
        GmuResponse,

        KeyExchangeStart,
        KeyExchangePartialKey,
        KeyExchangeEnd,
        KeyExchangeStatus,
    }
}
