using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Status_PlayerCardIn")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Status_PlayerCardIn
        : MonTgt_G2H_Status_CardBase
    {
        public MonTgt_G2H_Status_PlayerCardIn()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Status_PlayerCardOut")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Status_PlayerCardOut
        : MonTgt_G2H_Status_CardBase
    {
        public MonTgt_G2H_Status_PlayerCardOut()
        {
        }
    }
}
