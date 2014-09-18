using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Business.CashDeskOperator;


namespace BMC.CashDeskOperator.BusinessObjects
{
    public class AnalysisBusinessObject:IAnalysis
    {

        #region Private Variables

        Analysis analysis = new Analysis();

        #endregion

        #region Constructor

        private AnalysisBusinessObject() { }
        
        #endregion
        
        #region "Public Function"

        public DataTable GetAnalysisDetails(int Type, DateTime StartDate, DateTime EndDate)
        {
            return analysis.GetAnalysisDetails(Type, StartDate, EndDate);
        }

        public DataTable GetAnalysisDetails(int Type, DateTime StartDate, DateTime EndDate, AnalysisView viewType, int zoneId)
        {
            return analysis.GetAnalysisDetails(Type, StartDate, EndDate, viewType, zoneId);
        }

        public DataTable GetWeeklyCollectionSummary()
        {
            return analysis.GetWeekCollectionSummary();
        }

        public DataTable GetWeeklyCollectionDetails(int iWeekID)
        {
            return analysis.GetWeekCollectionDetails(iWeekID);
        }

        public DataTable GetWTDMTD()
        {
            return analysis.GetWTDMTD();
        }

        public DataTable GetBarPositionDetails(string SortBy, int InstallNo)
        {
            return analysis.GetBarPositionDetails(SortBy, InstallNo);
        }

        public void SaveFloorPosition(int slotID, int securityUserID, int topPosition, int leftPosition)
        {
            analysis.SaveFloorPosition(slotID, securityUserID, topPosition, leftPosition);
        }

        public DataTable GetSpotCheckDataSAS(int InstallationNo, int SelectDay, DateTime date)
        {
            return analysis.GetSpotCheckDataSAS(InstallationNo, SelectDay, date);
        }

        public DataTable GetTicketsClaimedByCDForPeriod(int SelectDay, DateTime IsDate, int MachineNo)
        {
            return analysis.GetTicketsClaimedByCDForPeriod(SelectDay, IsDate, MachineNo);
        }

        public DataTable GetInstallations()
        {
            return analysis.GetInstallations();
        }

        public DataTable GetInstallationsForReports(string reportType)
        {
            return analysis.GetInstallationsForReports(reportType);
        }        

        public DataTable GetEPIDetails(int InstallationNo)
        {
            return analysis.GetEPIDetails(InstallationNo);
        }

        public DataTable GetTicketException(int InstallationNo)
        {
            return analysis.GetTicketException(InstallationNo);
        }

        public DataTable GetHandpaystoClear(int InstallationNo)
        {
            return analysis.GetHandpaystoClear(InstallationNo);
        }

        public void PrintFunction(System.Windows.Controls.ListView lvView, string ReportName,DateTime ReportStartdate)
        {
            analysis.PrintFunction(lvView, ReportName, ReportStartdate);
        }
        #endregion

        #region "Public Static Function"

        public static IAnalysis CreateInstance()
        {
            return new AnalysisBusinessObject();
        }

        #endregion

    }
}
