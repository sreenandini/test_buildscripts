using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Encrypted Authentication Byte with Unencrypted Data
    /// </summary>
    public class FFTgt_B2B_Security_BMCAuthentication
        : FFTgt_B2B_Security_Data
    {
        #region Private Members

        private byte _authenticationByte;

        #endregion //Private Members

        #region Properties

        public byte AuthenticationByte
        {
            get
            {
                return _authenticationByte;
            }
            set
            {
                _authenticationByte = value;
            }
        }

        #endregion //Properties
    }
}
