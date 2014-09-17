/* ================================================================================= 
 * Purpose		:	Registry Helper Class
 * File Name	:   RegHelper.cs
 * Author		:	A.Vinod Kumar
 * Created  	:	06/12/2010
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 06/12/2010		A.Vinod Kumar    Initial Version
 * ===============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Net;

namespace BMC.CoreLib.Registry
{
    public static class RegistryHelper
    {
        /// <summary>
        /// Gets the RegValue string.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static string GetValueString(this RegistryKey regKey, string subKey, string keyName, string defaultValue)
        {
            string value = string.Empty;
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = regKey.OpenSubKey(subKey);
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
        /// Gets the RegValue byte.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>Value from the key.</returns>
        public static sbyte GetValueSByte(this RegistryKey regKey, string subKey, string keyName, sbyte defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueSByte(sValue);
        }       

        /// <summary>
        /// Gets the RegValue byte.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>Value from the key.</returns>
        public static byte GetValueByte(this RegistryKey regKey, string subKey, string keyName, byte defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueByte(sValue);
        }

        /// <summary>
        /// Gets the RegValue character.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>Value from the key.</returns>
        public static char GetValueChar(this RegistryKey regKey, string subKey, string keyName, char defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueChar(sValue);
        }

        /// <summary>
        /// Gets the RegValue bool.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>Value from the key.</returns>
        public static bool GetValueBool(this RegistryKey regKey, string subKey, string keyName, bool defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueBoolSimple(sValue);
        }

        /// <summary>
        /// Gets the reg value int8.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static SByte GetValueInt8(this RegistryKey regKey, string subKey, string keyName, SByte defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueSByte(sValue);
        }

        /// <summary>
        /// Gets the reg value int8.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static Byte GetValueUInt8(this RegistryKey regKey, string subKey, string keyName, Byte defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueByte(sValue);
        }

        /// <summary>
        /// Gets the RegValue short.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static Int16 GetValueInt16(this RegistryKey regKey, string subKey, string keyName, Int16 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueShort(sValue);
        }

        /// <summary>
        /// Gets the RegValue ushort.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static UInt16 GetValueUInt16(this RegistryKey regKey, string subKey, string keyName, UInt16 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueUShort(sValue);
        }

        /// <summary>
        /// Gets the RegValue integer.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static Int32 GetValueInt32(this RegistryKey regKey, string subKey, string keyName, Int32 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueInt(sValue);
        }

        /// <summary>
        /// Gets the RegValue uinteger.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static UInt32 GetValueUInt32(this RegistryKey regKey, string subKey, string keyName, UInt32 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueUInt(sValue);
        }

        /// <summary>
        /// Gets the RegValue long.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static Int64 GetValueInt64(this RegistryKey regKey, string subKey, string keyName, Int64 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueInt64(sValue);
        }

        /// <summary>
        /// Gets the RegValue long.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static UInt64 GetValueUInt64(this RegistryKey regKey, string subKey, string keyName, UInt64 defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueUInt64(sValue);
        }

        /// <summary>
        /// Gets the RegValue single.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static Single GetValueSingle(this RegistryKey regKey, string subKey, string keyName, double defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueSingle(sValue);
        }

        /// <summary>
        /// Gets the RegValue double.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static double GetValueDouble(this RegistryKey regKey, string subKey, string keyName, double defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueDouble(sValue);
        }

        /// <summary>
        /// Gets the RegValue decimal.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static decimal GetValueDecimal(this RegistryKey regKey, string subKey, string keyName, double defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueDecimal(sValue);
        }

        /// <summary>
        /// Gets the RegValue datetime.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static DateTime GetValueDateTime(this RegistryKey regKey, string subKey, string keyName, double defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueDateTime(sValue);
        }

        /// <summary>
        /// Gets the RegValue timespan.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static TimeSpan GetValueTimeSpan(this RegistryKey regKey, string subKey, string keyName, double defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueTimeSpan(sValue);
        }

        /// <summary>
        /// Gets the value IP address.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Value from the key.</returns>
        public static IPAddress GetValueIPAddress(this RegistryKey regKey, string subKey, string keyName, IPAddress defaultValue)
        {
            string sValue = GetValueString(regKey, subKey, keyName, string.Empty);
            return TypeSystem.GetValueIPAddress(sValue);
        }

        /// <summary>
        /// Sets the reg value.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="value">The value.</param>
        public static void SetRegValue(this RegistryKey regKey, string subKey, string keyName, IPAddress value)
        {
            if (value == null)
                SetRegValue(regKey, subKey, keyName, (object)string.Empty);
            else
                SetRegValue(regKey, subKey, keyName, (object)value.ToString());
        }

        /// <summary>
        /// Sets the RegValue value.
        /// </summary>
        /// <param name="regKey">The reg key.</param>
        /// <param name="subKey">The sub key.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="value">The value.</param>
        public static void SetRegValue(this RegistryKey regKey, string subKey, string keyName, object value)
        {
            RegistryKey regSubKey = null;

            try
            {
                regSubKey = regKey.OpenSubKey(subKey, true);
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
    }
}
