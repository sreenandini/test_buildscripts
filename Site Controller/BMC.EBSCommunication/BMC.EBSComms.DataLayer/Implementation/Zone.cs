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
        public DLZoneCollectionDto GetZones(string siteCode, int zoneID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetZones");
            DLZoneCollectionDto result = new DLZoneCollectionDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DbParameter[] parameters = db.CreateParameters(2);
                    parameters[0] = db.CreateParameter("@SiteCode", DbType.AnsiString, 50, siteCode);
                    parameters[1] = db.CreateParameter("@ZoneID", DBNull.Value);
                    if (zoneID > 0) parameters[1].Value = zoneID;
                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetZoneDetails]", parameters);
                    DataTable dt = ds.GetDataTable(0);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DLZoneDto dto = new DLZoneDto();
                            dto.ZoneID = dr.Field<int>("ZoneID");
                            dto.ZoneName = dr.Field<string>("ZoneName");
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
