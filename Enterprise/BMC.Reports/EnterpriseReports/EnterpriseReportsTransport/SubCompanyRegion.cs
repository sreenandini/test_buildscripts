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
    [Table(Name = "dbo.Sub_Company_Region")]
    public partial class SubCompanyRegion : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Sub_Company_Region_ID;

        private System.Nullable<int> _Sub_Company_ID;

        private string _Sub_Company_Region_Name;

        private System.Nullable<int> _Staff_ID;

        private int _Company_ID;

        private string _Sub_Company_Region_Description;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnSub_Company_Region_IDChanging(int value);
        partial void OnSub_Company_Region_IDChanged();
        partial void OnSub_Company_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_IDChanged();
        partial void OnSub_Company_Region_NameChanging(string value);
        partial void OnSub_Company_Region_NameChanged();
        partial void OnStaff_IDChanging(System.Nullable<int> value);
        partial void OnStaff_IDChanged();
        partial void OnCompany_IDChanging(int value);
        partial void OnCompany_IDChanged();
        partial void OnSub_Company_Region_DescriptionChanging(string value);
        partial void OnSub_Company_Region_DescriptionChanged();
        #endregion

        public SubCompanyRegion()
        {
            OnCreated();
        }

        [Column(Storage = "_Sub_Company_Region_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Sub_Company_Region_ID
        {
            get
            {
                return this._Sub_Company_Region_ID;
            }
            set
            {
                if ((this._Sub_Company_Region_ID != value))
                {
                    this.OnSub_Company_Region_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Region_ID = value;
                    this.SendPropertyChanged("Sub_Company_Region_ID");
                    this.OnSub_Company_Region_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this.OnSub_Company_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_ID = value;
                    this.SendPropertyChanged("Sub_Company_ID");
                    this.OnSub_Company_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
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
                    this.OnSub_Company_Region_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Region_Name = value;
                    this.SendPropertyChanged("Sub_Company_Region_Name");
                    this.OnSub_Company_Region_NameChanged();
                }
            }
        }

        [Column(Storage = "_Staff_ID", DbType = "Int")]
        public System.Nullable<int> Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this.OnStaff_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Staff_ID = value;
                    this.SendPropertyChanged("Staff_ID");
                    this.OnStaff_IDChanged();
                }
            }
        }

        [Column(Storage = "_Company_ID", DbType = "Int NOT NULL")]
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this.OnCompany_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Company_ID = value;
                    this.SendPropertyChanged("Company_ID");
                    this.OnCompany_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_Description", DbType = "VarChar(50)")]
        public string Sub_Company_Region_Description
        {
            get
            {
                return this._Sub_Company_Region_Description;
            }
            set
            {
                if ((this._Sub_Company_Region_Description != value))
                {
                    this.OnSub_Company_Region_DescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Region_Description = value;
                    this.SendPropertyChanged("Sub_Company_Region_Description");
                    this.OnSub_Company_Region_DescriptionChanged();
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
}
