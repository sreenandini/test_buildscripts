using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.DataAccess;

namespace BMC.DBInterface.CashDeskOperator
{
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="Exchange")]
	public partial class AuditDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
        partial void OnCreated();
    #endregion
		
		public AuditDataContext() :
        base(global::BMC.DBInterface.CashDeskOperator.Properties.Settings.Default.ExchangeConnectionString1, mappingSource)
		{
           //OnCreated();
		}
		
		public AuditDataContext(string connection) : 
				base(connection, mappingSource)
		{
			//OnCreated();
            this.Connection.ConnectionString = CommonDataAccess.ExchangeConnectionString;
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
		}
		
		public AuditDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			//OnCreated();
		}
		
		public AuditDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			//OnCreated();
		}
		
		public AuditDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			//OnCreated();
		}

        public IEnumerable<FillModules> GetModulesList()
        {
            FillModules oFillModules = null;
            List<FillModules> Olist=null;
            try
            {
                var result =
                        (from AH in this.GetTable<Audit_History>()
                         orderby AH.Audit_Module_ID descending
                         select new {AH.Audit_Module_ID, AH.Audit_Module_Name }).Distinct();
                if (result != null)
                {
                    Olist=new List<FillModules>();
                    FillModules objAudit=new FillModules();
                    objAudit.Audit_Module_Name = "--ALL--";
                    objAudit.Audit_Module_ID = 0;
                    Olist.Add(objAudit);
                    foreach (var AH in result)
                    {
                        oFillModules = new FillModules();
                        
                        oFillModules.Audit_Module_ID = AH.Audit_Module_ID;
                        oFillModules.Audit_Module_Name = AH.Audit_Module_Name;
                        Olist.Add(oFillModules);
                    }
                }
               
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Olist = null;
            }
            return Olist;
        }

        [Function(Name = "dbo.rsp_GetAuditDetails")]
        public ISingleResult<GetAuditDetailsResult> rsp_GetAuditDetails([Parameter(Name = "FromDate", DbType = "DateTime")] System.Nullable<System.DateTime> fromDate, [Parameter(Name = "ToDate", DbType = "DateTime")] System.Nullable<System.DateTime> toDate,
        [Parameter(Name = "ModuleID", DbType = "VarChar(50)")] string moduleID, [Parameter(Name = "Rows", DbType = "int")] int Rows)
        {
            try
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fromDate, toDate, moduleID, Rows);
                return ((ISingleResult<GetAuditDetailsResult>)(result.ReturnValue));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);   
                return null;
            }
        }

        [Function(Name = "dbo.rsp_GetAFTAuditDetails")]
        public ISingleResult<GetAFTAuditDetailsResult> GetAFTAuditDetails([Parameter(Name = "Start_Date", DbType = "DateTime")] System.Nullable<System.DateTime> start_Date, [Parameter(Name = "End_Date", DbType = "DateTime")] System.Nullable<System.DateTime> end_Date, [Parameter(Name = "Rows", DbType = "Int")] System.Nullable<int> rows)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), start_Date, end_Date, rows);
            return ((ISingleResult<GetAFTAuditDetailsResult>)(result.ReturnValue));
        }
		
	}
	
	
}

