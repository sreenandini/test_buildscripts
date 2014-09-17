using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib
{
    public static class Log
    {
        static Log()
        {
            Log.LogType = LogEntryType.Information | LogEntryType.Exception;
        }

        public static LogEntryType LogType
        {
            get { return SharedData.LogType; }
            set { SharedData.LogType = value; }
        }

        public static void AddAppFileLoggingSystem()
        {
            AddAppFileLoggingSystem(10485760);
        }

        public static void AddAppFileLoggingSystem(int maxLogSize)
        {
            var loggingSystem = (from l in SharedData.ActiveLogger.LoggingSystems
                                 where l.GetType().FullName.IgnoreCaseCompare(typeof(AppFileLoggingSystem).FullName)
                                 select l).FirstOrDefault();
            if (loggingSystem == null)
            {
                SharedData.ActiveLogger.LoggingSystems.Add(new AppFileLoggingSystem(maxLogSize));
            }
        }

        public static void AddConsoleLoggingSystem()
        {
            var loggingSystem = (from l in SharedData.ActiveLogger.LoggingSystems
                                 where l.GetType().FullName.IgnoreCaseCompare(typeof(ConsoleLoggingSystem).FullName)
                                 select l).FirstOrDefault();
            if (loggingSystem == null)
            {
                SharedData.ActiveLogger.LoggingSystems.Add(new ConsoleLoggingSystem());
            }
        }

        public static void AddLoggingSystem(ILoggingSystem loggingSystem)
        {
            SharedData.ActiveLogger.LoggingSystems.Add(loggingSystem);
        }

        public static void AddFileLoggingSystem(string fileName)
        {
            AddLoggingSystem(new FileLoggingSystem(fileName, 10485760, QueueProcessMode.Asynchronous));
        }

        public static ILogger AddFileLogger(string fileName)
        {
#if !REF_BMC_COMMON
            return new BMCLogger(LoggingSystems);
#else
            return new BMCCommonLogger(fileName, false);
#endif
        }

        public static void Info(ModuleProc proc, string message)
        {
            SharedData.ActiveLogger.WriteLogInfo(proc, message);
        }

        public static void Info(string message)
        {
            SharedData.ActiveLogger.WriteLogInfo(message);
        }

        public static void InfoV(string message, params object[] args)
        {
            SharedData.ActiveLogger.WriteLogInfo(string.Format(message, args));
        }

        public static void InfoV(ModuleProc proc, string message, params object[] args)
        {
            SharedData.ActiveLogger.WriteLogInfo(proc, string.Format(message, args));
        }

        public static void Debug(ModuleProc proc, string message)
        {
            if (LogType == LogEntryType.All ||
                ((LogType & LogEntryType.Debug) == LogEntryType.Debug))
            {
                SharedData.ActiveLogger.WriteLogInfo(proc, message);
            }
        }

        public static void Description(ModuleProc proc, string message)
        {
            if ((LogType & LogEntryType.Description) == LogEntryType.Description)
            {
                SharedData.ActiveLogger.WriteLogInfo(proc, message);
            }
        }

        public static void DescriptionV(ModuleProc proc, string message, params object[] args)
        {
            if ((LogType & LogEntryType.Description) == LogEntryType.Description)
            {
                SharedData.ActiveLogger.WriteLogInfo(proc, string.Format(message, args));
            }
        }

        public static void Exception(ModuleProc proc, Exception ex)
        {
            SharedData.ActiveLogger.WriteLogException(proc, ex);
        }

        public static void Exception(Exception ex)
        {
            SharedData.ActiveLogger.WriteLogException(ex);
        }

        public static void Warning(string message)
        {
            SharedData.ActiveLogger.WriteLogWarning(message);
        }

        public static event LogFormatMessageHandler LogFormatMessage
        {
            add { SharedData.ActiveLogger.LogFormatMessage += value; }
            remove { SharedData.ActiveLogger.LogFormatMessage -= value; }
        }

        public static event WriteToExternalLogHandler WriteToExternalLog
        {
            add { SharedData.ActiveLogger.WriteToExternalLog += value; }
            remove { SharedData.ActiveLogger.WriteToExternalLog -= value; }
        }

        public static event WriteToExternalLogHandler GlobalWriteToExternalLog;

        internal static void OnGlobalWriteToExternalLog(string formattedMessage)
        {
            try
            {
                if (GlobalWriteToExternalLog != null)
                    GlobalWriteToExternalLog(formattedMessage, LogEntryType.Information, null);
            }
            catch { }
        }

        internal static void OnGlobalWriteToExternalLog(Exception ex)
        {
            try
            {
                if (GlobalWriteToExternalLog != null)
                    GlobalWriteToExternalLog(ex.GetAllExceptions(), LogEntryType.Exception, null);
            }
            catch { }
        }

        public static ILogMethod LogMethod(ModuleProc proc)
        {
            return LogMethod(SharedData.ActiveLogger, proc);
        }

        public static ILogMethod LogMethod(ILogger logger, ModuleProc proc)
        {
            return new __LogMethod
            {
                Logger = logger,
                PROC = proc,
            };
        }

        public static ILogMethod LogMethod(string module, string procedure)
        {
            return LogMethod(SharedData.ActiveLogger, module, procedure);
        }

        public static ILogMethod LogMethod(ILogger logger, string module, string procedure)
        {
            return new __LogMethod
            {
                Logger = logger,
                PROC = new ModuleProc(module, procedure),
            };
        }

        public static ILogMethod LogMethod(MethodBase methodBase)
        {
            return LogMethod(SharedData.ActiveLogger, methodBase);
        }

        public static ILogMethod LogMethod(ILogger logger, MethodBase methodBase)
        {
            return new __LogMethod
            {
                Logger = logger,
                PROC = new ModuleProc(methodBase),
            };
        }
    }

    public interface ILogMethod : IDisposable
    {
        ModuleProc PROC { get; }
        ILogger Logger { get; set; }

        void Enter();
        void Exit();

        void Info(string message);
        void InfoV(string message, params object[] args);
        void Debug(string message);
        void DebugV(string message, params object[] args);
        void Exception(Exception ex);
    }

    internal class __LogMethod : ILogMethod, IDisposable
    {
        private ModuleProc _proc;
        private static bool _canLogMethod = false;

        static __LogMethod()
        {
            _canLogMethod = Extensions.GetAppSettingValueBool("LOG_METHOD_ENTER_EXIT", false);
        }

        public ModuleProc PROC
        {
            get
            {
                return _proc;
            }
            internal set
            {
                _proc = value;
                this.Enter();
            }
        }

        public ILogger Logger { get; set; }

        public void Enter()
        {
            if (_canLogMethod) Log.Info(_proc, "Entered.");
        }

        public void Exit()
        {
            if (_canLogMethod) Log.Info(_proc, "Exited.");
        }

        public void Dispose()
        {
            this.Exit();
        }

        public void Info(string message)
        {
            this.Logger.WriteLogInfo(this.PROC, message);
        }

        public void InfoV(string message, params object[] args)
        {
            this.Logger.WriteLogInfo(this.PROC, string.Format(message, args));
        }

        public void Debug(string message)
        {
            if (Log.LogType == LogEntryType.All ||
                ((Log.LogType & LogEntryType.Debug) == LogEntryType.Debug))
            {
                this.Logger.WriteLogInfo(this.PROC, message);
            }
        }

        public void DebugV(string message, params object[] args)
        {
            if (Log.LogType == LogEntryType.All ||
                ((Log.LogType & LogEntryType.Debug) == LogEntryType.Debug))
            {
                this.Logger.WriteLogInfo(this.PROC, string.Format(message, args));
            }
        }

        public void Exception(Exception ex)
        {
            this.Logger.WriteLogException(this.PROC, ex);
        }
    }
}
