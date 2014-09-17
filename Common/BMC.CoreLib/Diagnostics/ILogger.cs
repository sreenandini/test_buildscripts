/* ================================================================================= 
 * Purpose		:	Log Writer Interface
 * File Name	:   ILogger.cs
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
using System.Reflection;

namespace BMC.CoreLib.Diagnostics {    
    /// <summary>
    /// Log Writer Interface
    /// </summary>
    public interface ILogger {
        #region Events
        /// <summary>
        /// Occurs when [write to external log].
        /// </summary>
        event WriteToExternalLogHandler WriteToExternalLog;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the logging systems.
        /// </summary>
        /// <value>The logging systems.</value>
        IList<ILoggingSystem> LoggingSystems { get; }

        /// <summary>
        /// Occurs when log format message.
        /// </summary>
        event LogFormatMessageHandler LogFormatMessage;

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        string ModuleName { get; }

        /// <summary>
        /// Gets a value indicating whether [skip log writing].
        /// </summary>
        /// <value><c>true</c> if [skip log writing]; otherwise, <c>false</c>.</value>
        bool SkipLogWriting { get; set; }

        bool HasExternalLogger { get; }
        #endregion

        #region Methods

        #region Writes the log information (plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLogInfo(string message);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLogError(string message);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLogWarning(string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLogException(string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void WriteLogException(Exception ex);

        /// <summary>
        /// Writes the raw message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteLogRawMessage(string message);

        #endregion

        #region Writes the log information (plain message + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogInfo(string message, object extra);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogError(string message, object extra);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogWarning(string message, object extra);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogException(string message, object extra);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogException(Exception ex, object extra);

        /// <summary>
        /// Writes the raw message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogRawMessage(string message, object extra);

        #endregion

        #region Writes the log information (method base logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        void WriteLogInfo(MethodBase methodBase, string message);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        void WriteLogError(MethodBase methodBase, string message);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        void WriteLogWarning(MethodBase methodBase, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        void WriteLogException(MethodBase methodBase, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        void WriteLogException(MethodBase methodBase, Exception ex);

        #endregion

        #region Writes the log information (method base logging system + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogInfo(MethodBase methodBase, string message, object extra);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogError(MethodBase methodBase, string message, object extra);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogWarning(MethodBase methodBase, string message, object extra);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogException(MethodBase methodBase, string message, object extra);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogException(MethodBase methodBase, Exception ex, object extra);

        #endregion

        #region Writes the log information (module + procedure logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogInfo(string moduleName, string procedureName, string message);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogError(string moduleName, string procedureName, string message);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogWarning(string moduleName, string procedureName, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogException(string moduleName, string procedureName, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        void WriteLogException(string moduleName, string procedureName, Exception ex);

        #endregion

        #region Writes the log information (module + procedure logging system + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogInfo(string moduleName, string procedureName, string message, object extra);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogError(string moduleName, string procedureName, string message, object extra);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogWarning(string moduleName, string procedureName, string message, object extra);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogException(string moduleName, string procedureName, string message, object extra);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        void WriteLogException(string moduleName, string procedureName, Exception ex, object extra);

        #endregion

        #region Writes the log information (procedure + plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogInfo(string procedureName, string message);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogError(string procedureName, string message);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogWarning(string procedureName, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        void WriteLogException(string procedureName, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        void WriteLogException(string procedureName, Exception ex);

        #endregion 

        #region Writes the log information (module procedure + plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        void WriteLogInfo(ModuleProc proc, string message);

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        void WriteLogError(ModuleProc proc, string message);

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        void WriteLogWarning(ModuleProc proc, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        void WriteLogException(ModuleProc proc, string message);

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="ex">The ex.</param>
        void WriteLogException(ModuleProc proc, Exception ex);

        #endregion 

        #endregion
    }
}
