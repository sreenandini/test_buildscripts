using BMC.ExComms.Contracts.DTO.Freeform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    /// <summary>
    /// GMU to Host Freeform for Enable Printing of Restricted Tickets Request
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EnablePrint_RT_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EnablePrint_RT_Request
        : MonTgt_B2B_GMUVarAction, IMonTgt_G2H { }

    /// <summary>
    /// Host To GMU Freeform for Enable Printing of Restricted Tickets Response
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_H2G_GVA_EnablePrint_RT_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_GVA_EnablePrint_RT_Response
        : MonTgt_B2B_GMUVarAction, IMonTgt_H2G
    {
        // Enable Restricted Tickets
        // 0 -> Do not allow printing of restricted tickets, 1 -> Allow printing of restricted tickets.
        public FF_AppId_PrintRestrictedTicket EnableRestrictedTickets { get; set; }
    }

    /// <summary>
    /// GMU To Host Freeform for Enable Printing of Restricted Tickets Status
    /// </summary>
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
          Name = "MonTgt_G2H_GVA_EnablePrint_RT_Status")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_GVA_EnablePrint_RT_Status
        : MonTgt_B2B_GMUVarAction_Status, IMonTgt_G2H { }

}

