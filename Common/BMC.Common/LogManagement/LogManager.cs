using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using BMC.Common.Interfaces;
using System.Diagnostics;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;

namespace BMC.Common.LogManagement
{
    
    public static class LogManager
    {
        #region Types

        public enum enumLogLevel
        {
            Debug = 1,
            Info = 2,
            Warning = 4,
            Error = 8
        }

        #endregion

        #region Private Fields

        private static ILoggingAdapter _LoggingAdapter = null;
        private static ILoggingAdapter _DefaultLoggingAdapter = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="logMode"></param>
        public static void WriteLog(string message, enumLogLevel logLevel, string logMode)
        {
            try
            {
                if (logMode == "EVENTLOG")
                {
                    if (_DefaultLoggingAdapter == null)
                        _DefaultLoggingAdapter = GetObject(logMode);
                    _DefaultLoggingAdapter.WriteLog(message, (int)(EventLogEntryType.Information));
                }
                else
                {
                    if (_LoggingAdapter == null)
                        _LoggingAdapter = GetObject(logMode);
                    _LoggingAdapter.WriteLog(message, (int)logLevel);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method will write the log in a text file that is configured in the .config file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public static void WriteLog(string message, enumLogLevel logLevel)
        {
            try
            {
                string logMode = ConfigurationManager.AppSettings["LogMode"];
                if (_LoggingAdapter == null)
                    _LoggingAdapter = GetObject(logMode);
                _LoggingAdapter.WriteLog(message, (int)logLevel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method will write the log in the given text file.
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="logLevel"></param>
        public static void WriteLog(string fileName, string message, enumLogLevel level)
        {
            try
            {
                string logMode = ConfigurationManager.AppSettings["LogMode"];
                
                if (_LoggingAdapter == null)
                    _LoggingAdapter = GetObject(logMode);

                _LoggingAdapter.WriteLog(fileName, message, (int)level);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logMode"></param>
        /// <returns></returns>
        public static ILoggingAdapter GetObject(string logMode)
        {
            try
            {
                if (ExtensionMethods.IsInDesignMode())
                {
                    return new DesignTimeAdapter();
                }

                if (logMode == "XML")
                    return new XMLLogAdapter();
                else if (logMode == "DB")
                    return new DBLogAdapter();
                else if (logMode == "EVENTLOG")
                    return new EventLogAdapter();
                else
                    return TextLogAdapter.GetInstance();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

    public class DesignTimeAdapter : ILoggingAdapter
    {
        public void WriteLog(string message, int level)
        {
            
        }

        public void WriteLog(string fileName, string message, int level)
        {
            
        }
    }

}
