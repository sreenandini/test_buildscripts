using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.ExComms.Contracts.DTO.Freeform
{
    #region Jackpot Printer Selection
    
    /// <summary>
    /// GMU to Host Freeform for Jackpot Printer Selection
    /// </summary>
    public class FFTgt_G2H_JP_Printer_Select
        : FFTgt_G2H
    {
        #region Private Data Members

        private string _printer;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Printer location selected by the employee
        /// </summary>
        public string Printer
        {
            get
            {
                return this._printer;
            }
            set
            {
                if (this._printer.Equals(value)) return;
                this._printer = value;
            }
        }

        #endregion //Properties
    }

    #endregion Jackpot Printer Selection

    #region Jackpot Printer Selection Response

    /// <summary>
    /// GMU to Host Freeform for Jackpot Printer Selection
    /// </summary>
    public class FFTgt_H2G_JP_PS_Response
        :FFTgt_H2G
    {
        #region Private Data Members

        private FF_ClearJP _clearJPFlag;
        private int _txtMsgLength;
        private string _txtMsg;

        #endregion //Private Data Members

        #region Properties

        /// <summary>
        /// Clear JP Flag. 0 -> do not clear the JP, 1 -> Clear the JP and put the game back into play
        /// </summary>
        public FF_ClearJP ClearJPFlag
        {
            get
            {
                return this._clearJPFlag;
            }
            set
            {
                if (this._clearJPFlag == value) return;
                this._clearJPFlag = value;
            }
        }

        /// <summary>
        /// Length of the text message. Maximum value is 60
        /// </summary>
        public int TxtMsgLength
        {
            get
            {
                return this._txtMsgLength;
            }
            set
            {
                if (this._txtMsgLength == value) return;
                this._txtMsgLength = value;
            }
        }

        /// <summary>
        /// Successful message or Error message  wnding with null character
        /// </summary>
        public string TxtMsg
        {
            get
            {
                return this._txtMsg;
            }
            set
            {
                if (this._txtMsg == value) return;
                this._txtMsg = value;
            }
        }

        #endregion //Properties
    }

    #endregion //Jackpot Printer Selection Response
}
