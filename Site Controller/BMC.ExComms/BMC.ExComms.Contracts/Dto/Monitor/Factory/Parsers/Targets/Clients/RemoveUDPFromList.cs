using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_Client_RemoveUDPFromList),
        typeof(MonTgt_H2G_Client_RemoveUDPFromList),
        (int)FaultSource.Client,
        (int)FaultType_Client.RemoveUDPFromList,
        FF_AppId_H2G_PollCodes.Internal,
        FF_AppId_SessionIds.Internal)]
    internal class MonTgtParser_Client_RemoveUDPFromList_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Client_RemoveUDPFromList tgtSrc = request as MonTgt_H2G_Client_RemoveUDPFromList;
            if (tgtSrc != null)
            {
                FFTgt_B2B_Client tgtDest = new FFTgt_B2B_Client();
                FFTgt_H2G_Client_RemoveUDPFromList ffTgtGameIdInfo = new FFTgt_H2G_Client_RemoveUDPFromList()
                {
                    InstallationNo = parent.InstallationNo,
                };

                tgtDest.AddTarget(ffTgtGameIdInfo);
                return tgtDest;
            }
            return null;
        }
    }
}
