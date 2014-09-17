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
          Name = "MonTgt_G2H_GVA_EFT_MaxDeposit_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFT_MaxDeposit_Request
        : MonTgt_B2B_GMUVarAction_Data, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Time Of Day Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_EFT_MaxDeposit_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_EFT_MaxDeposit_Response
        : MonTgt_B2B_GMUVarAction_Data, IMonTgt_H2G
    {
        // MaxDeposit value
        public double MaxDeposit { get; set; }
    }
}
