using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_H2G_GMU_Auth_Initiate
        : FFTgt_B2B_GMU_Auth_Data, IFFTgt_H2G
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUAuthentication.Initiate;
            }
        }

        #region Properties
        private int _seed;
        #endregion

        #region Methods
        public int Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }
        #endregion
    }
}
