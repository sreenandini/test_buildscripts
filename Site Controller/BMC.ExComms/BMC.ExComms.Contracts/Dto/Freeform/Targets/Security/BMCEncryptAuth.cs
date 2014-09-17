using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Extended Encryption, BMC encryption with authentication
    /// </summary>
    public class FFTgt_B2B_Security_EncryptAuthentication
        : FFTgt_B2B_Security_Data
    {
        #region Private Members

        private byte _encryptionAlgorithm;
        private byte _keyIndex;

        #endregion //Private Members

        #region Properties

        // 0x05—SDS  encryption with auth
        public byte EncryptionAlgorithm
        {
            get
            {
                return this._encryptionAlgorithm;
            }
            set
            {
                _encryptionAlgorithm = value;
            }
        }

        // Indicates which key is being used.
        public byte KeyIndex
        {
            get
            {
                return this._keyIndex;
            }
            set
            {
                _keyIndex = value;
            }
        }

        #endregion //Properties
    }
}
