using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Cryptography
{
    public interface ICryptoHelper
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        CryptoType Type { get; }

        /// <summary>
        /// Encrypts the specified sourc.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Encrypted string.</returns>
        string Encrypt(string source);

        /// <summary>
        /// Decrypts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Plain string.</returns>
        string Decrypt(string source);
    }
}
