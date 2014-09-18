using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.DBInterface.CashDeskOperator
{
   public class DBConstants
   {
       public const string CONST_SP_GET_USER_ROLE_PROC = "rsp_getUserRoles";
       public const string CONST_PARAM_USER_NAME = "@UserName";
       public const string CONST_PARAM_USER_PASSWORD = "@UserPassword";
       public const string CONSTANT_RSP_GETINITIALSETTINGS = "rsp_GetInitialSettings";
       public const string CONSTANT_USP_SETSETTINGSVALUE = "usp_SetSettingsValue";
       public const string CONSTANT_RSP_GETASSETDETAILS = "rsp_GetAssetDetails";
       public const string CONSTANT_USP_SETTICKETEXPIRE = "usp_SetTicketExpire";
       public const string CONSTANT_USP_SETGMUSITECODESTATUS = "usp_UpdateGMUSiteCodeStatus";
       public const string CONSTANT_RSP_GETASSETPOSDETAILS = "rsp_GetAssetPosDetails";
       public const string CONSTANT_RSP_GETAPPSETTINGS = "rsp_GetAppSettings";
       public const string CONSTANT_USP_APPSETTINGS_SORTORDER = "usp_Update_FlrViw_BarPositionSortOrder";
       
       #region Issue Ticket Section
       /// <summary>
       /// Constants & Variables used in Issue Ticket functions
       /// </summary>
       public const string CONST_SP_GETSETTING = "rsp_getSetting";
       public const string CONST_SP_PARAM_TICKETCONNECTION = "Ticketing.Connection";
       public const string CONST_SP_CREATETICKETSTART = "usp_CreateTicketStart";
       public const string CONST_SP_CREATETICKETNUMBER = "usp_CreateTicketNumber";
       public const string CONST_SP_PARAM_REGION = "REGION";      
       public const string CONST_SP_PARAM_VOUCHERSITENAME = "Voucher_Site_Name";
       public const string CONST_SP_CREATETICKETCOMPLETE = "esp_CreateTicketComplete";
       public const string CONST_SP_INSERTTICKETISSUE = "usp_InsertTicketIssue";
       public const string CONST_SP_PARAM_ENABLEISSUERECEIPT = "TicketValidate_EnableIssueReceipt";
       public const string CONST_SP_BGSVOUCHERINFORMATION = "rsp_BGS_VoucherInformation";
       public const string CONST_SP_GETMACHINEDETAILSFROMASSET= "rsp_GetMachineDetailsFromAsset";

       //Columns returned from the SP rsp_BGS_VoucherInformation from Ticketing DB

       public const string CONST_SP_RESULT_PRINTDEVICE="PrintDevice";
       public const string CONST_SP_RESULT_PAYDEVICE="PayDevice";
       public const string CONST_SP_RESULT_BARCODE="strBarcode";
       public const string CONST_SP_RESULT_AMOUNT="iAmount";
       public const string CONST_SP_RESULT_VOUCHERSTATUS="strVoucherStatus";
       public const string CONST_SP_RESULT_DATEPAID="dtPaid";
       public const string CONST_SP_RESULT_DATEPRINTED="dtPrinted";
       public const string CONST_SP_RESULT_DEVICETYPE="strDeviceType";

       //Columns returned from the SP rsp_GetMachineDetailsFromAsset from Exchange DB

       public const string CONST_SP_RESULT_BARPOSITIONNAME = "Bar_Pos_Name";
       public const string CONST_SP_RESULT_STOCKNUMBER = "Stock_No";
       public const string CONST_SP_RESULT_INSTALLATIONNUMBER = "Installation_No";
       public const string CONST_SP_RESULT_MACHINENAME = "Name";


        //Parameter for rsp_getSetting
       public const string CONST_SP_PARAM_SMACHINENAME = "@sMachine";

       //Parameter for rsp_getSetting
       public const string CONST_SP_PARAM_SETTINGID = "@Setting_ID";
       public const string CONST_SP_PARAM_SETTINGNAME = "@Setting_Name";
       public const string CONST_SP_PARAM_SETTINGDEFAULT = "@Setting_Default";
       public const string CONST_SP_PARAM_SETTINGVALUE = "@Setting_Value";

       //Parameters for rsp_BGS_VoucherInformation
       public const string CONST_SP_PARAM_STARTDATE = "@StartDate";
       public const string CONST_SP_PARAM_ENDDATE = "@EndDate";
       public const string CONST_SP_PARAM_TYPE = "@Type";       

       //Parameter for usp_CreateTicketNumber
       //public const string CONST_SP_PARAM_TICKETTYPE = "@ticketprefix";
       //public const string CONST_SP_PARAM_INSTALLATIONID = "@Installation_ID";
       //public const string CONST_SP_PARAM_RETURNTICKETNUMBER = "@retTicketNumber";

       //Parameter for usp_CreateTicketStart
       public const string CONST_SP_PARAM_TICKETTYPE = "@ticketprefix";
       public const string CONST_SP_PARAM_INSTALLATIONID = "@Installation_ID";
       public const string CONST_SP_PARAM_RETURNTICKETNUMBER = "@retTicketNumber";
       public const string CONST_SP_PARAM_DATAPAKSERIALNUMBER = "@Datapak_Serial";
       public const string CONST_SP_PARAM_TICKETVALUE= "@Ticket_Value";
       public const string CONST_SP_PARAM_SERVERNAME = "@server";

       //Parameter for esp_createticketcomplete
       public const string CONST_SP_PARAM_BARCODE="@Barcode";
       public const string CONST_SP_PARAM_RETRESULT= "@retResult";
       public const string CONST_SP_PARAM_RETURNVALUE = "@RETURN_VALUE";
       public const string CONST_SP_PARAM_ISLIABILITY = "@IsLiability";

       //Parameter for usp_InsertTicketIssue
       public const string CONST_SP_PARAM_TICKETNUMBER = "@TicketNumber";
       public const string CONST_SP_PARAM_DATEPRINTED = "@Date";
       public const string CONST_SP_PARAM_WINDOWUSER = "@WindowsUser";
       public const string CONST_SP_PARAM_USERID = "@UserID";
       public const string CONST_SP_PARAM_MACHINENAME = "@Workstation";
       public const string CONST_SP_PARAM_CUSTOMERDETAILS = "@CustomerDetails";
       public const string CONST_SP_PARAM_TOTALVALUE = "@TotalValue";
       public const string CONST_SP_PARAM_CASHTOTAL = "@CashTotal";
       // Replicated from VB code,Not required any more
       public const string CONST_SP_PARAM_CHEQUETOTAL = "@ChequeTotal";
       public const string CONST_SP_PARAM_CARDTOTAL = "@CardTotal";
       public const string CONST_SP_PARAM_POINTSTOTAL = "@PointsTotal";
       public const string CONST_SP_PARAM_OTHERTOTAL = "@OtherTotal";
       public const string CONST_SP_PARAM_CASHBREAKDOWN5000 = "@Cash_Breakdown_5000";
       public const string CONST_SP_PARAM_CASHBREAKDOWN2000 = "Cash_Breakdown_2000";
       public const string CONST_SP_PARAM_CASHBREAKDOWN1000 = "@Cash_Breakdown_1000";
       public const string CONST_SP_PARAM_CASHBREAKDOWN500 = "@Cash_Breakdown_500";
       public const string CONST_SP_PARAM_CASHBREAKDOWN200 = "@Cash_Breakdown_200";
       public const string CONST_SP_PARAM_CASHBREAKDOWN100 = "@Cash_Breakdown_100";
       public const string CONST_SP_PARAM_CASHBREAKDOWN50 = "@Cash_Breakdown_50";
       public const string CONST_SP_PARAM_CASHBREAKDOWN20 = "@Cash_Breakdown_20";
       public const string CONST_SP_PARAM_CASHBREAKDOWN10 = "@Cash_Breakdown_10";
       public const string CONST_SP_PARAM_CASHBREAKDOWN5 = "@Cash_Breakdown_5";
       public const string CONST_SP_PARAM_CASHBREAKDOWNOTHER = "@Cash_Breakdown_Other";
       public const string CONST_SP_PARAM_POINTSBARCODE = "@Points_Barcode";
       public const string CONST_SP_PARAM_CASHABLE = "@Cashable";

       #endregion 

       #region Redeem Ticket Online
       /// <summary>
       /// Constants & Variables used in Redeem Ticket functions
       /// </summary>
       ///   Method Revision History
       ///
       /// Author             Date              Description
       /// ---------------------------------------------------
       /// Renjish N         21-Oct-2008      Intial Version 
       //Stored Procedure Names
       public const string CONST_RSP_GETTICKETHISTORY = "rsp_GetTicketHistory";
       public const string CONST_RSP_BGS_VOUCHERINFORMATION = "rsp_BGS_VoucherInformation";
       public const string CONST_RSP_GETMACHINEDETAILSFROMASSET = "rsp_GetMachineDetailsFromAsset";
       public const string CONST_RSP_GETMACHINEDETAILSVIATBRPAYOUT = "rsp_GetMachineDetailsViaTBRPayout";
       public const string CONST_RSP_DOESOFFLINETICKETEXIST = "rsp_DoesOfflineTicketExist";
       public const string CONST_RSP_GETEXCEPTIONDETAILS = "rsp_GetExceptionDetails";
       public const string CONST_RSP_GETREDEEMTICKETAMOUNT = "rsp_GetRedeemTicketAmount";
       public const string CONST_SP_PARAM_ENABLELAUNDERING = "EnableLaundering";
       public const string CONST_SP_PARAM_ALLOWOFFLINEREDEEM = "Allow_Offline_Redeem";
       public const string CONST_SP_PARAM_AMBERCREDITSWAGEREDTOCASHIN = "Amber_Credits_Wagered_to_CashIn";
       public const string CONST_SP_PARAM_TICKETVALIDATEENABLEVOUCHER = "TicketValidate_EnableVoucher";
       public const string CONST_USP_CHECKSITECODEFOROFFLINEVOUCHERREDEEM = "usp_CheckSiteCodeforOfflineVoucherRedeem";
       public const string CONST_PARAM_VOUCHER = "@voucher";
       public const string CONST_PARAM_STARTDATE = "@StartDate";
       public const string CONST_PARAM_ENDDATE = "@EndDate";
       public const string CONST_PARAM_BATCH = "@BatchNo";
       public const string CONST_PARAM_WEEK = "@WeekCollection";
       public const string CONST_PARAM_TYPE = "@Type";
       public const string CONST_PARAM_BARCODE = "@Barcode";
       public const string CONST_PARAM_DEVICEID = "@DeviceId";
       public const string CONST_PARAM_TICKETSTATUS = "@TicketStatus";
       public const string CONST_PARAM_TICKETAMOUNT = "@TicketAmount";
       public const string CONST_PARAM_MACHINE = "@sMachine";
       public const string CONST_PARAM_PAYOUT = "@TBR_Payout_ID";
       public const string CONST_PARAM_RouteNo = "@Route_No";
       public const string CONST_PARAN_NUMBEROFPARTCOUNTRECORDS = "@NumberOfRecords";
       public const string CONST_PARAM_RECORDSINPAGE = "@NoOfRecords";

       #endregion Redeem Ticket Online

       public const string CONST_SP_GET_INSTALLTION_DETAILS_PROC = "rsp_GetAllInstallationDetails";
        public const string CONST_SP_GET_INSTALLTION_LIST_PROC = "rsp_GetInstallationList";
        public const string CONST_SP_SAVE_OFFLINE_TICKET_PROC = "usp_SaveOfflineTicketDetails";
        public const string CONST_SP_GET_INSTALLTION_DETAILS_REPORTS_PROC = "rsp_GetInstallationDetailsForReports";

       #region Parameter Names
       //for rsp_getsetting SP
       public const string CONST_PARAM_DATAPAK_SERIAL = "@Datapak_Serial";
       public const string CONST_PARAM_INSTALLATION_NUMBER = "@Installation_No";
       public const string CONST_PARAM_INCLUDE_VIRTUAL = "@IncludeVirtual";
       public const string CONST_PARAM_SORT_BY_ZONE = "@SortByZone";
       public const string CONST_PARAM_REPORT_TYPE = "@ReportType";

       //for offline ticket redeem
       public const string CONST_PARAM_sTICKET_NUMBER = "@sTicketNumber";
       public const string CONST_PARAM_iINSTALLATION_NUMBER = "@iInstallation_No";
       public const string CONST_PARAM_dDate = "@dDate";
       public const string CONST_PARAM_sWINDOWS_USER = "@sWindowsUser";
       public const string CONST_PARAM_iUSERID = "@iUserID";
       public const string CONST_PARAM_sWORKSTATION = "@sWorkstation";
       public const string CONST_PARAM_sCUSTOMER_DETAILS = "@sCustomerDetails";
       public const string CONST_PARAM_iVALUE = "@iValue";
       public const string CONST_PARAM_TreasuryNo = "@Treasury_No";

       #endregion

       #region "GetUnProcessedHandpay"

       //Stored Proceure for GetHandpays
       public const string CONST_SP_RSP_GETHANDPAYS = "rsp_GetHandPays";
       public const int USERID = 0;

       //Constant Variable Names for rsp_GetHandpays
       public const string CONST_VAR_GETHANDPAYS_BARPOSITIONNAME = "Bar_Pos_Name";
       public const string CONST_VAR_GETHANDPAYS_MACHINECLASSNAME = "Name";
       public const string CONST_VAR_GETHANDPAYS_INSTALLATIONREFERENCE = "Installation_Reference";
       public const string CONST_VAR_GETHANDPAYS_TEID = "TE_Id";
       public const string CONST_VAR_GETHANDPAYS_TEDATE = "TE_Date";
       public const string CONST_VAR_GETHANDPAYS_TEVALUE = "TE_Value";
       public const string CONST_VAR_GETHANDPAYS_TEHPTYPE = "TE_HP_Type";
       public const string CONST_VAR_GETHANDPAYS_INSTALLATIONNO = "Installation_No";

       public const string TREASURY_REFILL = "Refill";
       public const string TREASURY_REFUND = "Refund";
       public const string TREASURY_PRIZE_CARD_REFILL = "Prize Card Refill";
       public const string TREASURY_HANDPAY_JACKPOT = "Attendantpay Jackpot";
       public const string TREASURY_HANDPAY_CREDIT = "Attendantpay Credit";
       public const string TREASURY_SHORTPAY = "Shortpay";
       public const string TREASURY_CASH_DESK_FLOAT = "Cash Desk Float";
       public const string TREASURY_FLOAT = "Float";
       public const string TREASURY_PROG = "Prog";
       public const string TREASURY_VOID = "VOID";
       public const string LOCKAPP = "LOCKAPP_TICKETING";
       public const string LOCKTYPE = "LOCKTYPE_VOIDTIC";
       public const string CONST_ENABLEHANDPAYRECEIPT = "TicketValidate_EnableHandpayReceipt";
       public const string CONST_ENABLEREFILLRECEIPT = "TicketValidate_EnableRefillReceipt";
       public const string CONST_ENABLEREFUNDRECEIPT = "TicketValidate_EnableRefundReceipt";
       public const string CONST_ENABLEPROGRESSIVERECEIPT = "TicketValidate_EnableProgressivePayoutReceipt";

       public const string CONST_PARAM_INSTALLATION_NO = "@Installation_No";
       public const string CONST_PARAM_COLLECTION_NO = "@Collection_No";
       public const string CONST_PARAM_USER_ID = "@User_ID";
       public const string CONST_PARAM_TREASURY_NUMBER = "@TreasuryNo";
       public const string CONST_PARAM_TREASURY_DATE = "@dDate";       
       public const string CONST_PARAM_TREASURY_TIME = "@dTime";
       public const string CONST_PARAM_USERNO = "@UserNo";

       public const string CONST_PARAM_REASONCODE = "@ReasonCode";
       public const string CONST_PARAM_REASONDESCRIPTION = "@ReasonDesc";

       public const string CONST_PARAM_TREASURY_TYPE = "@Treasury_Type";
       public const string CONST_PARAM_TREASURY_REASON = "@Treasury_Reason";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_2P = "@Treasury_Breakdown_2p";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_5P = "@Treasury_Breakdown_5p";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_10P = "@Treasury_Breakdown_10p";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_20P = "@Treasury_Breakdown_20p";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_50P = "@Treasury_Breakdown_50p";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_100P = "@Treasury_Breakdown_100p";
       public const string CONST_PARAM_TREASURY_BREAKDOWN_200P = "@Treasury_Breakdown_200p";

       public const string CONST_PARAM_TREASURY_AMOUNT = "@Treasury_Amount";
       public const string CONST_PARAM_TREASURY_ALLOCATED = "@Treasury_Allocated";
       public const string CONST_PARAM_TREASURY_MEMBERSHIP_NO = "@Treasury_Membership_No";
       public const string CONST_PARAM_TREASURY_REASON_CODE = "@Treasury_Reason_Code";
       public const string CONST_PARAM_TREASURY_ISSUER_USER_NO = "@Treasury_Issuer_User_No";
       public const string CONST_PARAM_TREASURY_TEMP = "@Treasury_Temp";
       public const string CONST_PARAM_TREASURY_FLOATISSUEDBY = "@Treasury_Float_Issued_By";
       public const string CONST_PARAM_TREASURY_AUTHORIZEDUSER_NO = "@AuthorizedUser_No";
       public const string CONST_PARAM_TREASURY_AUTHORIZED_DATE = "@Authorized_Date";


       public const string CONST_PARAM_INSTALLATION_ID = "@Installation_ID";
       public const string CONST_PARAM_EXCEPTION_TYPE = "@Exception_Type";
       public const string CONST_PARAM_EXCEPTION_DETAILS = "@Details";
        public const string CONST_PARAM_EXCEPTION_REFERENCE ="@Reference";
        public const string CONST_PARAM_EXCEPTION_USER = "@User";

       public const string CONST_PARAM_TE_TICKET_NUMBER = "@TE_ticketNumber";
       public const string CONST_PARAM_TE_ID = "@TE_ID";
       public const string CONST_PARAM_TE_STATUS = "@TE_Status";

       public const string CONST_PARAM_TRANSACTION_TYPE = "@TransactionType";
       public const string CONST_PARAM_TICKETNUMBERorID = "@TicketNumberorID";
       public const string CONST_PARAM_TREASURYREASON = "@TreasuryReason";

       #endregion

       public const string CONSTANT_RSP_RSP_GETSITEDETAILS = "rsp_GetSiteDetails";
       public const string CONSTANT_RSP_GETCMPKIOSKURL = "CMP_KIOSKURL";
       public const string CONSTANT_RSP_GETCMPAPPUSER = "CMPAPP_UNAME";
       public const string CONSTANT_RSP_GETCMPAPPPWD = "CMPAPP_PWD";
       // To print Redeem Receipt only if the setting is true in setting table - Added by Vineetha on 04-03-2009
       public const string CONST_SP_PARAM_ENABLEREDEEMPRINTCDO = "EnableRedeemPrintCDO";

       #region Shortpays

       public const string CONST_SP_SAVE_TREASURY_PROC= "usp_InsertTreasury";
       public const string CONST_SP_GET_TICKETING_EXCEPTIONS_PROC = "rsp_GetTicketExceptions";
       public const string CONST_SP_UPDATE_VOID_EXPIRED_TREASURY_PROC = "usp_VoidOrExpiredTicket_Treasury";
       public const string CONST_SP_UPDATE_TICKET_EXCEPTION_PROC = "usp_UpdateTicketException";
       public const string CONST_SP_CREATE_SHORTPAY_PROC= "";
       public const string CONST_SP_GET_FAILURE_REASONS_PROC = "rsp_GetFailureReasons";
       public const string CONST_SP_INSERT_EXCEPTION_PROC = "usp_InsertException";
       public const string CONST_SP_UPDATE_TICKETEXCEPTION = "usp_UpdateFinalStatusTicketException";
       public const string CONST_SP_GET_MAX_TREASURYID = "rsp_getMaxTreasuryID";
       public const string CONST_SP_GET_FAILURE_REASONS = "rsp_GetAllFailureReasons";
       public const string CONST_SP_SAVE_REASON_PROC = "usp_InsertReason";
       public const string CONST_SP_DELETE_REASON_PROC = "usp_DeleteReason";

       public const string CONST_SP_CREATE_SHORTPAY_FOR_APPROVAL = "usp_CreateShortPayForApproval";       

       #endregion

       #region VoidTransactions
       public const string CONST_SP_VOIDTREASURY_CREATENEGTRAN = "usp_VoidTreasury_CreateNegTran";
      
       #endregion

       #region Void Transaction

       public const string CONSTANT_USP_CREATEVOIDTREASURY = "usp_VoidTreasury_CreateNegTran";
       public const string CONSTANT_RSP_GETMCDETAILSVIATREASURY = "rsp_GetMachineDetailsViaTreasury";
       public const string CONSTANT_RSP_GETVOIDTRANSACTIONLIST = "rsp_GetVoidTransactionList";

       #endregion

       public const string CONSTANT_RSP_GETMETERLIFE = "rsp_GetMeterLife";
       public const string CONSTANT_RSP_GETCURRENTDAYMETERS = "rsp_GetCurrentDayMeters";

       #region Field Service
       public const string CONST_SP_RSP_GETCURRENTSERVICECALLS = "rsp_GetCurrentServiceCalls";
       public const string CONST_SP_RSP_GETCURRENTBARPOSITIONNAMES = "rsp_GetCurrentBarPositionNames";
       public const string CONST_SP_RSP_GETSITEDETAILS = "rsp_GetSiteDetails";
       public const string CONST_PARAM_SITE_CODE = "@SITECODE";
       public const string CONST_SP_RSP_CASHDESKEVENT = "rsp_PrepareCashDeskEvent";
       #endregion

       #region Enrollment
       public const string CONST_SP_RSP_GETPOSITIONDETAILS = "rsp_GetPositionDetails";
       public const string SP_USP_REMOVEINSTALLATIONREFERENCES = "usp_RemoveInstallationReferences";
       public const string SP_PARAM_REMOVEINSTALLATIONREFERENCES = "@Installation";
       public const string CONST_SP_USP_INSERTMACHINEMAINTENANCEDETAILS = "usp_InsertMachineMaintenanceDetails";
       public const string CONST_SP_USP_UPDATEMACHINEMAINTENANCEDETAILS = "usp_UpdateMachineMaintenanceDetails";
       public const string CONST_SP_USP_HOURLY_VTP = "usp_Hourly_VTP";
       public const string CONST_SP_ESP_COLLATEHSGAMINGDAY = "esp_Collate_HS_GamingDay";
       public const string CONST_SP_RSP_GETASSETDETAILS = "rsp_GetAssetDetails";

       public const string CONST_PARAM_THEINSTALLATION = "@TheInstallation";
       public const string CONST_PARAM_THEDATETIME = "@TheDateTime";
       public const string CONST_PARAM_THEHOUR = "@TheHour";
       public const string CONST_PARAM_ISREAD = "@IsRead";
       public const string CONST_PARAM_RETURNVALUE = "@RETURN_VALUE";

       #endregion

       #region Events
       public const string CONST_PARAM_START_DATE = "@sDate";
       public const string CONST_PARAM_END_DATE = "@eDate";
       public const string CONST_PARAM_SHOW_CLEARED = "@ShowCleared";
       public const string CONST_PARAM_BARPOSITION = "@Bar_Pos_Name";
       public const string CONST_PARAM_EVENTTYPE = "@Event_Type";       
       public const string CONST_PARAM_CLEAR_TYPE = "@Clear_Type";
       public const string CONST_PARAM_EVENT_NO = "@Event_No";
       public const string CONST_PARAM_INSTALATION_NO = "@Installation_No";
       public const string CONST_PARAM_ERROR_CODE = "@Error_Code";
       public const string CONST_PARAM_ERROR_MESSAGE = "@Error_Message";
       public const string CONST_SP_RSP_GETPOSITIONEVENTDETAILS = "rsp_GetPositionEventDetails";
       public const string CONST_SP_USP_CLEAREVENTDETAILS = "USP_CLEAREVENTDETAILS";
       #endregion Events

       #region Factory Reset
           public const string SPCHECKINSTALLATIONAVAILABLE = "rsp_CheckInstallationsAvailable";
           public const string SPCheckDataToExport = "rsp_CheckDataToExport";
           public const string CONST_SP_PARAM_CMPCONNECTION = "EPIGatewayConnstr";
           public const string TICKETLOCATIONCODENAME = "TICKET_LOCATION_CODE";
           public const string SP_RUNSCRIPTS = "usp_ClearExchangeServerData";
       #endregion

       #region SiteSetup
           public const string SPIMPORTINSTALLATIONS = "usp_ImportInstallations";
           public const string SPIMPORTINSTALLATIONGAMEINFO = "usp_ImportInstallationGameInfo";
           public const string SPIMPORTAAMSDETAILS = "usp_ImportAAMSDetails";
           public const string SP_PARAM_IMPORTINSTALLATION = "@doc";
           public const string SP_PARAM_IMPORTSITEALLIANCE = "@doc";
           public const string SPIMPORTMACHINES = "usp_ImportMachines";
           public const string SP_PARAM_IMPORTMACHINES = "@doc";
           public const string SPIMPORTBARPOSITIONS = "usp_ImportBarPositions";
           public const string SP_PARAM_IMPORTBARPOSITIONS = "@doc";
           public const string SPIMPORTZONES = "usp_ImportZones";
           public const string SPIMPORTSITEALLIANCE = "rsp_ImportSiteAlliance";
           public const string SP_PARAM_IMPORTZONES = "@doc";
           public const string SP_PARAM_IMPORTSITEDETAILS1 = "@doc";
           public const string SPIMPORTSITEDETAILS = "usp_ImportSiteForSiteSetupFromXML";
           public const string SP_PARAM_IMPORTSITEDETAILS2 = "@IsSuccess";
           public const string SPIMPORTSITESETTINGS = "usp_ImportSiteSettingsFromXML";
           public const string SP_PARAM_IMPORTSITESETTINGS = "@Doc";
           public const string CONSTANT_RSP_GETINSTALLATIONDETAILSXML = "rsp_GetInstallationDetailsXML";           
           public const string SP_IMPORTSITEALLEVENTS = "rsp_ImportSiteAllEvents";
           public const string SP_PARAM_IMPORTSITEALLEVENTS_SITECODE = "@doc";
           public const string SP_PARAM_IMPORTSITEALLEVENTS_XDays = "@IsSuccess";
           public const string CONSTANT_USP_INSERTCOLLECTIONFROMXML = "usp_InsertCollectionfromXML";
           public const string CONSTANT_USP_INSERTTREASURYFROMXML = "usp_InsertTreasuryfromXML";
           public const string CONSTANT_USP_INSERTAFTTRANSACTIONSFROMXML = "usp_InsertAFTTransactionsFromXML";
           public const string CONSTANT_USP_INSERTAUDITHISTORYFROMXML = "usp_InsertAuditHistoryFromXML"; 
           public const string CONSTANT_USP_INSERTEVENTSFROMXML = "usp_InsertEventsfromXML";
           public const string SP_PARAM_USP_INSERTCOLLECTIONFROMXML_DOC = "@doc";
           public const string SP_PARAM_USP_INSERTAFTTRANSACTIONSFROMXML_DOC = "@doc";
           public const string SP_PARAM_USP_INSERTAUDITHISTORYFROMXML_DOC = "@doc";
           public const string SP_PARAM_USP_INSERTCOLLECTIONFROMXML_SUCCESS = "@IsSuccess";
           public const string SP_IMPORTSITEALLUSERDETAILS = "usp_UpdateUserAccessFromXML";
           public const string SP_PARAM_IMPORTSITEALLUSERDETAILS_DOC = "@doc";
           public const string SP_PARAM_IMPORTSITEALLUSERDETAILS_ADDUSER = "@AddUser";
           public const string SP_IMPORTSITEALLROLESDETAILS = "usp_UpdateRoleAccessLnkFromXML";
           public const string SP_PARAM_IMPORTSITEALLROLESDETAILS_DOC = "@doc";
           public const string SP_IMPORTSITEALLUSERROLELINK = "usp_UpdateUserRoleLinkFromXML";
           public const string SP_PARAM_IMPORTSITEALLUSERROLELINK_DOC = "@doc";
           public const string CONSTANT_USP_IMPORTCALENDARS = "usp_ImportCalendarDetailsFromXML";
           public const string SP_PARAM_IMPORTCALENDARS_DOC = "@doc";
           public const string SP_PARAM_IMPORTCALENDARS_SUCCESS = "@IsSuccess";
           public const string SP_UPDATECHECKPOINTS = "usp_UpdateImportCheckPoints";
           public const string SP_PARAM_UPDATECHECKPOINTS_VALUE = "@Value";
           public const string SP_PARAM_UPDATECHECKPOINTS_SITECODE = "@SiteCode";
           public const string SP_PARAM_UPDATECHECKPOINTS_TABLENAME = "@TableName";           
           public const string SP_UPDATESITESTATUS = "usp_UpdateSiteStatus";
           public const string SP_PARAM_UPDATESITESTATUS_UPDATE = "@Update";
           public const string SP_PARAM_UPDATESITESTATUS_SITECODE = "@SiteCode";
           public const string SP_UPDATEVOUCHER = "usp_UpdateVoucher";
           public const string SP_PARAM_UPDATEVOUCHER_TICKETSITEID = "@TicketSiteId";
           public const string SP_UPDATETICKETEXCEPTIONAFTERRECOVERY = "usp_UpdateTicketExceptionAfterRecovery";
           public const string SP_UPDATEMETERHISTORYAFTERRECOVERY = "usp_UpdateMeterHistoryAfterRecovery";
           public const string SP_USP_UPDATEHOURLYSTATISTICSAFTERRECOVERY = "usp_UpdateHourlyStatisticsAfterRecovery";
           public const string SP_PARAM_UPDATEHOURLYSTATISTICS_XMLDOC = "@xmldoc";
           public const string SP_USP_UPDATEREADAFTERRECOVERY = "usp_UpdateReadAfterRecovery";
           public const string SP_USP_CLEAREXCHANGESERVERDATA = "usp_ClearExchangeServerData";
           public const string SP_ESP_COLLATE_HS_GAMINGDAY = "esp_Collate_HS_GamingDay";
           public const string SP_USP_INSERTDEVICEDETAILSFROMXML = "usp_InsertDeviceDetailsfromXML";
           public const string SP_PARAM_INSERTDEVICEDETAILSFROMXML = "@doc";
           public const string SP_PARAM_ISSUCCESS_INSERTDEVICEDETAILSFROMXML = "@IsSuccess";
           public const string SP_USP_INSERTWORKSTATIONFROMXML = "usp_InsertWorkStationfromXML";
           public const string SP_PARAM1_INSERTWORKSTATIONFROMXML = "@doc";
           public const string SP_PARAM2_INSERTWORKSTATIONFROMXML = "@IsSuccess";

           public const string SP_USP_INSERTVOUCHERFROMXML = "usp_InsertVoucherfromXML";
           public const string SP_PARAM1_INSERTVOUCHERFROMXML = "@doc";
           public const string SP_PARAM2_INSERTVOUCHERFROMXML = "@IsSuccess";

           public const string SP_USP_INSERTTICKETEXCEPTIONFROMXML = "usp_InsertTicketExceptionfromXML";
           public const string SP_PARAM1_INSERTTICKETEXCEPTIONFROMXML = "@doc";
           public const string SP_PARAM2_INSERTTICKETEXCEPTIONFROMXML = "@IsSuccess";

           public const string TicketConnectSettingName = "Ticketing.Connection";

           public const string SP_ROLEACCESSOBJECTRIGHTLNKFROMXML = "usp_RoleAccessObjectRightLnkFromXML";           
           public const string SP_PARAM1_ROLEACCESSOBJECTRIGHTLNKFROMXML = "@doc";
           public const string SP_PARAM2_ROLEACCESSOBJECTRIGHTLNKFROMXML = "@IsSuccess";

           public const string SP_EXCHANGEADMINOBJECTFROMXML = "usp_ExchangeAdminObjectFromXML";
           public const string SP_PARAM1_EXCHANGEADMINOBJECTFROMXML= "@doc";
           public const string SP_PARAM2_EXCHANGEADMINOBJECTFROMXML= "@IsSuccess";
       #endregion

       #region CashDeskManager
           public const string CONST_PARAM_PERIOD_FROM = "@PeriodFrom";
           public const string CONST_PARAM_PERIOD_TO = "@PeriodTo";


          // public const string CONST_PARAM_STARTDATE = "@StartDate";
         //  public const string CONST_PARAM_ENDDATE = "@EndDate";
          // public const string CONST_PARAM_TYPE = "@Type";
           public const string CONST_PARAM_LIABILITY = "@IsLiability";
          // public const string CONST_PARAM_BARCODE = "@Barcode";

           //public const string CONST_SP_PARAM_SETTINGID = "@Setting_ID";
           //public const string CONST_SP_PARAM_SETTINGNAME = "@Setting_Name";
           //public const string CONST_SP_PARAM_SETTINGDEFAULT = "@Setting_Default";
           //public const string CONST_SP_PARAM_SETTINGVALUE = "@Setting_Value";
           //public const string CONST_SP_PARAM_RETURNVALUE = "@RETURN_VALUE";



           //Stored procedures
           public const string CONST_SP_GET_TicketExceptions = "rsp_GetTicketExceptions";
           public const string CONST_SP_GET_TICKETS_CLAIMED = "rsp_GetTicketsClaimed";
           public const string CONST_SP_RSP_GETTREASURYITEMS = "rsp_GetTreasuryItems";
           public const string CONST_SP_GET_TICKETS_UNCLAIMED = "rsp_GetTicketsUnclaimed";
           public const string CONST_SP_RSP_TICKET_ANOMALIES = "rsp_Ticket_Anomalies";
           public const string CONST_SP_GET_TICKETS_PRINTED = "rsp_GetTicketsPrinted";
           public const string CONST_SP_RSP_GETPROMOTICKETFORPERIODDETAILS = "rsp_GetPromoTicketForPeriodDetails";
          // public const string CONST_SP_GETSETTING = "rsp_GetSetting";

           //Treasury Constants

           public const string CONST_HANDPAYCREDIT = "AttendantPay Credit";
           public const string CONST_PROG = "Progressive";
           public const string CONST_JACKPOT = "AttendantPay Jackpot";
           public const string CONST_REFUND = "Refund";
           public const string CONST_REFILL = "Refill";
           public const string CONST_SHORTPAY = "Shortpay";
           public const string CONST_FLOAT = "Cash Desk Float";
       #endregion

       #region Reports

       public const string SP_RSP_GETBADAAMSDETAILSFORREPORTS = "rsp_GetBadAAMSDetailsforReports";
       public const string SP_RSP_GETJACKPOTSLIPSUMMARYDETAILSFORREPORTS = "rsp_GetJackpotSlipSummaryDetails";
       public const string SP_RSP_GETVOUCHERCOUPONLIABILITYREPORT = "rsp_GetTicketLiabilityReport";
       public const string SP_RSP_GETREDEEMEDTICKETBYDEVICE = "rsp_getRedeemedTicketByDevice";
       public const string SP_RSP_RSP_GETVERSION_SITENAME = "rsp_getVersion_SiteName";
       public const string SP_RSP_GETSPLASH_DETAILS = "rsp_GetSplashDetails";
       public const string SP_RSP_GETEXPIREDVOUCHERCOUPONREPORT = "rsp_GetExpiredVoucherCouponReport";
       public const string SP_RSP_GETVOUCHERLISTINGREPORT = "rsp_GetVoucherListingReport";
       public const string SP_RSP_GETEXPENSEDETAILSFORREPORTS = "rsp_GetExpenseDetails";
       public const string SP_RSP_GETASSETNUMBERFORACTIVEINSTALLATIONS = "rsp_GetAssetNumberforActiveInstallations";
       public const string SP_GETBATCHNO = "rsp_GetBatchNo";
       public const string SP_RSP_GETMETERDETAILSFORREPORTS = "rsp_GetMeterLife";
       public const string SP_RSP_GETCASHDESKRECONDETAILS = "rsp_Report_DailyCashDeskRecon";
       public const string SP_RSP_GETCASHDESKMOVEMENTDETAILS = "rsp_Report_DailyCashDesk";
       public const string SP_RSP_GETSYSTEMBALANCINGDETAILS = "rsp_REPORT_DailyCashDeskCollectionConsolidated";
       public const string RSP_REPORT_SGVI_LIQUIDATIONDETAIL = "RSP_REPORT_SGVI_LIQUIDATIONDETAIL";
       public const string RSP_REPORT_SGVI_LIQUIDATIONSUMMARY = "RSP_REPORT_SGVI_LIQUIDATIONSUMMARY";
       public const string RSP_REPORT_SGVI_EXCEPTIONSUMMARY = "RSP_REPORT_SGVI_EXCEPTIONSUMMARY";
       public const string RSP_REPORT_BATCHWINLOSS = "rsp_REPORT_BatchWinLossReport";
       public const string RSP_REPORT_TICKETANOMALIES = "rsp_TicketAnomaliesReport";

       public const string RSP_REPORT_MACHINEDROP = "rsp_REPORT_MachineDropReport";
       public const string RSP_GETEXCEPTION_VOUCHER_FOR_REPORTS = "rsp_GetExceptionVouchers";
       public const string SP_RSP_GETSTACKERLEVLDETAILSFORREPORTS = "rsp_StackerLevelDetails";
       public const string RSP_REPORT_GETPARTCOLLECTIONBATCHDATA = "rsp_Report_GetPartCollectionBatchData";

       public const string SP_PARAM_BAD_AAMS_ENTITY_TYPE = "@Bad_AAMS_Entity_Type";
       public const string SP_PARAM_EXPENSE_DETAIL_REPORT_DATE = "@ReportDate";
       public const string SP_PARAM_EXPENSE_DETAIL_REPORT_PERIOD = "@ReportPeriod";
       public const string SP_PARAM_EXPENSE_DETAIL_REPORT_ERROR_CODE = "@Error_Code";
       public const string SP_PARAM_EXPENSE_DETAIL_REPORT_ERROR_MSG = "@Error_Message";
       public const string SP_PARAM_EXPENSE_DETAIL_GamingDay_BasedReport = "@IsGamingDayBasedReport";
       public const string SP_PARAM_JACKPOT_SUMMARY_REPORT_START_DATE_TIME = "@ReportStartDateTime";
       public const string SP_PARAM_JACKPOT_SUMMARY_REPORT_END_DATE_TIME = "@ReportEndDateTime";
       public const string SP_PARAM_JACKPOT_SUMMARY_REPORT_INCLUDEHANDPAY = "@ShowHandpay";
       public const string SP_PARAM_JACKPOT_SUMMARY_REPORT_SHOWJACKPOT = "@ShowJackpot";
       public const string SP_PARAM_METER_DETAIL_INSTALLATION_ID = "@inst_id";
       public const string SP_PARAM_STACKER_LEVEL = "@StackerLevel";

       #endregion Reports
       #region CAGE
       public const string CONST_PARAM_TE_Slip_NUMBER = "@SlipNo";
       public const string CONST_PARAM_JACKPOT_SITE = "@Site";
       #endregion

       #region GloryCashDispenser
       // public const string CONST_PARAM_TransactionType = "@TransactionType";
       public const string CONST_PARAM_TRANSACTIONSTARTTIME = "@TransactionStarttime";
       public const string CONST_PARAM_TRANSACTIONENDTIME = "@TransactionEndtime";
       public const string CONST_PARAM_VALIDATIONNO = "@ValidationNo";
       public const string CONST_PARAM_TICKETNO = "@TicketNo";
       public const string CONST_PARAM_ASSETNO = "@AssetNo";
       public const string CONST_PARAM_TRANSACTIONAMOUNT = "@TransactionAmount";
       public const string CONST_PARAM_USERID = "@UserID";
       public const string CONST_PARAM_SESSIONID = "@SessionID";
       public const string CONST_PARAM_DEVICE = "@Device";
       public const string CONST_PARAM_STATUS = "@Status";
       public const string CONST_PARAM_ERRORCODE = "@ErrorCode";
       public const string CONST_PARAM_SEQID = "@id";
       public const string CONSTANT_SP_USP_INSERT_GLORYCDDETAILS = "usp_InsertGloryCashDispenseDetails";
       public const string CONSTANT_SP_USP_UPDATE_GLORYCDDETAILS = "usp_UpdateGlory_CDDetails";
       public const string CONST_PARAM_TicketException_ID = "@TE_ID";
       public const string CONST_SP_ROLLBACK_PROCESS_HANDPAY = "usp_RollBackHandPayProcess";
       public const string CONST_PARAM_Reference1 = "@Reference1";
       public const string CONST_PARAM_Export_Type = "@Type";
       public const string CONST_SP_EXPORT_HISTORY = "usp_Export_History";
       #endregion

       public const string RSP_SELECTPROMOTIONHISTORYREPORT = "rsp_selectpromotionhistoryreport";
       public const string RSP_SELECTTISDETAILSREPORT = "rsp_SelectTISPromotionDetails";
   }
}
