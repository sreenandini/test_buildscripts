using System;
using System.Globalization;

namespace BMC.Presentation.POS.Helper_classes
{
    public static class ExtensionMethods
    {
        public static string GetUniversalDateFormat(this DateTime dateTime)
        {
            return String.Format("{0:dd'/'MM'/'yyyy}", dateTime);
        }

        public static string GetUniversalTimeFormat(this DateTime dateTime)
        {
            return String.Format("{0:HH':'mm':'ss}", dateTime);
        }

        public static string GetUniversalDateTimeFormat(this DateTime dateTime)
        {
            return String.Format("{0:dd'/'MM'/'yyyy HH':'mm':'ss}", dateTime);
        }

        public static string GetUniversalCurrencyFormatWithSymbol(this decimal decimalvalue)
        {
            string ReturnString = String.Format(new RegionInfo("it-IT").CurrencySymbol + " {0:###,##0.00}", decimalvalue);
            ReturnString = ReturnString.Replace(".", "~");
            ReturnString = ReturnString.Replace(",", ".");
            ReturnString = ReturnString.Replace("~", ",");
            return ReturnString;
        }

        public static string GetUniversalCurrencyFormat(this decimal decimalvalue)
        {
            string ReturnString = String.Format("{0:###,##0.00}", decimalvalue);
            ReturnString = ReturnString.Replace(".", "~");
            ReturnString = ReturnString.Replace(",", ".");
            ReturnString = ReturnString.Replace("~", ",");
            return ReturnString;
            //return String.Format("{0:###,###.00}", decimalvalue);
        }

        public static DateTime ReadDate(this string dateValue)
        {
            return DateTime.ParseExact(dateValue, "dd/MM/yyyy", new CultureInfo("en-US"));
        }

        public static DateTime ReadTimeWithoutSeconds(this string timeValue)
        {
            return DateTime.ParseExact(timeValue, "HH:mm", new CultureInfo("en-US"));
        }

        public static DateTime ReadTimeWithSeconds(this string timeValue)
        {
            return DateTime.ParseExact(timeValue, "HH:mm:ss", new CultureInfo("en-US"));
        }

        public static string GetCurrencySymbol(this string currencySymbol)
        {
            return new RegionInfo("it-IT").CurrencySymbol;
        }
    }
}
