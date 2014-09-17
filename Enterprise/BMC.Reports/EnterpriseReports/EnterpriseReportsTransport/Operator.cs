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
    [Table(Name = "dbo.Operator")]
    public partial class Operator : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Operator_ID;

        private System.Nullable<int> _Calendar_ID;

        private string _Operator_Name;

        private string _Operator_Address;

        private string _Operator_PostCode;

        private string _Operator_Depot_Phone;

        private string _Operator_Fax;

        private string _Operator_EMail;

        private string _Operator_Contact;

        private string _Operator_Invoice_Address;

        private string _Operator_Invoice_Postcode;

        private string _Operator_Invoice_Name;

        private string _Operator_Start_Date;

        private string _Operator_End_Date;

        private string _Operator_AMEDIS_Code;

        private string _Operator_Logo_Reference;

        private string _Operator_Account_Name;

        private string _Operator_Sort_Code;

        private string _Operator_Account_No;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnOperator_IDChanging(int value);
        partial void OnOperator_IDChanged();
        partial void OnCalendar_IDChanging(System.Nullable<int> value);
        partial void OnCalendar_IDChanged();
        partial void OnOperator_NameChanging(string value);
        partial void OnOperator_NameChanged();
        partial void OnOperator_AddressChanging(string value);
        partial void OnOperator_AddressChanged();
        partial void OnOperator_PostCodeChanging(string value);
        partial void OnOperator_PostCodeChanged();
        partial void OnOperator_Depot_PhoneChanging(string value);
        partial void OnOperator_Depot_PhoneChanged();
        partial void OnOperator_FaxChanging(string value);
        partial void OnOperator_FaxChanged();
        partial void OnOperator_EMailChanging(string value);
        partial void OnOperator_EMailChanged();
        partial void OnOperator_ContactChanging(string value);
        partial void OnOperator_ContactChanged();
        partial void OnOperator_Invoice_AddressChanging(string value);
        partial void OnOperator_Invoice_AddressChanged();
        partial void OnOperator_Invoice_PostcodeChanging(string value);
        partial void OnOperator_Invoice_PostcodeChanged();
        partial void OnOperator_Invoice_NameChanging(string value);
        partial void OnOperator_Invoice_NameChanged();
        partial void OnOperator_Start_DateChanging(string value);
        partial void OnOperator_Start_DateChanged();
        partial void OnOperator_End_DateChanging(string value);
        partial void OnOperator_End_DateChanged();
        partial void OnOperator_AMEDIS_CodeChanging(string value);
        partial void OnOperator_AMEDIS_CodeChanged();
        partial void OnOperator_Logo_ReferenceChanging(string value);
        partial void OnOperator_Logo_ReferenceChanged();
        partial void OnOperator_Account_NameChanging(string value);
        partial void OnOperator_Account_NameChanged();
        partial void OnOperator_Sort_CodeChanging(string value);
        partial void OnOperator_Sort_CodeChanged();
        partial void OnOperator_Account_NoChanging(string value);
        partial void OnOperator_Account_NoChanged();
        #endregion

        public Operator()
        {
            OnCreated();
        }

        [Column(Storage = "_Operator_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this.OnOperator_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_ID = value;
                    this.SendPropertyChanged("Operator_ID");
                    this.OnOperator_IDChanged();
                }
            }
        }

        [Column(Storage = "_Calendar_ID", DbType = "Int")]
        public System.Nullable<int> Calendar_ID
        {
            get
            {
                return this._Calendar_ID;
            }
            set
            {
                if ((this._Calendar_ID != value))
                {
                    this.OnCalendar_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Calendar_ID = value;
                    this.SendPropertyChanged("Calendar_ID");
                    this.OnCalendar_IDChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this.OnOperator_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Name = value;
                    this.SendPropertyChanged("Operator_Name");
                    this.OnOperator_NameChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Operator_Address
        {
            get
            {
                return this._Operator_Address;
            }
            set
            {
                if ((this._Operator_Address != value))
                {
                    this.OnOperator_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Address = value;
                    this.SendPropertyChanged("Operator_Address");
                    this.OnOperator_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Operator_PostCode", DbType = "VarChar(15)")]
        public string Operator_PostCode
        {
            get
            {
                return this._Operator_PostCode;
            }
            set
            {
                if ((this._Operator_PostCode != value))
                {
                    this.OnOperator_PostCodeChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_PostCode = value;
                    this.SendPropertyChanged("Operator_PostCode");
                    this.OnOperator_PostCodeChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Depot_Phone", DbType = "VarChar(15)")]
        public string Operator_Depot_Phone
        {
            get
            {
                return this._Operator_Depot_Phone;
            }
            set
            {
                if ((this._Operator_Depot_Phone != value))
                {
                    this.OnOperator_Depot_PhoneChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Depot_Phone = value;
                    this.SendPropertyChanged("Operator_Depot_Phone");
                    this.OnOperator_Depot_PhoneChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Fax", DbType = "VarChar(15)")]
        public string Operator_Fax
        {
            get
            {
                return this._Operator_Fax;
            }
            set
            {
                if ((this._Operator_Fax != value))
                {
                    this.OnOperator_FaxChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Fax = value;
                    this.SendPropertyChanged("Operator_Fax");
                    this.OnOperator_FaxChanged();
                }
            }
        }

        [Column(Storage = "_Operator_EMail", DbType = "VarChar(100)")]
        public string Operator_EMail
        {
            get
            {
                return this._Operator_EMail;
            }
            set
            {
                if ((this._Operator_EMail != value))
                {
                    this.OnOperator_EMailChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_EMail = value;
                    this.SendPropertyChanged("Operator_EMail");
                    this.OnOperator_EMailChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Contact", DbType = "VarChar(50)")]
        public string Operator_Contact
        {
            get
            {
                return this._Operator_Contact;
            }
            set
            {
                if ((this._Operator_Contact != value))
                {
                    this.OnOperator_ContactChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Contact = value;
                    this.SendPropertyChanged("Operator_Contact");
                    this.OnOperator_ContactChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Operator_Invoice_Address
        {
            get
            {
                return this._Operator_Invoice_Address;
            }
            set
            {
                if ((this._Operator_Invoice_Address != value))
                {
                    this.OnOperator_Invoice_AddressChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Invoice_Address = value;
                    this.SendPropertyChanged("Operator_Invoice_Address");
                    this.OnOperator_Invoice_AddressChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Postcode", DbType = "VarChar(50)")]
        public string Operator_Invoice_Postcode
        {
            get
            {
                return this._Operator_Invoice_Postcode;
            }
            set
            {
                if ((this._Operator_Invoice_Postcode != value))
                {
                    this.OnOperator_Invoice_PostcodeChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Invoice_Postcode = value;
                    this.SendPropertyChanged("Operator_Invoice_Postcode");
                    this.OnOperator_Invoice_PostcodeChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Invoice_Name", DbType = "VarChar(50)")]
        public string Operator_Invoice_Name
        {
            get
            {
                return this._Operator_Invoice_Name;
            }
            set
            {
                if ((this._Operator_Invoice_Name != value))
                {
                    this.OnOperator_Invoice_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Invoice_Name = value;
                    this.SendPropertyChanged("Operator_Invoice_Name");
                    this.OnOperator_Invoice_NameChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Start_Date", DbType = "VarChar(30)")]
        public string Operator_Start_Date
        {
            get
            {
                return this._Operator_Start_Date;
            }
            set
            {
                if ((this._Operator_Start_Date != value))
                {
                    this.OnOperator_Start_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Start_Date = value;
                    this.SendPropertyChanged("Operator_Start_Date");
                    this.OnOperator_Start_DateChanged();
                }
            }
        }

        [Column(Storage = "_Operator_End_Date", DbType = "VarChar(30)")]
        public string Operator_End_Date
        {
            get
            {
                return this._Operator_End_Date;
            }
            set
            {
                if ((this._Operator_End_Date != value))
                {
                    this.OnOperator_End_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_End_Date = value;
                    this.SendPropertyChanged("Operator_End_Date");
                    this.OnOperator_End_DateChanged();
                }
            }
        }

        [Column(Storage = "_Operator_AMEDIS_Code", DbType = "VarChar(4)")]
        public string Operator_AMEDIS_Code
        {
            get
            {
                return this._Operator_AMEDIS_Code;
            }
            set
            {
                if ((this._Operator_AMEDIS_Code != value))
                {
                    this.OnOperator_AMEDIS_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_AMEDIS_Code = value;
                    this.SendPropertyChanged("Operator_AMEDIS_Code");
                    this.OnOperator_AMEDIS_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Logo_Reference", DbType = "VarChar(50)")]
        public string Operator_Logo_Reference
        {
            get
            {
                return this._Operator_Logo_Reference;
            }
            set
            {
                if ((this._Operator_Logo_Reference != value))
                {
                    this.OnOperator_Logo_ReferenceChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Logo_Reference = value;
                    this.SendPropertyChanged("Operator_Logo_Reference");
                    this.OnOperator_Logo_ReferenceChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Account_Name", DbType = "VarChar(32)")]
        public string Operator_Account_Name
        {
            get
            {
                return this._Operator_Account_Name;
            }
            set
            {
                if ((this._Operator_Account_Name != value))
                {
                    this.OnOperator_Account_NameChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Account_Name = value;
                    this.SendPropertyChanged("Operator_Account_Name");
                    this.OnOperator_Account_NameChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Sort_Code", DbType = "VarChar(8)")]
        public string Operator_Sort_Code
        {
            get
            {
                return this._Operator_Sort_Code;
            }
            set
            {
                if ((this._Operator_Sort_Code != value))
                {
                    this.OnOperator_Sort_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Sort_Code = value;
                    this.SendPropertyChanged("Operator_Sort_Code");
                    this.OnOperator_Sort_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Account_No", DbType = "VarChar(12)")]
        public string Operator_Account_No
        {
            get
            {
                return this._Operator_Account_No;
            }
            set
            {
                if ((this._Operator_Account_No != value))
                {
                    this.OnOperator_Account_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Account_No = value;
                    this.SendPropertyChanged("Operator_Account_No");
                    this.OnOperator_Account_NoChanged();
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
