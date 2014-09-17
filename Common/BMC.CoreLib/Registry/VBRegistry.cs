using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace BMC.CoreLib.Registry
{
    public static class VBRegistry
    {
        private static RegistryKey REG_KEY = Microsoft.Win32.Registry.CurrentUser;
        private const string SUB_KEY = "Software\\VB and VBA Program Settings";

        static VBRegistry()
        {
            CreateRootKey();
        }

        public static void CreateRootKey()
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = REG_KEY.OpenSubKey(SUB_KEY);
                if (regSubKey == null)
                {
                    REG_KEY.CreateSubKey(SUB_KEY, RegistryKeyPermissionCheck.Default);
                }
            }
            catch { }
            finally
            {
                if (regSubKey != null)
                {
                    regSubKey.Close();
                }
            }
        }

        public static void CreateKeyIfNotExists(string key)
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = REG_KEY.OpenSubKey(SUB_KEY + "\\" + key, true);
                if (regSubKey == null)
                {
                    regSubKey = REG_KEY.OpenSubKey(SUB_KEY, true);
                    if (regSubKey != null)
                    {
                        regSubKey.CreateSubKey(key, RegistryKeyPermissionCheck.Default);
                    }
                }
            }
            catch { }
            finally
            {
                if (regSubKey != null)
                {
                    regSubKey.Close();
                }
            }
        }

        public static IEnumerable<string> GetAllSettings(string appName, string section)
        {
            IList<string> result = new List<string>();
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = REG_KEY.OpenSubKey(SUB_KEY + "\\" + appName + "\\" + section);
                string[] valueNames = regSubKey.GetValueNames();
                if (valueNames != null)
                {
                    foreach (string valueName in valueNames)
                    {
                        object value = regSubKey.GetValue(valueName);
                        if (value != null)
                        {
                            result.Add(value.ToString());
                        }
                    }
                }
            }
            catch { }
            finally
            {
                regSubKey.Close();
            }

            return result;
        }

        public static void SaveSetting(string appName, string section, string key, string setting)
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = REG_KEY.OpenSubKey(SUB_KEY + "\\" + appName + "\\" + section, true);
                regSubKey.SetValue(key, setting);
            }
            catch { }
            finally
            {
                regSubKey.Close();
            }
        }

        public static string GetSetting(string appName, string section, string key, object defaultValue)
        {
            RegistryKey regSubKey = null;
            string result = string.Empty;

            try
            {
                regSubKey = REG_KEY.OpenSubKey(SUB_KEY + "\\" + appName + "\\" + section);
                object value = regSubKey.GetValue(key, defaultValue);
                if (value == null)
                {
                    result = defaultValue.ToString();
                }
                else
                {
                    result = value.ToString();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                regSubKey.Close();
            }

            return result;
        }
    }
}
