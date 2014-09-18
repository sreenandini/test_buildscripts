using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Configuration;
using BMC.CoreLib.Diagnostics;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.Configuration
{
    public interface IExCommsServerConfigStore : IExchangeConfigStore
    {
        int FreeformExecutorType { get; set; }

        int PriorityThreads { get; set; }

        int PriorityThreadsGIM { get; set; }

        int NonPriorityThreads { get; set; }

        bool LogRawFreeformMessages { get; set; }

        int ExCommServerTcpPort { get; set; }

        int ExCommServerHttpPort { get; set; }

        bool EnableDHCP { get; set; }

        string DefaultServerIP { get; set; }

        string MulticastIp { get; set; }

        string InterfaceIp { get; set; }

        int WaitTime { get; set; }

        int ReceivePortNo { get; set; }

        int TransmitPortNo { get; set; }

        long AFTMsgTimeOut { get; set; }

        bool Is1AReqForBalReq { get; set; }

        bool Is1AReqForECashWd { get; set; }

        bool Is1AReqForECashDep { get; set; }

        bool IsCrossTicketingEnabled { get; set; }

        bool RSAEnable { get; set; }
    }

    internal class ExCommsServerConfigStore 
        : ExchangeConfigStore, IExCommsServerConfigStore
    {
        internal ExCommsServerConfigStore()
        {
            this.Initialize();
        }

        protected override sealed void Initialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {                
                this.ReceivePortNo = FreeformConstants.PORTNO_RECEIVE;
                this.TransmitPortNo = FreeformConstants.PORTNO_SEND;
                this.ExCommServerHttpPort = 8991;
                this.ExCommServerTcpPort = 8992;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        // Default Values
        [ConfigAppSetting("FreeformExecutorType", typeof(int))]
        public int FreeformExecutorType { get; set; }
        
        [ConfigAppSetting("Freeform_PriorityThreads", typeof(int))]
        public int PriorityThreads { get; set; }

        [ConfigAppSetting("Freeform_PriorityThreads_GIM", typeof(int))]
        public int PriorityThreadsGIM { get; set; }

        [ConfigAppSetting("Freeform_NonPriorityThreads", typeof(int))]
        public int NonPriorityThreads { get; set; }

        [ConfigAppSetting("LogRawFreeformMessages", typeof(bool))]
        public bool LogRawFreeformMessages { get; set; }

        [ConfigAppSetting("ReceivePortNo", typeof(int))]
        public int ReceivePortNo { get; set; }

        [ConfigAppSetting("TransmitPortNo", typeof(int))]
        public int TransmitPortNo { get; set; }

        [ConfigAppSetting("AFTMsgTimeOut", typeof(long))]
        public long AFTMsgTimeOut { get; set; }

        [ConfigAppSetting("Is1AReqForBalReq", typeof(bool))]
        public bool Is1AReqForBalReq { get; set; }

        [ConfigAppSetting("Is1AReqForECashWd", typeof(bool))]
        public bool Is1AReqForECashWd { get; set; }

        [ConfigAppSetting("Is1AReqForECashDep", typeof(bool))]
        public bool Is1AReqForECashDep { get; set; }

        [ConfigAppSetting("IsCrossTicketingEnabled", typeof(bool))]
        public bool IsCrossTicketingEnabled { get; set; }

        // Registry Values
        public int ExCommServerTcpPort { get; set;
            //get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExCommsServerTcpPort; }
            //set { _cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExCommsServerTcpPort = value; }
        }

        public int ExCommServerHttpPort
        {
            get;
            set;
            //get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExCommsServerHttpPort; }
            //set { _cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExCommsServerHttpPort = value; }
        }

        public bool EnableDHCP
        {
            get { return TypeSystem.GetValueBoolSimple(_cfgExchange.Honeyframe_Cashmaster_Exchange_EnableDhcp); }
            set { _cfgExchange.Honeyframe_Cashmaster_Exchange_EnableDhcp = value ? 1 : 0; }
        }

        public string DefaultServerIP
        {
            get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_default_server_ip; }
            set { _cfgExchange.Honeyframe_Cashmaster_Exchange_default_server_ip = value; }
        }

        public string MulticastIp
        {
            get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_multicastip; }
            set { _cfgExchange.Honeyframe_Cashmaster_Exchange_multicastip = value; }
        }

        public string InterfaceIp
        {
            get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_interface; }
            set { _cfgExchange.Honeyframe_Cashmaster_Exchange_interface = value; }
        }

        public int WaitTime
        {
            get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_Events_dwWaitTime; }
            set { _cfgExchange.Honeyframe_Cashmaster_Exchange_Events_dwWaitTime = value; }
        }

        public bool RSAEnable
        {
            get
            {
                return _cfgExchange.Honeyframe_Cashmaster_Exchange_Events_RSAEnable == 1;
            }
            set
            {
                _cfgExchange.Honeyframe_Cashmaster_Exchange_Events_RSAEnable = (value ? 1 : 0);
            }
        }
    }

    public static class ExCommsServerConfigStoreFactory
    {
        private static readonly SingletonHelper<IExCommsServerConfigStore> _singletonHelper =
            new SingletonHelper<IExCommsServerConfigStore>(new Lazy<IExCommsServerConfigStore>(() => new ExCommsServerConfigStore()));

        public static IExCommsServerConfigStore Store
        {
            get { return _singletonHelper.Current; }
        }
    }
}
