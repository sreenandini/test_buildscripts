using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_WithdrawalRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_WithdrawalRequest
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_G2H_EFT_WithdrawalRequest()
        {
        }

        [DataMember]
        public FF_AppId_EFT_AccountTypes AccountType { get; set; }

        [DataMember]
        public double AmountRequested { get; set; }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public string Pin { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_WithdrawalAuthorization")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_WithdrawalAuthorization
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_WithdrawalAuthorization()
        {
        }

        [DataMember]
        public double NonCashableAmount { get; set; }

        [DataMember]
        public double CashableAmount { get; set; }

        [DataMember]
        public byte ErrorCode { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_WithdrawalComplete")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_WithdrawalComplete
        : MonTgt_B2B_EFT_AmountDetails_CardNo_Error, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_WithdrawalComplete()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_WithdrawalAcknowledgement")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_WithdrawalAcknowledgement
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_WithdrawalAcknowledgement()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_WithdrawalAuthorization2")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_WithdrawalAuthorization2
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_WithdrawalAuthorization2()
        {
        }

        [DataMember]
        public FF_AppId_EFT_AccountTypes AccountType { get; set; }

        [DataMember]
        public double NonCashableAmount { get; set; }

        [DataMember]
        public double CashableAmount { get; set; }

        [DataMember]
        public double NonCashableBalanceAmount { get; set; }

        [DataMember]
        public double CashableBalanceAmount { get; set; }

        [DataMember]
        public byte ErrorCode { get; set; }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public byte DisplayMessageLength { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }
    }
}
