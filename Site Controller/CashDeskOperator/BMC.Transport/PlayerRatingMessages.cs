using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace BMC.Transport
{
    public partial class rsp_GetPTRatingsResult :INotifyPropertyChanged 
    {
        private void ReportChange(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private System.Nullable<long> _RowNo;

        private System.Nullable<bool> _IsSelected;

        private long _Sno;

        private string _MessageType;

        private System.Nullable<int> _TransactionCode;

        private string _CardNumber;

        private string _RawData;

        private System.Nullable<System.DateTime> _dtMessageSent;

        private System.DateTime _dtCreated;

        private System.Nullable<bool> _SendStatus;

        private string _Asset;

        private string _UIProcess;

        public rsp_GetPTRatingsResult()
        {
        }

        [Column(Storage = "_RowNo", DbType = "BigInt")]
        public System.Nullable<long> RowNo
        {
            get
            {
                return this._RowNo;
            }
            set
            {
                if ((this._RowNo != value))
                {
                    this._RowNo = value;
                }
            }
        }

        [Column(Storage = "_IsSelected", DbType = "Bit")]
        public System.Nullable<bool> IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                if ((this._IsSelected != value))
                {
                    this._IsSelected = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        [Column(Storage = "_Sno", DbType = "BigInt NOT NULL")]
        public long Sno
        {
            get
            {
                return this._Sno;
            }
            set
            {
                if ((this._Sno != value))
                {
                    this._Sno = value;
                }
            }
        }

        [Column(Storage = "_MessageType", DbType = "VarChar(2)")]
        public string MessageType
        {
            get
            {
                return this._MessageType;
            }
            set
            {
                if ((this._MessageType != value))
                {
                    this._MessageType = value;
                }
            }
        }

        [Column(Storage = "_TransactionCode", DbType = "Int")]
        public System.Nullable<int> TransactionCode
        {
            get
            {
                return this._TransactionCode;
            }
            set
            {
                if ((this._TransactionCode != value))
                {
                    this._TransactionCode = value;
                }
            }
        }

        [Column(Storage = "_CardNumber", DbType = "Char(10)")]
        public string CardNumber
        {
            get
            {
                return this._CardNumber;
            }
            set
            {
                if ((this._CardNumber != value))
                {
                    this._CardNumber = value;
                }
            }
        }

        [Column(Storage = "_RawData", DbType = "NVarChar(MAX)")]
        public string RawData
        {
            get
            {
                return this._RawData;
            }
            set
            {
                if ((this._RawData != value))
                {
                    this._RawData = value;
                }
            }
        }

        [Column(Storage = "_dtMessageSent", DbType = "DateTime")]
        public System.Nullable<System.DateTime> dtMessageSent
        {
            get
            {
                return this._dtMessageSent;
            }
            set
            {
                if ((this._dtMessageSent != value))
                {
                    this._dtMessageSent = value;
                }
            }
        }

        [Column(Storage = "_dtCreated", DbType = "DateTime NOT NULL")]
        public System.DateTime dtCreated
        {
            get
            {
                return this._dtCreated;
            }
            set
            {
                if ((this._dtCreated != value))
                {
                    this._dtCreated = value;
                }
            }
        }

        [Column(Storage = "_SendStatus", DbType = "Bit")]
        public System.Nullable<bool> SendStatus
        {
            get
            {
                return this._SendStatus;
            }
            set
            {
                if ((this._SendStatus != value))
                {
                    this._SendStatus = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("SendStatus"));
                }
            }
        }

        [Column(Storage = "_Asset", DbType = "VarChar(10)")]
        public string Asset
        {
            get
            {
                return this._Asset;
            }
            set
            {
                if ((this._Asset != value))
                {
                    this._Asset = value;
                }
            }
        }

        [Column(Storage = "_UIProcess", DbType = "VarChar(100)")]
        public string UIProcess
        {
            get
            {
                return this._UIProcess;
            }
            set
            {
                if ((this._UIProcess != value))
                {
                    this._UIProcess = value;
                    if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs("UIProcess"));
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
