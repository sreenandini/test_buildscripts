using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_GIM_GameIDRequest : FFTgt_B2B_GIM_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GIM_SubTargets.GameIDRequest;
            }
        }       
    }
}
