using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace BMC.CoreLib.Configuration
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public abstract class ConfigSettingAttribute : Attribute
    {
        public ConfigSettingAttribute(string keyName, Type keyType)
        {
            this.KeyName = keyName;
            this.KeyType = keyType;
        }

        public string KeyName { get; set; }

        public Type KeyType { get; set; }

        public object DefaultValue { get; set; }

        public bool CustomAction { get; set; }
    }

    public class ConfigAppSettingAttribute : ConfigSettingAttribute
    {
        public ConfigAppSettingAttribute(string keyName, Type keyType)
            : base(keyName, keyType) { }

        public ConfigAppSettingAttribute(string keyName, Type keyType, object defaultValue)
            : base(keyName, keyType)
        {
            this.DefaultValue = defaultValue;
        }

        public object DefaultValue { get; set; }
    }

    public class ConfigRegistrySettingAttribute : ConfigSettingAttribute
    {
        public ConfigRegistrySettingAttribute(string subKey, string keyName, Type keyType)
            : this(GetRegBaseKey(RegistryHive.LocalMachine), subKey, keyName, keyType, RegKeyType.REG_KT_DWORD) { }

        public ConfigRegistrySettingAttribute(string subKey, string keyName, Type keyType, RegKeyType regKeyType)
            : this(GetRegBaseKey(RegistryHive.LocalMachine), subKey, keyName, keyType, regKeyType) { }

        public ConfigRegistrySettingAttribute(RegistryKey registryKey, string subKey, string keyName, Type keyType, RegKeyType regKeyType)
            : base(keyName, keyType)
        {
            this.RegistryKey = registryKey;
            this.SubKey = subKey;
            this.RegKeyType = regKeyType;
        }

        public static RegistryKey GetRegBaseKey(RegistryHive hive)
        {
#if NET4
            return RegistryKey.OpenBaseKey(hive, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32));
#else
            RegistryKey result = null;

            switch (hive)
            {
                case RegistryHive.ClassesRoot:
                    break;
                case RegistryHive.CurrentConfig:
                    break;
                case RegistryHive.CurrentUser:
                    break;
                case RegistryHive.DynData:
                    break;
                case RegistryHive.PerformanceData:
                    break;
                case RegistryHive.Users:
                    break;
                default:
                    result = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }

            return result;
#endif
        }

        public RegistryKey RegistryKey { get; private set; }
        public string SubKey { get; private set; }
        public RegKeyType RegKeyType { get; private set; }
    }

    public class ConfigApplicationSettingAttribute : ConfigSettingAttribute
    {
        public ConfigApplicationSettingAttribute(string subKey, string keyName)
            : this(subKey, keyName, typeof(string)) { }

        public ConfigApplicationSettingAttribute(string subKey, string keyName, Type keyType)
            : base(keyName, keyType)
        {
            this.SubKey = subKey;
        }

        public string SubKey { get; private set; }
    }

    public class ConfigDatabaseSettingAttribute : ConfigSettingAttribute
    {
        public ConfigDatabaseSettingAttribute(string keyName, Type keyType)
            : base(keyName, keyType)
        {
            this.CustomAction = true;
        }

        public ConfigDatabaseSettingAttribute(string keyName, Type keyType, object defaultValue)
            : this(keyName, keyType)
        {
            this.DefaultValue = defaultValue;
        }

        public string SubKey { get; set; }
    }

    public enum RegKeyType
    {
        REG_KT_SZ = 0,
        REG_KT_DWORD = 1,
        REG_KT_BINARY = 2
    }
}
