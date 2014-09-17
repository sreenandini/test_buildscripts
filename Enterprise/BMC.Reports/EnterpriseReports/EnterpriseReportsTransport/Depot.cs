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
    [Table(Name = "dbo.Depot")]
    public partial class Depot : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Depot_ID;

        private string _Depot_Name;

        private string _Depot_Address;

        private string _Depot_Postcode;

        private string _Depot_Contact_Name;

        private string _Depot_AMEDIS_Depot_Code;

        private System.Nullable<int> _Supplier_ID;

        private string _Depot_Reference;

        private System.Nullable<bool> _Depot_Service;

        private string _Depot_Financial_Code;

        private string _Depot_Account_Name;

        private string _Depot_Sort_Code;

        private string _Depot_Account_No;

        private string _Depot_Phone_Number;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnDepot_IDChanging(int value);
        partial void OnDepot_IDChanged();
        partial void OnDepot_NameChanging(string value);
        partial void OnDepot_NameChanged();
        partial void OnDepot_AddressChanging(string value);
        partial void OnDepot_AddressChanged();
        partial void OnDepot_PostcodeChanging(string value);
        partial void OnDepot_PostcodeChanged();
        partial void OnDepot_Contact_NameChanging(string value);
        partial void OnDepot_Contact_NameChanged();
        partial void OnDepot_AMEDIS_Depot_CodeChanging(string value);
        partial void OnDepot_AMEDIS_Depot_CodeChanged();
        partial void OnSupplier_IDChanging(System.Nullable<int> value);
        partial void OnSupplier_IDChanged();
        partial void OnDepot_ReferenceChanging(string value);
        partial void OnDepot_ReferenceChanged();
        partial void OnDepot_ServiceChanging(System.Nullable<bool> value);
        partial void OnDepot_ServiceChanged();
        partial void OnDepot_Financial_CodeChanging(string value);
        partial void OnDepot_Financial_CodeChanged();
        partial void OnDepot_Account_NameChanging(string value);
        partial void OnDepot_Account_NameChanged();
        partial void OnDepot_Sort_CodeChanging(string value);
        partial void OnDepot_Sort_CodeChanged();
        partial void OnDepot_Account_NoChanging(string value);
        partial void OnDepot_Account_NoChanged();
        partial void OnDepot_Phone_NumberChanging(string value);
        partial void OnDepot_Phone_NumberChanged();
        #endregion

        public Depot()
        {
            OnCreated();
        }

        [Column(Storage = "_Depot_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this.OnDepot_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_ID = value;
                    this.SendPropertyChanged("Depot_ID");
                    this.OnDepot_IDChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this.OnDepot_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Name = value;
                    this.SendPropertyChanged("Depot_Name");
                    this.OnDepot_NameChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Depot_Address
        {
            get
            {
                return this._Depot_Address;
            }
            set
            {
                if ((this._Depot_Address != value))
                {
                    this.OnDepot_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Address = value;
                    this.SendPropertyChanged("Depot_Address");
                    this.OnDepot_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Postcode", DbType = "VarChar(10)")]
        public string Depot_Postcode
        {
            get
            {
                return this._Depot_Postcode;
            }
            set
            {
                if ((this._Depot_Postcode != value))
                {
                    this.OnDepot_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Postcode = value;
                    this.SendPropertyChanged("Depot_Postcode");
                    this.OnDepot_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Contact_Name", DbType = "VarChar(50)")]
        public string Depot_Contact_Name
        {
            get
            {
                return this._Depot_Contact_Name;
            }
            set
            {
                if ((this._Depot_Contact_Name != value))
                {
                    this.OnDepot_Contact_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Contact_Name = value;
                    this.SendPropertyChanged("Depot_Contact_Name");
                    this.OnDepot_Contact_NameChanged();
                }
            }
        }

        [Column(Storage = "_Depot_AMEDIS_Depot_Code", DbType = "VarChar(4)")]
        public string Depot_AMEDIS_Depot_Code
        {
            get
            {
                return this._Depot_AMEDIS_Depot_Code;
            }
            set
            {
                if ((this._Depot_AMEDIS_Depot_Code != value))
                {
                    this.OnDepot_AMEDIS_Depot_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_AMEDIS_Depot_Code = value;
                    this.SendPropertyChanged("Depot_AMEDIS_Depot_Code");
                    this.OnDepot_AMEDIS_Depot_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Supplier_ID", DbType = "Int")]
        public System.Nullable<int> Supplier_ID
        {
            get
            {
                return this._Supplier_ID;
            }
            set
            {
                if ((this._Supplier_ID != value))
                {
                    this.OnSupplier_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Supplier_ID = value;
                    this.SendPropertyChanged("Supplier_ID");
                    this.OnSupplier_IDChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Reference", DbType = "VarChar(20)")]
        public string Depot_Reference
        {
            get
            {
                return this._Depot_Reference;
            }
            set
            {
                if ((this._Depot_Reference != value))
                {
                    this.OnDepot_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Reference = value;
                    this.SendPropertyChanged("Depot_Reference");
                    this.OnDepot_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Service", DbType = "Bit")]
        public System.Nullable<bool> Depot_Service
        {
            get
            {
                return this._Depot_Service;
            }
            set
            {
                if ((this._Depot_Service != value))
                {
                    this.OnDepot_ServiceChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Service = value;
                    this.SendPropertyChanged("Depot_Service");
                    this.OnDepot_ServiceChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Financial_Code", DbType = "VarChar(20)")]
        public string Depot_Financial_Code
        {
            get
            {
                return this._Depot_Financial_Code;
            }
            set
            {
                if ((this._Depot_Financial_Code != value))
                {
                    this.OnDepot_Financial_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Financial_Code = value;
                    this.SendPropertyChanged("Depot_Financial_Code");
                    this.OnDepot_Financial_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Account_Name", DbType = "VarChar(32)")]
        public string Depot_Account_Name
        {
            get
            {
                return this._Depot_Account_Name;
            }
            set
            {
                if ((this._Depot_Account_Name != value))
                {
                    this.OnDepot_Account_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Account_Name = value;
                    this.SendPropertyChanged("Depot_Account_Name");
                    this.OnDepot_Account_NameChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Sort_Code", DbType = "VarChar(8)")]
        public string Depot_Sort_Code
        {
            get
            {
                return this._Depot_Sort_Code;
            }
            set
            {
                if ((this._Depot_Sort_Code != value))
                {
                    this.OnDepot_Sort_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Sort_Code = value;
                    this.SendPropertyChanged("Depot_Sort_Code");
                    this.OnDepot_Sort_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Account_No", DbType = "VarChar(12)")]
        public string Depot_Account_No
        {
            get
            {
                return this._Depot_Account_No;
            }
            set
            {
                if ((this._Depot_Account_No != value))
                {
                    this.OnDepot_Account_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Account_No = value;
                    this.SendPropertyChanged("Depot_Account_No");
                    this.OnDepot_Account_NoChanged();
                }
            }
        }

        [Column(Storage = "_Depot_Phone_Number", DbType = "VarChar(50)")]
        public string Depot_Phone_Number
        {
            get
            {
                return this._Depot_Phone_Number;
            }
            set
            {
                if ((this._Depot_Phone_Number != value))
                {
                    this.OnDepot_Phone_NumberChanging(value);
                    this.SendPropertyChanging();
                    this._Depot_Phone_Number = value;
                    this.SendPropertyChanged("Depot_Phone_Number");
                    this.OnDepot_Phone_NumberChanged();
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
