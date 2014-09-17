using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Ticket Number
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_GVA_TN_Request),
                typeof(MonTgt_G2H_GVA_TN_Request),
                (int)FaultSource.GMUVarAction,
                (int)FaultType_GMUVarAction.TicketNumberRequest)]
    internal class MonTgtParser_GVA_TN_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TN_Request tgtSrc = request as FFTgt_G2H_GVA_TN_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TN_Request tgtDest = new MonTgt_G2H_GVA_TN_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_TN_Response),
        typeof(MonTgt_H2G_GVA_TN_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TicketNumberResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_GVA_TN_Request_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_TN_Response tgtSrc = request as MonTgt_H2G_GVA_TN_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction()
                {
                    ActionData = new FFTgt_H2G_GVA_TN_Response()
                    {
                        TicketNumber = tgtSrc.TicketNumber,
                    }
                };
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_TN_Status),
                    typeof(MonTgt_G2H_GVA_TN_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketNumberStatus)]
    internal class MonTgtParser_GVA_TN_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TN_Status tgtSrc = request as FFTgt_G2H_GVA_TN_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TN_Status tgtDest = new MonTgt_G2H_GVA_TN_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
    #endregion

    #region Ticket Print Date
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_GVA_TPD_Request),
                typeof(MonTgt_G2H_GVA_TPD_Request),
                (int)FaultSource.GMUVarAction,
                (int)FaultType_GMUVarAction.TicketPrintDateRequest)]
    internal class MonTgtParser_GVA_TPD_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TPD_Request tgtSrc = request as FFTgt_G2H_GVA_TPD_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TPD_Request tgtDest = new MonTgt_G2H_GVA_TPD_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_TPD_Response),
        typeof(MonTgt_H2G_GVA_TPD_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TicketPrintDateResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_GVA_TPD_Request_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_TPD_Response tgtSrc = request as MonTgt_H2G_GVA_TPD_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_TPD_Response tgtSub = new FFTgt_H2G_GVA_TPD_Response()
                {
                    Date = tgtSrc.Date,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_TPD_Status),
                    typeof(MonTgt_G2H_GVA_TPD_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketPrintDateStatus)]
    internal class MonTgtParser_GVA_TPD_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TPD_Status tgtSrc = request as FFTgt_G2H_GVA_TPD_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TPD_Status tgtDest = new MonTgt_G2H_GVA_TPD_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
    #endregion

    #region Ticket Expiration Date
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_GVA_TED_Request),
                typeof(MonTgt_G2H_GVA_TED_Request),
                (int)FaultSource.GMUVarAction,
                (int)FaultType_GMUVarAction.TicketExpirationDateRequest)]
    internal class MonTgtParser_GVA_TED_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TED_Request tgtSrc = request as FFTgt_G2H_GVA_TED_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TED_Request tgtDest = new MonTgt_G2H_GVA_TED_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_TED_Response),
        typeof(MonTgt_H2G_GVA_TED_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TicketExpirationDateResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_GVA_TED_Request_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_TED_Response tgtSrc = request as MonTgt_H2G_GVA_TED_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_TED_Response tgtSub = new FFTgt_H2G_GVA_TED_Response()
                {
                    Date = tgtSrc.Date,
                    ExipreDays = tgtSrc.ExipreDays,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_TED_Status),
                    typeof(MonTgt_G2H_GVA_TED_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketExpirationDateStatus)]
    internal class MonTgtParser_GVA_TED_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TED_Status tgtSrc = request as FFTgt_G2H_GVA_TED_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TED_Status tgtDest = new MonTgt_G2H_GVA_TED_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
    #endregion

    #region Ticket Key
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                typeof(FFTgt_G2H_GVA_TK_Request),
                typeof(MonTgt_G2H_GVA_TK_Request),
                (int)FaultSource.GMUVarAction,
                (int)FaultType_GMUVarAction.TicketKeyRequest)]
    internal class MonTgtParser_GVA_TK_Request_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TK_Request tgtSrc = request as FFTgt_G2H_GVA_TK_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TK_Request tgtDest = new MonTgt_G2H_GVA_TK_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_TK_Response),
        typeof(MonTgt_H2G_GVA_TK_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.TicketKeyResponse,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.Tickets)]
    internal class MonTgtParser_GVA_TK_Request_H2G
        : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity parent, IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_TK_Response tgtSrc = request as MonTgt_H2G_GVA_TK_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_TK_Response tgtSub = new FFTgt_H2G_GVA_TK_Response()
                {
                    BarcodeKey = tgtSrc.BarcodeKey,
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_GVA_TK_Status),
                    typeof(MonTgt_G2H_GVA_TK_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketKeyStatus)]
    internal class MonTgtParser_GVA_TK_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_TK_Status tgtSrc = request as FFTgt_G2H_GVA_TK_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_TK_Status tgtDest = new MonTgt_G2H_GVA_TK_Status()
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
