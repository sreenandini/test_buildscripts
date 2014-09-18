using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Presentation.CashDeskManager.HelperClasses
{
    public static class RegionalSetting
    {
        #region Regional Date Time format
        public static string GetRegionalDate(DateTime dtCurrentDate)
        {
            string strCurrentDate = string.Empty;
            // set currency format

            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

            System.Globalization.DateTimeFormatInfo DateFormat = new System.Globalization.CultureInfo(curCulture).DateTimeFormat;

            strCurrentDate = dtCurrentDate.ToString("D", DateFormat);
           
            return strCurrentDate;
        }

        public static string GetRegionalTime(DateTime dtCurrentTime)
        {
            string strCurrentTime = string.Empty;
            // set currency format

            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

            System.Globalization.DateTimeFormatInfo TimeFormat = new System.Globalization.CultureInfo(curCulture).DateTimeFormat;

            strCurrentTime = dtCurrentTime.ToString(TimeFormat.LongTimePattern);

            return strCurrentTime;
        }

        #endregion
    }
}
