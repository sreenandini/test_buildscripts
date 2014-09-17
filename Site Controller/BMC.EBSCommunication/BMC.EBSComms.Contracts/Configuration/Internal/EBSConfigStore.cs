using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Persistence;
using BMC.CoreLib;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Configuration;

namespace BMC.EBSComms.Contracts.Configuration
{
    internal abstract class EBSConfigStore : DisposableObject, IEBSConfigStore
    {
        protected IExecutorService _executor = null;
        protected IConfig_ExchangeServer _cfgExchange = null;

        internal EBSConfigStore()
        {
            _executor = ExecutorServiceFactory.CreateExecutorService();            
            _cfgExchange = ConfigApplicationFactory.Get<IConfig_ExchangeServer>();
            ConfigStoreManager.PullValues(this);
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

        [ConfigAppSetting("LogClients", typeof(bool))]
        public bool LogClients { get; set; }
    }
}
