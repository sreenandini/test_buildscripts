using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Cryptography
{
    /// <summary>
    /// RSATripleDESEncryption
    /// </summary>
    internal class RSATripleDESEncryption : RSAEncryption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RSAEncryption"/> class.
        /// </summary>
        /// <param name="privateKeyPath">The private key path.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="publicKeyPath">The public key path.</param>
        public RSATripleDESEncryption(string privateKeyPath,
             string privateKeyPassword,
             string publicKeyPath)
            : base(privateKeyPath, privateKeyPassword, publicKeyPath) { }

        #region ICryptoHelper Members

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public CryptoType Type
        {
            get
            {
                return CryptoType.RSATripleDES;
            }
        }

        /// <summary>
        /// Encrypts the specified sourc.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Encrypted string.</returns>
        public string Encrypt(string source)
        {
            string destination = base.Encrypt(source);
            return TripleDESEncryption.Encrypt(destination);
        }

        /// <summary>
        /// Decrypts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Plain string.</returns>
        public string Decrypt(string source)
        {
            string destination = base.Decrypt(source);
            return TripleDESEncryption.Decrypt(destination);
        }

        #endregion
    }
}
