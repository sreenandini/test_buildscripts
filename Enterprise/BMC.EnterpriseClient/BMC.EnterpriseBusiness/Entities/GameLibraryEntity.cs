using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public partial class GamesByCategory
    {

        private int _Game_Title_ID;

        private string _Game_Title;

        private int _Game_Category_ID;

        private string _Game_Category_Name;

        private string _Manufacturer_Name;

        public GamesByCategory()
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

    public partial class Manufacturer
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public Manufacturer()
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

    public partial class GameCategory
    {

        private int _Game_Category_ID;

        private string _Game_Category_Name;

        public GameCategory()
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

    public partial class GamesByManufacturer
    {

        private System.Nullable<int> _Game_Title_ID;

        private string _Game_Title;

        private System.Nullable<int> _Manufacturer_ID;

        private string _Manufacturer_Name;

        private string _Game_Category_Name;

        public GamesByManufacturer()
        {
        }


        public System.Nullable<int> Game_Title_ID
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

    public partial class ManufacturerDetails
    {
        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        private string _Manufacturer_Service_Contact;

        private string _Manufacturer_Service_EMail;

        private string _Manufacturer_Service_Tel;

        private string _Manufacturer_Service_Address;

        private string _Manufacturer_Service_Postcode;

        private string _Manufacturer_Sales_Contact;

        private string _Manufacturer_Sales_EMail;

        private string _Manufacturer_Sales_Tel;

        private string _Manufacturer_Sales_Address;

        private string _Manufacturer_Sales_Postcode;

        private string _Manufacturer_Code;

        private System.Nullable<bool> _Manufacturer_Coins_In_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Coins_Out_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Coin_Drop_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Handpay_Meter_Used;

        private System.Nullable<bool> _Manufacturer_External_Credits_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Games_Bet_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Games_Won_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Notes_Meter_Used;

        private System.Nullable<bool> _Manufacturer_Single_Coin_Build;

        private System.Nullable<bool> _Manufacturer_Handpay_Added_To_Coin_Out;

        public ManufacturerDetails()
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

        public string Manufacturer_Service_Contact
        {
            get
            {
                return this._Manufacturer_Service_Contact;
            }
            set
            {
                if ((this._Manufacturer_Service_Contact != value))
                {
                    this._Manufacturer_Service_Contact = value;
                }
            }
        }

        public string Manufacturer_Service_EMail
        {
            get
            {
                return this._Manufacturer_Service_EMail;
            }
            set
            {
                if ((this._Manufacturer_Service_EMail != value))
                {
                    this._Manufacturer_Service_EMail = value;
                }
            }
        }

        public string Manufacturer_Service_Tel
        {
            get
            {
                return this._Manufacturer_Service_Tel;
            }
            set
            {
                if ((this._Manufacturer_Service_Tel != value))
                {
                    this._Manufacturer_Service_Tel = value;
                }
            }
        }

        public string Manufacturer_Service_Address
        {
            get
            {
                return this._Manufacturer_Service_Address;
            }
            set
            {
                if ((this._Manufacturer_Service_Address != value))
                {
                    this._Manufacturer_Service_Address = value;
                }
            }
        }

        public string Manufacturer_Service_Postcode
        {
            get
            {
                return this._Manufacturer_Service_Postcode;
            }
            set
            {
                if ((this._Manufacturer_Service_Postcode != value))
                {
                    this._Manufacturer_Service_Postcode = value;
                }
            }
        }

        public string Manufacturer_Sales_Contact
        {
            get
            {
                return this._Manufacturer_Sales_Contact;
            }
            set
            {
                if ((this._Manufacturer_Sales_Contact != value))
                {
                    this._Manufacturer_Sales_Contact = value;
                }
            }
        }

        public string Manufacturer_Sales_EMail
        {
            get
            {
                return this._Manufacturer_Sales_EMail;
            }
            set
            {
                if ((this._Manufacturer_Sales_EMail != value))
                {
                    this._Manufacturer_Sales_EMail = value;
                }
            }
        }

        public string Manufacturer_Sales_Tel
        {
            get
            {
                return this._Manufacturer_Sales_Tel;
            }
            set
            {
                if ((this._Manufacturer_Sales_Tel != value))
                {
                    this._Manufacturer_Sales_Tel = value;
                }
            }
        }

        public string Manufacturer_Sales_Address
        {
            get
            {
                return this._Manufacturer_Sales_Address;
            }
            set
            {
                if ((this._Manufacturer_Sales_Address != value))
                {
                    this._Manufacturer_Sales_Address = value;
                }
            }
        }

        public string Manufacturer_Sales_Postcode
        {
            get
            {
                return this._Manufacturer_Sales_Postcode;
            }
            set
            {
                if ((this._Manufacturer_Sales_Postcode != value))
                {
                    this._Manufacturer_Sales_Postcode = value;
                }
            }
        }

        public string Manufacturer_Code
        {
            get
            {
                return this._Manufacturer_Code;
            }
            set
            {
                if ((this._Manufacturer_Code != value))
                {
                    this._Manufacturer_Code = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Coins_In_Meter_Used
        {
            get
            {
                return this._Manufacturer_Coins_In_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Coins_In_Meter_Used != value))
                {
                    this._Manufacturer_Coins_In_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Coins_Out_Meter_Used
        {
            get
            {
                return this._Manufacturer_Coins_Out_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Coins_Out_Meter_Used != value))
                {
                    this._Manufacturer_Coins_Out_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Coin_Drop_Meter_Used
        {
            get
            {
                return this._Manufacturer_Coin_Drop_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Coin_Drop_Meter_Used != value))
                {
                    this._Manufacturer_Coin_Drop_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Handpay_Meter_Used
        {
            get
            {
                return this._Manufacturer_Handpay_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Handpay_Meter_Used != value))
                {
                    this._Manufacturer_Handpay_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_External_Credits_Meter_Used
        {
            get
            {
                return this._Manufacturer_External_Credits_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_External_Credits_Meter_Used != value))
                {
                    this._Manufacturer_External_Credits_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Games_Bet_Meter_Used
        {
            get
            {
                return this._Manufacturer_Games_Bet_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Games_Bet_Meter_Used != value))
                {
                    this._Manufacturer_Games_Bet_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Games_Won_Meter_Used
        {
            get
            {
                return this._Manufacturer_Games_Won_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Games_Won_Meter_Used != value))
                {
                    this._Manufacturer_Games_Won_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Notes_Meter_Used
        {
            get
            {
                return this._Manufacturer_Notes_Meter_Used;
            }
            set
            {
                if ((this._Manufacturer_Notes_Meter_Used != value))
                {
                    this._Manufacturer_Notes_Meter_Used = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Single_Coin_Build
        {
            get
            {
                return this._Manufacturer_Single_Coin_Build;
            }
            set
            {
                if ((this._Manufacturer_Single_Coin_Build != value))
                {
                    this._Manufacturer_Single_Coin_Build = value;
                }
            }
        }

        public System.Nullable<bool> Manufacturer_Handpay_Added_To_Coin_Out
        {
            get
            {
                return this._Manufacturer_Handpay_Added_To_Coin_Out;
            }
            set
            {
                if ((this._Manufacturer_Handpay_Added_To_Coin_Out != value))
                {
                    this._Manufacturer_Handpay_Added_To_Coin_Out = value;
                }
            }

        }
    }

    public partial class PayTableDetails
    {

        private string _GAMENAME;

        private int _Paytable_ID;

        private System.Nullable<int> _Game_ID;

        private System.Nullable<double> _Payout;

        private string _PT_Description;

        private double _MaxBet;

        private double _TheoreticalPayout;

        private System.Nullable<int> _HQ_Paytable_ID;

        private System.Nullable<int> _Site_ID;

        public PayTableDetails()
        {
        }

        public string GAMENAME
        {
            get
            {
                return this._GAMENAME;
            }
            set
            {
                if ((this._GAMENAME != value))
                {
                    this._GAMENAME = value;
                }
            }
        }

        public int Paytable_ID
        {
            get
            {
                return this._Paytable_ID;
            }
            set
            {
                if ((this._Paytable_ID != value))
                {
                    this._Paytable_ID = value;
                }
            }
        }

        public System.Nullable<int> Game_ID
        {
            get
            {
                return this._Game_ID;
            }
            set
            {
                if ((this._Game_ID != value))
                {
                    this._Game_ID = value;
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

        public string PT_Description
        {
            get
            {
                return this._PT_Description;
            }
            set
            {
                if ((this._PT_Description != value))
                {
                    this._PT_Description = value;
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

        public System.Nullable<int> HQ_Paytable_ID
        {
            get
            {
                return this._HQ_Paytable_ID;
            }
            set
            {
                if ((this._HQ_Paytable_ID != value))
                {
                    this._HQ_Paytable_ID = value;
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
    }

    public partial class GameDetails
    {

        private int _GameTitleId;

        private string _GameTitle;

        private int _CategoryID;

        private string _CategoryName;

        private string _ManufacturerName;

        public GameDetails()
        {
        }

        public int GameTitleId
        {
            get
            {
                return this._GameTitleId;
            }
            set
            {
                if ((this._GameTitleId != value))
                {
                    this._GameTitleId = value;
                }
            }
        }

        public string GameTitle
        {
            get
            {
                return this._GameTitle;
            }
            set
            {
                if ((this._GameTitle != value))
                {
                    this._GameTitle = value;
                }
            }
        }

        public int CategoryID
        {
            get
            {
                return this._CategoryID;
            }
            set
            {
                if ((this._CategoryID != value))
                {
                    this._CategoryID = value;
                }
            }
        }

        public string CategoryName
        {
            get
            {
                return this._CategoryName;
            }
            set
            {
                if ((this._CategoryName != value))
                {
                    this._CategoryName = value;
                }
            }
        }

        public string ManufacturerName
        {
            get
            {
                return this._ManufacturerName;
            }
            set
            {
                if ((this._ManufacturerName != value))
                {
                    this._ManufacturerName = value;
                }
            }
        }
    }

    public partial class GameDetailsFromGameLibrary
    {

        private int _MG_Game_ID;

        private string _MG_Game_Name;

        public GameDetailsFromGameLibrary()
        {
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
    }

    public partial class GameDetailsByGameGroup
    {

        private int _MG_Game_ID;

        private string _MG_Game_Name;

        private int _MG_Group_ID;

        public GameDetailsByGameGroup()
        {
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

        public int MG_Group_ID
        {
            get
            {
                return this._MG_Group_ID;
            }
            set
            {
                if ((this._MG_Group_ID != value))
                {
                    this._MG_Group_ID = value;
                }
            }
        }
    }

    public partial class AssetDetailsForGame
    {

        private string _Site_Name;

        private string _Bar_Position_Name;

        private string _Machine_Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _Game_Part_Number;

        private int _IsGameActive;

        public AssetDetailsForGame()
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

        public string Game_Part_Number
        {
            get
            {
                return this._Game_Part_Number;
            }
            set
            {
                if ((this._Game_Part_Number != value))
                {
                    this._Game_Part_Number = value;
                }
            }
        }

        public int IsGameActive
        {
            get
            {
                return this._IsGameActive;
            }
            set
            {
                if ((this._IsGameActive != value))
                {
                    this._IsGameActive = value;
                }
            }
        }
    }

    public partial class MultiGameLibraryThemesDetails
    {

        private int _MG_Game_ID;

        private string _GameName;

        private string _Version;

        private System.Nullable<int> _SerialNo;

        private string _Manufacturer;

        private System.Nullable<int> _CategoryID;

        private string _Category;

        private int _GroupID;
        
        private string _Alias_Machine_Name;
        
        public MultiGameLibraryThemesDetails()
        {
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

        public string Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                if ((this._Version != value))
                {
                    this._Version = value;
                }
            }
        }

        public System.Nullable<int> SerialNo
        {
            get
            {
                return this._SerialNo;
            }
            set
            {
                if ((this._SerialNo != value))
                {
                    this._SerialNo = value;
                }
            }
        }

        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }

        public System.Nullable<int> CategoryID
        {
            get
            {
                return this._CategoryID;
            }
            set
            {
                if ((this._CategoryID != value))
                {
                    this._CategoryID = value;
                }
            }
        }

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

        public int GroupID
        {
            get
            {
                return this._GroupID;
            }
            set
            {
                if ((this._GroupID != value))
                {
                    this._GroupID = value;
                }
            }
        }

        public string Alias_Machine_Name
        {
            get
            {
                return this._Alias_Machine_Name;
            }
            set
            {
                if ((this._Alias_Machine_Name != value))
                {
                    this._Alias_Machine_Name = value;
                }
            }
        }
    }

    public partial class PayTableForGameTitle
    {

        private int _Paytable_ID;

        private string _Denom;

        private System.Nullable<double> _Payout;

        private string _PT_Description;

        private double _MaxBet;

        private double _TheoreticalPayout;

        private string _Game_Title;

        private int _MG_Game_ID;

        private string _Machine_Stock_No;

        public PayTableForGameTitle()
        {
        }

        public int Paytable_ID
        {
            get
            {
                return this._Paytable_ID;
            }
            set
            {
                if ((this._Paytable_ID != value))
                {
                    this._Paytable_ID = value;
                }
            }
        }

        public string Denom
        {
            get
            {
                return this._Denom;
            }
            set
            {
                if ((this._Denom != value))
                {
                    this._Denom = value;
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

        public string PT_Description
        {
            get
            {
                return this._PT_Description;
            }
            set
            {
                if ((this._PT_Description != value))
                {
                    this._PT_Description = value;
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
    }

    public partial class PayTable
    {

        private int _Paytable_ID;

        private string _Denom;

        private System.Nullable<double> _Payout;

        private string _PT_Description;

        private double _MaxBet;

        private double _TheoreticalPayout;

        public PayTable()
        {
        }

        public int Paytable_ID
        {
            get
            {
                return this._Paytable_ID;
            }
            set
            {
                if ((this._Paytable_ID != value))
                {
                    this._Paytable_ID = value;
                }
            }
        }

        public string Denom
        {
            get
            {
                return this._Denom;
            }
            set
            {
                if ((this._Denom != value))
                {
                    this._Denom = value;
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

        public string PT_Description
        {
            get
            {
                return this._PT_Description;
            }
            set
            {
                if ((this._PT_Description != value))
                {
                    this._PT_Description = value;
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

    public class FormedGame
    {

        private string _Game_ID;

        private string _Machine_Class_ID;

        private int _IsMultiGame;

        public FormedGame()
        {
        }

        public string Game_IDs
        {
            get
            {
                return this._Game_ID;
            }
            set
            {
                if ((this._Game_ID != value))
                {
                    this._Game_ID = value;
                }
            }
        }

        public string Machine_Class_IDs
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

        public int IsMultiGame
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
    }

    public partial class GetGameTitleDetailsByTitleId
    {

        private int _Game_Title_ID;

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Title;

        private System.Nullable<int> _Manufacturer_ID;

        private System.Nullable<bool> _IsMultiGame;

        private System.Nullable<int> _EnrolledStatus;

        public GetGameTitleDetailsByTitleId()
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

        public System.Nullable<int> EnrolledStatus
        {
            get
            {
                return this._EnrolledStatus;
            }
            set
            {
                if ((this._EnrolledStatus != value))
                {
                    this._EnrolledStatus = value;
                }
            }
        }
    }
}
