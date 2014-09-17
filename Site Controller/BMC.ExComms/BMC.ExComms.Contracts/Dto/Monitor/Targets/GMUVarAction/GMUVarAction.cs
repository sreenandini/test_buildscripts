using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_B2B_GMUVarAction")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_GMUVarAction
        : MonTgt_B2B
    {
        public MonTgt_B2B_GMUVarAction() { }
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_B2B_GMUVarAction_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_B2B_GMUVarAction_Status
        : MonTgt_B2B
    {
        public MonTgt_B2B_GMUVarAction_Status() { }

        [DataMember]
        public FF_AppId_ResponseStatus_Types Status { get; set; }
    }
}
