using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using BMC.EnterpriseDataAccess;
using System.Collections;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using BMC.Common.ExceptionManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;

namespace BMC.EnterpriseBusiness.Business
{
    public class GameLibraryBiz
    {
        private static GameLibraryBiz _GameLibraryBiz;

        public GameLibraryBiz() { }

        /// <summary>
        /// To create a new instance for GameLibraryBiz and return the created instance
        /// </summary>
        /// <returns></returns>
        public static GameLibraryBiz CreateInstance()
        {
            if (_GameLibraryBiz == null)
                _GameLibraryBiz = new GameLibraryBiz();

            return _GameLibraryBiz;
        }

        /// <summary>
        /// Based on the category fetch the games details list
        /// </summary>
        /// <param name="strGameCategory"></param>
        /// <param name="strManufacturer"></param>
        /// <returns></returns>
        public List<GamesByCategory> GetGamesByCategory(string strGameCategory, string strManufacturer, int GameID)
        {
            List<GamesByCategory> obcoll = null;
            try
            {
                List<rsp_GetGamesByCategoryResult> gameList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    gameList = DataContext.GetGamesByCategory(strGameCategory, strManufacturer, GameID).ToList();
                }

                obcoll = (from obj in gameList
                          select new GamesByCategory
                          {
                              Game_Category_ID = Convert.ToInt32(obj.Game_Category_ID),
                              Game_Category_Name = obj.Game_Category_Name,
                              Game_Title = obj.Game_Title,
                              Game_Title_ID = obj.Game_Title_ID,
                              Manufacturer_Name = obj.Manufacturer_Name
                          }).ToList<GamesByCategory>();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return obcoll;
        }

        /// <summary>
        /// Based on the manufacturer fetch the games details list
        /// </summary>
        /// <param name="strGameCategory"></param>
        /// <param name="strManufacturer"></param>
        /// <returns></returns>
        public List<GamesByManufacturer> GetGamesByManufacturer(string strGameCategory, string strManufacturer, int GameID)
        {
            List<GamesByManufacturer> obcoll = null;
            try
            {
                List<rsp_GetGamesByManufacturerResult> gameList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    gameList = DataContext.GetGamesByManufacturer(strGameCategory, strManufacturer, GameID).ToList();
                }

                obcoll = (from obj in gameList
                          select new GamesByManufacturer
                          {
                              Game_Category_Name = obj.Game_Category_Name,
                              Game_Title = obj.Game_Title,
                              Game_Title_ID = obj.Game_Title_ID,
                              Manufacturer_ID = obj.Manufacturer_ID,
                              Manufacturer_Name = obj.Manufacturer_Name
                          }).ToList<GamesByManufacturer>();
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return obcoll;
        }

        /// <summary>
        /// To get all the manufacturer list and add 'ALL' as default first item
        /// </summary>
        /// <returns></returns>
        public List<Manufacturer> GetManufacturers(bool bAddAll, string sText, string defaultString)
        {
            List<Manufacturer> lstManufacture = null;
            try
            {
                lstManufacture = GetManufacturersByName(bAddAll, sText, defaultString);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstManufacture;
        }

        public List<Manufacturer> GetManufacturers(string sManufacturersName, string defaultString)
        {
            List<Manufacturer> lstManufacture = null;
            try
            {
                lstManufacture = GetManufacturersByName(false, sManufacturersName, defaultString);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstManufacture;
        }

        private List<Manufacturer> GetManufacturersByName(bool bAddAll, string sManufacturersName, string defaultString)
        {
            List<Manufacturer> lstManufacture = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstManufacture = (from rsp_GetManufacturerResult Man in DataContext.GetManufacturer(sManufacturersName.Equals(defaultString) ? "All" : sManufacturersName).ToList()
                                      select new Manufacturer
                                      {
                                          Manufacturer_ID = Man.Manufacturer_ID,
                                          Manufacturer_Name = Man.Manufacturer_Name
                                      }).ToList();
                }
                if (bAddAll)
                    lstManufacture.Insert(0, new Manufacturer() { Manufacturer_ID = 0, Manufacturer_Name = sManufacturersName });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstManufacture;
        }

        /// <summary>
        /// To get all the GameCategory list and add 'ALL' as default first item
        /// </summary>
        /// <returns></returns>
        public List<GameCategory> GetGameCategory(bool bAddAll, string sText, string defaultString)
        {
            List<GameCategory> lstCategory = null;
            try
            {
                lstCategory = GetGameCategoryByCategoryName(bAddAll, sText, defaultString);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstCategory;
        }

        public List<GameCategory> GetGameCategory(string sGameCategoryName, string defaultString)
        {
            List<GameCategory> lstCategory = null;
            try
            {
                lstCategory = GetGameCategoryByCategoryName(false, sGameCategoryName, defaultString);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstCategory;
        }
        //gameName 

        public List<GameCategory> GetGameCategoryGL(string sGameCategoryName, string SGameName, string defaultString)
        {
            List<GameCategory> lstCategoryGL = null;
            try
            {
                lstCategoryGL = GetGameCategoryByCategoryNameGL(false, sGameCategoryName, SGameName, defaultString);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstCategoryGL;
        }

        //Gamename

        //Gamename
        public List<GameCategory> GetGameCategoryByCategoryNameGL(bool bAddAll, string sCategoryName, string SGameName, string defaultString)
        {
            List<GameCategory> lstCategory = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCategory = (from rsp_GetGameCategoryGLResult Man in DataContext.GetGameCategoryGL(sCategoryName.Equals(defaultString) ? "All" : sCategoryName, SGameName).ToList()
                                   select new GameCategory
                                   {
                                       Game_Category_ID = Man.Game_Category_ID,
                                       Game_Category_Name = Man.Game_Category_Name
                                   }).ToList();
                }
                if (bAddAll)
                    lstCategory.Insert(0, new GameCategory() { Game_Category_ID = 0, Game_Category_Name = sCategoryName });
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstCategory;
        }

        //Gamename 
        private List<GameCategory> GetGameCategoryByCategoryName(bool bAddAll, string sCategoryName, string defaultString = "")
        {
            List<GameCategory> lstCategory = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstCategory = (from rsp_GetGameCategoryResult Man in DataContext.GetGameCategory(sCategoryName.Equals(defaultString) ? "All" : sCategoryName).ToList()
                                   select new GameCategory
                                   {
                                       Game_Category_ID = Man.Game_Category_ID,
                                       Game_Category_Name = Man.Game_Category_Name
                                   }).ToList();
                }
                if (bAddAll)
                    lstCategory.Insert(0, new GameCategory() { Game_Category_ID = 0, Game_Category_Name = sCategoryName });
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstCategory;
        }

        public GetGameTitleDetailsByTitleId GetGameTitleDetailsByTitle(int iGameTitleId)
        {
            GetGameTitleDetailsByTitleId objGameTitleByTitle = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (GetGameTitleDetailsByTitleId GameTitleByTitle in (from rsp_GetGameTitleDetailsByTitleIdResult GameTitleDetails in DataContext.GetGameTitleDetailsByTitleId(iGameTitleId).ToList()
                                                                               select new GetGameTitleDetailsByTitleId
                                                                               {
                                                                                   Game_Category_ID = GameTitleDetails.Game_Category_ID,
                                                                                   Game_Title = GameTitleDetails.Game_Title,
                                                                                   Game_Title_ID = GameTitleDetails.Game_Title_ID,
                                                                                   Manufacturer_ID = GameTitleDetails.Manufacturer_ID,
                                                                                   IsMultiGame = GameTitleDetails.IsMultiGame
                                                                               }).ToList())
                    {
                        objGameTitleByTitle = GameTitleByTitle;
                        break;
                    }
                }
                return objGameTitleByTitle;
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return objGameTitleByTitle;
            }
        }

        public int VerifyGameTitleIsExists(string sGameTitle)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.VerifyGameTitleIsExists(sGameTitle).Count();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }


        public int InsertGameTitle(int iGameCategoryId, string sGameTitle, int iManufacturerId, bool isMultiGame)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.InsertGameTitle(iGameCategoryId, sGameTitle, iManufacturerId, isMultiGame);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        public int UpdateGametitle(int iGameCategoryId, int iManufacturerId, string sOldGameTitle, string sNewGameTitle, bool isMultiGame)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.UpdateGameTitle(iGameCategoryId, iManufacturerId, sOldGameTitle, sNewGameTitle, isMultiGame);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        /// <summary>
        /// To get teh manufacturer details based on the Manufacturer Id
        /// </summary>
        /// <param name="iMan_ID"></param>
        /// <returns></returns>
        public List<ManufacturerDetails> GetManufacturerDetails(int iManufacturer_ID)
        {
            List<ManufacturerDetails> lstManufacturerDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstManufacturerDetails = (from rsp_GetManufacturerDetailsResult ManDetails in DataContext.GetManufacturerDetails(iManufacturer_ID).ToList()

                                              select new ManufacturerDetails
                                              {
                                                  Manufacturer_ID = ManDetails.Manufacturer_ID,
                                                  Manufacturer_Name = ManDetails.Manufacturer_Name,
                                                  Manufacturer_Service_Contact = ManDetails.Manufacturer_Service_Contact,
                                                  Manufacturer_Service_EMail = ManDetails.Manufacturer_Service_EMail,
                                                  Manufacturer_Service_Tel = ManDetails.Manufacturer_Service_Tel,
                                                  Manufacturer_Service_Address = ManDetails.Manufacturer_Service_Address,
                                                  Manufacturer_Service_Postcode = ManDetails.Manufacturer_Service_Postcode,
                                                  Manufacturer_Sales_Contact = ManDetails.Manufacturer_Sales_Contact,
                                                  Manufacturer_Sales_EMail = ManDetails.Manufacturer_Sales_EMail,
                                                  Manufacturer_Sales_Tel = ManDetails.Manufacturer_Sales_Tel,
                                                  Manufacturer_Sales_Address = ManDetails.Manufacturer_Sales_Address,
                                                  Manufacturer_Sales_Postcode = ManDetails.Manufacturer_Sales_Postcode,
                                                  Manufacturer_Code = ManDetails.Manufacturer_Code,
                                                  Manufacturer_Coins_In_Meter_Used = ManDetails.Manufacturer_Coins_In_Meter_Used,
                                                  Manufacturer_Coins_Out_Meter_Used = ManDetails.Manufacturer_Coins_Out_Meter_Used,
                                                  Manufacturer_Coin_Drop_Meter_Used = ManDetails.Manufacturer_Coin_Drop_Meter_Used,
                                                  Manufacturer_Handpay_Meter_Used = ManDetails.Manufacturer_Handpay_Meter_Used,
                                                  Manufacturer_External_Credits_Meter_Used = ManDetails.Manufacturer_External_Credits_Meter_Used,
                                                  Manufacturer_Games_Bet_Meter_Used = ManDetails.Manufacturer_Games_Bet_Meter_Used,
                                                  Manufacturer_Games_Won_Meter_Used = ManDetails.Manufacturer_Games_Won_Meter_Used,
                                                  Manufacturer_Notes_Meter_Used = ManDetails.Manufacturer_Notes_Meter_Used,
                                                  Manufacturer_Single_Coin_Build = ManDetails.Manufacturer_Single_Coin_Build,
                                                  Manufacturer_Handpay_Added_To_Coin_Out = ManDetails.Manufacturer_Handpay_Added_To_Coin_Out
                                              }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstManufacturerDetails;
        }

        /// <summary>
        /// To update the modified values of specific manufacturer
        /// </summary>
        /// <param name="objManufacturerDetails"></param>
        /// <returns></returns>
        public int UpdateManufacturerDetails(ManufacturerDetails objManufacturerDetails)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.UpdateManufacturerDetails(objManufacturerDetails.Manufacturer_ID, objManufacturerDetails.Manufacturer_Name, objManufacturerDetails.Manufacturer_Code,
                        objManufacturerDetails.Manufacturer_Sales_Address, objManufacturerDetails.Manufacturer_Sales_Contact, objManufacturerDetails.Manufacturer_Sales_EMail,
                        objManufacturerDetails.Manufacturer_Sales_Postcode, objManufacturerDetails.Manufacturer_Sales_Tel, objManufacturerDetails.Manufacturer_Service_Address,
                        objManufacturerDetails.Manufacturer_Service_Contact, objManufacturerDetails.Manufacturer_Service_EMail, objManufacturerDetails.Manufacturer_Service_Postcode,
                        objManufacturerDetails.Manufacturer_Service_Tel, objManufacturerDetails.Manufacturer_Coins_In_Meter_Used, objManufacturerDetails.Manufacturer_Coins_Out_Meter_Used,
                        objManufacturerDetails.Manufacturer_Coin_Drop_Meter_Used, objManufacturerDetails.Manufacturer_Handpay_Meter_Used, objManufacturerDetails.Manufacturer_External_Credits_Meter_Used,
                        objManufacturerDetails.Manufacturer_Games_Bet_Meter_Used, objManufacturerDetails.Manufacturer_Games_Won_Meter_Used, objManufacturerDetails.Manufacturer_Notes_Meter_Used,
                        objManufacturerDetails.Manufacturer_Single_Coin_Build, objManufacturerDetails.Manufacturer_Handpay_Added_To_Coin_Out);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        /// <summary>
        /// To verify the manufacturer name doesn't match with other manufacturer name
        /// </summary>
        /// <param name="strManufacturerName"></param>
        /// <param name="iManufacturerId"></param>
        /// <returns></returns>
        public int VerifyManufacturerName(string strManufacturerName, int iManufacturerId)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.VerifyManufacturerName(strManufacturerName, iManufacturerId).Count();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        /// <summary>
        /// To insert a new Manufacturer record with Manufacturer name
        /// </summary>
        /// <param name="strManufacturerName"></param>
        /// <returns></returns>
        public int InsertManufacturerName(string strManufacturerName)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    esp_InsertManufacturerNameResult em = Datacontext.InsertManufacturerName(strManufacturerName).SingleOrDefault();
                    return Convert.ToInt32(em.Manufacturer_ID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        /// <summary>
        /// To Game category by game id
        /// </summary>
        /// <param name="iGameCategoryID"></param>
        /// <returns></returns>
        public string GetGameCategoryByGameID(int iGameCategoryID)
        {
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    rsp_GetGameCategoryByGameIDResult CatName = Datacontext.GetGameCategoryByGameID(iGameCategoryID).SingleOrDefault();
                    return CatName.Game_Category_Name;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// To save the game catogory name, if not already there insert new GameCategory
        /// </summary>
        /// <param name="iGameCategoryID"></param>
        /// <param name="strGameCategoryName"></param>
        /// <returns></returns>
        public int SaveGameCategory(int iGameCategoryID, string strGameCategoryName)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.SaveGameCategory(iGameCategoryID, strGameCategoryName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        /// <summary>
        /// Once create/update manufacturer export the details manufacturer details to Exchange
        /// </summary>
        /// <param name="iManufacturer_ID"></param>
        /// <param name="strEH_Type"></param>
        /// <param name="strSite_Code"></param>
        /// <returns></returns>
        public int InsertExportHistory(int iManufacturer_ID, string strEH_Type, string strSite_Code)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.InsertExportHistory(iManufacturer_ID, strEH_Type, strSite_Code);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        public List<GameDetails> GetGameDetails(int manufacturerID, int gameCategoryID)
        {
            List<GameDetails> lstGameDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstGameDetails = (from rsp_GetGameDetailsResult gameDetails in DataContext.GetGameDetails(manufacturerID, gameCategoryID).ToList()

                                      select new GameDetails
                                      {
                                          GameTitleId = gameDetails.GameTitleId,
                                          GameTitle = gameDetails.GameTitle,
                                          CategoryID = gameDetails.CategoryID,
                                          CategoryName = gameDetails.CategoryName,
                                          ManufacturerName = gameDetails.ManufacturerName,
                                      }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstGameDetails;
        }

        public List<GameDetailsFromGameLibrary> GetGameDetailsFromGameLibrary()
        {
            List<GameDetailsFromGameLibrary> lstGameDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstGameDetails = (from rsp_GetGameDetailsFromGameLibraryResult gameDetails in DataContext.GetGameDetailsFromGameLibrary().ToList()

                                      select new GameDetailsFromGameLibrary
                                      {
                                          MG_Game_ID = gameDetails.MG_Game_ID,
                                          MG_Game_Name = gameDetails.MG_Game_Name,
                                      }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstGameDetails;
        }

        public List<GameDetailsByGameGroup> GetGameDetailsByGameGroup(int iGroupId)
        {
            List<GameDetailsByGameGroup> lstGameDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstGameDetails = (from rsp_GetGameDetailsByGameGroupResult gameDetails in DataContext.GetGameDetailsByGameGroup(iGroupId).ToList()

                                      select new GameDetailsByGameGroup
                                      {
                                          MG_Game_ID = gameDetails.MG_Game_ID,
                                          MG_Game_Name = gameDetails.MG_Game_Name,
                                          MG_Group_ID = gameDetails.MG_Group_ID
                                      }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstGameDetails;
        }

        public List<AssetDetailsForGame> GetAssetDetailsForGame(int iGameId)
        {
            List<AssetDetailsForGame> lstGameDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstGameDetails = (from rsp_GetAssetDetailsForGameResult assetDetails in DataContext.GetAssetDetailsForGame(iGameId).ToList()

                                      select new AssetDetailsForGame
                                      {
                                          Site_Name = assetDetails.Site_Name,
                                          Bar_Position_Name = assetDetails.Bar_Position_Name,
                                          Machine_Stock_No = assetDetails.Machine_Stock_No,
                                          Machine_Manufacturers_Serial_No = assetDetails.Machine_Manufacturers_Serial_No,
                                          Game_Part_Number = assetDetails.Game_Part_Number,
                                          IsGameActive = assetDetails.IsGameActive
                                      }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstGameDetails;
        }

        public int UpdateGameIDForSlotGames(string sGroupId, string sID)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext Datacontext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = Datacontext.UpdateGameIDForSlotGames(sGroupId, sID);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        public List<MultiGameLibraryThemesDetails> GetMultiGameLibraryThemesDetails(int iGameTitleId, int iGameCatID, int iManufacturerId, int iGameCategoryID, int iMachineManufacturerId, string GameName)
        {
            List<MultiGameLibraryThemesDetails> lstMultiGameLibraryThemesDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMultiGameLibraryThemesDetails = (from rsp_MultiGameLibraryThemesDetailsResult multiGameLibraryThemesDetails in DataContext.MultiGameLibraryThemesDetails(iGameTitleId, iGameCatID, iManufacturerId, iGameCategoryID, iMachineManufacturerId, GameName).ToList()

                                                        select new MultiGameLibraryThemesDetails
                                                        {
                                                            MG_Game_ID = Convert.ToInt32(multiGameLibraryThemesDetails.MG_Game_ID),
                                                            GameName = multiGameLibraryThemesDetails.GameName,
                                                            //Version = multiGameLibraryThemesDetails.Version,
                                                            SerialNo = multiGameLibraryThemesDetails.SerialNo,
                                                            Manufacturer = multiGameLibraryThemesDetails.Manufacturer,
                                                            CategoryID = multiGameLibraryThemesDetails.CategoryID,
                                                            Category = multiGameLibraryThemesDetails.Category,
                                                            GroupID = Convert.ToInt32(multiGameLibraryThemesDetails.GroupID),
                                                            Alias_Machine_Name = multiGameLibraryThemesDetails.Alias_Machine_Name
                                                        }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstMultiGameLibraryThemesDetails;
        }

        public List<PayTable> GetPayTable(int iGameId, int iManufacturerId, int iGameCategoryID, string StockNo)
        {
            List<PayTable> lstPayTable = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstPayTable = (from rsp_GetPayTableResult payTable in DataContext.GetPayTable(iGameId, iManufacturerId, iGameCategoryID, StockNo).ToList()
                                   orderby payTable.PT_Description
                                   select new PayTable
                                   {
                                       Paytable_ID = payTable.Paytable_ID,
                                       Denom = payTable.Denom,
                                       Payout = payTable.Payout,
                                       PT_Description = payTable.PT_Description,
                                       MaxBet = payTable.MaxBet,
                                       TheoreticalPayout = payTable.TheoreticalPayout
                                   }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstPayTable;
        }

        public List<PayTableForGameTitle> GetPayTableForGameTitle(int iGameId, int iManufacturerId, int iGameCategoryID)
        {
            List<PayTableForGameTitle> lstPayTableForGameTitle = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstPayTableForGameTitle = (from rsp_GetPayTableForGameTitleResult payTableForGameTitle in DataContext.GetPayTableForGameTitle(iGameId, iManufacturerId, iGameCategoryID).ToList()
                                               orderby payTableForGameTitle.PT_Description
                                               select new PayTableForGameTitle
                                               {
                                                   Paytable_ID = payTableForGameTitle.Paytable_ID,
                                                   Denom = payTableForGameTitle.Denom,
                                                   Payout = payTableForGameTitle.Payout,
                                                   PT_Description = payTableForGameTitle.PT_Description,
                                                   MaxBet = payTableForGameTitle.MaxBet,
                                                   TheoreticalPayout = payTableForGameTitle.TheoreticalPayout,
                                                   Game_Title = payTableForGameTitle.Game_Title,
                                                   MG_Game_ID = payTableForGameTitle.MG_Game_ID,
                                                   Machine_Stock_No = payTableForGameTitle.Machine_Stock_No,
                                               }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstPayTableForGameTitle;
        }

        /// <summary>
        /// To insert audit records for newly created manufacturer
        /// </summary>
        /// <param name="iModuleID"></param>
        /// <param name="strModuleName"></param>
        /// <param name="strScreenName"></param>
        /// <param name="strField"></param>
        /// <param name="strNewItem"></param>
        public void InsertNewAuditEntry(ModuleNameEnterprise moduleName, string strScreenName, string strField, string strNewItem)
        {
            try
            {
                //Calling Audit Method
                Audit_History AH = new Audit_History();
                //Populate required Values            
                AH.EnterpriseModuleName = moduleName;
                AH.Audit_Screen_Name = strScreenName;
                AH.Audit_Desc = "Record [" + strNewItem + "] added to " + strScreenName;
                AH.AuditOperationType = OperationType.ADD;
                AH.Audit_Field = strField;
                AH.Audit_New_Vl = strNewItem;
                AH.Audit_Slot = string.Empty;
                AH.Audit_Old_Vl = string.Empty;

                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To audit modified manufacturer details, check what are all the values got modified
        /// </summary>
        /// <param name="objOldData"></param>
        /// <param name="objNewData"></param>
        public void AuditUpdatedManufacturerDetails(ManufacturerDetails objOldData, ManufacturerDetails objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;

                if (objOldData.Manufacturer_Name.NullToString() != objNewData.Manufacturer_Name)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Name", objOldData.Manufacturer_Name, objNewData.Manufacturer_Name);

                if (objOldData.Manufacturer_Code.NullToString() != objNewData.Manufacturer_Code)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Code", objOldData.Manufacturer_Code, objNewData.Manufacturer_Code);

                if (objOldData.Manufacturer_Sales_Address.NullToString() != objNewData.Manufacturer_Sales_Address)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Sales_Address", objOldData.Manufacturer_Sales_Address, objNewData.Manufacturer_Sales_Address);

                if (objOldData.Manufacturer_Sales_Postcode.NullToString() != objNewData.Manufacturer_Sales_Postcode)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Sales_Postcode", objOldData.Manufacturer_Sales_Postcode, objNewData.Manufacturer_Sales_Postcode);

                if (objOldData.Manufacturer_Sales_Contact.NullToString() != objNewData.Manufacturer_Sales_Contact)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Sales_Contact", objOldData.Manufacturer_Sales_Contact, objNewData.Manufacturer_Sales_Contact);

                if (objOldData.Manufacturer_Sales_Tel.NullToString() != objNewData.Manufacturer_Sales_Tel)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Sales_Tel", objOldData.Manufacturer_Sales_Tel, objNewData.Manufacturer_Sales_Tel);

                if (objOldData.Manufacturer_Sales_EMail.NullToString() != objNewData.Manufacturer_Sales_EMail)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Sales_EMail", objOldData.Manufacturer_Sales_EMail, objNewData.Manufacturer_Sales_EMail);

                if (objOldData.Manufacturer_Service_Address.NullToString() != objNewData.Manufacturer_Service_Address)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Service_Address", objOldData.Manufacturer_Service_Address, objNewData.Manufacturer_Service_Address);

                if (objOldData.Manufacturer_Service_Postcode.NullToString() != objNewData.Manufacturer_Service_Postcode)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Service_Postcode", objOldData.Manufacturer_Service_Postcode, objNewData.Manufacturer_Service_Postcode);

                if (objOldData.Manufacturer_Service_Contact.NullToString() != objNewData.Manufacturer_Service_Contact)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Service_Contact", objOldData.Manufacturer_Service_Contact, objNewData.Manufacturer_Service_Contact);

                if (objOldData.Manufacturer_Service_Tel.NullToString() != objNewData.Manufacturer_Service_Tel)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Service_Tel", objOldData.Manufacturer_Service_Tel, objNewData.Manufacturer_Service_Tel);

                if (objOldData.Manufacturer_Service_EMail.NullToString() != objNewData.Manufacturer_Service_EMail)
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Service_EMail", objOldData.Manufacturer_Service_EMail, objNewData.Manufacturer_Service_EMail);

                if (objOldData.Manufacturer_Single_Coin_Build.GetValueOrDefault() != objNewData.Manufacturer_Single_Coin_Build.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Single_Coin_Build", objOldData.Manufacturer_Single_Coin_Build.GetValueOrDefault().ToString(), objNewData.Manufacturer_Single_Coin_Build.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Coins_In_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Coins_In_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Coins_In_Meter_Used", objOldData.Manufacturer_Coins_In_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Coins_In_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Coins_Out_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Coins_Out_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Coins_Out_Meter_Used", objOldData.Manufacturer_Coins_Out_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Coins_Out_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Coin_Drop_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Coin_Drop_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Coin_Drop_Meter_Used", objOldData.Manufacturer_Coin_Drop_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Coin_Drop_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Handpay_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Handpay_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Handpay_Meter_Used", objOldData.Manufacturer_Handpay_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Handpay_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_External_Credits_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_External_Credits_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_External_Credits_Meter_Used", objOldData.Manufacturer_External_Credits_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_External_Credits_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Notes_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Notes_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Notes_Meter_Used", objOldData.Manufacturer_Notes_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Notes_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Games_Bet_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Games_Bet_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Games_Bet_Meter_Used", objOldData.Manufacturer_Games_Bet_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Games_Bet_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Games_Won_Meter_Used.GetValueOrDefault() != objNewData.Manufacturer_Games_Won_Meter_Used.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Games_Won_Meter_Used", objOldData.Manufacturer_Games_Won_Meter_Used.GetValueOrDefault().ToString(), objNewData.Manufacturer_Games_Won_Meter_Used.GetValueOrDefault().ToString());

                if (objOldData.Manufacturer_Handpay_Added_To_Coin_Out.GetValueOrDefault() != objNewData.Manufacturer_Handpay_Added_To_Coin_Out.GetValueOrDefault())
                    AuditModifiedDataForManufacturer(objNewData.Manufacturer_Name, "Manufacturer_Handpay_Added_To_Coin_Out", objOldData.Manufacturer_Handpay_Added_To_Coin_Out.GetValueOrDefault().ToString(), objNewData.Manufacturer_Handpay_Added_To_Coin_Out.GetValueOrDefault().ToString());
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// To Insert the modified manufactuer values with the previous and modified values of the specifis field
        /// </summary>
        /// <param name="sManufacturerName"></param>
        /// <param name="sField"></param>
        /// <param name="sPrevValue"></param>
        /// <param name="sNewValue"></param>
        private void AuditModifiedDataForManufacturer(string sManufacturerName, string sField, string sPrevValue, string sNewValue)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.AUDIT_MANUFACTURER;
                AH.Audit_Screen_Name = "Manufacturer";
                AH.Audit_Desc = "Manufacturer " + sManufacturerName + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue;
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;
                AH.Audit_New_Vl = sNewValue; //current value
                AH.Audit_Old_Vl = sPrevValue;  // previous value
                AH.Audit_Slot = string.Empty;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void AuditUpdatedGameTitleDetails(GetGameTitleDetailsByTitleId objOldData, GetGameTitleDetailsByTitleId objNewData)
        {
            try
            {
                if ((objOldData == null) || (objNewData == null)) return;

                if (objOldData.Game_Category_ID != objNewData.Game_Category_ID)
                    AuditModifiedDataForGameTitle(objNewData.Game_Title, "Game_Category_ID", objOldData.Game_Category_ID.ToString(), objNewData.Game_Category_ID.ToString());

                if (objOldData.Game_Title != objNewData.Game_Title)
                    AuditModifiedDataForGameTitle(objNewData.Game_Title, "Game_Title", objOldData.Game_Title, objNewData.Game_Title);

                if (objOldData.Manufacturer_ID != objNewData.Manufacturer_ID)
                    AuditModifiedDataForGameTitle(objNewData.Game_Title, "Game_Category_ID", objOldData.Manufacturer_ID.ToString(), objNewData.Manufacturer_ID.ToString());
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void AuditModifiedDataForGameTitle(string sGameTitle, string sField, string sPrevValue, string sNewValue)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.AUDIT_MANUFACTURER;
                AH.Audit_Screen_Name = "Game Title Admin";
                AH.Audit_Desc = "Game Title " + sGameTitle + " modified  ..[" + sField + "] : " + sPrevValue + " -> " + sNewValue;
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;
                AH.Audit_New_Vl = sNewValue; //current value
                AH.Audit_Old_Vl = sPrevValue;  // previous value
                AH.Audit_Slot = string.Empty;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public List<PayTableDetails> GetPayTableDetails(int iPayTable_ID)
        {
            List<PayTableDetails> lstPayTableDetails = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstPayTableDetails = (from rsp_GetPayTableDetailsResult objPayTableDetails in DataContext.GetPayTableDetails(iPayTable_ID).ToList()

                                          select new PayTableDetails
                                          {
                                              Paytable_ID = objPayTableDetails.Paytable_ID,
                                              GAMENAME = objPayTableDetails.GAMENAME,
                                              Game_ID = objPayTableDetails.Game_ID,
                                              Payout = objPayTableDetails.Payout,
                                              PT_Description = objPayTableDetails.PT_Description,
                                              MaxBet = objPayTableDetails.MaxBet,
                                              TheoreticalPayout = objPayTableDetails.TheoreticalPayout,
                                              HQ_Paytable_ID = objPayTableDetails.HQ_Paytable_ID,
                                              Site_ID = objPayTableDetails.Site_ID,
                                          }).ToList();
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return lstPayTableDetails;
        }

        public int UpdatePayTableTheoreticalPayout(int iPayTableId, double dTheoPayout)
        {
            int result = -1;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = DataContext.UpdatePayTableTheoreticalPayout(iPayTableId, dTheoPayout);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }

        public void AuditPayTableModification(string sField, string sPrevValue, string sNewValue)
        {
            try
            {
                Audit_History AH = new Audit_History();
                AH.EnterpriseModuleName = ModuleNameEnterprise.AUDIT_GAMELIBRARY;
                AH.Audit_Screen_Name = "Pay Table Administration";
                AH.Audit_Desc = "Paytable Theoritical value modified";
                AH.AuditOperationType = OperationType.MODIFY;
                AH.Audit_Field = sField;
                AH.Audit_User_ID = CommonBiz.iUserId;
                AH.Audit_User_Name = CommonBiz.strUsername;
                AH.Audit_New_Vl = sNewValue; //current value
                AH.Audit_Old_Vl = sPrevValue;  // previous value
                AH.Audit_Slot = string.Empty;

                AuditViewerBusiness AVB = new AuditViewerBusiness(Common.Utilities.DatabaseHelper.GetConnectionString());
                AVB.InsertAuditData(AH, true);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public FormedGame IsFormedGame(string sGameIDs)
        {
            FormedGame objFormedGame = null;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var item in DataContext.IsFormedGame(sGameIDs))
                    {
                        objFormedGame = new FormedGame()
                        {
                            IsMultiGame = item.IsMultiGame,
                            Game_IDs = item.Game_ID,
                            Machine_Class_IDs = item.Machine_Class_ID
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return objFormedGame;
        }

        public int MapGames(string sGroupID, string sGameIDs)
        {
            int result = -1;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = DataContext.MapGames(sGroupID, sGameIDs);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }

        public int UpdateMachineClass(string sMac_Class_Ids, int iGame_Title_ID, int iRemove)
        {
            int result = -1;

            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    result = DataContext.UpdateMachineClass(sMac_Class_Ids, iGame_Title_ID, iRemove);
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return result;
        }
    }
}
