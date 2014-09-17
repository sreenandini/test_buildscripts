
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
    using BMC.Security.Entity;
	using System;

namespace BMC.Security.Manager
{	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="Enterprise")]
	public  class ReportManager : System.Data.Linq.DataContext
	{

    public ReportManager(string connectionString)
        : base(connectionString)
		{
			
		}		
		
		[Function(Name="dbo.rsp_GetRoleAccessForReport")]
    public ISingleResult<GetRoleAccessForReport> GetRoleAccessForReport([Parameter(Name = "SecurityRoleID", DbType = "Int")] System.Nullable<int> securityRoleID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), securityRoleID);
			return ((ISingleResult<GetRoleAccessForReport>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.rsp_GetALLReportMenu")]
        public ISingleResult<GetALLReportMenu> GetALLReportMenu()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GetALLReportMenu>)(result.ReturnValue));
		}
	}
	
	
}
