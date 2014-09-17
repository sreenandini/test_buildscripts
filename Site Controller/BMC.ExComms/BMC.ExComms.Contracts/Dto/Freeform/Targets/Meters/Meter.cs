using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_AccountingMeter
       : FFTgt_G2H
    {
        public FFTgt_G2H_AccountingMeter() { }

        public FFTgt_G2H_AccountingMeter(FF_AppId_AccountingMeterIds meterId, double value)
        {
            _meterId = meterId;
            _value = value;
        }

        #region Memebers
        private FF_AppId_AccountingMeterIds _meterId = FF_AppId_AccountingMeterIds.None;

        /// <summary>
        /// AccountMeterData From GMU TO HOST.It contains Various Meters like Coin,Bet etc..with Tag,TargetLenghth and Data properties
        /// </summary>
        private double _value;

        #endregion

        #region Properties

        public FF_AppId_AccountingMeterIds MeterId
        {
            get { return _meterId; }
            set
            {
                if (_meterId != value)
                {
                    _meterId = value;
                }
            }
        }

        public double Value
        {
            get
            {
                return this._value;
            }
            set
            {
                if (this._value == value) return;
                this._value = value;
            }
        }

        public override bool IsLeafNode
        {
            get { return true; }
        }

        #endregion
    }
}
