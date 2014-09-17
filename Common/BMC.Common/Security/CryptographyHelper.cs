using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Cryptography;
using BMC.Common.ConfigurationManagement;

namespace BMC.Common.Security
{
    public class CryptographyHelper
    {

        public static X509Certificate2 x509Certificate;
        public static X509Certificate2 x509Certificate2PublicKey;

        /// <summary>
        /// Bytearraytostrings the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string Bytearraytostring(byte[] bytes)
        {
            return Convert.ToBase64String(bytes); //enc.GetString(bytes);
        }

        /// <summary>
        /// Strings to byte array.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string inputString)
        {
            return Convert.FromBase64String(inputString);
        }

        /// <summary>
        /// Encrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, "AnCaGaKaMaMaNaPoRaReSuVi");
        }

        /// <summary>
        /// Decrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, "AnCaGaKaMaMaNaPoRaReSuVi", Encoding.Default);
        }

        /// <summary>
        /// Decrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, Encoding.Default);
        }

        /// <summary>
        /// Decrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Decrypt(string original, Encoding encoding)
        {
            return Decrypt(original, "AnCaGaKaMaMaNaPoRaReSuVi", encoding);
        }

        /// <summary>
        /// Encrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Encrypt(string original, string key)
        {
            var isRsa = "FALSE";
            var isTripleDes = "TRUE";
            try
            {
                isRsa = RegistryConfigurationAdapter.Read("IsRSAEnabled", "FALSE").ToUpper();
                isTripleDes = RegistryConfigurationAdapter.Read("IsTripleDES", "TRUE").ToUpper();
            }
            catch { }

            if (isRsa.ToUpper() == "TRUE")
            {
                if (x509Certificate2PublicKey == null)
                    x509Certificate2PublicKey = new X509Certificate2(RegistryConfigurationAdapter.Read("RSAPublicKeyPath", ""));

                if (x509Certificate2PublicKey.NotAfter < DateTime.Now)
                    throw new CryptographicException("Certificate Expired");

                if (x509Certificate2PublicKey.NotBefore > DateTime.Now)
                    throw new CryptographicException("Invalid Certificate date");

                var rsaCryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate2PublicKey.PublicKey.Key;

                var buffer = Encoding.Default.GetBytes(original);
                var encryptedMessagesInBytes = rsaCryptoServiceProvider.Encrypt(buffer, true);
                var encryptedMessage = Encoding.Default.GetString(encryptedMessagesInBytes);
                var nbuff = Encoding.Default.GetBytes(encryptedMessage);
                original = Convert.ToBase64String(nbuff);
            }

            if (isTripleDes.ToUpper() == "TRUE")
            {
                byte[] buff = Encoding.Default.GetBytes(original);
                byte[] kb = Encoding.Default.GetBytes(key);
                return Convert.ToBase64String(Encrypt(buff, kb));
            }
            return original;
        }

        /// <summary>
        /// Decrypts the specified encrypted.
        /// </summary>
        /// <param name="encrypted">The encrypted.</param>
        /// <param name="key">The key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            var isRsa = "FALSE";
            var isTripleDes = "TRUE";
            try
            {
                isRsa = RegistryConfigurationAdapter.Read("IsRSAEnabled", "false").ToUpper();
                isTripleDes = RegistryConfigurationAdapter.Read("IsTripleDES", "true").ToUpper();
            }
            catch { }

            if (isRsa.ToUpper() == "TRUE")
            {
                if (x509Certificate == null)
                    x509Certificate = new X509Certificate2(RegistryConfigurationAdapter.Read("RSAPrivateKeyPath", ""), RegistryConfigurationAdapter.Read("PrivateKeyPassword", ""));

                if (x509Certificate.NotAfter < DateTime.Now)
                    throw new CryptographicException("Certificate Expired");

                if (x509Certificate.NotBefore > DateTime.Now)
                    throw new CryptographicException("Invalid Certificate date");

                if (!x509Certificate.HasPrivateKey)
                    throw new CryptographicException("Not a valid Certificate (No Private key available)");

                var rsaCryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.PrivateKey;

                byte[] nbuff = Convert.FromBase64String(encrypted);
                var encryptedString = Encoding.Default.GetString(nbuff);

                var buff = Encoding.Default.GetBytes(encryptedString);
                var encryptedMessagesInBytes = rsaCryptoServiceProvider.Decrypt(buff, true);
                encrypted = Encoding.Default.GetString(encryptedMessagesInBytes);

            }
            if (isTripleDes.ToUpper() == "TRUE")
            {
                byte[] buff = Convert.FromBase64String(encrypted);
                byte[] kb = Encoding.Default.GetBytes(key);
                return encoding.GetString(Decrypt(buff, kb));
            }
            return encrypted;
        }

        /// <summary>
        /// Makes the MD.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            var des = new TripleDESCryptoServiceProvider { Key = MakeMd(key), Mode = CipherMode.ECB };

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        /// <summary>
        /// Encrypts the specified original.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = Encoding.Default.GetBytes("AnCaGaKaMaMaNaPoRaReSuVi");
            return Encrypt(original, key);
        }

        /// <summary>
        /// Decrypts the specified encrypted.
        /// </summary>
        /// <param name="encrypted">The encrypted.</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = Encoding.Default.GetBytes("AnCaGaKaMaMaNaPoRaReSuVi");
            return Decrypt(encrypted, key);
        }


        /// <summary>
        /// Creates the hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static byte[] CreateHash(string value)
        {
            Stream stream = new MemoryStream(Encoding.Default.GetBytes(value));
            var alg = HashAlgorithm.Create("MD5");
            return alg.ComputeHash(stream);
        }

        /// <summary>
        /// Gets the hash string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetHashString(string value)
        {
            var stream = new MemoryStream(Encoding.Default.GetBytes(value));
            var alg = HashAlgorithm.Create("MD5");
            return Bytearraytostring(alg.ComputeHash(stream));
        }

    }
}
