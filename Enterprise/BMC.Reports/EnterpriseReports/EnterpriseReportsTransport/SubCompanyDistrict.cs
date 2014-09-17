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
    [Table(Name = "dbo.Sub_Company_District")]
    public partial class SubCompanyDistrict : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Sub_Company_District_ID;

        private System.Nullable<int> _Sub_Company_Area_ID;

        private string _Sub_Company_District_Name;

        private System.Nullable<int> _Staff_ID;

        private string _Sub_Company_District_Description;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnSub_Company_District_IDChanging(int value);
        partial void OnSub_Company_District_IDChanged();
        partial void OnSub_Company_Area_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_Area_IDChanged();
        partial void OnSub_Company_District_NameChanging(string value);
        partial void OnSub_Company_District_NameChanged();
        partial void OnStaff_IDChanging(System.Nullable<int> value);
        partial void OnStaff_IDChanged();
        partial void OnSub_Company_District_DescriptionChanging(string value);
        partial void OnSub_Company_District_DescriptionChanged();
        #endregion

        public SubCompanyDistrict()
        {
            OnCreated();
        }

        [Column(Storage = "_Sub_Company_District_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Sub_Company_District_ID
        {
            get
            {
                return this._Sub_Company_District_ID;
            }
            set
            {
                if ((this._Sub_Company_District_ID != value))
                {
                    this.OnSub_Company_District_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_District_ID = value;
                    this.SendPropertyChanged("Sub_Company_District_ID");
                    this.OnSub_Company_District_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Area_ID
        {
            get
            {
                return this._Sub_Company_Area_ID;
            }
            set
            {
                if ((this._Sub_Company_Area_ID != value))
                {
                    this.OnSub_Company_Area_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_Area_ID = value;
                    this.SendPropertyChanged("Sub_Company_Area_ID");
                    this.OnSub_Company_Area_IDChanged();
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_Name", DbType = "VarChar(50)")]
        public string Sub_Company_District_Name
        {
            get
            {
                return this._Sub_Company_District_Name;
            }
            set
            {
                if ((this._Sub_Company_District_Name != value))
                {
                    this.OnSub_Company_District_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_District_Name = value;
                    this.SendPropertyChanged("Sub_Company_District_Name");
                    this.OnSub_Company_District_NameChanged();
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

        [Column(Storage = "_Sub_Company_District_Description", DbType = "VarChar(50)")]
        public string Sub_Company_District_Description
        {
            get
            {
                return this._Sub_Company_District_Description;
            }
            set
            {
                if ((this._Sub_Company_District_Description != value))
                {
                    this.OnSub_Company_District_DescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Sub_Company_District_Description = value;
                    this.SendPropertyChanged("Sub_Company_District_Description");
                    this.OnSub_Company_District_DescriptionChanged();
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
