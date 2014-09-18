using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;
using BMC.Transport;
namespace BMC.CashDeskOperator.BusinessObjects
{
   public class Audit :IAuditDetails
    {
       AuditBusiness oAuditBusiness = new AuditBusiness();
        #region IAuditDetails Members

       public IEnumerable<FillModules> GetModulesList()
        {
            return oAuditBusiness.GetModulesList().OrderBy(mod => mod.Audit_Module_Name);
          
        }

        public List<GetAuditDetailsResult> GetAuditDetails(DateTime fromDate, DateTime ToDate, string ModuleID,int Rows)
        {
            return oAuditBusiness.GetAuditDetails(fromDate, ToDate, ModuleID, Rows);
        }

        public List<GetAFTAuditDetailsResult> GetAFTAuditData(DateTime startDate, DateTime endDate, int Rows)
        {
            return oAuditBusiness.GetAFTAuditData(startDate, endDate, Rows).ToList();
        }

        #endregion
    }
}
