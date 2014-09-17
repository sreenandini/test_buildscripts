using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public abstract class FFTgt_B2B_Security_KeyExchange_Base
        : FFTgt_B2B_Security_Data, IFreeformEntity_MsgTgt_Primary
    {
        public override int EntityId
        {
            get { return (int)FF_AppId_Security_Encryption_Types.SDSKeyExchange; }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Key Exchange Request
    /// </summary>
    public class FFTgt_B2B_Security_KeyExchange_Request
        : FFTgt_B2B_Security_KeyExchange_Base
    {
    }

    /// <summary>
    /// G2H (or) H2G Partial Key Transfer
    /// </summary>
    public class FFTgt_B2B_Security_PartialKey
        : FFTgt_B2B_Security_KeyExchange_Base
    {
        #region Private Members

        private byte[] _partialKey;

        #endregion //Private Members

        #region Properties

        public byte[] PartialKey
        {
            get
            {
                return this._partialKey;
            }
            set
            {
                this._partialKey = value;
            }
        }

        #endregion //Properties
    }

    /// <summary>
    /// Host to GMU Freeform for Key Exchange Partial Key
    /// </summary>
    public class FFTgt_B2B_Security_KeyExchange_PartialKey
        : FFTgt_B2B_Security_PartialKey
    {
    }

    /// <summary>
    /// GMU to Host Freeform for Key Exchange End
    /// </summary>
    public class FFTgt_B2B_Security_KeyExchange_End
        : FFTgt_B2B_Security_PartialKey { }

    /// <summary>
    /// Host to GMU Key Exchange Status
    /// </summary>
    public class FFTgt_B2B_Security_KeyExchange_Status
        : FFTgt_B2B_Security_KeyExchange_Base
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
}
