namespace BMC.BusinessClasses.Interfaces
{
    public interface IBMCExchangeImportExport
    {
        bool ExportDataToEnterprise();
        bool ImportDataToExchange();
        void ExportInstallationDataToEnterprise();
        bool ResetExportHistory();
        void StopThread();
        bool CheckSiteStatus();
        string GetSettingDetail(string strSetting);
    } 
}
