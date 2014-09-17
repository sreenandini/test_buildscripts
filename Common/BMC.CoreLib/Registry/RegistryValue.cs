using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib.Registry
{
    /// <summary>
    /// Registry Value
    /// </summary>
    /// <typeparam name="T">Type of the outer.</typeparam>
    /// <typeparam name="S">Type of the inner.</typeparam>
    public class RegistryValue<T, S> : DisposableObjectNotify, IConfigValue<T, S>
    {
        private RegistryKey _hkey = null;
        private Func<S, T> _getAction = null;
        private Func<T, S> _setAction = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryValue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="regBase">The reg base.</param>
        /// <param name="keyName">Name of the key.</param>
        public RegistryValue(RegistryKey hkey, string subKeyName, string keyName)
            : this(hkey, subKeyName, keyName, null, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryValue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="regBase">The reg base.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public RegistryValue(RegistryKey hkey, string subKeyName, string keyName,
            Func<S, T> getAction, Func<T, S> setAction)
        {
            _hkey = hkey;
            this.SubKeyName = subKeyName;
            this.KeyName = keyName;

            _getAction = getAction;
            _setAction = setAction;

            // Initial fetching
            T value = this.RealValue;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is same type.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is same type; otherwise, <c>false</c>.
        /// </value>
        protected bool IsSameType { get; set; }

        /// <summary>
        /// Gets or sets the name of the sub key.
        /// </summary>
        /// <value>The name of the sub key.</value>
        public string SubKeyName { get; private set; }

        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        /// <value>The name of the key.</value>
        public string KeyName { get; private set; }

        /// <summary>
        /// Value of the registry
        /// </summary>
        private T _value = default(T);

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="BMC.CoreLib.Registry.RegistryValue&lt;T&gt;"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator T(RegistryValue<T, S> source)
        {
            return source.Value;
        }

        /// <summary>
        /// Gets or sets the real value.
        /// </summary>
        /// <value>The real value.</value>
        public T RealValue
        {
            get
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetRealValue");
                object value2 = default(S);

                try
                {
                    switch (typeof(S).FullName)
                    {
                        case "System.Boolean":
                            value2 = _hkey.GetValueBool(this.SubKeyName, this.KeyName, false);
                            break;

                        case "System.Byte":
                        case "System.SByte":
                            value2 = _hkey.GetValueInt8(this.SubKeyName, this.KeyName, 0);
                            break;

                        case "System.Single":
                        case "System.Decimal":
                        case "System.Double":
                            value2 = _hkey.GetValueDouble(this.SubKeyName, this.KeyName, 0);
                            break;

                        case "System.Int16":
                        case "System.UInt16":
                            value2 = _hkey.GetValueInt16(this.SubKeyName, this.KeyName, 0);
                            break;

                        case "System.Int32":
                        case "System.UInt32":
                            value2 = _hkey.GetValueInt32(this.SubKeyName, this.KeyName, 0);
                            break;

                        case "System.Int64":
                        case "System.UInt64":
                            value2 = _hkey.GetValueInt64(this.SubKeyName, this.KeyName, 0);
                            break;

                        default:
                            value2 = _hkey.GetValueString(this.SubKeyName, this.KeyName, string.Empty);
                            break;
                    }

                    if (_getAction != null)
                    {
                        value2 = (T)_getAction((S)value2);
                    }
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
                _value = (T)value2;
                return _value;
            }
            set
            {
                ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SetRealValue");
                object value2 = null;

                try
                {
                    if (_setAction != null)
                    {
                        value2 = _setAction(value);
                    }
                    else
                    {
                        value2 = value;
                    }
                    if (this.IsSameType &&
                        (typeof(S) == typeof(T)))
                    {
                        _value = (T)value2;
                    }
                    else
                    {
                        _value = value;
                    }
                    _hkey.SetRegValue(this.SubKeyName, this.KeyName, value2);
                    base.NotifyPropertyChanged(this.KeyName);
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (!_value.Equals(default(T)))
            {
                return _value.ToString();
            }
            return base.ToString();
        }
    }

    /// <summary>
    /// Registry Value
    /// </summary>
    /// <typeparam name="T">Type of the outer.</typeparam>
    [Serializable]
    public class RegistryValue<T> : RegistryValue<T, T>, IConfigValue<T, T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryValue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="regBase">The reg base.</param>
        /// <param name="keyName">Name of the key.</param>
        public RegistryValue(RegistryKey hkey, string subKeyName, string keyName)
            : base(hkey, subKeyName, keyName)
        {
            this.IsSameType = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryValue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="regBase">The reg base.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public RegistryValue(RegistryKey hkey, string subKeyName, string keyName,
            Func<T, T> getAction, Func<T, T> setAction)
            : base(hkey, subKeyName, keyName, getAction, setAction)
        {
            this.IsSameType = true;
        }
    }
}
