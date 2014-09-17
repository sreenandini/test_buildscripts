namespace BMC.BusinessClasses.Interfaces
{
    public interface IBMCEnterpriseExportImport
    {
        bool ImportDataToEnterprise();
        bool ResetImportHistory();
        bool ResetExportHistory();
        void Stop();
    }
}   