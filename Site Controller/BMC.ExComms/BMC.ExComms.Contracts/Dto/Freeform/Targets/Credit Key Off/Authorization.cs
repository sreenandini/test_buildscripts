using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Authorization
    
    /// <summary>
    /// GMU to Host (Or) Host to GMU Freeform for Authorization
    /// </summary>
    public class FFTgt_B2B_CreditKeyOff
       : FFTgt_B2B, IFFTgt_B2B
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.CreditKeyOff;
            }
        }

        public FFTgt_B2B_CreditKeyOff_Data CreditKeyOffData { get; set; }
    }

    public class FFTgt_B2B_CreditKeyOff_Data
        : FFTgt_B2B { }

    #endregion //Authorization

    #region Request Authorization
    
    /// <summary>
    /// GMU to Host Freeform for Request Authorization
    /// </summary>
    public class FFTgt_G2H_CreditKeyOff_AuthorizationRequest
        : FFTgt_B2B_CreditKeyOff_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_GmuId_CreditKeyOff_G2H_Types.AuthorizationRequest;
            }
        }
    }

    #endregion //Request Authorization

    #region Authorization Approved

    /// <summary>
    /// Host to GMU Freeform for Authorization Approved
    /// </summary>
    public class FFTgt_H2G_CreditKeyOff_AuthorizationApproved
        : FFTgt_B2B_CreditKeyOff_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_GmuId_CreditKeyOff_H2G_Types.AuthorizationApproved;
            }
        }
    }

    #endregion //Request Authorization

    #region Not Authorized

    /// <summary>
    /// Host to GMU Freeform for Not Authorized
    /// </summary>
    public class FFTgt_H2G_CreditKeyOff_NotAuthorized
        : FFTgt_B2B_CreditKeyOff_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_GmuId_CreditKeyOff_H2G_Types.NotAuthorized;
            }
        }

        #region Private Data Members

        private string _data;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Text to Display
        /// </summary>
        public string Data
        {
            get
            {
                return this._data;
            }
            set
            {
                if (this._data.Equals(value)) return;
                this._data = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Not Authorized
}
