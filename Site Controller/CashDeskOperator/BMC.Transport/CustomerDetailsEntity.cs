using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;

namespace BMC.Transport
{
    [Table(Name = "dbo.Customer")]
    public partial class Customer : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private long _CustomerID;

        private string _Title;

        private string _FirstName;

        private string _MiddleName;

        private string _LastName;

        private string _ADDRESS1;

        private string _ADDRESS2;

        private string _ADDRESS3;

        private string _PinCode;

        private string _BankAccNo;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnCustomerIDChanging(long value);
        partial void OnCustomerIDChanged();
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
        partial void OnFirstNameChanging(string value);
        partial void OnFirstNameChanged();
        partial void OnMiddleNameChanging(string value);
        partial void OnMiddleNameChanged();
        partial void OnLastNameChanging(string value);
        partial void OnLastNameChanged();
        partial void OnADDRESS1Changing(string value);
        partial void OnADDRESS1Changed();
        partial void OnADDRESS2Changing(string value);
        partial void OnADDRESS2Changed();
        partial void OnADDRESS3Changing(string value);
        partial void OnADDRESS3Changed();
        partial void OnPinCodeChanging(string value);
        partial void OnPinCodeChanged();
        partial void OnBankAccNoChanging(string value);
        partial void OnBankAccNoChanged();
        #endregion

        public Customer()
        {
            OnCreated();
        }

        [Column(Storage = "_CustomerID", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public long CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                if ((this._CustomerID != value))
                {
                    this.OnCustomerIDChanging(value);
                    this.SendPropertyChanging();
                    this._CustomerID = value;
                    this.SendPropertyChanged("CustomerID");
                    this.OnCustomerIDChanged();
                }
            }
        }

        [Column(Storage = "_Title", DbType = "VarChar(10)")]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                if ((this._Title != value))
                {
                    this.OnTitleChanging(value);
                    this.SendPropertyChanging();
                    this._Title = value;
                    this.SendPropertyChanged("Title");
                    this.OnTitleChanged();
                }
            }
        }

        [Column(Storage = "_FirstName", DbType = "VarChar(25)")]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this.OnFirstNameChanging(value);
                    this.SendPropertyChanging();
                    this._FirstName = value;
                    this.SendPropertyChanged("FirstName");
                    this.OnFirstNameChanged();
                }
            }
        }

        [Column(Storage = "_MiddleName", DbType = "VarChar(25)")]
        public string MiddleName
        {
            get
            {
                return this._MiddleName;
            }
            set
            {
                if ((this._MiddleName != value))
                {
                    this.OnMiddleNameChanging(value);
                    this.SendPropertyChanging();
                    this._MiddleName = value;
                    this.SendPropertyChanged("MiddleName");
                    this.OnMiddleNameChanged();
                }
            }
        }

        [Column(Storage = "_LastName", DbType = "VarChar(25)")]
        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this.OnLastNameChanging(value);
                    this.SendPropertyChanging();
                    this._LastName = value;
                    this.SendPropertyChanged("LastName");
                    this.OnLastNameChanged();
                }
            }
        }

        [Column(Storage = "_ADDRESS1", DbType = "VarChar(50)")]
        public string ADDRESS1
        {
            get
            {
                return this._ADDRESS1;
            }
            set
            {
                if ((this._ADDRESS1 != value))
                {
                    this.OnADDRESS1Changing(value);
                    this.SendPropertyChanging();
                    this._ADDRESS1 = value;
                    this.SendPropertyChanged("ADDRESS1");
                    this.OnADDRESS1Changed();
                }
            }
        }

        [Column(Storage = "_ADDRESS2", DbType = "VarChar(50)")]
        public string ADDRESS2
        {
            get
            {
                return this._ADDRESS2;
            }
            set
            {
                if ((this._ADDRESS2 != value))
                {
                    this.OnADDRESS2Changing(value);
                    this.SendPropertyChanging();
                    this._ADDRESS2 = value;
                    this.SendPropertyChanged("ADDRESS2");
                    this.OnADDRESS2Changed();
                }
            }
        }

        [Column(Storage = "_ADDRESS3", DbType = "VarChar(50)")]
        public string ADDRESS3
        {
            get
            {
                return this._ADDRESS3;
            }
            set
            {
                if ((this._ADDRESS3 != value))
                {
                    this.OnADDRESS3Changing(value);
                    this.SendPropertyChanging();
                    this._ADDRESS3 = value;
                    this.SendPropertyChanged("ADDRESS3");
                    this.OnADDRESS3Changed();
                }
            }
        }

        [Column(Storage = "_PinCode", DbType = "VarChar(25)")]
        public string PinCode
        {
            get
            {
                return this._PinCode;
            }
            set
            {
                if ((this._PinCode != value))
                {
                    this.OnPinCodeChanging(value);
                    this.SendPropertyChanging();
                    this._PinCode = value;
                    this.SendPropertyChanged("PinCode");
                    this.OnPinCodeChanged();
                }
            }
        }

        [Column(Storage = "_BankAccNo", DbType = "VarChar(25)")]
        public string BankAccNo
        {
            get
            {
                return this._BankAccNo;
            }
            set
            {
                if ((this._BankAccNo != value))
                {
                    this.OnBankAccNoChanging(value);
                    this.SendPropertyChanging();
                    this._BankAccNo = value;
                    this.SendPropertyChanged("BankAccNo");
                    this.OnBankAccNoChanged();
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
    public partial class SearchCustomerDetailsResult
    {

        private long _CustomerID;

        private string _Title;

        private string _FirstName;

        private string _MiddleName;

        private string _LastName;

        private string _ADDRESS1;

        private string _ADDRESS2;

        private string _ADDRESS3;

        private string _PinCode;

        private string _BankAccNo;

        public SearchCustomerDetailsResult()
        {
        }

        [Column(Storage = "_CustomerID", DbType = "BigInt NOT NULL")]
        public long CustomerID
        {
            get
            {
                return this._CustomerID;
            }
            set
            {
                if ((this._CustomerID != value))
                {
                    this._CustomerID = value;
                }
            }
        }

        [Column(Storage = "_Title", DbType = "VarChar(10)")]
        public string Title
        {
            get
            {
                return this._Title;
            }
            set
            {
                if ((this._Title != value))
                {
                    this._Title = value;
                }
            }
        }

        [Column(Storage = "_FirstName", DbType = "VarChar(25)")]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this._FirstName = value;
                }
            }
        }

        [Column(Storage = "_MiddleName", DbType = "VarChar(25)")]
        public string MiddleName
        {
            get
            {
                return this._MiddleName;
            }
            set
            {
                if ((this._MiddleName != value))
                {
                    this._MiddleName = value;
                }
            }
        }

        [Column(Storage = "_LastName", DbType = "VarChar(25)")]
        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this._LastName = value;
                }
            }
        }

        [Column(Storage = "_ADDRESS1", DbType = "VarChar(50)")]
        public string ADDRESS1
        {
            get
            {
                return this._ADDRESS1;
            }
            set
            {
                if ((this._ADDRESS1 != value))
                {
                    this._ADDRESS1 = value;
                }
            }
        }

        [Column(Storage = "_ADDRESS2", DbType = "VarChar(50)")]
        public string ADDRESS2
        {
            get
            {
                return this._ADDRESS2;
            }
            set
            {
                if ((this._ADDRESS2 != value))
                {
                    this._ADDRESS2 = value;
                }
            }
        }

        [Column(Storage = "_ADDRESS3", DbType = "VarChar(50)")]
        public string ADDRESS3
        {
            get
            {
                return this._ADDRESS3;
            }
            set
            {
                if ((this._ADDRESS3 != value))
                {
                    this._ADDRESS3 = value;
                }
            }
        }

        [Column(Storage = "_PinCode", DbType = "VarChar(25)")]
        public string PinCode
        {
            get
            {
                return this._PinCode;
            }
            set
            {
                if ((this._PinCode != value))
                {
                    this._PinCode = value;
                }
            }
        }

        [Column(Storage = "_BankAccNo", DbType = "VarChar(25)")]
        public string BankAccNo
        {
            get
            {
                return this._BankAccNo;
            }
            set
            {
                if ((this._BankAccNo != value))
                {
                    this._BankAccNo = value;
                }
            }
        }
    }
}
