using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
      Name = "MonTgt_B2B_EFT_AssetandAccountDetails")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_EFT_AssetandAccountDetails
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_B2B_EFT_AssetandAccountDetails() { }

        [DataMember]
        public int EGMAssetNumber{ get; set; }

        [DataMember]
        public byte CashlessAccountNumberLength { get; set; }

        [DataMember]
        public string CashlessAccountNumber { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_CashlessAccountLookup")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_CashlessAccountLookup
        : MonTgt_B2B_EFT_AssetandAccountDetails, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_CashlessAccountLookup()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_CashlessAccVerify")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_CashlessAccVerify
        : MonTgt_B2B_EFT_AssetandAccountDetails, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_CashlessAccVerify()
        {
        }

        [DataMember]
        public byte ResponseStatus { get; set; }

        [DataMember]
        public string PlayerAccountNumber { get; set; }

        [DataMember]
        public byte PINCheck { get; set; }

        [DataMember]
        public FF_AppId_EFT_AutoDownload_TopUp_StatusFlags AutoDownloadTopUPStatusFlag { get; set; }

        [DataMember]
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AutoDownloadAccountType { get; set; }

        [DataMember]
        public double AutoDownloadAmount { get; set; }

        [DataMember]
        public double AutoTopUpTrigger { get; set; }

        [DataMember]
        public byte ErrorTextLength { get; set; }

        [DataMember]
        public string ErrorText { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_CashlessAccountNumber")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_CashlessAccountNumber
        : MonTgt_B2B_EFT_AssetandAccountDetails, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_CashlessAccountNumber()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_EmployeeAccountNumber")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_EmployeeAccountNumber
        : MonTgt_B2B_EFT_AssetandAccountDetails, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_EmployeeAccountNumber()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_SC_VerifyBalance")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_SC_VerifyBalance
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_SC_VerifyBalance()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public FF_AppId_EFT_BalanceVerify_Status Status { get; set; }

        [DataMember]
        public string PlayerAccountNumber { get; set; }

        [DataMember]
        public double Balance1 { get; set; }

        [DataMember]
        public double Balance2 { get; set; }

        [DataMember]
        public double Balance3 { get; set; }

        [DataMember]
        public double Balance4 { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_SC_VerifyBalance")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_SC_VerifyBalance
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_SC_VerifyBalance()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public FF_AppId_EFT_BalanceVerify_Status Status { get; set; }

        [DataMember]
        public string PlayerAccountNumber { get; set; }

        [DataMember]
        public double Balance1 { get; set; }

        [DataMember]
        public double Balance2 { get; set; }

        [DataMember]
        public double Balance3 { get; set; }

        [DataMember]
        public double Balance4 { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_SC_Transaction_Update")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_SC_Transaction_Update
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_SC_Transaction_Update()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public FF_AppId_EFT_SC_Tranaction_Update_Status Status { get; set; }

        [DataMember]
        public FF_AppId_EFT_SC_Tranaction_Update_AccTypes AccountType { get; set; }

        [DataMember]
        public double TransactionAmount { get; set; }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public TimeSpan TransTimestamp { get; set; }

        [DataMember]
        public byte TransactionID { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_SC_Transaction_Update")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_SC_Transaction_Update
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_SC_Transaction_Update()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public FF_AppId_EFT_SC_Tranaction_Update_Status Status { get; set; }

        [DataMember]
        public FF_AppId_EFT_SC_Tranaction_Update_AccTypes AccountType { get; set; }

        [DataMember]
        public double TransactionAmount { get; set; }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public TimeSpan TransTimestamp { get; set; }

        [DataMember]
        public byte TransactionID { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_SC_SerailNumber")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_SC_SerailNumber
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_SC_SerailNumber()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public byte SmartCardSNLen { get; set; }

        [DataMember]
        public string SmartCardSN { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_SC_SerailNumber")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_SC_SerailNumber
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_SC_SerailNumber()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public byte SmartCardSNLen { get; set; }

        [DataMember]
        public string SmartCardSN { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_EmployeeAccountNumber_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_EmployeeAccountNumber_Response
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_EmployeeAccountNumber_Response()
        {
        }

        [DataMember]
        public int EGMAssetNumber { get; set; }

        [DataMember]
        public byte CashlessAccountNumberLength { get; set; }

        [DataMember]
        public string CashlessAccountNumber { get; set; }

        [DataMember]
        public byte ResponseStatus { get; set; }

        [DataMember]
        public FF_AppId_EFT_PINCheck PINCheck { get; set; }

        [DataMember]
        public byte ErrorTextLength { get; set; }

        [DataMember]
        public string ErrorText { get; set; }
    }
}
