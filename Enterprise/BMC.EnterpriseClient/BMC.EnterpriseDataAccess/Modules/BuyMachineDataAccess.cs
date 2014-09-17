using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [Function(Name = "dbo.usp_GetMachineID")]
        public ISingleResult<usp_GetMachineIDResult> GetMachineID([Parameter(Name = "MachineID", DbType = "Int")] System.Nullable<int> machineID, [Parameter(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID, [Parameter(Name = "Machine_New_Install", DbType = "Int")] System.Nullable<int> machine_New_Install, [Parameter(Name = "Machine_Stock_No", DbType = "VarChar(50)")] ref string machine_Stock_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineID, machine_Class_ID, machine_New_Install, machine_Stock_No);
            machine_Stock_No = ((string)(result.GetParameterValue(3)));
            return ((ISingleResult<usp_GetMachineIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineClassDetails")]
        public ISingleResult<rsp_GetMachineClassDetailsResult> GetMachineClassDetails([Parameter(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID, [Parameter(Name = "Machine_Name", DbType = "VarChar(50)")] string machine_Name, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID
            , [Parameter(Name = "Machine_Type_ID", DbType = "Int")] System.Nullable<int> Machine_Type_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Class_ID, machine_Name, manufacturer_ID, Machine_Type_ID);
            return ((ISingleResult<rsp_GetMachineClassDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepreciationPolicyDetails")]
        public ISingleResult<rsp_GetDepreciationPolicyDetailsResult> GetDepreciationPolicyDetails([Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_ID);
            return ((ISingleResult<rsp_GetDepreciationPolicyDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetManufacturerDetails")]
        public ISingleResult<rsp_GetManufacturerDetailsResult> GetManufacturerDetails([Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturer_ID);
            return ((ISingleResult<rsp_GetManufacturerDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineTypeDetails")]
        public ISingleResult<rsp_GetMachineTypeDetailsResult> GetMachineTypeDetails([Parameter(Name = "Machine_Type_ID", DbType = "Int")] System.Nullable<int> machine_Type_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Type_ID);
            return ((ISingleResult<rsp_GetMachineTypeDetailsResult>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetGameTitle")]
        public ISingleResult<rsp_GetGameTitleResult> GetGameTitle([Parameter(Name = "IsMultiGame", DbType = "Bit")] System.Nullable<bool> isMultiGame)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), isMultiGame);
            return ((ISingleResult<rsp_GetGameTitleResult>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetStaffByDepot")]
        public ISingleResult<rsp_GetStaffByDepotResult> GetStaffByDepot([Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depot_ID);
            return ((ISingleResult<rsp_GetStaffByDepotResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBuyMachineDetails")]
        public ISingleResult<rsp_GetBuyMachineDetailsResult> GetBuyMachineDetails([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID, [Parameter(Name = "TemplateName", DbType = "VarChar(50)")] string templateName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, templateName);
            return ((ISingleResult<rsp_GetBuyMachineDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepreciationDetails")]
        public ISingleResult<rsp_GetDepreciationDetailsResult> GetDepreciationDetails([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID);
            return ((ISingleResult<rsp_GetDepreciationDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetRepresentativeDetails")]
        public ISingleResult<rsp_GetRepresentativeDetailsResult> CheckForceSiteRepsDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetRepresentativeDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckMachineAndMachineClassInUse")]
        public int CheckMachineAndMachineClassInUse([Parameter(Name = "Machine_Stock_No", DbType = "VarChar(50)")] string machine_Stock_No, [Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID, [Parameter(Name = "ActSerialNo", DbType = "VarChar(50)")] string actSerialNo, [Parameter(Name = "ActAssetNo", DbType = "VarChar(50)")] string actAssetNo, [Parameter(Name = "GMUNo", DbType = "VarChar(50)")] string gMUNo, [Parameter(Name = "MachineExist", DbType = "Bit")] ref System.Nullable<bool> machineExist, [Parameter(Name = "MachineClassExist", DbType = "Bit")] ref System.Nullable<bool> machineClassExist)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Stock_No, machine_ID, actSerialNo, actAssetNo, gMUNo, machineExist, machineClassExist);
            machineExist = ((System.Nullable<bool>)(result.GetParameterValue(5)));
            machineClassExist = ((System.Nullable<bool>)(result.GetParameterValue(6)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineIDForTemplate")]
        public int GetMachineIDForTemplate([Parameter(Name = "TemplateName", DbType = "VarChar(50)")] string templateName, [Parameter(Name = "Machine_ID", DbType = "Int")] ref System.Nullable<int> machine_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), templateName, machine_ID);
            machine_ID = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_AddMachineClass_Details")]
        public int AddMachineClass_Details(
                    [Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID,
                    [Parameter(Name = "Machine_Name", DbType = "VarChar(50)")] string machine_Name,
                    [Parameter(Name = "EDIT", DbType = "Bit")] System.Nullable<bool> eDIT,
                    [Parameter(Name = "IsNGA", DbType = "Bit")] System.Nullable<bool> isNGA,
                    [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID,
                    [Parameter(Name = "Machine_Type_ID", DbType = "Int")] ref System.Nullable<int> machine_Type_ID,
                    [Parameter(Name = "Machine_Class_Category_ID", DbType = "Int")] System.Nullable<int> machine_Class_Category_ID,
                    [Parameter(Name = "Machine_Class_SP_Features", DbType = "Int")] System.Nullable<int> machine_Class_SP_Features,
                    [Parameter(Name = "Machine_Class_Model_Code", DbType = "VarChar(50)")] string machine_Class_Model_Code,
                    [Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID,
                    [Parameter(Name = "Depreciation_Policy_Use_Default", DbType = "Bit")] System.Nullable<bool> depreciation_Policy_Use_Default,
                   // [Parameter(Name = "Machine_Class_Occupancy_Games_Per_Hour", DbType = "Int")] System.Nullable<int> machine_Class_Occupancy_Games_Per_Hour,
                    [Parameter(Name = "Machine_Class_Counter_Cash_In_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Cash_In_Units,
                    [Parameter(Name = "Machine_Class_Counter_Cash_Out_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Cash_Out_Units,
                    [Parameter(Name = "Machine_Class_Counter_Tokens_In_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Tokens_In_Units,
                    [Parameter(Name = "Machine_Class_Counter_Tokens_Out_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Tokens_Out_Units,
                    [Parameter(Name = "Machine_Class_Config_Machine_Version", DbType = "VarChar(50)")] string machine_Class_Config_Machine_Version,
                    [Parameter(Name = "Machine_Class_Config_Attract_Mode_Text", DbType = "VarChar(50)")] string machine_Class_Config_Attract_Mode_Text,
                    [Parameter(Name = "Machine_Class_UseCancelledCreditsAsTicketsPrinted", DbType = "Bit")] System.Nullable<bool> machine_Class_UseCancelledCreditsAsTicketsPrinted,
                    [Parameter(Name = "Machine_Class_RecreateTicketsInsertedfromDrop", DbType = "Bit")] System.Nullable<bool> machine_Class_RecreateTicketsInsertedfromDrop,
                    [Parameter(Name = "Meter_Rollover", DbType = "Int")] System.Nullable<int> meter_Rollover,
                    [Parameter(Name = "Machine_Class_Test_Machine", DbType = "Bit")] System.Nullable<bool> machine_Class_Test_Machine,
                    [Parameter(Name = "Validation_Length", DbType = "Int")] System.Nullable<int> validation_Length,                    
                    [Parameter(Name = "MachineClassID", DbType = "Int")] ref System.Nullable<int> machineClassID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, machine_Name, eDIT, isNGA, manufacturer_ID, machine_Type_ID, machine_Class_Category_ID, machine_Class_SP_Features, machine_Class_Model_Code, depreciation_Policy_ID, depreciation_Policy_Use_Default, 
              //  machine_Class_Occupancy_Games_Per_Hour,
                machine_Class_Counter_Cash_In_Units, machine_Class_Counter_Cash_Out_Units, machine_Class_Counter_Tokens_In_Units, machine_Class_Counter_Tokens_Out_Units, machine_Class_Config_Machine_Version, machine_Class_Config_Attract_Mode_Text, machine_Class_UseCancelledCreditsAsTicketsPrinted, machine_Class_RecreateTicketsInsertedfromDrop, meter_Rollover, machine_Class_Test_Machine, validation_Length,machineClassID);
            machine_Type_ID = ((System.Nullable<int>)(result.GetParameterValue(5)));
            machineClassID = ((System.Nullable<int>)(result.GetParameterValue(22)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_AddMachineDetails")]
        public int AddMachineDetails(
                    [Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID,
                    [Parameter(Name = "ActAssetNo", DbType = "VarChar(50)")] string actAssetNo,
                    [Parameter(Name = "ActSerialNo", DbType = "VarChar(50)")] string actSerialNo,
                    [Parameter(Name = "CMPGameType", DbType = "Char(1)")] System.Nullable<char> cMPGameType,
                    [Parameter(Name = "GameType", DbType = "VarChar(50)")] string gameType,
                    [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID,
                    [Parameter(Name = "Depreciation_Policy_Use_Default", DbType = "Bit")] System.Nullable<bool> depreciation_Policy_Use_Default,
                    [Parameter(Name = "EnrolmentFlag", DbType = "Int")] System.Nullable<int> enrolmentFlag,
                    [Parameter(Name = "GMUNo", DbType = "VarChar(50)")] string gMUNo,
                    [Parameter(Name = "IsAFTEnabled", DbType = "Bit")] System.Nullable<bool> isAFTEnabled,                    
                    [Parameter(Name = "IsMultiGame", DbType = "Bit")] System.Nullable<bool> isMultiGame,
                    [Parameter(Name = "IsNonCashVoucherEnabled", DbType = "Int")] System.Nullable<int> isNonCashVoucherEnabled,
                    [Parameter(Name = "IsTITOEnabled", DbType = "Int")] System.Nullable<int> isTITOEnabled,
                    [Parameter(Name = "Machine_Alternative_Serial_Numbers", DbType = "VarChar(50)")] string machine_Alternative_Serial_Numbers,
                    [Parameter(Name = "Machine_Category_ID", DbType = "Int")] System.Nullable<int> machine_Category_ID,
                    [Parameter(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID,
                    [Parameter(Name = "Machine_Date_Entered", DbType = "VarChar(30)")] string machine_Date_Entered,
                    [Parameter(Name = "Machine_Depreciation_Start_Date", DbType = "VarChar(30)")] string machine_Depreciation_Start_Date,
                    [Parameter(Name = "Machine_End_Date", DbType = "VarChar(30)")] string machine_End_Date,
                    [Parameter(Name = "Machine_MAC_Address", DbType = "VarChar(17)")] string machine_MAC_Address,
                    [Parameter(Name = "Machine_MAC_Address_Prev", DbType = "VarChar(17)")] string machine_MAC_Address_Prev,
                    [Parameter(Name = "Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")] string machine_Manufacturers_Serial_No,
                    [Parameter(Name = "Machine_Memo", DbType = "NText")] string machine_Memo,
                    [Parameter(Name = "Machine_ModelTypeID", DbType = "Int")] System.Nullable<int> machine_ModelTypeID,
                    [Parameter(Name = "Machine_New_Install", DbType = "Int")] System.Nullable<int> machine_New_Install,
                    [Parameter(Name = "Machine_Original_Purchase_Price", DbType = "Money")] System.Nullable<decimal> machine_Original_Purchase_Price,
                    [Parameter(Name = "Machine_Purchase_Invoice_Number", DbType = "VarChar(50)")] string machine_Purchase_Invoice_Number,
                    [Parameter(Name = "Machine_Purchased_From", DbType = "VarChar(50)")] string machine_Purchased_From,
                    [Parameter(Name = "Machine_Start_Date", DbType = "VarChar(30)")] string machine_Start_Date,
                    [Parameter(Name = "Machine_Status", DbType = "VarChar(50)")] string machine_Status,
                    [Parameter(Name = "Machine_Status_Flag", DbType = "Int")] System.Nullable<int> machine_Status_Flag,
                    [Parameter(Name = "Machine_Stock_No", DbType = "VarChar(50)")] string machine_Stock_No,
                    [Parameter(Name = "IsDefaultAssetDetail", DbType = "Bit")] System.Nullable<bool> bDefaultAssetDetail,
                    [Parameter(Name = "Base_Denom", DbType = "Int")] System.Nullable<int> Base_Denom,
                    [Parameter(Name = "Percentage_Payout", DbType = "Real")] float Percentage_Payout,
                    [Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID,
                    [Parameter(Name = "Stacker_Id", DbType = "Int")] System.Nullable<int> stacker_Id,
                    [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID,
                    [Parameter(Name = "Staff_ID_Entered", DbType = "Int")] System.Nullable<int> staff_ID_Entered,
                    [Parameter(Name = "Terms_Profile_ID", DbType = "Int")] System.Nullable<int> terms_Profile_ID,
                    [Parameter(Name = "GetGameDetails", DbType = "Bit")] System.Nullable<bool> getGameDetails,
                    [Parameter(Name = "IsGameCappingEnabled", DbType = "Bit")] System.Nullable<bool> IsGameCappingEnabled,
                    [Parameter(Name = "AssetDisplayName", DbType = "VarChar(8)")] string AssetDisplayName,
                    [Parameter(Name = "OccupancyHour", DbType = "Int")] System.Nullable<int> OccupancyHour
                  )
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, actAssetNo,
                actSerialNo, cMPGameType, gameType, depot_ID, depreciation_Policy_ID, depreciation_Policy_Use_Default, enrolmentFlag,
                gMUNo, isAFTEnabled, isMultiGame, isNonCashVoucherEnabled, isTITOEnabled, machine_Alternative_Serial_Numbers, machine_Category_ID,
                machine_Class_ID, machine_Date_Entered, machine_Depreciation_Start_Date, machine_End_Date, machine_MAC_Address, machine_MAC_Address_Prev,
                machine_Manufacturers_Serial_No, machine_Memo, machine_ModelTypeID, machine_New_Install, machine_Original_Purchase_Price,
                machine_Purchase_Invoice_Number, machine_Purchased_From, machine_Start_Date, machine_Status, machine_Status_Flag, machine_Stock_No,
                bDefaultAssetDetail, Base_Denom, Percentage_Payout, operator_ID, stacker_Id, staff_ID, staff_ID_Entered, terms_Profile_ID,
                getGameDetails, IsGameCappingEnabled, AssetDisplayName, OccupancyHour);
            return ((int)(result.ReturnValue));
        }


        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_AddMultiGameNameForAsset")]
        public int AddMultiGameNameForAsset([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MultiGameName", DbType = "VarChar(50)")] string multiGameName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AddNew", DbType = "Bit")] System.Nullable<bool> addNew)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, multiGameName, addNew);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_InsertMachineUpdateEHRecord")]
        public int InsertMachineUpdateEHRecord([Parameter(Name = "ID", DbType = "Int")] System.Nullable<int> iD, [Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string site_Code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD, site_Code);
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetMachineDetailsFromAsset")]
        public ISingleResult<rsp_GetMachineDetailsFromAssetResult> GetMachineDetailsFromAsset([Parameter(DbType = "VarChar(50)")] string sMachine, [Parameter(Name = "SITE", DbType = "Int")] System.Nullable<int> sITE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sMachine, sITE);
            return ((ISingleResult<rsp_GetMachineDetailsFromAssetResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetActiveInstallationFromMachineID")]
        public int GetActiveInstallationFromMachineID([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID, [Parameter(Name = "Installation_ID", DbType = "Int")] ref System.Nullable<int> installation_ID, [Parameter(Name = "Site_Code", DbType = "VarChar(50)")] ref string site_Code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, installation_ID, site_Code);
            installation_ID = ((System.Nullable<int>)(result.GetParameterValue(1)));
            site_Code = ((string)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckMACInUse")]
        public ISingleResult<rsp_CheckMACInUseResult> CheckMACInUse([Parameter(Name = "MAC_Address", DbType = "VarChar(17)")] string mAC_Address, [Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mAC_Address, machine_ID);
            return ((ISingleResult<rsp_CheckMACInUseResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateTemplateDetails")]
        public int usp_UpdateTemplateDetails(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TemplateName", DbType = "VarChar(50)")] string templateName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ActAssetNo", DbType = "VarChar(50)")] string actAssetNo,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "ActSerialNo", DbType = "VarChar(50)")] string actSerialNo,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CMPGameType", DbType = "Char(1)")] System.Nullable<char> cMPGameType,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GameType", DbType = "VarChar(50)")] string gameType,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Depreciation_Policy_Use_Default", DbType = "Bit")] System.Nullable<bool> depreciation_Policy_Use_Default,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EnrolmentFlag", DbType = "Int")] System.Nullable<int> enrolmentFlag,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GMUNo", DbType = "VarChar(50)")] string gMUNo,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsAFTEnabled", DbType = "Bit")] System.Nullable<bool> isAFTEnabled,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsMultiGame", DbType = "Bit")] System.Nullable<bool> isMultiGame,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsNonCashVoucherEnabled", DbType = "Int")] System.Nullable<int> isNonCashVoucherEnabled,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsTITOEnabled", DbType = "Int")] System.Nullable<int> isTITOEnabled,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Alternative_Serial_Numbers", DbType = "VarChar(50)")] string machine_Alternative_Serial_Numbers,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Category_ID", DbType = "Int")] System.Nullable<int> machine_Category_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Date_Entered", DbType = "VarChar(30)")] string machine_Date_Entered,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Depreciation_Start_Date", DbType = "VarChar(30)")] string machine_Depreciation_Start_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_End_Date", DbType = "VarChar(30)")] string machine_End_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_MAC_Address", DbType = "VarChar(17)")] string machine_MAC_Address,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_MAC_Address_Prev", DbType = "VarChar(17)")] string machine_MAC_Address_Prev,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")] string machine_Manufacturers_Serial_No,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Memo", DbType = "NText")] string machine_Memo,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_ModelTypeID", DbType = "Int")] System.Nullable<int> machine_ModelTypeID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_New_Install", DbType = "Int")] System.Nullable<int> machine_New_Install,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Original_Purchase_Price", DbType = "Money")] System.Nullable<decimal> machine_Original_Purchase_Price,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Purchase_Invoice_Number", DbType = "VarChar(50)")] string machine_Purchase_Invoice_Number,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Purchased_From", DbType = "VarChar(50)")] string machine_Purchased_From,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Start_Date", DbType = "VarChar(30)")] string machine_Start_Date,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Status", DbType = "VarChar(50)")] string machine_Status,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Status_Flag", DbType = "Int")] System.Nullable<int> machine_Status_Flag,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Stock_No", DbType = "VarChar(50)")] string machine_Stock_No,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsDefaultAssetDetail", DbType = "Bit")] System.Nullable<bool> isDefaultAssetDetail,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Base_Denom", DbType = "Int")] System.Nullable<int> base_Denom,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Percentage_Payout", DbType = "Real")] System.Nullable<float> percentage_Payout,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Stacker_Id", DbType = "Int")] System.Nullable<int> stacker_Id,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Staff_ID_Entered", DbType = "Int")] System.Nullable<int> staff_ID_Entered,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_ID", DbType = "Int")] System.Nullable<int> terms_Profile_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GetGameDetails", DbType = "Bit")] System.Nullable<bool> getGameDetails,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "IsGameCappingEnabled", DbType = "Bit")] System.Nullable<bool> isGameCappingEnabled,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AssetDisplayName", DbType = "VarChar(8)")] string assetDisplayName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), templateName, actAssetNo, actSerialNo, cMPGameType, gameType, depot_ID, depreciation_Policy_ID, depreciation_Policy_Use_Default, enrolmentFlag, gMUNo, isAFTEnabled, isMultiGame, isNonCashVoucherEnabled, isTITOEnabled, machine_Alternative_Serial_Numbers, machine_Category_ID, machine_Class_ID, machine_Date_Entered, machine_Depreciation_Start_Date, machine_End_Date, machine_MAC_Address, machine_MAC_Address_Prev, machine_Manufacturers_Serial_No, machine_Memo, machine_ModelTypeID, machine_New_Install, machine_Original_Purchase_Price, machine_Purchase_Invoice_Number, machine_Purchased_From, machine_Start_Date, machine_Status, machine_Status_Flag, machine_Stock_No, isDefaultAssetDetail, base_Denom, percentage_Payout, operator_ID, stacker_Id, staff_ID, staff_ID_Entered, terms_Profile_ID, getGameDetails, isGameCappingEnabled, assetDisplayName);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_Update_TemplateMachine_Class")]
        public int usp_Update_TemplateMachine_Class(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TemplateName", DbType = "VarChar(50)")] string templateName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Name", DbType = "VarChar(50)")] string machine_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Type_ID", DbType = "Int")] ref System.Nullable<int> machine_Type_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Category_ID", DbType = "Int")] System.Nullable<int> machine_Class_Category_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_SP_Features", DbType = "Int")] System.Nullable<int> machine_Class_SP_Features,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Model_Code", DbType = "VarChar(50)")] string machine_Class_Model_Code,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Depreciation_Policy_Use_Default", DbType = "Bit")] System.Nullable<bool> depreciation_Policy_Use_Default,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Occupancy_Games_Per_Hour", DbType = "Int")] System.Nullable<int> machine_Class_Occupancy_Games_Per_Hour,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Counter_Cash_In_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Cash_In_Units,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Counter_Cash_Out_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Cash_Out_Units,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Counter_Tokens_In_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Tokens_In_Units,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Counter_Tokens_Out_Units", DbType = "Int")] System.Nullable<int> machine_Class_Counter_Tokens_Out_Units,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Config_Machine_Version", DbType = "VarChar(50)")] string machine_Class_Config_Machine_Version,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Config_Attract_Mode_Text", DbType = "VarChar(50)")] string machine_Class_Config_Attract_Mode_Text,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_UseCancelledCreditsAsTicketsPrinted", DbType = "Bit")] System.Nullable<bool> machine_Class_UseCancelledCreditsAsTicketsPrinted,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_RecreateTicketsInsertedfromDrop", DbType = "Bit")] System.Nullable<bool> machine_Class_RecreateTicketsInsertedfromDrop,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Meter_Rollover", DbType = "Int")] System.Nullable<int> meter_Rollover,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_Test_Machine", DbType = "Bit")] System.Nullable<bool> machine_Class_Test_Machine,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Validation_Length", DbType = "Int")] System.Nullable<int> validation_Length,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MachineClassID", DbType = "Int")] ref System.Nullable<int> machineClassID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), templateName, machine_Name, manufacturer_ID, machine_Type_ID, machine_Class_Category_ID, machine_Class_SP_Features, machine_Class_Model_Code, depreciation_Policy_ID, depreciation_Policy_Use_Default, machine_Class_Occupancy_Games_Per_Hour, machine_Class_Counter_Cash_In_Units, machine_Class_Counter_Cash_Out_Units, machine_Class_Counter_Tokens_In_Units, machine_Class_Counter_Tokens_Out_Units, machine_Class_Config_Machine_Version, machine_Class_Config_Attract_Mode_Text, machine_Class_UseCancelledCreditsAsTicketsPrinted, machine_Class_RecreateTicketsInsertedfromDrop, meter_Rollover, machine_Class_Test_Machine, validation_Length, machineClassID);
            machine_Type_ID = ((System.Nullable<int>)(result.GetParameterValue(3)));
            machineClassID = ((System.Nullable<int>)(result.GetParameterValue(21)));
            return ((int)(result.ReturnValue));
        }

    }

    public partial class usp_GetMachineIDResult
    {

        private System.Nullable<int> _Machine_ID;

        public usp_GetMachineIDResult()
        {
        }

        [Column(Storage = "_Machine_ID", DbType = "Int")]
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
    }

    public partial class rsp_GetMachineClassDetailsResult
    {

        private int _Machine_Class_ID;

        private string _Machine_Name;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private int _Machine_Class_Category_ID;

        private System.Nullable<int> _Manufacturer_ID;

        private string _Manufacturer_Name;

        private System.Nullable<int> _Validation_Length;

        public rsp_GetMachineClassDetailsResult()
        {
        }

        [Column(Storage = "_Machine_Class_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Class_Category_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Validation_Length", DbType = "Int")]
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

    public partial class rsp_GetDepreciationPolicyDetailsResult
    {

        private int _Depreciation_Policy_ID;

        private int _Depreciation_Policy_Details_ID;

        private string _Depreciation_Policy_Description;

        private System.Nullable<float> _Depreciation_Policy_Residual_Value;

        private System.Nullable<int> _Depreciation_Policy_Details_Period;

        private System.Nullable<int> _Depreciation_Policy_Details_Duration;

        private System.Nullable<int> _Depreciation_Policy_Details_Percentage;

        public rsp_GetDepreciationPolicyDetailsResult()
        {
        }

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Depreciation_Policy_Details_ID", DbType = "Int NOT NULL")]
        public int Depreciation_Policy_Details_ID
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

        [Column(Storage = "_Depreciation_Policy_Description", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Depreciation_Policy_Residual_Value", DbType = "Real")]
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

        [Column(Storage = "_Depreciation_Policy_Details_Period", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_Details_Period
        {
            get
            {
                return this._Depreciation_Policy_Details_Period;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Period != value))
                {
                    this._Depreciation_Policy_Details_Period = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_Details_Duration", DbType = "Int")]
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

        [Column(Storage = "_Depreciation_Policy_Details_Percentage", DbType = "Int")]
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

    public partial class rsp_GetManufacturerDetailsResult
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

        public rsp_GetManufacturerDetailsResult()
        {
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

        [Column(Storage = "_Manufacturer_Service_Contact", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Manufacturer_Service_EMail", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Manufacturer_Service_Tel", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Manufacturer_Service_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Manufacturer_Service_Postcode", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Manufacturer_Sales_Contact", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Manufacturer_Sales_EMail", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Manufacturer_Sales_Tel", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Manufacturer_Sales_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Manufacturer_Sales_Postcode", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Manufacturer_Code", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Manufacturer_Coins_In_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Coins_Out_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Coin_Drop_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Handpay_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_External_Credits_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Games_Bet_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Games_Won_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Notes_Meter_Used", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Single_Coin_Build", DbType = "Bit")]
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

        [Column(Storage = "_Manufacturer_Handpay_Added_To_Coin_Out", DbType = "Bit")]
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

    public partial class rsp_GetMachineTypeDetailsResult
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

        public rsp_GetMachineTypeDetailsResult()
        {
        }

        [Column(Storage = "_Machine_Type_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Type_Description", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Type_Category", DbType = "VarChar(20)")]
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

        [Column(Storage = "_IsNonGamingAssetType", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Machine_Type_AMEDIS_ID", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Type_Income_Ledger_Code", DbType = "VarChar(20)")]
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

        [Column(Storage = "_Machine_Type_Site_Icon", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Machine_Type_Icon_ref", DbType = "Int")]
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

        [Column(Storage = "_SiteIconPath", DbType = "NVarChar(300)")]
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

    public partial class rsp_GetGameTitleResult
    {

        private int _Game_Title_ID;

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Title;

        private System.Nullable<int> _Manufacturer_ID;

        public rsp_GetGameTitleResult()
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
    }

    public partial class rsp_CheckMACInUseResult
    {

        private int _Machine_ID;

        public rsp_CheckMACInUseResult()
        {
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
    }

    public partial class rsp_GetStaffByDepotResult
    {

        private int _Staff_ID;

        private string _Staff_First_Name;

        private string _Staff_Last_Name;

        public rsp_GetStaffByDepotResult()
        {
        }

        [Column(Storage = "_Staff_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_GetBuyMachineDetailsResult
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

        private string _GMUNo;

        private string _Machine_Memo;

        private string _Machine_Purchased_From;

        private string _Machine_Alternative_Serial_Numbers;

        private string _Machine_MAC_Address;

        private string _Machine_Depreciation_Start_Date;

        private string _Machine_Start_Date;

        private System.Nullable<decimal> _Machine_Original_Purchase_Price;

        private string _Machine_Purchase_Invoice_Number;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private System.Nullable<bool> _Depreciation_Policy_Use_Default;

        private System.Nullable<bool> _IsDefaultAssetDetail;

        private System.Nullable<int> _Base_Denom;

        private System.Nullable<float> _Percentage_Payout;

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

        private bool _GetGameDetails;

       private string _AssetDisplayName;

        private string _MultiGameName;
        public rsp_GetBuyMachineDetailsResult()
        {
        }
        [Column(Storage = "_MultiGameName", DbType = "VarChar(50)")]
        public string MultiGameName
        {
            get
            {
                return this._MultiGameName;
            }
            set
            {
                if ((this._MultiGameName != value))
                {
                    this._MultiGameName = value;
                }
            }
        }

        [Column(Storage = "_GameTypeCode", DbType = "VarChar(50)")]
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

        [Column(Storage = "_IsAFTEnabled", DbType = "Bit")]
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

        [Column(Storage = "_IsGameCappingEnabled", DbType = "Bit")]
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

        [Column(Storage = "_GetGameDetails", DbType = "Bit NOT NULL")]
        public bool GetGameDetails
        {
            get
            {
                return this._GetGameDetails;
            }
            set
            {
                if ((this._GetGameDetails != value))
                {
                    this._GetGameDetails = value;
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int")]
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

        [Column(Storage = "_Operator_ID", DbType = "Int")]
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

        [Column(Storage = "_CMPGameType", DbType = "Char(1)")]
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

        [Column(Storage = "_Staff_ID", DbType = "Int")]
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

        [Column(Storage = "_Machine_Connection_Type", DbType = "Int")]
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

        [Column(Storage = "_Machine_CIV_Change_Reason", DbType = "Int")]
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

        [Column(Storage = "_Machine_ModelTypeID", DbType = "Int")]
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

        [Column(Storage = "_Stacker_Id", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_IsTITOEnabled", DbType = "Bit")]
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

        [Column(Storage = "_IsNonCashVoucherEnabled", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Status_Flag", DbType = "Int")]
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

        [Column(Storage = "_AssetDisplayName", DbType = "VarChar(8)")]
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
        [Column(Storage = "_Machine_End_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Machine_Sales_Invoice_Number", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Sale_Price", DbType = "Money")]
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

        [Column(Storage = "_Machine_Sold_To", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Type_Of_Sale", DbType = "VarChar(150)")]
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

        [Column(Storage = "_ActAssetNo", DbType = "VarChar(50)")]
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

        [Column(Storage = "_ActSerialNo", DbType = "VarChar(50)")]
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

        [Column(Storage = "_GMUNo", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
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

        [Column(Storage = "_Machine_Purchased_From", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_MAC_Address", DbType = "VarChar(17)")]
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

        [Column(Storage = "_Machine_Depreciation_Start_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Machine_Start_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Machine_Original_Purchase_Price", DbType = "Money")]
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

        [Column(Storage = "_Machine_Purchase_Invoice_Number", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
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

        [Column(Storage = "_Depreciation_Policy_Use_Default", DbType = "Bit")]
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

        [Column(Storage = "_IsDefaultAssetDetail", DbType = "Bit")]
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

        [Column(Storage = "_Base_Denom", DbType = "Int")]
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

        [Column(Storage = "_Percentage_Payout", DbType = "Real")]
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

        [Column(Storage = "_Machine_Class_Occupancy_Games_Per_Hour", DbType = "Int")]
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

        [Column(Storage = "_Class_Depreciation", DbType = "Int")]
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

        [Column(Storage = "_Machine_Class_Category_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Type_Depreciation", DbType = "Int")]
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

        [Column(Storage = "_Validation_Length", DbType = "Int")]
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

        [Column(Storage = "_Staff_Sold_Staff_Last_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Staff_Sold_Staff_First_Name", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Old_Machine_ID", DbType = "Int")]
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

        [Column(Storage = "_Old_Machine_Start_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Old_Machine_Name", DbType = "VarChar(50)")]
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

    public partial class rsp_GetDepreciationDetailsResult
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

        public rsp_GetDepreciationDetailsResult()
        {
        }

        [Column(Storage = "_Machine_End_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Machine_Original_Purchase_Price", DbType = "Money")]
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

        [Column(Storage = "_Machine_Depreciation_Start_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
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

        [Column(Storage = "_Depreciation_Policy_Details_ID", DbType = "Int")]
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

        [Column(Storage = "_Depreciation_Policy_Residual_Value", DbType = "Real")]
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

        [Column(Storage = "_Depreciation_Policy_Details_Duration", DbType = "Int")]
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

        [Column(Storage = "_Depreciation_Policy_Details_Percentage", DbType = "Int")]
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

    public partial class rsp_GetRepresentativeDetailsResult
    {

        private System.Nullable<bool> _System_Parameter_Force_Site_Reps_On_Stock;

        public rsp_GetRepresentativeDetailsResult()
        {
        }

        [Column(Storage = "_System_Parameter_Force_Site_Reps_On_Stock", DbType = "Bit")]
        public System.Nullable<bool> System_Parameter_Force_Site_Reps_On_Stock
        {
            get
            {
                return this._System_Parameter_Force_Site_Reps_On_Stock;
            }
            set
            {
                if ((this._System_Parameter_Force_Site_Reps_On_Stock != value))
                {
                    this._System_Parameter_Force_Site_Reps_On_Stock = value;
                }
            }
        }
    }

    public partial class rsp_GetMachineDetailsFromAssetResult
    {

        private string _Bar_Position_Name;

        private string _Machine_Stock_No;

        private int _Installation_ID;

        private string _Machine_Name;

        private System.Nullable<int> _Site_ID;

        private string _Site_Code;

        public rsp_GetMachineDetailsFromAssetResult()
        {
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

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
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

}
