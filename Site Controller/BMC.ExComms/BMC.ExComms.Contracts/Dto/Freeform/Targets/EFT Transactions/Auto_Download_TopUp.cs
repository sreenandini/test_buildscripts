using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Auto-Download Enable/Set/Change Amount Request
    /// <summary>
    /// GMU to Host Freeform for Auto-Download Enable/Set/Change Amount Request
    /// </summary>
    public class FFTgt_G2H_AD_Amount_Request
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        #region Private Data Members

        private FF_AppId_EFT_AutoDownload_Status _status;
        private FF_AppId_EFT_AutoDownload_TopUp_AccountTypes _accountType;
        private double _autoDownloadAmount;

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Status Flag : 0x01 -> AutoDownload Enabled, 0x02 -> Require PIN on AutoDownload
        public FF_AppId_EFT_AutoDownload_Status Status
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

        // Auto Download Account Type
        // 1 -> Promotional Offer, 2 -> Points Redemption, 3 -> Player Cash, 4 -> Offers, 5 -> All
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AccountType
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

        #endregion //Properties
    }

    #endregion //Auto-Download Enable/Set/Change Amount Request

    #region Auto-Download Enable/Set/Change Amount Response

    /// <summary>
    /// Host to GMU Freeform for Auto-Download Enable/Set/Change Amount Response
    /// </summary>
    public class FFTgt_H2G_AD_Amount_Response
        : FFTgt_B2B_EFT_Data, IFFTgt_H2G
    {
        #region Private Data Members

        private byte _errorCode;
        private int _dispMsgLength;
        private string _displayMessage;

        #endregion Private Data Members

        #region Properties

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

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Display message length
        public int DispMsgLength
        {
            get
            {
                return this._dispMsgLength;
            }
            set
            {
                if (this._dispMsgLength == value)
                    this._dispMsgLength = value;
            }
        }

        // Display message
        public string DisplayMessage
        {
            get
            {
                return this._displayMessage;
            }
            set
            {
                if (this._displayMessage == value) return;
                this._displayMessage = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Auto-Download Enable/Set/Change Amount Response

    #region Auto-TopUp Enable/Set/Change Amount Request

    /// <summary>
    /// GMU to Host Freeform for Auto-TopUp Enable/Set/Change Amount Request
    /// </summary>
    public class FFTgt_G2H_ATU_AmountRequest
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        #region Private Data Members

        private string _playerCardNumber;
        private FF_AppId_EFT_AutoTopUp_Status _status;
        private FF_AppId_EFT_AutoDownload_TopUp_AccountTypes _accountType;
        private double _autoDownloadMaxAmount;
        private double _autoTopUpTrigger;

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Status Flag : 0x10 -> AutoTopUp Enabled, 0x20 -> Require PIN on AutoTopUp
        public FF_AppId_EFT_AutoTopUp_Status Status
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

        // Auto Download Account Type
        // 1 -> Promotional Offer, 2 -> Points Redemption, 3 -> Player Cash, 4 -> Offers, 5 -> All
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AccountType
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

        // Auto Download Amount
        public double AutoDownloadMaxAmount
        {
            get
            {
                return this._autoDownloadMaxAmount;
            }
            set
            {
                if (this._autoDownloadMaxAmount == value) return;
                this._autoDownloadMaxAmount = value;
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

        #endregion //Properties
    }

    #endregion //Auto-TopUp Enable/Set/Change Amount Request

    #region Auto-TopUp Enable/Set/Change Amount Response

    /// <summary>
    /// Host to GMU Freeform for Auto-TopUp Enable/Set/Change Amount Response
    /// </summary>
    public class FFTgt_H2G_ATU_Amount_Response
        : FFTgt_B2B_EFT_Data, IFFTgt_H2G
    {
        #region Private Data Members

        private byte _errorCode;
        private int _dispMsgLength;
        private string _displayMessage;

        #endregion Private Data Members

        #region Properties

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

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Display message length
        public int DispMsgLength
        {
            get
            {
                return this._dispMsgLength;
            }
            set
            {
                if (this._dispMsgLength == value)
                    this._dispMsgLength = value;
            }
        }

        // Display message
        public string DisplayMessage
        {
            get
            {
                return this._displayMessage;
            }
            set
            {
                if (this._displayMessage == value) return;
                this._displayMessage = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Auto-Download Enable/Set/Change Amount Response
}
