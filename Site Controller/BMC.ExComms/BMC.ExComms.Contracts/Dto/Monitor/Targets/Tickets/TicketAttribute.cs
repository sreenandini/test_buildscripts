using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_TicketAttribute")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_TicketAttribute
        : MonTgt_B2B_TicketInfoData, IMonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_TicketAttribute()
        {
        }
        #endregion Constructor

        #region Properties

        [DataMember]
        public DateTime TicketPrintDateTime
        {
            // Ticket Printed Date and Time in BCD of format MM/DD/YY HH:MM:SS
            get;
            set;
        }

        [DataMember]
        public FF_AppId_TicketOfflineStatus OfflineStatus
        {
            //Offline Status { 0 - Online, 1 - Offline } 
            get;
            set;
        }

        #endregion Properties
    }
}
