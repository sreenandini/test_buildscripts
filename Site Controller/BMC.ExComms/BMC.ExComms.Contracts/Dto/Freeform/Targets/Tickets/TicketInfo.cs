using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Base class - GMU to Host (or) Host to GMU Freeform for Ticket Processing info
    /// </summary>
    public class FFTgt_B2B_TicketInfo
        : FFTgt_B2B_SubData<FFTgt_B2B_TicketInfoData>
    {
        public override FF_AppId_Encryption_Types EncryptionType
        {
            get
            {
                return FF_AppId_Encryption_Types.AuthByteClearData;
            }
            set
            {
                base.EncryptionType = value;
            }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.Tickets;
            }
        }
    }

    /// <summary>
    /// GMU to Host (or) Host to GMU Ticket Sub Information
    /// </summary>
    public class FFTgt_B2B_TicketInfoData : FFTgt_B2B { }
}
