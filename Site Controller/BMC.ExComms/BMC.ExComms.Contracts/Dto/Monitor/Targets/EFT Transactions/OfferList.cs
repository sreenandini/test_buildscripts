using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_H2G_OfferListRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_H2G_OfferListRequest
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_H2G_OfferListRequest()
        {
        }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_OfferList")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_OfferList
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_OfferList()
        {
        }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public string Pin { get; set; }

        [DataMember]
        public double NonCashableBalance { get; set; }

        [DataMember]
        public double CashableBalance { get; set; }

        [DataMember]
        public double OfferId1 { get; set; }

        [DataMember]
        public double OfferId2 { get; set; }

        [DataMember]
        public double OfferId3 { get; set; }

        [DataMember]
        public double OfferId4 { get; set; }

        [DataMember]
        public double OfferId5 { get; set; }

        [DataMember]
        public double OfferId6 { get; set; }

        [DataMember]
        public double OfferId7 { get; set; }

        [DataMember]
        public double OfferId8 { get; set; }

        [DataMember]
        public double OfferId9 { get; set; }

        [DataMember]
        public double OfferId10 { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_H2G_EFT_OfferRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_H2G_EFT_OfferRequest
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_H2G_EFT_OfferRequest()
        {
        }

        [DataMember]
        public FF_AppId_EFT_AccountTypes AccountType { get; set; }

        [DataMember]
        public string PlayerAccountNumber { get; set; }

        [DataMember]
        public string Pin { get; set; }

        [DataMember]
        public double OfferId { get; set; }
    }
}
