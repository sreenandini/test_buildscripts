using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;
using BMC.Common;

namespace BMC.EnterpriseBusiness.Business
{
    public class AssetManagementBiz
    {
        private static AssetManagementBiz _asset;
        public AssetManagementBiz()
        {

        }
        public static AssetManagementBiz CreateInstance()
        {
            if (_asset == null)
            {
                _asset = new AssetManagementBiz();
            }
            return _asset;
        }


        public List<GetMachineDetailsResult> GetMachineDetails(int Machine_Type_ID, int Operator_ID, int Depot_ID, int Staff_ID, int Manufacturer_ID, int Game_Category_ID, int ModelTypeID, string Machine_Status, string orderBy, string searchCriteria, int MG_Game_ID, string _GameName)
        {

            List<GetMachineDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetMachineDetailsResult> lstMachineDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMachineDetails = DataContext.GetMachineDetails(Machine_Type_ID, Operator_ID, Depot_ID, Staff_ID, Manufacturer_ID, Game_Category_ID, ModelTypeID, Machine_Status, orderBy, searchCriteria, MG_Game_ID, _GameName).ToList<rsp_GetMachineDetailsResult>();
                }

                obcoll = (from obj in lstMachineDetails
                          select new GetMachineDetailsResult
                          {

                              Depot_ID = obj.Depot_ID,
                              Depot_Name = obj.Depot_Name,
                              Game_Category_ID = obj.Game_Category_ID,
                              Game_Category_Name = obj.Game_Category_Name,
                              IsNonGamingAssetType = obj.IsNonGamingAssetType,
                              Machine_Alternative_Serial_Numbers = obj.Machine_Alternative_Serial_Numbers,
                              Machine_BACTA_Code = obj.Machine_BACTA_Code,
                              Machine_Category_Code = obj.Machine_Category_Code,
                              Machine_Class_ID = obj.Machine_Class_ID,
                              Machine_Class_Model_Code = obj.Machine_Class_Model_Code,
                              Machine_End_Date = obj.Machine_End_Date,
                              Machine_ID = obj.Machine_ID,
                              Machine_Manufacturers_Serial_No = obj.Machine_Manufacturers_Serial_No,
                              Machine_Name = obj.Machine_Name,
                              Machine_Status_Flag = obj.Machine_Status_Flag,
                              Machine_Stock_No = obj.Machine_Stock_No,
                              Machine_Transit_Site_Code = obj.Machine_Transit_Site_Code,
                              Machine_Type_Code = obj.Machine_Type_Code,
                              Machine_Type_ID = obj.Machine_Type_ID,
                              Manufacturer_Name = obj.Manufacturer_Name,
                              Operator_ID = obj.Operator_ID,
                              Operator_Name = obj.Operator_Name,
                              Site_Code = obj.Site_Code,
                              Site_Name = obj.Site_Name,
                              Staff_First_Name = obj.Staff_First_Name,
                              Staff_ID = obj.Staff_ID,
                              Staff_Last_Name = obj.Staff_Last_Name,
                              PaytableFlag=obj.PaytableFlag,
                              Bar_Position_Name=obj.Bar_Position_Name,
                              Site_ZonaRice=obj.Site_ZonaRice
                              
                          }).ToList<GetMachineDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        //public List<GetSiteDetailsByGAResult> GetSiteDetailsGA(int Machine_ID)
        //{
        //    List<GetSiteDetailsByGAResult> obcoll = null;
        //    try
        //    {
        //        List<rsp_GetSiteDetailsByGAResult> lstSiteDetails;
        //        using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //        {
        //            lstSiteDetails = DataContext.GetSiteDetailsByGA(Machine_ID).ToList();
        //        }

        //        obcoll = (from obj in lstSiteDetails
        //                  select new GetSiteDetailsByGAResult
        //                  {
        //                  }).ToList<GetSiteDetailsByGAResult>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obcoll;

        //}
        //public List<GetSiteDetailsByNGAResult> GetSiteDetailsNGA(int Machine_ID)
        //{
        //    List<GetSiteDetailsByNGAResult> obcoll = null;
        //    try
        //    {
        //        List<rsp_GetSiteDetailsByNGAResult> lstSiteDetails;
        //        using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //        {
        //            lstSiteDetails = DataContext.GetSiteDetailsByNGA(Machine_ID).ToList();
        //        }

        //        obcoll = (from obj in lstSiteDetails
        //                  select new GetSiteDetailsByNGAResult
        //                  {
        //                  }).ToList<GetSiteDetailsByNGAResult>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obcoll;
        //}GetPaytableDetailsForGameResult

        public List<GetSiteDetailsByStockResult> GetSiteDetailsByStock(int Machine_ID, bool IsNonGaming)
        {
            List<GetSiteDetailsByStockResult> obcoll = null;
            try
            {
                List<rsp_GetSiteDetailsByStockResult> lstSiteDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstSiteDetails = DataContext.GetSiteDetailsByStock(Machine_ID, IsNonGaming).ToList();
                }

                obcoll = (from obj in lstSiteDetails
                          select new GetSiteDetailsByStockResult
                          {
                              Site_Name = obj.Site_Name,
                              Site_Code = obj.Site_Code,
                              Bar_Position_Name = obj.Bar_Position_Name,
                              Site_ZonaRice = obj.Site_ZonaRice

                          }).ToList<GetSiteDetailsByStockResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }
        public List<GetPaytableDetailsForGameResult> GetPaytableDetailsForGameResult(int Machine_ID)
        {
            List<GetPaytableDetailsForGameResult> obcoll = null;
            try
            {
                List<rsp_GetPaytableDetailsForGameResult> lstPaytableDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstPaytableDetails = DataContext.GetPaytableDetailsForGame(Machine_ID).ToList();
                }

                obcoll = (from obj in lstPaytableDetails
                          select new GetPaytableDetailsForGameResult
                          {
                              AliasGameName = obj.AliasGameName,
                              Manufacturer_Name = obj.Manufacturer_Name,
                              Installation_No = obj.Installation_No,
                              Machine_ID = obj.Machine_ID,
                              Game_Name = obj.Game_Name,
                              PaytableID = obj.PaytableID,
                              PaytableDescription = obj.PaytableDescription,
                              Payout = obj.Payout,
                              MaxBet = obj.MaxBet,
                              TheoreticalPayout = obj.TheoreticalPayout
                          }).ToList<GetPaytableDetailsForGameResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<FetchGameCategoryResult> FetchGameCategoryResult(int Category_ID)
        {
            List<FetchGameCategoryResult> obcoll = null;
            try
            {
                List<rsp_FetchGameCategoryResult> lstGameCategory;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstGameCategory = DataContext.FetchGameCategory(Category_ID).ToList();
                }

                obcoll = (from obj in lstGameCategory
                          select new FetchGameCategoryResult
                          {
                              Game_Category_ID = obj.Game_Category_ID,
                              Game_Category_Name = obj.Game_Category_Name
                          }).ToList<FetchGameCategoryResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }
        public int IsTemplateNameExists(string TemplateName)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.rsp_CheckTemplateNameExists(TemplateName);
            }
        }
        public int InsertAssetTemplate(string AssetNumber, string TemplateName)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.usp_CreateAssetTemplate(AssetNumber, TemplateName);
            }
        }
        public int UpdateAssetTemplate(bool IsEdit, string AssetNumber, string TemplateName)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.usp_UpdateTemplate(IsEdit, AssetNumber, TemplateName);
            }
        }
        public List<GetAssetNumberForTemplateResult> GetAssetNumber(string TemplateName)
        {
            List<GetAssetNumberForTemplateResult> objAssetNumber = null;
            try
            {
                List<rsp_GetAssetNumberForTemplateResult> AssetNumber;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    AssetNumber = DataContext.rsp_GetAssetNumberForTemplate(TemplateName).ToList();
                }
                objAssetNumber = (from obj in AssetNumber
                                  select new GetAssetNumberForTemplateResult
                                  {
                                      AssetNumber = obj.AssetNumber
                                  }).ToList<GetAssetNumberForTemplateResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAssetNumber;
        }

        public List<GetAssetTemplateDetailsResult> DisplayTemplate()
        {
            List<GetAssetTemplateDetailsResult> objTemplate=null;
            try
            {
                List<rsp_GetAssetTemplateDetailsResult> TemplateDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    TemplateDetails=DataContext.rsp_GetAssetTemplateDetails().ToList();
                }
                objTemplate = (from obj in TemplateDetails
                                 select new GetAssetTemplateDetailsResult
                                 {
                                     AssetCrTempNumber=obj.AssetCrTempNumber,
                                     TemplateName=obj.TemplateName
                                 }).ToList<GetAssetTemplateDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objTemplate;
        }



        //load gamenames
        public List<rsp_GetGameNames> LoadGameNames()
        {
            List<rsp_GetGameNames> GetGameNamesDetails=new List<rsp_GetGameNames>();
            try
            {
               
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (rsp_GetGameNamesResult item in DataContext.GetGameNameForAsset())
	                {
                        GetGameNamesDetails.Add(new rsp_GetGameNames() { MG_Game_Name = item.MG_Game_Name,MG_Game_ID = item.MG_Game_ID });
	                }
                    
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetGameNamesDetails;
        }

        public List<rsp_GetGameNames> Load_GameLibraryGameNames()
        {
            List<rsp_GetGameNames> GetGameNamesDetails = new List<rsp_GetGameNames>();
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (rsp_GetGameNamesResult item in DataContext.rsp_GetGameNames())
                    {
                        GetGameNamesDetails.Add(new rsp_GetGameNames() { MG_Game_Name = item.MG_Game_Name, MG_Game_ID = item.MG_Game_ID });
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return GetGameNamesDetails;
        }

        //load gamenames


        public bool CheckMachineInUse(int Machine_id)
        {
            bool retVal = false;
            try
            {
                List<rsp_CheckMachineInUseResult> lst_chkMC;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lst_chkMC = DataContext.CheckMachineInUse(Machine_id).ToList();
                }

                if (lst_chkMC != null && lst_chkMC.Count > 0)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        #region TerminationBiz
        public List<GetMachineTerminationReasonResult> GetMachineTerminationReason()
        {
            List<GetMachineTerminationReasonResult> obcoll = null;
            try
            {
                List<rsp_GetMachineTerminationReasonResult> lstMCTermination;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMCTermination = DataContext.GetMachineTerminationReason().ToList();
                }

                obcoll = (from obj in lstMCTermination
                          select new GetMachineTerminationReasonResult
                          {
                              MTRT_Description = ResourceExtensions.GetResourceTextByKey(null,"Key_DBV_"+obj.MTRT_Description),
                              MTRT_Display_Order = obj.MTRT_Display_Order,
                              MTRT_ID = obj.MTRT_ID
                          }).ToList<GetMachineTerminationReasonResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetTerminationMCDetailsResult> GetTerminationMachineDetails(string StockNo)
        {
            List<GetTerminationMCDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetTerminationMCDetailsResult> lstMCTermination;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMCTermination = DataContext.GetTerminationMCDetails(StockNo).ToList();
                }

                obcoll = (from obj in lstMCTermination
                          select new GetTerminationMCDetailsResult
                          {
                              Machine_Termination_Comments = obj.Machine_Termination_Comments,
                              Machine_Termination_Username = obj.Machine_Termination_Username,
                              Machine_Termination_Reason = obj.Machine_Termination_Reason,
                              Machine_End_Date = obj.Machine_End_Date
                          }).ToList<GetTerminationMCDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }


        public bool UpdateTerminationMCDetails(string Stock_No, string MCTermination_Comments, string MCTermination_Username, int MCTermination_Reason, int MCStatus_Flag, string MCEnd_Date,bool isNGA)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateTerminationMCDetails(Stock_No, MCTermination_Comments, MCTermination_Username, MCTermination_Reason, MCStatus_Flag, MCEnd_Date,isNGA) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        #endregion

        #region AddMachineType


        public List<GetSiteIconDetailsResult> GetSiteIconDetails()
        {
            List<GetSiteIconDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetSiteIconDetailsResult> lstSiteIcon;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstSiteIcon = DataContext.GetSiteIconDetails().ToList();
                }

                obcoll = (from obj in lstSiteIcon
                          select new GetSiteIconDetailsResult
                          {
                              SiteIconID = obj.SiteIconID,
                              Machine_Type_Site_Icon = obj.Machine_Type_Site_Icon,
                              SiteIconPath = obj.SiteIconPath,
                              Machine_Site_Icon_Display = ResourceExtensions.GetResourceTextByKey(null, "Key_DBV_" + obj.Machine_Type_Site_Icon)
                          }).ToList<GetSiteIconDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool UpdateMachineType(ref int? MachineType_ID, int? Depreciation_Policy_ID, string MCType_Code, string MCType_Description, int? MCType_Icon_ref, string MCType_Site_Icon, string MCType_Income_Ledger_Code, string MCType_AMEDIS_ID, int? isNonGamingAssetType)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateMachineType(ref MachineType_ID, Depreciation_Policy_ID, MCType_Code, MCType_Description,
                        MCType_Icon_ref, MCType_Site_Icon, MCType_Income_Ledger_Code
                        , MCType_AMEDIS_ID, isNonGamingAssetType) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        #endregion

        #region MachineModelAdminGamingBiz

        public List<GetManufacturerbyMCTypeResult> GetManufacturerbyMCType(int MachineType_ID)
        {
            List<GetManufacturerbyMCTypeResult> obcoll = null;
            try
            {
                List<rsp_GetManufacturerbyMCTypeResult> lstManfact;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstManfact = DataContext.GetManufacturerbyMCType(MachineType_ID).ToList();
                }

                obcoll = (from obj in lstManfact
                          select new GetManufacturerbyMCTypeResult
                          {
                              Manufacturer_ID = obj.Manufacturer_ID,
                              Manufacturer_Name = obj.Manufacturer_Name
                          }).ToList<GetManufacturerbyMCTypeResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }


        public List<BMC.EnterpriseBusiness.Entities.GetMachineNamesOnMachineTypeResult> GetMachineNamesOnMachineType(int MachineType_ID)
        {
            List<BMC.EnterpriseBusiness.Entities.GetMachineNamesOnMachineTypeResult> obcoll = null;
            try
            {
                List<BMC.EnterpriseDataAccess.GetMachineNamesOnMachineTypeResult> lstMCName;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMCName = DataContext.GetMachineNamesOnMachineType(MachineType_ID).ToList();
                }

                obcoll = (from obj in lstMCName
                          select new BMC.EnterpriseBusiness.Entities.GetMachineNamesOnMachineTypeResult
                          {

                              Machine_Name = obj.Machine_Name
                          }).ToList<BMC.EnterpriseBusiness.Entities.GetMachineNamesOnMachineTypeResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetModelConfigurationsResult> GetModelConfigurations(int MachineType_ID, string MachineName, int Manfacture_ID)
        {
            List<GetModelConfigurationsResult> obcoll = null;
            try
            {
                List<rsp_GetModelConfigurationsResult> lstModel;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstModel = DataContext.GetModelConfigurations(MachineType_ID, MachineName, Manfacture_ID).ToList();
                }

                obcoll = (from obj in lstModel
                          select new GetModelConfigurationsResult
                          {
                              Machine_Class_AddTrueCoinInToDrop = obj.Machine_Class_AddTrueCoinInToDrop,
                              Machine_Class_JackpotAddedToCancelledCredits = obj.Machine_Class_JackpotAddedToCancelledCredits,
                              Machine_Class_RecreateCancelledCredits = obj.Machine_Class_RecreateCancelledCredits,
                              Machine_Class_RecreateTicketsInsertedfromDrop = obj.Machine_Class_RecreateTicketsInsertedfromDrop,
                              Machine_Class_UseCancelledCreditsAsTicketsPrinted = obj.Machine_Class_UseCancelledCreditsAsTicketsPrinted,

                          }).ToList<GetModelConfigurationsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool UpdateModelConfigurations(int MachineTypeID, string MachineName, int ManufacturerID, bool RecreateCancelledCredits, bool JackpotAddedToCancelledCredits, bool AddTrueCoinInToDrop, bool UseCancelledCreditsAsTicketsPrinted, bool RecreateTicketsInsertedfromDrop)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateModelConfigurations(MachineTypeID, MachineName, ManufacturerID, RecreateCancelledCredits, JackpotAddedToCancelledCredits, AddTrueCoinInToDrop, UseCancelledCreditsAsTicketsPrinted, RecreateTicketsInsertedfromDrop) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }
        #endregion

        #region MachineModelAdminNonGamingBiz
        public List<GetMachineClassListResult> GetMachineClassDetails(int MachineClass_ID)
        {
            List<GetMachineClassListResult> obcoll = null;
            try
            {
                List<rsp_GetMachineClassListResult> lstMC_Class;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMC_Class = DataContext.GetMachineClassList(MachineClass_ID).ToList();
                }

                obcoll = (from obj in lstMC_Class
                          select new GetMachineClassListResult
                          {
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              Depreciation_Policy_Use_Default = obj.Depreciation_Policy_Use_Default,
                              Machine_Class_Category = obj.Machine_Class_Category,
                              Machine_Class_DeListed = obj.Machine_Class_DeListed,
                              Machine_Class_Model_Code = obj.Machine_Class_Model_Code,
                              Machine_Class_Release_Date = obj.Machine_Class_Release_Date,
                              Machine_Class_Test_Machine = obj.Machine_Class_Test_Machine,
                              Machine_Name = obj.Machine_Name,
                              Machine_Type_Code = obj.Machine_Type_Code,
                              Manufacturer_ID = obj.Manufacturer_ID,
                              Manufacturer_Name = obj.Manufacturer_Name,
                              Machine_Type_ID = obj.Machine_Type_ID

                          }).ToList<GetMachineClassListResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool GetMachineClassID(ref int? MachineClassID, bool IsDelete)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.GetMachineClassID(ref MachineClassID, IsDelete) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool CheckAutoModelCodeExists()
        {
            bool retVal = false;
            try
            {
                List<rsp_CheckAutoModelCodeExistsResult> lst_chkMC;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lst_chkMC = DataContext.CheckAutoModelCodeExists().ToList();
                }
                if (lst_chkMC != null && lst_chkMC.Count > 0)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public void CheckModelCodeAndMachineExists(int? Machine_ClassID, string MC_Model_Code, string Machine_Name, ref bool? ModelCodeExist, ref bool? MachineNameExist)
        {
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.CheckModelCodeExist(Machine_ClassID, MC_Model_Code, Machine_Name, ref ModelCodeExist, ref MachineNameExist);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateMachineClassDetails(string Machine_Name, int? Manufacturer_ID, string MCModel_Code, bool? MC_DeListed, bool? MCTest_Machine, string MC_Category, int? DepreciationPolicy_ID, bool? DepPolicy_UseDefault, string MC_Release_Date, int? Machine_Type_ID, int UserID, int AuditModuleID, string AuditScreenName, ref int? Machine_Class_ID)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateMachineClassDetails(Machine_Name, Manufacturer_ID, MCModel_Code, MC_DeListed, MCTest_Machine, MC_Category, DepreciationPolicy_ID, DepPolicy_UseDefault, MC_Release_Date, Machine_Type_ID, UserID, AuditModuleID, AuditScreenName, ref Machine_Class_ID) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }
        #endregion


        #region SellMachineBiz

        public List<GetMachineAssetDetailsResult> GetMachineAssetDetails(int? Machine_ID)
        {
            List<GetMachineAssetDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetMachineAssetDetailsResult> lstMCAsset;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMCAsset = DataContext.GetMachineAssetDetails(Machine_ID).ToList();
                }

                obcoll = (from obj in lstMCAsset
                          select new GetMachineAssetDetailsResult
                          {
                              ModelName = obj.Machine_Name + "[" + obj.Machine_Class_Model_Code + "]",
                              Machine_Stock_No = obj.Machine_Stock_No,
                              Machine_Manufacturers_Serial_No = obj.Machine_Manufacturers_Serial_No,
                              Machine_Alternative_Serial_Numbers = obj.Machine_Alternative_Serial_Numbers

                          }).ToList<GetMachineAssetDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool UpdateMachineAssetDetails(int Machine_ID, DateTime Machine_End_Date, int Machine_Status_Flag, string Machine_Sold_To, string MType_Of_Sale, decimal? Machine_Sale_Price, int Staff_ID_Deleted, DateTime Machine_Date_Deleted, string MC_Sales_Invoice_Number)
        {

            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateMachineAssetDetails(Machine_ID, Machine_End_Date, Machine_Status_Flag, Machine_Sold_To, MType_Of_Sale, Machine_Sale_Price, Staff_ID_Deleted, Machine_Date_Deleted, MC_Sales_Invoice_Number) >= 0)
                    {
                        retVal = true;
                    }
                }

            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;

        }

        #endregion


        #region ModelTypeBiz

        public List<GetModelTypeResult> GetModelTypeDetails(bool? IsNGA, int? MT_ID)
        {
            List<GetModelTypeResult> obcoll = null;
            try
            {
                List<rsp_GetModelTypeResult> lstModelType;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstModelType = DataContext.GetModelType(IsNGA, MT_ID).ToList();
                }

                obcoll = (from obj in lstModelType
                          select new GetModelTypeResult
                          {
                              MT_ID = obj.MT_ID,
                              MT_Model_Name = obj.MT_Model_Name,
                              MT_Model_Desc = obj.MT_Model_Desc,
                              MT_IsNGA = obj.MT_IsNGA

                          }).ToList<GetModelTypeResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public bool UpdateModelType(string Model_Name, string Model_Desc, bool? isNGA, ref int? MT_ID)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateModelType(Model_Name, Model_Desc, isNGA, ref MT_ID) >= 0)
                    {
                        retVal = true;
                    }
                }

            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        #endregion
    }

}
