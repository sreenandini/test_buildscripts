/* ================================================================================= 
 * Purpose		:	Logging System Interface
 * File Name	:   ILoggingSystem.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	10/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 10/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BMC.CoreLib.Diagnostics {
    /// <summary>
    /// Logging System Interface
    /// </summary>
    public interface ILoggingSystem {
        /// <summary>
        /// Logging system source either file name or event log name.
        /// </summary>
        /// <value>The source.</value>
        string Source { get; }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="extra">The extra.</param>
        void WriteLog(string message, LogEntryType type, object extra);

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        void WriteLog(string message, LogEntryType type);

        /// <summary>
        /// Writes the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void WriteException(Exception ex);
    }
}
