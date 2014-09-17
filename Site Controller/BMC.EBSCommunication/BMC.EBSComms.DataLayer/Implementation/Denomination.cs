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
        public DLDenominationCollectionDto GetDenominations(string siteCode, object denominationId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetDenominations");
            DLDenominationCollectionDto result = new DLDenominationCollectionDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DbParameter[] parameters = db.CreateParameters(2);
                    parameters[0] = db.CreateParameter("@SiteCode", DbType.AnsiString, 50, siteCode);
                    parameters[1] = db.CreateParameter("@Denomination_Id", denominationId);
                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetDenominationDetails]", parameters);
                    DataTable dt = ds.GetDataTable(0);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DLDenominationDto dto = new DLDenominationDto();
                            dto.DenominationId = dr.Field<string>("DenominationId");
                            dto.DenominationName = dr.Field<string>("DenominationName");
                            dto.DenominationValue = dr.Field<string>("DenominationValue");
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
