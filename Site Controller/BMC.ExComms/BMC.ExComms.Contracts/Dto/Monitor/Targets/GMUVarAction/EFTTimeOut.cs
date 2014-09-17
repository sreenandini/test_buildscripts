using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    /// <summary>
    /// GMU To Host Freeform for EFT Transaction Timeout Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFT_TTimeOut_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFT_TTimeOut_Request
        : MonTgt_B2B_GMUVarAction_Data, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for EFT Transaction Timeout Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_EFT_TTimeOut_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_EFT_TTimeOut_Response
        : MonTgt_B2B_GMUVarAction_Data, IMonTgt_H2G
    {
        // EFT Time Out value
        public int TimeOut { get; set; }
    }
    /// <summary>
    /// GMU To Host Freeform for EFT Transaction Timeout Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EFT_TTimeOut_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EFT_TTimeOut_Status
        : MonTgt_G2H_GMUVarAction_Status, IMonTgt_G2H { }
}
