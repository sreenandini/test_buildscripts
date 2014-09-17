using System.Net;
using System.Runtime.Serialization;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GIM_GameIDInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GIM_GameIDInfo
        : MonTgt_G2H
    {
        public MonTgt_G2H_GIM_GameIDInfo()
        {
        }

        [DataMember]
        public string GMUNumber { get; set; }

        [DataMember]
        public string AssetNumber { get; set; }

        [DataMember]
        public string ManufacturerID { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }

        [DataMember]
        public string MACAddress { get; set; }

        [DataMember]
        public string SASVersion { get; set; }

        [DataMember]
        public string GMUVersion { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_GIM_GameIDInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GIM_GameIDInfo
        : MonTgt_H2G
    {
        public MonTgt_H2G_GIM_GameIDInfo()
        {
        }

        [DataMember]
        public IPAddress SourceAddress { get; set; }

        [DataMember]
        public int AssetNumberInt { get; set; }

        [DataMember]
        public string PokerGamePrefix { get; set; }

        [DataMember]
        public bool EnableNetworkMessaging { get; set; }
    }
}
