using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Cryptography
{
    public static class CryptoHelperFactory
    {
        /// <summary>
        /// Creates the specified crypto type.
        /// </summary>
        /// <param name="cryptoType">Type of the crypto.</param>
        /// <returns>Implementation of crypto helper.</returns>
        public static ICryptoHelper Create(CryptoType cryptoType)
        {
            return new CryptoHelperImpl(cryptoType);
        }
        
#if !SILVERLIGHT
        /// <summary>
        /// Creates the RSA.
        /// </summary>
        /// <param name="privateKeyPath">The private key path.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="publicKeyPath">The public key path.</param>
        /// <returns>Implementation of crypto helper.</returns>
        public static ICryptoHelper CreateRSA(CryptoType cryptoType,
            string privateKeyPath,
            string privateKeyPassword,
            string publicKeyPath)
        {
            if (cryptoType == CryptoType.RSA)
            {
                return new RSAEncryption(privateKeyPath, privateKeyPassword, publicKeyPath);
            }
            else if (cryptoType == CryptoType.RSATripleDES)
            {
                return new RSATripleDESEncryption(privateKeyPath, privateKeyPassword, publicKeyPath);
            }
            else
            {
                return Create(cryptoType);
            }
        }

        /// <summary>
        /// Recreates the specified current.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="cryptoType">Type of the crypto.</param>
        /// <param name="privateKeyPath">The private key path.</param>
        /// <param name="privateKeyPassword">The private key password.</param>
        /// <param name="publicKeyPath">The public key path.</param>
        /// <returns>Implementation of crypto helper.</returns>
        public static ICryptoHelper Recreate(ref ICryptoHelper current,
            CryptoType cryptoType,
            string privateKeyPath,
            string privateKeyPassword,
            string publicKeyPath)
        {
           if ((current == null) ||
                (current.Type != cryptoType))
            {
                current = CreateRSA(cryptoType, privateKeyPath, privateKeyPassword, publicKeyPath);
            }

            return current;
        }
#endif
    }
}
