using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor", Name = "MonTgt_H2G_SetTicketExpireNW")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_SetTicketExpireNW
        : MonTgt_H2G
    {
        [DataMember]
        public int NoOfDays { get; set; }
    }
}
