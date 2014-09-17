using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Offer List request
    /// <summary>
    /// GMU to Host Freeform for Offer List request
    /// </summary>
    public class FFTgt_G2H_EFT_OfferListRequest
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.OfferListRequest;
            }
        }

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

    #endregion //Offer List request

    #region Offer List

    /// <summary>
    /// GMU to Host Freeform for Offer List
    /// </summary>
    public class FFTgt_H2G_EFT_OfferList
        : FFTgt_B2B_EFT_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.OfferListReply;
            }
        }

        #region Private Data Members

        private string _pin;
        private double _nonCashableBalance;
        private double _cashableBalance;
        private double _offerId1;
        private double _offerId2;
        private double _offerId3;
        private double _offerId4;
        private double _offerId5;
        private double _offerId6;
        private double _offerId7;
        private double _offerId8;
        private double _offerId9;
        private double _offerId10;

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

        // Non_Cashable Balance value
        public double NonCashableBalance
        {
            get
            {
                return this._nonCashableBalance;
            }
            set
            {
                if (this._nonCashableBalance == value) return;
                this._nonCashableBalance = value;
            }
        }

        // Cashable Balance value
        public double CashableBalance
        {
            get
            {
                return this._cashableBalance;
            }
            set
            {
                if (this._cashableBalance == value) return;
                this._cashableBalance = value;
            }
        }

        // Offer Id 1
        public double OfferId1
        {
            get
            {
                return this._offerId1;
            }
            set
            {
                if (this._offerId1 == value) return;
                this._offerId1 = value;
            }
        }

        // Offer Id 2
        public double OfferId2
        {
            get
            {
                return this._offerId2;
            }
            set
            {
                if (this._offerId2 == value) return;
                this._offerId2 = value;
            }
        }

        // Offer Id 3
        public double OfferId3
        {
            get
            {
                return this._offerId3;
            }
            set
            {
                if (this._offerId3 == value) return;
                this._offerId3 = value;
            }
        }

        // Offer Id 4
        public double OfferId4
        {
            get
            {
                return this._offerId4;
            }
            set
            {
                if (this._offerId4 == value) return;
                this._offerId4 = value;
            }
        }

        // Offer Id 5
        public double OfferId5
        {
            get
            {
                return this._offerId5;
            }
            set
            {
                if (this._offerId5 == value) return;
                this._offerId5 = value;
            }
        }

        // Offer Id 6
        public double OfferId6
        {
            get
            {
                return this._offerId6;
            }
            set
            {
                if (this._offerId6 == value) return;
                this._offerId6 = value;
            }
        }

        // Offer Id 7
        public double OfferId7
        {
            get
            {
                return this._offerId7;
            }
            set
            {
                if (this._offerId7 == value) return;
                this._offerId7 = value;
            }
        }

        // Offer Id 8
        public double OfferId8
        {
            get
            {
                return this._offerId8;
            }
            set
            {
                if (this._offerId8 == value) return;
                this._offerId8 = value;
            }
        }

        // Offer Id 9
        public double OfferId9
        {
            get
            {
                return this._offerId9;
            }
            set
            {
                if (this._offerId9 == value) return;
                this._offerId9 = value;
            }
        }

        // Offer Id 10
        public double OfferId10
        {
            get
            {
                return this._offerId10;
            }
            set
            {
                if (this._offerId10 == value) return;
                this._offerId10 = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Offer List

    #region Offer request
    /// <summary>
    /// GMU to Host Freeform for Offer request
    /// </summary>
    public class FFTgt_G2H_EFT_OfferRequest
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.OfferRequest;
            }
        }

        #region Private Data Members

        private FF_AppId_EFT_AccountTypes _accountType;
        private string _playerAccountNumber;
        private string _pin;
        private double _offerId;

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

        // Player Account Number
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

        // Offer Id
        public double OfferId
        {
            get
            {
                return this._offerId;
            }
            set
            {
                if (this._offerId == value) return;
                this._offerId = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Offer List request
}
