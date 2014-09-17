using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class CollectionBreakDowndetailsResult
    {

        private string _Batch_User_Name;

        private string _Batch_Date;

        private string _Batch_Time;

        private string _Batch_Date_Performed;

        private string _Batch_Ref;

        private string _Bar_Position_Name;
        
        private string _Installation_ID;

        private int _Installation_Price_Per_Play;

        private int _Installation_Counter_Cash_In_Units;

        private int _Installation_Counter_Cash_Out_Units;

        private int _Installation_Counter_Refill_Units;

        private string _Zone_Name;

        private int _Machine_Class_SP_Features;

        private string _Machine_Name;

        private string _GameName;

        private float _CashCollected;

        private float _Collection_Treasury_Defloat;

        private float _CashRefills;

        private float _Collection_Sundries_Unsupported;

        private float _Collection_Sundries;

        private int _CASH_IN_1P;

        private int _CASH_IN_2P;

        private System.Nullable<int>  _CASH_IN_5P;

        private int _CASH_IN_10P;

        private int _CASH_IN_20P;

        private int _CASH_IN_50P;

        private int _CASH_IN_100P;

        private int _CASH_IN_200P;

        private int _CASH_IN_500P;

        private int _CASH_IN_1000P;

        private int _CASH_IN_2000P;

        private int _CASH_IN_5000P;

        private int _CASH_IN_10000P;

        private int _CASH_IN_20000P;

        private int _CASH_IN_50000P;

        private int _CASH_IN_100000P;

        private int _CASH_OUT_1P;
        
        private int _CASH_OUT_2P;

        private System.Nullable<int> _CASH_OUT_5P;

        private int _CASH_OUT_10P;

        private int _CASH_OUT_20P;

        private int _CASH_OUT_50P;

        private int _CASH_OUT_100P;

        private int _CASH_OUT_200P;

        private int _CASH_OUT_500P;

        private int _CASH_OUT_1000P;

        private int _CASH_OUT_2000P;

        private int _CASH_OUT_5000P;

        private int _CASH_OUT_10000P;

        private int _CASH_OUT_20000P;

        private int _CASH_OUT_50000P;

        private int _CASH_OUT_100000P;

        private int _CASH_REFILL_5P;

        private int _CASH_REFILL_10P;

        private int _CASH_REFILL_20P;

        private int _CASH_REFILL_50P;

        private int _CASH_REFILL_100P;

        private int _CASH_REFILL_200P;

        private int _CASH_REFILL_500P;

        private int _CASH_REFILL_1000P;

        private int _CASH_REFILL_2000P;

        private int _CASH_REFILL_5000P;

        private int _CASH_REFILL_10000P;

        private int _CASH_REFILL_20000P;

        private int _CASH_REFILL_50000P;

        private int _CASH_REFILL_100000P;

        private int _CounterCashIn;

        private int _PreviousCounterCashIn;

        private int _CounterCashOut;

        private int _PreviousCounterCashOut;

        private int _CounterRefill;

        private int _PreviousCounterRefills;

        private int _Collection_Meters_CoinsIn;

        private int _Previous_Meters_Coins_In;

        private int _Collection_Meters_CoinsOut;

        private int _Previous_Meters_Coins_Out;

        private float _Collection_Treasury_Handpay;

        private int _COLLECTION_RDC_COINSIN;

        private int _COLLECTION_RDC_COINSOUT;

        private int _COLLECTION_RDC_VTP;

        private int _COLLECTION_RDC_HANDPAY;

        private int _Collection_Meters_Handpay;

        private int _Previous_Meters_Handpay;

        private string _Collection_Date_Of_Collection;

        public CollectionBreakDowndetailsResult()
        {
        }


        public string PrivUserName
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


        public string PrivPositionName
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

        public string Installation_ID
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


        
        
        public int Installation_Price_Per_Play
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


        public string PrivZoneName
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


        public string PrivMachineName
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

        
        public float Collection_Treasury_Defloat
        {
            get
            {
                return this._Collection_Treasury_Defloat;
            }
            set
            {
                if ((this._Collection_Treasury_Defloat != value))
                {
                    this._Collection_Treasury_Defloat = value;
                }
            }
        }

        
        public float CashRefills
        {
            get
            {
                return this._CashRefills;
            }
            set
            {
                if ((this._CashRefills != value))
                {
                    this._CashRefills = value;
                }
            }
        }

        
        public float Collection_Sundries_Unsupported
        {
            get
            {
                return this._Collection_Sundries_Unsupported;
            }
            set
            {
                if ((this._Collection_Sundries_Unsupported != value))
                {
                    this._Collection_Sundries_Unsupported = value;
                }
            }
        }

        
        public float Collection_Sundries
        {
            get
            {
                return this._Collection_Sundries;
            }
            set
            {
                if ((this._Collection_Sundries != value))
                {
                    this._Collection_Sundries = value;
                }
            }
        }

        public int CASH_IN_1P
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
        
        public int CASH_IN_2P
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

        
        public System.Nullable<int>  CASH_IN_5P
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

        
        public int CASH_IN_10P
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

        
        public int CASH_IN_20P
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

        
        public int CASH_IN_50P
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

        
        public int CASH_IN_100P
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

        
        public int CASH_IN_200P
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

        
        public int CASH_IN_500P
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

        
        public int CASH_IN_1000P
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

      
        public int CASH_IN_2000P
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

   
        public int CASH_IN_5000P
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

        
        public int CASH_IN_10000P
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

        
        public int CASH_IN_20000P
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

        
        public int CASH_IN_50000P
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

        
        public int CASH_IN_100000P
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

        public int CASH_OUT_1P
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

        public int CASH_OUT_2P
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

        
        public int CASH_OUT_10P
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

        
        public int CASH_OUT_20P
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

        
        public int CASH_OUT_50P
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

        
        public int CASH_OUT_100P
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

        
        public int CASH_OUT_200P
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

        
        public int CASH_OUT_500P
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

        
        public int CASH_OUT_1000P
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

        
        public int CASH_OUT_2000P
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

        
        public int CASH_OUT_5000P
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

        
        public int CASH_OUT_10000P
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

        
        public int CASH_OUT_20000P
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

        
        public int CASH_OUT_50000P
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

        
        public int CASH_OUT_100000P
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

        
        public int CASH_REFILL_5P
        {
            get
            {
                return this._CASH_REFILL_5P;
            }
            set
            {
                if ((this._CASH_REFILL_5P != value))
                {
                    this._CASH_REFILL_5P = value;
                }
            }
        }

        
        public int CASH_REFILL_10P
        {
            get
            {
                return this._CASH_REFILL_10P;
            }
            set
            {
                if ((this._CASH_REFILL_10P != value))
                {
                    this._CASH_REFILL_10P = value;
                }
            }
        }

        
        public int CASH_REFILL_20P
        {
            get
            {
                return this._CASH_REFILL_20P;
            }
            set
            {
                if ((this._CASH_REFILL_20P != value))
                {
                    this._CASH_REFILL_20P = value;
                }
            }
        }

        
        public int CASH_REFILL_50P
        {
            get
            {
                return this._CASH_REFILL_50P;
            }
            set
            {
                if ((this._CASH_REFILL_50P != value))
                {
                    this._CASH_REFILL_50P = value;
                }
            }
        }

        
        public int CASH_REFILL_100P
        {
            get
            {
                return this._CASH_REFILL_100P;
            }
            set
            {
                if ((this._CASH_REFILL_100P != value))
                {
                    this._CASH_REFILL_100P = value;
                }
            }
        }

        
        public int CASH_REFILL_200P
        {
            get
            {
                return this._CASH_REFILL_200P;
            }
            set
            {
                if ((this._CASH_REFILL_200P != value))
                {
                    this._CASH_REFILL_200P = value;
                }
            }
        }

        
        public int CASH_REFILL_500P
        {
            get
            {
                return this._CASH_REFILL_500P;
            }
            set
            {
                if ((this._CASH_REFILL_500P != value))
                {
                    this._CASH_REFILL_500P = value;
                }
            }
        }

        
        public int CASH_REFILL_1000P
        {
            get
            {
                return this._CASH_REFILL_1000P;
            }
            set
            {
                if ((this._CASH_REFILL_1000P != value))
                {
                    this._CASH_REFILL_1000P = value;
                }
            }
        }

        
        public int CASH_REFILL_2000P
        {
            get
            {
                return this._CASH_REFILL_2000P;
            }
            set
            {
                if ((this._CASH_REFILL_2000P != value))
                {
                    this._CASH_REFILL_2000P = value;
                }
            }
        }

        
        public int CASH_REFILL_5000P
        {
            get
            {
                return this._CASH_REFILL_5000P;
            }
            set
            {
                if ((this._CASH_REFILL_5000P != value))
                {
                    this._CASH_REFILL_5000P = value;
                }
            }
        }

        
        public int CASH_REFILL_10000P
        {
            get
            {
                return this._CASH_REFILL_10000P;
            }
            set
            {
                if ((this._CASH_REFILL_10000P != value))
                {
                    this._CASH_REFILL_10000P = value;
                }
            }
        }

        
        public int CASH_REFILL_20000P
        {
            get
            {
                return this._CASH_REFILL_20000P;
            }
            set
            {
                if ((this._CASH_REFILL_20000P != value))
                {
                    this._CASH_REFILL_20000P = value;
                }
            }
        }

        
        public int CASH_REFILL_50000P
        {
            get
            {
                return this._CASH_REFILL_50000P;
            }
            set
            {
                if ((this._CASH_REFILL_50000P != value))
                {
                    this._CASH_REFILL_50000P = value;
                }
            }
        }

        
        public int CASH_REFILL_100000P
        {
            get
            {
                return this._CASH_REFILL_100000P;
            }
            set
            {
                if ((this._CASH_REFILL_100000P != value))
                {
                    this._CASH_REFILL_100000P = value;
                }
            }
        }

        
        public int CounterCashIn
        {
            get
            {
                return this._CounterCashIn;
            }
            set
            {
                if ((this._CounterCashIn != value))
                {
                    this._CounterCashIn = value;
                }
            }
        }

        
        public int PreviousCounterCashIn
        {
            get
            {
                return this._PreviousCounterCashIn;
            }
            set
            {
                if ((this._PreviousCounterCashIn != value))
                {
                    this._PreviousCounterCashIn = value;
                }
            }
        }

        
        public int CounterCashOut
        {
            get
            {
                return this._CounterCashOut;
            }
            set
            {
                if ((this._CounterCashOut != value))
                {
                    this._CounterCashOut = value;
                }
            }
        }

        
        public int PreviousCounterCashOut
        {
            get
            {
                return this._PreviousCounterCashOut;
            }
            set
            {
                if ((this._PreviousCounterCashOut != value))
                {
                    this._PreviousCounterCashOut = value;
                }
            }
        }

        
        public int CounterRefill
        {
            get
            {
                return this._CounterRefill;
            }
            set
            {
                if ((this._CounterRefill != value))
                {
                    this._CounterRefill = value;
                }
            }
        }

        
        public int PreviousCounterRefills
        {
            get
            {
                return this._PreviousCounterRefills;
            }
            set
            {
                if ((this._PreviousCounterRefills != value))
                {
                    this._PreviousCounterRefills = value;
                }
            }
        }

        
        public int Collection_Meters_CoinsIn
        {
            get
            {
                return this._Collection_Meters_CoinsIn;
            }
            set
            {
                if ((this._Collection_Meters_CoinsIn != value))
                {
                    this._Collection_Meters_CoinsIn = value;
                }
            }
        }

        
        public int Previous_Meters_Coins_In
        {
            get
            {
                return this._Previous_Meters_Coins_In;
            }
            set
            {
                if ((this._Previous_Meters_Coins_In != value))
                {
                    this._Previous_Meters_Coins_In = value;
                }
            }
        }

        
        public int Collection_Meters_CoinsOut
        {
            get
            {
                return this._Collection_Meters_CoinsOut;
            }
            set
            {
                if ((this._Collection_Meters_CoinsOut != value))
                {
                    this._Collection_Meters_CoinsOut = value;
                }
            }
        }

        
        public int Previous_Meters_Coins_Out
        {
            get
            {
                return this._Previous_Meters_Coins_Out;
            }
            set
            {
                if ((this._Previous_Meters_Coins_Out != value))
                {
                    this._Previous_Meters_Coins_Out = value;
                }
            }
        }

        
        public float Collection_Treasury_Handpay
        {
            get
            {
                return this._Collection_Treasury_Handpay;
            }
            set
            {
                if ((this._Collection_Treasury_Handpay != value))
                {
                    this._Collection_Treasury_Handpay = value;
                }
            }
        }

        
        public int COLLECTION_RDC_COINSIN
        {
            get
            {
                return this._COLLECTION_RDC_COINSIN;
            }
            set
            {
                if ((this._COLLECTION_RDC_COINSIN != value))
                {
                    this._COLLECTION_RDC_COINSIN = value;
                }
            }
        }

        
        public int COLLECTION_RDC_COINSOUT
        {
            get
            {
                return this._COLLECTION_RDC_COINSOUT;
            }
            set
            {
                if ((this._COLLECTION_RDC_COINSOUT != value))
                {
                    this._COLLECTION_RDC_COINSOUT = value;
                }
            }
        }

        
        public int COLLECTION_RDC_VTP
        {
            get
            {
                return this._COLLECTION_RDC_VTP;
            }
            set
            {
                if ((this._COLLECTION_RDC_VTP != value))
                {
                    this._COLLECTION_RDC_VTP = value;
                }
            }
        }

        
        public int COLLECTION_RDC_HANDPAY
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

        
        public int Collection_Meters_Handpay
        {
            get
            {
                return this._Collection_Meters_Handpay;
            }
            set
            {
                if ((this._Collection_Meters_Handpay != value))
                {
                    this._Collection_Meters_Handpay = value;
                }
            }
        }

        
        public int Previous_Meters_Handpay
        {
            get
            {
                return this._Previous_Meters_Handpay;
            }
            set
            {
                if ((this._Previous_Meters_Handpay != value))
                {
                    this._Previous_Meters_Handpay = value;
                }
            }
        }

        
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

        public float PrivCashCollected { get; set; }
        public float PrivFloatRec { get; set; }
        public float PrivGrossCash { get; set; }
        public float PrivRefills { get; set; }
        public float PrivRefunds { get; set; }
        public float PrivNetCash { get; set; }
        public float PrivRDCCashIn { get; set; }
        public float PrivRDCCashOut { get; set; }
        public string  PrivRDCRefills { get; set; }
       
        public string  PrivRDCCash { get; set; }

        public float PrivRDCVar { get; set; }




        public float PrivMeterCashIn { get; set; }
        public float PrivMeterCashOut { get; set; }
        public float PrivMeterRefills { get; set; }
        public float PrivMeterCash { get; set; }

        public float PrivMeterCoinIn { get; set; }
        public float PrivMeterCoinOut { get; set; }


        public float PrivMeterVar { get; set; }
        public float PrivDecHandpay { get; set; }
        public string PrivRDCHandpay { get; set; }
        public string PrivRDCHandpayVar { get; set; }

       
        public float PrivMeterHandpay { get; set; }
        public float PrivMeterHandpayVar { get; set; }
        public bool PrivSingleCoin { get; set; }

    }

    public partial class CollectionBreakDown
    {

        private string _Batch_User_Name;

        private string _Batch_Date;

        private string _Batch_Time;

        private string _Batch_Date_Performed;

        private string _Batch_Ref;

        private string _Collection_Date_Of_Collection;

        private string _Collection_Defloat_Collection;

        private string _Bar_Position_Name;

        private int _Installation_Price_Per_Play;

        private int _Installation_Counter_Cash_In_Units;

        private int _Installation_Counter_Cash_Out_Units;

        private int _Installation_Counter_Refill_Units;

        private int _Machine_Class_SP_Features;

        private string _Machine_Name;

        private string _GameName;

        private string _Zone_Name;

        private double _Collection_Treasury_Handpay_float;

        private decimal _nCoinsOut;

        private decimal _RDCCash;

        private decimal _RDCCashIn;

        private decimal _RDCCashOut;

        private float _Collections;

        private float _CashCollected;

        private decimal _Cash_Collected_100000p;

        private decimal _Cash_Collected_50000p;

        private decimal _Cash_Collected_20000p;

        private decimal _Cash_Collected_10000p;

        private int _Cash_Collected_5000p;

        private int _Cash_Collected_2000p;

        private int _Cash_Collected_1000p;

        private decimal _Cash_Collected_500p;

        private decimal _Cash_Collected_200p;

        private decimal _Cash_Collected_100p;

        private decimal _COL_COINSIN;

        private float _DeclaredTicketValue;

        private double _COL_TICKETSOUT;

        private double _COL_TICKETS;

        private double _COL_PROG;

        private double _COL_EFTOUT;

        private double _COL_EFT;

        private double _FR_COL_TOTAL;

        private double _FR_COL_20;

        private double _FR_COL_10;

        private double _FR_COL_5;

        private double _FR_COL_2;

        private double _FR_COL_1;

        private double _FR_COL_TOTALCOINS;

        private decimal _Refills;

        private int _RF_COL_1000;

        private int _RF_COL_500;

        private int _RF_COL_200;

        private int _RF_COL_100;

        private int _RF_COL_50;

        private double _RF_COL_20;

        private double _RF_COL_10;

        private double _RF_COL_5;

        private double _Short_Pay;

        private double _TicketVoid;

        private int _RDC_COL_1000;

        private int _COL_500;

        private int _COL_200;

        private int _COL_100;

        private int _COL_50;

        private int _COL_20;

        private int _COL_10;

        private int _COL_5;

        private int _RDC__COL_2;

        private int _RDC_COL_1;

        private decimal _RDC_COL_TOTALCOINS;

        private decimal _RDC_COL_COINSIN;

        private decimal _RDC_COL_TICKETSIN;

        private decimal _RDC_COL_TICKETSOUT;

        private decimal _RDC_COL_TICKETS;

        private decimal _RDC_COL_HANDPAY;

        private decimal _RDC_COL_PROG;

        private decimal _RDC_COL_EFTIN;

        private decimal _RDC_COL_EFTOUT;

        private decimal _RDC_COL_EFT;

        private double _CASH_IN_1P;

        private double _CASH_OUT_1P;

        private string _Setting_SVGIEnabled;

        private string _Setting_Region;

        private string _Setting_AddShortpay;

        private string _Setting_Auto_Declare_Monies;

        private string _Setting_IsAFTIncludedInCalculation;

        private decimal _COL_PromoCashableIn;

        private decimal _COL_PromoNonCashableIn;


        private decimal _RDC_COL_PromoCashableIn;

        private decimal _RDC_COL_PromoNonCashableIn;

        private string _asset;

        private string _Declaredby;

        public CollectionBreakDown()
        {
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

        public int Installation_Id { get;  set; }

        public int Installation_Price_Per_Play
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

        
        public double Collection_Treasury_Handpay_float
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

        
        public decimal nCoinsOut
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

        
        public decimal RDCCash
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

        
        public decimal RDCCashIn
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

        
        public decimal RDCCashOut
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

        
        public float Collections
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

        
        public decimal Cash_Collected_100000p
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

        
        public decimal Cash_Collected_50000p
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

        
        public decimal Cash_Collected_20000p
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

        
        public decimal Cash_Collected_10000p
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

        
        public int Cash_Collected_5000p
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

        
        public int Cash_Collected_2000p
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

        
        public int Cash_Collected_1000p
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

        
        public decimal Cash_Collected_500p
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

        
        public decimal Cash_Collected_200p
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

        
        public decimal Cash_Collected_100p
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

        
        public decimal COL_COINSIN
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

        
        public double COL_TICKETSOUT
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

        
        public double COL_TICKETS
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

        
        public double COL_EFTOUT
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

        
        public double COL_EFT
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

        
        public decimal Refills
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

        
        public int RF_COL_1000
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

        
        public int RF_COL_500
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

        
        public int RF_COL_200
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

        
        public int RF_COL_100
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

        
        public int RF_COL_50
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

        
        public double RF_COL_20
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

        
        public double RF_COL_10
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

        
        public double RF_COL_5
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

        
        public double Short_Pay
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

        
        public double TicketVoid
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

        
        public int RDC_COL_1000
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

        
        public int COL_500
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

        
        public int COL_200
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

        
        public int COL_100
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

        
        public int COL_50
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

        
        public int COL_20
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

        
        public int COL_10
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

        
        public int COL_5
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

        
        public int RDC__COL_2
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

        
        public int RDC_COL_1
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

        
        public decimal RDC_COL_TOTALCOINS
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


        public decimal RDC_COL_COINSIN
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

        
        public decimal RDC_COL_TICKETSIN
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

        
        public decimal RDC_COL_TICKETSOUT
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

        
        public decimal RDC_COL_TICKETS
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

        
        public decimal RDC_COL_HANDPAY
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


        public decimal RDC_COL_PROG
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

        
        public decimal RDC_COL_EFTIN
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

        
        public decimal RDC_COL_EFTOUT
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

        
        public decimal RDC_COL_EFT
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

        
        public double CASH_IN_1P
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

        
        public double CASH_OUT_1P
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

        public decimal COL_PromoCashableIn
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

        public decimal COL_PromoNonCashableIn
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



        public decimal RDC_COL_PromoCashableIn
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



        public decimal RDC_COL_PromoNonCashableIn
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

        public String Asset
        {
            get
            {
                return this._asset;
            }
            set
            {
                if ((this._asset != value))
                {
                    this._asset = value;
                }
            }
        }

        public String Declaredby
        {
            get { return this._Declaredby; }

            set
            {
                if ((this._Declaredby != value))
                {
                    this._Declaredby = value;
                } 

            }
        }


    }

    public partial class AssetVarianceHistory
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

        public AssetVarianceHistory()
        {
        }

        public string Gaming_Day
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

        public string Collection_Day
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

        public System.Nullable<float> Coin_Var
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

        public float Note_Var
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

        public System.Nullable<double> Ticket_In_Var
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

        public System.Nullable<double> Ticket_Out_Var
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

        public System.Nullable<double> Handpay_Var
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

        public System.Nullable<double> EftIn_Var
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

        public System.Nullable<double> EftOut_Var
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

    public partial class TreasuryDetails
    {

        private string _Treasury_Type;

        private string _Treasury_Date;

        private string _Treasury_Time;

        private System.Nullable<double> _Treasury_Amount;

        private string _Treasury_User;

        private string _Treasury_Issued_User;

        private string _Treasury_Reason;

        public TreasuryDetails()
        {
        }

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


    public partial class DeclaredCollection
    {

        private int _Batch_ID;

        private int _Site_ID;

        private double _Cash_Collected_100p;

        private double _Cash_Collected_200p;

        private double _Cash_Collected_500p;

        private double _Cash_Collected_1000p;

        private double _Cash_Collected_2000p;
        private double _Cash_Collected_5000p;

        private double _Cash_Collected_10000p;

        private double _Cash_Collected_20000p;

        private double _Cash_Collected_50000p;

        private float _Declared_Tickets;

        private float _Tickets_Printed;

        private double _DecHandpay;

        private double _Progressive_Value_Declared;

        private double _Hand_Pay;

        public DeclaredCollection()
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

        
        public double Cash_Collected_100p
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
        public double Cash_Collected_200p
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
        
 

        
        public double Cash_Collected_500p
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

        public double Cash_Collected_1000p
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

        
        public double Cash_Collected_2000p
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



        
        public double Cash_Collected_5000p
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

        
        public double Cash_Collected_10000p
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

        public double Cash_Collected_20000p
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

      
        public double Cash_Collected_50000p
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

     
        public double Hand_Pay
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
        public string Region { get ; set; }
    }
}
