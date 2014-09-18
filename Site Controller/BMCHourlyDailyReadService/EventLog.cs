using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace BMC.HourlyDailyReadJobs
{
   public static class HourlyDailyEventLog
    { 
        //public  string EventLogSource = "Hourly";
        //public  string EventLogName = "HourlyDailyService";
        //public  string EventLogType = "Application";

        #region Private Functions
        private static bool CreateLog(string sLogName)
        {
            bool bLog = false;
            EventLog HourlyDailyEventLog = new EventLog();
            try
            {
                if (!(EventLog.SourceExists(sLogName))) { EventLog.CreateEventSource(sLogName, sLogName); }            
                
                HourlyDailyEventLog.Source = sLogName;
                HourlyDailyEventLog.Log = sLogName;
                HourlyDailyEventLog.Source = sLogName;
                HourlyDailyEventLog.WriteEntry("The " + sLogName + " was successfully initialized ", EventLogEntryType.Information);
                bLog = true;
            }
            catch 
            {                
                bLog = false;
            }
            return bLog;
        }       

        private static bool DeleteLog(string sLogName)
        {
            bool bLog = false;
            try
            {
                if (EventLog.Exists(sLogName))
                {
                    EventLog.Delete(sLogName);
                    bLog = true;
                }
            }
            catch
            {
                bLog = false;
            }
            return bLog;
        }

        private static void DisplayEventLogProperties()
        {
           

            EventLog[] eventLogs = EventLog.GetEventLogs();
            foreach (EventLog e in eventLogs)
            {
                Int64 sizeKB = 0;
               
                //Console.WriteLine("{0}:", e.LogDisplayName);
               
                // Determine if there is an event log file for this event log.
                Microsoft.Win32.RegistryKey regEventLog = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\EventLog\\" + e.Log);
                if (regEventLog != null)
                {
                    Object temp = regEventLog.GetValue("File");
                    if (temp != null)
                    {
                        Console.WriteLine("  Log file path = \t {0}", temp.ToString());
                        System.IO.FileInfo file = new System.IO.FileInfo(temp.ToString());

                        // Get the current size of the event log file.
                        if (file.Exists)
                        {
                            sizeKB = file.Length / 1024;
                            if ((file.Length % 1024) != 0)
                            {
                                sizeKB++;
                            }
                            Console.WriteLine("  Current size = \t {0} kilobytes", sizeKB.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("  Log file path = \t <not set>");
                    }
                }

                // Display the maximum size and overflow settings.

                sizeKB = e.MaximumKilobytes;
                Console.WriteLine("  Maximum size = \t {0} kilobytes", sizeKB.ToString());
                Console.WriteLine("  Overflow setting = \t {0}", e.OverflowAction.ToString());

                switch (e.OverflowAction)
                {
                    case OverflowAction.OverwriteOlder:
                        Console.WriteLine("\t Entries are retained a minimum of {0} days.",
                            e.MinimumRetentionDays);
                        break;
                    case OverflowAction.DoNotOverwrite:
                        Console.WriteLine("\t Older entries are not overwritten.");
                        break;
                    case OverflowAction.OverwriteAsNeeded:
                        Console.WriteLine("\t If number of entries equals max size limit, a new event log entry overwrites the oldest entry.");
                        break;
                    default:
                        break;
                }
            }
        }


        private static void CheckEventLogSize()
        {
             Microsoft.Win32.RegistryKey DailyHourlyEventLog = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\EventLog\\Application",true);
           
//TestEventLog.SetValue(”MaxSize”, “102367232″, RegistryValueKind.DWord);
//TestEventLog.SetValue(”Retention”, “0″, RegistryValueKind.DWord);

        }

        #endregion Private Functions

        #region Public Functions
        public static void WriteToEventLog(string sLogName, string sLogSource, string sErrorDetail,EventLogEntryType evType)
        {
             EventLog HourlyDailyEventLog = new EventLog();
             try
             {

                 if (!(EventLog.SourceExists(sLogName))) { CreateLog(sLogName); }

                 HourlyDailyEventLog.Source = sLogName;
                 HourlyDailyEventLog.WriteEntry(Convert.ToString(sLogSource)
                     + Convert.ToString(sErrorDetail), evType);
             }
             catch (Exception ex)
             {
                 HourlyDailyEventLog.Source = sLogName;
                 HourlyDailyEventLog.WriteEntry(Convert.ToString("INFORMATION: ")
                                       + Convert.ToString(ex.Message), EventLogEntryType.Error);
             }
             finally
             {
                 HourlyDailyEventLog.Dispose();
                 HourlyDailyEventLog = null;               
             }
        }
        #endregion Public Functions
    }
}
