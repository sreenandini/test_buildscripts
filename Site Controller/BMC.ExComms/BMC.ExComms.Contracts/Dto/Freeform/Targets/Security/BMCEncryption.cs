using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for BMC Encryption
    /// </summary>
    public class FFTgt_G2H_Security_BMCEncryption
        : FFTgt_B2B_Security_Data, IFFTgt_G2H { }

    /// <summary>
    /// Host to GMU Freeform for BMC Encryption Response
    /// </summary>
    public class FFTgt_H2G_Security_BMCEncryption_Response 
        : FFTgt_B2B_Security_Data, IFFTgt_H2G
    {
        #region Private Members

        private FF_AppId_ResponseStatus_Types _status;

        #endregion //Private Members

        #region Properties

        public FF_AppId_ResponseStatus_Types Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                _status = value;
            }
        }

        #endregion //Properties
    }

    /// <summary>
    /// Host to GMU Freeform for BMC Encryption
    /// </summary>
    public class FFTgt_H2G_Security_BMCEncryption
        : FFTgt_B2B_Security_Data, IFFTgt_H2G { }

    /// <summary>
    /// GMU to Host Freeform for BMC Encryption Response
    /// </summary>
    public class FFTgt_G2H_Security_BMCEncryption_Response 
        : FFTgt_B2B_Security_Data, IFFTgt_G2H
    {
        #region Private Members

        private FF_AppId_ResponseStatus_Types _status;

        #endregion //Private Members

        #region Properties

        // Response Status -> Success (Or) Fail
        public FF_AppId_ResponseStatus_Types Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                _status = value;
            }
        }

        #endregion //Properties
    }    
}
