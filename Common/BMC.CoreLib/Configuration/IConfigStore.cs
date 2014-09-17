using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BMC.CoreLib.Configuration
{
    public interface IConfigStore { }

    public interface IConfigStoreCustomAction : IConfigStore
    {
        void DoCustomAction(string propertyName, string propertyValue);
    }

    public delegate string ConfigStoreCustomActionDBHandler(string subKey, string propertyName, string propertyValue);

    public interface IConfigStoreCustomActionDB : IConfigStore
    {
        string DoCustomAction(string subKey, string propertyName, string propertyValue);
    }

    [DataContract(
        Namespace = "BMC.CoreLib.Configuration",
        Name = "ConfigStore")]
    [Serializable]
    public class ConfigStore : IConfigStore
    {
        public ConfigStore()
        {
            Log.Info("ConfigStore.ctor()");
        }
        public ConfigStore(bool reload)
            : this()
        {
            this.Reload();
        }
        public void Reload()
        {
            ConfigStoreManager.GetPropertyValues(this);
        }
    }

    [MessageContract(IsWrapped = true,
        WrapperNamespace = "BMC.CoreLib.Configuration",
        WrapperName = "ConfigStoreRequest")]
    public class ConfigStoreRequest
    {
        [MessageBodyMember(Namespace = "BMC.CoreLib.Configuration.ConfigStoreRequest",
            Name = "Request", Order = 0)]
        public ConfigStore Request { get; set; }
    }

    [MessageContract(IsWrapped = true,
        WrapperNamespace = "BMC.CoreLib.Configuration",
        WrapperName = "ConfigStoreResponse")]
    public class ConfigStoreResponse
    {
        [MessageBodyMember(Namespace = "BMC.CoreLib.Configuration.ConfigStoreResponse",
            Name = "Response", Order = 0)]
        public bool Response { get; set; }
    }
}
