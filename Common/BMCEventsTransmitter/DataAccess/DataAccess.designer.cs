﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMC.EventsTransmitter.DataAccess
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
	public partial class DataAccessDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public DataAccessDataContext() : 
				base(global::BMC.EventsTransmitter.Properties.Settings.Default.ExchangeConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataAccessDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataAccessDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataAccessDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataAccessDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.rsp_EventTransmitter_GetExcludedEvents")]
		public ISingleResult<rsp_EventTransmitter_GetExcludedEventsResult> rsp_EventTransmitter_GetExcludedEvents()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_EventTransmitter_GetExcludedEventsResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.rsp_GetSetting")]
		public int rsp_GetSetting([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Setting_ID", DbType="Int")] System.Nullable<int> setting_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Setting_Name", DbType="VarChar(1000)")] string setting_Name, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Setting_Default", DbType="VarChar(1000)")] string setting_Default, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Setting_Value", DbType="VarChar(1000)")] ref string setting_Value)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
			setting_Value = ((string)(result.GetParameterValue(3)));
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.rsp_GetInstallationDetails_STM")]
		public ISingleResult<rsp_GetInstallationDetails_STMResult> rsp_GetInstallationDetails()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_GetInstallationDetails_STMResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.rsp_EventTransmitter_GetSiteDetails")]
		public ISingleResult<rsp_EventTransmitter_GetSiteDetailsResult> rsp_EventTransmitter_GetSiteDetails()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_EventTransmitter_GetSiteDetailsResult>)(result.ReturnValue));
		}
	}
	
	public partial class rsp_EventTransmitter_GetExcludedEventsResult
	{
		
		private System.Nullable<int> _EventID;
		
		private string _EventDescription;
		
		public rsp_EventTransmitter_GetExcludedEventsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EventID", DbType="Int")]
		public System.Nullable<int> EventID
		{
			get
			{
				return this._EventID;
			}
			set
			{
				if ((this._EventID != value))
				{
					this._EventID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EventDescription", DbType="VarChar(100)")]
		public string EventDescription
		{
			get
			{
				return this._EventDescription;
			}
			set
			{
				if ((this._EventDescription != value))
				{
					this._EventDescription = value;
				}
			}
		}
	}
	
	public partial class rsp_GetInstallationDetails_STMResult
	{
		
		private System.Nullable<int> _Machine_No;
		
		private System.Nullable<int> _Installation_No;
		
		private int _Bar_Pos_No;
		
		private string _Bar_Pos_Name;
		
		private string _NAME;
		
		private string _Stock_No;
		
		private string _Machine_Manufacturers_Serial_No;
		
		public rsp_GetInstallationDetails_STMResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Machine_No", DbType="Int")]
		public System.Nullable<int> Machine_No
		{
			get
			{
				return this._Machine_No;
			}
			set
			{
				if ((this._Machine_No != value))
				{
					this._Machine_No = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Installation_No", DbType="Int")]
		public System.Nullable<int> Installation_No
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Bar_Pos_No", DbType="Int NOT NULL")]
		public int Bar_Pos_No
		{
			get
			{
				return this._Bar_Pos_No;
			}
			set
			{
				if ((this._Bar_Pos_No != value))
				{
					this._Bar_Pos_No = value;
				}
			}
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="VarChar(50)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this._NAME = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Machine_Manufacturers_Serial_No", DbType="VarChar(50)")]
		public string Machine_Manufacturers_Serial_No
		{
			get
			{
				return this._Machine_Manufacturers_Serial_No;
			}
			set
			{
				if ((this._Machine_Manufacturers_Serial_No != value))
				{
					this._Machine_Manufacturers_Serial_No = value;
				}
			}
		}
	}
	
	public partial class rsp_EventTransmitter_GetSiteDetailsResult
	{
		
		private string _Site_Code;
		
		private string _Company_name;
		
		private string _sub_company_Name;
		
		private string _Sub_Company_Region_Name;
		
		private string _sub_company_area_name;
		
		private string _sub_company_District_Name;
		
		public rsp_EventTransmitter_GetSiteDetailsResult()
		{
		}
		
		[Column(Storage="_Site_Code", DbType="VarChar(10)")]
		public string Site_Code
		{
			get
			{
				return this._Site_Code;
			}
			set
			{
				if ((this._Site_Code != value))
				{
					this._Site_Code = value;
				}
			}
		}
		
		[Column(Storage="_Company_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Company_name
		{
			get
			{
				return this._Company_name;
			}
			set
			{
				if ((this._Company_name != value))
				{
					this._Company_name = value;
				}
			}
		}
		
		[Column(Storage="_sub_company_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string sub_company_Name
		{
			get
			{
				return this._sub_company_Name;
			}
			set
			{
				if ((this._sub_company_Name != value))
				{
					this._sub_company_Name = value;
				}
			}
		}
		
		[Column(Storage="_Sub_Company_Region_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Sub_Company_Region_Name
		{
			get
			{
				return this._Sub_Company_Region_Name;
			}
			set
			{
				if ((this._Sub_Company_Region_Name != value))
				{
					this._Sub_Company_Region_Name = value;
				}
			}
		}
		
		[Column(Storage="_sub_company_area_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string sub_company_area_name
		{
			get
			{
				return this._sub_company_area_name;
			}
			set
			{
				if ((this._sub_company_area_name != value))
				{
					this._sub_company_area_name = value;
				}
			}
		}
		
		[Column(Storage="_sub_company_District_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string sub_company_District_Name
		{
			get
			{
				return this._sub_company_District_Name;
			}
			set
			{
				if ((this._sub_company_District_Name != value))
				{
					this._sub_company_District_Name = value;
				}
			}
		}
	}
}
#pragma warning restore 1591