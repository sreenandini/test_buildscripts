using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// GMU to Host Freeform for Ticketing Enablement Request
    /// </summary>
    public class FFTgt_H2G_TicketingEnablement_Request 
        : FFTgt_B2B_TicketInfoData, IFFTgt_H2G
    {
        #region Private Members

        private FF_AppId_TicketEnablement_Request_Command _command;

        #endregion Private Members

        #region Properties

        // TIcket Enable/Disable Command
        public FF_AppId_TicketEnablement_Request_Command Command
        {
            get
            {
                return this._command;
            }
            set
            {
                if (this._command == value) return;
                this._command = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.EnablementRequest;
            }
        }
    }

    /// <summary>
    /// Host to GMU Freeform for Ticketing Enablement_Response
    /// </summary>
    public class FFTgt_G2H_TicketingEnablement 
        : FFTgt_B2B_TicketInfoData, IFFTgt_G2H
    {
        #region Private Members

        private FF_AppId_TicketEnablement_Response_Status _status;

        #endregion //Private Members

        #region Properties

        // TIcket Enable/Disable Command
        public FF_AppId_TicketEnablement_Response_Status Status
        {
            get
            {
                return this._status;
            }
            set
            {
                if (this._status == value) return;
                this._status = value;
            }
        }

        #endregion //Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TicketMessageTypes.EnablementResponse;
            }
        }
    }
}
