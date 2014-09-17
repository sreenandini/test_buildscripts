using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_GMU_Auth_Status
        : FFTgt_B2B_GMU_Auth_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUAuthentication.Status;
            }
        }

        #region Properties
        private FF_AppId_GMUAuthentication_AuthStatusTypes _status; 
        #endregion

        #region Methods

        public FF_AppId_GMUAuthentication_AuthStatusTypes Status
        {
            get { return _status; }
            set { _status = value; }
        } 

        #endregion
    }
}
