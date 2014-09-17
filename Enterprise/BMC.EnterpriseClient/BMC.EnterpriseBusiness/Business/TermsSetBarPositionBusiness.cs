using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class TermsSetBarPositionBusiness
    {
        private static TermsSetBarPositionBusiness _termsSetBarPositionBusiness;

        public TermsSetBarPositionBusiness() { }

        /// <summary>
        /// To create a new instance for TermsSetBarPositionBusiness and return the created instance
        /// </summary>
        /// <returns></returns>
        public static TermsSetBarPositionBusiness CreateInstance()
        {
            if (_termsSetBarPositionBusiness == null)
                _termsSetBarPositionBusiness = new TermsSetBarPositionBusiness();

            return _termsSetBarPositionBusiness;
        }

        public List<CompanyInfo> GetAllCompanyNames()
        {
            List<CompanyInfo> lstCompanyInfo = new List<CompanyInfo>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetAllCompanyNames())
                    {
                        lstCompanyInfo.Add(new CompanyInfo()
                        {
                            Company_ID = entity.Company_ID,
                            Company_Name = entity.Company_Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstCompanyInfo;
        }

        public List<SubCompanyInfo> GetAllSubCompanyNamesForCompanyID(int companyID)
        {
            List<SubCompanyInfo> lstSubCompanyInfo = new List<SubCompanyInfo>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetAllSubCompanyNamesForCompanyID(companyID))
                    {
                        lstSubCompanyInfo.Add(new SubCompanyInfo()
                        {
                            Sub_Company_ID = entity.Sub_Company_ID,
                            Sub_Company_Name = entity.Sub_Company_Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstSubCompanyInfo;
        }

        public List<SiteInfo> GetAllSiteNamesForSubCompanyID(int subCompanyID)
        {
            List<SiteInfo> lstSiteInfo = new List<SiteInfo>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetAllSiteNamesForSubCompanyID(subCompanyID))
                    {
                        lstSiteInfo.Add(new SiteInfo()
                        {
                            Site_ID = entity.Site_ID,
                            Site_Name = entity.Site_Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstSiteInfo;
        }

        public void UpdateTermsBarPosition(bool validationFlag, DateTime preDates, DateTime postDates, int siteID, int companyID, int subCompanyID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.UpdateBarPositionWithTermsInfo(validationFlag, preDates, postDates, siteID, companyID, subCompanyID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
