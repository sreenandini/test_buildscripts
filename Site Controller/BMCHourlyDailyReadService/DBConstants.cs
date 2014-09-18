using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.HourlyDailyReadJobs
{
    class DBConstants
    {
      
        public const string CONSTANT_RSP_GETINITIALSETTINGS = "rsp_GetInitialSettings";
        public const string CONST_SP_GET_INSTALLTION_DETAILS_PROC = "rsp_GetInstallationDetails";
        public const string CONST_SP_USP_HOURLY_VTP = "usp_Hourly_VTP";
        public const string CONST_SP_ESP_COLLATEHSGAMINGDAY = "esp_Collate_HS_GamingDay";

        #region Fund and Fill SPs

        public const string CONST_SP_USP_DAILY_READ = "usp_Daily_READ";
        public const string CONST_SP_USP_VAULT_HOURLYFILL = "usp_Vault_HourlyFill";
        public const string CONST_SP_USP_HOURLY_FUNDS = "usp_Hourly_Funds";
        public const string CONST_SP_USP_VAULT_GETBALANCE = "usp_Vault_GetBalance";
        public const string CONST_SP_RSP_GETVAULTID = "rsp_GetVaultId";
        public const string CONST_SP_RSP_EXPORTVAULTCURRENTBALANCE = "rsp_ExportVaultCurrentBalance";

        #endregion //Fund and Fill Sps

        public const string CONST_PARAM_DATAPAK_SERIAL = "@Datapak_Serial";
        public const string CONST_PARAM_INSTALLATION_NUMBER = "@Installation_No";
        public const string CONST_PARAM_INCLUDE_VIRTUAL = "@IncludeVirtual";
        public const string CONST_PARAM_SORT_BY_ZONE = "@SortByZone";

        public const string CONST_PARAM_THEINSTALLATION = "@TheInstallation";
        public const string CONST_PARAM_THEDATETIME = "@TheDateTime";
        public const string CONST_PARAM_THEHOUR = "@TheHour";
        public const string CONST_PARAM_ISREAD = "@IsRead";
        public const string CONST_PARAM_RETURNVALUE = "@RETURN_VALUE";
        public const string CONST_SP_USP_MGMDUPDATE_JOB = "usp_MGMDUpdate_Job";

        #region Fund and Fill Params
        
        public const string CONST_PARAM_VAULTID = "@Vault_id";
        public const string CONST_PARAM_ALERTLEVEL = "@AlertLevel";
        public const string CONST_PARAM_TOTALAMOUNTONFILL = "@TotalAmountOnFill";
        public const string CONST_PARAM_CURRENTVAULTLEVEL = "@CurrentVaultLevel";
        public const string CONST_PARAM_CAPACITY = "@Capacity";
        public const string CONST_PARAM_CURRENTBALANCE = "@CurrentBalance";

        #endregion //Fund and Fill Params

    }
}
