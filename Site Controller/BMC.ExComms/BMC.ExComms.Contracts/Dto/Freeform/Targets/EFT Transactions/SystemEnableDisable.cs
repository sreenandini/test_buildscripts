using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region System
    /// <summary>
    /// Host to GMU freeform for System Enable EFT
    /// </summary>
    public class FFTgt_H2G_EFT_SystemEnable
        : FFTgt_B2B_EFT_Data_NoEncryption, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SystemEnableEFT;
            }
        }

        public override FF_AppId_Encryption_Types EncryptionType
        {
            get
            {
                return FF_AppId_Encryption_Types.None;
            }
            set
            {
                base.EncryptionType = value;
            }
        }
    }

    public class FFTgt_G2H_EFT_SystemEnable
        : FFTgt_B2B_EFT_Data_NoEncryption, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SystemEnableEFT;
            }
        }

        public FF_AppId_EFT_ResponseTypes ResponseType { get; set; }
    }

    /// <summary>
    /// Host to GMU freeform for System Disable EFT
    /// </summary>
    public class FFTgt_H2G_EFT_SystemDisable
        : FFTgt_B2B_EFT_Data_NoEncryption, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SystemDisableEFT;
            }
        }
    }

    public class FFTgt_G2H_EFT_SystemDisable
        : FFTgt_B2B_EFT_Data_NoEncryption, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_EFT_TransactionTypes.SystemDisableEFT;
            }
        }

        public FF_AppId_EFT_ResponseTypes ResponseType { get; set; }
    }
    
    #endregion //System
}
