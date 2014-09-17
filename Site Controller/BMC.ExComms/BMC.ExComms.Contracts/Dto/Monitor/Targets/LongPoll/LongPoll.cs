using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_LP_CurrentCredits")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_CurrentCredits
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_CurrentCredits() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_LP_HandpayInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_HandpayInfo
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_HandpayInfo() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_LP_GameMachineInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_GameMachineInfo
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_GameMachineInfo() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_LP_TotalGames")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_TotalGames
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_TotalGames() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_LP_TotalGames")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_LP_TotalGames
        : MonTgt_G2H
    {
        public MonTgt_G2H_LP_TotalGames() { }

        public short TotalGames { get; set; }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_LP_GetGameInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_GetGameInfo
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_GetGameInfo() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_LP_GetExtendedGameInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_LP_GetExtendedGameInfo
        : MonTgt_H2G
    {
        public MonTgt_H2G_LP_GetExtendedGameInfo() { }
    }
}
