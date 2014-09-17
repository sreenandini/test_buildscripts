using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using Audit.Transport;

namespace BMC.EnterpriseBusiness.Business
{
    public class SiteAreaBiz
    {
        #region Local Declaration

        private static SiteAreaBiz _SiteArea;

        #endregion Local Declaration

        #region Instance Method
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>MaintenanceBiz Object</returns>
        public static SiteAreaBiz CreateInstance()
        {
            if (_SiteArea == null)
                _SiteArea = new SiteAreaBiz();

            return _SiteArea;
        }
        #endregion Instance Method

        public List<SubCompayRegions> GetSubCompayRegions(int iSiteid)
        {
            List<SubCompayRegions> lstRetSubCompayRegions = null;
            try
            {
                List<rsp_getSubCompayRegionsResult> lstSubCompayRegions;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstSubCompayRegions = DataContext.GetSubCompayRegions(iSiteid).ToList();
                }
                lstRetSubCompayRegions = (from Records in lstSubCompayRegions
                                          select new SubCompayRegions
                                              {
                                                  Sub_Company_Region_ID = Records.Sub_Company_Region_ID,
                                                  Sub_Company_Region_Name = Records.Sub_Company_Region_Name,
                                                  Staff_ID = Records.Staff_ID,
                                                  Staff_First_Name = Records.Staff_First_Name,
                                                  Staff_Last_Name = Records.Staff_Last_Name,                                                  
                                                  Sub_Company_Region_Description = Records.Sub_Company_Region_Description
                                                  
                                              }).ToList<SubCompayRegions>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetSubCompayRegions;
        }
    }
}
