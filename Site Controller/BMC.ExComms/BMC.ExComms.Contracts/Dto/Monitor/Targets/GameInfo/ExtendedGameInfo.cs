using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_ExtendedGameInfoInfo")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_ExtendedGameInfoInfo
        : MonTgt_G2H
    {
        public MonTgt_G2H_ExtendedGameInfoInfo()
        {
        }

        [DataMember]
        public int GameNumber { get; set; }

        [DataMember]
        public int MaxBet { get; set; }

        [DataMember]
        public string GameName { get; set; }

        [DataMember]
        public string PayTableName { get; set; }

        [DataMember]
        public int ProgressLevel { get; set; }

        [DataMember]
        public int ProgressGroup { get; set; }

        [DataMember]
        public bool HasGameNameFramed { get; set; }


    }
}
