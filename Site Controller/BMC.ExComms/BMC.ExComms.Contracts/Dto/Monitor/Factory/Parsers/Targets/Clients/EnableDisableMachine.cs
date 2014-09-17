using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GM_SAS_EnableDisable),
        typeof(MonTgt_H2G_Client_EnableDisableMachine),
        (int)FaultSource.Client,
        (int)FaultType_Client.EnableDisableMachine,
        FF_AppId_H2G_PollCodes.FreeformNoResponse,
        FF_AppId_SessionIds.GameMessage)]
    internal class MonTgtParser_Client_EnableDisbleMachine_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Client_EnableDisableMachine tgtSrc = request as MonTgt_H2G_Client_EnableDisableMachine;
            if (tgtSrc != null)
            {
                FFTgt_H2G_GM_SAS_EnableDisable tgtDest = new FFTgt_H2G_GM_SAS_EnableDisable()
                {
                    EnableDisable = tgtSrc.EnableDisable,
                };

                return tgtDest;
            }
            return null;
        }
    }
}
