using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_GMUEvent_EFTData
        : FFTgt_B2B_GMUEventData_Primary
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUEvent_DataSetIds.EFT;
            }
        }
        
        public byte TransactionID { get; set; }
        public byte ErrorCode { get; set; }
    }
}
