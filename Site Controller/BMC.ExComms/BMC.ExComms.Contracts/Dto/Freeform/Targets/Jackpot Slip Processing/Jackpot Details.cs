using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Jackpot Details Request

    /// <summary>
    /// GMU to Host Freeform for Jackpot Details Request
    /// </summary>
    public class FFTgt_G2H_JP_Details_Request
        : FFTgt_G2H
    {
        #region Private Data Members

        private int _employeeNumber;
        private int _playerNumber;
        private double _jPAmount;
        private string _payLine;
        private string _winningComb;
        private string _coinsPlayed;
        private FF_JPS_Shift_Data _shift;
        private bool _isCreditMeterHP;
        private bool _isPouchPay;
        private byte _empAuthorization;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Employee Number
        /// </summary>
        public int EmployeeNumber
        {
            get
            {
                return this._employeeNumber;
            }
            set
            {
                if (this._employeeNumber == value) return;
                this._employeeNumber = value;
            }
        }

        /// <summary>
        /// Player Number
        /// </summary>
        public int PlayerNumber
        {
            get
            {
                return this._playerNumber;
            }
            set
            {
                if (this._playerNumber == value) return;
                this._playerNumber = value;
            }
        }

        /// <summary>
        /// _JackPot Amount
        /// </summary>
        public double JPAmount
        {
            get
            {
                return this._jPAmount;
            }
            set
            {
                if (this._jPAmount == value) return;
                this._jPAmount = value;
            }
        }

        /// <summary>
        /// Pay Line
        /// </summary>
        public string PayLine
        {
            get
            {
                return this._payLine;
            }
            set
            {
                if (this._payLine == value) return;
                this._payLine = value;
            }
        }

        /// <summary>
        /// Winning Combination
        /// </summary>
        public string WinningComb
        {
            get
            {
                return this._winningComb;
            }
            set
            {
                if (this._winningComb == value) return;
                this._winningComb = value;
            }
        }

        /// <summary>
        /// Coins Played
        /// </summary>
        public string CoinsPlayed
        {
            get
            {
                return this._coinsPlayed;
            }
            set
            {
                if (this._coinsPlayed == value) return;
                this._coinsPlayed = value;
            }
        }

        /// <summary>
        /// Shift. 1 -> Day, 2 -> Swing, 3 -> Graveyard
        /// </summary>
        public FF_JPS_Shift_Data Shift
        {
            get
            {
                return this._shift;
            }
            set
            {
                if (this._shift == value) return;
                this._shift = value;
            }
        }

        /// <summary>
        /// IS Credit Meter Handpay
        /// </summary>
        public bool IsCreditMeterHP
        {
            get
            {
                return this._isCreditMeterHP;
            }
            set
            {
                if (this._isCreditMeterHP == value) return;
                this._isCreditMeterHP = value;
            }
        }

        /// <summary>
        /// Is Pouch Pay
        /// </summary>
        public bool IsPouchPay
        {
            get
            {
                return this._isPouchPay;
            }
            set
            {
                if (this._isPouchPay == value) return;
                this._isPouchPay = value;
            }
        }

        /// <summary>
        /// Employee Authorization
        /// </summary>
        public byte EmpAuthorization
        {
            get
            {
                return this._empAuthorization;
            }
            set
            {
                if (this._empAuthorization == value) return;
                this._empAuthorization = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Jackpot Details Request

    #region Jackpot Details Response

    /// <summary>
    /// GMU to Host Freeform for Jackpot Details Request
    /// </summary>
    public class FFTgt_H2G_JP_Details_Response
        : FFTgt_H2G
    {
        #region Private Data Members

        private int _textLength;
        private string _text;
        private short _noOfPrinterLocations;
        private byte[] _printerLocations;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Error Message Text Length
        /// </summary>
        public int TextLength
        {
            get
            {
                return this._textLength;
            }
            set
            {
                if (this._textLength == value) return;
                this._textLength = value;
            }
        }

        /// <summary>
        /// Error Message Text
        /// </summary>
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if (this._text.Equals(value)) return;
                this._text = value;
            }
        }

        /// <summary>
        /// Number of printer locations available
        /// </summary>
        public short NoOfPrinterLocations
        {
            get
            {
                return this._noOfPrinterLocations;
            }
            set
            {
                if (this._noOfPrinterLocations == value) return;
                this._noOfPrinterLocations = value;
            }
        }

        /// <summary>
        /// Printer locations
        /// </summary>
        public byte[] PrinterLocations
        {
            get
            {
                return this._printerLocations;
            }
            set
            {
                this._printerLocations = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Jackpot Details Response
}
