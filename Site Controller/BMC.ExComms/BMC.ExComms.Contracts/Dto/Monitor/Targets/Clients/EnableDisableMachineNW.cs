using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor", 
        Name = "MonTgt_H2G_Client_EnableDisableMachineNW")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Client_EnableDisableMachineNW
        : MonTgt_H2G
    {
        [DataMember]
        public bool EnableDisable { get; set; }
    }
}
