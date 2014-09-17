using System.ServiceProcess;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;
using BMC.BusinessClasses.Interfaces;
using BMC.BusinessClasses;
namespace BMC.DataImportExport
{
    partial class EnterpriseImportExport : ServiceBase
    {
        private readonly IBMCEnterpriseExportImport _bmcEnterpriseExportImport;
        private readonly ISTMExportDetails _stmExport;

        private readonly IBMCUtilities _bmcUtility;
        public EnterpriseImportExport()
        {
            InitializeComponent();
            ConfigManager.SetConfigurationMode(ConfigManager.ConfigurationMode.AppConfig);
            _bmcEnterpriseExportImport = ObjectFactory.GetEnterpriseFactoryObject(true);
            _stmExport = new STMExportDetails();

            _bmcUtility = UtitlityObjectFactory.GetEnterpriseFactoryObject(true);
        }

        protected override void OnStart(string[] args)
        {
            _bmcEnterpriseExportImport.ResetImportHistory();
            _stmExport.Start();
            LogManager.WriteLog("Service Started... ", LogManager.enumLogLevel.Info);
            _bmcEnterpriseExportImport.ImportDataToEnterprise();


            _bmcUtility.ResetAlertProcessHistory();
            _bmcUtility.ImportAlertData();
        }

        protected override void OnStop()
        {
            _stmExport.Stop();
            //Quit processing 
           _bmcEnterpriseExportImport.Stop() ;  
        }
    }
}
