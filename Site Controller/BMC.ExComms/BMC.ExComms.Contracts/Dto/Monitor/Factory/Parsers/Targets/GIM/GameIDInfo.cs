using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GIM_GameIDInfo),
                    typeof(MonTgt_G2H_GIM_GameIDInfo),
                    (int)FaultSource.GIM_Event,
                    (int)FaultType_GIM.Game_Id_Info_G2H)]
    internal class MonTgtParser_GIM_GameIDInfo_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GIM_GameIDInfo tgtSrc = request as FFTgt_G2H_GIM_GameIDInfo;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GIM_GameIDInfo tgtDest = new MonTgt_G2H_GIM_GameIDInfo()
                {
                    GMUNumber = tgtSrc.GMUNumber,
                    AssetNumber = tgtSrc.AssetNumber,
                    ManufacturerID = tgtSrc.ManufacturerID,
                    SerialNumber = tgtSrc.SerialNumber,
                    MACAddress = tgtSrc.MACAddress,
                    SASVersion = tgtSrc.SASVersion,
                    GMUVersion = tgtSrc.GMUVersion
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GIM_GameIDInfo),
        typeof(MonTgt_H2G_GIM_GameIDInfo),
        (int)FaultSource.GIM_Event,
        (int)FaultType_GIM.Game_Id_Info_H2G,
        FF_AppId_H2G_PollCodes.FreeformNoResponse,
        FF_AppId_SessionIds.GIM)]
    internal class MonTgtParser_GIM_GameIDInfo_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GIM_GameIDInfo tgtSrc = request as MonTgt_H2G_GIM_GameIDInfo;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GIM tgtDest = new FFTgt_B2B_GIM();
                FFTgt_H2G_GIM_GameIDInfo ffTgtGameIdInfo = new FFTgt_H2G_GIM_GameIDInfo()
                {
                    SourceAddress = tgtSrc.SourceAddress,
                    AssetNumberInt = tgtSrc.AssetNumberInt,
                    PokerGamePrefix = tgtSrc.PokerGamePrefix,
                    EnableNetworkMessaging = tgtSrc.EnableNetworkMessaging,
                };

                tgtDest.AddTarget(ffTgtGameIdInfo);
                return tgtDest;
            }
            return null;
        }
    }
}
