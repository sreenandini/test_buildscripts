using BMC.CoreLib;
using BMC.CoreLib.Data;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.DataLayer.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace BMC.EBSComms.DataLayer
{
    public partial class DataInterfaceBase
    {
        public DLSettingDto GetSettings()
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSettings");
            DLSettingDto result = new DLSettingDto();

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    DataRow dr = db.ExecuteDataset(CommandType.StoredProcedure, "dbo.rsp_EBS_GetInitialSettings").GetDataRow(0, 0);
                    result.IsEnabled = TypeSystem.GetValueBool(dr["IsEBSEnabled"].ToString());
                    result.SendDataToEBS = TypeSystem.GetValueBool(dr["SendDataToEBS"].ToString());
                    result.EBSEndPointURL = dr["EBSEndPointURL"].ToString();
                    result.EBSVersion = dr["EBSVersion"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        public T GetSettingValue<T>(string settingName, T defaultValue)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "GetSettingValue");
            object result = defaultValue;

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    DbParameter[] parameters = db.CreateParameters(1);
                    parameters[0] = db.CreateParameter("@SettingName", settingName);
                    DataRow dr = db.ExecuteDataset("dbo.rsp_EBS_GetSettingValue", parameters).GetDataRow(0, 0);
                    if (dr != null)
                    {
                        string settingValue = dr.Field<string>("Setting_Value");

                        if (typeof(T) == typeof(int))
                            result = TypeSystem.GetValueInt(settingValue);
                        else if (typeof(T) == typeof(long))
                            result = TypeSystem.GetValueInt64(settingValue);
                        else if (typeof(T) == typeof(bool))
                            result = TypeSystem.GetValueBoolSimple(settingValue);
                        else
                            result = settingValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return (T)result;
        }

        public bool UpdateSettingValue(string settingName, string settingValue)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "UpdateSettingValue");
            bool result = false;

            try
            {
                using (Database db = DbFactory.OpenDB(_connectionString))
                {
                    db.Open();
                    DbParameter[] parameters = db.CreateParameters(3);
                    parameters[0] = db.CreateParameter("@Setting_Name", DbType.String, 100, settingName);
                    parameters[1] = db.CreateParameter("@Setting_Value", DbType.String, 8000, settingValue);
                    parameters[2] = db.CreateRetValueParameter(DbType.Int32);
                    result = db.ExecuteNonQueryAndReturnIntOK("[dbo].[usp_EBS_InsertOrUpdateSetting]", parameters);
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
