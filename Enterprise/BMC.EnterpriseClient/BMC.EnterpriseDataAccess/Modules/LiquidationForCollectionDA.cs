using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using System.Data;
using BMC.DataAccess;
using BMC.EnterpriseDataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        public DataTable GetBatchNoForLiquidation(int SiteId)
        {
            DataSet dsBatchNos = new DataSet();
            try
            {
                SqlParameter[] oParams = new SqlParameter[1];
                oParams[0] = new SqlParameter("SiteCode", SiteId);
                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(),CommandType.StoredProcedure,"rsp_GetBatchNoForLiquidation",dsBatchNos, new string[] {"BatchList"},oParams);
              
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            if (dsBatchNos.Tables.Count > 0)
                return dsBatchNos.Tables[0];
            else
                return null;
        }
        public DataTable GetSiteCodeForLiquidation(int UserId, string setting)
        {
            DataSet dsSiteCode = new DataSet();
            try
            {
                SqlParameter[] oParams = new SqlParameter[2];
                oParams[0] = new SqlParameter("UserID", UserId);
                oParams[1] = new SqlParameter("SettingName", setting);

                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetActiveSiteDetailsForCollectionLiquidation", dsSiteCode, new string[] { "SiteCodeList" }, oParams);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            if (dsSiteCode.Tables.Count > 0)
                return dsSiteCode.Tables[0];
            else
                return null;
        }

    }
}
