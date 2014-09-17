using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_DepositRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_DepositRequest
        : MonTgt_B2B_EFT_AmountDetails_CardNo, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_DepositRequest()
        {
        }

        [DataMember]
        public string Pin { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_DepositAuthorization")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_DepositAuthorization
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_DepositAuthorization()
        {
        }

        [DataMember]
        public byte ErrorCode { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_DepositComplete")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_DepositComplete
        : MonTgt_B2B_EFT_AmountDetails_CardNo_Error, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_DepositComplete()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_DepositAcknowledgement")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_DepositAcknowledgement
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_DepositAcknowledgement()
        {
        }
    }
}
