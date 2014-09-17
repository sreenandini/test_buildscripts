using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class CollectionEvents
    {

        private string _Type;

        private int _Event_ID;

        private string _Event_Date;

        private string _EVENT_Time;

        private double _Duration;

        private string _Description;

        public CollectionEvents()
        {
        }

      
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


        public double Duration
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

    public partial class DropBatchBreakDown
    {

        private bool _IsSAS;

        private int _Collection_ID;

        private int _Installation_ID;

        private string _Zone_Name;

        private string _PosName;

        private string _MachineName;

        private string _StockNo;

        private int _Collection_Days;

        private double _DecWinOrLoss;

        private double _MeterWinOrLoss;

        private double _TakeVariance;

        private float _Handle;

        private double _nCasino;

        private double _nHold;

        private float _Declared_Coins;

        private float _DeFloat;

        private float _Refills;

        private float _Refunds;

        private float _Net_Coin;

        private int _Datapak_ID;

        private float _RDC_Coins;

        private float _RDC_Notes;

        private float _VTP;

        private int _Collection_EDC_Status;

        private float _Coin_Var;

        private float _Declared_Notes;

        private float _RDC_Notes1;

        private float _Note_Var;

        private double _DecTicketBalance;

        private double _Shortpay;

        private double _Void;

        private double _RDCTickets;

        private double _Ticket_Var;

        private double _DecHandpay;

        private double _RDCHandpay;

        private double _Handpay_Var;

        private double _Progressive_Value_Declared;

        private double _Progressive_Value_Meter;

        private double _Progressive_Value_Variance;

        private float _Collection_Total_Power_Duration;

        private int _Total_Fault_Events;

        private int _Total_Door_Events;

        private int _Total_Power_Events;

        private int _Collection_Days1;

        private float _PromoCashableIn;

        private float _PromoNonCashableIn;

        public DropBatchBreakDown()
        {
        }

    
        public bool IsSAS
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

     
        public double DecWinOrLoss
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

      
        public double MeterWinOrLoss
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

     
        public double TakeVariance
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

      
        public float Handle
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

     
        public double nCasino
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

  
        public double nHold
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

       
        public float Refunds
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

  
        public float Net_Coin
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


        public int Datapak_ID
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

 
        public float Coin_Var
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

     
        public float RDC_Notes1
        {
            get
            {
                return this._RDC_Notes1;
            }
            set
            {
                if ((this._RDC_Notes1 != value))
                {
                    this._RDC_Notes1 = value;
                }
            }
        }

       
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

   
        public double DecTicketBalance
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

 
        public double Shortpay
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


        public double Void
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

      
        public double RDCTickets
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

    
        public double Ticket_Var
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


        public double DecHandpay
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

   
        public double RDCHandpay
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

      
        public double Handpay_Var
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

    
        public double Progressive_Value_Declared
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

     
        public double Progressive_Value_Meter
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

       
        public double Progressive_Value_Variance
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

     
        public int Total_Fault_Events
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

       
        public int Total_Door_Events
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

       
        public int Total_Power_Events
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

       
        public int Collection_Days1
        {
            get
            {
                return this._Collection_Days1;
            }
            set
            {
                if ((this._Collection_Days1 != value))
                {
                    this._Collection_Days1 = value;
                }
            }
        }

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
    public partial class FillBatchSummary 
    {

        private int _Batch_ID;

        private string _BatchRef;

        private string _BatchDate;

        private string _Batch_Date_Performed;

        private string _Batch_Time;

        private string _Batch_User_Name;

        private double _Declared_Coins;

        private double _Defloat;

        private double _Refills;

        private double _RDC_Notes;

        private double _Refunds;

        private double _Progressive_Value_Declared;

        private double _Shortpay;

        private double _Declared_Notes;

        private double _DecTicketBalance;

        private double _DecHandpay;

        private double _Net_Coin;

        private double _Ticket_Balance;

        private double _Ticket_Var;

        private double _RDCHandpay;

        private double _RDC_Coins;

        private double _RDC_Coins_Out;

        private double _Handpay_Var;

        private double _Coin_Var;

        private double _Note_Var;

        private double _Progressive_Value_Variance;

        private double _PercentageIn;

        private double _PercentageOut;

        private double _Handle;

        private int _BatchCount;

        private double _EftIn;

        private double _DecEftIn;

        private double _EftOut;

        private double _DecEftOut;

        private int _Mystery_Attendant_Paid;
        private bool _IsAFTIncluded;

        private String _RouteName;
        public FillBatchSummary()
        {
        }

        
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

        
        public double Declared_Coins
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

        
        public double Defloat
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

        
        public double Refills
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

        
        public double RDC_Notes
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

        
        public double Refunds
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

        
        public double Progressive_Value_Declared
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

        
        public double Shortpay
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

        
        public double Declared_Notes
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

        
        public double DecTicketBalance
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

        
        public double DecHandpay
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

        
        public double Net_Coin
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

        
        public double Ticket_Balance
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

        
        public double Ticket_Var
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

        
        public double RDCHandpay
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

        
        public double RDC_Coins
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

        
        public double RDC_Coins_Out
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

        
        public double Handpay_Var
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

        
        public double Coin_Var
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

        
        public double Note_Var
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

        
        public double Progressive_Value_Variance
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

        
        public double PercentageIn
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

        
        public double PercentageOut
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

        
        public double Handle
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

        
        public int BatchCount
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

        
        public double EftIn
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

        
        public double DecEftIn
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

        
        public double EftOut
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

        
        public double DecEftOut
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

        
        public int Mystery_Attendant_Paid
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
        public bool IsAFTIncluded
        {
            get
            {
                return this._IsAFTIncluded;
            }
            set
            {
                _IsAFTIncluded = value;
            }
        }
        public double DecWinOrLoss { get; set; }
        public double MeterWinOrLoss { get; set; }
        public double nHold { get; set; }
        public double nCasino { get; set; }

        public String RouteName
        {
            get
            {
                return this._RouteName;
            }
            set
            {
                _RouteName = value;
            }
        }
        }

    public partial class CollsWeekDetailSummary
    {

        private string _StartDate;

        private string _EndDate;

        private int _WeekNumber;

        private int _WeekCount;

        private int _Batch_ID;

        private string _BatchRef;

        private string _BatchDate;

        private System.Nullable<float> _BatchAdj;

        private int _Week_ID;

        private int _Site_ID;

        private string _Batch_Memo;

        private string _Batch_User_Name;

        private string _Batch_Time;

        private int _BatchCount;

        private double _CashCollected;

        private double _Defloat;

        private double _GrossCash;

        private double _Refills;

        private double _Refunds;

        private double _Ticket;

        private double _NetCash;

        private double _CashTake;

        private double _RDCCash;

        private double _RDCRefill;

        private double _RDCVar;

        private double _MeterCash;

        private double _MeterRefill;

        private double _MeterVar;

        private double _VTP;

        private double _Handle;

        private double _DecTicketBalance;

        private double _PercentageIn;

        private double _PercentageOut;

        private double _DecHandpay;

        private double _RDCHandpay;

        private double _RDCHandpayVar;

        private double _MeterHandpay;

        private double _MeterHandpayVar;

        private double _HopperChange;

        private double _Handpay_Var;

        private double _Declared_Coins;

        private double _Net_Coin;

        private double _RDC_Coins;

        private double _Coin_Var;

        private double _RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;

        private double _RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;

        private double _Declared_Notes;

        private double _RDC_Notes;

        private double _Note_Var;

        private double _RDC_Coins_Out;

        private double _Tickets_Printed;

        private double _Ticket_Balance;

        private double _RDC_Ticket_Balance;

        private double _Ticket_Var;

        private double _Cash_Take;

        private double _RDC_Take;

        private double _Take_Var;

        private double _Shortpay;

        private double _Expired;

        private double _Void;

        private double _Progressive_Value_Meter;

        private double _Progressive_Value_Declared;

        private double _Progressive_Value_Variance;

        private double _EftIn;

        private double _EftOut;

        private double _DecEftIn;

        private double _DecEftOut;

        private bool _IsAFTIncluded;

        private double _PromoCashableIn;

        private double _PromoNonCashableIn;

        public CollsWeekDetailSummary()
        {
        }


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


        public int WeekNumber
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


        public int WeekCount
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


        public int Week_ID
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


        public int BatchCount
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


        public double CashCollected
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


        public double Defloat
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


        public double GrossCash
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


        public double Refills
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


        public double Refunds
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


        public double Ticket
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


        public double NetCash
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


        public double CashTake
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


        public double RDCCash
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


        public double RDCRefill
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


        public double RDCVar
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


        public double MeterCash
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


        public double MeterRefill
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


        public double MeterVar
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


        public double VTP
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


        public double Handle
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


        public double DecTicketBalance
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


        public double PercentageIn
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


        public double PercentageOut
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


        public double DecHandpay
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


        public double RDCHandpay
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


        public double RDCHandpayVar
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


        public double MeterHandpay
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


        public double MeterHandpayVar
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


        public double HopperChange
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


        public double Handpay_Var
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


        public double Declared_Coins
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


        public double Net_Coin
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


        public double RDC_Coins
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


        public double Coin_Var
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


        public double RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
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


        public double RDC_TICKETS_PRINTED_NONCASHABLE_VALUE
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


        public double Declared_Notes
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


        public double RDC_Notes
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


        public double Note_Var
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


        public double RDC_Coins_Out
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


        public double Tickets_Printed
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


        public double Ticket_Balance
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


        public double RDC_Ticket_Balance
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


        public double Ticket_Var
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


        public double Cash_Take
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


        public double RDC_Take
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


        public double Take_Var
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


        public double Shortpay
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


        public double Expired
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


        public double Void
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


        public double Progressive_Value_Meter
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


        public double Progressive_Value_Declared
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


        public double Progressive_Value_Variance
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


        public double EftIn
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


        public double EftOut
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


        public double DecEftIn
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


        public double DecEftOut
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

        public double PromoCashableIn
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


        public double PromoNonCashableIn
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
        public double DecWinOrLoss { get; set; }
        public double MeterWinOrLoss { get; set; }
        public double nHold { get; set; }
        public double nCasino { get; set; } 

    }


}
