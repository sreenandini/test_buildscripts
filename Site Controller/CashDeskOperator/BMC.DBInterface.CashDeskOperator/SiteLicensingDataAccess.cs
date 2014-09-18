using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using BMC.Transport;
using BMC.DataAccess;

namespace BMC.DBInterface.CashDeskOperator
{
    public class SiteLicensingDataAccess
    {
        private static SiteLicensingDataContext siteLicensingDataContext = new SiteLicensingDataContext(CommonDataAccess.ExchangeConnectionString);

        #region Methods

        /// <summary>
        /// To get license details
        /// </summary>
        /// <returns></returns>
        public static ISingleResult<rsp_SL_GetSiteLicenseDetailsResult> GetSiteLicenseDetailsResult()
        {
            return siteLicensingDataContext.rsp_SL_GetSiteLicenseDetails();
        }

        /// <summary>
        /// To update license key staus
        /// </summary>
        /// <param name="iLicenseInfoId"></param>
        /// <param name="iLicenseKeyStatus"></param>
        /// <returns></returns>
        public static void UpdateLicenseStaus(string sLicenseKey, Int32 iLicenseKeyStatus, Int32 iUserNo)
        {
            siteLicensingDataContext.usp_SL_UpdateLicenseStatus(sLicenseKey, iLicenseKeyStatus, iUserNo);
        }

        public static bool IsSiteLincenseEnabled()
        {
            return Convert.ToBoolean(siteLicensingDataContext.IsSiteLincenseEnabled());
        }

        public int CheckLicenseKey(string licenseKey,  string userName)
        {
            System.Nullable<int> result = -10;
            return siteLicensingDataContext.CheckLicenseKey(licenseKey, userName, ref result);

        }

        public int UpdateSlotStatus(bool siteLicensing_DisableGames)
        {
            return siteLicensingDataContext.UpdateSlotStatus(siteLicensing_DisableGames);
        }

        public int GetSetting(int? setting_ID, string setting_Name, string setting_Default, ref string setting_Value)
        {
            return siteLicensingDataContext.GetSetting(setting_ID, setting_Name, setting_Default, ref setting_Value);
        }

        #endregion //Methods
    }

    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Exchange")]
    public partial class SiteLicensingDataContext : System.Data.Linq.DataContext
    {
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public SiteLicensingDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
		}

        [Function(Name = "dbo.rsp_SL_GetSiteLicenseDetails")]
        public ISingleResult<rsp_SL_GetSiteLicenseDetailsResult> rsp_SL_GetSiteLicenseDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_SL_GetSiteLicenseDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_SL_UpdateLicenseStatus")]
        public int usp_SL_UpdateLicenseStatus([Parameter(Name = "LicenseKey", DbType = "VarChar(100)")] string licenseKey, [Parameter(Name = "KeyStatus", DbType = "Int")] System.Nullable<int> keyStatus, [Parameter(Name = "UserNo", DbType = "Int")] System.Nullable<int> userNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), licenseKey, keyStatus, userNo);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.IsSiteLincenseEnabled", IsComposable = true)]
        public System.Nullable<bool> IsSiteLincenseEnabled()
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod()))).ReturnValue));
        }

        [Function(Name = "dbo.rsp_SL_CheckLicenseKey")]
        public int CheckLicenseKey([Parameter(Name = "LicenseKey", DbType = "VarChar(100)")] string licenseKey, [Parameter(Name = "UserName", DbType = "VarChar(50)")] string userName, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), licenseKey, userName, result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(2)));
            return Convert.ToInt32(result);
        }

        [Function(Name = "dbo.usp_SL_EnableorDisableSlot")]
        public int UpdateSlotStatus([Parameter(Name = "SiteLicensing_DisableGames", DbType = "Bit")] System.Nullable<bool> siteLicensing_DisableGames)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteLicensing_DisableGames);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        public int GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(100)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(100)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }
    }
}
