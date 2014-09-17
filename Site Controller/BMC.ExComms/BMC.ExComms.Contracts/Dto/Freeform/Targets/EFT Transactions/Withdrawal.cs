using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Withdrawal Request

    /// <summary>
    /// GMU to Host Freeform for Withdrawal Request
    /// </summary>
    public class FFTgt_G2H_EFT_WithdrawalRequest
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        #region Private Data Members

        private FF_AppId_EFT_AccountTypes _accountType;
        private double _amountRequested;
        private string _pin;

        #endregion //Private Data Members

        #region Properties

        // Account Types 1 - Promo, 2 - Points, 3 - Cash, 4 - Offers, 5 - All
        public FF_AppId_EFT_AccountTypes AccountType
        {
            get
            {
                return this._accountType;
            }
            set
            {
                if (this._accountType == value) return;
                this._accountType = value;
            }
        }

        // Amount Requested for Withdrawal
        public double AmountRequested
        {
            get
            {
                return this._amountRequested;
            }
            set
            {
                if (this._amountRequested == value) return;
                this._amountRequested = value;
            }
        }

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Player Pin number
        public string Pin
        {
            get
            {
                return this._pin;
            }
            set
            {
                if (this._pin == value) return;
                this._pin = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.WithdrawalRequest;
            }
        }
    }

    #endregion //Withdrawal Request

    #region Withdrawal Authorization

    /// <summary>
    /// Host to GMU Freeform for Withdrawal Authorization
    /// </summary>
    public class FFTgt_H2G_EFT_WithdrawalAuthorization
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        #region Private Data Members

        private double _nonCashableAmount;
        private double _cashableAmount;
        private byte _errorCode;

        #endregion //Private Data Members

        #region Properties

        // Non_Cashable value
        public double NonCashableAmount
        {
            get
            {
                return this._nonCashableAmount;
            }
            set
            {
                if (this._nonCashableAmount == value) return;
                this._nonCashableAmount = value;
            }
        }

        // Cashable value
        public double CashableAmount
        {
            get
            {
                return this._cashableAmount;
            }
            set
            {
                if (this._cashableAmount == value) return;
                this._cashableAmount = value;
            }
        }

        // Error code, if any
        public byte ErrorCode
        {
            get
            {
                return this._errorCode;
            }
            set
            {
                if (this._errorCode == value) return;
                this._errorCode = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.WithdrawalAuthorization;
            }
        }
    }

    #endregion //Withdrawal Authorization

    #region Withdrawal Complete

    /// <summary>
    /// GMU to Host Freeform for Withdrawal Complete
    /// </summary>
    public class FFTgt_G2H_EFT_WithdrawalComplete
        : FFTgt_B2B_EFT_AmountDetails_CardNo_Error, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.WithdrawalComplete;
            }
        }
    }

    #endregion //Withdrawal Complete

    #region Withdrawal Acknowledgement

    /// <summary>
    /// Host to GMU Freeform for Withdrawal Acknowledgement
    /// </summary>
    public class FFTgt_H2G_EFT_WithdrawalAcknowledgement
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.WithdrawalAcknowledgement;
            }
        }
    }

    #endregion //Withdrawal Acknowledgement

    #region Withdrawal Authorization 2

    /// <summary>
    /// Host to GMU Freeform for Withdrawal Authorization 2
    /// </summary>
    public class FFTgt_H2G_EFT_WithdrawalAuthorization2
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        #region Private Data Members

        private FF_AppId_EFT_AccountTypes _accountType;
        private double _nonCashableAmount;
        private double _cashableAmount;
        private double _nonCashableBalanceAmount;
        private double _cashableBalanceAmount;
        private byte _errorCode;

        #endregion //Private Data Members

        #region Properties

        // Account Types 4 - Offers
        public FF_AppId_EFT_AccountTypes AccountType
        {
            get
            {
                return this._accountType;
            }
            set
            {
                if (this._accountType == value) return;
                this._accountType = value;
            }
        }

        // Non_Cashable value
        public double NonCashableAmount
        {
            get
            {
                return this._nonCashableAmount;
            }
            set
            {
                if (this._nonCashableAmount == value) return;
                this._nonCashableAmount = value;
            }
        }

        // Cashable value
        public double CashableAmount
        {
            get
            {
                return this._cashableAmount;
            }
            set
            {
                if (this._cashableAmount == value) return;
                this._cashableAmount = value;
            }
        }

        // Non_Cashable Balance value
        public double NonCashableBalanceAmount
        {
            get
            {
                return this._nonCashableBalanceAmount;
            }
            set
            {
                if (this._nonCashableBalanceAmount == value) return;
                this._nonCashableBalanceAmount = value;
            }
        }

        // Cashable Balance value
        public double CashableBalanceAmount
        {
            get
            {
                return this._cashableBalanceAmount;
            }
            set
            {
                if (this._cashableBalanceAmount == value) return;
                this._cashableBalanceAmount = value;
            }
        }

        // Error code, if any
        public byte ErrorCode
        {
            get
            {
                return this._errorCode;
            }
            set
            {
                if (this._errorCode == value) return;
                this._errorCode = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.WithdrawalAuthorization2;
            }
        }
    }

    #endregion //Withdrawal Authorization
}
