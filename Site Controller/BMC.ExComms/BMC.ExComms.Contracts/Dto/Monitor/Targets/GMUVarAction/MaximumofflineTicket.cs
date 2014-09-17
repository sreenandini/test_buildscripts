using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Request
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_MOT_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_MOT_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }
    #endregion

    #region Response

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GVA_MOT_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_MOT_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public int MaxOfflineTickets { get; set; }
    }
    #endregion
}
