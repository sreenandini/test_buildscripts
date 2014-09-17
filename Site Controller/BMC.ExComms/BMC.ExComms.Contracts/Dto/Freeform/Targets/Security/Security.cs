using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Game Information
    /// </summary>
    public class FFTgt_B2B_Security
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.Security;
            }
        }

        public FFTgt_B2B_Security_Data SecurityData
        {
            get { return this.GetPrimaryTarget<FFTgt_B2B_Security_Data>(); }
            set { this.SetPrimaryTarget<FFTgt_B2B_Security_Data>(value); }
        }
    }

    public class FFTgt_B2B_Security_ResponseRequired :
        FFTgt_B2B_Security
    {
        public FFTgt_B2B_Security_ResponseRequired()
        {
            this.IsResponseRequired = true;
        }
    }

    public class FFTgt_B2B_Security_Data
        : FFTgt_B2B { }
}
