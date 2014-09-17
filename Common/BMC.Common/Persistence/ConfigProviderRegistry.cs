using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Common.Persistence
{
    internal delegate void GetValueTryParseHandler<T>(string text, out T value);

    internal static class RegistryHelper
    {
        internal static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        internal static bool IgnoreCaseCompare(this string source, string comapare)
        {
            return string.Compare(source, comapare, true) == 0;
        }

        internal static T GetValueT<T>(object source, GetValueTryParseHandler<T> func)
        {
            T value = default(T);
            if (GetValueT<T>(source, out value)) return value;

            string text = GetValueString(source);
            if (text.IsEmpty()) return default(T);
            func(text, out value);
            return value;
        }

        internal static bool GetValueT<T>(object value, out T result)
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

        internal static string GetValueString(object value)
        {
            if (value == null) return string.Empty;
            return value.ToString();
        }

        internal static bool GetValueBoolSimple(object source)
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
        internal static int GetValueInt(object source)
        {
            return GetValueT<int>(source,
                delegate(string text, out int value)
                {
                    int.TryParse(text, out value);
                });
        }
        internal static Int64 GetValueInt64(object source)
        {
            return GetValueT<Int64>(source,
                delegate(string text, out Int64 value)
                {
                    Int64.TryParse(text, out value);
                });
        }

        internal static RegistryKey OpenOrGetSubKey(ref RegistryKey regKey, string subKey, bool writable)
        {
            RegistryKey regSubKey = null;

            try
            {
                string subKeyFull = "SOFTWARE\\" + subKey;
                if (regKey == null) regKey = GetRegLocalMachine();
                regSubKey = regKey.OpenSubKey(subKeyFull, writable);
                if (regSubKey == null)
                {
                    regSubKey = regKey.CreateSubKey(subKeyFull, RegistryKeyPermissionCheck.Default);
                }
            }
            catch { }

            return regSubKey;
        }

        internal static RegistryKey OpenOrGetRootKey(ref RegistryKey regKey)
        {
            RegistryKey regSubKey = null;

            try
            {
                string subKeyFull = "SOFTWARE";
                if (regKey == null) regKey = GetRegLocalMachine();
                regSubKey = regKey.OpenSubKey(subKeyFull, false);
            }
            catch { }

            return regSubKey;
        }

        /// <summary>
        /// Gets the RegValue string.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        internal static string GetValueString(this RegistryKey regKey, string subKey, string keyName, string defaultValue)
        {
            string value = string.Empty;
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = OpenOrGetSubKey(ref regKey, subKey, false);
                value = regSubKey.GetValue(keyName, defaultValue).ToString();
            }
            catch
            {
                value = defaultValue;
            }
            finally
            {
                regSubKey.Close();
                regKey.Close();
            }

            return value;
        }

        /// <summary>
        /// Gets the RegValue bool.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>Value from the key.</returns>
        internal static bool GetValueBool(this RegistryKey regKey, string subKey, string keyName, bool defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return GetValueBoolSimple(sValue);
        }

        /// <summary>
        /// Gets the RegValue integer.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        internal static Int32 GetValueInt32(this RegistryKey regKey, string subKey, string keyName, Int32 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return GetValueInt(sValue);
        }

        /// <summary>
        /// Gets the RegValue long.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        internal static Int64 GetValueInt64(this RegistryKey regKey, string subKey, string keyName, Int64 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return GetValueInt64(sValue);
        }

        /// <summary>
        /// Sets the RegValue value.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="value">The value.</param>
        internal static void SetRegValue(this RegistryKey regKey, string subKey, string keyName, object value)
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = OpenOrGetSubKey(ref regKey, subKey, true);
                regSubKey.SetValue(keyName, value);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                regSubKey.Close();
                regKey.Close();
            }
        }

        /// <summary>
        /// Sets the RegValue value.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="value">The value.</param>
        internal static void RemoveRegSubKey(this RegistryKey regKey, string subKey)
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = OpenOrGetRootKey(ref regKey);
                regSubKey.DeleteSubKey(subKey);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                regSubKey.Close();
                regKey.Close();
            }
        }

        /// <summary>
        /// Sets the RegValue value.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="value">The value.</param>
        internal static void RemoveRegValue(this RegistryKey regKey, string subKey, string keyName)
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = OpenOrGetSubKey(ref regKey, subKey, true);
                regSubKey.DeleteValue(keyName);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                regSubKey.Close();
                regKey.Close();
            }
        }

        internal static RegistryKey GetRegBaseKey(RegistryHive hive)
        {
            return RegistryKey.OpenBaseKey(hive, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32));
        }

        internal static RegistryKey GetRegLocalMachine()
        {
            return GetRegBaseKey(RegistryHive.LocalMachine);
        }
    }

    internal class ConfigProviderRegistry : ConfigProviderBase
    {
        public override void LoadInternal()
        {
        }

        public override void Save() { }

        internal override void InitializeToDefaultValues(IConfigKeyValuePair keyValue, string sectionName, string keyName)
        {
            RegistryHelper.SetRegValue(null, sectionName, keyName, keyValue.GetDefaultObjectValue());
        }

        internal override void RemoveSubKey(string sectionName)
        {
            RegistryHelper.RemoveRegSubKey(null, sectionName);
        }

        internal override void RemoveValue(IConfigKeyValuePair keyValue, string sectionName, string keyName)
        {
            RegistryHelper.RemoveRegValue(null, sectionName, keyName);
        }

        public override object GetObjectValue(string sectionName, string keyName)
        {
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];
            object result = null;

            if (keyValue != null)
            {
                if (keyValue is ConfigKeyValuePairLong)
                {
                    result = RegistryHelper.GetValueInt64(null, sectionName, keyName, 0);
                }
                else if (keyValue is ConfigKeyValuePairInt)
                {
                    result = RegistryHelper.GetValueInt32(null, sectionName, keyName, 0);
                }
                else if (keyValue is ConfigKeyValuePairBool)
                {
                    result = RegistryHelper.GetValueBool(null, sectionName, keyName, false);
                }
                else
                {
                    result = RegistryHelper.GetValueString(null, sectionName, keyName, string.Empty);
                }
            }

            return result;
        }

        public override T GetValue<T>(string sectionName, string keyName, T defaultValue)
        {
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];
            object result = null;
            object resultT = defaultValue;
            object defaultValue2 = Convert.ChangeType(defaultValue, typeof(T));
            Type typeOfT = typeof(T);

            if (keyValue != null)
            {
                if (typeOfT == typeof(long))
                {
                    resultT = RegistryHelper.GetValueInt64(null, sectionName, keyName, ((int)defaultValue2));
                }
                else if (typeOfT == typeof(int))
                {
                    resultT = RegistryHelper.GetValueInt32(null, sectionName, keyName, ((int)defaultValue2));
                }
                else if (typeOfT == typeof(bool))
                {
                    resultT = RegistryHelper.GetValueBool(null, sectionName, keyName, ((bool)defaultValue2));
                }
                else
                {
                    resultT = RegistryHelper.GetValueString(null, sectionName, keyName, ((string)defaultValue2));
                }
            }


            if (typeof(T) == typeof(string))
            {
                if (resultT != null)
                {
                    result = resultT.ToString();
                }
                return (T)result;
            }
            else
            {
                return (T)resultT;
            }
        }

        public override void SetObjectValue(string sectionName, string keyName, object value)
        {
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];

            if (keyValue != null)
            {
                if (keyValue is ConfigKeyValuePairLong)
                {
                    RegistryHelper.SetRegValue(null, sectionName, keyName, (long)Convert.ChangeType(value, typeof(long)));
                }
                else if (keyValue is ConfigKeyValuePairInt)
                {
                    RegistryHelper.SetRegValue(null, sectionName, keyName, (int)Convert.ChangeType(value, typeof(int)));
                }
                else if (keyValue is ConfigKeyValuePairBool)
                {
                    RegistryHelper.SetRegValue(null, sectionName, keyName, (bool)Convert.ChangeType(value, typeof(bool)));
                }
                else
                {
                    RegistryHelper.SetRegValue(null, sectionName, keyName, (string)Convert.ChangeType(value, typeof(string)));
                }
            }
        }

        public override void SetValue<T>(string sectionName, string keyName, T value)
        {
            this.SetObjectValue(sectionName, keyName, value);
        }

        public override void SetValue(string sectionName, string keyName, string value, bool save)
        {
            base.SetValue(sectionName, keyName, value, save);
            if (save)
            {
                IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];
                this.SetObjectValue(sectionName, keyName, keyValue.GetObjectValue());
            }
        }
    }
}
