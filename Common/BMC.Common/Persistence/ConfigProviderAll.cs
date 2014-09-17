using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Common.Persistence
{
    internal class ConfigProviderAllInput
    {
        private ConfigProviderAccessTypes _accessTypes = ConfigProviderAccessTypes.None;
        private bool _isReadable = false;
        private bool _isWritable = false;

        public ConfigProviderBase Provider { get; set; }

        public ConfigProviderAccessTypes AccessTypes
        {
            get { return _accessTypes; }
            set
            {
                _accessTypes = value;
                _isReadable = ((value & ConfigProviderAccessTypes.Read) == ConfigProviderAccessTypes.Read);
                _isWritable = ((value & ConfigProviderAccessTypes.Write) == ConfigProviderAccessTypes.Write);
            }
        }

        internal bool IsReadable { get { return _isReadable; } }

        internal bool IsWritable { get { return _isWritable; } }
    }

    internal class ConfigProviderAll : ConfigProviderBase
    {
        private ConfigProviderAllInput[] _inputs = null;

        private int _length = 0;

        private ConfigProviderBase _defaultReader = null;

        internal ConfigProviderAll(ConfigProviderAllInput[] inputs)
        {
            _inputs = inputs;
            _length = inputs.Length;
            this.FindDefaultReader();
        }

        public override bool IsMetadataInitialized
        {
            get
            {
                return base.IsMetadataInitialized;
            }
            set
            {
                base.IsMetadataInitialized = value;
                this.DoWriteWork((p) => p.IsMetadataInitialized = value);
            }
        }

        private void FindDefaultReader()
        {
            for (int i = 0; i < _length; i++)
            {
                ConfigProviderAllInput input = _inputs[i];
                if (input.IsReadable)
                {
                    _defaultReader = input.Provider;
                    break;
                }
            }
        }

        private void DoWriteWork(Action<ConfigProviderBase> work)
        {
            for (int i = 0; i < _length; i++)
            {
                ConfigProviderAllInput input = _inputs[i];
                if (input.IsWritable) work(input.Provider);
            }
        }

        public override void Load()
        {
            this.DoWriteWork((p) => p.Load());
        }

        public override void LoadInternal() { }

        public override void Save()
        {
            this.DoWriteWork((p) => p.Save());
        }

        public override void Initialize<T>(string sectionName, string keyName, T value)
        {
            this.DoWriteWork((p) =>
            {
                p.Initialize<T>(sectionName, keyName, value);
            });
        }

        public override object GetObjectValue(string sectionName, string keyName)
        {
            return _defaultReader.GetObjectValue(sectionName, keyName);
        }

        public override T GetValue<T>(string sectionName, string keyName, T defaultValue)
        {
            return _defaultReader.GetValue<T>(sectionName, keyName, defaultValue);
        }

        public override string GetValue(string sectionName, string keyName, string defaultValue)
        {
            return _defaultReader.GetValue(sectionName, keyName, defaultValue);
        }

        public override void InitializeToDefaultValues(ConfigKeyValuePairTopDictionary storeValues)
        {
            this.DoWriteWork((p) =>
            {
                p.InitializeToDefaultValues(storeValues);
            });
        }

        internal override void InitializeToDefaultValues(IConfigKeyValuePair keyValue, string sectionName, string keyName)
        {
            this.DoWriteWork((p) =>
            {
                p.InitializeToDefaultValues(keyValue, sectionName, keyName);
            });
        }

        internal override void RemoveSubKey(string sectionName)
        {
            this.DoWriteWork((p) =>
            {
                p.RemoveSubKey(sectionName);
            });
        }

        internal override void RemoveValue(IConfigKeyValuePair keyValue, string sectionName, string keyName)
        {
            this.DoWriteWork((p) =>
            {
                p.RemoveValue(keyValue, sectionName, keyName);
            });
        }

        public override void SetObjectValue(string sectionName, string keyName, object value)
        {
            this.DoWriteWork((p) =>
            {
                p.SetObjectValue(sectionName, keyName, value);
            });
        }

        public override void SetValue<T>(string sectionName, string keyName, T value)
        {
            this.DoWriteWork((p) =>
            {
                p.SetValue<T>(sectionName, keyName, value);
            });
        }

        public override void SetValue(string sectionName, string keyName, string value, bool save)
        {
            this.DoWriteWork((p) =>
            {
                p.SetValue(sectionName, keyName, value, save);
            });
        }
    }
}
