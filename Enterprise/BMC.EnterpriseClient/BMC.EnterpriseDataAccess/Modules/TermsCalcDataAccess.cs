using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCashTakeForSgviTerms")]
        public ISingleResult<CashTakeResult> GetCashTake([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CollectionId", DbType = "Int")] System.Nullable<int> collectionId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collectionId);
            return ((ISingleResult<CashTakeResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetTermsInfoForInstallation")]
        public ISingleResult<InstallationTermsResult> GetTermsInfoForInstallation([global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> installation_id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType = "Int")] System.Nullable<int> period)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_id, period);
            return ((ISingleResult<InstallationTermsResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetRentInfoForTermsCalculation")]
        public ISingleResult<RentResult> GetRentInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "MachineClassID", DbType = "Int")] System.Nullable<int> machineClassID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RentScheduleID", DbType = "Int")] System.Nullable<int> rentScheduleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineClassID, rentScheduleID);
            return ((ISingleResult<RentResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetAccessoryInstallationInfoForTermsCalculation")]
        public ISingleResult<AccessoryInstallationResult> GetAccessoryInstallationInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Bar_Position_ID", DbType = "Int")] System.Nullable<int> bar_Position_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bar_Position_ID);
            return ((ISingleResult<AccessoryInstallationResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetMachineClassIDForTermsCalculation")]
        public ISingleResult<MachineClassResult> GetMachineClassIDForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID);
            return ((ISingleResult<MachineClassResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetShareBandInfoForTermsCalculation")]
        public ISingleResult<ShareBandResult> GetShareBandInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Band_ID", DbType = "Int")] System.Nullable<int> share_Band_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Band_ID);
            return ((ISingleResult<ShareBandResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetShareScheduleInfoForTermsCalculation")]
        public ISingleResult<ShareScheduleResult> GetShareScheduleInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Share_Schedule_ID", DbType = "Int")] System.Nullable<int> share_Schedule_ID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Machine_Class_ID", DbType = "Int")] System.Nullable<int> machine_Class_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), share_Schedule_ID, machine_Class_ID);
            return ((ISingleResult<ShareScheduleResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetBarPositionInfoForTermsCalculation")]
        public ISingleResult<BarPositionResult> GetBarPositionInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_ID", DbType = "Int")] System.Nullable<int> installation_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_ID);
            return ((ISingleResult<BarPositionResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetCollectionInfoForTermsCalculation")]
        public ISingleResult<CollectionInfoResult> GetCollectionInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CollectionID", DbType = "Int")] System.Nullable<int> collectionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collectionID);
            return ((ISingleResult<CollectionInfoResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetShareScheduleInfo")]
        public ISingleResult<ShareScheduleInfoResult> GetShareScheduleInfoForTermsCalculation()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ShareScheduleInfoResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetRentScheduleInfo")]
        public ISingleResult<RentScheduleInfoResult> GetRentScheduleForTermsCalculation()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<RentScheduleInfoResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateCollectionDetailsWithTermsResults")]
        public int UpdateCollectionDetailsWithTermsResults(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CollectionID", DbType = "Int")] System.Nullable<int> collectionID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Gross", DbType = "Real")] System.Nullable<float> collection_Gross,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Days", DbType = "Real")] System.Nullable<float> collection_Days,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Prize_Value", DbType = "Real")] System.Nullable<float> collection_Prize_Value,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Unpaid_Shortfall", DbType = "Real")] System.Nullable<float> collection_Unpaid_Shortfall,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_Share", DbType = "Real")] System.Nullable<float> collection_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_Share", DbType = "Real")] System.Nullable<float> collection_Supplier_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Location_Share", DbType = "Real")] System.Nullable<float> collection_Location_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_AMLD", DbType = "Real")] System.Nullable<float> collection_AMLD,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Output", DbType = "Real")] System.Nullable<float> collection_VAT_Output,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Company", DbType = "Real")] System.Nullable<float> collection_VAT_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Supplier", DbType = "Real")] System.Nullable<float> collection_VAT_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Location", DbType = "Real")] System.Nullable<float> collection_VAT_Location,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_LOS", DbType = "Real")] System.Nullable<float> collection_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_VAT_LOS", DbType = "Real")] System.Nullable<float> collection_Supplier_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_VAT_LOS", DbType = "Real")] System.Nullable<float> collection_Company_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Sec_Brewery_VAT_LOS", DbType = "Real")] System.Nullable<float> collection_Sec_Brewery_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Location_VAT_LOS", DbType = "Real")] System.Nullable<float> collection_Location_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Supplier_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Company_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Sec_Brewery_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Sec_Brewery_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Location_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Location_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Supplier_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Company_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Sec_Brewery_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Sec_Brewery_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Location_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Location_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_Share_LOS", DbType = "Real")] System.Nullable<float> collection_Company_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_Share_LOS", DbType = "Real")] System.Nullable<float> collection_Supplier_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Location_Share_LOS", DbType = "Real")] System.Nullable<float> collection_Location_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_VAT_Sec_Brewery", DbType = "Real")] System.Nullable<float> collection_VAT_Sec_Brewery,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Sec_Brewery_Share_LOS", DbType = "Real")] System.Nullable<float> collection_Sec_Brewery_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Company_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Company_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Company_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Sec_Brewery_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Sec_Brewery_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Sec_Brewery_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Sec_Brewery_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Supplier_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Supplier_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Supplier_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Site_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Site_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Site_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Site_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Licence_Banked_To_Company", DbType = "Real")] System.Nullable<float> collection_Licence_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Licence_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> collection_Licence_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Licence_LOS", DbType = "Real")] System.Nullable<float> collection_Licence_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_LOS", DbType = "Real")] System.Nullable<float> collection_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SiteGroupShareLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SiteGroupShareLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SiteGroupShareBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SiteGroupShareBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SecSiteGroupShareLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SecSiteGroupShareLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SecSiteGroupShareLOS_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SecSiteGroupShareLOS_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SecSiteGroupShareBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SecSiteGroupShareBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SecSiteGroupShareBankedToSupplier_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SecSiteGroupShareBankedToSupplier_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SecSiteGroupShareBankedToGroup_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SecSiteGroupShareBankedToGroup_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SupplierShareLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_SupplierShareLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SupplierShareBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_SupplierShareBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SiteShareBankedToSupplier_toSite", DbType = "Real")] System.Nullable<float> collection_Deferred_SiteShareBankedToSupplier_toSite,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SiteShareBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_SiteShareBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_SiteShareBankedToGroup_toSite", DbType = "Real")] System.Nullable<float> collection_Deferred_SiteShareBankedToGroup_toSite,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputLos_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputLos_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputLos_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputLos_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputBankedToSupplier_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputBankedToSupplier_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputBankedToSupplier_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputBankedToSupplier_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATOutputBankedToGroup_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATOutputBankedToGroup_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierLos_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierLos_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierLos_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierLos_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierBankedToSupplier_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierBankedToSupplier_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierBankedToSupplier_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierBankedToSupplier_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSupplierBankedToGroup_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSupplierBankedToGroup_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteLos_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteLos_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteLos_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteLos_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteLos_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteLos_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteBankedToSupplier_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteBankedToSupplier_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteBankedToSupplier_toSite", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteBankedToSupplier_toSite,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSiteBankedToGroup_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSiteBankedToGroup_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATGroupLos_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATGroupLos_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATGroupLos_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATGroupLos_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATGroupBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATGroupBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATGroupBankedToSupplier_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATGroupBankedToSupplier_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATGroupBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATGroupBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATGroupBankedToGroup_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATGroupBankedToGroup_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupLos_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupLos_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupLos_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupLos_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupLos_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupLos_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupBankedToSupplier_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupBankedToSupplier_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupBankedToSupplier_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupBankedToSupplier_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupBankedToGroup_toSecGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupBankedToGroup_toSecGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_VATSecGroupBankedToGroup_toCustoms", DbType = "Real")] System.Nullable<float> collection_Deferred_VATSecGroupBankedToGroup_toCustoms,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_LicenceChargeLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_LicenceChargeLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_LicenceChargeLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_LicenceChargeLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_LicenceChargeBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_LicenceChargeBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_LicenceChargeBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_LicenceChargeBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_PrizeChargeLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_PrizeChargeLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_PrizeChargeLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_PrizeChargeLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_PrizeChargeBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_PrizeChargeBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_PrizeChargeBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_PrizeChargeBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_ConsultancyChargeLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_ConsultancyChargeLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_ConsultancyChargeLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_ConsultancyChargeLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_ConsultancyChargeBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_ConsultancyChargeBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_ConsultancyChargeBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_ConsultancyChargeBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_RoyaltyChargeLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_RoyaltyChargeLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_RoyaltyChargeLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_RoyaltyChargeLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_RoyaltyChargeBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_RoyaltyChargeBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_RoyaltyChargeBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_RoyaltyChargeBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other1ChargeLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_Other1ChargeLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other1ChargeLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_Other1ChargeLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other1ChargeBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_Other1ChargeBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other1ChargeBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_Other1ChargeBankedToGroup_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other2ChargeLOS_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_Other2ChargeLOS_toSupplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other2ChargeLOS_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_Other2ChargeLOS_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other2ChargeBankedToSupplier_toGroup", DbType = "Real")] System.Nullable<float> collection_Deferred_Other2ChargeBankedToSupplier_toGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Collection_Deferred_Other2ChargeBankedToGroup_toSupplier", DbType = "Real")] System.Nullable<float> collection_Deferred_Other2ChargeBankedToGroup_toSupplier)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collectionID, collection_Gross, collection_Days, collection_Prize_Value, collection_Unpaid_Shortfall, collection_Company_Share, collection_Supplier_Share, collection_Location_Share, collection_AMLD, collection_VAT_Output, collection_VAT_Company, collection_VAT_Supplier, collection_VAT_Location, collection_VAT_LOS, collection_VAT_Banked_To_Company, collection_VAT_Banked_To_Supplier, collection_Supplier_VAT_LOS, collection_Company_VAT_LOS, collection_Sec_Brewery_VAT_LOS, collection_Location_VAT_LOS, collection_Supplier_VAT_Banked_To_Company, collection_Company_VAT_Banked_To_Company, collection_Sec_Brewery_VAT_Banked_To_Company, collection_Location_VAT_Banked_To_Company, collection_Supplier_VAT_Banked_To_Supplier, collection_Company_VAT_Banked_To_Supplier, collection_Sec_Brewery_VAT_Banked_To_Supplier, collection_Location_VAT_Banked_To_Supplier, collection_Company_Share_LOS, collection_Supplier_Share_LOS, collection_Location_Share_LOS, collection_Banked_To_Company, collection_Banked_To_Supplier, collection_VAT_Sec_Brewery, collection_Sec_Brewery_Share_LOS, collection_Company_Share_Banked_To_Company, collection_Company_Share_Banked_To_Supplier, collection_Sec_Brewery_Share_Banked_To_Company, collection_Sec_Brewery_Share_Banked_To_Supplier, collection_Supplier_Share_Banked_To_Company, collection_Supplier_Share_Banked_To_Supplier, collection_Site_Share_Banked_To_Company, collection_Site_Share_Banked_To_Supplier, collection_Licence_Banked_To_Company, collection_Licence_Banked_To_Supplier, collection_Licence_LOS, collection_LOS, collection_Deferred_SiteGroupShareLOS_toGroup, collection_Deferred_SiteGroupShareBankedToSupplier_toGroup, collection_Deferred_SecSiteGroupShareLOS_toGroup, collection_Deferred_SecSiteGroupShareLOS_toSecGroup, collection_Deferred_SecSiteGroupShareBankedToSupplier_toGroup, collection_Deferred_SecSiteGroupShareBankedToSupplier_toSecGroup, collection_Deferred_SecSiteGroupShareBankedToGroup_toSecGroup, collection_Deferred_SupplierShareLOS_toSupplier, collection_Deferred_SupplierShareBankedToGroup_toSupplier, collection_Deferred_SiteShareBankedToSupplier_toSite, collection_Deferred_SiteShareBankedToSupplier_toGroup, collection_Deferred_SiteShareBankedToGroup_toSite, collection_Deferred_VATOutputLos_toSupplier, collection_Deferred_VATOutputLos_toGroup, collection_Deferred_VATOutputBankedToSupplier_toGroup, collection_Deferred_VATOutputBankedToSupplier_toSecGroup, collection_Deferred_VATOutputBankedToSupplier_toCustoms, collection_Deferred_VATOutputBankedToGroup_toSupplier, collection_Deferred_VATOutputBankedToGroup_toCustoms, collection_Deferred_VATSupplierLos_toSupplier, collection_Deferred_VATSupplierLos_toGroup, collection_Deferred_VATSupplierBankedToSupplier_toGroup, collection_Deferred_VATSupplierBankedToSupplier_toSecGroup, collection_Deferred_VATSupplierBankedToSupplier_toCustoms, collection_Deferred_VATSupplierBankedToGroup_toSupplier, collection_Deferred_VATSupplierBankedToGroup_toCustoms, collection_Deferred_VATSiteLos_toSupplier, collection_Deferred_VATSiteLos_toGroup, collection_Deferred_VATSiteLos_toCustoms, collection_Deferred_VATSiteBankedToSupplier_toGroup, collection_Deferred_VATSiteBankedToSupplier_toCustoms, collection_Deferred_VATSiteBankedToSupplier_toSite, collection_Deferred_VATSiteBankedToGroup_toSupplier, collection_Deferred_VATSiteBankedToGroup_toCustoms, collection_Deferred_VATGroupLos_toSupplier, collection_Deferred_VATGroupLos_toGroup, collection_Deferred_VATGroupBankedToSupplier_toGroup, collection_Deferred_VATGroupBankedToSupplier_toCustoms, collection_Deferred_VATGroupBankedToGroup_toSupplier, collection_Deferred_VATGroupBankedToGroup_toCustoms, collection_Deferred_VATSecGroupLos_toSupplier, collection_Deferred_VATSecGroupLos_toGroup, collection_Deferred_VATSecGroupLos_toSecGroup, collection_Deferred_VATSecGroupBankedToSupplier_toGroup, collection_Deferred_VATSecGroupBankedToSupplier_toSecGroup, collection_Deferred_VATSecGroupBankedToSupplier_toCustoms, collection_Deferred_VATSecGroupBankedToGroup_toSupplier, collection_Deferred_VATSecGroupBankedToGroup_toSecGroup, collection_Deferred_VATSecGroupBankedToGroup_toCustoms, collection_Deferred_LicenceChargeLOS_toSupplier, collection_Deferred_LicenceChargeLOS_toGroup, collection_Deferred_LicenceChargeBankedToSupplier_toGroup, collection_Deferred_LicenceChargeBankedToGroup_toSupplier, collection_Deferred_PrizeChargeLOS_toSupplier, collection_Deferred_PrizeChargeLOS_toGroup, collection_Deferred_PrizeChargeBankedToSupplier_toGroup, collection_Deferred_PrizeChargeBankedToGroup_toSupplier, collection_Deferred_ConsultancyChargeLOS_toSupplier, collection_Deferred_ConsultancyChargeLOS_toGroup, collection_Deferred_ConsultancyChargeBankedToSupplier_toGroup, collection_Deferred_ConsultancyChargeBankedToGroup_toSupplier, collection_Deferred_RoyaltyChargeLOS_toSupplier, collection_Deferred_RoyaltyChargeLOS_toGroup, collection_Deferred_RoyaltyChargeBankedToSupplier_toGroup, collection_Deferred_RoyaltyChargeBankedToGroup_toSupplier, collection_Deferred_Other1ChargeLOS_toSupplier, collection_Deferred_Other1ChargeLOS_toGroup, collection_Deferred_Other1ChargeBankedToSupplier_toGroup, collection_Deferred_Other1ChargeBankedToGroup_toSupplier, collection_Deferred_Other2ChargeLOS_toSupplier, collection_Deferred_Other2ChargeLOS_toGroup, collection_Deferred_Other2ChargeBankedToSupplier_toGroup, collection_Deferred_Other2ChargeBankedToGroup_toSupplier);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateCollectionTermsWithTermsResults")]
        public int UpdateCollectionTermsWithTermsResults(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "CollectionID", DbType = "Int")] System.Nullable<int> collectionID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_Share", DbType = "Real")] System.Nullable<float> hQTerms_Company_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_Share", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Location_Share", DbType = "Real")] System.Nullable<float> hQTerms_Location_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_AMLD", DbType = "Real")] System.Nullable<float> hQTerms_AMLD,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Output", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Output,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Company", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Location", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Location,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_LOS", DbType = "Real")] System.Nullable<float> hQTerms_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_VAT_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_VAT_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Company_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Sec_Brewery_VAT_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Sec_Brewery_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Location_VAT_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Location_VAT_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Company_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Sec_Brewery_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Sec_Brewery_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Location_VAT_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Location_VAT_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Company_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Sec_Brewery_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Sec_Brewery_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Location_VAT_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Location_VAT_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_Share_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Company_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_Share_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Location_Share_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Location_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_VAT_Sec_Brewery", DbType = "Real")] System.Nullable<float> hQTerms_VAT_Sec_Brewery,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Sec_Brewery_Share_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Sec_Brewery_Share_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Company_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Company_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Company_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Sec_Brewery_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Sec_Brewery_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Sec_Brewery_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Sec_Brewery_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Supplier_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Supplier_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Site_Share_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Site_Share_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Site_Share_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Site_Share_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Licence_Banked_To_Company", DbType = "Real")] System.Nullable<float> hQTerms_Licence_Banked_To_Company,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Licence_Banked_To_Supplier", DbType = "Real")] System.Nullable<float> hQTerms_Licence_Banked_To_Supplier,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_Licence_LOS", DbType = "Real")] System.Nullable<float> hQTerms_Licence_LOS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HQTerms_LOS", DbType = "Real")] System.Nullable<float> hQTerms_LOS)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collectionID, hQTerms_Company_Share, hQTerms_Supplier_Share, hQTerms_Location_Share, hQTerms_AMLD, hQTerms_VAT_Output, hQTerms_VAT_Company, hQTerms_VAT_Supplier, hQTerms_VAT_Location, hQTerms_VAT_LOS, hQTerms_VAT_Banked_To_Company, hQTerms_VAT_Banked_To_Supplier, hQTerms_Supplier_VAT_LOS, hQTerms_Company_VAT_LOS, hQTerms_Sec_Brewery_VAT_LOS, hQTerms_Location_VAT_LOS, hQTerms_Supplier_VAT_Banked_To_Company, hQTerms_Company_VAT_Banked_To_Company, hQTerms_Sec_Brewery_VAT_Banked_To_Company, hQTerms_Location_VAT_Banked_To_Company, hQTerms_Supplier_VAT_Banked_To_Supplier, hQTerms_Company_VAT_Banked_To_Supplier, hQTerms_Sec_Brewery_VAT_Banked_To_Supplier, hQTerms_Location_VAT_Banked_To_Supplier, hQTerms_Company_Share_LOS, hQTerms_Supplier_Share_LOS, hQTerms_Location_Share_LOS, hQTerms_Banked_To_Company, hQTerms_Banked_To_Supplier, hQTerms_VAT_Sec_Brewery, hQTerms_Sec_Brewery_Share_LOS, hQTerms_Company_Share_Banked_To_Company, hQTerms_Company_Share_Banked_To_Supplier, hQTerms_Sec_Brewery_Share_Banked_To_Company, hQTerms_Sec_Brewery_Share_Banked_To_Supplier, hQTerms_Supplier_Share_Banked_To_Company, hQTerms_Supplier_Share_Banked_To_Supplier, hQTerms_Site_Share_Banked_To_Company, hQTerms_Site_Share_Banked_To_Supplier, hQTerms_Licence_Banked_To_Company, hQTerms_Licence_Banked_To_Supplier, hQTerms_Licence_LOS, hQTerms_LOS);
            return ((int)(result.ReturnValue));
        }
    }
    
    public partial class CashTakeResult
    {

        private System.Nullable<double> _Cash_Take;

        public CashTakeResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cash_Take", DbType = "Float")]
        public System.Nullable<double> Cash_Take
        {
            get
            {
                return this._Cash_Take;
            }
            set
            {
                if ((this._Cash_Take != value))
                {
                    this._Cash_Take = value;
                }
            }
        }
    }

    public partial class InstallationTermsResult
    {

        private System.Nullable<int> _Machine_ID;

        private string _Installation_Start_Date;

        private System.Nullable<int> _Machine_Class_ID;

        private int _Bar_Position_ID;

        private string _Bar_Position_Last_Collection_Date;

        private System.Nullable<bool> _Bar_Position_Override_Rent;

        private System.Nullable<bool> _Bar_Position_Override_Shares;

        private System.Nullable<bool> _Bar_Position_Override_Licence;

        private System.Nullable<float> _Bar_Position_Rent;

        private System.Nullable<float> _Bar_Position_Rent_Previous;

        private System.Nullable<float> _Bar_Position_Rent_Future;

        private string _Bar_Position_Rent_Past_Date;

        private string _Bar_Position_Rent_Future_Date;

        private System.Nullable<float> _Bar_Position_Supplier_Share;

        private System.Nullable<float> _Bar_Position_Site_Share;

        private System.Nullable<float> _Bar_Position_Owners_Share;

        private System.Nullable<float> _Bar_Position_Secondary_Owners_Share;

        private System.Nullable<float> _Bar_Position_Supplier_Share_Previous;

        private System.Nullable<float> _Bar_Position_Site_Share_Previous;

        private System.Nullable<float> _Bar_Position_Owners_Share_Previous;

        private System.Nullable<float> _Bar_Position_Secondary_Owners_Share_Previous;

        private System.Nullable<float> _Bar_Position_Supplier_Share_Future;

        private System.Nullable<float> _Bar_Position_Site_Share_Future;

        private System.Nullable<float> _Bar_Position_Owners_Share_Future;

        private System.Nullable<float> _Bar_Position_Secondary_Owners_Share_Future;

        private string _Bar_Position_Share_Past_Date;

        private string _Bar_Position_Share_Future_Date;

        private System.Nullable<float> _Bar_Position_Licence_Charge;

        private System.Nullable<float> _Bar_Position_Licence_Previous;

        private System.Nullable<float> _Bar_Position_Licence_Future;

        private string _Bar_Position_Licence_Past_Date;

        private string _Bar_Position_Licence_Future_Date;

        private string _Bar_Position_Collection_Rent_Paid_Until;

        private System.Nullable<int> _Bar_Position_Collection_Period;

        private System.Nullable<bool> _Bar_Position_Prize_LOS;

        private int _Terms_Profile_ID;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<int> _Machine_Type_ID;

        private string _Terms_Profile_Name;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Supplier_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Supplier_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Supplier_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Supplier_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Supplier_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Share_Schedule;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Rent_Schedule;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Site_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Site_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Site_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Site_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Site_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Group_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Group_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Group_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Group_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Group_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Sec_Group_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Sec_Group_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Sec_Group_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Sec_Group_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Sec_Group_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_VAT_Output_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Output_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Output_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Output_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Supplier_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Guarantor;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Site_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Guarantor;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Group_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Sec_Group_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Licence_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Licence_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Licence_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Prize_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Prize_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Prize_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Consultancy_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Consultancy_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Consultancy_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Royalty_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Royalty_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Royalty_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Index;

        private string _Terms_Profile_Other_Other1_Name;

        private System.Nullable<bool> _Terms_Profile_Other_Other1_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Other1_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Other1_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Index;

        private string _Terms_Profile_Other_Other2_Name;

        private System.Nullable<bool> _Terms_Profile_Other_Other2_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Other2_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Other2_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Frequency;

        private System.Nullable<bool> _Terms_Profile_London_Rent_Use;

        private System.Nullable<float> _Terms_Profile_London_Rent_Charge;

        private int _Terms_Profile_GPT_Index;

        private int _Terms_Profile_GPT_Use;

        private int _Terms_Profile_GPT_Cash_Destination;

        private int _Terms_Profile_GPT_Deferred_Remittance;

        private System.Nullable<bool> _Sub_Company_Use_Split_Rents;

        private System.Nullable<bool> _Bar_Position_Use_Terms;

        private System.Nullable<bool> _Bar_Position_Override_Rent_Schedule;

        private System.Nullable<bool> _Bar_Position_Override_Share_Schedule;

        private System.Nullable<int> _Bar_Position_Rent_Schedule_ID;

        private System.Nullable<int> _Bar_Position_Share_Schedule_ID;

        private string _Terms_Group_Name;

        private System.Nullable<bool> _Bar_Position_Override_Rent_From_Schedule_To_Rent;

        private System.Nullable<bool> _Bar_Position_Override_Rent_From_Rent_To_Schedule;

        private string _Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

        private string _Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

        private System.Nullable<int> _Bar_Position_Rent_Schedule_ID_From;

        public InstallationTermsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_Start_Date", DbType = "VarChar(30)")]
        public string Installation_Start_Date
        {
            get
            {
                return this._Installation_Start_Date;
            }
            set
            {
                if ((this._Installation_Start_Date != value))
                {
                    this._Installation_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
        public int Bar_Position_ID
        {
            get
            {
                return this._Bar_Position_ID;
            }
            set
            {
                if ((this._Bar_Position_ID != value))
                {
                    this._Bar_Position_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Last_Collection_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Last_Collection_Date
        {
            get
            {
                return this._Bar_Position_Last_Collection_Date;
            }
            set
            {
                if ((this._Bar_Position_Last_Collection_Date != value))
                {
                    this._Bar_Position_Last_Collection_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Rent", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent
        {
            get
            {
                return this._Bar_Position_Override_Rent;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent != value))
                {
                    this._Bar_Position_Override_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Shares", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Shares
        {
            get
            {
                return this._Bar_Position_Override_Shares;
            }
            set
            {
                if ((this._Bar_Position_Override_Shares != value))
                {
                    this._Bar_Position_Override_Shares = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Licence", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Licence
        {
            get
            {
                return this._Bar_Position_Override_Licence;
            }
            set
            {
                if ((this._Bar_Position_Override_Licence != value))
                {
                    this._Bar_Position_Override_Licence = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Rent
        {
            get
            {
                return this._Bar_Position_Rent;
            }
            set
            {
                if ((this._Bar_Position_Rent != value))
                {
                    this._Bar_Position_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Rent_Previous
        {
            get
            {
                return this._Bar_Position_Rent_Previous;
            }
            set
            {
                if ((this._Bar_Position_Rent_Previous != value))
                {
                    this._Bar_Position_Rent_Previous = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Rent_Future
        {
            get
            {
                return this._Bar_Position_Rent_Future;
            }
            set
            {
                if ((this._Bar_Position_Rent_Future != value))
                {
                    this._Bar_Position_Rent_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent_Past_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Rent_Past_Date
        {
            get
            {
                return this._Bar_Position_Rent_Past_Date;
            }
            set
            {
                if ((this._Bar_Position_Rent_Past_Date != value))
                {
                    this._Bar_Position_Rent_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent_Future_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Rent_Future_Date
        {
            get
            {
                return this._Bar_Position_Rent_Future_Date;
            }
            set
            {
                if ((this._Bar_Position_Rent_Future_Date != value))
                {
                    this._Bar_Position_Rent_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Supplier_Share
        {
            get
            {
                return this._Bar_Position_Supplier_Share;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Share != value))
                {
                    this._Bar_Position_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Site_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Site_Share
        {
            get
            {
                return this._Bar_Position_Site_Share;
            }
            set
            {
                if ((this._Bar_Position_Site_Share != value))
                {
                    this._Bar_Position_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Owners_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Owners_Share
        {
            get
            {
                return this._Bar_Position_Owners_Share;
            }
            set
            {
                if ((this._Bar_Position_Owners_Share != value))
                {
                    this._Bar_Position_Owners_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Secondary_Owners_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share
        {
            get
            {
                return this._Bar_Position_Secondary_Owners_Share;
            }
            set
            {
                if ((this._Bar_Position_Secondary_Owners_Share != value))
                {
                    this._Bar_Position_Secondary_Owners_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Supplier_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Supplier_Share_Previous
        {
            get
            {
                return this._Bar_Position_Supplier_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Share_Previous != value))
                {
                    this._Bar_Position_Supplier_Share_Previous = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Site_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Site_Share_Previous
        {
            get
            {
                return this._Bar_Position_Site_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Site_Share_Previous != value))
                {
                    this._Bar_Position_Site_Share_Previous = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Owners_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Owners_Share_Previous
        {
            get
            {
                return this._Bar_Position_Owners_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Owners_Share_Previous != value))
                {
                    this._Bar_Position_Owners_Share_Previous = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Secondary_Owners_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Previous
        {
            get
            {
                return this._Bar_Position_Secondary_Owners_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Secondary_Owners_Share_Previous != value))
                {
                    this._Bar_Position_Secondary_Owners_Share_Previous = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Supplier_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Supplier_Share_Future
        {
            get
            {
                return this._Bar_Position_Supplier_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Share_Future != value))
                {
                    this._Bar_Position_Supplier_Share_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Site_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Site_Share_Future
        {
            get
            {
                return this._Bar_Position_Site_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Site_Share_Future != value))
                {
                    this._Bar_Position_Site_Share_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Owners_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Owners_Share_Future
        {
            get
            {
                return this._Bar_Position_Owners_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Owners_Share_Future != value))
                {
                    this._Bar_Position_Owners_Share_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Secondary_Owners_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Future
        {
            get
            {
                return this._Bar_Position_Secondary_Owners_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Secondary_Owners_Share_Future != value))
                {
                    this._Bar_Position_Secondary_Owners_Share_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Share_Past_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Share_Past_Date
        {
            get
            {
                return this._Bar_Position_Share_Past_Date;
            }
            set
            {
                if ((this._Bar_Position_Share_Past_Date != value))
                {
                    this._Bar_Position_Share_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Share_Future_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Share_Future_Date
        {
            get
            {
                return this._Bar_Position_Share_Future_Date;
            }
            set
            {
                if ((this._Bar_Position_Share_Future_Date != value))
                {
                    this._Bar_Position_Share_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Licence_Charge", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Licence_Charge
        {
            get
            {
                return this._Bar_Position_Licence_Charge;
            }
            set
            {
                if ((this._Bar_Position_Licence_Charge != value))
                {
                    this._Bar_Position_Licence_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Licence_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Licence_Previous
        {
            get
            {
                return this._Bar_Position_Licence_Previous;
            }
            set
            {
                if ((this._Bar_Position_Licence_Previous != value))
                {
                    this._Bar_Position_Licence_Previous = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Licence_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Licence_Future
        {
            get
            {
                return this._Bar_Position_Licence_Future;
            }
            set
            {
                if ((this._Bar_Position_Licence_Future != value))
                {
                    this._Bar_Position_Licence_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Licence_Past_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Licence_Past_Date
        {
            get
            {
                return this._Bar_Position_Licence_Past_Date;
            }
            set
            {
                if ((this._Bar_Position_Licence_Past_Date != value))
                {
                    this._Bar_Position_Licence_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Licence_Future_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Licence_Future_Date
        {
            get
            {
                return this._Bar_Position_Licence_Future_Date;
            }
            set
            {
                if ((this._Bar_Position_Licence_Future_Date != value))
                {
                    this._Bar_Position_Licence_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Collection_Rent_Paid_Until", DbType = "VarChar(30)")]
        public string Bar_Position_Collection_Rent_Paid_Until
        {
            get
            {
                return this._Bar_Position_Collection_Rent_Paid_Until;
            }
            set
            {
                if ((this._Bar_Position_Collection_Rent_Paid_Until != value))
                {
                    this._Bar_Position_Collection_Rent_Paid_Until = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Collection_Period", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Collection_Period
        {
            get
            {
                return this._Bar_Position_Collection_Period;
            }
            set
            {
                if ((this._Bar_Position_Collection_Period != value))
                {
                    this._Bar_Position_Collection_Period = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Prize_LOS", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Prize_LOS
        {
            get
            {
                return this._Bar_Position_Prize_LOS;
            }
            set
            {
                if ((this._Bar_Position_Prize_LOS != value))
                {
                    this._Bar_Position_Prize_LOS = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_ID", DbType = "Int NOT NULL")]
        public int Terms_Profile_ID
        {
            get
            {
                return this._Terms_Profile_ID;
            }
            set
            {
                if ((this._Terms_Profile_ID != value))
                {
                    this._Terms_Profile_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Name", DbType = "VarChar(50)")]
        public string Terms_Profile_Name
        {
            get
            {
                return this._Terms_Profile_Name;
            }
            set
            {
                if ((this._Terms_Profile_Name != value))
                {
                    this._Terms_Profile_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Index != value))
                {
                    this._Terms_Profile_Partners_Supplier_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Supplier_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Use != value))
                {
                    this._Terms_Profile_Partners_Supplier_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Supplier_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Supplier_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Type != value))
                {
                    this._Terms_Profile_Partners_Supplier_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Supplier_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Value != value))
                {
                    this._Terms_Profile_Partners_Supplier_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Supplier_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Supplier_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Supplier_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Share != value))
                {
                    this._Terms_Profile_Partners_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Share_Schedule", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Share_Schedule
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Share_Schedule;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Share_Schedule != value))
                {
                    this._Terms_Profile_Partners_Supplier_Share_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Rent_Schedule", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Rent_Schedule
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Rent_Schedule;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Rent_Schedule != value))
                {
                    this._Terms_Profile_Partners_Supplier_Rent_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Supplier_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Supplier_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Index != value))
                {
                    this._Terms_Profile_Partners_Site_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Site_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Use != value))
                {
                    this._Terms_Profile_Partners_Site_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Site_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Site_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Type != value))
                {
                    this._Terms_Profile_Partners_Site_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Site_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Value != value))
                {
                    this._Terms_Profile_Partners_Site_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Site_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Site_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Site_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Share != value))
                {
                    this._Terms_Profile_Partners_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Site_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Site_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Site_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Index != value))
                {
                    this._Terms_Profile_Partners_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Group_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Use != value))
                {
                    this._Terms_Profile_Partners_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Type != value))
                {
                    this._Terms_Profile_Partners_Group_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Group_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Value != value))
                {
                    this._Terms_Profile_Partners_Group_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Group_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Group_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Group_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Share != value))
                {
                    this._Terms_Profile_Partners_Group_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Group_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Group_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Group_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Index != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Sec_Group_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Use != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Type != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Sec_Group_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Value != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Sec_Group_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Sec_Group_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Share != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Sec_Group_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Output_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Index != value))
                {
                    this._Terms_Profile_VAT_Output_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Output_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Use != value))
                {
                    this._Terms_Profile_VAT_Output_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Output_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Output_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Output_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Output_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Index != value))
                {
                    this._Terms_Profile_VAT_Supplier_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Supplier_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Use != value))
                {
                    this._Terms_Profile_VAT_Supplier_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Supplier_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Supplier_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Supplier_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Supplier_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Index != value))
                {
                    this._Terms_Profile_VAT_Site_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Site_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Use != value))
                {
                    this._Terms_Profile_VAT_Site_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Site_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Site_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Site_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Site_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Index != value))
                {
                    this._Terms_Profile_VAT_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Group_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Use != value))
                {
                    this._Terms_Profile_VAT_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Group_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Index != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Sec_Group_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Use != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Index
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Index != value))
                {
                    this._Terms_Profile_Other_Licence_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Licence_Use
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Use != value))
                {
                    this._Terms_Profile_Other_Licence_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Licence_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Vat != value))
                {
                    this._Terms_Profile_Other_Licence_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Licence_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Licence_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Licence_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Charge != value))
                {
                    this._Terms_Profile_Other_Licence_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Paid_By != value))
                {
                    this._Terms_Profile_Other_Licence_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Guarantor != value))
                {
                    this._Terms_Profile_Other_Licence_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Frequency != value))
                {
                    this._Terms_Profile_Other_Licence_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Index
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Index != value))
                {
                    this._Terms_Profile_Other_Prize_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Prize_Use
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Use != value))
                {
                    this._Terms_Profile_Other_Prize_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Prize_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Vat != value))
                {
                    this._Terms_Profile_Other_Prize_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Prize_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Prize_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Prize_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Charge != value))
                {
                    this._Terms_Profile_Other_Prize_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Paid_By != value))
                {
                    this._Terms_Profile_Other_Prize_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Guarantor != value))
                {
                    this._Terms_Profile_Other_Prize_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Frequency != value))
                {
                    this._Terms_Profile_Other_Prize_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Index
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Index != value))
                {
                    this._Terms_Profile_Other_Consultancy_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Consultancy_Use
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Use != value))
                {
                    this._Terms_Profile_Other_Consultancy_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Consultancy_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Vat != value))
                {
                    this._Terms_Profile_Other_Consultancy_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Consultancy_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Consultancy_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Consultancy_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Charge != value))
                {
                    this._Terms_Profile_Other_Consultancy_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Paid_By != value))
                {
                    this._Terms_Profile_Other_Consultancy_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Guarantor != value))
                {
                    this._Terms_Profile_Other_Consultancy_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Frequency != value))
                {
                    this._Terms_Profile_Other_Consultancy_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Index
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Index != value))
                {
                    this._Terms_Profile_Other_Royalty_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Royalty_Use
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Use != value))
                {
                    this._Terms_Profile_Other_Royalty_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Royalty_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Vat != value))
                {
                    this._Terms_Profile_Other_Royalty_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Royalty_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Royalty_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Royalty_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Charge != value))
                {
                    this._Terms_Profile_Other_Royalty_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Paid_By != value))
                {
                    this._Terms_Profile_Other_Royalty_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Guarantor != value))
                {
                    this._Terms_Profile_Other_Royalty_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Frequency != value))
                {
                    this._Terms_Profile_Other_Royalty_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Index
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Index != value))
                {
                    this._Terms_Profile_Other_Other1_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Name", DbType = "VarChar(50)")]
        public string Terms_Profile_Other_Other1_Name
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Name;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Name != value))
                {
                    this._Terms_Profile_Other_Other1_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other1_Use
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Use != value))
                {
                    this._Terms_Profile_Other_Other1_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other1_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Vat != value))
                {
                    this._Terms_Profile_Other_Other1_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Other1_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Other1_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Other1_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Charge != value))
                {
                    this._Terms_Profile_Other_Other1_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Paid_By != value))
                {
                    this._Terms_Profile_Other_Other1_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Guarantor != value))
                {
                    this._Terms_Profile_Other_Other1_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Frequency != value))
                {
                    this._Terms_Profile_Other_Other1_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Index
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Index != value))
                {
                    this._Terms_Profile_Other_Other2_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Name", DbType = "VarChar(50)")]
        public string Terms_Profile_Other_Other2_Name
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Name;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Name != value))
                {
                    this._Terms_Profile_Other_Other2_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other2_Use
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Use != value))
                {
                    this._Terms_Profile_Other_Other2_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other2_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Vat != value))
                {
                    this._Terms_Profile_Other_Other2_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Other2_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Other2_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Other2_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Charge != value))
                {
                    this._Terms_Profile_Other_Other2_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Paid_By != value))
                {
                    this._Terms_Profile_Other_Other2_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Guarantor != value))
                {
                    this._Terms_Profile_Other_Other2_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Frequency != value))
                {
                    this._Terms_Profile_Other_Other2_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_London_Rent_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_London_Rent_Use
        {
            get
            {
                return this._Terms_Profile_London_Rent_Use;
            }
            set
            {
                if ((this._Terms_Profile_London_Rent_Use != value))
                {
                    this._Terms_Profile_London_Rent_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_London_Rent_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_London_Rent_Charge
        {
            get
            {
                return this._Terms_Profile_London_Rent_Charge;
            }
            set
            {
                if ((this._Terms_Profile_London_Rent_Charge != value))
                {
                    this._Terms_Profile_London_Rent_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Index", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Index
        {
            get
            {
                return this._Terms_Profile_GPT_Index;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Index != value))
                {
                    this._Terms_Profile_GPT_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Use", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Use
        {
            get
            {
                return this._Terms_Profile_GPT_Use;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Use != value))
                {
                    this._Terms_Profile_GPT_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Cash_Destination", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_GPT_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Cash_Destination != value))
                {
                    this._Terms_Profile_GPT_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Deferred_Remittance", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_GPT_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Deferred_Remittance != value))
                {
                    this._Terms_Profile_GPT_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sub_Company_Use_Split_Rents", DbType = "Bit")]
        public System.Nullable<bool> Sub_Company_Use_Split_Rents
        {
            get
            {
                return this._Sub_Company_Use_Split_Rents;
            }
            set
            {
                if ((this._Sub_Company_Use_Split_Rents != value))
                {
                    this._Sub_Company_Use_Split_Rents = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Use_Terms", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Use_Terms
        {
            get
            {
                return this._Bar_Position_Use_Terms;
            }
            set
            {
                if ((this._Bar_Position_Use_Terms != value))
                {
                    this._Bar_Position_Use_Terms = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Rent_Schedule", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent_Schedule
        {
            get
            {
                return this._Bar_Position_Override_Rent_Schedule;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_Schedule != value))
                {
                    this._Bar_Position_Override_Rent_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Share_Schedule", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Share_Schedule
        {
            get
            {
                return this._Bar_Position_Override_Share_Schedule;
            }
            set
            {
                if ((this._Bar_Position_Override_Share_Schedule != value))
                {
                    this._Bar_Position_Override_Share_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent_Schedule_ID", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Rent_Schedule_ID
        {
            get
            {
                return this._Bar_Position_Rent_Schedule_ID;
            }
            set
            {
                if ((this._Bar_Position_Rent_Schedule_ID != value))
                {
                    this._Bar_Position_Rent_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Share_Schedule_ID", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Share_Schedule_ID
        {
            get
            {
                return this._Bar_Position_Share_Schedule_ID;
            }
            set
            {
                if ((this._Bar_Position_Share_Schedule_ID != value))
                {
                    this._Bar_Position_Share_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_Name", DbType = "VarChar(50)")]
        public string Terms_Group_Name
        {
            get
            {
                return this._Terms_Group_Name;
            }
            set
            {
                if ((this._Terms_Group_Name != value))
                {
                    this._Terms_Group_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Rent_From_Schedule_To_Rent", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent_From_Schedule_To_Rent
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Schedule_To_Rent;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Schedule_To_Rent != value))
                {
                    this._Bar_Position_Override_Rent_From_Schedule_To_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Rent_From_Rent_To_Schedule", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent_From_Rent_To_Schedule
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Rent_To_Schedule;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Rent_To_Schedule != value))
                {
                    this._Bar_Position_Override_Rent_From_Rent_To_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Rent_From_Schedule_To_Rent_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Override_Rent_From_Schedule_To_Rent_Date
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Schedule_To_Rent_Date != value))
                {
                    this._Bar_Position_Override_Rent_From_Schedule_To_Rent_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Override_Rent_From_Rent_To_Schedule_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Override_Rent_From_Rent_To_Schedule_Date
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Rent_To_Schedule_Date != value))
                {
                    this._Bar_Position_Override_Rent_From_Rent_To_Schedule_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_Rent_Schedule_ID_From", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Rent_Schedule_ID_From
        {
            get
            {
                return this._Bar_Position_Rent_Schedule_ID_From;
            }
            set
            {
                if ((this._Bar_Position_Rent_Schedule_ID_From != value))
                {
                    this._Bar_Position_Rent_Schedule_ID_From = value;
                }
            }
        }
    }

    public partial class RentResult
    {

        private string _Machine_Class_Past_Date;

        private string _Machine_Class_Future_Date;

        private string _Current_Future_Date;

        private string _Current_Past_Date;

        private System.Nullable<float> _Current_Current_Price;

        private System.Nullable<float> _Current_Current_SupCharge;

        private System.Nullable<float> _Current_Future_Price;

        private System.Nullable<float> _Current_Future_SupCharge;

        private System.Nullable<float> _Current_Past_Price;

        private System.Nullable<float> _Current_Past_SupCharge;

        private string _Past_Future_Date;

        private string _Past_Past_Date;

        private System.Nullable<float> _Past_Current_Price;

        private System.Nullable<float> _Past_Current_SupCharge;

        private System.Nullable<float> _Past_Future_Price;

        private System.Nullable<float> _Past_Future_SupCharge;

        private System.Nullable<float> _Past_Past_Price;

        private System.Nullable<float> _Past_Past_SupCharge;

        private string _Future_Future_Date;

        private string _Future_Past_Date;

        private System.Nullable<float> _Future_Current_Price;

        private System.Nullable<float> _Future_Current_SupCharge;

        private System.Nullable<float> _Future_Future_Price;

        private System.Nullable<float> _Future_Future_SupCharge;

        private System.Nullable<float> _Future_Past_Price;

        private System.Nullable<float> _Future_Past_SupCharge;

        private string _Rent_Schedule_Name;

        private System.Nullable<float> _Machine_Class_Rent_Band_Supplemental_Charge;

        private System.Nullable<bool> _Rent_Schedule_Supplemental_Is_Percentage;

        public RentResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Past_Date", DbType = "VarChar(30)")]
        public string Machine_Class_Past_Date
        {
            get
            {
                return this._Machine_Class_Past_Date;
            }
            set
            {
                if ((this._Machine_Class_Past_Date != value))
                {
                    this._Machine_Class_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Future_Date", DbType = "VarChar(30)")]
        public string Machine_Class_Future_Date
        {
            get
            {
                return this._Machine_Class_Future_Date;
            }
            set
            {
                if ((this._Machine_Class_Future_Date != value))
                {
                    this._Machine_Class_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Future_Date", DbType = "VarChar(30)")]
        public string Current_Future_Date
        {
            get
            {
                return this._Current_Future_Date;
            }
            set
            {
                if ((this._Current_Future_Date != value))
                {
                    this._Current_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Past_Date", DbType = "VarChar(30)")]
        public string Current_Past_Date
        {
            get
            {
                return this._Current_Past_Date;
            }
            set
            {
                if ((this._Current_Past_Date != value))
                {
                    this._Current_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Current_Price", DbType = "Real")]
        public System.Nullable<float> Current_Current_Price
        {
            get
            {
                return this._Current_Current_Price;
            }
            set
            {
                if ((this._Current_Current_Price != value))
                {
                    this._Current_Current_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Current_SupCharge", DbType = "Real")]
        public System.Nullable<float> Current_Current_SupCharge
        {
            get
            {
                return this._Current_Current_SupCharge;
            }
            set
            {
                if ((this._Current_Current_SupCharge != value))
                {
                    this._Current_Current_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Future_Price", DbType = "Real")]
        public System.Nullable<float> Current_Future_Price
        {
            get
            {
                return this._Current_Future_Price;
            }
            set
            {
                if ((this._Current_Future_Price != value))
                {
                    this._Current_Future_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Future_SupCharge", DbType = "Real")]
        public System.Nullable<float> Current_Future_SupCharge
        {
            get
            {
                return this._Current_Future_SupCharge;
            }
            set
            {
                if ((this._Current_Future_SupCharge != value))
                {
                    this._Current_Future_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Past_Price", DbType = "Real")]
        public System.Nullable<float> Current_Past_Price
        {
            get
            {
                return this._Current_Past_Price;
            }
            set
            {
                if ((this._Current_Past_Price != value))
                {
                    this._Current_Past_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Current_Past_SupCharge", DbType = "Real")]
        public System.Nullable<float> Current_Past_SupCharge
        {
            get
            {
                return this._Current_Past_SupCharge;
            }
            set
            {
                if ((this._Current_Past_SupCharge != value))
                {
                    this._Current_Past_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Future_Date", DbType = "VarChar(30)")]
        public string Past_Future_Date
        {
            get
            {
                return this._Past_Future_Date;
            }
            set
            {
                if ((this._Past_Future_Date != value))
                {
                    this._Past_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Past_Date", DbType = "VarChar(30)")]
        public string Past_Past_Date
        {
            get
            {
                return this._Past_Past_Date;
            }
            set
            {
                if ((this._Past_Past_Date != value))
                {
                    this._Past_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Current_Price", DbType = "Real")]
        public System.Nullable<float> Past_Current_Price
        {
            get
            {
                return this._Past_Current_Price;
            }
            set
            {
                if ((this._Past_Current_Price != value))
                {
                    this._Past_Current_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Current_SupCharge", DbType = "Real")]
        public System.Nullable<float> Past_Current_SupCharge
        {
            get
            {
                return this._Past_Current_SupCharge;
            }
            set
            {
                if ((this._Past_Current_SupCharge != value))
                {
                    this._Past_Current_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Future_Price", DbType = "Real")]
        public System.Nullable<float> Past_Future_Price
        {
            get
            {
                return this._Past_Future_Price;
            }
            set
            {
                if ((this._Past_Future_Price != value))
                {
                    this._Past_Future_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Future_SupCharge", DbType = "Real")]
        public System.Nullable<float> Past_Future_SupCharge
        {
            get
            {
                return this._Past_Future_SupCharge;
            }
            set
            {
                if ((this._Past_Future_SupCharge != value))
                {
                    this._Past_Future_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Past_Price", DbType = "Real")]
        public System.Nullable<float> Past_Past_Price
        {
            get
            {
                return this._Past_Past_Price;
            }
            set
            {
                if ((this._Past_Past_Price != value))
                {
                    this._Past_Past_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Past_Past_SupCharge", DbType = "Real")]
        public System.Nullable<float> Past_Past_SupCharge
        {
            get
            {
                return this._Past_Past_SupCharge;
            }
            set
            {
                if ((this._Past_Past_SupCharge != value))
                {
                    this._Past_Past_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Future_Date", DbType = "VarChar(30)")]
        public string Future_Future_Date
        {
            get
            {
                return this._Future_Future_Date;
            }
            set
            {
                if ((this._Future_Future_Date != value))
                {
                    this._Future_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Past_Date", DbType = "VarChar(30)")]
        public string Future_Past_Date
        {
            get
            {
                return this._Future_Past_Date;
            }
            set
            {
                if ((this._Future_Past_Date != value))
                {
                    this._Future_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Current_Price", DbType = "Real")]
        public System.Nullable<float> Future_Current_Price
        {
            get
            {
                return this._Future_Current_Price;
            }
            set
            {
                if ((this._Future_Current_Price != value))
                {
                    this._Future_Current_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Current_SupCharge", DbType = "Real")]
        public System.Nullable<float> Future_Current_SupCharge
        {
            get
            {
                return this._Future_Current_SupCharge;
            }
            set
            {
                if ((this._Future_Current_SupCharge != value))
                {
                    this._Future_Current_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Future_Price", DbType = "Real")]
        public System.Nullable<float> Future_Future_Price
        {
            get
            {
                return this._Future_Future_Price;
            }
            set
            {
                if ((this._Future_Future_Price != value))
                {
                    this._Future_Future_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Future_SupCharge", DbType = "Real")]
        public System.Nullable<float> Future_Future_SupCharge
        {
            get
            {
                return this._Future_Future_SupCharge;
            }
            set
            {
                if ((this._Future_Future_SupCharge != value))
                {
                    this._Future_Future_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Past_Price", DbType = "Real")]
        public System.Nullable<float> Future_Past_Price
        {
            get
            {
                return this._Future_Past_Price;
            }
            set
            {
                if ((this._Future_Past_Price != value))
                {
                    this._Future_Past_Price = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Future_Past_SupCharge", DbType = "Real")]
        public System.Nullable<float> Future_Past_SupCharge
        {
            get
            {
                return this._Future_Past_SupCharge;
            }
            set
            {
                if ((this._Future_Past_SupCharge != value))
                {
                    this._Future_Past_SupCharge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rent_Schedule_Name", DbType = "VarChar(50)")]
        public string Rent_Schedule_Name
        {
            get
            {
                return this._Rent_Schedule_Name;
            }
            set
            {
                if ((this._Rent_Schedule_Name != value))
                {
                    this._Rent_Schedule_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Rent_Band_Supplemental_Charge", DbType = "Real")]
        public System.Nullable<float> Machine_Class_Rent_Band_Supplemental_Charge
        {
            get
            {
                return this._Machine_Class_Rent_Band_Supplemental_Charge;
            }
            set
            {
                if ((this._Machine_Class_Rent_Band_Supplemental_Charge != value))
                {
                    this._Machine_Class_Rent_Band_Supplemental_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rent_Schedule_Supplemental_Is_Percentage", DbType = "Bit")]
        public System.Nullable<bool> Rent_Schedule_Supplemental_Is_Percentage
        {
            get
            {
                return this._Rent_Schedule_Supplemental_Is_Percentage;
            }
            set
            {
                if ((this._Rent_Schedule_Supplemental_Is_Percentage != value))
                {
                    this._Rent_Schedule_Supplemental_Is_Percentage = value;
                }
            }
        }
    }

    public partial class AccessoryInstallationResult
    {

        private int _Accessory_Installation_ID;

        private System.Nullable<int> _Bar_Position_ID;

        private System.Nullable<int> _Accessory_ID;

        private string _Accessory_Installation_Start_Date;

        private string _Accessory_Installation_End_Date;

        private string _Accessory_Installation_Serial_No;

        private string _Accessory_Installation_Supplier_Position;

        private string _Accessory_Installation_Company_Position;

        private System.Nullable<int> _Accessory_Installation_Meters_In;

        private System.Nullable<int> _Accessory_Installation_Meters_Out;

        private System.Nullable<int> _Accessory_Installation_Amedis_Import_Log_ID;

        private System.Nullable<int> _Accessory_Installation_Amedis_Import_Log_Withdrawl_ID;

        private System.Nullable<float> _Accessory_Installation_Charge;

        public AccessoryInstallationResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_ID", DbType = "Int NOT NULL")]
        public int Accessory_Installation_ID
        {
            get
            {
                return this._Accessory_Installation_ID;
            }
            set
            {
                if ((this._Accessory_Installation_ID != value))
                {
                    this._Accessory_Installation_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bar_Position_ID", DbType = "Int")]
        public System.Nullable<int> Bar_Position_ID
        {
            get
            {
                return this._Bar_Position_ID;
            }
            set
            {
                if ((this._Bar_Position_ID != value))
                {
                    this._Bar_Position_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_ID", DbType = "Int")]
        public System.Nullable<int> Accessory_ID
        {
            get
            {
                return this._Accessory_ID;
            }
            set
            {
                if ((this._Accessory_ID != value))
                {
                    this._Accessory_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Start_Date", DbType = "VarChar(30)")]
        public string Accessory_Installation_Start_Date
        {
            get
            {
                return this._Accessory_Installation_Start_Date;
            }
            set
            {
                if ((this._Accessory_Installation_Start_Date != value))
                {
                    this._Accessory_Installation_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_End_Date", DbType = "VarChar(30)")]
        public string Accessory_Installation_End_Date
        {
            get
            {
                return this._Accessory_Installation_End_Date;
            }
            set
            {
                if ((this._Accessory_Installation_End_Date != value))
                {
                    this._Accessory_Installation_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Serial_No", DbType = "VarChar(50)")]
        public string Accessory_Installation_Serial_No
        {
            get
            {
                return this._Accessory_Installation_Serial_No;
            }
            set
            {
                if ((this._Accessory_Installation_Serial_No != value))
                {
                    this._Accessory_Installation_Serial_No = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Supplier_Position", DbType = "VarChar(10)")]
        public string Accessory_Installation_Supplier_Position
        {
            get
            {
                return this._Accessory_Installation_Supplier_Position;
            }
            set
            {
                if ((this._Accessory_Installation_Supplier_Position != value))
                {
                    this._Accessory_Installation_Supplier_Position = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Company_Position", DbType = "VarChar(10)")]
        public string Accessory_Installation_Company_Position
        {
            get
            {
                return this._Accessory_Installation_Company_Position;
            }
            set
            {
                if ((this._Accessory_Installation_Company_Position != value))
                {
                    this._Accessory_Installation_Company_Position = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Meters_In", DbType = "Int")]
        public System.Nullable<int> Accessory_Installation_Meters_In
        {
            get
            {
                return this._Accessory_Installation_Meters_In;
            }
            set
            {
                if ((this._Accessory_Installation_Meters_In != value))
                {
                    this._Accessory_Installation_Meters_In = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Meters_Out", DbType = "Int")]
        public System.Nullable<int> Accessory_Installation_Meters_Out
        {
            get
            {
                return this._Accessory_Installation_Meters_Out;
            }
            set
            {
                if ((this._Accessory_Installation_Meters_Out != value))
                {
                    this._Accessory_Installation_Meters_Out = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Amedis_Import_Log_ID", DbType = "Int")]
        public System.Nullable<int> Accessory_Installation_Amedis_Import_Log_ID
        {
            get
            {
                return this._Accessory_Installation_Amedis_Import_Log_ID;
            }
            set
            {
                if ((this._Accessory_Installation_Amedis_Import_Log_ID != value))
                {
                    this._Accessory_Installation_Amedis_Import_Log_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Amedis_Import_Log_Withdrawl_ID", DbType = "Int")]
        public System.Nullable<int> Accessory_Installation_Amedis_Import_Log_Withdrawl_ID
        {
            get
            {
                return this._Accessory_Installation_Amedis_Import_Log_Withdrawl_ID;
            }
            set
            {
                if ((this._Accessory_Installation_Amedis_Import_Log_Withdrawl_ID != value))
                {
                    this._Accessory_Installation_Amedis_Import_Log_Withdrawl_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Accessory_Installation_Charge", DbType = "Real")]
        public System.Nullable<float> Accessory_Installation_Charge
        {
            get
            {
                return this._Accessory_Installation_Charge;
            }
            set
            {
                if ((this._Accessory_Installation_Charge != value))
                {
                    this._Accessory_Installation_Charge = value;
                }
            }
        }
    }

    public partial class MachineClassResult
    {

        private System.Nullable<int> _Machine_Class_ID;

        public MachineClassResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_ID", DbType = "Int")]
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
    }

    public partial class ShareBandResult
    {

        private int _Share_Band_ID;

        private System.Nullable<int> _Share_Schedule_ID;

        private string _Share_Band_Name;

        private string _Share_Band_Start_Date;

        private string _Share_Band_End_Date;

        private string _Share_Band_Description;

        private System.Nullable<float> _Share_Band_Supplier_Share;

        private System.Nullable<float> _Share_Band_Site_Share;

        private System.Nullable<float> _Share_Band_Company_Share;

        private System.Nullable<float> _Share_Band_Sec_Company_Share;

        private System.Nullable<float> _Share_Band_Future_Supplier_Share;

        private System.Nullable<float> _Share_Band_Future_Site_Share;

        private System.Nullable<float> _Share_Band_Future_Company_Share;

        private System.Nullable<float> _Share_Band_Future_Sec_Company_Share;

        private string _Share_Band_Future_Start_Date;

        private System.Nullable<float> _Share_Band_Past_Supplier_Share;

        private System.Nullable<float> _Share_Band_Past_Site_Share;

        private System.Nullable<float> _Share_Band_Past_Company_Share;

        private System.Nullable<float> _Share_Band_Past_Sec_Company_Share;

        private string _Share_Band_Past_End_Date;

        private System.Nullable<float> _Share_Band_Supplier_Rent;

        private System.Nullable<float> _Share_Band_Future_Supplier_Rent;

        private System.Nullable<float> _Share_Band_Past_Supplier_Rent;

        private System.Nullable<bool> _Share_Band_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Supplier_Rent_Guaranteed;

        private System.Nullable<bool> _Share_Band_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Supplier_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Site_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Sec_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Future_Sec_Company_Share_Guaranteed;

        private System.Nullable<bool> _Share_Band_Past_Sec_Company_Share_Guaranteed;

        public ShareBandResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID", DbType = "Int NOT NULL")]
        public int Share_Band_ID
        {
            get
            {
                return this._Share_Band_ID;
            }
            set
            {
                if ((this._Share_Band_ID != value))
                {
                    this._Share_Band_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_ID", DbType = "Int")]
        public System.Nullable<int> Share_Schedule_ID
        {
            get
            {
                return this._Share_Schedule_ID;
            }
            set
            {
                if ((this._Share_Schedule_ID != value))
                {
                    this._Share_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Name", DbType = "VarChar(50)")]
        public string Share_Band_Name
        {
            get
            {
                return this._Share_Band_Name;
            }
            set
            {
                if ((this._Share_Band_Name != value))
                {
                    this._Share_Band_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Start_Date", DbType = "VarChar(30)")]
        public string Share_Band_Start_Date
        {
            get
            {
                return this._Share_Band_Start_Date;
            }
            set
            {
                if ((this._Share_Band_Start_Date != value))
                {
                    this._Share_Band_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_End_Date", DbType = "VarChar(30)")]
        public string Share_Band_End_Date
        {
            get
            {
                return this._Share_Band_End_Date;
            }
            set
            {
                if ((this._Share_Band_End_Date != value))
                {
                    this._Share_Band_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Description", DbType = "VarChar(50)")]
        public string Share_Band_Description
        {
            get
            {
                return this._Share_Band_Description;
            }
            set
            {
                if ((this._Share_Band_Description != value))
                {
                    this._Share_Band_Description = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Supplier_Share
        {
            get
            {
                return this._Share_Band_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Supplier_Share != value))
                {
                    this._Share_Band_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Site_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Site_Share
        {
            get
            {
                return this._Share_Band_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Site_Share != value))
                {
                    this._Share_Band_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Company_Share
        {
            get
            {
                return this._Share_Band_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Company_Share != value))
                {
                    this._Share_Band_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Sec_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Sec_Company_Share
        {
            get
            {
                return this._Share_Band_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Sec_Company_Share != value))
                {
                    this._Share_Band_Sec_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Supplier_Share
        {
            get
            {
                return this._Share_Band_Future_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Share != value))
                {
                    this._Share_Band_Future_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Site_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Site_Share
        {
            get
            {
                return this._Share_Band_Future_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Site_Share != value))
                {
                    this._Share_Band_Future_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Company_Share
        {
            get
            {
                return this._Share_Band_Future_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Company_Share != value))
                {
                    this._Share_Band_Future_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Sec_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Sec_Company_Share
        {
            get
            {
                return this._Share_Band_Future_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Future_Sec_Company_Share != value))
                {
                    this._Share_Band_Future_Sec_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Start_Date", DbType = "VarChar(30)")]
        public string Share_Band_Future_Start_Date
        {
            get
            {
                return this._Share_Band_Future_Start_Date;
            }
            set
            {
                if ((this._Share_Band_Future_Start_Date != value))
                {
                    this._Share_Band_Future_Start_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Supplier_Share
        {
            get
            {
                return this._Share_Band_Past_Supplier_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Share != value))
                {
                    this._Share_Band_Past_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Site_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Site_Share
        {
            get
            {
                return this._Share_Band_Past_Site_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Site_Share != value))
                {
                    this._Share_Band_Past_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Company_Share
        {
            get
            {
                return this._Share_Band_Past_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Company_Share != value))
                {
                    this._Share_Band_Past_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Sec_Company_Share", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Sec_Company_Share
        {
            get
            {
                return this._Share_Band_Past_Sec_Company_Share;
            }
            set
            {
                if ((this._Share_Band_Past_Sec_Company_Share != value))
                {
                    this._Share_Band_Past_Sec_Company_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_End_Date", DbType = "VarChar(30)")]
        public string Share_Band_Past_End_Date
        {
            get
            {
                return this._Share_Band_Past_End_Date;
            }
            set
            {
                if ((this._Share_Band_Past_End_Date != value))
                {
                    this._Share_Band_Past_End_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Rent", DbType = "Real")]
        public System.Nullable<float> Share_Band_Supplier_Rent
        {
            get
            {
                return this._Share_Band_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Supplier_Rent != value))
                {
                    this._Share_Band_Supplier_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Rent", DbType = "Real")]
        public System.Nullable<float> Share_Band_Future_Supplier_Rent
        {
            get
            {
                return this._Share_Band_Future_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Rent != value))
                {
                    this._Share_Band_Future_Supplier_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Rent", DbType = "Real")]
        public System.Nullable<float> Share_Band_Past_Supplier_Rent
        {
            get
            {
                return this._Share_Band_Past_Supplier_Rent;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Rent != value))
                {
                    this._Share_Band_Past_Supplier_Rent = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Rent_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Supplier_Rent_Guaranteed
        {
            get
            {
                return this._Share_Band_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Rent_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Supplier_Rent_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Future_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Rent_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Supplier_Rent_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Supplier_Rent_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Rent_Guaranteed != value))
                {
                    this._Share_Band_Past_Supplier_Rent_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Supplier_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Site_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Site_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Site_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Site_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Sec_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Future_Sec_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Future_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Future_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Future_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Future_Sec_Company_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_Past_Sec_Company_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Share_Band_Past_Sec_Company_Share_Guaranteed
        {
            get
            {
                return this._Share_Band_Past_Sec_Company_Share_Guaranteed;
            }
            set
            {
                if ((this._Share_Band_Past_Sec_Company_Share_Guaranteed != value))
                {
                    this._Share_Band_Past_Sec_Company_Share_Guaranteed = value;
                }
            }
        }
    }

    public partial class ShareScheduleResult
    {

        private int _Machine_Class_Share_Band;

        private System.Nullable<int> _Machine_Class_ID;

        private System.Nullable<int> _Share_Band_ID;

        private System.Nullable<int> _Share_Band_ID_Future;

        private string _Machine_Class_Share_Future_Date;

        private System.Nullable<int> _Share_Band_ID_Past;

        private string _Machine_Class_Share_Past_Date;

        private string _Share_Schedule_Name;

        public ShareScheduleResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Share_Band", DbType = "Int NOT NULL")]
        public int Machine_Class_Share_Band
        {
            get
            {
                return this._Machine_Class_Share_Band;
            }
            set
            {
                if ((this._Machine_Class_Share_Band != value))
                {
                    this._Machine_Class_Share_Band = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID", DbType = "Int")]
        public System.Nullable<int> Share_Band_ID
        {
            get
            {
                return this._Share_Band_ID;
            }
            set
            {
                if ((this._Share_Band_ID != value))
                {
                    this._Share_Band_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID_Future", DbType = "Int")]
        public System.Nullable<int> Share_Band_ID_Future
        {
            get
            {
                return this._Share_Band_ID_Future;
            }
            set
            {
                if ((this._Share_Band_ID_Future != value))
                {
                    this._Share_Band_ID_Future = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Share_Future_Date", DbType = "VarChar(30)")]
        public string Machine_Class_Share_Future_Date
        {
            get
            {
                return this._Machine_Class_Share_Future_Date;
            }
            set
            {
                if ((this._Machine_Class_Share_Future_Date != value))
                {
                    this._Machine_Class_Share_Future_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Band_ID_Past", DbType = "Int")]
        public System.Nullable<int> Share_Band_ID_Past
        {
            get
            {
                return this._Share_Band_ID_Past;
            }
            set
            {
                if ((this._Share_Band_ID_Past != value))
                {
                    this._Share_Band_ID_Past = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Class_Share_Past_Date", DbType = "VarChar(30)")]
        public string Machine_Class_Share_Past_Date
        {
            get
            {
                return this._Machine_Class_Share_Past_Date;
            }
            set
            {
                if ((this._Machine_Class_Share_Past_Date != value))
                {
                    this._Machine_Class_Share_Past_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Name", DbType = "VarChar(50)")]
        public string Share_Schedule_Name
        {
            get
            {
                return this._Share_Schedule_Name;
            }
            set
            {
                if ((this._Share_Schedule_Name != value))
                {
                    this._Share_Schedule_Name = value;
                }
            }
        }
    }

    public partial class BarPositionResult
    {

        private System.Nullable<int> _Terms_Group_ID;

        private string _Terms_Group_Changeover_Date;

        private System.Nullable<int> _Terms_Group_Future_ID;

        private System.Nullable<int> _Terms_Group_Past_ID;

        private string _Terms_Group_Past_Changeover_Date;

        public BarPositionResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_Changeover_Date", DbType = "VarChar(30)")]
        public string Terms_Group_Changeover_Date
        {
            get
            {
                return this._Terms_Group_Changeover_Date;
            }
            set
            {
                if ((this._Terms_Group_Changeover_Date != value))
                {
                    this._Terms_Group_Changeover_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_Future_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_Future_ID
        {
            get
            {
                return this._Terms_Group_Future_ID;
            }
            set
            {
                if ((this._Terms_Group_Future_ID != value))
                {
                    this._Terms_Group_Future_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_Past_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_Past_ID
        {
            get
            {
                return this._Terms_Group_Past_ID;
            }
            set
            {
                if ((this._Terms_Group_Past_ID != value))
                {
                    this._Terms_Group_Past_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_Past_Changeover_Date", DbType = "VarChar(30)")]
        public string Terms_Group_Past_Changeover_Date
        {
            get
            {
                return this._Terms_Group_Past_Changeover_Date;
            }
            set
            {
                if ((this._Terms_Group_Past_Changeover_Date != value))
                {
                    this._Terms_Group_Past_Changeover_Date = value;
                }
            }
        }
    }

    public partial class CollectionInfoResult
    {

        private System.Nullable<int> _Installation_ID;

        private float _CashCollected;

        private string _Collection_Date;

        private string _Previous_Collection_Date;

        private int _Collection_Days;

        private System.Nullable<float> _Collection_Prize_Value;

        private System.Nullable<float> _Collection_Sundries;

        private System.Nullable<float> _Collection_Sundries_Unsupported;

        private System.Nullable<float> _Collection_Supplier_Float_Recovered;

        private System.Nullable<float> _Collection_Non_Supplier_Float_Recovered;

        private System.Nullable<float> _Collection_Treasury_Defloat;

        private System.Nullable<int> _Down_Days;

        private System.Nullable<float> _CashRefills;

        public CollectionInfoResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Installation_ID", DbType = "Int")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashCollected", DbType = "Real NOT NULL")]
        public float CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this._CashCollected = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Date", DbType = "VarChar(30)")]
        public string Collection_Date
        {
            get
            {
                return this._Collection_Date;
            }
            set
            {
                if ((this._Collection_Date != value))
                {
                    this._Collection_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Previous_Collection_Date", DbType = "VarChar(30)")]
        public string Previous_Collection_Date
        {
            get
            {
                return this._Previous_Collection_Date;
            }
            set
            {
                if ((this._Previous_Collection_Date != value))
                {
                    this._Previous_Collection_Date = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Days", DbType = "Int NOT NULL")]
        public int Collection_Days
        {
            get
            {
                return this._Collection_Days;
            }
            set
            {
                if ((this._Collection_Days != value))
                {
                    this._Collection_Days = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Prize_Value", DbType = "Real")]
        public System.Nullable<float> Collection_Prize_Value
        {
            get
            {
                return this._Collection_Prize_Value;
            }
            set
            {
                if ((this._Collection_Prize_Value != value))
                {
                    this._Collection_Prize_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Sundries", DbType = "Real")]
        public System.Nullable<float> Collection_Sundries
        {
            get
            {
                return this._Collection_Sundries;
            }
            set
            {
                if ((this._Collection_Sundries != value))
                {
                    this._Collection_Sundries = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Sundries_Unsupported", DbType = "Real")]
        public System.Nullable<float> Collection_Sundries_Unsupported
        {
            get
            {
                return this._Collection_Sundries_Unsupported;
            }
            set
            {
                if ((this._Collection_Sundries_Unsupported != value))
                {
                    this._Collection_Sundries_Unsupported = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Supplier_Float_Recovered", DbType = "Real")]
        public System.Nullable<float> Collection_Supplier_Float_Recovered
        {
            get
            {
                return this._Collection_Supplier_Float_Recovered;
            }
            set
            {
                if ((this._Collection_Supplier_Float_Recovered != value))
                {
                    this._Collection_Supplier_Float_Recovered = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Non_Supplier_Float_Recovered", DbType = "Real")]
        public System.Nullable<float> Collection_Non_Supplier_Float_Recovered
        {
            get
            {
                return this._Collection_Non_Supplier_Float_Recovered;
            }
            set
            {
                if ((this._Collection_Non_Supplier_Float_Recovered != value))
                {
                    this._Collection_Non_Supplier_Float_Recovered = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Collection_Treasury_Defloat", DbType = "Real")]
        public System.Nullable<float> Collection_Treasury_Defloat
        {
            get
            {
                return this._Collection_Treasury_Defloat;
            }
            set
            {
                if ((this._Collection_Treasury_Defloat != value))
                {
                    this._Collection_Treasury_Defloat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Down_Days", DbType = "Int")]
        public System.Nullable<int> Down_Days
        {
            get
            {
                return this._Down_Days;
            }
            set
            {
                if ((this._Down_Days != value))
                {
                    this._Down_Days = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CashRefills", DbType = "Real")]
        public System.Nullable<float> CashRefills
        {
            get
            {
                return this._CashRefills;
            }
            set
            {
                if ((this._CashRefills != value))
                {
                    this._CashRefills = value;
                }
            }
        }
    }

    public partial class ShareScheduleInfoResult
    {

        private int _Share_Schedule_ID;

        private string _Share_Schedule_Name;

        public ShareScheduleInfoResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_ID", DbType = "Int NOT NULL")]
        public int Share_Schedule_ID
        {
            get
            {
                return this._Share_Schedule_ID;
            }
            set
            {
                if ((this._Share_Schedule_ID != value))
                {
                    this._Share_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Name", DbType = "VarChar(50)")]
        public string Share_Schedule_Name
        {
            get
            {
                return this._Share_Schedule_Name;
            }
            set
            {
                if ((this._Share_Schedule_Name != value))
                {
                    this._Share_Schedule_Name = value;
                }
            }
        }
    }

    public partial class RentScheduleInfoResult
    {

        private int _Rent_Schedule_ID;

        private string _Rent_Schedule_Name;

        public RentScheduleInfoResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rent_Schedule_ID", DbType = "Int NOT NULL")]
        public int Rent_Schedule_ID
        {
            get
            {
                return this._Rent_Schedule_ID;
            }
            set
            {
                if ((this._Rent_Schedule_ID != value))
                {
                    this._Rent_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rent_Schedule_Name", DbType = "VarChar(50)")]
        public string Rent_Schedule_Name
        {
            get
            {
                return this._Rent_Schedule_Name;
            }
            set
            {
                if ((this._Rent_Schedule_Name != value))
                {
                    this._Rent_Schedule_Name = value;
                }
            }
        }
    }
}