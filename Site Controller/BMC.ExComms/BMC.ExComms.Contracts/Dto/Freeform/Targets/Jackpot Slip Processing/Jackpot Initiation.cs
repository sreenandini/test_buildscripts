using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Jackpot Initiation
    
    /// <summary>
    /// GMU to Host Freeform for Jackpot Initiation
    /// </summary>
    public class FFTgt_G2H_Jackpot_Init
    {
        #region Private Data Members

        private int _employeeID;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Employee ID
        /// </summary>
        public int EmployeeID
        {
            get
            {
                return this._employeeID;
            }
            set
            {
                if (this._employeeID == value) return;
                this._employeeID = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Jackpot Initiation

    #region Jackpot Initiation Response

    /// <summary>
    /// Host to GMU Freeform for Jackpot Initiation Response
    /// </summary>
    public class FFTgt_H2G_Jackpot_Init_Resp
    {
        #region Private Data Members

        private int _processflag;
        private FF_JPS_Prompt _jPAmt;
        private FF_JPS_PayLine _payLine;
        private FF_JPS_WinningComb _winningComb;
        private FF_JPS_CoinsPlay _coinsPlayed;
        private FF_JPS_Shift _shift;
        private bool _creditMeterHP;
        private FF_JPS_PouchPay _pouchPay;
        private FF_JPS_Emp_Auth _empAuthorization;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Process flag
        /// </summary>
        public int Processflag
        {
            get
            {
                return this._processflag;
            }
            set
            {
                if (this._processflag == value) return;
                this._processflag = value;
            }
        }

        /// <summary>
        /// Prompt for Jackpot Amount
        /// </summary>
        public FF_JPS_Prompt JPAmt
        {
            get
            {
                return this._jPAmt;
            }
            set
            {
                if (this._jPAmt == value) return;
                this._jPAmt = value;
            }
        }

        /// <summary>
        /// Pay Line
        /// </summary>
        public FF_JPS_PayLine PayLine
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
        public FF_JPS_WinningComb WinningComb
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
        /// CoinsPlayed
        /// </summary>
        public FF_JPS_CoinsPlay CoinsPlayed
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
        /// Shift
        /// </summary>
        public FF_JPS_Shift Shift
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
        /// Credit meter hand pay
        /// </summary>
        public bool CreditMeterHP
        {
            get
            {
                return this._creditMeterHP;
            }
            set
            {
                if (this._creditMeterHP == value) return;
                this._creditMeterHP = value;
            }
        }

        /// <summary>
        /// Pouch Pay
        /// </summary>
        public FF_JPS_PouchPay PouchPay
        {
            get
            {
                return this._pouchPay;
            }
            set
            {
                if (this._pouchPay == value) return;
                this._pouchPay = value;
            }
        }

        /// <summary>
        /// Employee Authorization
        /// </summary>
        public FF_JPS_Emp_Auth EmpAuthorization
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

    #endregion //Jackpot Initiation Response
}
