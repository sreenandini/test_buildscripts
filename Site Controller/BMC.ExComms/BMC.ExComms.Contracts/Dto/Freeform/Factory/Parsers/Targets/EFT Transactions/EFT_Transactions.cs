using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT
        : FFTgtParser_NoSubTargets
    {
        internal FFParser_Tgt_Generic_EFT()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_EFT tgt = new FFTgt_B2B_EFT();
            entity = tgt;
            this.ParseBuffer(tgt, rootEntity, buffer, 0, buffer.Length);
            return tgt;
        }
    }

    internal class FFParser_Tgt_MC300_EFT
        : FFParser_Tgt_Generic_EFT
    {
        internal FFParser_Tgt_MC300_EFT()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_G2H : FFParser_Tgt_MC300_EFT
    {
        internal FFParser_Tgt_MC300_EFT_G2H()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.WithdrawalRequest, (int)FF_AppId_EFT_TransactionTypes.WithdrawalRequest, new FFParser_Tgt_MC300_EFT_WithdrawalReq_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.WithdrawalComplete, (int)FF_AppId_EFT_TransactionTypes.WithdrawalComplete, new FFParser_Tgt_MC300_EFT_WithdrawalComplete_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.DepositRequest, (int)FF_AppId_EFT_TransactionTypes.DepositRequest, new FFParser_Tgt_MC300_EFT_DepositReq_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.DepositComplete, (int)FF_AppId_EFT_TransactionTypes.DepositComplete, new FFParser_Tgt_MC300_EFT_DepositComplete_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.BalanceRequest, (int)FF_AppId_EFT_TransactionTypes.BalanceRequest, new FFParser_Tgt_MC300_EFT_BalanceReq_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.OfferListRequest, (int)FF_AppId_EFT_TransactionTypes.OfferListRequest, new FFParser_Tgt_MC300_EFT_OfferList_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.OfferRequest, (int)FF_AppId_EFT_TransactionTypes.OfferRequest, new FFParser_Tgt_MC300_EFT_OfferReq_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.PINCheckRequest, (int)FF_AppId_EFT_TransactionTypes.PINCheckRequest, new FFParser_Tgt_MC300_EFT_PinChkReq_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.PINChangeRequest, (int)FF_AppId_EFT_TransactionTypes.PINChangeRequest, new FFParser_Tgt_MC300_EFT_PinChangeReq_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.Phishing, (int)FF_AppId_EFT_TransactionTypes.Phishing, new FFParser_Tgt_MC300_EFT_Phishing_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.CashlessAccountLookup, (int)FF_AppId_EFT_TransactionTypes.CashlessAccountLookup, new FFParser_Tgt_MC300_EFT_CashlessAccountLookup_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.iButtonAccountNumber, (int)FF_AppId_EFT_TransactionTypes.iButtonAccountNumber, new FFParser_Tgt_MC300_EFT_EmployeeAccountNumber_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SmartcardVerifyBalance, (int)FF_AppId_EFT_TransactionTypes.SmartcardVerifyBalance, new FFParser_Tgt_MC300_EFT_SC_VerifyBalance_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SmartcardTransactionUpdate, (int)FF_AppId_EFT_TransactionTypes.SmartcardTransactionUpdate, new FFParser_Tgt_MC300_EFT_SC_Trans_Update_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SmartcardSerialNumber, (int)FF_AppId_EFT_TransactionTypes.SmartcardSerialNumber, new FFParser_Tgt_MC300_EFT_SC_SerailNumber_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.AutoDownloadSet_EnableAmountRqst, (int)FF_AppId_EFT_TransactionTypes.AutoDownloadSet_EnableAmountRqst, new FFParser_Tgt_MC300_EFT_AD_Amount_Request_G2H());
            //this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.AutoTopUpSet_EnableAmountRqst, (int)FF_AppId_EFT_TransactionTypes.AutoTopUpSet_EnableAmountRqst, new FFParser_Tgt_MC300_EFT_ATU_Amount_Request_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SystemEnableEFT, (int)FF_AppId_EFT_TransactionTypes.SystemEnableEFT, new FFParser_Tgt_MC300_EFT_SysEnable_G2H());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SystemDisableEFT, (int)FF_AppId_EFT_TransactionTypes.SystemDisableEFT, new FFParser_Tgt_MC300_EFT_SysDisable_G2H());
        }
    }

    internal class FFParser_Tgt_MC300_EFT_H2G : FFParser_Tgt_MC300_TicketInfo
    {

        internal FFParser_Tgt_MC300_EFT_H2G()
            : base()
        {
            this.AddSubParsers();
        }

        protected override void AddSubParsersInternal()
        {
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.WithdrawalAuthorization, (int)FF_AppId_EFT_TransactionTypes.WithdrawalAuthorization, new FFParser_Tgt_MC300_EFT_WithdrawalAuth_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.WithdrawalAcknowledgement, (int)FF_AppId_EFT_TransactionTypes.WithdrawalAcknowledgement, new FFParser_Tgt_MC300_EFT_WithdrawalAck_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.WithdrawalAuthorization2, (int)FF_AppId_EFT_TransactionTypes.WithdrawalAuthorization2, new FFParser_Tgt_MC300_EFT_WithdrawalAuth2_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.DepositAuthorization, (int)FF_AppId_EFT_TransactionTypes.DepositAuthorization, new FFParser_Tgt_MC300_EFT_DepositAuth_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.DepositAcknowledgement, (int)FF_AppId_EFT_TransactionTypes.DepositAcknowledgement, new FFParser_Tgt_MC300_EFT_DepositAck_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.BalanceResponse, (int)FF_AppId_EFT_TransactionTypes.BalanceResponse, new FFParser_Tgt_MC300_EFT_BalanceResp_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SystemEnableEFT, (int)FF_AppId_EFT_TransactionTypes.SystemEnableEFT, new FFParser_Tgt_MC300_EFT_SysEnable_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SystemDisableEFT, (int)FF_AppId_EFT_TransactionTypes.SystemDisableEFT, new FFParser_Tgt_MC300_EFT_SysDisable_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.PlayerEnableEFT, (int)FF_AppId_EFT_TransactionTypes.PlayerEnableEFT, new FFParser_Tgt_MC300_EFT_PlayerEnable_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.OfferListReply, (int)FF_AppId_EFT_TransactionTypes.OfferListReply, new FFParser_Tgt_MC300_EFT_OfferList_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.PINCheckReply, (int)FF_AppId_EFT_TransactionTypes.PINCheckReply, new FFParser_Tgt_MC300_EFT_PinChkReply_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.PINChangeResponse, (int)FF_AppId_EFT_TransactionTypes.PINChangeResponse, new FFParser_Tgt_MC300_EFT_PinChangeResponse_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.CashlessAccountVerify, (int)FF_AppId_EFT_TransactionTypes.CashlessAccountVerify, new FFParser_Tgt_MC300_EFT_CashlessAccountVerify_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.CashlessAccountAccountNumber, (int)FF_AppId_EFT_TransactionTypes.CashlessAccountAccountNumber, new FFParser_Tgt_MC300_EFT_CashlessAccountNumber_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SmartcardVerifyBalance, (int)FF_AppId_EFT_TransactionTypes.SmartcardVerifyBalance, new FFParser_Tgt_MC300_EFT_SC_VerifyBalance_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.SmartcardSerialNumber, (int)FF_AppId_EFT_TransactionTypes.SmartcardSerialNumber, new FFParser_Tgt_MC300_EFT_SC_SerailNumber_H2G());
            this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.iButtonAccountNumberResponse, (int)FF_AppId_EFT_TransactionTypes.iButtonAccountNumberResponse, new FFParser_Tgt_MC300_EFT_EmployeeAccountNumber_H2G());
            //this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.AutoDownloadSet_EnableAmountResp, (int)FF_AppId_EFT_TransactionTypes.AutoDownloadSet_EnableAmountResp, new FFParser_Tgt_MC300_EFT_AD_Amount_Response_H2G());
            //this.AddBufferEntityParser((int)FF_GmuId_EFT_TransactionTypes.AutoTopUpSet_EnableAmountResp, (int)FF_AppId_EFT_TransactionTypes.AutoTopUpSet_EnableAmountResp, new FFParser_Tgt_MC300_EFT_ATU_Amount_Response_H2G());
        }
    }
}
