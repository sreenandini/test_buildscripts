using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.BusinessClasses.BusinessLogic
{
    public partial class AlertDetails
    {

        private long _ID;

        private string _AlertType;

        private short _AlertStatus;

        private string _AlertMessage;

        private DateTime _AlertReceivedOn;

        private Nullable<System.DateTime> _AlertExportedDate;

        private string _SiteCode;

        public AlertDetails()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", DbType = "BigInt NOT NULL")]
        public long ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if ((this._ID != value))
                {
                    this._ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string AlertType
        {
            get
            {
                return this._AlertType;
            }
            set
            {
                if ((this._AlertType != value))
                {
                    this._AlertType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertStatus", DbType = "SmallInt NOT NULL")]
        public short AlertStatus
        {
            get
            {
                return this._AlertStatus;
            }
            set
            {
                if ((this._AlertStatus != value))
                {
                    this._AlertStatus = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertMessage", DbType = "VarChar(max) NOT NULL", CanBeNull = false)]
        public string AlertMessage
        {
            get
            {
                return this._AlertMessage;
            }
            set
            {
                if ((this._AlertMessage != value))
                {
                    this._AlertMessage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertReceivedOn", DbType = "DateTime NOT NULL")]
        public System.DateTime AlertReceivedOn
        {
            get
            {
                return this._AlertReceivedOn;
            }
            set
            {
                if ((this._AlertReceivedOn != value))
                {
                    this._AlertReceivedOn = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlertExportedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> AlertExportedDate
        {
            get
            {
                return this._AlertExportedDate;
            }
            set
            {
                if ((this._AlertExportedDate != value))
                {
                    this._AlertExportedDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SiteCode", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string SiteCode
        {
            get
            {
                return this._SiteCode;
            }
            set
            {
                if ((this._SiteCode != value))
                {
                    this._SiteCode = value;
                }
            }
        }

    }

    public partial class EmailAlertStatusDetails
    {

        private int _EMD_Type_ID;

        private string _EMD_Content;

        private System.Nullable<short> _EMD_Sent_Mail_Status;

        private string _EMD_Sent_Result;

        private string _EMD_SiteCode;

        private System.Nullable<System.DateTime> _EMD_SentDate;

        public EmailAlertStatusDetails()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_Type_ID", DbType = "Int NOT NULL")]
        public int EMD_Type_ID
        {
            get
            {
                return this._EMD_Type_ID;
            }
            set
            {
                if ((this._EMD_Type_ID != value))
                {
                    this._EMD_Type_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_Content", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false)]
        public string EMD_Content
        {
            get
            {
                return this._EMD_Content;
            }
            set
            {
                if ((this._EMD_Content != value))
                {
                    this._EMD_Content = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_Sent_Mail_Status", DbType = "SmallInt")]
        public System.Nullable<short> EMD_Sent_Mail_Status
        {
            get
            {
                return this._EMD_Sent_Mail_Status;
            }
            set
            {
                if ((this._EMD_Sent_Mail_Status != value))
                {
                    this._EMD_Sent_Mail_Status = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_Sent_Result", DbType = "VarChar(2000)")]
        public string EMD_Sent_Result
        {
            get
            {
                return this._EMD_Sent_Result;
            }
            set
            {
                if ((this._EMD_Sent_Result != value))
                {
                    this._EMD_Sent_Result = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_SiteCode", DbType = "VarChar(20)")]
        public string EMD_SiteCode
        {
            get
            {
                return this._EMD_SiteCode;
            }
            set
            {
                if ((this._EMD_SiteCode != value))
                {
                    this._EMD_SiteCode = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EMD_SentDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> EMD_SentDate
        {
            get
            {
                return this._EMD_SentDate;
            }
            set
            {
                if ((this._EMD_SentDate != value))
                {
                    this._EMD_SentDate = value;
                }
            }
        }
    }

    
}
