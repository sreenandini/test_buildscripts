using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Interfaces;

namespace BMC.Common.LogManagement
{
    /// <summary>
    /// This class has functions to Enter Logs in EventViewer
    /// Revision History:
    /// Date 08-Jan-2008    Created     Rakesh Marwaha
    /// </summary>
    class EventLogAdapter : ILoggingAdapter
    {
        #region Private Fields

        /// <summary>
        /// This will be used to tell its application type of log for logging in Eventlog
        /// </summary>
        private string m_slogName = "Application";

        #endregion

        #region Private Methods

        /// <summary>
        /// This will check whether application is actually running or not
        /// </summary>
        /// <param name="sApplicationName"></param>
        private void VerifyValidSource(string sApplicationName)
        {
            try
            {
                if (!EventLog.SourceExists(sApplicationName))
                {
                    EventLog.CreateEventSource(sApplicationName, m_slogName);
                }
            }
            catch (SecurityException)
            {
                throw new SecurityException("Eventlog permission is denied");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region ILoggingAdapter Members

        /// <summary>
        /// This function will write log in event viewer
        /// </summary>
        /// <param name="sApplicationName"></param>
        /// <param name="entry"></param>
        /// <param name="type"></param>
        public void WriteLog(string entry, int type)
        {
            string applicationName = "InternalException";
            try
            {

                // Verify that the Source exists before gathering exception information.
                VerifyValidSource(applicationName);

                // Write the entry to the Event Log.
                EventLog.WriteEntry(applicationName, entry, (EventLogEntryType)(type));
            }
            catch (SecurityException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ILoggingAdapter.WriteLog(string fileName, string message, int level)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
