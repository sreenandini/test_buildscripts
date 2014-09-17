using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using WPFAPP = System.Windows.Application;
using WPFCTL = System.Windows.Controls;
using System.Diagnostics;
using System.Threading;

namespace BMC.Common.Utilities
{
    public static class ExtensionMethods
    {
        public static string CurrentSiteCulture = null;
        public static string CurrentCurrenyCulture = null;
        public static string CurrentDateCulture = null;

        static ExtensionMethods()
        {
            if (!IsInDesignMode())
            {
                try
                {
                    CurrentSiteCulture = ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForRegion");
                    CurrentCurrenyCulture = ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForCurrency");
                    CurrentDateCulture = ConfigurationManagement.ConfigManager.Read("GetDefaultCultureForDate");
                }
                catch
                {
                    CurrentSiteCulture = CurrentCurrenyCulture = CurrentDateCulture = "en-US";
                }
            }
        }

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalDateFormat(this DateTime dateTime)
        {
            //Change Request #203622 fix.
            //return String.Format(new CultureInfo(CurrentDateCulture), "{0:d}", dateTime);
            return dateTime.ToString(Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetZoneDateinUTCFormat(this string TIMEZONEID)
        {
            TimeZoneInfo tZone = TimeZoneInfo.FindSystemTimeZoneById(TIMEZONEID);
            DateTimeOffset utcDto = new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero);
            DateTimeOffset localDateTime = TimeZoneInfo.ConvertTime(utcDto, tZone);
            return localDateTime.ToString(new CultureInfo(CurrentDateCulture));
        }

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static object ToShortUTC(this DateTime? dateTime)
        {
            if (dateTime == null || !dateTime.HasValue) return DBNull.Value;
            return dateTime.Value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// Gets the minimum date value
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static DateTime DBMinValue(this DateTime dateTime)
        {
            return new DateTime(1800, 1, 1, 0, 0, 0);
        }


        /// <summary>
        /// Gets the universal time format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalTimeFormat(this DateTime dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:T}", dateTime);

        }

        /// <summary>
        /// Gets the universal date time format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalDateTimeFormat(this DateTime dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:G}", dateTime);
        }

        /// <summary>
        /// Gets the universal date time format without seconds.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalDateTimeFormatWithoutSeconds(this DateTime dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:g}", dateTime);
        }

        /// <summary>
        /// Gets the universal date format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalDateFormat(this DateTime? dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:d}", dateTime);
        }

        /// <summary>
        /// Gets the universal time format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalTimeFormat(this DateTime? dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:T}", dateTime);
        }

        /// <summary>
        /// Gets the universal time format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetDuration(this TimeSpan timeSpan)
        {

            return timeSpan.Hours + GetTimeDelimiter() + timeSpan.Minutes + GetTimeDelimiter() + timeSpan.Seconds;

        }

        /// <summary>
        /// Gets the universal date time format.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalDateTimeFormat(this DateTime? dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:G}", dateTime);
        }

        /// <summary>
        /// Gets the universal date time format without seconds.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string GetUniversalDateTimeFormatWithoutSeconds(this DateTime? dateTime)
        {
            return String.Format(new CultureInfo(CurrentDateCulture), "{0:g}", dateTime);
        }

        /// <summary>
        /// Gets the universal currency format with symbol.
        /// </summary>
        /// <param name="decimalvalue">The decimalvalue.</param>
        /// <returns></returns>
        public static string GetUniversalCurrencyFormatWithSymbol(this decimal decimalvalue)
        {
            var cultureInfo = new CultureInfo(CurrentCurrenyCulture);
            var returnString = new RegionInfo(CurrentSiteCulture).CurrencySymbol + " " + String.Format(cultureInfo, "{0:###,##0.00}", decimalvalue);
            return returnString;
        }

        public static string GetUniversalCurrencyFormatWithSymbolForRecipts(this decimal decimalvalue)
        {
            var cultureInfo = new CultureInfo(CurrentCurrenyCulture);
            var CurrencyFormat = new RegionInfo(CurrentSiteCulture).CurrencySymbol;
            var returnString = "";
            string CurrencyValue = string.Empty;

            if (CurrencyFormat == "£")
            {
                CurrencyValue = "GBP";
                returnString = CurrencyValue + "  " + String.Format(cultureInfo, "{0:###,##0.00}", decimalvalue);
            }
            else if (CurrencyFormat == "€")
            {
                CurrencyValue = "EUR";
                returnString = CurrencyValue + "  " + String.Format(cultureInfo, "{0:###.##0,00}", decimalvalue);
            }
            else
            {
                returnString = new RegionInfo(CurrentSiteCulture).CurrencySymbol + " " + String.Format(cultureInfo, "{0:###,##0.00}", decimalvalue);
            }

            return returnString;
        }

        /// <summary>
        /// Gets the universal currency format with symbol.
        /// </summary>
        /// <param name="decimalvalue">The decimalvalue Formatted to 0 decimal places(To Format Total Bills).</param>
        /// <returns></returns>
        public static string GetUniversalCurrencyFormatWithSymbolFormattedToZeroDecimal(this decimal decimalvalue)
        {
            var cultureInfo = new CultureInfo(CurrentCurrenyCulture);
            var returnString = new RegionInfo(CurrentSiteCulture).CurrencySymbol + " " + String.Format(cultureInfo, "{0:###,##0}", decimalvalue);
            return returnString;
        }

        /// <summary>
        /// Gets the universal currency format.
        /// </summary>
        /// <param name="decimalvalue">The decimalvalue.</param>
        /// <returns></returns>
        public static string GetUniversalCurrencyFormat(this decimal decimalvalue)
        {
            if (decimalvalue == 0)
                return "0";
            var cultureInfo = new CultureInfo(CurrentCurrenyCulture);
            var returnString = String.Format(cultureInfo, "{0:###,##0.00}", decimalvalue);
            return returnString;
        }


        /// <summary>
        /// Gets the universal currency format.
        /// </summary>
        /// <param name="doublevalue">The doublevalue.</param>
        /// <returns></returns>
        public static string GetUniversalCurrencyFormatForDouble(this double? doublevalue)
        {
            //if (doublevalue == null || doublevalue == 0)
            //    return "0";

            if (doublevalue != null & doublevalue == 0)
            {

            }
            var cultureInfo = new CultureInfo(CurrentCurrenyCulture);
            var returnString = String.Format(cultureInfo, "{0:###,##0.00}", doublevalue);
            return returnString;
        }

        /// <summary>
        /// Gets the single from string.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        /// <returns></returns>
        public static Single GetSingleFromString(this string stringValue)
        {
            return Convert.ToSingle(stringValue, new CultureInfo(CurrentCurrenyCulture));
        }

        /// <summary>
        /// Gets the float from string.
        /// </summary>
        /// <param name="currencyValue">The currency value.</param>
        /// <returns></returns>
        public static float GetFloatFromString(this string currencyValue)
        {
            float returnValue;
            float.TryParse(currencyValue, NumberStyles.Currency, new CultureInfo(CurrentCurrenyCulture), out returnValue);
            return returnValue;
        }

        /// <summary>
        /// Reads the date.
        /// </summary>
        /// <param name="dateValue">The date value.</param>
        /// <returns></returns>
        public static DateTime ReadDate(this string dateValue)
        {
            DateTime newDateTime;
            if (!DateTime.TryParse(dateValue, out newDateTime)) newDateTime = DateTime.MinValue;
            return newDateTime;
        }

        /// <summary>
        /// Reads the time without seconds.
        /// </summary>
        /// <param name="timeValue">The time value.</param>
        /// <returns></returns>
        public static DateTime ReadTimeWithoutSeconds(this string timeValue)
        {
            return DateTime.Parse(timeValue, new CultureInfo(CurrentDateCulture));
        }

        /// <summary>
        /// Reads the time with seconds.
        /// </summary>
        /// <param name="timeValue">The time value.</param>
        /// <returns></returns>
        public static DateTime ReadTimeWithSeconds(this string timeValue)
        {
            return DateTime.Parse(timeValue, new CultureInfo(CurrentDateCulture));
        }

        /// <summary>
        /// Reads the date.
        /// </summary>
        /// <param name="dateValue">The date value.</param>
        /// <returns></returns>
        public static DateTime ReadDateTimeWithSeconds(this string dateValue)
        {
            return DateTime.Parse(dateValue, new CultureInfo(CurrentDateCulture));
        }

        /// <summary>
        /// Reads the date.
        /// </summary>
        /// <param name="dateValue">The date value.</param>
        /// <returns></returns>
        public static DateTime ReadDateTimeWithoutSeconds(this string dateValue)
        {
            DateTime returnDateValue;
            DateTime.TryParse(dateValue, out returnDateValue);
            return returnDateValue;
        }

        /// <summary>
        /// Gets the currency symbol.
        /// </summary>
        /// <param name="currencySymbol">The currency symbol.</param>
        /// <returns></returns>
        public static string GetCurrencySymbol(this string currencySymbol)
        {
            return new RegionInfo(CurrentSiteCulture).CurrencySymbol;
        }

        /// <summary>
        /// converts to decimal
        /// </summary>
        /// <param name="stringValue">string to convert</param>
        /// <returns></returns>
        public static decimal GetDecimal(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return 0;
            var decimalValue = Convert.ToDecimal(0);
            decimal.TryParse(stringValue, out decimalValue);
            return decimalValue;
        }


        public static string GetCurrencyDecimalDelimiter()
        {
            var d = Convert.ToDecimal(1.1);
            decimal.TryParse(d.ToString(new CultureInfo(CurrentCurrenyCulture)), NumberStyles.Currency, new CultureInfo(CurrentCurrenyCulture), out d);
            return d.ToString(new CultureInfo(CurrentCurrenyCulture)).Substring(1, 1);
        }

        public static string GetTimeDelimiter()
        {
            DateTime dt = new DateTime(1, 1, 1, 12, 12, 12);
            string s = dt.GetUniversalTimeFormat();
            return s.Substring(2, 1);
        }

        public static string GetCurrencyCoinSymbol()
        {
            switch (CurrentSiteCulture)
            {
                case "en-GB":
                    return "p";
                default:
                    return "¢";
            }

        }

        public static string ToDateTimeString(this object item)
        {
            DateTime result;
            if (DateTime.TryParse(item.ToString(), out result))
                return result.ToString("dd MMM yyyy HH:mm:ss", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            else
                return item.ToString();
        }

        public static string ToShortDateTimeString(this object item)
        {
            DateTime result;
            if (DateTime.TryParse(item.ToString(), out result))
                return result.ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture)) + " "
                    + result.ToString("HH:mm:ss", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            else
                return item.ToString();
        }

        public static string ToShortDateString(this object item)
        {
            DateTime result;
            if (DateTime.TryParse(item.ToString(), out result))
                return result.ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            else
                return item.ToString();
        }

        public static bool IsNullOrEmpty(this string s)
        {

            return System.String.IsNullOrEmpty(s);

        }
        public static bool IsValidDigit(this char s)
        {
            return Convert.ToBoolean(!Char.IsDigit(s) && s != ((char)8));
        }
        public static bool IsValidText(this char s)
        {
            int ascii = (int)s;
            return !Convert.ToBoolean((ascii >= 32 || ascii <= 122) && (ascii != 34 && ascii != 37 && ascii != 95 && ascii != 96));
        }

        public static bool IsLengthGreaterThanZero(this string s)
        {
            return (!System.String.IsNullOrEmpty(s)) && (s.Length > 0);
        }

        public static bool IsValidDecimalNumber(this char s)
        {
            return Convert.ToBoolean(!Char.IsDigit(s) && s != ((char)8) && s != ((char)46));
        }

        public static bool IsNumeric(this string s)
        {
            Regex reg = new Regex("^[0-9]");
            return reg.IsMatch(s);
        }

        public static bool IsValidEmail(this string s)
        {
            string strPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            Regex reg = new Regex(strPattern);
            return reg.IsMatch(s);
        }
        public static string NullToString(this string str)
        {
            if (str == null)
                return string.Empty;
            else
                return str;
        }

        public static string DayOfDate(this object item)
        {
            DateTime result;
            if (DateTime.TryParse(item.ToString(), out result))
                return result.ToString("ddd", new CultureInfo(ExtensionMethods.CurrentDateCulture).DateTimeFormat);
            else
                return "";
        }

        #region Visual Studio Specific Members
        /// <summary>
        /// Determines whether [is in design mode].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is in design mode]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInDesignMode()
        {
            bool returnFlag = false;

#if DEBUG
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"))
            {
                returnFlag = true;
            }
#endif
            return returnFlag;

        }
        #endregion
    }

}

namespace BMC.Common
{
    public static class ConfigExtensions
    {
        #region Config Settings
        public static string GetAppSettingValue(string keyName, string defaultValue)
        {
            try
            {
                string value = ConfigurationManager.AppSettings.Get(keyName);
                if (string.IsNullOrEmpty(value)) return defaultValue;
                return value;
            }
            catch { return defaultValue; }
        }

        public static int GetAppSettingValueInt(string keyName, int defaultValue)
        {
            int result = 0;
            try
            {
                int.TryParse(GetAppSettingValue(keyName, defaultValue.ToString()), out result);
            }
            catch { }
            return result;
        }

        public static bool GetAppSettingValueBool(string keyName, bool defaultValue)
        {
            bool result = false;
            try
            {
                bool.TryParse(GetAppSettingValue(keyName, defaultValue.ToString()), out result);
            }
            catch { }
            return result;
        }
        #endregion
    }

    public static class ResourceExtensions
    {
        private static IDictionary<int, ResourceManager> _resourceManagers = null;
        private static Type _controlType = typeof(Control);
        private static Type _columnHeaderType = typeof(ColumnHeader);
        private static Type _gridViewColumnType = typeof(DataGridViewColumn);
        private static Type _toolStripItemType = typeof(ToolStripItem);

        private static WPFAPP _wpfApp = null;

        static ResourceExtensions()
        {
            _resourceManagers = new SortedDictionary<int, ResourceManager>();
        }

        public static ResourceManager GetResourceManager(int resourceType)
        {
            if (_resourceManagers.ContainsKey(resourceType))
            {
                return _resourceManagers[resourceType];
            }
            return null;
        }

        public static void RegisterResource(string resourceName, Assembly resourceAssembly)
        {
            RegisterResource(resourceName, 0, resourceAssembly);
        }

        public static void RegisterResource(string resourceName, int resourceType, Assembly resourceAssembly)
        {
            try
            {
                RegisterResource(resourceType, new ResourceManager(resourceName, resourceAssembly));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static void RegisterResource(ResourceManager resourceManager)
        {
            RegisterResource(0, resourceManager);
        }

        public static void RegisterResource(int resourceType, ResourceManager resourceManager)
        {
            resourceManager.IgnoreCase = true;
            if (!_resourceManagers.ContainsKey(resourceType))
            {
                _resourceManagers.Add(resourceType, resourceManager);
            }
            else
            {
                _resourceManagers[resourceType] = resourceManager;
            }
        }

        public static void RegisterWpfResource()
        {
            if (_wpfApp == null &&
                WPFAPP.Current != null)
            {
                _wpfApp = WPFAPP.Current;
            }
        }

        public static void ResolveResources(this Control owner)
        {
            ResolveResources(owner, 0);
        }

        public static void ResolveResources(this Control owner, int resourceType)
        {
            if (GetResourceManager(resourceType) == null) return;
            Stack<IComponent> st = new Stack<IComponent>();

            try
            {
                // find the text for this control
                FindResourceText(owner);

                // push all the child controls into stack
                AddControlsToStack(st, owner);

                // push all the grandchildren into stack and process again
                while (st.Count != 0)
                {
                    IComponent ctl = st.Pop() as IComponent;
                    FindResourceText(ctl);
                    AddControlsToStack(st, ctl);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private static void AddControlsToStack(Stack<IComponent> st, IComponent ctl)
        {
            IList controls = null;
            Type controlType = _controlType;

            if (ctl is DataGridView)
            {
                controls = (ctl as DataGridView).Columns;
                controlType = _gridViewColumnType;
            }
            else if (ctl is ListView)
            {
                controls = (ctl as ListView).Columns;
                controlType = _columnHeaderType;
            }
            else if (ctl is ToolStripDropDownItem)
            {
                controls = (ctl as ToolStripDropDownItem).DropDownItems;
                controlType = _toolStripItemType;

            }
            else if (ctl is ToolStrip)
            {
                controls = (ctl as ToolStrip).Items;
                controlType = _toolStripItemType;

            }
            else if (ctl is Control)
            {
                controls = (ctl as Control).Controls;
            }

            if (controls != null)
            {
                foreach (var child in controls)
                {
                    if (controlType.IsAssignableFrom(child.GetType()))
                    {
                        st.Push(child as IComponent);
                    }
                }
            }
        }

        private static void FindResourceText<C>(C control)
            where C : IComponent
        {
            FindResourceText(control, 0);
        }

        private static void FindResourceText<C>(C control, int resourceType)
            where C : IComponent
        {
            Type controlType = control.GetType();

            if (_gridViewColumnType.IsAssignableFrom(controlType))
            {
                FindResourceText((DataGridViewColumn)Convert.ChangeType(control, controlType), resourceType);
            }
            else if (_columnHeaderType.IsAssignableFrom(controlType))
            {
                FindResourceText((ColumnHeader)Convert.ChangeType(control, controlType), resourceType);
            }
            else if (_toolStripItemType.IsAssignableFrom(controlType))
            {
                FindResourceText((ToolStripItem)Convert.ChangeType(control, controlType), resourceType);
            }
            else if (_controlType.IsAssignableFrom(controlType))
            {
                FindResourceText((Control)Convert.ChangeType(control, controlType), resourceType);
            }
        }

        private static void FindResourceText(Control control)
        {
            FindResourceText(control, 0);
        }

        private static void FindResourceText(Control control, int resourceType)
        {
            FindResourceText<Control>(control, control.Tag, control.Text, (t) => { control.Text = t; }, resourceType);
        }

        private static void FindResourceText(DataGridViewColumn control)
        {
            FindResourceText(control, 0);
        }

        private static void FindResourceText(DataGridViewColumn control, int resourceType)
        {
            FindResourceText<DataGridViewColumn>(control, control.Tag, control.HeaderText, (t) => { control.HeaderText = t; }, resourceType);
        }

        public static void FindResourceText(ToolStripItem control, int resourceType)
        {
            FindResourceText<ToolStripItem>(control, control.Tag, control.Text, (t) => { control.Text = t; }, resourceType);
        }

        private static void FindResourceText(ColumnHeader control)
        {
            FindResourceText(control, 0);
        }

        private static void FindResourceText(ColumnHeader control, int resourceType)
        {
            FindResourceText<ColumnHeader>(control, control.Tag, control.Text, (t) => { control.Text = t; }, resourceType);
        }

        private static void FindResourceText<C>(C control, object componentTag, string componentText, Action<string> setText, int resourceType)
            where C : IComponent
        {
            try
            {
                Type controlType = typeof(C);
                string keyName = string.Empty;
                string replaceText = string.Empty;

                // any tag is set 
                if (componentTag != null &&
                    componentTag.GetType() == typeof(string))
                {
                    keyName = componentTag.ToString();
                }

                // any text is set with special prefix
                if (!typeof(TextBoxBase).IsAssignableFrom(controlType) &&
                    !string.IsNullOrEmpty(componentText))
                {
                    string text = componentText;
                    if (text.StartsWith("@@") &&
                        text.EndsWith("@@"))
                    {
                        keyName = text.Replace("@", "");
                    }
                    else if (text.Contains("@@"))
                    {
                        int start = text.IndexOf("@@") + 2;
                        int end = text.IndexOf("@@", start);
                        keyName = text.Substring(start, (end - start));
                        replaceText = "@@" + keyName + "@@";
                    }
                }

                if (!string.IsNullOrEmpty(keyName))
                {
                    // get the text from resource manager
                    string text = control.GetResourceTextByKey(keyName);
                    if (!string.IsNullOrEmpty(text))
                    {
                        if (!string.IsNullOrEmpty(replaceText))
                        {
                            text = componentText.Replace(replaceText, text);
                        }
                        setText(text);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public static string GetResourceTextByKey(this IComponent owner, string keyName)
        {
            return GetResourceTextByKey(owner, 0, keyName);
        }

        public static string GetResourceTextByKey(this IComponent owner, int resourceType, string keyName)
        {
            string result = default(string);

            try
            {
                if (_wpfApp != null)
                {
                    result = _wpfApp.TryFindResource(keyName).ToString();
                }
                else
                {
                    result = GetResourceManager(resourceType).GetString(keyName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }
}
