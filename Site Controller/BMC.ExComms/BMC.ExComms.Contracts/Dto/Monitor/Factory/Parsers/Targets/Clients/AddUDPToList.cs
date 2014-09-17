using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_Client_AddUDPToList),
        typeof(MonTgt_H2G_Client_AddUDPToList),
        (int)FaultSource.Client,
        (int)FaultType_Client.AddUDPToList,
        FF_AppId_H2G_PollCodes.Internal,
        FF_AppId_SessionIds.Internal)]
    internal class MonTgtParser_Client_AddUDPToList_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Client_AddUDPToList tgtSrc = request as MonTgt_H2G_Client_AddUDPToList;
            if (tgtSrc != null)
            {
                FFTgt_B2B_Client tgtDest = new FFTgt_B2B_Client();
                FFTgt_H2G_Client_AddUDPToList ffTgtGameIdInfo = new FFTgt_H2G_Client_AddUDPToList()
                {
                    ServerIP = tgtSrc.ServerIP,
                    Port = tgtSrc.Port,
                    PollingID = tgtSrc.PollingID,
                    Type = tgtSrc.Type,
                    InstallationNo = tgtSrc.InstallationNo,
                };

                tgtDest.AddTarget(ffTgtGameIdInfo);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_Client_AddUDPsToList),
        typeof(MonTgt_H2G_Client_AddUDPsToList),
        (int)FaultSource.Client,
        (int)FaultType_Client.AddUDPsToList,
        FF_AppId_H2G_PollCodes.Internal,
        FF_AppId_SessionIds.Internal)]
    internal class MonTgtParser_Client_AddUDPsToList_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_Client_AddUDPsToList tgtSrc = request as MonTgt_H2G_Client_AddUDPsToList;
            if (tgtSrc != null)
            {
                FFTgt_B2B_Client tgtDest = new FFTgt_B2B_Client();
                FFTgt_H2G_Client_AddUDPsToList ffTgtGameIdInfo = new FFTgt_H2G_Client_AddUDPsToList()
                {
                    InstallationNos = tgtSrc.InstallationNos,
                };

                tgtDest.AddTarget(ffTgtGameIdInfo);
                return tgtDest;
            }
            return null;
        }
    }
}
