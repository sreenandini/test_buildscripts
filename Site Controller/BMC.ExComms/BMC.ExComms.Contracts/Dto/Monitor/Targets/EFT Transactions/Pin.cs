using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_PIN_CheckRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_PIN_CheckRequest
        : MonTgt_G2H_EFT_PlayerAndPinDetails, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_PIN_CheckRequest()
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
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_PIN_CheckReply")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_PIN_CheckReply
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_PIN_CheckReply()
        {
        }

        public byte ErrorCode { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_PIN_ChangeRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_PIN_ChangeRequest
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_PIN_ChangeRequest()
        {
        }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public string CurrentPIN { get; set; }

        [DataMember]
        public string NewPIN { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_PIN_ChangeResponse")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_PIN_ChangeResponse
        : MonTgt_B2B_EFT_Transactions, IMonTgt_G2H
    {
        public MonTgt_H2G_EFT_PIN_ChangeResponse()
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
        public int DisplayMessageLength { get; set; }

        [DataMember]
        public string DisplayMessage { get; set; }
    }


}
