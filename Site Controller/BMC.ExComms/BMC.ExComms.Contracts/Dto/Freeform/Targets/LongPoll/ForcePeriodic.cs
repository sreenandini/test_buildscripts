using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_LP_ForcePeriodic
        : FFTgt_H2G
    {
        #region Properties

        ///// <summary>
        ///// Data1
        ///// </summary>
        public byte Data1 { get; set; }

        ///// <summary>
        ///// Data2
        ///// </summary>
        public byte Data2 { get; set; }

        #endregion //Properties
    }
}
