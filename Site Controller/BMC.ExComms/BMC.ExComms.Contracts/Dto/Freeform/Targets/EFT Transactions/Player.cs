using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Player Enable

    /// <summary>
    /// Host to GMU freeform for Player Enable
    /// </summary>
    public class FFTgt_H2G_EFT_PlayerEnable
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.PlayerEnableEFT;
            }
        }
    }

    #endregion //Player Enable

    #region Player EFT Characteristics

    /// <summary>
    /// Host to GMU (Or) GMU to Host Player EFT Characteristics
    /// </summary>
    public class FFTgt_B2B_Player_EFT_Char
        : FFTgt_B2B
    {
        #region Private Data Members

        private bool _isEFTTransactionsAllowed;
        private bool _isPINRequired;
        private bool _isQueryAmount;
        private bool _isAllowCashDeposit;
        private bool _isVIP;
        private bool _isAllowNonCashableDeposit;
        private bool _isAllowNonCashableWithdrawal;
        private bool _isAllowPointsWithdrawal;
        private bool _isAllowCashWithdrawal;
        private bool _isAllowOffers;
        private bool _isPINRequiredForWithdrawal;
        private bool _isPINRequiredForDeposit;

        #endregion //Private Data Members

        #region Properties

        //Flag - Is EFT Transactions Allowed
        public bool IsEFTTransactionsAllowed
        {
            get
            {
                return this._isEFTTransactionsAllowed;
            }
            set
            {
                if (this._isEFTTransactionsAllowed == value) return;
                this._isEFTTransactionsAllowed = value;
            }
        }

        //Flag Is PIN Required
        public bool IsPINRequired
        {
            get
            {
                return this._isPINRequired;
            }
            set
            {
                if (this._isPINRequired == value) return;
                this._isPINRequired = value;
            }
        }

        //Flag Is Query Amount
        public bool IsQueryAmount
        {
            get
            {
                return this._isQueryAmount;
            }
            set
            {
                if (this._isQueryAmount == value) return;
                this._isQueryAmount = value;
            }
        }

        //Flag Is Allow Cash Deposit
        public bool IsAllowCashDeposit
        {
            get
            {
                return this._isAllowCashDeposit;
            }
            set
            {
                if (this._isAllowCashDeposit == value) return;
                this._isAllowCashDeposit = value;
            }
        }

        //Flag Is VIP
        public bool IsVIP
        {
            get
            {
                return this._isVIP;
            }
            set
            {
                if (this._isVIP == value) return;
                this._isVIP = value;
            }
        }

        // Flag - Is Allow Non Cashable Deposit
        public bool IsAllowNonCashableDeposit
        {
            get
            {
                return this._isAllowNonCashableDeposit;
            }
            set
            {
                if (this._isAllowNonCashableDeposit == value) return;
                this._isAllowNonCashableDeposit = value;
            }
        }

        // Flag - Is Allow Non Cashable Withdrawal
        public bool IsAllowNonCashableWithdrawal
        {
            get
            {
                return this._isAllowNonCashableWithdrawal;
            }
            set
            {
                if (this._isAllowNonCashableWithdrawal == value) return;
                this._isAllowNonCashableWithdrawal = value;
            }
        }

        // Flag - Is Allow Points Withdrawal
        public bool IsAllowPointsWithdrawal
        {
            get
            {
                return this._isAllowPointsWithdrawal;
            }
            set
            {
                if (this._isAllowPointsWithdrawal == value) return;
                this._isAllowPointsWithdrawal = value;
            }
        }

        // Flag - Is Allow Cash Withdrawal
        public bool IsAllowCashWithdrawal
        {
            get
            {
                return this._isAllowCashWithdrawal;
            }
            set
            {
                if (this._isAllowCashWithdrawal == value) return;
                this._isAllowCashWithdrawal = value;
            }
        }

        // Flag - Is Allow Offers
        public bool IsAllowOffers
        {
            get
            {
                return this._isAllowOffers;
            }
            set
            {
                if (this._isAllowOffers == value) return;
                this._isAllowOffers = value;
            }
        }

        // Flag - Is PIN Required For Withdrawal
        public bool IsPINRequiredForWithdrawal
        {
            get
            {
                return this._isPINRequiredForWithdrawal;
            }
            set
            {
                if (this._isPINRequiredForWithdrawal == value) return;
                this._isPINRequiredForWithdrawal = value;
            }
        }

        // Flag - Is PIN Required ForDeposit
        public bool IsPINRequiredForDeposit
        {
            get
            {
                return this._isPINRequiredForDeposit;
            }
            set
            {
                if (this._isPINRequiredForDeposit == value) return;
                this._isPINRequiredForDeposit = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Player EFT Characteristics
}
