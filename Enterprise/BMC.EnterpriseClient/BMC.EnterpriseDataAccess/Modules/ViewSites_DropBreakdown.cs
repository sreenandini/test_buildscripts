using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;


namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
      
        //[Function(Name = "dbo.rsp_ecCollectionBreakDowndetails")]
        //public ISingleResult<rsp_ecCollectionBreakDowndetailsResult> rsp_ecCollectionBreakDowndetails([Parameter(Name = "Collection_id", DbType = "Int")] System.Nullable<int> collection_id)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_id);
        //    return ((ISingleResult<rsp_ecCollectionBreakDowndetailsResult>)(result.ReturnValue));
        //}
        [Function(Name = "dbo.rsp_ecCollectionBreakDown")]
        public ISingleResult<rsp_ecCollectionBreakDownResult> rsp_ecCollectionBreakDown([Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID, [Parameter(Name = "Site_id", DbType = "Int")] System.Nullable<int> site_id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID, site_id);
            return ((ISingleResult<rsp_ecCollectionBreakDownResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_AssetVarianceHistory")]
        public ISingleResult<rsp_AssetVarianceHistoryResult> GetAssetVarianceHistory([Parameter(DbType = "Int")] System.Nullable<int> installation_id, [Parameter(DbType = "Int")] System.Nullable<int> rows)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_id, rows);
            return ((ISingleResult<rsp_AssetVarianceHistoryResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetTreasuryDetails")]
        public ISingleResult<rsp_GetTreasuryDetailsResult> GetTreasuryDetails([Parameter(DbType = "Int")] System.Nullable<int> collection_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID);
            return ((ISingleResult<rsp_GetTreasuryDetailsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_ecGetDeclaredCollection")]
        public ISingleResult<rsp_ecGetDeclaredCollectionResult> GetDeclaredCollection([Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID, [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> Site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID, Site_ID);
            return ((ISingleResult<rsp_ecGetDeclaredCollectionResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_EditCollection")]
        public ISingleResult<usp_EditCollectionResult> EditCollection([Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID, [Parameter(Name = "Bill1", DbType = "Money")] System.Nullable<decimal> bill1, [Parameter(Name = "Bill2", DbType = "Money")] System.Nullable<decimal> bill2, [Parameter(Name = "Bill5", DbType = "Money")] System.Nullable<decimal> bill5, [Parameter(Name = "Bill10", DbType = "Money")] System.Nullable<decimal> bill10, [Parameter(Name = "Bill20", DbType = "Money")] System.Nullable<decimal> bill20, [Parameter(Name = "Bill50", DbType = "Money")] System.Nullable<decimal> bill50, [Parameter(Name = "Bill100", DbType = "Money")] System.Nullable<decimal> bill100, [Parameter(Name = "Ticket_In", DbType = "Money")] System.Nullable<decimal> ticket_In, [Parameter(Name = "Ticket_Out", DbType = "Money")] System.Nullable<decimal> ticket_Out, [Parameter(Name = "Handpay", DbType = "Money")] System.Nullable<decimal> handpay, [Parameter(Name = "Progressive", DbType = "Money")] System.Nullable<decimal> progressive)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID, bill1,bill2, bill5, bill10, bill20, bill50, bill100, ticket_In, ticket_Out, handpay, progressive);
            return ((ISingleResult<usp_EditCollectionResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_Auditusers")]
        public int Auditusers([Parameter(DbType = "VarChar(50)")] string audittime, [Parameter(Name = "Sitecode", DbType = "VarChar(20)")] string sitecode, [Parameter(Name = "CollectionBatch", DbType = "Int")] System.Nullable<int> collectionBatch, [Parameter(DbType = "VarChar(50)")] string username, [Parameter(DbType = "VarChar(5000)")] string logmessage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), audittime, sitecode, collectionBatch, username, logmessage);
            return ((int)(result.ReturnValue));
        }
    }

    //public partial class rsp_ecCollectionBreakDowndetailsResult
    //{

    //    private string _Batch_User_Name;

    //    private string _Batch_Date;

    //    private string _Batch_Time;

    //    private string _Batch_Date_Performed;

    //    private string _Batch_Ref;

    //    private string _Bar_Position_Name;

    //    public int _Installation_ID;

    //    private System.Nullable<int> _Installation_Price_Per_Play;

    //    private int _Installation_Counter_Cash_In_Units;

    //    private int _Installation_Counter_Cash_Out_Units;

    //    private int _Installation_Counter_Refill_Units;

    //    private string _Zone_Name;

    //    private System.Nullable<int> _Machine_Class_SP_Features;

    //    private string _Machine_Name;

    //    private string _GameName;

    //    private float _CashCollected;

    //    private float _Collection_Treasury_Defloat;

    //    private float _CashRefills;

    //    private float _Collection_Sundries_Unsupported;

    //    private float _Collection_Sundries;

    //    private int _CASH_IN_2P;

    //    private int _CASH_IN_5P;

    //    private int _CASH_IN_10P;

    //    private int _CASH_IN_20P;

    //    private int _CASH_IN_50P;

    //    private int _CASH_IN_100P;

    //    private int _CASH_IN_200P;

    //    private int _CASH_IN_500P;

    //    private int _CASH_IN_1000P;

    //    private int _CASH_IN_2000P;

    //    private int _CASH_IN_5000P;

    //    private int _CASH_IN_10000P;

    //    private int _CASH_IN_20000P;

    //    private int _CASH_IN_50000P;

    //    private int _CASH_IN_100000P;

    //    private int _CASH_OUT_2P;

    //    private System.Nullable<int> _CASH_OUT_5P;

    //    private int _CASH_OUT_10P;

    //    private int _CASH_OUT_20P;

    //    private int _CASH_OUT_50P;

    //    private int _CASH_OUT_100P;

    //    private int _CASH_OUT_200P;

    //    private int _CASH_OUT_500P;

    //    private int _CASH_OUT_1000P;

    //    private int _CASH_OUT_2000P;

    //    private int _CASH_OUT_5000P;

    //    private int _CASH_OUT_10000P;

    //    private int _CASH_OUT_20000P;

    //    private int _CASH_OUT_50000P;

    //    private int _CASH_OUT_100000P;

    //    private int _CASH_REFILL_5P;

    //    private int _CASH_REFILL_10P;

    //    private int _CASH_REFILL_20P;

    //    private int _CASH_REFILL_50P;

    //    private int _CASH_REFILL_100P;

    //    private int _CASH_REFILL_200P;

    //    private int _CASH_REFILL_500P;

    //    private int _CASH_REFILL_1000P;

    //    private int _CASH_REFILL_2000P;

    //    private int _CASH_REFILL_5000P;

    //    private int _CASH_REFILL_10000P;

    //    private int _CASH_REFILL_20000P;

    //    private int _CASH_REFILL_50000P;

    //    private int _CASH_REFILL_100000P;

    //    private System.Nullable<int> _CounterCashIn;

    //    private System.Nullable<int> _PreviousCounterCashIn;

    //    private System.Nullable<int> _CounterCashOut;

    //    private System.Nullable<int> _PreviousCounterCashOut;

    //    private System.Nullable<int> _CounterRefill;

    //    private System.Nullable<int> _PreviousCounterRefills;

    //    private System.Nullable<int> _Collection_Meters_CoinsIn;

    //    private System.Nullable<int> _Previous_Meters_Coins_In;

    //    private System.Nullable<int> _Collection_Meters_CoinsOut;

    //    private System.Nullable<int> _Previous_Meters_Coins_Out;

    //    private System.Nullable<float> _Collection_Treasury_Handpay;

    //    private System.Nullable<int> _COLLECTION_RDC_COINSIN;

    //    private System.Nullable<int> _COLLECTION_RDC_COINSOUT;

    //    private System.Nullable<int> _COLLECTION_RDC_VTP;

    //    private System.Nullable<int> _COLLECTION_RDC_HANDPAY;

    //    private System.Nullable<int> _Collection_Meters_Handpay;

    //    private System.Nullable<int> _Previous_Meters_Handpay;

    //    private string _Collection_Date_Of_Collection;

    //    public rsp_ecCollectionBreakDowndetailsResult()
    //    {
    //    }

    //    [Column(Storage = "_Batch_User_Name", DbType = "VarChar(50)")]
    //    public string Batch_User_Name
    //    {
    //        get
    //        {
    //            return this._Batch_User_Name;
    //        }
    //        set
    //        {
    //            if ((this._Batch_User_Name != value))
    //            {
    //                this._Batch_User_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Batch_Date", DbType = "VarChar(30)")]
    //    public string Batch_Date
    //    {
    //        get
    //        {
    //            return this._Batch_Date;
    //        }
    //        set
    //        {
    //            if ((this._Batch_Date != value))
    //            {
    //                this._Batch_Date = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Batch_Time", DbType = "VarChar(50)")]
    //    public string Batch_Time
    //    {
    //        get
    //        {
    //            return this._Batch_Time;
    //        }
    //        set
    //        {
    //            if ((this._Batch_Time != value))
    //            {
    //                this._Batch_Time = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Batch_Date_Performed", DbType = "VarChar(30)")]
    //    public string Batch_Date_Performed
    //    {
    //        get
    //        {
    //            return this._Batch_Date_Performed;
    //        }
    //        set
    //        {
    //            if ((this._Batch_Date_Performed != value))
    //            {
    //                this._Batch_Date_Performed = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Batch_Ref", DbType = "VarChar(50)")]
    //    public string Batch_Ref
    //    {
    //        get
    //        {
    //            return this._Batch_Ref;
    //        }
    //        set
    //        {
    //            if ((this._Batch_Ref != value))
    //            {
    //                this._Batch_Ref = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
    //    public string Bar_Position_Name
    //    {
    //        get
    //        {
    //            return this._Bar_Position_Name;
    //        }
    //        set
    //        {
    //            if ((this._Bar_Position_Name != value))
    //            {
    //                this._Bar_Position_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "Installation_ID", DbType = "VarChar(50)")]
    //    public int Installation_ID
    //    {
    //        get
    //        {
    //            return this._Installation_ID;
    //        }
    //        set
    //        {
    //            if ((this._Installation_ID != value))
    //            {
    //                this._Installation_ID = value;
    //            }
    //        }
    //    }




    //    [Column(Storage = "_Installation_Price_Per_Play", DbType = "Int")]
    //    public System.Nullable<int> Installation_Price_Per_Play
    //    {
    //        get
    //        {
    //            return this._Installation_Price_Per_Play;
    //        }
    //        set
    //        {
    //            if ((this._Installation_Price_Per_Play != value))
    //            {
    //                this._Installation_Price_Per_Play = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Installation_Counter_Cash_In_Units", DbType = "Int NOT NULL")]
    //    public int Installation_Counter_Cash_In_Units
    //    {
    //        get
    //        {
    //            return this._Installation_Counter_Cash_In_Units;
    //        }
    //        set
    //        {
    //            if ((this._Installation_Counter_Cash_In_Units != value))
    //            {
    //                this._Installation_Counter_Cash_In_Units = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Installation_Counter_Cash_Out_Units", DbType = "Int NOT NULL")]
    //    public int Installation_Counter_Cash_Out_Units
    //    {
    //        get
    //        {
    //            return this._Installation_Counter_Cash_Out_Units;
    //        }
    //        set
    //        {
    //            if ((this._Installation_Counter_Cash_Out_Units != value))
    //            {
    //                this._Installation_Counter_Cash_Out_Units = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Installation_Counter_Refill_Units", DbType = "Int NOT NULL")]
    //    public int Installation_Counter_Refill_Units
    //    {
    //        get
    //        {
    //            return this._Installation_Counter_Refill_Units;
    //        }
    //        set
    //        {
    //            if ((this._Installation_Counter_Refill_Units != value))
    //            {
    //                this._Installation_Counter_Refill_Units = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
    //    public string Zone_Name
    //    {
    //        get
    //        {
    //            return this._Zone_Name;
    //        }
    //        set
    //        {
    //            if ((this._Zone_Name != value))
    //            {
    //                this._Zone_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Machine_Class_SP_Features", DbType = "Int")]
    //    public System.Nullable<int> Machine_Class_SP_Features
    //    {
    //        get
    //        {
    //            return this._Machine_Class_SP_Features;
    //        }
    //        set
    //        {
    //            if ((this._Machine_Class_SP_Features != value))
    //            {
    //                this._Machine_Class_SP_Features = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Machine_Name", DbType = "VarChar(50)")]
    //    public string Machine_Name
    //    {
    //        get
    //        {
    //            return this._Machine_Name;
    //        }
    //        set
    //        {
    //            if ((this._Machine_Name != value))
    //            {
    //                this._Machine_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_GameName", DbType = "VarChar(50)")]
    //    public string GameName
    //    {
    //        get
    //        {
    //            return this._GameName;
    //        }
    //        set
    //        {
    //            if ((this._GameName != value))
    //            {
    //                this._GameName = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CashCollected", DbType = "Real NOT NULL")]
    //    public float CashCollected
    //    {
    //        get
    //        {
    //            return this._CashCollected;
    //        }
    //        set
    //        {
    //            if ((this._CashCollected != value))
    //            {
    //                this._CashCollected = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Treasury_Defloat", DbType = "Real NOT NULL")]
    //    public float Collection_Treasury_Defloat
    //    {
    //        get
    //        {
    //            return this._Collection_Treasury_Defloat;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Treasury_Defloat != value))
    //            {
    //                this._Collection_Treasury_Defloat = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CashRefills", DbType = "Real NOT NULL")]
    //    public float CashRefills
    //    {
    //        get
    //        {
    //            return this._CashRefills;
    //        }
    //        set
    //        {
    //            if ((this._CashRefills != value))
    //            {
    //                this._CashRefills = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Sundries_Unsupported", DbType = "Real NOT NULL")]
    //    public float Collection_Sundries_Unsupported
    //    {
    //        get
    //        {
    //            return this._Collection_Sundries_Unsupported;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Sundries_Unsupported != value))
    //            {
    //                this._Collection_Sundries_Unsupported = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Sundries", DbType = "Real NOT NULL")]
    //    public float Collection_Sundries
    //    {
    //        get
    //        {
    //            return this._Collection_Sundries;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Sundries != value))
    //            {
    //                this._Collection_Sundries = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_2P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_2P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_2P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_2P != value))
    //            {
    //                this._CASH_IN_2P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_5P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_5P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_5P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_5P != value))
    //            {
    //                this._CASH_IN_5P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_10P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_10P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_10P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_10P != value))
    //            {
    //                this._CASH_IN_10P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_20P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_20P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_20P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_20P != value))
    //            {
    //                this._CASH_IN_20P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_50P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_50P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_50P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_50P != value))
    //            {
    //                this._CASH_IN_50P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_100P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_100P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_100P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_100P != value))
    //            {
    //                this._CASH_IN_100P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_200P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_200P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_200P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_200P != value))
    //            {
    //                this._CASH_IN_200P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_500P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_500P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_500P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_500P != value))
    //            {
    //                this._CASH_IN_500P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_1000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_1000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_1000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_1000P != value))
    //            {
    //                this._CASH_IN_1000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_2000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_2000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_2000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_2000P != value))
    //            {
    //                this._CASH_IN_2000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_5000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_5000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_5000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_5000P != value))
    //            {
    //                this._CASH_IN_5000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_10000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_10000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_10000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_10000P != value))
    //            {
    //                this._CASH_IN_10000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_20000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_20000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_20000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_20000P != value))
    //            {
    //                this._CASH_IN_20000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_50000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_50000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_50000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_50000P != value))
    //            {
    //                this._CASH_IN_50000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_IN_100000P", DbType = "Int NOT NULL")]
    //    public int CASH_IN_100000P
    //    {
    //        get
    //        {
    //            return this._CASH_IN_100000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_IN_100000P != value))
    //            {
    //                this._CASH_IN_100000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_2P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_2P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_2P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_2P != value))
    //            {
    //                this._CASH_OUT_2P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_5P", DbType = "Int")]
    //    public System.Nullable<int>  CASH_OUT_5P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_5P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_5P != value))
    //            {
    //                this._CASH_OUT_5P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_10P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_10P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_10P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_10P != value))
    //            {
    //                this._CASH_OUT_10P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_20P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_20P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_20P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_20P != value))
    //            {
    //                this._CASH_OUT_20P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_50P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_50P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_50P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_50P != value))
    //            {
    //                this._CASH_OUT_50P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_100P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_100P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_100P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_100P != value))
    //            {
    //                this._CASH_OUT_100P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_200P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_200P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_200P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_200P != value))
    //            {
    //                this._CASH_OUT_200P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_500P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_500P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_500P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_500P != value))
    //            {
    //                this._CASH_OUT_500P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_1000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_1000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_1000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_1000P != value))
    //            {
    //                this._CASH_OUT_1000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_2000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_2000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_2000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_2000P != value))
    //            {
    //                this._CASH_OUT_2000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_5000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_5000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_5000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_5000P != value))
    //            {
    //                this._CASH_OUT_5000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_10000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_10000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_10000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_10000P != value))
    //            {
    //                this._CASH_OUT_10000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_20000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_20000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_20000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_20000P != value))
    //            {
    //                this._CASH_OUT_20000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_50000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_50000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_50000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_50000P != value))
    //            {
    //                this._CASH_OUT_50000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_OUT_100000P", DbType = "Int NOT NULL")]
    //    public int CASH_OUT_100000P
    //    {
    //        get
    //        {
    //            return this._CASH_OUT_100000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_OUT_100000P != value))
    //            {
    //                this._CASH_OUT_100000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_5P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_5P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_5P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_5P != value))
    //            {
    //                this._CASH_REFILL_5P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_10P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_10P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_10P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_10P != value))
    //            {
    //                this._CASH_REFILL_10P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_20P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_20P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_20P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_20P != value))
    //            {
    //                this._CASH_REFILL_20P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_50P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_50P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_50P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_50P != value))
    //            {
    //                this._CASH_REFILL_50P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_100P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_100P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_100P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_100P != value))
    //            {
    //                this._CASH_REFILL_100P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_200P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_200P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_200P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_200P != value))
    //            {
    //                this._CASH_REFILL_200P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_500P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_500P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_500P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_500P != value))
    //            {
    //                this._CASH_REFILL_500P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_1000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_1000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_1000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_1000P != value))
    //            {
    //                this._CASH_REFILL_1000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_2000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_2000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_2000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_2000P != value))
    //            {
    //                this._CASH_REFILL_2000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_5000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_5000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_5000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_5000P != value))
    //            {
    //                this._CASH_REFILL_5000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_10000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_10000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_10000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_10000P != value))
    //            {
    //                this._CASH_REFILL_10000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_20000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_20000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_20000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_20000P != value))
    //            {
    //                this._CASH_REFILL_20000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_50000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_50000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_50000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_50000P != value))
    //            {
    //                this._CASH_REFILL_50000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CASH_REFILL_100000P", DbType = "Int NOT NULL")]
    //    public int CASH_REFILL_100000P
    //    {
    //        get
    //        {
    //            return this._CASH_REFILL_100000P;
    //        }
    //        set
    //        {
    //            if ((this._CASH_REFILL_100000P != value))
    //            {
    //                this._CASH_REFILL_100000P = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CounterCashIn", DbType = "Int")]
    //    public System.Nullable<int> CounterCashIn
    //    {
    //        get
    //        {
    //            return this._CounterCashIn;
    //        }
    //        set
    //        {
    //            if ((this._CounterCashIn != value))
    //            {
    //                this._CounterCashIn = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_PreviousCounterCashIn", DbType = "Int")]
    //    public System.Nullable<int> PreviousCounterCashIn
    //    {
    //        get
    //        {
    //            return this._PreviousCounterCashIn;
    //        }
    //        set
    //        {
    //            if ((this._PreviousCounterCashIn != value))
    //            {
    //                this._PreviousCounterCashIn = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CounterCashOut", DbType = "Int")]
    //    public System.Nullable<int> CounterCashOut
    //    {
    //        get
    //        {
    //            return this._CounterCashOut;
    //        }
    //        set
    //        {
    //            if ((this._CounterCashOut != value))
    //            {
    //                this._CounterCashOut = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_PreviousCounterCashOut", DbType = "Int")]
    //    public System.Nullable<int> PreviousCounterCashOut
    //    {
    //        get
    //        {
    //            return this._PreviousCounterCashOut;
    //        }
    //        set
    //        {
    //            if ((this._PreviousCounterCashOut != value))
    //            {
    //                this._PreviousCounterCashOut = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_CounterRefill", DbType = "Int")]
    //    public System.Nullable<int> CounterRefill
    //    {
    //        get
    //        {
    //            return this._CounterRefill;
    //        }
    //        set
    //        {
    //            if ((this._CounterRefill != value))
    //            {
    //                this._CounterRefill = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_PreviousCounterRefills", DbType = "Int")]
    //    public System.Nullable<int> PreviousCounterRefills
    //    {
    //        get
    //        {
    //            return this._PreviousCounterRefills;
    //        }
    //        set
    //        {
    //            if ((this._PreviousCounterRefills != value))
    //            {
    //                this._PreviousCounterRefills = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Meters_CoinsIn", DbType = "Int")]
    //    public System.Nullable<int> Collection_Meters_CoinsIn
    //    {
    //        get
    //        {
    //            return this._Collection_Meters_CoinsIn;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Meters_CoinsIn != value))
    //            {
    //                this._Collection_Meters_CoinsIn = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Previous_Meters_Coins_In", DbType = "Int")]
    //    public System.Nullable<int> Previous_Meters_Coins_In
    //    {
    //        get
    //        {
    //            return this._Previous_Meters_Coins_In;
    //        }
    //        set
    //        {
    //            if ((this._Previous_Meters_Coins_In != value))
    //            {
    //                this._Previous_Meters_Coins_In = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Meters_CoinsOut", DbType = "Int")]
    //    public System.Nullable<int> Collection_Meters_CoinsOut
    //    {
    //        get
    //        {
    //            return this._Collection_Meters_CoinsOut;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Meters_CoinsOut != value))
    //            {
    //                this._Collection_Meters_CoinsOut = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Previous_Meters_Coins_Out", DbType = "Int")]
    //    public System.Nullable<int> Previous_Meters_Coins_Out
    //    {
    //        get
    //        {
    //            return this._Previous_Meters_Coins_Out;
    //        }
    //        set
    //        {
    //            if ((this._Previous_Meters_Coins_Out != value))
    //            {
    //                this._Previous_Meters_Coins_Out = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Treasury_Handpay", DbType = "Real")]
    //    public System.Nullable<float> Collection_Treasury_Handpay
    //    {
    //        get
    //        {
    //            return this._Collection_Treasury_Handpay;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Treasury_Handpay != value))
    //            {
    //                this._Collection_Treasury_Handpay = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_COLLECTION_RDC_COINSIN", DbType = "Int")]
    //    public System.Nullable<int> COLLECTION_RDC_COINSIN
    //    {
    //        get
    //        {
    //            return this._COLLECTION_RDC_COINSIN;
    //        }
    //        set
    //        {
    //            if ((this._COLLECTION_RDC_COINSIN != value))
    //            {
    //                this._COLLECTION_RDC_COINSIN = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_COLLECTION_RDC_COINSOUT", DbType = "Int")]
    //    public System.Nullable<int> COLLECTION_RDC_COINSOUT
    //    {
    //        get
    //        {
    //            return this._COLLECTION_RDC_COINSOUT;
    //        }
    //        set
    //        {
    //            if ((this._COLLECTION_RDC_COINSOUT != value))
    //            {
    //                this._COLLECTION_RDC_COINSOUT = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_COLLECTION_RDC_VTP", DbType = "Int")]
    //    public System.Nullable<int> COLLECTION_RDC_VTP
    //    {
    //        get
    //        {
    //            return this._COLLECTION_RDC_VTP;
    //        }
    //        set
    //        {
    //            if ((this._COLLECTION_RDC_VTP != value))
    //            {
    //                this._COLLECTION_RDC_VTP = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_COLLECTION_RDC_HANDPAY", DbType = "Int")]
    //    public System.Nullable<int> COLLECTION_RDC_HANDPAY
    //    {
    //        get
    //        {
    //            return this._COLLECTION_RDC_HANDPAY;
    //        }
    //        set
    //        {
    //            if ((this._COLLECTION_RDC_HANDPAY != value))
    //            {
    //                this._COLLECTION_RDC_HANDPAY = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Meters_Handpay", DbType = "Int")]
    //    public System.Nullable<int> Collection_Meters_Handpay
    //    {
    //        get
    //        {
    //            return this._Collection_Meters_Handpay;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Meters_Handpay != value))
    //            {
    //                this._Collection_Meters_Handpay = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Previous_Meters_Handpay", DbType = "Int")]
    //    public System.Nullable<int> Previous_Meters_Handpay
    //    {
    //        get
    //        {
    //            return this._Previous_Meters_Handpay;
    //        }
    //        set
    //        {
    //            if ((this._Previous_Meters_Handpay != value))
    //            {
    //                this._Previous_Meters_Handpay = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Collection_Date_Of_Collection", DbType = "VarChar(30)")]
    //    public string Collection_Date_Of_Collection
    //    {
    //        get
    //        {
    //            return this._Collection_Date_Of_Collection;
    //        }
    //        set
    //        {
    //            if ((this._Collection_Date_Of_Collection != value))
    //            {
    //                this._Collection_Date_Of_Collection = value;
    //            }
    //        }
    //    }
    //}

    public partial class rsp_ecCollectionBreakDownResult
    {

        private string _Batch_User_Name;

        private string _Batch_Date;

        private string _Batch_Time;

        private string _Batch_Date_Performed;

        private string _Batch_Ref;

        private string _Collection_Date_Of_Collection;

        private string  _Collection_Defloat_Collection;
        
        private string _Bar_Position_Name;

        private int _Installation_Id;

        private System.Nullable<int> _Installation_Price_Per_Play;

        private int _Installation_Counter_Cash_In_Units;

        private int _Installation_Counter_Cash_Out_Units;

        private int _Installation_Counter_Refill_Units;

        private int _Machine_Class_SP_Features;

        private string _Machine_Name;

        private string _GameName;

        private string _Zone_Name;

        private System.Nullable<double> _Collection_Treasury_Handpay_float;

        private System.Nullable<decimal> _nCoinsOut;

        private System.Nullable<decimal> _RDCCash;

        private System.Nullable<decimal> _RDCCashIn;

        private System.Nullable<decimal> _RDCCashOut;

        private System.Nullable<float> _Collections;

        private float _CashCollected;

        private System.Nullable<decimal> _Cash_Collected_100000p;

        private System.Nullable<decimal> _Cash_Collected_50000p;

        private System.Nullable<decimal> _Cash_Collected_20000p;

        private System.Nullable<decimal> _Cash_Collected_10000p;

        private System.Nullable<int> _Cash_Collected_5000p;

        private System.Nullable<int> _Cash_Collected_2000p;

        private System.Nullable<int> _Cash_Collected_1000p;

        private System.Nullable<decimal> _Cash_Collected_500p;

        private System.Nullable<decimal> _Cash_Collected_200p;

        private System.Nullable<decimal> _Cash_Collected_100p;

        private System.Nullable<decimal> _COL_COINSIN;

        private float _DeclaredTicketValue;

        private System.Nullable<double> _COL_TICKETSOUT;

        private System.Nullable<double> _COL_TICKETS;

        private double _COL_PROG;

        private System.Nullable<double> _COL_EFTOUT;

        private System.Nullable<double> _COL_EFT;

        private double _FR_COL_TOTAL;

        private double _FR_COL_20;

        private double _FR_COL_10;

        private double _FR_COL_5;

        private double _FR_COL_2;

        private double _FR_COL_1;

        private double _FR_COL_TOTALCOINS;

        private System.Nullable<decimal> _Refills;

        private System.Nullable<int> _RF_COL_1000;

        private System.Nullable<int> _RF_COL_500;

        private System.Nullable<int> _RF_COL_200;

        private System.Nullable<int> _RF_COL_100;

        private System.Nullable<int> _RF_COL_50;

        private System.Nullable<double> _RF_COL_20;

        private System.Nullable<double> _RF_COL_10;

        private System.Nullable<double> _RF_COL_5;

        private System.Nullable<double> _Short_Pay;

        private System.Nullable<double> _TicketVoid;

        private System.Nullable<int> _RDC_COL_1000;

        private System.Nullable<int> _COL_500;

        private System.Nullable<int> _COL_200;

        private System.Nullable<int> _COL_100;

        private System.Nullable<int> _COL_50;

        private System.Nullable<int> _COL_20;

        private System.Nullable<int> _COL_10;

        private System.Nullable<int> _COL_5;

        private System.Nullable<int> _RDC__COL_2;

        private System.Nullable<int> _RDC_COL_1;

        private System.Nullable<decimal> _RDC_COL_TOTALCOINS;

        private System.Nullable<decimal> _RDC_COL_COINSIN;

        private System.Nullable<decimal> _RDC_COL_TICKETSIN;

        private System.Nullable<decimal> _RDC_COL_TICKETSOUT;

        private System.Nullable<decimal> _RDC_COL_TICKETS;

        private System.Nullable<decimal> _RDC_COL_HANDPAY;

        private System.Nullable<decimal> _RDC_COL_PROG;

        private System.Nullable<decimal> _RDC_COL_EFTIN;

        private System.Nullable<decimal> _RDC_COL_EFTOUT;

        private System.Nullable<decimal> _RDC_COL_EFT;

        private System.Nullable<double> _CASH_IN_1P;

        private System.Nullable<double> _CASH_OUT_1P;

        private string _Setting_SVGIEnabled;

        private string _Setting_Region;

        private string _Setting_AddShortpay;

        private string _Setting_Auto_Declare_Monies;

        private string _Setting_IsAFTIncludedInCalculation;



        private System.Nullable<decimal> _COL_PromoCashableIn;

        private System.Nullable<decimal> _COL_PromoNonCashableIn;


        private System.Nullable<decimal> _RDC_COL_PromoCashableIn;

        private System.Nullable<decimal> _RDC_COL_PromoNonCashableIn;
        private string _Asset;
        private string _DeclaredBy;


        public rsp_ecCollectionBreakDownResult()
        {
        }

        [Column(Storage = "_Batch_User_Name", DbType = "VarChar(50)")]
        public string Batch_User_Name
        {
            get
            {
                return this._Batch_User_Name;
            }
            set
            {
                if ((this._Batch_User_Name != value))
                {
                    this._Batch_User_Name = value;
                }
            }
        }

        [Column(Storage = "_Batch_Date", DbType = "VarChar(30)")]
        public string Batch_Date
        {
            get
            {
                return this._Batch_Date;
            }
            set
            {
                if ((this._Batch_Date != value))
                {
                    this._Batch_Date = value;
                }
            }
        }

        [Column(Storage = "_Batch_Time", DbType = "VarChar(50)")]
        public string Batch_Time
        {
            get
            {
                return this._Batch_Time;
            }
            set
            {
                if ((this._Batch_Time != value))
                {
                    this._Batch_Time = value;
                }
            }
        }

        [Column(Storage = "_Batch_Date_Performed", DbType = "VarChar(30)")]
        public string Batch_Date_Performed
        {
            get
            {
                return this._Batch_Date_Performed;
            }
            set
            {
                if ((this._Batch_Date_Performed != value))
                {
                    this._Batch_Date_Performed = value;
                }
            }
        }

        [Column(Storage = "_Batch_Ref", DbType = "VarChar(50)")]
        public string Batch_Ref
        {
            get
            {
                return this._Batch_Ref;
            }
            set
            {
                if ((this._Batch_Ref != value))
                {
                    this._Batch_Ref = value;
                }
            }
        }

        [Column(Storage = "_Collection_Date_Of_Collection", DbType = "VarChar(30)")]
        public string Collection_Date_Of_Collection
        {
            get
            {
                return this._Collection_Date_Of_Collection;
            }
            set
            {
                if ((this._Collection_Date_Of_Collection != value))
                {
                    this._Collection_Date_Of_Collection = value;
                }
            }
        }
 
        
        [Column(Storage = "_Collection_Defloat_Collection", DbType = "VarChar(30)")]
        public string Collection_Defloat_Collection
        {
            get
            {
                return this._Collection_Defloat_Collection;
            }
            set
            {
                if ((this._Collection_Defloat_Collection != value))
                {
                    this._Collection_Defloat_Collection = value;
                }
            }
        }
         

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }

        [Column(Storage = "_Installation_Id", DbType = "Int")]
        public int Installation_Id
        {
            get
            {
                return this._Installation_Id;
            }
            set
            {
                if ((this._Installation_Id != value))
                {
                    this._Installation_Id = value;
                }
            }
        }


        [Column(Storage = "_Installation_Price_Per_Play", DbType = "Int")]
        public System.Nullable<int> Installation_Price_Per_Play
        {
            get
            {
                return this._Installation_Price_Per_Play;
            }
            set
            {
                if ((this._Installation_Price_Per_Play != value))
                {
                    this._Installation_Price_Per_Play = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Cash_In_Units", DbType = "Int NOT NULL")]
        public int Installation_Counter_Cash_In_Units
        {
            get
            {
                return this._Installation_Counter_Cash_In_Units;
            }
            set
            {
                if ((this._Installation_Counter_Cash_In_Units != value))
                {
                    this._Installation_Counter_Cash_In_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Cash_Out_Units", DbType = "Int NOT NULL")]
        public int Installation_Counter_Cash_Out_Units
        {
            get
            {
                return this._Installation_Counter_Cash_Out_Units;
            }
            set
            {
                if ((this._Installation_Counter_Cash_Out_Units != value))
                {
                    this._Installation_Counter_Cash_Out_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Refill_Units", DbType = "Int NOT NULL")]
        public int Installation_Counter_Refill_Units
        {
            get
            {
                return this._Installation_Counter_Refill_Units;
            }
            set
            {
                if ((this._Installation_Counter_Refill_Units != value))
                {
                    this._Installation_Counter_Refill_Units = value;
                }
            }
        }

        [Column(Storage = "_Machine_Class_SP_Features", DbType = "Int NOT NULL")]
        public int Machine_Class_SP_Features
        {
            get
            {
                return this._Machine_Class_SP_Features;
            }
            set
            {
                if ((this._Machine_Class_SP_Features != value))
                {
                    this._Machine_Class_SP_Features = value;
                }
            }
        }

        [Column(Storage = "_Machine_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Machine_Name
        {
            get
            {
                return this._Machine_Name;
            }
            set
            {
                if ((this._Machine_Name != value))
                {
                    this._Machine_Name = value;
                }
            }
        }

        [Column(Storage = "_GameName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string GameName
        {
            get
            {
                return this._GameName;
            }
            set
            {
                if ((this._GameName != value))
                {
                    this._GameName = value;
                }
            }
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value;
                }
            }
        }

        [Column(Storage = "_Collection_Treasury_Handpay_float", DbType = "Float")]
        public System.Nullable<double> Collection_Treasury_Handpay_float
        {
            get
            {
                return this._Collection_Treasury_Handpay_float;
            }
            set
            {
                if ((this._Collection_Treasury_Handpay_float != value))
                {
                    this._Collection_Treasury_Handpay_float = value;
                }
            }
        }

        [Column(Storage = "_nCoinsOut", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> nCoinsOut
        {
            get
            {
                return this._nCoinsOut;
            }
            set
            {
                if ((this._nCoinsOut != value))
                {
                    this._nCoinsOut = value;
                }
            }
        }

        [Column(Storage = "_RDCCash", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDCCash
        {
            get
            {
                return this._RDCCash;
            }
            set
            {
                if ((this._RDCCash != value))
                {
                    this._RDCCash = value;
                }
            }
        }

        [Column(Storage = "_RDCCashIn", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDCCashIn
        {
            get
            {
                return this._RDCCashIn;
            }
            set
            {
                if ((this._RDCCashIn != value))
                {
                    this._RDCCashIn = value;
                }
            }
        }

        [Column(Storage = "_RDCCashOut", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDCCashOut
        {
            get
            {
                return this._RDCCashOut;
            }
            set
            {
                if ((this._RDCCashOut != value))
                {
                    this._RDCCashOut = value;
                }
            }
        }

        [Column(Storage = "_Collections", DbType = "Real")]
        public System.Nullable<float> Collections
        {
            get
            {
                return this._Collections;
            }
            set
            {
                if ((this._Collections != value))
                {
                    this._Collections = value;
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Real NOT NULL")]
        public float CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this._CashCollected = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100000p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_100000p
        {
            get
            {
                return this._Cash_Collected_100000p;
            }
            set
            {
                if ((this._Cash_Collected_100000p != value))
                {
                    this._Cash_Collected_100000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50000p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_50000p
        {
            get
            {
                return this._Cash_Collected_50000p;
            }
            set
            {
                if ((this._Cash_Collected_50000p != value))
                {
                    this._Cash_Collected_50000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20000p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_20000p
        {
            get
            {
                return this._Cash_Collected_20000p;
            }
            set
            {
                if ((this._Cash_Collected_20000p != value))
                {
                    this._Cash_Collected_20000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10000p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_10000p
        {
            get
            {
                return this._Cash_Collected_10000p;
            }
            set
            {
                if ((this._Cash_Collected_10000p != value))
                {
                    this._Cash_Collected_10000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5000p", DbType = "Int")]
        public System.Nullable<int> Cash_Collected_5000p
        {
            get
            {
                return this._Cash_Collected_5000p;
            }
            set
            {
                if ((this._Cash_Collected_5000p != value))
                {
                    this._Cash_Collected_5000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2000p", DbType = "Int")]
        public System.Nullable<int> Cash_Collected_2000p
        {
            get
            {
                return this._Cash_Collected_2000p;
            }
            set
            {
                if ((this._Cash_Collected_2000p != value))
                {
                    this._Cash_Collected_2000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1000p", DbType = "Int")]
        public System.Nullable<int> Cash_Collected_1000p
        {
            get
            {
                return this._Cash_Collected_1000p;
            }
            set
            {
                if ((this._Cash_Collected_1000p != value))
                {
                    this._Cash_Collected_1000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_500p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_500p
        {
            get
            {
                return this._Cash_Collected_500p;
            }
            set
            {
                if ((this._Cash_Collected_500p != value))
                {
                    this._Cash_Collected_500p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_200p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_200p
        {
            get
            {
                return this._Cash_Collected_200p;
            }
            set
            {
                if ((this._Cash_Collected_200p != value))
                {
                    this._Cash_Collected_200p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100p", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Cash_Collected_100p
        {
            get
            {
                return this._Cash_Collected_100p;
            }
            set
            {
                if ((this._Cash_Collected_100p != value))
                {
                    this._Cash_Collected_100p = value;
                }
            }
        }

        [Column(Storage = "_COL_COINSIN", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> COL_COINSIN
        {
            get
            {
                return this._COL_COINSIN;
            }
            set
            {
                if ((this._COL_COINSIN != value))
                {
                    this._COL_COINSIN = value;
                }
            }
        }

        [Column(Storage = "_DeclaredTicketValue", DbType = "Real NOT NULL")]
        public float DeclaredTicketValue
        {
            get
            {
                return this._DeclaredTicketValue;
            }
            set
            {
                if ((this._DeclaredTicketValue != value))
                {
                    this._DeclaredTicketValue = value;
                }
            }
        }

        [Column(Storage = "_COL_TICKETSOUT", DbType = "Float")]
        public System.Nullable<double> COL_TICKETSOUT
        {
            get
            {
                return this._COL_TICKETSOUT;
            }
            set
            {
                if ((this._COL_TICKETSOUT != value))
                {
                    this._COL_TICKETSOUT = value;
                }
            }
        }

        [Column(Storage = "_COL_TICKETS", DbType = "Float")]
        public System.Nullable<double> COL_TICKETS
        {
            get
            {
                return this._COL_TICKETS;
            }
            set
            {
                if ((this._COL_TICKETS != value))
                {
                    this._COL_TICKETS = value;
                }
            }
        }

        [Column(Storage = "_COL_PROG", DbType = "Float NOT NULL")]
        public double COL_PROG
        {
            get
            {
                return this._COL_PROG;
            }
            set
            {
                if ((this._COL_PROG != value))
                {
                    this._COL_PROG = value;
                }
            }
        }

        [Column(Storage = "_COL_EFTOUT", DbType = "Float")]
        public System.Nullable<double> COL_EFTOUT
        {
            get
            {
                return this._COL_EFTOUT;
            }
            set
            {
                if ((this._COL_EFTOUT != value))
                {
                    this._COL_EFTOUT = value;
                }
            }
        }

        [Column(Storage = "_COL_EFT", DbType = "Float")]
        public System.Nullable<double> COL_EFT
        {
            get
            {
                return this._COL_EFT;
            }
            set
            {
                if ((this._COL_EFT != value))
                {
                    this._COL_EFT = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_TOTAL", DbType = "Float NOT NULL")]
        public double FR_COL_TOTAL
        {
            get
            {
                return this._FR_COL_TOTAL;
            }
            set
            {
                if ((this._FR_COL_TOTAL != value))
                {
                    this._FR_COL_TOTAL = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_20", DbType = "Float NOT NULL")]
        public double FR_COL_20
        {
            get
            {
                return this._FR_COL_20;
            }
            set
            {
                if ((this._FR_COL_20 != value))
                {
                    this._FR_COL_20 = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_10", DbType = "Float NOT NULL")]
        public double FR_COL_10
        {
            get
            {
                return this._FR_COL_10;
            }
            set
            {
                if ((this._FR_COL_10 != value))
                {
                    this._FR_COL_10 = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_5", DbType = "Float NOT NULL")]
        public double FR_COL_5
        {
            get
            {
                return this._FR_COL_5;
            }
            set
            {
                if ((this._FR_COL_5 != value))
                {
                    this._FR_COL_5 = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_2", DbType = "Float NOT NULL")]
        public double FR_COL_2
        {
            get
            {
                return this._FR_COL_2;
            }
            set
            {
                if ((this._FR_COL_2 != value))
                {
                    this._FR_COL_2 = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_1", DbType = "Float NOT NULL")]
        public double FR_COL_1
        {
            get
            {
                return this._FR_COL_1;
            }
            set
            {
                if ((this._FR_COL_1 != value))
                {
                    this._FR_COL_1 = value;
                }
            }
        }

        [Column(Storage = "_FR_COL_TOTALCOINS", DbType = "Float NOT NULL")]
        public double FR_COL_TOTALCOINS
        {
            get
            {
                return this._FR_COL_TOTALCOINS;
            }
            set
            {
                if ((this._FR_COL_TOTALCOINS != value))
                {
                    this._FR_COL_TOTALCOINS = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Refills
        {
            get
            {
                return this._Refills;
            }
            set
            {
                if ((this._Refills != value))
                {
                    this._Refills = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_1000", DbType = "Int")]
        public System.Nullable<int> RF_COL_1000
        {
            get
            {
                return this._RF_COL_1000;
            }
            set
            {
                if ((this._RF_COL_1000 != value))
                {
                    this._RF_COL_1000 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_500", DbType = "Int")]
        public System.Nullable<int> RF_COL_500
        {
            get
            {
                return this._RF_COL_500;
            }
            set
            {
                if ((this._RF_COL_500 != value))
                {
                    this._RF_COL_500 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_200", DbType = "Int")]
        public System.Nullable<int> RF_COL_200
        {
            get
            {
                return this._RF_COL_200;
            }
            set
            {
                if ((this._RF_COL_200 != value))
                {
                    this._RF_COL_200 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_100", DbType = "Int")]
        public System.Nullable<int> RF_COL_100
        {
            get
            {
                return this._RF_COL_100;
            }
            set
            {
                if ((this._RF_COL_100 != value))
                {
                    this._RF_COL_100 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_50", DbType = "Int")]
        public System.Nullable<int> RF_COL_50
        {
            get
            {
                return this._RF_COL_50;
            }
            set
            {
                if ((this._RF_COL_50 != value))
                {
                    this._RF_COL_50 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_20", DbType = "Float")]
        public System.Nullable<double> RF_COL_20
        {
            get
            {
                return this._RF_COL_20;
            }
            set
            {
                if ((this._RF_COL_20 != value))
                {
                    this._RF_COL_20 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_10", DbType = "Float")]
        public System.Nullable<double> RF_COL_10
        {
            get
            {
                return this._RF_COL_10;
            }
            set
            {
                if ((this._RF_COL_10 != value))
                {
                    this._RF_COL_10 = value;
                }
            }
        }

        [Column(Storage = "_RF_COL_5", DbType = "Float")]
        public System.Nullable<double> RF_COL_5
        {
            get
            {
                return this._RF_COL_5;
            }
            set
            {
                if ((this._RF_COL_5 != value))
                {
                    this._RF_COL_5 = value;
                }
            }
        }

        [Column(Storage = "_Short_Pay", DbType = "Float")]
        public System.Nullable<double> Short_Pay
        {
            get
            {
                return this._Short_Pay;
            }
            set
            {
                if ((this._Short_Pay != value))
                {
                    this._Short_Pay = value;
                }
            }
        }

        [Column(Storage = "_TicketVoid", DbType = "Float")]
        public System.Nullable<double> TicketVoid
        {
            get
            {
                return this._TicketVoid;
            }
            set
            {
                if ((this._TicketVoid != value))
                {
                    this._TicketVoid = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_1000", DbType = "Int")]
        public System.Nullable<int> RDC_COL_1000
        {
            get
            {
                return this._RDC_COL_1000;
            }
            set
            {
                if ((this._RDC_COL_1000 != value))
                {
                    this._RDC_COL_1000 = value;
                }
            }
        }

        [Column(Storage = "_COL_500", DbType = "Int")]
        public System.Nullable<int> COL_500
        {
            get
            {
                return this._COL_500;
            }
            set
            {
                if ((this._COL_500 != value))
                {
                    this._COL_500 = value;
                }
            }
        }

        [Column(Storage = "_COL_200", DbType = "Int")]
        public System.Nullable<int> COL_200
        {
            get
            {
                return this._COL_200;
            }
            set
            {
                if ((this._COL_200 != value))
                {
                    this._COL_200 = value;
                }
            }
        }

        [Column(Storage = "_COL_100", DbType = "Int")]
        public System.Nullable<int> COL_100
        {
            get
            {
                return this._COL_100;
            }
            set
            {
                if ((this._COL_100 != value))
                {
                    this._COL_100 = value;
                }
            }
        }

        [Column(Storage = "_COL_50", DbType = "Int")]
        public System.Nullable<int> COL_50
        {
            get
            {
                return this._COL_50;
            }
            set
            {
                if ((this._COL_50 != value))
                {
                    this._COL_50 = value;
                }
            }
        }

        [Column(Storage = "_COL_20", DbType = "Int")]
        public System.Nullable<int> COL_20
        {
            get
            {
                return this._COL_20;
            }
            set
            {
                if ((this._COL_20 != value))
                {
                    this._COL_20 = value;
                }
            }
        }

        [Column(Storage = "_COL_10", DbType = "Int")]
        public System.Nullable<int> COL_10
        {
            get
            {
                return this._COL_10;
            }
            set
            {
                if ((this._COL_10 != value))
                {
                    this._COL_10 = value;
                }
            }
        }

        [Column(Storage = "_COL_5", DbType = "Int")]
        public System.Nullable<int> COL_5
        {
            get
            {
                return this._COL_5;
            }
            set
            {
                if ((this._COL_5 != value))
                {
                    this._COL_5 = value;
                }
            }
        }

        [Column(Storage = "_RDC__COL_2", DbType = "Int")]
        public System.Nullable<int> RDC__COL_2
        {
            get
            {
                return this._RDC__COL_2;
            }
            set
            {
                if ((this._RDC__COL_2 != value))
                {
                    this._RDC__COL_2 = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_1", DbType = "Int")]
        public System.Nullable<int> RDC_COL_1
        {
            get
            {
                return this._RDC_COL_1;
            }
            set
            {
                if ((this._RDC_COL_1 != value))
                {
                    this._RDC_COL_1 = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_TOTALCOINS", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_TOTALCOINS
        {
            get
            {
                return this._RDC_COL_TOTALCOINS;
            }
            set
            {
                if ((this._RDC_COL_TOTALCOINS != value))
                {
                    this._RDC_COL_TOTALCOINS = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_COINSIN", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_COINSIN
        {
            get
            {
                return this._RDC_COL_COINSIN;
            }
            set
            {
                if ((this._RDC_COL_COINSIN != value))
                {
                    this._RDC_COL_COINSIN = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_TICKETSIN", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_TICKETSIN
        {
            get
            {
                return this._RDC_COL_TICKETSIN;
            }
            set
            {
                if ((this._RDC_COL_TICKETSIN != value))
                {
                    this._RDC_COL_TICKETSIN = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_TICKETSOUT", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_TICKETSOUT
        {
            get
            {
                return this._RDC_COL_TICKETSOUT;
            }
            set
            {
                if ((this._RDC_COL_TICKETSOUT != value))
                {
                    this._RDC_COL_TICKETSOUT = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_TICKETS", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_TICKETS
        {
            get
            {
                return this._RDC_COL_TICKETS;
            }
            set
            {
                if ((this._RDC_COL_TICKETS != value))
                {
                    this._RDC_COL_TICKETS = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_HANDPAY", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_HANDPAY
        {
            get
            {
                return this._RDC_COL_HANDPAY;
            }
            set
            {
                if ((this._RDC_COL_HANDPAY != value))
                {
                    this._RDC_COL_HANDPAY = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_PROG", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_PROG
        {
            get
            {
                return this._RDC_COL_PROG;
            }
            set
            {
                if ((this._RDC_COL_PROG != value))
                {
                    this._RDC_COL_PROG = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_EFTIN", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_EFTIN
        {
            get
            {
                return this._RDC_COL_EFTIN;
            }
            set
            {
                if ((this._RDC_COL_EFTIN != value))
                {
                    this._RDC_COL_EFTIN = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_EFTOUT", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_EFTOUT
        {
            get
            {
                return this._RDC_COL_EFTOUT;
            }
            set
            {
                if ((this._RDC_COL_EFTOUT != value))
                {
                    this._RDC_COL_EFTOUT = value;
                }
            }
        }

        [Column(Storage = "_RDC_COL_EFT", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RDC_COL_EFT
        {
            get
            {
                return this._RDC_COL_EFT;
            }
            set
            {
                if ((this._RDC_COL_EFT != value))
                {
                    this._RDC_COL_EFT = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_1P", DbType = "Float")]
        public System.Nullable<double> CASH_IN_1P
        {
            get
            {
                return this._CASH_IN_1P;
            }
            set
            {
                if ((this._CASH_IN_1P != value))
                {
                    this._CASH_IN_1P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1P", DbType = "Float")]
        public System.Nullable<double> CASH_OUT_1P
        {
            get
            {
                return this._CASH_OUT_1P;
            }
            set
            {
                if ((this._CASH_OUT_1P != value))
                {
                    this._CASH_OUT_1P = value;
                }
            }
        }

        [Column(Storage = "_Setting_SVGIEnabled", DbType = "VarChar(20)")]
        public string Setting_SVGIEnabled
        {
            get
            {
                return this._Setting_SVGIEnabled;
            }
            set
            {
                if ((this._Setting_SVGIEnabled != value))
                {
                    this._Setting_SVGIEnabled = value;
                }
            }
        }

        [Column(Storage = "_Setting_Region", DbType = "VarChar(20)")]
        public string Setting_Region
        {
            get
            {
                return this._Setting_Region;
            }
            set
            {
                if ((this._Setting_Region != value))
                {
                    this._Setting_Region = value;
                }
            }
        }

        [Column(Storage = "_Setting_AddShortpay", DbType = "VarChar(20)")]
        public string Setting_AddShortpay
        {
            get
            {
                return this._Setting_AddShortpay;
            }
            set
            {
                if ((this._Setting_AddShortpay != value))
                {
                    this._Setting_AddShortpay = value;
                }
            }
        }

        [Column(Storage = "_Setting_Auto_Declare_Monies", DbType = "VarChar(20)")]
        public string Setting_Auto_Declare_Monies
        {
            get
            {
                return this._Setting_Auto_Declare_Monies;
            }
            set
            {
                if ((this._Setting_Auto_Declare_Monies != value))
                {
                    this._Setting_Auto_Declare_Monies = value;
                }
            }
        }

        [Column(Storage = "_Setting_IsAFTIncludedInCalculation", DbType = "VarChar(20)")]
        public string Setting_IsAFTIncludedInCalculation
        {
            get
            {
                return this._Setting_IsAFTIncludedInCalculation;
            }
            set
            {
                if ((this._Setting_IsAFTIncludedInCalculation != value))
                {
                    this._Setting_IsAFTIncludedInCalculation = value;
                }
            }
        }



       [Column(Storage = "_COL_PromoCashableIn", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> COL_PromoCashableIn
        {
            get
            {
                return this._COL_PromoCashableIn;
            }
            set
            {
                if ((this._COL_PromoCashableIn != value))
                {
                    this._COL_PromoCashableIn = value;
                }
            }
        }



       [Column(Storage = "_COL_PromoNonCashableIn", DbType = "Decimal(0,0)")]
       public System.Nullable<decimal> COL_PromoNonCashableIn
       {
           get
           {
               return this._COL_PromoNonCashableIn;
           }
           set
           {
               if ((this._COL_PromoNonCashableIn != value))
               {
                   this._COL_PromoNonCashableIn = value;
               }
           }
       }

       [Column(Storage = "_RDC_COL_PromoCashableIn", DbType = "Decimal(0,0)")]
       public System.Nullable<decimal> RDC_COL_PromoCashableIn
       {
           get
           {
               return this._RDC_COL_PromoCashableIn;
           }
           set
           {
               if ((this._RDC_COL_PromoCashableIn != value))
               {
                   this._RDC_COL_PromoCashableIn = value;
               }
           }
       }


       [Column(Storage = "_RDC_COL_PromoNonCashableIn", DbType = "Decimal(0,0)")]
       public System.Nullable<decimal> RDC_COL_PromoNonCashableIn
       {
           get
           {
               return this._RDC_COL_PromoNonCashableIn;
           }
           set
           {
               if ((this._RDC_COL_PromoNonCashableIn != value))
               {
                   this._RDC_COL_PromoNonCashableIn = value;
               }
           }
       }

       [Column(Storage = "_Asset", DbType = "nvarchar(20)")]
       public String Asset
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
        //DeclaredBy

       [Column(Storage = "_DeclaredBy", DbType = "nvarchar(20)")]
       public String DeclaredBy
       {
           get
           {
               return this._DeclaredBy;
           }
           set
           {
               if ((this._DeclaredBy != value))
               {
                   this._DeclaredBy = value;
               }
           }
       }

      
    }

    public partial class rsp_AssetVarianceHistoryResult
    {

        private string _gaming_day;

        private string _collection_day;

        private System.Nullable<float> _coin_Var;

        private float _note_var;

        private System.Nullable<double> _ticket_in_var;

        private System.Nullable<double> _ticket_out_var;

        private System.Nullable<double> _handpay_var;

        private System.Nullable<double> _EftIn_var;

        private System.Nullable<double> _EftOut_var;

        private System.Nullable<double> _Progressive_Var;

        private double _Total_Var;

        public rsp_AssetVarianceHistoryResult()
        {
        }

        [Column(Storage = "_gaming_day", DbType = "VarChar(30)")]
        public string gaming_day
        {
            get
            {
                return this._gaming_day;
            }
            set
            {
                if ((this._gaming_day != value))
                {
                    this._gaming_day = value;
                }
            }
        }

        [Column(Storage = "_collection_day", DbType = "VarChar(30)")]
        public string collection_day
        {
            get
            {
                return this._collection_day;
            }
            set
            {
                if ((this._collection_day != value))
                {
                    this._collection_day = value;
                }
            }
        }

        [Column(Storage = "_coin_Var", DbType = "Real")]
        public System.Nullable<float> coin_Var
        {
            get
            {
                return this._coin_Var;
            }
            set
            {
                if ((this._coin_Var != value))
                {
                    this._coin_Var = value;
                }
            }
        }

        [Column(Storage = "_note_var", DbType = "Real NOT NULL")]
        public float note_var
        {
            get
            {
                return this._note_var;
            }
            set
            {
                if ((this._note_var != value))
                {
                    this._note_var = value;
                }
            }
        }

        [Column(Storage = "_ticket_in_var", DbType = "Float")]
        public System.Nullable<double> ticket_in_var
        {
            get
            {
                return this._ticket_in_var;
            }
            set
            {
                if ((this._ticket_in_var != value))
                {
                    this._ticket_in_var = value;
                }
            }
        }

        [Column(Storage = "_ticket_out_var", DbType = "Float")]
        public System.Nullable<double> ticket_out_var
        {
            get
            {
                return this._ticket_out_var;
            }
            set
            {
                if ((this._ticket_out_var != value))
                {
                    this._ticket_out_var = value;
                }
            }
        }

        [Column(Storage = "_handpay_var", DbType = "Float")]
        public System.Nullable<double> handpay_var
        {
            get
            {
                return this._handpay_var;
            }
            set
            {
                if ((this._handpay_var != value))
                {
                    this._handpay_var = value;
                }
            }
        }

        [Column(Storage = "_EftIn_var", DbType = "Float")]
        public System.Nullable<double> EftIn_var
        {
            get
            {
                return this._EftIn_var;
            }
            set
            {
                if ((this._EftIn_var != value))
                {
                    this._EftIn_var = value;
                }
            }
        }

        [Column(Storage = "_EftOut_var", DbType = "Float")]
        public System.Nullable<double> EftOut_var
        {
            get
            {
                return this._EftOut_var;
            }
            set
            {
                if ((this._EftOut_var != value))
                {
                    this._EftOut_var = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Var", DbType = "Float")]
        public System.Nullable<double> Progressive_Var
        {
            get
            {
                return this._Progressive_Var;
            }
            set
            {
                if ((this._Progressive_Var != value))
                {
                    this._Progressive_Var = value;
                }
            }
        }

        [Column(Storage = "_Total_Var", DbType = "Float NOT NULL")]
        public double Total_Var
        {
            get
            {
                return this._Total_Var;
            }
            set
            {
                if ((this._Total_Var != value))
                {
                    this._Total_Var = value;
                }
            }
        }
    }

    public partial class rsp_GetTreasuryDetailsResult
    {

        private string _Treasury_Type;

        private string _Treasury_Date;

        private string _Treasury_Time;

        private System.Nullable<double> _Treasury_Amount;

        private string _Treasury_User;
        
        private string _Treasury_Issued_User;

        private string _Treasury_Reason;

        public rsp_GetTreasuryDetailsResult()
        {
        }

        [Column(Storage = "_Treasury_Type", DbType = "VarChar(50)")]
        public string Treasury_Type
        {
            get
            {
                return this._Treasury_Type;
            }
            set
            {
                if ((this._Treasury_Type != value))
                {
                    this._Treasury_Type = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Date", DbType = "VarChar(30)")]
        public string Treasury_Date
        {
            get
            {
                return this._Treasury_Date;
            }
            set
            {
                if ((this._Treasury_Date != value))
                {
                    this._Treasury_Date = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Time", DbType = "VarChar(50)")]
        public string Treasury_Time
        {
            get
            {
                return this._Treasury_Time;
            }
            set
            {
                if ((this._Treasury_Time != value))
                {
                    this._Treasury_Time = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Amount", DbType = "Float")]
        public System.Nullable<double> Treasury_Amount
        {
            get
            {
                return this._Treasury_Amount;
            }
            set
            {
                if ((this._Treasury_Amount != value))
                {
                    this._Treasury_Amount = value;
                }
            }
        }

        [Column(Storage = "_Treasury_User", DbType = "VarChar(50)")]
        public string Treasury_User
        {
            get
            {
                return this._Treasury_User;
            }
            set
            {
                if ((this._Treasury_User != value))
                {
                    this._Treasury_User = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Issued_User", DbType = "VarChar(50)")]
        public string Treasury_Issued_User
        {
            get
            {
                return this._Treasury_Issued_User;
            }
            set
            {
                if ((this._Treasury_Issued_User != value))
                {
                    this._Treasury_Issued_User = value;
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
    }


    public partial class rsp_ecGetDeclaredCollectionResult
    {

        private int _Batch_ID;

        private int _Site_ID;

        private System.Nullable<double> _Cash_Collected_100p;

        private System.Nullable<double> _Cash_Collected_200p;

        private System.Nullable<double> _Cash_Collected_500p;

        private System.Nullable<double> _Cash_Collected_1000p;

        private System.Nullable<double> _Cash_Collected_2000p;

        private System.Nullable<double> _Cash_Collected_5000p;

        private System.Nullable<double> _Cash_Collected_10000p;

        private System.Nullable<double> _Cash_Collected_20000p;

        private System.Nullable<double> _Cash_Collected_50000p;

        private float _Declared_Tickets;

        private float _Tickets_Printed;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Hand_Pay;

        private string _Region;

        public rsp_ecGetDeclaredCollectionResult()
        {
        }

        [Column(Storage = "_Batch_ID", DbType = "Int NOT NULL")]
        public int Batch_ID
        {
            get
            {
                return this._Batch_ID;
            }
            set
            {
                if ((this._Batch_ID != value))
                {
                    this._Batch_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
        public int Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_100p
        {
            get
            {
                return this._Cash_Collected_100p;
            }
            set
            {
                if ((this._Cash_Collected_100p != value))
                {
                    this._Cash_Collected_100p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_200p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_200p
        {
            get
            {
                return this._Cash_Collected_200p;
            }
            set
            {
                if ((this._Cash_Collected_200p != value))
                {
                    this._Cash_Collected_200p = value;
                }
            }
        }

     

        [Column(Storage = "_Cash_Collected_500p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_500p
        {
            get
            {
                return this._Cash_Collected_500p;
            }
            set
            {
                if ((this._Cash_Collected_500p != value))
                {
                    this._Cash_Collected_500p = value;
                }
            }
        }

   

        [Column(Storage = "_Cash_Collected_1000p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_1000p
        {
            get
            {
                return this._Cash_Collected_1000p;
            }
            set
            {
                if ((this._Cash_Collected_1000p != value))
                {
                    this._Cash_Collected_1000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2000p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_2000p
        {
            get
            {
                return this._Cash_Collected_2000p;
            }
            set
            {
                if ((this._Cash_Collected_2000p != value))
                {
                    this._Cash_Collected_2000p = value;
                }
            }
        }

      

        [Column(Storage = "_Cash_Collected_5000p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_5000p
        {
            get
            {
                return this._Cash_Collected_5000p;
            }
            set
            {
                if ((this._Cash_Collected_5000p != value))
                {
                    this._Cash_Collected_5000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10000p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_10000p
        {
            get
            {
                return this._Cash_Collected_10000p;
            }
            set
            {
                if ((this._Cash_Collected_10000p != value))
                {
                    this._Cash_Collected_10000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20000p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_20000p
        {
            get
            {
                return this._Cash_Collected_20000p;
            }
            set
            {
                if ((this._Cash_Collected_20000p != value))
                {
                    this._Cash_Collected_20000p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50000p", DbType = "Float")]
        public System.Nullable<double> Cash_Collected_50000p
        {
            get
            {
                return this._Cash_Collected_50000p;
            }
            set
            {
                if ((this._Cash_Collected_50000p != value))
                {
                    this._Cash_Collected_50000p = value;
                }
            }
        }

        [Column(Storage = "_Declared_Tickets", DbType = "Real NOT NULL")]
        public float Declared_Tickets
        {
            get
            {
                return this._Declared_Tickets;
            }
            set
            {
                if ((this._Declared_Tickets != value))
                {
                    this._Declared_Tickets = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Printed", DbType = "Real NOT NULL")]
        public float Tickets_Printed
        {
            get
            {
                return this._Tickets_Printed;
            }
            set
            {
                if ((this._Tickets_Printed != value))
                {
                    this._Tickets_Printed = value;
                }
            }
        }

        [Column(Storage = "_DecHandpay", DbType = "Float")]
        public System.Nullable<double> DecHandpay
        {
            get
            {
                return this._DecHandpay;
            }
            set
            {
                if ((this._DecHandpay != value))
                {
                    this._DecHandpay = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Declared", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Declared
        {
            get
            {
                return this._Progressive_Value_Declared;
            }
            set
            {
                if ((this._Progressive_Value_Declared != value))
                {
                    this._Progressive_Value_Declared = value;
                }
            }
        }

        [Column(Storage = "_Hand_Pay", DbType = "Float")]
        public System.Nullable<double> Hand_Pay
        {
            get
            {
                return this._Hand_Pay;
            }
            set
            {
                if ((this._Hand_Pay != value))
                {
                    this._Hand_Pay = value;
                }
            }
        }
        [Column(Storage = "_Region", DbType = "VarChar(50)")]
        public string Region 
        {  
            get
            {

                return this._Region;
            }
            set
            {
                if ((this._Region != value))
                {
                    this._Region = value;
                }
            }
        }
    }

    public partial class usp_EditCollectionResult
    {

        private System.Nullable<int> _Column1;

        private System.Nullable<int> _Column2;

        private System.Nullable<double> _Column3;

        public usp_EditCollectionResult()
        {
        }

        [Column(Storage = "_Column1", DbType = "Int")]
        public System.Nullable<int> Column1
        {
            get
            {
                return this._Column1;
            }
            set
            {
                if ((this._Column1 != value))
                {
                    this._Column1 = value;
                }
            }
        }

        [Column(Storage = "_Column2", DbType = "Int")]
        public System.Nullable<int> Column2
        {
            get
            {
                return this._Column2;
            }
            set
            {
                if ((this._Column2 != value))
                {
                    this._Column2 = value;
                }
            }
        }

        [Column(Storage = "_Column3", DbType = "Float")]
        public System.Nullable<double> Column3
        {
            get
            {
                return this._Column3;
            }
            set
            {
                if ((this._Column3 != value))
                {
                    this._Column3 = value;
                }
            }
        }
    }
}
