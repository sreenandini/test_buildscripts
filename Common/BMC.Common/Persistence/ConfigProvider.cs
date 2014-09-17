using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BMC.Common.Persistence
{
    public interface IConfigKeyValuePair
    {
        string Key { get; set; }

        bool IsValueSet { get; }

        bool IsValueChanged { get; }

        bool SetDefaultValue(object value);

        bool SetValueAsDefault();

        bool SetValueFromDefault();

        object GetObjectValue();

        object GetDefaultObjectValue();

        void SetObjectValue(object value);
    }

    public class ConfigKeyValuePair<T> : IConfigKeyValuePair
    {
        private T _value = default(T);
        private bool _isValueSet = false;
        private bool _isValueChanged = false;

        private Type _typeOfT = typeof(T);

        public string Key { get; set; }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _isValueChanged = !this.IsDefaultOrSameValue(value);
                _value = value;
                if (!_isValueSet) _isValueSet = true;
            }
        }

        public T DefaultValue { get; set; }

        public bool IsValueSet { get { return _isValueSet; } }

        public bool IsValueChanged
        {
            get { return _isValueChanged; }
        }

        private bool IsDefaultOrSameValue(T value)
        {
            return this.Value != null &&
                    this.Value.Equals(value);
        }

        public bool SetDefaultValue(object value)
        {
            if (value != null &&
                value.GetType() == _typeOfT)
            {
                this.DefaultValue = (T)value;
            }
            return true;
        }

        public bool SetValueAsDefault()
        {
            if (this.IsDefaultOrSameValue(this.DefaultValue)) return false;

            this.Value = this.DefaultValue;
            return true;
        }

        public bool SetValueFromDefault()
        {
            this.Value = this.DefaultValue;
            return true;
        }

        public object GetObjectValue() { return _value; }

        public object GetDefaultObjectValue()
        {
            return this.DefaultValue;
        }

        public void SetObjectValue(object value)
        {
            if (value != null &&
                _typeOfT == value.GetType())
            {
                this.Value = (T)Convert.ChangeType(value, _typeOfT);
            }
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }

    public class ConfigKeyValuePairString : ConfigKeyValuePair<string> { }

    public class ConfigKeyValuePairBool : ConfigKeyValuePair<bool> { }

    public class ConfigKeyValuePairInt : ConfigKeyValuePair<int> { }

    public class ConfigKeyValuePairLong : ConfigKeyValuePair<long> { }

    public class ConfigKeyValuePairDictionary : SortedDictionary<string, IConfigKeyValuePair>
    {
        public ConfigKeyValuePairDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }
    }

    public class ConfigKeyValuePairTopDictionary : SortedDictionary<string, ConfigKeyValuePairDictionary>
    {
        public ConfigKeyValuePairTopDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase) { }

        public bool IsExists(string sectionName, string keyName)
        {
            if (!this.ContainsKey(sectionName)) return false;
            if (!this[sectionName].ContainsKey(keyName)) return false;
            return true;
        }
    }

    public enum ConfigProviderTypes
    {
        Xml = 0,
        Registry = 1,
        All = 2
    };

    [Flags]
    public enum ConfigProviderAccessTypes
    {
        None = 2,
        Read = 4,
        Write = 6
    };

    public interface IConfigProvider
    {
        bool IsMetadataInitialized { get; set; }

        void InitializeToDefaultValues(ConfigKeyValuePairTopDictionary storeValues);

        void RemoveValues(ConfigKeyValuePairTopDictionary storeValues);

        void InitializeSchema<T>(string sectionName, string keyName);

        void Initialize<T>(string sectionName, string keyName, T defaultValue);

        T GetValue<T>(string sectionName, string keyName, T defaultValue);

        object GetObjectValue(string sectionName, string keyName);

        string GetValue(string sectionName, string keyName, string defaultValue);

        void SetValue<T>(string sectionName, string keyName, T value);

        void SetObjectValue(string sectionName, string keyName, object value);

        void SetValue(string sectionName, string keyName, string value);

        void SetValue(string sectionName, string keyName, string value, bool save);

        void Load();

        void Save();
    }

    public static class ConfigProviderFactory
    {
        private static ConfigProviderTypes ProviderType = ConfigProviderTypes.Xml;

        public static IConfigProvider Create(string fileName)
        {
            ConfigProviderBase provider = null;

            switch (ProviderType)
            {
                case ConfigProviderTypes.All:
                    provider = new ConfigProviderAll(
                        new ConfigProviderAllInput[] 
                        {
                            new ConfigProviderAllInput() 
                            { 
                                Provider = new ConfigProviderXml(fileName), 
                                AccessTypes = ConfigProviderAccessTypes.Read | ConfigProviderAccessTypes.Write
                            },
                            new ConfigProviderAllInput() 
                            {
                                Provider = new ConfigProviderRegistry(),
                                AccessTypes= ConfigProviderAccessTypes.Write
                            }
                        });
                    break;

                case ConfigProviderTypes.Registry:
                    provider = new ConfigProviderRegistry();
                    break;

                default:
                    provider = new ConfigProviderXml(fileName);
                    break;
            }

            return provider;
        }
    }

    public abstract class ConfigProviderBase : IConfigProvider
    {
        protected ConfigKeyValuePairTopDictionary _storeValues = new ConfigKeyValuePairTopDictionary();

        private bool _isMetadataInitialized = false;

        protected ConfigProviderBase() { }

        public virtual bool IsMetadataInitialized
        {
            get { return _isMetadataInitialized; }
            set { _isMetadataInitialized = value; }
        }

        private void ThrowIfMetadataNotInitialized()
        {
            if (!_isMetadataInitialized)
            {
                throw new InvalidOperationException("Metadata was not initialized properly.");
            }
        }

        public virtual T GetValue<T>(string sectionName, string keyName, T defaultValue)
        {
            if (!_storeValues.IsExists(sectionName, keyName)) return default(T);
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];
            object result = null;
            object resultT = defaultValue;

            if (keyValue != null)
            {
                if (keyValue is ConfigKeyValuePairLong)
                {
                    resultT = ((ConfigKeyValuePairLong)keyValue).Value;
                }
                else if (keyValue is ConfigKeyValuePairInt)
                {
                    resultT = ((ConfigKeyValuePairInt)keyValue).Value;
                }
                else if (keyValue is ConfigKeyValuePairBool)
                {
                    resultT = ((ConfigKeyValuePairBool)keyValue).Value;
                }
                else
                {
                    resultT = ((ConfigKeyValuePairString)keyValue).Value;
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

        public virtual object GetObjectValue(string sectionName, string keyName)
        {
            if (!_storeValues.IsExists(sectionName, keyName)) return null;
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];
            object result = null;

            if (keyValue != null)
            {
                result = keyValue.GetObjectValue();
            }

            return result;
        }

        public virtual string GetValue(string sectionName, string keyName, string defaultValue)
        {
            return GetValue<string>(sectionName, keyName, defaultValue);
        }

        public virtual void Load()
        {
            this.ThrowIfMetadataNotInitialized();

            this.LoadInternal();

            this.FillMissingValues();
        }

        private void FillMissingValues()
        {
            // find any values is not set
            bool valuesChanged = false;

            var notSetValues = (from a in _storeValues.Values
                                from b in a.Values
                                where b.IsValueSet == false
                                select b);
            foreach (var notSetValue in notSetValues)
            {
                valuesChanged |= notSetValue.SetValueAsDefault();
            }

            //if (valuesChanged)
            //{
            //    this.Save();
            //}
        }

        public abstract void LoadInternal();

        public virtual void InitializeToDefaultValues(ConfigKeyValuePairTopDictionary storeValues)
        {
            foreach (var pair1 in storeValues)
            {
                foreach (var pair2 in pair1.Value)
                {
                    IConfigKeyValuePair keyValue = pair2.Value;
                    this.InitializeToDefaultValues(keyValue, pair1.Key, pair2.Key);
                }
            }
        }

        internal virtual void InitializeToDefaultValues(IConfigKeyValuePair keyValue, string sectionName, string keyName)
        {
            keyValue.SetValueFromDefault();
        }

        public void RemoveValues(ConfigKeyValuePairTopDictionary storeValues)
        {
            foreach (var pair1 in storeValues)
            {
                foreach (var pair2 in pair1.Value)
                {
                    IConfigKeyValuePair keyValue = pair2.Value;
                    this.RemoveValue(keyValue, pair1.Key, pair2.Key);
                }

                this.RemoveSubKey(pair1.Key);
            }
        }

        internal virtual void RemoveSubKey(string sectionName) { }

        internal virtual void RemoveValue(IConfigKeyValuePair keyValue, string sectionName, string keyName) { }

        public void InitializeSchema<T>(string sectionName, string keyName)
        {
            ConfigKeyValuePairDictionary dictKey = null;
            Type typeOfT = typeof(T);

            if (_storeValues.ContainsKey(sectionName))
            {
                dictKey = _storeValues[sectionName];
            }
            else
            {
                dictKey = new ConfigKeyValuePairDictionary();
                _storeValues.Add(sectionName, dictKey);
            }

            if (dictKey != null)
            {
                IConfigKeyValuePair keyValue = null;

                if (dictKey.ContainsKey(keyName))
                {
                    keyValue = dictKey[keyName];
                }
                else
                {
                    if (typeOfT == typeof(long))
                    {
                        keyValue = new ConfigKeyValuePairLong();
                    }
                    else if (typeOfT == typeof(int))
                    {
                        keyValue = new ConfigKeyValuePairInt();
                    }
                    else if (typeOfT == typeof(bool))
                    {
                        keyValue = new ConfigKeyValuePairBool();
                    }
                    else
                    {
                        keyValue = new ConfigKeyValuePairString();
                    }
                    keyValue.Key = keyName;
                    dictKey.Add(keyName, keyValue);
                }
            }
        }

        public virtual void Initialize<T>(string sectionName, string keyName, T value)
        {
            this.InitializeSchema<T>(sectionName, keyName);
            if (!_storeValues.IsExists(sectionName, keyName)) return;
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];

            if (keyValue != null)
            {
                if (keyValue is ConfigKeyValuePairLong)
                {
                    ((ConfigKeyValuePairLong)keyValue).DefaultValue = (long)Convert.ChangeType(value, typeof(long));
                }
                else if (keyValue is ConfigKeyValuePairInt)
                {
                    ((ConfigKeyValuePairInt)keyValue).DefaultValue = (int)Convert.ChangeType(value, typeof(int));
                }
                else if (keyValue is ConfigKeyValuePairBool)
                {
                    ((ConfigKeyValuePairBool)keyValue).DefaultValue = (bool)Convert.ChangeType(value, typeof(bool));
                }
                else
                {
                    ((ConfigKeyValuePairString)keyValue).DefaultValue = (string)Convert.ChangeType(value, typeof(string));
                }
            }
        }

        protected virtual void ReloadIfModified() { }

        public virtual void SetValue<T>(string sectionName, string keyName, T value)
        {
            this.ReloadIfModified();
            if (!_storeValues.IsExists(sectionName, keyName)) return;
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];

            if (keyValue != null)
            {
                if (keyValue is ConfigKeyValuePairLong)
                {
                    ((ConfigKeyValuePairLong)keyValue).Value = (long)Convert.ChangeType(value, typeof(long));
                }
                else if (keyValue is ConfigKeyValuePairInt)
                {
                    ((ConfigKeyValuePairInt)keyValue).Value = (int)Convert.ChangeType(value, typeof(int));
                }
                else if (keyValue is ConfigKeyValuePairBool)
                {
                    ((ConfigKeyValuePairBool)keyValue).Value = (bool)Convert.ChangeType(value, typeof(bool));
                }
                else
                {
                    ((ConfigKeyValuePairString)keyValue).Value = (string)Convert.ChangeType(value, typeof(string));
                }

                if (keyValue.IsValueChanged)
                {
                    this.Save();
                }
            }
        }

        public virtual void SetObjectValue(string sectionName, string keyName, object value)
        {
            this.ReloadIfModified();
            if (!_storeValues.IsExists(sectionName, keyName)) return;
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];

            if (keyValue != null)
            {
                keyValue.SetObjectValue(value);

                if (keyValue.IsValueChanged)
                {
                    this.Save();
                }
            }
        }

        public void SetValue(string sectionName, string keyName, string value)
        {
            SetValue(sectionName, keyName, value, true);
        }

        public virtual void SetValue(string sectionName, string keyName, string value, bool save)
        {
            if(save) this.ReloadIfModified();
            if (!_storeValues.IsExists(sectionName, keyName)) return;
            IConfigKeyValuePair keyValue = _storeValues[sectionName][keyName];

            if (keyValue != null)
            {
                if (keyValue is ConfigKeyValuePairLong)
                {
                    long value2 = 0;
                    long.TryParse(value, out value2);
                    ((ConfigKeyValuePairLong)keyValue).Value = value2;
                }
                else if (keyValue is ConfigKeyValuePairInt)
                {
                    int value2 = 0;
                    int.TryParse(value, out value2);
                    ((ConfigKeyValuePairInt)keyValue).Value = value2;
                }
                else if (keyValue is ConfigKeyValuePairBool)
                {
                    bool value2 = false;
                    if (value != null)
                    {
                        string valueNoCase = value.ToString().ToLower();
                        value2 = (valueNoCase == "true" || valueNoCase == "1");
                    }
                    ((ConfigKeyValuePairBool)keyValue).Value = value2;
                }
                else
                {
                    ((ConfigKeyValuePairString)keyValue).Value = value;
                }
            }

            if (save && keyValue.IsValueChanged)
            {
                this.Save();
            }
        }

        public abstract void Save();
    }
}
