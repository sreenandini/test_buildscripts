/* ================================================================================= 
 * Purpose		:	Log Item
 * File Name	:   LogItem.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	13/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 13/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.Diagnostics {
    /// <summary>
    /// Log Item
    /// </summary>
    public class LogItem {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogItem"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LogItem(string message)
            : this(message, LogEntryType.Information) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogItem"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logType">Type of the log.</param>
        public LogItem(string message, LogEntryType logType)
            : this(message, logType, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogItem"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="logType">Type of the log.</param>
        /// <param name="rawData">The raw data.</param>
        public LogItem(string message, LogEntryType logType, object extra) {
            this.Message = message;
            this.LogType = logType;
            this.Extra = extra;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the log.
        /// </summary>
        /// <value>The type of the log.</value>
        public LogEntryType LogType { get; set; }

        /// <summary>
        /// Gets or sets the extra.
        /// </summary>
        /// <value>The extra.</value>
        public object Extra { get; set; }
    }
}
