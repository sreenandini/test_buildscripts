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
        public DLMachineCollectionDto GetMachines(string siteCode, object machineID)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetMachines");
            DLMachineCollectionDto result = new DLMachineCollectionDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DbParameter[] parameters = db.CreateParameters(2);
                    parameters[0] = db.CreateParameter("@SiteCode", DbType.AnsiString, 50, siteCode);
                    parameters[1] = db.CreateParameter("@MachineId", machineID);
                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetMachines]", parameters);
                    DataTable dt = ds.GetDataTable(0);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DLMachineDto dto = new DLMachineDto();
                            dto.MachineID = dr.Field<string>("MachineID");
                            dto.Area = dr.Field<string>("Area");
                            dto.MachineType = dr.Field<string>("MachineType");
                            dto.Bank = dr.Field<string>("BANK");
                            dto.GameName = dr.Field<string>("GameName");
                            dto.DenominationID = dr.Field<string>("DenominationID");
                            dto.ManufacturerName = dr.Field<string>("ManufacturerId");

                            dto.MachineLoc = dr.Field<string>("MachineLoc");

                            
                            dto.CasinoID = dr.Field<string>("CasinoID");
                            dto.ZoneID = dr.Field<string>("ZoneID");
                            
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
