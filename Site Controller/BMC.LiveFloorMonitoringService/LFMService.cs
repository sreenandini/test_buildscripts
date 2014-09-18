using System;
using System.Security.Authentication;
using System.ServiceProcess;
using BMC.Common.ConfigurationManagement;
using LFMEngine;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using LFMEngine.Helper;

namespace BMC.LiveFloorMonitoringService
{
    public partial class LfmService : ServiceBase
    {
        readonly SiteMessageProcessor _siteMessageProcessor = new SiteMessageProcessor();
        public LfmService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogManager.WriteLog("Start Service Initiated", LogManager.enumLogLevel.Info);

            DBHelper dbHelper           =   new DBHelper();

            string regulatoryType       =   string.Empty;
            bool isRegulatoryEnabled    =   false;

            regulatoryType              =   dbHelper.GetSettingFromDB("RegulatoryType").ToUpper();
            isRegulatoryEnabled         =   Convert.ToBoolean(dbHelper.GetSettingFromDB("IsRegulatoryEnabled"));

            LogManager.WriteLog(string.Format("{0} - {1}, {2} - {3}", "Regulatory Enabled", isRegulatoryEnabled.ToString().ToUpper(),
                "Regulatory Type", regulatoryType.ToUpper()), LogManager.enumLogLevel.Info);

            if (isRegulatoryEnabled == true && regulatoryType == "AAMS")
            {
                LogManager.WriteLog("Checking Site Status...", LogManager.enumLogLevel.Info);

                if (dbHelper.CheckSiteStatus())
                {
                    LogManager.WriteLog("Site is in enabled state.", LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("Site is in disable state. Cannot start service.", LogManager.enumLogLevel.Info);
                    throw new Exception("Site is in disable state. Cannot start service.");
                }
            }

            try
            {
                SiteMessageProcessor.EnterpriseUrl = RegistryConfigurationAdapter.Read("LFMWebService", "");
                LogManager.WriteLog("Enterprise URL : " + SiteMessageProcessor.EnterpriseUrl, LogManager.enumLogLevel.Info);

                if (string.IsNullOrEmpty(SiteMessageProcessor.EnterpriseUrl))
                    throw new InvalidCredentialException("Web Service is Null or Empty");
                    _siteMessageProcessor.StartEngine();
            }
            catch (Exception exception)
            {
                ExceptionManager.Publish(exception);
                throw exception;
            }
            LogManager.WriteLog("Service Started", LogManager.enumLogLevel.Info);
        }

        protected override void OnStop()
        {
            _siteMessageProcessor.StopEngine();
        }
    }
}
