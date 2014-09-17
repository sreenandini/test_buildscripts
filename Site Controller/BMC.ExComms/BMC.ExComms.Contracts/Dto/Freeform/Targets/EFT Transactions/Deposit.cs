using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Deposit Request

    /// <summary>
    /// GMU to Host Freeform for Deposit Request
    /// </summary>
    public class FFTgt_G2H_EFT_DepositRequest
        : FFTgt_B2B_EFT_AmountDetails_CardNo, IFFTgt_G2H
    {
        #region Private Data Members

        private string _pin;

        #endregion //Private Data Members

        #region Properties

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
                return (int)FF_AppId_EFT_TransactionTypes.DepositRequest;
            }
        }
    }

    #endregion //Deposit Request

    #region Deposit Authorization

    /// <summary>
    /// Host to GMU Freeform for Deposit  Authorization
    /// </summary>
    public class FFTgt_H2G_EFT_DepositAuthorization
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
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

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.DepositAuthorization;
            }
        }
    }

    #endregion //Deposit Authorization

    #region Deposit Complete

    /// <summary>
    /// GMU to Host Freeform for Deposit Complete
    /// </summary>
    public class FFTgt_G2H_EFT_DepositComplete
        : FFTgt_B2B_EFT_AmountDetails_CardNo_Error, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.DepositComplete;
            }
        }
    }

    #endregion //Deposit Complete

    #region Deposit Acknowledgement

    /// <summary>
    /// Host to GMU Freeform for Deposit Acknowledgement
    /// </summary>
    public class FFTgt_H2G_EFT_DepositAcknowledgement
        : FFTgt_B2B_EFT_PlayerAndMessageInfo, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.DepositAcknowledgement;
            }
        }
    }

    #endregion //Deposit Acknowledgement
}
