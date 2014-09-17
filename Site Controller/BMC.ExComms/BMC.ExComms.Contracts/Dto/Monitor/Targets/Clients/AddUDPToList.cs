using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_Client_AddUDPToList")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Client_AddUDPToList
        : MonTgt_H2G
    {
        [DataMember]
        public string ServerIP { get; set; }

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public long PollingID { get; set; }

        [DataMember]
        public long Type { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_Client_AddUDPsToList")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Client_AddUDPsToList
        : MonTgt_H2G
    {
        [DataMember]
        public List<int> InstallationNos { get; set; }
    } 
}
