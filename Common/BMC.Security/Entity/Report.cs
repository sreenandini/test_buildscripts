namespace BMC.Security.Entity
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
       

    [Table(Name = "dbo.ReportsMenuAccess")]
    public  class ReportsMenuAccess : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _SecurityRoleID;

        private int _ReportID;

        private EntityRef<ReportsMenu> _ReportsMenu;

        #region Extensibility Method Definitions
         //void OnLoaded();
         //void OnValidate(System.Data.Linq.ChangeAction action);
         //void OnCreated();
         //void OnSecurityRoleIDChanging(int value);
         //void OnSecurityRoleIDChanged();
         //void OnReportIDChanging(int value);
         //void OnReportIDChanged();
        #endregion

        public ReportsMenuAccess()
        {
            this._ReportsMenu = default(EntityRef<ReportsMenu>);
           // OnCreated();
        }

        [Column(Storage = "_SecurityRoleID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int SecurityRoleID
        {
            get
            {
                return this._SecurityRoleID;
            }
            set
            {
                if ((this._SecurityRoleID != value))
                {
                   // this.OnSecurityRoleIDChanging(value);
                    this.SendPropertyChanging();
                    this._SecurityRoleID = value;
                    this.SendPropertyChanged("SecurityRoleID");
                    //this.OnSecurityRoleIDChanged();
                }
            }
        }

        [Column(Storage = "_ReportID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
                    if (this._ReportsMenu.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    //this.OnReportIDChanging(value);
                    this.SendPropertyChanging();
                    this._ReportID = value;
                    this.SendPropertyChanged("ReportID");
                   // this.OnReportIDChanged();
                }
            }
        }

        [Association(Name = "ReportsMenu_ReportsMenuAccess", Storage = "_ReportsMenu", ThisKey = "ReportID", OtherKey = "ReportID", IsForeignKey = true)]
        public ReportsMenu ReportsMenu
        {
            get
            {
                return this._ReportsMenu.Entity;
            }
            set
            {
                ReportsMenu previousValue = this._ReportsMenu.Entity;
                if (((previousValue != value)
                            || (this._ReportsMenu.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ReportsMenu.Entity = null;
                        previousValue.ReportsMenuAccesses.Remove(this);
                    }
                    this._ReportsMenu.Entity = value;
                    if ((value != null))
                    {
                        value.ReportsMenuAccesses.Add(this);
                        this._ReportID = value.ReportID;
                    }
                    else
                    {
                        this._ReportID = default(int);
                    }
                    this.SendPropertyChanged("ReportsMenu");
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

    [Table(Name = "dbo.ReportsMenu")]
    public  class ReportsMenu : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _ReportID;

        private System.Nullable<int> _ReportMenuID;

        private System.Nullable<int> _Level;

        private string _ReportName;

        private string _ReportDescription;

        private string _ApplicationServer;

        private string _ReportArgName;

        private System.Nullable<bool> _ReportStatus;

        private System.Nullable<bool> _ShowException;

        private string _MS_ProcedureUsed;

        private EntitySet<ReportsMenuAccess> _ReportsMenuAccesses;

       

        #region Extensibility Method Definitions
         //void OnLoaded();
         //void OnValidate(System.Data.Linq.ChangeAction action);
         //void OnCreated();
         //void OnReportIDChanging(int value);
         //void OnReportIDChanged();
         //void OnReportMenuIDChanging(System.Nullable<int> value);
         //void OnReportMenuIDChanged();
         //void OnLevelChanging(System.Nullable<int> value);
         //void OnLevelChanged();
         //void OnReportNameChanging(string value);
         //void OnReportNameChanged();
         //void OnReportDescriptionChanging(string value);
         //void OnReportDescriptionChanged();
         //void OnApplicationServerChanging(string value);
         //void OnApplicationServerChanged();
         //void OnReportArgNameChanging(string value);
         //void OnReportArgNameChanged();
         //void OnReportStatusChanging(System.Nullable<bool> value);
         //void OnReportStatusChanged();
         //void OnShowExceptionChanging(System.Nullable<bool> value);
         //void OnShowExceptionChanged();
         //void OnMS_ProcedureUsedChanging(string value);
         //void OnMS_ProcedureUsedChanged();
        #endregion

        public ReportsMenu()
        {
            this._ReportsMenuAccesses = new EntitySet<ReportsMenuAccess>(new Action<ReportsMenuAccess>(this.attach_ReportsMenuAccesses), new Action<ReportsMenuAccess>(this.detach_ReportsMenuAccesses));
                      //OnCreated();
        }

        [Column(Storage = "_ReportID", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
                   // this.OnReportIDChanging(value);
                    this.SendPropertyChanging();
                    this._ReportID = value;
                    this.SendPropertyChanged("ReportID");
                   // this.OnReportIDChanged();
                }
            }
        }

        [Column(Storage = "_ReportMenuID", DbType = "Int")]
        public System.Nullable<int> ReportMenuID
        {
            get
            {
                return this._ReportMenuID;
            }
            set
            {
                if ((this._ReportMenuID != value))
                {
                    
                   // this.OnReportMenuIDChanging(value);
                    this.SendPropertyChanging();
                    this._ReportMenuID = value;
                    this.SendPropertyChanged("ReportMenuID");
                  //  this.OnReportMenuIDChanged();
                }
            }
        }

        [Column(Name = "[Level]", Storage = "_Level", DbType = "Int")]
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
                   // this.OnLevelChanging(value);
                    this.SendPropertyChanging();
                    this._Level = value;
                    this.SendPropertyChanged("Level");
                  //  this.OnLevelChanged();
                }
            }
        }

        [Column(Storage = "_ReportName", DbType = "VarChar(100)")]
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
                   // this.OnReportNameChanging(value);
                    this.SendPropertyChanging();
                    this._ReportName = value;
                    this.SendPropertyChanged("ReportName");
                   // this.OnReportNameChanged();
                }
            }
        }

        [Column(Storage = "_ReportDescription", DbType = "VarChar(100)")]
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
                   // this.OnReportDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._ReportDescription = value;
                    this.SendPropertyChanged("ReportDescription");
                   // this.OnReportDescriptionChanged();
                }
            }
        }

        [Column(Storage = "_ApplicationServer", DbType = "VarChar(100)")]
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
                    //this.OnApplicationServerChanging(value);
                    this.SendPropertyChanging();
                    this._ApplicationServer = value;
                    this.SendPropertyChanged("ApplicationServer");
                    //this.OnApplicationServerChanged();
                }
            }
        }

        [Column(Storage = "_ReportArgName", DbType = "VarChar(100)")]
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
                    //this.OnReportArgNameChanging(value);
                    this.SendPropertyChanging();
                    this._ReportArgName = value;
                    this.SendPropertyChanged("ReportArgName");
                    //this.OnReportArgNameChanged();
                }
            }
        }

        [Column(Storage = "_ReportStatus", DbType = "Bit")]
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
                   // this.OnReportStatusChanging(value);
                    this.SendPropertyChanging();
                    this._ReportStatus = value;
                    this.SendPropertyChanged("ReportStatus");
                   // this.OnReportStatusChanged();
                }
            }
        }

        [Column(Storage = "_ShowException", DbType = "Bit")]
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
                    //this.OnShowExceptionChanging(value);
                    this.SendPropertyChanging();
                    this._ShowException = value;
                    this.SendPropertyChanged("ShowException");
                   // this.OnShowExceptionChanged();
                }
            }
        }

        [Column(Storage = "_MS_ProcedureUsed", DbType = "VarChar(100)")]
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
                   // this.OnMS_ProcedureUsedChanging(value);
                    this.SendPropertyChanging();
                    this._MS_ProcedureUsed = value;
                    this.SendPropertyChanged("MS_ProcedureUsed");
                   // this.OnMS_ProcedureUsedChanged();
                }
            }
        }

        [Association(Name = "ReportsMenu_ReportsMenuAccess", Storage = "_ReportsMenuAccesses", ThisKey = "ReportID", OtherKey = "ReportID")]
        public EntitySet<ReportsMenuAccess> ReportsMenuAccesses
        {
            get
            {
                return this._ReportsMenuAccesses;
            }
            set
            {
                this._ReportsMenuAccesses.Assign(value);
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

        private void attach_ReportsMenuAccesses(ReportsMenuAccess entity)
        {
            this.SendPropertyChanging();
            entity.ReportsMenu = this;
        }

        private void detach_ReportsMenuAccesses(ReportsMenuAccess entity)
        {
            this.SendPropertyChanging();
            entity.ReportsMenu = null;
        }
    }

    public  class GetRoleAccessForReport
    {

     	
		private int _ReportID;
		
		private string _ParentName;
		
		private System.Nullable<int> _ReportMenuID;
		
		private string _ReportName;
		
		private string _ReportDescription;

        private System.Nullable<bool> _ReportStatus;

        public GetRoleAccessForReport()
		{
		}
		
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
		
		[Column(Storage="_ReportMenuID", DbType="Int")]
		public System.Nullable<int> ReportMenuID
		{
			get
			{
				return this._ReportMenuID;
			}
			set
			{
				if ((this._ReportMenuID != value))
				{
					this._ReportMenuID = value;
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

        [Column(Storage = "_ReportStatus", DbType = "Bit")]
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
    }

    public  class GetALLReportMenu
    {

        private int _ReportID;
		
		private string _ParentName;
		
		private System.Nullable<int> _ParentID;
		
		private string _ReportName;
		
		private string _ReportDescription;

        public GetALLReportMenu()
		{
		}
		
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
    }
    
}