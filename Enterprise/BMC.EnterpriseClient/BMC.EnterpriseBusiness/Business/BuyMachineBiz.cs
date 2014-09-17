using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Collections.ObjectModel;

namespace BMC.EnterpriseBusiness.Business
{
    public class BuyMachineBiz
    {
        private static BuyMachineBiz _buy;
        public static BuyMachineBiz CreateInstance()
        {
            if (_buy == null)
            {
                _buy = new BuyMachineBiz();
            }
            return _buy;
        }


        public int? AddOrDeleteMachineDetails(int? Machine_ID, int? Machine_Class_ID, int? Machine_New_Install, ref string Machine_Stock_No)
        {
            try
            {
                usp_GetMachineIDResult lq_Machine = null;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lq_Machine = DataContext.GetMachineID(Machine_ID, Machine_Class_ID, Machine_New_Install, ref Machine_Stock_No).SingleOrDefault<usp_GetMachineIDResult>();
                }
                if (lq_Machine != null)
                {
                    Machine_ID = lq_Machine.Machine_ID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Machine_ID;
        }

        public List<GetMachineClassDetailsResult> GetMachineClassDetails(int? MachineClassID, string MachineName, int? Manufacture_ID,int? Machine_Type_Id)
        {
            List<GetMachineClassDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetMachineClassDetailsResult> lstMachineClass;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMachineClass = DataContext.GetMachineClassDetails(MachineClassID, MachineName, Manufacture_ID, Machine_Type_Id).ToList();
                }

                obcoll = (from obj in lstMachineClass
                          select new GetMachineClassDetailsResult
                          {
                              Machine_Class_ID = obj.Machine_Class_ID,
                              Machine_Name = obj.Machine_Name,
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              Machine_Class_Category_ID = obj.Machine_Class_Category_ID,
                              Manufacturer_ID = obj.Manufacturer_ID,
                              Manufacturer_Name = obj.Manufacturer_Name,
                              Validation_Length = obj.Validation_Length
                          }).ToList<GetMachineClassDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetDepreciationPolicyDetailsResult> GetDepreciationPolicyDetails()
        {
            List<GetDepreciationPolicyDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetDepreciationPolicyDetailsResult> lstDepreciation;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstDepreciation = DataContext.GetDepreciationPolicyDetails(null).ToList();
                }

                obcoll = (from obj in lstDepreciation
                          select new GetDepreciationPolicyDetailsResult
                          {
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              Depreciation_Policy_Description = obj.Depreciation_Policy_Description

                          }).ToList<GetDepreciationPolicyDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<Manufacturer> GetManufacturers()
        {
            List<Manufacturer> lstManufacture = null;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstManufacture = (from rsp_GetManufacturerDetailsResult Man in DataContext.GetManufacturerDetails(null).ToList() orderby Man.Manufacturer_Name ascending
                                      select new Manufacturer 
                                      {
                                          Manufacturer_ID = Man.Manufacturer_ID,
                                          Manufacturer_Name = Man.Manufacturer_Name
                                      }).ToList();
                }
                lstManufacture.Insert(0, new Manufacturer() { Manufacturer_ID = -1, Manufacturer_Name = "All" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstManufacture;
        }

        public List<GetMachineTypeDetailsResult> GetMachineTypeDetails(int? MachineTypeID)
        {
            List<GetMachineTypeDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetMachineTypeDetailsResult> lstMachineType;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMachineType = DataContext.GetMachineTypeDetails(MachineTypeID).ToList();
                }

                obcoll = (from obj in lstMachineType
                          select new GetMachineTypeDetailsResult
                          {
                              Machine_Type_ID = obj.Machine_Type_ID,
                              Machine_Type_Code = obj.Machine_Type_Code,
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              IsNonGamingAssetType = obj.IsNonGamingAssetType,
                              Machine_Type_AMEDIS_ID = obj.Machine_Type_AMEDIS_ID,
                              Machine_Type_Category = obj.Machine_Type_Category,
                              Machine_Type_Description = obj.Machine_Type_Description,
                              Machine_Type_Icon_ref = obj.Machine_Type_Icon_ref,
                              Machine_Type_Income_Ledger_Code = obj.Machine_Type_Income_Ledger_Code,
                              Machine_Type_Site_Icon = obj.Machine_Type_Site_Icon,
                              MCTypeDescription_NGA = obj.Machine_Type_Code + (obj.Machine_Type_Description != null && obj.Machine_Type_Description!="" ? ("     (" + obj.Machine_Type_Description + ")") : ""),
                              SiteIconPath = obj.SiteIconPath
                          }).ToList<GetMachineTypeDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetGameTitleResult> GetGameTitleDetails(bool isMultiGame)
        {
            List<GetGameTitleResult> obcoll = null;
            try
            {
                List<rsp_GetGameTitleResult> lstGameTitle;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstGameTitle = DataContext.GetGameTitle(isMultiGame).ToList();
                }

                obcoll = (from obj in lstGameTitle
                          select new GetGameTitleResult
                          {
                              Game_Title = obj.Game_Title,
                              Game_Title_ID = obj.Game_Title_ID

                          }).ToList<GetGameTitleResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }
		
       public void GetSetting(int setting_ID, string setting_Name, string setting_Default, ref string setting_Value)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.GetSetting(setting_ID, setting_Name, setting_Default, ref setting_Value);
            }
        }
		
        public List<GetActiveStackerDetailsResult> GetActiveStakersDetails()
        {
            List<GetActiveStackerDetailsResult> lstActiveStackers = new List<GetActiveStackerDetailsResult>();
            lstActiveStackers = null;
            try
            {
                List<rsp_GetStackerDetailsResult> lstStackerDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstStackerDetails = DataContext.GetActiveStackerDetails().ToList();// to get active stacker details
                }
                lstActiveStackers = (from obj in lstStackerDetails
                                     select new GetActiveStackerDetailsResult
                                     {
                                         ActiveStacker_Id = obj.Stacker_Id,
                                         ActiveStackerName = obj.StackerName
                                     }).ToList<GetActiveStackerDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            
            }
            return lstActiveStackers;
        }

        public List<GetStackerDetailsResult> GetStackerDetails()
        {
            List<GetStackerDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetStackerDetailsResult> lstStacker;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstStacker = DataContext.GetStackerDetails(true).ToList();// to get active stacker details
                }

                obcoll = (from obj in lstStacker
                          select new GetStackerDetailsResult
                          {
                              Stacker_Id = obj.Stacker_Id,
                              StackerName = obj.StackerName

                          }).ToList<GetStackerDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetStaffByDepotResult> GetStaffByDepot(int? DepotID)
        {
            List<GetStaffByDepotResult> obcoll = null;
            try
            {
                List<rsp_GetStaffByDepotResult> lstStaff;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstStaff = DataContext.GetStaffByDepot(DepotID).ToList();
                }

                obcoll = (from obj in lstStaff
                          select new GetStaffByDepotResult
                          {
                              Staff_ID = obj.Staff_ID,
                              Staff_First_Name = obj.Staff_First_Name,
                              Staff_Last_Name = obj.Staff_Last_Name,
                              FullName = obj.Staff_First_Name + " " + obj.Staff_Last_Name,

                          }).ToList<GetStaffByDepotResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetBuyMachineDetailsResult> GetBuyMachineDetails(int? Machine_ID, string templateName)
        {
            List<GetBuyMachineDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetBuyMachineDetailsResult> lstBuyMachine;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstBuyMachine = DataContext.GetBuyMachineDetails(Machine_ID, templateName).ToList();
                }

                obcoll = (from obj in lstBuyMachine
                          select new GetBuyMachineDetailsResult
                          {
                              GameTypeCode = obj.GameTypeCode,
                              IsAFTEnabled = obj.IsAFTEnabled,
                              Depot_ID = obj.Depot_ID,
                              Operator_ID = obj.Operator_ID,
                              CMPGameType = obj.CMPGameType,
                              Staff_ID = obj.Staff_ID,
                              Machine_Connection_Type = obj.Machine_Connection_Type,
                              Machine_CIV_Change_Reason = obj.Machine_CIV_Change_Reason,
                              Machine_ModelTypeID = obj.Machine_ModelTypeID,
                              Stacker_Id = obj.Stacker_Id,
                              IsTITOEnabled = obj.IsTITOEnabled,
                              IsNonCashVoucherEnabled = obj.IsNonCashVoucherEnabled,
                              IsMultiGame = obj.IsMultiGame,
                              Machine_Status_Flag = obj.Machine_Status_Flag,
                              Machine_Stock_No = obj.Machine_Stock_No,
                              Machine_End_Date = obj.Machine_End_Date,
                              Machine_Sales_Invoice_Number = obj.Machine_Sales_Invoice_Number,
                              Machine_Sale_Price = obj.Machine_Sale_Price,
                              Machine_Sold_To = obj.Machine_Sold_To,
                              Machine_Type_Of_Sale = obj.Machine_Type_Of_Sale,
                              ActAssetNo = obj.ActAssetNo,
                              GMUNo = obj.GMUNo,
                              ActSerialNo = obj.ActSerialNo,
                              Installation_ID = obj.Installation_ID,
                              Machine_Purchased_From = obj.Machine_Purchased_From,
                              Machine_Alternative_Serial_Numbers = obj.Machine_Alternative_Serial_Numbers,
                              Machine_MAC_Address = obj.Machine_MAC_Address,
                              Machine_Depreciation_Start_Date = obj.Machine_Depreciation_Start_Date,
                              Machine_Start_Date = obj.Machine_Start_Date,
                              Machine_Original_Purchase_Price = obj.Machine_Original_Purchase_Price,
                              Machine_Purchase_Invoice_Number = obj.Machine_Purchase_Invoice_Number,
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              Depreciation_Policy_Use_Default = obj.Depreciation_Policy_Use_Default,
                              Machine_Class_Occupancy_Games_Per_Hour = obj.Machine_Class_Occupancy_Games_Per_Hour,
                              Class_Depreciation = obj.Class_Depreciation,
                              Machine_Class_Category_ID = obj.Machine_Class_Category_ID,
                              Type_Depreciation = obj.Type_Depreciation,
                              Machine_Name = obj.Machine_Name,
                              Machine_BACTA_Code = obj.Machine_BACTA_Code,
                              Manufacturer_ID = obj.Manufacturer_ID,
                              Validation_Length = obj.Validation_Length,
                              Staff_Sold_Staff_Last_Name = obj.Staff_Sold_Staff_Last_Name,
                              Staff_Sold_Staff_First_Name = obj.Staff_Sold_Staff_First_Name,
                              Old_Machine_ID = obj.Old_Machine_ID,
                              Old_Machine_Start_Date = obj.Old_Machine_Start_Date,
                              Old_Machine_Name = obj.Old_Machine_Name,
                              MG_Game_ID = obj.MG_Game_ID,
                              Machine_Memo = obj.Machine_Memo,
                              IsDefaultAssetDetail = obj.IsDefaultAssetDetail,
                              GetGameDetails=obj.GetGameDetails,
                              Base_Denom = obj.Base_Denom,
                              Percentage_Payout = obj.Percentage_Payout ,
                              IsGameCappingEnabled=obj.IsGameCappingEnabled,
                              AssetDisplayName = obj.AssetDisplayName,
                              MultiGameName = obj.MultiGameName,
                          }).ToList<GetBuyMachineDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public List<GetDepreciationDetailsResult> GetDepreciationDetails(int? Machine_ID)
        {
            List<GetDepreciationDetailsResult> obcoll = null;
            try
            {
                List<rsp_GetDepreciationDetailsResult> lstDepDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstDepDetails = DataContext.GetDepreciationDetails(Machine_ID).ToList();
                }

                obcoll = (from obj in lstDepDetails
                          select new GetDepreciationDetailsResult
                          {
                              Depreciation_Policy_Details_Duration = obj.Depreciation_Policy_Details_Duration,
                              Depreciation_Policy_Details_ID = obj.Depreciation_Policy_Details_ID,
                              Depreciation_Policy_Details_Percentage = obj.Depreciation_Policy_Details_Percentage,
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              Depreciation_Policy_Residual_Value = obj.Depreciation_Policy_Residual_Value,
                              Machine_Class_ID = obj.Machine_Class_ID,
                              Machine_Depreciation_Start_Date = obj.Machine_Depreciation_Start_Date,
                              Machine_End_Date = obj.Machine_End_Date,
                              Machine_Original_Purchase_Price = obj.Machine_Original_Purchase_Price
                          }).ToList<GetDepreciationDetailsResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }


        public bool CheckForceSiteRepsDetails()
        {
            bool retVal = false;
            try
            {
                List<rsp_GetRepresentativeDetailsResult> lstRepDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstRepDetails = DataContext.CheckForceSiteRepsDetails().ToList();
                }
                if (lstRepDetails != null && lstRepDetails.Count > 0 && (lstRepDetails[0].System_Parameter_Force_Site_Reps_On_Stock ?? false))
                {
                    retVal = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public bool CheckMACInUse(string MACAddress, int? machine_ID)
        {
            bool retVal = false;
            try
            {
                List<rsp_CheckMACInUseResult> lstMACDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMACDetails = DataContext.CheckMACInUse(MACAddress, machine_ID).ToList();
                }
                if (lstMACDetails != null && lstMACDetails.Count > 0 && (lstMACDetails[0].Machine_ID > 0))
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

        public void GetMachine_IDForTemplate(string TemplateName,ref int Machine_Id)
        {
            try
            {
                int retVal = 0;
                Machine_Id = 0;
                int? machineID = 0;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = DataContext.GetMachineIDForTemplate(TemplateName, ref machineID);
                }
                Machine_Id = Convert.ToInt32(machineID);
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }



        public void CheckMCAndMCClassExists(string machine_Stock_No, int? machine_ID, string actSerialNo, string actAssetNo, string GMUNo, ref bool? MachineExist, ref bool? MachineClassExist)
        {
            try
            {
                int retVal = 0;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = DataContext.CheckMachineAndMachineClassInUse(machine_Stock_No, machine_ID, actSerialNo, actAssetNo, GMUNo, ref MachineExist, ref MachineClassExist);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool AddMachineClass_Details(int? machine_ID, string machine_Name, bool? IsEdit,
                    bool? isNGA,
                    int? manufacturer_ID,
                    ref int? machine_Type_ID,
                    int? machine_Class_Category_ID,
                    int? machine_Class_SP_Features,
                    string machine_Class_Model_Code,
                    int? depreciation_Policy_ID,
                    bool? depreciation_Policy_Use_Default,
                  //  int? machine_Class_Occupancy_Games_Per_Hour,
                    int? machine_Class_Counter_Cash_In_Units,
                    int? machine_Class_Counter_Cash_Out_Units,
                    int? machine_Class_Counter_Tokens_In_Units,
                    int? machine_Class_Counter_Tokens_Out_Units,
                    string machine_Class_Config_Machine_Version,
                    string machine_Class_Config_Attract_Mode_Text,
                    bool? machine_Class_UseCancelledCreditsAsTicketsPrinted,
                    bool? machine_Class_RecreateTicketsInsertedfromDrop,
                    int? meter_Rollover,
                    bool? machine_Class_Test_Machine,
                    int? validation_Length,                    
                    ref int? Machine_ClassID)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.AddMachineClass_Details(machine_ID,
                        machine_Name,
                        IsEdit,
                        isNGA,
                        manufacturer_ID,
                        ref machine_Type_ID,
                        machine_Class_Category_ID,
                        machine_Class_SP_Features,
                        machine_Class_Model_Code,
                        depreciation_Policy_ID,
                        depreciation_Policy_Use_Default,
                       // machine_Class_Occupancy_Games_Per_Hour,
                        machine_Class_Counter_Cash_In_Units,
                        machine_Class_Counter_Cash_Out_Units,
                        machine_Class_Counter_Tokens_In_Units,
                        machine_Class_Counter_Tokens_Out_Units,
                        machine_Class_Config_Machine_Version,
                        machine_Class_Config_Attract_Mode_Text,
                        machine_Class_UseCancelledCreditsAsTicketsPrinted,
                        machine_Class_RecreateTicketsInsertedfromDrop,
                        meter_Rollover,
                        machine_Class_Test_Machine,
                        validation_Length,
                        ref Machine_ClassID) >= 0)
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


        public bool AddMachineDetails(
                    int? machine_ID,
                    string actAssetNo,
                    string actSerialNo,
                    char? CMPGameType,
                    string gameType,
                    int? depot_ID,
                    int? depreciation_Policy_ID,
                    bool depreciation_Policy_Use_Default,
                    int? enrolmentFlag,
                    string gMUNo,
                    bool? isAFTEnabled,
                    bool isMultiGame,
                    int? isNonCashVoucherEnabled,
                    int? isTITOEnabled,
                    string machine_Alternative_Serial_Numbers,
                    int? machine_Category_ID,
                    int? machine_Class_ID,
                    string machine_Date_Entered,
                    string machine_Depreciation_Start_Date,
                    string machine_End_Date,
                    string machine_MAC_Address,
                    string machine_MAC_Address_Prev,
                    string machine_Manufacturers_Serial_No,
                    string machine_Memo,
                    int? machine_ModelTypeID,
                    int? machine_New_Install,
                    decimal? machine_Original_Purchase_Price,
                    string machine_Purchase_Invoice_Number,
                    string machine_Purchased_From,
                    string machine_Start_Date,
                    string machine_Status,
                    int? machine_Status_Flag,
                    string machine_Stock_No,
                    bool bDefaultAssetDetail,                    
                    int Base_Denom,
                    float Percentage_Payout,
                    int? operator_ID,
                    int? stacker_Id,
                    int? staff_ID,
                    int? staff_ID_Entered,
                    int? terms_Profile_ID,
                    bool getGameDetails,
                    bool? isGameCappingEnabled,
                    string assetDisplayName,
                    int? OccupancyHour
                   )
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.AddMachineDetails(machine_ID,
                        actAssetNo,
                        actSerialNo,
                        CMPGameType,
                        gameType,
                        depot_ID,
                        depreciation_Policy_ID,
                        depreciation_Policy_Use_Default,
                        enrolmentFlag,
                        gMUNo,
                        isAFTEnabled,
                        isMultiGame,
                        isNonCashVoucherEnabled,
                        isTITOEnabled,                        
                        machine_Alternative_Serial_Numbers,
                        machine_Category_ID,
                        machine_Class_ID,
                        machine_Date_Entered,
                        machine_Depreciation_Start_Date,
                        machine_End_Date,
                        machine_MAC_Address,
                        machine_MAC_Address_Prev,
                        machine_Manufacturers_Serial_No,
                        machine_Memo,
                        machine_ModelTypeID,
                        machine_New_Install,
                        machine_Original_Purchase_Price,
                        machine_Purchase_Invoice_Number,
                        machine_Purchased_From,
                        machine_Start_Date,
                        machine_Status,
                        machine_Status_Flag,
                        machine_Stock_No,
                        bDefaultAssetDetail,
                        Base_Denom,
                        Percentage_Payout,
                        operator_ID,
                        stacker_Id,
                        staff_ID,
                        staff_ID_Entered,
                        terms_Profile_ID,
                        getGameDetails,
                        isGameCappingEnabled,
                        assetDisplayName,
                        OccupancyHour
                        ) >= 0)
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


        public bool UpdateTemplateDetails(
                    string templateName,
                    string actAssetNo,
                    string actSerialNo,
                    char? CMPGameType,
                    string gameType,
                    int? depot_ID,
                    int? depreciation_Policy_ID,
                    bool depreciation_Policy_Use_Default,
                    int? enrolmentFlag,
                    string gMUNo,
                    bool? isAFTEnabled,
                    bool isMultiGame,
                    int? isNonCashVoucherEnabled,
                    int? isTITOEnabled,
                    string machine_Alternative_Serial_Numbers,
                    int? machine_Category_ID,
                    int? machine_Class_ID,
                    string machine_Date_Entered,
                    string machine_Depreciation_Start_Date,
                    string machine_End_Date,
                    string machine_MAC_Address,
                    string machine_MAC_Address_Prev,
                    string machine_Manufacturers_Serial_No,
                    string machine_Memo,
                    int? machine_ModelTypeID,
                    int? machine_New_Install,
                    decimal? machine_Original_Purchase_Price,
                    string machine_Purchase_Invoice_Number,
                    string machine_Purchased_From,
                    string machine_Start_Date,
                    string machine_Status,
                    int? machine_Status_Flag,
                    string machine_Stock_No,
                    bool bDefaultAssetDetail,
                    int Base_Denom,
                    float Percentage_Payout,
                    int? operator_ID,
                    int? stacker_Id,
                    int? staff_ID,
                    int? staff_ID_Entered,
                    int? terms_Profile_ID,
                    bool getGameDetails,
                    bool? isGameCappingEnabled,
                    string assetDisplayName
                   )
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.usp_UpdateTemplateDetails(templateName,
                        actAssetNo,
                        actSerialNo,
                        CMPGameType,
                        gameType,
                        depot_ID,
                        depreciation_Policy_ID,
                        depreciation_Policy_Use_Default,
                        enrolmentFlag,
                        gMUNo,
                        isAFTEnabled,
                        isMultiGame,
                        isNonCashVoucherEnabled,
                        isTITOEnabled,
                        machine_Alternative_Serial_Numbers,
                        machine_Category_ID,
                        machine_Class_ID,
                        machine_Date_Entered,
                        machine_Depreciation_Start_Date,
                        machine_End_Date,
                        machine_MAC_Address,
                        machine_MAC_Address_Prev,
                        machine_Manufacturers_Serial_No,
                        machine_Memo,
                        machine_ModelTypeID,
                        machine_New_Install,
                        machine_Original_Purchase_Price,
                        machine_Purchase_Invoice_Number,
                        machine_Purchased_From,
                        machine_Start_Date,
                        machine_Status,
                        machine_Status_Flag,
                        machine_Stock_No,
                        bDefaultAssetDetail,
                        Base_Denom,
                        Percentage_Payout,
                        operator_ID,
                        stacker_Id,
                        staff_ID,
                        staff_ID_Entered,
                        terms_Profile_ID,
                        getGameDetails,
                        isGameCappingEnabled,
                        assetDisplayName
                        ) >= 0)
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

        public bool UpdateTemplateMachineClass(string templateName, string machine_Name, bool? IsEdit,
                    bool? isNGA,
                    int? manufacturer_ID,
                    ref int? machine_Type_ID,
                    int? machine_Class_Category_ID,
                    int? machine_Class_SP_Features,
                    string machine_Class_Model_Code,
                    int? depreciation_Policy_ID,
                    bool? depreciation_Policy_Use_Default,
                    int? machine_Class_Occupancy_Games_Per_Hour,
                    int? machine_Class_Counter_Cash_In_Units,
                    int? machine_Class_Counter_Cash_Out_Units,
                    int? machine_Class_Counter_Tokens_In_Units,
                    int? machine_Class_Counter_Tokens_Out_Units,
                    string machine_Class_Config_Machine_Version,
                    string machine_Class_Config_Attract_Mode_Text,
                    bool? machine_Class_UseCancelledCreditsAsTicketsPrinted,
                    bool? machine_Class_RecreateTicketsInsertedfromDrop,
                    int? meter_Rollover,
                    bool? machine_Class_Test_Machine,
                    int? validation_Length,
                    ref int? Machine_ClassID)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.usp_Update_TemplateMachine_Class(templateName,
                        machine_Name,
                        manufacturer_ID,
                        ref machine_Type_ID,
                        machine_Class_Category_ID,
                        machine_Class_SP_Features,
                        machine_Class_Model_Code,
                        depreciation_Policy_ID,
                        depreciation_Policy_Use_Default,
                        machine_Class_Occupancy_Games_Per_Hour,
                        machine_Class_Counter_Cash_In_Units,
                        machine_Class_Counter_Cash_Out_Units,
                        machine_Class_Counter_Tokens_In_Units,
                        machine_Class_Counter_Tokens_Out_Units,
                        machine_Class_Config_Machine_Version,
                        machine_Class_Config_Attract_Mode_Text,
                        machine_Class_UseCancelledCreditsAsTicketsPrinted,
                        machine_Class_RecreateTicketsInsertedfromDrop,
                        meter_Rollover,
                        machine_Class_Test_Machine,
                        validation_Length,
                        ref Machine_ClassID) >= 0)
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

        public bool AddMultiGameNameForAsset(int? MachineID,string MultiGameMachineName,bool NewMultiGameMC)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.AddMultiGameNameForAsset(MachineID, MultiGameMachineName, NewMultiGameMC) >= 0)
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


        public bool InsertMachineUpdateEHRecord(int? ID, string site_Code)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.InsertMachineUpdateEHRecord(ID, site_Code) >= 0)
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

        public List<GetMachineDetailsFromAssetResult> GetMachineDetailsFromAsset(string Stock_No)
        {
            List<GetMachineDetailsFromAssetResult> obcoll = null;
            try
            {
                List<rsp_GetMachineDetailsFromAssetResult> lstMCDetails;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    lstMCDetails = DataContext.GetMachineDetailsFromAsset(Stock_No, 0).ToList();
                }

                obcoll = (from obj in lstMCDetails
                          select new GetMachineDetailsFromAssetResult
                          {
                              Installation_ID = obj.Installation_ID,
                              Machine_Name = obj.Machine_Name,
                              Site_ID = obj.Site_ID,
                              Bar_Position_Name = obj.Bar_Position_Name,
                              Site_Code = obj.Site_Code

                          }).ToList<GetMachineDetailsFromAssetResult>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public void GetActiveInstallationFromMachineID(int? machine_ID, ref int? installation_ID, ref string site_Code)
        {
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.GetActiveInstallationFromMachineID(machine_ID, ref installation_ID, ref site_Code);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
