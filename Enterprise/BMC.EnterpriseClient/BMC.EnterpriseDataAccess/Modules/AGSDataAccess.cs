using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.usp_InsertOrUpdateAGSSetting")]
        public int usp_InsertOrUpdateAGSSetting([Parameter(Name = "Setting_Value", DbType = "VarChar(8000)")] string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_Value);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_InsertOrUpdateSetting")]
        public int usp_InsertOrUpdateSetting([Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Value", DbType = "VarChar(8000)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_Name, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CheckEnrolmentType")]
        public int rsp_CheckEnrolmentType([Parameter(Name = "Serial", DbType = "VarChar(50)")] string serial, [Parameter(Name = "Asset", DbType = "VarChar(50)")] string asset, [Parameter(Name = "GMU", DbType = "VarChar(50)")] string gMU, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result, [Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> Machine_ID)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), serial, asset, gMU, result,Machine_ID);
            result = ((System.Nullable<int>)(result1.GetParameterValue(3)));
           // return ((int)(result1.ReturnValue));
            return Convert.ToInt32( (result1.GetParameterValue(3)));
        }
    }
}
