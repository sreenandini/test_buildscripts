using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Persistence;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Configuration;

namespace BMC.ExComms.Contracts.Configuration
{
    public interface IExchangeConfigStore : IConfigStore
    {
        IExecutorService Executor { get; set; }

        string LogPath { get; set; }

        bool LogIncomingMessages { get; set; }

        bool LogOutgoingMessages { get; set; }

        bool LogRawMessages { get; set; }

        bool LogClients { get; set; }
    }

    internal class ExchangeConfigStore 
        : DisposableObject, IExchangeConfigStore
    {
        protected IExecutorService _executor = null;
        protected IConfig_ExchangeServer _cfgExchange = null;

        internal ExchangeConfigStore()
        {
            _executor = ExecutorServiceFactory.CreateExecutorService();
            _cfgExchange = ConfigApplicationFactory.Get<IConfig_ExchangeServer>();
            ConfigStoreManager.PullValues(this);
            //ConfigStoreManager.Register(new System.Runtime.Remoting.ObjectHandle(this));
        }

        protected virtual void Initialize() { }

        public IExecutorService Executor
        {
            get
            {
                return _executor;
            }
            set
            {
                _executor = value;
            }
        }

        // AppSetting Values
        [ConfigAppSetting("LogPath", typeof(string))]
        public string LogPath { get; set; }

        [ConfigAppSetting("LogIncomingMessages", typeof(bool))]
        public bool LogIncomingMessages { get; set; }

        [ConfigAppSetting("LogOutgoingMessages", typeof(bool))]
        public bool LogOutgoingMessages { get; set; }

        [ConfigAppSetting("LogRawMessages", typeof(bool))]
        public bool LogRawMessages { get; set; }

        [ConfigAppSetting("LogClients", typeof(bool))]
        public bool LogClients { get; set; }
    }

    public static class ExchangeConfigStoreFactory
    {
        private static readonly SingletonHelper<IExchangeConfigStore> _singletonHelper =
            new SingletonHelper<IExchangeConfigStore>(new Lazy<IExchangeConfigStore>(() => new ExchangeConfigStore()));

        public static IExchangeConfigStore Store
        {
            get { return _singletonHelper.Current; }
        }
    }
}
