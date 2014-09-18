using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    internal class FFParser_Tgt_Generic_EFT_CashlessAccount :
        FFTgtParser
    {
        internal FFParser_Tgt_Generic_EFT_CashlessAccount()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_CashlessAccount
        : FFParser_Tgt_Generic_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_CashlessAccount()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_CashlessAccountLookup_G2H
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_CashlessAccountLookup_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_EFT_CashlessAccountLookup tgt = new FFTgt_G2H_EFT_CashlessAccountLookup();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetASCIIStringValue(buffer, 5, 40);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_EFT_CashlessAccountLookup tgt2 = tgt as FFTgt_G2H_EFT_CashlessAccountLookup;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_CashlessAccountVerify_H2G
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_CashlessAccountVerify_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_CashlessAccVerify tgt = new FFTgt_H2G_EFT_CashlessAccVerify();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetASCIIStringValue(buffer, 5, 40);
            tgt.ResponseStatus = buffer[45];
            tgt.PlayerAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 46, 5);
            tgt.PINCheck = buffer[51];
            tgt.AutoDownloadTopUPStatusFlag = buffer[52].GetAppId<FF_GmuId_EFT_AutoDownload_TopUp_StatusFlags, FF_AppId_EFT_AutoDownload_TopUp_StatusFlags>();
            tgt.AutoDownloadAccountType = buffer[53].GetAppId<FF_GmuId_EFT_AutoDownload_TopUp_AccountTypes, FF_AppId_EFT_AutoDownload_TopUp_AccountTypes>();
            tgt.AutoDownloadAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 54, 4);
            tgt.AutoTopUpAccountType = buffer[58].GetAppId<FF_GmuId_EFT_AutoDownload_TopUp_AccountTypes, FF_AppId_EFT_AutoDownload_TopUp_AccountTypes>();
            tgt.AutoTopUpMaxAmount = FreeformHelper.GetBytesToBCDDouble(buffer, 59, 4);
            tgt.AutoTopUpTrigger = FreeformHelper.GetBytesToBCDDouble(buffer, 63, 4);
            tgt.ErrorTextLength = FreeformHelper.GetBytesToNumberUInt8(buffer, 67, 1);
            tgt.ErrorText = FreeformHelper.GetASCIIStringValue(buffer, 68, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_CashlessAccVerify tgt2 = tgt as FFTgt_H2G_EFT_CashlessAccVerify;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
            buffer.SetValue(tgt2.ResponseStatus);
            buffer.SetBCDValue(tgt2.PlayerAccountNumber, 5);
            buffer.SetValue(tgt2.PINCheck);
            buffer.SetValue(tgt2.AutoDownloadTopUPStatusFlag.GetGmuIdInt8());
            buffer.SetValue(tgt2.AutoDownloadAccountType.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.AutoDownloadAmount, 4);
            buffer.SetValue(tgt2.AutoTopUpAccountType.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.AutoTopUpMaxAmount, 4);
            buffer.SetBCDValue(tgt2.AutoDownloadAmount, 4);
            buffer.SetBCDValue(tgt2.AutoTopUpTrigger, 4);
            buffer.SetValue(tgt2.ErrorTextLength);
            buffer.SetASCIIBytesValue(tgt2.ErrorText, 128);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_CashlessAccountNumber_H2G
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_CashlessAccountNumber_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_CashlessAccountNumber tgt = new FFTgt_H2G_EFT_CashlessAccountNumber();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetASCIIStringValue(buffer, 5, 40);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_CashlessAccountNumber tgt2 = tgt as FFTgt_H2G_EFT_CashlessAccountNumber;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_EmployeeAccountNumber_G2H
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_EmployeeAccountNumber_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_EmployeeAccountNumber tgt = new FFTgt_H2G_EFT_EmployeeAccountNumber();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 40);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_EmployeeAccountNumber tgt2 = tgt as FFTgt_H2G_EFT_EmployeeAccountNumber;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_EmployeeAccountNumber_H2G
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_EmployeeAccountNumber_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_H2G_EFT_EmployeeAccountNumber_Response tgt = new FFTgt_H2G_EFT_EmployeeAccountNumber_Response();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 40);
            tgt.ResponseStatus = buffer[45];
            tgt.PINCheck = buffer[46].GetAppId<FF_GmuId_EFT_PINCheck, FF_AppId_EFT_PINCheck>(); ;
            tgt.ErrorTextLength = FreeformHelper.GetBytesToNumberUInt8(buffer, 67, 1);
            tgt.ErrorText = FreeformHelper.GetASCIIStringValue(buffer, 68, 128);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_H2G_EFT_EmployeeAccountNumber_Response tgt2 = tgt as FFTgt_H2G_EFT_EmployeeAccountNumber_Response;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
            buffer.SetValue(tgt2.ResponseStatus);
            buffer.SetValue(tgt2.PINCheck.GetGmuIdInt8());
            buffer.SetValue(tgt2.ErrorTextLength);
            buffer.SetASCIIBytesValue(tgt2.ErrorText, 128);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SC_VerifyBalance_B2B
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_SC_VerifyBalance_B2B()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_EFT_SC_VerifyBalance tgt = new FFTgt_B2B_EFT_SC_VerifyBalance();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.Status = buffer[4].GetAppId<FF_GmuId_EFT_BalanceVerify_Status, FF_AppId_EFT_BalanceVerify_Status>();
            tgt.PlayerAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 40);
            tgt.Balance1 = FreeformHelper.GetBytesToNumberDouble(buffer, 45, 4);
            tgt.Balance2 = FreeformHelper.GetBytesToNumberDouble(buffer, 49, 4);
            tgt.Balance3 = FreeformHelper.GetBytesToNumberDouble(buffer, 53, 4);
            tgt.Balance4 = FreeformHelper.GetBytesToNumberDouble(buffer, 57, 4);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_B2B_EFT_SC_VerifyBalance tgt2 = tgt as FFTgt_B2B_EFT_SC_VerifyBalance;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetValue(tgt2.Status.GetGmuIdInt8());
            buffer.SetBCDValue(tgt2.PlayerAccountNumber, 5);
            buffer.SetValue(tgt2.Balance1, 4);
            buffer.SetValue(tgt2.Balance2, 4);
            buffer.SetValue(tgt2.Balance3, 4);
            buffer.SetValue(tgt2.Balance4, 4);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SC_VerifyBalance_G2H
        : FFParser_Tgt_MC300_EFT_SC_VerifyBalance_B2B
    {
        internal FFParser_Tgt_MC300_EFT_SC_VerifyBalance_G2H()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_SC_VerifyBalance_H2G
        : FFParser_Tgt_MC300_EFT_SC_VerifyBalance_B2B
    {
        internal FFParser_Tgt_MC300_EFT_SC_VerifyBalance_H2G()
            : base() { }
    }

    internal class FFParser_Tgt_MC300_EFT_SC_Trans_Update_G2H
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_SC_Trans_Update_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_G2H_SC_Transaction_Update tgt = new FFTgt_G2H_SC_Transaction_Update();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.Status = buffer[4].GetAppId<FF_GmuId_EFT_SC_Tranaction_Update_Status, FF_AppId_EFT_SC_Tranaction_Update_Status>();
            tgt.AccountType = buffer[5].GetAppId<FF_GmuId_EFT_SC_Tranaction_Update_AccTypes, FF_AppId_EFT_SC_Tranaction_Update_AccTypes>();
            tgt.TransactionAmount = FreeformHelper.GetBytesToNumberDouble(buffer, 5, 4);
            tgt.PlayerCardNumber = FreeformHelper.GetBCDValueString(buffer, 0, 9, 5);
            tgt.TransTimestamp = FreeformHelper.GetBytesToNumberTimeSpan(buffer, 13, 2);
            tgt.TransactionID = buffer[18];
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_G2H_SC_Transaction_Update tgt2 = tgt as FFTgt_G2H_SC_Transaction_Update;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetValue(tgt2.Status.GetGmuIdInt8());
            buffer.SetValue(tgt2.AccountType.GetGmuIdInt8());
            buffer.SetValue(tgt2.TransactionAmount, 4);
            buffer.SetBCDValue(tgt2.PlayerCardNumber, 5);
            buffer.SetValue(tgt2.TransTimestamp, 2);
            buffer.SetValue(tgt2.TransactionID);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SC_SerailNumber_G2H
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_SC_SerailNumber_G2H()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_EFT_SC_SerailNumber tgt = new FFTgt_B2B_EFT_SC_SerailNumber();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 40);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_B2B_EFT_SC_SerailNumber tgt2 = tgt as FFTgt_B2B_EFT_SC_SerailNumber;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
        }
    }

    internal class FFParser_Tgt_MC300_EFT_SC_SerailNumber_H2G
        : FFParser_Tgt_MC300_EFT_CashlessAccount
    {
        internal FFParser_Tgt_MC300_EFT_SC_SerailNumber_H2G()
            : base() { }

        internal override IFreeformEntity ParseBufferInternal(ref IFreeformEntity entity, IFreeformEntity rootEntity, int id, byte[] buffer)
        {
            FFTgt_B2B_EFT_SC_SerailNumber tgt = new FFTgt_B2B_EFT_SC_SerailNumber();
            tgt.EGMAssetNumber = FreeformHelper.GetBytesToNumberInt32(buffer, 0, 4);
            tgt.CashlessAccountNumberLength = buffer[4];
            tgt.CashlessAccountNumber = FreeformHelper.GetBCDValueString(buffer, 0, 5, 40);
            return tgt;
        }

        public override void GetTargetData(IFreeformEntity_MsgTgt tgt, ref List<byte> buffer)
        {
            FFTgt_B2B_EFT_SC_SerailNumber tgt2 = tgt as FFTgt_B2B_EFT_SC_SerailNumber;
            buffer.SetValue(tgt2.EGMAssetNumber, 4);
            buffer.SetASCIIBytesValueWithLength(tgt2.CashlessAccountNumber, 40);
        }
    }
}
