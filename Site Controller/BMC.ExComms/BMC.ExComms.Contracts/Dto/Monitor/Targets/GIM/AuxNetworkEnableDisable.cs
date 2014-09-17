using System.Runtime.Serialization;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GIM_AuxNetworkEnableDisable")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GIM_AuxNetworkEnableDisable
        : MonTgt_H2G
    {
        public MonTgt_H2G_GIM_AuxNetworkEnableDisable()
        {
        }

        [DataMember]
        public bool EnableDisable { get; set; }

        [DataMember]
        public string Display { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GIM_AuxNetworkEnableDisable")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GIM_AuxNetworkEnableDisable
        : MonTgt_G2H
    {
        public MonTgt_G2H_GIM_AuxNetworkEnableDisable()
        {
        }
    }
}
