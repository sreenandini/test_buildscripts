using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.rsp_GetManufacturer")]
        public ISingleResult<rsp_GetManufacturerResult> GetManufacturer([Parameter(Name = "Manufacturer_Name", DbType = "VarChar(50)")] string manufacturer_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturer_Name);
            return ((ISingleResult<rsp_GetManufacturerResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameCategory")]
        public ISingleResult<rsp_GetGameCategoryResult> GetGameCategory([Parameter(Name = "Game_Category_Name", DbType = "VarChar(50)")] string game_Category_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_Name);
            return ((ISingleResult<rsp_GetGameCategoryResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetGameCategoryGL")]
        public ISingleResult<rsp_GetGameCategoryGLResult>GetGameCategoryGL([Parameter(Name = "Game_Category_Name", DbType = "VarChar(50)")] string game_Category_Name, [Parameter(Name = "GameName", DbType = "VarChar(50)")] string gameName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_Name, gameName);
            return ((ISingleResult<rsp_GetGameCategoryGLResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateManufacturerDetails")]
        public int UpdateManufacturerDetails(
                    [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID,
                    [Parameter(Name = "Manufacturer_Name", DbType = "VarChar(50)")] string manufacturer_Name,
                    [Parameter(Name = "Manufacturer_Code", DbType = "VarChar(10)")] string manufacturer_Code,
                    [Parameter(Name = "Manufacturer_Sales_Address", DbType = "NText")] string manufacturer_Sales_Address,
                    [Parameter(Name = "Manufacturer_Sales_Contact", DbType = "VarChar(50)")] string manufacturer_Sales_Contact,
                    [Parameter(Name = "Manufacturer_Sales_EMail", DbType = "VarChar(50)")] string manufacturer_Sales_EMail,
                    [Parameter(Name = "Manufacturer_Sales_Postcode", DbType = "VarChar(10)")] string manufacturer_Sales_Postcode,
                    [Parameter(Name = "Manufacturer_Sales_Tel", DbType = "VarChar(50)")] string manufacturer_Sales_Tel,
                    [Parameter(Name = "Manufacturer_Service_Address", DbType = "NText")] string manufacturer_Service_Address,
                    [Parameter(Name = "Manufacturer_Service_Contact", DbType = "VarChar(50)")] string manufacturer_Service_Contact,
                    [Parameter(Name = "Manufacturer_Service_EMail", DbType = "VarChar(50)")] string manufacturer_Service_EMail,
                    [Parameter(Name = "Manufacturer_Service_Postcode", DbType = "VarChar(10)")] string manufacturer_Service_Postcode,
                    [Parameter(Name = "Manufacturer_Service_Tel", DbType = "VarChar(50)")] string manufacturer_Service_Tel,
                    [Parameter(Name = "Manufacturer_Coins_In_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Coins_In_Meter_Used,
                    [Parameter(Name = "Manufacturer_Coins_Out_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Coins_Out_Meter_Used,
                    [Parameter(Name = "Manufacturer_Coin_Drop_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Coin_Drop_Meter_Used,
                    [Parameter(Name = "Manufacturer_Handpay_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Handpay_Meter_Used,
                    [Parameter(Name = "Manufacturer_External_Credits_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_External_Credits_Meter_Used,
                    [Parameter(Name = "Manufacturer_Games_Bet_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Games_Bet_Meter_Used,
                    [Parameter(Name = "Manufacturer_Games_Won_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Games_Won_Meter_Used,
                    [Parameter(Name = "Manufacturer_Notes_Meter_Used", DbType = "Bit")] System.Nullable<bool> manufacturer_Notes_Meter_Used,
                    [Parameter(Name = "Manufacturer_Single_Coin_Build", DbType = "Bit")] System.Nullable<bool> manufacturer_Single_Coin_Build,
                    [Parameter(Name = "Manufacturer_Handpay_Added_To_Coin_Out", DbType = "Bit")] System.Nullable<bool> manufacturer_Handpay_Added_To_Coin_Out)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturer_ID, manufacturer_Name, manufacturer_Code, manufacturer_Sales_Address, manufacturer_Sales_Contact, manufacturer_Sales_EMail, manufacturer_Sales_Postcode, manufacturer_Sales_Tel, manufacturer_Service_Address, manufacturer_Service_Contact, manufacturer_Service_EMail, manufacturer_Service_Postcode, manufacturer_Service_Tel, manufacturer_Coins_In_Meter_Used, manufacturer_Coins_Out_Meter_Used, manufacturer_Coin_Drop_Meter_Used, manufacturer_Handpay_Meter_Used, manufacturer_External_Credits_Meter_Used, manufacturer_Games_Bet_Meter_Used, manufacturer_Games_Won_Meter_Used, manufacturer_Notes_Meter_Used, manufacturer_Single_Coin_Build, manufacturer_Handpay_Added_To_Coin_Out);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_VerifyManufacturerName")]
        public ISingleResult<rsp_VerifyManufacturerNameResult> VerifyManufacturerName([Parameter(Name = "Manufacturer_Name", DbType = "VarChar(50)")] string manufacturer_Name, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturer_Name, manufacturer_ID);
            return ((ISingleResult<rsp_VerifyManufacturerNameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.esp_InsertManufacturerName")]
        public ISingleResult<esp_InsertManufacturerNameResult> InsertManufacturerName([Parameter(Name = "Manufacturer_Name", DbType = "VarChar(50)")] string manufacturer_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturer_Name);
            return ((ISingleResult<esp_InsertManufacturerNameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_SaveGameCategory")]
        public int SaveGameCategory([Parameter(Name = "Category_ID", DbType = "Int")] System.Nullable<int> category_ID, [Parameter(Name = "Category_Name", DbType = "VarChar(50)")] string category_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), category_ID, category_Name);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_MultiGameLibraryThemesDetails")]
        public ISingleResult<rsp_MultiGameLibraryThemesDetailsResult> MultiGameLibraryThemesDetails([Parameter(Name = "GameTitleID", DbType = "Int")] System.Nullable<int> gameTitleID, [Parameter(Name = "GameCatID", DbType = "Int")] System.Nullable<int> gameCatID, [Parameter(Name = "ManufacturerID", DbType = "Int")] System.Nullable<int> manufacturerID, [Parameter(Name = "GameCategoryID", DbType = "Int")] System.Nullable<int> gameCategoryID, [Parameter(Name = "MachineManufacturerID", DbType = "Int")] System.Nullable<int> iMachineManufacturerId, [Parameter(Name = "GameName", DbType = "VarChar(250)")] string GameName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gameTitleID, gameCatID, manufacturerID, gameCategoryID, iMachineManufacturerId, GameName);
            return ((ISingleResult<rsp_MultiGameLibraryThemesDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPayTable")]
        public ISingleResult<rsp_GetPayTableResult> GetPayTable([Parameter(Name = "GameID", DbType = "Int")] System.Nullable<int> gameID, [Parameter(Name = "ManufacturerID", DbType = "Int")] System.Nullable<int> manufacturerID, [Parameter(Name = "GameCategoryID", DbType = "Int")] System.Nullable<int> gameCategoryID, [Parameter(Name = "Machine_Stock_No", DbType = "VarChar(50)")] string Machine_Stock_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gameID, manufacturerID, gameCategoryID, Machine_Stock_No);
            return ((ISingleResult<rsp_GetPayTableResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameDetails")]
        public ISingleResult<rsp_GetGameDetailsResult> GetGameDetails([Parameter(Name = "ManufacturerID", DbType = "Int")] System.Nullable<int> manufacturerID, [Parameter(Name = "GameCategoryID", DbType = "Int")] System.Nullable<int> gameCategoryID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturerID, gameCategoryID);
            return ((ISingleResult<rsp_GetGameDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameCategoryByGameID")]
        public ISingleResult<rsp_GetGameCategoryByGameIDResult> GetGameCategoryByGameID([Parameter(Name = "Game_Category_ID", DbType = "Int")] System.Nullable<int> game_Category_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_ID);
            return ((ISingleResult<rsp_GetGameCategoryByGameIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameDetailsFromGameLibrary")]
        public ISingleResult<rsp_GetGameDetailsFromGameLibraryResult> GetGameDetailsFromGameLibrary()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetGameDetailsFromGameLibraryResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAssetDetailsForGame")]
        public ISingleResult<rsp_GetAssetDetailsForGameResult> GetAssetDetailsForGame([Parameter(Name = "GameId", DbType = "Int")] System.Nullable<int> gameId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gameId);
            return ((ISingleResult<rsp_GetAssetDetailsForGameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPayTableForGameTitle")]
        public ISingleResult<rsp_GetPayTableForGameTitleResult> GetPayTableForGameTitle([Parameter(Name = "GameID", DbType = "Int")] System.Nullable<int> gameID, [Parameter(Name = "ManufacturerID", DbType = "Int")] System.Nullable<int> manufacturerID, [Parameter(Name = "GameCategoryID", DbType = "Int")] System.Nullable<int> gameCategoryID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), gameID, manufacturerID, gameCategoryID);
            return ((ISingleResult<rsp_GetPayTableForGameTitleResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameDetailsByGameGroup")]
        public ISingleResult<rsp_GetGameDetailsByGameGroupResult> GetGameDetailsByGameGroup([Parameter(Name = "GroupId", DbType = "Int")] System.Nullable<int> groupId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupId);
            return ((ISingleResult<rsp_GetGameDetailsByGameGroupResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateGameIDForSlotGames")]
        public int UpdateGameIDForSlotGames([Parameter(Name = "GroupId", DbType = "VarChar(10)")] string groupId, [Parameter(Name = "PaytableIDs", DbType = "VarChar(MAX)")] string paytableIDs)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupId, paytableIDs);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPayTableDetails")]
        public ISingleResult<rsp_GetPayTableDetailsResult> GetPayTableDetails([Parameter(Name = "Paytable_ID", DbType = "Int")] System.Nullable<int> paytable_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), paytable_ID);
            return ((ISingleResult<rsp_GetPayTableDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdatePayTableTheoreticalPayout")]
        public int UpdatePayTableTheoreticalPayout([Parameter(Name = "Paytable_ID", DbType = "Int")] System.Nullable<int> paytable_ID, [Parameter(Name = "TheoreticalPayout", DbType = "Float")] System.Nullable<double> theoreticalPayout)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), paytable_ID, theoreticalPayout);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGamesByCategory")]
        public ISingleResult<rsp_GetGamesByCategoryResult> GetGamesByCategory([Parameter(Name = "Game_Category_Name", DbType = "VarChar(50)")] string game_Category_Name, [Parameter(Name = "Manufacturer", DbType = "VarChar(50)")] string manufacturer, [Parameter(Name = "GameID", DbType = "Int")] int GameID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_Name, manufacturer, GameID);
            return ((ISingleResult<rsp_GetGamesByCategoryResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGamesByManufacturer")]
        public ISingleResult<rsp_GetGamesByManufacturerResult> GetGamesByManufacturer([Parameter(Name = "Game_Category_Name", DbType = "VarChar(50)")] string game_Category_Name, [Parameter(Name = "Manufacturer", DbType = "VarChar(50)")] string manufacturer, [Parameter(Name = "GameID", DbType = "Int")] int GameID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_Name, manufacturer, GameID);
            return ((ISingleResult<rsp_GetGamesByManufacturerResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_IsFormedGame")]
        public ISingleResult<rsp_IsFormedGameResult> IsFormedGame([Parameter(Name = "Game_Ids", DbType = "VarChar(MAX)")] string game_Ids)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Ids);
            return ((ISingleResult<rsp_IsFormedGameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_MapGames")]
        public int MapGames([Parameter(Name = "GroupId", DbType = "VarChar(10)")] string groupId, [Parameter(Name = "Game_Ids", DbType = "VarChar(MAX)")] string game_Ids)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupId, game_Ids);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateMachineClass")]
        public int UpdateMachineClass([Parameter(Name = "Mac_Class_Ids", DbType = "VarChar(MAX)")] string mac_Class_Ids, [Parameter(Name = "Game_Tile_Id", DbType = "Int")] System.Nullable<int> game_Tile_Id, [Parameter(Name = "Remove", DbType = "Int")] System.Nullable<int> remove)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mac_Class_Ids, game_Tile_Id, remove);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameTitleDetailsByTitleId")]
        public ISingleResult<rsp_GetGameTitleDetailsByTitleIdResult> GetGameTitleDetailsByTitleId([Parameter(Name = "Game_Title", DbType = "Int")] System.Nullable<int> game_Title)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Title);
            return ((ISingleResult<rsp_GetGameTitleDetailsByTitleIdResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_VerifyGameTitleIsExists")]
        public ISingleResult<rsp_VerifyGameTitleIsExistsResult> VerifyGameTitleIsExists([Parameter(Name = "Game_Title", DbType = "VarChar(100)")] string game_Title)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Title);
            return ((ISingleResult<rsp_VerifyGameTitleIsExistsResult>)(result.ReturnValue));
        }

      
        [Function(Name = "dbo.usp_InsertGameTitle")]
        public int InsertGameTitle([Parameter(Name = "Game_Category_ID", DbType = "Int")] System.Nullable<int> game_Category_ID, [Parameter(Name = "Game_Title", DbType = "VarChar(100)")] string game_Title, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID, [Parameter(Name = "IsMultiGame", DbType = "Bit")] System.Nullable<bool> IsMultiGame)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_ID, game_Title, manufacturer_ID, IsMultiGame);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateGameTitle")]
        public int UpdateGameTitle([Parameter(Name = "Game_Category_ID", DbType = "Int")] System.Nullable<int> game_Category_ID, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID, [Parameter(Name = "Old_Game_Title", DbType = "VarChar(100)")] string old_Game_Title, [Parameter(Name = "New_Game_Title", DbType = "VarChar(100)")] string new_Game_Title, [Parameter(Name = "IsMultiGame", DbType = "Bit")] System.Nullable<bool> IsMultiGame)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), game_Category_ID, manufacturer_ID, old_Game_Title, new_Game_Title, IsMultiGame);
            return ((int)(result.ReturnValue));
        }
    }
    

    public partial class rsp_GetManufacturerResult
    {

        private string _Manufacturer_Name;

        private int _Manufacturer_ID;

        public rsp_GetManufacturerResult()
        {
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

        [Column(Storage = "_Manufacturer_ID", DbType = "Int NOT NULL")]
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
    }

    public partial class rsp_GetGameCategoryResult
    {

        private int _Game_Category_ID;

        private string _Game_Category_Name;

        public rsp_GetGameCategoryResult()
        {
        }

        [Column(Storage = "_Game_Category_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_VerifyManufacturerNameResult
    {

        private string _Manufacturer_Name;

        public rsp_VerifyManufacturerNameResult()
        {
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
    }

    public partial class esp_InsertManufacturerNameResult
    {

        private System.Nullable<decimal> _Manufacturer_ID;

        public esp_InsertManufacturerNameResult()
        {
        }

        [Column(Storage = "_Manufacturer_ID", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> Manufacturer_ID
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

    public partial class rsp_MultiGameLibraryThemesDetailsResult
    {

        private System.Nullable<int> _MG_Game_ID;

        private string _GameName;

        private System.Nullable<int> _SerialNo;

        private string _Manufacturer;

        private string _Alias_Machine_Name;

        private System.Nullable<int> _CategoryID;

        private string _Category;

        private System.Nullable<int> _GroupID;

        private System.Nullable<int> _Installation_ID;

        public rsp_MultiGameLibraryThemesDetailsResult()
        {
        }

        [Column(Storage = "_MG_Game_ID", DbType = "Int")]
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

        [Column(Storage = "_SerialNo", DbType = "Int")]
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

        [Column(Storage = "_Manufacturer", DbType = "VarChar(MAX)")]
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

        [Column(Storage = "_Alias_Machine_Name", DbType = "VarChar(MAX)")]
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

        [Column(Storage = "_CategoryID", DbType = "Int")]
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

        [Column(Storage = "_Category", DbType = "VarChar(50)")]
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

        [Column(Storage = "_GroupID", DbType = "Int")]
        public System.Nullable<int> GroupID
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

        [Column(Storage = "_Installation_ID", DbType = "Int")]
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

    public partial class rsp_GetPayTableResult
    {

        private int _Paytable_ID;

        private string _Denom;

        private System.Nullable<double> _Payout;

        private string _PT_Description;

        private double _MaxBet;

        private double _TheoreticalPayout;

        public rsp_GetPayTableResult()
        {
        }

        [Column(Storage = "_Paytable_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Denom", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Payout", DbType = "Float")]
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

        [Column(Storage = "_PT_Description", DbType = "VarChar(100)")]
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

        [Column(Storage = "_MaxBet", DbType = "Float NOT NULL")]
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

        [Column(Storage = "_TheoreticalPayout", DbType = "Float NOT NULL")]
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

    public partial class rsp_GetGameDetailsResult
    {

        private int _GameTitleId;

        private string _GameTitle;

        private int _CategoryID;

        private string _CategoryName;

        private string _ManufacturerName;

        public rsp_GetGameDetailsResult()
        {
        }

        [Column(Storage = "_GameTitleId", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_GameTitle", DbType = "VarChar(100)")]
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

        [Column(Storage = "_CategoryID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_CategoryName", DbType = "VarChar(50)")]
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

        [Column(Storage = "_ManufacturerName", DbType = "VarChar(50)")]
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

    public partial class rsp_GetGameCategoryByGameIDResult
    {

        private string _Game_Category_Name;

        public rsp_GetGameCategoryByGameIDResult()
        {
        }

        [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_GetGameDetailsFromGameLibraryResult
    {

        private int _MG_Game_ID;

        private string _MG_Game_Name;

        public rsp_GetGameDetailsFromGameLibraryResult()
        {
        }

        [Column(Storage = "_MG_Game_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_MG_Game_Name", DbType = "VarChar(100)")]
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

    public partial class rsp_GetAssetDetailsForGameResult
    {

        private int _IGI_ID;

        private string _Site_Name;

        private string _Bar_Position_Name;

        private string _Machine_Stock_No;

        private string _Machine_Manufacturers_Serial_No;

        private string _Game_Part_Number;

        private int _IsGameActive;

        public rsp_GetAssetDetailsForGameResult()
        {
        }

        [Column(Storage = "_IGI_ID", DbType = "Int NOT NULL")]
        public int IGI_ID
        {
            get
            {
                return this._IGI_ID;
            }
            set
            {
                if ((this._IGI_ID != value))
                {
                    this._IGI_ID = value;
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

        [Column(Storage = "_Game_Part_Number", DbType = "VarChar(20)")]
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

        [Column(Storage = "_IsGameActive", DbType = "Int NOT NULL")]
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

    public partial class rsp_GetPayTableForGameTitleResult
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

        public rsp_GetPayTableForGameTitleResult()
        {
        }

        [Column(Storage = "_Paytable_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Denom", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Payout", DbType = "Float")]
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

        [Column(Storage = "_PT_Description", DbType = "VarChar(100)")]
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

        [Column(Storage = "_MaxBet", DbType = "Float NOT NULL")]
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

        [Column(Storage = "_TheoreticalPayout", DbType = "Float NOT NULL")]
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

        [Column(Storage = "_Game_Title", DbType = "VARCHAR(100) NULL")]
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

        [Column(Storage = "_MG_Game_ID", DbType = "Int NULL")]
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

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(100)")]
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

    public partial class rsp_GetGameDetailsByGameGroupResult
    {

        private int _MG_Game_ID;

        private string _MG_Game_Name;

        private int _MG_Group_ID;

        public rsp_GetGameDetailsByGameGroupResult()
        {
        }

        [Column(Storage = "_MG_Game_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_MG_Game_Name", DbType = "VarChar(100)")]
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

        [Column(Storage = "_MG_Group_ID", DbType = "Int NOT NULL")]
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

    public partial class rsp_GetPayTableDetailsResult
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

        public rsp_GetPayTableDetailsResult()
        {
        }

        [Column(Storage = "_GAMENAME", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Paytable_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_ID", DbType = "Int")]
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

        [Column(Storage = "_Payout", DbType = "Float")]
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

        [Column(Storage = "_PT_Description", DbType = "VarChar(100)")]
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

        [Column(Storage = "_MaxBet", DbType = "Float NOT NULL")]
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

        [Column(Storage = "_TheoreticalPayout", DbType = "Float NOT NULL")]
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

        [Column(Storage = "_HQ_Paytable_ID", DbType = "Int")]
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

        [Column(Storage = "_Site_ID", DbType = "Int")]
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

    //public partial class rsp_GetGamesByCategoryResult
    //{

    //    private int _Game_Title_ID;

    //    private string _Game_Title;

    //    private System.Nullable<int> _Game_Category_ID;

    //    private string _Game_Category_Name;

    //    private string _Manufacturer_Name;

    //    public rsp_GetGamesByCategoryResult()
    //    {
    //    }

    //    [Column(Storage = "_Game_Title_ID", DbType = "Int NOT NULL")]
    //    public int Game_Title_ID
    //    {
    //        get
    //        {
    //            return this._Game_Title_ID;
    //        }
    //        set
    //        {
    //            if ((this._Game_Title_ID != value))
    //            {
    //                this._Game_Title_ID = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
    //    public string Game_Title
    //    {
    //        get
    //        {
    //            return this._Game_Title;
    //        }
    //        set
    //        {
    //            if ((this._Game_Title != value))
    //            {
    //                this._Game_Title = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Game_Category_ID", DbType = "Int")]
    //    public System.Nullable<int> Game_Category_ID
    //    {
    //        get
    //        {
    //            return this._Game_Category_ID;
    //        }
    //        set
    //        {
    //            if ((this._Game_Category_ID != value))
    //            {
    //                this._Game_Category_ID = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
    //    public string Game_Category_Name
    //    {
    //        get
    //        {
    //            return this._Game_Category_Name;
    //        }
    //        set
    //        {
    //            if ((this._Game_Category_Name != value))
    //            {
    //                this._Game_Category_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
    //    public string Manufacturer_Name
    //    {
    //        get
    //        {
    //            return this._Manufacturer_Name;
    //        }
    //        set
    //        {
    //            if ((this._Manufacturer_Name != value))
    //            {
    //                this._Manufacturer_Name = value;
    //            }
    //        }
    //    }
    //}

    public partial class rsp_GetGamesByCategoryResult
    {

        private int _Game_Title_ID;

        private string _Game_Title;

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Category_Name;

        private string _Manufacturer_Name;

        private System.Nullable<bool> _IsMultiGame;

        public rsp_GetGamesByCategoryResult()
        {
        }

        [Column(Storage = "_Game_Title_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Game_Category_ID", DbType = "Int")]
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

        [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_IsMultiGame", DbType = "Bit")]
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
    }

    //public partial class rsp_GetGamesByManufacturerResult
    //{

    //    private int _Game_Title_ID;

    //    private string _Game_Title;

    //    private System.Nullable<int> _Manufacturer_ID;

    //    private string _Game_Category_Name;

    //    private string _Manufacturer_Name;

    //    public rsp_GetGamesByManufacturerResult()
    //    {
    //    }

    //    [Column(Storage = "_Game_Title_ID", DbType = "Int NOT NULL")]
    //    public int Game_Title_ID
    //    {
    //        get
    //        {
    //            return this._Game_Title_ID;
    //        }
    //        set
    //        {
    //            if ((this._Game_Title_ID != value))
    //            {
    //                this._Game_Title_ID = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
    //    public string Game_Title
    //    {
    //        get
    //        {
    //            return this._Game_Title;
    //        }
    //        set
    //        {
    //            if ((this._Game_Title != value))
    //            {
    //                this._Game_Title = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Manufacturer_ID", DbType = "Int")]
    //    public System.Nullable<int> Manufacturer_ID
    //    {
    //        get
    //        {
    //            return this._Manufacturer_ID;
    //        }
    //        set
    //        {
    //            if ((this._Manufacturer_ID != value))
    //            {
    //                this._Manufacturer_ID = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
    //    public string Game_Category_Name
    //    {
    //        get
    //        {
    //            return this._Game_Category_Name;
    //        }
    //        set
    //        {
    //            if ((this._Game_Category_Name != value))
    //            {
    //                this._Game_Category_Name = value;
    //            }
    //        }
    //    }

    //    [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
    //    public string Manufacturer_Name
    //    {
    //        get
    //        {
    //            return this._Manufacturer_Name;
    //        }
    //        set
    //        {
    //            if ((this._Manufacturer_Name != value))
    //            {
    //                this._Manufacturer_Name = value;
    //            }
    //        }
    //    }
    //}
    public partial class rsp_GetGamesByManufacturerResult
    {

        private int _Game_Title_ID;

        private string _Game_Title;

        private System.Nullable<int> _Manufacturer_ID;

        private string _Game_Category_Name;

        private string _Manufacturer_Name;

        private System.Nullable<bool> _IsMultiGame;

        public rsp_GetGamesByManufacturerResult()
        {
        }

        [Column(Storage = "_Game_Title_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_IsMultiGame", DbType = "Bit")]
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
    }

    public partial class rsp_IsFormedGameResult
    {

        private string _Game_ID;

        private string _Machine_Class_ID;

        private int _IsMultiGame;

        public rsp_IsFormedGameResult()
        {
        }

        [Column(Storage = "_Game_ID", DbType = "VarChar(MAX)")]
        public string Game_ID
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

        [Column(Storage = "_Machine_Class_ID", DbType = "VarChar(MAX)")]
        public string Machine_Class_ID
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

        [Column(Storage = "_IsMultiGame", DbType = "Int NOT NULL")]
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

    public partial class rsp_GetGameTitleDetailsByTitleIdResult
    {

        private int _Game_Title_ID;

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Title;

        private System.Nullable<int> _Manufacturer_ID;

        private System.Nullable<bool> _IsMultiGame;

        public rsp_GetGameTitleDetailsByTitleIdResult()
        {
        }

        [Column(Storage = "_Game_Title_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_Category_ID", DbType = "Int")]
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

        [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
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

        [Column(Storage = "_IsMultiGame", DbType = "Bit")]
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
    }		

    public partial class rsp_VerifyGameTitleIsExistsResult
    {

        private string _Game_Title;

        public rsp_VerifyGameTitleIsExistsResult()
        {
        }

        [Column(Storage = "_Game_Title", DbType = "VarChar(100)")]
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

    public partial class rsp_GetGameCategoryGLResult
    {

        private int _Game_Category_ID;

        private string _Game_Category_Name;

        public rsp_GetGameCategoryGLResult()
        {
        }

        [Column(Storage = "_Game_Category_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
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







}
