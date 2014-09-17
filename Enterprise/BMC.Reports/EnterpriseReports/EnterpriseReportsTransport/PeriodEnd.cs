using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace BMC.EnterpriseReportsTransport
{
    [Table(Name = "dbo.Period_End")]
    public partial class PeriodEnd : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Period_End_ID;

        private System.Nullable<int> _Sub_Company_ID;

        private string _Period_End_Setup_Date;

        private string _Period_End_Draft_Date;

        private string _Period_End_Final_Date;

        private System.Nullable<int> _Period_End_User;

        private string _Period_End_Description;

        private string _Period_End_Details;

        private System.Nullable<int> _Statement_No;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnPeriod_End_IDChanging(int value);
        partial void OnPeriod_End_IDChanged();
        partial void OnSub_Company_IDChanging(System.Nullable<int> value);
        partial void OnSub_Company_IDChanged();
        partial void OnPeriod_End_Setup_DateChanging(string value);
        partial void OnPeriod_End_Setup_DateChanged();
        partial void OnPeriod_End_Draft_DateChanging(string value);
        partial void OnPeriod_End_Draft_DateChanged();
        partial void OnPeriod_End_Final_DateChanging(string value);
        partial void OnPeriod_End_Final_DateChanged();
        partial void OnPeriod_End_UserChanging(System.Nullable<int> value);
        partial void OnPeriod_End_UserChanged();
        partial void OnPeriod_End_DescriptionChanging(string value);
        partial void OnPeriod_End_DescriptionChanged();
        partial void OnPeriod_End_DetailsChanging(string value);
        partial void OnPeriod_End_DetailsChanged();
        partial void OnStatement_NoChanging(System.Nullable<int> value);
        partial void OnStatement_NoChanged();
        #endregion

        public PeriodEnd()
        {
            OnCreated();
        }

        [Column(Storage = "_Period_End_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Period_End_ID
        {
            get
            {
                return this._Period_End_ID;
            }
            set
            {
                if ((this._Period_End_ID != value))
                {
                    this.OnPeriod_End_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_ID = value;
                    this.SendPropertyChanged("Period_End_ID");
                    this.OnPeriod_End_IDChanged();
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

        [Column(Storage = "_Period_End_Setup_Date", DbType = "VarChar(30)")]
        public string Period_End_Setup_Date
        {
            get
            {
                return this._Period_End_Setup_Date;
            }
            set
            {
                if ((this._Period_End_Setup_Date != value))
                {
                    this.OnPeriod_End_Setup_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_Setup_Date = value;
                    this.SendPropertyChanged("Period_End_Setup_Date");
                    this.OnPeriod_End_Setup_DateChanged();
                }
            }
        }

        [Column(Storage = "_Period_End_Draft_Date", DbType = "VarChar(30)")]
        public string Period_End_Draft_Date
        {
            get
            {
                return this._Period_End_Draft_Date;
            }
            set
            {
                if ((this._Period_End_Draft_Date != value))
                {
                    this.OnPeriod_End_Draft_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_Draft_Date = value;
                    this.SendPropertyChanged("Period_End_Draft_Date");
                    this.OnPeriod_End_Draft_DateChanged();
                }
            }
        }

        [Column(Storage = "_Period_End_Final_Date", DbType = "VarChar(30)")]
        public string Period_End_Final_Date
        {
            get
            {
                return this._Period_End_Final_Date;
            }
            set
            {
                if ((this._Period_End_Final_Date != value))
                {
                    this.OnPeriod_End_Final_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_Final_Date = value;
                    this.SendPropertyChanged("Period_End_Final_Date");
                    this.OnPeriod_End_Final_DateChanged();
                }
            }
        }

        [Column(Storage = "_Period_End_User", DbType = "Int")]
        public System.Nullable<int> Period_End_User
        {
            get
            {
                return this._Period_End_User;
            }
            set
            {
                if ((this._Period_End_User != value))
                {
                    this.OnPeriod_End_UserChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_User = value;
                    this.SendPropertyChanged("Period_End_User");
                    this.OnPeriod_End_UserChanged();
                }
            }
        }

        [Column(Storage = "_Period_End_Description", DbType = "VarChar(50)")]
        public string Period_End_Description
        {
            get
            {
                return this._Period_End_Description;
            }
            set
            {
                if ((this._Period_End_Description != value))
                {
                    this.OnPeriod_End_DescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_Description = value;
                    this.SendPropertyChanged("Period_End_Description");
                    this.OnPeriod_End_DescriptionChanged();
                }
            }
        }

        [Column(Storage = "_Period_End_Details", DbType = "VarChar(50)")]
        public string Period_End_Details
        {
            get
            {
                return this._Period_End_Details;
            }
            set
            {
                if ((this._Period_End_Details != value))
                {
                    this.OnPeriod_End_DetailsChanging(value);
                    this.SendPropertyChanging();
                    this._Period_End_Details = value;
                    this.SendPropertyChanged("Period_End_Details");
                    this.OnPeriod_End_DetailsChanged();
                }
            }
        }

        [Column(Storage = "_Statement_No", DbType = "Int")]
        public System.Nullable<int> Statement_No
        {
            get
            {
                return this._Statement_No;
            }
            set
            {
                if ((this._Statement_No != value))
                {
                    this.OnStatement_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Statement_No = value;
                    this.SendPropertyChanged("Statement_No");
                    this.OnStatement_NoChanged();
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
