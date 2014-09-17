using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Tickets Printed Class
    /// </summary>
    public class FFTgt_G2H_Ticket_Printed_Request 
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        public string BarCode { get; set; }
        public double Amount { get; set; }
        public FF_AppId_TicketTypes Type { get; set; }
        public int SequenceNo { get; set; }

        public override FF_AppId_Encryption_Types EncryptionType
        {
            get
            {
                return FF_AppId_Encryption_Types.Standard;
            }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketPrinted;
            }
        }
    }

    /// <summary>
    /// Host to GMU Freeform for Ticket Printed Response
    /// </summary>
    public class FFTgt_H2G_Ticket_Printed_Response 
        : FFTgt_B2B_TicketInfoData, IFFTgt_H2G
    {
        public FF_AppId_ResponseStatus_Types Status { get; set; }

        public override FF_AppId_Encryption_Types EncryptionType
        {
            get
            {
                return FF_AppId_Encryption_Types.Standard;
            }
        }

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketPrinted;
            }
        }
    }

    /// <summary>
    /// GMU to Host Freeform for Ticketing Print Result Status
    /// </summary>
    public class FFTgt_G2H_TicketPrint_ResultStatus 
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        #region Private Members

        private short _gameTicketSequence;
        private FF_AppId_TicketPrintStatus _printStatus;

        #endregion Private Members

        #region Properties

        public short GameTicketSequence
        {
            get
            {
                return this._gameTicketSequence;
            }
            set
            {
                if (this._gameTicketSequence == value) return;
                this._gameTicketSequence = value;
            }
        }

        public FF_AppId_TicketPrintStatus PrintStatus
        {
            get
            {
                return this._printStatus;
            }
            set
            {
                if (this._printStatus == value) return;
                this._printStatus = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketPrintStatusResult;
            }
        }
    }
}
