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
    [Table(Name = "dbo.Zone")]
    public partial class Zone : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Zone_ID;

        private System.Nullable<int> _Site_ID;

        private string _Zone_Name;

        private string _Zone_Description;

        private string _Zone_Start_Date;

        private string _Zone_End_Date;

        private string _Zone_Price_Per_Play_Default;

        private string _Zone_Jackpot_Default;

        private System.Nullable<int> _Standard_Opening_Hours_ID;

        private string _Zone_Open_Monday;

        private string _Zone_Open_Tuesday;

        private string _Zone_Open_Wednesday;

        private string _Zone_Open_Thursday;

        private string _Zone_Open_Friday;

        private string _Zone_Open_Saturday;

        private string _Zone_Open_Sunday;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnZone_IDChanging(int value);
        partial void OnZone_IDChanged();
        partial void OnSite_IDChanging(System.Nullable<int> value);
        partial void OnSite_IDChanged();
        partial void OnZone_NameChanging(string value);
        partial void OnZone_NameChanged();
        partial void OnZone_DescriptionChanging(string value);
        partial void OnZone_DescriptionChanged();
        partial void OnZone_Start_DateChanging(string value);
        partial void OnZone_Start_DateChanged();
        partial void OnZone_End_DateChanging(string value);
        partial void OnZone_End_DateChanged();
        partial void OnZone_Price_Per_Play_DefaultChanging(string value);
        partial void OnZone_Price_Per_Play_DefaultChanged();
        partial void OnZone_Jackpot_DefaultChanging(string value);
        partial void OnZone_Jackpot_DefaultChanged();
        partial void OnStandard_Opening_Hours_IDChanging(System.Nullable<int> value);
        partial void OnStandard_Opening_Hours_IDChanged();
        partial void OnZone_Open_MondayChanging(string value);
        partial void OnZone_Open_MondayChanged();
        partial void OnZone_Open_TuesdayChanging(string value);
        partial void OnZone_Open_TuesdayChanged();
        partial void OnZone_Open_WednesdayChanging(string value);
        partial void OnZone_Open_WednesdayChanged();
        partial void OnZone_Open_ThursdayChanging(string value);
        partial void OnZone_Open_ThursdayChanged();
        partial void OnZone_Open_FridayChanging(string value);
        partial void OnZone_Open_FridayChanged();
        partial void OnZone_Open_SaturdayChanging(string value);
        partial void OnZone_Open_SaturdayChanged();
        partial void OnZone_Open_SundayChanging(string value);
        partial void OnZone_Open_SundayChanged();
        #endregion

        public Zone()
        {
            OnCreated();
        }

        [Column(Storage = "_Zone_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Zone_ID
        {
            get
            {
                return this._Zone_ID;
            }
            set
            {
                if ((this._Zone_ID != value))
                {
                    this.OnZone_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_ID = value;
                    this.SendPropertyChanged("Zone_ID");
                    this.OnZone_IDChanged();
                }
            }
        }

        [Column(Storage = "_Site_ID", DbType = "Int")]
        public System.Nullable<int> Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this.OnSite_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Site_ID = value;
                    this.SendPropertyChanged("Site_ID");
                    this.OnSite_IDChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this.OnZone_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Name = value;
                    this.SendPropertyChanged("Zone_Name");
                    this.OnZone_NameChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Description", DbType = "VarChar(50)")]
        public string Zone_Description
        {
            get
            {
                return this._Zone_Description;
            }
            set
            {
                if ((this._Zone_Description != value))
                {
                    this.OnZone_DescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Description = value;
                    this.SendPropertyChanged("Zone_Description");
                    this.OnZone_DescriptionChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Start_Date", DbType = "VarChar(30)")]
        public string Zone_Start_Date
        {
            get
            {
                return this._Zone_Start_Date;
            }
            set
            {
                if ((this._Zone_Start_Date != value))
                {
                    this.OnZone_Start_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Start_Date = value;
                    this.SendPropertyChanged("Zone_Start_Date");
                    this.OnZone_Start_DateChanged();
                }
            }
        }

        [Column(Storage = "_Zone_End_Date", DbType = "VarChar(30)")]
        public string Zone_End_Date
        {
            get
            {
                return this._Zone_End_Date;
            }
            set
            {
                if ((this._Zone_End_Date != value))
                {
                    this.OnZone_End_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_End_Date = value;
                    this.SendPropertyChanged("Zone_End_Date");
                    this.OnZone_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Price_Per_Play_Default", DbType = "VarChar(50)")]
        public string Zone_Price_Per_Play_Default
        {
            get
            {
                return this._Zone_Price_Per_Play_Default;
            }
            set
            {
                if ((this._Zone_Price_Per_Play_Default != value))
                {
                    this.OnZone_Price_Per_Play_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Price_Per_Play_Default = value;
                    this.SendPropertyChanged("Zone_Price_Per_Play_Default");
                    this.OnZone_Price_Per_Play_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Jackpot_Default", DbType = "VarChar(50)")]
        public string Zone_Jackpot_Default
        {
            get
            {
                return this._Zone_Jackpot_Default;
            }
            set
            {
                if ((this._Zone_Jackpot_Default != value))
                {
                    this.OnZone_Jackpot_DefaultChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Jackpot_Default = value;
                    this.SendPropertyChanged("Zone_Jackpot_Default");
                    this.OnZone_Jackpot_DefaultChanged();
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
        public System.Nullable<int> Standard_Opening_Hours_ID
        {
            get
            {
                return this._Standard_Opening_Hours_ID;
            }
            set
            {
                if ((this._Standard_Opening_Hours_ID != value))
                {
                    this.OnStandard_Opening_Hours_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Standard_Opening_Hours_ID = value;
                    this.SendPropertyChanged("Standard_Opening_Hours_ID");
                    this.OnStandard_Opening_Hours_IDChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Monday", DbType = "VarChar(96)")]
        public string Zone_Open_Monday
        {
            get
            {
                return this._Zone_Open_Monday;
            }
            set
            {
                if ((this._Zone_Open_Monday != value))
                {
                    this.OnZone_Open_MondayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Monday = value;
                    this.SendPropertyChanged("Zone_Open_Monday");
                    this.OnZone_Open_MondayChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Tuesday", DbType = "VarChar(96)")]
        public string Zone_Open_Tuesday
        {
            get
            {
                return this._Zone_Open_Tuesday;
            }
            set
            {
                if ((this._Zone_Open_Tuesday != value))
                {
                    this.OnZone_Open_TuesdayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Tuesday = value;
                    this.SendPropertyChanged("Zone_Open_Tuesday");
                    this.OnZone_Open_TuesdayChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Wednesday", DbType = "VarChar(96)")]
        public string Zone_Open_Wednesday
        {
            get
            {
                return this._Zone_Open_Wednesday;
            }
            set
            {
                if ((this._Zone_Open_Wednesday != value))
                {
                    this.OnZone_Open_WednesdayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Wednesday = value;
                    this.SendPropertyChanged("Zone_Open_Wednesday");
                    this.OnZone_Open_WednesdayChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Thursday", DbType = "VarChar(96)")]
        public string Zone_Open_Thursday
        {
            get
            {
                return this._Zone_Open_Thursday;
            }
            set
            {
                if ((this._Zone_Open_Thursday != value))
                {
                    this.OnZone_Open_ThursdayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Thursday = value;
                    this.SendPropertyChanged("Zone_Open_Thursday");
                    this.OnZone_Open_ThursdayChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Friday", DbType = "VarChar(96)")]
        public string Zone_Open_Friday
        {
            get
            {
                return this._Zone_Open_Friday;
            }
            set
            {
                if ((this._Zone_Open_Friday != value))
                {
                    this.OnZone_Open_FridayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Friday = value;
                    this.SendPropertyChanged("Zone_Open_Friday");
                    this.OnZone_Open_FridayChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Saturday", DbType = "VarChar(96)")]
        public string Zone_Open_Saturday
        {
            get
            {
                return this._Zone_Open_Saturday;
            }
            set
            {
                if ((this._Zone_Open_Saturday != value))
                {
                    this.OnZone_Open_SaturdayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Saturday = value;
                    this.SendPropertyChanged("Zone_Open_Saturday");
                    this.OnZone_Open_SaturdayChanged();
                }
            }
        }

        [Column(Storage = "_Zone_Open_Sunday", DbType = "VarChar(96)")]
        public string Zone_Open_Sunday
        {
            get
            {
                return this._Zone_Open_Sunday;
            }
            set
            {
                if ((this._Zone_Open_Sunday != value))
                {
                    this.OnZone_Open_SundayChanging(value);
                    this.SendPropertyChanging();
                    this._Zone_Open_Sunday = value;
                    this.SendPropertyChanged("Zone_Open_Sunday");
                    this.OnZone_Open_SundayChanged();
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
