using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Ticket Redemption Request
    /// </summary>
    public class FFTgt_G2H_Ticket_Redemption_Request
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        public string Barcode { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketRedemption;
            }
        }
    }

    /// <summary>
    /// Host to GMU Freeform for Ticket Redemption Response
    /// </summary>
    public class FFTgt_H2G_Ticket_Redemption_Response
        : FFTgt_B2B_TicketInfoData, IFFTgt_H2G
    {
        public string Barcode { get; set; }
        public double Amount { get; set; }
        public FF_AppId_TicketTypes Type { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketRedemption;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Ticket Redemption Close
    /// </summary>
    public class FFTgt_G2H_Ticket_Redemption_Close
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        public string Barcode { get; set; }
        public double Amount { get; set; }
        public FF_AppId_TicketTypes Type { get; set; }
        public FF_AppId_TicketRedemption_Close_Status Status { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketRedemptionComplete;
            }
        }
    }

    /// <summary>
    /// Host 2 GMU Freeform for Ticket Redemption Close Response
    /// </summary>
    public class FFTgt_H2G_Ticket_Redemption_Close
        : FFTgt_B2B_TicketInfoData, IFFTgt_H2G
    {
        public FF_AppId_ResponseStatus_Types Status { get; set; }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketRedemptionComplete;
            }
        }
    }
}
