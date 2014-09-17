/* ================================================================================= 
 * Purpose		:	BGS Encryption/Decription Class
 * File Name	:   BGSEncryption.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	22/11/11
 * ================================================================================= 
 * Copyright © 2012 Bally Technologies, Inc. All rights reserved.
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 22/11/11		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Cryptography
{
    /// <summary>
    /// Encryption/Decription Class for Site Controller
    /// </summary>
    internal class BGSEncryption
        : DisposableObject
    {
        #region Variables
        //private BGSGeneral._cConstants _bgsConstants = null;
        //private BGSEncryptDecrypt.clsBlowFish _bgsBlowFish = null;
        private string _bgsEncryptionKey = string.Empty;
        private int[] _decryptAllowedIdxs = new int[] { 0, 2 };
        private int[] _hexAllowedIdx = new int[] { 2 };
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EnterpriseEncryption"/> class.
        /// </summary>
        public BGSEncryption()
        {
            ////_bgsConstants = new BGSGeneral.cConstantsClass();
            //_bgsBlowFish = new BGSEncryptDecrypt.clsBlowFishClass();
            //_bgsEncryptionKey = EncryptionConstants.BGS_ENCRYPTIONKEY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BGSEncryption"/> class.
        /// </summary>
        /// <param name="outputInHex">if set to <c>true</c> [output in hex].</param>
        public BGSEncryption(bool outputInHex)
            : this() { this.OutputInHex = outputInHex; }
        #endregion

        #region Properties
        private bool _outputInHex = false;
        public bool OutputInHex
        {
            get { return _outputInHex; }
            set { _outputInHex = value; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="source">Normal string.</param>
        /// <returns>Encrypted string.</returns>
        public string EncryptString(string source)
        {
            return string.Empty;// _bgsBlowFish.EncryptString(ref source, ref _bgsEncryptionKey, ref _outputInHex);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="source">Encrypted string.</param>
        /// <returns>Decrypted string.</returns>
        public string DecryptString(string source)
        {
            return string.Empty;// _bgsBlowFish.DecryptString(ref source, ref _bgsEncryptionKey, ref _outputInHex);
        }

        #endregion
    }
}
