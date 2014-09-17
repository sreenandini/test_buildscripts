using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU 2 Host Freeform form Ticket Attribute
    /// </summary>
    public class FFTgt_G2H_TicketAttribute 
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        #region Private Members

        private DateTime _ticketPrintDateTime;
        private FF_AppId_TicketOfflineStatus _offlineStatus;

        #endregion Private Members

        #region Properties

        // Ticket Printed Date and Time in BCD of format MM/DD/YY HH:MM:SS
        public DateTime TicketPrintDateTime
        {
            get
            {
                return this._ticketPrintDateTime;
            }
            set
            {
                if (this._ticketPrintDateTime == value) return;
                this._ticketPrintDateTime = value;
            }
        }

        //Offline Status { 0 - Online, 1 - Offline } 
        public FF_AppId_TicketOfflineStatus OfflineStatus
        {
            get
            {
                return this._offlineStatus;
            }
            set
            {
                if (this._offlineStatus == value) return;
                this._offlineStatus = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.OfflineTicketInfo;
            }
        }
    }
}
