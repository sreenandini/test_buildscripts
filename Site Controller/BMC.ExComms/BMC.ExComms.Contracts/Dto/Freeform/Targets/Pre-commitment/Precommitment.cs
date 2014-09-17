using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Precommitment

    /// <summary>
    /// GMU to Host Freeform for Precommitment
    /// </summary>
    public class FFTgt_B2B_Precommitment
        : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.Precommitment;
            }
        }

        public FFTgt_B2B_Precommitment_Data PrecommitmentData { get; set; }
    }

    public class FFTgt_B2B_Precommitment_Data
        : FFTgt_B2B { }

    #endregion //Precommitment

    #region Player Account Details

    public class FFTgt_B2B_PC_Player_AccDetails
        : FFTgt_B2B_Precommitment_Data
    {
        #region Private Data Members

        private int _playerAccNoLen;
        private string _playerAccNo;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Player Account Number Length
        /// </summary>
        public int PlayerAccNoLen
        {
            get
            {
                return this._playerAccNoLen;
            }
            set
            {
                if (this._playerAccNoLen == value) return;
                this._playerAccNoLen = value;
            }
        }

        /// <summary>
        /// Player Account Number
        /// </summary>
        public string PlayerAccNo
        {
            get
            {
                return this._playerAccNo;
            }
            set
            {
                if (this._playerAccNo == value) return;
                this._playerAccNo = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Player Account Details
}
