using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Utilities;
using System.Data.Linq;
using System.Reflection;
using BMC.CoreLib;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
        : DisposableObject
    {
        public string GetSettingValue(string settingName, string defaultValue)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "GetSettingValue"))
            {
                string result = default(string);

                try
                {
                    using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                    {
                        DataContext.rsp_GetSetting(null, settingName, defaultValue, ref result);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return result;
            }
        }

        public bool GetSettingValueBool(string settingName, bool defaultValue)
        {
            return TypeSystem.GetValueBoolSimple(this.GetSettingValue(settingName, defaultValue.ToString()));
        }

        public InstallationDetailsForComms GetInstallationDetailsForComms(int? installationNo)
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    return DataContext.GetInstallationDetailsForComms(installationNo).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                return null;
            }
        }

        public string GetLocalSiteCode()
        {
            string siteCode = string.Empty;
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.rsp_GetSiteCode(ref siteCode);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return siteCode;
        }

        public int GetInstallationNoByBarPosition(string bar_Position_Name)
        {
            int? installationNo = null;
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.rsp_GetInstallationNoByBarPosition(bar_Position_Name, ref installationNo);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            return installationNo.GetValueOrDefault();
        }
    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSetting")]
        public int rsp_GetSetting([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Setting_Name", DbType = "VarChar(1000)")] string setting_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Setting_Default", DbType = "VarChar(1000)")] string setting_Default, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Setting_Value", DbType = "VarChar(1000)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetInstallationDetailsForComms")]
        public ISingleResult<InstallationDetailsForComms> GetInstallationDetailsForComms([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Datapak_Serial", DbType = "Int")] System.Nullable<int> datapak_Serial)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), datapak_Serial);
            return ((ISingleResult<InstallationDetailsForComms>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetSiteCode")]
        public int rsp_GetSiteCode([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteCode", DbType = "VarChar(50)")] ref string siteCode)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode);
            siteCode = ((string)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetInstallationNoByBarPosition")]
        public int rsp_GetInstallationNoByBarPosition([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Bar_Position_Name", DbType = "VarChar(50)")] string bar_Position_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] ref System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bar_Position_Name, installation_No);
            installation_No = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_GetEPIDetails")]
        public ISingleResult<EPIDetailsResult> GetEPIDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No);
            return ((ISingleResult<EPIDetailsResult>)(result.ReturnValue));
        }
    }

    public partial class InstallationDetailsForComms
    {

        private string _Stock_No;

        private int _Installation_No;

        public InstallationDetailsForComms()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }
    }

    public partial class EPIDetailsResult
    {

        private string _EPIDetails;

        public EPIDetailsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EPIDetails", DbType = "VarChar(50)")]
        public string EPIDetails
        {
            get
            {
                return this._EPIDetails;
            }
            set
            {
                if ((this._EPIDetails != value))
                {
                    this._EPIDetails = value;
                }
            }
        }
    }
}
