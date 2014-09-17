using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Net;

namespace BMC.CoreLib.Registry
{
    [CLSCompliant(true)]
    public class RegistrySubKey : DisposableObjectQueryInterface
    {
        protected RegistryKey _hkey = null;
        private string _subKeyName = string.Empty;
        
        protected RegistrySubKey(RegistryHive hkey, string machineName, string subKeyName)
            : this(RegistryKey.OpenRemoteBaseKey(hkey, machineName), subKeyName) { }

        protected RegistrySubKey(RegistryKey hkey, string subKeyName)
        {
            _hkey = hkey;
            _subKeyName = subKeyName;
            this.InitializeObjects();
            this.InitializeValues();
        }

        public string SubKeyName
        {
            get
            {
                return _subKeyName;
            }
        }

        public override string ToString()
        {
            return Path.Combine(_hkey.ToString(), this.SubKeyName);
        }

        #region Key/Values

        protected virtual void InitializeObjects() { }

        /// <summary>
        /// Initializes the values.
        /// </summary>
        protected virtual void InitializeValues() { }

        /// <summary>
        /// Creates the registry value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyName">Name of the property.</param>
        /// <returns>Reg key value instance.</returns>
        protected IConfigValue<T, T> CreateRegistryValue<T>(string keyName)
        {
            return this.CreateRegistryValue<T>(keyName, null, null);
        }

        /// <summary>
        /// Creates the reg value.
        /// </summary>
        /// <typeparam name="T">Type of the outer.</typeparam>
        /// <param name="keyName">Name of the property.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        /// <returns>Reg key value instance.</returns>
        protected IConfigValue<T, T> CreateRegistryValue<T>(string keyName,
            Func<T, T> getAction, Func<T, T> setAction)
        {
            RegistryValue<T> value = null;
            if (getAction == null || setAction == null)
            {
                value = new RegistryValue<T>(_hkey, this.SubKeyName, keyName);
            }
            else
            {
                value = new RegistryValue<T>(_hkey, this.SubKeyName, keyName, getAction, setAction);
            }
            return value;
        }

        /// <summary>
        /// Creates the reg value.
        /// </summary>
        /// <typeparam name="T">Type of the outer.</typeparam>
        /// <typeparam name="S">Type of the inner.</typeparam>
        /// <param name="keyName">Name of the property.</param>
        /// <returns>Reg key value instance.</returns>
        protected IConfigValue<T, S> CreateRegistryValue<T, S>(string keyName)
        {
            return this.CreateRegistryValue<T, S>(keyName, null, null);
        }

        /// <summary>
        /// Creates the reg value.
        /// </summary>
        /// <typeparam name="T">Type of the outer.</typeparam>
        /// <typeparam name="S">Type of the inner.</typeparam>
        /// <param name="keyName">Name of the property.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        /// <returns>Reg key value instance.</returns>
        protected IConfigValue<T, S> CreateRegistryValue<T, S>(string keyName,
            Func<S, T> getAction, Func<T, S> setAction)
        {
            RegistryValue<T, S> value = null;
            if (getAction == null || setAction == null)
            {
                value = new RegistryValue<T, S>(_hkey, this.SubKeyName, keyName);
            }
            else
            {
                value = new RegistryValue<T, S>(_hkey, this.SubKeyName, keyName, getAction, setAction);
            }
            return value;
        }

        protected string ConvertFrom(IPAddress ipAddress)
        {
            return TypeSystem.GetValueString(ipAddress);
        }

        protected IPAddress ConvertTo(string ipAddress)
        {
            return TypeSystem.GetValueIPAddress(ipAddress);
        }
        #endregion
    }
}
