using System;

namespace BMC.ExchangeTicketExportService
{
    public static class Constants
    {
        public const string CONSTANT_RSP_CHECKTICKETEXPORTRECORDS = "rsp_CheckTicketExportRecords";
        public const string CONSTANT_RSP_GETTICKETEXPORTRECORDS = "rsp_GetTicketExportRecords";
        public const string CONSTANT_RSP_GETVOUCHERDETAILSTOEXPORT = "rsp_GetVoucherDetailsToExport";
        public const string CONSTANT_RSP_GETDEVICEDETAILSTOEXPORT = "rsp_GetDeviceDetailsToExport";
        public const string CONSTANT_RSP_GETTICKETEXCEPTIONDETAILSTOEXPORT = "rsp_GetTicketExceptionDetailsToExport";
        public const string CONSTANT_RSP_CHECKTICKETEXPORTTABLE = "rsp_CheckTicketExportTable";
        public const string CONSTANT_USP_UPDATETICKETEXPORTSTATUS = "usp_UpdateTicketExportStatus";
        public const string CONSTANT_REGISTRYPATH = "RegistryPath";
        public const string CONSTANT_RSP_CHECKCVEXPORTRECORDS = "rsp_CheckCVExportRecords";
        public const string CONSTANT_RSP_GETCVEXPORTRECORDS = "rsp_GetCVExportRecords";
        public const string CONSTANT_USP_UPDATECVEXPORTSTATUS = "usp_UpdateCVExportStatus";
        public const string RSP_GETSETTING = "rsp_GetSetting";
        public const string CONSTANT_RSP_GETPROMOTIONSDETAILSTOEXPORT = "rsp_GetPromotionsDetails";
        public const string CONSTANT_RSP_GETPROMOTIONALTICKETSDETAILSTOEXPORT = "rsp_GetPromotionalTicketDetails";
        public const string CONSTANT_RSP_GETTISPROMOTIONALTICKETSDETAILSTOEXPORT = "rsp_GetTISPromotionalTicketDetailsToExport";
       
        public const string CONSTANT_RSP_GETEXTERNALVOUCHERDETAILSTOEXPORT = "rsp_GetExternalVoucherDetailsToExport";

        //Parameter for rsp_getSetting
        public const string CONST_SP_PARAM_SETTINGID = "@Setting_ID";
        public const string CONST_SP_PARAM_SETTINGNAME = "@Setting_Name";
        public const string CONST_SP_PARAM_SETTINGDEFAULT = "@Setting_Default";
        public const string CONST_SP_PARAM_SETTINGVALUE = "@Setting_Value";
        public const string CONST_SP_PARAM_RETURNVALUE = "@RETURN_VALUE";
    }
}
