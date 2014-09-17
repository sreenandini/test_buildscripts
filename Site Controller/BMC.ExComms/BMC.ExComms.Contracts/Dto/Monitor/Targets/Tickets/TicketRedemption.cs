using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Ticket_Redemption_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Ticket_Redemption_Request
        : MonTgt_B2B_TicketInfoData, IMonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_Ticket_Redemption_Request()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public string Barcode
        {
            get;
            set;
        }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_Ticket_Redemption_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Ticket_Redemption_Response
        : MonTgt_B2B_TicketInfoData, IMonTgt_H2G
    {
        #region Constructor
        public MonTgt_H2G_Ticket_Redemption_Response()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public string Barcode
        {
            get;
            set;
        }

        [DataMember]
        public double Amount
        {
            get;
            set;
        }

        [DataMember]
        public FF_AppId_TicketTypes Type
        {
            // Ticket Type { 0 - Cashable, 1 - NonCashable, 2 - CashablePromo } 
            get;
            set;
        }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_Ticket_Redemption_Close")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Ticket_Redemption_Close
        : MonTgt_B2B_TicketInfoData, IMonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_Ticket_Redemption_Close()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public FF_AppId_TicketRedemption_Close_Status Status
        {
            // Ticket Redemption Status
            get;
            set;
        }

        [DataMember]
        public string Barcode
        {
            // Ticket identifier in BCD
            get;
            set;
        }

        [DataMember]
        public double Amount
        {
            // Value of the Ticket in BCD
            get;
            set;
        }

        [DataMember]
        public FF_AppId_TicketTypes Type
        {
            // Ticket Type { 0 - Cashable, 1 - NonCashable, 2 - CashablePromo } 
            get;
            set;
        }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_H2G_Ticket_Redemption_Close")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Ticket_Redemption_Close
        : MonTgt_B2B_TicketInfoData, IMonTgt_H2G
    {
        #region Constructor
        public MonTgt_H2G_Ticket_Redemption_Close()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public FF_AppId_ResponseStatus_Types Status
        {
            // Redemption Close Status -> Success (Or) Fail
            get;
            set;
        }
        #endregion Properties
    }
}
