/* ================================================================================= 
 * Purpose		:	Triple DES Encryption
 * File Name	:   TripleDESEncryption.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	21/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 21/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace BMC.CoreLib.Cryptography
{
    /// <summary>
    /// Triple DES Encryption
    /// </summary>
    internal static class TripleDESEncryption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TripleDESEncryption"/> class.
        /// </summary>
        static TripleDESEncryption() { }

        #region Cryptographic Values

        /// <summary>
        /// Makes the MD.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Byte arry</returns>
        public static byte[] MakeMd(byte[] original)
        {
            var hashmd = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd.ComputeHash(original);
            return keyhash;
        }

        /// <summary>
        /// Encrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="key">The key.</param>
        /// <returns>Byte arry</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            var des = new TripleDESCryptoServiceProvider { Key = MakeMd(key), Mode = CipherMode.ECB };
            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// Decrypts the specified encrypted.
        /// </summary>
        /// <param name="encrypted">The encrypted.</param>
        /// <param name="key">The key.</param>
        /// <returns>Byte arry</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            var des = new TripleDESCryptoServiceProvider { Key = MakeMd(key), Mode = CipherMode.ECB };
            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        /// <summary>
        /// Encrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="key">The key.</param>
        /// <returns>Encryped string</returns>
        public static string Encrypt(string original, string key)
        {
            try
            {
                byte[] buff = Encoding.Default.GetBytes(original);
                byte[] kb = Encoding.Default.GetBytes(key);
                return Convert.ToBase64String(Encrypt(buff, kb));
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Decrypts the specified encrypted.
        /// </summary>
        /// <param name="encrypted">The encrypted.</param>
        /// <param name="key">The key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Decryped string</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            try
            {
                byte[] buff = Convert.FromBase64String(encrypted);
                byte[] kb = Encoding.Default.GetBytes(key);
                return encoding.GetString(Decrypt(buff, kb));
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Encrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Encryped string</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, EncryptionConstants.SYMMETRIC_KEY);
        }

        /// <summary>
        /// Decrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, EncryptionConstants.SYMMETRIC_KEY, Encoding.Default);
        }

        /// <summary>
        /// Decrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="key">The key.</param>
        /// <returns>Decryped string</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, Encoding.Default);
        }

        /// <summary>
        /// Decrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>Decryped string</returns>
        public static string Decrypt(string original, Encoding encoding)
        {
            return Decrypt(original, EncryptionConstants.SYMMETRIC_KEY, encoding);
        }

        #endregion
    }
}
