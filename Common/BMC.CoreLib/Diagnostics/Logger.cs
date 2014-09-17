/* ================================================================================= 
 * Purpose		:	Logger Class
 * File Name	:   Logger.cs
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
using System.Diagnostics;
using System.Reflection;

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// Logger Class
    /// </summary>
    [Serializable]
    public class Logger
        : DisposableObject, ILogger
    {
        #region Variables
        [NonSerialized]
        private readonly string _separator = new String('*', 50);

        [NonSerialized]
        private string _moduleName = string.Empty;

        [NonSerialized]
        private IList<ILoggingSystem> _loggingSystems = null;

        [NonSerialized]
        private bool _skipLogWriting = false;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
            : base()
        {
            this.LoggingSystems = new List<ILoggingSystem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public Logger(string moduleName)
            : this()
        {
            this.ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="loggingSystem">The logging system.</param>
        public Logger(ILoggingSystem loggingSystem)
            : this()
        {
            this.LoggingSystems.Add(loggingSystem);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="loggingSystems">The logging systems.</param>
        public Logger(string moduleName, ILoggingSystem loggingSystem)
            : this(loggingSystem)
        {
            this.ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="loggingSystems">The logging systems.</param>
        public Logger(IList<ILoggingSystem> loggingSystems)
            : base()
        {
            this.LoggingSystems = loggingSystems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="loggingSystems">The logging systems.</param>
        public Logger(string moduleName, IList<ILoggingSystem> loggingSystems)
            : this(loggingSystems)
        {
            this.ModuleName = moduleName;
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
            private set
            {
                _moduleName = value;
            }
        }

        #endregion

        #region ILogger Members

        #region Events
        /// <summary>
        /// Occurs when [write to external log].
        /// </summary>
        public event WriteToExternalLogHandler WriteToExternalLog = null;

        public bool HasExternalLogger
        {
            get
            {
                return (this.WriteToExternalLog != null);
            }
        }

        /// <summary>
        /// Called when [write to external log].
        /// </summary>
        /// <param name="formattedMessage">The formatted message.</param>
        /// <param name="type">The type.</param>
        /// <param name="extra">The extra.</param>
        protected void OnWriteToExternalLog(string formattedMessage, LogEntryType type, object extra)
        {
            if (this.WriteToExternalLog != null)
                this.WriteToExternalLog(formattedMessage, type, extra);
        }

        #endregion

        #region Basic Methods
        /// <summary>
        /// Gets the logging systems.
        /// </summary>
        /// <value>The logging systems.</value>
        public IList<ILoggingSystem> LoggingSystems
        {
            get
            {
                return _loggingSystems;
            }
            private set
            {
                _loggingSystems = value;
            }
        }

        /// <summary>
        /// Occurs when log format message.
        /// </summary>
        public event LogFormatMessageHandler LogFormatMessage = null;

        /// <summary>
        /// Gets a value indicating whether [skip log writing].
        /// </summary>
        /// <value><c>true</c> if [skip log writing]; otherwise, <c>false</c>.</value>
        public bool SkipLogWriting
        {
            get { return _skipLogWriting; }
            set { _skipLogWriting = value; }
        }

        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>Formatted message.</returns>
        protected virtual string FormatMessage(string moduleName, string procedureName, string message, LogEntryType type)
        {
            string formattedMessage = string.Empty;
            if (type == LogEntryType.RawMessage)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(new String('-', 120));
                sb.AppendLine(message);
                sb.Append(new String('-', 120));
                formattedMessage = sb.ToString();
                this.OnLogFormatMessage(type, string.Empty, string.Empty, string.Empty, message);
            }
            else
            {
                string typeString = "M";

                switch (type)
                {
                    case LogEntryType.Error:
                    case LogEntryType.Exception:
                        typeString = "E";
                        break;
                    case LogEntryType.Warning:
                        typeString = "W";
                        break;
                    default:
                        break;
                }

                string dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
                bool hasModule = !moduleName.IsEmpty();
                bool hasProcedure = !procedureName.IsEmpty();
                bool hasMessagePrefix = (hasModule || hasProcedure);
                formattedMessage = dateTime +
                    //".[" + typeString + "]" + (hasMessagePrefix ? " " : " ") +
                    (hasMessagePrefix ? " " : " ") +
                    (hasModule ? "[" + moduleName + "]" : "") +
                    (hasProcedure ? ((hasModule ? "." : "") + "[" + procedureName + "]") : "") +
                    (hasMessagePrefix ? " " : " ") + message;
                this.OnLogFormatMessage(type, dateTime, moduleName, procedureName, message);
            }
            return formattedMessage;
        }

        /// <summary>
        /// Called when [log format message].
        /// </summary>
        /// <param name="logType">Type of the log.</param>
        /// <param name="dateTime">The date time.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        private void OnLogFormatMessage(LogEntryType logType, string dateTime, string moduleName, string procedureName, string message)
        {
            if (this.LogFormatMessage != null)
                this.LogFormatMessage(logType, dateTime, moduleName, procedureName, message);
        }

        #endregion

        #region Writes the log information (plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLogInfo(string message)
        {
            this._WriteLog(message, LogEntryType.Information, null);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLogError(string message)
        {
            this._WriteLog(message, LogEntryType.Error, null);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLogWarning(string message)
        {
            this._WriteLog(message, LogEntryType.Warning, null);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLogException(string message)
        {
            this._WriteLog(message, LogEntryType.Exception, null);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void WriteLogException(Exception ex)
        {
            this._WriteException(ex, null);
        }

        /// <summary>
        /// Writes the raw message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteLogRawMessage(string message)
        {
            this._WriteLog(message, LogEntryType.RawMessage, null);
        }

        #endregion

        #region Writes the log information (plain message + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogInfo(string message, object extra)
        {
            this._WriteLog(message, LogEntryType.Information, extra);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogError(string message, object extra)
        {
            this._WriteLog(message, LogEntryType.Error, extra);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogWarning(string message, object extra)
        {
            this._WriteLog(message, LogEntryType.Warning, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogException(string message, object extra)
        {
            this._WriteLog(message, LogEntryType.Exception, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogException(Exception ex, object extra)
        {
            this._WriteException(ex, extra);
        }

        /// <summary>
        /// Writes the raw message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogRawMessage(string message, object extra)
        {
            this._WriteLog(message, LogEntryType.RawMessage, extra);
        }

        #endregion

        #region Writes the log information (method base logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogInfo(MethodBase methodBase, string message)
        {
            this._WriteLog(methodBase, message, LogEntryType.Information, null);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogError(MethodBase methodBase, string message)
        {
            this._WriteLog(methodBase, message, LogEntryType.Error, null);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogWarning(MethodBase methodBase, string message)
        {
            this._WriteLog(methodBase, message, LogEntryType.Warning, null);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogException(MethodBase methodBase, string message)
        {
            this._WriteLog(methodBase, message, LogEntryType.Exception, null);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        public void WriteLogException(MethodBase methodBase, Exception ex)
        {
            this._WriteException(methodBase, ex, null);
        }

        #endregion

        #region Writes the log information (method base logging system + Extra)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogInfo(MethodBase methodBase, string message, object extra)
        {
            this._WriteLog(methodBase, message, LogEntryType.Information, extra);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogError(MethodBase methodBase, string message, object extra)
        {
            this._WriteLog(methodBase, message, LogEntryType.Error, extra);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogWarning(MethodBase methodBase, string message, object extra)
        {
            this._WriteLog(methodBase, message, LogEntryType.Warning, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogException(MethodBase methodBase, string message, object extra)
        {
            this._WriteLog(methodBase, message, LogEntryType.Exception, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogException(MethodBase methodBase, Exception ex, object extra)
        {
            this._WriteException(methodBase, ex, extra);
        }

        #endregion

        #region Writes the log information (module + procedure logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogInfo(string moduleName, string procedureName, string message)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Information, null);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogError(string moduleName, string procedureName, string message)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Error, null);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogWarning(string moduleName, string procedureName, string message)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Warning, null);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        public void WriteLogException(string moduleName, string procedureName, string message)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Exception, null);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="ex">The ex.</param>
        public void WriteLogException(string moduleName, string procedureName, Exception ex)
        {
            this._WriteException(moduleName, procedureName, ex, null);
        }

        #endregion

        #region Writes the log information (module + procedure logging system)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogInfo(string moduleName, string procedureName, string message, object extra)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Information, extra);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogError(string moduleName, string procedureName, string message, object extra)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Error, extra);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogWarning(string moduleName, string procedureName, string message, object extra)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Warning, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogException(string moduleName, string procedureName, string message, object extra)
        {
            this._WriteLog(moduleName, procedureName, message, LogEntryType.Exception, extra);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="moduleName, procedureName">The method base.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLogException(string moduleName, string procedureName, Exception ex, object extra)
        {
            this._WriteException(moduleName, procedureName, ex, extra);
        }

        #endregion

        #region Writes the log information (procedure + plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public void WriteLogInfo(string procedureName, string message)
        {
            this.WriteLogInfo(this.ModuleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public void WriteLogError(string procedureName, string message)
        {
            this.WriteLogError(this.ModuleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public void WriteLogWarning(string procedureName, string message)
        {
            this.WriteLogWarning(this.ModuleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        public void WriteLogException(string procedureName, string message)
        {
            this.WriteLogException(this.ModuleName, procedureName, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        public void WriteLogException(string procedureName, Exception ex)
        {
            this.WriteLogException(this.ModuleName, procedureName, ex);
        }

        #endregion

        #region Writes the log information (module proc + plain message)

        /// <summary>
        /// Writes the log info.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public void WriteLogInfo(ModuleProc proc, string message)
        {
            this.WriteLogInfo(proc.Module, proc.Procedure, message);
        }

        /// <summary>
        /// Writes the log error.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public void WriteLogError(ModuleProc proc, string message)
        {
            this.WriteLogError(proc.Module, proc.Procedure, message);
        }

        /// <summary>
        /// Writes the log warning.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public void WriteLogWarning(ModuleProc proc, string message)
        {
            this.WriteLogWarning(proc.Module, proc.Procedure, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="message">The message.</param>
        public void WriteLogException(ModuleProc proc, string message)
        {
            this.WriteLogException(proc.Module, proc.Procedure, message);
        }

        /// <summary>
        /// Writes the log exception.
        /// </summary>
        /// <param name="proc">Module procedure name.</param>
        /// <param name="ex">The ex.</param>
        public void WriteLogException(ModuleProc proc, Exception ex)
        {
            this.WriteLogException(proc.Module, proc.Procedure, ex);
        }

        #endregion

        #endregion

        #region Private Log Methods

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="this.ProcedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        private void _WriteLog(string message, LogEntryType type, object extra)
        {
            this._WriteLog(string.Empty, string.Empty, message, type, extra);
        }

        /// <summary>
        /// Writes the exception.
        /// </summary>
        /// <param name="this.ProcedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        private void _WriteException(Exception ex, object extra)
        {
            this._WriteException(string.Empty, string.Empty, ex, extra);
        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="extra">The extra.</param>
        private void _WriteLog(MethodBase methodBase, string message, LogEntryType type, object extra)
        {
            this._WriteLog(methodBase.DeclaringType.Name, methodBase.Name, message, type, extra);
        }

        /// <summary>
        /// Writes the exception.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="extra">The extra.</param>
        private void _WriteException(MethodBase methodBase, Exception ex, object extra)
        {
            this._WriteException(methodBase.DeclaringType.Name, methodBase.Name, ex, extra);
        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="this.ProcedureName">Name of the procedure.</param>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        private void _WriteLog(string moduleName, string procedureName, string message, LogEntryType type, object extra)
        {
            if (this.SkipLogWriting) return;
            if (this.LoggingSystems == null) return;
            string formattedMessage = this.FormatMessage(moduleName, procedureName, message, type);
            foreach (ILoggingSystem loggingSystem in this.LoggingSystems)
            {
                loggingSystem.WriteLog(formattedMessage, type, extra);
            }
            this.OnWriteToExternalLog(formattedMessage, type, extra);
        }

        /// <summary>
        /// Writes the exception.
        /// </summary>
        /// <param name="this.ProcedureName">Name of the procedure.</param>
        /// <param name="ex">The ex.</param>
        private void _WriteException(string moduleName, string procedureName, Exception ex, object extra)
        {
            if (this.SkipLogWriting) return;
            if (this.LoggingSystems == null) return;

            ex.IterateException(e =>
            {
                string formattedMessage = this.FormatMessage(moduleName, procedureName, e.Message, LogEntryType.Exception);
                foreach (ILoggingSystem loggingSystem in this.LoggingSystems)
                {
                    loggingSystem.WriteLog(formattedMessage, LogEntryType.Exception, extra);

                    if (ex.StackTrace != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("StackTrace Information");
                        sb.AppendLine(_separator);
                        sb.AppendLine(ex.StackTrace);
                        sb.AppendLine(_separator);
                        loggingSystem.WriteLog(sb.ToString(), LogEntryType.RawMessage);
                    }
                }

                this.OnWriteToExternalLog(formattedMessage, LogEntryType.Exception, extra);
                if (ex.StackTrace != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("StackTrace Information");
                    sb.AppendLine(_separator);
                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine(_separator);
                    this.OnWriteToExternalLog(sb.ToString(), LogEntryType.RawMessage, null);
                }
            });
        }

        #endregion
    }
}
