using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Server.Handlers.Security
{
    //! These assign the key indices to applications. They must have no gaps.
    internal enum SECURITY_KEY_INDEX
    {
        TICKET_KEY = 0,	//!< This index is implied for ticket  encryption 
        EFT_KEY = 1, 	//!< This index is implied for eft  encryption
        FRONT_END_KEY = 2,	//!< Tis index is used explicitly for aver all encryption
        DIALOG_KEY = 3,	//!< key for dialogs
        PWR_CRD_KEY = 4,	//! < the key for power ball
        TRIPLE_DES_KEY = 6,   //! Added for BMC TripleDES Requirement
        NUMBER_OF_KEYS,		//!< not a key index but the number of current keys assigned
        NO_KEY = 0xFF	//! designates there is no key, it was unencrypted 
    } ;

    internal enum ENCRYP_TARGETS
    {
        ET_Test = 0x01,	//!<  Test encryption is like ET_ExtEncrypt with ES_Test style and don't care key
        ET_SdsEncryption = 0x02,	//!<  Standard Encryption (for tickets)
        ET_SdsKeyExchange = 0x03,	//!<  Old style key exchange GMU starts (for tickets )
        ET_SdsAuthentication = 0x04,	//!<  Authenticate byte with data in clear
        ET_EFTStyleEncryption = 0x05,	//!<  Both authenticate byte and encrypted data
        ET_EFTKeyStart = 0x06,	//!<  new style key exchange (either can start) (for eft)
        ET_EFTKeyEnd = 0x07,	//!<  new style key exchange (either can start) (for eft)
        ET_ExtEncrypt = 0x08,	//!<  Extensible Encryption target
        ET_ExtKeyStart = 0x09,	//!<  new style key exchange (either can start) with key index
        ET_ExtKeyEnd = 0x0A,	//!<  new style key exchange (either can start) with key index
    };
}
