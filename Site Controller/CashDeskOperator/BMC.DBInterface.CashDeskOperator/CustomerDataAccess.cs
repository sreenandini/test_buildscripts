using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Text;
using BMC.Common.LogManagement;
using BMC.DataAccess;
using System.Data.SqlClient;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
namespace BMC.DBInterface.CashDeskOperator
{

	public  class CustomerDataAccess : System.Data.Linq.DataContext
	{		
    	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();   
		
		public CustomerDataAccess() : 
				base(global::BMC.DBInterface.CashDeskOperator.Properties.Settings.Default.ExchangeConnectionString, mappingSource)
		{
			//OnCreated();
		}
		
		public CustomerDataAccess(string connection) : 
				base(connection, mappingSource)
		{
			//OnCreated();
            this.Connection.ConnectionString = CommonDataAccess.ExchangeConnectionString;
            this.CommandTimeout = SqlHelper.LoadCommandTimeout();
		}
		
		public CustomerDataAccess(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			//OnCreated();
		}
		
		public CustomerDataAccess(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			//OnCreated();
		}
		
		public CustomerDataAccess(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			//OnCreated();
		}

        [Function(Name = "dbo.usp_InsertCustomerDetails")]
        public int InsertCustomerDetails([Parameter(Name = "Title", DbType = "VarChar(10)")] string title,
            [Parameter(Name = "FirstName", DbType = "VarChar(25)")] string firstName, 
            [Parameter(Name = "MiddleName", DbType = "VarChar(25)")] string middleName,
            [Parameter(Name = "LastName", DbType = "VarChar(25)")] string lastName, 
            [Parameter(Name = "ADDRESS1", DbType = "VarChar(50)")] string aDDRESS1, 
            [Parameter(Name = "ADDRESS2", DbType = "VarChar(50)")] string aDDRESS2, 
            [Parameter(Name = "ADDRESS3", DbType = "VarChar(50)")] string aDDRESS3, 
            [Parameter(Name = "PinCode", DbType = "VarChar(25)")] string pinCode, 
            [Parameter(Name = "BankAccNo", DbType = "VarChar(50)")] string bankAccNo)
        {
            IExecuteResult result = null;
            try
            {
                result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), title, firstName, middleName, lastName, aDDRESS1, aDDRESS2, aDDRESS3, pinCode, bankAccNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_SearchCustomerDetails")]
        public ISingleResult<SearchCustomerDetailsResult> rsp_SearchCustomerDetails([Parameter(Name = "FirstName", DbType = "VarChar(25)")] string firstName,
            [Parameter(Name = "LastName", DbType = "VarChar(25)")] string lastName,
            [Parameter(Name = "PinCode", DbType = "VarChar(25)")] string pinCode,
            [Parameter(Name = "BankAccNo", DbType = "VarChar(50)")] string bankAccNo)
        {
            IExecuteResult result=null;
            try
            {
                result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), firstName, lastName, pinCode, bankAccNo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ((ISingleResult<SearchCustomerDetailsResult>)(result.ReturnValue));
        }
        public long RecentCustomerID()
        {
            long iReturn = 0;
            try
            {
              var  value = (from C in this.GetTable<Customer>()
                           orderby C.CustomerID descending
                           select new {C.CustomerID }).FirstOrDefault();
              iReturn = Convert.ToInt64(value.CustomerID.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                iReturn = 0;
            }
            return iReturn;
        }
     
        
	}
}

