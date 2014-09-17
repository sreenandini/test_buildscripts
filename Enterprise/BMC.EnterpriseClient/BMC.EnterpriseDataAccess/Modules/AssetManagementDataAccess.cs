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
        [Function(Name = "dbo.rsp_GetMachineDetailsBasedonGameName")]
        public ISingleResult<rsp_GetMachineDetailsResult> GetMachineDetails([Parameter(Name = "Machine_Type_ID", DbType = "Int")] System.Nullable<int> machine_Type_ID, [Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID, [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID, [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID, [Parameter(Name = "Game_Category_ID", DbType = "Int")] System.Nullable<int> game_Category_ID, [Parameter(Name = "ModelTypeID", DbType = "Int")] System.Nullable<int> modelTypeID, [Parameter(Name = "Machine_Status", DbType = "NVarChar(50)")] string machine_Status, [Parameter(Name = "OrderBy", DbType = "VarChar(30)")] string orderBy, [Parameter(Name = "SearchCriteria", DbType = "NVarChar(100)")] string searchCriteria, [Parameter(Name = "MG_Game_ID", DbType = "Int")] System.Nullable<int> mG_Game_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MG_Game_Name", DbType = "NVarChar(50)")] string mG_Game_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Type_ID, operator_ID, depot_ID, staff_ID, manufacturer_ID, game_Category_ID, modelTypeID, machine_Status, orderBy, searchCriteria, mG_Game_ID, mG_Game_Name);
            return ((ISingleResult<rsp_GetMachineDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteDetailsByStock")]
        public ISingleResult<rsp_GetSiteDetailsByStockResult> GetSiteDetailsByStock([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID, [Parameter(Name = "IsNonGamingAsset", DbType = "Bit")] System.Nullable<bool> isNonGamingAsset)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, isNonGamingAsset);
            return ((ISingleResult<rsp_GetSiteDetailsByStockResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPaytableDetailsForGame")]
        public ISingleResult<rsp_GetPaytableDetailsForGameResult> GetPaytableDetailsForGame([Parameter(Name = "MachineId", DbType = "Int")] System.Nullable<int> machineId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineId);
            return ((ISingleResult<rsp_GetPaytableDetailsForGameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_FetchGameCategory")]
        public ISingleResult<rsp_FetchGameCategoryResult> FetchGameCategory([Parameter(Name = "Category_ID", DbType = "Int")] System.Nullable<int> category_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), category_ID);
            return ((ISingleResult<rsp_FetchGameCategoryResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckMachineInUse")]
        public ISingleResult<rsp_CheckMachineInUseResult> CheckMachineInUse([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID);
            return ((ISingleResult<rsp_CheckMachineInUseResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_CheckTemplateNameExists")]
        public int rsp_CheckTemplateNameExists([Parameter(Name = "TemplateName", DbType = "VarChar(50)")] string templateName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), templateName);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_CreateAssetTemplate")]
        public int usp_CreateAssetTemplate([Parameter(Name = "AssetNumber", DbType = "VarChar(50)")] string assetNumber, [Parameter(Name = "TemplateName", DbType = "VarChar(50)")] string templateName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), assetNumber, templateName);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetAssetTemplateDetails")]
        public ISingleResult<rsp_GetAssetTemplateDetailsResult> rsp_GetAssetTemplateDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetAssetTemplateDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameNames")]
        public ISingleResult<rsp_GetGameNamesResult> rsp_GetGameNames()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetGameNamesResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetGameNameForAsset")]
        public ISingleResult<rsp_GetGameNamesResult> GetGameNameForAsset()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetGameNamesResult>)(result.ReturnValue));
        }


        #region TerminationDataAccess

        [Function(Name = "dbo.rsp_GetMachineTerminationReason")]
        public ISingleResult<rsp_GetMachineTerminationReasonResult> GetMachineTerminationReason()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetMachineTerminationReasonResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateTemplate")]
        public int usp_UpdateTemplate([Parameter(DbType = "Bit")] System.Nullable<bool> bMode, [Parameter(Name = "StockNumber", DbType = "VarChar(50)")] string stockNumber, [Parameter(Name = "TemplateName", DbType = "VarChar(50)")] string templateName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bMode, stockNumber, templateName);
            return ((int)(result.ReturnValue));
        }

        
        [Function(Name = "dbo.rsp_GetTerminationMCDetails")]
        public ISingleResult<rsp_GetTerminationMCDetailsResult> GetTerminationMCDetails([Parameter(Name = "Machine_Stock_No", DbType = "VarChar(50)")] string machine_Stock_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Stock_No);
            return ((ISingleResult<rsp_GetTerminationMCDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateTerminationMCDetails")]
        public int UpdateTerminationMCDetails([Parameter(Name = "Machine_Stock_No", DbType = "VarChar(50)")] string machine_Stock_No, [Parameter(Name = "Machine_Termination_Comments", DbType = "VarChar(250)")] string machine_Termination_Comments, [Parameter(Name = "Machine_Termination_Username", DbType = "VarChar(100)")] string machine_Termination_Username, [Parameter(Name = "Machine_Termination_Reason", DbType = "Int")] System.Nullable<int> machine_Termination_Reason, [Parameter(Name = "Machine_Status_Flag", DbType = "Int")] System.Nullable<int> machine_Status_Flag, [Parameter(Name = "Machine_End_Date", DbType = "VarChar(30)")] string machine_End_Date, [Parameter(DbType = "Bit")] System.Nullable<bool> isNGA)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Stock_No, machine_Termination_Comments, machine_Termination_Username, machine_Termination_Reason, machine_Status_Flag, machine_End_Date, isNGA);
            return ((int)(result.ReturnValue));
        }

        #endregion

        #region Depreciation
        [Function(Name = "dbo.usp_DeleteDepreciationPolicy")]
        public int DeleteDepreciationPolicy([Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_DeleteDepreciationDetails")]
        public int DeleteDepreciationDetails([Parameter(Name = "Depreciation_Policy_Details_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_Details_ID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateDepreciationUseDefault")]
        public int UpdateDepreciationUseDefault([Parameter(Name = "Depreciation_Policy_Use_Default", DbType = "Bit")] System.Nullable<bool> depreciation_Policy_Use_Default)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_Use_Default);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertDepreciationDetails")]
        public int InsertDepreciationDetails([Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID, [Parameter(Name = "Depreciation_Policy_Details_Period", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_Period, [Parameter(Name = "Depreciation_Policy_Details_Duration", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_Duration, [Parameter(Name = "Depreciation_Policy_Details_Percentage", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_Percentage, [Parameter(Name = "Depreciation_Policy_Details_ID", DbType = "Int")] ref System.Nullable<int> depreciation_Policy_Details_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_ID, depreciation_Policy_Details_Period, depreciation_Policy_Details_Duration, depreciation_Policy_Details_Percentage, depreciation_Policy_Details_ID);
            depreciation_Policy_Details_ID = ((System.Nullable<int>)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertDepreciationPolicy")]
        public int InsertDepreciationPolicy([Parameter(Name = "Depreciation_Policy_Description", DbType = "VarChar(50)")] string depreciation_Policy_Description, [Parameter(Name = "Depreciation_Policy_Residual_Value", DbType = "Int")] System.Nullable<int> depreciation_Policy_Residual_Value, [Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] ref System.Nullable<int> depreciation_Policy_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_Description, depreciation_Policy_Residual_Value, depreciation_Policy_ID);
            depreciation_Policy_ID = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateDepreciationDetails")]
        public int UpdateDepreciationDetails([Parameter(Name = "Depreciation_Policy_Details_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_ID, [Parameter(Name = "Depreciation_Policy_Details_Duration", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_Duration, [Parameter(Name = "Depreciation_Policy_Details_Percentage", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_Percentage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_Details_ID, depreciation_Policy_Details_Duration, depreciation_Policy_Details_Percentage);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateDepreciationPolicy")]
        public int UpdateDepreciationPolicy([Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID, [Parameter(Name = "Depreciation_Policy_Description", DbType = "VarChar(50)")] string depreciation_Policy_Description, [Parameter(Name = "Depreciation_Policy_Residual_Value", DbType = "Real")] System.Nullable<float> depreciation_Policy_Residual_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_ID, depreciation_Policy_Description, depreciation_Policy_Residual_Value);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.fn_IsDepreciationPolicyExists", IsComposable = true)]
        public System.Nullable<bool> IsDepreciationPolicyExists([Parameter(Name = "Depreciation_Policy_Description", DbType = "VarChar(2000)")] string depreciation_Policy_Description)
        {
            return ((System.Nullable<bool>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_Description).ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepreciationPolicy")]
        public ISingleResult<rsp_GetDepreciationPolicyResult> GetDepreciationPolicy([Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_ID);
            return ((ISingleResult<rsp_GetDepreciationPolicyResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepreciationPolicyPercent")]
        public ISingleResult<rsp_GetDepreciationPolicyPercentResult> GetDepreciationPolicyPercent([Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID, [Parameter(Name = "Depreciation_Policy_Details_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_Details_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depreciation_Policy_ID, depreciation_Policy_Details_ID);
            return ((ISingleResult<rsp_GetDepreciationPolicyPercentResult>)(result.ReturnValue));
        }

        #endregion

        #region AddMachineType

        [Function(Name = "dbo.rsp_GetSiteIconDetails")]
        public ISingleResult<rsp_GetSiteIconDetailsResult> GetSiteIconDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteIconDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateMachineType")]
        public int UpdateMachineType([Parameter(Name = "Machine_Type_ID", DbType = "Int")] ref System.Nullable<int> machine_Type_ID, [Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID, [Parameter(Name = "Machine_Type_Code", DbType = "VarChar(50)")] string machine_Type_Code, [Parameter(Name = "Machine_Type_Description", DbType = "VarChar(50)")] string machine_Type_Description, [Parameter(Name = "Machine_Type_Icon_ref", DbType = "Int")] System.Nullable<int> machine_Type_Icon_ref, [Parameter(Name = "Machine_Type_Site_Icon", DbType = "VarChar(10)")] string machine_Type_Site_Icon, [Parameter(Name = "Machine_Type_Income_Ledger_Code", DbType = "VarChar(20)")] string machine_Type_Income_Ledger_Code, [Parameter(Name = "Machine_Type_AMEDIS_ID", DbType = "VarChar(50)")] string machine_Type_AMEDIS_ID, [Parameter(Name = "IsNonGamingAssetType", DbType = "Int")] System.Nullable<int> isNonGamingAssetType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Type_ID, depreciation_Policy_ID, machine_Type_Code, machine_Type_Description, machine_Type_Icon_ref, machine_Type_Site_Icon, machine_Type_Income_Ledger_Code, machine_Type_AMEDIS_ID, isNonGamingAssetType);
            machine_Type_ID = ((System.Nullable<int>)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }

        #endregion

        #region MachineModelAdminGaming

        [Function(Name = "dbo.rsp_GetManufacturerbyMCType")]
        public ISingleResult<rsp_GetManufacturerbyMCTypeResult> GetManufacturerbyMCType([Parameter(Name = "Machine_TypeID", DbType = "Int")] System.Nullable<int> machine_TypeID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_TypeID);
            return ((ISingleResult<rsp_GetManufacturerbyMCTypeResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetModelConfigurations")]
        public ISingleResult<rsp_GetModelConfigurationsResult> GetModelConfigurations([Parameter(Name = "MachineTypeID", DbType = "Int")] System.Nullable<int> machineTypeID, [Parameter(Name = "MachineName", DbType = "VarChar(100)")] string machineName, [Parameter(Name = "ManufacturerID", DbType = "Int")] System.Nullable<int> manufacturerID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineTypeID, machineName, manufacturerID);
            return ((ISingleResult<rsp_GetModelConfigurationsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.GetMachineNamesOnMachineType")]
        public ISingleResult<GetMachineNamesOnMachineTypeResult> GetMachineNamesOnMachineType([Parameter(DbType = "Int")] System.Nullable<int> mtype)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mtype);
            return ((ISingleResult<GetMachineNamesOnMachineTypeResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateModelConfigurations")]
        public int UpdateModelConfigurations([Parameter(Name = "MachineTypeID", DbType = "Int")] System.Nullable<int> machineTypeID, [Parameter(Name = "MachineName", DbType = "VarChar(50)")] string machineName, [Parameter(Name = "ManufacturerID", DbType = "Int")] System.Nullable<int> manufacturerID, [Parameter(Name = "RecreateCancelledCredits", DbType = "Bit")] System.Nullable<bool> recreateCancelledCredits, [Parameter(Name = "JackpotAddedToCancelledCredits", DbType = "Bit")] System.Nullable<bool> jackpotAddedToCancelledCredits, [Parameter(Name = "AddTrueCoinInToDrop", DbType = "Bit")] System.Nullable<bool> addTrueCoinInToDrop, [Parameter(Name = "UseCancelledCreditsAsTicketsPrinted", DbType = "Bit")] System.Nullable<bool> useCancelledCreditsAsTicketsPrinted, [Parameter(Name = "RecreateTicketsInsertedfromDrop", DbType = "Bit")] System.Nullable<bool> recreateTicketsInsertedfromDrop)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineTypeID, machineName, manufacturerID, recreateCancelledCredits, jackpotAddedToCancelledCredits, addTrueCoinInToDrop, useCancelledCreditsAsTicketsPrinted, recreateTicketsInsertedfromDrop);
            return ((int)(result.ReturnValue));
        }
        #endregion

        #region MachineModelAdminNonGaming(Add Category)
        [Function(Name = "dbo.rsp_GetMachineClassList")]
        public ISingleResult<rsp_GetMachineClassListResult> GetMachineClassList([Parameter(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Class_ID);
            return ((ISingleResult<rsp_GetMachineClassListResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_GetMachineClassID")]
        public int GetMachineClassID([Parameter(Name = "Machine_Class_ID", DbType = "Int")] ref System.Nullable<int> machine_Class_ID, [Parameter(Name = "IsDelete", DbType = "Bit")] System.Nullable<bool> isDelete)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Class_ID, isDelete);
            machine_Class_ID = ((System.Nullable<int>)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckAutoModelCodeExists")]
        public ISingleResult<rsp_CheckAutoModelCodeExistsResult> CheckAutoModelCodeExists()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_CheckAutoModelCodeExistsResult>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_CheckModelCodeExist")]
        public int CheckModelCodeExist([Parameter(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID, [Parameter(Name = "Machine_Class_Model_Code", DbType = "VarChar(50)")] string machine_Class_Model_Code, [Parameter(Name = "Machine_Name", DbType = "VarChar(50)")] string machine_Name, [Parameter(Name = "ModelCodeExist", DbType = "Bit")] ref System.Nullable<bool> modelCodeExist, [Parameter(Name = "MachineNameExist", DbType = "Bit")] ref System.Nullable<bool> machineNameExist)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Class_ID, machine_Class_Model_Code, machine_Name, modelCodeExist, machineNameExist);
            modelCodeExist = ((System.Nullable<bool>)(result.GetParameterValue(3)));
            machineNameExist = ((System.Nullable<bool>)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateMachineClassDetails")]
        public int UpdateMachineClassDetails([Parameter(Name = "Machine_Name", DbType = "VarChar(50)")] string machine_Name, [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID, [Parameter(Name = "Machine_Class_Model_Code", DbType = "VarChar(50)")] string machine_Class_Model_Code, [Parameter(Name = "Machine_Class_DeListed", DbType = "Bit")] System.Nullable<bool> machine_Class_DeListed, [Parameter(Name = "Machine_Class_Test_Machine", DbType = "Bit")] System.Nullable<bool> machine_Class_Test_Machine, [Parameter(Name = "Machine_Class_Category", DbType = "VarChar(50)")] string machine_Class_Category, [Parameter(Name = "Depreciation_Policy_ID", DbType = "Int")] System.Nullable<int> depreciation_Policy_ID, [Parameter(Name = "Depreciation_Policy_Use_Default", DbType = "Bit")] System.Nullable<bool> depreciation_Policy_Use_Default, [Parameter(Name = "Machine_Class_Release_Date", DbType = "VarChar(30)")] string machine_Class_Release_Date, [Parameter(Name = "Machine_Type_ID", DbType = "Int")] System.Nullable<int> machine_Type_ID, [Parameter(Name = "UserID", DbType = "Int")] System.Nullable<int> userID, [Parameter(Name = "Audit_Module_ID", DbType = "Int")] System.Nullable<int> audit_Module_ID, [Parameter(Name = "Audit_Screen_Name", DbType = "VarChar(50)")] string audit_Screen_Name, [Parameter(Name = "Machine_Class_ID", DbType = "Int")] ref System.Nullable<int> machine_Class_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_Name, manufacturer_ID, machine_Class_Model_Code, machine_Class_DeListed, machine_Class_Test_Machine, machine_Class_Category, depreciation_Policy_ID, depreciation_Policy_Use_Default, machine_Class_Release_Date, machine_Type_ID, userID, audit_Module_ID, audit_Screen_Name, machine_Class_ID);
            machine_Class_ID = ((System.Nullable<int>)(result.GetParameterValue(13)));
            return ((int)(result.ReturnValue));
        }
        #endregion

        #region SellMachine
        [Function(Name = "dbo.rsp_GetMachineAssetDetails")]
        public ISingleResult<rsp_GetMachineAssetDetailsResult> GetMachineAssetDetails([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID);
            return ((ISingleResult<rsp_GetMachineAssetDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateMachineAssetDetails")]
        public int UpdateMachineAssetDetails([Parameter(Name = "Machine_ID", DbType = "Int")] System.Nullable<int> machine_ID, [Parameter(Name = "Machine_End_Date", DbType = "DateTime")] System.Nullable<System.DateTime> machine_End_Date, [Parameter(Name = "Machine_Status_Flag", DbType = "Int")] System.Nullable<int> machine_Status_Flag, [Parameter(Name = "Machine_Sold_To", DbType = "VarChar(50)")] string machine_Sold_To, [Parameter(Name = "Machine_Type_Of_Sale", DbType = "VarChar(150)")] string machine_Type_Of_Sale, [Parameter(Name = "Machine_Sale_Price", DbType = "Money")] System.Nullable<decimal> machine_Sale_Price, [Parameter(Name = "Staff_ID_Deleted", DbType = "Int")] System.Nullable<int> staff_ID_Deleted, [Parameter(Name = "Machine_Date_Deleted", DbType = "DateTime")] System.Nullable<System.DateTime> machine_Date_Deleted, [Parameter(Name = "Machine_Sales_Invoice_Number", DbType = "VarChar(50)")] string machine_Sales_Invoice_Number)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machine_ID, machine_End_Date, machine_Status_Flag, machine_Sold_To, machine_Type_Of_Sale, machine_Sale_Price, staff_ID_Deleted, machine_Date_Deleted, machine_Sales_Invoice_Number);
            return ((int)(result.ReturnValue));
        }
        
        #endregion

        #region ModelType

        [Function(Name = "dbo.rsp_GetModelType")]
        public ISingleResult<rsp_GetModelTypeResult> GetModelType([Parameter(Name = "IsNGA", DbType = "Bit")] System.Nullable<bool> isNGA, [Parameter(Name = "MT_ID", DbType = "Int")] System.Nullable<int> mT_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), isNGA, mT_ID);
            return ((ISingleResult<rsp_GetModelTypeResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAssetNumberForTemplate")]
        public ISingleResult<rsp_GetAssetNumberForTemplateResult> rsp_GetAssetNumberForTemplate([Parameter(Name = "TemplateName", DbType = "VarChar(50)")] string templateName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), templateName);
            return ((ISingleResult<rsp_GetAssetNumberForTemplateResult>)(result.ReturnValue));
        }
        [Function(Name="dbo.usp_UpdateModelType")]
		public int UpdateModelType([Parameter(Name="MT_Model_Name", DbType="VarChar(20)")] string mT_Model_Name, [Parameter(Name="MT_Model_Desc", DbType="VarChar(50)")] string mT_Model_Desc, [Parameter(Name="IsNGA", DbType="Bit")] System.Nullable<bool> isNGA, [Parameter(Name="MT_ID", DbType="Int")] ref System.Nullable<int> mT_ID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), mT_Model_Name, mT_Model_Desc, isNGA, mT_ID);
			mT_ID = ((System.Nullable<int>)(result.GetParameterValue(3)));
			return ((int)(result.ReturnValue));
		}
        #endregion
    }


    public partial class rsp_GetMachineDetailsResult
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

        private string _MG_Game_Name;

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

        private int _PaytableFlag;

        private System.Nullable<int> _Game_Category_ID;

        private string _Game_Category_Name;

        private string _Orderby_Operator_Name;

        private System.Nullable<int> _Orderby_Operator_ID;

        private string _Orderby_Depot_Name;

        private System.Nullable<int> _Orderby_Depot_ID;

        private System.Nullable<int> _Orderby_Machine_Status_Flag;

        private string _Orderby_Staff_Last_Name;

        private string _Orderby_Staff_First_Name;

        private System.Nullable<int> _Orderby_Game_Category_ID;

        private System.Nullable<int> _Orderby_MG_Game_ID;

       

        public rsp_GetMachineDetailsResult()
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

        [Column(Storage = "_Site_ZonaRice", DbType = "VarChar(10)")]
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

        [Column(Storage = "_Machine_Transit_Site_Code", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Category_Code", DbType = "VarChar(50)")]
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

        [Column(Storage = "_PaytableFlag", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Orderby_Operator_Name", DbType = "VarChar(50)")]
        public string Orderby_Operator_Name
        {
            get
            {
                return this._Orderby_Operator_Name;
            }
            set
            {
                if ((this._Orderby_Operator_Name != value))
                {
                    this._Orderby_Operator_Name = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Operator_ID", DbType = "Int")]
        public System.Nullable<int> Orderby_Operator_ID
        {
            get
            {
                return this._Orderby_Operator_ID;
            }
            set
            {
                if ((this._Orderby_Operator_ID != value))
                {
                    this._Orderby_Operator_ID = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Depot_Name", DbType = "VarChar(50)")]
        public string Orderby_Depot_Name
        {
            get
            {
                return this._Orderby_Depot_Name;
            }
            set
            {
                if ((this._Orderby_Depot_Name != value))
                {
                    this._Orderby_Depot_Name = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Depot_ID", DbType = "Int")]
        public System.Nullable<int> Orderby_Depot_ID
        {
            get
            {
                return this._Orderby_Depot_ID;
            }
            set
            {
                if ((this._Orderby_Depot_ID != value))
                {
                    this._Orderby_Depot_ID = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Machine_Status_Flag", DbType = "Int")]
        public System.Nullable<int> Orderby_Machine_Status_Flag
        {
            get
            {
                return this._Orderby_Machine_Status_Flag;
            }
            set
            {
                if ((this._Orderby_Machine_Status_Flag != value))
                {
                    this._Orderby_Machine_Status_Flag = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Staff_Last_Name", DbType = "VarChar(50)")]
        public string Orderby_Staff_Last_Name
        {
            get
            {
                return this._Orderby_Staff_Last_Name;
            }
            set
            {
                if ((this._Orderby_Staff_Last_Name != value))
                {
                    this._Orderby_Staff_Last_Name = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Staff_First_Name", DbType = "VarChar(50)")]
        public string Orderby_Staff_First_Name
        {
            get
            {
                return this._Orderby_Staff_First_Name;
            }
            set
            {
                if ((this._Orderby_Staff_First_Name != value))
                {
                    this._Orderby_Staff_First_Name = value;
                }
            }
        }

        [Column(Storage = "_Orderby_Game_Category_ID", DbType = "Int")]
        public System.Nullable<int> Orderby_Game_Category_ID
        {
            get
            {
                return this._Orderby_Game_Category_ID;
            }
            set
            {
                if ((this._Orderby_Game_Category_ID != value))
                {
                    this._Orderby_Game_Category_ID = value;
                }
            }
        }

        [Column(Storage = "_Orderby_MG_Game_ID", DbType = "Int")]
        public System.Nullable<int> Orderby_MG_Game_ID
        {
            get
            {
                return this._Orderby_MG_Game_ID;
            }
            set
            {
                if ((this._Orderby_MG_Game_ID != value))
                {
                    this._Orderby_MG_Game_ID = value;
                }
            }
        }
    }



    public partial class rsp_GetSiteDetailsByStockResult
    {

        private string _Site_Name;

        private string _Site_Code;

        private string _Bar_Position_Name;

        private string _Site_ZonaRice;

        public rsp_GetSiteDetailsByStockResult()
        {
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

        [Column(Storage = "_Site_ZonaRice", DbType = "VarChar(10)")]
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

    public partial class rsp_GetPaytableDetailsForGameResult
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

        public rsp_GetPaytableDetailsForGameResult()
        {
        }

        [Column(Storage = "_AliasGameName", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Installation_No", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_Game_Name", DbType = "VarChar(100)")]
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

        [Column(Storage = "_PaytableID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_PaytableDescription", DbType = "VarChar(100)")]
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

    public partial class rsp_FetchGameCategoryResult
    {

        private int _Game_Category_ID;

        private string _Game_Category_Name;

        public rsp_FetchGameCategoryResult()
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



    //Load game Names

    public partial class rsp_GetGameNamesResult
    {

        private string _MG_Game_Name;

        private int _MG_Game_ID;

        public rsp_GetGameNamesResult()
        {
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
    }
    //Load game Names




    public partial class rsp_GetAssetTemplateDetailsResult
    {

        private int _AssetCrTempNumber;

        private string _TemplateName;

        public rsp_GetAssetTemplateDetailsResult()
        {
        }

        [Column(Storage = "_AssetCrTempNumber", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_TemplateName", DbType = "VarChar(50)")]
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
    public partial class rsp_CheckMachineInUseResult
    {

        private System.Nullable<int> _Machine_Status_Flag;

        public rsp_CheckMachineInUseResult()
        {
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
    }

    #region TerminationDataEntity

    public partial class rsp_GetMachineTerminationReasonResult
    {

        private int _MTRT_ID;

        private string _MTRT_Description;

        private int _MTRT_Display_Order;

        public rsp_GetMachineTerminationReasonResult()
        {
        }

        [Column(Storage = "_MTRT_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_MTRT_Description", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_MTRT_Display_Order", DbType = "Int NOT NULL")]
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

    public partial class rsp_GetTerminationMCDetailsResult
    {

        private string _Machine_Termination_Comments;

        private string _Machine_Termination_Username;

        private System.Nullable<int> _Machine_Termination_Reason;

        private string _Machine_End_Date;

        public rsp_GetTerminationMCDetailsResult()
        {
        }

        [Column(Storage = "_Machine_Termination_Comments", DbType = "VarChar(250)")]
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

        [Column(Storage = "_Machine_Termination_Username", DbType = "VarChar(100)")]
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

        [Column(Storage = "_Machine_Termination_Reason", DbType = "Int")]
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
    }

    #endregion

    #region DepreciationEntity

    public partial class rsp_GetDepreciationPolicyResult
    {

        private int _Depreciation_Policy_ID;

        private string _Depreciation_Policy_Description;

        private System.Nullable<float> _Depreciation_Policy_Residual_Value;

        public rsp_GetDepreciationPolicyResult()
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
    }

    public partial class rsp_GetDepreciationPolicyPercentResult
    {

        private System.Nullable<int> _TotalDrop;

        public rsp_GetDepreciationPolicyPercentResult()
        {
        }

        [Column(Storage = "_TotalDrop", DbType = "Int")]
        public System.Nullable<int> TotalDrop
        {
            get
            {
                return this._TotalDrop;
            }
            set
            {
                if ((this._TotalDrop != value))
                {
                    this._TotalDrop = value;
                }
            }
        }
    }
    #endregion

    #region AddMachineTypeEntity
    public partial class rsp_GetSiteIconDetailsResult
    {

        private int _SiteIconID;

        private string _Machine_Type_Site_Icon;

        private string _SiteIconPath;

        public rsp_GetSiteIconDetailsResult()
        {
        }

        [Column(Storage = "_SiteIconID", DbType = "Int NOT NULL")]
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
    #endregion

    #region MachineModelAdminGaming
    public partial class rsp_GetManufacturerbyMCTypeResult
    {

        private int _Manufacturer_ID;

        private string _Manufacturer_Name;

        public rsp_GetManufacturerbyMCTypeResult()
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
    }
    public partial class rsp_GetAssetNumberForTemplateResult
    {

        private string _AssetNumber;

        public rsp_GetAssetNumberForTemplateResult()
        {
        }

        [Column(Storage = "_AssetNumber", DbType = "VarChar(50)")]
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
    public partial class rsp_GetModelConfigurationsResult
    {

        private System.Nullable<bool> _Machine_Class_RecreateCancelledCredits;

        private System.Nullable<bool> _Machine_Class_JackpotAddedToCancelledCredits;

        private System.Nullable<bool> _Machine_Class_AddTrueCoinInToDrop;

        private System.Nullable<bool> _Machine_Class_UseCancelledCreditsAsTicketsPrinted;

        private System.Nullable<bool> _Machine_Class_RecreateTicketsInsertedfromDrop;

        public rsp_GetModelConfigurationsResult()
        {
        }

        [Column(Storage = "_Machine_Class_RecreateCancelledCredits", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Class_JackpotAddedToCancelledCredits", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Class_AddTrueCoinInToDrop", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Class_UseCancelledCreditsAsTicketsPrinted", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Class_RecreateTicketsInsertedfromDrop", DbType = "Bit")]
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
    }
    #endregion

    #region MachineModelAdminNonGaming(Add Category)
    public partial class rsp_GetMachineClassListResult
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

        public rsp_GetMachineClassListResult()
        {
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

        [Column(Storage = "_Machine_Class_DeListed", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Class_Test_Machine", DbType = "Bit")]
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

        [Column(Storage = "_Machine_Class_Category", DbType = "VarChar(50)")]
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

        [Column(Storage = "_Machine_Class_Release_Date", DbType = "VarChar(30)")]
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

        [Column(Storage = "_Machine_Type_ID", DbType = "Int")]
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

    public partial class rsp_CheckAutoModelCodeExistsResult
    {

        private System.Nullable<bool> _System_Parameter_Auto_Generate_Model_Codes;

        public rsp_CheckAutoModelCodeExistsResult()
        {
        }

        [Column(Storage = "_System_Parameter_Auto_Generate_Model_Codes", DbType = "Bit")]
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
    public partial class rsp_GetMachineAssetDetailsResult
    {

        private int _Machine_ID;

        private string _Machine_Stock_No;

        private string _Machine_MAC_Address;

        private string _Machine_Class_Model_Code;

        private string _Machine_Name;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Alternative_Serial_Numbers;

        public rsp_GetMachineAssetDetailsResult()
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
    }
    #endregion

    #region ModelTypeEntity
    public partial class rsp_GetModelTypeResult
    {

        private int _MT_ID;

        private string _MT_Model_Name;

        private string _MT_Model_Desc;

        private bool _MT_IsNGA;

        public rsp_GetModelTypeResult()
        {
        }

        [Column(Storage = "_MT_ID", DbType = "Int NOT NULL")]
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

        [Column(Storage = "_MT_Model_Name", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
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

        [Column(Storage = "_MT_Model_Desc", DbType = "VarChar(50)")]
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

        [Column(Storage = "_MT_IsNGA", DbType = "Bit NOT NULL")]
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
    #endregion
}
