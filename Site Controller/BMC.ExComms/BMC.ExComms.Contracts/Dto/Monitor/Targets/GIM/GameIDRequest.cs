using System.Runtime.Serialization;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GIM_GameIDRequest")]
    [ExCommsMessageKnownType]
    class MonTgt_H2G_GIM_GameIDRequest
        : MonTgt_H2G
    {
        public MonTgt_H2G_GIM_GameIDRequest()
        {
        }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GIM_GameIDRequest")]
    [ExCommsMessageKnownType]
    class MonTgt_G2H_GIM_GameIDRequest
        : MonTgt_G2H
    {
        public MonTgt_G2H_GIM_GameIDRequest()
        {
        }
    }
}
