using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GM_SAS_HandpayInfo),
        typeof(MonTgt_H2G_LP_HandpayInfo),
        (int)FaultSource.LongPoll,
        (int)FaultType_LongPollCode.LPC_Send_1B,
        FF_AppId_H2G_PollCodes.FreeformNoResponse,
        FF_AppId_SessionIds.GameMessage)]
    internal class MonTgtParser_LP_HandpayInfo_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_LP_HandpayInfo tgtSrc = request as MonTgt_H2G_LP_HandpayInfo;
            if (tgtSrc != null)
            {
                FFTgt_H2G_GM_SAS_HandpayInfo tgtDest = new FFTgt_H2G_GM_SAS_HandpayInfo();
                return tgtDest;
            }
            return null;
        }
    }
}
