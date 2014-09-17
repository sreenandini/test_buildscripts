using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Interfaces;
using BMC.Common.Persistence;
using BMC.Common.Utilities;

namespace BMC.Common.LogManagement
{
    /// <summary>
    /// Write logs in the text file.
    /// </summary>
    internal class TextLogAdapter : ILoggingAdapter
    {
        #region Types

        private ManualResetEventSlim _mre = new ManualResetEventSlim(false);
        private CountdownEvent _ceShutdown = new CountdownEvent(0);
        private ManualResetEvent _mreFlush = new ManualResetEvent(false);

        private const int BUFFER_SIZE = 8192;

        protected static bool _disableLogging = false;
        private static UTF8Encoding UTFNoBOM = new UTF8Encoding(false, true);
        private static int _logBufferSize = 0;
        private static int _idleTimeInteval = 0;
        private static bool _noIdleTimer = false;
        private static bool _immediateLogging = false;

        private int _currentLength = 0;
        private IDictionary<string, LogFileItem> _fileLogItems = null;
        private object _contentLock = new object();
        private DateTime _dtQueued = DateTime.MinValue;

        #endregion

        #region Private Fields

        private int _isShutDown = 0;
        private static object lockObject = new Object();

        #endregion

        #region Construction

        static TextLogAdapter()
        {
            InitLogger();
        }



        public static TextLogAdapter GetInstance()
        {
            if (textLogAdapter == null)
            {
                lock (lockObject)
                {
                    if (textLogAdapter == null)
                        textLogAdapter = new TextLogAdapter();
                }
            }
            return textLogAdapter;
        }

        private static TextLogAdapter textLogAdapter = null;

        private TextLogAdapter()
        {
            _fileLogItems = new SortedDictionary<string, LogFileItem>(StringComparer.InvariantCultureIgnoreCase);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            ShutdownHelper.ShutdownInitiated += CurrentDomain_ProcessExit;
            if (!_noIdleTimer) this.InitIdleTimer();
            this.WriteLog("::: Log Buffer Size : " + _logBufferSize.ToString(), 0);
        }

        private string ReadDefaultLogDir()
        {
            string result = string.Empty;

            try
            {
                string dir = ConfigApplicationFactory.DefaultLogDir;
                if (!dir.IsEmpty())
                {
                    DirectoryInfo fi = new DirectoryInfo(dir);
                    if (fi.Parent.Exists)
                    {
                        if (!fi.Exists)
                        {
                            fi.Create();
                        }
                        result = dir;
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// This method will write the log in a text file that is configured in the .config file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void WriteLog(string message, int level)
        {
            string fileName = ConfigManager.Read(Constants.CONSTANT_LOGPATH);
            string defaultLogDir = this.ReadDefaultLogDir();
            if (!defaultLogDir.IsEmpty())
            {
                fileName = Path.Combine(defaultLogDir, Path.GetFileName(fileName));
            }
            this.PushToQueue(fileName, message, level);
        }

        /// <summary>
        /// This method will write the log in the given text file.
        /// </summary>
        /// <param name="msgLog"></param>
        /// <param name="msglevel"></param>
        public void WriteLog(string fileName, string message, int level)
        {
            this.PushToQueue(fileName, message, level);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the logger.
        /// </summary>
        private static void InitLogger()
        {
            try
            {
                _disableLogging = ConfigApplicationFactory.DisableLogging;
                _logBufferSize = Math.Max(0, ConfigExtensions.GetAppSettingValueInt("LogBufferSize", BUFFER_SIZE));
                _idleTimeInteval = Math.Max(60000, ConfigExtensions.GetAppSettingValueInt("LogIdleTimeInterval", 0));
                _noIdleTimer = ConfigExtensions.GetAppSettingValueBool("LogNoIdleTimer", false);
                _immediateLogging = ConfigExtensions.GetAppSettingValueBool("ImmediateLogging", true);
                if (!_immediateLogging && _logBufferSize <= 0) _logBufferSize = BUFFER_SIZE;
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
        }

        private class LogFileItem : IDisposable
        {
            public LogFileItem(ManualResetEventSlim mre)
            {
                this.Builder = new StringBuilder();
                this.Queue = new UserModeQueue<string>(mre, -1, 10, true);
            }
            public StringBuilder Builder { get; private set; }
            public UserModeQueue<string> Queue { get; private set; }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        private class LogFileThreadInfo : IDisposable
        {
            public string FileName { get; set; }
            public LogFileItem FileItem { get; set; }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        private string FormatContent(string message)
        {
            return String.Format("{0:dd-MMM-yyyy HH.mm.ss.fff}\t{1}", DateTime.Now, message);
        }

        private void PushToQueue(string fileName, string message, int level)
        {
            try
            {
                if (_disableLogging) return;

                // format the message
                string formattedMessage = this.FormatContent(message);

                // put the contents into string buffer
                lock (_contentLock)
                {
                    // create the file queue
                    if (!_fileLogItems.ContainsKey(fileName))
                    {
                        this.InitListenForFile(fileName);
                    }

                    // append the data to buffer
                    LogFileItem fi = _fileLogItems[fileName];
                    StringBuilder sb = fi.Builder;
                    sb.AppendLine(formattedMessage);

                    // immediate logging
                    if (_immediateLogging)
                    {
                        this.MoveMessageIntoQueue(fi);
                    }
                    else
                    {
                        // move the content to file only if exceeds length
                        if (sb.Length > _logBufferSize)
                        {
                            this.MoveMessageIntoQueue(fi);
                        }
                    }
                    _dtQueued = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        private void MoveMessageIntoQueue(LogFileItem fi)
        {
            try
            {
                // move the message into queue
                StringBuilder sb = fi.Builder;
                fi.Queue.Enqueue(sb.ToString());
                sb.Clear();
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            if (Interlocked.CompareExchange(ref _isShutDown, 1, 0) == 0)
            {
                _mre.Set();
                this.FlushAllFiles();
                _ceShutdown.Wait(new TimeSpan(0, 1, 0));
                _mreFlush.WaitOne(new TimeSpan(0, 0, 10)); // 10 seconds is enough to flush the data to log file
            }
        }

        private void FlushAllFiles()
        {
            try
            {
                lock (_contentLock)
                {
                    Parallel.ForEach<KeyValuePair<string, LogFileItem>>(_fileLogItems,
                        (kv) =>
                        {
                            kv.Value.Builder.AppendLine(this.FormatContent("(# END OF LOG #)"));
                            this.WriteContentToFile(kv.Key, kv.Value.Builder.ToString());
                            kv.Value.Queue.Shutdown();
                        });
                }
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        private void InitListenForFile(string fileName)
        {
            try
            {
                LogFileThreadInfo threadInfo = new LogFileThreadInfo()
                {
                    FileName = fileName,
                    FileItem = new LogFileItem(_mre),
                };
                _fileLogItems.Add(fileName, threadInfo.FileItem);
                threadInfo.FileItem.Builder.AppendLine(this.FormatContent("(# START OF LOG #)"));

                Thread thListen = new Thread(new ParameterizedThreadStart(this.OnListen));
                thListen.IsBackground = true;
                thListen.Name = "LogThread_" + Path.GetFileName(fileName);
                thListen.Start(threadInfo);

                _ceShutdown.Reset(_fileLogItems.Count);
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        private void OnListen(object state)
        {
            try
            {
                LogFileThreadInfo threadInfo = state as LogFileThreadInfo;
                string fileName = threadInfo.FileName;
                UserModeQueue<string> queue = threadInfo.FileItem.Queue;
                StringBuilder sb = threadInfo.FileItem.Builder;

                while (!_mre.Wait(10))
                {
                    try
                    {
                        string content = queue.Dequeue();
                        if (_mre.IsSet)
                        {
                            if (!content.IsEmpty())
                            {
                                this.WriteContentToFile(fileName, content);
                            }
                            break;
                        }

                        this.WriteContentToFile(fileName, content);
                    }
                    catch (Exception ex)
                    {
                        this.WriteExceptionToEventLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
            finally
            {
                _ceShutdown.Signal();
            }
        }

        private void InitIdleTimer()
        {
            try
            {
                Thread thListen = new Thread(new ThreadStart(this.OnListenIdleTimer));
                thListen.IsBackground = true;
                thListen.Name = "IdleTimer_" + thListen.ManagedThreadId.ToString();
                thListen.Start();
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        private void OnListenIdleTimer()
        {
            try
            {
                while (!_mre.Wait(_idleTimeInteval))
                {
                    try
                    {
                        if (_mre.IsSet) break;

                        // if the item was last queue before more than _idleTimeInteval
                        if (_dtQueued == DateTime.MinValue ||
                            DateTime.Now.Subtract(_dtQueued).TotalMilliseconds > _idleTimeInteval)
                        {
                            // it is time to flush
                            lock (_contentLock)
                            {
                                Parallel.ForEach<KeyValuePair<string, LogFileItem>>(_fileLogItems,
                                    (kv) =>
                                    {
                                        StringBuilder sb = kv.Value.Builder;
                                        if (sb.Length > 0)
                                        {
                                            sb.AppendLine(this.FormatContent("(# Flushing queue items #)"));
                                            this.MoveMessageIntoQueue(kv.Value);
                                        }
                                    });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.WriteExceptionToEventLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        private void WriteContentToFile(string fileName, string content)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Exists)
                {
                    if (fileInfo.Length >= (1024 * 2000))
                    {
                        string newFileName = String.Format(@"{0}\{1} {2:dd-MMM-yyyy HH.mm.ss.fff}.txt",
                            fileInfo.DirectoryName, Path.GetFileNameWithoutExtension(fileInfo.FullName), DateTime.Now);
                        try
                        {
                            File.Move(fileInfo.FullName, newFileName);
                        }
                        catch (Exception ex)
                        {
                            this.WriteExceptionToEventLog(ex);
                        }
                    }
                }
                int count = 1;
                do
                {
                    try
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileName, true))
                        {
                            streamWriter.Write(content);
                            break;
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        count++;
                        System.Threading.Thread.Sleep(100);

                    }
                } while (count < 10);
                if (count == 10)
                    throw new Exception("Unable to write the Logs in " + fileName);
                
            }
            catch (Exception ex)
            {
                this.WriteExceptionToEventLog(ex);
            }
        }

        private void WriteExceptionToEventLog(Exception ex)
        {
            EventLogExceptionAdapter.WriteException(ex);
        }

        #endregion
    }

    internal static class EventLogExceptionAdapter
    {
        private static readonly string EVENT_SRC = string.Empty;
        private static readonly string _separator = new String('*', 50);

        static EventLogExceptionAdapter()
        {
            try
            {
                string location = string.Empty;
                Assembly asm = Assembly.GetEntryAssembly();
                if (asm == null)
                {
                    asm = Assembly.GetCallingAssembly();
                    if (asm == null)
                    {
                        asm = Assembly.GetExecutingAssembly();
                    }
                }

                if (asm != null)
                {
                    location = asm.Location;
                }

                if (!location.IsEmpty() &&
                    File.Exists(location))
                {
                    EVENT_SRC = Path.GetFileNameWithoutExtension(location);
                }
            }
            catch { }
            finally
            {
                if (EVENT_SRC.IsEmpty())
                {
                    EVENT_SRC = "BMC.COMMON";
                }
            }
        }

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
