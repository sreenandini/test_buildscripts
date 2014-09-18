using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.Transport
{
    public partial class rsp_SL_GetSiteLicenseDetailsResult
    {

        private System.DateTime _StartDate;

        private string _ExpireDate;

        private int _RuleID;

        private System.Nullable<short> _AlertBeforeDays;

        private int _KeyStatusID;

        private string _LicenseKey;

        private bool _ValidationRequired;

        private bool _LockSite;

        private bool _DisableGames;

        private bool _WarningOnly;

        private bool _AlertRequired;

        private System.Nullable<System.DateTime> _CreatedDateTime;

        private System.Nullable<System.DateTime> _ActivatedDateTime;

        private System.Nullable<System.DateTime> _UpdatedDateTime;

        public rsp_SL_GetSiteLicenseDetailsResult()
        {
        }

        [Column(Storage = "_StartDate", DbType = "DateTime NOT NULL")]
        public System.DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_ExpireDate", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
        public string ExpireDate
        {
            get
            {
                return this._ExpireDate;
            }
            set
            {
                if ((this._ExpireDate != value))
                {
                    this._ExpireDate = value;
                }
            }
        }

        [Column(Storage = "_RuleID", DbType = "Int NOT NULL")]
        public int RuleID
        {
            get
            {
                return this._RuleID;
            }
            set
            {
                if ((this._RuleID != value))
                {
                    this._RuleID = value;
                }
            }
        }

        [Column(Storage = "_AlertBeforeDays", DbType = "SmallInt")]
        public System.Nullable<short> AlertBeforeDays
        {
            get
            {
                return this._AlertBeforeDays;
            }
            set
            {
                if ((this._AlertBeforeDays != value))
                {
                    this._AlertBeforeDays = value;
                }
            }
        }

        [Column(Storage = "_KeyStatusID", DbType = "Int NOT NULL")]
        public int KeyStatusID
        {
            get
            {
                return this._KeyStatusID;
            }
            set
            {
                if ((this._KeyStatusID != value))
                {
                    this._KeyStatusID = value;
                }
            }
        }

        [Column(Storage = "_LicenseKey", DbType = "VarChar(100)")]
        public string LicenseKey
        {
            get
            {
                return this._LicenseKey;
            }
            set
            {
                if ((this._LicenseKey != value))
                {
                    this._LicenseKey = value;
                }
            }
        }

        [Column(Storage = "_ValidationRequired", DbType = "Bit NOT NULL")]
        public bool ValidationRequired
        {
            get
            {
                return this._ValidationRequired;
            }
            set
            {
                if ((this._ValidationRequired != value))
                {
                    this._ValidationRequired = value;
                }
            }
        }

        [Column(Storage = "_LockSite", DbType = "Bit NOT NULL")]
        public bool LockSite
        {
            get
            {
                return this._LockSite;
            }
            set
            {
                if ((this._LockSite != value))
                {
                    this._LockSite = value;
                }
            }
        }

        [Column(Storage = "_DisableGames", DbType = "Bit NOT NULL")]
        public bool DisableGames
        {
            get
            {
                return this._DisableGames;
            }
            set
            {
                if ((this._DisableGames != value))
                {
                    this._DisableGames = value;
                }
            }
        }

        [Column(Storage = "_WarningOnly", DbType = "Bit NOT NULL")]
        public bool WarningOnly
        {
            get
            {
                return this._WarningOnly;
            }
            set
            {
                if ((this._WarningOnly != value))
                {
                    this._WarningOnly = value;
                }
            }
        }

        [Column(Storage = "_AlertRequired", DbType = "Bit NOT NULL")]
        public bool AlertRequired
        {
            get
            {
                return this._AlertRequired;
            }
            set
            {
                if ((this._AlertRequired != value))
                {
                    this._AlertRequired = value;
                }
            }
        }

        [Column(Storage = "_CreatedDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CreatedDateTime
        {
            get
            {
                return this._CreatedDateTime;
            }
            set
            {
                if ((this._CreatedDateTime != value))
                {
                    this._CreatedDateTime = value;
                }
            }
        }

        [Column(Storage = "_ActivatedDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ActivatedDateTime
        {
            get
            {
                return this._ActivatedDateTime;
            }
            set
            {
                if ((this._ActivatedDateTime != value))
                {
                    this._ActivatedDateTime = value;
                }
            }
        }

        [Column(Storage = "_UpdatedDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> UpdatedDateTime
        {
            get
            {
                return this._UpdatedDateTime;
            }
            set
            {
                if ((this._UpdatedDateTime != value))
                {
                    this._UpdatedDateTime = value;
                }
            }
        }
    }
    public partial class SiteLicenseDetailsEntity
    {

        private System.DateTime _StartDate;

        private System.DateTime _ExpireDate;

        private int _RuleID;

        private System.Nullable<short> _AlertBeforeDays;

        private int _KeyStatusID;

        private string _LicenseKey;

        private bool _ValidationRequired;

        private bool _LockSite;

        private bool _DisableGames;

        private bool _WarningOnly;

        private bool _AlertRequired;

        private System.Nullable<System.DateTime> _CreatedDateTime;

        private System.Nullable<System.DateTime> _ActivatedDateTime;

        private System.Nullable<System.DateTime> _UpdatedDateTime;
    

        public SiteLicenseDetailsEntity()
        {
        }

        public System.DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        public System.DateTime ExpireDate
        {
            get
            {
                return this._ExpireDate;
            }
            set
            {
                if ((this._ExpireDate != value))
                {
                    this._ExpireDate = value;
                }
            }
        }

        public int RuleID
        {
            get
            {
                return this._RuleID;
            }
            set
            {
                if ((this._RuleID != value))
                {
                    this._RuleID = value;
                }
            }
        }

        public System.Nullable<short> AlertBeforeDays
        {
            get
            {
                return this._AlertBeforeDays;
            }
            set
            {
                if ((this._AlertBeforeDays != value))
                {
                    this._AlertBeforeDays = value;
                }
            }
        }

        public int KeyStatusID
        {
            get
            {
                return this._KeyStatusID;
            }
            set
            {
                if ((this._KeyStatusID != value))
                {
                    this._KeyStatusID = value;
                }
            }
        }

        public string LicenseKey
        {
            get
            {
                return this._LicenseKey;
            }
            set
            {
                if ((this._LicenseKey != value))
                {
                    this._LicenseKey = value;
                }
            }
        } 

        public bool ValidationRequired
        {
            get
            {
                return this._ValidationRequired;
            }
            set
            {
                if ((this._ValidationRequired != value))
                {
                    this._ValidationRequired = value;
                }
            }
        }

        public bool LockSite
        {
            get
            {
                return this._LockSite;
            }
            set
            {
                if ((this._LockSite != value))
                {
                    this._LockSite = value;
                }
            }
        }

        public bool DisableGames
        {
            get
            {
                return this._DisableGames;
            }
            set
            {
                if ((this._DisableGames != value))
                {
                    this._DisableGames = value;
                }
            }
        }

        public bool WarningOnly
        {
            get
            {
                return this._WarningOnly;
            }
            set
            {
                if ((this._WarningOnly != value))
                {
                    this._WarningOnly = value;
                }
            }
        }

        public bool AlertRequired
        {
            get
            {
                return this._AlertRequired;
            }
            set
            {
                if ((this._AlertRequired != value))
                {
                    this._AlertRequired = value;
                }
            }
        }

        public System.Nullable<System.DateTime> CreatedDateTime
        {
            get
            {
                return this._CreatedDateTime;
            }
            set
            {
                if ((this._CreatedDateTime != value))
                {
                    this._CreatedDateTime = value;
                }
            }
        }

        public System.Nullable<System.DateTime> ActivatedDateTime
        {
            get
            {
                return this._ActivatedDateTime;
            }
            set
            {
                if ((this._ActivatedDateTime != value))
                {
                    this._ActivatedDateTime = value;
                }
            }
        }

        public System.Nullable<System.DateTime> UpdatedDateTime
        {
            get
            {
                return this._UpdatedDateTime;
            }
            set
            {
                if ((this._UpdatedDateTime != value))
                {
                    this._UpdatedDateTime = value;
                }
            }
        }
    }    
}
