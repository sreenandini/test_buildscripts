using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using System;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        #region Constants
        private const string CONST_GETSITEDETAILS_PROC = "rsp_GetSiteDetails";
        private const string CONST_GETPROFILEDETAILS_PROC = "rsp_GetProfileDetails";
        private const string CONST_GETSETTINGSDETAILS_PROC = "rsp_GetSettingsDetails";
        private const string CONST_UPDATESETTING_PROC = "usp_UpdateSetting";
        private const string CONST_INSERTSETTING_PROC = "usp_InsertSetting";
        private const string CONST_INSERTPROFILESETTING_PROC = "usp_InsertProfileSetting";
        private const string CONST_GETPROFILENAMEFORSITE_PROC = "rsp_GetProfileNameForSite";
        private const string CONST_UPDATEPROFILEFORSITE_PROC = "usp_UpdateProfileForSite";
        private const string CONST_EXPORTHISTORY_PROC = "usp_export_history";
        private const string CONST_SITESFORPROFILE_PROC = "rsp_GetSitesForProfile";
        #endregion Constants

        #region Public Methods
        /// <summary>
        /// Method to get the Sites List.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSiteList()
        {
            DataSet objDS = new DataSet();
            DataTable objSites = new DataTable();
            try
            {

                objDS = SqlHelper.ExecuteDataset(EnterpriseDataContextHelper.ConnectionString, CONST_GETSITEDETAILS_PROC);
                objSites = objDS.Tables[0];

                foreach (DataRow dr in objSites.Rows)
                {
                    dr["Site_Name"] += " [ " + dr["SettingsProfile_Description"].ToString() + " ]";
                }
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        public DataTable GetSiteList(string SiteName, int SiteStatusID)
        {
            DataSet objDS = new DataSet();
            SqlParameter[] Params = new SqlParameter[2];
            try
            {

                Params[0] = new SqlParameter("@Site_Name", SiteName);
                Params[1] = new SqlParameter("@Site_Status", SiteStatusID);
                objDS = SqlHelper.ExecuteDataset(EnterpriseDataContextHelper.ConnectionString, CONST_GETSITEDETAILS_PROC, Params);
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        /// <summary>
        /// Method to get the Profiles List.
        /// </summary>
        /// <returns></returns>
        public DataTable GetProfileList()
        {
            DataSet objDS = new DataSet();

            try
            {
                objDS = SqlHelper.ExecuteDataset(EnterpriseDataContextHelper.ConnectionString, CONST_GETPROFILEDETAILS_PROC);
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        /// <summary>
        /// Method to get the Settings List.
        /// </summary>
        /// <returns></returns>
        public DataTable GetSettingsList(string strProfileName, bool GetAllSettings)
        {
            DataSet objDS = new DataSet();

            try
            {
                SqlParameter[] objParam = new SqlParameter[4];
                objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                objParam[1] = new SqlParameter("@GetAllSettings", GetAllSettings);
                objParam[2] = new SqlParameter("@GETAFTSettings", "false");
                objParam[3] = new SqlParameter("@SiteID", 0);

                objDS = SqlHelper.ExecuteDataset(EnterpriseDataContextHelper.ConnectionString, CONST_GETSETTINGSDETAILS_PROC, objParam);
                return objDS.Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
            finally
            {
                objDS.Dispose();
            }
        }

        /// <summary>
        /// Method to update the Individual Settings.
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strProfileName"></param>
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateSetting")]
        public int UpdateSetting([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingsProfile_Description", DbType = "VarChar(200)")] string settingsProfile_Description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingName", DbType = "VarChar(200)")] string settingName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingValue", DbType = "VarChar(200)")] string settingValue)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), settingsProfile_Description, settingName, settingValue);
            return ((int)(result.ReturnValue));
        }
        
        /// <summary>
        /// Method to insert the Individual Settings.
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strProfileName"></param>
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertSetting")]
        public int InsertSetting([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingsProfile_Description", DbType = "VarChar(200)")] string settingsProfile_Description, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingName", DbType = "VarChar(200)")] string settingName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingValue", DbType = "VarChar(200)")] string settingValue)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), settingsProfile_Description, settingName, settingValue);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_InsertProfileSetting")]
        public int InsertProfile([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingsProfile_Description", DbType = "VarChar(200)")] string settingsProfile_Description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), settingsProfile_Description);
            return ((int)(result.ReturnValue));
        }
        
        public void InsertAllSetting(string strProfileName, bool GetAllSettings)
        {
            DataTable dtSettings = new DataTable();
            SqlParameter[] objParam = new SqlParameter[3];
            dtSettings = GetSettingsList("Default Profile", GetAllSettings);
            try
            {
                foreach (DataRow dr in dtSettings.Rows)
                {
                    objParam = new SqlParameter[3];
                    objParam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
                    objParam[1] = new SqlParameter("@SettingName", dr["Name"].ToString());
                    objParam[2] = new SqlParameter("@SettingValue", dr["Value"].ToString());

                    SqlHelper.ExecuteNonQuery(EnterpriseDataContextHelper.ConnectionString, CONST_INSERTSETTING_PROC, objParam);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            finally
            {
                dtSettings.Dispose();
            }
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetProfileNameForSite")]
        public ISingleResult<rsp_GetProfileNameForSiteResult> GetProfileNameForSite([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Site_ID", DbType = "VarChar(10)")] string site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_GetProfileNameForSiteResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateProfileForSite")]
        public int UpdateProfileForSite([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Site_ID", DbType = "VarChar(10)")] string site_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SettingsProfile_Description", DbType = "VarChar(200)")] string settingsProfile_Description)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, settingsProfile_Description);
            return ((int)(result.ReturnValue));
        }

        public DataTable GetSitesForProfile(string strProfileName)
        {
            SqlParameter[] objparam = new SqlParameter[1];
            objparam[0] = new SqlParameter("@SettingsProfile_Description", strProfileName);
            try
            {
                return SqlHelper.ExecuteDataset(EnterpriseDataContextHelper.ConnectionString, CONST_SITESFORPROFILE_PROC, objparam).Tables[0];
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                return new DataTable();
            }
        }
        #endregion Public Methods
    }

    public partial class rsp_GetProfileNameForSiteResult
    {

        private string _ProfileName;

        public rsp_GetProfileNameForSiteResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProfileName", DbType = "VarChar(200)")]
        public string ProfileName
        {
            get
            {
                return this._ProfileName;
            }
            set
            {
                if ((this._ProfileName != value))
                {
                    this._ProfileName = value;
                }
            }
        }
    }
}
