using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Phishing
    /// </summary>
    public class FFTgt_G2H_EFT_Phishing
        : FFTgt_B2B_EFT_Data, IFFTgt_G2H
    {
        #region Private Data Members

        private byte _type;

        #endregion //Private Data Members

        #region Properties

        // Player Card Number
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        // Type
        public byte Type
        {
            get
            {
                return this._type;
            }
            set
            {
                if (this._type == value) return;
                this._type = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.Phishing;
            }
        }
    }
}
