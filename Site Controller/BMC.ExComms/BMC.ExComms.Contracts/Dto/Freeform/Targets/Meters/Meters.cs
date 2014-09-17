using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_AccountingMeters
        : FFTgt_G2H
    {
        public FFTgt_G2H_AccountingMeters()
        {
            this.Meters = new List<FFTgt_G2H_AccountingMeter>();
        }

        #region Members
        /// <summary>
        /// Get Source From Enum-Collection 1.GMU 2.Game
        /// </summary>
        private FF_AppId_AccountingMetersSourceIds _sourceId;

        #endregion

        #region Properties
        public FF_AppId_AccountingMetersSourceIds SourceId
        {
            get
            {
                return this._sourceId;
            }
            set
            {
                if (this._sourceId == value) return;
                this._sourceId = value;
            }
        }

        /// <summary>
        /// Returns Meter Data From AccountMeterData Class.It contains Various Meters like Coin,Bet etc..with Tag,TargetLenghth and Data properties
        /// </summary>
        public List<FFTgt_G2H_AccountingMeter> Meters { get; private set; }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.MeterBlock;
            }
        }
    }
}
