using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{

    /// <summary>
    /// GMU To Host Freeform for Ticket System Slot ID Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TSSlotID_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TSSlotID_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Ticket System Slot ID Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_TSSlotID_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_TSSlotID_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        public int SlotID { get; set; }
    }

    #region Ticket System Slot ID

    /// <summary>
    /// GMU To Host Freeform for Ticket System Slot ID Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_TSSlotID_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_TSSlotID_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

    #endregion Ticket System Slot ID Status
}
