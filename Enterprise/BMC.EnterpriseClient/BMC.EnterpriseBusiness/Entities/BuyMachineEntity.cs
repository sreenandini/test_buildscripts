using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{



    public partial class BuyAuditEntity
    {
        public string Manufacturer_Name{ get; set; }
        public string Machine_Name { get; set; }
        public string Stacker_Name { get; set; }
        public string Machine_Class_Category_Name { get; set; }
        public string Operator_Name { get; set; }
        public string Depot_Name { get; set; }
        public string Rep_Name { get; set; }
        public string Status { get; set; }
        public string Machine_Purchased_From { get; set; }
        public string ActAssetNo { get; set; }
        public string GMUNo { get; set; }
        public string ActSerialNo { get; set; }
        public string Machine_Alternative_Serial_Numbers { get; set; }
        public System.Nullable<int> Validation_Length { get; set; }
        public string Depreciation { get; set; }
        public string Machine_MAC_Address { get; set; }
        public string GameTypeCode { get; set; }
        public string Model_Type { get; set; }
        public System.Nullable<bool> IsDefaultAssetDetail { get; set; }
        public System.Nullable<int> Base_Denom { get; set; }
        public System.Nullable<float> Percentage_Payout { get; set; }
        public bool AFTEnable { get; set; }
        public bool GetGameDetails { get; set; }
        public System.Nullable<bool> IsTITOEnabled { get; set; }
        public System.Nullable<bool> IsGameCappingEnabled { get; set; }
        public System.Nullable<bool> IsNonCashVoucherEnabled { get; set; }
        public string Machine_Memo { get; set; }
        public string Game_Title { get; set; }
        public string Stock_No { get; set; }
        public string Original_Price { get; set; }
        public string Invoice_Number { get; set; }
        public System.Nullable<int> Occupancy_Games_Per_Hour { get; set; }
        public string Asset_Template { get; set; }    
        public string ActStockNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DepreciationStartDate { get; set; }
        public string NBV { get; set; }
        public string WeeklyDepreciation { get; set; }
        public string GamePrefix { get; set; }
        public string AssetDisplayName { get; set; }
        
    }

    public partial class GetMachineClassDetailsResult
    {

        private int _Machine_Class_ID;

        private string _Machine_Name;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private int _Machine_Class_Category_ID;

        private System.Nullable<int> _Manufacturer_ID;

        private string _Manufacturer_Name;

        private System.Nullable<int> _Validation_Length;

        public GetMachineClassDetailsResult()
        {
        }


        public int Machine_Class_ID
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


        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
                }
            }
        }


        public int Machine_Class_Category_ID
        {
            get
            {
                return this._Machine_Class_Category_ID;
            }
            set
            {
                if ((this._Machine_Class_Category_ID != value))
                {
                    this._Machine_Class_Category_ID = value;
                }
            }
        }


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


        public System.Nullable<int> Validation_Length
        {
            get
            {
                return this._Validation_Length;
            }
            set
            {
                if ((this._Validation_Length != value))
                {
                    this._Validation_Length = value;
                }
            }
        }
    }

    public partial class GetDepreciationPolicyDetailsResult
    {

        private int _Depreciation_Policy_ID;

        private string _Depreciation_Policy_Description;

        public GetDepreciationPolicyDetailsResult()
        {
        }

        public int Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
                }
            }
        }

        public string Depreciation_Policy_Description
        {
            get
            {
                return this._Depreciation_Policy_Description;
            }
            set
            {
                if ((this._Depreciation_Policy_Description != value))
                {
                    this._Depreciation_Policy_Description = value;
                }
            }
        }
    }

    public partial class GetMachineTypeDetailsResult
    {

        private int _Machine_Type_ID;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private string _Machine_Type_Code;

        private string _Machine_Type_Description;

        private string _Machine_Type_Category;

        private int _IsNonGamingAssetType;

        private string _Machine_Type_AMEDIS_ID;

        private string _Machine_Type_Income_Ledger_Code;

        private string _Machine_Type_Site_Icon;

        private System.Nullable<int> _Machine_Type_Icon_ref;

        private string _SiteIconPath;

        public GetMachineTypeDetailsResult()
        {
        }

        public string MCTypeDescription_NGA
        {
            get;
            set;
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


        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
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


        public string Machine_Type_Description
        {
            get
            {
                return this._Machine_Type_Description;
            }
            set
            {
                if ((this._Machine_Type_Description != value))
                {
                    this._Machine_Type_Description = value;
                }
            }
        }


        public string Machine_Type_Category
        {
            get
            {
                return this._Machine_Type_Category;
            }
            set
            {
                if ((this._Machine_Type_Category != value))
                {
                    this._Machine_Type_Category = value;
                }
            }
        }


        public int IsNonGamingAssetType
        {
            get
            {
                return this._IsNonGamingAssetType;
            }
            set
            {
                if ((this._IsNonGamingAssetType != value))
                {
                    this._IsNonGamingAssetType = value;
                }
            }
        }


        public string Machine_Type_AMEDIS_ID
        {
            get
            {
                return this._Machine_Type_AMEDIS_ID;
            }
            set
            {
                if ((this._Machine_Type_AMEDIS_ID != value))
                {
                    this._Machine_Type_AMEDIS_ID = value;
                }
            }
        }


        public string Machine_Type_Income_Ledger_Code
        {
            get
            {
                return this._Machine_Type_Income_Ledger_Code;
            }
            set
            {
                if ((this._Machine_Type_Income_Ledger_Code != value))
                {
                    this._Machine_Type_Income_Ledger_Code = value;
                }
            }
        }


        public string Machine_Type_Site_Icon
        {
            get
            {
                return this._Machine_Type_Site_Icon;
            }
            set
            {
                if ((this._Machine_Type_Site_Icon != value))
                {
                    this._Machine_Type_Site_Icon = value;
                }
            }
        }

        public System.Nullable<int> Machine_Type_Icon_ref
        {
            get
            {
                return this._Machine_Type_Icon_ref;
            }
            set
            {
                if ((this._Machine_Type_Icon_ref != value))
                {
                    this._Machine_Type_Icon_ref = value;
                }
            }
        }

        public string SiteIconPath
        {
            get
            {
                return this._SiteIconPath;
            }
            set
            {
                if ((this._SiteIconPath != value))
                {
                    this._SiteIconPath = value;
                }
            }
        }
    }

    public partial class GetGameTitleResult
    {

        private int _Game_Title_ID;

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Title;

        private System.Nullable<int> _Manufacturer_ID;

        public GetGameTitleResult()
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


        public System.Nullable<int> Game_Category_ID
        {
            get
            {
                return this._Game_Category_ID;
            }
            set
            {
                if ((this._Game_Category_ID != value))
                {
                    this._Game_Category_ID = value;
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
    }

    public partial class GetModelTypeResult
    {

        private int _MT_ID;

        private string _MT_Model_Name;

        private string _MT_Model_Desc;

        private bool _MT_IsNGA;

        public GetModelTypeResult()
        {
        }


        public int MT_ID
        {
            get
            {
                return this._MT_ID;
            }
            set
            {
                if ((this._MT_ID != value))
                {
                    this._MT_ID = value;
                }
            }
        }


        public string MT_Model_Name
        {
            get
            {
                return this._MT_Model_Name;
            }
            set
            {
                if ((this._MT_Model_Name != value))
                {
                    this._MT_Model_Name = value;
                }
            }
        }


        public string MT_Model_Desc
        {
            get
            {
                return this._MT_Model_Desc;
            }
            set
            {
                if ((this._MT_Model_Desc != value))
                {
                    this._MT_Model_Desc = value;
                }
            }
        }


        public bool MT_IsNGA
        {
            get
            {
                return this._MT_IsNGA;
            }
            set
            {
                if ((this._MT_IsNGA != value))
                {
                    this._MT_IsNGA = value;
                }
            }
        }
    }

    public partial class GetStackerDetailsResult
    {

        private int _Stacker_Id;

        private string _StackerName;

        private int _StackerSize;

        private bool _StackerStatus;

        private string _StackerDescription;

        public GetStackerDetailsResult()
        {
        }


        public int Stacker_Id
        {
            get
            {
                return this._Stacker_Id;
            }
            set
            {
                if ((this._Stacker_Id != value))
                {
                    this._Stacker_Id = value;
                }
            }
        }


        public string StackerName
        {
            get
            {
                return this._StackerName;
            }
            set
            {
                if ((this._StackerName != value))
                {
                    this._StackerName = value;
                }
            }
        }


        public int StackerSize
        {
            get
            {
                return this._StackerSize;
            }
            set
            {
                if ((this._StackerSize != value))
                {
                    this._StackerSize = value;
                }
            }
        }


        public bool StackerStatus
        {
            get
            {
                return this._StackerStatus;
            }
            set
            {
                if ((this._StackerStatus != value))
                {
                    this._StackerStatus = value;
                }
            }
        }


        public string StackerDescription
        {
            get
            {
                return this._StackerDescription;
            }
            set
            {
                if ((this._StackerDescription != value))
                {
                    this._StackerDescription = value;
                }
            }
        }
    }

    public partial class GetStaffByDepotResult
    {

        private int _Staff_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        public GetStaffByDepotResult()
        {
        }

        public int Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }

        public string FullName
        {
            get;
            set;
        }

        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
                }
            }
        }


        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }


    }

    public partial class GetBuyMachineDetailsResult
    {

        private string _GameTypeCode;

        private System.Nullable<bool> _IsAFTEnabled;

        private System.Nullable<bool> _IsGameCappingEnabled;

        private System.Nullable<int> _Depot_ID;

        private System.Nullable<int> _Operator_ID;

        private System.Nullable<char> _CMPGameType;

        private System.Nullable<int> _Staff_ID;

        private System.Nullable<int> _Machine_Connection_Type;

        private System.Nullable<int> _Machine_CIV_Change_Reason;

        private System.Nullable<int> _Machine_ModelTypeID;

        private int _Stacker_Id;

        private System.Nullable<bool> _IsTITOEnabled;

        private System.Nullable<bool> _IsNonCashVoucherEnabled;

        private System.Nullable<bool> _IsMultiGame;

        private System.Nullable<int> _Machine_Status_Flag;

        private string _Machine_Stock_No;

        private string _Machine_End_Date;

        private string _Machine_Sales_Invoice_Number;

        private System.Nullable<decimal> _Machine_Sale_Price;

        private string _Machine_Sold_To;

        private string _Machine_Type_Of_Sale;

        private string _ActAssetNo;

        private string _ActSerialNo;

        private string _Machine_Memo;

        private string _GMUNo;

        private string _Machine_Purchased_From;

        private string _Machine_Alternative_Serial_Numbers;

        private string _Machine_MAC_Address;

        private string _Machine_Depreciation_Start_Date;

        private string _Machine_Start_Date;

        private System.Nullable<decimal> _Machine_Original_Purchase_Price;

        private string _Machine_Purchase_Invoice_Number;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private System.Nullable<bool> _Depreciation_Policy_Use_Default;

        private System.Nullable<int> _Machine_Class_Occupancy_Games_Per_Hour;

        private System.Nullable<int> _Class_Depreciation;

        private int _Machine_Class_Category_ID;

        private System.Nullable<int> _Type_Depreciation;

        private string _Machine_Name;

        private string _Machine_BACTA_Code;

        private System.Nullable<int> _Manufacturer_ID;

        private System.Nullable<int> _Validation_Length;

        private string _Staff_Sold_Staff_Last_Name;

        private string _Staff_Sold_Staff_First_Name;

        private System.Nullable<int> _Old_Machine_ID;

        private string _Old_Machine_Start_Date;

        private string _Old_Machine_Name;

        private System.Nullable<int> _MG_Game_ID;

        private System.Nullable<int> _Installation_ID;

        private System.Nullable<int> _Base_Denom;

        System.Nullable<float> _Percentage_Payout;

        private System.Nullable<bool> _IsDefaultAssetDetail;

        private string _AssetDisplayName;



        public GetBuyMachineDetailsResult()
        {
        }

        public string MultiGameName
        {
            get;
            set;
        }
        public System.Nullable<int> Base_Denom
        {
            get
            {
                return this._Base_Denom;
            }
            set
            {
                if ((this._Base_Denom != value))
                {
                    this._Base_Denom = value;
                }
            }
        }

        public System.Nullable<float> Percentage_Payout
        {
            get
            {
                return this._Percentage_Payout;
            }
            set
            {
                if ((this._Percentage_Payout != value))
                {
                    this._Percentage_Payout = value;
                }
            }
        }

        public System.Nullable<bool> IsDefaultAssetDetail
        {
            get
            {
                return this._IsDefaultAssetDetail;
            }
            set
            {
                if ((this._IsDefaultAssetDetail != value))
                {
                    this._IsDefaultAssetDetail = value;
                }
            }
        }



        public string GameTypeCode
        {
            get
            {
                return this._GameTypeCode;
            }
            set
            {
                if ((this._GameTypeCode != value))
                {
                    this._GameTypeCode = value;
                }
            }
        }

        public string AssetDisplayName
        {
            get
            {
                return this._AssetDisplayName;
            }
            set
            {
                if ((this._AssetDisplayName != value))
                {
                    this._AssetDisplayName = value;
                }
            }
        }
        public System.Nullable<int> MG_Game_ID
        {
            get
            {
                return this._MG_Game_ID;
            }
            set
            {
                if ((this._MG_Game_ID != value))
                {
                    this._MG_Game_ID = value;
                }
            }
        }

        public System.Nullable<bool> IsAFTEnabled
        {
            get
            {
                return this._IsAFTEnabled;
            }
            set
            {
                if ((this._IsAFTEnabled != value))
                {
                    this._IsAFTEnabled = value;
                }
            }
        }
        public System.Nullable<bool> IsGameCappingEnabled
        {
            get
            {
                return this._IsGameCappingEnabled;
            }
            set
            {
                if ((this._IsGameCappingEnabled != value))
                {
                    this._IsGameCappingEnabled = value;
                }
            }
        }

        public System.Nullable<int> Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this._Depot_ID = value;
                }
            }
        }

        public bool GetGameDetails
        {
            get;
            set;
        }

        public System.Nullable<int> Operator_ID
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


        public System.Nullable<char> CMPGameType
        {
            get
            {
                return this._CMPGameType;
            }
            set
            {
                if ((this._CMPGameType != value))
                {
                    this._CMPGameType = value;
                }
            }
        }


        public System.Nullable<int> Staff_ID
        {
            get
            {
                return this._Staff_ID;
            }
            set
            {
                if ((this._Staff_ID != value))
                {
                    this._Staff_ID = value;
                }
            }
        }


        public System.Nullable<int> Machine_Connection_Type
        {
            get
            {
                return this._Machine_Connection_Type;
            }
            set
            {
                if ((this._Machine_Connection_Type != value))
                {
                    this._Machine_Connection_Type = value;
                }
            }
        }


        public System.Nullable<int> Machine_CIV_Change_Reason
        {
            get
            {
                return this._Machine_CIV_Change_Reason;
            }
            set
            {
                if ((this._Machine_CIV_Change_Reason != value))
                {
                    this._Machine_CIV_Change_Reason = value;
                }
            }
        }


        public System.Nullable<int> Machine_ModelTypeID
        {
            get
            {
                return this._Machine_ModelTypeID;
            }
            set
            {
                if ((this._Machine_ModelTypeID != value))
                {
                    this._Machine_ModelTypeID = value;
                }
            }
        }


        public int Stacker_Id
        {
            get
            {
                return this._Stacker_Id;
            }
            set
            {
                if ((this._Stacker_Id != value))
                {
                    this._Stacker_Id = value;
                }
            }
        }


        public System.Nullable<bool> IsTITOEnabled
        {
            get
            {
                return this._IsTITOEnabled;
            }
            set
            {
                if ((this._IsTITOEnabled != value))
                {
                    this._IsTITOEnabled = value;
                }
            }
        }


        public System.Nullable<bool> IsNonCashVoucherEnabled
        {
            get
            {
                return this._IsNonCashVoucherEnabled;
            }
            set
            {
                if ((this._IsNonCashVoucherEnabled != value))
                {
                    this._IsNonCashVoucherEnabled = value;
                }
            }
        }


        public System.Nullable<bool> IsMultiGame
        {
            get
            {
                return this._IsMultiGame;
            }
            set
            {
                if ((this._IsMultiGame != value))
                {
                    this._IsMultiGame = value;
                }
            }
        }


        public System.Nullable<int> Machine_Status_Flag
        {
            get
            {
                return this._Machine_Status_Flag;
            }
            set
            {
                if ((this._Machine_Status_Flag != value))
                {
                    this._Machine_Status_Flag = value;
                }
            }
        }


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


        public string Machine_End_Date
        {
            get
            {
                return this._Machine_End_Date;
            }
            set
            {
                if ((this._Machine_End_Date != value))
                {
                    this._Machine_End_Date = value;
                }
            }
        }


        public string Machine_Sales_Invoice_Number
        {
            get
            {
                return this._Machine_Sales_Invoice_Number;
            }
            set
            {
                if ((this._Machine_Sales_Invoice_Number != value))
                {
                    this._Machine_Sales_Invoice_Number = value;
                }
            }
        }


        public System.Nullable<decimal> Machine_Sale_Price
        {
            get
            {
                return this._Machine_Sale_Price;
            }
            set
            {
                if ((this._Machine_Sale_Price != value))
                {
                    this._Machine_Sale_Price = value;
                }
            }
        }


        public string Machine_Sold_To
        {
            get
            {
                return this._Machine_Sold_To;
            }
            set
            {
                if ((this._Machine_Sold_To != value))
                {
                    this._Machine_Sold_To = value;
                }
            }
        }


        public string Machine_Type_Of_Sale
        {
            get
            {
                return this._Machine_Type_Of_Sale;
            }
            set
            {
                if ((this._Machine_Type_Of_Sale != value))
                {
                    this._Machine_Type_Of_Sale = value;
                }
            }
        }


        public string ActAssetNo
        {
            get
            {
                return this._ActAssetNo;
            }
            set
            {
                if ((this._ActAssetNo != value))
                {
                    this._ActAssetNo = value;
                }
            }
        }

        public string ActSerialNo
        {
            get
            {
                return this._ActSerialNo;
            }
            set
            {
                if ((this._ActSerialNo != value))
                {
                    this._ActSerialNo = value;
                }
            }
        }


        public string GMUNo
        {
            get
            {
                return this._GMUNo;
            }
            set
            {
                if ((this._GMUNo != value))
                {
                    this._GMUNo = value;
                }
            }
        }


        public string Machine_Memo
        {
            get
            {
                return this._Machine_Memo;
            }
            set
            {
                if ((this._Machine_Memo != value))
                {
                    this._Machine_Memo = value;
                }
            }
        }

        public string Machine_Purchased_From
        {
            get
            {
                return this._Machine_Purchased_From;
            }
            set
            {
                if ((this._Machine_Purchased_From != value))
                {
                    this._Machine_Purchased_From = value;
                }
            }
        }


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


        public string Machine_MAC_Address
        {
            get
            {
                return this._Machine_MAC_Address;
            }
            set
            {
                if ((this._Machine_MAC_Address != value))
                {
                    this._Machine_MAC_Address = value;
                }
            }
        }


        public string Machine_Depreciation_Start_Date
        {
            get
            {
                return this._Machine_Depreciation_Start_Date;
            }
            set
            {
                if ((this._Machine_Depreciation_Start_Date != value))
                {
                    this._Machine_Depreciation_Start_Date = value;
                }
            }
        }


        public string Machine_Start_Date
        {
            get
            {
                return this._Machine_Start_Date;
            }
            set
            {
                if ((this._Machine_Start_Date != value))
                {
                    this._Machine_Start_Date = value;
                }
            }
        }


        public System.Nullable<decimal> Machine_Original_Purchase_Price
        {
            get
            {
                return this._Machine_Original_Purchase_Price;
            }
            set
            {
                if ((this._Machine_Original_Purchase_Price != value))
                {
                    this._Machine_Original_Purchase_Price = value;
                }
            }
        }


        public string Machine_Purchase_Invoice_Number
        {
            get
            {
                return this._Machine_Purchase_Invoice_Number;
            }
            set
            {
                if ((this._Machine_Purchase_Invoice_Number != value))
                {
                    this._Machine_Purchase_Invoice_Number = value;
                }
            }
        }


        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
                }
            }
        }


        public System.Nullable<bool> Depreciation_Policy_Use_Default
        {
            get
            {
                return this._Depreciation_Policy_Use_Default;
            }
            set
            {
                if ((this._Depreciation_Policy_Use_Default != value))
                {
                    this._Depreciation_Policy_Use_Default = value;
                }
            }
        }


        public System.Nullable<int> Machine_Class_Occupancy_Games_Per_Hour
        {
            get
            {
                return this._Machine_Class_Occupancy_Games_Per_Hour;
            }
            set
            {
                if ((this._Machine_Class_Occupancy_Games_Per_Hour != value))
                {
                    this._Machine_Class_Occupancy_Games_Per_Hour = value;
                }
            }
        }


        public System.Nullable<int> Class_Depreciation
        {
            get
            {
                return this._Class_Depreciation;
            }
            set
            {
                if ((this._Class_Depreciation != value))
                {
                    this._Class_Depreciation = value;
                }
            }
        }


        public int Machine_Class_Category_ID
        {
            get
            {
                return this._Machine_Class_Category_ID;
            }
            set
            {
                if ((this._Machine_Class_Category_ID != value))
                {
                    this._Machine_Class_Category_ID = value;
                }
            }
        }


        public System.Nullable<int> Type_Depreciation
        {
            get
            {
                return this._Type_Depreciation;
            }
            set
            {
                if ((this._Type_Depreciation != value))
                {
                    this._Type_Depreciation = value;
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


        public System.Nullable<int> Validation_Length
        {
            get
            {
                return this._Validation_Length;
            }
            set
            {
                if ((this._Validation_Length != value))
                {
                    this._Validation_Length = value;
                }
            }
        }


        public string Staff_Sold_Staff_Last_Name
        {
            get
            {
                return this._Staff_Sold_Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Sold_Staff_Last_Name != value))
                {
                    this._Staff_Sold_Staff_Last_Name = value;
                }
            }
        }


        public string Staff_Sold_Staff_First_Name
        {
            get
            {
                return this._Staff_Sold_Staff_First_Name;
            }
            set
            {
                if ((this._Staff_Sold_Staff_First_Name != value))
                {
                    this._Staff_Sold_Staff_First_Name = value;
                }
            }
        }


        public System.Nullable<int> Old_Machine_ID
        {
            get
            {
                return this._Old_Machine_ID;
            }
            set
            {
                if ((this._Old_Machine_ID != value))
                {
                    this._Old_Machine_ID = value;
                }
            }
        }


        public string Old_Machine_Start_Date
        {
            get
            {
                return this._Old_Machine_Start_Date;
            }
            set
            {
                if ((this._Old_Machine_Start_Date != value))
                {
                    this._Old_Machine_Start_Date = value;
                }
            }
        }


        public string Old_Machine_Name
        {
            get
            {
                return this._Old_Machine_Name;
            }
            set
            {
                if ((this._Old_Machine_Name != value))
                {
                    this._Old_Machine_Name = value;
                }
            }
        }

        public System.Nullable<int> Installation_ID
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
    }


    public partial class GetDepreciationDetailsResult
    {

        private string _Machine_End_Date;

        private System.Nullable<decimal> _Machine_Original_Purchase_Price;

        private string _Machine_Depreciation_Start_Date;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private System.Nullable<int> _Machine_Class_ID;

        private System.Nullable<int> _Depreciation_Policy_Details_ID;

        private System.Nullable<float> _Depreciation_Policy_Residual_Value;

        private System.Nullable<int> _Depreciation_Policy_Details_Duration;

        private System.Nullable<int> _Depreciation_Policy_Details_Percentage;

        public GetDepreciationDetailsResult()
        {
        }


        public string Machine_End_Date
        {
            get
            {
                return this._Machine_End_Date;
            }
            set
            {
                if ((this._Machine_End_Date != value))
                {
                    this._Machine_End_Date = value;
                }
            }
        }


        public System.Nullable<decimal> Machine_Original_Purchase_Price
        {
            get
            {
                return this._Machine_Original_Purchase_Price;
            }
            set
            {
                if ((this._Machine_Original_Purchase_Price != value))
                {
                    this._Machine_Original_Purchase_Price = value;
                }
            }
        }


        public string Machine_Depreciation_Start_Date
        {
            get
            {
                return this._Machine_Depreciation_Start_Date;
            }
            set
            {
                if ((this._Machine_Depreciation_Start_Date != value))
                {
                    this._Machine_Depreciation_Start_Date = value;
                }
            }
        }


        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
                }
            }
        }


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


        public System.Nullable<int> Depreciation_Policy_Details_ID
        {
            get
            {
                return this._Depreciation_Policy_Details_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_ID != value))
                {
                    this._Depreciation_Policy_Details_ID = value;
                }
            }
        }


        public System.Nullable<float> Depreciation_Policy_Residual_Value
        {
            get
            {
                return this._Depreciation_Policy_Residual_Value;
            }
            set
            {
                if ((this._Depreciation_Policy_Residual_Value != value))
                {
                    this._Depreciation_Policy_Residual_Value = value;
                }
            }
        }


        public System.Nullable<int> Depreciation_Policy_Details_Duration
        {
            get
            {
                return this._Depreciation_Policy_Details_Duration;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Duration != value))
                {
                    this._Depreciation_Policy_Details_Duration = value;
                }
            }
        }


        public System.Nullable<int> Depreciation_Policy_Details_Percentage
        {
            get
            {
                return this._Depreciation_Policy_Details_Percentage;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Percentage != value))
                {
                    this._Depreciation_Policy_Details_Percentage = value;
                }
            }
        }
    }

    public partial class GetMachineDetailsFromAssetResult
    {

        private string _Bar_Position_Name;

        private string _Machine_Stock_No;

        private int _Installation_ID;

        private string _Machine_Name;

        private System.Nullable<int> _Site_ID;

        private string _Site_Code;

        public GetMachineDetailsFromAssetResult()
        {
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

        public System.Nullable<int> Site_ID
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
    }


    public partial class GetActiveStackerDetailsResult
    {

        private int _ActiveStacker_Id;

        private string _ActiveStackerName;



        public GetActiveStackerDetailsResult()
        {
        }


        public int ActiveStacker_Id
        {
            get
            {
                return this._ActiveStacker_Id;
            }
            set
            {
                if ((this._ActiveStacker_Id != value))
                {
                    this._ActiveStacker_Id = value;
                }
            }
        }


        public string ActiveStackerName
        {
            get
            {
                return this._ActiveStackerName;
            }
            set
            {
                if ((this._ActiveStackerName != value))
                {
                    this._ActiveStackerName = value;
                }
            }
        }

       
    }

}
