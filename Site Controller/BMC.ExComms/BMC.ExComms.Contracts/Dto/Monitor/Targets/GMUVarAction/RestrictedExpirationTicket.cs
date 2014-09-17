using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_RET_Request")]
    [ExCommsMessageKnownType]
    #region Request
    public class MonTgt_G2H_GVA_RET_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }
    #endregion

    #region Response
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_GVA_RET_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_RET_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public int ExpirationDays { get; set; }
    }
    #endregion

    #region Status
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_RET_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_RET_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }
    #endregion 

}
