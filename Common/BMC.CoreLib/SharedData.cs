/* ================================================================================= 
 * Purpose		:	Singleton Shared Data class whose members can be shared accorss this library
 * File Name	:   SharedData.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	01/04/11
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 01/04/11		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib
{
    /// <summary>
    /// Singleton Shared Data class whose members can be shared accorss this library
    /// </summary>
    public static class SharedData
    {
        #region Variables
        private static IList<ILogger> _loggers = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the <see cref="SharedData"/> class.
        /// </summary>
        static SharedData()
        {
            LoggingSystems = new List<ILoggingSystem>();

#if !REF_BMC_COMMON
            GlobalLogger = new BMCLogger(LoggingSystems);
#else
            GlobalLogger = new BMCCommonLogger(LoggingSystems);
#endif

            _loggers = new List<ILogger>();
            _loggers.Add(GlobalLogger);

#if !REF_BMC_COMMON
            InitAppFileLoggingSystem();
#endif
        }
        #endregion

        #region Properties
        public static LogEntryType LogType { get; set; }

        /// <summary>
        /// Logger instance
        /// </summary>
        private static ILogger _globalLogger = null;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public static ILogger GlobalLogger
        {
            get { return _globalLogger; }
            set { _globalLogger = value; }
        }

        /// <summary>
        /// Logging Systems
        /// </summary>
        private static IList<ILoggingSystem> _loggingSystems = null;

        /// <summary>
        /// Gets or sets the logging systems.
        /// </summary>
        /// <value>The logging systems.</value>
        public static IList<ILoggingSystem> LoggingSystems
        {
            get { return _loggingSystems; }
            set { _loggingSystems = value; }
        }

        /// <summary>
        /// Thread Level Logger
        /// </summary>
        [ThreadStatic]
        public static ILogger ThreadLogger = null;

        /// <summary>
        /// Gets the loggers.
        /// </summary>
        /// <value>The loggers.</value>
        public static IList<ILogger> Loggers
        {
            get { return _loggers; }
        }

        /// <summary>
        /// Gets the active logger.
        /// </summary>
        /// <value>The active logger.</value>
        public static ILogger ActiveLogger
        {
            get
            {
                if (ThreadLogger != null) return ThreadLogger;
                return _globalLogger;
            }
        }

        #endregion

        #region Methods
        private static void InitAppFileLoggingSystem()
        {
            try
            {
                string logPath = Extensions.GetAppSettingValue("LogPath", string.Empty);
                if (!logPath.IsEmpty() &&
                    Directory.Exists(Path.GetDirectoryName(logPath)))
                {
                    if (!ActiveLogger.HasExternalLogger)
                    {
                        Log.AddAppFileLoggingSystem();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Logger implementation</returns>
        public static ILogger GetLogger(this object source)
        {
            return (ThreadLogger != null) ? ThreadLogger : _globalLogger;
        }

        /// <summary>
        /// Gets the global logger.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Logger implementation</returns>
        public static ILogger GetGlobalLogger(this object source)
        {
            return GlobalLogger;
        }

        /// <summary>
        /// Gets the thread logger.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Logger implementation</returns>
        public static ILogger GetThreadLogger(this object source)
        {
            return ThreadLogger;
        }

        #endregion
    }
}
