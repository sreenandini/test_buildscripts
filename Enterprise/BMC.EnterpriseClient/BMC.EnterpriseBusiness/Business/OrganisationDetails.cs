using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using System.Data.Linq;
using BMC.EnterpriseBusiness.Entities;

namespace BMC.EnterpriseBusiness.Business
{
    public class OrganisationDetails
    {
        public List<OrganisationDetailsEntity> GetOrganisationInfo(int UserId)
        {
            
            List<rsp_GetOrganisationInfoResult>  DetailList = null;
            using (EnterpriseDataContext DataContext =EnterpriseDataContextHelper.GetDataContext())
            {
                DetailList = DataContext.GetOrganisationInfo(UserId).ToList();
            }
            return DetailList.Select(X => new OrganisationDetailsEntity 
            {Company_ID = X.Company_ID,
            Company_Name = X.Company_Name,
            Sub_Company_ID = X.Sub_Company_ID,
            Sub_Company_Name = X.Sub_Company_Name,
            Site_ID = X.Site_ID,
            Site_Name = X.Site_Name,
            Site_Code = X.Site_Code,
            SiteStatus = X.SiteStatus
            }).ToList();
            
        }
    }
}
