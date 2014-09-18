using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Common
{
    public static class Constants
    {
        public const string CONSTANT_LOGMODE = "LogMode";   //Mode of file as XML, DB, .. etc
        public const string CONSTANT_LOGLEVEL = "LogLevel"; //Level of the Error is of int type
        //Debug = 1,Info = 2,Warning = 4,Error = 8
        public const string CONSTANT_LOGPATH = "LogPath";   //File path where to save log file

        //Constant for Connection String

        
        public const string CONSTANT_REGISTRYPATH = "RegistryPath";


        //Stored Procedure Names
        public const string CONSTANT_USP_EXPORTCOLLECTIONBATCH = "rsp_ExportCollectionForXML";
        public const string CONSTANT_USP_EXPORTCOLLECTIONBATCH_UPDATE = "usp_Export_Collection_Batch_Update";
        public const string CONSTANT_USP_ALLEXPORTDATA = "rsp_GetAllExportData";
        public const string CONSTANT_USP_EXPORTMETERHISTORY = "rsp_Export_MeterHistoryData";
        public const string CONSTANT_USP_EXPORTREADRECORD = "rsp_Export_Read";
        public const string CONSTANT_USP_EXPORTHourlyStatistics = "rsp_Export_HourlyStatistics";
        public const string CONSTANT_USP_EXPORTSiteEvents = "rsp_GetEventDetails";
        public const string CONSTANT_USP_EXPORTINSTALLATIONDETAILS = "rsp_ExportInstallationDetails";
        public const string CONSTANT_USP_EXPORTINDIVIDUALCOLLDETAILS = "rsp_ExportOtherCollectionDetails";
        public const string CONSTANT_USP_EXPORTCALENDAR = "rsp_ExportCalendarDetails";
        public const string CONSTANT_USP_EXPORTSITE = "rsp_ExportSiteDetails";
        public const string CONSTANT_RSP_EXPORTMODEL = "rsp_ExportModelDetails";
        public const string CONSTANT_USP_UPDATEEXPORTHISTORY = "usp_UpdateExportHistory";
        public const string CONSTANT_USP_GETINSTALLATIONS = "rsp_GetInstallationDetails";
        public const string CONSTANT_RSP_CHECKFORHQID = "rsp_CheckforHQID";
        public const string CONSTANT_RSP_GETINSTALLATIONUPDATE = "rsp_GetXMLForInstallationUpdate";
        public const string CONSTANT_RSP_RESETINPROGRESSEHRECORDS = "usp_export_resetrecordstobeprocessed";
        //central machine control related SPs
        public const string CONSTANT_USP_GETBARPOSITIONSNAMES = "rsp_GetBarPositiondetails";
        public const string CONSTANT_USP_UPDATEMACHINECONTROL = "usp_UpdateBarPositionForMachineControl";
        public const string CONSTANT_USP_UPDATECOLLECTIONBYDATEDETAILSEXPORTHISTORY = "usp_UpdateCollectionByDateDetailsExportHistory";
        public const string CONSTANT_RSP_RESETINPROGRESSIHRECORDSTOENTERPRISE = "USP_ResetImportHistoryRecords";
        public const string CONSTANT_RSP_RESETINPROGRESSEHRECORDSINENTERPRISE = "USP_ResetExportHistoryRecords";
        public const string CONSTANT_RSP_GETINSTALLATIONXML = "rsp_GetInstallationXML";
        
        //added for the Wireless laptop changes
        public const string CONSTANT_USP_UPDATECONNECTION = "usp_UpdateConnectionSetting";

        public const string CONSTANT_USP_IMPORTHOURLYSTATISTICS = "usp_InsertHourlyStatisticsfromXML";
        public const string CONSTANT_USP_IMPORTHISTORYDETAILS = "USP_UpdateImportHistoryProcessDetails";
        public const string CONSTANT_USP_INSERTCOLLECTIONFROMXML = "usp_InsertCollectionfromXML";
        public const string CONSTANT_USP_INSERTINDVCOLLECTIONFROMXML = "usp_InsertOtherCollectionDetailsfromXML";
        public const string CONSTANT_USP_GETUNPROCESSEDRECORSFROMIH = "rsp_GetUnprocessedRecordsFromImportHistory";
        public const string CONSTANT_USP_INSERTINTOREAD = "esp_InsertReadRecordfromXML";
        public const string CONSTANT_USP_INSERTMETERHISTORY = "usp_InsertMeterHistoryfromXML";
        public const string CONSTANT_USP_IMPORTMODELDETAILS = "usp_ImportModelDetails";
        public const string CONSTANT_RSP_GETSITELIST = "rsp_GetSiteList";
        public const string CONSTANT_USP_CANDATABEPROCESSED = "rsp_CanRecordBeProcessed";
        public const string CONSTANT_USP_DIALUPDATEPROCESSDETAILS = "USP_UpdateImportHistoryProcessDetails";
        public const string CONSTANT_RSP_WRAPSITEDETAILS = "rsp_WrapXMLWithSiteDetails";
        public const string CONSTANT_USP_INSTALLATIONWITHXML = "usp_updateInstallationNoUsingXML";
        public const string CONSTANT_USP_IMPORTCALENDERDETAILS = "usp_ImportCalendarDetailsFromXML";
        public const string CONSTANT_USP_IMPORTSITEDETAILS = "usp_ImportSiteDetailsFromXML";
        public const string CONSTANT_USP_DIALUPIMPORTMODELDETAILS = "usp_ImportModelDetails";
        public const string CONSTANT_USP_GETRECORDSTOBEIMPORTED = "rSP_GetUnprocessedRecordsFromImportHistory";

        //Newly added constants for RealTime Treasury

        public const string CONSTANT_RSP_ENTERPRISEINSTALLATIONNO = "rsp_GetEnterPriseInstallationNumber";
        public const string CONSTANT_RSP_EXPORTTREASURYDETAILS= "rsp_ExportTreasuryDetails";
        public const string CONSTANT_USP_UPDATETREASURYID = "usp_UpdateTreasuryIDInExchange";
        public const string CONSTANT_RSP_EHDATATOBEPROCESSED = "rsp_CheckEHDataTobeProcessed";
        public const string CONSTANT_USP_UPDATESITESTATS = "usp_UpdateSiteStats";
        public const string CONSTANT_RSP_GETHQFORUPDATE = "rsp_GetHQInstallationID";
        //Table Names
        public const string CONSTANT_TBL_EXPORTHISTORY = "Export_History";

        //Column Names

        public const string CONSTANT_COL_EH_TYPE = "EH_Type";
        public const string CONSTANT_COL_EH_REFERENCE = "EH_Reference1";
        public const string CONSTANT_COL_EH_ID = "EH_ID";
        public const string CONSTANT_COL_EH_SITE_CODE = "EH_Site_Code";

        //WebMethod constants

        public const string CONSTANT_WEBMETHOD_IMPORTMETERHISTORY = "ImportMeterHistory";
        public const string CONSTANT_WEBMETHOD_HOURLYSTATISTICS = "ExportHourlyStatistics";
        public const string CONSTANT_WEBMETHOD_READRECORD = "InsertRead";
        public const string CONSTANT_WEBMETHOD_SITEEVENTRECORD = "LogSiteEvent";
        public const string CONSTANT_WEBMETHOD_INSTALLATION_DETAILS = "GetInstallationNumber";
        public const string CONSTANT_WEBMETHOD_IMPORTCOLLECTION = "ImportCollection";
        public const string CONSTANT_WEBMETHOD_IMPORTINDIVIDUALCOLLDETAILS = "ImportIndividualCollectionDetails";
        public const string CONSTANT_WEBMETHOD_IMPORTCALENDAR = "ImportCalendar";
        public const string CONSTANT_WEBMETHOD_IMPORTSITE = "ImportSite";
        public const string CONSTANT_WEBMETHOD_IMPORTDATA = "ImportData";
        public const string CONSTANT_WEBMETHOD_IMPORTMODEL = "ImportModel";
        //Added newly to call web service method for machine control
        public const string CONSTANT_WEBMETHOD_ENABLENOTEACCEPTOR = "EnableNoteAcceptor";
        public const string CONSTANT_WEBMETHOD_DISABLENOTEACCEPTOR = "DisableNoteAcceptor";
        public const string CONSTANT_WEBMETHOD_ENABLEMACHINE = "EnableMachines";
        public const string CONSTANT_WEBMETHOD_DISABLEMACHINE = "DisableMachines";


        //other -  app related
        public const string CONSTANT_DIALUP_TIMERINTERVAL = "TimerIntervalinSecs";
        public const string CONSTANT_ENTERPRISEIMPORTEXPORT_TIMERINTERVAL = "TimerIntervalinSecs";
        public const string CONSTANT_WEBSERVICE_KEY = "BGSAdminWSURL";
        

    }
}
