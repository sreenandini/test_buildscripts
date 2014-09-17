using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GM_SAS_GetGameInfo),
        typeof(MonTgt_H2G_LP_GetGameInfo),
        (int)FaultSource.LongPoll,
        (int)FaultType_LongPollCode.LPC_Send_53,
        FF_AppId_H2G_PollCodes.FreeformNoResponse,
        FF_AppId_SessionIds.GameMessage)]
    internal class MonTgtParser_LP_GetGameInfo_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_LP_GetGameInfo tgtSrc = request as MonTgt_H2G_LP_GetGameInfo;
            if (tgtSrc != null)
            {
                FFTgt_H2G_GM_SAS_GetGameInfo tgtDest = new FFTgt_H2G_GM_SAS_GetGameInfo();
                return tgtDest;
            }
            return null;
        }
    }
}
