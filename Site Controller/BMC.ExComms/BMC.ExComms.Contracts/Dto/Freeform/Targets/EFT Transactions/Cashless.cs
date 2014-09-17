using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{

    #region Asset and Account Details

    /// <summary>
    /// GMU to Host (Or) Host to GMU Freeform for Asset and Account Details
    /// </summary>
    public class FFTgt_B2B_EFT_AssetandAccountDetails
        : FFTgt_B2B_EFT_Data, IFFTgt_B2B
    {
        #region Private Data Members

        private int _eGMAssetNumber;
        private byte _cashlessAccountNumberLength;
        private string _cashlessAccountNumber;

        #endregion //Private Data Members

        #region Properties

        // Game Asset Number 
        public int EGMAssetNumber
        {
            get
            {
                return this._eGMAssetNumber;
            }
            set
            {
                if (this._eGMAssetNumber == value) return;
                this._eGMAssetNumber = value;
            }
        }

        // Cashless Account Number Length
        public byte CashlessAccountNumberLength
        {
            get
            {
                return this._cashlessAccountNumberLength;
            }
            set
            {
                if (this._cashlessAccountNumberLength == value) return;
                this._cashlessAccountNumberLength = value;
            }
        }

        // Cashless Account Number
        public string CashlessAccountNumber
        {
            get
            {
                return _cashlessAccountNumber;
            }
            set
            {
                if (this._cashlessAccountNumber == value) return;
                this._cashlessAccountNumber = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Asset and Account Details

    #region Cashless Account Lookup

    /// <summary>
    /// GMU to Host Freeform for Casheless/Smart Card Account Lookup 
    /// </summary>
    public class FFTgt_G2H_EFT_CashlessAccountLookup
        : FFTgt_B2B_EFT_AssetandAccountDetails, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.CashlessAccountLookup;
            }
        }
    }

    #endregion //Cashless Account Lookup

    #region Cashless/Smart Card Account Verify

    /// <summary>
    /// Host to GMU Freeform for Cashless/Smart Card Account Verify 
    /// </summary>
    public class FFTgt_H2G_EFT_CashlessAccVerify
        : FFTgt_B2B_EFT_AssetandAccountDetails, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.CashlessAccountVerify;
            }
        }

        #region Private Data Members

        private byte _responseStatus;
        private string _playerAccountNumber;
        private byte _pINCheck;
        private FF_AppId_EFT_AutoDownload_TopUp_StatusFlags _autoDownloadTopUPStatusFlag;
        private FF_AppId_EFT_AutoDownload_TopUp_AccountTypes _autoDownloadAccountType;
        private double _autoDownloadAmount;
        private FF_AppId_EFT_AutoDownload_TopUp_AccountTypes _autoTopUpAccountType;
        private double _autoTopUpMaxAmount;
        private double _autoTopUpTrigger;
        private byte _errorTextLength;
        private string _errorText;

        #endregion //Private Data Members

        #region Properties

        // 0x00 –> Valid Player ID / Non-Zero -> Error
        public byte ResponseStatus
        {
            get
            {
                return this._responseStatus;
            }
            set
            {
                if (this._responseStatus == value) return;
                this._responseStatus = value;
            }
        }

        // Player Accoun tNumber
        public string PlayerAccountNumber
        {
            get
            {
                return this._playerAccountNumber;
            }
            set
            {
                if (this._playerAccountNumber == value) return;
                this._playerAccountNumber = value;
            }
        }

        // 0 -> PIN required?, 1 -> Allow PIN change?, 2 -> Undefined
        public byte PINCheck
        {
            get
            {
                return this._pINCheck;
            }
            set
            {
                if (this._pINCheck == value) return;
                this._pINCheck = value;
            }
        }

        //Auto Download TopUP Status Flag
        // 0x01 -> AutoDownload Enabled, 0x02 -> PIN Required on AutoDownload, 0x10 -> Auto TopUp Enabled, 0x20 -> PIN required on TopUp
        public FF_AppId_EFT_AutoDownload_TopUp_StatusFlags AutoDownloadTopUPStatusFlag
        {
            get
            {
                return this._autoDownloadTopUPStatusFlag;
            }
            set
            {
                if (this._autoDownloadTopUPStatusFlag == value) return;
                this._autoDownloadTopUPStatusFlag = value;
            }
        }

        // Auto Download Account Type
        // 1 -> Promotional Offer, 2 -> Points Redemption, 3 -> Player Cash, 4 -> Offers, 5 -> All
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AutoDownloadAccountType
        {
            get
            {
                return this._autoDownloadAccountType;
            }
            set
            {
                if (this._autoDownloadAccountType == value) return;
                this._autoDownloadAccountType = value;
            }
        }

        // Auto Download Amount
        public double AutoDownloadAmount
        {
            get
            {
                return this._autoDownloadAmount;
            }
            set
            {
                if (this._autoDownloadAmount == value) return;
                this._autoDownloadAmount = value;
            }
        }

        // Auto TopUp Account Type
        // 1 -> Promotional Offer, 2 -> Points Redemption, 3 -> Player Cash, 4 -> Offers, 5 -> All
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AutoTopUpAccountType
        {
            get
            {
                return this._autoTopUpAccountType;
            }
            set
            {
                if (this._autoTopUpAccountType == value) return;
                this._autoTopUpAccountType = value;
            }
        }

        // Auto TopUp Max Amount
        public double AutoTopUpMaxAmount
        {
            get
            {
                return this._autoTopUpMaxAmount;
            }
            set
            {
                if (this._autoTopUpMaxAmount == value) return;
                this._autoTopUpMaxAmount = value;
            }
        }

        // Auto TopUp Trigger
        public double AutoTopUpTrigger
        {
            get
            {
                return this._autoTopUpTrigger;
            }
            set
            {
                if (this._autoTopUpTrigger == value) return;
                this._autoTopUpTrigger = value;
            }
        }

        // Error Text Max Length
        public byte ErrorTextLength
        {
            get
            {
                return this._errorTextLength;
            }
            set
            {
                if (this._errorTextLength == value) return;
                this._errorTextLength = value;
            }
        }

        // Error Text
        public string ErrorText
        {
            get
            {
                return this._errorText;
            }
            set
            {
                if (this._errorText == value) return;
                this._errorText = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Cashless/Smart Card Account Verify

    #region Cashless Account NUmber

    /// <summary>
    /// Host to GMU Freeform for Cashless Account Number
    /// </summary>
    public class FFTgt_H2G_EFT_CashlessAccountNumber
        : FFTgt_B2B_EFT_AssetandAccountDetails, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.CashlessAccountAccountNumber;
            }
        }
    }

    #endregion //Cashless Account NUmber

    #region Employee Account NUmber

    /// <summary>
    /// Host to GMU Freeform for Employee Account NUmber
    /// </summary>
    public class FFTgt_H2G_EFT_EmployeeAccountNumber
        : FFTgt_B2B_EFT_AssetandAccountDetails, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.iButtonAccountNumber;
            }
        }
    }

    #endregion //Cashless Account NUmber

    #region Smartcard Verify Balance System

    /// <summary>
    /// Host to GMU Freeform for Smartcard Verify Balance System
    /// </summary>
    public class FFTgt_B2B_EFT_SC_VerifyBalance
        : FFTgt_B2B_EFT_Data, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SmartcardVerifyBalance;
            }
        }

        #region Private Data Member

        private int _eGMAssetNumber;
        private FF_AppId_EFT_BalanceVerify_Status _status;
        private string _playerAccountNumber;
        private double _balance1;
        private double _balance2;
        private double _balance3;
        private double _balance4;

        #endregion //Private Data Member

        #region Properties

        // Game Asset Number 
        public int EGMAssetNumber
        {
            get
            {
                return this._eGMAssetNumber;
            }
            set
            {
                if (this._eGMAssetNumber == value) return;
                this._eGMAssetNumber = value;
            }
        }

        // Balance Verify Status
        public FF_AppId_EFT_BalanceVerify_Status Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                this._status = value;
            }
        }

        // Player Accoun tNumber
        public string PlayerAccountNumber
        {
            get
            {
                return this._playerAccountNumber;
            }
            set
            {
                if (this._playerAccountNumber == value) return;
                this._playerAccountNumber = value;
            }
        }

        // Restricted Promotional
        public double Balance1
        {
            get
            {
                return this._balance1;
            }
            set
            {
                if (this._balance1 == value) return;
                this._balance1 = value;
            }
        }

        // Non-Restricted Promotional
        public double Balance2
        {
            get
            {
                return this._balance2;
            }
            set
            {
                if (this._balance2 == value) return;
                this._balance2 = value;
            }
        }

        // Cashable
        public double Balance3
        {
            get
            {
                return this._balance3;
            }
            set
            {
                if (this._balance3 == value) return;
                this._balance3 = value;
            }
        }

        // WAT
        public double Balance4
        {
            get
            {
                return this._balance4;
            }
            set
            {
                if (this._balance4 == value) return;
                this._balance4 = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Smartcard Verify Balance System

    #region Smart-Card Transaction Update

    /// <summary>
    /// GMU 2 Host Freeform for Smart-Card Transaction Update
    /// </summary>
    public class FFTgt_G2H_SC_Transaction_Update
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SmartcardTransactionUpdate;
            }
        }

        #region Private Data Member

        private int _eGMAssetNumber;
        private FF_AppId_EFT_SC_Tranaction_Update_Status _status;
        private FF_AppId_EFT_SC_Tranaction_Update_AccTypes _accountType;
        private double _transactionAmount;
        private TimeSpan _transTimestamp;
        private byte _transactionID;

        #endregion //Private Data Member

        #region Properties

        // Game Asset Number 
        public int EGMAssetNumber
        {
            get
            {
                return this._eGMAssetNumber;
            }
            set
            {
                if (this._eGMAssetNumber == value) return;
                this._eGMAssetNumber = value;
            }
        }

        // Smart-Card Transaction Update Status
        // 0 - NotProcessed, 1 - Deposit, 2 - Withdrawal, 3 - Processed
        public FF_AppId_EFT_SC_Tranaction_Update_Status Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                this._status = value;
            }
        }

        // Smart-Card Transaction Update Account Type
        // 1 - RestrictedPromotional, 2 - Non_Restrictedpromotional, 3 - PlayerCash, 4 - WAT
        public FF_AppId_EFT_SC_Tranaction_Update_AccTypes AccountType
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

        // Transaction Amount in cents
        public double TransactionAmount
        {
            get
            {
                return this._transactionAmount;
            }
            set
            {
                if (this._transactionAmount == value) return;
                this._transactionAmount = value;
            }
        }

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Transaction Timestamp - UTC
        public TimeSpan TransTimestamp
        {
            get
            {
                return this._transTimestamp;
            }
            set
            {
                if (this._transTimestamp == value) return;
                this._transTimestamp = value;
            }
        }

        // Transaction ID
        public byte TransactionID
        {
            get
            {
                return this._transactionID;
            }
            set
            {
                if (this._transactionID == value) return;
                this._transactionID = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Smart-Card Transaction Update

    #region SmartCard Serail Number

    /// <summary>
    /// Host to GMU (Or) GMU to Host Freeform for SmartCard Serail Number
    /// </summary>
    public class FFTgt_B2B_EFT_SC_SerailNumber
        : FFTgt_B2B_EFT_AssetandAccountDetails, IFreeformEntity_MsgTgt
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SmartcardSerialNumber;
            }
        }
    }

    #endregion //Cashless Account NUmber

    /// <summary>
    /// Host to GMU Freeform for iButton Account Number Response
    /// </summary>
    public class FFTgt_H2G_EFT_EmployeeAccountNumber_Response
        : FFTgt_B2B_EFT_AssetandAccountDetails, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.iButtonAccountNumberResponse;
            }
        }

        #region Private Data Member

        private byte _responseStatus;
        private FF_AppId_EFT_PINCheck _pINCheck;
        private byte _errorTextLength;
        private string _errorText;

        #endregion //Private Data Member

        #region Properties

        // Response Status
        public byte ResponseStatus
        {
            get
            {
                return this._responseStatus;
            }
            set
            {
                if (this._responseStatus == value) return;
                this._responseStatus = value;
            }
        }

        // PIN Check: 0 - PIN Not Required, 1 - PIN Required
        public FF_AppId_EFT_PINCheck PINCheck
        {
            get
            {
                return this._pINCheck;
            }
            set
            {
                if (this._pINCheck == value) return;
                this._pINCheck = value;
            }
        }

        // Error Text Max Length
        public byte ErrorTextLength
        {
            get
            {
                return this._errorTextLength;
            }
            set
            {
                if (this._errorTextLength == value) return;
                this._errorTextLength = value;
            }
        }

        // Error Text
        public string ErrorText
        {
            get
            {
                return this._errorText;
            }
            set
            {
                if (this._errorText == value) return;
                this._errorText = value;
            }
        }

        #endregion //Properties
    }

}
