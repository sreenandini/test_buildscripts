using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using BMC.CoreLib;

namespace BMC.CoreLib.Cryptography
{
    internal class CryptoHelperImpl : DisposableObject, ICryptoHelper
    {
        private CryptoType _cryptoType = CryptoType.NoEncryption;
        //private BGSEncryption _bgsEncryption = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoHelperImpl"/> class.
        /// </summary>
        /// <param name="cryptoType">Type of the crypto.</param>
        internal CryptoHelperImpl(CryptoType cryptoType)
        {
            _cryptoType = cryptoType;
            //if (cryptoType == CryptoType.BGSWithHex) _bgsEncryption = new BGSEncryption(true);
            //else if (cryptoType == CryptoType.BSGWithoutHex) _bgsEncryption = new BGSEncryption(false);
        }

        #region ICryptoHelper Members

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public CryptoType Type
        {
            get
            {
                return _cryptoType;
            }
        }

        /// <summary>
        /// Encrypts the specified sourc.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Encrypted string.</returns>
        public string Encrypt(string source)
        {
            switch (_cryptoType)
            {
#if !SILVERLIGHT
                case CryptoType.TripleDES:
                    return TripleDESEncryption.Encrypt(source);
                case CryptoType.RSA:
                    throw new NotSupportedException("Use RSAEncryption class.");

                case CryptoType.RSATripleDES:
                    throw new NotSupportedException("Use RSATripleDESEncryption class.");

                //case CryptoType.BGSWithHex:
                //case CryptoType.BSGWithoutHex:
                //    return _bgsEncryption.EncryptString(source);

                case CryptoType.MD5:
                    return MD5Hash.CreateHash(source);
#endif
                default:
                    return source;
            }
        }

        /// <summary>
        /// Decrypts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Plain string.</returns>
        public string Decrypt(string source)
        {
            switch (_cryptoType)
            {
#if !SILVERLIGHT
                case CryptoType.TripleDES:
                    return TripleDESEncryption.Decrypt(source);

                case CryptoType.RSA:
                    throw new NotSupportedException("Use RSAEncryption class.");

                case CryptoType.RSATripleDES:
                    throw new NotSupportedException("Use RSATripleDESEncryption class.");

                //case CryptoType.BGSWithHex:
                //case CryptoType.BSGWithoutHex:
                //    return _bgsEncryption.DecryptString(source);

                case CryptoType.MD5:
                    throw new NotSupportedException("MD5 does not support decrypting.");
#endif
                default:
                    return source;
            }
        }

        #endregion
    }
}
