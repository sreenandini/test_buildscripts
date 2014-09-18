using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using System.Data.SqlClient;
using BMC.Business.CashDeskOperator;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;


namespace BMC.CashDeskOperator
{

    internal class VoidTransactionDataContext : DataContext
    {
        public VoidTransactionDataContext(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }


        [Function(Name = "dbo.rsp_GetVoidTransactionList")]
        public ISingleResult<GetVoidTransactionListResult> GetVoidTransactionList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<GetVoidTransactionListResult>)(result.ReturnValue));
        }
    }
    public partial class GetVoidTransactionListResult
    {

        private int _Treasury_No;

        private string _Position;

        private string _Type;

        private double? _Amount;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<System.DateTime> _FormattedDate;

        private System.Nullable<int> _User_No;

        private string _Treasury_Reason;

        private string _Game_Title;

        public GetVoidTransactionListResult()
        {
        }

        [Column(Storage = "_Treasury_No", DbType = "Int NOT NULL")]
        public int Treasury_No
        {
            get
            {
                return this._Treasury_No;
            }
            set
            {
                if ((this._Treasury_No != value))
                {
                    this._Treasury_No = value;
                }
            }
        }

        [Column(Storage = "_Position", DbType = "VarChar(50)")]
        public string Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    this._Position = value;
                }
            }
        }

        [Column(Storage = "_Type", DbType = "VarChar(50)")]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this._Type = value;
                }
            }
        }

        [Column(Storage = "_Amount", DbType = "Money")]
        public double? Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                if ((this._Amount != value))
                {
                    this._Amount = value;
                }
            }
        }

        [Column(Storage = "_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                if ((this._Date != value))
                {
                    this._Date = value;
                }
            }
        }

        [Column(Storage = "_FormattedDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> FormattedDate
        {
            get
            {
                return this._FormattedDate;
            }
            set
            {
                if ((this._FormattedDate != value))
                {
                    this._FormattedDate = value;
                }
            }
        }

        [Column(Storage = "_User_No", DbType = "Int")]
        public System.Nullable<int> User_No
        {
            get
            {
                return this._User_No;
            }
            set
            {
                if ((this._User_No != value))
                {
                    this._User_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Reason", DbType = "VarChar(200)")]
        public string Treasury_Reason
        {
            get
            {
                return this._Treasury_Reason;
            }
            set
            {
                if ((this._Treasury_Reason != value))
                {
                    this._Treasury_Reason = value;
                }
            }
        }

        [Column(Storage = "_Game_Title", DbType = "VarChar(50)")]
        public string Game_Title
        {
            get
            {
                return this._Game_Title;
            }
            set
            {
                if ((this._Game_Title != value))
                {
                    this._Game_Title = value;
                }
            }
        }
    }

    public class VoidTransactionHelper
    {
        private VoidTransactionDataContext _voidTransactionDataContext;

        public VoidTransactionHelper()
        {
            _voidTransactionDataContext = new VoidTransactionDataContext(new SqlConnection(CommonUtilities.ExchangeConnectionString));
        }

        public ISingleResult<GetVoidTransactionListResult> GetVoidTransactionList()
        {
            return _voidTransactionDataContext.GetVoidTransactionList();
        }

    }
}
