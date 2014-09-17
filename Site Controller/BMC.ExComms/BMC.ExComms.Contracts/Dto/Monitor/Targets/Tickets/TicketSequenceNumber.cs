using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [DataContract(Namespace = "BMC.ExComms.Contracts.DTO.Monitor",
        Name = "MonTgt_G2H_TicketSeqNoUpdate")]
    [ExCommsMessageKnownType]
    public class MonTgt_G2H_TicketSeqNoUpdate
        : MonTgt_B2B_TicketInfoData, IMonTgt_G2H
    {
        #region Constructor
        public MonTgt_G2H_TicketSeqNoUpdate()
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
        public FF_AppId_TicketSeqNoUpdate_Error Error
        {
            get;
            set;
        }

        [DataMember]
        public short GameTicketSequence
        {
            get;
            set;
        }
        #endregion Properties
    }
}
