using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                     typeof(FFTgt_G2H_GVA_EFT_CHAR_Request),
                     (int)FaultSource.GMUVarAction,
                     (int)FaultType_GMUVarAction.EFTCharacteristicsRequest)]
    internal class MonTgtParser_GVA_EFTCHAR_Request_G2H
    : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_GVA_EFT_CHAR_Request tgtSrc = request as FFTgt_G2H_GVA_EFT_CHAR_Request;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFTCHAR_Request tgtDest = new MonTgt_G2H_GVA_EFTCHAR_Request();
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingH2G(FF_FlowDirection.H2G,
        typeof(FFTgt_H2G_GVA_EFT_Char_Response),
        (int)FaultSource.GMUVarAction,
        (int)FaultType_GMUVarAction.EFTCharacteristics,
        FF_AppId_H2G_PollCodes.FreeformResponse,
        FF_AppId_SessionIds.A1)]

    internal class MonTgtParser_GVA_EFTCHARS_Request_H2G
       : MonTgtParser_H2G
    {
        protected override IFreeformEntity_MsgTgt CreateFreeformTarget(IMonitorEntity_MsgTgt request)
        {
            MonTgt_H2G_GVA_EFTCHAR_Response tgtSrc = request as MonTgt_H2G_GVA_EFTCHAR_Response;
            if (tgtSrc != null)
            {
                FFTgt_B2B_GMUVarAction tgtDest = new FFTgt_B2B_GMUVarAction();
                FFTgt_H2G_GVA_EFT_Char_Response tgtSub = new FFTgt_H2G_GVA_EFT_Char_Response()
                {
                    IsAutoDepositEnabledForCashableOnCardOut = tgtSrc.IsAutoDepositEnabledForCashableOnCardOut,
                    IsAutoDepositEnabledForNonCashableOnCardOut=tgtSrc.IsAutoDepositEnabledForNonCashableOnCardOut,
                    IsAutoDownloadEnabled=tgtSrc.IsAutoDownloadEnabled,
                    IsAutoTopEnabled=tgtSrc.IsAutoTopEnabled,
                    IsCashableDpositEnabled=tgtSrc.IsCashableDpositEnabled,
                    IsCashlessSmartCardEnabled=tgtSrc.IsCashlessSmartCardEnabled,
                    ISCashWithdrawalEnabled=tgtSrc.ISCashWithdrawalEnabled,
                    IsEftEnabled=tgtSrc.IsEftEnabled,
                    IsFullDownloadEnabled=tgtSrc.IsFullDownloadEnabled,
                    IsNon_CashableDepositEnabled=tgtSrc.IsNon_CashableDepositEnabled,
                    IsOfferEnabled=tgtSrc.IsOfferEnabled,
                    IsPartialTransferEnabled=tgtSrc.IsPartialTransferEnabled,
                    IsPointsWithdrawalEnabled=tgtSrc.IsPointsWithdrawalEnabled
                };

                tgtDest.AddTarget(tgtSub);
                return tgtDest;
            }
            return null;
        }
    }

    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_TPD_Status),
                    (int)FaultSource.GMUVarAction,
                    (int)FaultType_GMUVarAction.TicketPrintDateStatus)]
    internal class MonTgtParser_GVA_EFTCHARS_Status_G2H
        : MonTgtParser_G2H
    {
        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            FFTgt_G2H_EFT_CHAR_Status tgtSrc = request as FFTgt_G2H_EFT_CHAR_Status;
            if (tgtSrc != null)
            {
                MonTgt_G2H_GVA_EFTChar_Status tgtDest = new MonTgt_G2H_GVA_EFTChar_Status()
                {
                    Status = tgtSrc.Status,
                };
                return tgtDest;
            }
            return null;
        }
    }
}
