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

    [Table(Name = "dbo.Audit_History")]
    public partial class Audit_History
    {

        private long _Audit_ID;

        private System.Nullable<System.DateTime> _Audit_Date;

        private System.Nullable<int> _Audit_User_ID;

        private string _Audit_User_Name;

        private System.Nullable<int> _Audit_Module_ID;

        private string _Audit_Module_Name;

        private string _Audit_Screen_Name;

        private string _Audit_Desc;

        private string _Audit_Slot;

        private string _Audit_Field;

        private string _Audit_Old_Vl;

        private string _Audit_New_Vl;

        private string _Audit_Operation_Type;

        public Audit_History()
        {
        }

        [Column(Storage = "_Audit_ID", AutoSync = AutoSync.Always, DbType = "BigInt NOT NULL IDENTITY", IsDbGenerated = true)]
        public long Audit_ID
        {
            get
            {
                return this._Audit_ID;
            }
            set
            {
                if ((this._Audit_ID != value))
                {
                    this._Audit_ID = value;
                }
            }
        }

        [Column(Storage = "_Audit_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Audit_Date
        {
            get
            {
                return this._Audit_Date;
            }
            set
            {
                if ((this._Audit_Date != value))
                {
                    this._Audit_Date = value;
                }
            }
        }

        [Column(Storage = "_Audit_User_ID", DbType = "Int")]
        public System.Nullable<int> Audit_User_ID
        {
            get
            {
                return this._Audit_User_ID;
            }
            set
            {
                if ((this._Audit_User_ID != value))
                {
                    this._Audit_User_ID = value;
                }
            }
        }

        [Column(Storage = "_Audit_User_Name", DbType = "VarChar(50)")]
        public string Audit_User_Name
        {
            get
            {
                return this._Audit_User_Name;
            }
            set
            {
                if ((this._Audit_User_Name != value))
                {
                    this._Audit_User_Name = value;
                }
            }
        }

        [Column(Storage = "_Audit_Module_ID", DbType = "Int")]
        public System.Nullable<int> Audit_Module_ID
        {
            get
            {
                return this._Audit_Module_ID;
            }
            set
            {
                if ((this._Audit_Module_ID != value))
                {
                    this._Audit_Module_ID = value;
                }
            }
        }

        [Column(Storage = "_Audit_Module_Name", DbType = "VarChar(50)")]
        public string Audit_Module_Name
        {
            get
            {
                return this._Audit_Module_Name;
            }
            set
            {
                if ((this._Audit_Module_Name != value))
                {
                    this._Audit_Module_Name = value;
                }
            }
        }

        [Column(Storage = "_Audit_Screen_Name", DbType = "VarChar(50)")]
        public string Audit_Screen_Name
        {
            get
            {
                return this._Audit_Screen_Name;
            }
            set
            {
                if ((this._Audit_Screen_Name != value))
                {
                    this._Audit_Screen_Name = value;
                }
            }
        }

        [Column(Storage = "_Audit_Desc", DbType = "VarChar(500)")]
        public string Audit_Desc
        {
            get
            {
                return this._Audit_Desc;
            }
            set
            {
                if ((this._Audit_Desc != value))
                {
                    this._Audit_Desc = value;
                }
            }
        }

        [Column(Storage = "_Audit_Slot", DbType = "VarChar(50)")]
        public string Audit_Slot
        {
            get
            {
                return this._Audit_Slot;
            }
            set
            {
                if ((this._Audit_Slot != value))
                {
                    this._Audit_Slot = value;
                }
            }
        }

        [Column(Storage = "_Audit_Field", DbType = "VarChar(100)")]
        public string Audit_Field
        {
            get
            {
                return this._Audit_Field;
            }
            set
            {
                if ((this._Audit_Field != value))
                {
                    this._Audit_Field = value;
                }
            }
        }

        [Column(Storage = "_Audit_Old_Vl", DbType = "VarChar(500)")]
        public string Audit_Old_Vl
        {
            get
            {
                return this._Audit_Old_Vl;
            }
            set
            {
                if ((this._Audit_Old_Vl != value))
                {
                    this._Audit_Old_Vl = value;
                }
            }
        }

        [Column(Storage = "_Audit_New_Vl", DbType = "VarChar(500)")]
        public string Audit_New_Vl
        {
            get
            {
                return this._Audit_New_Vl;
            }
            set
            {
                if ((this._Audit_New_Vl != value))
                {
                    this._Audit_New_Vl = value;
                }
            }
        }

        [Column(Storage = "_Audit_Operation_Type", DbType = "VarChar(25)")]
        public string Audit_Operation_Type
        {
            get
            {
                return this._Audit_Operation_Type;
            }
            set
            {
                if ((this._Audit_Operation_Type != value))
                {
                    this._Audit_Operation_Type = value;
                }
            }
        }
    }

    public partial class GetAuditDetailsResult : INotifyPropertyChanged
    {
        private long _Audit_ID;

        private System.Nullable<int> _Audit_User_ID;

        private string _Audit_User_Name;

        private System.Nullable<System.DateTime> _Audit_Date;

        private string _Audit_Module_Name;

        private string _Audit_Screen_Name;

        private string _Audit_Desc;

        private string _Audit_Slot;

        private string _Audit_Field;

        private string _Audit_Old_Vl;

        private string _Audit_New_Vl;

        private string _Audit_Operation_Type;

        public GetAuditDetailsResult()
        {
        }

        [Column(Storage = "_Audit_ID", DbType = "BigInt NOT NULL")]
        public long Audit_ID
        {
            get
            {
                return this._Audit_ID;
            }
            set
            {
                if ((this._Audit_ID != value))
                {
                    this._Audit_ID = value;
                    OnPropertyChanged("Audit_ID");
                }
            }
        }

        [Column(Storage = "_Audit_User_ID", DbType = "Int")]
        public System.Nullable<int> Audit_User_ID
        {
            get
            {
                return this._Audit_User_ID;
            }
            set
            {
                if ((this._Audit_User_ID != value))
                {
                    this._Audit_User_ID = value;
                    OnPropertyChanged("Audit_User_ID");
                }
            }
        }

        [Column(Storage = "_Audit_User_Name", DbType = "VarChar(50)")]
        public string Audit_User_Name
        {
            get
            {
                return this._Audit_User_Name;
            }
            set
            {
                if ((this._Audit_User_Name != value))
                {
                    this._Audit_User_Name = value;
                    OnPropertyChanged("Audit_User_Name");
                }
            }
        }

        [Column(Storage = "_Audit_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Audit_Date
        {
            get
            {
                return this._Audit_Date;
            }
            set
            {
                if ((this._Audit_Date != value))
                {
                    this._Audit_Date = value;
                    OnPropertyChanged("Audit_Date");
                }
            }
        }

        [Column(Storage = "_Audit_Module_Name", DbType = "VarChar(50)")]
        public string Audit_Module_Name
        {
            get
            {
                return this._Audit_Module_Name;
            }
            set
            {
                if ((this._Audit_Module_Name != value))
                {
                    this._Audit_Module_Name = value;
                    OnPropertyChanged("Audit_Module_Name");
                }
            }
        }

        [Column(Storage = "_Audit_Screen_Name", DbType = "VarChar(50)")]
        public string Audit_Screen_Name
        {
            get
            {
                return this._Audit_Screen_Name;
            }
            set
            {
                if ((this._Audit_Screen_Name != value))
                {
                    this._Audit_Screen_Name = value;
                    OnPropertyChanged("Audit_Screen_Name");
                }
            }
        }

        [Column(Storage = "_Audit_Desc", DbType = "VarChar(500)")]
        public string Audit_Desc
        {
            get
            {
                return this._Audit_Desc;
            }
            set
            {
                if ((this._Audit_Desc != value))
                {
                    this._Audit_Desc = value;
                    OnPropertyChanged("Audit_Desc");
                }
            }
        }

        [Column(Storage = "_Audit_Slot", DbType = "VarChar(50)")]
        public string Audit_Slot
        {
            get
            {
                return this._Audit_Slot;
            }
            set
            {
                if ((this._Audit_Slot != value))
                {
                    this._Audit_Slot = value;
                    OnPropertyChanged("Audit_Slot");
                }
            }
        }

        [Column(Storage = "_Audit_Field", DbType = "VarChar(100)")]
        public string Audit_Field
        {
            get
            {
                return this._Audit_Field;
            }
            set
            {
                if ((this._Audit_Field != value))
                {
                    this._Audit_Field = value;
                    OnPropertyChanged("Audit_Field");
                }
            }
        }

        [Column(Storage = "_Audit_Old_Vl", DbType = "VarChar(500)")]
        public string Audit_Old_Vl
        {
            get
            {
                return this._Audit_Old_Vl;
            }
            set
            {
                if ((this._Audit_Old_Vl != value))
                {
                    this._Audit_Old_Vl = value;
                    OnPropertyChanged("Audit_Old_Vl");
                }
            }
        }

        [Column(Storage = "_Audit_New_Vl", DbType = "VarChar(500)")]
        public string Audit_New_Vl
        {
            get
            {
                return this._Audit_New_Vl;
            }
            set
            {
                if ((this._Audit_New_Vl != value))
                {
                    this._Audit_New_Vl = value;
                    OnPropertyChanged("Audit_New_Vl");
                }
            }
        }

        [Column(Storage = "_Audit_Operation_Type", DbType = "VarChar(25)")]
        public string Audit_Operation_Type
        {
            get
            {
                return this._Audit_Operation_Type;
            }
            set
            {
                if ((this._Audit_Operation_Type != value))
                {
                    this._Audit_Operation_Type = value;
                    OnPropertyChanged("Audit_Operation_Type");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class FillModules
    {
        public System.Nullable<int> Audit_Module_ID
        {
            get;
            set;
        }
       
        public string Audit_Module_Name
        {
            get;
            set;
        }
    }

}
