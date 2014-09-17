using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    #region Offline Ticket Text Line 1

    /// <summary>
    /// GMU to Host Freeform for Offline Ticket Text Line 1 Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_OfflineT_TxtLine1_Req")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_OfflineT_TxtLine1_Req
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Offline Ticket Text Line 1 - Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_OfflineT_TxtLine1_Resp")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_OfflineT_TxtLine1_Resp
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public string Line1Text { get; set; }
    }

    #endregion Offline Ticket Text Line 1

    #region Offline Ticket Text Line 2

    /// <summary>
    /// GMU to Host Freeform for Offline Ticket Text Line 2 Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_OfflineT_TxtLine2_Req")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_OfflineT_TxtLine2_Req
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Offline Ticket Text Line 2 - Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_OfflineT_TxtLine2_Resp")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_OfflineT_TxtLine2_Resp
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public string Line2Text { get; set; }

    }

    #endregion Offline Ticket Text Line 2
}

