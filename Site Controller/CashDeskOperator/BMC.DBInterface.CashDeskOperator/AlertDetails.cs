using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.DBInterface.CashDeskOperator
{
    public partial class AlertDetails
    {

        private long _ID;

        private string _AlertType;

        private short _AlertStatus;

        private string _AlertMessage;

        private DateTime _AlertReceivedOn;

        private Nullable<System.DateTime> _AlertExportedDate;

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
    }
}
