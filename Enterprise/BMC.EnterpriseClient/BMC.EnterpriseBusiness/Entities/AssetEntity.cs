using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace BMC.EnterpriseBusiness.Entities
{

    public partial class AssetReportResult
    {

        private System.Nullable<long> _rn;

        private string _Position;

        private string _Zone_Name;

        private string _Site_Name;

        private string _Model;

        private string _GameName;

        private int _Machine_ID;

        private string _asset;

        private string _Manu;

        private string _Category;

        private string _Type;

        private System.Nullable<double> _Handle;

        private System.Nullable<double> _RDCCashOut;

        private System.Nullable<double> _CasinoWin;

        private System.Nullable<decimal> _DailyWin;

        private System.Nullable<float> _TheoPerc;

        private System.Nullable<decimal> _ActPerc;

        private System.Nullable<decimal> _PercVar;

        public AssetReportResult()
        {
        }

        [Column(Storage = "_rn", DbType = "BigInt")]
        public System.Nullable<long> rn
        {
            get
            {
                return this._rn;
            }
            set
            {
                if ((this._rn != value))
                {
                    this._rn = value;
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

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }

        [Column(Storage = "_Model", DbType = "VarChar(50)")]
        public string Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                if ((this._Model != value))
                {
                    this._Model = value;
                }
            }
        }

        [Column(Storage = "_GameName", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Machine_ID", DbType = "Int NOT NULL")]
        public int Machine_ID
        {
            get
            {
                return this._Machine_ID;
            }
            set
            {
                if ((this._Machine_ID != value))
                {
                    this._Machine_ID = value;
                }
            }
        }

        [Column(Storage = "_asset", DbType = "VarChar(50)")]
        public string asset
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

        [Column(Storage = "_Manu", DbType = "VarChar(50)")]
        public string Manu
        {
            get
            {
                return this._Manu;
            }
            set
            {
                if ((this._Manu != value))
                {
                    this._Manu = value;
                }
            }
        }

        [Column(Storage = "_Category", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Category
        {
            get
            {
                return this._Category;
            }
            set
            {
                if ((this._Category != value))
                {
                    this._Category = value;
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

        [Column(Storage = "_RDCCashOut", DbType = "Float")]
        public System.Nullable<double> RDCCashOut
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

        [Column(Storage = "_CasinoWin", DbType = "Float")]
        public System.Nullable<double> CasinoWin
        {
            get
            {
                return this._CasinoWin;
            }
            set
            {
                if ((this._CasinoWin != value))
                {
                    this._CasinoWin = value;
                }
            }
        }

        [Column(Storage = "_DailyWin", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> DailyWin
        {
            get
            {
                return this._DailyWin;
            }
            set
            {
                if ((this._DailyWin != value))
                {
                    this._DailyWin = value;
                }
            }
        }

        [Column(Storage = "_TheoPerc", DbType = "Real")]
        public System.Nullable<float> TheoPerc
        {
            get
            {
                return this._TheoPerc;
            }
            set
            {
                if ((this._TheoPerc != value))
                {
                    this._TheoPerc = value;
                }
            }
        }

        [Column(Storage = "_ActPerc", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> ActPerc
        {
            get
            {
                return this._ActPerc;
            }
            set
            {
                if ((this._ActPerc != value))
                {
                    this._ActPerc = value;
                }
            }
        }

        [Column(Storage = "_PercVar", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> PercVar
        {
            get
            {
                return this._PercVar;
            }
            set
            {
                if ((this._PercVar != value))
                {
                    this._PercVar = value;
                }
            }
        }
    }

    public partial class DetailedHistoryResult
    {

        private int _Installation_ID;

        private System.Nullable<int> _Installation_Jackpot_Value;

        private System.Nullable<int> _Collection_ID;

        private System.Nullable<double> _Collection_Gross;

        private System.Nullable<int> _Collection_Days;

        private System.Nullable<double> _Collection_Company_Share;

        private System.Nullable<double> _Collection_Supplier_Share;

        private System.Nullable<double> _Collection_Location_Share;

        private System.Nullable<double> _Collection_Other_Share;

        private System.Nullable<double> _Collection_CashTake;

        private System.Nullable<double> _AvgDailyWin;

        private string _Machine_Name;

        private string _GameName;

        private string _Machine_BACTA_Code;

        private string _Machine_Type_Code;

        private string _Machine_Class_Model_Code;

        private string _Machine_Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Alternative_Serial_Numbers;

        private System.Nullable<int> _Installation_Price_Per_Play;

        private string _Installation_Start_Date;

        private string _Installation_End_Date;

        private string _Depot_Name;

        private string _Operator_Name;

        private System.Nullable<int> _Machine_Class_ID;

        public DetailedHistoryResult()
        {
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

        [Column(Storage = "_Installation_Jackpot_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Jackpot_Value
        {
            get
            {
                return this._Installation_Jackpot_Value;
            }
            set
            {
                if ((this._Installation_Jackpot_Value != value))
                {
                    this._Installation_Jackpot_Value = value;
                }
            }
        }

        [Column(Storage = "_Collection_ID", DbType = "Int")]
        public System.Nullable<int> Collection_ID
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

        [Column(Storage = "_Collection_Gross", DbType = "Float")]
        public System.Nullable<double> Collection_Gross
        {
            get
            {
                return this._Collection_Gross;
            }
            set
            {
                if ((this._Collection_Gross != value))
                {
                    this._Collection_Gross = value;
                }
            }
        }

        [Column(Storage = "_Collection_Days", DbType = "Int")]
        public System.Nullable<int> Collection_Days
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

        [Column(Storage = "_Collection_Company_Share", DbType = "Float")]
        public System.Nullable<double> Collection_Company_Share
        {
            get
            {
                return this._Collection_Company_Share;
            }
            set
            {
                if ((this._Collection_Company_Share != value))
                {
                    this._Collection_Company_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_Supplier_Share", DbType = "Float")]
        public System.Nullable<double> Collection_Supplier_Share
        {
            get
            {
                return this._Collection_Supplier_Share;
            }
            set
            {
                if ((this._Collection_Supplier_Share != value))
                {
                    this._Collection_Supplier_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_Location_Share", DbType = "Float")]
        public System.Nullable<double> Collection_Location_Share
        {
            get
            {
                return this._Collection_Location_Share;
            }
            set
            {
                if ((this._Collection_Location_Share != value))
                {
                    this._Collection_Location_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_Other_Share", DbType = "Float")]
        public System.Nullable<double> Collection_Other_Share
        {
            get
            {
                return this._Collection_Other_Share;
            }
            set
            {
                if ((this._Collection_Other_Share != value))
                {
                    this._Collection_Other_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_CashTake", DbType = "Float")]
        public System.Nullable<double> Collection_CashTake
        {
            get
            {
                return this._Collection_CashTake;
            }
            set
            {
                if ((this._Collection_CashTake != value))
                {
                    this._Collection_CashTake = value;
                }
            }
        }

        [Column(Storage = "_AvgDailyWin", DbType = "Float")]
        public System.Nullable<double> AvgDailyWin
        {
            get
            {
                return this._AvgDailyWin;
            }
            set
            {
                if ((this._AvgDailyWin != value))
                {
                    this._AvgDailyWin = value;
                }
            }
        }

        [Column(Storage = "_Machine_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_GameName", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Machine_BACTA_Code", DbType = "VarChar(50)")]
        public string Machine_BACTA_Code
        {
            get
            {
                return this._Machine_BACTA_Code;
            }
            set
            {
                if ((this._Machine_BACTA_Code != value))
                {
                    this._Machine_BACTA_Code = value;
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

        [Column(Storage = "_Machine_Class_Model_Code", DbType = "VarChar(50)")]
        public string Machine_Class_Model_Code
        {
            get
            {
                return this._Machine_Class_Model_Code;
            }
            set
            {
                if ((this._Machine_Class_Model_Code != value))
                {
                    this._Machine_Class_Model_Code = value;
                }
            }
        }

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
        public string Machine_Stock_No
        {
            get
            {
                return this._Machine_Stock_No;
            }
            set
            {
                if ((this._Machine_Stock_No != value))
                {
                    this._Machine_Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")]
        public string Machine_Manufacturers_Serial_No
        {
            get
            {
                return this._Machine_Manufacturers_Serial_No;
            }
            set
            {
                if ((this._Machine_Manufacturers_Serial_No != value))
                {
                    this._Machine_Manufacturers_Serial_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Alternative_Serial_Numbers", DbType = "VarChar(50)")]
        public string Machine_Alternative_Serial_Numbers
        {
            get
            {
                return this._Machine_Alternative_Serial_Numbers;
            }
            set
            {
                if ((this._Machine_Alternative_Serial_Numbers != value))
                {
                    this._Machine_Alternative_Serial_Numbers = value;
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

        [Column(Storage = "_Installation_Start_Date", DbType = "VarChar(30)")]
        public string Installation_Start_Date
        {
            get
            {
                return this._Installation_Start_Date;
            }
            set
            {
                if ((this._Installation_Start_Date != value))
                {
                    this._Installation_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Installation_End_Date", DbType = "VarChar(30)")]
        public string Installation_End_Date
        {
            get
            {
                return this._Installation_End_Date;
            }
            set
            {
                if ((this._Installation_End_Date != value))
                {
                    this._Installation_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }

        [Column(Storage = "_Machine_Class_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Class_ID
        {
            get
            {
                return this._Machine_Class_ID;
            }
            set
            {
                if ((this._Machine_Class_ID != value))
                {
                    this._Machine_Class_ID = value;
                }
            }
        }
    }

    /*
     * Get/Set the parameters for Asset Report
     * */
    public partial class AssetParams
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int SiteID { get; set; }

        public int Category { get; set; }
    }

    public partial class AssetHistoryParams
    {
        public int BarPositionID { get; set; }

        public bool IsDetailed { get; set; }

        public int? SiteID { get; set; }
    }
}
