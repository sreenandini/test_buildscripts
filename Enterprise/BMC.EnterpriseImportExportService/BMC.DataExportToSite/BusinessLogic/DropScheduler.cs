using System;
using System.Globalization;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;

namespace BMC.DataExportToSite.BusinessLogic
{
    class DropScheduler
    {
        public static bool CompareTimes(DateTime dt1,DateTime dt2)
        {
            try
            {
               // LogManager.WriteLog("CompareTimes entry", LogManager.enumLogLevel.Info);
               // LogManager.WriteLog("Arguments : dt1 " + dt1.ToString() + " dt2 " + dt2.ToString(), LogManager.enumLogLevel.Debug);
                String date1 = dt1.ToString("MM/dd/yy H:mm");
                String date2 = dt2.ToString("MM/dd/yy H:mm");
               // LogManager.WriteLog("CompareTimes exit", LogManager.enumLogLevel.Info);
                return date1.Equals(date2);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
         }

        public static bool CompareTimes(DateTime currentDate,DateTime runDate,Double factor)
        {
            try
            {
                //LogManager.WriteLog("CompareTimes entry", LogManager.enumLogLevel.Info);
                //LogManager.WriteLog("Arguments : currentDate " + currentDate.ToString() + " runDate " + runDate.ToString(), LogManager.enumLogLevel.Debug);
                String strCurrentTime = currentDate.ToString("MM/dd/yy H:mm");
                String strRunTime = runDate.ToString("MM/dd/yy H:mm");
                String factoredTime = (runDate.AddMinutes(Convert.ToInt32(factor))).ToString("MM/dd/yy H:mm");
                //LogManager.WriteLog("CompareTimes exit", LogManager.enumLogLevel.Info);
                return (strRunTime.CompareTo(strCurrentTime) <= 0 && strCurrentTime.CompareTo(factoredTime) <= 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return false;
         }

        public static DateTime? GetNextOcc(DropScheduleEntity obj)
        {
            try
            {
                //LogManager.WriteLog("GetNextOcc entry", LogManager.enumLogLevel.Info);
                if (obj.ScheduleTime == null) return null;
                DateTime currentDate = OmitSeconds(DateTime.Now);
                switch (obj.ScheduleOccurance)
                {
                    case ScheduleOccurances.Daily: //Daily Calculation

                        DateTime scheduleDateTime = Convert.ToDateTime(((DateTime)obj.ScheduleTime).ToString("H:mm"));
                        if (DateTime.Compare(currentDate, scheduleDateTime) <= 0)
                            return ConcatDateandTime(DateTime.Now, (DateTime)obj.ScheduleTime);
                        if (DateTime.Compare(scheduleDateTime.AddDays(1), obj.EndDate) <= 0)
                            return ConcatDateandTime(DateTime.Now.AddDays(1), (DateTime)obj.ScheduleTime);
                        else
                            return null;

                    case ScheduleOccurances.Weekly: //Weekly Calculation
                        DateTime comingDate = (DateTime)ConcatDateandTime(DateTime.Now, (DateTime)obj.ScheduleTime);
                        while (comingDate <= obj.EndDate)
                        {
                            Int32 day = Convert.ToInt32(Math.Pow(Convert.ToDouble(2), Convert.ToDouble(comingDate.DayOfWeek)));
                            if ((obj.WeekDays & day) == day && currentDate <= comingDate)
                            {
                                return ConcatDateandTime(comingDate, (DateTime)obj.ScheduleTime);
                            }
                            else
                            {
                                comingDate = comingDate.AddDays(1);
                            }
                        }
                        break;

                    case ScheduleOccurances.Monthly: //Monthly Calculation

                        DateTime startDate = new DateTime(obj.StartDate.Year, obj.StartDate.Month, obj.StartDate.Day,
                            obj.ScheduleTime.Value.Hour, obj.ScheduleTime.Value.Minute, 0);
                        DateTime endDate = Convert.ToDateTime(obj.EndDate);
                        DateTime resultDate = startDate;
                        int MonthDays = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                        int OrgDayOfMonth = obj.DayOfMonth;
                        int MaxDaysInCurrentMonth = 0;
                        int DaysDiff = 0;
                        if (obj.DayOfMonth > MonthDays)
                            obj.DayOfMonth = MonthDays;
                        bool MaxDaySelected = false;

                        int intCurrentDay = 0;
                        while (startDate <= endDate)
                        {
                            MaxDaysInCurrentMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                            MaxDaySelected = (OrgDayOfMonth >= MaxDaysInCurrentMonth);

                            intCurrentDay = startDate.Day;
                            if ((intCurrentDay == obj.DayOfMonth) || (MaxDaySelected && (intCurrentDay == MaxDaysInCurrentMonth)))
                            {
                                if (startDate < currentDate)
                                {
                                    startDate = startDate.AddMonths(Convert.ToInt32(obj.MonthDuration));
                                }
                                else
                                {
                                    resultDate = (DateTime)ConcatDateandTime(startDate, Convert.ToDateTime(obj.ScheduleTime));
                                    break;
                                }
                                    
                            }
                            else
                            {
                                startDate = startDate.AddDays(1);
                            }
                        }


                        if (OrgDayOfMonth > resultDate.Day)
                        {
                            MaxDaysInCurrentMonth = DateTime.DaysInMonth(resultDate.Year, resultDate.Month);
                            DaysDiff = MaxDaysInCurrentMonth - resultDate.Day;
                            resultDate = resultDate.AddDays(DaysDiff);
                        }
                        else if (OrgDayOfMonth < resultDate.Day)
                        {
                            DaysDiff = OrgDayOfMonth - resultDate.Day;
                            resultDate = resultDate.AddDays(DaysDiff);
                        }

                        if (resultDate <= endDate) 
                            return resultDate;
                        break;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return null;
        }
        
        private static DateTime? ConcatDateandTime(DateTime date, DateTime time)
        {
            try
            {
                //LogManager.WriteLog("ConcatDateandTime entry", LogManager.enumLogLevel.Info);
                return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return null;
        }

        internal static Int32 GetRemainingHours(DateTime date1, DateTime date2)
        {
            //LogManager.WriteLog("GetRemainingHours entry", LogManager.enumLogLevel.Info);
            try
            {
                TimeSpan span = date1 - date2;
                return span.Hours;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return -10;
        }

        internal static DateTime OmitSeconds(DateTime date)
        {
            try
            {
                return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return date;
        }
    }
}
