using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.IO;
using System.Security.Cryptography;

namespace BMC.CoreLib.Cryptography
{
    internal static class MD5Hash
    {
        /// <summary>
        /// Creates the MD5 hash.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Computed Hash</returns>
        public static string CreateHash(string original)
        {
            if (original.IsEmpty()) return string.Empty;

            using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(original)))
            {
                MD5 md5Hash = HashAlgorithm.Create("MD5") as MD5;
                return Convert.ToBase64String(md5Hash.ComputeHash(ms));
            }
        }
    }
}
