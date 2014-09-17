using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.LogManagement;
using BMC.EnterpriseReportsBusiness;

namespace BMC.EnterpriseReportsUI
{
    public partial class CrystalReports
    {
        ReportsBusiness oReportsBusiness; 

        public CrystalReports()
        {
            oReportsBusiness = new ReportsBusiness();
        }

        
    }
}
