namespace BMC.Security
{
    using System.IO;
    using System;
    using System.Security.Cryptography;
    using System.Text;

        public static class SiteLicensingCryptoHelper
        {
            private const string Key = "B411y";
            
            public static string Decrypt(string encryptedText)
            {
                string returnValue;
                byte[] keyBuffer = Encoding.BigEndianUnicode.GetBytes(Key);
                byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);
                using (var tripleDesProvider = new TripleDESCryptoServiceProvider { Key = keyBuffer, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    using (ICryptoTransform decryptor = tripleDesProvider.CreateDecryptor())
                    {
                        byte[] resultBuffer = decryptor.TransformFinalBlock(encryptedBuffer, 0, encryptedBuffer.Length);
                        returnValue = Encoding.BigEndianUnicode.GetString(resultBuffer);
                    }
                }
                return returnValue;
            }
            //This method used for Site Licensing Decrypt. Key is "B411y51T" 
            public static string Decrypt(string encryptedText, string key)
            {
                string returnValue;
                byte[] keyBuffer = Encoding.BigEndianUnicode.GetBytes(key);
                byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);
                using (var tripleDesProvider = new TripleDESCryptoServiceProvider { Key = keyBuffer, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    using (ICryptoTransform decryptor = tripleDesProvider.CreateDecryptor())
                    {
                        byte[] resultBuffer = decryptor.TransformFinalBlock(encryptedBuffer, 0, encryptedBuffer.Length);
                        returnValue = Encoding.BigEndianUnicode.GetString(resultBuffer);
                    }
                }
                return returnValue;
            }

            public static string Encrypt(string plainText)
            {
                string returnValue;
                byte[] keyBuffer = Encoding.BigEndianUnicode.GetBytes(Key);
                byte[] plainBuffer = Encoding.BigEndianUnicode.GetBytes(plainText);
                using (var tripleDesProvider = new TripleDESCryptoServiceProvider { Key = keyBuffer, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    using (ICryptoTransform encryptor = tripleDesProvider.CreateEncryptor())
                    {
                        byte[] resultBuffer = encryptor.TransformFinalBlock(plainBuffer, 0, plainBuffer.Length);
                        returnValue = Convert.ToBase64String(resultBuffer, 0, resultBuffer.Length);
                    }
                }
                return returnValue;
            }

            //This method used for Site Licensing Encrypt. Key is "B411y51T" 
            public static string Encrypt(string plainText, string key)
            {
                string returnValue;
                byte[] keyBuffer = Encoding.BigEndianUnicode.GetBytes(key);
                byte[] plainBuffer = Encoding.BigEndianUnicode.GetBytes(plainText);
                using (var tripleDesProvider = new TripleDESCryptoServiceProvider { Key = keyBuffer, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    using (ICryptoTransform encryptor = tripleDesProvider.CreateEncryptor())
                    {
                        byte[] resultBuffer = encryptor.TransformFinalBlock(plainBuffer, 0, plainBuffer.Length);
                        returnValue = Convert.ToBase64String(resultBuffer, 0, resultBuffer.Length);
                    }
                }
                return returnValue;
            }
            public static string CreateHash(string value)
            {
                Stream stream = new MemoryStream(Encoding.Default.GetBytes(value));
                var alg = HashAlgorithm.Create("MD5");
                return Convert.ToBase64String(alg.ComputeHash(stream));
            }
        }
    }



