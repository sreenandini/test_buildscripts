using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// This target allows the caller to perform default type I/O at the GMU
    /// </summary>
    public class FFTgt_B2B_DefaultIO
       : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.DefaultIO;
            }
        }

        public FFTgt_B2B_DefaultIO_Data DefaultIOData { get; set; }
    }

    public class FFTgt_B2B_DefaultIO_Data
        : FFTgt_B2B { }
}
