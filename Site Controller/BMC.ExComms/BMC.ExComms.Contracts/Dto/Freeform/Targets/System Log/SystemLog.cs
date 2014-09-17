using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region System Log
    
    /// <summary>
    /// GMU to Host Freefrom for System Log
    /// </summary>
    public class FFTgt_G2H_SystemLog
        : FFTgt_G2H
    {
        #region Private Data Members

        private TimeSpan _timeStamp;
        private short _priority;
        private string _group;
        private string _assetNumber;
        private int _textLength;
        private string _text;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// UTC Time
        /// </summary>
        public TimeSpan TimeStamp
        {
            get
            {
                return this._timeStamp;
            }
            set
            {
                this._timeStamp = value;
            }
        }

        /// <summary>
        /// Priority 0-255. 0 -> Unspecified, 1 -> Highest Priority, 255 -> Lowest Priority
        /// </summary>
        public short Priority
        {
            get
            {
                return this._priority;
            }
            set
            {
                if (this._priority == value) return;
                this._priority = value;
            }
        }

        /// <summary>
        /// GMU-defined group number associated with the log entry
        /// </summary>
        public string Group
        {
            get
            {
                return this._group;
            }
            set
            {
                if (this._group.Equals(value)) return;
                this._group = value;
            }
        }

        /// <summary>
        /// Asset Number
        /// </summary>
        public string AssetNumber
        {
            get
            {
                return this._assetNumber;
            }
            set
            {
                if (this._assetNumber.Equals(value)) return;
                this._assetNumber = value;
            }
        }

        /// <summary>
        /// Text Length
        /// </summary>
        public int TextLength
        {
            get
            {
                return this._textLength;
            }
            set
            {
                if (this._textLength ==value) return;
                this._textLength = value;
            }
        }

        /// <summary>
        /// Text Logged by BMC
        /// </summary>
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                if (this._text.Equals(value)) return;
                this._text = value;
            }
        }

        #endregion /Properties

        public override int EntityId
        {
            get
            {
                return (int)FF_AppId_TargetIds.SystemLog;
            }
        }
    }

    #endregion //System Log
}
