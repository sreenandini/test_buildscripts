namespace BMC.DataExportToSite.Interfaces
{
    public interface IBMCEnterpriseExport
    {
        void ExportDataToExchange();
        bool ResetExportHistory();
        void VerifyVLTComponents();
        void Stop();
    }
}
