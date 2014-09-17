using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_Ticket_Printed_Request")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_Ticket_Printed_Request
        : MonTgt_B2B_TicketInfoData, IMonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_Ticket_Printed_Request()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public FF_AppId_TicketTypes Type { get; set; }

        [DataMember]
        public int SequenceNo { get; set; }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_H2G_Ticket_Printed_Response")]
    [ExCommsMessageKnownType]
    public class MonTgt_H2G_Ticket_Printed_Response
        : MonTgt_B2B_TicketInfoData, IMonTgt_H2G
    {
        #region Constructor
        public MonTgt_H2G_Ticket_Printed_Response()
        {
        }
        #endregion Constructor

        #region Properties
        [DataMember]
        public FF_AppId_ResponseStatus_Types Status { get; set; }
        #endregion Properties
    }

    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
       Name = "MonTgt_G2H_TicketPrint_ResultStatus")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_TicketPrint_ResultStatus
        : MonTgt_B2B_TicketInfoData, IMonTgt_H2G
    {
        #region Constructor
        public MonTgt_G2H_TicketPrint_ResultStatus()
        {
        }
        #endregion Constructor

        #region Properties
        
        [DataMember]
        public short GameTicketSequence { get; set; }

        [DataMember]
        public FF_AppId_TicketPrintStatus PrintStatus { get; set; }

        #endregion Properties
    }
}
