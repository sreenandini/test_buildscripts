using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BMC.EnterpriseBusiness.Entities
{
    public partial class Operators
    {

        private int _Operator_ID;

        private string _Operator_Name;

        public Operators()
        {
        }

        public int Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }

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
    }

    public partial class Machine_Types
    {

        private int _Machine_Type_ID;

        private string _Machine_Type_Code;

        public Machine_Types()
        {
        }

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
    }

    public partial class Manufacturers
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public Manufacturers()
        {
        }

        public int Manufacturer_ID
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
    }

    public partial class GameTitle
    {

        private int _Game_Title_ID;

        private string _Game_Title;

        public GameTitle()
        {
        }

        public int Game_Title_ID
        {
            get
            {
                return this._Game_Title_ID;
            }
            set
            {
                if ((this._Game_Title_ID != value))
                {
                    this._Game_Title_ID = value;
                }
            }
        }

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

    public partial class BaseDenom
    {
        private int? _MGMD_Denom;

        private string _MGMD_Denom_Value;

        public BaseDenom()
        {

        }
        public int? MGMD_Denom
        {
            get
            {
                return this._MGMD_Denom;
            }
            set
            {
                if ((this._MGMD_Denom != value))
                {
                    this._MGMD_Denom = value;
                }
            }

        }

        public string MGMD_Denom_Value
        {
            get
            {
                return this._MGMD_Denom_Value;
            }
            set
            {
                if ((this._MGMD_Denom_Value != value))
                {
                    this._MGMD_Denom_Value = value;
                }
            }
        }
    }

    public partial class Payout
    {
        private string _TheoreticalPayout;
        private float _TheoPayout;

        public Payout()
        {

        }

        public float TheoPayout
        {
            get
            {
                return this._TheoPayout;
            }
            set
            {
                if ((this._TheoPayout != value))
                {
                    this._TheoPayout = value;
                }
            }
        }

        public string TheoreticalPayout
        {
            get
            {
                return this._TheoreticalPayout;
            }
            set
            {
                if ((this._TheoreticalPayout != value))
                {
                    this._TheoreticalPayout = value;
                }
            }
        }
    }

    public partial class OrganisationHierarchy
    {

        private string _Company_Name;

        private string _Sub_Company_Name;

        private string _Site_Name;

        private string _Sub_Company_Area_Name;

        private string _Sub_Company_District_Name;

        private string _Sub_Company_Region_Name;

        private int _Site_ID;

        private int _Company_ID;

        private int _Sub_Company_ID;

        private string _Site_Code;

        private System.Nullable<int> _Sub_Company_Area_ID;

        private System.Nullable<int> _Sub_Company_District_ID;

        private System.Nullable<int> _Sub_Company_Region_ID;

        public OrganisationHierarchy()
        {
        }

        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }

        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
        }

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

        public string Sub_Company_Area_Name
        {
            get
            {
                return this._Sub_Company_Area_Name;
            }
            set
            {
                if ((this._Sub_Company_Area_Name != value))
                {
                    this._Sub_Company_Area_Name = value;
                }
            }
        }

        public string Sub_Company_District_Name
        {
            get
            {
                return this._Sub_Company_District_Name;
            }
            set
            {
                if ((this._Sub_Company_District_Name != value))
                {
                    this._Sub_Company_District_Name = value;
                }
            }
        }

        public string Sub_Company_Region_Name
        {
            get
            {
                return this._Sub_Company_Region_Name;
            }
            set
            {
                if ((this._Sub_Company_Region_Name != value))
                {
                    this._Sub_Company_Region_Name = value;
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

        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        public int Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }

        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this._Site_Code = value;
                }
            }
        }

        public System.Nullable<int> Sub_Company_Area_ID
        {
            get
            {
                return this._Sub_Company_Area_ID;
            }
            set
            {
                if ((this._Sub_Company_Area_ID != value))
                {
                    this._Sub_Company_Area_ID = value;
                }
            }
        }

        public System.Nullable<int> Sub_Company_District_ID
        {
            get
            {
                return this._Sub_Company_District_ID;
            }
            set
            {
                if ((this._Sub_Company_District_ID != value))
                {
                    this._Sub_Company_District_ID = value;
                }
            }
        }

        public System.Nullable<int> Sub_Company_Region_ID
        {
            get
            {
                return this._Sub_Company_Region_ID;
            }
            set
            {
                if ((this._Sub_Company_Region_ID != value))
                {
                    this._Sub_Company_Region_ID = value;
                }
            }
        }
    }

    public class MeterAnalysisParams
    {
        //constructor
        public MeterAnalysisParams()
        {

        }

        public int UserID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CompanyID { get; set; }

        public int SubCompanyID { get; set; }
        
        public int RegionID { get; set; }

        public int AreaID { get; set; }

        public int DistrictID { get; set; }

        public int SiteID { get; set; }

        public int OperatorID { get; set; }

        public int DepotID { get; set; }

        public bool ActiveSites { get; set; }

        public int SiteStatusID { get; set; }

        public bool ActiveAsset { get; set; }

        public int Machine_TypeID { get; set; }

        public int Game_CategoryID { get; set; }

        public int Game_Title_ID { get; set; }

        public string Game_Title { get; set; }

        public int ManufacturerID { get; set; }

        public int PeriodID { get; set; }

        public string NoOfRecords { get; set; }

        public string GroupByClause { get; set; }

        public string Order_By { get; set; }

        public int Denom { get; set; }

        public float Payout { get; set; }

    }

    public class MeterAnalysisChartData
    {
        private DateTime strChartDataLabel;
        private decimal fMachineValue, fGroupValue;
        private int iMachineQuantity, iGroupQuantity;
        public static object objGraphData = null;
        public static string strLegendLabelSelected = "";
        public static string strLegendLabelAll = "";
        public static ArrayList objCollection = new ArrayList();

        public decimal MachineValue
        {
            get { return fMachineValue; }
            set { fMachineValue = value; }
        }
        public decimal GroupValue
        {
            get { return fGroupValue; }
            set { fGroupValue = value; }
        }
        public int GroupQuantity
        {
            get { return iGroupQuantity; }
            set { iGroupQuantity = value; }
        }
        public DateTime ChartDataLabel
        {
            get { return strChartDataLabel; }
            set { strChartDataLabel = value; }
        }
        public int MachineQuantity
        {
            get { return iMachineQuantity; }
            set { iMachineQuantity = value; }
        }
    }
}
