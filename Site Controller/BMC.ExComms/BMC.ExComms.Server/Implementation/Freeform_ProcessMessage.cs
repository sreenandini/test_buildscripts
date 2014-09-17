using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.Hosting;

namespace BMC.ExComms.Server
{
    internal partial class ExCommsServerImpl
    {
        bool IExCommsServerImpl.ProcessH2GMessage(Contracts.DTO.Freeform.FFMsg_G2H request)
        {
            throw new NotImplementedException();
        }

        bool IExCommsServerImpl.ProcessH2GMessage(Contracts.DTO.Freeform.IFFTgt_H2G request)
        {
            throw new NotImplementedException();
        }
    }
}
