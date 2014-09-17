using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

using Audit.Transport;
using BMC.Common.ExceptionManagement;
using System.Collections.Specialized;

namespace Audit.DBBuilder
{
    public class Class1
    {
    }

    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Exchange")]
    public partial class AuditDataContext : System.Data.Linq.DataContext
    {
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public AuditDataContext() :
            base(global::Audit.DBBuilder.Properties.Settings.Default.ExchangeConnectionString, mappingSource)
        {
            OnCreated();
        }

        public AuditDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public AuditDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public AuditDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public AuditDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public System.Data.Linq.Table<AuditModules> Audit_Modules
        {
            get
            {
                return this.GetTable<AuditModules>();
            }
        }

        public System.Data.Linq.Table<Audit_History> Audit_Histories
        {
            get
            {
                return this.GetTable<Audit_History>();
            }
        }


        //[Function(Name = "dbo.usp_InsertAuditData")]
        //public int InsertAuditData([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "User_Name", DbType = "VarChar(50)")] string user_Name, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(50)")] string module_Name, [Parameter(Name = "Slot", DbType = "VarChar(50)")] string slot, [Parameter(Name = "Aud_Field", DbType = "VarChar(100)")] string aud_Field, [Parameter(Name = "Old_Value", DbType = "VarChar(100)")] string old_Value, [Parameter(Name = "New_Value", DbType = "VarChar(100)")] string new_Value, [Parameter(Name = "Aud_Desc", DbType = "VarChar(500)")] string aud_Desc)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_ID, user_Name, module_ID, module_Name, slot, aud_Field, old_Value, new_Value, aud_Desc);
        //    return ((int)(result.ReturnValue));
        //}

        [Function(Name = "dbo.usp_InsertAuditData")]
        public int InsertAuditData([Parameter(Name = "User_ID", DbType = "Int")] System.Nullable<int> user_ID, [Parameter(Name = "User_Name", DbType = "VarChar(50)")] string user_Name, [Parameter(Name = "Module_ID", DbType = "Int")] System.Nullable<int> module_ID, [Parameter(Name = "Module_Name", DbType = "VarChar(50)")] string module_Name, [Parameter(Name = "Screen_Name", DbType = "VarChar(50)")] string screen_Name, [Parameter(Name = "Slot", DbType = "VarChar(50)")] string slot, [Parameter(Name = "Aud_Field", DbType = "VarChar(500)")] string aud_Field, [Parameter(Name = "Old_Value", DbType = "VarChar(500)")] string old_Value, [Parameter(Name = "New_Value", DbType = "VarChar(500)")] string new_Value, [Parameter(Name = "Aud_Desc", DbType = "VarChar(500)")] string aud_Desc, [Parameter(Name = "Operation_Type", DbType = "VarChar(25)")] string operation_Type)
        {
            try
            {
                IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), user_ID, user_Name, module_ID, module_Name, screen_Name, slot, aud_Field, old_Value, new_Value, aud_Desc, operation_Type);
                return ((int)(result.ReturnValue));
            }
            catch (Exception ex)
            {
                NameValueCollection nVC = new NameValueCollection();
                nVC.Add("User_ID", user_ID.ToString());
                nVC.Add("User_Name", user_Name);
                nVC.Add("module_ID", module_ID.ToString());
                nVC.Add("module_Name", module_Name);
                nVC.Add("screen_Name", screen_Name);
                nVC.Add("slot", slot);
                nVC.Add("aud_Field", aud_Field);
                nVC.Add("old_Value", old_Value);
                nVC.Add("new_Value", new_Value);
                nVC.Add("aud_Desc", aud_Desc);
                nVC.Add("operation_Type", operation_Type);

                ex.Source = "AuditViewer";                

                ExceptionManager.Publish(ex ,nVC);                
                return -1;
            }
        }

        public List<AuditModules> GetModulesList()
        {
            List<AuditModules> audit = Audit_Modules.ToList<AuditModules>();
            return audit;
        }


        [Function(Name = "dbo.rsp_GetAuditDetails")]
        public ISingleResult<Audit_History> GetAuditDetails([Parameter(Name = "FromDate", DbType = "DateTime")] System.Nullable<System.DateTime> fromDate, [Parameter(Name = "ToDate", DbType = "DateTime")] System.Nullable<System.DateTime> toDate, [Parameter(Name = "ModuleID", DbType = "VarChar(50)")] string moduleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fromDate, toDate, moduleID);
            return ((ISingleResult<Audit_History>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetInitialSettings")]
        public ISingleResult<rsp_GetInitialSettingsResult> GetInitialSettings()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetInitialSettingsResult>)(result.ReturnValue));
        }

    }

      
}
