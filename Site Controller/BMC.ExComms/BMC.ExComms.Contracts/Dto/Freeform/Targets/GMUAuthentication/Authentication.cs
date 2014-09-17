using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_B2B_GMU_Auth
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.GMUAuthentication;
            }
        }

        public FFTgt_B2B_GMU_Auth_Data GMU_AuthData { get; set; }
    }

    public class FFTgt_B2B_GMU_Auth_Data
        : FFTgt_B2B { }
}
