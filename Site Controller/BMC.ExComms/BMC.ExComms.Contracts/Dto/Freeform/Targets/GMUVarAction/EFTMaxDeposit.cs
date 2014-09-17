using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU To Host Freeform for Time Of Day Request
    /// </summary>
    public class FFTgt_G2H_GVA_EFT_MaxDeposit_Request
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Time Of Day Response
    /// </summary>
    public class FFTgt_H2G_GVA_EFT_MaxDeposit_Response
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    {
        // MaxDeposit value
        public double MaxDeposit { get; set; }
    }
}
