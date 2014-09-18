using System;
using System.Collections.Generic;
using System.Text;
using BMC.Common.Security;
using BMC.Common.Utilities;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.CoreLib.Configuration;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib.IoC;

namespace BMC.ExComms.Contracts.Configuration
{
    public interface IExMonitorServerConfigStore
        : IExchangeConfigStore,
        IConfigStoreCustomAction,
        IConfigStoreCustomActionDB
    {
        string ConnectionString { get; }

        string ExtSysMsgConnectionString { get; }

        int dwWaitTime { get; set; }

        string MonitorServerLocalPath { get; }

        string MonitorServerRemotePath { get; }

        int MonitorServerTcpPort { get; }

        int MonitorServerHttpPort { get; }

        // App.config Settings
        string CMPEncodingType { get; set; }

        Encoding CMPEncodingType2 { get; }

        string EncryptionKey { get; set; }

        string AllowPT10ForNonCardedPlay { get; set; }

        bool IsCrossTicketingEnabled { get; set; }

        bool GameCapPINValidationRequired { get; set; }

        double EPICardInMeterUpdate { get; set; }

        bool GamePlayInfoRequiredForSession { get; set; }

        short MaxRows { get; set; }

        IDictionary<string, bool> ForceMeterReads { get; }

        IDictionary<string, bool> ForceInstallations { get; }

        char PCRatingBasis { get; set; }

        bool PreCommitMentEnabled { get; set; }
        bool IsEmployeeCardTrackingEnabled { get; set; }

        // Player Gateway
        string PT_GATEWAY_IP { get; set; }
        int SDT_SendPTPortNo { get; set; }
        int SDT_ReceivePTPortNo { get; set; }
        int SDT_SendCAPortNo { get; set; }
        int SDT_ReceiveCAPortNo { get; set; }
        bool IsWSCallMessage { get; set; }
        bool IsPlayerCacheResponseRequired { get; set; }
        bool IsAlwaysSendRequestsViaSocket { get; set; }
        bool IsGetResponseFromCache { get; set; }
        bool IsPDWSCallRequired { get; set; }
        bool IsCACallRequiredForWithdrawal { get; set; }
        bool IsCACallRequiredForDeposit { get; set; }
        bool IsCACallRequiredForBalance { get; set; }
        bool IsPT81WSCallRequired { get; set; }
        bool IsMultipleSocketClientCallsRequired { get; set; }
        string CMPWebServiceURL { get; }
        string PCGatewayURL { get; set; }
        string UseCMP123 { get; set; }
        bool SocketSendWithoutReconnect { get; set; }
        int SocketSendRetryCount { get; set; }
        bool IsStockPrefixRequired { get; set; }
        string Stock_Code_Prefix { get; set; }
        int GMU_TimeOut { get; set; }
        int TotInstGrp { get; set; }
        int InsPerDelay { get; set; }
        bool RamResetRequired { get; set; }
        bool CheckDenomChangeForRamReset { get; set; }
        int MachineDisableRetryCount { get; set; }
        int DenomChangeRetryCount { get; set; }
        //bool IsAlwaysSendRequestsViaSocket { get; set; }
        //bool IsAlwaysSendRequestsViaSocket { get; set; }
        //bool IsAlwaysSendRequestsViaSocket { get; set; }
        //bool IsAlwaysSendRequestsViaSocket { get; set; }
        //bool IsAlwaysSendRequestsViaSocket { get; set; }

        bool Iview3AssetNum { get; set; }
    }

    internal class ExMonitorServerConfigStore
        : ExchangeConfigStore,
        IExMonitorServerConfigStore,
        IConfigStoreCustomAction,
        IConfigStoreCustomActionDB
    {
        private IDictionary<string, bool> _forceMeterReads = null;
        private IDictionary<string, bool> _forceInstallations = null;

        internal ExMonitorServerConfigStore()
        {
            this.Initialize();
        }

        protected override sealed void Initialize()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Initialize");

            try
            {
                this.GIMReceivePort = 11112; // never changes
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public virtual string DoCustomAction(string subKey, string propertyName, string propertyValue)
        {
            return string.Empty;
        }

        // Default Values
        public int GIMReceivePort { get; private set; }

        // Registry Values

        public string ConnectionString
        {
            get
            {
                return DatabaseHelper.GetConnectionString();
            }
        }

        public string ExtSysMsgConnectionString
        {
            get
            {
                string connectionString = _cfgExchange.Honeyframe_Cashmaster_PCConnect;
                if (!connectionString.Contains("SERVER"))
                {
                    connectionString = CryptEncode.Decrypt(connectionString);
                }
                return connectionString;
            }
        }

        public int dwWaitTime
        {
            get { return _cfgExchange.Honeyframe_Cashmaster_Exchange_Events_dwWaitTime; }
            set { _cfgExchange.Honeyframe_Cashmaster_Exchange_Events_dwWaitTime = value; }
        }

        public string MonitorServerLocalPath
        {
            get
            {
                return string.Empty;// _cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExMonitorServerLocalPath;
            }
        }

        public string MonitorServerRemotePath
        {
            get
            {
                return string.Empty;// _cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExMonitorServerRemotePath;
            }
        }

        public int MonitorServerTcpPort
        {
            get
            {
                return 8891;//_cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExMonitorServerTcpPort;
            }
        }

        public int MonitorServerHttpPort
        {
            get
            {
                return 8892;//_cfgExchange.Honeyframe_Cashmaster_Exchange_Services_ExMonitorServerHttpPort;
            }
        }

        [ConfigAppSetting("CMPEncodingType", typeof(string), "Default")]
        public string CMPEncodingType { get; set; }

        public Encoding CMPEncodingType2
        {
            get
            {
                return (this.CMPEncodingType.IgnoreCaseCompare("ASCII") ? Encoding.ASCII : ASCIIEncoding.Default);
            }
        }

        [ConfigAppSetting("EncryptionKey", typeof(string), "")]
        public string EncryptionKey { get; set; }

        [ConfigAppSetting("AllowPT10ForNonCardedPlay", typeof(string), "")]
        public string AllowPT10ForNonCardedPlay { get; set; }

        [ConfigAppSetting("IsCrossTktEn", typeof(bool))]
        public bool IsCrossTicketingEnabled { get; set; }

        [ConfigAppSetting("GameCapPINValidationRequired", typeof(bool))]
        public bool GameCapPINValidationRequired { get; set; }

        [ConfigAppSetting("EPICardInMeterUpdate", typeof(double))]
        public double EPICardInMeterUpdate { get; set; }

        [ConfigAppSetting("PCRatingBasis", typeof(char))]
        public char PCRatingBasis { get; set; }

        [ConfigAppSetting("PreCommitMentEnabled", typeof(bool))]
        public bool PreCommitMentEnabled { get; set; }

        [ConfigAppSetting("GamePlayInfoRequiredForSession", typeof(bool))]
        public bool GamePlayInfoRequiredForSession { get; set; }

        [ConfigAppSetting("MaxRows", typeof(short))]
        public short MaxRows { get; set; }

        [ConfigAppSetting("Iview3AssetNum", typeof(bool))]
        public bool Iview3AssetNum { get; set; }

        [ConfigAppSetting("ForceMeterReads", typeof(string), CustomAction = true)]
        public IDictionary<string, bool> ForceMeterReads
        {
            get { return _forceMeterReads; }
        }

        [ConfigAppSetting("ForceInstallations", typeof(string), CustomAction = true)]
        public IDictionary<string, bool> ForceInstallations
        {
            get { return _forceInstallations; }
        }

        public void DoCustomAction(string propertyName, string propertyValue)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "DoCustomAction"))
            {
                try
                {
                    switch (propertyName)
                    {
                        case "ForceMeterReads":
                        case "ForceInstallations":
                            {
                                if (propertyValue.IsEmpty()) return;
                                string[] splitValues = propertyValue.Split(';');
                                if (splitValues.Length == 0) return;

                                IDictionary<string, bool> collection = null;
                                if (propertyName == "ForceMeterReads")
                                    collection = _forceMeterReads ?? (_forceMeterReads = new StringConcurrentDictionary<bool>());
                                else if (propertyName == "ForceInstallations")
                                    collection = _forceInstallations ?? (_forceInstallations = new StringConcurrentDictionary<bool>());

                                foreach (var splitValue in splitValues)
                                {
                                    if (!collection.ContainsKey(splitValue))
                                        collection.Add(splitValue, true);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        // Player Gateway
        [ConfigDatabaseSetting("PT_GATEWAY_IP", typeof(string), DefaultValue = "")]
        public string PT_GATEWAY_IP { get; set; }

        [ConfigDatabaseSetting("SDT_SendPTPortNo", typeof(int), DefaultValue = "6701")]
        public int SDT_SendPTPortNo { get; set; }

        [ConfigDatabaseSetting("SDT_ReceivePTPortNo", typeof(int), DefaultValue = "6702")]
        public int SDT_ReceivePTPortNo { get; set; }

        [ConfigDatabaseSetting("SDT_SendCAPortNo", typeof(int), DefaultValue = "8801")]
        public int SDT_SendCAPortNo { get; set; }

        [ConfigDatabaseSetting("SDT_ReceiveCAPortNo", typeof(int), DefaultValue = "8802")]
        public int SDT_ReceiveCAPortNo { get; set; }

        [ConfigDatabaseSetting("SDT_IsWSCallMessage", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsWSCallMessage { get; set; }

        [ConfigDatabaseSetting("IsPlayerCacheResponseRequired", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsPlayerCacheResponseRequired { get; set; }

        [ConfigDatabaseSetting("IsAlwaysSendRequestsViaSocket", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsAlwaysSendRequestsViaSocket { get; set; }

        [ConfigDatabaseSetting("IsGetResponseFromCache", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsGetResponseFromCache { get; set; }

        [ConfigDatabaseSetting("SDT_IsPDWSCallRequired", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsPDWSCallRequired { get; set; }

        [ConfigDatabaseSetting("SDT_IsCACallRequiredForWithdrawal", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsCACallRequiredForWithdrawal { get; set; }

        [ConfigDatabaseSetting("SDT_IsCACallRequiredForDeposit", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsCACallRequiredForDeposit { get; set; }

        [ConfigDatabaseSetting("SDT_IsCACallRequiredForBalance", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsCACallRequiredForBalance { get; set; }

        [ConfigDatabaseSetting("SDT_IsPT81WSCallRequired", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsPT81WSCallRequired { get; set; }

        [ConfigDatabaseSetting("SDT_MultipleSocketCalls", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool IsMultipleSocketClientCallsRequired { get; set; }

        public string CMPWebServiceURL
        {
            get
            {
                return _cfgExchange.Honeyframe_Cashmaster_CMPWebserviceURL;
            }
        }

        [ConfigAppSetting("PCGatewayURL", typeof(string))]
        public string PCGatewayURL { get; set; }

        [ConfigAppSetting("UseCMP123", typeof(string))]
        public string UseCMP123 { get; set; }

        [ConfigDatabaseSetting("SocketSendWithoutReconnect", typeof(bool), SubKey = "DevSettings", DefaultValue = false)]
        public bool SocketSendWithoutReconnect { get; set; }

        [ConfigDatabaseSetting("SocketSendRetryCount", typeof(int), SubKey = "DevSettings", DefaultValue = 1)]
        public int SocketSendRetryCount { get; set; }

        [ConfigDatabaseSetting("CheckForStockPrefix", typeof(bool), DefaultValue = false)]
        public bool IsStockPrefixRequired { get; set; }

        [ConfigDatabaseSetting("System_Parameter_Stock_Code_Prefix", typeof(string), DefaultValue = "")]
        public string Stock_Code_Prefix { get; set; }

        [ConfigDatabaseSetting("IsEmployeeCardTrackingEnabled", typeof(bool), DefaultValue = false)]
        public bool IsEmployeeCardTrackingEnabled { get; set; }

        [ConfigDatabaseSetting("GMU_TimeOut", typeof(int), DefaultValue = 10000)]
        public int GMU_TimeOut { get; set; }

        [ConfigDatabaseSetting("TotInstGrp", typeof(int), DefaultValue = 1)]
        public int TotInstGrp { get; set; }

        [ConfigDatabaseSetting("InsPerDelay", typeof(int), DefaultValue = 1)]
        public int InsPerDelay { get; set; }

        [ConfigDatabaseSetting("RamResetRequired", typeof(bool), DefaultValue = false)]
        public bool RamResetRequired { get; set; }

        [ConfigDatabaseSetting("CheckDenomChangeForRamReset", typeof(bool), DefaultValue = false)]
        public bool CheckDenomChangeForRamReset { get; set; }

        [ConfigDatabaseSetting("MachineDisableRetryCount", typeof(int), DefaultValue = 10)]
        public int MachineDisableRetryCount { get; set; }

        [ConfigDatabaseSetting("DenomChangeRetryCount", typeof(int), DefaultValue = 12)]
        public int DenomChangeRetryCount { get; set; }
    }

    public static class ExMonitorServerConfigStoreFactory
    {
        private static readonly SingletonHelper<IExMonitorServerConfigStore> _singletonHelper = null;

        static ExMonitorServerConfigStoreFactory()
        {
            _singletonHelper = new SingletonHelper<IExMonitorServerConfigStore>(Create);
        }

        private static IExMonitorServerConfigStore Create()
        {
            using (ILogMethod method = Log.LogMethod("ExMonitorServerConfigStoreFactory", "Create"))
            {
                IExMonitorServerConfigStore result = default(IExMonitorServerConfigStore);

                try
                {
                    try
                    {
                        result = MEFHelper.GetExportedValue<IExMonitorServerConfigStore>("ExMonitorServerConfigStore");
                    }
                    catch { result = new ExMonitorServerConfigStore(); }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        public static IExMonitorServerConfigStore Store
        {
            get { return _singletonHelper.Current; }
        }
    }
}
