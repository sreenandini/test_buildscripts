using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Ticket Print Date Request

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TPD_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TPD_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }
    
    #endregion

    #region Ticket Print Date Response

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TPD_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_TPD_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public DateTime Date { get; set; }
    }

    #endregion 

    #region Ticket Print Date Status

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TPD_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TPD_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

    #endregion
}
