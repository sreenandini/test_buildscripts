using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_GameInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GameInfo
        : MonTgt_G2H
    {
        public MonTgt_G2H_GameInfo() { }

        [DataMember]
        public string PaytableID { get; set; }

        [DataMember]
        public int CurrentGamePayback { get; set; }

        [DataMember]
        public int CurrentGameDenom { get; set; }

        [DataMember]
        public string CurrentGameID { get; set; }

        [DataMember]
        public string GameProtocolVersion { get; set; }

        [DataMember]
        public string CurrentGameName { get; set; }

        [DataMember]
        public bool HasGameNameFramed { get; set; }
    }
}
