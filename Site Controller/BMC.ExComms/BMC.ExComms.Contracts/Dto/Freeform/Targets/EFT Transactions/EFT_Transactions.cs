using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region EFT Transactions Main

    /// <summary>
    /// Base class - GMU to Host (or) Host to GMU
    /// </summary>
    public class FFTgt_B2B_EFT : FFTgt_B2B
    {
        public FFTgt_B2B_EFT_Data EFTData
        {
            get { return this.GetPrimaryTarget<FFTgt_B2B_EFT_Data>(); }
            set { this.SetPrimaryTarget<FFTgt_B2B_EFT_Data>(value); }
        }

        public override FF_AppId_Encryption_Types EncryptionType
        {
            get
            {
                return FF_AppId_Encryption_Types.AuthByteEncryptedData;
            }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.ECash;
            }
        }
    }

    #endregion //EFT Transactions Main

    #region EFT Transactions Sub

    /// <summary>
    /// GMU to Host (or) Host to GMU EFT transaction Sub Information
    /// </summary>
    public class FFTgt_B2B_EFT_Data
        : FFTgt_B2B, IFreeformEntity_MsgTgt_Primary
    {
        private string _cardNumber;
        protected readonly Lazy<ExCommsPlayerFlags> _playerFlags2 = new Lazy<ExCommsPlayerFlags>(() => { return new ExCommsPlayerFlags(); });

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.ECash;
            }
        }

        public string CardNumber
        {
            get
            {
                return this._cardNumber;
            }
            set
            {
                if (this._cardNumber == value) return;
                this._cardNumber = value;
            }
        }

        public ExCommsPlayerFlags PlayerFlagsEx
        {
            get { return _playerFlags2.Value; }
        }
    }

    public class FFTgt_B2B_EFT_Data_NoEncryption
        : FFTgt_B2B_EFT_Data
    {
        public override FF_AppId_Encryption_Types EncryptionType
        {
            get
            {
                return FF_AppId_Encryption_Types.None;
            }
            set
            {
                base.EncryptionType = value;
            }
        }
    }

    #endregion //EFT Transactions Sub

    #region Player and Message Details

    /// <summary>
    /// GMU to Host (or) Host to GMU Freefrom for Player and Message Details
    /// </summary>
    public class FFTgt_B2B_EFT_PlayerAndMessageInfo
        : FFTgt_B2B_EFT_Data, IFFTgt_B2B
    {

        #region Private Data Members

        private byte[] _playerFlags;
        private byte _displayMessageLength;
        private string _displayMessage;

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Player Flags
        public ExCommsPlayerFlags PlayerFlags
        {
            get
            {
                return this.PlayerFlagsEx;
            }
        }

        // Display message length
        public byte DisplayMessageLength
        {
            get
            {
                return this._displayMessageLength;
            }
            set
            {
                if (this._displayMessageLength == value)
                    this._displayMessageLength = value;
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

    #endregion //Player and PIN Details

    #region Player and Message Details

    /// <summary>
    /// GMU to Host (or) Host to GMU Freefrom for Player and Message Details
    /// </summary>
    public class FFTgt_G2H_EFT_PlayerAndPinDetails
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {

        #region Private Data Members

        private string _pin;

        #endregion //Private Data Members

        #region Properties

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
    }

    #endregion //Player and PIN Details

    #region Amount Details
    public class FFTgt_B2B_EFT_AmountDetails
        : FFTgt_B2B_EFT_Data, IFFTgt_B2B
    {
        #region Private Data Members

        private double _nonCashableAmount;
        private double _cashableAmount;

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

        #endregion //Properties
    }
    #endregion

    #region Amount Detail, Error
    public class FFTgt_B2B_EFT_AmountDetails_Error
        : FFTgt_B2B_EFT_AmountDetails, IFFTgt_B2B
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
    }
    #endregion

    #region Amount Details, Card Number
    public class FFTgt_B2B_EFT_AmountDetails_CardNo
    : FFTgt_B2B_EFT_AmountDetails, IFFTgt_B2B
    {
        #region Private Data Members

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        #endregion //Properties
    }
    #endregion

    #region Amount Details, Card Number, Error
    public class FFTgt_B2B_EFT_AmountDetails_CardNo_Error
    : FFTgt_B2B_EFT_AmountDetails_Error, IFFTgt_B2B
    {
        #region Private Data Members

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        #endregion //Properties
    }
    #endregion
}
