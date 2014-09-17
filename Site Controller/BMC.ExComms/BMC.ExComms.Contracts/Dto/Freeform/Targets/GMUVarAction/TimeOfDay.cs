using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public abstract class FFTgt_B2B_GVA_TimeOfDay_Base
        : FFTgt_B2B_GMUVarAction_Data
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TimeOfDay;
            }
        }
    }

    /// <summary>
    /// GMU To Host Freeform for Time Of Day Request
    /// </summary>
    public class FFTgt_G2H_GVA_TimeOfDay_Request
        : FFTgt_B2B_GVA_TimeOfDay_Base, IFFTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Time Of Day Response
    /// </summary>
    public class FFTgt_H2G_GVA_TimeOfDay_Response
        : FFTgt_B2B_GVA_TimeOfDay_Base, IFFTgt_H2G
    {
        // Time Of Day value HHMMSS 24hrs format
        public TimeSpan TimeOfDay { get; set; }
    }
    /// <summary>
    /// GMU To Host Freeform for Time Of Day Status
    /// </summary>
    public class FFTgt_G2H_GVA_TimeOfDay_Status
        : FFTgt_G2H_GMUVariableAction_Status, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUVarAction.TimeOfDay;
            }
        }
    }

}
