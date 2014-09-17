using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_AD_Amount_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_AD_Amount_Request
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_AD_Amount_Request()
        {
        }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public FF_AppId_EFT_AutoDownload_Status Status { get; set; }

        [DataMember]
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AccountType { get; set; }

        [DataMember]
        public double AutoDownloadAmount { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_AD_Amount_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_AD_Amount_Response
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_AD_Amount_Response()
        {
        }

        [DataMember]
        public byte ErrorCode { get; set; }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public int DispMsgLength { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_ATU_AmountRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_ATU_AmountRequest
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_ATU_AmountRequest()
        {
        }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public FF_AppId_EFT_AutoTopUp_Status Status { get; set; }

        [DataMember]
        public FF_AppId_EFT_AutoDownload_TopUp_AccountTypes AccountType { get; set; }

        [DataMember]
        public double AutoDownloadMaxAmount { get; set; }

        [DataMember]
        public double AutoTopUpTrigger { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_ATU_Amount_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_ATU_Amount_Response
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_ATU_Amount_Response()
        {
        }

        [DataMember]
        public byte ErrorCode { get; set; }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public int DispMsgLength { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }
    }
}
