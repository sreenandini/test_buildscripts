USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateUser_Access]')
              AND TYPE IN (N'P', N'PC')
   )
DROP PROCEDURE [dbo].[usp_UpdateUser_Access]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
* Revision History
*
* Name : Selva Kumar S
* Date : 31st May 2012
*
* Name					DateCreated       Type(Created/Modified)  Description
*
*/

CREATE PROCEDURE usp_UpdateUser_Access
	@UserGroupId INT,
	@XMLDoc VARCHAR(MAX),
	@IsSuccess INT OUTPUT
AS
BEGIN
	DECLARE @iDoc INT
	
	SET @IsSuccess = 0    
	
	IF ISNULL(@XMLDoc, '') = ''
	BEGIN
	    RETURN 0
	END	    
	
	SET @XMLDoc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @XMLDoc
	
	EXEC sp_xml_preparedocument @iDoc OUTPUT,
	     @XMLDoc
	
	UPDATE UA
	SET    UA.HQ_Admin = IP_Xml.HQ_Admin,
	       UA.HQ_Admin_Customers = IP_Xml.HQ_Admin_Customers,
	       UA.HQ_Admin_Customers_Company = IP_Xml.HQ_Admin_Customers_Company,
	       UA.HQ_Admin_Customers_Company_New = IP_Xml.HQ_Admin_Customers_Company_New,
	       UA.HQ_Admin_Customers_Company_Edit = IP_Xml.HQ_Admin_Customers_Company_Edit,
	       UA.HQ_Admin_Customers_Sub = IP_Xml.HQ_Admin_Customers_Sub,
	       UA.HQ_Admin_Customers_Sub_New = IP_Xml.HQ_Admin_Customers_Sub_New,
	       UA.HQ_Admin_Customers_Sub_Edit = IP_Xml.HQ_Admin_Customers_Sub_Edit,
	       UA.HQ_Admin_Customers_Site = IP_Xml.HQ_Admin_Customers_Site,
	       UA.HQ_Admin_Customers_Site_New = IP_Xml.HQ_Admin_Customers_Site_New,
	       UA.HQ_Admin_Customers_Site_Edit = IP_Xml.HQ_Admin_Customers_Site_Edit,
	       UA.HQ_Admin_Customers_Zone = IP_Xml.HQ_Admin_Customers_Zone,
	       UA.HQ_Admin_Customers_Zone_New = IP_Xml.HQ_Admin_Customers_Zone_New,
	       UA.HQ_Admin_Customers_Zone_Edit = IP_Xml.HQ_Admin_Customers_Zone_Edit,
	       UA.HQ_Admin_Customers_Bar = IP_Xml.HQ_Admin_Customers_Bar,
	       UA.HQ_Admin_Customers_Bar_New = IP_Xml.HQ_Admin_Customers_Bar_New,
	       UA.HQ_Admin_Customers_Bar_Edit = IP_Xml.HQ_Admin_Customers_Bar_Edit,
	       UA.HQ_Admin_Customers_Bar_BulkCopyPositions = IP_Xml.HQ_Admin_Customers_Bar_BulkCopyPositions,
	       UA.HQ_Admin_Access = IP_Xml.HQ_Admin_Access,
	       UA.HQ_Admin_Groups = IP_Xml.HQ_Admin_Groups,
	       UA.HQ_Admin_Groups_Edit = IP_Xml.HQ_Admin_Groups_Edit,
	       UA.HQ_Admin_Users = IP_Xml.HQ_Admin_Users,
	       UA.HQ_Admin_Users_ViewAll = IP_Xml.HQ_Admin_Users_ViewAll,
	       UA.HQ_Admin_Users_Edit = IP_Xml.HQ_Admin_Users_Edit,
	       UA.HQ_Admin_Users_EditAll = IP_Xml.HQ_Admin_Users_EditAll,
	       UA.HQ_Admin_Settings = IP_Xml.HQ_Admin_Settings,
	       UA.HQ_Admin_Settings_Edit = IP_Xml.HQ_Admin_Settings_Edit,
	       UA.HQ_Admin_Depot = IP_Xml.HQ_Admin_Depot,
	       UA.HQ_Admin_Depot_Edit = IP_Xml.HQ_Admin_Depot_Edit,
	       UA.HQ_Admin_Operator = IP_Xml.HQ_Admin_Operator,
	       UA.HQ_Admin_Operator_Edit = IP_Xml.HQ_Admin_Operator_Edit,
	       UA.HQ_Admin_Calendar = IP_Xml.HQ_Admin_Calendar,
	       UA.HQ_Calendar_Edit = IP_Xml.HQ_Calendar_Edit,
	       UA.HQ_Admin_OpenHours = IP_Xml.HQ_Admin_OpenHours,
	       UA.HQ_Admin_SiteSettings = IP_Xml.HQ_Admin_SiteSettings,
	       UA.HQ_Admin_Declaration = IP_Xml.HQ_Admin_Declaration,
	       UA.HQ_Admin_StackerFeature = IP_Xml.HQ_Admin_StackerFeature,
	       UA.HQ_Admin_DropSchedule = IP_Xml.HQ_Admin_DropSchedule,
	       UA.HQ_Sites = IP_Xml.HQ_Sites,
	       UA.HQ_Stock_Machine_Control = IP_Xml.HQ_Stock_Machine_Control,
	       UA.HQ_Collections = IP_Xml.HQ_Collections,
	       UA.HQ_CashierTransactions = IP_Xml.HQ_CashierTransactions,
	       UA.HQ_CashierTransactions_ViewNumberTickets = IP_Xml.HQ_CashierTransactions_ViewNumberTickets,
	       UA.HQ_Gamelibrary = IP_Xml.HQ_Gamelibrary,
	       UA.HQ_Engineers = IP_Xml.HQ_Engineers,
	       UA.HQ_Engineers_CreateCall = IP_Xml.HQ_Engineers_CreateCall,
	       UA.HQ_Engineers_CreateCall_Edit = IP_Xml.HQ_Engineers_CreateCall_Edit,
	       UA.HQ_Engineers_Current = IP_Xml.HQ_Engineers_Current,
	       UA.HQ_Engineers_Current_Edit = IP_Xml.HQ_Engineers_Current_Edit,
	       UA.HQ_Engineers_Current_Close = IP_Xml.HQ_Engineers_Current_Close,
	       UA.HQ_Engineers_Closed = IP_Xml.HQ_Engineers_Closed,
	       UA.HQ_Engineers_Closed_Edit = IP_Xml.HQ_Engineers_Closed_Edit,
	       UA.HQ_Engineers_Engineers = IP_Xml.HQ_Engineers_Engineers,
	       UA.HQ_Engineers_Engineers_Edit = IP_Xml.HQ_Engineers_Engineers_Edit,
	       UA.HQ_Engineers_Schedule = IP_Xml.HQ_Engineers_Schedule,
	       UA.HQ_Stock = IP_Xml.HQ_Stock,
	       UA.HQ_Stock_BuyMachine = IP_Xml.HQ_Stock_BuyMachine,
	       UA.HQ_Stock_TerminateMachine = IP_Xml.HQ_Stock_TerminateMachine,
	       UA.HQ_Stock_Edit = IP_Xml.HQ_Stock_Edit,
	       UA.HQ_Stock_Machine_History = IP_Xml.HQ_Stock_Machine_History,
	       UA.HQ_Stock_Depreciation = IP_Xml.HQ_Stock_Depreciation,
	       UA.HQ_Stock_Manufacturer = IP_Xml.HQ_Stock_Manufacturer,
	       UA.HQ_Stock_Supplier = IP_Xml.HQ_Stock_Supplier,
	       UA.hq_stock_machine_type = IP_Xml.hq_stock_machine_type,
	       UA.HQ_Financial = IP_Xml.HQ_Financial,
	       UA.HQ_Financial_Terms = IP_Xml.HQ_Financial_Terms,
	       UA.HQ_Financial_Shares = IP_Xml.HQ_Financial_Shares,
	       UA.HQ_Financial_Shares_Edit = IP_Xml.HQ_Financial_Shares_Edit,
	       UA.HQ_Financial_Terms_Summary = IP_Xml.HQ_Financial_Terms_Summary,
	       UA.HQ_Financial_Period_End = IP_Xml.HQ_Financial_Period_End,
	       UA.HQ_Reports = IP_Xml.HQ_Reports,
	       UA.HQ_DataSheet = IP_Xml.HQ_DataSheet,
	       UA.HQ_Analysis = IP_Xml.HQ_Analysis,
	       UA.HQ_AuditViewer = IP_Xml.HQ_AuditViewer,
	       UA.HQ_Guardian = IP_Xml.HQ_Guardian,
	       UA.HQ_Customer_Access_View_Entire_Database = IP_Xml.HQ_Customer_Access_View_Entire_Database,
	        UA.HQ_Admin_SiteLicensing =   IP_Xml.HQ_Admin_SiteLicensing,
		   UA.HQ_Admin_SiteLicensing_LicenseGen =   IP_Xml.HQ_Admin_SiteLicensing_LicenseGen,
		   UA.HQ_Admin_SiteLicensing_LicenseGen_RuleInfo = IP_Xml.HQ_Admin_SiteLicensing_LicenseGen_RuleInfo,
           UA.HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule = IP_Xml.HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule,
           UA.HQ_Admin_SiteLicensing_LicenseGen_KeyGen = IP_Xml.HQ_Admin_SiteLicensing_LicenseGen_KeyGen,
           UA.HQ_Admin_SiteLicensing_ViewCancelLicense = IP_Xml.HQ_Admin_SiteLicensing_ViewCancelLicense,
           UA.HQ_Admin_SiteLicensing_LicenseHistory = IP_Xml.HQ_Admin_SiteLicensing_LicenseHistory,
           UA.HQ_Asset_Template = IP_Xml.HQ_Asset_Template,
		   UA.HQ_Template_Create = IP_Xml.HQ_Template_Create,
		   UA.HQ_Template_Edit = IP_Xml.HQ_Template_Edit,
		   UA.HQ_Template_Delete = IP_Xml.HQ_Template_Delete,
		   UA.HQ_Admin_AGSCombination=IP_Xml.HQ_Admin_AGSCombination,
		   UA.HQ_Stock_Asset_Export=IP_Xml.HQ_Stock_Asset_Export,
		   UA.HQ_Stock_Asset_Import=IP_Xml.HQ_Stock_Asset_Import,
		   UA.HQ_Financial_ShareHolder = IP_Xml.HQ_Financial_ShareHolder,
	       UA.HQ_Financial_ProfitShare = IP_Xml.HQ_Financial_ProfitShare,
	       UA.HQ_Financial_ExpenseShare = IP_Xml.HQ_Financial_ExpenseShare,
	       UA.HQ_Financial_ReadLiquidation = IP_Xml.HQ_Financial_ReadLiquidation,
		   UA.HQ_Financial_CollectionLiquidation = IP_Xml.HQ_Financial_CollectionLiquidation,
		   UA.HQ_Admin_Routes = IP_Xml.HQ_Admin_Routes,
		   UA.HQ_Admin_Routes_Edit = IP_Xml.HQ_Admin_Routes_Edit,
		   UA.HQ_Admin_EmployeeCard =IP_Xml.HQ_Admin_EmployeeCard,
		   UA.HQ_Admin_VaultInterface =IP_Xml.HQ_Admin_VaultInterface,
		   UA.HQ_Financial_ReadLiquidationReport = IP_Xml.HQ_Financial_ReadLiquidationReport,
		   UA.HQ_SystemAudit = IP_Xml.HQ_SystemAudit,
		   UA.HQ_DataCommsAudit = IP_Xml.HQ_DataCommsAudit,
		   UA.HQ_Admin_CreateVault = IP_Xml.HQ_Admin_CreateVault,
		   UA.HQ_Admin_EditCreateVault = IP_Xml.HQ_Admin_EditCreateVault,
		   UA.HQ_Admin_VaultDeclaration = IP_Xml.HQ_Admin_VaultDeclaration,
		   UA.HQ_Admin_EditVaultDeclaration = IP_Xml.HQ_Admin_EditVaultDeclaration,
		   UA.HQ_Admin_AuditVaultDeclaration = IP_Xml.HQ_Admin_AuditVaultDeclaration,
		   UA.HQ_Admin_EditAuditVaultDeclaration  = IP_Xml.HQ_Admin_EditAuditVaultDeclaration,
		   UA.HQ_Admin_TerminateVault = IP_Xml.HQ_Admin_TerminateVault,
		   UA.HQ_Admin_AuthorizeProfitShare=IP_XML.HQ_Admin_AuthorizeProfitShare,
		   UA.HQ_Admin_SiteUserAccess = IP_XML.HQ_Admin_SiteUserAccess,
		   UA.HQ_Admin_MeterAdjustment = IP_XML.HQ_Admin_MeterAdjustment,
		   UA.HQ_Admin_Customers_Bar_EnableDisable = IP_XML.HQ_Admin_Customers_Bar_EnableDisable,
		   UA.HQ_Financial_TermsProfiles=IP_XML.HQ_Financial_TermsProfiles,
		   UA.HQ_Financial_ShareSchedules=IP_XML.HQ_Financial_ShareSchedules,
		   UA.HQ_Financial_TermsSummary=IP_XML.HQ_Financial_TermsSummary,
		   UA.HQ_Financial_PeriodEndTermsProcessor=IP_XML.HQ_Financial_PeriodEndTermsProcessor,
           UA.HQ_Engineer_EmailAlerts = IP_XML.HQ_Engineer_EmailAlerts,
	       UA.HQ_Engineer_AlertAudit = IP_XML.HQ_Engineer_AlertAudit,
	       UA.HQ_Engineers_EventViewer = IP_XML.HQ_Engineers_EventViewer,
		   UA.HQ_Admin_Users_ResetPassword = IP_XML.HQ_Admin_Users_ResetPassword,
		   UA.HQ_Admin_Users_Unlock = IP_XML.HQ_Admin_Users_Unlock,
		   UA.HQ_GMU_Events = IP_XML.HQ_GMU_Events,
		   UA.HQ_GMU_Events_Add = IP_XML.HQ_GMU_Events_Add,
		   UA.HQ_GMU_Events_Edit = IP_XML.HQ_GMU_Events_Edit 
	FROM   HQ_User_Access UA
	       INNER JOIN OPENXML(@idoc, './/UserRoles', 2) WITH
	            (
	                HQ_Admin BIT './HQ_Admin',
	                HQ_Admin_Customers BIT './HQ_Admin_Customers',
	                HQ_Admin_Customers_Company BIT 
	                './HQ_Admin_Customers_Company',
	                HQ_Admin_Customers_Company_New BIT 
	                './HQ_Admin_Customers_Company_New',
	                HQ_Admin_Customers_Company_Edit BIT 
	                './HQ_Admin_Customers_Company_Edit',
	                HQ_Admin_Customers_Sub BIT './HQ_Admin_Customers_Sub',
	                HQ_Admin_Customers_Sub_New BIT 
	                './HQ_Admin_Customers_Sub_New',
	                HQ_Admin_Customers_Sub_Edit BIT 
	                './HQ_Admin_Customers_Sub_Edit',
	                HQ_Admin_Customers_Site BIT './HQ_Admin_Customers_Site',
	                HQ_Admin_Customers_Site_New BIT 
	                './HQ_Admin_Customers_Site_New',
	                HQ_Admin_Customers_Site_Edit BIT 
	                './HQ_Admin_Customers_Site_Edit',
	                HQ_Admin_Customers_Zone BIT './HQ_Admin_Customers_Zone',
	                HQ_Admin_Customers_Zone_New BIT 
	                './HQ_Admin_Customers_Zone_New',
	                HQ_Admin_Customers_Zone_Edit BIT 
	                './HQ_Admin_Customers_Zone_Edit',
	                HQ_Admin_Customers_Bar BIT './HQ_Admin_Customers_Bar',
	                HQ_Admin_Customers_Bar_New BIT 
	                './HQ_Admin_Customers_Bar_New',
	                HQ_Admin_Customers_Bar_Edit BIT 
	                './HQ_Admin_Customers_Bar_Edit',
	                HQ_Admin_Customers_Bar_BulkCopyPositions BIT 
	                './HQ_Admin_Customers_Bar_BulkCopyPositions',
	                HQ_Admin_Access BIT './HQ_Admin_Access',
	                HQ_Admin_Groups BIT './HQ_Admin_Groups',
	                HQ_Admin_Groups_Edit BIT './HQ_Admin_Groups_Edit',
	                HQ_Admin_Users BIT './HQ_Admin_Users',
	                HQ_Admin_Users_ViewAll BIT './HQ_Admin_Users_ViewAll',
	                HQ_Admin_Users_Edit BIT './HQ_Admin_Users_Edit',
	                HQ_Admin_Users_EditAll BIT './HQ_Admin_Users_EditAll',
	                HQ_Admin_Settings BIT './HQ_Admin_Settings',
	                HQ_Admin_Settings_Edit BIT './HQ_Admin_Settings_Edit',
	                HQ_Admin_Depot BIT './HQ_Admin_Depot',
	                HQ_Admin_Depot_Edit BIT './HQ_Admin_Depot_Edit',
	                HQ_Admin_Operator BIT './HQ_Admin_Operator',
	                HQ_Admin_Operator_Edit BIT './HQ_Admin_Operator_Edit',
	                HQ_Admin_Calendar BIT './HQ_Admin_Calendar',
	                HQ_Calendar_Edit BIT './HQ_Calendar_Edit',
	                HQ_Admin_OpenHours BIT './HQ_Admin_OpenHours',
	                HQ_Admin_SiteSettings BIT './HQ_Admin_SiteSettings',
	                HQ_Admin_Declaration BIT './HQ_Admin_Declaration',
	                HQ_Admin_StackerFeature BIT 
	                './HQ_Admin_StackerFeature',
	                HQ_Admin_DropSchedule BIT './HQ_Admin_DropSchedule',
	                HQ_Sites BIT './HQ_Sites',
	                HQ_Stock_Machine_Control BIT 
	                './HQ_Stock_Machine_Control',
	                HQ_Collections BIT './HQ_Collections',
	                HQ_CashierTransactions BIT './HQ_CashierTransactions',
	                HQ_CashierTransactions_ViewNumberTickets BIT 
	                './HQ_CashierTransactions_ViewNumberTickets',
	                HQ_Gamelibrary BIT './HQ_Gamelibrary',
	                HQ_Engineers BIT './HQ_Engineers',
	                HQ_Engineers_CreateCall BIT './HQ_Engineers_CreateCall',
	                HQ_Engineers_CreateCall_Edit BIT './HQ_Engineers_CreateCall_Edit',
	                HQ_Engineers_Current BIT './HQ_Engineers_Current',
	                HQ_Engineers_Current_Edit BIT 
	                './HQ_Engineers_Current_Edit',
	                HQ_Engineers_Current_Close BIT 
	                './HQ_Engineers_Current_Close',
	                HQ_Engineers_Closed BIT './HQ_Engineers_Closed',
	                HQ_Engineers_Closed_Edit BIT 
	                './HQ_Engineers_Closed_Edit',
	                HQ_Engineers_Engineers BIT './HQ_Engineers_Engineers',
	                HQ_Engineers_Engineers_Edit BIT 
	                './HQ_Engineers_Engineers_Edit',
	                HQ_Engineers_Schedule BIT './HQ_Engineers_Schedule',
	                HQ_Stock BIT './HQ_Stock',
	                HQ_Stock_BuyMachine BIT './HQ_Stock_BuyMachine',
	                HQ_Stock_TerminateMachine BIT 
	                './HQ_Stock_TerminateMachine',
	                HQ_Stock_Edit BIT './HQ_Stock_Edit',
	                HQ_Stock_Machine_History BIT 
	                './HQ_Stock_Machine_History',
	                HQ_Stock_Depreciation BIT './HQ_Stock_Depreciation',
	                HQ_Stock_Manufacturer BIT './HQ_Stock_Manufacturer',
	                HQ_Stock_Supplier BIT './HQ_Stock_Supplier',
	                hq_stock_machine_type BIT './hq_stock_machine_type',
	                HQ_Financial BIT './HQ_Financial',
	                HQ_Financial_Terms BIT './HQ_Financial_Terms',
	                HQ_Financial_Shares BIT './HQ_Financial_Shares',
	                HQ_Financial_Shares_Edit BIT 
	                './HQ_Financial_Shares_Edit',
	                HQ_Financial_Terms_Summary BIT 
	                './HQ_Financial_Terms_Summary',
	                HQ_Financial_Period_End BIT './HQ_Financial_Period_End',
	                HQ_Reports BIT './HQ_Reports',
	                HQ_DataSheet BIT './HQ_DataSheet',
	                HQ_Analysis BIT './HQ_Analysis',
	                HQ_AuditViewer BIT './HQ_AuditViewer',
	                HQ_Guardian BIT './HQ_Guardian',
	                HQ_Customer_Access_View_Entire_Database BIT 
	                './HQ_Customer_Access_View_Entire_Database',
	                HQ_Admin_SiteLicensing BIT
                    './HQ_Admin_SiteLicensing',
                    HQ_Admin_SiteLicensing_LicenseGen BIT
                    './HQ_Admin_SiteLicensing_LicenseGen',
                    HQ_Admin_SiteLicensing_LicenseGen_RuleInfo BIT
                    './HQ_Admin_SiteLicensing_LicenseGen_RuleInfo',
                    HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule BIT
                    './HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule',
                    HQ_Admin_SiteLicensing_LicenseGen_KeyGen BIT
                    './HQ_Admin_SiteLicensing_LicenseGen_KeyGen',
                    HQ_Admin_SiteLicensing_ViewCancelLicense BIT
                    './HQ_Admin_SiteLicensing_ViewCancelLicense',
                    HQ_Admin_SiteLicensing_LicenseHistory BIT
                    './HQ_Admin_SiteLicensing_LicenseHistory', 
                   HQ_Asset_Template BIT './HQ_Asset_Template',
                   HQ_Template_Create BIT './HQ_Template_Create',
                   HQ_Template_Edit BIT  './HQ_Template_Edit',
                  HQ_Template_Delete BIT './HQ_Template_Delete',
                  HQ_Admin_AGSCombination BIT './HQ_Admin_AGSCombination',
                  HQ_Stock_Asset_Export BIT './HQ_Stock_Asset_Export',
                   HQ_Stock_Asset_Import BIT './HQ_Stock_Asset_Import',
                   HQ_Financial_ShareHolder BIT './HQ_Financial_ShareHolder',
	                HQ_Financial_ProfitShare BIT './HQ_Financial_ProfitShare',
	                HQ_Financial_ExpenseShare BIT './HQ_Financial_ExpenseShare',
	                HQ_Financial_ReadLiquidation BIT './HQ_Financial_ReadLiquidation',
	                HQ_Financial_ReadLiquidationReport BIT './HQ_Financial_ReadLiquidationReport',
					HQ_Financial_CollectionLiquidation BIT
                  './HQ_Financial_CollectionLiquidation',
                  HQ_Admin_Routes BIT  './HQ_Admin_Routes',
                  HQ_Admin_Routes_Edit BIT  './HQ_Admin_Routes_Edit',
                  HQ_Admin_EmployeeCard BIT  './HQ_Admin_EmployeeCard',
                  HQ_SystemAudit BIT  './HQ_SystemAudit',
                  HQ_DataCommsAudit BIT  './HQ_DataCommsAudit',
                  HQ_Admin_CreateVault BIT  './HQ_Admin_CreateVault',
                  HQ_Admin_VaultInterface BIT  './HQ_Admin_VaultInterface',
                  HQ_Admin_EditCreateVault BIT  './HQ_Admin_EditCreateVault',
                  HQ_Admin_VaultDeclaration BIT  './HQ_Admin_VaultDeclaration',
                  HQ_Admin_EditVaultDeclaration BIT './HQ_Admin_EditVaultDeclaration',
                  HQ_Admin_AuditVaultDeclaration BIT './HQ_Admin_AuditVaultDeclaration',
                  HQ_Admin_EditAuditVaultDeclaration BIT './HQ_Admin_EditAuditVaultDeclaration',
                  HQ_Admin_TerminateVault BIT './HQ_Admin_TerminateVault'   ,
                  HQ_Admin_AuthorizeProfitShare BIT './HQ_Admin_AuthorizeProfitShare' , 
                  HQ_Admin_SiteUserAccess BIT './HQ_Admin_SiteUserAccess',
                  HQ_Admin_MeterAdjustment BIT './HQ_Admin_MeterAdjustment',
                  HQ_Admin_Customers_Bar_EnableDisable BIT './HQ_Admin_Customers_Bar_EnableDisable',
                  HQ_Financial_TermsProfiles BIT './HQ_Financial_TermsProfiles',
                  HQ_Financial_ShareSchedules BIT './HQ_Financial_ShareSchedules',
                  HQ_Financial_TermsSummary BIT './HQ_Financial_TermsSummary',
                  HQ_Financial_PeriodEndTermsProcessor BIT './HQ_Financial_PeriodEndTermsProcessor',
				  HQ_Engineer_EmailAlerts BIT './HQ_Engineer_EmailAlerts',
	              HQ_Engineer_AlertAudit BIT './HQ_Engineer_AlertAudit',
	              HQ_Engineers_EventViewer BIT './HQ_Engineers_EventViewer',
                  HQ_Admin_Users_ResetPassword BIT './HQ_Admin_Users_ResetPassword',
                  HQ_Admin_Users_Unlock BIT './HQ_Admin_Users_Unlock',
                  HQ_GMU_Events  BIT './HQ_GMU_Events',
				  HQ_GMU_Events_Add  BIT './HQ_GMU_Events_Add',
				  HQ_GMU_Events_Edit  BIT './HQ_GMU_Events_Edit' 
	            )IP_Xml
	            ON  UA.HQ_User_Access_ID = (
	                    SELECT HQ_User_Access_ID
	                    FROM   User_Group
	                    WHERE  User_Group_ID = @UserGroupId
	                )
	
	IF @@Error <> 0
	BEGIN
	    SET @IsSuccess = -1
	END
END
GO