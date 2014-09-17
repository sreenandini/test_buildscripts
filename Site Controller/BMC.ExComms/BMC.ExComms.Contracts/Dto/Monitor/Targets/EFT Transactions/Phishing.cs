using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_H2G_EFT_Phishing")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_H2G_EFT_Phishing
        : MonTgt_B2B_EFT_Transactions, IMonTgt_H2G
    {
        public MonTgt_G2H_H2G_EFT_Phishing()
        {
        }

        [DataMember]
        public string PlayerCardNumber
        {
            get { return this.CardNumber; }
            set { this.CardNumber = value; }
        }

        [DataMember]
        public byte Type { get; set; }
    }
}
