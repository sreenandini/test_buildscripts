using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_Client_EnableDisableMachine")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Client_EnableDisableMachine
        : MonTgt_H2G
    {
        [DataMember]
        public bool EnableDisable { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Client_EnableMachine")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Client_EnableMachine
        : MonTgt_G2H
    {
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Client_DisableMachine")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Client_DisableMachine
        : MonTgt_G2H
    {
    }
}
