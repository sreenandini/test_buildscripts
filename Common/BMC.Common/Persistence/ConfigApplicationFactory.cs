using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;

namespace BMC.Common.Persistence
{
    public interface IConfigApplication
    {
        void Initialize(bool load);

        void Load();

        void Save();

        void InitializeToDefaultValues();

        void RemoveValues();

        T GetValue<T>(string sectionName, string keyName, T defaultValue);

        object GetObjectValue(string sectionName, string keyName);

        string GetValue(string sectionName, string keyName);

        string GetValue(string sectionName, string keyName, string defaultValue);

        void SetValue<T>(string sectionName, string keyName, T value);

        void SetObjectValue(string sectionName, string keyName, object value);

        void SetValue(string sectionName, string keyName, string value);

        ConfigKeyValuePairTopDictionary KeyValues { get; }
    }

    public abstract class ConfigApplicationBase : IConfigApplication
    {
        protected BMC.Common.Persistence.IConfigProvider _configProvider = null;
        protected ConfigKeyValuePairTopDictionary _storeValues = new ConfigKeyValuePairTopDictionary();

        protected ConfigApplicationBase() { }

        public abstract void Initialize(bool load);

        public abstract void Load();

        public abstract void Save();

        public void InitializeToDefaultValues()
        {
            _configProvider.InitializeToDefaultValues(_storeValues);
        }

        public void RemoveValues()
        {
            _configProvider.RemoveValues(_storeValues);
        }

        private void InitializeSchema<T>(string sectionName, string keyName, T defaultValue)
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
                    keyValue.SetDefaultValue(defaultValue);
                    dictKey.Add(keyName, keyValue);
                }
            }
        }

        public void Initialize<T>(string sectionName, string keyName, T defaultValue)
        {
            this.InitializeSchema<T>(sectionName, keyName, defaultValue);
            this._configProvider.Initialize<T>(sectionName, keyName, defaultValue);
        }

        public T GetValue<T>(string sectionName, string keyName, T defaultValue)
        {
            return _configProvider.GetValue<T>(sectionName, keyName, defaultValue);
        }

        public object GetObjectValue(string sectionName, string keyName)
        {
            return _configProvider.GetObjectValue(sectionName, keyName);
        }

        public string GetValue(string sectionName, string keyName)
        {
            return _configProvider.GetValue(sectionName, keyName, string.Empty);
        }

        public string GetValue(string sectionName, string keyName, string defaultValue)
        {
            return _configProvider.GetValue(sectionName, keyName, defaultValue);
        }

        public void SetValue<T>(string sectionName, string keyName, T value)
        {
            _configProvider.SetValue<T>(sectionName, keyName, value);
        }

        public void SetObjectValue(string sectionName, string keyName, object value)
        {
            _configProvider.SetObjectValue(sectionName, keyName, value);
        }

        public void SetValue(string sectionName, string keyName, string value)
        {
            _configProvider.SetValue(sectionName, keyName, value);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ConfigKeyValuePairTopDictionary KeyValues { get { return _storeValues; } }
    }

    public static class ConfigApplicationFactory
    {
        public static readonly IDictionary<string, string> AvailableInterfaces =
            new SortedDictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            { typeof(IConfig_EnterpriseClient).FullName, "Enterprise Client" },
            { typeof(IConfig_EnterpriseServer).FullName, "Enterprise Server" },
            { typeof(IConfig_ExchangeClient).FullName, "Exchange Client" },
            { typeof(IConfig_ExchangeServer).FullName, "Exchange Server"  },
        };

        private static IDictionary<string, Func<IConfigProvider, bool, IConfigApplication>> _instanceFuncs =
            new SortedDictionary<string, Func<IConfigProvider, bool, IConfigApplication>>(StringComparer.InvariantCultureIgnoreCase)
        {
            { typeof(IConfig_EnterpriseClient).FullName, (p,l) => { return new Config_EnterpriseClient(p,l); }},
            { typeof(IConfig_EnterpriseServer).FullName, (p,l) => { return new Config_EnterpriseServer(p,l); }},
            { typeof(IConfig_ExchangeClient).FullName, (p,l) => { return new Config_ExchangeClient(p,l); }},
            { typeof(IConfig_ExchangeServer).FullName, (p,l) => { return new Config_ExchangeServer(p,l); }},
        };
        private static IDictionary<string, IConfigApplication> _instances =
            new SortedDictionary<string, IConfigApplication>(StringComparer.InvariantCultureIgnoreCase);
        private static object _lock = new object();

        private static IConfigProvider _configProvider = null;
        private static object _lockProvider = new object();

        static ConfigApplicationFactory()
        {
            GetProvider();
        }

        [Export("DisableLogging")]
        public static bool DisableLogging
        {
            get { return GetValue<bool>("Honeyframe", "DisableLogging", false); }
        }

        public static string DefaultLogDir
        {
            get { return GetValue<string>("Honeyframe", "DefaultLogDir", string.Empty); }
            set { SetValue("Honeyframe", "DefaultLogDir", value); }
        }

        public static IConfigProvider GetProvider()
        {
            if (_configProvider == null)
            {
                lock (_lockProvider)
                {
                    if (_configProvider == null)
                    {
                        // remove invalid file name chareacters
                        string fileName = Environment.MachineName + ".xml";
                        foreach (var invalidChar in Path.GetInvalidFileNameChars())
                        {
                            fileName = fileName.Replace(invalidChar, '_');
                        }

                        _configProvider = ConfigProviderFactory.Create(fileName);
                        foreach (var instanceFunc in _instanceFuncs)
                        {
                            Get(instanceFunc.Key);
                        }
                    }
                }
            }

            return _configProvider;
        }

        public static T Get<T>()
            where T : IConfigApplication
        {
            return (T)Get(typeof(T).FullName);
        }

        public static T GetAny<T>()
            where T : IConfigApplication
        {
            T result = default(T);
            Type typeOfT = typeof(T);

            try
            {
                result = Get<T>();
                if (result == null)
                {
                    foreach (var instance in _instances)
                    {
                        IConfigApplication value = instance.Value;
                        if (value != null)
                        {
                            var found = (from i in value.GetType().GetInterfaces()
                                         where i.FullName.IgnoreCaseCompare(typeOfT.FullName)
                                         select i).FirstOrDefault();
                            if (found != null)
                            {
                                result = (T)value;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }

            return result;
        }

        public static IConfigApplication Get(string typeOfT)
        {
            IConfigApplication result = null;

            lock (_lock)
            {
                if (_instances.ContainsKey(typeOfT))
                {
                    result = _instances[typeOfT];
                }
                else if (_instanceFuncs.ContainsKey(typeOfT))
                {
                    result = _instanceFuncs[typeOfT](_configProvider, true);
                    _instances.Add(typeOfT, result);
                }
            }

            return result;
        }

        #region Generic Functions
        public static T GetValue<T>(string sectionName, string keyName, T defaultValue)
        {
            return GetProvider().GetValue<T>(sectionName, keyName, defaultValue);
        }

        public static string GetValue(string sectionName, string keyName)
        {
            return GetProvider().GetValue(sectionName, keyName, string.Empty);
        }

        public static string GetValue(string sectionName, string keyName, string defaultValue)
        {
            return GetProvider().GetValue(sectionName, keyName, defaultValue);
        }

        public static void SetValue<T>(string sectionName, string keyName, T value)
        {
            GetProvider().SetValue<T>(sectionName, keyName, value);
        }

        public static void SetValue(string sectionName, string keyName, string value)
        {
            GetProvider().SetValue(sectionName, keyName, value);
        }
        #endregion
    }
}
