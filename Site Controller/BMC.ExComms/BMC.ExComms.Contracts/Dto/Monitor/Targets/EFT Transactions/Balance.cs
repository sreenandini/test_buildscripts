using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_EFT_BalanceRequest")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_EFT_BalanceRequest
        : MonTgt_G2H_EFT_PlayerAndPinDetails, IMonTgt_G2H
    {
        public MonTgt_G2H_EFT_BalanceRequest()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_EFT_BalanceResponse")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_EFT_BalanceResponse
        : MonTgt_B2B_EFT_PlayerAndMessageInfo, IMonTgt_H2G
    {
        public MonTgt_H2G_EFT_BalanceResponse()
        {
        }
    }


}
