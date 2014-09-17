using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Cryptography
{
    public enum CryptoType
    {
        NoEncryption = 0,
        TripleDES = 1,
        RSA = 2,
        RSATripleDES = 3,
        BGSWithHex = 4,
        BSGWithoutHex = 5,
        MD5 = 6
    }
}
