﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMC.DBInterface.CashDeskOperator
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Exchange")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::BMC.DBInterface.CashDeskOperator.Properties.Settings.Default.ExchangeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_UpdateFinalStatusTicketException")]
		public int usp_UpdateFinalStatusTicketException([global::System.Data.Linq.Mapping.ParameterAttribute(Name="TEID", DbType="Int")] System.Nullable<int> tEID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tEID);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_VoidTreasury_CreateNegTran")]
		public int usp_VoidTreasury_CreateNegTran([global::System.Data.Linq.Mapping.ParameterAttribute(Name="TreasuryNo", DbType="Int")] System.Nullable<int> treasuryNo, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(50)")] string dDate, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(50)")] string dTime, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserNo", DbType="Int")] System.Nullable<int> userNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="OutVal", DbType="Int")] ref System.Nullable<int> outVal)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), treasuryNo, dDate, dTime, userNo, outVal);
			outVal = ((System.Nullable<int>)(result.GetParameterValue(4)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.rsp_GetInstallationList")]
		public ISingleResult<rsp_GetInstallationListResult> rsp_GetInstallationList()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_GetInstallationListResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.usp_InsertTreasury")]
		public int usp_InsertTreasury([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Installation_No", DbType="Int")] System.Nullable<int> installation_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Collection_No", DbType="Int")] System.Nullable<int> collection_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="User_ID", DbType="Int")] System.Nullable<int> user_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Type", DbType="VarChar(30)")] string treasury_Type, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Reason", DbType="VarChar(200)")] string treasury_Reason, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Amount", DbType="Money")] System.Nullable<decimal> treasury_Amount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Allocated", DbType="Bit")] System.Nullable<bool> treasury_Allocated, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Membership_No", DbType="VarChar(50)")] string treasury_Membership_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Reason_Code", DbType="Int")] System.Nullable<int> treasury_Reason_Code, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Issuer_User_No", DbType="Int")] System.Nullable<int> treasury_Issuer_User_No, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Temp", DbType="Bit")] System.Nullable<bool> treasury_Temp, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Float_Issued_By", DbType="Int")] System.Nullable<int> treasury_Float_Issued_By, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Treasury_Actual_Date", DbType="DateTime")] System.Nullable<System.DateTime> treasury_Actual_Date, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CustomerID", DbType="BigInt")] System.Nullable<long> customerID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TreasuryNo", DbType="Int")] ref System.Nullable<int> treasuryNo)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, collection_No, user_ID, treasury_Type, treasury_Reason, treasury_Amount, treasury_Allocated, treasury_Membership_No, treasury_Reason_Code, treasury_Issuer_User_No, treasury_Temp, treasury_Float_Issued_By, treasury_Actual_Date, customerID, treasuryNo);
			treasuryNo = ((System.Nullable<int>)(result.GetParameterValue(14)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.rsp_GetGMUPosDetails")]
		public ISingleResult<rsp_GetGMUPosDetailsResult> GetGMUPosDetails([global::System.Data.Linq.Mapping.ParameterAttribute(Name="IPList", DbType="NVarChar(MAX)")] string iPList)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iPList);
			return ((ISingleResult<rsp_GetGMUPosDetailsResult>)(result.ReturnValue));
		}
	}
	
	public partial class rsp_GetInstallationListResult
	{
		
		private string _Bar_Pos_Name;
		
		private string _Stock_No;
		
		private string _Name;
		
		private string _Installation_Reference;
		
		private int _Installation_No;
		
		public rsp_GetInstallationListResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Bar_Pos_Name", DbType="VarChar(50)")]
		public string Bar_Pos_Name
		{
			get
			{
				return this._Bar_Pos_Name;
			}
			set
			{
				if ((this._Bar_Pos_Name != value))
				{
					this._Bar_Pos_Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Stock_No", DbType="VarChar(50)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this._Name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Installation_Reference", DbType="VarChar(50)")]
		public string Installation_Reference
		{
			get
			{
				return this._Installation_Reference;
			}
			set
			{
				if ((this._Installation_Reference != value))
				{
					this._Installation_Reference = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Installation_No", DbType="Int NOT NULL")]
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
	
	public partial class rsp_GetGMUPosDetailsResult
	{
		
		private System.Nullable<bool> _IsChecked;
		
		private string _IP;
		
		private string _BarPostion;
		
		private string _Status;
		
		public rsp_GetGMUPosDetailsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsChecked", DbType="Bit")]
		public System.Nullable<bool> IsChecked
		{
			get
			{
				return this._IsChecked;
			}
			set
			{
				if ((this._IsChecked != value))
				{
					this._IsChecked = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IP", DbType="NVarChar(50)")]
		public string IP
		{
			get
			{
				return this._IP;
			}
			set
			{
				if ((this._IP != value))
				{
					this._IP = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BarPostion", DbType="VarChar(50)")]
		public string BarPostion
		{
			get
			{
				return this._BarPostion;
			}
			set
			{
				if ((this._BarPostion != value))
				{
					this._BarPostion = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="NVarChar(500)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this._Status = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
