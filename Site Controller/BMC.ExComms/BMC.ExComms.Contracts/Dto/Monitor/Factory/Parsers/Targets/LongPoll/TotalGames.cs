using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GM_SAS_TotalGames),
                    typeof(MonTgt_G2H_LP_TotalGames),
                    (int)FaultSource.LongPoll,
                    (int)FaultType_LongPollCode.LPC_Receive_51)]
    internal class MonTgtParser_LP_TotalGames_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GM_SAS_TotalGames tgtSrc = request as FFTgt_G2H_GM_SAS_TotalGames;
            if (tgtSrc != null)
            {
                MonTgt_G2H_LP_TotalGames tgtDest = new MonTgt_G2H_LP_TotalGames()
                {
                    TotalGames = tgtSrc.TotalGames,
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GM_SAS_TotalGames),
        typeof(MonTgt_H2G_LP_TotalGames),
        (int)FaultSource.LongPoll,
        (int)FaultType_LongPollCode.LPC_Send_51,
        FF_AppId_H2G_PollCodes.FreeformNoResponse,
        FF_AppId_SessionIds.GameMessage)]
    internal class MonTgtParser_LP_TotalGames_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_LP_TotalGames tgtSrc = request as MonTgt_H2G_LP_TotalGames;
            if (tgtSrc != null)
            {
                FFTgt_H2G_GM_SAS_TotalGames tgtDest = new FFTgt_H2G_GM_SAS_TotalGames();
                return tgtDest;
            }
            return null;
        }
    }
}
