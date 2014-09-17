using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_GMU_Auth_Results
        : FFTgt_B2B_GMU_Auth_Data, IFFTgt_G2H
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUAuthentication.Results;
            }
        }

        #region Properties
        private int _seed;
        private String _documentId; 
        #endregion

        #region Methods
        public int Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }

        public String DocumentID
        {
            get { return _documentId; }
            set { _documentId = value; }
        } 
        #endregion
    }
}
