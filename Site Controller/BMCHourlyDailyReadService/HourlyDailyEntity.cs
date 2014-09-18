using System;

namespace BMC.HourlyDailyReadJobs
{
    static class  HourlyDailyEntity
    { 
        public static string sRegistryPath = string.Empty;
        public static string EventLogName = "HourlyDailyService";
        #region Public properties

        public static int HourlyTry
        {
            get;
            set;
        }
        public static int DailyyTry
        {
            get;
            set;
        }
        public static string DailyAutoReadTime
        {
            get;
            set;
        }
        public static DateTime LastAutoRead
        {
            get;
            set;
        }
        public static int HourlyReadHour
        {
            get;
            set;
        }
        public static int HourlyReadInterval
        {
            get;
            set;
        }
        public static int BusinessDayAdjustment
        {
            get;
            set;
        }

        public static bool ShouldReadRunWithHourly
        {
            get;
            set;
        }

        //Included new property for the fix, Read run twice for a day
        public static bool HasReadRunWithHourly
        {
            get;
            set;
        }

        public static bool IsReadInHourly
        {
            get;
            set;
        }
    
        #endregion
         
    }
}
