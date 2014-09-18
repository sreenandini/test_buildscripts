using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;
using Microsoft.Win32;
using System.Globalization;
using System.Diagnostics;
using BMC.Common.Utilities;

namespace BMC.HourlyDailyReadJobs
{
    class RegistrySetting
    {
        #region Declarations
             Dictionary<string, string> RegistryEntries =null;
             string[] strSubKeyNames;
             string strRegPath = string.Empty;
             string[] strValueNames;
            String[] format = { "dd MMM yyyy ", "dd MMM yyyy hh:nn:ss " };
        #endregion

        public  void ReadRegistrySettings(string Registrypath)
        {
            string ReadDate = string.Empty;
            try
            {
                String readHour = BMCRegistryHelper.GetRegKeyValue(Registrypath, "HourlyReadHour");
                ReadDate=BMCRegistryHelper.GetRegKeyValue(Registrypath, "LastAutoRead");
                HourlyDailyEntity.HourlyReadHour = Convert.ToInt32(readHour);                
                DateTime dtRead;
                if(!string.IsNullOrWhiteSpace(ReadDate))
                {
                    dtRead = Convert.ToDateTime(ReadDate, CultureInfo.CurrentCulture);
                    dtRead.Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                    HourlyDailyEntity.LastAutoRead = dtRead.Date;
                }
                else
                {
                    dtRead = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString(), CultureInfo.CurrentCulture);                                     
                    dtRead.Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);                                    
                    HourlyDailyEntity.LastAutoRead = dtRead;
                }
            }
            catch(Exception ex)
            {
                LogManager.WriteLog("Please check the regional setting date format and LastAutoRead!! Date format from Registry: " + ReadDate.ToString() + " Regional setting date format: " + CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToString(), LogManager.enumLogLevel.Error);
                //HourlyDailyEventLog.WriteToEventLog(HourlyDailyEntity.EventLogName, "ReadRegistrySettings: ", "Message: " + ex.Message + "Source: " + ex.Source, EventLogEntryType.Error);
                ExceptionManager.Publish(ex);  
            }
        }




    }
}
