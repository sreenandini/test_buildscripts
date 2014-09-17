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
        [Function(Name = "dbo.rsp_GetSiteStatus")]
        public ISingleResult<rsp_GetSiteStatusResult> GetSiteStatus([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_GetSiteStatusResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckSite")]
        public ISingleResult<rsp_CheckSiteResult> CheckSite([Parameter(Name = "SiteCode", DbType = "VarChar(50)")] string siteCode, [Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteCode, siteID);
            return ((ISingleResult<rsp_CheckSiteResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotInfo")]
        public ISingleResult<rsp_GetDepotInfoResult> GetDepotInfo([Parameter(Name = "SupplierID", DbType = "Int")] System.Nullable<int> supplierID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), supplierID);
            return ((ISingleResult<rsp_GetDepotInfoResult>)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetDepotInfoForservice")]
        public ISingleResult<rsp_GetDepotInfoResult>GetDepotInfoForservice([Parameter(Name = "SupplierID", DbType = "Int")] System.Nullable<int> supplierID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), supplierID);
            return ((ISingleResult<rsp_GetDepotInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetOperatorInfo")]
        public ISingleResult<rsp_GetOperatorInfoResult> GetOperatorInfo()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetOperatorInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteClassification")]
        public ISingleResult<rsp_GetSiteClassificationResult> GetSiteClassification()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetSiteClassificationResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteClassifNameonID")]
        public ISingleResult<rsp_GetSiteClassifNameonIDResult> GetSiteClassifNameonID([Parameter(Name = "SiteClassificationID", DbType = "Int")] System.Nullable<int> siteClassificationID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteClassificationID);
            return ((ISingleResult<rsp_GetSiteClassifNameonIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertUpdateSiteClassification")]
        public int InsertUpdateSiteClassification([Parameter(Name = "SiteClassifID", DbType = "Int")] System.Nullable<int> siteClassifID, [Parameter(Name = "SiteClassifName", DbType = "VarChar(50)")] string siteClassifName, [Parameter(Name = "SiteClassifIdOUT", DbType = "Int")] ref System.Nullable<int> siteClassifIdOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteClassifID, siteClassifName, siteClassifIdOUT);
            siteClassifIdOUT = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_RemoveSiteClassifonSiteStatus")]
        public int RemoveSiteClassifonSiteStatus([Parameter(Name = "SiteClassifID", DbType = "Int")] System.Nullable<int> siteClassifID, [Parameter(Name = "SiteClassifStatusOUT", DbType = "Int")] ref System.Nullable<int> siteClassifStatusOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteClassifID, siteClassifStatusOUT);
            siteClassifStatusOUT = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ServiceOperator")]
        public ISingleResult<rsp_ServiceOperatorResult> ServiceOperator()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_ServiceOperatorResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetServiceAreaonDepot")]
        public ISingleResult<rsp_GetServiceAreaonDepotResult> GetServiceAreaonDepot([Parameter(Name = "DepotID", DbType = "Int")] System.Nullable<int> depotID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depotID);
            return ((ISingleResult<rsp_GetServiceAreaonDepotResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStdOpeningHrs")]
        public ISingleResult<rsp_GetStdOpeningHrsResult> GetStdOpeningHrs()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetStdOpeningHrsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_InsertExportHistoryCrossTicketing")]
        public int InsertExportHistoryCrossTicketing([Parameter(Name = "SITECODE", DbType = "VarChar(50)")] string sITECODE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sITECODE);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_CheckSiteTicketingURL")]
        public ISingleResult<usp_CheckSiteTicketingURLResult> CheckSiteTicketingURL([Parameter(Name = "SITECODE", DbType = "VarChar(50)")] string sITECODE)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sITECODE);
            return ((ISingleResult<usp_CheckSiteTicketingURLResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetActiveInstallationsForSite")]
        public ISingleResult<rsp_GetActiveInstallationsForSiteResult> GetActiveInstallationsForSite([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id);
            return ((ISingleResult<rsp_GetActiveInstallationsForSiteResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetSiteOpeningHours")]
        public ISingleResult<rsp_GetSiteOpeningHoursResult> GetSiteOpeningHours([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetSiteOpeningHoursResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckZoneonSite")]
        public int CheckZoneonSite([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "ZoneStatusOUT", DbType = "Int")] ref System.Nullable<int> zoneStatusOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, zoneStatusOUT);
            zoneStatusOUT = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateSiteOpeningHours")]
        public int UpdateSiteOpeningHours([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "SiteOpenMonday", DbType = "VarChar(96)")] string siteOpenMonday, [Parameter(Name = "SiteOpenTuesday", DbType = "VarChar(96)")] string siteOpenTuesday, [Parameter(Name = "SiteOpenWednesday", DbType = "VarChar(96)")] string siteOpenWednesday, [Parameter(Name = "SiteOpenThursday", DbType = "VarChar(96)")] string siteOpenThursday, [Parameter(Name = "SiteOpenFriday", DbType = "VarChar(96)")] string siteOpenFriday, [Parameter(Name = "SiteOpenSaturday", DbType = "VarChar(96)")] string siteOpenSaturday, [Parameter(Name = "SiteOpenSunday", DbType = "VarChar(96)")] string siteOpenSunday, [Parameter(Name = "SiteStatusOUT", DbType = "Int")] ref System.Nullable<int> siteStatusOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, siteOpenMonday, siteOpenTuesday, siteOpenWednesday, siteOpenThursday, siteOpenFriday, siteOpenSaturday, siteOpenSunday, siteStatusOUT);
            siteStatusOUT = ((System.Nullable<int>)(result.GetParameterValue(8)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateZoneOpeningHours")]
        public int UpdateZoneOpeningHours([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "ZoneOpenMonday", DbType = "VarChar(96)")] string zoneOpenMonday, [Parameter(Name = "ZoneOpenTuesday", DbType = "VarChar(96)")] string zoneOpenTuesday, [Parameter(Name = "ZoneOpenWednesday", DbType = "VarChar(96)")] string zoneOpenWednesday, [Parameter(Name = "ZoneOpenThursday", DbType = "VarChar(96)")] string zoneOpenThursday, [Parameter(Name = "ZoneOpenFriday", DbType = "VarChar(96)")] string zoneOpenFriday, [Parameter(Name = "ZoneOpenSaturday", DbType = "VarChar(96)")] string zoneOpenSaturday, [Parameter(Name = "ZoneOpenSunday", DbType = "VarChar(96)")] string zoneOpenSunday)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, zoneOpenMonday, zoneOpenTuesday, zoneOpenWednesday, zoneOpenThursday, zoneOpenFriday, zoneOpenSaturday, zoneOpenSunday);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertNTPonSite")]
        public int InsertNTPonSite([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "NTPFrom", DbType = "VarChar(30)")] string nTPFrom, [Parameter(Name = "NTPTo", DbType = "VarChar(30)")] string nTPTo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, nTPFrom, nTPTo);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_NTPZeroCollection")]
        public int NTPZeroCollection([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "NTPFrom", DbType = "VarChar(30)")] string nTPFrom, [Parameter(Name = "NTPTo", DbType = "VarChar(30)")] string nTPTo, [Parameter(Name = "CollDays", DbType = "Int")] System.Nullable<int> collDays, [Parameter(Name = "CollTime", DbType = "VarChar(50)")] string collTime, [Parameter(Name = "PrevCollTime", DbType = "VarChar(30)")] string prevCollTime, [Parameter(Name = "CollType", DbType = "VarChar(1)")] System.Nullable<char> collType, [Parameter(Name = "Remarks", DbType = "VarChar(MAX)")] string remarks)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, nTPFrom, nTPTo, collDays, collTime, prevCollTime, collType, remarks);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertStdOpeningHrsDesc")]
        public int InsertStdOpeningHrsDesc([Parameter(Name = "SOHr_Desc", DbType = "VarChar(50)")] string sOHr_Desc, [Parameter(Name = "SOHr_ID", DbType = "Int")] ref System.Nullable<int> sOHr_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sOHr_Desc, sOHr_ID);
            sOHr_ID = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStdOpeningHrsDetails")]
        public ISingleResult<rsp_GetStdOpeningHrsDetailsResult> GetStdOpeningHrsDetails([Parameter(Name = "STDHRID", DbType = "Int")] System.Nullable<int> sTDHRID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), sTDHRID);
            return ((ISingleResult<rsp_GetStdOpeningHrsDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateStdOpeningHours")]
        public int UpdateStdOpeningHours([Parameter(Name = "StdOpenHRID", DbType = "Int")] System.Nullable<int> stdOpenHRID, [Parameter(Name = "StdOpenMonday", DbType = "VarChar(96)")] string stdOpenMonday, [Parameter(Name = "StdOpenTuesday", DbType = "VarChar(96)")] string stdOpenTuesday, [Parameter(Name = "StdOpenWednesday", DbType = "VarChar(96)")] string stdOpenWednesday, [Parameter(Name = "StdOpenThursday", DbType = "VarChar(96)")] string stdOpenThursday, [Parameter(Name = "StdOpenFriday", DbType = "VarChar(96)")] string stdOpenFriday, [Parameter(Name = "StdOpenSaturday", DbType = "VarChar(96)")] string stdOpenSaturday, [Parameter(Name = "StdOpenSunday", DbType = "VarChar(96)")] string stdOpenSunday, [Parameter(Name = "StdOpeningHoursTotal", DbType = "Int")] System.Nullable<int> stdOpeningHoursTotal, [Parameter(Name = "StdStatusOUT", DbType = "Int")] ref System.Nullable<int> stdStatusOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), stdOpenHRID, stdOpenMonday, stdOpenTuesday, stdOpenWednesday, stdOpenThursday, stdOpenFriday, stdOpenSaturday, stdOpenSunday, stdOpeningHoursTotal, stdStatusOUT);
            stdStatusOUT = ((System.Nullable<int>)(result.GetParameterValue(9)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStaffCustomerAccessOperator")]
        public ISingleResult<rsp_GetStaffCustomerAccessOperatorResult> GetStaffCustomerAccessOperator([Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffId);
            return ((ISingleResult<rsp_GetStaffCustomerAccessOperatorResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCustomerAccessDepotonOperator")]
        public ISingleResult<rsp_GetCustomerAccessDepotonOperatorResult> GetCustomerAccessDepotonOperator([Parameter(Name = "SupplierID", DbType = "Int")] System.Nullable<int> supplierID, [Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), supplierID, staffId);
            return ((ISingleResult<rsp_GetCustomerAccessDepotonOperatorResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetStaffCustomerAccessServiceOperator")]
        public ISingleResult<rsp_GetStaffCustomerAccessServiceOperatorResult> GetStaffCustomerAccessServiceOperator([Parameter(Name = "StaffId", DbType = "Int")] System.Nullable<int> staffId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), staffId);
            return ((ISingleResult<rsp_GetStaffCustomerAccessServiceOperatorResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetZoneOpeningHours")]
        public ISingleResult<rsp_GetZoneOpeningHoursResult> GetZoneOpeningHours([Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), zoneID);
            return ((ISingleResult<rsp_GetZoneOpeningHoursResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateZoneOpeningHoursonZone")]
        public int UpdateZoneOpeningHoursonZone([Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID, [Parameter(Name = "ZoneOpenMonday", DbType = "VarChar(96)")] string zoneOpenMonday, [Parameter(Name = "ZoneOpenTuesday", DbType = "VarChar(96)")] string zoneOpenTuesday, [Parameter(Name = "ZoneOpenWednesday", DbType = "VarChar(96)")] string zoneOpenWednesday, [Parameter(Name = "ZoneOpenThursday", DbType = "VarChar(96)")] string zoneOpenThursday, [Parameter(Name = "ZoneOpenFriday", DbType = "VarChar(96)")] string zoneOpenFriday, [Parameter(Name = "ZoneOpenSaturday", DbType = "VarChar(96)")] string zoneOpenSaturday, [Parameter(Name = "ZoneOpenSunday", DbType = "VarChar(96)")] string zoneOpenSunday, [Parameter(Name = "ZoneStatusOUT", DbType = "Int")] ref System.Nullable<int> zoneStatusOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), zoneID, zoneOpenMonday, zoneOpenTuesday, zoneOpenWednesday, zoneOpenThursday, zoneOpenFriday, zoneOpenSaturday, zoneOpenSunday, zoneStatusOUT);
            zoneStatusOUT = ((System.Nullable<int>)(result.GetParameterValue(8)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateZoneOpeningHoursSiteTime")]
        public int UpdateZoneOpeningHoursSiteTime([Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID, [Parameter(Name = "ZoneStatusOUT", DbType = "Int")] ref System.Nullable<int> zoneStatusOUT)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), zoneID, zoneStatusOUT);
            zoneStatusOUT = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.ReturnValue));
        }
        
        [Function(Name = "dbo.rsp_GetRepresentativeonSite")]
        public ISingleResult<rsp_GetRepresentativeonSiteResult> GetRepresentativeonSite([Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_GetRepresentativeonSiteResult>)(result.ReturnValue));
        }
        
        [Function(Name = "dbo.usp_InsertorUpdateSiteInfo")]
        public int InsertorUpdateSiteInfo(
                    [Parameter(Name = "Site_ID", DbType = "Int")] ref System.Nullable<int> site_ID,
                    [Parameter(Name = "Service_Supplier_ID", DbType = "Int")] System.Nullable<int> service_Supplier_ID,
                    [Parameter(Name = "Service_Area_ID", DbType = "Int")] System.Nullable<int> service_Area_ID,
                    [Parameter(Name = "Service_Depot_ID", DbType = "Int")] System.Nullable<int> service_Depot_ID,
                    [Parameter(Name = "Site_Name", DbType = "VarChar(50)")] string site_Name,
                    [Parameter(Name = "Site_Code", DbType = "VarChar(50)")] string site_Code,
                    [Parameter(Name = "Site_Supplier_Code", DbType = "VarChar(50)")] string site_Supplier_Code,
                    [Parameter(Name = "Site_Company_Code", DbType = "VarChar(50)")] string site_Company_Code,
                    [Parameter(Name = "Site_Phone_No", DbType = "VarChar(15)")] string site_Phone_No,
                    [Parameter(Name = "Site_Fax_No", DbType = "VarChar(15)")] string site_Fax_No,
                    [Parameter(Name = "Site_Email_Address", DbType = "VarChar(100)")] string site_Email_Address,
                    [Parameter(Name = "Site_Address_1", DbType = "VarChar(50)")] string site_Address_1,
                    [Parameter(Name = "Site_Address_2", DbType = "VarChar(50)")] string site_Address_2,
                    [Parameter(Name = "Site_Address_3", DbType = "VarChar(50)")] string site_Address_3,
                    [Parameter(Name = "Site_Address_4", DbType = "VarChar(50)")] string site_Address_4,
                    [Parameter(Name = "Site_Address_5", DbType = "VarChar(50)")] string site_Address_5,
                    [Parameter(Name = "Site_Postcode", DbType = "VarChar(15)")] string site_Postcode,
                    [Parameter(Name = "Site_Manager", DbType = "VarChar(50)")] string site_Manager,
                    [Parameter(Name = "Site_Price_Per_Play_Default", DbType = "Bit")] System.Nullable<bool> site_Price_Per_Play_Default,
                    [Parameter(Name = "Site_Price_Per_Play", DbType = "VarChar(50)")] string site_Price_Per_Play,
                    [Parameter(Name = "Site_Jackpot_Default", DbType = "Bit")] System.Nullable<bool> site_Jackpot_Default,
                    [Parameter(Name = "Site_Jackpot", DbType = "VarChar(50)")] string site_Jackpot,
                    [Parameter(Name = "Site_Percentage_Payout_Default", DbType = "Bit")] System.Nullable<bool> site_Percentage_Payout_Default,
                    [Parameter(Name = "Site_Percentage_Payout", DbType = "VarChar(50)")] string site_Percentage_Payout,
                    [Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID,
                    [Parameter(Name = "Terms_Group_ID_Default", DbType = "Bit")] System.Nullable<bool> terms_Group_ID_Default,
                    [Parameter(Name = "Terms_Group_ID", DbType = "Int")] System.Nullable<int> terms_Group_ID,
                    [Parameter(Name = "Access_Key_ID_Default", DbType = "Bit")] System.Nullable<bool> access_Key_ID_Default,
                    [Parameter(Name = "Access_Key_ID", DbType = "Int")] System.Nullable<int> access_Key_ID,
                    [Parameter(Name = "Staff_ID_Default", DbType = "Bit")] System.Nullable<bool> staff_ID_Default,
                    [Parameter(Name = "Staff_ID", DbType = "Int")] System.Nullable<int> staff_ID,
                    [Parameter(Name = "Site_Invoice_Name", DbType = "VarChar(50)")] string site_Invoice_Name,
                    [Parameter(Name = "Site_Invoice_Address", DbType = "NText")] string site_Invoice_Address,
                    [Parameter(Name = "Site_Invoice_Postcode", DbType = "VarChar(15)")] string site_Invoice_Postcode,
                    [Parameter(Name = "Sub_Company_Region_ID", DbType = "Int")] System.Nullable<int> sub_Company_Region_ID,
                    [Parameter(Name = "Sub_Company_Area_ID", DbType = "Int")] System.Nullable<int> sub_Company_Area_ID,
                    [Parameter(Name = "Sub_Company_District_ID", DbType = "Int")] System.Nullable<int> sub_Company_District_ID,
                    [Parameter(Name = "Site_Image_Reference", DbType = "VarChar(255)")] string site_Image_Reference,
                    [Parameter(Name = "Site_Image_Reference_2", DbType = "VarChar(255)")] string site_Image_Reference_2,
                    [Parameter(Name = "Site_Closed", DbType = "Int")] System.Nullable<int> site_Closed,
                    [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [Parameter(Name = "Site_Classification_ID", DbType = "Int")] System.Nullable<int> site_Classification_ID,
                    [Parameter(Name = "Site_Grade", DbType = "VarChar(10)")] string site_Grade,
                    [Parameter(Name = "Sage_Account_Ref", DbType = "VarChar(50)")] string sage_Account_Ref,
                    [Parameter(Name = "Site_Is_FreeFloat", DbType = "Bit")] System.Nullable<bool> site_Is_FreeFloat,
                    [Parameter(Name = "Site_Local_Inbox", DbType = "VarChar(100)")] string site_Local_Inbox,
                    [Parameter(Name = "Site_Local_Outbox", DbType = "VarChar(100)")] string site_Local_Outbox,
                    [Parameter(Name = "Site_Memo", DbType = "NText")] string site_Memo,
                    [Parameter(Name = "Site_Reference", DbType = "VarChar(50)")] string site_Reference,
                    [Parameter(Name = "Site_Trade_Type", DbType = "VarChar(50)")] string site_Trade_Type,
                    [Parameter(Name = "Site_Non_Trading_Period_From", DbType = "VarChar(30)")] string site_Non_Trading_Period_From,
                    [Parameter(Name = "Site_Non_Trading_Period_To", DbType = "VarChar(30)")] string site_Non_Trading_Period_To,
                    [Parameter(Name = "Site_Supplier_Service_Area", DbType = "VarChar(50)")] string site_Supplier_Service_Area,
                    [Parameter(Name = "Site_Supplier_Area", DbType = "VarChar(50)")] string site_Supplier_Area,
                    [Parameter(Name = "Standard_Opening_Hours_ID", DbType = "Int")] System.Nullable<int> standard_Opening_Hours_ID,
                    [Parameter(Name = "Next_Sub_Company_ID", DbType = "Int")] System.Nullable<int> next_Sub_Company_ID,
                    [Parameter(Name = "Next_Sub_Company_Change_Date", DbType = "VarChar(30)")] string next_Sub_Company_Change_Date,
                    [Parameter(Name = "Site_Previous_Sub_Company_ID", DbType = "Int")] System.Nullable<int> site_Previous_Sub_Company_ID,
                    [Parameter(Name = "Previous_Sub_Company_Change_Date", DbType = "VarChar(30)")] string previous_Sub_Company_Change_Date,
                    [Parameter(Name = "Site_Honeyframe_EDI", DbType = "Int")] System.Nullable<int> site_Honeyframe_EDI,
                    [Parameter(Name = "Site_Datapak_Protocol", DbType = "Int")] System.Nullable<int> site_Datapak_Protocol,
                    [Parameter(Name = "Site_Start_Date", DbType = "VarChar(30)")] string site_Start_Date,
                    [Parameter(Name = "Site_End_Date", DbType = "VarChar(30)")] string site_End_Date,
                    [Parameter(Name = "Site_Licence_Number", DbType = "VarChar(25)")] string site_Licence_Number,
                    [Parameter(Name = "Site_Fiscal_Code", DbType = "VarChar(16)")] string site_Fiscal_Code,
                    [Parameter(Name = "Site_Street_Number", DbType = "VarChar(15)")] string site_Street_Number,
                    [Parameter(Name = "Site_Province", DbType = "VarChar(15)")] string site_Province,
                    [Parameter(Name = "Site_Municipality", DbType = "VarChar(40)")] string site_Municipality,
                    [Parameter(Name = "Site_Cadastral_Code", DbType = "VarChar(15)")] string site_Cadastral_Code,
                    [Parameter(Name = "Site_Area", DbType = "Int")] System.Nullable<int> site_Area,
                    [Parameter(Name = "Site_Location_Type", DbType = "Int")] System.Nullable<int> site_Location_Type,
                    [Parameter(Name = "Site_Toponym", DbType = "Int")] System.Nullable<int> site_Toponym,
                    [Parameter(Name = "Site_Licensee_Commenced_Date", DbType = "SmallDateTime")] System.Nullable<System.DateTime> site_Licensee_Commenced_Date,
                    [Parameter(Name = "Site_Licensee_Agreement_End_Date", DbType = "DateTime")] System.Nullable<System.DateTime> site_Licensee_Agreement_End_Date,
                    [Parameter(Name = "Site_Licensee_Agreement_Type", DbType = "VarChar(50)")] string site_Licensee_Agreement_Type,
                    [Parameter(Name = "Site_Application", DbType = "SmallInt")] System.Nullable<short> site_Application,
                    [Parameter(Name = "Region", DbType = "VarChar(10)")] string region,
                    [Parameter(Name = "WebURL", DbType = "VarChar(2000)")] string webURL,
                    [Parameter(Name = "Site_Status_ID", DbType = "Int")] System.Nullable<int> site_Status_ID,
                    [Parameter(Name = "Site_Inactive_Date", DbType = "DateTime")] System.Nullable<System.DateTime> site_Inactive_Date,
                    [Parameter(Name = "Site_Connection_IPAddress", DbType = "VarChar(15)")] string site_Connection_IPAddress,
                    [Parameter(Name = "Site_MaxNumber_VLT", DbType = "Int")] System.Nullable<int> site_MaxNumber_VLT,
                    [Parameter(Name = "Site_ZonaRice", DbType = "VarChar(10)")] string site_ZonaRice,
                    [Parameter(Name = "StackerLimitPercentage", DbType = "Int")] System.Nullable<int> stackerLimitPercentage,
                    [Parameter(Name = "Site_Enabled", DbType = "Bit")] System.Nullable<bool> site_Enabled,
                    [Parameter(Name = "IsTITOEnabled", DbType = "Int")] System.Nullable<int> isTITOEnabled,
                    [Parameter(Name = "IsCrossTicketingEnabled", DbType = "Int")] System.Nullable<int> isCrossTicketingEnabled,
                    [Parameter(Name = "IsNonCashVoucherEnabled", DbType = "Int")] System.Nullable<int> isNonCashVoucherEnabled)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID, service_Supplier_ID, service_Area_ID, service_Depot_ID, site_Name, site_Code, site_Supplier_Code, site_Company_Code, site_Phone_No, site_Fax_No, site_Email_Address, site_Address_1, site_Address_2, site_Address_3, site_Address_4, site_Address_5, site_Postcode, site_Manager, site_Price_Per_Play_Default, site_Price_Per_Play, site_Jackpot_Default, site_Jackpot, site_Percentage_Payout_Default, site_Percentage_Payout, sub_Company_ID, terms_Group_ID_Default, terms_Group_ID, access_Key_ID_Default, access_Key_ID, staff_ID_Default, staff_ID, site_Invoice_Name, site_Invoice_Address, site_Invoice_Postcode, sub_Company_Region_ID, sub_Company_Area_ID, sub_Company_District_ID, site_Image_Reference, site_Image_Reference_2, site_Closed, depot_ID, site_Classification_ID, site_Grade, sage_Account_Ref, site_Is_FreeFloat, site_Local_Inbox, site_Local_Outbox, site_Memo, site_Reference, site_Trade_Type, site_Non_Trading_Period_From, site_Non_Trading_Period_To, site_Supplier_Service_Area, site_Supplier_Area, standard_Opening_Hours_ID, next_Sub_Company_ID, next_Sub_Company_Change_Date, site_Previous_Sub_Company_ID, previous_Sub_Company_Change_Date, site_Honeyframe_EDI, site_Datapak_Protocol, site_Start_Date, site_End_Date, site_Licence_Number, site_Fiscal_Code, site_Street_Number, site_Province, site_Municipality, site_Cadastral_Code, site_Area, site_Location_Type, site_Toponym, site_Licensee_Commenced_Date, site_Licensee_Agreement_End_Date, site_Licensee_Agreement_Type, site_Application, region, webURL, site_Status_ID, site_Inactive_Date, site_Connection_IPAddress, site_MaxNumber_VLT, site_ZonaRice, stackerLimitPercentage, site_Enabled, isTITOEnabled, isCrossTicketingEnabled, isNonCashVoucherEnabled);
            site_ID = ((System.Nullable<int>)(result.GetParameterValue(0)));
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_CheckWebURLExists")]
        public int rsp_CheckWebURLExists([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "WebURl", DbType = "VarChar(2000)")] string webURl, [Parameter(Name = "Exists", DbType = "Int")] ref System.Nullable<int> exists)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, webURl, exists);
            exists = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_UpdateDepotIDForBarPosition")]
        public int UpdateDepotIDForBarPosition([Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID, [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), depot_ID, site_ID);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetDetailFieldsonLoad")]
        public ISingleResult<rsp_GetDetailFieldsonLoadResult> GetDetailFields([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetDetailFieldsonLoadResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_CheckIsExchangeKeyExists")]
        public ISingleResult<rsp_CheckIsExchangeKeyExistsResult> CheckIsExchangeKeyExists([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_ID);
            return ((ISingleResult<rsp_CheckIsExchangeKeyExistsResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetDetailFieldsonLoadResult
    {

        private int _Site_ID;

        private System.Nullable<int> _Service_Supplier_ID;

        private System.Nullable<int> _Service_Area_ID;

        private System.Nullable<int> _Service_Depot_ID;

        private string _Site_Name;

        private string _Site_Code;

        private string _Site_Supplier_Code;

        private string _Site_Company_Code;

        private string _Site_Phone_No;

        private string _Site_Fax_No;

        private string _Site_Email_Address;

        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private string _Site_Address_4;

        private string _Site_Address_5;

        private string _Site_Postcode;

        private string _Site_Manager;

        private System.Nullable<bool> _Site_Price_Per_Play_Default;

        private string _Site_Price_Per_Play;

        private System.Nullable<bool> _Site_Jackpot_Default;

        private string _Site_Jackpot;

        private System.Nullable<bool> _Site_Percentage_Payout_Default;

        private string _Site_Percentage_Payout;

        private System.Nullable<int> _Sub_Company_ID;

        private System.Nullable<int> _Company_ID;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<bool> _Staff_ID_Default;

        private System.Nullable<int> _Staff_ID;

        private string _Site_Invoice_Name;

        private string _Site_Invoice_Address;

        private string _Site_Invoice_Postcode;

        private System.Nullable<int> _Sub_Company_Region_ID;

        private System.Nullable<int> _Sub_Company_Area_ID;

        private System.Nullable<int> _Sub_Company_District_ID;

        private string _Site_Image_Reference;

        private string _Site_Image_Reference_2;

        private System.Nullable<int> _Site_Closed;

        private System.Nullable<int> _Depot_ID;

        private System.Nullable<int> _Supplier_ID;

        private int _Site_Classification_ID;

        private string _Site_Grade;

        private string _Sage_Account_Ref;

        private System.Nullable<bool> _Site_Is_FreeFloat;

        private string _Site_Local_Inbox;

        private string _Site_Local_Outbox;

        private string _Site_Memo;

        private string _Site_Reference;

        private string _Site_Trade_Type;

        private string _Site_Non_Trading_Period_From;

        private string _Site_Non_Trading_Period_To;

        private string _Site_Supplier_Service_Area;

        private string _Site_Supplier_Area;

        private System.Nullable<int> _Standard_Opening_Hours_ID;

        private System.Nullable<int> _Next_Sub_Company_ID;

        private string _Next_Sub_Company_Change_Date;

        private System.Nullable<int> _Site_Previous_Sub_Company_ID;

        private string _Previous_Sub_Company_Change_Date;

        private System.Nullable<int> _Site_Honeyframe_EDI;

        private System.Nullable<int> _Site_Datapak_Protocol;

        private string _Site_Start_Date;

        private string _Site_End_Date;

        private string _Site_Licence_Number;

        private string _Site_Fiscal_Code;

        private string _Site_Street_Number;

        private string _Site_Province;

        private string _Site_Municipality;

        private string _Site_Cadastral_Code;

        private System.Nullable<int> _Site_Area;

        private System.Nullable<int> _Site_Location_Type;

        private System.Nullable<int> _Site_Toponym;

        private System.Nullable<System.DateTime> _Site_Licensee_Commenced_Date;

        private System.Nullable<System.DateTime> _Site_Licensee_Agreement_End_Date;

        private string _Site_Licensee_Agreement_Type;

        private System.Nullable<short> _Site_Application;

        private string _Region;

        private string _WebURL;

        private int _Site_Status_ID;

        private System.Nullable<System.DateTime> _Site_Inactive_Date;

        private string _Site_Connection_IPAddress;

        private System.Nullable<int> _Site_MaxNumber_VLT;

        private string _Site_ZonaRice;

        private string _Site_Closed_Date;

        private System.Nullable<int> _StackerLimitPercentage;

        //private bool _Site_Enabled;

        //private int _IsTITOEnabled;

        //private int _IsNonCashVoucherEnabled;

        public rsp_GetDetailFieldsonLoadResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
        public int Site_ID
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

        [Column(Storage = "_Service_Supplier_ID", DbType = "Int")]
        public System.Nullable<int> Service_Supplier_ID
        {
            get
            {
                return this._Service_Supplier_ID;
            }
            set
            {
                if ((this._Service_Supplier_ID != value))
                {
                    this._Service_Supplier_ID = value;
                }
            }
        }

        [Column(Storage = "_Service_Area_ID", DbType = "Int")]
        public System.Nullable<int> Service_Area_ID
        {
            get
            {
                return this._Service_Area_ID;
            }
            set
            {
                if ((this._Service_Area_ID != value))
                {
                    this._Service_Area_ID = value;
                }
            }
        }

        [Column(Storage = "_Service_Depot_ID", DbType = "Int")]
        public System.Nullable<int> Service_Depot_ID
        {
            get
            {
                return this._Service_Depot_ID;
            }
            set
            {
                if ((this._Service_Depot_ID != value))
                {
                    this._Service_Depot_ID = value;
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

        [Column(Storage = "_Site_Supplier_Code", DbType = "VarChar(50)")]
        public string Site_Supplier_Code
        {
            get
            {
                return this._Site_Supplier_Code;
            }
            set
            {
                if ((this._Site_Supplier_Code != value))
                {
                    this._Site_Supplier_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Company_Code", DbType = "VarChar(50)")]
        public string Site_Company_Code
        {
            get
            {
                return this._Site_Company_Code;
            }
            set
            {
                if ((this._Site_Company_Code != value))
                {
                    this._Site_Company_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Phone_No", DbType = "VarChar(15)")]
        public string Site_Phone_No
        {
            get
            {
                return this._Site_Phone_No;
            }
            set
            {
                if ((this._Site_Phone_No != value))
                {
                    this._Site_Phone_No = value;
                }
            }
        }

        [Column(Storage = "_Site_Fax_No", DbType = "VarChar(15)")]
        public string Site_Fax_No
        {
            get
            {
                return this._Site_Fax_No;
            }
            set
            {
                if ((this._Site_Fax_No != value))
                {
                    this._Site_Fax_No = value;
                }
            }
        }

        [Column(Storage = "_Site_Email_Address", DbType = "VarChar(100)")]
        public string Site_Email_Address
        {
            get
            {
                return this._Site_Email_Address;
            }
            set
            {
                if ((this._Site_Email_Address != value))
                {
                    this._Site_Email_Address = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
        public string Site_Address_1
        {
            get
            {
                return this._Site_Address_1;
            }
            set
            {
                if ((this._Site_Address_1 != value))
                {
                    this._Site_Address_1 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
        public string Site_Address_2
        {
            get
            {
                return this._Site_Address_2;
            }
            set
            {
                if ((this._Site_Address_2 != value))
                {
                    this._Site_Address_2 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
        public string Site_Address_3
        {
            get
            {
                return this._Site_Address_3;
            }
            set
            {
                if ((this._Site_Address_3 != value))
                {
                    this._Site_Address_3 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
        public string Site_Address_4
        {
            get
            {
                return this._Site_Address_4;
            }
            set
            {
                if ((this._Site_Address_4 != value))
                {
                    this._Site_Address_4 = value;
                }
            }
        }

        [Column(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
        public string Site_Address_5
        {
            get
            {
                return this._Site_Address_5;
            }
            set
            {
                if ((this._Site_Address_5 != value))
                {
                    this._Site_Address_5 = value;
                }
            }
        }

        [Column(Storage = "_Site_Postcode", DbType = "VarChar(15)")]
        public string Site_Postcode
        {
            get
            {
                return this._Site_Postcode;
            }
            set
            {
                if ((this._Site_Postcode != value))
                {
                    this._Site_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Site_Manager", DbType = "VarChar(50)")]
        public string Site_Manager
        {
            get
            {
                return this._Site_Manager;
            }
            set
            {
                if ((this._Site_Manager != value))
                {
                    this._Site_Manager = value;
                }
            }
        }

        [Column(Storage = "_Site_Price_Per_Play_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_Price_Per_Play_Default
        {
            get
            {
                return this._Site_Price_Per_Play_Default;
            }
            set
            {
                if ((this._Site_Price_Per_Play_Default != value))
                {
                    this._Site_Price_Per_Play_Default = value;
                }
            }
        }

        [Column(Storage = "_Site_Price_Per_Play", DbType = "VarChar(50)")]
        public string Site_Price_Per_Play
        {
            get
            {
                return this._Site_Price_Per_Play;
            }
            set
            {
                if ((this._Site_Price_Per_Play != value))
                {
                    this._Site_Price_Per_Play = value;
                }
            }
        }

        [Column(Storage = "_Site_Jackpot_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_Jackpot_Default
        {
            get
            {
                return this._Site_Jackpot_Default;
            }
            set
            {
                if ((this._Site_Jackpot_Default != value))
                {
                    this._Site_Jackpot_Default = value;
                }
            }
        }

        [Column(Storage = "_Site_Jackpot", DbType = "VarChar(50)")]
        public string Site_Jackpot
        {
            get
            {
                return this._Site_Jackpot;
            }
            set
            {
                if ((this._Site_Jackpot != value))
                {
                    this._Site_Jackpot = value;
                }
            }
        }

        [Column(Storage = "_Site_Percentage_Payout_Default", DbType = "Bit")]
        public System.Nullable<bool> Site_Percentage_Payout_Default
        {
            get
            {
                return this._Site_Percentage_Payout_Default;
            }
            set
            {
                if ((this._Site_Percentage_Payout_Default != value))
                {
                    this._Site_Percentage_Payout_Default = value;
                }
            }
        }

        [Column(Storage = "_Site_Percentage_Payout", DbType = "VarChar(50)")]
        public string Site_Percentage_Payout
        {
            get
            {
                return this._Site_Percentage_Payout;
            }
            set
            {
                if ((this._Site_Percentage_Payout != value))
                {
                    this._Site_Percentage_Payout = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_ID
        {
            get
            {
                return this._Sub_Company_ID;
            }
            set
            {
                if ((this._Sub_Company_ID != value))
                {
                    this._Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Company_ID", DbType = "Int")]
        public System.Nullable<int> Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Terms_Group_ID_Default
        {
            get
            {
                return this._Terms_Group_ID_Default;
            }
            set
            {
                if ((this._Terms_Group_ID_Default != value))
                {
                    this._Terms_Group_ID_Default = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int")]
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

        [Column(Storage = "_Access_Key_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Access_Key_ID_Default
        {
            get
            {
                return this._Access_Key_ID_Default;
            }
            set
            {
                if ((this._Access_Key_ID_Default != value))
                {
                    this._Access_Key_ID_Default = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int")]
        public System.Nullable<int> Access_Key_ID
        {
            get
            {
                return this._Access_Key_ID;
            }
            set
            {
                if ((this._Access_Key_ID != value))
                {
                    this._Access_Key_ID = value;
                }
            }
        }

        [Column(Storage = "_Staff_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Staff_ID_Default
        {
            get
            {
                return this._Staff_ID_Default;
            }
            set
            {
                if ((this._Staff_ID_Default != value))
                {
                    this._Staff_ID_Default = value;
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

        [Column(Storage = "_Site_Invoice_Name", DbType = "VarChar(50)")]
        public string Site_Invoice_Name
        {
            get
            {
                return this._Site_Invoice_Name;
            }
            set
            {
                if ((this._Site_Invoice_Name != value))
                {
                    this._Site_Invoice_Name = value;
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Invoice_Address
        {
            get
            {
                return this._Site_Invoice_Address;
            }
            set
            {
                if ((this._Site_Invoice_Address != value))
                {
                    this._Site_Invoice_Address = value;
                }
            }
        }

        [Column(Storage = "_Site_Invoice_Postcode", DbType = "VarChar(15)")]
        public string Site_Invoice_Postcode
        {
            get
            {
                return this._Site_Invoice_Postcode;
            }
            set
            {
                if ((this._Site_Invoice_Postcode != value))
                {
                    this._Site_Invoice_Postcode = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Region_ID
        {
            get
            {
                return this._Sub_Company_Region_ID;
            }
            set
            {
                if ((this._Sub_Company_Region_ID != value))
                {
                    this._Sub_Company_Region_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_Area_ID
        {
            get
            {
                return this._Sub_Company_Area_ID;
            }
            set
            {
                if ((this._Sub_Company_Area_ID != value))
                {
                    this._Sub_Company_Area_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_ID", DbType = "Int")]
        public System.Nullable<int> Sub_Company_District_ID
        {
            get
            {
                return this._Sub_Company_District_ID;
            }
            set
            {
                if ((this._Sub_Company_District_ID != value))
                {
                    this._Sub_Company_District_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Image_Reference", DbType = "VarChar(255)")]
        public string Site_Image_Reference
        {
            get
            {
                return this._Site_Image_Reference;
            }
            set
            {
                if ((this._Site_Image_Reference != value))
                {
                    this._Site_Image_Reference = value;
                }
            }
        }

        [Column(Storage = "_Site_Image_Reference_2", DbType = "VarChar(255)")]
        public string Site_Image_Reference_2
        {
            get
            {
                return this._Site_Image_Reference_2;
            }
            set
            {
                if ((this._Site_Image_Reference_2 != value))
                {
                    this._Site_Image_Reference_2 = value;
                }
            }
        }

        [Column(Storage = "_Site_Closed", DbType = "Int")]
        public System.Nullable<int> Site_Closed
        {
            get
            {
                return this._Site_Closed;
            }
            set
            {
                if ((this._Site_Closed != value))
                {
                    this._Site_Closed = value;
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

        [Column(Storage = "_Supplier_ID", DbType = "Int")]
        public System.Nullable<int> Supplier_ID
        {
            get
            {
                return this._Supplier_ID;
            }
            set
            {
                if ((this._Supplier_ID != value))
                {
                    this._Supplier_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Classification_ID", DbType = "Int NOT NULL")]
        public int Site_Classification_ID
        {
            get
            {
                return this._Site_Classification_ID;
            }
            set
            {
                if ((this._Site_Classification_ID != value))
                {
                    this._Site_Classification_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Grade", DbType = "VarChar(10)")]
        public string Site_Grade
        {
            get
            {
                return this._Site_Grade;
            }
            set
            {
                if ((this._Site_Grade != value))
                {
                    this._Site_Grade = value;
                }
            }
        }

        [Column(Storage = "_Sage_Account_Ref", DbType = "VarChar(50)")]
        public string Sage_Account_Ref
        {
            get
            {
                return this._Sage_Account_Ref;
            }
            set
            {
                if ((this._Sage_Account_Ref != value))
                {
                    this._Sage_Account_Ref = value;
                }
            }
        }

        [Column(Storage = "_Site_Is_FreeFloat", DbType = "Bit")]
        public System.Nullable<bool> Site_Is_FreeFloat
        {
            get
            {
                return this._Site_Is_FreeFloat;
            }
            set
            {
                if ((this._Site_Is_FreeFloat != value))
                {
                    this._Site_Is_FreeFloat = value;
                }
            }
        }

        [Column(Storage = "_Site_Local_Inbox", DbType = "VarChar(100)")]
        public string Site_Local_Inbox
        {
            get
            {
                return this._Site_Local_Inbox;
            }
            set
            {
                if ((this._Site_Local_Inbox != value))
                {
                    this._Site_Local_Inbox = value;
                }
            }
        }

        [Column(Storage = "_Site_Local_Outbox", DbType = "VarChar(100)")]
        public string Site_Local_Outbox
        {
            get
            {
                return this._Site_Local_Outbox;
            }
            set
            {
                if ((this._Site_Local_Outbox != value))
                {
                    this._Site_Local_Outbox = value;
                }
            }
        }

        [Column(Storage = "_Site_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Site_Memo
        {
            get
            {
                return this._Site_Memo;
            }
            set
            {
                if ((this._Site_Memo != value))
                {
                    this._Site_Memo = value;
                }
            }
        }

        [Column(Storage = "_Site_Reference", DbType = "VarChar(50)")]
        public string Site_Reference
        {
            get
            {
                return this._Site_Reference;
            }
            set
            {
                if ((this._Site_Reference != value))
                {
                    this._Site_Reference = value;
                }
            }
        }

        [Column(Storage = "_Site_Trade_Type", DbType = "VarChar(50)")]
        public string Site_Trade_Type
        {
            get
            {
                return this._Site_Trade_Type;
            }
            set
            {
                if ((this._Site_Trade_Type != value))
                {
                    this._Site_Trade_Type = value;
                }
            }
        }

        [Column(Storage = "_Site_Non_Trading_Period_From", DbType = "VarChar(30)")]
        public string Site_Non_Trading_Period_From
        {
            get
            {
                return this._Site_Non_Trading_Period_From;
            }
            set
            {
                if ((this._Site_Non_Trading_Period_From != value))
                {
                    this._Site_Non_Trading_Period_From = value;
                }
            }
        }

        [Column(Storage = "_Site_Non_Trading_Period_To", DbType = "VarChar(30)")]
        public string Site_Non_Trading_Period_To
        {
            get
            {
                return this._Site_Non_Trading_Period_To;
            }
            set
            {
                if ((this._Site_Non_Trading_Period_To != value))
                {
                    this._Site_Non_Trading_Period_To = value;
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Service_Area", DbType = "VarChar(50)")]
        public string Site_Supplier_Service_Area
        {
            get
            {
                return this._Site_Supplier_Service_Area;
            }
            set
            {
                if ((this._Site_Supplier_Service_Area != value))
                {
                    this._Site_Supplier_Service_Area = value;
                }
            }
        }

        [Column(Storage = "_Site_Supplier_Area", DbType = "VarChar(50)")]
        public string Site_Supplier_Area
        {
            get
            {
                return this._Site_Supplier_Area;
            }
            set
            {
                if ((this._Site_Supplier_Area != value))
                {
                    this._Site_Supplier_Area = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
        public System.Nullable<int> Standard_Opening_Hours_ID
        {
            get
            {
                return this._Standard_Opening_Hours_ID;
            }
            set
            {
                if ((this._Standard_Opening_Hours_ID != value))
                {
                    this._Standard_Opening_Hours_ID = value;
                }
            }
        }

        [Column(Storage = "_Next_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Next_Sub_Company_ID
        {
            get
            {
                return this._Next_Sub_Company_ID;
            }
            set
            {
                if ((this._Next_Sub_Company_ID != value))
                {
                    this._Next_Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Next_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Next_Sub_Company_Change_Date
        {
            get
            {
                return this._Next_Sub_Company_Change_Date;
            }
            set
            {
                if ((this._Next_Sub_Company_Change_Date != value))
                {
                    this._Next_Sub_Company_Change_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_Previous_Sub_Company_ID", DbType = "Int")]
        public System.Nullable<int> Site_Previous_Sub_Company_ID
        {
            get
            {
                return this._Site_Previous_Sub_Company_ID;
            }
            set
            {
                if ((this._Site_Previous_Sub_Company_ID != value))
                {
                    this._Site_Previous_Sub_Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Previous_Sub_Company_Change_Date", DbType = "VarChar(30)")]
        public string Previous_Sub_Company_Change_Date
        {
            get
            {
                return this._Previous_Sub_Company_Change_Date;
            }
            set
            {
                if ((this._Previous_Sub_Company_Change_Date != value))
                {
                    this._Previous_Sub_Company_Change_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_Honeyframe_EDI", DbType = "Int")]
        public System.Nullable<int> Site_Honeyframe_EDI
        {
            get
            {
                return this._Site_Honeyframe_EDI;
            }
            set
            {
                if ((this._Site_Honeyframe_EDI != value))
                {
                    this._Site_Honeyframe_EDI = value;
                }
            }
        }

        [Column(Storage = "_Site_Datapak_Protocol", DbType = "Int")]
        public System.Nullable<int> Site_Datapak_Protocol
        {
            get
            {
                return this._Site_Datapak_Protocol;
            }
            set
            {
                if ((this._Site_Datapak_Protocol != value))
                {
                    this._Site_Datapak_Protocol = value;
                }
            }
        }

        [Column(Storage = "_Site_Start_Date", DbType = "VarChar(30)")]
        public string Site_Start_Date
        {
            get
            {
                return this._Site_Start_Date;
            }
            set
            {
                if ((this._Site_Start_Date != value))
                {
                    this._Site_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_End_Date", DbType = "VarChar(30)")]
        public string Site_End_Date
        {
            get
            {
                return this._Site_End_Date;
            }
            set
            {
                if ((this._Site_End_Date != value))
                {
                    this._Site_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_Licence_Number", DbType = "VarChar(25)")]
        public string Site_Licence_Number
        {
            get
            {
                return this._Site_Licence_Number;
            }
            set
            {
                if ((this._Site_Licence_Number != value))
                {
                    this._Site_Licence_Number = value;
                }
            }
        }

        [Column(Storage = "_Site_Fiscal_Code", DbType = "VarChar(16)")]
        public string Site_Fiscal_Code
        {
            get
            {
                return this._Site_Fiscal_Code;
            }
            set
            {
                if ((this._Site_Fiscal_Code != value))
                {
                    this._Site_Fiscal_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Street_Number", DbType = "VarChar(15)")]
        public string Site_Street_Number
        {
            get
            {
                return this._Site_Street_Number;
            }
            set
            {
                if ((this._Site_Street_Number != value))
                {
                    this._Site_Street_Number = value;
                }
            }
        }

        [Column(Storage = "_Site_Province", DbType = "VarChar(15)")]
        public string Site_Province
        {
            get
            {
                return this._Site_Province;
            }
            set
            {
                if ((this._Site_Province != value))
                {
                    this._Site_Province = value;
                }
            }
        }

        [Column(Storage = "_Site_Municipality", DbType = "VarChar(40)")]
        public string Site_Municipality
        {
            get
            {
                return this._Site_Municipality;
            }
            set
            {
                if ((this._Site_Municipality != value))
                {
                    this._Site_Municipality = value;
                }
            }
        }

        [Column(Storage = "_Site_Cadastral_Code", DbType = "VarChar(15)")]
        public string Site_Cadastral_Code
        {
            get
            {
                return this._Site_Cadastral_Code;
            }
            set
            {
                if ((this._Site_Cadastral_Code != value))
                {
                    this._Site_Cadastral_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Area", DbType = "Int")]
        public System.Nullable<int> Site_Area
        {
            get
            {
                return this._Site_Area;
            }
            set
            {
                if ((this._Site_Area != value))
                {
                    this._Site_Area = value;
                }
            }
        }

        [Column(Storage = "_Site_Location_Type", DbType = "Int")]
        public System.Nullable<int> Site_Location_Type
        {
            get
            {
                return this._Site_Location_Type;
            }
            set
            {
                if ((this._Site_Location_Type != value))
                {
                    this._Site_Location_Type = value;
                }
            }
        }

        [Column(Storage = "_Site_Toponym", DbType = "Int")]
        public System.Nullable<int> Site_Toponym
        {
            get
            {
                return this._Site_Toponym;
            }
            set
            {
                if ((this._Site_Toponym != value))
                {
                    this._Site_Toponym = value;
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Commenced_Date", DbType = "SmallDateTime")]
        public System.Nullable<System.DateTime> Site_Licensee_Commenced_Date
        {
            get
            {
                return this._Site_Licensee_Commenced_Date;
            }
            set
            {
                if ((this._Site_Licensee_Commenced_Date != value))
                {
                    this._Site_Licensee_Commenced_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Agreement_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Site_Licensee_Agreement_End_Date
        {
            get
            {
                return this._Site_Licensee_Agreement_End_Date;
            }
            set
            {
                if ((this._Site_Licensee_Agreement_End_Date != value))
                {
                    this._Site_Licensee_Agreement_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_Licensee_Agreement_Type", DbType = "VarChar(50)")]
        public string Site_Licensee_Agreement_Type
        {
            get
            {
                return this._Site_Licensee_Agreement_Type;
            }
            set
            {
                if ((this._Site_Licensee_Agreement_Type != value))
                {
                    this._Site_Licensee_Agreement_Type = value;
                }
            }
        }

        [Column(Storage = "_Site_Application", DbType = "SmallInt")]
        public System.Nullable<short> Site_Application
        {
            get
            {
                return this._Site_Application;
            }
            set
            {
                if ((this._Site_Application != value))
                {
                    this._Site_Application = value;
                }
            }
        }

        [Column(Storage = "_Region", DbType = "VarChar(10) NOT NULL", CanBeNull = false)]
        public string Region
        {
            get
            {
                return this._Region;
            }
            set
            {
                if ((this._Region != value))
                {
                    this._Region = value;
                }
            }
        }

        [Column(Storage = "_WebURL", DbType = "VarChar(2000) NOT NULL", CanBeNull = false)]
        public string WebURL
        {
            get
            {
                return this._WebURL;
            }
            set
            {
                if ((this._WebURL != value))
                {
                    this._WebURL = value;
                }
            }
        }

        [Column(Storage = "_Site_Status_ID", DbType = "Int NOT NULL")]
        public int Site_Status_ID
        {
            get
            {
                return this._Site_Status_ID;
            }
            set
            {
                if ((this._Site_Status_ID != value))
                {
                    this._Site_Status_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Inactive_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Site_Inactive_Date
        {
            get
            {
                return this._Site_Inactive_Date;
            }
            set
            {
                if ((this._Site_Inactive_Date != value))
                {
                    this._Site_Inactive_Date = value;
                }
            }
        }

        [Column(Storage = "_Site_Connection_IPAddress", DbType = "VarChar(15)")]
        public string Site_Connection_IPAddress
        {
            get
            {
                return this._Site_Connection_IPAddress;
            }
            set
            {
                if ((this._Site_Connection_IPAddress != value))
                {
                    this._Site_Connection_IPAddress = value;
                }
            }
        }

        [Column(Storage = "_Site_MaxNumber_VLT", DbType = "Int")]
        public System.Nullable<int> Site_MaxNumber_VLT
        {
            get
            {
                return this._Site_MaxNumber_VLT;
            }
            set
            {
                if ((this._Site_MaxNumber_VLT != value))
                {
                    this._Site_MaxNumber_VLT = value;
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

        [Column(Storage = "_StackerLimitPercentage", DbType = "Int")]
        public System.Nullable<int> StackerLimitPercentage
        {
            get
            {
                return this._StackerLimitPercentage;
            }
            set
            {
                if ((this._StackerLimitPercentage != value))
                {
                    this._StackerLimitPercentage = value;
                }
            }
        }

        [Column(Storage = "_Site_Closed_Date", DbType = "VarChar(50)")]
        public string Site_Closed_Date
        {
            get
            {
                return this._Site_Closed_Date;
            }
            set
            {
                if ((this._Site_Closed_Date != value))
                {
                    this._Site_Closed_Date = value;
                }
            }
        }

        //[Column(Storage="_Site_Enabled", DbType="Bit NOT NULL")]
        //public bool Site_Enabled
        //{
        //    get
        //    {
        //        return this._Site_Enabled;
        //    }
        //    set
        //    {
        //        if ((this._Site_Enabled != value))
        //        {
        //            this._Site_Enabled = value;
        //        }
        //    }
        //}

        //[Column(Storage = "_IsTITOEnabled", DbType = "Int NOT NULL")]
        //public int IsTITOEnabled
        //{
        //    get
        //    {
        //        return this._IsTITOEnabled;
        //    }
        //    set
        //    {
        //        if ((this._IsTITOEnabled != value))
        //        {
        //            this._IsTITOEnabled = value;
        //        }
        //    }
        //}

        //[Column(Storage = "_IsNonCashVoucherEnabled", DbType = "Int NOT NULL")]
        //public int IsNonCashVoucherEnabled
        //{
        //    get
        //    {
        //        return this._IsNonCashVoucherEnabled;
        //    }
        //    set
        //    {
        //        if ((this._IsNonCashVoucherEnabled != value))
        //        {
        //            this._IsNonCashVoucherEnabled = value;
        //        }
        //    }
        //}
    }

    public partial class rsp_GetSiteStatusResult
    {

        private bool _Site_Enabled;

        private int _IsTITOEnabled;

        private int _IsCrossTicketingEnabled;

        private int _IsNonCashVoucherEnabled;

        public rsp_GetSiteStatusResult()
        {
        }

        [Column(Storage = "_Site_Enabled", DbType = "Bit NOT NULL")]
        public bool Site_Enabled
        {
            get
            {
                return this._Site_Enabled;
            }
            set
            {
                if ((this._Site_Enabled != value))
                {
                    this._Site_Enabled = value;
                }
            }
        }

        [Column(Storage = "_IsTITOEnabled", DbType = "Int NOT NULL")]
        public int IsTITOEnabled
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

        [Column(Storage = "_IsCrossTicketingEnabled", DbType = "Int NOT NULL")]
        public int IsCrossTicketingEnabled
        {
            get
            {
                return this._IsCrossTicketingEnabled;
            }
            set
            {
                if ((this._IsCrossTicketingEnabled != value))
                {
                    this._IsCrossTicketingEnabled = value;
                }
            }
        }

        [Column(Storage = "_IsNonCashVoucherEnabled", DbType = "Int NOT NULL")]
        public int IsNonCashVoucherEnabled
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
    }

    public partial class rsp_CheckSiteResult
    {

        private int _Site_ID;

        public rsp_CheckSiteResult()
        {
        }

        [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
        public int Site_ID
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

    public partial class rsp_GetDepotInfoResult
    {

        private string _Depot_Name;

        private int _Depot_ID;

        public rsp_GetDepotInfoResult()
        {
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

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
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
    }



    public partial class rsp_GetOperatorInfoResult
    {

        private string _Operator_Name;

        private int _Operator_ID;

        public rsp_GetOperatorInfoResult()
        {
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

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
        public int Operator_ID
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
    }

    public partial class rsp_GetSiteClassificationResult
    {

        private int _Site_Classification_ID;

        private string _Site_Classification_Name;

        public rsp_GetSiteClassificationResult()
        {
        }

        [Column(Storage = "_Site_Classification_ID", DbType = "Int NOT NULL")]
        public int Site_Classification_ID
        {
            get
            {
                return this._Site_Classification_ID;
            }
            set
            {
                if ((this._Site_Classification_ID != value))
                {
                    this._Site_Classification_ID = value;
                }
            }
        }

        [Column(Storage = "_Site_Classification_Name", DbType = "VarChar(50)")]
        public string Site_Classification_Name
        {
            get
            {
                return this._Site_Classification_Name;
            }
            set
            {
                if ((this._Site_Classification_Name != value))
                {
                    this._Site_Classification_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteClassifNameonIDResult
    {

        private string _Site_Classification_Name;

        public rsp_GetSiteClassifNameonIDResult()
        {
        }

        [Column(Storage = "_Site_Classification_Name", DbType = "VarChar(50)")]
        public string Site_Classification_Name
        {
            get
            {
                return this._Site_Classification_Name;
            }
            set
            {
                if ((this._Site_Classification_Name != value))
                {
                    this._Site_Classification_Name = value;
                }
            }
        }
    }

    public partial class rsp_ServiceOperatorResult
    {

        private string _Operator_Name;

        private int _Operator_ID;

        public rsp_ServiceOperatorResult()
        {
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

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
        public int Operator_ID
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
    }

    public partial class rsp_GetServiceAreaonDepotResult
    {

        private string _Service_Area_Name;

        private int _Service_Area_ID;

        public rsp_GetServiceAreaonDepotResult()
        {
        }

        [Column(Storage = "_Service_Area_Name", DbType = "VarChar(50)")]
        public string Service_Area_Name
        {
            get
            {
                return this._Service_Area_Name;
            }
            set
            {
                if ((this._Service_Area_Name != value))
                {
                    this._Service_Area_Name = value;
                }
            }
        }

        [Column(Storage = "_Service_Area_ID", DbType = "Int NOT NULL")]
        public int Service_Area_ID
        {
            get
            {
                return this._Service_Area_ID;
            }
            set
            {
                if ((this._Service_Area_ID != value))
                {
                    this._Service_Area_ID = value;
                }
            }
        }
    }

    public partial class rsp_GetStdOpeningHrsResult
    {

        private int _Standard_Opening_Hours_ID;

        private string _Standard_Opening_Hours_Description;

        public rsp_GetStdOpeningHrsResult()
        {
        }

        [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int NOT NULL")]
        public int Standard_Opening_Hours_ID
        {
            get
            {
                return this._Standard_Opening_Hours_ID;
            }
            set
            {
                if ((this._Standard_Opening_Hours_ID != value))
                {
                    this._Standard_Opening_Hours_ID = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Description", DbType = "VarChar(50)")]
        public string Standard_Opening_Hours_Description
        {
            get
            {
                return this._Standard_Opening_Hours_Description;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Description != value))
                {
                    this._Standard_Opening_Hours_Description = value;
                }
            }
        }
    }

    public partial class usp_CheckSiteTicketingURLResult
    {

        private string _TicketingURL;

        public usp_CheckSiteTicketingURLResult()
        {
        }

        [Column(Storage = "_TicketingURL", DbType = "VarChar(2000)")]
        public string TicketingURL
        {
            get
            {
                return this._TicketingURL;
            }
            set
            {
                if ((this._TicketingURL != value))
                {
                    this._TicketingURL = value;
                }
            }
        }
    }

    public partial class rsp_GetActiveInstallationsForSiteResult
    {

        private System.Nullable<int> _Bar_Position_ID;

        public rsp_GetActiveInstallationsForSiteResult()
        {
        }

        [Column(Storage = "_Bar_Position_ID", DbType = "Int")]
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
    }

    public partial class rsp_GetSiteOpeningHoursResult
    {

        private string _Site_Name;

        private string _Site_Open_Monday;

        private string _Site_Open_Tuesday;

        private string _Site_Open_Wednesday;

        private string _Site_Open_Thursday;

        private string _Site_Open_Friday;

        private string _Site_Open_Saturday;

        private string _Site_Open_Sunday;

        public rsp_GetSiteOpeningHoursResult()
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

        [Column(Storage = "_Site_Open_Monday", DbType = "VarChar(96)")]
        public string Site_Open_Monday
        {
            get
            {
                return this._Site_Open_Monday;
            }
            set
            {
                if ((this._Site_Open_Monday != value))
                {
                    this._Site_Open_Monday = value;
                }
            }
        }

        [Column(Storage = "_Site_Open_Tuesday", DbType = "VarChar(96)")]
        public string Site_Open_Tuesday
        {
            get
            {
                return this._Site_Open_Tuesday;
            }
            set
            {
                if ((this._Site_Open_Tuesday != value))
                {
                    this._Site_Open_Tuesday = value;
                }
            }
        }

        [Column(Storage = "_Site_Open_Wednesday", DbType = "VarChar(96)")]
        public string Site_Open_Wednesday
        {
            get
            {
                return this._Site_Open_Wednesday;
            }
            set
            {
                if ((this._Site_Open_Wednesday != value))
                {
                    this._Site_Open_Wednesday = value;
                }
            }
        }

        [Column(Storage = "_Site_Open_Thursday", DbType = "VarChar(96)")]
        public string Site_Open_Thursday
        {
            get
            {
                return this._Site_Open_Thursday;
            }
            set
            {
                if ((this._Site_Open_Thursday != value))
                {
                    this._Site_Open_Thursday = value;
                }
            }
        }

        [Column(Storage = "_Site_Open_Friday", DbType = "VarChar(96)")]
        public string Site_Open_Friday
        {
            get
            {
                return this._Site_Open_Friday;
            }
            set
            {
                if ((this._Site_Open_Friday != value))
                {
                    this._Site_Open_Friday = value;
                }
            }
        }

        [Column(Storage = "_Site_Open_Saturday", DbType = "VarChar(96)")]
        public string Site_Open_Saturday
        {
            get
            {
                return this._Site_Open_Saturday;
            }
            set
            {
                if ((this._Site_Open_Saturday != value))
                {
                    this._Site_Open_Saturday = value;
                }
            }
        }

        [Column(Storage = "_Site_Open_Sunday", DbType = "VarChar(96)")]
        public string Site_Open_Sunday
        {
            get
            {
                return this._Site_Open_Sunday;
            }
            set
            {
                if ((this._Site_Open_Sunday != value))
                {
                    this._Site_Open_Sunday = value;
                }
            }
        }
    }

    public partial class rsp_GetStdOpeningHrsDetailsResult
    {

        private string _Standard_Opening_Hours_Description;

        private string _Standard_Opening_Hours_Open_Monday;

        private string _Standard_Opening_Hours_Open_Tuesday;

        private string _Standard_Opening_Hours_Open_Wednesday;

        private string _Standard_Opening_Hours_Open_Thursday;

        private string _Standard_Opening_Hours_Open_Friday;

        private string _Standard_Opening_Hours_Open_Saturday;

        private string _Standard_Opening_Hours_Open_Sunday;

        public rsp_GetStdOpeningHrsDetailsResult()
        {
        }

        [Column(Storage = "_Standard_Opening_Hours_Description", DbType = "VarChar(50)")]
        public string Standard_Opening_Hours_Description
        {
            get
            {
                return this._Standard_Opening_Hours_Description;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Description != value))
                {
                    this._Standard_Opening_Hours_Description = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Monday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Monday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Monday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Monday != value))
                {
                    this._Standard_Opening_Hours_Open_Monday = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Tuesday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Tuesday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Tuesday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Tuesday != value))
                {
                    this._Standard_Opening_Hours_Open_Tuesday = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Wednesday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Wednesday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Wednesday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Wednesday != value))
                {
                    this._Standard_Opening_Hours_Open_Wednesday = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Thursday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Thursday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Thursday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Thursday != value))
                {
                    this._Standard_Opening_Hours_Open_Thursday = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Friday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Friday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Friday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Friday != value))
                {
                    this._Standard_Opening_Hours_Open_Friday = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Saturday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Saturday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Saturday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Saturday != value))
                {
                    this._Standard_Opening_Hours_Open_Saturday = value;
                }
            }
        }

        [Column(Storage = "_Standard_Opening_Hours_Open_Sunday", DbType = "VarChar(96)")]
        public string Standard_Opening_Hours_Open_Sunday
        {
            get
            {
                return this._Standard_Opening_Hours_Open_Sunday;
            }
            set
            {
                if ((this._Standard_Opening_Hours_Open_Sunday != value))
                {
                    this._Standard_Opening_Hours_Open_Sunday = value;
                }
            }
        }
    }

    public partial class rsp_GetStaffCustomerAccessOperatorResult
    {

        private string _Operator_Name;

        private int _Operator_ID;

        public rsp_GetStaffCustomerAccessOperatorResult()
        {
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

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
        public int Operator_ID
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
    }

    public partial class rsp_GetCustomerAccessDepotonOperatorResult
    {

        private string _Depot_Name;

        private int _Depot_ID;

        public rsp_GetCustomerAccessDepotonOperatorResult()
        {
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

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
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
    }

    public partial class rsp_GetStaffCustomerAccessServiceOperatorResult
    {

        private string _Operator_Name;

        private int _Operator_ID;

        public rsp_GetStaffCustomerAccessServiceOperatorResult()
        {
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

        [Column(Storage = "_Operator_ID", DbType = "Int NOT NULL")]
        public int Operator_ID
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
    }

    public partial class rsp_GetZoneOpeningHoursResult
    {

        private string _Zone_Name;

        private string _Zone_Open_Monday;

        private string _Zone_Open_Tuesday;

        private string _Zone_Open_Wednesday;

        private string _Zone_Open_Thursday;

        private string _Zone_Open_Friday;

        private string _Zone_Open_Saturday;

        private string _Zone_Open_Sunday;

        public rsp_GetZoneOpeningHoursResult()
        {
        }

        [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
        public string Zone_Name
        {
            get
            {
                return this._Zone_Name;
            }
            set
            {
                if ((this._Zone_Name != value))
                {
                    this._Zone_Name = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Monday", DbType = "VarChar(96)")]
        public string Zone_Open_Monday
        {
            get
            {
                return this._Zone_Open_Monday;
            }
            set
            {
                if ((this._Zone_Open_Monday != value))
                {
                    this._Zone_Open_Monday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Tuesday", DbType = "VarChar(96)")]
        public string Zone_Open_Tuesday
        {
            get
            {
                return this._Zone_Open_Tuesday;
            }
            set
            {
                if ((this._Zone_Open_Tuesday != value))
                {
                    this._Zone_Open_Tuesday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Wednesday", DbType = "VarChar(96)")]
        public string Zone_Open_Wednesday
        {
            get
            {
                return this._Zone_Open_Wednesday;
            }
            set
            {
                if ((this._Zone_Open_Wednesday != value))
                {
                    this._Zone_Open_Wednesday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Thursday", DbType = "VarChar(96)")]
        public string Zone_Open_Thursday
        {
            get
            {
                return this._Zone_Open_Thursday;
            }
            set
            {
                if ((this._Zone_Open_Thursday != value))
                {
                    this._Zone_Open_Thursday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Friday", DbType = "VarChar(96)")]
        public string Zone_Open_Friday
        {
            get
            {
                return this._Zone_Open_Friday;
            }
            set
            {
                if ((this._Zone_Open_Friday != value))
                {
                    this._Zone_Open_Friday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Saturday", DbType = "VarChar(96)")]
        public string Zone_Open_Saturday
        {
            get
            {
                return this._Zone_Open_Saturday;
            }
            set
            {
                if ((this._Zone_Open_Saturday != value))
                {
                    this._Zone_Open_Saturday = value;
                }
            }
        }

        [Column(Storage = "_Zone_Open_Sunday", DbType = "VarChar(96)")]
        public string Zone_Open_Sunday
        {
            get
            {
                return this._Zone_Open_Sunday;
            }
            set
            {
                if ((this._Zone_Open_Sunday != value))
                {
                    this._Zone_Open_Sunday = value;
                }
            }
        }
    }

    public partial class rsp_GetRepresentativeonSiteResult
    {

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        private int _Staff_ID;

        public rsp_GetRepresentativeonSiteResult()
        {
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
    }

    public partial class rsp_CheckIsExchangeKeyExistsResult
    {

        private System.Nullable<bool> _IsExchangeKeyAvailable;

        public rsp_CheckIsExchangeKeyExistsResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsExchangeKeyAvailable", DbType = "Bit")]
        public System.Nullable<bool> IsExchangeKeyAvailable
        {
            get
            {
                return this._IsExchangeKeyAvailable;
            }
            set
            {
                if ((this._IsExchangeKeyAvailable != value))
                {
                    this._IsExchangeKeyAvailable = value;
                }
            }
        }
    }
}