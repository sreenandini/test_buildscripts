using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace BMC.EnterpriseReportsUI
{
    public static class ExtensionMethods
    {
        public static string CurrentSiteCulture         =   "en-US";
        public static string CurrentCurrenyCulture      =   "en-US";
        public static string CurrentDateCulture         =   "en-US";

        public static string GetCurrencySymbol(this string currencySymbol)
        {
            return new RegionInfo(CurrentSiteCulture).CurrencySymbol;
        }
    }
}
