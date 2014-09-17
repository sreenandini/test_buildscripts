using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace BMC.EnterpriseReportsTransport
{
    public class GetAllReportsToRolesAccess
    {

       private int _ReportID;
		
		private string _ParentName;
		
		private System.Nullable<int> _ParentID;
		
		private string _ReportName;
		
		private string _ReportDescription;
		
		private System.Nullable<int> _Level;
		
		private string _ApplicationServer;
		
		private string _ReportArgName;
		
		private System.Nullable<bool> _ReportStatus;
		
		private System.Nullable<bool> _ShowException;
		
		private System.Nullable<int> _SecurityRoleID;
		
		private string _MS_ProcedureUsed;

        private System.Nullable<bool> _ExportExcel;

        private string _XMLName;

        private System.Nullable<bool> _IsTimeRequired;
		
		
		
		[Column(Storage="_ReportID", DbType="Int NOT NULL")]
		public int ReportID
		{
			get
			{
				return this._ReportID;
			}
			set
			{
				if ((this._ReportID != value))
				{
					this._ReportID = value;
				}
			}
		}
		
		[Column(Storage="_ParentName", DbType="VarChar(100)")]
		public string ParentName
		{
			get
			{
				return this._ParentName;
			}
			set
			{
				if ((this._ParentName != value))
				{
					this._ParentName = value;
				}
			}
		}
		
		[Column(Storage="_ParentID", DbType="Int")]
		public System.Nullable<int> ParentID
		{
			get
			{
				return this._ParentID;
			}
			set
			{
				if ((this._ParentID != value))
				{
					this._ParentID = value;
				}
			}
		}
		
		[Column(Storage="_ReportName", DbType="VarChar(100)")]
		public string ReportName
		{
			get
			{
				return this._ReportName;
			}
			set
			{
				if ((this._ReportName != value))
				{
					this._ReportName = value;
				}
			}
		}
		
		[Column(Storage="_ReportDescription", DbType="VarChar(100)")]
		public string ReportDescription
		{
			get
			{
				return this._ReportDescription;
			}
			set
			{
				if ((this._ReportDescription != value))
				{
					this._ReportDescription = value;
				}
			}
		}
		
		[Column(Name="[Level]", Storage="_Level", DbType="Int")]
		public System.Nullable<int> Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if ((this._Level != value))
				{
					this._Level = value;
				}
			}
		}
		
		[Column(Storage="_ApplicationServer", DbType="VarChar(100)")]
		public string ApplicationServer
		{
			get
			{
				return this._ApplicationServer;
			}
			set
			{
				if ((this._ApplicationServer != value))
				{
					this._ApplicationServer = value;
				}
			}
		}
		
		[Column(Storage="_ReportArgName", DbType="VarChar(100)")]
		public string ReportArgName
		{
			get
			{
				return this._ReportArgName;
			}
			set
			{
				if ((this._ReportArgName != value))
				{
					this._ReportArgName = value;
				}
			}
		}
		
		[Column(Storage="_ReportStatus", DbType="Bit")]
		public System.Nullable<bool> ReportStatus
		{
			get
			{
				return this._ReportStatus;
			}
			set
			{
				if ((this._ReportStatus != value))
				{
					this._ReportStatus = value;
				}
			}
		}
		
		[Column(Storage="_ShowException", DbType="Bit")]
		public System.Nullable<bool> ShowException
		{
			get
			{
				return this._ShowException;
			}
			set
			{
				if ((this._ShowException != value))
				{
					this._ShowException = value;
				}
			}
		}
		
		[Column(Storage="_SecurityRoleID", DbType="Int")]
		public System.Nullable<int> SecurityRoleID
		{
			get
			{
				return this._SecurityRoleID;
			}
			set
			{
				if ((this._SecurityRoleID != value))
				{
					this._SecurityRoleID = value;
				}
			}
		}
		
		[Column(Storage="_MS_ProcedureUsed", DbType="VarChar(100)")]
		public string MS_ProcedureUsed
		{
			get
			{
				return this._MS_ProcedureUsed;
			}
			set
			{
				if ((this._MS_ProcedureUsed != value))
				{
					this._MS_ProcedureUsed = value;
				}
			}
		}
        [Column(Storage = "_ExportExcel", DbType = "Bit")]
        public System.Nullable<bool> ExportExcel
        {
            get
            {
                return this._ExportExcel;
            }
            set
            {
                if ((this._ExportExcel != value))
                {
                    this._ExportExcel = value;
                }
            }
        }
        [Column(Storage = "_XMLName", DbType = "VarChar(100)")]
        public string XMLName
        {
            get
            {
                return this._XMLName;
            }
            set
            {
                if ((this._XMLName != value))
                {
                    this._XMLName = value;
                }
            }
        }

        [Column(Storage = "_IsTimeRequired", DbType = "Bit")]
        public System.Nullable<bool> IsTimeRequired
        {
            get
            {
                return this._IsTimeRequired;
            }
            set
            {
                if ((this._IsTimeRequired != value))
                {
                    this._IsTimeRequired = value;
                }
            }
        }
    }
}
