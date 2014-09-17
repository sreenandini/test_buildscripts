using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Pin Check Request

    /// <summary>
    /// GMU to Host Freeform for Pin Check Request
    /// </summary>
    public class FFTgt_G2H_EFT_PinCheck_Request
        : FFTgt_G2H_EFT_PlayerAndPinDetails
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.PINCheckRequest;
            }
        }
    }

    #endregion //Pin Check Request

    #region Pin Check Reply

    /// <summary>
    /// Host to GMU Freeform for Pin Check Reply
    /// </summary>
    public class FFTgt_H2G_EFT_PinCheckReply
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.PINCheckReply;
            }
        }

        #region Private Data Members

        private byte _errorCode;

        #endregion //Private Data Members

        #region Properties

        // Error code: 0 - Approved, >0 - Cancel Deposit
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
    }

    #endregion //Pin Check Reply

    #region PIN Change Request

    /// <summary>
    /// GMU to Host Freeform for PIN Change Request
    /// </summary>
    public class FFTgt_G2H_EFT_PIN_ChangeRequest
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.PINChangeRequest;
            }
        }

        #region Private Data Members

        private string _currentPIN;
        private string _newPIN;

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Current PIN number
        public string CurrentPIN
        {
            get
            {
                return this._currentPIN;
            }
            set
            {
                if (this._currentPIN == value) return;
                this._currentPIN = value;
            }
        }

        // New PIN number
        public string NewPIN
        {
            get
            {
                return this._newPIN;
            }
            set
            {
                if (this._newPIN == value) return;
                this._newPIN = value;
            }
        }

        #endregion Properties
    }

    #endregion PIN Change Request

    #region PIN Change Response

    /// <summary>
    /// GMU to Host Freeform for PIN Change Response
    /// </summary>
    public class FFTgt_H2G_EFT_PIN_ChangeResponse
        : FFTgt_B2B_EFT_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.PINChangeResponse;
            }
        }

        #region Private Data Members

        private int _displayMessageLength;
        private string _displayMessage;

        #endregion //Private Data Members

        #region Properties

        public byte ErrorCode { get; set; }

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Display Message Length
        public int DisplayMessageLength
        {
            get
            {
                return this._displayMessageLength;
            }
            set
            {
                if (this._displayMessageLength == value) return;
                this._displayMessageLength = value;
            }
        }

        // Display Message
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

        #endregion Properties
    }

    #endregion PIN Change Response
}
