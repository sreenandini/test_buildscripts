using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_LP_InstantPeriodic),
        (int)FaultSource.LongPoll,
        (int)FaultType_LongPollCode.LPC_Send_InstantPeriodic,
        FF_AppId_H2G_PollCodes.Poll,
        FF_AppId_SessionIds.A1)]
    internal class MonTgtParser_LP_InstantPeriodic_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_LP_InstantPeriodic tgtSrc = request as MonTgt_H2G_LP_InstantPeriodic;
            if (tgtSrc == null) return null;

            FFTgt_H2G_LP_InstantPeriodic tgtDest = new FFTgt_H2G_LP_InstantPeriodic()
            {
                ConfiguredInterval = tgtSrc.ConfiguredInterval,
                LowerOrderInterval = tgtSrc.LowerOrderInterval,
                HigherOrderInterval = tgtSrc.HigherOrderInterval
            };

            return tgtDest;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(MonTgt_H2G_LP_InstantPeriodic),
                    (int)FaultSource.LongPoll,
                    (int)FaultType_LongPollCode.LPC_Send_InstantPeriodic)]
    internal class MonTgtParser_LP_InstantPeriodic_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_H2G_LP_InstantPeriodic tgtSrc = request as FFTgt_H2G_LP_InstantPeriodic;
            if (tgtSrc == null) return null;

            MonTgt_H2G_LP_InstantPeriodic tgtDest = new MonTgt_H2G_LP_InstantPeriodic()
            {
                ConfiguredInterval = tgtSrc.ConfiguredInterval,
                LowerOrderInterval = tgtSrc.LowerOrderInterval,
                HigherOrderInterval = tgtSrc.HigherOrderInterval
            };

            return tgtDest;
        }
    }
}
