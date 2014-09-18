using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Text.RegularExpressions;
using BMC.Common.Utilities;

namespace BMC.Transport
{

    [Table(Name = "dbo.VW_S_CollectionData")]
    public partial class CollectionView
    {

        private System.Nullable<int> _Site_No;

        private string _Name;

        private string _Code;

        private int _Collection_Batch_No;

        private string _Collection_Batch_Name;

        private System.Nullable<System.DateTime> _Collection_Batch_Date;

        private System.Nullable<System.DateTime> _Collection_Performed_Date;

        private int _Collection_No;

        private int _Installation_No;

        private System.Nullable<int> _Installation_Price_of_Play;

        private string _DropType;

        private System.Nullable<int> _Datapak_No;

        private System.Nullable<System.DateTime> _Start_Date;

        private System.Nullable<System.DateTime> _End_Date;

        private System.Nullable<int> _Zone_No;

        private int _Bar_Pos_No;

        private int _Machine_Type_ID;

        private string _Machine_Type_Code;

        private System.Nullable<int> _Manufacturer_ID;

        private string _Manufacturer_Name;

        private int _Machine_Class_No;

        private System.Nullable<int> _Week_ID;

        private System.Nullable<System.DateTime> _Calendar_Week_Start_Date;

        private System.Nullable<System.DateTime> _Calendar_Week_End_Date;

        private System.Nullable<int> _Calendar_Week_Number;

        private System.Nullable<int> _Period_ID;

        private System.Nullable<System.DateTime> _Calendar_Period_Start_Date;

        private System.Nullable<System.DateTime> _Calendar_Period_End_Date;

        private System.Nullable<int> _Calendar_Period_Number;

        private string _ZoneName;

        private string _PosName;

        private string _MachineName;

        private string _UserName;

        private string _Stock_No;

        private System.Nullable<bool> _Machine_Uses_Meters;

        private System.Nullable<bool> _IsTicket;

        private int _IsSAS;

        private System.Nullable<System.DateTime> _BatchDate;

        private System.Nullable<double> _BatchAdj;

        private int _CollectionCount;

        private System.Nullable<System.DateTime> _CollectionDate;

        private System.Nullable<System.DateTime> _PreviousCollectionDate;

        private System.Nullable<int> _CollectionDays;

        private System.Nullable<System.DateTime> _CollectionDateTime;

        private System.Nullable<bool> _Declaration;

        private System.Nullable<int> _Collection_EDC_Status;

        private System.Nullable<float> _CashCollected;

        private System.Nullable<float> _Cash_Collected_1p;

        private System.Nullable<float> _Cash_Collected_2p;

        private System.Nullable<float> _Cash_Collected_5p;

        private System.Nullable<float> _Cash_Collected_10p;

        private System.Nullable<float> _Cash_Collected_20p;

        private System.Nullable<float> _Cash_Collected_50p;

        private System.Nullable<float> _Cash_Collected_100p;

        private System.Nullable<float> _Cash_Collected_200p;

        private System.Nullable<float> _Cash_Collected_500p;

        private System.Nullable<float> _Cash_Collected_1000p;

        private System.Nullable<float> _Cash_Collected_2000p;

        private System.Nullable<float> _Cash_Collected_5000p;

        private System.Nullable<float> _Cash_Collected_10000p;

        private System.Nullable<float> _Cash_Collected_20000p;

        private System.Nullable<float> _Cash_Collected_50000p;

        private System.Nullable<float> _Cash_Collected_100000p;

        private System.Nullable<double> _COLLECTION_RDC_JACKPOT;

        private System.Nullable<float> _DeclaredTicketValue;

        private System.Nullable<int> _DeclaredTicketQty;

        private System.Nullable<decimal> _PromoCashableValue;

        private System.Nullable<decimal> _PromoNonCashableValue;


        private System.Nullable<float> _Defloat;

        private System.Nullable<float> _GrossCash;

        private System.Nullable<double> _Refills;

        private System.Nullable<float> _ManualRefills;

        private System.Nullable<double> _RDCRefill;

        private System.Nullable<float> _Refunds;

        private System.Nullable<double> _Ticket;

        private System.Nullable<double> _NetCash;

        private System.Nullable<double> _HopperChange;

        private System.Nullable<double> _RDCCash;

        private System.Nullable<double> _RDCIn;

        private System.Nullable<double> _RDCOut;

        private System.Nullable<double> _RDCVar;

        private System.Nullable<int> _MeterCash;

        private System.Nullable<int> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _VTP;

        private System.Nullable<double> _PercentageIn;

        private System.Nullable<double> _PercentageOut;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _HandpayVar;

        private System.Nullable<int> _MeterHandpay;

        private System.Nullable<double> _RDC_Total_Coinage_In;

        private System.Nullable<double> _RDC_Total_Coinage_Out;

        private System.Nullable<double> _RDC_Coin;

        private System.Nullable<double> _RDC_Total_Notes_In;

        private System.Nullable<double> _RDC_Total_Notes_Out;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<double> _Declared_Total_Coinage;

        private System.Nullable<double> _Declared_Total_TrueCoin_In;

        private System.Nullable<double> _Declared_Total_Notes;

        private System.Nullable<double> _Note_Var;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _Coin_Var;

        private System.Nullable<double> _TicketsPrinted;

        private System.Nullable<double> _TicketBalance;

        private System.Nullable<double> _RDCTicketsIn;

        private System.Nullable<double> _RDCTicketsOut;

        private System.Nullable<double> _RDCTicketBalance;

        private System.Nullable<double> _DecTicketsIn;

        private System.Nullable<double> _DecTicketsOut;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _TicketVar;

        private System.Nullable<double> _CashTake;

        private System.Nullable<double> _RDCTake;

        private System.Nullable<double> _TakeVar;

        private System.Nullable<int> _CollectionTotalDurationPower;

        private System.Nullable<int> _CollectionNoFaultEvents;

        private System.Nullable<int> _CollectionNoPowerEvents;

        private System.Nullable<int> _CollectionNoDoorEvents;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Expired;

        private System.Nullable<System.DateTime> _Collection_Date_Performed;

        private System.Nullable<System.DateTime> _Collection_Date;

        private System.Nullable<double> _Progressive_Value_Meter;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _CashIn;

        private System.Nullable<double> _CashOut;

        private System.Nullable<double> _Mystery_Machine_Paid;

        private System.Nullable<double> _Mystery_Attendant_Paid;

        private System.Nullable<double> _RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;

        private System.Nullable<double> _RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;

        private System.Nullable<double> _Promo_Cashable_EFT_IN;

        private System.Nullable<double> _Promo_Cashable_EFT_OUT;

        private System.Nullable<double> _NonCashable_EFT_IN;

        private System.Nullable<double> _NonCashable_EFT_OUT;

        private System.Nullable<double> _Cashable_EFT_IN;

        private System.Nullable<double> _Cashable_EFT_OUT;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<float> _DeclaredTicketPrintedValue;

        private System.Nullable<int> _CASH_IN_1P;

        private System.Nullable<int> _CASH_IN_2P;

        private System.Nullable<int> _CASH_IN_5P;

        private System.Nullable<int> _CASH_IN_10P;

        private System.Nullable<int> _CASH_IN_20P;

        private System.Nullable<int> _CASH_IN_50P;

        private System.Nullable<int> _CASH_IN_100P;

        private System.Nullable<int> _CASH_IN_200P;

        private System.Nullable<int> _CASH_IN_500P;

        private System.Nullable<int> _CASH_IN_1000P;

        private System.Nullable<int> _CASH_IN_2000P;

        private System.Nullable<int> _CASH_IN_5000P;

        private System.Nullable<int> _CASH_IN_10000P;

        private System.Nullable<int> _CASH_IN_20000P;

        private System.Nullable<int> _CASH_IN_50000P;

        private System.Nullable<int> _CASH_IN_100000P;

        private System.Nullable<int> _CASH_OUT_1P;
        
        private System.Nullable<int> _CASH_OUT_2P;

        private System.Nullable<int> _CASH_OUT_5P;

        private System.Nullable<int> _CASH_OUT_10P;

        private System.Nullable<int> _CASH_OUT_20P;

        private System.Nullable<int> _CASH_OUT_50P;

        private System.Nullable<int> _CASH_OUT_100P;

        private System.Nullable<int> _CASH_OUT_200P;

        private System.Nullable<int> _CASH_OUT_500P;

        private System.Nullable<int> _CASH_OUT_1000P;

        private System.Nullable<int> _CASH_OUT_2000P;

        private System.Nullable<int> _CASH_OUT_5000P;

        private System.Nullable<int> _CASH_OUT_10000P;

        private System.Nullable<int> _CASH_OUT_20000P;

        private System.Nullable<int> _CASH_OUT_50000P;

        private System.Nullable<int> _CASH_OUT_100000P;

        private int _COLLECTION_RDC_TICKETS_INSERTED_VALUE;

        private int _COLLECTION_RDC_TICKETS_PRINTED_VALUE;

        private System.Nullable<int> _COLLECTION_RDC_HANDPAY;

        private System.Nullable<int> _progressive_win_Handpay_value;

        private System.Nullable<int> _BatchNo;

        private System.Nullable<int> _WeekNo;

        private System.Nullable<int> _WeekNumber;

        private System.Nullable<System.DateTime> _WeekStartDate;

        private System.Nullable<System.DateTime> _WeekEndDate;

        private System.Nullable<double> _OffLineShortpay;

        private string _DeclaredUserName;
        
        public CollectionView()
        {
        }

        [Column(Storage = "_Site_No", DbType = "Int")]
        public System.Nullable<int> Site_No
        {
            get
            {
                return this._Site_No;
            }
            set
            {
                if ((this._Site_No != value))
                {
                    this._Site_No = value;
                }
            }
        }

        [Column(Storage = "_Name", DbType = "VarChar(50)")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Name", DbType = "VarChar(50)")]
        public string Collection_Batch_Name
        {
            get
            {
                return this._Collection_Batch_Name;
            }
            set
            {
                if ((this._Collection_Batch_Name != value))
                {
                    this._Collection_Batch_Name = value;
                }
            }
        }


        [Column(Storage = "_Code", DbType = "VarChar(50)")]
        public string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if ((this._Code != value))
                {
                    this._Code = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int NOT NULL")]
        public int Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this._Collection_Batch_No = value;
                }
            }
        }

        [Column(Storage = "_Collection_Batch_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Batch_Date
        {
            get
            {
                return this._Collection_Batch_Date;
            }
            set
            {
                if ((this._Collection_Batch_Date != value))
                {
                    this._Collection_Batch_Date = value;
                }
            }
        }

        [Column(Storage = "_Collection_Performed_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Performed_Date
        {
            get
            {
                return this._Collection_Performed_Date;
            }
            set
            {
                if ((this._Collection_Performed_Date != value))
                {
                    this._Collection_Performed_Date = value;
                }
            }
        }

        [Column(Storage = "_Collection_No", DbType = "Int NOT NULL")]
        public int Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_Price_of_Play", DbType = "Int")]
        public System.Nullable<int> Installation_Price_of_Play
        {
            get
            {
                return this._Installation_Price_of_Play;
            }
            set
            {
                if ((this._Installation_Price_of_Play != value))
                {
                    this._Installation_Price_of_Play = value;
                }
            }
        }

        [Column(Storage = "_DropType", DbType = "VarChar(50)")]
        public string DropType
        {
            get
            {
                return this._DropType;
            }
            set
            {
                if ((this._DropType != value))
                {
                    this._DropType = value;
                }
            }
        }
        
        [Column(Storage = "_Datapak_No", DbType = "Int")]
        public System.Nullable<int> Datapak_No
        {
            get
            {
                return this._Datapak_No;
            }
            set
            {
                if ((this._Datapak_No != value))
                {
                    this._Datapak_No = value;
                }
            }
        }

        [Column(Storage = "_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Start_Date
        {
            get
            {
                return this._Start_Date;
            }
            set
            {
                if ((this._Start_Date != value))
                {
                    this._Start_Date = value;
                }
            }
        }

        [Column(Storage = "_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> End_Date
        {
            get
            {
                return this._End_Date;
            }
            set
            {
                if ((this._End_Date != value))
                {
                    this._End_Date = value;
                }
            }
        }

        [Column(Storage = "_Zone_No", DbType = "Int")]
        public System.Nullable<int> Zone_No
        {
            get
            {
                return this._Zone_No;
            }
            set
            {
                if ((this._Zone_No != value))
                {
                    this._Zone_No = value;
                }
            }
        }

        [Column(Storage = "_Bar_Pos_No", DbType = "Int NOT NULL")]
        public int Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Type_ID", DbType = "Int NOT NULL")]
        public int Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer_ID", DbType = "Int")]
        public System.Nullable<int> Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {
                if ((this._Manufacturer_ID != value))
                {
                    this._Manufacturer_ID = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }

        [Column(Storage = "_Machine_Class_No", DbType = "Int NOT NULL")]
        public int Machine_Class_No
        {
            get
            {
                return this._Machine_Class_No;
            }
            set
            {
                if ((this._Machine_Class_No != value))
                {
                    this._Machine_Class_No = value;
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

        [Column(Storage = "_Calendar_Week_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Calendar_Week_Start_Date
        {
            get
            {
                return this._Calendar_Week_Start_Date;
            }
            set
            {
                if ((this._Calendar_Week_Start_Date != value))
                {
                    this._Calendar_Week_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Calendar_Week_End_Date
        {
            get
            {
                return this._Calendar_Week_End_Date;
            }
            set
            {
                if ((this._Calendar_Week_End_Date != value))
                {
                    this._Calendar_Week_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Week_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Week_Number
        {
            get
            {
                return this._Calendar_Week_Number;
            }
            set
            {
                if ((this._Calendar_Week_Number != value))
                {
                    this._Calendar_Week_Number = value;
                }
            }
        }

        [Column(Storage = "_Period_ID", DbType = "Int")]
        public System.Nullable<int> Period_ID
        {
            get
            {
                return this._Period_ID;
            }
            set
            {
                if ((this._Period_ID != value))
                {
                    this._Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Number", DbType = "Int")]
        public System.Nullable<int> Calendar_Period_Number
        {
            get
            {
                return this._Calendar_Period_Number;
            }
            set
            {
                if ((this._Calendar_Period_Number != value))
                {
                    this._Calendar_Period_Number = value;
                }
            }
        }

        [Column(Storage = "_ZoneName", DbType = "VarChar(50)")]
        public string ZoneName
        {
            get
            {
                return this._ZoneName;
            }
            set
            {
                if ((this._ZoneName != value))
                {
                    this._ZoneName = value;
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

        [Column(Storage = "_UserName", DbType = "VarChar(200)")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        [Column(Storage = "_Stock_No", DbType = "VarChar(50)")]
        public string Stock_No
        {
            get
            {
                return this._Stock_No;
            }
            set
            {
                if ((this._Stock_No != value))
                {
                    this._Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Uses_Meters", DbType = "Bit")]
        public System.Nullable<bool> Machine_Uses_Meters
        {
            get
            {
                return this._Machine_Uses_Meters;
            }
            set
            {
                if ((this._Machine_Uses_Meters != value))
                {
                    this._Machine_Uses_Meters = value;
                }
            }
        }

        [Column(Storage = "_IsTicket", DbType = "Bit")]
        public System.Nullable<bool> IsTicket
        {
            get
            {
                return this._IsTicket;
            }
            set
            {
                if ((this._IsTicket != value))
                {
                    this._IsTicket = value;
                }
            }
        }

        [Column(Storage = "_IsSAS", DbType = "Int NOT NULL")]
        public int IsSAS
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

        [Column(Storage = "_BatchDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> BatchDate
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

        [Column(Storage = "_BatchAdj", DbType = "Float")]
        public System.Nullable<double> BatchAdj
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

        [Column(Storage = "_CollectionCount", DbType = "Int NOT NULL")]
        public int CollectionCount
        {
            get
            {
                return this._CollectionCount;
            }
            set
            {
                if ((this._CollectionCount != value))
                {
                    this._CollectionCount = value;
                }
            }
        }

        [Column(Storage = "_CollectionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CollectionDate
        {
            get
            {
                return this._CollectionDate;
            }
            set
            {
                if ((this._CollectionDate != value))
                {
                    this._CollectionDate = value;
                }
            }
        }

        [Column(Storage = "_PreviousCollectionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PreviousCollectionDate
        {
            get
            {
                return this._PreviousCollectionDate;
            }
            set
            {
                if ((this._PreviousCollectionDate != value))
                {
                    this._PreviousCollectionDate = value;
                }
            }
        }

        [Column(Storage = "_CollectionDays", DbType = "Int")]
        public System.Nullable<int> CollectionDays
        {
            get
            {
                return this._CollectionDays;
            }
            set
            {
                if ((this._CollectionDays != value))
                {
                    this._CollectionDays = value;
                }
            }
        }

        [Column(Storage = "_CollectionDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> CollectionDateTime
        {
            get
            {
                return this._CollectionDateTime;
            }
            set
            {
                if ((this._CollectionDateTime != value))
                {
                    this._CollectionDateTime = value;
                }
            }
        }

        [Column(Storage = "_Declaration", DbType = "Bit")]
        public System.Nullable<bool> Declaration
        {
            get
            {
                return this._Declaration;
            }
            set
            {
                if ((this._Declaration != value))
                {
                    this._Declaration = value;
                }
            }
        }

        [Column(Storage = "_Collection_EDC_Status", DbType = "Int")]
        public System.Nullable<int> Collection_EDC_Status
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

        [Column(Storage = "_CashCollected", DbType = "Real")]
        public System.Nullable<float> CashCollected
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

        [Column(Storage = "_Cash_Collected_1p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1p
        {
            get
            {
                return this._Cash_Collected_1p;
            }
            set
            {
                if ((this._Cash_Collected_1p != value))
                {
                    this._Cash_Collected_1p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2p
        {
            get
            {
                return this._Cash_Collected_2p;
            }
            set
            {
                if ((this._Cash_Collected_2p != value))
                {
                    this._Cash_Collected_2p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5p
        {
            get
            {
                return this._Cash_Collected_5p;
            }
            set
            {
                if ((this._Cash_Collected_5p != value))
                {
                    this._Cash_Collected_5p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10p
        {
            get
            {
                return this._Cash_Collected_10p;
            }
            set
            {
                if ((this._Cash_Collected_10p != value))
                {
                    this._Cash_Collected_10p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20p
        {
            get
            {
                return this._Cash_Collected_20p;
            }
            set
            {
                if ((this._Cash_Collected_20p != value))
                {
                    this._Cash_Collected_20p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50p
        {
            get
            {
                return this._Cash_Collected_50p;
            }
            set
            {
                if ((this._Cash_Collected_50p != value))
                {
                    this._Cash_Collected_50p = value;
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100p
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

        [Column(Storage = "_Cash_Collected_200p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_200p
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

        [Column(Storage = "_Cash_Collected_500p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_500p
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

        [Column(Storage = "_Cash_Collected_1000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1000p
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

        [Column(Storage = "_Cash_Collected_2000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2000p
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

        [Column(Storage = "_Cash_Collected_5000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5000p
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

        [Column(Storage = "_Cash_Collected_10000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10000p
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

        [Column(Storage = "_Cash_Collected_20000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20000p
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

        [Column(Storage = "_Cash_Collected_50000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50000p
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

        [Column(Storage = "_Cash_Collected_100000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100000p
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

        [Column(Storage = "_COLLECTION_RDC_JACKPOT", DbType = "Float")]
        public System.Nullable<double> COLLECTION_RDC_JACKPOT
        {
            get
            {
                return this._COLLECTION_RDC_JACKPOT;
            }
            set
            {
                if ((this._COLLECTION_RDC_JACKPOT != value))
                {
                    this._COLLECTION_RDC_JACKPOT = value;
                }
            }
        }

        [Column(Storage = "_DeclaredTicketValue", DbType = "Real")]
        public System.Nullable<float> DeclaredTicketValue
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

        [Column(Storage = "_DeclaredTicketQty", DbType = "Int")]
        public System.Nullable<int> DeclaredTicketQty
        {
            get
            {
                return this._DeclaredTicketQty;
            }
            set
            {
                if ((this._DeclaredTicketQty != value))
                {
                    this._DeclaredTicketQty = value;
                }
            }
        }

        [Column(Storage = "_PromoCashableValue", DbType = "decimal(18,2) NOT NULL")]
        public System.Nullable<decimal> PromoCashableValue
        {
            get
            {
                return this._PromoCashableValue;
            }
            set
            {
                if ((this._PromoCashableValue != value))
                {
                    this._PromoCashableValue = value;
                }
            }
        }

        [Column(Storage = "_PromoNonCashableValue", DbType = "decimal(18,2) NOT NULL")]
        public System.Nullable<decimal> PromoNonCashableValue
        {
            get
            {
                return this._PromoNonCashableValue;
            }
            set
            {
                if ((this._PromoNonCashableValue != value))
                {
                    this._PromoNonCashableValue = value;
                }
            }
        }

        [Column(Storage = "_Defloat", DbType = "Real")]
        public System.Nullable<float> Defloat
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

        [Column(Storage = "_GrossCash", DbType = "Real")]
        public System.Nullable<float> GrossCash
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

        [Column(Storage = "_ManualRefills", DbType = "Real")]
        public System.Nullable<float> ManualRefills
        {
            get
            {
                return this._ManualRefills;
            }
            set
            {
                if ((this._ManualRefills != value))
                {
                    this._ManualRefills = value;
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

        [Column(Storage = "_RDCIn", DbType = "Float")]
        public System.Nullable<double> RDCIn
        {
            get
            {
                return this._RDCIn;
            }
            set
            {
                if ((this._RDCIn != value))
                {
                    this._RDCIn = value;
                }
            }
        }

        [Column(Storage = "_RDCOut", DbType = "Float")]
        public System.Nullable<double> RDCOut
        {
            get
            {
                return this._RDCOut;
            }
            set
            {
                if ((this._RDCOut != value))
                {
                    this._RDCOut = value;
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

        [Column(Storage = "_MeterCash", DbType = "Int")]
        public System.Nullable<int> MeterCash
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

        [Column(Storage = "_MeterRefill", DbType = "Int")]
        public System.Nullable<int> MeterRefill
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

        [Column(Storage = "_HandpayVar", DbType = "Float")]
        public System.Nullable<double> HandpayVar
        {
            get
            {
                return this._HandpayVar;
            }
            set
            {
                if ((this._HandpayVar != value))
                {
                    this._HandpayVar = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpay", DbType = "Int")]
        public System.Nullable<int> MeterHandpay
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

        [Column(Storage = "_RDC_Total_Coinage_In", DbType = "Float")]
        public System.Nullable<double> RDC_Total_Coinage_In
        {
            get
            {
                return this._RDC_Total_Coinage_In;
            }
            set
            {
                if ((this._RDC_Total_Coinage_In != value))
                {
                    this._RDC_Total_Coinage_In = value;
                }
            }
        }

        [Column(Storage = "_RDC_Total_Coinage_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Total_Coinage_Out
        {
            get
            {
                return this._RDC_Total_Coinage_Out;
            }
            set
            {
                if ((this._RDC_Total_Coinage_Out != value))
                {
                    this._RDC_Total_Coinage_Out = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coin", DbType = "Float")]
        public System.Nullable<double> RDC_Coin
        {
            get
            {
                return this._RDC_Coin;
            }
            set
            {
                if ((this._RDC_Coin != value))
                {
                    this._RDC_Coin = value;
                }
            }
        }

        [Column(Storage = "_RDC_Total_Notes_In", DbType = "Float")]
        public System.Nullable<double> RDC_Total_Notes_In
        {
            get
            {
                return this._RDC_Total_Notes_In;
            }
            set
            {
                if ((this._RDC_Total_Notes_In != value))
                {
                    this._RDC_Total_Notes_In = value;
                }
            }
        }

        [Column(Storage = "_RDC_Total_Notes_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Total_Notes_Out
        {
            get
            {
                return this._RDC_Total_Notes_Out;
            }
            set
            {
                if ((this._RDC_Total_Notes_Out != value))
                {
                    this._RDC_Total_Notes_Out = value;
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

        [Column(Storage = "_Declared_Total_Coinage", DbType = "Float")]
        public System.Nullable<double> Declared_Total_Coinage
        {
            get
            {
                return this._Declared_Total_Coinage;
            }
            set
            {
                if ((this._Declared_Total_Coinage != value))
                {
                    this._Declared_Total_Coinage = value;
                }
            }
        }

        [Column(Storage = "_Declared_Total_TrueCoin_In", DbType = "Float")]
        public System.Nullable<double> Declared_Total_TrueCoin_In
        {
            get
            {
                return this._Declared_Total_TrueCoin_In;
            }
            set
            {
                if ((this._Declared_Total_TrueCoin_In != value))
                {
                    this._Declared_Total_TrueCoin_In = value;
                }
            }
        }

        [Column(Storage = "_Declared_Total_Notes", DbType = "Float")]
        public System.Nullable<double> Declared_Total_Notes
        {
            get
            {
                return this._Declared_Total_Notes;
            }
            set
            {
                if ((this._Declared_Total_Notes != value))
                {
                    this._Declared_Total_Notes = value;
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

        [Column(Storage = "_TicketsPrinted", DbType = "Float")]
        public System.Nullable<double> TicketsPrinted
        {
            get
            {
                return this._TicketsPrinted;
            }
            set
            {
                if ((this._TicketsPrinted != value))
                {
                    this._TicketsPrinted = value;
                }
            }
        }

        [Column(Storage = "_TicketBalance", DbType = "Float")]
        public System.Nullable<double> TicketBalance
        {
            get
            {
                return this._TicketBalance;
            }
            set
            {
                if ((this._TicketBalance != value))
                {
                    this._TicketBalance = value;
                }
            }
        }

        [Column(Storage = "_RDCTicketsIn", DbType = "Float")]
        public System.Nullable<double> RDCTicketsIn
        {
            get
            {
                return this._RDCTicketsIn;
            }
            set
            {
                if ((this._RDCTicketsIn != value))
                {
                    this._RDCTicketsIn = value;
                }
            }
        }

        [Column(Storage = "_RDCTicketsOut", DbType = "Float")]
        public System.Nullable<double> RDCTicketsOut
        {
            get
            {
                return this._RDCTicketsOut;
            }
            set
            {
                if ((this._RDCTicketsOut != value))
                {
                    this._RDCTicketsOut = value;
                }
            }
        }

        [Column(Storage = "_RDCTicketBalance", DbType = "Float")]
        public System.Nullable<double> RDCTicketBalance
        {
            get
            {
                return this._RDCTicketBalance;
            }
            set
            {
                if ((this._RDCTicketBalance != value))
                {
                    this._RDCTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_DecTicketsIn", DbType = "Float")]
        public System.Nullable<double> DecTicketsIn
        {
            get
            {
                return this._DecTicketsIn;
            }
            set
            {
                if ((this._DecTicketsIn != value))
                {
                    this._DecTicketsIn = value;
                }
            }
        }

        [Column(Storage = "_DecTicketsOut", DbType = "Float")]
        public System.Nullable<double> DecTicketsOut
        {
            get
            {
                return this._DecTicketsOut;
            }
            set
            {
                if ((this._DecTicketsOut != value))
                {
                    this._DecTicketsOut = value;
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

        [Column(Storage = "_TicketVar", DbType = "Float")]
        public System.Nullable<double> TicketVar
        {
            get
            {
                return this._TicketVar;
            }
            set
            {
                if ((this._TicketVar != value))
                {
                    this._TicketVar = value;
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

        [Column(Storage = "_RDCTake", DbType = "Float")]
        public System.Nullable<double> RDCTake
        {
            get
            {
                return this._RDCTake;
            }
            set
            {
                if ((this._RDCTake != value))
                {
                    this._RDCTake = value;
                }
            }
        }

        [Column(Storage = "_TakeVar", DbType = "Float")]
        public System.Nullable<double> TakeVar
        {
            get
            {
                return this._TakeVar;
            }
            set
            {
                if ((this._TakeVar != value))
                {
                    this._TakeVar = value;
                }
            }
        }

        [Column(Storage = "_CollectionTotalDurationPower", DbType = "Int")]
        public System.Nullable<int> CollectionTotalDurationPower
        {
            get
            {
                return this._CollectionTotalDurationPower;
            }
            set
            {
                if ((this._CollectionTotalDurationPower != value))
                {
                    this._CollectionTotalDurationPower = value;
                }
            }
        }

        [Column(Storage = "_CollectionNoFaultEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoFaultEvents
        {
            get
            {
                return this._CollectionNoFaultEvents;
            }
            set
            {
                if ((this._CollectionNoFaultEvents != value))
                {
                    this._CollectionNoFaultEvents = value;
                }
            }
        }

        [Column(Storage = "_CollectionNoPowerEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoPowerEvents
        {
            get
            {
                return this._CollectionNoPowerEvents;
            }
            set
            {
                if ((this._CollectionNoPowerEvents != value))
                {
                    this._CollectionNoPowerEvents = value;
                }
            }
        }

        [Column(Storage = "_CollectionNoDoorEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoDoorEvents
        {
            get
            {
                return this._CollectionNoDoorEvents;
            }
            set
            {
                if ((this._CollectionNoDoorEvents != value))
                {
                    this._CollectionNoDoorEvents = value;
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

        [Column(Storage = "_Collection_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Date_Performed
        {
            get
            {
                return this._Collection_Date_Performed;
            }
            set
            {
                if ((this._Collection_Date_Performed != value))
                {
                    this._Collection_Date_Performed = value;
                }
            }
        }

        [Column(Storage = "_Collection_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Date
        {
            get
            {
                return this._Collection_Date;
            }
            set
            {
                if ((this._Collection_Date != value))
                {
                    this._Collection_Date = value;
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

        [Column(Storage = "_CashIn", DbType = "Float")]
        public System.Nullable<double> CashIn
        {
            get
            {
                return this._CashIn;
            }
            set
            {
                if ((this._CashIn != value))
                {
                    this._CashIn = value;
                }
            }
        }

        [Column(Storage = "_CashOut", DbType = "Float")]
        public System.Nullable<double> CashOut
        {
            get
            {
                return this._CashOut;
            }
            set
            {
                if ((this._CashOut != value))
                {
                    this._CashOut = value;
                }
            }
        }

        [Column(Storage = "_Mystery_Machine_Paid", DbType = "Float")]
        public System.Nullable<double> Mystery_Machine_Paid
        {
            get
            {
                return this._Mystery_Machine_Paid;
            }
            set
            {
                if ((this._Mystery_Machine_Paid != value))
                {
                    this._Mystery_Machine_Paid = value;
                }
            }
        }

        [Column(Storage = "_Mystery_Attendant_Paid", DbType = "Float")]
        public System.Nullable<double> Mystery_Attendant_Paid
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

        [Column(Storage = "_Promo_Cashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> Promo_Cashable_EFT_IN
        {
            get
            {
                return this._Promo_Cashable_EFT_IN;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_IN != value))
                {
                    this._Promo_Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_Promo_Cashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> Promo_Cashable_EFT_OUT
        {
            get
            {
                return this._Promo_Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_OUT != value))
                {
                    this._Promo_Cashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> NonCashable_EFT_IN
        {
            get
            {
                return this._NonCashable_EFT_IN;
            }
            set
            {
                if ((this._NonCashable_EFT_IN != value))
                {
                    this._NonCashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> NonCashable_EFT_OUT
        {
            get
            {
                return this._NonCashable_EFT_OUT;
            }
            set
            {
                if ((this._NonCashable_EFT_OUT != value))
                {
                    this._NonCashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_Cashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> Cashable_EFT_IN
        {
            get
            {
                return this._Cashable_EFT_IN;
            }
            set
            {
                if ((this._Cashable_EFT_IN != value))
                {
                    this._Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_Cashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> Cashable_EFT_OUT
        {
            get
            {
                return this._Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Cashable_EFT_OUT != value))
                {
                    this._Cashable_EFT_OUT = value;
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

        [Column(Storage = "_DeclaredTicketPrintedValue", DbType = "Real")]
        public System.Nullable<float> DeclaredTicketPrintedValue
        {
            get
            {
                return this._DeclaredTicketPrintedValue;
            }
            set
            {
                if ((this._DeclaredTicketPrintedValue != value))
                {
                    this._DeclaredTicketPrintedValue = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_1P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1P
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

        [Column(Storage = "_CASH_IN_2P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2P
        {
            get
            {
                return this._CASH_IN_2P;
            }
            set
            {
                if ((this._CASH_IN_2P != value))
                {
                    this._CASH_IN_2P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_5P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_5P
        {
            get
            {
                return this._CASH_IN_5P;
            }
            set
            {
                if ((this._CASH_IN_5P != value))
                {
                    this._CASH_IN_5P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_10P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_10P
        {
            get
            {
                return this._CASH_IN_10P;
            }
            set
            {
                if ((this._CASH_IN_10P != value))
                {
                    this._CASH_IN_10P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_20P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_20P
        {
            get
            {
                return this._CASH_IN_20P;
            }
            set
            {
                if ((this._CASH_IN_20P != value))
                {
                    this._CASH_IN_20P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_50P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_50P
        {
            get
            {
                return this._CASH_IN_50P;
            }
            set
            {
                if ((this._CASH_IN_50P != value))
                {
                    this._CASH_IN_50P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_100P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100P
        {
            get
            {
                return this._CASH_IN_100P;
            }
            set
            {
                if ((this._CASH_IN_100P != value))
                {
                    this._CASH_IN_100P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_200P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_200P
        {
            get
            {
                return this._CASH_IN_200P;
            }
            set
            {
                if ((this._CASH_IN_200P != value))
                {
                    this._CASH_IN_200P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_500P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_500P
        {
            get
            {
                return this._CASH_IN_500P;
            }
            set
            {
                if ((this._CASH_IN_500P != value))
                {
                    this._CASH_IN_500P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1000P
        {
            get
            {
                return this._CASH_IN_1000P;
            }
            set
            {
                if ((this._CASH_IN_1000P != value))
                {
                    this._CASH_IN_1000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2000P
        {
            get
            {
                return this._CASH_IN_2000P;
            }
            set
            {
                if ((this._CASH_IN_2000P != value))
                {
                    this._CASH_IN_2000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_5000P
        {
            get
            {
                return this._CASH_IN_5000P;
            }
            set
            {
                if ((this._CASH_IN_5000P != value))
                {
                    this._CASH_IN_5000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_10000P
        {
            get
            {
                return this._CASH_IN_10000P;
            }
            set
            {
                if ((this._CASH_IN_10000P != value))
                {
                    this._CASH_IN_10000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_20000P
        {
            get
            {
                return this._CASH_IN_20000P;
            }
            set
            {
                if ((this._CASH_IN_20000P != value))
                {
                    this._CASH_IN_20000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_50000P
        {
            get
            {
                return this._CASH_IN_50000P;
            }
            set
            {
                if ((this._CASH_IN_50000P != value))
                {
                    this._CASH_IN_50000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_IN_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100000P
        {
            get
            {
                return this._CASH_IN_100000P;
            }
            set
            {
                if ((this._CASH_IN_100000P != value))
                {
                    this._CASH_IN_100000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_1P
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

        [Column(Storage = "_CASH_OUT_2P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_2P
        {
            get
            {
                return this._CASH_OUT_2P;
            }
            set
            {
                if ((this._CASH_OUT_2P != value))
                {
                    this._CASH_OUT_2P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_5P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_5P
        {
            get
            {
                return this._CASH_OUT_5P;
            }
            set
            {
                if ((this._CASH_OUT_5P != value))
                {
                    this._CASH_OUT_5P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_10P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_10P
        {
            get
            {
                return this._CASH_OUT_10P;
            }
            set
            {
                if ((this._CASH_OUT_10P != value))
                {
                    this._CASH_OUT_10P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_20P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_20P
        {
            get
            {
                return this._CASH_OUT_20P;
            }
            set
            {
                if ((this._CASH_OUT_20P != value))
                {
                    this._CASH_OUT_20P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_50P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_50P
        {
            get
            {
                return this._CASH_OUT_50P;
            }
            set
            {
                if ((this._CASH_OUT_50P != value))
                {
                    this._CASH_OUT_50P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_100P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_100P
        {
            get
            {
                return this._CASH_OUT_100P;
            }
            set
            {
                if ((this._CASH_OUT_100P != value))
                {
                    this._CASH_OUT_100P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_200P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_200P
        {
            get
            {
                return this._CASH_OUT_200P;
            }
            set
            {
                if ((this._CASH_OUT_200P != value))
                {
                    this._CASH_OUT_200P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_500P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_500P
        {
            get
            {
                return this._CASH_OUT_500P;
            }
            set
            {
                if ((this._CASH_OUT_500P != value))
                {
                    this._CASH_OUT_500P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_1000P
        {
            get
            {
                return this._CASH_OUT_1000P;
            }
            set
            {
                if ((this._CASH_OUT_1000P != value))
                {
                    this._CASH_OUT_1000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_2000P
        {
            get
            {
                return this._CASH_OUT_2000P;
            }
            set
            {
                if ((this._CASH_OUT_2000P != value))
                {
                    this._CASH_OUT_2000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_5000P
        {
            get
            {
                return this._CASH_OUT_5000P;
            }
            set
            {
                if ((this._CASH_OUT_5000P != value))
                {
                    this._CASH_OUT_5000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_10000P
        {
            get
            {
                return this._CASH_OUT_10000P;
            }
            set
            {
                if ((this._CASH_OUT_10000P != value))
                {
                    this._CASH_OUT_10000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_20000P
        {
            get
            {
                return this._CASH_OUT_20000P;
            }
            set
            {
                if ((this._CASH_OUT_20000P != value))
                {
                    this._CASH_OUT_20000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_50000P
        {
            get
            {
                return this._CASH_OUT_50000P;
            }
            set
            {
                if ((this._CASH_OUT_50000P != value))
                {
                    this._CASH_OUT_50000P = value;
                }
            }
        }

        [Column(Storage = "_CASH_OUT_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_100000P
        {
            get
            {
                return this._CASH_OUT_100000P;
            }
            set
            {
                if ((this._CASH_OUT_100000P != value))
                {
                    this._CASH_OUT_100000P = value;
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TICKETS_INSERTED_VALUE", DbType = "Int NOT NULL")]
        public int COLLECTION_RDC_TICKETS_INSERTED_VALUE
        {
            get
            {
                return this._COLLECTION_RDC_TICKETS_INSERTED_VALUE;
            }
            set
            {
                if ((this._COLLECTION_RDC_TICKETS_INSERTED_VALUE != value))
                {
                    this._COLLECTION_RDC_TICKETS_INSERTED_VALUE = value;
                }
            }
        }
        [Column(Storage = "_COLLECTION_RDC_TICKETS_PRINTED_VALUE", DbType = "Int NOT NULL")]
        public int COLLECTION_RDC_TICKETS_PRINTED_VALUE
        {
            get
            {
                return this._COLLECTION_RDC_TICKETS_PRINTED_VALUE;
            }
            set
            {
                if ((this._COLLECTION_RDC_TICKETS_PRINTED_VALUE != value))
                {
                    this._COLLECTION_RDC_TICKETS_PRINTED_VALUE = value;
                }
            }
        }
        [Column(Storage = "_COLLECTION_RDC_HANDPAY", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_HANDPAY
        {
            get
            {
                return this._COLLECTION_RDC_HANDPAY;
            }
            set
            {
                if ((this._COLLECTION_RDC_HANDPAY != value))
                {
                    this._COLLECTION_RDC_HANDPAY = value;
                }
            }
        }
        [Column(Storage = "_progressive_win_Handpay_value", DbType = "Int")]
        public System.Nullable<int> progressive_win_Handpay_value
        {
            get
            {
                return this._progressive_win_Handpay_value;
            }
            set
            {
                if ((this._progressive_win_Handpay_value != value))
                {
                    this._progressive_win_Handpay_value = value;
                }
            }
        }

        [Column(Storage = "_BatchNo", DbType = "Int")]
        public System.Nullable<int> BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
                }
            }
        }

        [Column(Storage = "_WeekNo", DbType = "Int")]
        public System.Nullable<int> WeekNo
        {
            get
            {
                return this._WeekNo;
            }
            set
            {
                if ((this._WeekNo != value))
                {
                    this._WeekNo = value;
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

        [Column(Storage = "_WeekStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> WeekStartDate
        {
            get
            {
                return this._WeekStartDate;
            }
            set
            {
                if ((this._WeekStartDate != value))
                {
                    this._WeekStartDate = value;
                }
            }
        }

        [Column(Storage = "_WeekEndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> WeekEndDate
        {
            get
            {
                return this._WeekEndDate;
            }
            set
            {
                if ((this._WeekEndDate != value))
                {
                    this._WeekEndDate = value;
                }
            }
        }

        [Column(Storage = "_OffLineShortpay", DbType = "Float")]
        public System.Nullable<double> OffLineShortpay
        {
            get
            {
                return this._OffLineShortpay;
            }
            set
            {
                if ((this._OffLineShortpay != value))
                {
                    this._OffLineShortpay = value;
                }
            }
        }

        [Column(Storage = "_DeclaredUserName", DbType = "VarChar(50)")]
        public string DeclaredUserName
        {
            get
            {
                return this._DeclaredUserName;
            }
            set
            {
                if ((this._DeclaredUserName != value))
                {
                    this._DeclaredUserName = value;
                }
            }
        }

    }

    [Table(Name = "dbo.Collection")]
    public partial class CollectionRecord : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Collection_No;

        private System.Nullable<int> _Collection_Batch_No;

        private string _Machine_Serial;

        private string _Collection_RDC_Machine_Code;

        private string _Collection_RDC_Secondary_Machine_Code;

        private System.Nullable<int> _Datapak_Read_Occurrence;

        private System.Nullable<int> _Float_Level;

        private System.Nullable<int> _Period_ID;

        private System.Nullable<int> _Week_ID;

        private System.Nullable<int> _CASH_IN_1P;

        private System.Nullable<int> _CASH_IN_2P;

        private System.Nullable<int> _CASH_IN_5P;

        private System.Nullable<int> _CASH_IN_10P;

        private System.Nullable<int> _CASH_IN_20P;

        private System.Nullable<int> _CASH_IN_50P;

        private System.Nullable<int> _CASH_IN_100P;

        private System.Nullable<int> _CASH_IN_200P;

        private System.Nullable<int> _CASH_IN_500P;

        private System.Nullable<int> _CASH_IN_1000P;

        private System.Nullable<int> _CASH_IN_2000P;

        private System.Nullable<int> _CASH_IN_5000P;

        private System.Nullable<int> _CASH_IN_10000P;

        private System.Nullable<int> _CASH_IN_20000P;

        private System.Nullable<int> _CASH_IN_50000P;

        private System.Nullable<int> _CASH_IN_100000P;

        private System.Nullable<int> _TOKEN_IN_5P;

        private System.Nullable<int> _TOKEN_IN_10P;

        private System.Nullable<int> _TOKEN_IN_20P;

        private System.Nullable<int> _TOKEN_IN_50P;

        private System.Nullable<int> _TOKEN_IN_100P;

        private System.Nullable<int> _TOKEN_IN_200P;

        private System.Nullable<int> _TOKEN_IN_500P;

        private System.Nullable<int> _TOKEN_IN_1000P;

        private System.Nullable<int> _CASH_OUT_1P;
        
        private System.Nullable<int> _CASH_OUT_2P;

        private System.Nullable<int> _CASH_OUT_5P;

        private System.Nullable<int> _CASH_OUT_10P;

        private System.Nullable<int> _CASH_OUT_20P;

        private System.Nullable<int> _CASH_OUT_50P;

        private System.Nullable<int> _CASH_OUT_100P;

        private System.Nullable<int> _CASH_OUT_200P;

        private System.Nullable<int> _CASH_OUT_500P;

        private System.Nullable<int> _CASH_OUT_1000P;

        private System.Nullable<int> _CASH_OUT_2000P;

        private System.Nullable<int> _CASH_OUT_5000P;

        private System.Nullable<int> _CASH_OUT_10000P;

        private System.Nullable<int> _CASH_OUT_20000P;

        private System.Nullable<int> _CASH_OUT_50000P;

        private System.Nullable<int> _CASH_OUT_100000P;

        private System.Nullable<int> _TOKEN_OUT_5P;

        private System.Nullable<int> _TOKEN_OUT_10P;

        private System.Nullable<int> _TOKEN_OUT_20P;

        private System.Nullable<int> _TOKEN_OUT_50P;

        private System.Nullable<int> _TOKEN_OUT_100P;

        private System.Nullable<int> _TOKEN_OUT_200P;

        private System.Nullable<int> _TOKEN_OUT_500P;

        private System.Nullable<int> _TOKEN_OUT_1000P;

        private System.Nullable<int> _CASH_REFILL_5P;

        private System.Nullable<int> _CASH_REFILL_10P;

        private System.Nullable<int> _CASH_REFILL_20P;

        private System.Nullable<int> _CASH_REFILL_50P;

        private System.Nullable<int> _CASH_REFILL_100P;

        private System.Nullable<int> _CASH_REFILL_200P;

        private System.Nullable<int> _CASH_REFILL_500P;

        private System.Nullable<int> _CASH_REFILL_1000P;

        private System.Nullable<int> _CASH_REFILL_2000P;

        private System.Nullable<int> _CASH_REFILL_5000P;

        private System.Nullable<int> _CASH_REFILL_10000P;

        private System.Nullable<int> _CASH_REFILL_20000P;

        private System.Nullable<int> _CASH_REFILL_50000P;

        private System.Nullable<int> _CASH_REFILL_100000P;

        private System.Nullable<int> _TOKEN_REFILL_5P;

        private System.Nullable<int> _TOKEN_REFILL_10P;

        private System.Nullable<int> _TOKEN_REFILL_20P;

        private System.Nullable<int> _TOKEN_REFILL_50P;

        private System.Nullable<int> _TOKEN_REFILL_100P;

        private System.Nullable<int> _TOKEN_REFILL_200P;

        private System.Nullable<int> _TOKEN_REFILL_500P;

        private System.Nullable<int> _TOKEN_REFILL_1000P;

        private System.Nullable<bool> _Declaration;

        private System.Nullable<float> _Treasury_Total;

        private System.Nullable<int> _CounterCashIn;

        private System.Nullable<int> _CounterCashOut;

        private System.Nullable<int> _CounterTokensIn;

        private System.Nullable<int> _CounterTokensOut;

        private System.Nullable<int> _CounterPrize;

        private System.Nullable<int> _CounterTournament;

        private System.Nullable<int> _CounterJukebox;

        private System.Nullable<int> _CounterRefills;

        private System.Nullable<float> _CashCollected;

        private System.Nullable<float> _TokensCollected;

        private System.Nullable<float> _Cash_Collected_1p;

        private System.Nullable<float> _Cash_Collected_2p;

        private System.Nullable<float> _Cash_Collected_5p;

        private System.Nullable<float> _Cash_Collected_10p;

        private System.Nullable<float> _Cash_Collected_20p;

        private System.Nullable<float> _Cash_Collected_50p;

        private System.Nullable<float> _Cash_Collected_100p;

        private System.Nullable<float> _Cash_Collected_200p;

        private System.Nullable<float> _Cash_Collected_500p;

        private System.Nullable<float> _Cash_Collected_1000p;

        private System.Nullable<float> _Cash_Collected_2000p;

        private System.Nullable<float> _Cash_Collected_5000p;

        private System.Nullable<float> _Cash_Collected_10000p;

        private System.Nullable<float> _Cash_Collected_20000p;

        private System.Nullable<float> _Cash_Collected_50000p;

        private System.Nullable<float> _Cash_Collected_100000p;

        private System.Nullable<float> _CashRefills;

        private System.Nullable<float> _TokenRefills;

        private System.Nullable<float> _Cash_Refills_2p;

        private System.Nullable<float> _Cash_Refills_5p;

        private System.Nullable<float> _Cash_Refills_10p;

        private System.Nullable<float> _Cash_Refills_20p;

        private System.Nullable<float> _Cash_Refills_50p;

        private System.Nullable<float> _Cash_Refills_100p;

        private System.Nullable<float> _Cash_Refills_200p;

        private System.Nullable<float> _Cash_Refills_500p;

        private System.Nullable<float> _Cash_Refills_1000p;

        private System.Nullable<float> _Cash_Refills_2000p;

        private System.Nullable<float> _Cash_Refills_5000p;

        private System.Nullable<float> _Cash_Refills_10000p;

        private System.Nullable<float> _Cash_Refills_20000p;

        private System.Nullable<float> _Cash_Refills_50000p;

        private System.Nullable<float> _Cash_Refills_100000p;

        private System.Nullable<int> _CounterCashInElectronic;

        private System.Nullable<int> _CounterCashOutElectronic;

        private System.Nullable<int> _CounterTokensInElectronic;

        private System.Nullable<int> _CounterTokensOutElectronic;

        private System.Nullable<int> _JackpotsOut;

        private System.Nullable<int> _PreviousCounterCashIn;

        private System.Nullable<int> _PreviousCounterCashOut;

        private System.Nullable<int> _PreviousCounterTokensIn;

        private System.Nullable<int> _PreviousCounterTokensOut;

        private System.Nullable<int> _PreviousCounterPrize;

        private System.Nullable<int> _PreviousCounterJackpotsOut;

        private System.Nullable<int> _PreviousCounterTournament;

        private System.Nullable<int> _PreviousCounterJukebox;

        private System.Nullable<int> _PreviousCounterRefills;

        private System.Nullable<int> _PreviousCounterCashInElectronic;

        private System.Nullable<int> _PreviousCounterCashOutElectronic;

        private System.Nullable<int> _PreviousCounterTokensInElectronic;

        private System.Nullable<int> _PreviousCounterTokensOutElectronic;

        private System.Nullable<System.DateTime> _PreviousCollectionDate;

        private System.Nullable<int> _PreviousCollectionNo;

        private System.Nullable<float> _Treasury_Refills;

        private System.Nullable<float> _Treasury_Repayments;

        private System.Nullable<float> _Treasury_Tokens;

        private System.Nullable<float> _ExpectedBaggedCash;

        private System.Nullable<float> _ActualBaggedCash;

        private System.Nullable<int> _Collection_Meters_Coins_In;

        private System.Nullable<int> _Collection_Meters_Coins_Out;

        private System.Nullable<int> _Collection_Meters_Coin_Drop;

        private System.Nullable<int> _Collection_Meters_Handpay;

        private System.Nullable<int> _Collection_Meters_External_Credit;

        private System.Nullable<int> _Collection_Meters_Games_Bet;

        private System.Nullable<int> _Collection_Meters_Games_Won;

        private System.Nullable<int> _Collection_Meters_Notes;

        private System.Nullable<float> _Collection_Treasury_Defloat;

        private System.Nullable<bool> _Collection_Defloat_Collection;

        private System.Nullable<int> _Previous_Meters_Coins_In;

        private System.Nullable<int> _Previous_Meters_Coins_Out;

        private System.Nullable<int> _Previous_Meters_Coin_Drop;

        private System.Nullable<int> _Previous_Meters_Handpay;

        private System.Nullable<int> _Previous_Meters_External_Credit;

        private System.Nullable<int> _Previous_Meters_Games_Bet;

        private System.Nullable<int> _Previous_Meters_Games_Won;

        private System.Nullable<int> _Previous_Meters_Notes;

        private System.Nullable<float> _Treasury_Handpay;

        private System.Nullable<int> _Operator_Week_ID;

        private System.Nullable<int> _Operator_Period_ID;

        private System.Nullable<int> _CollectionHandHeldMetersReceived;

        private System.Nullable<int> _CollectionNoDoorEvents;

        private System.Nullable<int> _CollectionNoPowerEvents;

        private System.Nullable<int> _CollectionNoFaultEvents;

        private System.Nullable<int> _CollectionTotalDurationPower;

        private System.Nullable<int> _CollectionTotalDurationDoor;

        private System.Nullable<int> _COLLECTION_RDC_VTP;

        private System.Nullable<float> _Collection_NetEx;

        private System.Nullable<float> _Collection_VAT_Rate;

        private System.Nullable<int> _COLLECTION_RDC_COINS_IN;

        private System.Nullable<int> _COLLECTION_RDC_COINS_OUT;

        private System.Nullable<int> _COLLECTION_RDC_COIN_DROP;

        private System.Nullable<int> _COLLECTION_RDC_HANDPAY;

        private System.Nullable<int> _COLLECTION_RDC_EXTERNAL_CREDIT;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_BET;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_WON;

        private System.Nullable<int> _COLLECTION_RDC_NOTES;

        private System.Nullable<int> _COLLECTION_RDC_CANCELLED_CREDITS;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_LOST;

        private System.Nullable<int> _COLLECTION_RDC_GAMES_SINCE_POWER_UP;

        private System.Nullable<int> _COLLECTION_RDC_TRUE_COIN_IN;

        private System.Nullable<int> _COLLECTION_RDC_TRUE_COIN_OUT;

        private System.Nullable<int> _COLLECTION_RDC_CURRENT_CREDITS;

        private System.Nullable<int> _Collection_PoP_Actual;

        private System.Nullable<int> _Collection_PoP_Configured;

        private System.Nullable<int> _Collection_EDC_Status;

        private System.Nullable<int> _Collection_Meter_Status;

        private System.Nullable<int> _Collection_Cash_Status;

        private int _Installation_No;

        private System.Nullable<System.DateTime> _Collection_Date;

        private int _CASH_FLOAT_CHANGE_1p;

        private int _CASH_FLOAT_CHANGE_2p;

        private int _CASH_FLOAT_CHANGE_5p;

        private int _CASH_FLOAT_CHANGE_10p;

        private int _CASH_FLOAT_CHANGE_20p;

        private int _CASH_FLOAT_CHANGE_50p;

        private int _CASH_FLOAT_CHANGE_100p;

        private int _CASH_FLOAT_CHANGE_200p;

        private int _CASH_FLOAT_CHANGE_500p;

        private int _CASH_FLOAT_CHANGE_1000p;

        private float _CASH_FLOAT_TOTAL;

        private System.Nullable<int> _DeclaredTicketQty;

        private System.Nullable<float> _DeclaredTicketValue;

        private System.Nullable<int> _COLLECTION_RDC_JACKPOT;

        private int _COLLECTION_RDC_TICKETS_INSERTED_VALUE;

        private int _COLLECTION_RDC_TICKETS_PRINTED_VALUE;

        private System.Nullable<int> _DeclaredTicketPrintedQty;

        private System.Nullable<float> _DeclaredTicketPrintedValue;

        private System.Nullable<System.DateTime> _Collection_Date_Performed;

        private System.Nullable<int> _progressive_win_value;

        private System.Nullable<int> _progressive_win_Handpay_value;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<int> _Mystery_Machine_Paid;
        private System.Nullable<int> _Mystery_Attendant_Paid;
        private System.Nullable<int> _RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;
        private System.Nullable<int> _RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;
        private System.Nullable<int> _Promo_Cashable_EFT_IN;
        private System.Nullable<int> _Promo_Cashable_EFT_OUT;
        private System.Nullable<int> _NonCashable_EFT_IN;
        private System.Nullable<int> _NonCashable_EFT_OUT;
        private System.Nullable<int> _Cashable_EFT_IN;
        private System.Nullable<int> _Cashable_EFT_OUT;



        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnCollection_NoChanging(int value);
        partial void OnCollection_NoChanged();
        partial void OnCollection_Batch_NoChanging(System.Nullable<int> value);
        partial void OnCollection_Batch_NoChanged();
        partial void OnMachine_SerialChanging(string value);
        partial void OnMachine_SerialChanged();
        partial void OnCollection_RDC_Machine_CodeChanging(string value);
        partial void OnCollection_RDC_Machine_CodeChanged();
        partial void OnCollection_RDC_Secondary_Machine_CodeChanging(string value);
        partial void OnCollection_RDC_Secondary_Machine_CodeChanged();
        partial void OnDatapak_Read_OccurrenceChanging(System.Nullable<int> value);
        partial void OnDatapak_Read_OccurrenceChanged();
        partial void OnFloat_LevelChanging(System.Nullable<int> value);
        partial void OnFloat_LevelChanged();
        partial void OnPeriod_IDChanging(System.Nullable<int> value);
        partial void OnPeriod_IDChanged();
        partial void OnWeek_IDChanging(System.Nullable<int> value);
        partial void OnWeek_IDChanged();
        partial void OnCASH_IN_1PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_1PChanged();
        partial void OnCASH_IN_2PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_2PChanged();
        partial void OnCASH_IN_5PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_5PChanged();
        partial void OnCASH_IN_10PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_10PChanged();
        partial void OnCASH_IN_20PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_20PChanged();
        partial void OnCASH_IN_50PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_50PChanged();
        partial void OnCASH_IN_100PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_100PChanged();
        partial void OnCASH_IN_200PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_200PChanged();
        partial void OnCASH_IN_500PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_500PChanged();
        partial void OnCASH_IN_1000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_1000PChanged();
        partial void OnCASH_IN_2000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_2000PChanged();
        partial void OnCASH_IN_5000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_5000PChanged();
        partial void OnCASH_IN_10000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_10000PChanged();
        partial void OnCASH_IN_20000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_20000PChanged();
        partial void OnCASH_IN_50000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_50000PChanged();
        partial void OnCASH_IN_100000PChanging(System.Nullable<int> value);
        partial void OnCASH_IN_100000PChanged();
        partial void OnTOKEN_IN_5PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_5PChanged();
        partial void OnTOKEN_IN_10PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_10PChanged();
        partial void OnTOKEN_IN_20PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_20PChanged();
        partial void OnTOKEN_IN_50PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_50PChanged();
        partial void OnTOKEN_IN_100PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_100PChanged();
        partial void OnTOKEN_IN_200PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_200PChanged();
        partial void OnTOKEN_IN_500PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_500PChanged();
        partial void OnTOKEN_IN_1000PChanging(System.Nullable<int> value);
        partial void OnTOKEN_IN_1000PChanged();
        partial void OnCASH_OUT_1PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_1PChanged();
        partial void OnCASH_OUT_2PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_2PChanged();
        partial void OnCASH_OUT_5PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_5PChanged();
        partial void OnCASH_OUT_10PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_10PChanged();
        partial void OnCASH_OUT_20PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_20PChanged();
        partial void OnCASH_OUT_50PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_50PChanged();
        partial void OnCASH_OUT_100PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_100PChanged();
        partial void OnCASH_OUT_200PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_200PChanged();
        partial void OnCASH_OUT_500PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_500PChanged();
        partial void OnCASH_OUT_1000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_1000PChanged();
        partial void OnCASH_OUT_2000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_2000PChanged();
        partial void OnCASH_OUT_5000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_5000PChanged();
        partial void OnCASH_OUT_10000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_10000PChanged();
        partial void OnCASH_OUT_20000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_20000PChanged();
        partial void OnCASH_OUT_50000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_50000PChanged();
        partial void OnCASH_OUT_100000PChanging(System.Nullable<int> value);
        partial void OnCASH_OUT_100000PChanged();
        partial void OnTOKEN_OUT_5PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_5PChanged();
        partial void OnTOKEN_OUT_10PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_10PChanged();
        partial void OnTOKEN_OUT_20PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_20PChanged();
        partial void OnTOKEN_OUT_50PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_50PChanged();
        partial void OnTOKEN_OUT_100PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_100PChanged();
        partial void OnTOKEN_OUT_200PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_200PChanged();
        partial void OnTOKEN_OUT_500PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_500PChanged();
        partial void OnTOKEN_OUT_1000PChanging(System.Nullable<int> value);
        partial void OnTOKEN_OUT_1000PChanged();
        partial void OnCASH_REFILL_5PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_5PChanged();
        partial void OnCASH_REFILL_10PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_10PChanged();
        partial void OnCASH_REFILL_20PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_20PChanged();
        partial void OnCASH_REFILL_50PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_50PChanged();
        partial void OnCASH_REFILL_100PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_100PChanged();
        partial void OnCASH_REFILL_200PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_200PChanged();
        partial void OnCASH_REFILL_500PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_500PChanged();
        partial void OnCASH_REFILL_1000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_1000PChanged();
        partial void OnCASH_REFILL_2000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_2000PChanged();
        partial void OnCASH_REFILL_5000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_5000PChanged();
        partial void OnCASH_REFILL_10000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_10000PChanged();
        partial void OnCASH_REFILL_20000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_20000PChanged();
        partial void OnCASH_REFILL_50000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_50000PChanged();
        partial void OnCASH_REFILL_100000PChanging(System.Nullable<int> value);
        partial void OnCASH_REFILL_100000PChanged();
        partial void OnTOKEN_REFILL_5PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_5PChanged();
        partial void OnTOKEN_REFILL_10PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_10PChanged();
        partial void OnTOKEN_REFILL_20PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_20PChanged();
        partial void OnTOKEN_REFILL_50PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_50PChanged();
        partial void OnTOKEN_REFILL_100PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_100PChanged();
        partial void OnTOKEN_REFILL_200PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_200PChanged();
        partial void OnTOKEN_REFILL_500PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_500PChanged();
        partial void OnTOKEN_REFILL_1000PChanging(System.Nullable<int> value);
        partial void OnTOKEN_REFILL_1000PChanged();
        partial void OnDeclarationChanging(System.Nullable<bool> value);
        partial void OnDeclarationChanged();
        partial void OnTreasury_TotalChanging(System.Nullable<float> value);
        partial void OnTreasury_TotalChanged();
        partial void OnCounterCashInChanging(System.Nullable<int> value);
        partial void OnCounterCashInChanged();
        partial void OnCounterCashOutChanging(System.Nullable<int> value);
        partial void OnCounterCashOutChanged();
        partial void OnCounterTokensInChanging(System.Nullable<int> value);
        partial void OnCounterTokensInChanged();
        partial void OnCounterTokensOutChanging(System.Nullable<int> value);
        partial void OnCounterTokensOutChanged();
        partial void OnCounterPrizeChanging(System.Nullable<int> value);
        partial void OnCounterPrizeChanged();
        partial void OnCounterTournamentChanging(System.Nullable<int> value);
        partial void OnCounterTournamentChanged();
        partial void OnCounterJukeboxChanging(System.Nullable<int> value);
        partial void OnCounterJukeboxChanged();
        partial void OnCounterRefillsChanging(System.Nullable<int> value);
        partial void OnCounterRefillsChanged();
        partial void OnCashCollectedChanging(System.Nullable<float> value);
        partial void OnCashCollectedChanged();
        partial void OnTokensCollectedChanging(System.Nullable<float> value);
        partial void OnTokensCollectedChanged();
        partial void OnCash_Collected_1pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_1pChanged();
        partial void OnCash_Collected_2pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_2pChanged();
        partial void OnCash_Collected_5pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_5pChanged();
        partial void OnCash_Collected_10pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_10pChanged();
        partial void OnCash_Collected_20pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_20pChanged();
        partial void OnCash_Collected_50pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_50pChanged();
        partial void OnCash_Collected_100pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_100pChanged();
        partial void OnCash_Collected_200pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_200pChanged();
        partial void OnCash_Collected_500pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_500pChanged();
        partial void OnCash_Collected_1000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_1000pChanged();
        partial void OnCash_Collected_2000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_2000pChanged();
        partial void OnCash_Collected_5000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_5000pChanged();
        partial void OnCash_Collected_10000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_10000pChanged();
        partial void OnCash_Collected_20000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_20000pChanged();
        partial void OnCash_Collected_50000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_50000pChanged();
        partial void OnCash_Collected_100000pChanging(System.Nullable<float> value);
        partial void OnCash_Collected_100000pChanged();
        partial void OnCashRefillsChanging(System.Nullable<float> value);
        partial void OnCashRefillsChanged();
        partial void OnTokenRefillsChanging(System.Nullable<float> value);
        partial void OnTokenRefillsChanged();
        partial void OnCash_Refills_2pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_2pChanged();
        partial void OnCash_Refills_5pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_5pChanged();
        partial void OnCash_Refills_10pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_10pChanged();
        partial void OnCash_Refills_20pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_20pChanged();
        partial void OnCash_Refills_50pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_50pChanged();
        partial void OnCash_Refills_100pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_100pChanged();
        partial void OnCash_Refills_200pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_200pChanged();
        partial void OnCash_Refills_500pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_500pChanged();
        partial void OnCash_Refills_1000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_1000pChanged();
        partial void OnCash_Refills_2000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_2000pChanged();
        partial void OnCash_Refills_5000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_5000pChanged();
        partial void OnCash_Refills_10000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_10000pChanged();
        partial void OnCash_Refills_20000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_20000pChanged();
        partial void OnCash_Refills_50000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_50000pChanged();
        partial void OnCash_Refills_100000pChanging(System.Nullable<float> value);
        partial void OnCash_Refills_100000pChanged();
        partial void OnCounterCashInElectronicChanging(System.Nullable<int> value);
        partial void OnCounterCashInElectronicChanged();
        partial void OnCounterCashOutElectronicChanging(System.Nullable<int> value);
        partial void OnCounterCashOutElectronicChanged();
        partial void OnCounterTokensInElectronicChanging(System.Nullable<int> value);
        partial void OnCounterTokensInElectronicChanged();
        partial void OnCounterTokensOutElectronicChanging(System.Nullable<int> value);
        partial void OnCounterTokensOutElectronicChanged();
        partial void OnJackpotsOutChanging(System.Nullable<int> value);
        partial void OnJackpotsOutChanged();
        partial void OnPreviousCounterCashInChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashInChanged();
        partial void OnPreviousCounterCashOutChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashOutChanged();
        partial void OnPreviousCounterTokensInChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensInChanged();
        partial void OnPreviousCounterTokensOutChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensOutChanged();
        partial void OnPreviousCounterPrizeChanging(System.Nullable<int> value);
        partial void OnPreviousCounterPrizeChanged();
        partial void OnPreviousCounterJackpotsOutChanging(System.Nullable<int> value);
        partial void OnPreviousCounterJackpotsOutChanged();
        partial void OnPreviousCounterTournamentChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTournamentChanged();
        partial void OnPreviousCounterJukeboxChanging(System.Nullable<int> value);
        partial void OnPreviousCounterJukeboxChanged();
        partial void OnPreviousCounterRefillsChanging(System.Nullable<int> value);
        partial void OnPreviousCounterRefillsChanged();
        partial void OnPreviousCounterCashInElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashInElectronicChanged();
        partial void OnPreviousCounterCashOutElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterCashOutElectronicChanged();
        partial void OnPreviousCounterTokensInElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensInElectronicChanged();
        partial void OnPreviousCounterTokensOutElectronicChanging(System.Nullable<int> value);
        partial void OnPreviousCounterTokensOutElectronicChanged();
        partial void OnPreviousCollectionDateChanging(System.Nullable<System.DateTime> value);
        partial void OnPreviousCollectionDateChanged();
        partial void OnPreviousCollectionNoChanging(System.Nullable<int> value);
        partial void OnPreviousCollectionNoChanged();
        partial void OnTreasury_RefillsChanging(System.Nullable<float> value);
        partial void OnTreasury_RefillsChanged();
        partial void OnTreasury_RepaymentsChanging(System.Nullable<float> value);
        partial void OnTreasury_RepaymentsChanged();
        partial void OnTreasury_TokensChanging(System.Nullable<float> value);
        partial void OnTreasury_TokensChanged();
        partial void OnExpectedBaggedCashChanging(System.Nullable<float> value);
        partial void OnExpectedBaggedCashChanged();
        partial void OnActualBaggedCashChanging(System.Nullable<float> value);
        partial void OnActualBaggedCashChanged();
        partial void OnCollection_Meters_Coins_InChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Coins_InChanged();
        partial void OnCollection_Meters_Coins_OutChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Coins_OutChanged();
        partial void OnCollection_Meters_Coin_DropChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Coin_DropChanged();
        partial void OnCollection_Meters_HandpayChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_HandpayChanged();
        partial void OnCollection_Meters_External_CreditChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_External_CreditChanged();
        partial void OnCollection_Meters_Games_BetChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Games_BetChanged();
        partial void OnCollection_Meters_Games_WonChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_Games_WonChanged();
        partial void OnCollection_Meters_NotesChanging(System.Nullable<int> value);
        partial void OnCollection_Meters_NotesChanged();
        partial void OnCollection_Treasury_DefloatChanging(System.Nullable<float> value);
        partial void OnCollection_Treasury_DefloatChanged();
        partial void OnCollection_Defloat_CollectionChanging(System.Nullable<bool> value);
        partial void OnCollection_Defloat_CollectionChanged();
        partial void OnPrevious_Meters_Coins_InChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Coins_InChanged();
        partial void OnPrevious_Meters_Coins_OutChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Coins_OutChanged();
        partial void OnPrevious_Meters_Coin_DropChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Coin_DropChanged();
        partial void OnPrevious_Meters_HandpayChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_HandpayChanged();
        partial void OnPrevious_Meters_External_CreditChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_External_CreditChanged();
        partial void OnPrevious_Meters_Games_BetChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Games_BetChanged();
        partial void OnPrevious_Meters_Games_WonChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_Games_WonChanged();
        partial void OnPrevious_Meters_NotesChanging(System.Nullable<int> value);
        partial void OnPrevious_Meters_NotesChanged();
        partial void OnTreasury_HandpayChanging(System.Nullable<float> value);
        partial void OnTreasury_HandpayChanged();
        partial void OnOperator_Week_IDChanging(System.Nullable<int> value);
        partial void OnOperator_Week_IDChanged();
        partial void OnOperator_Period_IDChanging(System.Nullable<int> value);
        partial void OnOperator_Period_IDChanged();
        partial void OnCollectionHandHeldMetersReceivedChanging(System.Nullable<int> value);
        partial void OnCollectionHandHeldMetersReceivedChanged();
        partial void OnCollectionNoDoorEventsChanging(System.Nullable<int> value);
        partial void OnCollectionNoDoorEventsChanged();
        partial void OnCollectionNoPowerEventsChanging(System.Nullable<int> value);
        partial void OnCollectionNoPowerEventsChanged();
        partial void OnCollectionNoFaultEventsChanging(System.Nullable<int> value);
        partial void OnCollectionNoFaultEventsChanged();
        partial void OnCollectionTotalDurationPowerChanging(System.Nullable<int> value);
        partial void OnCollectionTotalDurationPowerChanged();
        partial void OnCollectionTotalDurationDoorChanging(System.Nullable<int> value);
        partial void OnCollectionTotalDurationDoorChanged();
        partial void OnCOLLECTION_RDC_VTPChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_VTPChanged();
        partial void OnCollection_NetExChanging(System.Nullable<float> value);
        partial void OnCollection_NetExChanged();
        partial void OnCollection_VAT_RateChanging(System.Nullable<float> value);
        partial void OnCollection_VAT_RateChanged();
        partial void OnCOLLECTION_RDC_COINS_INChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_COINS_INChanged();
        partial void OnCOLLECTION_RDC_COINS_OUTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_COINS_OUTChanged();
        partial void OnCOLLECTION_RDC_COIN_DROPChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_COIN_DROPChanged();
        partial void OnCOLLECTION_RDC_HANDPAYChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_HANDPAYChanged();
        partial void OnCOLLECTION_RDC_EXTERNAL_CREDITChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_EXTERNAL_CREDITChanged();
        partial void OnCOLLECTION_RDC_GAMES_BETChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_BETChanged();
        partial void OnCOLLECTION_RDC_GAMES_WONChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_WONChanged();
        partial void OnCOLLECTION_RDC_NOTESChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_NOTESChanged();
        partial void OnCOLLECTION_RDC_CANCELLED_CREDITSChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_CANCELLED_CREDITSChanged();
        partial void OnCOLLECTION_RDC_GAMES_LOSTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_LOSTChanged();
        partial void OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanged();
        partial void OnCOLLECTION_RDC_TRUE_COIN_INChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_TRUE_COIN_INChanged();
        partial void OnCOLLECTION_RDC_TRUE_COIN_OUTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_TRUE_COIN_OUTChanged();
        partial void OnCOLLECTION_RDC_CURRENT_CREDITSChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_CURRENT_CREDITSChanged();
        partial void OnCollection_PoP_ActualChanging(System.Nullable<int> value);
        partial void OnCollection_PoP_ActualChanged();
        partial void OnCollection_PoP_ConfiguredChanging(System.Nullable<int> value);
        partial void OnCollection_PoP_ConfiguredChanged();
        partial void OnCollection_EDC_StatusChanging(System.Nullable<int> value);
        partial void OnCollection_EDC_StatusChanged();
        partial void OnCollection_Meter_StatusChanging(System.Nullable<int> value);
        partial void OnCollection_Meter_StatusChanged();
        partial void OnCollection_Cash_StatusChanging(System.Nullable<int> value);
        partial void OnCollection_Cash_StatusChanged();
        partial void OnInstallation_NoChanging(int value);
        partial void OnInstallation_NoChanged();
        partial void OnCollection_DateChanging(System.Nullable<System.DateTime> value);
        partial void OnCollection_DateChanged();
        partial void OnCASH_FLOAT_CHANGE_1pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_1pChanged();
        partial void OnCASH_FLOAT_CHANGE_2pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_2pChanged();
        partial void OnCASH_FLOAT_CHANGE_5pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_5pChanged();
        partial void OnCASH_FLOAT_CHANGE_10pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_10pChanged();
        partial void OnCASH_FLOAT_CHANGE_20pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_20pChanged();
        partial void OnCASH_FLOAT_CHANGE_50pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_50pChanged();
        partial void OnCASH_FLOAT_CHANGE_100pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_100pChanged();
        partial void OnCASH_FLOAT_CHANGE_200pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_200pChanged();
        partial void OnCASH_FLOAT_CHANGE_500pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_500pChanged();
        partial void OnCASH_FLOAT_CHANGE_1000pChanging(int value);
        partial void OnCASH_FLOAT_CHANGE_1000pChanged();
        partial void OnCASH_FLOAT_TOTALChanging(float value);
        partial void OnCASH_FLOAT_TOTALChanged();
        partial void OnDeclaredTicketQtyChanging(System.Nullable<int> value);
        partial void OnDeclaredTicketQtyChanged();
        partial void OnDeclaredTicketValueChanging(System.Nullable<float> value);
        partial void OnDeclaredTicketValueChanged();

        partial void OnPromoCashableValueChanging(System.Nullable<float> value);
        partial void OnPromoCashableValueChanged();
        partial void OnPromoNonCashableValueChanging(System.Nullable<float> value);
        partial void OnPromoNonCashableValueChanged();

        partial void OnCOLLECTION_RDC_JACKPOTChanging(System.Nullable<int> value);
        partial void OnCOLLECTION_RDC_JACKPOTChanged();
        partial void OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanging(int value);
        partial void OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanged();
        partial void OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanging(int value);
        partial void OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanged();
        partial void OnDeclaredTicketPrintedQtyChanging(System.Nullable<int> value);
        partial void OnDeclaredTicketPrintedQtyChanged();
        partial void OnDeclaredTicketPrintedValueChanging(System.Nullable<float> value);
        partial void OnDeclaredTicketPrintedValueChanged();
        partial void OnCollection_Date_PerformedChanging(System.Nullable<System.DateTime> value);
        partial void OnCollection_Date_PerformedChanged();
        partial void Onprogressive_win_valueChanging(System.Nullable<int> value);
        partial void Onprogressive_win_valueChanged();
        partial void Onprogressive_win_Handpay_valueChanging(System.Nullable<int> value);
        partial void Onprogressive_win_Handpay_valueChanged();
        partial void OnProgressive_Value_DeclaredChanging(System.Nullable<double> value);
        partial void OnProgressive_Value_DeclaredChanged();
        partial void OnMystery_Machine_PaidChanging(System.Nullable<int> value);
        partial void OnMystery_Machine_PaidChanged();
        partial void OnMystery_Attendant_PaidChanging(System.Nullable<int> value);
        partial void OnMystery_Attendant_PaidChanged();
        partial void OnRDC_TICKETS_INSERTED_NONCASHABLE_VALUEChanging(System.Nullable<int> value);
        partial void OnRDC_TICKETS_INSERTED_NONCASHABLE_VALUEChanged();
        partial void OnRDC_TICKETS_PRINTED_NONCASHABLE_VALUEChanging(System.Nullable<int> value);
        partial void OnRDC_TICKETS_PRINTED_NONCASHABLE_VALUEChanged();
        partial void OnPromo_Cashable_EFT_INChanging(System.Nullable<int> value);
        partial void OnPromo_Cashable_EFT_INChanged();
        partial void OnPromo_Cashable_EFT_OUTChanging(System.Nullable<int> value);
        partial void OnPromo_Cashable_EFT_OUTChanged();
        partial void OnNonCashable_EFT_INChanging(System.Nullable<int> value);
        partial void OnNonCashable_EFT_INChanged();
        partial void OnNonCashable_EFT_OUTChanging(System.Nullable<int> value);
        partial void OnNonCashable_EFT_OUTChanged();
        partial void OnCashable_EFT_INChanging(System.Nullable<int> value);
        partial void OnCashable_EFT_INChanged();
        partial void OnCashable_EFT_OUTChanging(System.Nullable<int> value);
        partial void OnCashable_EFT_OUTChanged();

        #endregion

        public CollectionRecord()
        {
            OnCreated();

        }

        [Column(Storage = "_Collection_No", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this.OnCollection_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_No = value;
                    this.SendPropertyChanged("Collection_No");
                    this.OnCollection_NoChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Batch_No", DbType = "Int")]
        public System.Nullable<int> Collection_Batch_No
        {
            get
            {
                return this._Collection_Batch_No;
            }
            set
            {
                if ((this._Collection_Batch_No != value))
                {
                    this.OnCollection_Batch_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Batch_No = value;
                    this.SendPropertyChanged("Collection_Batch_No");
                    this.OnCollection_Batch_NoChanged();
                }
            }
        }

        [Column(Storage = "_Machine_Serial", DbType = "VarChar(50)")]
        public string Machine_Serial
        {
            get
            {
                return this._Machine_Serial;
            }
            set
            {
                if ((this._Machine_Serial != value))
                {
                    this.OnMachine_SerialChanging(value);
                    this.SendPropertyChanging();
                    this._Machine_Serial = value;
                    this.SendPropertyChanged("Machine_Serial");
                    this.OnMachine_SerialChanged();
                }
            }
        }

        [Column(Storage = "_Collection_RDC_Machine_Code", DbType = "VarChar(10)")]
        public string Collection_RDC_Machine_Code
        {
            get
            {
                return this._Collection_RDC_Machine_Code;
            }
            set
            {
                if ((this._Collection_RDC_Machine_Code != value))
                {
                    this.OnCollection_RDC_Machine_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_RDC_Machine_Code = value;
                    this.SendPropertyChanged("Collection_RDC_Machine_Code");
                    this.OnCollection_RDC_Machine_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Collection_RDC_Secondary_Machine_Code", DbType = "VarChar(20)")]
        public string Collection_RDC_Secondary_Machine_Code
        {
            get
            {
                return this._Collection_RDC_Secondary_Machine_Code;
            }
            set
            {
                if ((this._Collection_RDC_Secondary_Machine_Code != value))
                {
                    this.OnCollection_RDC_Secondary_Machine_CodeChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_RDC_Secondary_Machine_Code = value;
                    this.SendPropertyChanged("Collection_RDC_Secondary_Machine_Code");
                    this.OnCollection_RDC_Secondary_Machine_CodeChanged();
                }
            }
        }

        [Column(Storage = "_Datapak_Read_Occurrence", DbType = "Int")]
        public System.Nullable<int> Datapak_Read_Occurrence
        {
            get
            {
                return this._Datapak_Read_Occurrence;
            }
            set
            {
                if ((this._Datapak_Read_Occurrence != value))
                {
                    this.OnDatapak_Read_OccurrenceChanging(value);
                    this.SendPropertyChanging();
                    this._Datapak_Read_Occurrence = value;
                    this.SendPropertyChanged("Datapak_Read_Occurrence");
                    this.OnDatapak_Read_OccurrenceChanged();
                }
            }
        }

        [Column(Storage = "_Float_Level", DbType = "Int")]
        public System.Nullable<int> Float_Level
        {
            get
            {
                return this._Float_Level;
            }
            set
            {
                if ((this._Float_Level != value))
                {
                    this.OnFloat_LevelChanging(value);
                    this.SendPropertyChanging();
                    this._Float_Level = value;
                    this.SendPropertyChanged("Float_Level");
                    this.OnFloat_LevelChanged();
                }
            }
        }

        [Column(Storage = "_Period_ID", DbType = "Int")]
        public System.Nullable<int> Period_ID
        {
            get
            {
                return this._Period_ID;
            }
            set
            {
                if ((this._Period_ID != value))
                {
                    this.OnPeriod_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Period_ID = value;
                    this.SendPropertyChanged("Period_ID");
                    this.OnPeriod_IDChanged();
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
                    this.OnWeek_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Week_ID = value;
                    this.SendPropertyChanged("Week_ID");
                    this.OnWeek_IDChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_1P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1P
        {
            get
            {
                return this._CASH_IN_1P;
            }
            set
            {
                if ((this._CASH_IN_1P != value))
                {
                    this.OnCASH_IN_1PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_1P = value;
                    this.SendPropertyChanged("CASH_IN_1P");
                    this.OnCASH_IN_1PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_2P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2P
        {
            get
            {
                return this._CASH_IN_2P;
            }
            set
            {
                if ((this._CASH_IN_2P != value))
                {
                    this.OnCASH_IN_2PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_2P = value;
                    this.SendPropertyChanged("CASH_IN_2P");
                    this.OnCASH_IN_2PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_5P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_5P
        {
            get
            {
                return this._CASH_IN_5P;
            }
            set
            {
                if ((this._CASH_IN_5P != value))
                {
                    this.OnCASH_IN_5PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_5P = value;
                    this.SendPropertyChanged("CASH_IN_5P");
                    this.OnCASH_IN_5PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_10P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_10P
        {
            get
            {
                return this._CASH_IN_10P;
            }
            set
            {
                if ((this._CASH_IN_10P != value))
                {
                    this.OnCASH_IN_10PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_10P = value;
                    this.SendPropertyChanged("CASH_IN_10P");
                    this.OnCASH_IN_10PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_20P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_20P
        {
            get
            {
                return this._CASH_IN_20P;
            }
            set
            {
                if ((this._CASH_IN_20P != value))
                {
                    this.OnCASH_IN_20PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_20P = value;
                    this.SendPropertyChanged("CASH_IN_20P");
                    this.OnCASH_IN_20PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_50P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_50P
        {
            get
            {
                return this._CASH_IN_50P;
            }
            set
            {
                if ((this._CASH_IN_50P != value))
                {
                    this.OnCASH_IN_50PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_50P = value;
                    this.SendPropertyChanged("CASH_IN_50P");
                    this.OnCASH_IN_50PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_100P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100P
        {
            get
            {
                return this._CASH_IN_100P;
            }
            set
            {
                if ((this._CASH_IN_100P != value))
                {
                    this.OnCASH_IN_100PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_100P = value;
                    this.SendPropertyChanged("CASH_IN_100P");
                    this.OnCASH_IN_100PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_200P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_200P
        {
            get
            {
                return this._CASH_IN_200P;
            }
            set
            {
                if ((this._CASH_IN_200P != value))
                {
                    this.OnCASH_IN_200PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_200P = value;
                    this.SendPropertyChanged("CASH_IN_200P");
                    this.OnCASH_IN_200PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_500P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_500P
        {
            get
            {
                return this._CASH_IN_500P;
            }
            set
            {
                if ((this._CASH_IN_500P != value))
                {
                    this.OnCASH_IN_500PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_500P = value;
                    this.SendPropertyChanged("CASH_IN_500P");
                    this.OnCASH_IN_500PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_1000P
        {
            get
            {
                return this._CASH_IN_1000P;
            }
            set
            {
                if ((this._CASH_IN_1000P != value))
                {
                    this.OnCASH_IN_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_1000P = value;
                    this.SendPropertyChanged("CASH_IN_1000P");
                    this.OnCASH_IN_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_2000P
        {
            get
            {
                return this._CASH_IN_2000P;
            }
            set
            {
                if ((this._CASH_IN_2000P != value))
                {
                    this.OnCASH_IN_2000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_2000P = value;
                    this.SendPropertyChanged("CASH_IN_2000P");
                    this.OnCASH_IN_2000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_5000P
        {
            get
            {
                return this._CASH_IN_5000P;
            }
            set
            {
                if ((this._CASH_IN_5000P != value))
                {
                    this.OnCASH_IN_5000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_5000P = value;
                    this.SendPropertyChanged("CASH_IN_5000P");
                    this.OnCASH_IN_5000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_10000P
        {
            get
            {
                return this._CASH_IN_10000P;
            }
            set
            {
                if ((this._CASH_IN_10000P != value))
                {
                    this.OnCASH_IN_10000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_10000P = value;
                    this.SendPropertyChanged("CASH_IN_10000P");
                    this.OnCASH_IN_10000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_20000P
        {
            get
            {
                return this._CASH_IN_20000P;
            }
            set
            {
                if ((this._CASH_IN_20000P != value))
                {
                    this.OnCASH_IN_20000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_20000P = value;
                    this.SendPropertyChanged("CASH_IN_20000P");
                    this.OnCASH_IN_20000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_50000P
        {
            get
            {
                return this._CASH_IN_50000P;
            }
            set
            {
                if ((this._CASH_IN_50000P != value))
                {
                    this.OnCASH_IN_50000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_50000P = value;
                    this.SendPropertyChanged("CASH_IN_50000P");
                    this.OnCASH_IN_50000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_IN_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_IN_100000P
        {
            get
            {
                return this._CASH_IN_100000P;
            }
            set
            {
                if ((this._CASH_IN_100000P != value))
                {
                    this.OnCASH_IN_100000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_IN_100000P = value;
                    this.SendPropertyChanged("CASH_IN_100000P");
                    this.OnCASH_IN_100000PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_5P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_5P
        {
            get
            {
                return this._TOKEN_IN_5P;
            }
            set
            {
                if ((this._TOKEN_IN_5P != value))
                {
                    this.OnTOKEN_IN_5PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_5P = value;
                    this.SendPropertyChanged("TOKEN_IN_5P");
                    this.OnTOKEN_IN_5PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_10P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_10P
        {
            get
            {
                return this._TOKEN_IN_10P;
            }
            set
            {
                if ((this._TOKEN_IN_10P != value))
                {
                    this.OnTOKEN_IN_10PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_10P = value;
                    this.SendPropertyChanged("TOKEN_IN_10P");
                    this.OnTOKEN_IN_10PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_20P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_20P
        {
            get
            {
                return this._TOKEN_IN_20P;
            }
            set
            {
                if ((this._TOKEN_IN_20P != value))
                {
                    this.OnTOKEN_IN_20PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_20P = value;
                    this.SendPropertyChanged("TOKEN_IN_20P");
                    this.OnTOKEN_IN_20PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_50P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_50P
        {
            get
            {
                return this._TOKEN_IN_50P;
            }
            set
            {
                if ((this._TOKEN_IN_50P != value))
                {
                    this.OnTOKEN_IN_50PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_50P = value;
                    this.SendPropertyChanged("TOKEN_IN_50P");
                    this.OnTOKEN_IN_50PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_100P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_100P
        {
            get
            {
                return this._TOKEN_IN_100P;
            }
            set
            {
                if ((this._TOKEN_IN_100P != value))
                {
                    this.OnTOKEN_IN_100PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_100P = value;
                    this.SendPropertyChanged("TOKEN_IN_100P");
                    this.OnTOKEN_IN_100PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_200P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_200P
        {
            get
            {
                return this._TOKEN_IN_200P;
            }
            set
            {
                if ((this._TOKEN_IN_200P != value))
                {
                    this.OnTOKEN_IN_200PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_200P = value;
                    this.SendPropertyChanged("TOKEN_IN_200P");
                    this.OnTOKEN_IN_200PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_500P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_500P
        {
            get
            {
                return this._TOKEN_IN_500P;
            }
            set
            {
                if ((this._TOKEN_IN_500P != value))
                {
                    this.OnTOKEN_IN_500PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_500P = value;
                    this.SendPropertyChanged("TOKEN_IN_500P");
                    this.OnTOKEN_IN_500PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_IN_1000P", DbType = "Int")]
        public System.Nullable<int> TOKEN_IN_1000P
        {
            get
            {
                return this._TOKEN_IN_1000P;
            }
            set
            {
                if ((this._TOKEN_IN_1000P != value))
                {
                    this.OnTOKEN_IN_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_IN_1000P = value;
                    this.SendPropertyChanged("TOKEN_IN_1000P");
                    this.OnTOKEN_IN_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_1P
        {
            get
            {
                return this._CASH_OUT_1P;
            }
            set
            {
                if ((this._CASH_OUT_1P != value))
                {
                    this.OnCASH_OUT_1PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_1P = value;
                    this.SendPropertyChanged("CASH_OUT_1P");
                    this.OnCASH_OUT_1PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_2P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_2P
        {
            get
            {
                return this._CASH_OUT_2P;
            }
            set
            {
                if ((this._CASH_OUT_2P != value))
                {
                    this.OnCASH_OUT_2PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_2P = value;
                    this.SendPropertyChanged("CASH_OUT_2P");
                    this.OnCASH_OUT_2PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_5P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_5P
        {
            get
            {
                return this._CASH_OUT_5P;
            }
            set
            {
                if ((this._CASH_OUT_5P != value))
                {
                    this.OnCASH_OUT_5PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_5P = value;
                    this.SendPropertyChanged("CASH_OUT_5P");
                    this.OnCASH_OUT_5PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_10P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_10P
        {
            get
            {
                return this._CASH_OUT_10P;
            }
            set
            {
                if ((this._CASH_OUT_10P != value))
                {
                    this.OnCASH_OUT_10PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_10P = value;
                    this.SendPropertyChanged("CASH_OUT_10P");
                    this.OnCASH_OUT_10PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_20P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_20P
        {
            get
            {
                return this._CASH_OUT_20P;
            }
            set
            {
                if ((this._CASH_OUT_20P != value))
                {
                    this.OnCASH_OUT_20PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_20P = value;
                    this.SendPropertyChanged("CASH_OUT_20P");
                    this.OnCASH_OUT_20PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_50P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_50P
        {
            get
            {
                return this._CASH_OUT_50P;
            }
            set
            {
                if ((this._CASH_OUT_50P != value))
                {
                    this.OnCASH_OUT_50PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_50P = value;
                    this.SendPropertyChanged("CASH_OUT_50P");
                    this.OnCASH_OUT_50PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_100P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_100P
        {
            get
            {
                return this._CASH_OUT_100P;
            }
            set
            {
                if ((this._CASH_OUT_100P != value))
                {
                    this.OnCASH_OUT_100PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_100P = value;
                    this.SendPropertyChanged("CASH_OUT_100P");
                    this.OnCASH_OUT_100PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_200P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_200P
        {
            get
            {
                return this._CASH_OUT_200P;
            }
            set
            {
                if ((this._CASH_OUT_200P != value))
                {
                    this.OnCASH_OUT_200PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_200P = value;
                    this.SendPropertyChanged("CASH_OUT_200P");
                    this.OnCASH_OUT_200PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_500P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_500P
        {
            get
            {
                return this._CASH_OUT_500P;
            }
            set
            {
                if ((this._CASH_OUT_500P != value))
                {
                    this.OnCASH_OUT_500PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_500P = value;
                    this.SendPropertyChanged("CASH_OUT_500P");
                    this.OnCASH_OUT_500PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_1000P
        {
            get
            {
                return this._CASH_OUT_1000P;
            }
            set
            {
                if ((this._CASH_OUT_1000P != value))
                {
                    this.OnCASH_OUT_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_1000P = value;
                    this.SendPropertyChanged("CASH_OUT_1000P");
                    this.OnCASH_OUT_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_2000P
        {
            get
            {
                return this._CASH_OUT_2000P;
            }
            set
            {
                if ((this._CASH_OUT_2000P != value))
                {
                    this.OnCASH_OUT_2000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_2000P = value;
                    this.SendPropertyChanged("CASH_OUT_2000P");
                    this.OnCASH_OUT_2000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_5000P
        {
            get
            {
                return this._CASH_OUT_5000P;
            }
            set
            {
                if ((this._CASH_OUT_5000P != value))
                {
                    this.OnCASH_OUT_5000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_5000P = value;
                    this.SendPropertyChanged("CASH_OUT_5000P");
                    this.OnCASH_OUT_5000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_10000P
        {
            get
            {
                return this._CASH_OUT_10000P;
            }
            set
            {
                if ((this._CASH_OUT_10000P != value))
                {
                    this.OnCASH_OUT_10000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_10000P = value;
                    this.SendPropertyChanged("CASH_OUT_10000P");
                    this.OnCASH_OUT_10000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_20000P
        {
            get
            {
                return this._CASH_OUT_20000P;
            }
            set
            {
                if ((this._CASH_OUT_20000P != value))
                {
                    this.OnCASH_OUT_20000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_20000P = value;
                    this.SendPropertyChanged("CASH_OUT_20000P");
                    this.OnCASH_OUT_20000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_50000P
        {
            get
            {
                return this._CASH_OUT_50000P;
            }
            set
            {
                if ((this._CASH_OUT_50000P != value))
                {
                    this.OnCASH_OUT_50000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_50000P = value;
                    this.SendPropertyChanged("CASH_OUT_50000P");
                    this.OnCASH_OUT_50000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_OUT_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_OUT_100000P
        {
            get
            {
                return this._CASH_OUT_100000P;
            }
            set
            {
                if ((this._CASH_OUT_100000P != value))
                {
                    this.OnCASH_OUT_100000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_OUT_100000P = value;
                    this.SendPropertyChanged("CASH_OUT_100000P");
                    this.OnCASH_OUT_100000PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_5P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_5P
        {
            get
            {
                return this._TOKEN_OUT_5P;
            }
            set
            {
                if ((this._TOKEN_OUT_5P != value))
                {
                    this.OnTOKEN_OUT_5PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_5P = value;
                    this.SendPropertyChanged("TOKEN_OUT_5P");
                    this.OnTOKEN_OUT_5PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_10P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_10P
        {
            get
            {
                return this._TOKEN_OUT_10P;
            }
            set
            {
                if ((this._TOKEN_OUT_10P != value))
                {
                    this.OnTOKEN_OUT_10PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_10P = value;
                    this.SendPropertyChanged("TOKEN_OUT_10P");
                    this.OnTOKEN_OUT_10PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_20P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_20P
        {
            get
            {
                return this._TOKEN_OUT_20P;
            }
            set
            {
                if ((this._TOKEN_OUT_20P != value))
                {
                    this.OnTOKEN_OUT_20PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_20P = value;
                    this.SendPropertyChanged("TOKEN_OUT_20P");
                    this.OnTOKEN_OUT_20PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_50P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_50P
        {
            get
            {
                return this._TOKEN_OUT_50P;
            }
            set
            {
                if ((this._TOKEN_OUT_50P != value))
                {
                    this.OnTOKEN_OUT_50PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_50P = value;
                    this.SendPropertyChanged("TOKEN_OUT_50P");
                    this.OnTOKEN_OUT_50PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_100P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_100P
        {
            get
            {
                return this._TOKEN_OUT_100P;
            }
            set
            {
                if ((this._TOKEN_OUT_100P != value))
                {
                    this.OnTOKEN_OUT_100PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_100P = value;
                    this.SendPropertyChanged("TOKEN_OUT_100P");
                    this.OnTOKEN_OUT_100PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_200P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_200P
        {
            get
            {
                return this._TOKEN_OUT_200P;
            }
            set
            {
                if ((this._TOKEN_OUT_200P != value))
                {
                    this.OnTOKEN_OUT_200PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_200P = value;
                    this.SendPropertyChanged("TOKEN_OUT_200P");
                    this.OnTOKEN_OUT_200PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_500P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_500P
        {
            get
            {
                return this._TOKEN_OUT_500P;
            }
            set
            {
                if ((this._TOKEN_OUT_500P != value))
                {
                    this.OnTOKEN_OUT_500PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_500P = value;
                    this.SendPropertyChanged("TOKEN_OUT_500P");
                    this.OnTOKEN_OUT_500PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_OUT_1000P", DbType = "Int")]
        public System.Nullable<int> TOKEN_OUT_1000P
        {
            get
            {
                return this._TOKEN_OUT_1000P;
            }
            set
            {
                if ((this._TOKEN_OUT_1000P != value))
                {
                    this.OnTOKEN_OUT_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_OUT_1000P = value;
                    this.SendPropertyChanged("TOKEN_OUT_1000P");
                    this.OnTOKEN_OUT_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_5P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_5P
        {
            get
            {
                return this._CASH_REFILL_5P;
            }
            set
            {
                if ((this._CASH_REFILL_5P != value))
                {
                    this.OnCASH_REFILL_5PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_5P = value;
                    this.SendPropertyChanged("CASH_REFILL_5P");
                    this.OnCASH_REFILL_5PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_10P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_10P
        {
            get
            {
                return this._CASH_REFILL_10P;
            }
            set
            {
                if ((this._CASH_REFILL_10P != value))
                {
                    this.OnCASH_REFILL_10PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_10P = value;
                    this.SendPropertyChanged("CASH_REFILL_10P");
                    this.OnCASH_REFILL_10PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_20P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_20P
        {
            get
            {
                return this._CASH_REFILL_20P;
            }
            set
            {
                if ((this._CASH_REFILL_20P != value))
                {
                    this.OnCASH_REFILL_20PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_20P = value;
                    this.SendPropertyChanged("CASH_REFILL_20P");
                    this.OnCASH_REFILL_20PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_50P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_50P
        {
            get
            {
                return this._CASH_REFILL_50P;
            }
            set
            {
                if ((this._CASH_REFILL_50P != value))
                {
                    this.OnCASH_REFILL_50PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_50P = value;
                    this.SendPropertyChanged("CASH_REFILL_50P");
                    this.OnCASH_REFILL_50PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_100P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_100P
        {
            get
            {
                return this._CASH_REFILL_100P;
            }
            set
            {
                if ((this._CASH_REFILL_100P != value))
                {
                    this.OnCASH_REFILL_100PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_100P = value;
                    this.SendPropertyChanged("CASH_REFILL_100P");
                    this.OnCASH_REFILL_100PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_200P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_200P
        {
            get
            {
                return this._CASH_REFILL_200P;
            }
            set
            {
                if ((this._CASH_REFILL_200P != value))
                {
                    this.OnCASH_REFILL_200PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_200P = value;
                    this.SendPropertyChanged("CASH_REFILL_200P");
                    this.OnCASH_REFILL_200PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_500P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_500P
        {
            get
            {
                return this._CASH_REFILL_500P;
            }
            set
            {
                if ((this._CASH_REFILL_500P != value))
                {
                    this.OnCASH_REFILL_500PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_500P = value;
                    this.SendPropertyChanged("CASH_REFILL_500P");
                    this.OnCASH_REFILL_500PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_1000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_1000P
        {
            get
            {
                return this._CASH_REFILL_1000P;
            }
            set
            {
                if ((this._CASH_REFILL_1000P != value))
                {
                    this.OnCASH_REFILL_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_1000P = value;
                    this.SendPropertyChanged("CASH_REFILL_1000P");
                    this.OnCASH_REFILL_1000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_2000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_2000P
        {
            get
            {
                return this._CASH_REFILL_2000P;
            }
            set
            {
                if ((this._CASH_REFILL_2000P != value))
                {
                    this.OnCASH_REFILL_2000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_2000P = value;
                    this.SendPropertyChanged("CASH_REFILL_2000P");
                    this.OnCASH_REFILL_2000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_5000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_5000P
        {
            get
            {
                return this._CASH_REFILL_5000P;
            }
            set
            {
                if ((this._CASH_REFILL_5000P != value))
                {
                    this.OnCASH_REFILL_5000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_5000P = value;
                    this.SendPropertyChanged("CASH_REFILL_5000P");
                    this.OnCASH_REFILL_5000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_10000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_10000P
        {
            get
            {
                return this._CASH_REFILL_10000P;
            }
            set
            {
                if ((this._CASH_REFILL_10000P != value))
                {
                    this.OnCASH_REFILL_10000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_10000P = value;
                    this.SendPropertyChanged("CASH_REFILL_10000P");
                    this.OnCASH_REFILL_10000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_20000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_20000P
        {
            get
            {
                return this._CASH_REFILL_20000P;
            }
            set
            {
                if ((this._CASH_REFILL_20000P != value))
                {
                    this.OnCASH_REFILL_20000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_20000P = value;
                    this.SendPropertyChanged("CASH_REFILL_20000P");
                    this.OnCASH_REFILL_20000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_50000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_50000P
        {
            get
            {
                return this._CASH_REFILL_50000P;
            }
            set
            {
                if ((this._CASH_REFILL_50000P != value))
                {
                    this.OnCASH_REFILL_50000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_50000P = value;
                    this.SendPropertyChanged("CASH_REFILL_50000P");
                    this.OnCASH_REFILL_50000PChanged();
                }
            }
        }

        [Column(Storage = "_CASH_REFILL_100000P", DbType = "Int")]
        public System.Nullable<int> CASH_REFILL_100000P
        {
            get
            {
                return this._CASH_REFILL_100000P;
            }
            set
            {
                if ((this._CASH_REFILL_100000P != value))
                {
                    this.OnCASH_REFILL_100000PChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_REFILL_100000P = value;
                    this.SendPropertyChanged("CASH_REFILL_100000P");
                    this.OnCASH_REFILL_100000PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_5P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_5P
        {
            get
            {
                return this._TOKEN_REFILL_5P;
            }
            set
            {
                if ((this._TOKEN_REFILL_5P != value))
                {
                    this.OnTOKEN_REFILL_5PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_5P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_5P");
                    this.OnTOKEN_REFILL_5PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_10P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_10P
        {
            get
            {
                return this._TOKEN_REFILL_10P;
            }
            set
            {
                if ((this._TOKEN_REFILL_10P != value))
                {
                    this.OnTOKEN_REFILL_10PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_10P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_10P");
                    this.OnTOKEN_REFILL_10PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_20P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_20P
        {
            get
            {
                return this._TOKEN_REFILL_20P;
            }
            set
            {
                if ((this._TOKEN_REFILL_20P != value))
                {
                    this.OnTOKEN_REFILL_20PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_20P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_20P");
                    this.OnTOKEN_REFILL_20PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_50P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_50P
        {
            get
            {
                return this._TOKEN_REFILL_50P;
            }
            set
            {
                if ((this._TOKEN_REFILL_50P != value))
                {
                    this.OnTOKEN_REFILL_50PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_50P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_50P");
                    this.OnTOKEN_REFILL_50PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_100P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_100P
        {
            get
            {
                return this._TOKEN_REFILL_100P;
            }
            set
            {
                if ((this._TOKEN_REFILL_100P != value))
                {
                    this.OnTOKEN_REFILL_100PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_100P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_100P");
                    this.OnTOKEN_REFILL_100PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_200P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_200P
        {
            get
            {
                return this._TOKEN_REFILL_200P;
            }
            set
            {
                if ((this._TOKEN_REFILL_200P != value))
                {
                    this.OnTOKEN_REFILL_200PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_200P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_200P");
                    this.OnTOKEN_REFILL_200PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_500P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_500P
        {
            get
            {
                return this._TOKEN_REFILL_500P;
            }
            set
            {
                if ((this._TOKEN_REFILL_500P != value))
                {
                    this.OnTOKEN_REFILL_500PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_500P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_500P");
                    this.OnTOKEN_REFILL_500PChanged();
                }
            }
        }

        [Column(Storage = "_TOKEN_REFILL_1000P", DbType = "Int")]
        public System.Nullable<int> TOKEN_REFILL_1000P
        {
            get
            {
                return this._TOKEN_REFILL_1000P;
            }
            set
            {
                if ((this._TOKEN_REFILL_1000P != value))
                {
                    this.OnTOKEN_REFILL_1000PChanging(value);
                    this.SendPropertyChanging();
                    this._TOKEN_REFILL_1000P = value;
                    this.SendPropertyChanged("TOKEN_REFILL_1000P");
                    this.OnTOKEN_REFILL_1000PChanged();
                }
            }
        }

        [Column(Storage = "_Declaration", DbType = "Bit")]
        public System.Nullable<bool> Declaration
        {
            get
            {
                return this._Declaration;
            }
            set
            {
                if ((this._Declaration != value))
                {
                    this.OnDeclarationChanging(value);
                    this.SendPropertyChanging();
                    this._Declaration = value;
                    this.SendPropertyChanged("Declaration");
                    this.OnDeclarationChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Total", DbType = "Real")]
        public System.Nullable<float> Treasury_Total
        {
            get
            {
                return this._Treasury_Total;
            }
            set
            {
                if ((this._Treasury_Total != value))
                {
                    this.OnTreasury_TotalChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Total = value;
                    this.SendPropertyChanged("Treasury_Total");
                    this.OnTreasury_TotalChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashIn", DbType = "Int")]
        public System.Nullable<int> CounterCashIn
        {
            get
            {
                return this._CounterCashIn;
            }
            set
            {
                if ((this._CounterCashIn != value))
                {
                    this.OnCounterCashInChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashIn = value;
                    this.SendPropertyChanged("CounterCashIn");
                    this.OnCounterCashInChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashOut", DbType = "Int")]
        public System.Nullable<int> CounterCashOut
        {
            get
            {
                return this._CounterCashOut;
            }
            set
            {
                if ((this._CounterCashOut != value))
                {
                    this.OnCounterCashOutChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashOut = value;
                    this.SendPropertyChanged("CounterCashOut");
                    this.OnCounterCashOutChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensIn", DbType = "Int")]
        public System.Nullable<int> CounterTokensIn
        {
            get
            {
                return this._CounterTokensIn;
            }
            set
            {
                if ((this._CounterTokensIn != value))
                {
                    this.OnCounterTokensInChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensIn = value;
                    this.SendPropertyChanged("CounterTokensIn");
                    this.OnCounterTokensInChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensOut", DbType = "Int")]
        public System.Nullable<int> CounterTokensOut
        {
            get
            {
                return this._CounterTokensOut;
            }
            set
            {
                if ((this._CounterTokensOut != value))
                {
                    this.OnCounterTokensOutChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensOut = value;
                    this.SendPropertyChanged("CounterTokensOut");
                    this.OnCounterTokensOutChanged();
                }
            }
        }

        [Column(Storage = "_CounterPrize", DbType = "Int")]
        public System.Nullable<int> CounterPrize
        {
            get
            {
                return this._CounterPrize;
            }
            set
            {
                if ((this._CounterPrize != value))
                {
                    this.OnCounterPrizeChanging(value);
                    this.SendPropertyChanging();
                    this._CounterPrize = value;
                    this.SendPropertyChanged("CounterPrize");
                    this.OnCounterPrizeChanged();
                }
            }
        }

        [Column(Storage = "_CounterTournament", DbType = "Int")]
        public System.Nullable<int> CounterTournament
        {
            get
            {
                return this._CounterTournament;
            }
            set
            {
                if ((this._CounterTournament != value))
                {
                    this.OnCounterTournamentChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTournament = value;
                    this.SendPropertyChanged("CounterTournament");
                    this.OnCounterTournamentChanged();
                }
            }
        }

        [Column(Storage = "_CounterJukebox", DbType = "Int")]
        public System.Nullable<int> CounterJukebox
        {
            get
            {
                return this._CounterJukebox;
            }
            set
            {
                if ((this._CounterJukebox != value))
                {
                    this.OnCounterJukeboxChanging(value);
                    this.SendPropertyChanging();
                    this._CounterJukebox = value;
                    this.SendPropertyChanged("CounterJukebox");
                    this.OnCounterJukeboxChanged();
                }
            }
        }

        [Column(Storage = "_CounterRefills", DbType = "Int")]
        public System.Nullable<int> CounterRefills
        {
            get
            {
                return this._CounterRefills;
            }
            set
            {
                if ((this._CounterRefills != value))
                {
                    this.OnCounterRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._CounterRefills = value;
                    this.SendPropertyChanged("CounterRefills");
                    this.OnCounterRefillsChanged();
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Real")]
        public System.Nullable<float> CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this.OnCashCollectedChanging(value);
                    this.SendPropertyChanging();
                    this._CashCollected = value;
                    this.SendPropertyChanged("CashCollected");
                    this.OnCashCollectedChanged();
                }
            }
        }

        [Column(Storage = "_TokensCollected", DbType = "Real")]
        public System.Nullable<float> TokensCollected
        {
            get
            {
                return this._TokensCollected;
            }
            set
            {
                if ((this._TokensCollected != value))
                {
                    this.OnTokensCollectedChanging(value);
                    this.SendPropertyChanging();
                    this._TokensCollected = value;
                    this.SendPropertyChanged("TokensCollected");
                    this.OnTokensCollectedChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1p
        {
            get
            {
                return this._Cash_Collected_1p;
            }
            set
            {
                if ((this._Cash_Collected_1p != value))
                {
                    this.OnCash_Collected_1pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_1p = value;
                    this.SendPropertyChanged("Cash_Collected_1p");
                    this.OnCash_Collected_1pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2p
        {
            get
            {
                return this._Cash_Collected_2p;
            }
            set
            {
                if ((this._Cash_Collected_2p != value))
                {
                    this.OnCash_Collected_2pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_2p = value;
                    this.SendPropertyChanged("Cash_Collected_2p");
                    this.OnCash_Collected_2pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5p
        {
            get
            {
                return this._Cash_Collected_5p;
            }
            set
            {
                if ((this._Cash_Collected_5p != value))
                {
                    this.OnCash_Collected_5pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_5p = value;
                    this.SendPropertyChanged("Cash_Collected_5p");
                    this.OnCash_Collected_5pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10p
        {
            get
            {
                return this._Cash_Collected_10p;
            }
            set
            {
                if ((this._Cash_Collected_10p != value))
                {
                    this.OnCash_Collected_10pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_10p = value;
                    this.SendPropertyChanged("Cash_Collected_10p");
                    this.OnCash_Collected_10pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20p
        {
            get
            {
                return this._Cash_Collected_20p;
            }
            set
            {
                if ((this._Cash_Collected_20p != value))
                {
                    this.OnCash_Collected_20pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_20p = value;
                    this.SendPropertyChanged("Cash_Collected_20p");
                    this.OnCash_Collected_20pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50p
        {
            get
            {
                return this._Cash_Collected_50p;
            }
            set
            {
                if ((this._Cash_Collected_50p != value))
                {
                    this.OnCash_Collected_50pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_50p = value;
                    this.SendPropertyChanged("Cash_Collected_50p");
                    this.OnCash_Collected_50pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100p
        {
            get
            {
                return this._Cash_Collected_100p;
            }
            set
            {
                if ((this._Cash_Collected_100p != value))
                {
                    this.OnCash_Collected_100pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_100p = value;
                    this.SendPropertyChanged("Cash_Collected_100p");
                    this.OnCash_Collected_100pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_200p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_200p
        {
            get
            {
                return this._Cash_Collected_200p;
            }
            set
            {
                if ((this._Cash_Collected_200p != value))
                {
                    this.OnCash_Collected_200pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_200p = value;
                    this.SendPropertyChanged("Cash_Collected_200p");
                    this.OnCash_Collected_200pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_500p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_500p
        {
            get
            {
                return this._Cash_Collected_500p;
            }
            set
            {
                if ((this._Cash_Collected_500p != value))
                {
                    this.OnCash_Collected_500pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_500p = value;
                    this.SendPropertyChanged("Cash_Collected_500p");
                    this.OnCash_Collected_500pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_1000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_1000p
        {
            get
            {
                return this._Cash_Collected_1000p;
            }
            set
            {
                if ((this._Cash_Collected_1000p != value))
                {
                    this.OnCash_Collected_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_1000p = value;
                    this.SendPropertyChanged("Cash_Collected_1000p");
                    this.OnCash_Collected_1000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_2000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_2000p
        {
            get
            {
                return this._Cash_Collected_2000p;
            }
            set
            {
                if ((this._Cash_Collected_2000p != value))
                {
                    this.OnCash_Collected_2000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_2000p = value;
                    this.SendPropertyChanged("Cash_Collected_2000p");
                    this.OnCash_Collected_2000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_5000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_5000p
        {
            get
            {
                return this._Cash_Collected_5000p;
            }
            set
            {
                if ((this._Cash_Collected_5000p != value))
                {
                    this.OnCash_Collected_5000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_5000p = value;
                    this.SendPropertyChanged("Cash_Collected_5000p");
                    this.OnCash_Collected_5000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_10000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_10000p
        {
            get
            {
                return this._Cash_Collected_10000p;
            }
            set
            {
                if ((this._Cash_Collected_10000p != value))
                {
                    this.OnCash_Collected_10000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_10000p = value;
                    this.SendPropertyChanged("Cash_Collected_10000p");
                    this.OnCash_Collected_10000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_20000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_20000p
        {
            get
            {
                return this._Cash_Collected_20000p;
            }
            set
            {
                if ((this._Cash_Collected_20000p != value))
                {
                    this.OnCash_Collected_20000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_20000p = value;
                    this.SendPropertyChanged("Cash_Collected_20000p");
                    this.OnCash_Collected_20000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_50000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_50000p
        {
            get
            {
                return this._Cash_Collected_50000p;
            }
            set
            {
                if ((this._Cash_Collected_50000p != value))
                {
                    this.OnCash_Collected_50000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_50000p = value;
                    this.SendPropertyChanged("Cash_Collected_50000p");
                    this.OnCash_Collected_50000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Collected_100000p", DbType = "Real")]
        public System.Nullable<float> Cash_Collected_100000p
        {
            get
            {
                return this._Cash_Collected_100000p;
            }
            set
            {
                if ((this._Cash_Collected_100000p != value))
                {
                    this.OnCash_Collected_100000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Collected_100000p = value;
                    this.SendPropertyChanged("Cash_Collected_100000p");
                    this.OnCash_Collected_100000pChanged();
                }
            }
        }

        [Column(Storage = "_CashRefills", DbType = "Real")]
        public System.Nullable<float> CashRefills
        {
            get
            {
                return this._CashRefills;
            }
            set
            {
                if ((this._CashRefills != value))
                {
                    this.OnCashRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._CashRefills = value;
                    this.SendPropertyChanged("CashRefills");
                    this.OnCashRefillsChanged();
                }
            }
        }

        [Column(Storage = "_TokenRefills", DbType = "Real")]
        public System.Nullable<float> TokenRefills
        {
            get
            {
                return this._TokenRefills;
            }
            set
            {
                if ((this._TokenRefills != value))
                {
                    this.OnTokenRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._TokenRefills = value;
                    this.SendPropertyChanged("TokenRefills");
                    this.OnTokenRefillsChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_2p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_2p
        {
            get
            {
                return this._Cash_Refills_2p;
            }
            set
            {
                if ((this._Cash_Refills_2p != value))
                {
                    this.OnCash_Refills_2pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_2p = value;
                    this.SendPropertyChanged("Cash_Refills_2p");
                    this.OnCash_Refills_2pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_5p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_5p
        {
            get
            {
                return this._Cash_Refills_5p;
            }
            set
            {
                if ((this._Cash_Refills_5p != value))
                {
                    this.OnCash_Refills_5pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_5p = value;
                    this.SendPropertyChanged("Cash_Refills_5p");
                    this.OnCash_Refills_5pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_10p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_10p
        {
            get
            {
                return this._Cash_Refills_10p;
            }
            set
            {
                if ((this._Cash_Refills_10p != value))
                {
                    this.OnCash_Refills_10pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_10p = value;
                    this.SendPropertyChanged("Cash_Refills_10p");
                    this.OnCash_Refills_10pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_20p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_20p
        {
            get
            {
                return this._Cash_Refills_20p;
            }
            set
            {
                if ((this._Cash_Refills_20p != value))
                {
                    this.OnCash_Refills_20pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_20p = value;
                    this.SendPropertyChanged("Cash_Refills_20p");
                    this.OnCash_Refills_20pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_50p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_50p
        {
            get
            {
                return this._Cash_Refills_50p;
            }
            set
            {
                if ((this._Cash_Refills_50p != value))
                {
                    this.OnCash_Refills_50pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_50p = value;
                    this.SendPropertyChanged("Cash_Refills_50p");
                    this.OnCash_Refills_50pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_100p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_100p
        {
            get
            {
                return this._Cash_Refills_100p;
            }
            set
            {
                if ((this._Cash_Refills_100p != value))
                {
                    this.OnCash_Refills_100pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_100p = value;
                    this.SendPropertyChanged("Cash_Refills_100p");
                    this.OnCash_Refills_100pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_200p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_200p
        {
            get
            {
                return this._Cash_Refills_200p;
            }
            set
            {
                if ((this._Cash_Refills_200p != value))
                {
                    this.OnCash_Refills_200pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_200p = value;
                    this.SendPropertyChanged("Cash_Refills_200p");
                    this.OnCash_Refills_200pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_500p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_500p
        {
            get
            {
                return this._Cash_Refills_500p;
            }
            set
            {
                if ((this._Cash_Refills_500p != value))
                {
                    this.OnCash_Refills_500pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_500p = value;
                    this.SendPropertyChanged("Cash_Refills_500p");
                    this.OnCash_Refills_500pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_1000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_1000p
        {
            get
            {
                return this._Cash_Refills_1000p;
            }
            set
            {
                if ((this._Cash_Refills_1000p != value))
                {
                    this.OnCash_Refills_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_1000p = value;
                    this.SendPropertyChanged("Cash_Refills_1000p");
                    this.OnCash_Refills_1000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_2000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_2000p
        {
            get
            {
                return this._Cash_Refills_2000p;
            }
            set
            {
                if ((this._Cash_Refills_2000p != value))
                {
                    this.OnCash_Refills_2000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_2000p = value;
                    this.SendPropertyChanged("Cash_Refills_2000p");
                    this.OnCash_Refills_2000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_5000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_5000p
        {
            get
            {
                return this._Cash_Refills_5000p;
            }
            set
            {
                if ((this._Cash_Refills_5000p != value))
                {
                    this.OnCash_Refills_5000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_5000p = value;
                    this.SendPropertyChanged("Cash_Refills_5000p");
                    this.OnCash_Refills_5000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_10000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_10000p
        {
            get
            {
                return this._Cash_Refills_10000p;
            }
            set
            {
                if ((this._Cash_Refills_10000p != value))
                {
                    this.OnCash_Refills_10000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_10000p = value;
                    this.SendPropertyChanged("Cash_Refills_10000p");
                    this.OnCash_Refills_10000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_20000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_20000p
        {
            get
            {
                return this._Cash_Refills_20000p;
            }
            set
            {
                if ((this._Cash_Refills_20000p != value))
                {
                    this.OnCash_Refills_20000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_20000p = value;
                    this.SendPropertyChanged("Cash_Refills_20000p");
                    this.OnCash_Refills_20000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_50000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_50000p
        {
            get
            {
                return this._Cash_Refills_50000p;
            }
            set
            {
                if ((this._Cash_Refills_50000p != value))
                {
                    this.OnCash_Refills_50000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_50000p = value;
                    this.SendPropertyChanged("Cash_Refills_50000p");
                    this.OnCash_Refills_50000pChanged();
                }
            }
        }

        [Column(Storage = "_Cash_Refills_100000p", DbType = "Real")]
        public System.Nullable<float> Cash_Refills_100000p
        {
            get
            {
                return this._Cash_Refills_100000p;
            }
            set
            {
                if ((this._Cash_Refills_100000p != value))
                {
                    this.OnCash_Refills_100000pChanging(value);
                    this.SendPropertyChanging();
                    this._Cash_Refills_100000p = value;
                    this.SendPropertyChanged("Cash_Refills_100000p");
                    this.OnCash_Refills_100000pChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashInElectronic", DbType = "Int")]
        public System.Nullable<int> CounterCashInElectronic
        {
            get
            {
                return this._CounterCashInElectronic;
            }
            set
            {
                if ((this._CounterCashInElectronic != value))
                {
                    this.OnCounterCashInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashInElectronic = value;
                    this.SendPropertyChanged("CounterCashInElectronic");
                    this.OnCounterCashInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_CounterCashOutElectronic", DbType = "Int")]
        public System.Nullable<int> CounterCashOutElectronic
        {
            get
            {
                return this._CounterCashOutElectronic;
            }
            set
            {
                if ((this._CounterCashOutElectronic != value))
                {
                    this.OnCounterCashOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterCashOutElectronic = value;
                    this.SendPropertyChanged("CounterCashOutElectronic");
                    this.OnCounterCashOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensInElectronic", DbType = "Int")]
        public System.Nullable<int> CounterTokensInElectronic
        {
            get
            {
                return this._CounterTokensInElectronic;
            }
            set
            {
                if ((this._CounterTokensInElectronic != value))
                {
                    this.OnCounterTokensInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensInElectronic = value;
                    this.SendPropertyChanged("CounterTokensInElectronic");
                    this.OnCounterTokensInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_CounterTokensOutElectronic", DbType = "Int")]
        public System.Nullable<int> CounterTokensOutElectronic
        {
            get
            {
                return this._CounterTokensOutElectronic;
            }
            set
            {
                if ((this._CounterTokensOutElectronic != value))
                {
                    this.OnCounterTokensOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._CounterTokensOutElectronic = value;
                    this.SendPropertyChanged("CounterTokensOutElectronic");
                    this.OnCounterTokensOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_JackpotsOut", DbType = "Int")]
        public System.Nullable<int> JackpotsOut
        {
            get
            {
                return this._JackpotsOut;
            }
            set
            {
                if ((this._JackpotsOut != value))
                {
                    this.OnJackpotsOutChanging(value);
                    this.SendPropertyChanging();
                    this._JackpotsOut = value;
                    this.SendPropertyChanged("JackpotsOut");
                    this.OnJackpotsOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashIn", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashIn
        {
            get
            {
                return this._PreviousCounterCashIn;
            }
            set
            {
                if ((this._PreviousCounterCashIn != value))
                {
                    this.OnPreviousCounterCashInChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashIn = value;
                    this.SendPropertyChanged("PreviousCounterCashIn");
                    this.OnPreviousCounterCashInChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashOut", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashOut
        {
            get
            {
                return this._PreviousCounterCashOut;
            }
            set
            {
                if ((this._PreviousCounterCashOut != value))
                {
                    this.OnPreviousCounterCashOutChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashOut = value;
                    this.SendPropertyChanged("PreviousCounterCashOut");
                    this.OnPreviousCounterCashOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensIn", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensIn
        {
            get
            {
                return this._PreviousCounterTokensIn;
            }
            set
            {
                if ((this._PreviousCounterTokensIn != value))
                {
                    this.OnPreviousCounterTokensInChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensIn = value;
                    this.SendPropertyChanged("PreviousCounterTokensIn");
                    this.OnPreviousCounterTokensInChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensOut", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensOut
        {
            get
            {
                return this._PreviousCounterTokensOut;
            }
            set
            {
                if ((this._PreviousCounterTokensOut != value))
                {
                    this.OnPreviousCounterTokensOutChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensOut = value;
                    this.SendPropertyChanged("PreviousCounterTokensOut");
                    this.OnPreviousCounterTokensOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterPrize", DbType = "Int")]
        public System.Nullable<int> PreviousCounterPrize
        {
            get
            {
                return this._PreviousCounterPrize;
            }
            set
            {
                if ((this._PreviousCounterPrize != value))
                {
                    this.OnPreviousCounterPrizeChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterPrize = value;
                    this.SendPropertyChanged("PreviousCounterPrize");
                    this.OnPreviousCounterPrizeChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterJackpotsOut", DbType = "Int")]
        public System.Nullable<int> PreviousCounterJackpotsOut
        {
            get
            {
                return this._PreviousCounterJackpotsOut;
            }
            set
            {
                if ((this._PreviousCounterJackpotsOut != value))
                {
                    this.OnPreviousCounterJackpotsOutChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterJackpotsOut = value;
                    this.SendPropertyChanged("PreviousCounterJackpotsOut");
                    this.OnPreviousCounterJackpotsOutChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTournament", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTournament
        {
            get
            {
                return this._PreviousCounterTournament;
            }
            set
            {
                if ((this._PreviousCounterTournament != value))
                {
                    this.OnPreviousCounterTournamentChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTournament = value;
                    this.SendPropertyChanged("PreviousCounterTournament");
                    this.OnPreviousCounterTournamentChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterJukebox", DbType = "Int")]
        public System.Nullable<int> PreviousCounterJukebox
        {
            get
            {
                return this._PreviousCounterJukebox;
            }
            set
            {
                if ((this._PreviousCounterJukebox != value))
                {
                    this.OnPreviousCounterJukeboxChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterJukebox = value;
                    this.SendPropertyChanged("PreviousCounterJukebox");
                    this.OnPreviousCounterJukeboxChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterRefills", DbType = "Int")]
        public System.Nullable<int> PreviousCounterRefills
        {
            get
            {
                return this._PreviousCounterRefills;
            }
            set
            {
                if ((this._PreviousCounterRefills != value))
                {
                    this.OnPreviousCounterRefillsChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterRefills = value;
                    this.SendPropertyChanged("PreviousCounterRefills");
                    this.OnPreviousCounterRefillsChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashInElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashInElectronic
        {
            get
            {
                return this._PreviousCounterCashInElectronic;
            }
            set
            {
                if ((this._PreviousCounterCashInElectronic != value))
                {
                    this.OnPreviousCounterCashInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashInElectronic = value;
                    this.SendPropertyChanged("PreviousCounterCashInElectronic");
                    this.OnPreviousCounterCashInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterCashOutElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterCashOutElectronic
        {
            get
            {
                return this._PreviousCounterCashOutElectronic;
            }
            set
            {
                if ((this._PreviousCounterCashOutElectronic != value))
                {
                    this.OnPreviousCounterCashOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterCashOutElectronic = value;
                    this.SendPropertyChanged("PreviousCounterCashOutElectronic");
                    this.OnPreviousCounterCashOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensInElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensInElectronic
        {
            get
            {
                return this._PreviousCounterTokensInElectronic;
            }
            set
            {
                if ((this._PreviousCounterTokensInElectronic != value))
                {
                    this.OnPreviousCounterTokensInElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensInElectronic = value;
                    this.SendPropertyChanged("PreviousCounterTokensInElectronic");
                    this.OnPreviousCounterTokensInElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCounterTokensOutElectronic", DbType = "Int")]
        public System.Nullable<int> PreviousCounterTokensOutElectronic
        {
            get
            {
                return this._PreviousCounterTokensOutElectronic;
            }
            set
            {
                if ((this._PreviousCounterTokensOutElectronic != value))
                {
                    this.OnPreviousCounterTokensOutElectronicChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCounterTokensOutElectronic = value;
                    this.SendPropertyChanged("PreviousCounterTokensOutElectronic");
                    this.OnPreviousCounterTokensOutElectronicChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCollectionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PreviousCollectionDate
        {
            get
            {
                return this._PreviousCollectionDate;
            }
            set
            {
                if ((this._PreviousCollectionDate != value))
                {
                    this.OnPreviousCollectionDateChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCollectionDate = value;
                    this.SendPropertyChanged("PreviousCollectionDate");
                    this.OnPreviousCollectionDateChanged();
                }
            }
        }

        [Column(Storage = "_PreviousCollectionNo", DbType = "Int")]
        public System.Nullable<int> PreviousCollectionNo
        {
            get
            {
                return this._PreviousCollectionNo;
            }
            set
            {
                if ((this._PreviousCollectionNo != value))
                {
                    this.OnPreviousCollectionNoChanging(value);
                    this.SendPropertyChanging();
                    this._PreviousCollectionNo = value;
                    this.SendPropertyChanged("PreviousCollectionNo");
                    this.OnPreviousCollectionNoChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Refills", DbType = "Real")]
        public System.Nullable<float> Treasury_Refills
        {
            get
            {
                return this._Treasury_Refills;
            }
            set
            {
                if ((this._Treasury_Refills != value))
                {
                    this.OnTreasury_RefillsChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Refills = value;
                    this.SendPropertyChanged("Treasury_Refills");
                    this.OnTreasury_RefillsChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Repayments", DbType = "Real")]
        public System.Nullable<float> Treasury_Repayments
        {
            get
            {
                return this._Treasury_Repayments;
            }
            set
            {
                if ((this._Treasury_Repayments != value))
                {
                    this.OnTreasury_RepaymentsChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Repayments = value;
                    this.SendPropertyChanged("Treasury_Repayments");
                    this.OnTreasury_RepaymentsChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Tokens", DbType = "Real")]
        public System.Nullable<float> Treasury_Tokens
        {
            get
            {
                return this._Treasury_Tokens;
            }
            set
            {
                if ((this._Treasury_Tokens != value))
                {
                    this.OnTreasury_TokensChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Tokens = value;
                    this.SendPropertyChanged("Treasury_Tokens");
                    this.OnTreasury_TokensChanged();
                }
            }
        }

        [Column(Storage = "_ExpectedBaggedCash", DbType = "Real")]
        public System.Nullable<float> ExpectedBaggedCash
        {
            get
            {
                return this._ExpectedBaggedCash;
            }
            set
            {
                if ((this._ExpectedBaggedCash != value))
                {
                    this.OnExpectedBaggedCashChanging(value);
                    this.SendPropertyChanging();
                    this._ExpectedBaggedCash = value;
                    this.SendPropertyChanged("ExpectedBaggedCash");
                    this.OnExpectedBaggedCashChanged();
                }
            }
        }

        [Column(Storage = "_ActualBaggedCash", DbType = "Real")]
        public System.Nullable<float> ActualBaggedCash
        {
            get
            {
                return this._ActualBaggedCash;
            }
            set
            {
                if ((this._ActualBaggedCash != value))
                {
                    this.OnActualBaggedCashChanging(value);
                    this.SendPropertyChanging();
                    this._ActualBaggedCash = value;
                    this.SendPropertyChanged("ActualBaggedCash");
                    this.OnActualBaggedCashChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Coins_In", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Coins_In
        {
            get
            {
                return this._Collection_Meters_Coins_In;
            }
            set
            {
                if ((this._Collection_Meters_Coins_In != value))
                {
                    this.OnCollection_Meters_Coins_InChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Coins_In = value;
                    this.SendPropertyChanged("Collection_Meters_Coins_In");
                    this.OnCollection_Meters_Coins_InChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Coins_Out
        {
            get
            {
                return this._Collection_Meters_Coins_Out;
            }
            set
            {
                if ((this._Collection_Meters_Coins_Out != value))
                {
                    this.OnCollection_Meters_Coins_OutChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Coins_Out = value;
                    this.SendPropertyChanged("Collection_Meters_Coins_Out");
                    this.OnCollection_Meters_Coins_OutChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Coin_Drop", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Coin_Drop
        {
            get
            {
                return this._Collection_Meters_Coin_Drop;
            }
            set
            {
                if ((this._Collection_Meters_Coin_Drop != value))
                {
                    this.OnCollection_Meters_Coin_DropChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Coin_Drop = value;
                    this.SendPropertyChanged("Collection_Meters_Coin_Drop");
                    this.OnCollection_Meters_Coin_DropChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Handpay", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Handpay
        {
            get
            {
                return this._Collection_Meters_Handpay;
            }
            set
            {
                if ((this._Collection_Meters_Handpay != value))
                {
                    this.OnCollection_Meters_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Handpay = value;
                    this.SendPropertyChanged("Collection_Meters_Handpay");
                    this.OnCollection_Meters_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_External_Credit", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_External_Credit
        {
            get
            {
                return this._Collection_Meters_External_Credit;
            }
            set
            {
                if ((this._Collection_Meters_External_Credit != value))
                {
                    this.OnCollection_Meters_External_CreditChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_External_Credit = value;
                    this.SendPropertyChanged("Collection_Meters_External_Credit");
                    this.OnCollection_Meters_External_CreditChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Games_Bet
        {
            get
            {
                return this._Collection_Meters_Games_Bet;
            }
            set
            {
                if ((this._Collection_Meters_Games_Bet != value))
                {
                    this.OnCollection_Meters_Games_BetChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Games_Bet = value;
                    this.SendPropertyChanged("Collection_Meters_Games_Bet");
                    this.OnCollection_Meters_Games_BetChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Games_Won", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Games_Won
        {
            get
            {
                return this._Collection_Meters_Games_Won;
            }
            set
            {
                if ((this._Collection_Meters_Games_Won != value))
                {
                    this.OnCollection_Meters_Games_WonChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Games_Won = value;
                    this.SendPropertyChanged("Collection_Meters_Games_Won");
                    this.OnCollection_Meters_Games_WonChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meters_Notes", DbType = "Int")]
        public System.Nullable<int> Collection_Meters_Notes
        {
            get
            {
                return this._Collection_Meters_Notes;
            }
            set
            {
                if ((this._Collection_Meters_Notes != value))
                {
                    this.OnCollection_Meters_NotesChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meters_Notes = value;
                    this.SendPropertyChanged("Collection_Meters_Notes");
                    this.OnCollection_Meters_NotesChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Treasury_Defloat", DbType = "Real")]
        public System.Nullable<float> Collection_Treasury_Defloat
        {
            get
            {
                return this._Collection_Treasury_Defloat;
            }
            set
            {
                if ((this._Collection_Treasury_Defloat != value))
                {
                    this.OnCollection_Treasury_DefloatChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Treasury_Defloat = value;
                    this.SendPropertyChanged("Collection_Treasury_Defloat");
                    this.OnCollection_Treasury_DefloatChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Defloat_Collection", DbType = "Bit")]
        public System.Nullable<bool> Collection_Defloat_Collection
        {
            get
            {
                return this._Collection_Defloat_Collection;
            }
            set
            {
                if ((this._Collection_Defloat_Collection != value))
                {
                    this.OnCollection_Defloat_CollectionChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Defloat_Collection = value;
                    this.SendPropertyChanged("Collection_Defloat_Collection");
                    this.OnCollection_Defloat_CollectionChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Coins_In", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Coins_In
        {
            get
            {
                return this._Previous_Meters_Coins_In;
            }
            set
            {
                if ((this._Previous_Meters_Coins_In != value))
                {
                    this.OnPrevious_Meters_Coins_InChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Coins_In = value;
                    this.SendPropertyChanged("Previous_Meters_Coins_In");
                    this.OnPrevious_Meters_Coins_InChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Coins_Out
        {
            get
            {
                return this._Previous_Meters_Coins_Out;
            }
            set
            {
                if ((this._Previous_Meters_Coins_Out != value))
                {
                    this.OnPrevious_Meters_Coins_OutChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Coins_Out = value;
                    this.SendPropertyChanged("Previous_Meters_Coins_Out");
                    this.OnPrevious_Meters_Coins_OutChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Coin_Drop", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Coin_Drop
        {
            get
            {
                return this._Previous_Meters_Coin_Drop;
            }
            set
            {
                if ((this._Previous_Meters_Coin_Drop != value))
                {
                    this.OnPrevious_Meters_Coin_DropChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Coin_Drop = value;
                    this.SendPropertyChanged("Previous_Meters_Coin_Drop");
                    this.OnPrevious_Meters_Coin_DropChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Handpay", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Handpay
        {
            get
            {
                return this._Previous_Meters_Handpay;
            }
            set
            {
                if ((this._Previous_Meters_Handpay != value))
                {
                    this.OnPrevious_Meters_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Handpay = value;
                    this.SendPropertyChanged("Previous_Meters_Handpay");
                    this.OnPrevious_Meters_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_External_Credit", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_External_Credit
        {
            get
            {
                return this._Previous_Meters_External_Credit;
            }
            set
            {
                if ((this._Previous_Meters_External_Credit != value))
                {
                    this.OnPrevious_Meters_External_CreditChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_External_Credit = value;
                    this.SendPropertyChanged("Previous_Meters_External_Credit");
                    this.OnPrevious_Meters_External_CreditChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Games_Bet
        {
            get
            {
                return this._Previous_Meters_Games_Bet;
            }
            set
            {
                if ((this._Previous_Meters_Games_Bet != value))
                {
                    this.OnPrevious_Meters_Games_BetChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Games_Bet = value;
                    this.SendPropertyChanged("Previous_Meters_Games_Bet");
                    this.OnPrevious_Meters_Games_BetChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Games_Won", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Games_Won
        {
            get
            {
                return this._Previous_Meters_Games_Won;
            }
            set
            {
                if ((this._Previous_Meters_Games_Won != value))
                {
                    this.OnPrevious_Meters_Games_WonChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Games_Won = value;
                    this.SendPropertyChanged("Previous_Meters_Games_Won");
                    this.OnPrevious_Meters_Games_WonChanged();
                }
            }
        }

        [Column(Storage = "_Previous_Meters_Notes", DbType = "Int")]
        public System.Nullable<int> Previous_Meters_Notes
        {
            get
            {
                return this._Previous_Meters_Notes;
            }
            set
            {
                if ((this._Previous_Meters_Notes != value))
                {
                    this.OnPrevious_Meters_NotesChanging(value);
                    this.SendPropertyChanging();
                    this._Previous_Meters_Notes = value;
                    this.SendPropertyChanged("Previous_Meters_Notes");
                    this.OnPrevious_Meters_NotesChanged();
                }
            }
        }

        [Column(Storage = "_Treasury_Handpay", DbType = "Real")]
        public System.Nullable<float> Treasury_Handpay
        {
            get
            {
                return this._Treasury_Handpay;
            }
            set
            {
                if ((this._Treasury_Handpay != value))
                {
                    this.OnTreasury_HandpayChanging(value);
                    this.SendPropertyChanging();
                    this._Treasury_Handpay = value;
                    this.SendPropertyChanged("Treasury_Handpay");
                    this.OnTreasury_HandpayChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Week_ID", DbType = "Int")]
        public System.Nullable<int> Operator_Week_ID
        {
            get
            {
                return this._Operator_Week_ID;
            }
            set
            {
                if ((this._Operator_Week_ID != value))
                {
                    this.OnOperator_Week_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Week_ID = value;
                    this.SendPropertyChanged("Operator_Week_ID");
                    this.OnOperator_Week_IDChanged();
                }
            }
        }

        [Column(Storage = "_Operator_Period_ID", DbType = "Int")]
        public System.Nullable<int> Operator_Period_ID
        {
            get
            {
                return this._Operator_Period_ID;
            }
            set
            {
                if ((this._Operator_Period_ID != value))
                {
                    this.OnOperator_Period_IDChanging(value);
                    this.SendPropertyChanging();
                    this._Operator_Period_ID = value;
                    this.SendPropertyChanged("Operator_Period_ID");
                    this.OnOperator_Period_IDChanged();
                }
            }
        }

        [Column(Storage = "_CollectionHandHeldMetersReceived", DbType = "Int")]
        public System.Nullable<int> CollectionHandHeldMetersReceived
        {
            get
            {
                return this._CollectionHandHeldMetersReceived;
            }
            set
            {
                if ((this._CollectionHandHeldMetersReceived != value))
                {
                    this.OnCollectionHandHeldMetersReceivedChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionHandHeldMetersReceived = value;
                    this.SendPropertyChanged("CollectionHandHeldMetersReceived");
                    this.OnCollectionHandHeldMetersReceivedChanged();
                }
            }
        }

        [Column(Storage = "_CollectionNoDoorEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoDoorEvents
        {
            get
            {
                return this._CollectionNoDoorEvents;
            }
            set
            {
                if ((this._CollectionNoDoorEvents != value))
                {
                    this.OnCollectionNoDoorEventsChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionNoDoorEvents = value;
                    this.SendPropertyChanged("CollectionNoDoorEvents");
                    this.OnCollectionNoDoorEventsChanged();
                }
            }
        }

        [Column(Storage = "_CollectionNoPowerEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoPowerEvents
        {
            get
            {
                return this._CollectionNoPowerEvents;
            }
            set
            {
                if ((this._CollectionNoPowerEvents != value))
                {
                    this.OnCollectionNoPowerEventsChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionNoPowerEvents = value;
                    this.SendPropertyChanged("CollectionNoPowerEvents");
                    this.OnCollectionNoPowerEventsChanged();
                }
            }
        }

        [Column(Storage = "_CollectionNoFaultEvents", DbType = "Int")]
        public System.Nullable<int> CollectionNoFaultEvents
        {
            get
            {
                return this._CollectionNoFaultEvents;
            }
            set
            {
                if ((this._CollectionNoFaultEvents != value))
                {
                    this.OnCollectionNoFaultEventsChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionNoFaultEvents = value;
                    this.SendPropertyChanged("CollectionNoFaultEvents");
                    this.OnCollectionNoFaultEventsChanged();
                }
            }
        }

        [Column(Storage = "_CollectionTotalDurationPower", DbType = "Int")]
        public System.Nullable<int> CollectionTotalDurationPower
        {
            get
            {
                return this._CollectionTotalDurationPower;
            }
            set
            {
                if ((this._CollectionTotalDurationPower != value))
                {
                    this.OnCollectionTotalDurationPowerChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionTotalDurationPower = value;
                    this.SendPropertyChanged("CollectionTotalDurationPower");
                    this.OnCollectionTotalDurationPowerChanged();
                }
            }
        }

        [Column(Storage = "_CollectionTotalDurationDoor", DbType = "Int")]
        public System.Nullable<int> CollectionTotalDurationDoor
        {
            get
            {
                return this._CollectionTotalDurationDoor;
            }
            set
            {
                if ((this._CollectionTotalDurationDoor != value))
                {
                    this.OnCollectionTotalDurationDoorChanging(value);
                    this.SendPropertyChanging();
                    this._CollectionTotalDurationDoor = value;
                    this.SendPropertyChanged("CollectionTotalDurationDoor");
                    this.OnCollectionTotalDurationDoorChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_VTP", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_VTP
        {
            get
            {
                return this._COLLECTION_RDC_VTP;
            }
            set
            {
                if ((this._COLLECTION_RDC_VTP != value))
                {
                    this.OnCOLLECTION_RDC_VTPChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_VTP = value;
                    this.SendPropertyChanged("COLLECTION_RDC_VTP");
                    this.OnCOLLECTION_RDC_VTPChanged();
                }
            }
        }

        [Column(Storage = "_Collection_NetEx", DbType = "Real")]
        public System.Nullable<float> Collection_NetEx
        {
            get
            {
                return this._Collection_NetEx;
            }
            set
            {
                if ((this._Collection_NetEx != value))
                {
                    this.OnCollection_NetExChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_NetEx = value;
                    this.SendPropertyChanged("Collection_NetEx");
                    this.OnCollection_NetExChanged();
                }
            }
        }

        [Column(Storage = "_Collection_VAT_Rate", DbType = "Real")]
        public System.Nullable<float> Collection_VAT_Rate
        {
            get
            {
                return this._Collection_VAT_Rate;
            }
            set
            {
                if ((this._Collection_VAT_Rate != value))
                {
                    this.OnCollection_VAT_RateChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_VAT_Rate = value;
                    this.SendPropertyChanged("Collection_VAT_Rate");
                    this.OnCollection_VAT_RateChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_COINS_IN", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_COINS_IN
        {
            get
            {
                return this._COLLECTION_RDC_COINS_IN;
            }
            set
            {
                if ((this._COLLECTION_RDC_COINS_IN != value))
                {
                    this.OnCOLLECTION_RDC_COINS_INChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_COINS_IN = value;
                    this.SendPropertyChanged("COLLECTION_RDC_COINS_IN");
                    this.OnCOLLECTION_RDC_COINS_INChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_COINS_OUT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_COINS_OUT
        {
            get
            {
                return this._COLLECTION_RDC_COINS_OUT;
            }
            set
            {
                if ((this._COLLECTION_RDC_COINS_OUT != value))
                {
                    this.OnCOLLECTION_RDC_COINS_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_COINS_OUT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_COINS_OUT");
                    this.OnCOLLECTION_RDC_COINS_OUTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_COIN_DROP", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_COIN_DROP
        {
            get
            {
                return this._COLLECTION_RDC_COIN_DROP;
            }
            set
            {
                if ((this._COLLECTION_RDC_COIN_DROP != value))
                {
                    this.OnCOLLECTION_RDC_COIN_DROPChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_COIN_DROP = value;
                    this.SendPropertyChanged("COLLECTION_RDC_COIN_DROP");
                    this.OnCOLLECTION_RDC_COIN_DROPChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_HANDPAY", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_HANDPAY
        {
            get
            {
                return this._COLLECTION_RDC_HANDPAY;
            }
            set
            {
                if ((this._COLLECTION_RDC_HANDPAY != value))
                {
                    this.OnCOLLECTION_RDC_HANDPAYChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_HANDPAY = value;
                    this.SendPropertyChanged("COLLECTION_RDC_HANDPAY");
                    this.OnCOLLECTION_RDC_HANDPAYChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_EXTERNAL_CREDIT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_EXTERNAL_CREDIT
        {
            get
            {
                return this._COLLECTION_RDC_EXTERNAL_CREDIT;
            }
            set
            {
                if ((this._COLLECTION_RDC_EXTERNAL_CREDIT != value))
                {
                    this.OnCOLLECTION_RDC_EXTERNAL_CREDITChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_EXTERNAL_CREDIT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_EXTERNAL_CREDIT");
                    this.OnCOLLECTION_RDC_EXTERNAL_CREDITChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_BET", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_BET
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_BET;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_BET != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_BETChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_BET = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_BET");
                    this.OnCOLLECTION_RDC_GAMES_BETChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_WON", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_WON
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_WON;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_WON != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_WONChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_WON = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_WON");
                    this.OnCOLLECTION_RDC_GAMES_WONChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_NOTES", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_NOTES
        {
            get
            {
                return this._COLLECTION_RDC_NOTES;
            }
            set
            {
                if ((this._COLLECTION_RDC_NOTES != value))
                {
                    this.OnCOLLECTION_RDC_NOTESChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_NOTES = value;
                    this.SendPropertyChanged("COLLECTION_RDC_NOTES");
                    this.OnCOLLECTION_RDC_NOTESChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_CANCELLED_CREDITS", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_CANCELLED_CREDITS
        {
            get
            {
                return this._COLLECTION_RDC_CANCELLED_CREDITS;
            }
            set
            {
                if ((this._COLLECTION_RDC_CANCELLED_CREDITS != value))
                {
                    this.OnCOLLECTION_RDC_CANCELLED_CREDITSChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_CANCELLED_CREDITS = value;
                    this.SendPropertyChanged("COLLECTION_RDC_CANCELLED_CREDITS");
                    this.OnCOLLECTION_RDC_CANCELLED_CREDITSChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_LOST", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_LOST
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_LOST;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_LOST != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_LOSTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_LOST = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_LOST");
                    this.OnCOLLECTION_RDC_GAMES_LOSTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_GAMES_SINCE_POWER_UP", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_GAMES_SINCE_POWER_UP
        {
            get
            {
                return this._COLLECTION_RDC_GAMES_SINCE_POWER_UP;
            }
            set
            {
                if ((this._COLLECTION_RDC_GAMES_SINCE_POWER_UP != value))
                {
                    this.OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_GAMES_SINCE_POWER_UP = value;
                    this.SendPropertyChanged("COLLECTION_RDC_GAMES_SINCE_POWER_UP");
                    this.OnCOLLECTION_RDC_GAMES_SINCE_POWER_UPChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TRUE_COIN_IN", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_TRUE_COIN_IN
        {
            get
            {
                return this._COLLECTION_RDC_TRUE_COIN_IN;
            }
            set
            {
                if ((this._COLLECTION_RDC_TRUE_COIN_IN != value))
                {
                    this.OnCOLLECTION_RDC_TRUE_COIN_INChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TRUE_COIN_IN = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TRUE_COIN_IN");
                    this.OnCOLLECTION_RDC_TRUE_COIN_INChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TRUE_COIN_OUT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_TRUE_COIN_OUT
        {
            get
            {
                return this._COLLECTION_RDC_TRUE_COIN_OUT;
            }
            set
            {
                if ((this._COLLECTION_RDC_TRUE_COIN_OUT != value))
                {
                    this.OnCOLLECTION_RDC_TRUE_COIN_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TRUE_COIN_OUT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TRUE_COIN_OUT");
                    this.OnCOLLECTION_RDC_TRUE_COIN_OUTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_CURRENT_CREDITS", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_CURRENT_CREDITS
        {
            get
            {
                return this._COLLECTION_RDC_CURRENT_CREDITS;
            }
            set
            {
                if ((this._COLLECTION_RDC_CURRENT_CREDITS != value))
                {
                    this.OnCOLLECTION_RDC_CURRENT_CREDITSChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_CURRENT_CREDITS = value;
                    this.SendPropertyChanged("COLLECTION_RDC_CURRENT_CREDITS");
                    this.OnCOLLECTION_RDC_CURRENT_CREDITSChanged();
                }
            }
        }

        [Column(Storage = "_Collection_PoP_Actual", DbType = "Int")]
        public System.Nullable<int> Collection_PoP_Actual
        {
            get
            {
                return this._Collection_PoP_Actual;
            }
            set
            {
                if ((this._Collection_PoP_Actual != value))
                {
                    this.OnCollection_PoP_ActualChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_PoP_Actual = value;
                    this.SendPropertyChanged("Collection_PoP_Actual");
                    this.OnCollection_PoP_ActualChanged();
                }
            }
        }

        [Column(Storage = "_Collection_PoP_Configured", DbType = "Int")]
        public System.Nullable<int> Collection_PoP_Configured
        {
            get
            {
                return this._Collection_PoP_Configured;
            }
            set
            {
                if ((this._Collection_PoP_Configured != value))
                {
                    this.OnCollection_PoP_ConfiguredChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_PoP_Configured = value;
                    this.SendPropertyChanged("Collection_PoP_Configured");
                    this.OnCollection_PoP_ConfiguredChanged();
                }
            }
        }

        [Column(Storage = "_Collection_EDC_Status", DbType = "Int")]
        public System.Nullable<int> Collection_EDC_Status
        {
            get
            {
                return this._Collection_EDC_Status;
            }
            set
            {
                if ((this._Collection_EDC_Status != value))
                {
                    this.OnCollection_EDC_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_EDC_Status = value;
                    this.SendPropertyChanged("Collection_EDC_Status");
                    this.OnCollection_EDC_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Meter_Status", DbType = "Int")]
        public System.Nullable<int> Collection_Meter_Status
        {
            get
            {
                return this._Collection_Meter_Status;
            }
            set
            {
                if ((this._Collection_Meter_Status != value))
                {
                    this.OnCollection_Meter_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Meter_Status = value;
                    this.SendPropertyChanged("Collection_Meter_Status");
                    this.OnCollection_Meter_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Cash_Status", DbType = "Int")]
        public System.Nullable<int> Collection_Cash_Status
        {
            get
            {
                return this._Collection_Cash_Status;
            }
            set
            {
                if ((this._Collection_Cash_Status != value))
                {
                    this.OnCollection_Cash_StatusChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Cash_Status = value;
                    this.SendPropertyChanged("Collection_Cash_Status");
                    this.OnCollection_Cash_StatusChanged();
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this.OnInstallation_NoChanging(value);
                    this.SendPropertyChanging();
                    this._Installation_No = value;
                    this.SendPropertyChanged("Installation_No");
                    this.OnInstallation_NoChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Date
        {
            get
            {
                return this._Collection_Date;
            }
            set
            {
                if ((this._Collection_Date != value))
                {
                    this.OnCollection_DateChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Date = value;
                    this.SendPropertyChanged("Collection_Date");
                    this.OnCollection_DateChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_1p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_1p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_1p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_1p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_1pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_1p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_1p");
                    this.OnCASH_FLOAT_CHANGE_1pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_2p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_2p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_2p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_2p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_2pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_2p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_2p");
                    this.OnCASH_FLOAT_CHANGE_2pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_5p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_5p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_5p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_5p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_5pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_5p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_5p");
                    this.OnCASH_FLOAT_CHANGE_5pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_10p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_10p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_10p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_10p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_10pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_10p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_10p");
                    this.OnCASH_FLOAT_CHANGE_10pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_20p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_20p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_20p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_20p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_20pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_20p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_20p");
                    this.OnCASH_FLOAT_CHANGE_20pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_50p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_50p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_50p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_50p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_50pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_50p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_50p");
                    this.OnCASH_FLOAT_CHANGE_50pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_100p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_100p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_100p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_100p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_100pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_100p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_100p");
                    this.OnCASH_FLOAT_CHANGE_100pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_200p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_200p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_200p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_200p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_200pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_200p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_200p");
                    this.OnCASH_FLOAT_CHANGE_200pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_500p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_500p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_500p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_500p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_500pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_500p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_500p");
                    this.OnCASH_FLOAT_CHANGE_500pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_CHANGE_1000p", DbType = "Int NOT NULL")]
        public int CASH_FLOAT_CHANGE_1000p
        {
            get
            {
                return this._CASH_FLOAT_CHANGE_1000p;
            }
            set
            {
                if ((this._CASH_FLOAT_CHANGE_1000p != value))
                {
                    this.OnCASH_FLOAT_CHANGE_1000pChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_CHANGE_1000p = value;
                    this.SendPropertyChanged("CASH_FLOAT_CHANGE_1000p");
                    this.OnCASH_FLOAT_CHANGE_1000pChanged();
                }
            }
        }

        [Column(Storage = "_CASH_FLOAT_TOTAL", DbType = "Real NOT NULL")]
        public float CASH_FLOAT_TOTAL
        {
            get
            {
                return this._CASH_FLOAT_TOTAL;
            }
            set
            {
                if ((this._CASH_FLOAT_TOTAL != value))
                {
                    this.OnCASH_FLOAT_TOTALChanging(value);
                    this.SendPropertyChanging();
                    this._CASH_FLOAT_TOTAL = value;
                    this.SendPropertyChanged("CASH_FLOAT_TOTAL");
                    this.OnCASH_FLOAT_TOTALChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketQty", DbType = "Int")]
        public System.Nullable<int> DeclaredTicketQty
        {
            get
            {
                return this._DeclaredTicketQty;
            }
            set
            {
                if ((this._DeclaredTicketQty != value))
                {
                    this.OnDeclaredTicketQtyChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketQty = value;
                    this.SendPropertyChanged("DeclaredTicketQty");
                    this.OnDeclaredTicketQtyChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketValue", DbType = "Real")]
        public System.Nullable<float> DeclaredTicketValue
        {
            get
            {
                return this._DeclaredTicketValue;
            }
            set
            {
                if ((this._DeclaredTicketValue != value))
                {
                    this.OnDeclaredTicketValueChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketValue = value;
                    this.SendPropertyChanged("DeclaredTicketValue");
                    this.OnDeclaredTicketValueChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_JACKPOT", DbType = "Int")]
        public System.Nullable<int> COLLECTION_RDC_JACKPOT
        {
            get
            {
                return this._COLLECTION_RDC_JACKPOT;
            }
            set
            {
                if ((this._COLLECTION_RDC_JACKPOT != value))
                {
                    this.OnCOLLECTION_RDC_JACKPOTChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_JACKPOT = value;
                    this.SendPropertyChanged("COLLECTION_RDC_JACKPOT");
                    this.OnCOLLECTION_RDC_JACKPOTChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TICKETS_INSERTED_VALUE", DbType = "Int NOT NULL")]
        public int COLLECTION_RDC_TICKETS_INSERTED_VALUE
        {
            get
            {
                return this._COLLECTION_RDC_TICKETS_INSERTED_VALUE;
            }
            set
            {
                if ((this._COLLECTION_RDC_TICKETS_INSERTED_VALUE != value))
                {
                    this.OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TICKETS_INSERTED_VALUE = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TICKETS_INSERTED_VALUE");
                    this.OnCOLLECTION_RDC_TICKETS_INSERTED_VALUEChanged();
                }
            }
        }

        [Column(Storage = "_COLLECTION_RDC_TICKETS_PRINTED_VALUE", DbType = "Int NOT NULL")]
        public int COLLECTION_RDC_TICKETS_PRINTED_VALUE
        {
            get
            {
                return this._COLLECTION_RDC_TICKETS_PRINTED_VALUE;
            }
            set
            {
                if ((this._COLLECTION_RDC_TICKETS_PRINTED_VALUE != value))
                {
                    this.OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanging(value);
                    this.SendPropertyChanging();
                    this._COLLECTION_RDC_TICKETS_PRINTED_VALUE = value;
                    this.SendPropertyChanged("COLLECTION_RDC_TICKETS_PRINTED_VALUE");
                    this.OnCOLLECTION_RDC_TICKETS_PRINTED_VALUEChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketPrintedQty", DbType = "Int")]
        public System.Nullable<int> DeclaredTicketPrintedQty
        {
            get
            {
                return this._DeclaredTicketPrintedQty;
            }
            set
            {
                if ((this._DeclaredTicketPrintedQty != value))
                {
                    this.OnDeclaredTicketPrintedQtyChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketPrintedQty = value;
                    this.SendPropertyChanged("DeclaredTicketPrintedQty");
                    this.OnDeclaredTicketPrintedQtyChanged();
                }
            }
        }

        [Column(Storage = "_DeclaredTicketPrintedValue", DbType = "Real")]
        public System.Nullable<float> DeclaredTicketPrintedValue
        {
            get
            {
                return this._DeclaredTicketPrintedValue;
            }
            set
            {
                if ((this._DeclaredTicketPrintedValue != value))
                {
                    this.OnDeclaredTicketPrintedValueChanging(value);
                    this.SendPropertyChanging();
                    this._DeclaredTicketPrintedValue = value;
                    this.SendPropertyChanged("DeclaredTicketPrintedValue");
                    this.OnDeclaredTicketPrintedValueChanged();
                }
            }
        }

        [Column(Storage = "_Collection_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Date_Performed
        {
            get
            {
                return this._Collection_Date_Performed;
            }
            set
            {
                if ((this._Collection_Date_Performed != value))
                {
                    this.OnCollection_Date_PerformedChanging(value);
                    this.SendPropertyChanging();
                    this._Collection_Date_Performed = value;
                    this.SendPropertyChanged("Collection_Date_Performed");
                    this.OnCollection_Date_PerformedChanged();
                }
            }
        }

        [Column(Storage = "_progressive_win_value", DbType = "Int")]
        public System.Nullable<int> progressive_win_value
        {
            get
            {
                return this._progressive_win_value;
            }
            set
            {
                if ((this._progressive_win_value != value))
                {
                    this.Onprogressive_win_valueChanging(value);
                    this.SendPropertyChanging();
                    this._progressive_win_value = value;
                    this.SendPropertyChanged("progressive_win_value");
                    this.Onprogressive_win_valueChanged();
                }
            }
        }

        [Column(Storage = "_progressive_win_Handpay_value", DbType = "Int")]
        public System.Nullable<int> progressive_win_Handpay_value
        {
            get
            {
                return this._progressive_win_Handpay_value;
            }
            set
            {
                if ((this._progressive_win_Handpay_value != value))
                {
                    this.Onprogressive_win_Handpay_valueChanging(value);
                    this.SendPropertyChanging();
                    this._progressive_win_Handpay_value = value;
                    this.SendPropertyChanged("progressive_win_Handpay_value");
                    this.Onprogressive_win_Handpay_valueChanged();
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
                    this.OnProgressive_Value_DeclaredChanging(value);
                    this.SendPropertyChanging();
                    this._Progressive_Value_Declared = value;
                    this.SendPropertyChanged("Progressive_Value_Declared");
                    this.OnProgressive_Value_DeclaredChanged();
                }
            }
        }

        [Column(Storage = "_Mystery_Machine_Paid", DbType = "Int")]
        public System.Nullable<int> Mystery_Machine_Paid
        {
            get
            {
                return this._Mystery_Machine_Paid;
            }
            set
            {
                if ((this._Mystery_Machine_Paid != value))
                {
                    this.OnMystery_Machine_PaidChanging(value);
                    this.SendPropertyChanging();
                    this._Mystery_Machine_Paid = value;
                    this.SendPropertyChanged("Mystery_Machine_Paid");
                    this.OnMystery_Machine_PaidChanged();
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
                    this.OnMystery_Attendant_PaidChanging(value);
                    this.SendPropertyChanging();
                    this._Mystery_Attendant_Paid = value;
                    this.SendPropertyChanged("Mystery_Attendant_Paid");
                    this.OnMystery_Attendant_PaidChanged();
                }
            }
        }
        [Column(Storage = "_RDC_TICKETS_INSERTED_NONCASHABLE_VALUE", DbType = "Int")]
        public System.Nullable<int> RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
        {
            get
            {
                return this._RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;
            }
            set
            {
                if ((this._RDC_TICKETS_INSERTED_NONCASHABLE_VALUE != value))
                {
                    this.OnRDC_TICKETS_INSERTED_NONCASHABLE_VALUEChanging(value);
                    this.SendPropertyChanging();
                    this._RDC_TICKETS_INSERTED_NONCASHABLE_VALUE = value;
                    this.SendPropertyChanged("RDC_TICKETS_INSERTED_NONCASHABLE_VALUE");
                    this.OnRDC_TICKETS_INSERTED_NONCASHABLE_VALUEChanged();
                }
            }
        }
        [Column(Storage = "_RDC_TICKETS_PRINTED_NONCASHABLE_VALUE", DbType = "Int")]
        public System.Nullable<int> RDC_TICKETS_PRINTED_NONCASHABLE_VALUE
        {
            get
            {
                return this._RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;
            }
            set
            {
                if ((this._RDC_TICKETS_PRINTED_NONCASHABLE_VALUE != value))
                {
                    this.OnRDC_TICKETS_PRINTED_NONCASHABLE_VALUEChanging(value);
                    this.SendPropertyChanging();
                    this._RDC_TICKETS_PRINTED_NONCASHABLE_VALUE = value;
                    this.SendPropertyChanged("RDC_TICKETS_PRINTED_NONCASHABLE_VALUE");
                    this.OnRDC_TICKETS_PRINTED_NONCASHABLE_VALUEChanged();
                }
            }
        }
        [Column(Storage = "_Promo_Cashable_EFT_IN", DbType = "Int")]
        public System.Nullable<int> Promo_Cashable_EFT_IN
        {
            get
            {
                return this._Promo_Cashable_EFT_IN;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_IN != value))
                {
                    this.OnPromo_Cashable_EFT_INChanging(value);
                    this.SendPropertyChanging();
                    this._Promo_Cashable_EFT_IN = value;
                    this.SendPropertyChanged("Promo_Cashable_EFT_IN");
                    this.OnPromo_Cashable_EFT_INChanged();
                }
            }
        }
        [Column(Storage = "_Promo_Cashable_EFT_OUT", DbType = "Int")]
        public System.Nullable<int> Promo_Cashable_EFT_OUT
        {
            get
            {
                return this._Promo_Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_OUT != value))
                {
                    this.OnPromo_Cashable_EFT_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._Promo_Cashable_EFT_OUT = value;
                    this.SendPropertyChanged("Promo_Cashable_EFT_OUT");
                    this.OnPromo_Cashable_EFT_OUTChanged();
                }
            }
        }
        [Column(Storage = "_NonCashable_EFT_IN", DbType = "Int")]
        public System.Nullable<int> NonCashable_EFT_IN
        {
            get
            {
                return this._NonCashable_EFT_IN;
            }
            set
            {
                if ((this._NonCashable_EFT_IN != value))
                {
                    this.OnNonCashable_EFT_INChanging(value);
                    this.SendPropertyChanging();
                    this._NonCashable_EFT_IN = value;
                    this.SendPropertyChanged("NonCashable_EFT_IN");
                    this.OnNonCashable_EFT_INChanged();
                }
            }
        }
        [Column(Storage = "_NonCashable_EFT_OUT", DbType = "Int")]
        public System.Nullable<int> NonCashable_EFT_OUT
        {
            get
            {
                return this._NonCashable_EFT_OUT;
            }
            set
            {
                if ((this._NonCashable_EFT_OUT != value))
                {
                    this.OnNonCashable_EFT_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._NonCashable_EFT_OUT = value;
                    this.SendPropertyChanged("NonCashable_EFT_OUT");
                    this.OnNonCashable_EFT_OUTChanged();
                }
            }
        }
        [Column(Storage = "_Cashable_EFT_IN", DbType = "Int")]
        public System.Nullable<int> Cashable_EFT_IN
        {
            get
            {
                return this._Cashable_EFT_IN;
            }
            set
            {
                if ((this._Cashable_EFT_IN != value))
                {
                    this.OnCashable_EFT_INChanging(value);
                    this.SendPropertyChanging();
                    this._Cashable_EFT_IN = value;
                    this.SendPropertyChanged("Cashable_EFT_IN");
                    this.OnCashable_EFT_INChanged();
                }
            }
        }
        [Column(Storage = "_Cashable_EFT_OUT", DbType = "Int")]
        public System.Nullable<int> Cashable_EFT_OUT
        {
            get
            {
                return this._Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Cashable_EFT_OUT != value))
                {
                    this.OnCashable_EFT_OUTChanging(value);
                    this.SendPropertyChanging();
                    this._Cashable_EFT_OUT = value;
                    this.SendPropertyChanged("Cashable_EFT_OUT");
                    this.OnCashable_EFT_OUTChanged();
                }
            }
        }


        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public partial class TreasuryUser
    {

        private int _Treasury_No;

        private System.Nullable<int> _Collection_No;

        private System.Nullable<int> _Installation_No;

        private System.Nullable<int> _HQ_ID;

        private System.Nullable<int> _User_No;

        private System.Nullable<System.DateTime> _Treasury_Date;

        private System.Nullable<double> _Treasury_Amount;

        private string _Treasury_Reason;

        private System.Nullable<bool> _Treasury_Allocated;

        private string _Treasury_Type;

        private System.Nullable<bool> _Treasury_Temp;

        private string _Treasury_Docket_No;

        private System.Nullable<float> _Treasury_Breakdown_2000p;

        private System.Nullable<float> _Treasury_Breakdown_1000p;

        private System.Nullable<float> _Treasury_Breakdown_500p;

        private System.Nullable<float> _Treasury_Breakdown_200p;

        private System.Nullable<float> _Treasury_Breakdown_100p;

        private System.Nullable<float> _Treasury_Breakdown_50p;

        private System.Nullable<float> _Treasury_Breakdown_20p;

        private System.Nullable<float> _Treasury_Breakdown_10p;

        private System.Nullable<float> _Treasury_Breakdown_5p;

        private System.Nullable<float> _Treasury_Breakdown_2p;

        private System.Nullable<int> _Treasury_Float_Issued_By;

        private System.Nullable<float> _Treasury_Float_Recovered_Total;

        private System.Nullable<float> _Treasury_Float_Recovered_2000p;

        private System.Nullable<float> _Treasury_Float_Recovered_1000p;

        private System.Nullable<float> _Treasury_Float_Recovered_500p;

        private System.Nullable<float> _Treasury_Float_Recovered_200p;

        private System.Nullable<float> _Treasury_Float_Recovered_100p;

        private System.Nullable<float> _Treasury_Float_Recovered_50p;

        private System.Nullable<float> _Treasury_Float_Recovered_20p;

        private System.Nullable<float> _Treasury_Float_Recovered_10p;

        private System.Nullable<float> _Treasury_Float_Recovered_5p;

        private System.Nullable<float> _Treasury_Float_Recovered_2p;

        private System.Nullable<int> _Treasury_Issuer_User_No;

        private string _Treasury_Membership_No;

        private System.Nullable<int> _Treasury_Reason_Code;

        private System.Nullable<System.DateTime> _Treasury_Actual_Date;

        private string _User_Name;

        private string _User_ID;

        private string _User_Name1;
        

        public TreasuryUser()
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

        [Column(Storage = "_Collection_No", DbType = "Int")]
        public System.Nullable<int> Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_HQ_ID", DbType = "Int")]
        public System.Nullable<int> HQ_ID
        {
            get
            {
                return this._HQ_ID;
            }
            set
            {
                if ((this._HQ_ID != value))
                {
                    this._HQ_ID = value;
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

        [Column(Storage = "_Treasury_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Treasury_Date
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

        public string Treasury_Time
        {
            get
            {
                return Convert.ToDateTime(this._Treasury_Date).GetUniversalTimeFormat();
            }
            set { }
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

        [Column(Storage = "_Treasury_Allocated", DbType = "Bit")]
        public System.Nullable<bool> Treasury_Allocated
        {
            get
            {
                return this._Treasury_Allocated;
            }
            set
            {
                if ((this._Treasury_Allocated != value))
                {
                    this._Treasury_Allocated = value;
                }
            }
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

        [Column(Storage = "_Treasury_Temp", DbType = "Bit")]
        public System.Nullable<bool> Treasury_Temp
        {
            get
            {
                return this._Treasury_Temp;
            }
            set
            {
                if ((this._Treasury_Temp != value))
                {
                    this._Treasury_Temp = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Docket_No", DbType = "VarChar(50)")]
        public string Treasury_Docket_No
        {
            get
            {
                return this._Treasury_Docket_No;
            }
            set
            {
                if ((this._Treasury_Docket_No != value))
                {
                    this._Treasury_Docket_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_2000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_2000p
        {
            get
            {
                return this._Treasury_Breakdown_2000p;
            }
            set
            {
                if ((this._Treasury_Breakdown_2000p != value))
                {
                    this._Treasury_Breakdown_2000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_1000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_1000p
        {
            get
            {
                return this._Treasury_Breakdown_1000p;
            }
            set
            {
                if ((this._Treasury_Breakdown_1000p != value))
                {
                    this._Treasury_Breakdown_1000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_500p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_500p
        {
            get
            {
                return this._Treasury_Breakdown_500p;
            }
            set
            {
                if ((this._Treasury_Breakdown_500p != value))
                {
                    this._Treasury_Breakdown_500p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_200p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_200p
        {
            get
            {
                return this._Treasury_Breakdown_200p;
            }
            set
            {
                if ((this._Treasury_Breakdown_200p != value))
                {
                    this._Treasury_Breakdown_200p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_100p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_100p
        {
            get
            {
                return this._Treasury_Breakdown_100p;
            }
            set
            {
                if ((this._Treasury_Breakdown_100p != value))
                {
                    this._Treasury_Breakdown_100p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_50p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_50p
        {
            get
            {
                return this._Treasury_Breakdown_50p;
            }
            set
            {
                if ((this._Treasury_Breakdown_50p != value))
                {
                    this._Treasury_Breakdown_50p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_20p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_20p
        {
            get
            {
                return this._Treasury_Breakdown_20p;
            }
            set
            {
                if ((this._Treasury_Breakdown_20p != value))
                {
                    this._Treasury_Breakdown_20p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_10p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_10p
        {
            get
            {
                return this._Treasury_Breakdown_10p;
            }
            set
            {
                if ((this._Treasury_Breakdown_10p != value))
                {
                    this._Treasury_Breakdown_10p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_5p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_5p
        {
            get
            {
                return this._Treasury_Breakdown_5p;
            }
            set
            {
                if ((this._Treasury_Breakdown_5p != value))
                {
                    this._Treasury_Breakdown_5p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_2p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_2p
        {
            get
            {
                return this._Treasury_Breakdown_2p;
            }
            set
            {
                if ((this._Treasury_Breakdown_2p != value))
                {
                    this._Treasury_Breakdown_2p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Issued_By", DbType = "Int")]
        public System.Nullable<int> Treasury_Float_Issued_By
        {
            get
            {
                return this._Treasury_Float_Issued_By;
            }
            set
            {
                if ((this._Treasury_Float_Issued_By != value))
                {
                    this._Treasury_Float_Issued_By = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_Total", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_Total
        {
            get
            {
                return this._Treasury_Float_Recovered_Total;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_Total != value))
                {
                    this._Treasury_Float_Recovered_Total = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_2000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_2000p
        {
            get
            {
                return this._Treasury_Float_Recovered_2000p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_2000p != value))
                {
                    this._Treasury_Float_Recovered_2000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_1000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_1000p
        {
            get
            {
                return this._Treasury_Float_Recovered_1000p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_1000p != value))
                {
                    this._Treasury_Float_Recovered_1000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_500p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_500p
        {
            get
            {
                return this._Treasury_Float_Recovered_500p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_500p != value))
                {
                    this._Treasury_Float_Recovered_500p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_200p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_200p
        {
            get
            {
                return this._Treasury_Float_Recovered_200p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_200p != value))
                {
                    this._Treasury_Float_Recovered_200p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_100p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_100p
        {
            get
            {
                return this._Treasury_Float_Recovered_100p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_100p != value))
                {
                    this._Treasury_Float_Recovered_100p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_50p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_50p
        {
            get
            {
                return this._Treasury_Float_Recovered_50p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_50p != value))
                {
                    this._Treasury_Float_Recovered_50p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_20p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_20p
        {
            get
            {
                return this._Treasury_Float_Recovered_20p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_20p != value))
                {
                    this._Treasury_Float_Recovered_20p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_10p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_10p
        {
            get
            {
                return this._Treasury_Float_Recovered_10p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_10p != value))
                {
                    this._Treasury_Float_Recovered_10p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_5p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_5p
        {
            get
            {
                return this._Treasury_Float_Recovered_5p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_5p != value))
                {
                    this._Treasury_Float_Recovered_5p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_2p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_2p
        {
            get
            {
                return this._Treasury_Float_Recovered_2p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_2p != value))
                {
                    this._Treasury_Float_Recovered_2p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Issuer_User_No", DbType = "Int")]
        public System.Nullable<int> Treasury_Issuer_User_No
        {
            get
            {
                return this._Treasury_Issuer_User_No;
            }
            set
            {
                if ((this._Treasury_Issuer_User_No != value))
                {
                    this._Treasury_Issuer_User_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Membership_No", DbType = "VarChar(50)")]
        public string Treasury_Membership_No
        {
            get
            {
                return this._Treasury_Membership_No;
            }
            set
            {
                if ((this._Treasury_Membership_No != value))
                {
                    this._Treasury_Membership_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Reason_Code", DbType = "Int")]
        public System.Nullable<int> Treasury_Reason_Code
        {
            get
            {
                return this._Treasury_Reason_Code;
            }
            set
            {
                if ((this._Treasury_Reason_Code != value))
                {
                    this._Treasury_Reason_Code = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Actual_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Treasury_Actual_Date
        {
            get
            {
                return this._Treasury_Actual_Date;
            }
            set
            {
                if ((this._Treasury_Actual_Date != value))
                {
                    this._Treasury_Actual_Date = value;
                }
            }
        }

        [Column(Storage = "_User_Name", DbType = "VarChar(200)")]
        public string User_Name
        {
            get
            {
                return this._User_Name;
            }
            set
            {
                if ((this._User_Name != value))
                {
                    this._User_Name = value;
                }
            }
        }

        [Column(Storage = "_User_ID", DbType = "VarChar(50)")]
        public string User_ID
        {
            get
            {
                return this._User_ID;
            }
            set
            {
                if ((this._User_ID != value))
                {
                    this._User_ID = value;
                }
            }
        }

        [Column(Storage = "_User_Name1", DbType = "VarChar(200)")]
        public string User_Name1
        {
            get
            {
                return this._User_Name1;
            }
            set
            {
                if ((this._User_Name1 != value))
                {
                    this._User_Name1 = value;
                }
            }
        }
    }

    public partial class DoorEventRecord
    {

        private int _Event_No;

        private System.Nullable<int> _Event_Type;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<bool> _Event_Last_Collect;

        private System.Nullable<int> _Event_Detail;

        private string _Key_Owner;

        private System.Nullable<double> _Duration;

        private string _Door_Event_Type;

        private string _Error_Code_Description;

        public DoorEventRecord()
        {
        }

        [Column(Storage = "_Event_No", DbType = "Int NOT NULL")]
        public int Event_No
        {
            get
            {
                return this._Event_No;
            }
            set
            {
                if ((this._Event_No != value))
                {
                    this._Event_No = value;
                }
            }
        }

        [Column(Storage = "_Event_Type", DbType = "Int")]
        public System.Nullable<int> Event_Type
        {
            get
            {
                return this._Event_Type;
            }
            set
            {
                if ((this._Event_Type != value))
                {
                    this._Event_Type = value;
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

        [Column(Storage = "_Event_Last_Collect", DbType = "Bit")]
        public System.Nullable<bool> Event_Last_Collect
        {
            get
            {
                return this._Event_Last_Collect;
            }
            set
            {
                if ((this._Event_Last_Collect != value))
                {
                    this._Event_Last_Collect = value;
                }
            }
        }

        [Column(Storage = "_Event_Detail", DbType = "Int")]
        public System.Nullable<int> Event_Detail
        {
            get
            {
                return this._Event_Detail;
            }
            set
            {
                if ((this._Event_Detail != value))
                {
                    this._Event_Detail = value;
                }
            }
        }

        [Column(Storage = "_Key_Owner", DbType = "VarChar(50)")]
        public string Key_Owner
        {
            get
            {
                return this._Key_Owner;
            }
            set
            {
                if ((this._Key_Owner != value))
                {
                    this._Key_Owner = value;
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

        [Column(Storage = "_Error_Code_Description", DbType = "VarChar(50)")]
        public string Error_Code_Description
        {
            get
            {
                return this._Error_Code_Description;
            }
            set
            {
                if ((this._Error_Code_Description != value))
                {
                    this._Error_Code_Description = value;
                }
            }
        }

        [Column(Storage = "_Door_Event_Type", DbType = "VarChar(50)")]
        public string Door_Event_Type
        {
            get
            {
                return this._Door_Event_Type;
            }
            set
            {
                if ((this._Door_Event_Type != value))
                {
                    this._Door_Event_Type = value;
                }
            }
        }
    }

    public partial class FaultEventRecord
    {

        private int _Event_No;

        private System.Nullable<int> _Event_Type;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<bool> _Event_Last_Collect;

        private System.Nullable<int> _Event_Detail;

        private string _Fault_Description;

        public FaultEventRecord()
        {
        }

        [Column(Storage = "_Event_No", DbType = "Int NOT NULL")]
        public int Event_No
        {
            get
            {
                return this._Event_No;
            }
            set
            {
                if ((this._Event_No != value))
                {
                    this._Event_No = value;
                }
            }
        }

        [Column(Storage = "_Event_Type", DbType = "Int")]
        public System.Nullable<int> Event_Type
        {
            get
            {
                return this._Event_Type;
            }
            set
            {
                if ((this._Event_Type != value))
                {
                    this._Event_Type = value;
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

        [Column(Storage = "_Event_Last_Collect", DbType = "Bit")]
        public System.Nullable<bool> Event_Last_Collect
        {
            get
            {
                return this._Event_Last_Collect;
            }
            set
            {
                if ((this._Event_Last_Collect != value))
                {
                    this._Event_Last_Collect = value;
                }
            }
        }

        [Column(Storage = "_Event_Detail", DbType = "Int")]
        public System.Nullable<int> Event_Detail
        {
            get
            {
                return this._Event_Detail;
            }
            set
            {
                if ((this._Event_Detail != value))
                {
                    this._Event_Detail = value;
                }
            }
        }

        [Column(Storage = "_Fault_Description", DbType = "VarChar(50)")]
        public string Fault_Description
        {
            get
            {
                return this._Fault_Description;
            }
            set
            {
                if ((this._Fault_Description != value))
                {
                    this._Fault_Description = value;
                }
            }
        }
    }

    public partial class PowerEventRecord
    {

        private int _Event_No;

        private System.Nullable<int> _Event_Type;

        private System.Nullable<System.DateTime> _Date;

        private System.Nullable<bool> _Event_Last_Collect;

        private System.Nullable<int> _Event_Detail;

        private System.Nullable<double> _Duration;

        public PowerEventRecord()
        {
        }

        [Column(Storage = "_Event_No", DbType = "Int NOT NULL")]
        public int Event_No
        {
            get
            {
                return this._Event_No;
            }
            set
            {
                if ((this._Event_No != value))
                {
                    this._Event_No = value;
                }
            }
        }

        [Column(Storage = "_Event_Type", DbType = "Int")]
        public System.Nullable<int> Event_Type
        {
            get
            {
                return this._Event_Type;
            }
            set
            {
                if ((this._Event_Type != value))
                {
                    this._Event_Type = value;
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

        [Column(Storage = "_Event_Last_Collect", DbType = "Bit")]
        public System.Nullable<bool> Event_Last_Collect
        {
            get
            {
                return this._Event_Last_Collect;
            }
            set
            {
                if ((this._Event_Last_Collect != value))
                {
                    this._Event_Last_Collect = value;
                }
            }
        }

        [Column(Storage = "_Event_Detail", DbType = "Int")]
        public System.Nullable<int> Event_Detail
        {
            get
            {
                return this._Event_Detail;
            }
            set
            {
                if ((this._Event_Detail != value))
                {
                    this._Event_Detail = value;
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
    }

    public partial class PartCollectionUser
    {

        private System.Nullable<System.DateTime> _Part_Collection_Date;
        private System.Nullable<System.DateTime> _Part_Collection_Date_Performed;
        private string _User_Name;

        private System.Nullable<float> _Part_Collection_CashCollected;

        private string _Description;

        private string _Part_Collection_Time;

        private string _Part_Collection_DateOnly;

        public string Part_Collection_Time
        {
            get { return _Part_Collection_Time; }
            set { _Part_Collection_Time = value; }
        }

        public string Part_Collection_DateOnly
        {
            get { return _Part_Collection_DateOnly; }
            set { _Part_Collection_DateOnly = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [Column(Storage = "_Part_Collection_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Part_Collection_Date_Performed
        {
            get { return _Part_Collection_Date_Performed; }
            set { _Part_Collection_Date_Performed = value; }
        }


        public PartCollectionUser()
        {
        }

        [Column(Storage = "_Part_Collection_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Part_Collection_Date
        {
            get
            {
                return this._Part_Collection_Date;
            }
            set
            {
                if ((this._Part_Collection_Date != value))
                {
                    this._Part_Collection_Date = value;
                }
            }
        }

        [Column(Storage = "_User_Name", DbType = "VarChar(50)")]
        public string User_Name
        {
            get
            {
                return this._User_Name;
            }
            set
            {
                if ((this._User_Name != value))
                {
                    this._User_Name = value;
                }
            }
        }

        [Column(Storage = "_Part_Collection_CashCollected", DbType = "Real")]
        public System.Nullable<float> Part_Collection_CashCollected
        {
            get
            {
                return this._Part_Collection_CashCollected;
            }
            set
            {
                if ((this._Part_Collection_CashCollected != value))
                {
                    this._Part_Collection_CashCollected = value;
                }
            }
        }
    }

    public partial class InstallationRecord
    {

        private System.Nullable<int> _Datapak_No;

        private System.Nullable<int> _Installation_Price_Of_Play;

        private int _Installation_Token_Value;

        private int _Installation_No;

        private System.Nullable<int> _Bar_Pos_No;

        private System.Nullable<int> _Machine_No;

        private System.Nullable<int> _Datapak_No1;

        private System.Nullable<int> _Datapak_Variant;

        private System.Nullable<int> _HQ_Installation_No;

        private string _Installation_Reference;

        private System.Nullable<System.DateTime> _Start_Date;

        private System.Nullable<System.DateTime> _End_Date;

        private System.Nullable<float> _Anticipated_Percentage_Payout;

        private System.Nullable<int> _Coins_In_Counter;

        private System.Nullable<int> _Coins_Out_Counter;

        private System.Nullable<int> _Tokens_In_Counter;

        private System.Nullable<int> _Tokens_Out_Counter;

        private System.Nullable<int> _Prize_Counter;

        private System.Nullable<int> _Refill_Counter;

        private System.Nullable<int> _Tournament_Counter;

        private System.Nullable<int> _Jukebox_Counter;

        private System.Nullable<int> _Previous_Installation;

        private System.Nullable<float> _Bagged_Cash_Installation_No;

        private System.Nullable<float> _Bagged_Cash_Amount;

        private System.Nullable<float> _Bagged_Cash_Float;

        private System.Nullable<bool> _Installation_Out_Of_Order;

        private System.Nullable<float> _Float_Issued;

        private string _Float_Issued_By;

        private System.Nullable<int> _Installation_Price_Of_Play1;

        private System.Nullable<int> _Installation_Jackpot;

        private System.Nullable<int> _Installation_Meter_1_Initial_Value;

        private System.Nullable<int> _Installation_Meter_1_Final_Value;

        private System.Nullable<int> _Installation_Meter_2_Initial_Value;

        private System.Nullable<int> _Installation_Meter_2_Final_Value;

        private System.Nullable<int> _Installation_Meter_3_Initial_Value;

        private System.Nullable<int> _Installation_Meter_3_Final_Value;

        private System.Nullable<int> _Installation_Meter_4_Initial_Value;

        private System.Nullable<int> _Installation_Meter_4_Final_Value;

        private System.Nullable<int> _Installation_Meter_5_Initial_Value;

        private System.Nullable<int> _Installation_Meter_5_Final_Value;

        private System.Nullable<int> _Installation_Meter_6_Initial_Value;

        private System.Nullable<int> _Installation_Meter_6_Final_Value;

        private System.Nullable<int> _Installation_Meter_7_Initial_Value;

        private System.Nullable<int> _Installation_Meter_7_Final_Value;

        private System.Nullable<int> _Installation_Meter_8_Initial_Value;

        private System.Nullable<int> _Installation_Meter_8_Final_Value;

        private System.Nullable<int> _Installation_Meter_9_Initial_Value;

        private System.Nullable<int> _Installation_Meter_9_Final_Value;

        private System.Nullable<int> _Installation_Meter_10_Initial_Value;

        private System.Nullable<int> _Installation_Meter_10_Final_Value;

        private System.Nullable<int> _Installation_Meter_11_Initial_Value;

        private System.Nullable<int> _Installation_Meter_11_Final_Value;

        private System.Nullable<int> _Installation_Meter_12_Initial_Value;

        private System.Nullable<int> _Installation_Meter_12_Final_Value;

        private System.Nullable<int> _Installation_Meter_13_Initial_Value;

        private System.Nullable<int> _Installation_Meter_13_Final_Value;

        private System.Nullable<int> _Installation_Meter_14_Initial_Value;

        private System.Nullable<int> _Installation_Meter_14_Final_Value;

        private System.Nullable<int> _Installation_Meter_15_Initial_Value;

        private System.Nullable<int> _Installation_Meter_15_Final_Value;

        private System.Nullable<int> _Installation_Meter_16_Initial_Value;

        private System.Nullable<int> _Installation_Meter_16_Final_Value;

        private System.Nullable<int> _Installation_Meter_17_Initial_Value;

        private System.Nullable<int> _Installation_Meter_17_Final_Value;

        private System.Nullable<int> _Installation_Meter_18_Initial_Value;

        private System.Nullable<int> _Installation_Meter_18_Final_Value;

        private System.Nullable<int> _Installation_Meter_19_Initial_Value;

        private System.Nullable<int> _Installation_Meter_19_Final_Value;

        private System.Nullable<int> _Installation_Meter_20_Initial_Value;

        private System.Nullable<int> _Installation_Meter_20_Final_Value;

        private System.Nullable<int> _Installation_Meter_21_Initial_Value;

        private System.Nullable<int> _Installation_Meter_21_Final_Value;

        private System.Nullable<int> _Installation_Meter_22_Initial_Value;

        private System.Nullable<int> _Installation_Meter_22_Final_Value;

        private System.Nullable<int> _Installation_Meter_23_Initial_Value;

        private System.Nullable<int> _Installation_Meter_23_Final_Value;

        private System.Nullable<int> _Installation_Meter_24_Initial_Value;

        private System.Nullable<int> _Installation_Meter_24_Final_Value;

        private System.Nullable<int> _Installation_Meter_25_Initial_Value;

        private System.Nullable<int> _Installation_Meter_25_Final_Value;

        private System.Nullable<int> _Installation_Meter_26_Initial_Value;

        private System.Nullable<int> _Installation_Meter_26_Final_Value;

        private System.Nullable<int> _Installation_Meter_27_Initial_Value;

        private System.Nullable<int> _Installation_Meter_27_Final_Value;

        private System.Nullable<int> _Installation_Meter_28_Initial_Value;

        private System.Nullable<int> _Installation_Meter_28_Final_Value;

        private System.Nullable<int> _Installation_Meter_29_Initial_Value;

        private System.Nullable<int> _Installation_Meter_29_Final_Value;

        private System.Nullable<int> _Installation_Meter_30_Initial_Value;

        private System.Nullable<int> _Installation_Meter_30_Final_Value;

        private System.Nullable<int> _Installation_Meter_31_Initial_Value;

        private System.Nullable<int> _Installation_Meter_31_Final_Value;

        private System.Nullable<int> _Installation_Meter_32_Initial_Value;

        private System.Nullable<int> _Installation_Meter_32_Final_Value;

        private System.Nullable<int> _Installation_Float_Status;

        private System.Nullable<int> _Installation_Initial_Meters_Coins_In;

        private System.Nullable<int> _Installation_Initial_Meters_Coins_Out;

        private System.Nullable<int> _Installation_Initial_Meters_Coin_Drop;

        private System.Nullable<int> _Installation_Initial_Meters_External_Credit;

        private System.Nullable<int> _Installation_Initial_Meters_Games_Bet;

        private System.Nullable<int> _Installation_Initial_Meters_Games_Won;

        private System.Nullable<int> _Installation_Initial_Meters_Notes;

        private System.Nullable<int> _Installation_Initial_Meters_Handpay;

        private string _Anticipated_Removal_Date;

        private string _Rental_Step_Down_Date;

        private System.Nullable<decimal> _Rent1;

        private System.Nullable<decimal> _Rent2;

        private System.Nullable<decimal> _Licence;

        private System.Nullable<bool> _Installation_Out_Order;

        private System.Nullable<int> _Installation_Counter_Cash_In_Units;

        private System.Nullable<int> _Installation_Counter_Cash_Out_Units;

        private System.Nullable<int> _Installation_Counter_Token_In_Units;

        private System.Nullable<int> _Installation_Counter_Token_Out_Units;

        private System.Nullable<int> _Installation_Counter_Refill_Units;

        private System.Nullable<int> _Installation_Counter_Jackpot_Units;

        private System.Nullable<int> _Installation_Counter_Prize_Units;

        private System.Nullable<int> _Installation_Counter_Tournament_Units;

        private System.Nullable<int> _Installation_Counter_Jukebox_Play_Units;

        private System.Nullable<int> _Installation_Counter_Jukebox_Units;

        private System.Nullable<int> _Planned_Movement_ID;

        private string _Installation_RDC_Machine_Code;

        private string _Installation_RDC_Secondary_Machine_Code;

        private int _Installation_Token_Value1;

        public InstallationRecord()
        {
        }

        [Column(Storage = "_Datapak_No", DbType = "Int")]
        public System.Nullable<int> Datapak_No
        {
            get
            {
                return this._Datapak_No;
            }
            set
            {
                if ((this._Datapak_No != value))
                {
                    this._Datapak_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_Price_Of_Play", DbType = "Int")]
        public System.Nullable<int> Installation_Price_Of_Play
        {
            get
            {
                return this._Installation_Price_Of_Play;
            }
            set
            {
                if ((this._Installation_Price_Of_Play != value))
                {
                    this._Installation_Price_Of_Play = value;
                }
            }
        }

        [Column(Storage = "_Installation_Token_Value", DbType = "Int NOT NULL")]
        public int Installation_Token_Value
        {
            get
            {
                return this._Installation_Token_Value;
            }
            set
            {
                if ((this._Installation_Token_Value != value))
                {
                    this._Installation_Token_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
        public int Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
                }
            }
        }

        [Column(Storage = "_Bar_Pos_No", DbType = "Int")]
        public System.Nullable<int> Bar_Pos_No
        {
            get
            {
                return this._Bar_Pos_No;
            }
            set
            {
                if ((this._Bar_Pos_No != value))
                {
                    this._Bar_Pos_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_No", DbType = "Int")]
        public System.Nullable<int> Machine_No
        {
            get
            {
                return this._Machine_No;
            }
            set
            {
                if ((this._Machine_No != value))
                {
                    this._Machine_No = value;
                }
            }
        }

        [Column(Storage = "_Datapak_No1", DbType = "Int")]
        public System.Nullable<int> Datapak_No1
        {
            get
            {
                return this._Datapak_No1;
            }
            set
            {
                if ((this._Datapak_No1 != value))
                {
                    this._Datapak_No1 = value;
                }
            }
        }

        [Column(Storage = "_Datapak_Variant", DbType = "Int")]
        public System.Nullable<int> Datapak_Variant
        {
            get
            {
                return this._Datapak_Variant;
            }
            set
            {
                if ((this._Datapak_Variant != value))
                {
                    this._Datapak_Variant = value;
                }
            }
        }

        [Column(Storage = "_HQ_Installation_No", DbType = "Int")]
        public System.Nullable<int> HQ_Installation_No
        {
            get
            {
                return this._HQ_Installation_No;
            }
            set
            {
                if ((this._HQ_Installation_No != value))
                {
                    this._HQ_Installation_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_Reference", DbType = "VarChar(50)")]
        public string Installation_Reference
        {
            get
            {
                return this._Installation_Reference;
            }
            set
            {
                if ((this._Installation_Reference != value))
                {
                    this._Installation_Reference = value;
                }
            }
        }

        [Column(Storage = "_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Start_Date
        {
            get
            {
                return this._Start_Date;
            }
            set
            {
                if ((this._Start_Date != value))
                {
                    this._Start_Date = value;
                }
            }
        }

        [Column(Storage = "_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> End_Date
        {
            get
            {
                return this._End_Date;
            }
            set
            {
                if ((this._End_Date != value))
                {
                    this._End_Date = value;
                }
            }
        }

        [Column(Storage = "_Anticipated_Percentage_Payout", DbType = "Real")]
        public System.Nullable<float> Anticipated_Percentage_Payout
        {
            get
            {
                return this._Anticipated_Percentage_Payout;
            }
            set
            {
                if ((this._Anticipated_Percentage_Payout != value))
                {
                    this._Anticipated_Percentage_Payout = value;
                }
            }
        }

        [Column(Storage = "_Coins_In_Counter", DbType = "Int")]
        public System.Nullable<int> Coins_In_Counter
        {
            get
            {
                return this._Coins_In_Counter;
            }
            set
            {
                if ((this._Coins_In_Counter != value))
                {
                    this._Coins_In_Counter = value;
                }
            }
        }

        [Column(Storage = "_Coins_Out_Counter", DbType = "Int")]
        public System.Nullable<int> Coins_Out_Counter
        {
            get
            {
                return this._Coins_Out_Counter;
            }
            set
            {
                if ((this._Coins_Out_Counter != value))
                {
                    this._Coins_Out_Counter = value;
                }
            }
        }

        [Column(Storage = "_Tokens_In_Counter", DbType = "Int")]
        public System.Nullable<int> Tokens_In_Counter
        {
            get
            {
                return this._Tokens_In_Counter;
            }
            set
            {
                if ((this._Tokens_In_Counter != value))
                {
                    this._Tokens_In_Counter = value;
                }
            }
        }

        [Column(Storage = "_Tokens_Out_Counter", DbType = "Int")]
        public System.Nullable<int> Tokens_Out_Counter
        {
            get
            {
                return this._Tokens_Out_Counter;
            }
            set
            {
                if ((this._Tokens_Out_Counter != value))
                {
                    this._Tokens_Out_Counter = value;
                }
            }
        }

        [Column(Storage = "_Prize_Counter", DbType = "Int")]
        public System.Nullable<int> Prize_Counter
        {
            get
            {
                return this._Prize_Counter;
            }
            set
            {
                if ((this._Prize_Counter != value))
                {
                    this._Prize_Counter = value;
                }
            }
        }

        [Column(Storage = "_Refill_Counter", DbType = "Int")]
        public System.Nullable<int> Refill_Counter
        {
            get
            {
                return this._Refill_Counter;
            }
            set
            {
                if ((this._Refill_Counter != value))
                {
                    this._Refill_Counter = value;
                }
            }
        }

        [Column(Storage = "_Tournament_Counter", DbType = "Int")]
        public System.Nullable<int> Tournament_Counter
        {
            get
            {
                return this._Tournament_Counter;
            }
            set
            {
                if ((this._Tournament_Counter != value))
                {
                    this._Tournament_Counter = value;
                }
            }
        }

        [Column(Storage = "_Jukebox_Counter", DbType = "Int")]
        public System.Nullable<int> Jukebox_Counter
        {
            get
            {
                return this._Jukebox_Counter;
            }
            set
            {
                if ((this._Jukebox_Counter != value))
                {
                    this._Jukebox_Counter = value;
                }
            }
        }

        [Column(Storage = "_Previous_Installation", DbType = "Int")]
        public System.Nullable<int> Previous_Installation
        {
            get
            {
                return this._Previous_Installation;
            }
            set
            {
                if ((this._Previous_Installation != value))
                {
                    this._Previous_Installation = value;
                }
            }
        }

        [Column(Storage = "_Bagged_Cash_Installation_No", DbType = "Real")]
        public System.Nullable<float> Bagged_Cash_Installation_No
        {
            get
            {
                return this._Bagged_Cash_Installation_No;
            }
            set
            {
                if ((this._Bagged_Cash_Installation_No != value))
                {
                    this._Bagged_Cash_Installation_No = value;
                }
            }
        }

        [Column(Storage = "_Bagged_Cash_Amount", DbType = "Real")]
        public System.Nullable<float> Bagged_Cash_Amount
        {
            get
            {
                return this._Bagged_Cash_Amount;
            }
            set
            {
                if ((this._Bagged_Cash_Amount != value))
                {
                    this._Bagged_Cash_Amount = value;
                }
            }
        }

        [Column(Storage = "_Bagged_Cash_Float", DbType = "Real")]
        public System.Nullable<float> Bagged_Cash_Float
        {
            get
            {
                return this._Bagged_Cash_Float;
            }
            set
            {
                if ((this._Bagged_Cash_Float != value))
                {
                    this._Bagged_Cash_Float = value;
                }
            }
        }

        [Column(Storage = "_Installation_Out_Of_Order", DbType = "Bit")]
        public System.Nullable<bool> Installation_Out_Of_Order
        {
            get
            {
                return this._Installation_Out_Of_Order;
            }
            set
            {
                if ((this._Installation_Out_Of_Order != value))
                {
                    this._Installation_Out_Of_Order = value;
                }
            }
        }

        [Column(Storage = "_Float_Issued", DbType = "Real")]
        public System.Nullable<float> Float_Issued
        {
            get
            {
                return this._Float_Issued;
            }
            set
            {
                if ((this._Float_Issued != value))
                {
                    this._Float_Issued = value;
                }
            }
        }

        [Column(Storage = "_Float_Issued_By", DbType = "VarChar(50)")]
        public string Float_Issued_By
        {
            get
            {
                return this._Float_Issued_By;
            }
            set
            {
                if ((this._Float_Issued_By != value))
                {
                    this._Float_Issued_By = value;
                }
            }
        }

        [Column(Storage = "_Installation_Price_Of_Play1", DbType = "Int")]
        public System.Nullable<int> Installation_Price_Of_Play1
        {
            get
            {
                return this._Installation_Price_Of_Play1;
            }
            set
            {
                if ((this._Installation_Price_Of_Play1 != value))
                {
                    this._Installation_Price_Of_Play1 = value;
                }
            }
        }

        [Column(Storage = "_Installation_Jackpot", DbType = "Int")]
        public System.Nullable<int> Installation_Jackpot
        {
            get
            {
                return this._Installation_Jackpot;
            }
            set
            {
                if ((this._Installation_Jackpot != value))
                {
                    this._Installation_Jackpot = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_1_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_1_Initial_Value
        {
            get
            {
                return this._Installation_Meter_1_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_1_Initial_Value != value))
                {
                    this._Installation_Meter_1_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_1_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_1_Final_Value
        {
            get
            {
                return this._Installation_Meter_1_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_1_Final_Value != value))
                {
                    this._Installation_Meter_1_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_2_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_2_Initial_Value
        {
            get
            {
                return this._Installation_Meter_2_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_2_Initial_Value != value))
                {
                    this._Installation_Meter_2_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_2_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_2_Final_Value
        {
            get
            {
                return this._Installation_Meter_2_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_2_Final_Value != value))
                {
                    this._Installation_Meter_2_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_3_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_3_Initial_Value
        {
            get
            {
                return this._Installation_Meter_3_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_3_Initial_Value != value))
                {
                    this._Installation_Meter_3_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_3_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_3_Final_Value
        {
            get
            {
                return this._Installation_Meter_3_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_3_Final_Value != value))
                {
                    this._Installation_Meter_3_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_4_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_4_Initial_Value
        {
            get
            {
                return this._Installation_Meter_4_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_4_Initial_Value != value))
                {
                    this._Installation_Meter_4_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_4_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_4_Final_Value
        {
            get
            {
                return this._Installation_Meter_4_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_4_Final_Value != value))
                {
                    this._Installation_Meter_4_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_5_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_5_Initial_Value
        {
            get
            {
                return this._Installation_Meter_5_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_5_Initial_Value != value))
                {
                    this._Installation_Meter_5_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_5_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_5_Final_Value
        {
            get
            {
                return this._Installation_Meter_5_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_5_Final_Value != value))
                {
                    this._Installation_Meter_5_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_6_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_6_Initial_Value
        {
            get
            {
                return this._Installation_Meter_6_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_6_Initial_Value != value))
                {
                    this._Installation_Meter_6_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_6_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_6_Final_Value
        {
            get
            {
                return this._Installation_Meter_6_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_6_Final_Value != value))
                {
                    this._Installation_Meter_6_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_7_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_7_Initial_Value
        {
            get
            {
                return this._Installation_Meter_7_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_7_Initial_Value != value))
                {
                    this._Installation_Meter_7_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_7_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_7_Final_Value
        {
            get
            {
                return this._Installation_Meter_7_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_7_Final_Value != value))
                {
                    this._Installation_Meter_7_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_8_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_8_Initial_Value
        {
            get
            {
                return this._Installation_Meter_8_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_8_Initial_Value != value))
                {
                    this._Installation_Meter_8_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_8_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_8_Final_Value
        {
            get
            {
                return this._Installation_Meter_8_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_8_Final_Value != value))
                {
                    this._Installation_Meter_8_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_9_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_9_Initial_Value
        {
            get
            {
                return this._Installation_Meter_9_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_9_Initial_Value != value))
                {
                    this._Installation_Meter_9_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_9_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_9_Final_Value
        {
            get
            {
                return this._Installation_Meter_9_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_9_Final_Value != value))
                {
                    this._Installation_Meter_9_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_10_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_10_Initial_Value
        {
            get
            {
                return this._Installation_Meter_10_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_10_Initial_Value != value))
                {
                    this._Installation_Meter_10_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_10_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_10_Final_Value
        {
            get
            {
                return this._Installation_Meter_10_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_10_Final_Value != value))
                {
                    this._Installation_Meter_10_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_11_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_11_Initial_Value
        {
            get
            {
                return this._Installation_Meter_11_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_11_Initial_Value != value))
                {
                    this._Installation_Meter_11_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_11_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_11_Final_Value
        {
            get
            {
                return this._Installation_Meter_11_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_11_Final_Value != value))
                {
                    this._Installation_Meter_11_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_12_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_12_Initial_Value
        {
            get
            {
                return this._Installation_Meter_12_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_12_Initial_Value != value))
                {
                    this._Installation_Meter_12_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_12_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_12_Final_Value
        {
            get
            {
                return this._Installation_Meter_12_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_12_Final_Value != value))
                {
                    this._Installation_Meter_12_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_13_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_13_Initial_Value
        {
            get
            {
                return this._Installation_Meter_13_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_13_Initial_Value != value))
                {
                    this._Installation_Meter_13_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_13_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_13_Final_Value
        {
            get
            {
                return this._Installation_Meter_13_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_13_Final_Value != value))
                {
                    this._Installation_Meter_13_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_14_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_14_Initial_Value
        {
            get
            {
                return this._Installation_Meter_14_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_14_Initial_Value != value))
                {
                    this._Installation_Meter_14_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_14_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_14_Final_Value
        {
            get
            {
                return this._Installation_Meter_14_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_14_Final_Value != value))
                {
                    this._Installation_Meter_14_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_15_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_15_Initial_Value
        {
            get
            {
                return this._Installation_Meter_15_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_15_Initial_Value != value))
                {
                    this._Installation_Meter_15_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_15_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_15_Final_Value
        {
            get
            {
                return this._Installation_Meter_15_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_15_Final_Value != value))
                {
                    this._Installation_Meter_15_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_16_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_16_Initial_Value
        {
            get
            {
                return this._Installation_Meter_16_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_16_Initial_Value != value))
                {
                    this._Installation_Meter_16_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_16_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_16_Final_Value
        {
            get
            {
                return this._Installation_Meter_16_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_16_Final_Value != value))
                {
                    this._Installation_Meter_16_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_17_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_17_Initial_Value
        {
            get
            {
                return this._Installation_Meter_17_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_17_Initial_Value != value))
                {
                    this._Installation_Meter_17_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_17_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_17_Final_Value
        {
            get
            {
                return this._Installation_Meter_17_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_17_Final_Value != value))
                {
                    this._Installation_Meter_17_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_18_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_18_Initial_Value
        {
            get
            {
                return this._Installation_Meter_18_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_18_Initial_Value != value))
                {
                    this._Installation_Meter_18_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_18_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_18_Final_Value
        {
            get
            {
                return this._Installation_Meter_18_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_18_Final_Value != value))
                {
                    this._Installation_Meter_18_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_19_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_19_Initial_Value
        {
            get
            {
                return this._Installation_Meter_19_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_19_Initial_Value != value))
                {
                    this._Installation_Meter_19_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_19_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_19_Final_Value
        {
            get
            {
                return this._Installation_Meter_19_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_19_Final_Value != value))
                {
                    this._Installation_Meter_19_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_20_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_20_Initial_Value
        {
            get
            {
                return this._Installation_Meter_20_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_20_Initial_Value != value))
                {
                    this._Installation_Meter_20_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_20_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_20_Final_Value
        {
            get
            {
                return this._Installation_Meter_20_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_20_Final_Value != value))
                {
                    this._Installation_Meter_20_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_21_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_21_Initial_Value
        {
            get
            {
                return this._Installation_Meter_21_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_21_Initial_Value != value))
                {
                    this._Installation_Meter_21_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_21_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_21_Final_Value
        {
            get
            {
                return this._Installation_Meter_21_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_21_Final_Value != value))
                {
                    this._Installation_Meter_21_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_22_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_22_Initial_Value
        {
            get
            {
                return this._Installation_Meter_22_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_22_Initial_Value != value))
                {
                    this._Installation_Meter_22_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_22_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_22_Final_Value
        {
            get
            {
                return this._Installation_Meter_22_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_22_Final_Value != value))
                {
                    this._Installation_Meter_22_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_23_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_23_Initial_Value
        {
            get
            {
                return this._Installation_Meter_23_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_23_Initial_Value != value))
                {
                    this._Installation_Meter_23_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_23_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_23_Final_Value
        {
            get
            {
                return this._Installation_Meter_23_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_23_Final_Value != value))
                {
                    this._Installation_Meter_23_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_24_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_24_Initial_Value
        {
            get
            {
                return this._Installation_Meter_24_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_24_Initial_Value != value))
                {
                    this._Installation_Meter_24_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_24_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_24_Final_Value
        {
            get
            {
                return this._Installation_Meter_24_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_24_Final_Value != value))
                {
                    this._Installation_Meter_24_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_25_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_25_Initial_Value
        {
            get
            {
                return this._Installation_Meter_25_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_25_Initial_Value != value))
                {
                    this._Installation_Meter_25_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_25_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_25_Final_Value
        {
            get
            {
                return this._Installation_Meter_25_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_25_Final_Value != value))
                {
                    this._Installation_Meter_25_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_26_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_26_Initial_Value
        {
            get
            {
                return this._Installation_Meter_26_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_26_Initial_Value != value))
                {
                    this._Installation_Meter_26_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_26_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_26_Final_Value
        {
            get
            {
                return this._Installation_Meter_26_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_26_Final_Value != value))
                {
                    this._Installation_Meter_26_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_27_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_27_Initial_Value
        {
            get
            {
                return this._Installation_Meter_27_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_27_Initial_Value != value))
                {
                    this._Installation_Meter_27_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_27_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_27_Final_Value
        {
            get
            {
                return this._Installation_Meter_27_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_27_Final_Value != value))
                {
                    this._Installation_Meter_27_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_28_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_28_Initial_Value
        {
            get
            {
                return this._Installation_Meter_28_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_28_Initial_Value != value))
                {
                    this._Installation_Meter_28_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_28_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_28_Final_Value
        {
            get
            {
                return this._Installation_Meter_28_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_28_Final_Value != value))
                {
                    this._Installation_Meter_28_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_29_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_29_Initial_Value
        {
            get
            {
                return this._Installation_Meter_29_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_29_Initial_Value != value))
                {
                    this._Installation_Meter_29_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_29_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_29_Final_Value
        {
            get
            {
                return this._Installation_Meter_29_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_29_Final_Value != value))
                {
                    this._Installation_Meter_29_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_30_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_30_Initial_Value
        {
            get
            {
                return this._Installation_Meter_30_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_30_Initial_Value != value))
                {
                    this._Installation_Meter_30_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_30_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_30_Final_Value
        {
            get
            {
                return this._Installation_Meter_30_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_30_Final_Value != value))
                {
                    this._Installation_Meter_30_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_31_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_31_Initial_Value
        {
            get
            {
                return this._Installation_Meter_31_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_31_Initial_Value != value))
                {
                    this._Installation_Meter_31_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_31_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_31_Final_Value
        {
            get
            {
                return this._Installation_Meter_31_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_31_Final_Value != value))
                {
                    this._Installation_Meter_31_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_32_Initial_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_32_Initial_Value
        {
            get
            {
                return this._Installation_Meter_32_Initial_Value;
            }
            set
            {
                if ((this._Installation_Meter_32_Initial_Value != value))
                {
                    this._Installation_Meter_32_Initial_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Meter_32_Final_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Meter_32_Final_Value
        {
            get
            {
                return this._Installation_Meter_32_Final_Value;
            }
            set
            {
                if ((this._Installation_Meter_32_Final_Value != value))
                {
                    this._Installation_Meter_32_Final_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Float_Status", DbType = "Int")]
        public System.Nullable<int> Installation_Float_Status
        {
            get
            {
                return this._Installation_Float_Status;
            }
            set
            {
                if ((this._Installation_Float_Status != value))
                {
                    this._Installation_Float_Status = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Coins_In", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Coins_In
        {
            get
            {
                return this._Installation_Initial_Meters_Coins_In;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Coins_In != value))
                {
                    this._Installation_Initial_Meters_Coins_In = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Coins_Out", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Coins_Out
        {
            get
            {
                return this._Installation_Initial_Meters_Coins_Out;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Coins_Out != value))
                {
                    this._Installation_Initial_Meters_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Coin_Drop", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Coin_Drop
        {
            get
            {
                return this._Installation_Initial_Meters_Coin_Drop;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Coin_Drop != value))
                {
                    this._Installation_Initial_Meters_Coin_Drop = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_External_Credit", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_External_Credit
        {
            get
            {
                return this._Installation_Initial_Meters_External_Credit;
            }
            set
            {
                if ((this._Installation_Initial_Meters_External_Credit != value))
                {
                    this._Installation_Initial_Meters_External_Credit = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Games_Bet", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Games_Bet
        {
            get
            {
                return this._Installation_Initial_Meters_Games_Bet;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Games_Bet != value))
                {
                    this._Installation_Initial_Meters_Games_Bet = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Games_Won", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Games_Won
        {
            get
            {
                return this._Installation_Initial_Meters_Games_Won;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Games_Won != value))
                {
                    this._Installation_Initial_Meters_Games_Won = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Notes", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Notes
        {
            get
            {
                return this._Installation_Initial_Meters_Notes;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Notes != value))
                {
                    this._Installation_Initial_Meters_Notes = value;
                }
            }
        }

        [Column(Storage = "_Installation_Initial_Meters_Handpay", DbType = "Int")]
        public System.Nullable<int> Installation_Initial_Meters_Handpay
        {
            get
            {
                return this._Installation_Initial_Meters_Handpay;
            }
            set
            {
                if ((this._Installation_Initial_Meters_Handpay != value))
                {
                    this._Installation_Initial_Meters_Handpay = value;
                }
            }
        }

        [Column(Storage = "_Anticipated_Removal_Date", DbType = "VarChar(30)")]
        public string Anticipated_Removal_Date
        {
            get
            {
                return this._Anticipated_Removal_Date;
            }
            set
            {
                if ((this._Anticipated_Removal_Date != value))
                {
                    this._Anticipated_Removal_Date = value;
                }
            }
        }

        [Column(Storage = "_Rental_Step_Down_Date", DbType = "VarChar(30)")]
        public string Rental_Step_Down_Date
        {
            get
            {
                return this._Rental_Step_Down_Date;
            }
            set
            {
                if ((this._Rental_Step_Down_Date != value))
                {
                    this._Rental_Step_Down_Date = value;
                }
            }
        }

        [Column(Storage = "_Rent1", DbType = "Money")]
        public System.Nullable<decimal> Rent1
        {
            get
            {
                return this._Rent1;
            }
            set
            {
                if ((this._Rent1 != value))
                {
                    this._Rent1 = value;
                }
            }
        }

        [Column(Storage = "_Rent2", DbType = "Money")]
        public System.Nullable<decimal> Rent2
        {
            get
            {
                return this._Rent2;
            }
            set
            {
                if ((this._Rent2 != value))
                {
                    this._Rent2 = value;
                }
            }
        }

        [Column(Storage = "_Licence", DbType = "Money")]
        public System.Nullable<decimal> Licence
        {
            get
            {
                return this._Licence;
            }
            set
            {
                if ((this._Licence != value))
                {
                    this._Licence = value;
                }
            }
        }

        [Column(Storage = "_Installation_Out_Order", DbType = "Bit")]
        public System.Nullable<bool> Installation_Out_Order
        {
            get
            {
                return this._Installation_Out_Order;
            }
            set
            {
                if ((this._Installation_Out_Order != value))
                {
                    this._Installation_Out_Order = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Cash_In_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Cash_In_Units
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

        [Column(Storage = "_Installation_Counter_Cash_Out_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Cash_Out_Units
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

        [Column(Storage = "_Installation_Counter_Token_In_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Token_In_Units
        {
            get
            {
                return this._Installation_Counter_Token_In_Units;
            }
            set
            {
                if ((this._Installation_Counter_Token_In_Units != value))
                {
                    this._Installation_Counter_Token_In_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Token_Out_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Token_Out_Units
        {
            get
            {
                return this._Installation_Counter_Token_Out_Units;
            }
            set
            {
                if ((this._Installation_Counter_Token_Out_Units != value))
                {
                    this._Installation_Counter_Token_Out_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Refill_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Refill_Units
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

        [Column(Storage = "_Installation_Counter_Jackpot_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Jackpot_Units
        {
            get
            {
                return this._Installation_Counter_Jackpot_Units;
            }
            set
            {
                if ((this._Installation_Counter_Jackpot_Units != value))
                {
                    this._Installation_Counter_Jackpot_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Prize_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Prize_Units
        {
            get
            {
                return this._Installation_Counter_Prize_Units;
            }
            set
            {
                if ((this._Installation_Counter_Prize_Units != value))
                {
                    this._Installation_Counter_Prize_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Tournament_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Tournament_Units
        {
            get
            {
                return this._Installation_Counter_Tournament_Units;
            }
            set
            {
                if ((this._Installation_Counter_Tournament_Units != value))
                {
                    this._Installation_Counter_Tournament_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Jukebox_Play_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Jukebox_Play_Units
        {
            get
            {
                return this._Installation_Counter_Jukebox_Play_Units;
            }
            set
            {
                if ((this._Installation_Counter_Jukebox_Play_Units != value))
                {
                    this._Installation_Counter_Jukebox_Play_Units = value;
                }
            }
        }

        [Column(Storage = "_Installation_Counter_Jukebox_Units", DbType = "Int")]
        public System.Nullable<int> Installation_Counter_Jukebox_Units
        {
            get
            {
                return this._Installation_Counter_Jukebox_Units;
            }
            set
            {
                if ((this._Installation_Counter_Jukebox_Units != value))
                {
                    this._Installation_Counter_Jukebox_Units = value;
                }
            }
        }

        [Column(Storage = "_Planned_Movement_ID", DbType = "Int")]
        public System.Nullable<int> Planned_Movement_ID
        {
            get
            {
                return this._Planned_Movement_ID;
            }
            set
            {
                if ((this._Planned_Movement_ID != value))
                {
                    this._Planned_Movement_ID = value;
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Machine_Code", DbType = "VarChar(10)")]
        public string Installation_RDC_Machine_Code
        {
            get
            {
                return this._Installation_RDC_Machine_Code;
            }
            set
            {
                if ((this._Installation_RDC_Machine_Code != value))
                {
                    this._Installation_RDC_Machine_Code = value;
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Secondary_Machine_Code", DbType = "VarChar(20)")]
        public string Installation_RDC_Secondary_Machine_Code
        {
            get
            {
                return this._Installation_RDC_Secondary_Machine_Code;
            }
            set
            {
                if ((this._Installation_RDC_Secondary_Machine_Code != value))
                {
                    this._Installation_RDC_Secondary_Machine_Code = value;
                }
            }
        }

        [Column(Storage = "_Installation_Token_Value1", DbType = "Int NOT NULL")]
        public int Installation_Token_Value1
        {
            get
            {
                return this._Installation_Token_Value1;
            }
            set
            {
                if ((this._Installation_Token_Value1 != value))
                {
                    this._Installation_Token_Value1 = value;
                }
            }
        }
    }

    public partial class TreasuryRecord
    {

        private int _Treasury_No;

        private System.Nullable<int> _Collection_No;

        private System.Nullable<int> _Installation_No;

        private System.Nullable<int> _User_No;

        private string _Treasury_Date;

        private System.Nullable<float> _Treasury_Amount;

        private string _Treasury_Reason;

        private System.Nullable<bool> _Treasury_Allocated;

        private string _Treasury_Type;

        private System.Nullable<bool> _Treasury_Temp;

        private string _Treasury_Docket_No;

        private System.Nullable<float> _Treasury_Breakdown_2000p;

        private System.Nullable<float> _Treasury_Breakdown_1000p;

        private System.Nullable<float> _Treasury_Breakdown_500p;

        private System.Nullable<float> _Treasury_Breakdown_200p;

        private System.Nullable<float> _Treasury_Breakdown_100p;

        private System.Nullable<float> _Treasury_Breakdown_50p;

        private System.Nullable<float> _Treasury_Breakdown_20p;

        private System.Nullable<float> _Treasury_Breakdown_10p;

        private System.Nullable<float> _Treasury_Breakdown_5p;

        private System.Nullable<float> _Treasury_Breakdown_2p;

        private System.Nullable<int> _Treasury_Float_Issued_By;

        private System.Nullable<float> _Treasury_Float_Recovered_Total;

        private System.Nullable<float> _Treasury_Float_Recovered_2000p;

        private System.Nullable<float> _Treasury_Float_Recovered_1000p;

        private System.Nullable<float> _Treasury_Float_Recovered_500p;

        private System.Nullable<float> _Treasury_Float_Recovered_200p;

        private System.Nullable<float> _Treasury_Float_Recovered_100p;

        private System.Nullable<float> _Treasury_Float_Recovered_50p;

        private System.Nullable<float> _Treasury_Float_Recovered_20p;

        private System.Nullable<float> _Treasury_Float_Recovered_10p;

        private System.Nullable<float> _Treasury_Float_Recovered_5p;

        private System.Nullable<float> _Treasury_Float_Recovered_2p;

        private System.Nullable<int> _Treasury_Issuer_User_No;

        private string _Treasury_Membership_No;

        private System.Nullable<int> _Treasury_Reason_Code;

        private System.Nullable<int> _HQ_ID;

        private System.Nullable<System.DateTime> _Treasury_Actual_Date;

        public TreasuryRecord()
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

        [Column(Storage = "_Collection_No", DbType = "Int")]
        public System.Nullable<int> Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_No", DbType = "Int")]
        public System.Nullable<int> Installation_No
        {
            get
            {
                return this._Installation_No;
            }
            set
            {
                if ((this._Installation_No != value))
                {
                    this._Installation_No = value;
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

        [Column(Storage = "_Treasury_Amount", DbType = "Real")]
        public System.Nullable<float> Treasury_Amount
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

        [Column(Storage = "_Treasury_Allocated", DbType = "Bit")]
        public System.Nullable<bool> Treasury_Allocated
        {
            get
            {
                return this._Treasury_Allocated;
            }
            set
            {
                if ((this._Treasury_Allocated != value))
                {
                    this._Treasury_Allocated = value;
                }
            }
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

        [Column(Storage = "_Treasury_Temp", DbType = "Bit")]
        public System.Nullable<bool> Treasury_Temp
        {
            get
            {
                return this._Treasury_Temp;
            }
            set
            {
                if ((this._Treasury_Temp != value))
                {
                    this._Treasury_Temp = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Docket_No", DbType = "VarChar(50)")]
        public string Treasury_Docket_No
        {
            get
            {
                return this._Treasury_Docket_No;
            }
            set
            {
                if ((this._Treasury_Docket_No != value))
                {
                    this._Treasury_Docket_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_2000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_2000p
        {
            get
            {
                return this._Treasury_Breakdown_2000p;
            }
            set
            {
                if ((this._Treasury_Breakdown_2000p != value))
                {
                    this._Treasury_Breakdown_2000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_1000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_1000p
        {
            get
            {
                return this._Treasury_Breakdown_1000p;
            }
            set
            {
                if ((this._Treasury_Breakdown_1000p != value))
                {
                    this._Treasury_Breakdown_1000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_500p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_500p
        {
            get
            {
                return this._Treasury_Breakdown_500p;
            }
            set
            {
                if ((this._Treasury_Breakdown_500p != value))
                {
                    this._Treasury_Breakdown_500p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_200p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_200p
        {
            get
            {
                return this._Treasury_Breakdown_200p;
            }
            set
            {
                if ((this._Treasury_Breakdown_200p != value))
                {
                    this._Treasury_Breakdown_200p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_100p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_100p
        {
            get
            {
                return this._Treasury_Breakdown_100p;
            }
            set
            {
                if ((this._Treasury_Breakdown_100p != value))
                {
                    this._Treasury_Breakdown_100p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_50p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_50p
        {
            get
            {
                return this._Treasury_Breakdown_50p;
            }
            set
            {
                if ((this._Treasury_Breakdown_50p != value))
                {
                    this._Treasury_Breakdown_50p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_20p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_20p
        {
            get
            {
                return this._Treasury_Breakdown_20p;
            }
            set
            {
                if ((this._Treasury_Breakdown_20p != value))
                {
                    this._Treasury_Breakdown_20p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_10p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_10p
        {
            get
            {
                return this._Treasury_Breakdown_10p;
            }
            set
            {
                if ((this._Treasury_Breakdown_10p != value))
                {
                    this._Treasury_Breakdown_10p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_5p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_5p
        {
            get
            {
                return this._Treasury_Breakdown_5p;
            }
            set
            {
                if ((this._Treasury_Breakdown_5p != value))
                {
                    this._Treasury_Breakdown_5p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Breakdown_2p", DbType = "Real")]
        public System.Nullable<float> Treasury_Breakdown_2p
        {
            get
            {
                return this._Treasury_Breakdown_2p;
            }
            set
            {
                if ((this._Treasury_Breakdown_2p != value))
                {
                    this._Treasury_Breakdown_2p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Issued_By", DbType = "Int")]
        public System.Nullable<int> Treasury_Float_Issued_By
        {
            get
            {
                return this._Treasury_Float_Issued_By;
            }
            set
            {
                if ((this._Treasury_Float_Issued_By != value))
                {
                    this._Treasury_Float_Issued_By = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_Total", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_Total
        {
            get
            {
                return this._Treasury_Float_Recovered_Total;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_Total != value))
                {
                    this._Treasury_Float_Recovered_Total = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_2000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_2000p
        {
            get
            {
                return this._Treasury_Float_Recovered_2000p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_2000p != value))
                {
                    this._Treasury_Float_Recovered_2000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_1000p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_1000p
        {
            get
            {
                return this._Treasury_Float_Recovered_1000p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_1000p != value))
                {
                    this._Treasury_Float_Recovered_1000p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_500p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_500p
        {
            get
            {
                return this._Treasury_Float_Recovered_500p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_500p != value))
                {
                    this._Treasury_Float_Recovered_500p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_200p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_200p
        {
            get
            {
                return this._Treasury_Float_Recovered_200p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_200p != value))
                {
                    this._Treasury_Float_Recovered_200p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_100p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_100p
        {
            get
            {
                return this._Treasury_Float_Recovered_100p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_100p != value))
                {
                    this._Treasury_Float_Recovered_100p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_50p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_50p
        {
            get
            {
                return this._Treasury_Float_Recovered_50p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_50p != value))
                {
                    this._Treasury_Float_Recovered_50p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_20p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_20p
        {
            get
            {
                return this._Treasury_Float_Recovered_20p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_20p != value))
                {
                    this._Treasury_Float_Recovered_20p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_10p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_10p
        {
            get
            {
                return this._Treasury_Float_Recovered_10p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_10p != value))
                {
                    this._Treasury_Float_Recovered_10p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_5p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_5p
        {
            get
            {
                return this._Treasury_Float_Recovered_5p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_5p != value))
                {
                    this._Treasury_Float_Recovered_5p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Float_Recovered_2p", DbType = "Real")]
        public System.Nullable<float> Treasury_Float_Recovered_2p
        {
            get
            {
                return this._Treasury_Float_Recovered_2p;
            }
            set
            {
                if ((this._Treasury_Float_Recovered_2p != value))
                {
                    this._Treasury_Float_Recovered_2p = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Issuer_User_No", DbType = "Int")]
        public System.Nullable<int> Treasury_Issuer_User_No
        {
            get
            {
                return this._Treasury_Issuer_User_No;
            }
            set
            {
                if ((this._Treasury_Issuer_User_No != value))
                {
                    this._Treasury_Issuer_User_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Membership_No", DbType = "VarChar(50)")]
        public string Treasury_Membership_No
        {
            get
            {
                return this._Treasury_Membership_No;
            }
            set
            {
                if ((this._Treasury_Membership_No != value))
                {
                    this._Treasury_Membership_No = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Reason_Code", DbType = "Int")]
        public System.Nullable<int> Treasury_Reason_Code
        {
            get
            {
                return this._Treasury_Reason_Code;
            }
            set
            {
                if ((this._Treasury_Reason_Code != value))
                {
                    this._Treasury_Reason_Code = value;
                }
            }
        }

        [Column(Storage = "_HQ_ID", DbType = "Int")]
        public System.Nullable<int> HQ_ID
        {
            get
            {
                return this._HQ_ID;
            }
            set
            {
                if ((this._HQ_ID != value))
                {
                    this._HQ_ID = value;
                }
            }
        }

        [Column(Storage = "_Treasury_Actual_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Treasury_Actual_Date
        {
            get
            {
                return this._Treasury_Actual_Date;
            }
            set
            {
                if ((this._Treasury_Actual_Date != value))
                {
                    this._Treasury_Actual_Date = value;
                }
            }
        }
    }

    //public partial class TreasuryByRefill
    //{

    //    private System.Nullable<double> _Amount;

    //    private System.Nullable<double> _t2000p;

    //    private System.Nullable<double> _t1000p;

    //    private System.Nullable<double> _t500p;

    //    private System.Nullable<double> _t200p;

    //    private System.Nullable<double> _t100p;

    //    private System.Nullable<double> _t50p;

    //    private System.Nullable<double> _t20p;

    //    private System.Nullable<double> _t10p;

    //    private System.Nullable<double> _t5p;

    //    private System.Nullable<double> _t2p;

    //    public TreasuryByRefill()
    //    {
    //    }

    //    [Column(Storage = "_Amount", DbType = "Float")]
    //    public System.Nullable<double> Amount
    //    {
    //        get
    //        {
    //            return this._Amount;
    //        }
    //        set
    //        {
    //            if ((this._Amount != value))
    //            {
    //                this._Amount = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t2000p", DbType = "Float")]
    //    public System.Nullable<double> t2000p
    //    {
    //        get
    //        {
    //            return this._t2000p;
    //        }
    //        set
    //        {
    //            if ((this._t2000p != value))
    //            {
    //                this._t2000p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t1000p", DbType = "Float")]
    //    public System.Nullable<double> t1000p
    //    {
    //        get
    //        {
    //            return this._t1000p;
    //        }
    //        set
    //        {
    //            if ((this._t1000p != value))
    //            {
    //                this._t1000p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t500p", DbType = "Float")]
    //    public System.Nullable<double> t500p
    //    {
    //        get
    //        {
    //            return this._t500p;
    //        }
    //        set
    //        {
    //            if ((this._t500p != value))
    //            {
    //                this._t500p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t200p", DbType = "Float")]
    //    public System.Nullable<double> t200p
    //    {
    //        get
    //        {
    //            return this._t200p;
    //        }
    //        set
    //        {
    //            if ((this._t200p != value))
    //            {
    //                this._t200p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t100p", DbType = "Float")]
    //    public System.Nullable<double> t100p
    //    {
    //        get
    //        {
    //            return this._t100p;
    //        }
    //        set
    //        {
    //            if ((this._t100p != value))
    //            {
    //                this._t100p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t50p", DbType = "Float")]
    //    public System.Nullable<double> t50p
    //    {
    //        get
    //        {
    //            return this._t50p;
    //        }
    //        set
    //        {
    //            if ((this._t50p != value))
    //            {
    //                this._t50p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t20p", DbType = "Float")]
    //    public System.Nullable<double> t20p
    //    {
    //        get
    //        {
    //            return this._t20p;
    //        }
    //        set
    //        {
    //            if ((this._t20p != value))
    //            {
    //                this._t20p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t10p", DbType = "Float")]
    //    public System.Nullable<double> t10p
    //    {
    //        get
    //        {
    //            return this._t10p;
    //        }
    //        set
    //        {
    //            if ((this._t10p != value))
    //            {
    //                this._t10p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t5p", DbType = "Float")]
    //    public System.Nullable<double> t5p
    //    {
    //        get
    //        {
    //            return this._t5p;
    //        }
    //        set
    //        {
    //            if ((this._t5p != value))
    //            {
    //                this._t5p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t2p", DbType = "Float")]
    //    public System.Nullable<double> t2p
    //    {
    //        get
    //        {
    //            return this._t2p;
    //        }
    //        set
    //        {
    //            if ((this._t2p != value))
    //            {
    //                this._t2p = value;
    //            }
    //        }
    //    }
    //}

    //public partial class TreasuryByRefund
    //{

    //    private System.Nullable<double> _Amount;

    //    private System.Nullable<double> _t2000p;

    //    private System.Nullable<double> _t1000p;

    //    private System.Nullable<double> _t500p;

    //    private System.Nullable<double> _t200p;

    //    private System.Nullable<double> _t100p;

    //    private System.Nullable<double> _t50p;

    //    private System.Nullable<double> _t20p;

    //    private System.Nullable<double> _t10p;

    //    private System.Nullable<double> _t5p;

    //    private System.Nullable<double> _t2p;

    //    public TreasuryByRefund()
    //    {
    //    }

    //    [Column(Storage = "_Amount", DbType = "Float")]
    //    public System.Nullable<double> Amount
    //    {
    //        get
    //        {
    //            return this._Amount;
    //        }
    //        set
    //        {
    //            if ((this._Amount != value))
    //            {
    //                this._Amount = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t2000p", DbType = "Float")]
    //    public System.Nullable<double> t2000p
    //    {
    //        get
    //        {
    //            return this._t2000p;
    //        }
    //        set
    //        {
    //            if ((this._t2000p != value))
    //            {
    //                this._t2000p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t1000p", DbType = "Float")]
    //    public System.Nullable<double> t1000p
    //    {
    //        get
    //        {
    //            return this._t1000p;
    //        }
    //        set
    //        {
    //            if ((this._t1000p != value))
    //            {
    //                this._t1000p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t500p", DbType = "Float")]
    //    public System.Nullable<double> t500p
    //    {
    //        get
    //        {
    //            return this._t500p;
    //        }
    //        set
    //        {
    //            if ((this._t500p != value))
    //            {
    //                this._t500p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t200p", DbType = "Float")]
    //    public System.Nullable<double> t200p
    //    {
    //        get
    //        {
    //            return this._t200p;
    //        }
    //        set
    //        {
    //            if ((this._t200p != value))
    //            {
    //                this._t200p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t100p", DbType = "Float")]
    //    public System.Nullable<double> t100p
    //    {
    //        get
    //        {
    //            return this._t100p;
    //        }
    //        set
    //        {
    //            if ((this._t100p != value))
    //            {
    //                this._t100p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t50p", DbType = "Float")]
    //    public System.Nullable<double> t50p
    //    {
    //        get
    //        {
    //            return this._t50p;
    //        }
    //        set
    //        {
    //            if ((this._t50p != value))
    //            {
    //                this._t50p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t20p", DbType = "Float")]
    //    public System.Nullable<double> t20p
    //    {
    //        get
    //        {
    //            return this._t20p;
    //        }
    //        set
    //        {
    //            if ((this._t20p != value))
    //            {
    //                this._t20p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t10p", DbType = "Float")]
    //    public System.Nullable<double> t10p
    //    {
    //        get
    //        {
    //            return this._t10p;
    //        }
    //        set
    //        {
    //            if ((this._t10p != value))
    //            {
    //                this._t10p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t5p", DbType = "Float")]
    //    public System.Nullable<double> t5p
    //    {
    //        get
    //        {
    //            return this._t5p;
    //        }
    //        set
    //        {
    //            if ((this._t5p != value))
    //            {
    //                this._t5p = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_t2p", DbType = "Float")]
    //    public System.Nullable<double> t2p
    //    {
    //        get
    //        {
    //            return this._t2p;
    //        }
    //        set
    //        {
    //            if ((this._t2p != value))
    //            {
    //                this._t2p = value;
    //            }
    //        }
    //    }
    //}

    public partial class TreasuryByShortpay
    {

        private System.Nullable<double> _Amount;

        private System.Nullable<double> _t2000p;

        private System.Nullable<double> _t1000p;

        private System.Nullable<double> _t500p;

        private System.Nullable<double> _t200p;

        private System.Nullable<double> _t100p;

        private System.Nullable<double> _t50p;

        private System.Nullable<double> _t20p;

        private System.Nullable<double> _t10p;

        private System.Nullable<double> _t5p;

        private System.Nullable<double> _t2p;

        public TreasuryByShortpay()
        {
        }

        [Column(Storage = "_Amount", DbType = "Float")]
        public System.Nullable<double> Amount
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

        [Column(Storage = "_t2000p", DbType = "Float")]
        public System.Nullable<double> t2000p
        {
            get
            {
                return this._t2000p;
            }
            set
            {
                if ((this._t2000p != value))
                {
                    this._t2000p = value;
                }
            }
        }

        [Column(Storage = "_t1000p", DbType = "Float")]
        public System.Nullable<double> t1000p
        {
            get
            {
                return this._t1000p;
            }
            set
            {
                if ((this._t1000p != value))
                {
                    this._t1000p = value;
                }
            }
        }

        [Column(Storage = "_t500p", DbType = "Float")]
        public System.Nullable<double> t500p
        {
            get
            {
                return this._t500p;
            }
            set
            {
                if ((this._t500p != value))
                {
                    this._t500p = value;
                }
            }
        }

        [Column(Storage = "_t200p", DbType = "Float")]
        public System.Nullable<double> t200p
        {
            get
            {
                return this._t200p;
            }
            set
            {
                if ((this._t200p != value))
                {
                    this._t200p = value;
                }
            }
        }

        [Column(Storage = "_t100p", DbType = "Float")]
        public System.Nullable<double> t100p
        {
            get
            {
                return this._t100p;
            }
            set
            {
                if ((this._t100p != value))
                {
                    this._t100p = value;
                }
            }
        }

        [Column(Storage = "_t50p", DbType = "Float")]
        public System.Nullable<double> t50p
        {
            get
            {
                return this._t50p;
            }
            set
            {
                if ((this._t50p != value))
                {
                    this._t50p = value;
                }
            }
        }

        [Column(Storage = "_t20p", DbType = "Float")]
        public System.Nullable<double> t20p
        {
            get
            {
                return this._t20p;
            }
            set
            {
                if ((this._t20p != value))
                {
                    this._t20p = value;
                }
            }
        }

        [Column(Storage = "_t10p", DbType = "Float")]
        public System.Nullable<double> t10p
        {
            get
            {
                return this._t10p;
            }
            set
            {
                if ((this._t10p != value))
                {
                    this._t10p = value;
                }
            }
        }

        [Column(Storage = "_t5p", DbType = "Float")]
        public System.Nullable<double> t5p
        {
            get
            {
                return this._t5p;
            }
            set
            {
                if ((this._t5p != value))
                {
                    this._t5p = value;
                }
            }
        }

        [Column(Storage = "_t2p", DbType = "Float")]
        public System.Nullable<double> t2p
        {
            get
            {
                return this._t2p;
            }
            set
            {
                if ((this._t2p != value))
                {
                    this._t2p = value;
                }
            }
        }
    }

    [Table(Name = "dbo.VW_S_BatchData")]
    public partial class BatchDataView
    {

        private int _BatchNo;

        private string _BatchName;

        private System.Nullable<System.DateTime> _BatchDate;

        private System.Nullable<double> _BatchAdjustment;

        private string _BatchAdjustmentReason;

        private string _UserName;
        private System.Nullable<DateTime> _Collection_Batch_Date_Performed;

        public BatchDataView()
        {
        }

        [Column(Storage = "_Collection_Batch_Date_Performed", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Collection_Batch_Date_Performed
        {
            get
            {
                return this._Collection_Batch_Date_Performed;
            }
            set
            {
                if ((this._Collection_Batch_Date_Performed != value))
                {
                    this._Collection_Batch_Date_Performed = value;
                }
            }
        }

        [Column(Storage = "_BatchNo", DbType = "Int NOT NULL")]
        public int BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
                }
            }
        }

        [Column(Storage = "_BatchName", DbType = "VarChar(50)")]
        public string BatchName
        {
            get
            {
                return this._BatchName;
            }
            set
            {
                if ((this._BatchName != value))
                {
                    this._BatchName = value;
                }
            }
        }

        [Column(Storage = "_BatchDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> BatchDate
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

        [Column(Storage = "_BatchAdjustment", DbType = "Float")]
        public System.Nullable<double> BatchAdjustment
        {
            get
            {
                return this._BatchAdjustment;
            }
            set
            {
                if ((this._BatchAdjustment != value))
                {
                    this._BatchAdjustment = value;
                }
            }
        }

        [Column(Storage = "_BatchAdjustmentReason", DbType = "VarChar(255)")]
        public string BatchAdjustmentReason
        {
            get
            {
                return this._BatchAdjustmentReason;
            }
            set
            {
                if ((this._BatchAdjustmentReason != value))
                {
                    this._BatchAdjustmentReason = value;
                }
            }
        }

        [Column(Storage = "_UserName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }
    }

    [Table(Name = "dbo.VW_S_CollectionBatchData")]
    public partial class CollectionbatchDataView
    {

        private System.Nullable<int> _BatchNo;

        private System.Nullable<int> _WeekNo;

        private System.Nullable<int> _CollectionCount;

        private System.Nullable<double> _CashCollected;

        private System.Nullable<double> _Defloat;

        private System.Nullable<double> _GrossCash;

        private System.Nullable<double> _Refills;

        private System.Nullable<double> _Refunds;

        private System.Nullable<double> _Ticket;

        private System.Nullable<double> _NetCash;

        private System.Nullable<double> _CashTake;

        private System.Nullable<double> _RDC_Total_Coinage_Out;

        private System.Nullable<double> _RDCCash;

        private System.Nullable<double> _RDCRefill;

        private System.Nullable<double> _RDCVar;

        private System.Nullable<int> _MeterCash;

        private System.Nullable<int> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _VTP;

        private System.Nullable<double> _PercentageIn;

        private System.Nullable<double> _PercentageOut;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _HandpayVar;

        private System.Nullable<int> _MeterHandpay;

        private System.Nullable<double> _Declared_Total_Coinage;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _RDC_Coin;

        private System.Nullable<double> _Coin_Var;

        private System.Nullable<double> _Declared_Total_Notes;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<double> _Note_Var;

        private System.Nullable<double> _TicketsPrinted;

        private System.Nullable<double> _TicketBalance;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _RDCTicketBalance;

        private System.Nullable<double> _TicketVar;

        private System.Nullable<double> _RDCTake;

        private System.Nullable<double> _TakeVar;

        private System.Nullable<int> _TotalDurationPower;

        private System.Nullable<int> _TotalFaultEvents;

        private System.Nullable<int> _TotalPowerEvents;

        private System.Nullable<int> _TotalDoorEvents;

        private System.Nullable<double> _BatchAdjustment;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Expired;

        private System.Nullable<double> _Progressive_Value_Meter;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _RDC_TICKETS_PRINTED_NONCASHABLE_VALUE;

        private System.Nullable<double> _RDC_TICKETS_INSERTED_NONCASHABLE_VALUE;

        private System.Nullable<double> _Promo_Cashable_EFT_OUT;

        private System.Nullable<double> _NonCashable_EFT_OUT;

        private System.Nullable<double> _Cashable_EFT_OUT;

        private System.Nullable<double> _Promo_Cashable_EFT_IN;

        private System.Nullable<double> _NonCashable_EFT_IN;

        private System.Nullable<double> _Cashable_EFT_IN;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _EftOut;

        public CollectionbatchDataView()
        {
        }

        [Column(Storage = "_BatchNo", DbType = "Int")]
        public System.Nullable<int> BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
                }
            }
        }

        [Column(Storage = "_WeekNo", DbType = "Int")]
        public System.Nullable<int> WeekNo
        {
            get
            {
                return this._WeekNo;
            }
            set
            {
                if ((this._WeekNo != value))
                {
                    this._WeekNo = value;
                }
            }
        }

        [Column(Storage = "_CollectionCount", DbType = "Int")]
        public System.Nullable<int> CollectionCount
        {
            get
            {
                return this._CollectionCount;
            }
            set
            {
                if ((this._CollectionCount != value))
                {
                    this._CollectionCount = value;
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

        [Column(Storage = "_MeterCash", DbType = "Int")]
        public System.Nullable<int> MeterCash
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

        [Column(Storage = "_MeterRefill", DbType = "Int")]
        public System.Nullable<int> MeterRefill
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

        [Column(Storage = "_HandpayVar", DbType = "Float")]
        public System.Nullable<double> HandpayVar
        {
            get
            {
                return this._HandpayVar;
            }
            set
            {
                if ((this._HandpayVar != value))
                {
                    this._HandpayVar = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpay", DbType = "Int")]
        public System.Nullable<int> MeterHandpay
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

        [Column(Storage = "_Declared_Total_Coinage", DbType = "Float")]
        public System.Nullable<double> Declared_Total_Coinage
        {
            get
            {
                return this._Declared_Total_Coinage;
            }
            set
            {
                if ((this._Declared_Total_Coinage != value))
                {
                    this._Declared_Total_Coinage = value;
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

        [Column(Storage = "_RDC_Coin", DbType = "Float")]
        public System.Nullable<double> RDC_Coin
        {
            get
            {
                return this._RDC_Coin;
            }
            set
            {
                if ((this._RDC_Coin != value))
                {
                    this._RDC_Coin = value;
                }
            }
        }

        [Column(Storage = "_RDC_Total_Coinage_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Total_Coinage_Out
        {
            get
            {
                return this._RDC_Total_Coinage_Out;
            }
            set
            {
                if ((this._RDC_Total_Coinage_Out != value))
                {
                    this._RDC_Total_Coinage_Out = value;
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

        [Column(Storage = "_Promo_Cashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> Promo_Cashable_EFT_OUT
        {
            get
            {
                return this._Promo_Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_OUT != value))
                {
                    this._Promo_Cashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> NonCashable_EFT_OUT
        {
            get
            {
                return this._NonCashable_EFT_OUT;
            }
            set
            {
                if ((this._NonCashable_EFT_OUT != value))
                {
                    this._NonCashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_Cashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> Cashable_EFT_OUT
        {
            get
            {
                return this._Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Cashable_EFT_OUT != value))
                {
                    this._Cashable_EFT_OUT = value;
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

        [Column(Storage = "_Promo_Cashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> Promo_Cashable_EFT_IN
        {
            get
            {
                return this._Promo_Cashable_EFT_IN;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_IN != value))
                {
                    this._Promo_Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> NonCashable_EFT_IN
        {
            get
            {
                return this._NonCashable_EFT_IN;
            }
            set
            {
                if ((this._NonCashable_EFT_IN != value))
                {
                    this._NonCashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_Cashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> Cashable_EFT_IN
        {
            get
            {
                return this._Cashable_EFT_IN;
            }
            set
            {
                if ((this._Cashable_EFT_IN != value))
                {
                    this._Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_Declared_Total_Notes", DbType = "Float")]
        public System.Nullable<double> Declared_Total_Notes
        {
            get
            {
                return this._Declared_Total_Notes;
            }
            set
            {
                if ((this._Declared_Total_Notes != value))
                {
                    this._Declared_Total_Notes = value;
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

        [Column(Storage = "_TicketsPrinted", DbType = "Float")]
        public System.Nullable<double> TicketsPrinted
        {
            get
            {
                return this._TicketsPrinted;
            }
            set
            {
                if ((this._TicketsPrinted != value))
                {
                    this._TicketsPrinted = value;
                }
            }
        }

        [Column(Storage = "_TicketBalance", DbType = "Float")]
        public System.Nullable<double> TicketBalance
        {
            get
            {
                return this._TicketBalance;
            }
            set
            {
                if ((this._TicketBalance != value))
                {
                    this._TicketBalance = value;
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

        [Column(Storage = "_RDCTicketBalance", DbType = "Float")]
        public System.Nullable<double> RDCTicketBalance
        {
            get
            {
                return this._RDCTicketBalance;
            }
            set
            {
                if ((this._RDCTicketBalance != value))
                {
                    this._RDCTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_TicketVar", DbType = "Float")]
        public System.Nullable<double> TicketVar
        {
            get
            {
                return this._TicketVar;
            }
            set
            {
                if ((this._TicketVar != value))
                {
                    this._TicketVar = value;
                }
            }
        }

        [Column(Storage = "_RDCTake", DbType = "Float")]
        public System.Nullable<double> RDCTake
        {
            get
            {
                return this._RDCTake;
            }
            set
            {
                if ((this._RDCTake != value))
                {
                    this._RDCTake = value;
                }
            }
        }

        [Column(Storage = "_TakeVar", DbType = "Float")]
        public System.Nullable<double> TakeVar
        {
            get
            {
                return this._TakeVar;
            }
            set
            {
                if ((this._TakeVar != value))
                {
                    this._TakeVar = value;
                }
            }
        }

        [Column(Storage = "_TotalDurationPower", DbType = "Int")]
        public System.Nullable<int> TotalDurationPower
        {
            get
            {
                return this._TotalDurationPower;
            }
            set
            {
                if ((this._TotalDurationPower != value))
                {
                    this._TotalDurationPower = value;
                }
            }
        }

        [Column(Storage = "_TotalFaultEvents", DbType = "Int")]
        public System.Nullable<int> TotalFaultEvents
        {
            get
            {
                return this._TotalFaultEvents;
            }
            set
            {
                if ((this._TotalFaultEvents != value))
                {
                    this._TotalFaultEvents = value;
                }
            }
        }

        [Column(Storage = "_TotalPowerEvents", DbType = "Int")]
        public System.Nullable<int> TotalPowerEvents
        {
            get
            {
                return this._TotalPowerEvents;
            }
            set
            {
                if ((this._TotalPowerEvents != value))
                {
                    this._TotalPowerEvents = value;
                }
            }
        }

        [Column(Storage = "_TotalDoorEvents", DbType = "Int")]
        public System.Nullable<int> TotalDoorEvents
        {
            get
            {
                return this._TotalDoorEvents;
            }
            set
            {
                if ((this._TotalDoorEvents != value))
                {
                    this._TotalDoorEvents = value;
                }
            }
        }

        [Column(Storage = "_BatchAdjustment", DbType = "Float")]
        public System.Nullable<double> BatchAdjustment
        {
            get
            {
                return this._BatchAdjustment;
            }
            set
            {
                if ((this._BatchAdjustment != value))
                {
                    this._BatchAdjustment = value;
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
    }

    [Table(Name = "dbo.VW_S_CollectionWeekData")]
    public partial class VW_S_CollectionWeekData
    {

        private System.Nullable<int> _BatchNo;

        private System.Nullable<int> _WeekNo;

        private System.Nullable<int> _WeekNumber;

        private System.Nullable<System.DateTime> _WeekStartDate;

        private System.Nullable<System.DateTime> _WeekEndDate;

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

        private System.Nullable<int> _MeterCash;

        private System.Nullable<int> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _VTP;

        private System.Nullable<double> _PercentageIn;

        private System.Nullable<double> _PercentageOut;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<int> _MeterHandpay;

        private System.Nullable<double> _HandpayVar;

        private System.Nullable<double> _Declared_Total_Coinage;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _RDC_Coin;

        private System.Nullable<double> _RDC_Total_Coinage_Out;

        private System.Nullable<double> _Coin_Var;

        private System.Nullable<double> _Declared_Total_Notes;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<double> _Note_Var;

        private System.Nullable<double> _DeclaredTicketValue;

        private System.Nullable<int> _DeclaredTicketQty;

        private System.Nullable<double> _TicketsPrinted;

        private System.Nullable<double> _TicketBalance;

        private System.Nullable<double> _RDCTicketBalance;

        private System.Nullable<double> _TicketVar;

        private System.Nullable<double> _RDCTake;

        private System.Nullable<double> _TakeVar;

        private System.Nullable<int> _TotalDurationPower;

        private System.Nullable<int> _TotalFaultEvents;

        private System.Nullable<int> _TotalPowerEvents;

        private System.Nullable<int> _TotalDoorEvents;

        private System.Nullable<int> _CollectionCount;

        private System.Nullable<double> _BatchAdj;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Expired;

        private System.Nullable<double> _Progressive_Value_Meter;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _Promo_Cashable_EFT_IN;

        private System.Nullable<double> _Promo_Cashable_EFT_OUT;

        private System.Nullable<double> _NonCashable_EFT_IN;

        private System.Nullable<double> _NonCashable_EFT_OUT;

        private System.Nullable<double> _Cashable_EFT_IN;

        private System.Nullable<double> _Cashable_EFT_OUT;

        public VW_S_CollectionWeekData()
        {
        }

        [Column(Storage = "_BatchNo", DbType = "Int")]
        public System.Nullable<int> BatchNo
        {
            get
            {
                return this._BatchNo;
            }
            set
            {
                if ((this._BatchNo != value))
                {
                    this._BatchNo = value;
                }
            }
        }

        [Column(Storage = "_WeekNo", DbType = "Int")]
        public System.Nullable<int> WeekNo
        {
            get
            {
                return this._WeekNo;
            }
            set
            {
                if ((this._WeekNo != value))
                {
                    this._WeekNo = value;
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

        [Column(Storage = "_WeekStartDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> WeekStartDate
        {
            get
            {
                return this._WeekStartDate;
            }
            set
            {
                if ((this._WeekStartDate != value))
                {
                    this._WeekStartDate = value;
                }
            }
        }

        [Column(Storage = "_WeekEndDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> WeekEndDate
        {
            get
            {
                return this._WeekEndDate;
            }
            set
            {
                if ((this._WeekEndDate != value))
                {
                    this._WeekEndDate = value;
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

        [Column(Storage = "_MeterCash", DbType = "Int")]
        public System.Nullable<int> MeterCash
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

        [Column(Storage = "_MeterRefill", DbType = "Int")]
        public System.Nullable<int> MeterRefill
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

        [Column(Storage = "_MeterHandpay", DbType = "Int")]
        public System.Nullable<int> MeterHandpay
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

        [Column(Storage = "_HandpayVar", DbType = "Float")]
        public System.Nullable<double> HandpayVar
        {
            get
            {
                return this._HandpayVar;
            }
            set
            {
                if ((this._HandpayVar != value))
                {
                    this._HandpayVar = value;
                }
            }
        }

        [Column(Storage = "_Declared_Total_Coinage", DbType = "Float")]
        public System.Nullable<double> Declared_Total_Coinage
        {
            get
            {
                return this._Declared_Total_Coinage;
            }
            set
            {
                if ((this._Declared_Total_Coinage != value))
                {
                    this._Declared_Total_Coinage = value;
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
        
        [Column(Storage = "_RDC_Coin", DbType = "Float")]
        public System.Nullable<double> RDC_Coin
        {
            get
            {
                return this._RDC_Coin;
            }
            set
            {
                if ((this._RDC_Coin != value))
                {
                    this._RDC_Coin = value;
                }
            }
        }

        [Column(Storage = "_RDC_Total_Coinage_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Total_Coinage_Out
        {
            get
            {
                return this._RDC_Total_Coinage_Out;
            }
            set
            {
                if ((this._RDC_Total_Coinage_Out != value))
                {
                    this._RDC_Total_Coinage_Out = value;
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

        [Column(Storage = "_Declared_Total_Notes", DbType = "Float")]
        public System.Nullable<double> Declared_Total_Notes
        {
            get
            {
                return this._Declared_Total_Notes;
            }
            set
            {
                if ((this._Declared_Total_Notes != value))
                {
                    this._Declared_Total_Notes = value;
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

        [Column(Storage = "_DeclaredTicketValue", DbType = "Float")]
        public System.Nullable<double> DeclaredTicketValue
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

        [Column(Storage = "_DeclaredTicketQty", DbType = "Int")]
        public System.Nullable<int> DeclaredTicketQty
        {
            get
            {
                return this._DeclaredTicketQty;
            }
            set
            {
                if ((this._DeclaredTicketQty != value))
                {
                    this._DeclaredTicketQty = value;
                }
            }
        }

        [Column(Storage = "_TicketsPrinted", DbType = "Float")]
        public System.Nullable<double> TicketsPrinted
        {
            get
            {
                return this._TicketsPrinted;
            }
            set
            {
                if ((this._TicketsPrinted != value))
                {
                    this._TicketsPrinted = value;
                }
            }
        }

        [Column(Storage = "_TicketBalance", DbType = "Float")]
        public System.Nullable<double> TicketBalance
        {
            get
            {
                return this._TicketBalance;
            }
            set
            {
                if ((this._TicketBalance != value))
                {
                    this._TicketBalance = value;
                }
            }
        }

        [Column(Storage = "_RDCTicketBalance", DbType = "Float")]
        public System.Nullable<double> RDCTicketBalance
        {
            get
            {
                return this._RDCTicketBalance;
            }
            set
            {
                if ((this._RDCTicketBalance != value))
                {
                    this._RDCTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_TicketVar", DbType = "Float")]
        public System.Nullable<double> TicketVar
        {
            get
            {
                return this._TicketVar;
            }
            set
            {
                if ((this._TicketVar != value))
                {
                    this._TicketVar = value;
                }
            }
        }

        [Column(Storage = "_RDCTake", DbType = "Float")]
        public System.Nullable<double> RDCTake
        {
            get
            {
                return this._RDCTake;
            }
            set
            {
                if ((this._RDCTake != value))
                {
                    this._RDCTake = value;
                }
            }
        }

        [Column(Storage = "_TakeVar", DbType = "Float")]
        public System.Nullable<double> TakeVar
        {
            get
            {
                return this._TakeVar;
            }
            set
            {
                if ((this._TakeVar != value))
                {
                    this._TakeVar = value;
                }
            }
        }

        [Column(Storage = "_TotalDurationPower", DbType = "Int")]
        public System.Nullable<int> TotalDurationPower
        {
            get
            {
                return this._TotalDurationPower;
            }
            set
            {
                if ((this._TotalDurationPower != value))
                {
                    this._TotalDurationPower = value;
                }
            }
        }

        [Column(Storage = "_TotalFaultEvents", DbType = "Int")]
        public System.Nullable<int> TotalFaultEvents
        {
            get
            {
                return this._TotalFaultEvents;
            }
            set
            {
                if ((this._TotalFaultEvents != value))
                {
                    this._TotalFaultEvents = value;
                }
            }
        }

        [Column(Storage = "_TotalPowerEvents", DbType = "Int")]
        public System.Nullable<int> TotalPowerEvents
        {
            get
            {
                return this._TotalPowerEvents;
            }
            set
            {
                if ((this._TotalPowerEvents != value))
                {
                    this._TotalPowerEvents = value;
                }
            }
        }

        [Column(Storage = "_TotalDoorEvents", DbType = "Int")]
        public System.Nullable<int> TotalDoorEvents
        {
            get
            {
                return this._TotalDoorEvents;
            }
            set
            {
                if ((this._TotalDoorEvents != value))
                {
                    this._TotalDoorEvents = value;
                }
            }
        }

        [Column(Storage = "_CollectionCount", DbType = "Int")]
        public System.Nullable<int> CollectionCount
        {
            get
            {
                return this._CollectionCount;
            }
            set
            {
                if ((this._CollectionCount != value))
                {
                    this._CollectionCount = value;
                }
            }
        }

        [Column(Storage = "_BatchAdj", DbType = "Float")]
        public System.Nullable<double> BatchAdj
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

        [Column(Storage = "_Promo_Cashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> Promo_Cashable_EFT_IN
        {
            get
            {
                return this._Promo_Cashable_EFT_IN;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_IN != value))
                {
                    this._Promo_Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_Promo_Cashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> Promo_Cashable_EFT_OUT
        {
            get
            {
                return this._Promo_Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Promo_Cashable_EFT_OUT != value))
                {
                    this._Promo_Cashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> NonCashable_EFT_IN
        {
            get
            {
                return this._NonCashable_EFT_IN;
            }
            set
            {
                if ((this._NonCashable_EFT_IN != value))
                {
                    this._NonCashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_NonCashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> NonCashable_EFT_OUT
        {
            get
            {
                return this._NonCashable_EFT_OUT;
            }
            set
            {
                if ((this._NonCashable_EFT_OUT != value))
                {
                    this._NonCashable_EFT_OUT = value;
                }
            }
        }

        [Column(Storage = "_Cashable_EFT_IN", DbType = "Float")]
        public System.Nullable<double> Cashable_EFT_IN
        {
            get
            {
                return this._Cashable_EFT_IN;
            }
            set
            {
                if ((this._Cashable_EFT_IN != value))
                {
                    this._Cashable_EFT_IN = value;
                }
            }
        }

        [Column(Storage = "_Cashable_EFT_OUT", DbType = "Float")]
        public System.Nullable<double> Cashable_EFT_OUT
        {
            get
            {
                return this._Cashable_EFT_OUT;
            }
            set
            {
                if ((this._Cashable_EFT_OUT != value))
                {
                    this._Cashable_EFT_OUT = value;
                }
            }
        }
    }

    #region Custom classes
    public partial class AllEvents
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
    }

    public partial class BatchBreakdownDetails
    {
        public string CollectionDate { get; set; }
        public string Zone { get; set; }
        public string Position { get; set; }
        public string Machine { get; set; }
        public string GamingDay { get; set; }
        public string UserName { get; set; }
        public DateTime BreakdownDate { get; set; }
        public DateTime BreakdownTime { get; set; }
        public string BreakdownUser { get; set; }
        public string AssetNo { get; set; }
    }

    public partial class BatchDetails
    {
        public string BatchNo { get; set; }
        public DateTime BatchDate { get; set; }
        public DateTime BatchEndDate { get; set; }
        public DateTime CollectionDate { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public decimal GrossCoin { get; set; }
        public decimal FloatRec { get; set; }
        public decimal RefillsSum { get; set; }
        public decimal RefundsSum { get; set; }
        public decimal ProgressiveSum { get; set; }
        public decimal Shortpay { get; set; }
        public decimal NetCoinSum { get; set; }
        public decimal NotesSum { get; set; }
        public decimal HandpaySum { get; set; }
        public decimal TicketBalanceSum { get; set; }
        public decimal CashTakeSum { get; set; }
        public decimal CoinVarSum { get; set; }
        public decimal ProgressiveVarSum { get; set; }
        public decimal NotesVarSum { get; set; }
        public decimal TicketsVarSum { get; set; }
        public decimal HandpayVarSum { get; set; }
        public decimal PayoutSum { get; set; }
        public decimal HoldSum { get; set; }
        public decimal dPayoutSum { get; set; }
        public decimal dHoldSum { get; set; }
        public decimal VTPSum { get; set; }
        public int NoofCollections { get; set; }
        public DateTime? WeekEndDate { get; set; }
        public string BatchName { get; set; }
        public decimal WinLossSum { get; set; }

    }

    public partial class CollectionListView
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
        public decimal V1000 { get; set; }
        public decimal V500 { get; set; }
        public decimal V200 { get; set; }
        public decimal V100 { get; set; }
        public decimal V50 { get; set; }
        public decimal V20 { get; set; }
        public decimal V10 { get; set; }
        public decimal V5 { get; set; }
        public decimal V2 { get; set; }
        public decimal V1 { get; set; }
        public decimal V50p { get; set; }
        public decimal V20p { get; set; }
        public decimal V10P { get; set; }
        public decimal TotalCoins { get; set; }
        public decimal TicketsIn { get; set; }
        public decimal TicketsOut { get; set; }
        public decimal PromoCashableValue { get; set; }
        public decimal PromoNonCashableValue { get; set; }
        public decimal CoinsIn { get; set; }
        public decimal CoinsOut { get; set; }
        public decimal Tickets { get; set; }
        public decimal Handpay { get; set; }
        public decimal Progressive { get; set; }
        public decimal EFTIn { get; set;}
        public decimal EFTOut { get; set; }
        public decimal EFT { get; set; } 
        public bool IsMatch { get; set; }
    }

    public partial class BatchHistoryListView
    {
        public string CollectionKey { get; set; }
        public string Zone { get; set; }
        public string Pos { get; set; }
        public string Asset { get; set; }
        public string Game { get; set; }
        public double WinLoss { get; set; }
        public double WinLossMeter { get; set; }
        public double WinLossVar { get; set; }
        public double GrossCoin { get; set; }
        public float FloatRec { get; set; }
        public double Refills { get; set; }
        public float Refunds { get; set; }
        public double CoinNet { get; set; }
        public double CoinMeter { get; set; }
        public double CoinVar { get; set; }
        public double Bills { get; set; }
        public double BillsMeter { get; set; }
        public double BillsVar { get; set; }
        public double Tickets { get; set; }
        public double Shortpay { get; set; }
        public double VoidTicket { get; set; }
        public double TicketsMeter { get; set; }
        public double TicketsVar { get; set; }
        public double? Handpay { get; set; }
        public string HandpayNonTruncated { get; set; }
        public double HandpayMeter { get; set; }
        public double HandpayVar { get; set; }
        public double Progressive { get; set; }
        public double ProgressiveMeter { get; set; }
        public double ProgressiveVar { get; set; }
        public double Handle { get; set; }
        public double PercentPayout { get; set; }
        public double PercentHold { get; set; }
        public int Faults { get; set; }
        public int Door { get; set; }
        public int Power { get; set; }
        public DateTime WeekEndDate { get; set; }

    }

    public partial class rsp_AssetVarianceHistoryResult
    {

        private String _gaming_day=String.Empty;

        private System.Nullable<double> _coin_Var;

        private System.Nullable<double> _note_var;

        private System.Nullable<double> _ticket_in_var;

        private System.Nullable<double> _ticket_out_var;

        private System.Nullable<double> _HandPay_Var;

        private System.Nullable<double> _Prog_Var;

        private System.Nullable<double> _Total_Var;

        private System.Nullable<double> _EftIn_var;

        private System.Nullable<double> _EftOut_var;

        public rsp_AssetVarianceHistoryResult()
        {
        }

        [Column(Storage = "_gaming_day", DbType = "DateTime")]
        public String gaming_day
        {
            get
            {
                this._gaming_day = this._gaming_day.Split(' ').First();

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

        [Column(Storage = "_coin_Var", DbType = "Float")]
        public System.Nullable<double> coin_Var
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

        [Column(Storage = "_note_var", DbType = "Float")]
        public System.Nullable<double> note_var
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

        [Column(Storage = "_HandPay_Var", DbType = "Float")]
        public System.Nullable<double> HandPay_Var
        {
            get
            {
                return this._HandPay_Var;
            }
            set
            {
                if ((this._HandPay_Var != value))
                {
                    this._HandPay_Var = value;
                }
            }
        }

        [Column(Storage = "_Prog_Var", DbType = "Float")]
        public System.Nullable<double> Prog_Var
        {
            get
            {
                return this._Prog_Var;
            }
            set
            {
                if ((this._Prog_Var != value))
                {
                    this._Prog_Var = value;
                }
            }
        }

        [Column(Storage = "_Total_Var", DbType = "Float")]
        public System.Nullable<double> Total_Var
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

        
    }
    #endregion
}
