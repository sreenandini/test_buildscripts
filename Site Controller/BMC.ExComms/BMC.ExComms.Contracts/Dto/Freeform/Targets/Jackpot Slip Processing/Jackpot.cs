using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Base class - GMU to Host (or) Host to GMU Freeform for Ticket Processing info
    /// </summary>
    public class FFTgt_B2B_Jackpot 
        : FFTgt_B2B
    {
        #region Private Members

        private FFTgt_B2B_JackpotData _jackpotData;

        #endregion //Private Members

        #region Properties

        // Ticket Sub Target
        public FFTgt_B2B_JackpotData JackpotData
        {
            get
            {
                return this._jackpotData;
            }
            set
            {
                this._jackpotData = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.JackpotSlopProcessing;
            }
        }
    }

    /// <summary>
    /// GMU to Host (or) Host to GMU Ticket Sub Information
    /// </summary>
    public class FFTgt_B2B_JackpotData : FFTgt_B2B { }
}
