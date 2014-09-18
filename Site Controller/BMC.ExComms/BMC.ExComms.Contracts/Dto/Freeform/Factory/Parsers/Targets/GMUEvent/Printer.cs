using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_MC300_GMUEvent_Printer : FFTgtParser
    {
        internal FFParser_Tgt_MC300_GMUEvent_Printer()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_GMUEvent_PrinterData tgt = new FFTgt_G2H_GMUEvent_PrinterData();
            tgt.ActionID = buffer[0];
            tgt.StatusCode = buffer[1].GetAppId<FF_GmuId_GMUEvent_PrinterStatusCodes, FF_AppId_GMUEvent_PrinterStatusCodes>();
            return tgt;
        }
    }
}
