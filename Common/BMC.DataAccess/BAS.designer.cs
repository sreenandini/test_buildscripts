﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3607
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMC.DataAccess
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
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="BAS")]
	public partial class BASDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertBAS_IS_Export_History(BAS_IS_Export_History instance);
    partial void UpdateBAS_IS_Export_History(BAS_IS_Export_History instance);
    partial void DeleteBAS_IS_Export_History(BAS_IS_Export_History instance);
    partial void InsertBAS_Settings(BAS_Settings instance);
    partial void UpdateBAS_Settings(BAS_Settings instance);
    partial void DeleteBAS_Settings(BAS_Settings instance);
    #endregion
		
		public BASDataContext() : 
				base(global::BMC.DataAccess.Properties.Settings.Default.BASConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BASDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BASDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BASDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BASDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Export_History> Export_Histories
		{
			get
			{
				return this.GetTable<Export_History>();
			}
		}
		
		public System.Data.Linq.Table<Import_History> Import_Histories
		{
			get
			{
				return this.GetTable<Import_History>();
			}
		}
		
		public System.Data.Linq.Table<BAS_IS_Export_History> BAS_IS_Export_Histories
		{
			get
			{
				return this.GetTable<BAS_IS_Export_History>();
			}
		}
		
		public System.Data.Linq.Table<BAS_Settings> BAS_Settings
		{
			get
			{
				return this.GetTable<BAS_Settings>();
			}
		}
		
		[Function(Name="dbo.rsp_GetUnExportedRecordsFromExportHistory")]
		public ISingleResult<rsp_GetUnExportedRecordsFromExportHistoryResult> rsp_GetUnExportedRecordsFromExportHistory()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_GetUnExportedRecordsFromExportHistoryResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.rsp_GetUnExportedRecordsFromImportHistory")]
		public ISingleResult<rsp_GetUnExportedRecordsFromImportHistoryResult> rsp_GetUnExportedRecordsFromImportHistory()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_GetUnExportedRecordsFromImportHistoryResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_UpdateEHRecordAsExported")]
		public int usp_UpdateEHRecordAsExported([Parameter(Name="EH_ID", DbType="Int")] System.Nullable<int> eH_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), eH_ID);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_UpdateIHRecordAsExported")]
		public int usp_UpdateIHRecordAsExported([Parameter(Name="IH_ID", DbType="Int")] System.Nullable<int> iH_ID, [Parameter(Name="IH_Success", DbType="Int")] System.Nullable<int> iH_Success)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iH_ID, iH_Success);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.rsp_GetNotAcknowledgedMessages")]
		public ISingleResult<rsp_GetNotAcknowledgedMessagesResult> rsp_GetNotAcknowledgedMessages()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<rsp_GetNotAcknowledgedMessagesResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_UpdateEHRecordFor504")]
		public int usp_UpdateEHRecordFor504([Parameter(Name="EH_ID", DbType="Int")] System.Nullable<int> eH_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), eH_ID);
			return ((int)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_InsUpd_Export_History")]
		public ISingleResult<usp_InsUpd_Export_HistoryResult> usp_InsUpd_Export_History([Parameter(Name="EH_ID", DbType="Int")] System.Nullable<int> eH_ID, [Parameter(Name="EH_MESSAGE_ID", DbType="VarChar(100)")] string eH_MESSAGE_ID, [Parameter(Name="EH_Message", DbType="VarChar(4000)")] string eH_Message, [Parameter(Name="EH_Type", DbType="VarChar(30)")] string eH_Type, [Parameter(Name="EH_Status", DbType="VarChar(100)")] string eH_Status)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), eH_ID, eH_MESSAGE_ID, eH_Message, eH_Type, eH_Status);
			return ((ISingleResult<usp_InsUpd_Export_HistoryResult>)(result.ReturnValue));
		}
		
		[Function(Name="dbo.usp_InsertErrorMessage")]
		public int usp_InsertErrorMessage([Parameter(Name="Message_Id", DbType="VarChar(30)")] string message_Id, [Parameter(Name="Message_Type", DbType="VarChar(10)")] string message_Type, [Parameter(Name="Error", DbType="VarChar(8000)")] string error, [Parameter(Name="Details", DbType="VarChar(4000)")] string details)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), message_Id, message_Type, error, details);
			return ((int)(result.ReturnValue));
		}
	}
	
	[Table(Name="dbo.Export_History")]
	public partial class Export_History
	{
		
		private int _EH_ID;
		
		private string _EH_MESSAGE_ID;
		
		private System.DateTime _EH_Recieved_Date;
		
		private System.Nullable<System.DateTime> _EH_Exported_Date;
		
		private string _EH_Reference;
		
		private string _EH_Type;
		
		private string _EH_Status;
		
		private string _EH_Message;
		
		public Export_History()
		{
		}
		
		[Column(Storage="_EH_ID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int EH_ID
		{
			get
			{
				return this._EH_ID;
			}
			set
			{
				if ((this._EH_ID != value))
				{
					this._EH_ID = value;
				}
			}
		}
		
		[Column(Storage="_EH_MESSAGE_ID", DbType="VarChar(50)")]
		public string EH_MESSAGE_ID
		{
			get
			{
				return this._EH_MESSAGE_ID;
			}
			set
			{
				if ((this._EH_MESSAGE_ID != value))
				{
					this._EH_MESSAGE_ID = value;
				}
			}
		}
		
		[Column(Storage="_EH_Recieved_Date", DbType="DateTime NOT NULL")]
		public System.DateTime EH_Recieved_Date
		{
			get
			{
				return this._EH_Recieved_Date;
			}
			set
			{
				if ((this._EH_Recieved_Date != value))
				{
					this._EH_Recieved_Date = value;
				}
			}
		}
		
		[Column(Storage="_EH_Exported_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> EH_Exported_Date
		{
			get
			{
				return this._EH_Exported_Date;
			}
			set
			{
				if ((this._EH_Exported_Date != value))
				{
					this._EH_Exported_Date = value;
				}
			}
		}
		
		[Column(Storage="_EH_Reference", DbType="VarChar(50)")]
		public string EH_Reference
		{
			get
			{
				return this._EH_Reference;
			}
			set
			{
				if ((this._EH_Reference != value))
				{
					this._EH_Reference = value;
				}
			}
		}
		
		[Column(Storage="_EH_Type", DbType="VarChar(30)")]
		public string EH_Type
		{
			get
			{
				return this._EH_Type;
			}
			set
			{
				if ((this._EH_Type != value))
				{
					this._EH_Type = value;
				}
			}
		}
		
		[Column(Storage="_EH_Status", DbType="VarChar(100)")]
		public string EH_Status
		{
			get
			{
				return this._EH_Status;
			}
			set
			{
				if ((this._EH_Status != value))
				{
					this._EH_Status = value;
				}
			}
		}
		
		[Column(Storage="_EH_Message", DbType="VarChar(8000)")]
		public string EH_Message
		{
			get
			{
				return this._EH_Message;
			}
			set
			{
				if ((this._EH_Message != value))
				{
					this._EH_Message = value;
				}
			}
		}
	}
	
	[Table(Name="dbo.Import_History")]
	public partial class Import_History
	{
		
		private int _IH_ID;
		
		private string _IH_MESSAGE_ID;
		
		private string _IH_Type;
		
		private string _IH_Message;
		
		private int _IH_Status;
		
		private System.DateTime _IH_Received_Date;
		
		private System.Nullable<System.DateTime> _IH_Imported_Date;
		
		private string _IH_ExportResult;
		
		public Import_History()
		{
		}
		
		[Column(Storage="_IH_ID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int IH_ID
		{
			get
			{
				return this._IH_ID;
			}
			set
			{
				if ((this._IH_ID != value))
				{
					this._IH_ID = value;
				}
			}
		}
		
		[Column(Storage="_IH_MESSAGE_ID", DbType="VarChar(50)")]
		public string IH_MESSAGE_ID
		{
			get
			{
				return this._IH_MESSAGE_ID;
			}
			set
			{
				if ((this._IH_MESSAGE_ID != value))
				{
					this._IH_MESSAGE_ID = value;
				}
			}
		}
		
		[Column(Storage="_IH_Type", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string IH_Type
		{
			get
			{
				return this._IH_Type;
			}
			set
			{
				if ((this._IH_Type != value))
				{
					this._IH_Type = value;
				}
			}
		}
		
		[Column(Storage="_IH_Message", DbType="VarChar(8000) NOT NULL", CanBeNull=false)]
		public string IH_Message
		{
			get
			{
				return this._IH_Message;
			}
			set
			{
				if ((this._IH_Message != value))
				{
					this._IH_Message = value;
				}
			}
		}
		
		[Column(Storage="_IH_Status", DbType="Int NOT NULL")]
		public int IH_Status
		{
			get
			{
				return this._IH_Status;
			}
			set
			{
				if ((this._IH_Status != value))
				{
					this._IH_Status = value;
				}
			}
		}
		
		[Column(Storage="_IH_Received_Date", DbType="DateTime NOT NULL")]
		public System.DateTime IH_Received_Date
		{
			get
			{
				return this._IH_Received_Date;
			}
			set
			{
				if ((this._IH_Received_Date != value))
				{
					this._IH_Received_Date = value;
				}
			}
		}
		
		[Column(Storage="_IH_Imported_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> IH_Imported_Date
		{
			get
			{
				return this._IH_Imported_Date;
			}
			set
			{
				if ((this._IH_Imported_Date != value))
				{
					this._IH_Imported_Date = value;
				}
			}
		}
		
		[Column(Storage="_IH_ExportResult", DbType="VarChar(100)")]
		public string IH_ExportResult
		{
			get
			{
				return this._IH_ExportResult;
			}
			set
			{
				if ((this._IH_ExportResult != value))
				{
					this._IH_ExportResult = value;
				}
			}
		}
	}
	
	[Table(Name="dbo.BAS_IS_Export_History")]
	public partial class BAS_IS_Export_History : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _BIEH_ID;
		
		private int _BIEH_Reference;
		
		private string _BIEH_Message_ID;
		
		private string _BIEH_Message_Type;
		
		private System.Nullable<int> _BIEH_Status;
		
		private System.Nullable<System.DateTime> _BIEH_Received_Date;
		
		private System.Nullable<System.DateTime> _BIEH_Exported_Date;
		
		private string _BIEH_Comments;
		
		private string _BIEH_BASMessage;
		
		private string _BIEH_ISXMLMessage;
		
		private int _BIEH_Retry_Counter;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnBIEH_IDChanging(int value);
    partial void OnBIEH_IDChanged();
    partial void OnBIEH_ReferenceChanging(int value);
    partial void OnBIEH_ReferenceChanged();
    partial void OnBIEH_Message_IDChanging(string value);
    partial void OnBIEH_Message_IDChanged();
    partial void OnBIEH_Message_TypeChanging(string value);
    partial void OnBIEH_Message_TypeChanged();
    partial void OnBIEH_StatusChanging(System.Nullable<int> value);
    partial void OnBIEH_StatusChanged();
    partial void OnBIEH_Received_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnBIEH_Received_DateChanged();
    partial void OnBIEH_Exported_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnBIEH_Exported_DateChanged();
    partial void OnBIEH_CommentsChanging(string value);
    partial void OnBIEH_CommentsChanged();
    partial void OnBIEH_BASMessageChanging(string value);
    partial void OnBIEH_BASMessageChanged();
    partial void OnBIEH_ISXMLMessageChanging(string value);
    partial void OnBIEH_ISXMLMessageChanged();
    partial void OnBIEH_Retry_CounterChanging(int value);
    partial void OnBIEH_Retry_CounterChanged();
    #endregion
		
		public BAS_IS_Export_History()
		{
			OnCreated();
		}
		
		[Column(Storage="_BIEH_ID", DbType="Int Not Null Identity", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int BIEH_ID
		{
			get
			{
				return this._BIEH_ID;
			}
			set
			{
				if ((this._BIEH_ID != value))
				{
					this.OnBIEH_IDChanging(value);
					this.SendPropertyChanging();
					this._BIEH_ID = value;
					this.SendPropertyChanged("BIEH_ID");
					this.OnBIEH_IDChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Reference", DbType="INT Not NULL Identity", UpdateCheck=UpdateCheck.Never)]
		public int BIEH_Reference
		{
			get
			{
				return this._BIEH_Reference;
			}
			set
			{
				if ((this._BIEH_Reference != value))
				{
					this.OnBIEH_ReferenceChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Reference = value;
					this.SendPropertyChanged("BIEH_Reference");
					this.OnBIEH_ReferenceChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Message_ID", DbType="varchar(50)", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string BIEH_Message_ID
		{
			get
			{
				return this._BIEH_Message_ID;
			}
			set
			{
				if ((this._BIEH_Message_ID != value))
				{
					this.OnBIEH_Message_IDChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Message_ID = value;
					this.SendPropertyChanged("BIEH_Message_ID");
					this.OnBIEH_Message_IDChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Message_Type", DbType="varchar(5)", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string BIEH_Message_Type
		{
			get
			{
				return this._BIEH_Message_Type;
			}
			set
			{
				if ((this._BIEH_Message_Type != value))
				{
					this.OnBIEH_Message_TypeChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Message_Type = value;
					this.SendPropertyChanged("BIEH_Message_Type");
					this.OnBIEH_Message_TypeChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Status", DbType="INT", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<int> BIEH_Status
		{
			get
			{
				return this._BIEH_Status;
			}
			set
			{
				if ((this._BIEH_Status != value))
				{
					this.OnBIEH_StatusChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Status = value;
					this.SendPropertyChanged("BIEH_Status");
					this.OnBIEH_StatusChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Received_Date", DbType="DATETIME", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> BIEH_Received_Date
		{
			get
			{
				return this._BIEH_Received_Date;
			}
			set
			{
				if ((this._BIEH_Received_Date != value))
				{
					this.OnBIEH_Received_DateChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Received_Date = value;
					this.SendPropertyChanged("BIEH_Received_Date");
					this.OnBIEH_Received_DateChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Exported_Date", DbType="datetime", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> BIEH_Exported_Date
		{
			get
			{
				return this._BIEH_Exported_Date;
			}
			set
			{
				if ((this._BIEH_Exported_Date != value))
				{
					this.OnBIEH_Exported_DateChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Exported_Date = value;
					this.SendPropertyChanged("BIEH_Exported_Date");
					this.OnBIEH_Exported_DateChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Comments", DbType="varchar(100)", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string BIEH_Comments
		{
			get
			{
				return this._BIEH_Comments;
			}
			set
			{
				if ((this._BIEH_Comments != value))
				{
					this.OnBIEH_CommentsChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Comments = value;
					this.SendPropertyChanged("BIEH_Comments");
					this.OnBIEH_CommentsChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_BASMessage", DbType="varchar(8000)", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string BIEH_BASMessage
		{
			get
			{
				return this._BIEH_BASMessage;
			}
			set
			{
				if ((this._BIEH_BASMessage != value))
				{
					this.OnBIEH_BASMessageChanging(value);
					this.SendPropertyChanging();
					this._BIEH_BASMessage = value;
					this.SendPropertyChanged("BIEH_BASMessage");
					this.OnBIEH_BASMessageChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_ISXMLMessage", DbType="varchar(8000)", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string BIEH_ISXMLMessage
		{
			get
			{
				return this._BIEH_ISXMLMessage;
			}
			set
			{
				if ((this._BIEH_ISXMLMessage != value))
				{
					this.OnBIEH_ISXMLMessageChanging(value);
					this.SendPropertyChanging();
					this._BIEH_ISXMLMessage = value;
					this.SendPropertyChanged("BIEH_ISXMLMessage");
					this.OnBIEH_ISXMLMessageChanged();
				}
			}
		}
		
		[Column(Storage="_BIEH_Retry_Counter", DbType="int not null ", UpdateCheck=UpdateCheck.Never)]
		public int BIEH_Retry_Counter
		{
			get
			{
				return this._BIEH_Retry_Counter;
			}
			set
			{
				if ((this._BIEH_Retry_Counter != value))
				{
					this.OnBIEH_Retry_CounterChanging(value);
					this.SendPropertyChanging();
					this._BIEH_Retry_Counter = value;
					this.SendPropertyChanged("BIEH_Retry_Counter");
					this.OnBIEH_Retry_CounterChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="dbo.BAS_Settings")]
	public partial class BAS_Settings : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Setting_ID;
		
		private string _Setting_Name;
		
		private string _Setting_Value;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSetting_IDChanging(int value);
    partial void OnSetting_IDChanged();
    partial void OnSetting_NameChanging(string value);
    partial void OnSetting_NameChanged();
    partial void OnSetting_ValueChanging(string value);
    partial void OnSetting_ValueChanged();
    #endregion
		
		public BAS_Settings()
		{
			OnCreated();
		}
		
		[Column(Storage="_Setting_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int Setting_ID
		{
			get
			{
				return this._Setting_ID;
			}
			set
			{
				if ((this._Setting_ID != value))
				{
					this.OnSetting_IDChanging(value);
					this.SendPropertyChanging();
					this._Setting_ID = value;
					this.SendPropertyChanged("Setting_ID");
					this.OnSetting_IDChanged();
				}
			}
		}
		
		[Column(Storage="_Setting_Name", DbType="VarChar(50)")]
		public string Setting_Name
		{
			get
			{
				return this._Setting_Name;
			}
			set
			{
				if ((this._Setting_Name != value))
				{
					this.OnSetting_NameChanging(value);
					this.SendPropertyChanging();
					this._Setting_Name = value;
					this.SendPropertyChanged("Setting_Name");
					this.OnSetting_NameChanged();
				}
			}
		}
		
		[Column(Storage="_Setting_Value", DbType="VarChar(50)")]
		public string Setting_Value
		{
			get
			{
				return this._Setting_Value;
			}
			set
			{
				if ((this._Setting_Value != value))
				{
					this.OnSetting_ValueChanging(value);
					this.SendPropertyChanging();
					this._Setting_Value = value;
					this.SendPropertyChanged("Setting_Value");
					this.OnSetting_ValueChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class rsp_GetUnExportedRecordsFromExportHistoryResult
	{
		
		private int _EH_ID;
		
		private string _EH_MESSAGE_ID;
		
		private System.DateTime _EH_Recieved_Date;
		
		private System.Nullable<System.DateTime> _EH_Exported_Date;
		
		private string _EH_Reference;
		
		private string _EH_Type;
		
		private string _EH_Status;
		
		private string _EH_Message;
		
		public rsp_GetUnExportedRecordsFromExportHistoryResult()
		{
		}
		
		[Column(Storage="_EH_ID", DbType="Int NOT NULL")]
		public int EH_ID
		{
			get
			{
				return this._EH_ID;
			}
			set
			{
				if ((this._EH_ID != value))
				{
					this._EH_ID = value;
				}
			}
		}
		
		[Column(Storage="_EH_MESSAGE_ID", DbType="VarChar(50)")]
		public string EH_MESSAGE_ID
		{
			get
			{
				return this._EH_MESSAGE_ID;
			}
			set
			{
				if ((this._EH_MESSAGE_ID != value))
				{
					this._EH_MESSAGE_ID = value;
				}
			}
		}
		
		[Column(Storage="_EH_Recieved_Date", DbType="DateTime NOT NULL")]
		public System.DateTime EH_Recieved_Date
		{
			get
			{
				return this._EH_Recieved_Date;
			}
			set
			{
				if ((this._EH_Recieved_Date != value))
				{
					this._EH_Recieved_Date = value;
				}
			}
		}
		
		[Column(Storage="_EH_Exported_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> EH_Exported_Date
		{
			get
			{
				return this._EH_Exported_Date;
			}
			set
			{
				if ((this._EH_Exported_Date != value))
				{
					this._EH_Exported_Date = value;
				}
			}
		}
		
		[Column(Storage="_EH_Reference", DbType="VarChar(50)")]
		public string EH_Reference
		{
			get
			{
				return this._EH_Reference;
			}
			set
			{
				if ((this._EH_Reference != value))
				{
					this._EH_Reference = value;
				}
			}
		}
		
		[Column(Storage="_EH_Type", DbType="VarChar(30)")]
		public string EH_Type
		{
			get
			{
				return this._EH_Type;
			}
			set
			{
				if ((this._EH_Type != value))
				{
					this._EH_Type = value;
				}
			}
		}
		
		[Column(Storage="_EH_Status", DbType="VarChar(100)")]
		public string EH_Status
		{
			get
			{
				return this._EH_Status;
			}
			set
			{
				if ((this._EH_Status != value))
				{
					this._EH_Status = value;
				}
			}
		}
		
		[Column(Storage="_EH_Message", DbType="VarChar(8000)")]
		public string EH_Message
		{
			get
			{
				return this._EH_Message;
			}
			set
			{
				if ((this._EH_Message != value))
				{
					this._EH_Message = value;
				}
			}
		}
	}
	
	public partial class rsp_GetUnExportedRecordsFromImportHistoryResult
	{
		
		private int _IH_ID;
		
		private string _IH_MESSAGE_ID;
		
		private string _IH_Type;
		
		private string _IH_Message;
		
		private int _IH_Status;
		
		private System.DateTime _IH_Received_Date;
		
		private System.Nullable<System.DateTime> _IH_Imported_Date;
		
		private string _IH_ExportResult;
		
		public rsp_GetUnExportedRecordsFromImportHistoryResult()
		{
		}
		
		[Column(Storage="_IH_ID", DbType="Int NOT NULL")]
		public int IH_ID
		{
			get
			{
				return this._IH_ID;
			}
			set
			{
				if ((this._IH_ID != value))
				{
					this._IH_ID = value;
				}
			}
		}
		
		[Column(Storage="_IH_MESSAGE_ID", DbType="VarChar(50)")]
		public string IH_MESSAGE_ID
		{
			get
			{
				return this._IH_MESSAGE_ID;
			}
			set
			{
				if ((this._IH_MESSAGE_ID != value))
				{
					this._IH_MESSAGE_ID = value;
				}
			}
		}
		
		[Column(Storage="_IH_Type", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string IH_Type
		{
			get
			{
				return this._IH_Type;
			}
			set
			{
				if ((this._IH_Type != value))
				{
					this._IH_Type = value;
				}
			}
		}
		
		[Column(Storage="_IH_Message", DbType="VarChar(8000) NOT NULL", CanBeNull=false)]
		public string IH_Message
		{
			get
			{
				return this._IH_Message;
			}
			set
			{
				if ((this._IH_Message != value))
				{
					this._IH_Message = value;
				}
			}
		}
		
		[Column(Storage="_IH_Status", DbType="Int NOT NULL")]
		public int IH_Status
		{
			get
			{
				return this._IH_Status;
			}
			set
			{
				if ((this._IH_Status != value))
				{
					this._IH_Status = value;
				}
			}
		}
		
		[Column(Storage="_IH_Received_Date", DbType="DateTime NOT NULL")]
		public System.DateTime IH_Received_Date
		{
			get
			{
				return this._IH_Received_Date;
			}
			set
			{
				if ((this._IH_Received_Date != value))
				{
					this._IH_Received_Date = value;
				}
			}
		}
		
		[Column(Storage="_IH_Imported_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> IH_Imported_Date
		{
			get
			{
				return this._IH_Imported_Date;
			}
			set
			{
				if ((this._IH_Imported_Date != value))
				{
					this._IH_Imported_Date = value;
				}
			}
		}
		
		[Column(Storage="_IH_ExportResult", DbType="VarChar(100)")]
		public string IH_ExportResult
		{
			get
			{
				return this._IH_ExportResult;
			}
			set
			{
				if ((this._IH_ExportResult != value))
				{
					this._IH_ExportResult = value;
				}
			}
		}
	}
	
	public partial class rsp_GetNotAcknowledgedMessagesResult
	{
		
		private System.Nullable<int> _IH_ID;
		
		private string _IH_MESSAGE_ID;
		
		private string _IH_Type;
		
		private string _IH_Message;
		
		private System.Nullable<int> _IH_Status;
		
		private System.Nullable<System.DateTime> _IH_Received_Date;
		
		private System.Nullable<System.DateTime> _IH_Imported_Date;
		
		private string _IH_ExportResult;
		
		private int _EH_ID;
		
		private string _EH_MESSAGE_ID;
		
		private System.DateTime _EH_Recieved_Date;
		
		private System.Nullable<System.DateTime> _EH_Exported_Date;
		
		private string _EH_Reference;
		
		private string _EH_Type;
		
		private string _EH_Status;
		
		private string _EH_Message;
		
		public rsp_GetNotAcknowledgedMessagesResult()
		{
		}
		
		[Column(Storage="_IH_ID", DbType="Int")]
		public System.Nullable<int> IH_ID
		{
			get
			{
				return this._IH_ID;
			}
			set
			{
				if ((this._IH_ID != value))
				{
					this._IH_ID = value;
				}
			}
		}
		
		[Column(Storage="_IH_MESSAGE_ID", DbType="VarChar(50)")]
		public string IH_MESSAGE_ID
		{
			get
			{
				return this._IH_MESSAGE_ID;
			}
			set
			{
				if ((this._IH_MESSAGE_ID != value))
				{
					this._IH_MESSAGE_ID = value;
				}
			}
		}
		
		[Column(Storage="_IH_Type", DbType="VarChar(50)")]
		public string IH_Type
		{
			get
			{
				return this._IH_Type;
			}
			set
			{
				if ((this._IH_Type != value))
				{
					this._IH_Type = value;
				}
			}
		}
		
		[Column(Storage="_IH_Message", DbType="VarChar(8000)")]
		public string IH_Message
		{
			get
			{
				return this._IH_Message;
			}
			set
			{
				if ((this._IH_Message != value))
				{
					this._IH_Message = value;
				}
			}
		}
		
		[Column(Storage="_IH_Status", DbType="Int")]
		public System.Nullable<int> IH_Status
		{
			get
			{
				return this._IH_Status;
			}
			set
			{
				if ((this._IH_Status != value))
				{
					this._IH_Status = value;
				}
			}
		}
		
		[Column(Storage="_IH_Received_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> IH_Received_Date
		{
			get
			{
				return this._IH_Received_Date;
			}
			set
			{
				if ((this._IH_Received_Date != value))
				{
					this._IH_Received_Date = value;
				}
			}
		}
		
		[Column(Storage="_IH_Imported_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> IH_Imported_Date
		{
			get
			{
				return this._IH_Imported_Date;
			}
			set
			{
				if ((this._IH_Imported_Date != value))
				{
					this._IH_Imported_Date = value;
				}
			}
		}
		
		[Column(Storage="_IH_ExportResult", DbType="VarChar(100)")]
		public string IH_ExportResult
		{
			get
			{
				return this._IH_ExportResult;
			}
			set
			{
				if ((this._IH_ExportResult != value))
				{
					this._IH_ExportResult = value;
				}
			}
		}
		
		[Column(Storage="_EH_ID", DbType="Int NOT NULL")]
		public int EH_ID
		{
			get
			{
				return this._EH_ID;
			}
			set
			{
				if ((this._EH_ID != value))
				{
					this._EH_ID = value;
				}
			}
		}
		
		[Column(Storage="_EH_MESSAGE_ID", DbType="VarChar(50)")]
		public string EH_MESSAGE_ID
		{
			get
			{
				return this._EH_MESSAGE_ID;
			}
			set
			{
				if ((this._EH_MESSAGE_ID != value))
				{
					this._EH_MESSAGE_ID = value;
				}
			}
		}
		
		[Column(Storage="_EH_Recieved_Date", DbType="DateTime NOT NULL")]
		public System.DateTime EH_Recieved_Date
		{
			get
			{
				return this._EH_Recieved_Date;
			}
			set
			{
				if ((this._EH_Recieved_Date != value))
				{
					this._EH_Recieved_Date = value;
				}
			}
		}
		
		[Column(Storage="_EH_Exported_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> EH_Exported_Date
		{
			get
			{
				return this._EH_Exported_Date;
			}
			set
			{
				if ((this._EH_Exported_Date != value))
				{
					this._EH_Exported_Date = value;
				}
			}
		}
		
		[Column(Storage="_EH_Reference", DbType="VarChar(50)")]
		public string EH_Reference
		{
			get
			{
				return this._EH_Reference;
			}
			set
			{
				if ((this._EH_Reference != value))
				{
					this._EH_Reference = value;
				}
			}
		}
		
		[Column(Storage="_EH_Type", DbType="VarChar(30)")]
		public string EH_Type
		{
			get
			{
				return this._EH_Type;
			}
			set
			{
				if ((this._EH_Type != value))
				{
					this._EH_Type = value;
				}
			}
		}
		
		[Column(Storage="_EH_Status", DbType="VarChar(100)")]
		public string EH_Status
		{
			get
			{
				return this._EH_Status;
			}
			set
			{
				if ((this._EH_Status != value))
				{
					this._EH_Status = value;
				}
			}
		}
		
		[Column(Storage="_EH_Message", DbType="VarChar(8000)")]
		public string EH_Message
		{
			get
			{
				return this._EH_Message;
			}
			set
			{
				if ((this._EH_Message != value))
				{
					this._EH_Message = value;
				}
			}
		}
	}
	
	public partial class usp_InsUpd_Export_HistoryResult
	{
		
		private System.Nullable<int> _EH_ID;
		
		public usp_InsUpd_Export_HistoryResult()
		{
		}
		
		[Column(Storage="_EH_ID", DbType="Int")]
		public System.Nullable<int> EH_ID
		{
			get
			{
				return this._EH_ID;
			}
			set
			{
				if ((this._EH_ID != value))
				{
					this._EH_ID = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
