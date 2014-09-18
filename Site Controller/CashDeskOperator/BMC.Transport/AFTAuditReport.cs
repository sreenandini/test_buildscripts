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
    public class GetAFTAuditDetailsResult
    {

        private long _AFT_Audit_ID;

        private System.Nullable<System.DateTime> _AFT_TransactionDate;

        private string _AFT_TransactionType;

        private System.Nullable<decimal> _CashableAmount;

        private System.Nullable<decimal> _NonCashableAmount;

        private System.Nullable<decimal> _WATAmount;

        private System.Nullable<int> _AFT_PlayerID;

        private string _AFT_PlayerName;

        private string _AFT_Error_Message;

        public GetAFTAuditDetailsResult()
        {
        }

        [Column(Storage = "_AFT_Audit_ID", DbType = "BigInt NOT NULL")]
        public long AFT_Audit_ID
        {
            get
            {
                return this._AFT_Audit_ID;
            }
            set
            {
                if ((this._AFT_Audit_ID != value))
                {
                    this._AFT_Audit_ID = value;
                }
            }
        }

        [Column(Storage = "_AFT_TransactionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> AFT_TransactionDate
        {
            get
            {
                return this._AFT_TransactionDate;
            }
            set
            {
                if ((this._AFT_TransactionDate != value))
                {
                    this._AFT_TransactionDate = value;
                }
            }
        }

        [Column(Storage = "_AFT_TransactionType", DbType = "VarChar(30)")]
        public string AFT_TransactionType
        {
            get
            {
                return this._AFT_TransactionType;
            }
            set
            {
                if ((this._AFT_TransactionType != value))
                {
                    this._AFT_TransactionType = value;
                }
            }
        }

        [Column(Storage = "_CashableAmount", DbType = "decimal(10,2)")]
        public System.Nullable<decimal> CashableAmount
        {
            get
            {
                return this._CashableAmount;
            }
            set
            {
                if ((this._CashableAmount != value))
                {
                    this._CashableAmount = value;
                }
            }
        }

        [Column(Storage = "_NonCashableAmount", DbType = "decimal(10,2)")]
        public System.Nullable<decimal> NonCashableAmount
        {
            get
            {
                return this._NonCashableAmount;
            }
            set
            {
                if ((this._NonCashableAmount != value))
                {
                    this._NonCashableAmount = value;
                }
            }
        }

        [Column(Storage = "_WATAmount", DbType = "decimal(10,2)")]
        public System.Nullable<decimal> WATAmount
        {
            get
            {
                return this._WATAmount;
            }
            set
            {
                if ((this._WATAmount != value))
                {
                    this._WATAmount = value;
                }
            }
        }

        [Column(Storage = "_AFT_PlayerID", DbType = "Int")]
        public System.Nullable<int> AFT_PlayerID
        {
            get
            {
                return this._AFT_PlayerID;
            }
            set
            {
                if ((this._AFT_PlayerID != value))
                {
                    this._AFT_PlayerID = value;
                }
            }
        }

        [Column(Storage = "_AFT_PlayerName", DbType = "VarChar(101)")]
        public string AFT_PlayerName
        {
            get
            {
                return this._AFT_PlayerName;
            }
            set
            {
                if ((this._AFT_PlayerName != value))
                {
                    this._AFT_PlayerName = value;
                }
            }
        }

        [Column(Storage = "_AFT_Error_Message", DbType = "VarChar(100)")]
        public string AFT_Error_Message
        {
            get
            {
                return this._AFT_Error_Message;
            }
            set
            {
                if ((this._AFT_Error_Message != value))
                {
                    this._AFT_Error_Message = value;
                }
            }
        }
    }
}