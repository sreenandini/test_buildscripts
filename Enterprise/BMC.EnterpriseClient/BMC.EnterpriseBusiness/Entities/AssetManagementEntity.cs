using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    #region AssetManagement

    public partial class GetMachineDetailsResult
    {

        private string _Bar_Position_Name;

        private string _Site_ZonaRice;

        private System.Nullable<int> _Staff_ID;

        private string _Machine_Transit_Site_Code;

        private string _Machine_End_Date;

        private System.Nullable<int> _Operator_ID;

        private string _Operator_Name;

        private System.Nullable<int> _Depot_ID;

        private string _Depot_Name;

        private int _PaytableFlag;

        private int _Machine_Type_ID;

        private string _Machine_Type_Code;

        private int _IsNonGamingAssetType;

        private string _Machine_Name;

        private System.Nullable<int> _Machine_Class_ID;

        private string _Machine_Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Alternative_Serial_Numbers;

        private System.Nullable<int> _Machine_ID;

        private System.Nullable<int> _Machine_Status_Flag;

        private string _Machine_BACTA_Code;

        private string _Machine_Class_Model_Code;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        private string _Machine_Category_Code;

        private string _Manufacturer_Name;

        private string _Site_Code;

        private string _Site_Name;

   
        private int _MG_Game_ID;
        

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Category_Name;

        public GetMachineDetailsResult()
        {
        }

        public int PaytableFlag
        {
            get
            {
                return this._PaytableFlag;
            }
            set
            {
                if ((this._PaytableFlag != value))
                {
                    this._PaytableFlag = value;
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
       
        public string Site_ZonaRice
        {
            get
            {
                return this._Site_ZonaRice;
            }
            set
            {
                if ((this._Site_ZonaRice != value))
                {
                    this._Site_ZonaRice = value;
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
        
       
        public int MG_Game_ID
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
        public string Machine_Transit_Site_Code
        {
            get
            {
                return this._Machine_Transit_Site_Code;
            }
            set
            {
                if ((this._Machine_Transit_Site_Code != value))
                {
                    this._Machine_Transit_Site_Code = value;
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


        public System.Nullable<int> Machine_ID
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


        public string Machine_Category_Code
        {
            get
            {
                return this._Machine_Category_Code;
            }
            set
            {
                if ((this._Machine_Category_Code != value))
                {
                    this._Machine_Category_Code = value;
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


        public string Game_Category_Name
        {
            get
            {
                return this._Game_Category_Name;
            }
            set
            {
                if ((this._Game_Category_Name != value))
                {
                    this._Game_Category_Name = value;
                }
            }
        }
    }


    public partial class GetSiteDetailsByStockResult
    {

        private string _Site_Name;

        private string _Site_Code;

        private string _Bar_Position_Name;

        private string _Site_ZonaRice;

        public GetSiteDetailsByStockResult()
        {
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


        public string Site_ZonaRice
        {
            get
            {
                return this._Site_ZonaRice;
            }
            set
            {
                if ((this._Site_ZonaRice != value))
                {
                    this._Site_ZonaRice = value;
                }
            }
        }
    }
    public partial class CheckTemplateNameExistsResult
    {

        private System.Nullable<int> _Exist;

        public CheckTemplateNameExistsResult()
        {
        }

        
        public System.Nullable<int> Exist
        {
            get
            {
                return this._Exist;
            }
            set
            {
                if ((this._Exist != value))
                {
                    this._Exist = value;
                }
            }
        }
    }
    public partial class GetAssetNumberForTemplateResult
    {

        private string _AssetNumber;

        public GetAssetNumberForTemplateResult()
        {
        }

        
        public string AssetNumber
        {
            get
            {
                return this._AssetNumber;
            }
            set
            {
                if ((this._AssetNumber != value))
                {
                    this._AssetNumber = value;
                }
            }
        }
    }
    public partial class GetAssetTemplateDetailsResult
    {

        private int _AssetCrTempNumber;

        private string _TemplateName;

        public GetAssetTemplateDetailsResult()
        {
        }

        
        public int AssetCrTempNumber
        {
            get
            {
                return this._AssetCrTempNumber;
            }
            set
            {
                if ((this._AssetCrTempNumber != value))
                {
                    this._AssetCrTempNumber = value;
                }
            }
        }

       
        public string TemplateName
        {
            get
            {
                return this._TemplateName;
            }
            set
            {
                if ((this._TemplateName != value))
                {
                    this._TemplateName = value;
                }
            }
        }
    }






    //load game names 
    public partial class rsp_GetGameNames
    {

        private string _MG_Game_Name;


        private int _MG_Game_ID;

        public rsp_GetGameNames()
        {
        }

        
        public string MG_Game_Name
        {
            get
            {
                return this._MG_Game_Name;
            }
            set
            {
                if ((this._MG_Game_Name != value))
                {
                    this._MG_Game_Name = value;
                }
            }
        }
        public int MG_Game_ID
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
    }



    //load game names 
    public partial class GetPaytableDetailsForGameResult
    {

        private string _AliasGameName;

        private string _Manufacturer_Name;

        private int _Installation_No;

        private System.Nullable<int> _Machine_ID;

        private string _Game_Name;

        private int _PaytableID;

        private string _PaytableDescription;

        private System.Nullable<double> _Payout;

        private double _MaxBet;

        private double _TheoreticalPayout;

        public GetPaytableDetailsForGameResult()
        {
        }


        public string AliasGameName
        {
            get
            {
                return this._AliasGameName;
            }
            set
            {
                if ((this._AliasGameName != value))
                {
                    this._AliasGameName = value;
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


        public System.Nullable<int> Machine_ID
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


        public string Game_Name
        {
            get
            {
                return this._Game_Name;
            }
            set
            {
                if ((this._Game_Name != value))
                {
                    this._Game_Name = value;
                }
            }
        }


        public int PaytableID
        {
            get
            {
                return this._PaytableID;
            }
            set
            {
                if ((this._PaytableID != value))
                {
                    this._PaytableID = value;
                }
            }
        }


        public string PaytableDescription
        {
            get
            {
                return this._PaytableDescription;
            }
            set
            {
                if ((this._PaytableDescription != value))
                {
                    this._PaytableDescription = value;
                }
            }
        }


        public System.Nullable<double> Payout
        {
            get
            {
                return this._Payout;
            }
            set
            {
                if ((this._Payout != value))
                {
                    this._Payout = value;
                }
            }
        }


        public double MaxBet
        {
            get
            {
                return this._MaxBet;
            }
            set
            {
                if ((this._MaxBet != value))
                {
                    this._MaxBet = value;
                }
            }
        }


        public double TheoreticalPayout
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

    public partial class FetchGameCategoryResult
    {

        private int _Game_Category_ID;

        private string _Game_Category_Name;

        public FetchGameCategoryResult()
        {
        }

        public int Game_Category_ID
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

        public string Game_Category_Name
        {
            get
            {
                return this._Game_Category_Name;
            }
            set
            {
                if ((this._Game_Category_Name != value))
                {
                    this._Game_Category_Name = value;
                }
            }
        }
    }

    #endregion

    #region TerminationEntity
    public partial class GetMachineTerminationReasonResult
    {
        private int _MTRT_ID;

        private string _MTRT_Description;

        private int _MTRT_Display_Order;

        public GetMachineTerminationReasonResult()
        {
        }

        public int MTRT_ID
        {
            get
            {
                return this._MTRT_ID;
            }
            set
            {
                if ((this._MTRT_ID != value))
                {
                    this._MTRT_ID = value;
                }
            }
        }

        public string MTRT_Description
        {
            get
            {
                return this._MTRT_Description;
            }
            set
            {
                if ((this._MTRT_Description != value))
                {
                    this._MTRT_Description = value;
                }
            }
        }

        public int MTRT_Display_Order
        {
            get
            {
                return this._MTRT_Display_Order;
            }
            set
            {
                if ((this._MTRT_Display_Order != value))
                {
                    this._MTRT_Display_Order = value;
                }
            }
        }
    }

    public partial class GetTerminationMCDetailsResult
    {

        private string _Machine_Termination_Comments;

        private string _Machine_Termination_Username;

        private System.Nullable<int> _Machine_Termination_Reason;

        private string _Machine_End_Date;

        public GetTerminationMCDetailsResult()
        {
        }


        public string Machine_Termination_Comments
        {
            get
            {
                return this._Machine_Termination_Comments;
            }
            set
            {
                if ((this._Machine_Termination_Comments != value))
                {
                    this._Machine_Termination_Comments = value;
                }
            }
        }


        public string Machine_Termination_Username
        {
            get
            {
                return this._Machine_Termination_Username;
            }
            set
            {
                if ((this._Machine_Termination_Username != value))
                {
                    this._Machine_Termination_Username = value;
                }
            }
        }


        public System.Nullable<int> Machine_Termination_Reason
        {
            get
            {
                return this._Machine_Termination_Reason;
            }
            set
            {
                if ((this._Machine_Termination_Reason != value))
                {
                    this._Machine_Termination_Reason = value;
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
    }
    #endregion

    #region AddMachineTypeEntity
    public partial class GetSiteIconDetailsResult
    {

        private int _SiteIconID;

        private string _Machine_Type_Site_Icon;

        private string _Machine_Site_Icon_Display;

        private string _SiteIconPath;

        public GetSiteIconDetailsResult()
        {
        }

        public string Machine_Site_Icon_Display
        {
            get
            {
                return this._Machine_Site_Icon_Display;
            }
            set
            {
                if ((this._Machine_Site_Icon_Display != value))
                {
                    this._Machine_Site_Icon_Display = value;
                }
            }
        }
       
        public int SiteIconID
        {
            get
            {
                return this._SiteIconID;
            }
            set
            {
                if ((this._SiteIconID != value))
                {
                    this._SiteIconID = value;
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
    #endregion

    #region MachineModelAdminGamingEntity

    public partial class GetManufacturerbyMCTypeResult
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public GetManufacturerbyMCTypeResult()
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

    public partial class GetModelConfigurationsResult
    {

        private System.Nullable<bool> _Machine_Class_RecreateCancelledCredits;

        private System.Nullable<bool> _Machine_Class_JackpotAddedToCancelledCredits;

        private System.Nullable<bool> _Machine_Class_AddTrueCoinInToDrop;

        private System.Nullable<bool> _Machine_Class_UseCancelledCreditsAsTicketsPrinted;

        private System.Nullable<bool> _Machine_Class_RecreateTicketsInsertedfromDrop;

        public GetModelConfigurationsResult()
        {
        }


        public System.Nullable<bool> Machine_Class_RecreateCancelledCredits
        {
            get
            {
                return this._Machine_Class_RecreateCancelledCredits;
            }
            set
            {
                if ((this._Machine_Class_RecreateCancelledCredits != value))
                {
                    this._Machine_Class_RecreateCancelledCredits = value;
                }
            }
        }


        public System.Nullable<bool> Machine_Class_JackpotAddedToCancelledCredits
        {
            get
            {
                return this._Machine_Class_JackpotAddedToCancelledCredits;
            }
            set
            {
                if ((this._Machine_Class_JackpotAddedToCancelledCredits != value))
                {
                    this._Machine_Class_JackpotAddedToCancelledCredits = value;
                }
            }
        }


        public System.Nullable<bool> Machine_Class_AddTrueCoinInToDrop
        {
            get
            {
                return this._Machine_Class_AddTrueCoinInToDrop;
            }
            set
            {
                if ((this._Machine_Class_AddTrueCoinInToDrop != value))
                {
                    this._Machine_Class_AddTrueCoinInToDrop = value;
                }
            }
        }


        public System.Nullable<bool> Machine_Class_UseCancelledCreditsAsTicketsPrinted
        {
            get
            {
                return this._Machine_Class_UseCancelledCreditsAsTicketsPrinted;
            }
            set
            {
                if ((this._Machine_Class_UseCancelledCreditsAsTicketsPrinted != value))
                {
                    this._Machine_Class_UseCancelledCreditsAsTicketsPrinted = value;
                }
            }
        }


        public System.Nullable<bool> Machine_Class_RecreateTicketsInsertedfromDrop
        {
            get
            {
                return this._Machine_Class_RecreateTicketsInsertedfromDrop;
            }
            set
            {
                if ((this._Machine_Class_RecreateTicketsInsertedfromDrop != value))
                {
                    this._Machine_Class_RecreateTicketsInsertedfromDrop = value;
                }
            }
        }
    }

    public partial class GetMachineNamesOnMachineTypeResult
    {

        private string _Machine_Name;

        public GetMachineNamesOnMachineTypeResult()
        {
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
    }
    #endregion

    #region MachineModelAdminNonGamingEntity
    public partial class GetMachineClassListResult
    {

        private string _Machine_Name;

        private System.Nullable<int> _Manufacturer_ID;

        private string _Machine_Class_Model_Code;

        private System.Nullable<bool> _Machine_Class_DeListed;

        private System.Nullable<bool> _Machine_Class_Test_Machine;

        private string _Machine_Class_Category;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private System.Nullable<bool> _Depreciation_Policy_Use_Default;

        private string _Machine_Class_Release_Date;

        private string _Manufacturer_Name;

        private string _Machine_Type_Code;

        private System.Nullable<int> _Machine_Type_ID;

        public GetMachineClassListResult()
        {
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


        public System.Nullable<bool> Machine_Class_DeListed
        {
            get
            {
                return this._Machine_Class_DeListed;
            }
            set
            {
                if ((this._Machine_Class_DeListed != value))
                {
                    this._Machine_Class_DeListed = value;
                }
            }
        }


        public System.Nullable<bool> Machine_Class_Test_Machine
        {
            get
            {
                return this._Machine_Class_Test_Machine;
            }
            set
            {
                if ((this._Machine_Class_Test_Machine != value))
                {
                    this._Machine_Class_Test_Machine = value;
                }
            }
        }


        public string Machine_Class_Category
        {
            get
            {
                return this._Machine_Class_Category;
            }
            set
            {
                if ((this._Machine_Class_Category != value))
                {
                    this._Machine_Class_Category = value;
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


        public string Machine_Class_Release_Date
        {
            get
            {
                return this._Machine_Class_Release_Date;
            }
            set
            {
                if ((this._Machine_Class_Release_Date != value))
                {
                    this._Machine_Class_Release_Date = value;
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

        public System.Nullable<int> Machine_Type_ID
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
    }

    public partial class CheckAutoModelCodeExistsResult
    {

        private System.Nullable<bool> _System_Parameter_Auto_Generate_Model_Codes;

        public CheckAutoModelCodeExistsResult()
        {
        }


        public System.Nullable<bool> System_Parameter_Auto_Generate_Model_Codes
        {
            get
            {
                return this._System_Parameter_Auto_Generate_Model_Codes;
            }
            set
            {
                if ((this._System_Parameter_Auto_Generate_Model_Codes != value))
                {
                    this._System_Parameter_Auto_Generate_Model_Codes = value;
                }
            }
        }
    }
    #endregion

    #region SellMachineEntity
    public partial class GetMachineAssetDetailsResult
    {

        private int _Machine_ID;

        private string _Machine_Stock_No;

        private string _Machine_MAC_Address;

        private string _Machine_Class_Model_Code;

        private string _Machine_Name;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Alternative_Serial_Numbers;

        public GetMachineAssetDetailsResult()
        {
        }

        public string ModelName
        {
            get;
            set;

        }

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
    }
    #endregion
}
