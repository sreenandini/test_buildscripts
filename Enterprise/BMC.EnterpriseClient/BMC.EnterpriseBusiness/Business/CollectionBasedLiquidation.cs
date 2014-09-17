using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using System.Data;
using BMC.Common.ExceptionManagement;
namespace BMC.EnterpriseBusiness.Business
{
    public class CollectionBasedLiquidation
    {
        public DataTable GetBatchNoForLiquidation(int SiteId)
        {
            using (EnterpriseDataContext liquidationDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {

                try
                {
                    return liquidationDataAccess.GetBatchNoForLiquidation(SiteId);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
        }
        public DataTable GetSiteCodeForLiquidation(int userid,string Setting)
        {
            using (EnterpriseDataContext liquidationDataAccess = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    return liquidationDataAccess.GetSiteCodeForLiquidation(userid, Setting);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                    return null;
                }
            }
        }
        

    }
}
