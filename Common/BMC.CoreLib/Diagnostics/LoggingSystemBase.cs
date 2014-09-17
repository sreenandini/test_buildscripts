/* ================================================================================= 
 * Purpose		:	Logging System Base Class
 * File Name	:   LoggingSystemBase.cs
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
using System.Collections;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.IoC;
using System.IO;
using System.Xml;
#if REF_BMC_COMMON
using BMC.Common.Persistence;
#endif

namespace BMC.CoreLib.Diagnostics
{
    /// <summary>
    /// Logging System Base Class
    /// </summary>
    [Serializable]
    public abstract class LoggingSystemBase
        : QueueProcessManagerEx<LogItem>, ILoggingSystem
    {
        #region Variables
        private string _source = string.Empty;
        protected static bool _disableLogging = false;
        #endregion

        #region Constructors
        static LoggingSystemBase()
        {
            InitLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingSystemBase"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="mode">The mode.</param>
        protected LoggingSystemBase(string source, QueueProcessMode mode)
            : this(source, mode, null)
        {
            this.Source = source;
        }

        protected LoggingSystemBase(string source, QueueProcessMode mode, IExecutorService executorService)
            : base(mode, executorService, -1, 10, true)
        {
            this.Source = source;
        }
        #endregion

        #region ILoggingSystem Members

        private static void InitLogger()
        {
#if !REF_BMC_COMMON
            try
            {
                _disableLogging = MEFHelper.GetExportedValue<bool>("DisableLogging");
            }
            catch (Exception)
            {
                InitLoggerFromXml();
            }
#else
            _disableLogging = ConfigApplicationFactory.DisableLogging;
#endif
        }

#if !REF_BMC_COMMON
        private static void InitLoggerFromXml()
        {
            try
            {
                string directory = string.Empty;

                try
                {
                    directory = Environment.GetEnvironmentVariable("BMCConfigPath", EnvironmentVariableTarget.Machine);
                }
                catch { }

                if (!directory.IsEmpty() &&
                    Directory.Exists(directory))
                {
                    string fileName = Path.Combine(directory, "BMCApp.xml");
                    if (File.Exists(fileName) &&
                        new FileInfo(fileName).Length > 0)
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.Load(fileName);

                        if (xDoc.DocumentElement != null)
                        {
                            XmlNode valueNode = xDoc.DocumentElement.SelectSingleNode("Section[@id='Honeyframe']/KeyValue[@key='DisableLogging']/@value");
                            if (valueNode != null)
                            {
                                _disableLogging = TypeSystem.GetValueBoolSimple(valueNode.InnerText);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }
#endif

        /// <summary>
        /// Logging system source either file name or event log name.
        /// </summary>
        /// <value>The source.</value>
        public string Source
        {
            get
            {
                return _source;
            }
            private set
            {
                _source = value;
            }
        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="extra">The extra.</param>
        public void WriteLog(string message, LogEntryType type, object extra)
        {
            if (!_disableLogging)
            {
                // add the message into queue and notify the thread to process
                base.AddItemToQueue(new LogItem(message, type, extra));
            }
        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public void WriteLog(string message, LogEntryType type)
        {
            this.WriteLog(message, type, null);
        }

        /// <summary>
        /// Writes the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void WriteException(Exception ex)
        {
            ex.IterateException(e =>
            {
                this.WriteLog(e.Message, LogEntryType.Exception);
            });
        }

        #endregion

        #region QueueProcessManager<string> Members

        /// <summary>
        /// Processes the queue item.
        /// </summary>
        /// <param name="item">The item.</param>
        protected override void ProcessQueueItem(LogItem item) { }

        #endregion
    }

    public static class EventLogExceptionAdapter
    {
        private const string EVENT_SRC = "BMC.CORELIB";
        private static readonly string _separator = new String('*', 50);

        #region Exception Extension Methods

        public static void WriteException(Exception ex)
        {
            try
            {
                if (!EventLog.SourceExists(EVENT_SRC))
                {
                    EventLog.CreateEventSource(EVENT_SRC, "Application");
                }

                string errMessage = GetExceptionMessage(ex);
                EventLog.WriteEntry(EVENT_SRC, errMessage, EventLogEntryType.Error);
            }
            catch { }
        }

        /// <summary>
        /// Iterates the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="action">The action.</param>
        private static void IterateException(Exception ex, Action<Exception> action)
        {
            Exception inner = ex;
            while (inner != null)
            {
                byte[] rawData = null;
                try
                {
#if !SILVERLIGHT
                    rawData = Encoding.Default.GetBytes(inner.StackTrace);
#else
                    rawData = Encoding.Unicode.GetBytes(inner.StackTrace);
#endif
                }
                catch { }

                if (action != null) action(inner);
                inner = inner.InnerException;
            }
        }

        private static string GetExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            IterateException(ex, e =>
            {
                if (ex.StackTrace != null)
                {
                    sb.AppendLine("Message:" + ex.Message);
                    sb.AppendLine(_separator);
                    sb.AppendLine("StackTrace Information");
                    sb.AppendLine(_separator);
                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine(_separator);
                    sb.AppendLine();
                }
            });
            return sb.ToString();
        }

        #endregion
    }
}
