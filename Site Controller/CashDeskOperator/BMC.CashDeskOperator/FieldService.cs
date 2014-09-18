using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
//using BMC.Business.CashDeskOperator.WebServices;
using BMC.Transport.CashDeskOperatorEntity;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class FieldServiceBusinessObject : IFieldService
    {

        #region Private Variables

        FieldService fieldService = new FieldService();

        #endregion

        #region Constructor
        
        private FieldServiceBusinessObject() { }
        
        #endregion

        #region Public Functions

        public DataTable GetCurrentServiceCalls(string SiteCode, string StartingBarPos, string LastBarPos)
        {
            return fieldService.GetCurrentServiceCalls(SiteCode, StartingBarPos, LastBarPos);
        }

        [Obsolete]
        public string GetCurrentSiteCode()
        {
            return fieldService.GetCurrentSiteCode();
        }

        [Obsolete]
        public string GetCurrentBarPositionNames()
        {
            return fieldService.GetCurrentBarPositionNames();
        }

        public DataTable GetPositionList()
        {
            return fieldService.GetPositionList();
        }

        public DataTable GetCashdeskServiceFaults()
        {
            return fieldService.GetCashdeskServiceFaults();
        }

        public string LogSiteEvent(int InstallationNumber, int FaultType)
        {
            return fieldService.LogSiteEvent(InstallationNumber, FaultType);
        }

        public DataTable GetOpenServiceCalls(string site_Code, string bar_Pos)
        {
            return fieldService.GetOpenServiceCalls(site_Code, bar_Pos);
        }

        public DataTable GetServiceNotes(string JobID)
        {
            return fieldService.GetServiceNotes(JobID);
        }

        public DataTable GetRemedies()
        {
            return fieldService.GetRemedies();
        }

        public int EscalateServiceCall(string JobID, int UserID)
        {
            return fieldService.EscalateServiceCall(JobID, UserID);
        }

        public int InsertServiceNotes(string JobID, string Notes, string User)
        {
            return fieldService.InsertServiceNotes(JobID, Notes, User);
        }

        public int CloseServiceCall(int ServiceID, string JobID, int Remedy, int User, string Notes)
        {
            return fieldService.CloseServiceCall(ServiceID, JobID, Remedy, User, Notes);
        }


        #endregion

        #region Public Static Function
        public static IFieldService CreateInstance()
        {
            return new FieldServiceBusinessObject();
        }
        #endregion

    }
}
