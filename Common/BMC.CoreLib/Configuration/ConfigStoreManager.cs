using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.Comparers;
using System.Reflection;
using BMC.CoreLib.Registry;
using BMC.CoreLib.Concurrent;
using System.IO.Pipes;
using BMC.CoreLib.WMI.Win32;
using System.Runtime.Remoting;
using System.ComponentModel;
using System.Configuration;

namespace BMC.CoreLib.Configuration
{
    public class ConfigStoreManager : DisposableObject
    {
        private static IDictionary<Type, ConfigStoreManager> _configStores = null;

        private IList<ConfigAppSettingAttribute> _appSettings = new List<ConfigAppSettingAttribute>();
        private IList<ConfigRegistrySettingAttribute> _regSettings = new List<ConfigRegistrySettingAttribute>();

        private static ConfigStoreServiceHostFactory _hostFactory = null;
        private static object _hostFactoryLock = new object();

        static ConfigStoreManager()
        {
            _configStores = new SortedDictionary<Type, ConfigStoreManager>(new TypeComparer());
        }

        private ConfigStoreManager(IConfigStore store)
        {
            this.Store = store;
            this.Initialize();
        }

        internal static string GetPipeName(int processId)
        {
            return @"net.pipe://localhost/pipe/" + string.Format("ConfigStoreManager_{0:D}", processId);
        }

        internal IConfigStore Store { get; private set; }

        public static event ConfigStorePropertyChangedHandler PropertyChanged = null;

        private static void OnPropertyChanged(string propertyName, object propertyValue)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "OnPropertyChanged");
            Log.Info(PROC, "Method Entered.");

            try
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(propertyName, propertyValue);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static bool Register(params Type[] types)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Register");
            Log.Info(PROC, "Method Entered.");

            try
            {
                if (types != null)
                {
                    IList<Type> types2 = new List<Type>();
                    foreach (Type type in types)
                    {
                        Type type2 = RegisterInternal(type);
                        if (type2 != null)
                        {
                            types2.Add(type2);
                            Log.Info(PROC, type2.FullName + " was registered.");
                        }
                    }

                    if (types2.Count > 0)
                    {
                        if (_hostFactory == null)
                        {
                            lock (_hostFactoryLock)
                            {
                                if (_hostFactory == null)
                                {
                                    InitHostFactory(types2.ToArray());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return true;
        }

        public static T Get<T>()
            where T : IConfigStore
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Register");
            Type typeOfT = typeof(T);
            IConfigStore instance = null;

            try
            {
                if (_configStores.ContainsKey(typeOfT))
                {
                    instance = _configStores[typeOfT].Store as IConfigStore;
                    Log.Info(PROC, typeOfT.FullName + " was returned.");
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return (T)instance;
        }

        private static Type RegisterInternal(Type typeOfT)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Register");

            try
            {
                if (!_configStores.ContainsKey(typeOfT))
                {
                    IConfigStore instance = Activator.CreateInstance(typeOfT, true) as IConfigStore;
                    ConfigStoreManager manager = new ConfigStoreManager(instance);
                    _configStores.Add(typeOfT, manager);
                    return typeOfT;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return null;
        }

        private static void InitHostFactory(Type[] types)
        {
            ModuleProc PROC = new ModuleProc("", "InitHostFactory");

            try
            {
                _hostFactory = new ConfigStoreServiceHostFactory(types);
                _hostFactory.Start();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static IConfigStore Register(ObjectHandle oh)
        {
            ModuleProc PROC = new ModuleProc("", "Register");
            IConfigStore result = null;

            try
            {
                result = oh.Unwrap() as IConfigStore;
                if (result != null)
                {

                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private void Initialize()
        {
            GetPropertyValues(this.Store);
        }

        public static void GetPropertyValues(IConfigStore store)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Initialize");
            Type typeOfT = store.GetType();

            try
            {
                var properties = (from p in typeOfT.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                                  from a in p.GetCustomAttributes(typeof(ConfigSettingAttribute), true)
                                  select p);
                foreach (var property in properties)
                {
                    GetPropertyValueFromStore(store, property);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Initialize");
            Type typeOfT = this.Store.GetType();

            try
            {
                PropertyInfo property = (from p in typeOfT.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                         where p.Name.IgnoreCaseCompare(propertyName)
                                         select p).FirstOrDefault();
                if (property != null)
                {
                    this.SetPropertyValueToStore(property, value);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void SetPropertyValues()
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Initialize");
            Type typeOfT = this.Store.GetType();

            try
            {
                var properties = (from p in typeOfT.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                                  from a in p.GetCustomAttributes(typeof(ConfigSettingAttribute), true)
                                  select p);
                foreach (var property in properties)
                {
                    this.SetPropertyValueToStore(property, property.GetValue(this.Store, null), true);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void SetPropertyValues(IConfigStore store2)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Initialize");
            Type typeOfT = this.Store.GetType();
            Type typeOfT2 = store2.GetType();

            try
            {
                var properties = (from p in typeOfT2.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                                  from a in p.GetCustomAttributes(typeof(ConfigSettingAttribute), true)
                                  select p);
                var modifiedProperties = (from p in properties
                                          where p.GetValue(this.Store, null).Equals(p.GetValue(store2, null)) == false
                                          select new { Property = p, Value = p.GetValue(store2, null) });
                foreach (var property in modifiedProperties)
                {
                    this.SetPropertyValueToStore(property.Property, property.Value, true);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static string ExecuteCustomAction(IConfigStore store, string subKey, string keyName, string value)
        {
            using (ILogMethod method = Log.LogMethod("ConfigStoreManager", "ExecuteCustomAction"))
            {
                string result = default(string);

                try
                {
                    if (store is IConfigStoreCustomAction)
                    {
                        (store as IConfigStoreCustomAction).DoCustomAction(keyName, value);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private static string ExecuteCustomActionDB(IConfigStore store, string subKey, string keyName, string value)
        {
            using (ILogMethod method = Log.LogMethod("ConfigStoreManager", "ExecuteCustomActionDB"))
            {
                string result = default(string);

                try
                {
                    if (store is IConfigStoreCustomActionDB)
                    {
                        return (store as IConfigStoreCustomActionDB).DoCustomAction(subKey, keyName, value);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        private static object GetFormattedValue(Type keyType, string appValue)
        {
            object value = null;

            switch (keyType.FullName)
            {
                case "System.Char":
                    {
                        if (appValue.Length > 0)
                            value = appValue[0];
                        else
                            value = '\0';
                    }
                    break;

                case "System.Boolean":
                    value = TypeSystem.GetValueBoolSimple(appValue);
                    break;

                case "System.SByte":
                    value = TypeSystem.GetValueSByte(appValue);
                    break;

                case "System.Byte":
                    value = TypeSystem.GetValueByte(appValue);
                    break;

                case "System.Int16":
                    value = TypeSystem.GetValueShort(appValue);
                    break;

                case "System.UInt16":
                    value = TypeSystem.GetValueUShort(appValue);
                    break;

                case "System.Int32":
                    value = TypeSystem.GetValueInt(appValue);
                    break;

                case "System.UInt32":
                    value = TypeSystem.GetValueUInt(appValue);
                    break;

                case "System.Int64":
                    value = TypeSystem.GetValueInt64(appValue);
                    break;

                case "System.UInt64":
                    value = TypeSystem.GetValueUInt64(appValue);
                    break;

                case "System.Double":
                    value = TypeSystem.GetValueDouble(appValue);
                    break;

                default:
                    {
                        if (keyType.BaseType != null &&
                            keyType.BaseType == typeof(Enum))
                        {
                            if (Enum.IsDefined(keyType, appValue))
                            {
                                value = TypeSystem.GetValueEnum(keyType, appValue);
                            }
                            else
                            {
                                string sValue = Enum.GetName(keyType, TypeSystem.GetValueInt(appValue));
                                value = TypeSystem.GetValueEnum(keyType, sValue);
                            }
                        }
                        else
                        {
                            value = TypeSystem.GetValueString(appValue);
                        }
                    }
                    break;
            }

            return value;
        }

        private static void GetPropertyValueFromStore(IConfigStore store, PropertyInfo property)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Initialize");
            Type typeOfT = store.GetType();
            object value = null;

            try
            {
                var configAttribute = (from a in property.GetCustomAttributes(typeof(ConfigSettingAttribute), true)
                                       select a).OfType<ConfigSettingAttribute>().FirstOrDefault();
                if (configAttribute != null)
                {
                    Type attrType = configAttribute.GetType();
                    Type keyType = configAttribute.KeyType;

                    if (attrType == typeof(ConfigAppSettingAttribute))
                    {
                        ConfigAppSettingAttribute configAttributeApp = configAttribute as ConfigAppSettingAttribute;
                        string appValue = Extensions.GetAppSettingValue(configAttributeApp.KeyName, configAttributeApp.DefaultValue.ToStringSafe());

                        if (configAttributeApp.CustomAction)
                        {
                            ExecuteCustomAction(store, string.Empty, configAttributeApp.KeyName, appValue);
                        }
                        else
                        {
                            value = GetFormattedValue(keyType, appValue);
                        }
                    }
                    else if (attrType == typeof(ConfigRegistrySettingAttribute))
                    {
                        ConfigRegistrySettingAttribute configAttributeReg = configAttribute as ConfigRegistrySettingAttribute;
                        switch (configAttribute.KeyType.FullName)
                        {
                            case "System.Boolean":
                                value = RegistryHelper.GetValueBool(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                false);
                                break;

                            case "System.SByte":
                                value = RegistryHelper.GetValueSByte(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.Byte":
                                value = RegistryHelper.GetValueByte(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.Int16":
                                value = RegistryHelper.GetValueInt16(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.UInt16":
                                value = RegistryHelper.GetValueUInt16(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.Int32":
                                value = RegistryHelper.GetValueInt32(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.UInt32":
                                value = RegistryHelper.GetValueUInt32(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.Int64":
                                value = RegistryHelper.GetValueInt64(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.UInt64":
                                value = RegistryHelper.GetValueUInt64(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            case "System.Double":
                                value = RegistryHelper.GetValueDouble(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                0);
                                break;

                            default:
                                value = RegistryHelper.GetValueString(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                string.Empty);
                                break;
                        }
                    }
                    else if (attrType == typeof(ConfigApplicationSettingAttribute))
                    {
#if REF_BMC_COMMON
                        ConfigApplicationSettingAttribute configAttributeApp = configAttribute as ConfigApplicationSettingAttribute;
                        string appValue = BMC.Common.Utilities.BMCRegistryHelper.GetRegKeyValue(configAttributeApp.SubKey, configAttributeApp.KeyName);

                        if (configAttributeApp.CustomAction)
                        {
                            ExecuteCustomAction(store, configAttributeApp.SubKey, configAttributeApp.KeyName, appValue);
                        }
                        else
                        {
                            value = GetFormattedValue(keyType, appValue);
                        }
#endif
                    }
                    else if (attrType == typeof(ConfigDatabaseSettingAttribute))
                    {
                        ConfigDatabaseSettingAttribute configAttributeApp = configAttribute as ConfigDatabaseSettingAttribute;
                        if (configAttributeApp.CustomAction)
                        {
                            value = GetFormattedValue(keyType, ExecuteCustomActionDB(store, configAttributeApp.SubKey, configAttributeApp.KeyName, configAttributeApp.DefaultValue.ToStringSafe()));
                        }
                    }

                    if (property.CanWrite)
                        property.SetValue(store, value, null);
                    try
                    {
                        Log.InfoV(PROC, "Property : {0}, Value : {1}", property.Name, value.ToStringSafe());
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void SetPropertyValueToStore(PropertyInfo property, object value)
        {
            this.SetPropertyValueToStore(property, value, false);
        }

        private void SetPropertyValueToStore(PropertyInfo property, object value, bool skipUpdate)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "Initialize");
            Type typeOfT = this.Store.GetType();

            try
            {
                if (skipUpdate)
                {
                    property.SetValue(this.Store, value, null);
                }

                var configAttribute = (from a in property.GetCustomAttributes(typeof(ConfigSettingAttribute), true)
                                       select a).OfType<ConfigSettingAttribute>().FirstOrDefault();
                if (configAttribute != null)
                {
                    Type attrType = configAttribute.GetType();
                    if (attrType == typeof(ConfigAppSettingAttribute))
                    {

                    }
                    else if (attrType == typeof(ConfigRegistrySettingAttribute))
                    {
                        ConfigRegistrySettingAttribute configAttributeReg = configAttribute as ConfigRegistrySettingAttribute;
                        if (value.GetType() == typeof(bool))
                        {
                            if (configAttributeReg.RegKeyType == RegKeyType.REG_KT_DWORD)
                            {
                                value = ((bool)value ? 1 : 0);
                            }
                        }
                        RegistryHelper.SetRegValue(configAttributeReg.RegistryKey,
                                                                configAttributeReg.SubKey,
                                                                configAttributeReg.KeyName,
                                                                value);
                    }
                    OnPropertyChanged(property.Name, value);
                    try
                    {
                        Log.InfoV(PROC, "Property : {0}, Value : {1}", property.Name, value.ToString());
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private static ConfigStoreManager GetManager(IConfigStore store)
        {
            return GetManager(store, false);
        }

        private static ConfigStoreManager GetManager(IConfigStore store, bool ignoreInstance)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "GetManager");

            try
            {
                Type typeofT = store.GetType();
                if (_configStores.ContainsKey(typeofT))
                {
                    if (ignoreInstance)
                    {
                        return _configStores[typeofT];
                    }

                    IConfigStore storeSaved = _configStores[typeofT].Store;
                    if (Object.ReferenceEquals(store, storeSaved))
                    {
                        return _configStores[typeofT];
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return null;
        }

        public static void PushValue(IConfigStore store, string propertyName, object value)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "ModifyValue");

            try
            {
                ConfigStoreManager manager = GetManager(store);
                if (manager != null)
                {
                    manager.SetPropertyValue(propertyName, value);
                }

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void PushValues(IConfigStore store)
        {
            ModuleProc PROC = new ModuleProc("ConfigStoreManager", "ModifyValue");

            try
            {
                ConfigStoreManager manager = GetManager(store, true);
                if (manager != null)
                {
                    IConfigStore storeSaved = manager.Store;
                    if (Object.ReferenceEquals(store, storeSaved))
                    {
                        manager.SetPropertyValues();
                    }
                    else
                    {
                        manager.SetPropertyValues(store);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public static void PullValues(IConfigStore store)
        {
            GetPropertyValues(store);
        }
    }

    public delegate void ConfigStorePropertyChangedHandler(string propertyName, object propertyValue);
}
