using System.ServiceProcess;
using BMC.Common;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.DataExportToSite.Interfaces;
using System;
using BMC.Common.ExceptionManagement;
using BMC.DataExportToSite.BusinessLogic;

namespace BMC.DataExportToSite
{
    public partial class EnterpriseExportService : ServiceBase
    {
        readonly System.Timers.Timer _timer = new System.Timers.Timer();
        public static bool bCompVerProcessing = false;

        private readonly IBMCEnterpriseExport _bmcEnterpriseExportImport;
        private readonly BMCEnterpriseDropScheduler _bmcEnterpriseDropScheduler;
        private readonly SiteLicensingExpiryChecker _SiteLicensingExpiryChecker;

        public EnterpriseExportService()
        {
            InitializeComponent();

            try
            {
                ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
                _bmcEnterpriseExportImport = ObjectFactory.GetEnterpriseFactoryObject(true);
                _bmcEnterpriseDropScheduler = new BMCEnterpriseDropScheduler();
                _SiteLicensingExpiryChecker = new SiteLicensingExpiryChecker();
            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                LogManager.WriteLog("Enterprise Export service started...", LogManager.enumLogLevel.Info);

                BusinessLogic.BMCEnterpriseExport.IsRunning = true;
                _bmcEnterpriseExportImport.ResetExportHistory();
                _bmcEnterpriseExportImport.ExportDataToExchange();
                _bmcEnterpriseDropScheduler.Start();
                _SiteLicensingExpiryChecker.Start();
                //_timer.Elapsed += TimerElapsed;
                //_timer.Interval = int.Parse(ConfigManager.Read(Constants.CONSTANT_ENTERPRISEIMPORTEXPORT_TIMERINTERVAL)) * 1000;
                //_timer.Enabled = true;

            }
            catch (Exception ex)
            {                
                ExceptionManager.Publish(ex);
            }
        }

        protected override void OnStop()
        {
            //_timer.Enabled = false;
            _bmcEnterpriseDropScheduler.Stop();
            _bmcEnterpriseExportImport.Stop();
            _SiteLicensingExpiryChecker.Stop();
        }
    }
}
