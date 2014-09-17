using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_GMU_Auth_Query
        : FFTgt_B2B_GMU_Auth_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUAuthentication.Query;
            }
        }
    }
}
