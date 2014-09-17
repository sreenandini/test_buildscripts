using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if REF_BMC_COMMON
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
#endif
using BMC.CoreLib.Diagnostics;
using System.IO;

namespace BMC.CoreLib
{
    /// <summary>
    /// BMCLogger
    /// </summary>
    [Serializable]
    public class BMCLogger : Logger
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BMCLogger"/> class.
        /// </summary>
        public BMCLogger()
            : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BMCLogger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public BMCLogger(string moduleName)
            : base(moduleName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BMCLogger"/> class.
        /// </summary>
        /// <param name="loggingSystem">The logging system.</param>
        public BMCLogger(ILoggingSystem loggingSystem)
            : base(loggingSystem) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BMCLogger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="loggingSystems">The logging systems.</param>
        public BMCLogger(string moduleName, ILoggingSystem loggingSystem)
            : base(moduleName, loggingSystem) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BMCLogger"/> class.
        /// </summary>
        /// <param name="loggingSystems">The logging systems.</param>
        public BMCLogger(IList<ILoggingSystem> loggingSystems)
            : base(loggingSystems) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BMCLogger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="loggingSystems">The logging systems.</param>
        public BMCLogger(string moduleName, IList<ILoggingSystem> loggingSystems)
            : base(moduleName, loggingSystems) { }

        #endregion
    }

#if REF_BMC_COMMON
    public class BMCCommonLogger : DisposableObject, ILogger
    {
        [NonSerialized]
        private string _moduleName = string.Empty;

        [NonSerialized]
        private IList<ILoggingSystem> _loggingSystems = null;

        [NonSerialized]
        private bool _skipLogWriting = false;

        [NonSerialized]
        private string _fileName = string.Empty;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public BMCCommonLogger()
            : base()
        {
            this.LoggingSystems = new List<ILoggingSystem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public BMCCommonLogger(string moduleName)
            : this()
        {
            this.ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        public BMCCommonLogger(string fileName, bool dummy)
            : this()
        {
            if (Path.GetExtension(fileName).IsEmpty())
            {
                fileName += ".txt";
            }

            if (Path.GetDirectoryName(fileName).IsEmpty())
            {
                string dir = Path.GetDirectoryName(Extensions.GetAppSettingValue("LogPath", ""));
                if (dir.IsEmpty()) dir = Extensions.GetStartupDirectory();
                _fileName = Path.Combine(dir, fileName);
            }
            else
            {
                _fileName = fileName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="loggingSystem">The logging system.</param>
        public BMCCommonLogger(ILoggingSystem loggingSystem)
            : this()
        {
            this.LoggingSystems.Add(loggingSystem);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="loggingSystems">The logging systems.</param>
        public BMCCommonLogger(string moduleName, ILoggingSystem loggingSystem)
            : this(loggingSystem)
        {
            this.ModuleName = moduleName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="loggingSystems">The logging systems.</param>
        public BMCCommonLogger(IList<ILoggingSystem> loggingSystems)
            : base()
        {
            this.LoggingSystems = loggingSystems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="loggingSystems">The logging systems.</param>
        public BMCCommonLogger(string moduleName, IList<ILoggingSystem> loggingSystems)
            : this(loggingSystems)
        {
            this.ModuleName = moduleName;
        }
        #endregion

        public event WriteToExternalLogHandler WriteToExternalLog = null;

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

        public event LogFormatMessageHandler LogFormatMessage = null;

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

        public bool SkipLogWriting
        {
            get { return _skipLogWriting; }
            set { _skipLogWriting = value; }
        }

        public void WriteLogInfo(string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(Exception ex)
        {
            this.WriteLogMangerException(ex);
        }

        public void WriteLogRawMessage(string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Info);
        }

        public void WriteLogInfo(string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(Exception ex, object extra)
        {
            this.WriteLogMangerException(ex);
        }

        public void WriteLogRawMessage(string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Info);
        }

        public void WriteLogInfo(System.Reflection.MethodBase methodBase, string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(System.Reflection.MethodBase methodBase, string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(System.Reflection.MethodBase methodBase, string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(System.Reflection.MethodBase methodBase, string message)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(System.Reflection.MethodBase methodBase, Exception ex)
        {
            this.WriteLogMangerException(ex);
        }

        public void WriteLogInfo(System.Reflection.MethodBase methodBase, string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(System.Reflection.MethodBase methodBase, string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(System.Reflection.MethodBase methodBase, string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(System.Reflection.MethodBase methodBase, string message, object extra)
        {
            this.WriteLogMangerLog(message, LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(System.Reflection.MethodBase methodBase, Exception ex, object extra)
        {
            this.WriteLogMangerException(ex);
        }

        public void WriteLogInfo(string moduleName, string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(string moduleName, string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(string moduleName, string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(string moduleName, string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(string moduleName, string procedureName, Exception ex)
        {
            ExceptionManager.Publish(ex);
        }

        public void WriteLogInfo(string moduleName, string procedureName, string message, object extra)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(string moduleName, string procedureName, string message, object extra)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(string moduleName, string procedureName, string message, object extra)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(string moduleName, string procedureName, string message, object extra)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(moduleName, procedureName), LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(string moduleName, string procedureName, Exception ex, object extra)
        {
            ExceptionManager.Publish(ex);
        }

        public void WriteLogInfo(string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(procedureName, message), LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(procedureName, message), LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(procedureName, message), LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(string procedureName, string message)
        {
            this.WriteLogMangerLog(ModuleProc.Combine(procedureName, message), LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(string procedureName, Exception ex)
        {
            this.WriteLogMangerException(ex);
        }

        public void WriteLogInfo(ModuleProc proc, string message)
        {
            this.WriteLogMangerLog(proc.Combine(message), LogManager.enumLogLevel.Info);
        }

        public void WriteLogError(ModuleProc proc, string message)
        {
            this.WriteLogMangerLog(proc.Combine(message), LogManager.enumLogLevel.Error);
        }

        public void WriteLogWarning(ModuleProc proc, string message)
        {
            this.WriteLogMangerLog(proc.Combine(message), LogManager.enumLogLevel.Warning);
        }

        public void WriteLogException(ModuleProc proc, string message)
        {
            this.WriteLogMangerLog(proc.Combine(message), LogManager.enumLogLevel.Error);
        }

        public void WriteLogException(ModuleProc proc, Exception ex)
        {
            this.WriteLogMangerException(ex);
        }

        public bool HasExternalLogger
        {
            get { return false; }
        }

        private void WriteLogMangerLog(string message, LogManager.enumLogLevel level)
        {
            if (!_fileName.IsEmpty())
            {
                LogManager.WriteLog(_fileName, message, level);
            }
            else
            {
                LogManager.WriteLog(message, level);
            }
            Log.OnGlobalWriteToExternalLog(message);
        }

        private void WriteLogMangerException(Exception ex)
        {
            if (!_fileName.IsEmpty())
            {
                ExceptionManager.Publish(_fileName, ex);
            }
            else
            {
                ExceptionManager.Publish(ex);
            }
            Log.OnGlobalWriteToExternalLog(ex);
        }
    }
#endif
}
