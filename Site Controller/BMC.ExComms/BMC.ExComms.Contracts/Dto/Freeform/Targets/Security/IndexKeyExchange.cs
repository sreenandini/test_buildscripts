using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Indexed Key Exchange
    /// </summary>
    public class FFTgt_B2B_Security_Indexed_KeyExchange
        : FFTgt_B2B_Security_Data
    {
        private FF_AppId_KeyExchange _keyIndex;
        private byte[] _key;

        /// Indicates which key is being used.
        public FF_AppId_KeyExchange KeyIndex
        {
            get
            {
                return this._keyIndex;
            }
            set
            {
                this._keyIndex = value;
            }
        }

        /// 9 bytes of partial key data
        public byte[] Key
        {
            get
            {
                return this._key;
            }
            set
            {
                this._key = value;
            }
        }
    }

    /// <summary>
    /// Indexed Key Exchange Start
    /// </summary>
    public class FFTgt_B2B_Security_Indexed_KeyExchange_Start
        : FFTgt_B2B_Security_Indexed_KeyExchange { }

    /// <summary>
    /// Indexed Key Exchange End
    /// </summary>
    public class FFTgt_B2B_Security_Indexed_KeyExchange_End
        : FFTgt_B2B_Security_Indexed_KeyExchange { }
}
