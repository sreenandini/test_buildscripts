using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;
using System.Configuration;

namespace BMC.EventsTransmitter.Utils
{

//[Flags]
// enum LogLevel
//{
//        DEBUG=2 ,
//        INFO=4,
//        WARNING=8,
//        ERROR=16, 
//        ALL = 32
//}
    public class Log : IRelaucher
    {
        static Log objLog;
        BaseLog bLog;
        static object bLock = new object();
        private Log()
        {
            try
            {
                switch (ConfigurationManager.AppSettings["AppLogLevel"].NulltoString("DEBUG"))
                {
                    case "DEBUG":
                        bLog = new DebugLog();
                        break;
                    case "INFO":
                        bLog = new InfoLog();
                        break;
                    case "WARNING":
                        bLog = new WarningLog();
                        break;
                    case "ERROR":
                        bLog = new ErrorLog();
                        break;
                    default:
                        bLog = new DebugLog();
                        break;
                }
            }
            catch
            {
            }
        }
        public static Log GetInstance()
        {
            lock (bLock)
            {
                if (objLog == null)
                {
                    objLog = new Log();
                    Relauncher.GetInstance().RegisterForUpdate(objLog);
                }
            }
            return objLog;
        }
        public void Debug(string ClassName, string MethodName, string Text)
        {
            bLog.Debug(ClassName, MethodName, Text);
        }
        public void Info(string ClassName, string MethodName, string Text)
        {
            bLog.Info(ClassName, MethodName, Text);
        }
        public void Warning(string ClassName, string MethodName, string Text)
        {
            bLog.Warning(ClassName, MethodName, Text);
        }
        public void Error(string ClassName, string MethodName, string Text)
        {
            bLog.Error(ClassName, MethodName, Text);
        }
        public void Error(string ClassName, string MethodName, Exception Ex)
        {
            bLog.Error(ClassName, MethodName, Ex );
        }

        public void Debug(string Text)
        {
            bLog.Debug(Text);
        }
        public void Info(string Text)
        {
            bLog.Info(Text);
        }
        public void Warning(string Text)
        {
            bLog.Warning(Text);
        }
        public void Error(string Text)
        {
            bLog.Error(Text);
        }
        public void RefreshApp()
        {
            try
            {
                switch (ConfigurationManager.AppSettings["LogMode"].NulltoString("DEBUG"))
                {
                    case "DEBUG":
                        bLog = new DebugLog();
                        break;
                    case "INFO":
                        bLog = new InfoLog();
                        break;
                    case "WARNING":
                        bLog = new WarningLog();
                        break;
                    case "ERROR":
                        bLog = new ErrorLog();
                        break;
                    default:
                        bLog = new DebugLog();
                        break;
                }
            }
            catch
            {
            }
        }
    }

    public abstract class BaseLog
    {

        public void WriteLog(string Message, LogManager.enumLogLevel Level)
        {
            try
            {
                LogManager.WriteLog(Message, Level);
            }
            catch 
            {

            }
        }
        public virtual void Debug(string ClassName, string MethodName, string Text) { }
        public virtual void Info(string ClassName, string MethodName, string Text) { }
        public virtual void Warning(string ClassName, string MethodName, string Text) { }
        public virtual void Error(string ClassName, string MethodName, string Text) { }
        public virtual void Error(string ClassName, string MethodName, Exception Ex) 
        {
            BMC.Common.ExceptionManagement.ExceptionManager.Publish(Ex);
        }
        public virtual void Debug(string Text) { }
        public virtual void Info(string Text) { }
        public virtual void Warning(string Text) { }
        public virtual void Error(string Text) { }

    }
    /// <summary>
    /// Log Level 0 
    /// </summary>
    internal class DebugLog : BaseLog
    {
        public string strFormat = "{0} {1}->[{2}] Text:{3}";
        string strPlainFormat = "{0} {1}";
        public override void Debug(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Debug", ClassName, MethodName, Text), LogManager.enumLogLevel.Debug);

        }
        public override void Info(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Info", ClassName, MethodName, Text), LogManager.enumLogLevel.Info);
        }
        public override void Warning(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Warning", ClassName, MethodName, Text), LogManager.enumLogLevel.Warning);
        }
        public override void Error(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Error", ClassName, MethodName, Text), LogManager.enumLogLevel.Error);
        }

        public override void Debug(string Text)
        {
            WriteLog(string.Format(strPlainFormat, "Debug", Text), LogManager.enumLogLevel.Debug);
        }
        public override void Info(string Text)
        {

            WriteLog(string.Format(strPlainFormat, "Info", Text), LogManager.enumLogLevel.Info);
        }
        public override void Warning(string Text)
        {

            WriteLog(string.Format(strPlainFormat, "Warning", Text), LogManager.enumLogLevel.Warning);
        }
        public override void Error(string Text)
        {
            WriteLog(string.Format(strPlainFormat, "Error", Text), LogManager.enumLogLevel.Error);
        }

    }
    /// <summary>
    /// Log Level 1
    /// </summary>
    internal class InfoLog : BaseLog
    {
        public string strFormat = "{0} {1}->[{2}] Text:{3}";
        string strPlainFormat = "{0} {1}";

        public override void Info(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Info", ClassName, MethodName, Text), LogManager.enumLogLevel.Info);
        }
        public override void Warning(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Warning", ClassName, MethodName, Text), LogManager.enumLogLevel.Warning);
        }
        public override void Error(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Error", ClassName, MethodName, Text), LogManager.enumLogLevel.Error);
        }

        public override void Info(string Text)
        {

            WriteLog(string.Format(strPlainFormat, "Info", Text), LogManager.enumLogLevel.Info);
        }
        public override void Warning(string Text)
        {

            WriteLog(string.Format(strPlainFormat, "Warning", Text), LogManager.enumLogLevel.Warning);
        }
        public override void Error(string Text)
        {

            WriteLog(string.Format(strPlainFormat, "Error", Text), LogManager.enumLogLevel.Error);
        }


    }
    /// <summary>
    /// Log Level 2
    /// </summary>
    internal class WarningLog : BaseLog
    {
        public string strFormat = "{0} {1}->[{2}] Text:{3}";
        string strPlainFormat = "{0} {1}";

        public override void Warning(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Warning", ClassName, MethodName, Text), LogManager.enumLogLevel.Warning);
        }
        public override void Error(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Error", ClassName, MethodName, Text), LogManager.enumLogLevel.Error);
        }

        public override void Warning(string Text)
        {
            WriteLog(string.Format(strPlainFormat, "Warning", Text), LogManager.enumLogLevel.Warning);
        }
        public override void Error(string Text)
        {
            WriteLog(string.Format(strPlainFormat, "Error", Text), LogManager.enumLogLevel.Error);
        }

    }
    /// <summary>
    /// Log Level 3
    /// </summary>
    internal class ErrorLog : BaseLog
    {
        public string strFormat = "{0} {1}->[{2}] Text:{3}";
        string strPlainFormat = "{0} {1}";
        public override void Error(string ClassName, string MethodName, string Text)
        {
            WriteLog(string.Format(strFormat, "Error", ClassName, MethodName, Text), LogManager.enumLogLevel.Error);
        }
        public override void Error(string Text)
        {
            WriteLog(string.Format(strPlainFormat, "Error", Text), LogManager.enumLogLevel.Error);
        }
    }

}
