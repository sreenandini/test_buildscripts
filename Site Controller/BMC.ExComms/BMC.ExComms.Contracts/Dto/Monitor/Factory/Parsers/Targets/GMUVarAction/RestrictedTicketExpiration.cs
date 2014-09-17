using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor.Factory
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                     typeof(FFTgt_G2H_RTE_Days_Request),
                     (int)FaultSource.GMUVarAction,
                     (int)FaultType_GMUVarAction.RestrictedTicketExpirationDaysRequest)]
    internal class MonTgtParser_GVA_RTE_Request_G2H
    : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_RTE_Days_Request tgtSrc = request as FFTgt_G2H_RTE_Days_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_RET_Request tgtDest = new MonTgt_G2H_GVA_RET_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
     typeof(FFTgt_H2G_RTE_Days_Response),
     (int)FaultSource.GMUVarAction,
     (int)FaultType_GMUVarAction.RestrictedTicketExpirationDaysResponse,
     FF_AppId_H2G_PollCodes.FreeformResponse,
     FF_AppId_SessionIds.A1)]

    internal class MonTgtParser_GVA_RTE_Request_H2G
       : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_RET_Response tgtSrc = request as MonTgt_H2G_GVA_RET_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_RTE_Days_Response tgtSub = new FFTgt_H2G_RTE_Days_Response()
                {
                 ExpirationDays=tgtSrc.ExpirationDays
                
};

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                   typeof(FFTgt_G2H_RTE_Days_Status),
                   (int)FaultSource.GMUVarAction,
                   (int)FaultType_GMUVarAction.RestrictedTicketExpirationDaysStatus)]
    internal class MonTgtParser_GVA_RTE_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_RTE_Days_Status tgtSrc = request as FFTgt_G2H_RTE_Days_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_RET_Status tgtDest = new MonTgt_G2H_GVA_RET_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }

  
}
