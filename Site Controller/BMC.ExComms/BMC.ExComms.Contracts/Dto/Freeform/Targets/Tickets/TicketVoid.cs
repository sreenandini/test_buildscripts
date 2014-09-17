using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Ticket Void
    /// </summary>
    public class FFTgt_G2H_Ticket_Void
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        public string Barcode { get; set; }
        public FF_AppId_TicketPrintStatus Error { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketVoid;
            }
        }
    }
}
