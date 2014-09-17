using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    /// <summary>
    /// GMU To Host Freeform for Time Of Day Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TimeOfDay_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TimeOfDay_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Time Of Day Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TimeOfDay_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_TimeOfDay_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        // Time Of Day value HHMMSS 24hrs format
        public TimeSpan TimeOfDay { get; set; }
    }
    /// <summary>
    /// GMU To Host Freeform for Time Of Day Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TimeOfDay_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TimeOfDay_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }
}
