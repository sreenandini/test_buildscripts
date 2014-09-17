using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Ticket System Slot ID
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_GVA_TSSlotID_Request),
                typeof(MonTgt_G2H_GVA_TSSlotID_Request),
                (int)FaultSource.GMUVarAction,
                (int)FaultType_GMUVarAction.TicketSystemSlotIDRequest)]
    internal class MonTgtParser_GVA_TSSlotID_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TSSlotID_Request tgtSrc = request as FFTgt_G2H_GVA_TSSlotID_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TSSlotID_Request tgtDest = new MonTgt_G2H_GVA_TSSlotID_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_TSSlotID_Response),
        typeof(MonTgt_H2G_GVA_TSSlotID_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TicketSystemSlotIDResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_GVA_TSSlotID_Request_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_TSSlotID_Response tgtSrc = request as MonTgt_H2G_GVA_TSSlotID_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_TSSlotID_Response tgtSub = new FFTgt_H2G_GVA_TSSlotID_Response()
                {
                    SlotID = tgtSrc.SlotID,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_TSSlotID_Status),
                    typeof(MonTgt_G2H_GVA_TSSlotID_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketSystemSlotIDStatus)]
    internal class MonTgtParser_GVA_TSSlotID_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TSSlotID_Status tgtSrc = request as FFTgt_G2H_GVA_TSSlotID_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TSSlotID_Status tgtDest = new MonTgt_G2H_GVA_TSSlotID_Status()
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