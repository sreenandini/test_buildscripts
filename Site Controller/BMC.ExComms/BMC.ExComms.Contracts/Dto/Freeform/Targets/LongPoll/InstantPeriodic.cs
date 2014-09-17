using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_LP_InstantPeriodic
        : FFTgt_H2G
    {
        #region Properties

        /// <summary>
        /// Configured Interval
        /// </summary>
        public byte ConfiguredInterval { get; set; }

        /// <summary>
        /// Lower Order Interval
        /// </summary>
        public byte LowerOrderInterval { get; set; }

        /// <summary>
        /// Higer Order Interval
        /// </summary>
        public byte HigherOrderInterval { get; set; }

        #endregion //Properties
    }
}
