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
        [Function(Name = "dbo.rsp_ecGetCollectionEvents")]
        public ISingleResult<rsp_ecGetCollectionEventsResult> GetCollectionEvents([Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID);
            return ((ISingleResult<rsp_ecGetCollectionEventsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ecGetDropBatchBreakDown")]
        public ISingleResult<rsp_ecGetDropBatchBreakDownResult> GetDropBatchBreakDown([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "Batch_ID", DbType = "Int")] System.Nullable<int> batch_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, batch_ID);
            return ((ISingleResult<rsp_ecGetDropBatchBreakDownResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_FillBatchSummary")]
        public ISingleResult<rsp_FillBatchSummaryResult> FillBatchSummary([Parameter(Name = "Batch_ID", DbType = "Int")] System.Nullable<int> batch_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batch_ID);
            return ((ISingleResult<rsp_FillBatchSummaryResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_ecGetDropBatchBreakDownByWeek")]
        public ISingleResult<rsp_ecGetDropBatchBreakDownResult> GetDropWeekBreakdown([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID, [Parameter(Name = "Week_ID", DbType = "Int")] System.Nullable<int> Week_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, Week_ID);
            return ((ISingleResult<rsp_ecGetDropBatchBreakDownResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSiteViewerCollsWeekDetail")]
        public ISingleResult<rsp_GetSiteViewerCollsWeekDetailResult> GetSiteViewerCollsWeekDetail([Parameter(Name = "WeekId", DbType = "Int")] System.Nullable<int> weekId, [Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), weekId, site);
            return ((ISingleResult<rsp_GetSiteViewerCollsWeekDetailResult>)(result.ReturnValue));
        }
    }
    public partial class rsp_ecGetCollectionEventsResult
    {

        private string _Type;

        private int _Event_ID;

        private string _Event_Date;

        private string _EVENT_Time;

        private System.Nullable<double> _Duration;

        private string _Description;

        public rsp_ecGetCollectionEventsResult()
        {
        }

        [Column(Storage = "_Type", DbType = "VarChar(5) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_Event_ID", DbType = "Int NOT NULL")]
        public int Event_ID
        {
            get
            {
                return this._Event_ID;
            }
            set
            {
                if ((this._Event_ID != value))
                {
                    this._Event_ID = value;
                }
            }
        }

        [Column(Storage = "_Event_Date", DbType = "VarChar(30)")]
        public string Event_Date
        {
            get
            {
                return this._Event_Date;
            }
            set
            {
                if ((this._Event_Date != value))
                {
                    this._Event_Date = value;
                }
            }
        }

        [Column(Storage = "_EVENT_Time", DbType = "VarChar(8)")]
        public string EVENT_Time
        {
            get
            {
                return this._EVENT_Time;
            }
            set
            {
                if ((this._EVENT_Time != value))
                {
                    this._EVENT_Time = value;
                }
            }
        }

        [Column(Storage = "_Duration", DbType = "Float")]
        public System.Nullable<double> Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                if ((this._Duration != value))
                {
                    this._Duration = value;
                }
            }
        }

        [Column(Storage = "_Description", DbType = "VarChar(60)")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this._Description = value;
                }
            }
        }
    }

    public partial class rsp_ecGetDropBatchBreakDownResult
    {

        private System.Nullable<bool> _IsSAS;

        private int _Collection_ID;

        private int _Installation_ID;

        private string _Zone_Name;

        private string _PosName;

        private string _MachineName;

        private string _StockNo;

        private int _Collection_Days;

        private System.Nullable<double> _DecWinOrLoss;

        private System.Nullable<double> _MeterWinOrLoss;

        private System.Nullable<double> _TakeVariance;

        private System.Nullable<float> _Handle;

        private System.Nullable<double> _nCasino;

        private System.Nullable<double> _nHold;

        private float _Declared_Coins;

        private float _DeFloat;

        private float _Refills;

        private System.Nullable<float> _Refunds;

        private System.Nullable<float> _Net_Coin;

        private System.Nullable<int> _Datapak_ID;

        private float _RDC_Coins;

        private float _RDC_Notes;

        private float _VTP;

        private int _Collection_EDC_Status;

        private System.Nullable<float> _Coin_Var;

        private float _Declared_Notes;

  

        private float _Note_Var;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _RDCTickets;

        private System.Nullable<double> _Ticket_Var;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _Handpay_Var;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Meter;

        private System.Nullable<double> _Progressive_Value_Variance;

        private float _Collection_Total_Power_Duration;

        private System.Nullable<int> _Total_Fault_Events;

        private System.Nullable<int> _Total_Door_Events;

        private System.Nullable<int> _Total_Power_Events;

        private float _PromoCashableIn;

        private float _PromoNonCashableIn;

        public rsp_ecGetDropBatchBreakDownResult()
        {
        }

        [Column(Storage = "_IsSAS", DbType = "Bit")]
        public System.Nullable<bool> IsSAS
        {
            get
            {
                return this._IsSAS;
            }
            set
            {
                if ((this._IsSAS != value))
                {
                    this._IsSAS = value;
                }
            }
        }

        [Column(Storage = "_Collection_ID", DbType = "Int NOT NULL")]
        public int Collection_ID
        {
            get
            {
                return this._Collection_ID;
            }
            set
            {
                if ((this._Collection_ID != value))
                {
                    this._Collection_ID = value;
                }
            }
        }

        [Column(Storage = "_Installation_ID", DbType = "Int NOT NULL")]
        public int Installation_ID
        {
            get
            {
                return this._Installation_ID;
            }
            set
            {
                if ((this._Installation_ID != value))
                {
                    this._Installation_ID = value;
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

        [Column(Storage = "_PosName", DbType = "VarChar(50)")]
        public string PosName
        {
            get
            {
                return this._PosName;
            }
            set
            {
                if ((this._PosName != value))
                {
                    this._PosName = value;
                }
            }
        }

        [Column(Storage = "_MachineName", DbType = "VarChar(50)")]
        public string MachineName
        {
            get
            {
                return this._MachineName;
            }
            set
            {
                if ((this._MachineName != value))
                {
                    this._MachineName = value;
                }
            }
        }

        [Column(Storage = "_StockNo", DbType = "VarChar(50)")]
        public string StockNo
        {
            get
            {
                return this._StockNo;
            }
            set
            {
                if ((this._StockNo != value))
                {
                    this._StockNo = value;
                }
            }
        }

        [Column(Storage = "_Collection_Days", DbType = "Int NOT NULL")]
        public int Collection_Days
        {
            get
            {
                return this._Collection_Days;
            }
            set
            {
                if ((this._Collection_Days != value))
                {
                    this._Collection_Days = value;
                }
            }
        }

        [Column(Storage = "_DecWinOrLoss", DbType = "Float")]
        public System.Nullable<double> DecWinOrLoss
        {
            get
            {
                return this._DecWinOrLoss;
            }
            set
            {
                if ((this._DecWinOrLoss != value))
                {
                    this._DecWinOrLoss = value;
                }
            }
        }

        [Column(Storage = "_MeterWinOrLoss", DbType = "Float")]
        public System.Nullable<double> MeterWinOrLoss
        {
            get
            {
                return this._MeterWinOrLoss;
            }
            set
            {
                if ((this._MeterWinOrLoss != value))
                {
                    this._MeterWinOrLoss = value;
                }
            }
        }

        [Column(Storage = "_TakeVariance", DbType = "Float")]
        public System.Nullable<double> TakeVariance
        {
            get
            {
                return this._TakeVariance;
            }
            set
            {
                if ((this._TakeVariance != value))
                {
                    this._TakeVariance = value;
                }
            }
        }

        [Column(Storage = "_Handle", DbType = "Real")]
        public System.Nullable<float> Handle
        {
            get
            {
                return this._Handle;
            }
            set
            {
                if ((this._Handle != value))
                {
                    this._Handle = value;
                }
            }
        }

        [Column(Storage = "_nCasino", DbType = "Float")]
        public System.Nullable<double> nCasino
        {
            get
            {
                return this._nCasino;
            }
            set
            {
                if ((this._nCasino != value))
                {
                    this._nCasino = value;
                }
            }
        }

        [Column(Storage = "_nHold", DbType = "Float")]
        public System.Nullable<double> nHold
        {
            get
            {
                return this._nHold;
            }
            set
            {
                if ((this._nHold != value))
                {
                    this._nHold = value;
                }
            }
        }

        [Column(Storage = "_Declared_Coins", DbType = "Real NOT NULL")]
        public float Declared_Coins
        {
            get
            {
                return this._Declared_Coins;
            }
            set
            {
                if ((this._Declared_Coins != value))
                {
                    this._Declared_Coins = value;
                }
            }
        }

        [Column(Storage = "_DeFloat", DbType = "Real NOT NULL")]
        public float DeFloat
        {
            get
            {
                return this._DeFloat;
            }
            set
            {
                if ((this._DeFloat != value))
                {
                    this._DeFloat = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Real NOT NULL")]
        public float Refills
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

        [Column(Storage = "_Refunds", DbType = "Real")]
        public System.Nullable<float> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_Net_Coin", DbType = "Real")]
        public System.Nullable<float> Net_Coin
        {
            get
            {
                return this._Net_Coin;
            }
            set
            {
                if ((this._Net_Coin != value))
                {
                    this._Net_Coin = value;
                }
            }
        }

        [Column(Storage = "_Datapak_ID", DbType = "Int")]
        public System.Nullable<int> Datapak_ID
        {
            get
            {
                return this._Datapak_ID;
            }
            set
            {
                if ((this._Datapak_ID != value))
                {
                    this._Datapak_ID = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Real NOT NULL")]
        public float RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_RDC_Notes", DbType = "Real NOT NULL")]
        public float RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_VTP", DbType = "Real NOT NULL")]
        public float VTP
        {
            get
            {
                return this._VTP;
            }
            set
            {
                if ((this._VTP != value))
                {
                    this._VTP = value;
                }
            }
        }

        [Column(Storage = "_Collection_EDC_Status", DbType = "Int NOT NULL")]
        public int Collection_EDC_Status
        {
            get
            {
                return this._Collection_EDC_Status;
            }
            set
            {
                if ((this._Collection_EDC_Status != value))
                {
                    this._Collection_EDC_Status = value;
                }
            }
        }

        [Column(Storage = "_Coin_Var", DbType = "Real")]
        public System.Nullable<float> Coin_Var
        {
            get
            {
                return this._Coin_Var;
            }
            set
            {
                if ((this._Coin_Var != value))
                {
                    this._Coin_Var = value;
                }
            }
        }

        [Column(Storage = "_Declared_Notes", DbType = "Real NOT NULL")]
        public float Declared_Notes
        {
            get
            {
                return this._Declared_Notes;
            }
            set
            {
                if ((this._Declared_Notes != value))
                {
                    this._Declared_Notes = value;
                }
            }
        }

 
        [Column(Storage = "_Note_Var", DbType = "Real NOT NULL")]
        public float Note_Var
        {
            get
            {
                return this._Note_Var;
            }
            set
            {
                if ((this._Note_Var != value))
                {
                    this._Note_Var = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance
        {
            get
            {
                return this._DecTicketBalance;
            }
            set
            {
                if ((this._DecTicketBalance != value))
                {
                    this._DecTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_Void", DbType = "Float")]
        public System.Nullable<double> Void
        {
            get
            {
                return this._Void;
            }
            set
            {
                if ((this._Void != value))
                {
                    this._Void = value;
                }
            }
        }

        [Column(Storage = "_RDCTickets", DbType = "Float")]
        public System.Nullable<double> RDCTickets
        {
            get
            {
                return this._RDCTickets;
            }
            set
            {
                if ((this._RDCTickets != value))
                {
                    this._RDCTickets = value;
                }
            }
        }

        [Column(Storage = "_Ticket_Var", DbType = "Float")]
        public System.Nullable<double> Ticket_Var
        {
            get
            {
                return this._Ticket_Var;
            }
            set
            {
                if ((this._Ticket_Var != value))
                {
                    this._Ticket_Var = value;
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

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_Handpay_Var", DbType = "Float")]
        public System.Nullable<double> Handpay_Var
        {
            get
            {
                return this._Handpay_Var;
            }
            set
            {
                if ((this._Handpay_Var != value))
                {
                    this._Handpay_Var = value;
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

        [Column(Storage = "_Progressive_Value_Meter", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Meter
        {
            get
            {
                return this._Progressive_Value_Meter;
            }
            set
            {
                if ((this._Progressive_Value_Meter != value))
                {
                    this._Progressive_Value_Meter = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Variance", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Variance
        {
            get
            {
                return this._Progressive_Value_Variance;
            }
            set
            {
                if ((this._Progressive_Value_Variance != value))
                {
                    this._Progressive_Value_Variance = value;
                }
            }
        }

        [Column(Storage = "_Collection_Total_Power_Duration", DbType = "Real NOT NULL")]
        public float Collection_Total_Power_Duration
        {
            get
            {
                return this._Collection_Total_Power_Duration;
            }
            set
            {
                if ((this._Collection_Total_Power_Duration != value))
                {
                    this._Collection_Total_Power_Duration = value;
                }
            }
        }

        [Column(Storage = "_Total_Fault_Events", DbType = "Int")]
        public System.Nullable<int> Total_Fault_Events
        {
            get
            {
                return this._Total_Fault_Events;
            }
            set
            {
                if ((this._Total_Fault_Events != value))
                {
                    this._Total_Fault_Events = value;
                }
            }
        }

        [Column(Storage = "_Total_Door_Events", DbType = "Int")]
        public System.Nullable<int> Total_Door_Events
        {
            get
            {
                return this._Total_Door_Events;
            }
            set
            {
                if ((this._Total_Door_Events != value))
                {
                    this._Total_Door_Events = value;
                }
            }
        }

        [Column(Storage = "_Total_Power_Events", DbType = "Int")]
        public System.Nullable<int> Total_Power_Events
        {
            get
            {
                return this._Total_Power_Events;
            }
            set
            {
                if ((this._Total_Power_Events != value))
                {
                    this._Total_Power_Events = value;
                }
            }
        }


        [Column(Storage = "_PromoCashableIn", DbType = "Real NOT NULL")]
        public float PromoCashableIn
        {
            get
            {
                return this._PromoCashableIn;
            }
            set
            {
                if ((this._PromoCashableIn != value))
                {
                    this._PromoCashableIn = value;
                }
            }
        }

        [Column(Storage = "_PromoNonCashableIn", DbType = "Real NOT NULL")]
        public float PromoNonCashableIn
        {
            get
            {

                return this._PromoNonCashableIn;
            }
            set
            {
                if ((this._PromoNonCashableIn != value))
                {
                    this._PromoNonCashableIn = value;
                }
            }
        }

    }

    public partial class rsp_FillBatchSummaryResult
    {

        private int _Batch_ID;

        private string _BatchRef;

        private string _BatchDate;

        private string _Batch_Date_Performed;

        private string _Batch_Time;

        private string _Batch_User_Name;

        private System.Nullable<double> _Declared_Coins;

        private System.Nullable<double> _Defloat;

        private System.Nullable<double> _Refills;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<double> _Refunds;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _Declared_Notes;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _Ticket_Balance;

        private System.Nullable<double> _Ticket_Var;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _RDC_Coins;

        private System.Nullable<double> _RDC_Coins_Out;

        private System.Nullable<double> _Handpay_Var;

        private System.Nullable<double> _Coin_Var;

        private System.Nullable<double> _Note_Var;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _PercentageIn;

        private System.Nullable<double> _PercentageOut;

        private System.Nullable<double> _Handle;

        private System.Nullable<int> _BatchCount;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _DecEftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _DecEftOut;

        private System.Nullable<int> _Mystery_Attendant_Paid;

        private bool _IsAFTIncluded;
        
        private System.Nullable<double> _nCasino;

        private System.Nullable<double> _DecWinOrLoss;

        private System.Nullable<double> _MeterWinOrLoss;
        private String _RouteName;

        public rsp_FillBatchSummaryResult()
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

        [Column(Storage = "_BatchRef", DbType = "VarChar(50)")]
        public string BatchRef
        {
            get
            {
                return this._BatchRef;
            }
            set
            {
                if ((this._BatchRef != value))
                {
                    this._BatchRef = value;
                }
            }
        }

        [Column(Storage = "_BatchDate", DbType = "VarChar(30)")]
        public string BatchDate
        {
            get
            {
                return this._BatchDate;
            }
            set
            {
                if ((this._BatchDate != value))
                {
                    this._BatchDate = value;
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

        [Column(Storage = "_Declared_Coins", DbType = "Float")]
        public System.Nullable<double> Declared_Coins
        {
            get
            {
                return this._Declared_Coins;
            }
            set
            {
                if ((this._Declared_Coins != value))
                {
                    this._Declared_Coins = value;
                }
            }
        }

        [Column(Storage = "_Defloat", DbType = "Float")]
        public System.Nullable<double> Defloat
        {
            get
            {
                return this._Defloat;
            }
            set
            {
                if ((this._Defloat != value))
                {
                    this._Defloat = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Float")]
        public System.Nullable<double> Refills
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

        [Column(Storage = "_RDC_Notes", DbType = "Float")]
        public System.Nullable<double> RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_Refunds", DbType = "Float")]
        public System.Nullable<double> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
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

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_Declared_Notes", DbType = "Float")]
        public System.Nullable<double> Declared_Notes
        {
            get
            {
                return this._Declared_Notes;
            }
            set
            {
                if ((this._Declared_Notes != value))
                {
                    this._Declared_Notes = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance
        {
            get
            {
                return this._DecTicketBalance;
            }
            set
            {
                if ((this._DecTicketBalance != value))
                {
                    this._DecTicketBalance = value;
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

        [Column(Storage = "_Net_Coin", DbType = "Float")]
        public System.Nullable<double> Net_Coin
        {
            get
            {
                return this._Net_Coin;
            }
            set
            {
                if ((this._Net_Coin != value))
                {
                    this._Net_Coin = value;
                }
            }
        }

        [Column(Storage = "_Ticket_Balance", DbType = "Float")]
        public System.Nullable<double> Ticket_Balance
        {
            get
            {
                return this._Ticket_Balance;
            }
            set
            {
                if ((this._Ticket_Balance != value))
                {
                    this._Ticket_Balance = value;
                }
            }
        }

        [Column(Storage = "_Ticket_Var", DbType = "Float")]
        public System.Nullable<double> Ticket_Var
        {
            get
            {
                return this._Ticket_Var;
            }
            set
            {
                if ((this._Ticket_Var != value))
                {
                    this._Ticket_Var = value;
                }
            }
        }

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Float")]
        public System.Nullable<double> RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Coins_Out
        {
            get
            {
                return this._RDC_Coins_Out;
            }
            set
            {
                if ((this._RDC_Coins_Out != value))
                {
                    this._RDC_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Handpay_Var", DbType = "Float")]
        public System.Nullable<double> Handpay_Var
        {
            get
            {
                return this._Handpay_Var;
            }
            set
            {
                if ((this._Handpay_Var != value))
                {
                    this._Handpay_Var = value;
                }
            }
        }

        [Column(Storage = "_Coin_Var", DbType = "Float")]
        public System.Nullable<double> Coin_Var
        {
            get
            {
                return this._Coin_Var;
            }
            set
            {
                if ((this._Coin_Var != value))
                {
                    this._Coin_Var = value;
                }
            }
        }

        [Column(Storage = "_Note_Var", DbType = "Float")]
        public System.Nullable<double> Note_Var
        {
            get
            {
                return this._Note_Var;
            }
            set
            {
                if ((this._Note_Var != value))
                {
                    this._Note_Var = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Variance", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Variance
        {
            get
            {
                return this._Progressive_Value_Variance;
            }
            set
            {
                if ((this._Progressive_Value_Variance != value))
                {
                    this._Progressive_Value_Variance = value;
                }
            }
        }

        [Column(Storage = "_PercentageIn", DbType = "Float")]
        public System.Nullable<double> PercentageIn
        {
            get
            {
                return this._PercentageIn;
            }
            set
            {
                if ((this._PercentageIn != value))
                {
                    this._PercentageIn = value;
                }
            }
        }

        [Column(Storage = "_PercentageOut", DbType = "Float")]
        public System.Nullable<double> PercentageOut
        {
            get
            {
                return this._PercentageOut;
            }
            set
            {
                if ((this._PercentageOut != value))
                {
                    this._PercentageOut = value;
                }
            }
        }

        [Column(Storage = "_Handle", DbType = "Float")]
        public System.Nullable<double> Handle
        {
            get
            {
                return this._Handle;
            }
            set
            {
                if ((this._Handle != value))
                {
                    this._Handle = value;
                }
            }
        }

        [Column(Storage = "_BatchCount", DbType = "Int")]
        public System.Nullable<int> BatchCount
        {
            get
            {
                return this._BatchCount;
            }
            set
            {
                if ((this._BatchCount != value))
                {
                    this._BatchCount = value;
                }
            }
        }

        [Column(Storage = "_EftIn", DbType = "Float")]
        public System.Nullable<double> EftIn
        {
            get
            {
                return this._EftIn;
            }
            set
            {
                if ((this._EftIn != value))
                {
                    this._EftIn = value;
                }
            }
        }

        [Column(Storage = "_DecEftIn", DbType = "Float")]
        public System.Nullable<double> DecEftIn
        {
            get
            {
                return this._DecEftIn;
            }
            set
            {
                if ((this._DecEftIn != value))
                {
                    this._DecEftIn = value;
                }
            }
        }

        [Column(Storage = "_EftOut", DbType = "Float")]
        public System.Nullable<double> EftOut
        {
            get
            {
                return this._EftOut;
            }
            set
            {
                if ((this._EftOut != value))
                {
                    this._EftOut = value;
                }
            }
        }

        [Column(Storage = "_DecEftOut", DbType = "Float")]
        public System.Nullable<double> DecEftOut
        {
            get
            {
                return this._DecEftOut;
            }
            set
            {
                if ((this._DecEftOut != value))
                {
                    this._DecEftOut = value;
                }
            }
        }

        [Column(Storage = "_Mystery_Attendant_Paid", DbType = "Int")]
        public System.Nullable<int> Mystery_Attendant_Paid
        {
            get
            {
                return this._Mystery_Attendant_Paid;
            }
            set
            {
                if ((this._Mystery_Attendant_Paid != value))
                {
                    this._Mystery_Attendant_Paid = value;
                }
            }
        }
        [Column(Storage = "_IsAFTIncluded", DbType = "Bit")]
        public bool IsAFTIncluded
        {
            get
            {
                return this._IsAFTIncluded;
            }
            set
            {
                if ((this._IsAFTIncluded != value))
                {
                    this._IsAFTIncluded = value;
                }
            }
        }

        [Column(Storage = "_nCasino", DbType = "Float")]
        public System.Nullable<double> nCasino
        {
            get
            {
                return this._nCasino;
            }
            set
            {
                if ((this._nCasino != value))
                {
                    this._nCasino = value;
                }
            }
        }
        [Column(Storage = "_DecWinOrLoss", DbType = "Float")]
        public System.Nullable<double> DecWinOrLoss
        {
            get
            {
                return this._DecWinOrLoss;
            }
            set
            {
                if ((this._DecWinOrLoss != value))
                {
                    this._DecWinOrLoss = value;
                }
            }
        }

        [Column(Storage = "_MeterWinOrLoss", DbType = "Float")]
        public System.Nullable<double> MeterWinOrLoss
        {
            get
            {
                return this._MeterWinOrLoss;
            }
            set
            {
                if ((this._MeterWinOrLoss != value))
                {
                    this._MeterWinOrLoss = value;
                }
            }
        }

        [Column(Storage = "_RouteName", DbType = "nvarchar(50)")]
        public String RouteName
        {
            get
            {
                return this._RouteName;
            }
            set
            {
                if ((this._RouteName != value))
                {
                    this._RouteName = value;
                }
            }
        }

    }

    public partial class rsp_GetSiteViewerCollsWeekDetailResult
    {

        private string _StartDate;

        private string _EndDate;

        private System.Nullable<int> _WeekNumber;

        private System.Nullable<int> _WeekCount;

        private System.Nullable<int> _Batch_ID;

        private string _BatchRef;

        private string _BatchDate;

        private System.Nullable<float> _BatchAdj;

        private System.Nullable<int> _Week_ID;

        private int _Site_ID;

        private string _Batch_Memo;

        private string  _Batch_User_Name;

        private string  _Batch_Time;

        private System.Nullable<int> _BatchCount;

        private System.Nullable<double> _CashCollected;

        private System.Nullable<double> _Defloat;

        private System.Nullable<double> _GrossCash;

        private System.Nullable<double> _Refills;

        private System.Nullable<double> _Refunds;

        private System.Nullable<double> _Ticket;

        private System.Nullable<double> _NetCash;

        private System.Nullable<double> _CashTake;

        private System.Nullable<double> _RDCCash;

        private System.Nullable<double> _RDCRefill;

        private System.Nullable<double> _RDCVar;

        private System.Nullable<double> _MeterCash;

        private System.Nullable<double> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _VTP;

        private System.Nullable<double> _Handle;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _PercentageIn;

        private System.Nullable<double> _PercentageOut;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _RDCHandpayVar;

        private System.Nullable<double> _MeterHandpay;

        private System.Nullable<double> _MeterHandpayVar;

        private System.Nullable<double> _HopperChange;

        private System.Nullable<double> _Handpay_Var;

        private System.Nullable<double> _Declared_Coins;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _RDC_Coins;

        private System.Nullable<double> _Coin_Var;

        private System.Nullable<double> _RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;

        private System.Nullable<double> _RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;

        private System.Nullable<double> _Declared_Notes;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<double> _Note_Var;

        private System.Nullable<double> _RDC_Coins_Out;

        private System.Nullable<double> _Tickets_Printed;

        private System.Nullable<double> _Ticket_Balance;

        private System.Nullable<double> _RDC_Ticket_Balance;

        private System.Nullable<double> _Ticket_Var;

        private System.Nullable<double> _Cash_Take;

        private System.Nullable<double> _RDC_Take;

        private System.Nullable<double> _Take_Var;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _Expired;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Progressive_Value_Meter;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _DecEftIn;

        private System.Nullable<double> _DecEftOut;

        private System.Nullable<bool> _IsAFTIncluded;

        private System.Nullable<double> _nCasino;

        private System.Nullable<double> _DecWinOrLoss;

        private System.Nullable<double> _MeterWinOrLoss;

        public rsp_GetSiteViewerCollsWeekDetailResult()
        {
        }

        [Column(Storage = "_StartDate", DbType = "VarChar(30)")]
        public string StartDate
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

        [Column(Storage = "_EndDate", DbType = "VarChar(30)")]
        public string EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this._EndDate = value;
                }
            }
        }

        [Column(Storage = "_WeekNumber", DbType = "Int")]
        public System.Nullable<int> WeekNumber
        {
            get
            {
                return this._WeekNumber;
            }
            set
            {
                if ((this._WeekNumber != value))
                {
                    this._WeekNumber = value;
                }
            }
        }

        [Column(Storage = "_WeekCount", DbType = "Int")]
        public System.Nullable<int> WeekCount
        {
            get
            {
                return this._WeekCount;
            }
            set
            {
                if ((this._WeekCount != value))
                {
                    this._WeekCount = value;
                }
            }
        }

        [Column(Storage = "_Batch_ID", DbType = "Int")]
        public System.Nullable<int> Batch_ID
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

        [Column(Storage = "_BatchRef", DbType = "VarChar(50)")]
        public string BatchRef
        {
            get
            {
                return this._BatchRef;
            }
            set
            {
                if ((this._BatchRef != value))
                {
                    this._BatchRef = value;
                }
            }
        }

        [Column(Storage = "_BatchDate", DbType = "VarChar(30)")]
        public string BatchDate
        {
            get
            {
                return this._BatchDate;
            }
            set
            {
                if ((this._BatchDate != value))
                {
                    this._BatchDate = value;
                }
            }
        }

        [Column(Storage = "_BatchAdj", DbType = "Real")]
        public System.Nullable<float> BatchAdj
        {
            get
            {
                return this._BatchAdj;
            }
            set
            {
                if ((this._BatchAdj != value))
                {
                    this._BatchAdj = value;
                }
            }
        }

        [Column(Storage = "_Week_ID", DbType = "Int")]
        public System.Nullable<int> Week_ID
        {
            get
            {
                return this._Week_ID;
            }
            set
            {
                if ((this._Week_ID != value))
                {
                    this._Week_ID = value;
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

        [Column(Storage = "_Batch_Memo", DbType = "VarChar(20) NOT NULL")]
        public string Batch_Memo
        {
            get
            {
                return this._Batch_Memo;
            }
            set
            {
                if ((this._Batch_Memo != value))
                {
                    this._Batch_Memo = value;
                }
            }
        }

        [Column(Storage = "_Batch_User_Name", DbType = "VarChar(20) NOT NULL")]
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

        [Column(Storage = "_Batch_Time", DbType = "VarChar(30) NOT NULL")]
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

        [Column(Storage = "_BatchCount", DbType = "Int")]
        public System.Nullable<int> BatchCount
        {
            get
            {
                return this._BatchCount;
            }
            set
            {
                if ((this._BatchCount != value))
                {
                    this._BatchCount = value;
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Float")]
        public System.Nullable<double> CashCollected
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

        [Column(Storage = "_Defloat", DbType = "Float")]
        public System.Nullable<double> Defloat
        {
            get
            {
                return this._Defloat;
            }
            set
            {
                if ((this._Defloat != value))
                {
                    this._Defloat = value;
                }
            }
        }

        [Column(Storage = "_GrossCash", DbType = "Float")]
        public System.Nullable<double> GrossCash
        {
            get
            {
                return this._GrossCash;
            }
            set
            {
                if ((this._GrossCash != value))
                {
                    this._GrossCash = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Float")]
        public System.Nullable<double> Refills
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

        [Column(Storage = "_Refunds", DbType = "Float")]
        public System.Nullable<double> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_Ticket", DbType = "Float")]
        public System.Nullable<double> Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                if ((this._Ticket != value))
                {
                    this._Ticket = value;
                }
            }
        }

        [Column(Storage = "_NetCash", DbType = "Float")]
        public System.Nullable<double> NetCash
        {
            get
            {
                return this._NetCash;
            }
            set
            {
                if ((this._NetCash != value))
                {
                    this._NetCash = value;
                }
            }
        }

        [Column(Storage = "_CashTake", DbType = "Float")]
        public System.Nullable<double> CashTake
        {
            get
            {
                return this._CashTake;
            }
            set
            {
                if ((this._CashTake != value))
                {
                    this._CashTake = value;
                }
            }
        }

        [Column(Storage = "_RDCCash", DbType = "Float")]
        public System.Nullable<double> RDCCash
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

        [Column(Storage = "_RDCRefill", DbType = "Float")]
        public System.Nullable<double> RDCRefill
        {
            get
            {
                return this._RDCRefill;
            }
            set
            {
                if ((this._RDCRefill != value))
                {
                    this._RDCRefill = value;
                }
            }
        }

        [Column(Storage = "_RDCVar", DbType = "Float")]
        public System.Nullable<double> RDCVar
        {
            get
            {
                return this._RDCVar;
            }
            set
            {
                if ((this._RDCVar != value))
                {
                    this._RDCVar = value;
                }
            }
        }

        [Column(Storage = "_MeterCash", DbType = "Float")]
        public System.Nullable<double> MeterCash
        {
            get
            {
                return this._MeterCash;
            }
            set
            {
                if ((this._MeterCash != value))
                {
                    this._MeterCash = value;
                }
            }
        }

        [Column(Storage = "_MeterRefill", DbType = "Float")]
        public System.Nullable<double> MeterRefill
        {
            get
            {
                return this._MeterRefill;
            }
            set
            {
                if ((this._MeterRefill != value))
                {
                    this._MeterRefill = value;
                }
            }
        }

        [Column(Storage = "_MeterVar", DbType = "Float")]
        public System.Nullable<double> MeterVar
        {
            get
            {
                return this._MeterVar;
            }
            set
            {
                if ((this._MeterVar != value))
                {
                    this._MeterVar = value;
                }
            }
        }

        [Column(Storage = "_VTP", DbType = "Float")]
        public System.Nullable<double> VTP
        {
            get
            {
                return this._VTP;
            }
            set
            {
                if ((this._VTP != value))
                {
                    this._VTP = value;
                }
            }
        }

        [Column(Storage = "_Handle", DbType = "Float")]
        public System.Nullable<double> Handle
        {
            get
            {
                return this._Handle;
            }
            set
            {
                if ((this._Handle != value))
                {
                    this._Handle = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance
        {
            get
            {
                return this._DecTicketBalance;
            }
            set
            {
                if ((this._DecTicketBalance != value))
                {
                    this._DecTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_PercentageIn", DbType = "Float")]
        public System.Nullable<double> PercentageIn
        {
            get
            {
                return this._PercentageIn;
            }
            set
            {
                if ((this._PercentageIn != value))
                {
                    this._PercentageIn = value;
                }
            }
        }

        [Column(Storage = "_PercentageOut", DbType = "Float")]
        public System.Nullable<double> PercentageOut
        {
            get
            {
                return this._PercentageOut;
            }
            set
            {
                if ((this._PercentageOut != value))
                {
                    this._PercentageOut = value;
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

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDCHandpayVar", DbType = "Float")]
        public System.Nullable<double> RDCHandpayVar
        {
            get
            {
                return this._RDCHandpayVar;
            }
            set
            {
                if ((this._RDCHandpayVar != value))
                {
                    this._RDCHandpayVar = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpay", DbType = "Float")]
        public System.Nullable<double> MeterHandpay
        {
            get
            {
                return this._MeterHandpay;
            }
            set
            {
                if ((this._MeterHandpay != value))
                {
                    this._MeterHandpay = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpayVar", DbType = "Float")]
        public System.Nullable<double> MeterHandpayVar
        {
            get
            {
                return this._MeterHandpayVar;
            }
            set
            {
                if ((this._MeterHandpayVar != value))
                {
                    this._MeterHandpayVar = value;
                }
            }
        }

        [Column(Storage = "_HopperChange", DbType = "Float")]
        public System.Nullable<double> HopperChange
        {
            get
            {
                return this._HopperChange;
            }
            set
            {
                if ((this._HopperChange != value))
                {
                    this._HopperChange = value;
                }
            }
        }

        [Column(Storage = "_Handpay_Var", DbType = "Float")]
        public System.Nullable<double> Handpay_Var
        {
            get
            {
                return this._Handpay_Var;
            }
            set
            {
                if ((this._Handpay_Var != value))
                {
                    this._Handpay_Var = value;
                }
            }
        }

        [Column(Storage = "_Declared_Coins", DbType = "Float")]
        public System.Nullable<double> Declared_Coins
        {
            get
            {
                return this._Declared_Coins;
            }
            set
            {
                if ((this._Declared_Coins != value))
                {
                    this._Declared_Coins = value;
                }
            }
        }

        [Column(Storage = "_Net_Coin", DbType = "Float")]
        public System.Nullable<double> Net_Coin
        {
            get
            {
                return this._Net_Coin;
            }
            set
            {
                if ((this._Net_Coin != value))
                {
                    this._Net_Coin = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Float")]
        public System.Nullable<double> RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_Coin_Var", DbType = "Float")]
        public System.Nullable<double> Coin_Var
        {
            get
            {
                return this._Coin_Var;
            }
            set
            {
                if ((this._Coin_Var != value))
                {
                    this._Coin_Var = value;
                }
            }
        }

        [Column(Storage = "_RDC_TICKETS_INSERTED_NONCASHABLE_VALUE", DbType = "Float")]
        public System.Nullable<double> RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
        {
            get
            {
                return this._RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;
            }
            set
            {
                if ((this._RDC_TICKETS_INSERTED_NONCASHABLE_VALUE != value))
                {
                    this._RDC_TICKETS_INSERTED_NONCASHABLE_VALUE = value;
                }
            }
        }

        [Column(Storage = "_RDC_TICKETS_PRINTED_NONCASHABLE_VALUE", DbType = "Float")]
        public System.Nullable<double> RDC_TICKETS_PRINTED_NONCASHABLE_VALUE
        {
            get
            {
                return this._RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;
            }
            set
            {
                if ((this._RDC_TICKETS_PRINTED_NONCASHABLE_VALUE != value))
                {
                    this._RDC_TICKETS_PRINTED_NONCASHABLE_VALUE = value;
                }
            }
        }

        [Column(Storage = "_Declared_Notes", DbType = "Float")]
        public System.Nullable<double> Declared_Notes
        {
            get
            {
                return this._Declared_Notes;
            }
            set
            {
                if ((this._Declared_Notes != value))
                {
                    this._Declared_Notes = value;
                }
            }
        }

        [Column(Storage = "_RDC_Notes", DbType = "Float")]
        public System.Nullable<double> RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_Note_Var", DbType = "Float")]
        public System.Nullable<double> Note_Var
        {
            get
            {
                return this._Note_Var;
            }
            set
            {
                if ((this._Note_Var != value))
                {
                    this._Note_Var = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Coins_Out
        {
            get
            {
                return this._RDC_Coins_Out;
            }
            set
            {
                if ((this._RDC_Coins_Out != value))
                {
                    this._RDC_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Printed", DbType = "Float")]
        public System.Nullable<double> Tickets_Printed
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

        [Column(Storage = "_Ticket_Balance", DbType = "Float")]
        public System.Nullable<double> Ticket_Balance
        {
            get
            {
                return this._Ticket_Balance;
            }
            set
            {
                if ((this._Ticket_Balance != value))
                {
                    this._Ticket_Balance = value;
                }
            }
        }

        [Column(Storage = "_RDC_Ticket_Balance", DbType = "Float")]
        public System.Nullable<double> RDC_Ticket_Balance
        {
            get
            {
                return this._RDC_Ticket_Balance;
            }
            set
            {
                if ((this._RDC_Ticket_Balance != value))
                {
                    this._RDC_Ticket_Balance = value;
                }
            }
        }

        [Column(Storage = "_Ticket_Var", DbType = "Float")]
        public System.Nullable<double> Ticket_Var
        {
            get
            {
                return this._Ticket_Var;
            }
            set
            {
                if ((this._Ticket_Var != value))
                {
                    this._Ticket_Var = value;
                }
            }
        }

        [Column(Storage = "_Cash_Take", DbType = "Float")]
        public System.Nullable<double> Cash_Take
        {
            get
            {
                return this._Cash_Take;
            }
            set
            {
                if ((this._Cash_Take != value))
                {
                    this._Cash_Take = value;
                }
            }
        }

        [Column(Storage = "_RDC_Take", DbType = "Float")]
        public System.Nullable<double> RDC_Take
        {
            get
            {
                return this._RDC_Take;
            }
            set
            {
                if ((this._RDC_Take != value))
                {
                    this._RDC_Take = value;
                }
            }
        }

        [Column(Storage = "_Take_Var", DbType = "Float")]
        public System.Nullable<double> Take_Var
        {
            get
            {
                return this._Take_Var;
            }
            set
            {
                if ((this._Take_Var != value))
                {
                    this._Take_Var = value;
                }
            }
        }

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_Expired", DbType = "Float")]
        public System.Nullable<double> Expired
        {
            get
            {
                return this._Expired;
            }
            set
            {
                if ((this._Expired != value))
                {
                    this._Expired = value;
                }
            }
        }

        [Column(Storage = "_Void", DbType = "Float")]
        public System.Nullable<double> Void
        {
            get
            {
                return this._Void;
            }
            set
            {
                if ((this._Void != value))
                {
                    this._Void = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Meter", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Meter
        {
            get
            {
                return this._Progressive_Value_Meter;
            }
            set
            {
                if ((this._Progressive_Value_Meter != value))
                {
                    this._Progressive_Value_Meter = value;
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

        [Column(Storage = "_Progressive_Value_Variance", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Variance
        {
            get
            {
                return this._Progressive_Value_Variance;
            }
            set
            {
                if ((this._Progressive_Value_Variance != value))
                {
                    this._Progressive_Value_Variance = value;
                }
            }
        }

        [Column(Storage = "_EftIn", DbType = "Float")]
        public System.Nullable<double> EftIn
        {
            get
            {
                return this._EftIn;
            }
            set
            {
                if ((this._EftIn != value))
                {
                    this._EftIn = value;
                }
            }
        }

        [Column(Storage = "_EftOut", DbType = "Float")]
        public System.Nullable<double> EftOut
        {
            get
            {
                return this._EftOut;
            }
            set
            {
                if ((this._EftOut != value))
                {
                    this._EftOut = value;
                }
            }
        }

        [Column(Storage = "_DecEftIn", DbType = "Float")]
        public System.Nullable<double> DecEftIn
        {
            get
            {
                return this._DecEftIn;
            }
            set
            {
                if ((this._DecEftIn != value))
                {
                    this._DecEftIn = value;
                }
            }
        }

        [Column(Storage = "_DecEftOut", DbType = "Float")]
        public System.Nullable<double> DecEftOut
        {
            get
            {
                return this._DecEftOut;
            }
            set
            {
                if ((this._DecEftOut != value))
                {
                    this._DecEftOut = value;
                }
            }
        }

        [Column(Storage = "_IsAFTIncluded", DbType = "Bit")]
        public System.Nullable<bool> IsAFTIncluded
        {
            get
            {
                return this._IsAFTIncluded;
            }
            set
            {
                if ((this._IsAFTIncluded != value))
                {
                    this._IsAFTIncluded = value;
                }
            }
        }
        
        [Column(Storage = "_nCasino", DbType = "Float")]
        public System.Nullable<double> nCasino
        {
            get
            {
                return this._nCasino;
            }
            set
            {
                if ((this._nCasino != value))
                {
                    this._nCasino = value;
                }
            }
        }

        [Column(Storage = "_DecWinOrLoss", DbType = "Float")]
        public System.Nullable<double> DecWinOrLoss
        {
            get
            {
                return this._DecWinOrLoss;
            }
            set
            {
                if ((this._DecWinOrLoss != value))
                {
                    this._DecWinOrLoss = value;
                }
            }
        }

        [Column(Storage = "_MeterWinOrLoss", DbType = "Float")]
        public System.Nullable<double> MeterWinOrLoss
        {
            get
            {
                return this._MeterWinOrLoss;
            }
            set
            {
                if ((this._MeterWinOrLoss != value))
                {
                    this._MeterWinOrLoss = value;
                }
            }
        }

    }
}
