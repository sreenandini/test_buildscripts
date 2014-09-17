using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_SystemEnable),
                    typeof(MonTgt_G2H_EFT_SystemEnable),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.EnableEFT)]
    internal class MonTgtParser_EFT_SystemEnable_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_SystemEnable ffTgt = request as FFTgt_G2H_EFT_SystemEnable;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_SystemEnable monTgt = new MonTgt_G2H_EFT_SystemEnable()
                {
                   ResponseType = ffTgt.ResponseType,
                };
                return monTgt;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
       typeof(FFTgt_H2G_EFT_SystemEnable),
       typeof(MonTgt_H2G_EFT_SystemEnable),
       (int)FaultSource.ECash,
       (int)FaultType_ECash.EnableEFT,
       FF_AppId_H2G_PollCodes.FreeformNoResponse,
       FF_AppId_SessionIds.ECash)]
    internal class MonTgtParser_EFT_SystemEnable_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_EFT_SystemEnable tgtSrc = request as MonTgt_H2G_EFT_SystemEnable;
            if (tgtSrc != null)
            {
                FFTgt_B2B_EFT tgtDest = new FFTgt_B2B_EFT();
                FFTgt_H2G_EFT_SystemEnable ffTgtGameIdInfo = new FFTgt_H2G_EFT_SystemEnable();
                tgtDest.AddTarget(ffTgtGameIdInfo);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_EFT_SystemDisable),
                    typeof(MonTgt_G2H_EFT_SystemDisable),
                    (int)FaultSource.ECash,
                    (int)FaultType_ECash.DisableEFT)]
    internal class MonTgtParser_EFT_SystemDisable_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_SystemDisable ffTgt = request as FFTgt_G2H_EFT_SystemDisable;
            if (ffTgt != null)
            {
                MonTgt_G2H_EFT_SystemDisable monTgt = new MonTgt_G2H_EFT_SystemDisable()
                {
                    ResponseType = ffTgt.ResponseType,
                };
                return monTgt;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
       typeof(FFTgt_H2G_EFT_SystemDisable),
       typeof(MonTgt_H2G_EFT_SystemDisable),
       (int)FaultSource.ECash,
       (int)FaultType_ECash.DisableEFT,
       FF_AppId_H2G_PollCodes.FreeformNoResponse,
       FF_AppId_SessionIds.ECash)]
    internal class MonTgtParser_EFT_SystemDisable_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_EFT_SystemDisable tgtSrc = request as MonTgt_H2G_EFT_SystemDisable;
            if (tgtSrc != null)
            {
                FFTgt_B2B_EFT tgtDest = new FFTgt_B2B_EFT();
                FFTgt_H2G_EFT_SystemDisable ffTgtGameIdInfo = new FFTgt_H2G_EFT_SystemDisable();
                tgtDest.AddTarget(ffTgtGameIdInfo);
                return tgtDest;
            }
            return null;
        }
    }
}
