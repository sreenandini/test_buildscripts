using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_GMUVarAction
        : FFTgtParser_NoSubTargets
    {
            internal FFParser_Tgt_Generic_GMUVarAction()
            : base()
        {
        }

            internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_GMUVarAction tgt = new FFTgt_B2B_GMUVarAction();
            entity = tgt;
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_GMUVarAction : FFParser_Tgt_Generic_GMUVarAction
    {
        internal FFParser_Tgt_MC300_GMUVarAction()
            : base()
        {

        }
    }

    internal class FFParser_Tgt_MC300_GMUVarAction_G2H 
        : FFParser_Tgt_MC300_GMUVarAction
    {

        internal FFParser_Tgt_MC300_GMUVarAction_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketNumber, (int)FF_AppId_GMUVarAction.TicketNumber, new FFParser_Tgt_MC300_GVA_TN_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketSystemSlotID, (int)FF_AppId_GMUVarAction.TicketSystemSlotID, new FFParser_Tgt_MC300_GVA_TSSlotID_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketPrintDate, (int)FF_AppId_GMUVarAction.TicketPrintDate, new FFParser_Tgt_MC300_GVA_TPD_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketExpirationDate, (int)FF_AppId_GMUVarAction.TicketExpirationDate, new FFParser_Tgt_MC300_GVA_TED_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketKey, (int)FF_AppId_GMUVarAction.TicketKey, new FFParser_Tgt_MC300_GVA_TK_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TimeOfDay, (int)FF_AppId_GMUVarAction.TimeOfDay, new FFParser_Tgt_MC300_GVA_TimeOfDay_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.RestrictedTicketExpirationDays, (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDays, new FFParser_Tgt_MC300_GVA_RTE_Days_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.EnablePrintingOfRestrictedTickets, (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTickets, new FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.MaximumOfflineTicketsAllowed, (int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowed, new FFParser_Tgt_MC300_GVA_MOT_Allowed_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.OfflineTicketTextLine1, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine1, new FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine1_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.OfflineTicketTextLine2, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine2, new FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine2_Req_G2H());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.CardlessPlayTimeOutRequest, (int)FF_AppId_GMUVarAction.CardlessPlayTimeOutRequest, new FFParser_Tgt_MC300_GVA_CP_TimeOut_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.IntervalRatingRequest, (int)FF_AppId_GMUVarAction.IntervalRatingRequest, new FFParser_Tgt_MC300_GVA_IR_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.BonusPointsSubtractionRequest, (int)FF_AppId_GMUVarAction.BonusPointsSubtractionRequest, new FFParser_Tgt_MC300_GVA_BPS_Req_G2H());
            
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketSystemSlotIDRequest, (int)FF_AppId_GMUVarAction.TicketSystemSlotIDRequest, new FFParser_Tgt_MC300_GVA_TSSlotID_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketPrintDateRequest, (int)FF_AppId_GMUVarAction.TicketPrintDateRequest, new FFParser_Tgt_MC300_GVA_TPD_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketExpirationDateRequest, (int)FF_AppId_GMUVarAction.TicketExpirationDateRequest, new FFParser_Tgt_MC300_GVA_TED_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketKeyRequest, (int)FF_AppId_GMUVarAction.TicketKeyRequest, new FFParser_Tgt_MC300_GVA_TK_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.LiabilityLimit, (int)FF_AppId_GMUVarAction.LiabilityLimit, new FFParser_Tgt_MC300_GVA_LiabilityLimit_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTCharacteristicsRequest, (int)FF_AppId_GMUVarAction.EFTCharacteristicsRequest, new FFParser_Tgt_MC300_GVA_EFTChar_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTCharacteristics, (int)FF_AppId_GMUVarAction.EFTCharacteristics, new FFParser_Tgt_MC300_GVA_EFTChar_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTTransactionTimeoutRequest, (int)FF_AppId_GMUVarAction.EFTTransactionTimeoutRequest, new FFParser_Tgt_MC300_GVA_EFTTrans_TO_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTWithdrawalAmountsRequest, (int)FF_AppId_GMUVarAction.EFTWithdrawalAmountsRequest, new FFParser_Tgt_MC300_GVA_EFT_Withdraw_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TimeOfDayRequest, (int)FF_AppId_GMUVarAction.TimeOfDayRequest, new FFParser_Tgt_MC300_GVA_TimeOfDay_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDaysRequest, (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDaysRequest, new FFParser_Tgt_MC300_GVA_RTE_Days_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTicketsRequest, (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTicketsRequest, new FFParser_Tgt_MC300_GVA_EnablePrint_RT_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowedRequest, (int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowedRequest, new FFParser_Tgt_MC300_GVA_Max_OLT_Allowed_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.OfflineTicketTextLine1Request, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine1Request, new FFParser_Tgt_MC300_GVA_OLT_TxtLine1_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.OfflineTicketTextLine2Request, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine2Request, new FFParser_Tgt_MC300_GVA_OLT_TxtLine2_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.MaxWithdrawParameterRequest, (int)FF_AppId_GMUVarAction.MaxWithdrawParameterRequest, new FFParser_Tgt_MC300_GVA_EFT_Max_Withdraw_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.MaxDepositParameterRequest, (int)FF_AppId_GMUVarAction.MaxDepositParameterRequest, new FFParser_Tgt_MC300_GVA_EFT_Max_Deposit_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.SDSVersionRequest, (int)FF_AppId_GMUVarAction.SDSVersionRequest, new FFParser_Tgt_MC300_GVA_SDS_Version_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherProfileRequest, (int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherProfileRequest, new FFParser_Tgt_MC300_GVA_VV_Profile_Req_G2H());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherDataRequest, (int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherDataRequest, new FFParser_Tgt_MC300_GVA_VV_Data_Req_G2H());
        }
    }

    internal class FFParser_Tgt_MC300_GMUVarAction_H2G 
        : FFParser_Tgt_MC300_GMUVarAction
    {

        internal FFParser_Tgt_MC300_GMUVarAction_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketNumber, (int)FF_AppId_GMUVarAction.TicketNumber, new FFParser_Tgt_MC300_GVA_TN_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketSystemSlotID, (int)FF_AppId_GMUVarAction.TicketSystemSlotID, new FFParser_Tgt_MC300_GVA_TSSlotID_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketPrintDate, (int)FF_AppId_GMUVarAction.TicketPrintDate, new FFParser_Tgt_MC300_GVA_TPD_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketExpirationDate, (int)FF_AppId_GMUVarAction.TicketExpirationDate, new FFParser_Tgt_MC300_GVA_TED_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TicketKey, (int)FF_AppId_GMUVarAction.TicketKey, new FFParser_Tgt_MC300_GVA_TK_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.TimeOfDay, (int)FF_AppId_GMUVarAction.TimeOfDay, new FFParser_Tgt_MC300_GVA_TimeOfDay_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.RestrictedTicketExpirationDays, (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDays, new FFParser_Tgt_MC300_GVA_RTE_Days_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.EnablePrintingOfRestrictedTickets, (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTickets, new FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.MaximumOfflineTicketsAllowed, (int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowed, new FFParser_Tgt_MC300_GVA_MOT_Allowed_Req_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.OfflineTicketTextLine1, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine1, new FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine1_Resp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_GMUVarAction.OfflineTicketTextLine2, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine2, new FFParser_Tgt_MC300_GVA_OFFTKT_TxtLine2_Resp_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.CardlessPlayTimeOutResponse, (int)FF_AppId_GMUVarAction.CardlessPlayTimeOutResponse, new FFParser_Tgt_MC300_GVA_CP_TimeOut_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.CardlessPlayTimeOutStatus, (int)FF_AppId_GMUVarAction.CardlessPlayTimeOutStatus, new FFParser_Tgt_MC300_GVA_CP_TimeOut_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.IntervalRatingResponse, (int)FF_AppId_GMUVarAction.IntervalRatingResponse, new FFParser_Tgt_MC300_GVA_IR_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.IntervalRatingStatus, (int)FF_AppId_GMUVarAction.IntervalRatingStatus, new FFParser_Tgt_MC300_GVA_IR_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.BonusPointsSubtractionResponse, (int)FF_AppId_GMUVarAction.BonusPointsSubtractionResponse, new FFParser_Tgt_MC300_GVA_BPS_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.BonusPointsSubtractionStatus, (int)FF_AppId_GMUVarAction.BonusPointsSubtractionStatus, new FFParser_Tgt_MC300_GVA_BPS_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketSystemSlotIDResponse, (int)FF_AppId_GMUVarAction.TicketSystemSlotIDResponse, new FFParser_Tgt_MC300_GVA_TSSlotID_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketSystemSlotIDStatus, (int)FF_AppId_GMUVarAction.TicketSystemSlotIDStatus, new FFParser_Tgt_MC300_GVA_TSSlotID_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketPrintDateResponse, (int)FF_AppId_GMUVarAction.TicketPrintDateResponse, new FFParser_Tgt_MC300_GVA_TPD_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketPrintDateStatus, (int)FF_AppId_GMUVarAction.TicketPrintDateStatus, new FFParser_Tgt_MC300_GVA_TPD_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketExpirationDateResponse, (int)FF_AppId_GMUVarAction.TicketExpirationDateResponse, new FFParser_Tgt_MC300_GVA_TED_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketExpirationDateStatus, (int)FF_AppId_GMUVarAction.TicketExpirationDateStatus, new FFParser_Tgt_MC300_GVA_TED_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketKeyResponse, (int)FF_AppId_GMUVarAction.TicketKeyResponse, new FFParser_Tgt_MC300_GVA_TK_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TicketKeyStatus, (int)FF_AppId_GMUVarAction.TicketKeyStatus, new FFParser_Tgt_MC300_GVA_TK_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.LiabilityLimit, (int)FF_AppId_GMUVarAction.LiabilityLimit, new FFParser_Tgt_MC300_GVA_LiabilityLimit_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTCharacteristics, (int)FF_AppId_GMUVarAction.EFTCharacteristics, new FFParser_Tgt_MC300_GVA_EFTChar_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTCharacteristicsStatus, (int)FF_AppId_GMUVarAction.EFTCharacteristicsStatus, new FFParser_Tgt_MC300_GVA_EFTChar_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTTransactionTimeoutResponse, (int)FF_AppId_GMUVarAction.EFTTransactionTimeoutResponse, new FFParser_Tgt_MC300_GVA_EFTTrans_TimeOut_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTTransactionTimeoutStatus, (int)FF_AppId_GMUVarAction.EFTTransactionTimeoutStatus, new FFParser_Tgt_MC300_GVA_EFTTrans_TimeOut_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTWithdrawalAmountsResponse, (int)FF_AppId_GMUVarAction.EFTWithdrawalAmountsResponse, new FFParser_Tgt_MC300_GVA_EFT_Withdraw_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EFTWithdrawalAmountsStatus, (int)FF_AppId_GMUVarAction.EFTWithdrawalAmountsStatus, new FFParser_Tgt_MC300_GVA_EFT_Withdraw_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TimeOfDayResponse, (int)FF_AppId_GMUVarAction.TimeOfDayResponse, new FFParser_Tgt_MC300_GVA_EFT_TimeOfDay_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TimeOfDayResponse, (int)FF_AppId_GMUVarAction.TimeOfDayResponse, new FFParser_Tgt_MC300_GVA_EFT_TimeOfDay_Resp_NACK_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.TimeOfDayStatus, (int)FF_AppId_GMUVarAction.TimeOfDayStatus, new FFParser_Tgt_MC300_GVA_EFT_TimeOfDay_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDaysResponse, (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDaysResponse, new FFParser_Tgt_MC300_GVA_RTE_Days_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDaysStatus, (int)FF_AppId_GMUVarAction.RestrictedTicketExpirationDaysStatus, new FFParser_Tgt_MC300_GVA_RTE_Days_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTicketsResponse, (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTicketsResponse, new FFParser_Tgt_MC300_GVA_EnablePrint_RT_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTicketsStatus, (int)FF_AppId_GMUVarAction.EnablePrintingOfRestrictedTicketsStatus, new FFParser_Tgt_MC300_GVA_EnablePrint_RT_Status_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowedResponse, (int)FF_AppId_GMUVarAction.MaximumOfflineTicketsAllowedResponse, new FFParser_Tgt_MC300_GVA_Max_OLT_Allowed_Resp_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.OfflineTicketTextLine1Response, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine1Response, new FFParser_Tgt_MC300_GVA_OLT_TxtLine1_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.OfflineTicketTextLine2Response, (int)FF_AppId_GMUVarAction.OfflineTicketTextLine2Response, new FFParser_Tgt_MC300_GVA_OLT_TxtLine2_Resp_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.MaxWithdrawParameterResponse, (int)FF_AppId_GMUVarAction.MaxWithdrawParameterResponse, new FFParser_Tgt_MC300_GVA_EFT_Max_Withdraw_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.MaxDepositParameterResponse, (int)FF_AppId_GMUVarAction.MaxDepositParameterResponse, new FFParser_Tgt_MC300_GVA_EFT_Max_Deposit_Resp_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.SDSVersionResponse, (int)FF_AppId_GMUVarAction.SDSVersionResponse, new FFParser_Tgt_MC300_GVA_SDS_Version_Resp_H2G());

            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherProfileResponse, (int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherProfileResponse, new FFParser_Tgt_MC300_GVA_VV_Profile_Resp_H2G());
            //this.AddBufferEntityParser((int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherDataResponse, (int)FF_AppId_GMUVarAction.EnhanceVdalidationVoucherDataResponse, new FFParser_Tgt_MC300_GVA_VV_Data_Resp_H2G());
        }
    }

    internal class FFParser_Tgt_Generic_GVA_ActionData
        : FFTgtParser
    {
        internal FFParser_Tgt_Generic_GVA_ActionData()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_GVA_ActionData
        : FFParser_Tgt_Generic_GVA_ActionData
    {
        internal FFParser_Tgt_MC300_GVA_ActionData()
            : base() { }
    }
}
