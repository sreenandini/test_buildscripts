using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU To Host Freeform for EFT Transaction Timeout Request
    /// </summary>
    public class FFTgt_G2H_GVA_EFT_TTimeOut_Request
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for EFT Transaction Timeout Response
    /// </summary>
    public class FFTgt_H2G_GVA_EFT_TTimeOut_Response
        : FFTgt_B2B_GMUVarAction_Data, IFFTgt_H2G
    {
        // EFT Time Out value
        public int TimeOut { get; set; }
    }
    /// <summary>
    /// GMU To Host Freeform for EFT Transaction Timeout Status
    /// </summary>
    public class FFTgt_G2H_GVA_EFT_TTimeOut_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H { }

}
