using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.DBInterface.ExchangeConfig
{
    public static class DBConstants
    {
        public const string RSP_GETSETTING = "rsp_getsetting";
        public const string RSP_GETINITIALSETTING = "rsp_GetInitialSettings";
        public const string CMPCONNECTIONSETTING = "EPIGatewayConnstr";       
        public const string TICKETLOCATIONCODENAME = "TICKET_LOCATION_CODE";
        public const string SPEDITSETTING = "usp_EditSetting";
        public const string SP_PARAM_SETTINGID=" @Setting_ID";
        public const string SP_PARAM_SETTINGVALUE = "@Setting_Value";
        public const string SP_PARAM_SETTINGNAME = "@Setting_Name";
        public const string SP_PARAM_SETTINGDESCRIPTION="@Setting_Description";
        public const string TICKETINGCONNECTIONSETTING = "Ticketing.Connection";
        public const string ClientName = "Client";
        public const string SP_TIS_SETTING = "usp_InsertUpdateTISSetting";
         public const string RSP_GETGRIDVIEWCOLORRANGEDETAILS = "rsp_GetGridViewColorRangeDetails";
        public const string RSP_GETGRIDVIEWTYPEDETAILS = "rsp_GetGridViewTypeDetails";
        public const string USP_INSERTORUPDATEGRIDVIEWCOLORRANGEDETAILS = "usp_InsertOrUpdateGridViewColorRangeDetails";
        public const string USP_DELETEGRIDVIEWCOLORRANGEDETAILS = "usp_DeleteGridViewColorRangeDetails";
    }
}
