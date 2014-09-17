using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    public class FFTgt_G2H_GMUEvent_PrinterData
        : FFTgt_B2B_GMUEventData_Primary
    {
        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_GMUEvent_DataSetIds.Printer;
            }
        }

        public byte ActionID { get; set; }
        public FF_AppId_GMUEvent_PrinterStatusCodes StatusCode { get; set; }
    }
}
