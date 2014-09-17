using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Ticket Sequence Number Update
    /// </summary>
    public class FFTgt_G2H_TicketSeqNoUpdate
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        #region Private Variables
        private string _barcode;
        private FF_AppId_TicketSeqNoUpdate_Error _error;
        private short _gameTicketSequence; 
        #endregion

        public string Barcode
        {
            get
            {
                return this._barcode;
            }
            set
            {
                if (this._barcode == value) return;
                this._barcode = value;
            }
        }

        public FF_AppId_TicketSeqNoUpdate_Error Error
        {
            get
            {
                return this._error;
            }
            set
            {
                if (this._error == value) return;
                this._error = value;
            }
        }

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

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.TicketSequenceNumberUpdate;
            }
        }
    }
}
