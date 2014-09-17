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
    [Table(Name = "dbo.Machine_Type")]
    public partial class Machine_Type : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Machine_Type_ID;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private string _Machine_Type_AMEDIS_ID;

        private string _Machine_Type_Code;

        private string _Machine_Type_Description;

        private string _Machine_Type_Prize_Type;

        private System.Nullable<bool> _Machine_Type_Needs_PPL;

        private System.Nullable<int> _Machine_Type_Icon_Ref;

        private string _Machine_Type_Income_Ledger_Code;

        private string _Machine_Type_Royalty_Ledger_Code;

        private string _Machine_Type_Site_Icon;

        private string _Machine_Type_Category;

        private int _IsNonGamingAssetType;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnMachine_Type_IDChanging(int value);
        partial void OnMachine_Type_IDChanged();
        partial void OnDepreciation_Policy_IDChanging(System.Nullable<int> value);
        partial void OnDepreciation_Policy_IDChanged();
        partial void OnMachine_Type_AMEDIS_IDChanging(string value);
        partial void OnMachine_Type_AMEDIS_IDChanged();
        partial void OnMachine_Type_CodeChanging(string value);
        partial void OnMachine_Type_CodeChanged();
        partial void OnMachine_Type_DescriptionChanging(string value);
        partial void OnMachine_Type_DescriptionChanged();
        partial void OnMachine_Type_Prize_TypeChanging(string value);
        partial void OnMachine_Type_Prize_TypeChanged();
        partial void OnMachine_Type_Needs_PPLChanging(System.Nullable<bool> value);
        partial void OnMachine_Type_Needs_PPLChanged();
        partial void OnMachine_Type_Icon_RefChanging(System.Nullable<int> value);
        partial void OnMachine_Type_Icon_RefChanged();
        partial void OnMachine_Type_Income_Ledger_CodeChanging(string value);
        partial void OnMachine_Type_Income_Ledger_CodeChanged();
        partial void OnMachine_Type_Royalty_Ledger_CodeChanging(string value);
        partial void OnMachine_Type_Royalty_Ledger_CodeChanged();
        partial void OnMachine_Type_Site_IconChanging(string value);
        partial void OnMachine_Type_Site_IconChanged();
        partial void OnMachine_Type_CategoryChanging(string value);
        partial void OnMachine_Type_CategoryChanged();
        partial void OnIsNonGamingAssetTypeChanging(int value);
        partial void OnIsNonGamingAssetTypeChanged();
        #endregion

        public Machine_Type()
        {
            OnCreated();
        }

        [Column(Storage = "_Machine_Type_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this.OnMachine_Type_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_ID = value;
                    this.SendPropertyChanged("Machine_Type_ID");
                    this.OnMachine_Type_IDChanged();
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this.OnDepreciation_Policy_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Depreciation_Policy_ID = value;
                    this.SendPropertyChanged("Depreciation_Policy_ID");
                    this.OnDepreciation_Policy_IDChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_AMEDIS_ID", DbType = "VarChar(50)")]
        public string Machine_Type_AMEDIS_ID
        {
            get
            {
                return this._Machine_Type_AMEDIS_ID;
            }
            set
            {
                if ((this._Machine_Type_AMEDIS_ID != value))
                {
                    this.OnMachine_Type_AMEDIS_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_AMEDIS_ID = value;
                    this.SendPropertyChanged("Machine_Type_AMEDIS_ID");
                    this.OnMachine_Type_AMEDIS_IDChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this.OnMachine_Type_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Code = value;
                    this.SendPropertyChanged("Machine_Type_Code");
                    this.OnMachine_Type_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Description", DbType = "VarChar(50)")]
        public string Machine_Type_Description
        {
            get
            {
                return this._Machine_Type_Description;
            }
            set
            {
                if ((this._Machine_Type_Description != value))
                {
                    this.OnMachine_Type_DescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Description = value;
                    this.SendPropertyChanged("Machine_Type_Description");
                    this.OnMachine_Type_DescriptionChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Prize_Type", DbType = "VarChar(50)")]
        public string Machine_Type_Prize_Type
        {
            get
            {
                return this._Machine_Type_Prize_Type;
            }
            set
            {
                if ((this._Machine_Type_Prize_Type != value))
                {
                    this.OnMachine_Type_Prize_TypeChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Prize_Type = value;
                    this.SendPropertyChanged("Machine_Type_Prize_Type");
                    this.OnMachine_Type_Prize_TypeChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Needs_PPL", DbType = "Bit")]
        public System.Nullable<bool> Machine_Type_Needs_PPL
        {
            get
            {
                return this._Machine_Type_Needs_PPL;
            }
            set
            {
                if ((this._Machine_Type_Needs_PPL != value))
                {
                    this.OnMachine_Type_Needs_PPLChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Needs_PPL = value;
                    this.SendPropertyChanged("Machine_Type_Needs_PPL");
                    this.OnMachine_Type_Needs_PPLChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Icon_Ref", DbType = "Int")]
        public System.Nullable<int> Machine_Type_Icon_Ref
        {
            get
            {
                return this._Machine_Type_Icon_Ref;
            }
            set
            {
                if ((this._Machine_Type_Icon_Ref != value))
                {
                    this.OnMachine_Type_Icon_RefChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Icon_Ref = value;
                    this.SendPropertyChanged("Machine_Type_Icon_Ref");
                    this.OnMachine_Type_Icon_RefChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Income_Ledger_Code", DbType = "VarChar(20)")]
        public string Machine_Type_Income_Ledger_Code
        {
            get
            {
                return this._Machine_Type_Income_Ledger_Code;
            }
            set
            {
                if ((this._Machine_Type_Income_Ledger_Code != value))
                {
                    this.OnMachine_Type_Income_Ledger_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Income_Ledger_Code = value;
                    this.SendPropertyChanged("Machine_Type_Income_Ledger_Code");
                    this.OnMachine_Type_Income_Ledger_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Royalty_Ledger_Code", DbType = "VarChar(20)")]
        public string Machine_Type_Royalty_Ledger_Code
        {
            get
            {
                return this._Machine_Type_Royalty_Ledger_Code;
            }
            set
            {
                if ((this._Machine_Type_Royalty_Ledger_Code != value))
                {
                    this.OnMachine_Type_Royalty_Ledger_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Royalty_Ledger_Code = value;
                    this.SendPropertyChanged("Machine_Type_Royalty_Ledger_Code");
                    this.OnMachine_Type_Royalty_Ledger_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Site_Icon", DbType = "VarChar(10)")]
        public string Machine_Type_Site_Icon
        {
            get
            {
                return this._Machine_Type_Site_Icon;
            }
            set
            {
                if ((this._Machine_Type_Site_Icon != value))
                {
                    this.OnMachine_Type_Site_IconChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Site_Icon = value;
                    this.SendPropertyChanged("Machine_Type_Site_Icon");
                    this.OnMachine_Type_Site_IconChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Type_Category", DbType = "VarChar(20)")]
        public string Machine_Type_Category
        {
            get
            {
                return this._Machine_Type_Category;
            }
            set
            {
                if ((this._Machine_Type_Category != value))
                {
                    this.OnMachine_Type_CategoryChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Type_Category = value;
                    this.SendPropertyChanged("Machine_Type_Category");
                    this.OnMachine_Type_CategoryChanged();
                }
            }
        }

        [Column(Storage = "_IsNonGamingAssetType", DbType = "Int NOT NULL")]
        public int IsNonGamingAssetType
        {
            get
            {
                return this._IsNonGamingAssetType;
            }
            set
            {
                if ((this._IsNonGamingAssetType != value))
                {
                    this.OnIsNonGamingAssetTypeChanging(value);
                    this.SendPropertyChanging();
                    this._IsNonGamingAssetType = value;
                    this.SendPropertyChanged("IsNonGamingAssetType");
                    this.OnIsNonGamingAssetTypeChanged();
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
