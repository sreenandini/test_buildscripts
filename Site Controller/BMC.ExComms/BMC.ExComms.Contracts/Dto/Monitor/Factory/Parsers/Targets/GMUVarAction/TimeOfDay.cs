using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Time Of Day
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_GVA_TimeOfDay_Request),
                typeof(MonTgt_G2H_GVA_TimeOfDay_Request),
                (int)FaultSource.GMUVarAction,
                (int)FaultType_GMUVarAction.TimeOfDayRequest)]
    internal class MonTgtParser_GVA_TimeOfDay_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TimeOfDay_Request tgtSrc = request as FFTgt_G2H_GVA_TimeOfDay_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TimeOfDay_Request tgtDest = new MonTgt_G2H_GVA_TimeOfDay_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_TimeOfDay_Response),
        typeof(MonTgt_H2G_GVA_TimeOfDay_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TimeOfDayResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_GVA_TimeOfDay_Request_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_TimeOfDay_Response tgtSrc = request as MonTgt_H2G_GVA_TimeOfDay_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_TimeOfDay_Response tgtSub = new FFTgt_H2G_GVA_TimeOfDay_Response()
                {
                    TimeOfDay = tgtSrc.TimeOfDay,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_TimeOfDay_Status),
                    typeof(MonTgt_G2H_GVA_TimeOfDay_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TimeOfDayStatus)]
    internal class MonTgtParser_GVA_TimeOfDay_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TimeOfDay_Status tgtSrc = request as FFTgt_G2H_GVA_TimeOfDay_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TimeOfDay_Status tgtDest = new MonTgt_G2H_GVA_TimeOfDay_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
    #endregion
}
