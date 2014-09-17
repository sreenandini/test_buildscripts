using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_B2B_TicketInfoData")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_TicketInfoData : MonTgt_B2B { }
}
