using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    /// <summary>
    /// Base class - Host to GMU Freeform for EPI Processing info
    /// </summary>
    public class FFTgt_H2G_EPI_Info
        : FFTgt_B2B
    {
        #region Private Members

        private byte _deviceaddr;
        private byte _command;
        private byte[] _messages;

        #endregion

        #region Properties

        public byte DeviceAddress
        {
            get
            {
                return this._deviceaddr;
            }
            set
            {
                if (this._deviceaddr != value)
                    this._deviceaddr = value;
            }
        }

        // Command 
        public byte EPICommand
        {
            get
            {
                return this._command;
            }
            set
            {
                if (this._command != value)
                    this._command = value;
            }
        }

        // Messages 
        public byte[] EPIMessages
        {
            get
            {
                return this._messages;
            }
            set
            {
                this._messages = value;
            }
        }
        #endregion

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.EPI;
            }
        }
    }
}
