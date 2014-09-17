/* ================================================================================= 
 * Purpose		:	Type System
 * File Name	:   TypeSystem.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	22/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 22/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Net;

namespace BMC.CoreLib
{
    /// <summary>
    /// Type System Static Class
    /// </summary>
    public static class TypeSystem
    {
        #region Static Variables

        /// <summary>
        /// Date/Time Format Info
        /// </summary>
        private static DateTimeFormatInfo _dateTimeFormatInfo = null;

        /// <summary>
        /// Date Pattern
        /// </summary>
        private static string[] _datePattern = null;

        /// <summary>
        /// Date Time Pattern
        /// </summary>
        private static string[] _dateTimePattern = null;

        /// <summary>
        /// Time Pattern
        /// </summary>
        private static string[] _timePattern = null;

        #endregion

        #region Static Constructors

        /// <summary>
        /// Initializes the <see cref="TypeSystem"/> class.
        /// </summary>
        static TypeSystem()
        {
            _dateTimeFormatInfo = new DateTimeFormatInfo();
            _datePattern = new string[] {
                "dd/MM/yyyy",
                "MM/dd/yyyy",
                "yyyy/MM/dd",
                "yyyy/dd/MM"
            };
            _dateTimePattern = new string[] {
                "dd/MM/yyyy HH:mm:ss.fff",
                "MM/dd/yyyy HH:mm:ss.fff",
                "yyyy/MM/dd HH:mm:ss.fff",
                "yyyy/dd/MM HH:mm:ss.fff"
            };
            _timePattern = new string[]{
                "HH:mm:ss.fff",
                "HH:mm:ss"
            };
#if !SILVERLIGHT
            _dateTimeFormatInfo.SetAllDateTimePatterns(_dateTimePattern, 'd');
#endif
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the value T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if succeeded; otherwise, <c>false</c>.</returns>
        public static bool GetValueT<T>(object value, out T result)
        {
            result = default(T);
            if (value == null)
            {
                return true;
            }
            else if (value.GetType() == typeof(T))
            {
                result = ((T)value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the value T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="func">The func.</param>
        /// <returns>Value of the specified type.</returns>
        public static T GetValueT<T>(object source, GetValueTryParseHandler<T> func)
        {
            T value = default(T);
            if (GetValueT<T>(source, out value)) return value;

            string text = GetValueString(source);
            if (text.IsEmpty()) return default(T);
            func(text, out value);
            return value;
        }

        /// <summary>
        /// Gets the value string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>String representation of the given value.</returns>
        public static string GetValueString(object value)
        {
            if (value == null) return string.Empty;
            return value.ToString();
        }

        /// <summary>
        /// Gets the value of char.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type char.</returns>
        public static char GetValueChar(object source)
        {
            return GetValueT<char>(source,
                delegate(string text, out char value)
                {
                    char.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of bool.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type bool.</returns>
        public static bool GetValueBool(object source)
        {
            return GetValueT<bool>(source,
                delegate(string text, out bool value)
                {
                    bool.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value bool simple.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type bool.</returns>
        public static bool GetValueBoolSimple(object source)
        {
            if (source != null)
            {
                string source2 = source.ToString().Trim();
                if (source2.IsEmpty()) return false;

                if (source2.IgnoreCaseCompare("true") ||
                    source2 == "1") return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the value of byte.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type byte.</returns>
        public static byte GetValueByte(object source)
        {
            return GetValueT<byte>(source,
                delegate(string text, out byte value)
                {
                    byte.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of sbyte.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type sbyte.</returns>
        public static sbyte GetValueSByte(object source)
        {
            return GetValueT<sbyte>(source,
                delegate(string text, out sbyte value)
                {
                    sbyte.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of short.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type short.</returns>
        public static short GetValueShort(object source)
        {
            return GetValueT<short>(source,
                delegate(string text, out short value)
                {
                    short.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of ushort.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type ushort.</returns>
        public static ushort GetValueUShort(object source)
        {
            return GetValueT<ushort>(source,
                delegate(string text, out ushort value)
                {
                    ushort.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of int.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type int.</returns>
        public static int GetValueInt(object source)
        {
            return GetValueT<int>(source,
                delegate(string text, out int value)
                {
                    int.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Parses input string and returns int value
        /// </summary>
        /// <param name="val"></param>
        /// <returns>-1 if Input is Null or Empty Else Integer Value</returns>
        public static int TryGetIntValue(string val)
        {
            if (!string.IsNullOrEmpty(val))
                return GetValueInt(val);
            return -1;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns>Returns Null if Null or Empty else Actual Value</returns>
        public static string TryGetStringValue(string val)
        {
            if (string.IsNullOrEmpty(val))
                return null;
            else
                return val;
        }

        /// <summary>
        /// Gets the value of uint.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type uint.</returns>
        public static uint GetValueUInt(object source)
        {
            return GetValueT<uint>(source,
                delegate(string text, out uint value)
                {
                    uint.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of Int64.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type Int64.</returns>
        public static Int64 GetValueInt64(object source)
        {
            return GetValueT<Int64>(source,
                delegate(string text, out Int64 value)
                {
                    Int64.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of UInt64.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type UInt64.</returns>
        public static UInt64 GetValueUInt64(object source)
        {
            return GetValueT<UInt64>(source,
                delegate(string text, out UInt64 value)
                {
                    UInt64.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of Single.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type Single.</returns>
        public static Single GetValueSingle(object source)
        {
            return GetValueT<Single>(source,
                delegate(string text, out Single value)
                {
                    Single.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of double.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type double.</returns>
        public static double GetValueDouble(object source)
        {
            return GetValueT<double>(source,
                delegate(string text, out double value)
                {
                    double.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value of Decimal.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Value of the type Decimal.</returns>
        public static Decimal GetValueDecimal(object source)
        {
            return GetValueT<Decimal>(source,
                delegate(string text, out Decimal value)
                {
                    Decimal.TryParse(text, out value);
                });
        }

        /// <summary>
        /// Gets the value DateTime.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Default DateTime value.</returns>
        public static DateTime GetValueDateTime(object source)
        {
            if (source == null) return default(DateTime);

            if (source.GetType() == typeof(DateTime))
                return ((DateTime)source);

            string text = source.ToString();
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    return Convert.ToDateTime(text, _dateTimeFormatInfo);
                }
            }
            catch { }
            return default(DateTime);
        }

        /// <summary>
        /// Gets the value DateTime.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Default DateTime value.</returns>
        public static DateTime GetValueDateTimeAny(object source)
        {
            if (source == null) return default(DateTime);

            if (source.GetType() == typeof(DateTime))
                return ((DateTime)source);

            string text = source.ToString();
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    DateTime dt = DateTime.MinValue;
                    if (DateTime.TryParse(text, out dt)) return dt;
                    return DateTime.MinValue;
                }
            }
            catch { }
            return default(DateTime);
        }

        /// <summary>
        /// Gets the value DateTime.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Default DateTime value.</returns>
        public static DateTime GetValueDateTimeFormat(object source, string format)
        {
            if (source == null) return default(DateTime);

            if (source.GetType() == typeof(DateTime))
                return ((DateTime)source);

            string text = source.ToString();
            try
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParseExact(text, format, null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    return dt;
                }
            }
            catch { }
            return default(DateTime);
        }

        /// <summary>
        /// Gets the value date time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="time">The time.</param>
        /// <returns>Combination of date and time.</returns>
        public static DateTime GetValueDateTime(string date, string time)
        {
            DateTime date2 = DateTime.MinValue;
            TimeSpan time2 = TimeSpan.MinValue;
            DateTime value = DateTime.MinValue;

            date2 = GetValueDateTime(date);
            time2 = GetValueTimeSpan(time);
            value = new DateTime(date2.Year, date2.Month, date2.Day,
                    time2.Hours, time2.Minutes, time2.Seconds, time2.Milliseconds);
            return value;
        }

        /// <summary>
        /// Gets the value TimeSpan.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Default TimeSpan value.</returns>
        public static TimeSpan GetValueTimeSpan(object source)
        {
            DateTime result = DateTime.Now;

            try
            {
                if (source != null)
                {
                    if (source.GetType() == typeof(string))
                    {
                        TimeSpan time = default(TimeSpan);
                        TimeSpan.TryParse(source.ToString(), out time);
                        return time;
                    }
                    else if (source.GetType() == typeof(DateTime))
                    {
                        result = GetValueDateTime(source);
                    }
                    else if (source.GetType() == typeof(TimeSpan))
                    {
                        return (TimeSpan)source;
                    }
                }
            }
            catch { }

            return GetTimeSpanFromDate(result);
        }

        /// <summary>
        /// Gets the time span from date.
        /// </summary>
        /// <param name="now">The now.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan GetTimeSpanFromDate(DateTime now)
        {
            return new TimeSpan(0, now.Hour, now.Minute, now.Second, now.Millisecond);
        }

        /// <summary>
        /// Gets the value from the IP address.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>IP Address string representation.</returns>
        public static string GetValueString(IPAddress source)
        {
            string result = string.Empty;

            try
            {
                if (source != null)
                {
                    result = source.ToString();
                }
            }
            catch { }

            return result;
        }

        /// <summary>
        /// Gets the value IP address.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>IP Address.</returns>
        public static IPAddress GetValueIPAddress(string source)
        {
            IPAddress addr = null;

            try
            {
                if (!source.IsEmpty())
                {
                    if (IPAddress.TryParse(source, out addr))
                        return addr;
                }
            }
            catch { }

            return null;
        }

        /// <summary>
        /// Gets the value enum.
        /// </summary>
        /// <param name="clrType">Type of the CLR.</param>
        /// <param name="value">The value.</param>
        /// <returns>Enumeration Value.</returns>
        public static object GetValueEnum(Type clrType, object value)
        {
            bool valid = false;
            return GetValueEnum(false, clrType, value, ref valid);
        }

        /// <summary>
        /// Gets the value enum.
        /// </summary>
        /// <param name="clrType">Type of the CLR.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Enumeration Value.</returns>
        public static object GetValueEnum(Type clrType, object value, object defaultValue)
        {
            object retValue = GetValueEnum(clrType, value);
            if (retValue != null) return retValue;
            return defaultValue;
        }

        /// <summary>
        /// Gets the value enum generic.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Enumeration Value.</returns>
        public static T GetValueEnumGeneric<T>(object value, T defaultValue)
        {
            try
            {
                return (T)GetValueEnum(typeof(T), value, defaultValue);
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Gets the value enum generic.
        /// </summary>
        /// <typeparam name="T">Type of the enumeration.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Enumeration Value.</returns>
        public static T GetValueEnumGeneric2<T>(object value, T defaultValue)
        {
            try
            {
                if (value.GetType() == typeof(T)) return (T)value;
                else if (Enum.IsDefined(typeof(T), value)) return (T)value;
                return defaultValue;
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Gets the value enum.
        /// </summary>
        /// <param name="isBitFlags">if set to <c>true</c> [is bit flags].</param>
        /// <param name="clrType">Type of the CLR.</param>
        /// <param name="value">The value.</param>
        /// <param name="valid">if set to <c>true</c> [valid].</param>
        /// <returns>Enumeration Value.</returns>
        public static object GetValueEnum(bool isBitFlags, Type clrType, object value, ref bool valid)
        {
            string combinedValue = string.Empty;
            return GetValueEnum(isBitFlags, clrType, value, null, null, out combinedValue, ref valid);
        }

        /// <summary>
        /// Gets the value enum.
        /// </summary>
        /// <param name="isBitFlags">if set to <c>true</c> [is bit flags].</param>
        /// <param name="clrType">Type of the CLR.</param>
        /// <param name="value">The value.</param>
        /// <param name="enumNameValues">The enum name values.</param>
        /// <param name="selectedValues">The selected values.</param>
        /// <param name="combindedValue">The combinded value.</param>
        /// <param name="valid">if set to <c>true</c> [valid].</param>
        /// <returns>Enumeration Value.</returns>
        public static object GetValueEnum(bool isBitFlags, Type clrType, object value,
                                            IDictionary<string, ulong> enumNameValues,
                                            IDictionary<string, sbyte> selectedValues,
                                            out string combindedValue,
                                            ref bool valid)
        {
            combindedValue = string.Empty;

            if (isBitFlags ||
                Enum.IsDefined(clrType, value))
            {

                // same type
                if (value.GetType() == clrType)
                {
                    valid = true;
                    return value;
                } // string values
                else if (value.GetType() == typeof(string))
                {
                    try
                    {
                        string tvalue = value.ToString().Trim();

                        if (isBitFlags)
                        {
                            valid = true;

                            if (!tvalue.IsEmpty())
                            {
                                string[] values = tvalue.Split(new char[] { ',' });
                                StringBuilder sb = new StringBuilder();
                                selectedValues.Clear();

                                if (values.Length > 0)
                                {
                                    if (selectedValues != null)
                                    {
                                        ulong combindedNumericValue = 0;

                                        foreach (string svalue in values)
                                        {
                                            string svalue2 = svalue.Trim();
                                            if (!enumNameValues.ContainsKey(svalue2)) continue;

                                            selectedValues.Add(svalue2, 0);
                                            combindedNumericValue |= enumNameValues[svalue2];

                                            if (sb.Length > 0) sb.Append(",");
                                            sb.Append(svalue2);
                                        }

                                        tvalue = combindedNumericValue.ToString();
                                        combindedValue = sb.ToString();
                                    }
                                }
                            }
                            else
                            {
                                IEnumerator enm = enumNameValues.GetEnumerator();
                                enm.MoveNext();
                                tvalue = ((KeyValuePair<string, ulong>)enm.Current).Value.ToString();
                            }
                        }

                        // Set the values
                        valid = true;
                        object value3 = Enum.Parse(clrType, tvalue, true);
                        return value3;
                    }
                    catch { valid = false; }
                } // IList<string>
                else if (value.GetType() == typeof(List<string>))
                {
                    try
                    {
                        IList<string> values = value as IList<string>;
                        if (values != null && values.Count > 0)
                        {
                            string tvalue = values[0];

                            if (isBitFlags)
                            {
                                valid = true;
                                StringBuilder sb = new StringBuilder();
                                selectedValues.Clear();

                                if (selectedValues != null)
                                {
                                    ulong combindedNumericValue = 0;

                                    foreach (string svalue in values)
                                    {
                                        string svalue2 = svalue.Trim();
                                        if (!enumNameValues.ContainsKey(svalue2)) continue;

                                        selectedValues.Add(svalue2, 0);
                                        combindedNumericValue |= enumNameValues[svalue2];

                                        if (sb.Length > 0) sb.Append(",");
                                        sb.Append(svalue2);
                                    }

                                    tvalue = combindedNumericValue.ToString();
                                    combindedValue = sb.ToString();
                                }
                            }

                            // Set the values
                            valid = true;
                            object value3 = Enum.Parse(clrType, tvalue, true);
                            return value3;
                        }
                    }
                    catch { valid = false; }
                }
                else
                {
                    try
                    {
                        object value3 = Enum.Parse(clrType, value.ToString(), true);
                        return value3;
                    }
                    catch { valid = false; }
                }
            }
            return null;
        }

        #endregion
    }
}
