using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    internal abstract class MonTgtParser_Status_G2H
        : MonTgtParser_G2H
    {
    }

    internal abstract class MonTgtParser_Status_StdData_G2H
        : MonTgtParser_Status_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent,
            IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GMUEvent_StdData tgtSrc = request as FFTgt_G2H_GMUEvent_StdData;
            if (tgtSrc != null)
            {
                return this.CreateMonitorStatusTarget(tgtSrc);
            }
            return null;
        }

        protected abstract IMonitorEntity_MsgTgt CreateMonitorStatusTarget(FFTgt_G2H_GMUEvent_StdData tgtSrc);
    }
}
