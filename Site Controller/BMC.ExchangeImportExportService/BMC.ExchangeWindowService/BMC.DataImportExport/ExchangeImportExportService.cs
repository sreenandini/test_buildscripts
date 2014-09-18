using System.ServiceProcess;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System;
using BMC.BusinessClasses.BusinessLogic;
using BMCIPC;

namespace BMC.DataImportExport
{
    partial class ExchangeImportExportService : ServiceBase
    {
        readonly BusinessClasses.Interfaces.IBMCExchangeImportExport _bmcExchangeImportExport;
        private readonly BMC.BusinessClasses.Interfaces.ISTMExportDetails _stmExport;
        private BMC.BusinessClasses.Interfaces.IJob _vaultMessages;
        private FloorStatus _floorStatus;

        public ExchangeImportExportService()
        {            
            InitializeComponent();
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            _bmcExchangeImportExport = BusinessClasses.ObjectFactory.GetExchangeImportExportObject();
            _stmExport = new BMC.BusinessClasses.BusinessLogic.STMExportDetails();            
        }

        protected override void OnStart(string[] args)
        {
            LogManager.WriteLog("Starting Service...", LogManager.enumLogLevel.Info);

            string regulatoryType       =   string.Empty;
            bool isRegulatoryEnabled    =   false;

            regulatoryType              =   _bmcExchangeImportExport.GetSettingDetail("RegulatoryType").ToUpper();
            isRegulatoryEnabled         =   Convert.ToBoolean(_bmcExchangeImportExport.GetSettingDetail("IsRegulatoryEnabled"));

            LogManager.WriteLog(string.Format("{0} - {1}, {2} - {3}", "Regulatory Enabled", isRegulatoryEnabled.ToString().ToUpper(),
                "Regulatory Type", regulatoryType.ToUpper()), LogManager.enumLogLevel.Info);
            _stmExport.Start();
            if (isRegulatoryEnabled == true && regulatoryType == "AAMS")
            {
                LogManager.WriteLog("Checking Site Status...", LogManager.enumLogLevel.Info);

                if (_bmcExchangeImportExport.CheckSiteStatus())
                {
                    LogManager.WriteLog("Site is in enabled state.", LogManager.enumLogLevel.Info);
                    StartService();
                }
                else
                {
                    LogManager.WriteLog("Site is in disable state. Cannot start service.", LogManager.enumLogLevel.Info);
                    throw new Exception("Site is in disable state. Cannot start service.");
                }
            }
            else
            {
                StartService();
            }
			if(SiteLicensingExpiryChecker.Instance != null)
			{
           		SiteLicensingExpiryChecker.Instance.ValidateLicenseEnabled();
			}
        }
        
        protected override void OnStop()
        {
            LogManager.WriteLog("DialupService" + "---" + "on stop", LogManager.enumLogLevel.Info);
            _floorStatus.Stop();
            _stmExport.Stop();
            _vaultMessages.UnInit();
            _bmcExchangeImportExport.ResetExportHistory();
            _bmcExchangeImportExport.StopThread();            
			if(SiteLicensingExpiryChecker.Instance != null)
			{
            	SiteLicensingExpiryChecker.Instance.Stop();
			}
        }

        private void StartService()
        {
            LogManager.WriteLog("Inside StartService method", LogManager.enumLogLevel.Info);            
            System.Threading.Thread.Sleep(4000);
            LogManager.WriteLog("DialupService" + "---" + "Resetting the under process records.", LogManager.enumLogLevel.Info);
            _bmcExchangeImportExport.ResetExportHistory();
            LogManager.WriteLog("DialupService" + "---" + "Service started to pick records from EH.", LogManager.enumLogLevel.Info);
            _bmcExchangeImportExport.ExportDataToEnterprise();
            _bmcExchangeImportExport.ImportDataToExchange();
            _floorStatus = FloorStatus.GetInstance();
            _vaultMessages = new VaultMessageServiceHandle();
            _vaultMessages.Init();
            _vaultMessages.DoWork();
            LogManager.WriteLog("Service Started Successfully.", LogManager.enumLogLevel.Info);
        }
    }
}
