using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;


namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.usp_Export_Model")]
        public int usp_Export_Model([Parameter(Name = "Reference", DbType = "VarChar(50)")] string reference, [Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "ErrorMessage", DbType = "VarChar(250)")] ref string errorMessage)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference, site_id, result, errorMessage);
            result = ((System.Nullable<int>)(result1.GetParameterValue(2)));
            errorMessage = ((string)(result1.GetParameterValue(3)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_Export_GameLibrary")]
        public int usp_Export_GameLibrary([Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "ErrorMessage", DbType = "VarChar(250)")] ref string errorMessage)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_id, result, errorMessage);
            result = ((System.Nullable<int>)(result1.GetParameterValue(1)));
            errorMessage = ((string)(result1.GetParameterValue(2)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_Export_Calendar")]
        public int usp_Export_Calendar([Parameter(Name = "Reference1", DbType = "VarChar(50)")] string reference1, [Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "ErrorMessage", DbType = "VarChar(250)")] ref string errorMessage)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1, site_id, result, errorMessage);
            result = ((System.Nullable<int>)(result1.GetParameterValue(2)));
            errorMessage = ((string)(result1.GetParameterValue(3)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_Export_SiteSetup")]
        public int usp_Export_SiteSetup([Parameter(Name = "Reference1", DbType = "VarChar(50)")] string reference1, [Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "ErrorMessage", DbType = "VarChar(250)")] ref string errorMessage)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1, site_id, result, errorMessage);
            result = ((System.Nullable<int>)(result1.GetParameterValue(2)));
            errorMessage = ((string)(result1.GetParameterValue(3)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdatePasskey")]
        public int UpdatePasskey([Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "PassKey", DbType = "VarChar(300)")] string passKey, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "ErrorMessage", DbType = "VarChar(250)")] ref string errorMessage)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_id, passKey, result, errorMessage);
            result = ((System.Nullable<int>)(result1.GetParameterValue(2)));
            errorMessage = ((string)(result1.GetParameterValue(3)));
            return ((int)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_EnableorDisableSite")]
        public int EnableorDisableSite([Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "IsEnable", DbType = "Bit")] System.Nullable<bool> isEnable, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "ErrorMessage", DbType = "VarChar(250)")] ref string errorMessage)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_id, isEnable, result, errorMessage);
            result = ((System.Nullable<int>)(result1.GetParameterValue(2)));
            errorMessage = ((string)(result1.GetParameterValue(3)));
            return ((int)(result1.ReturnValue));
        }
    }
}
