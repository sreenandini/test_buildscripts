using BMC.CoreLib;
using BMC.CoreLib.Data;
using BMC.CoreLib.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BMC.EBSComms.DataLayer
{
    public partial class DataInterfaceBase
    {
        public bool UpdateRecordStatus(int ID, int Status)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateRecordStatus");
            bool result = false;
            
            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();
                    DbParameter[] parameters = db.CreateParameters(3);
                    parameters[0] = db.CreateParameter("@EH_ID", ID);
                    parameters[1] = db.CreateParameter("@EH_Status", Status);
                    parameters[2] = db.CreateRetValueParameter(DbType.Int32);
                    result = db.ExecuteNonQueryAndReturnIntOK ("[dbo].[usp_EBS_UpdateExportHistory]", parameters);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            return result;
        }
        public DataTable GetUnprocessedRecords()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetUnprocessedRecords");
            DataTable result = null;

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();

                    DataSet ds = db.ExecuteDataset("[dbo].[rsp_EBS_GetUnprocessedRecords]", null);
                    result = ds.GetDataTable(0);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public bool UpdateMessageHistory(bool logMessage, int fromSystem, int toSystem, string siteCode, DateTime dateTime,
                                            int refID, string request, string response)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateMessageHistory");
            bool result = default(bool);
            if (!logMessage) return true;

            try
            {
                
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();
                    DbParameter[] parameters = db.CreateParameters(8);
                    parameters[0] = db.CreateParameter("@EBS_FromSystem", fromSystem);
                    parameters[1] = db.CreateParameter("@EBS_ToSystem", toSystem);
                    parameters[2] = db.CreateParameter("@EBS_SiteCode", DbType.AnsiString, 50, siteCode);
                    parameters[3] = db.CreateParameter("@EBS_DateTime", dateTime);
                    parameters[4] = db.CreateParameter("@EBS_RefID", refID);
                    parameters[5] = db.CreateParameter("@EBS_Request", request);
                    parameters[6] = db.CreateParameter("@EBS_Response", response);

                    parameters[7] = db.CreateRetValueParameter(DbType.Int32);
                    result = db.ExecuteNonQueryAndReturnIntOK("[dbo].[usp_EBS_InsertMessageHistory]", parameters);
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
