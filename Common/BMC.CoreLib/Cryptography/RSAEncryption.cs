using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace BMC.CoreLib.Cryptography
{
    /// <summary>
    /// RSAEncryption
    /// </summary>
    internal class RSAEncryption : ICryptoHelper
    {
        private X509Certificate2 _x509PrivateKey = null;
        private X509Certificate2 _x509PublicKey = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RSAEncryption"/> class.
        /// </summary>
        /// <param name="privateKeyPath">The private key path.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="publicKeyPath">The public key path.</param>
        public RSAEncryption(string privateKeyPath,
            string privateKeyPassword,
            string publicKeyPath)
        {
            this.RSAPrivateKeyPassword = privateKeyPassword;
            this.RSAPrivateKeyPath = privateKeyPath;
            this.RSAPublicKeyPath = publicKeyPath;
        }

        /// <summary>
        /// Gets or sets the RSA private key password.
        /// </summary>
        /// <value>The RSA private key password.</value>
        public string RSAPrivateKeyPassword { get; private set; }

        /// <summary>
        /// Gets or sets the RSA private key path.
        /// </summary>
        /// <value>The RSA private key path.</value>
        public string RSAPrivateKeyPath { get; private set; }

        /// <summary>
        /// Gets or sets the RSA public key path.
        /// </summary>
        /// <value>The RSA public key path.</value>
        public string RSAPublicKeyPath { get; private set; }

        #region ICryptoHelper Members

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal void Initialize()
        {
            _x509PrivateKey = new X509Certificate2(this.RSAPrivateKeyPath, this.RSAPrivateKeyPassword);
            _x509PublicKey = new X509Certificate2(this.RSAPublicKeyPath);
        }

        /// <summary>
        /// Validates the X509 certificate.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        /// <param name="isPrivateKey">if set to <c>true</c> [is private key].</param>
        private void ValidateX509Certificate(X509Certificate2 certificate, bool isPrivateKey)
        {
            if (certificate.NotAfter < DateTime.Now)
                throw new CryptographicException("Certificate Expired.");
            if (certificate.NotBefore > DateTime.Now)
                throw new CryptographicException("Invalud Certificate Date.");

            if (isPrivateKey)
            {
                if (!certificate.HasPrivateKey)
                    throw new CryptographicException("Not a valid Certificate (No Private key available)");
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public CryptoType Type
        {
            get
            {
                return CryptoType.RSA;
            }
        }

        /// <summary>
        /// Encrypts the specified sourc.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Encrypted string.</returns>
        public virtual string Encrypt(string source)
        {
            this.ValidateX509Certificate(_x509PublicKey, false);
            RSACryptoServiceProvider rsaCryptoServiceProvider = (RSACryptoServiceProvider)_x509PublicKey.PublicKey.Key;

            byte[] buffer = Encoding.Default.GetBytes(source);
            byte[] encryptedMessagesInBytes = rsaCryptoServiceProvider.Encrypt(buffer, true);
            string encryptedMessage = Encoding.Default.GetString(encryptedMessagesInBytes);
            byte[] nbuff = Encoding.Default.GetBytes(encryptedMessage);
            return Convert.ToBase64String(nbuff);
        }

        /// <summary>
        /// Decrypts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Plain string.</returns>
        public virtual string Decrypt(string source)
        {
            this.ValidateX509Certificate(_x509PrivateKey, true);
            RSACryptoServiceProvider rsaCryptoServiceProvider = (RSACryptoServiceProvider)_x509PrivateKey.PrivateKey;

            byte[] nbuff = Convert.FromBase64String(source);
            var encryptedString = Encoding.Default.GetString(nbuff);

            byte[] buff = Encoding.Default.GetBytes(encryptedString);
            byte[] encryptedMessagesInBytes = rsaCryptoServiceProvider.Decrypt(buff, true);
            return Encoding.Default.GetString(encryptedMessagesInBytes);
        }

        #endregion
    }
}
