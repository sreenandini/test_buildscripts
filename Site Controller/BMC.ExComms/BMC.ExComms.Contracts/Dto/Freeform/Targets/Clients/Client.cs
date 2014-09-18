using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_Client
       : FFTgt_B2B_SubData<FFTgt_B2B_ClientData>
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.Internal;
            }
        }
    }

    public class FFTgt_B2B_ClientData : FFTgt_B2B { }
}
