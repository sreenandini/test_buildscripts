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
        public DLManufacturerCollectionDto GetManufacturers(string siteCode, int manufacturerId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetManufacturers");
            DLManufacturerCollectionDto result = new DLManufacturerCollectionDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DbParameter[] parameters = db.CreateParameters(2);
                    parameters[0] = db.CreateParameter("@SiteCode", DbType.AnsiString, 50, siteCode);
                    parameters[1] = db.CreateParameter("@Manufacturer_Id", DBNull.Value);
                    if (manufacturerId > 0) parameters[1].Value = manufacturerId;
                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetManufacturerDetails]", parameters);
                    DataTable dt = ds.GetDataTable(0);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DLManufacturerDto dto = new DLManufacturerDto();
                            dto.ManufacturerId = dr.Field<int>("ManufacturerId");
                            dto.ManufacturerName = dr.Field<string>("ManufacturerName");
                            dto.ManufacturerValue = dr.Field<string>("ManufacturerValue");
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
