using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Request
    public class FFTgt_H2G_DefaultIO_SetLockout 
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_H2G
    {
        /// <summary>
        /// 0-Lockout off; 1-Lockout on.
        /// </summary>
        public FF_AppId_DefaultIO_SetLockoutTypes Lockout { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.SetLockout;
            }
        }
    } 
    #endregion

    #region Response
    /// <summary>
    /// If this target receives a response required flag and an illegal or unsupported action ID, it will application NAK.
    /// If success, will application ACK
    /// </summary>
    public class FFTgt_G2H_DefaultIO_SetLockout 
        : FFTgt_B2B_DefaultIO_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_DefaultIO_Types.SetLockout;
            }
        }

    } 
    #endregion
}
