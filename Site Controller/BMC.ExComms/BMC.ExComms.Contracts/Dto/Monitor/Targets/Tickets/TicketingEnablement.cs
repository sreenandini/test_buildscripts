using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_TicketingEnablement_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_TicketingEnablement_Request
        : MonTgt_B2B_TicketInfoData, IMonTgt_H2G
    {
        
        #region Constructor
        public MonTgt_H2G_TicketingEnablement_Request()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public FF_AppId_TicketEnablement_Request_Command Command
        {
            // Ticket Enable/Disable Command
            get;
            set;
        }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_TicketingEnablement")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_TicketingEnablement
        : MonTgt_B2B_TicketInfoData, IMonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_TicketingEnablement()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public FF_AppId_TicketEnablement_Response_Status Status
        {
            // Ticket Enable/Disable Command
            get;
            set;
        }
        #endregion Properties
    }
}
