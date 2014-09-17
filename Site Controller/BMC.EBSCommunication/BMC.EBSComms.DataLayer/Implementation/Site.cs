using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Data;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.DataLayer.Dto;

namespace BMC.EBSComms.DataLayer
{
    public partial class DataInterfaceBase
    {
        public DLSiteCollectionDto GetSites(string siteCode)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSites");
            DLSiteCollectionDto result = new DLSiteCollectionDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DbParameter[] parameters = db.CreateParameters(1);
                    parameters[0] = db.CreateParameter("@SiteCode", DBNull.Value);
                    if (!siteCode.IsEmpty()) parameters[0].Value = siteCode;
                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetSiteDetails]", parameters);
                    DataTable dt = ds.GetDataTable(0);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DLSiteDto dto = new DLSiteDto();
                            dto.SiteId = dr.Field<string>("SiteId");
                            dto.SiteName = dr.Field<string>("SiteName");
                            dto.IsActive = dr.Field<bool>("IsActive");
                            result.Add(dto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
