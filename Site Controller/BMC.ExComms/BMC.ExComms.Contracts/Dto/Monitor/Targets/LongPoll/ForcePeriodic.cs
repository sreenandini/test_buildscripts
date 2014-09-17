using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_LP_ForcePeriodic")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_ForcePeriodic
        : MonTgt_H2G, IMonTgt_H2G
    {
        public MonTgt_H2G_LP_ForcePeriodic()
        {
        }

        [DataMember]
        public byte Data1 { get; set; }

        [DataMember]
        public byte Data2 { get; set; }
    }
}
