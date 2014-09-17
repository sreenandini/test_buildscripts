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
        public DLGameCollectionDto GetGames(string siteCode, object gameId)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetGames");
            DLGameCollectionDto result = new DLGameCollectionDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DbParameter[] parameters = db.CreateParameters(2);
                    parameters[0] = db.CreateParameter("@SiteCode", DbType.AnsiString, 50, siteCode);
                    parameters[1] = db.CreateParameter("@GamePrefix", gameId);
                   // if (string.IsNullOrWhiteSpace(gameId)) parameters[0].Value = gameId.Trim()[0];
                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetGameDetails]", parameters);
                    DataTable dt = ds.GetDataTable(0);
                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DLGameDto dto = new DLGameDto();
                            dto.GameID = dr.Field<string>("GameID").ToStringSafe();
                            dto.GameName = dr.Field<string>("GameName");
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
