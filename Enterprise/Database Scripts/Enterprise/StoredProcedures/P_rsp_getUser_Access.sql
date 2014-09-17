USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getUser_Access]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getUser_Access]
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

CREATE PROCEDURE rsp_getUser_Access
@User_Group INT
AS
	SELECT UG.User_Group_ID,UA.*,UP_HQ_UA.Access_Value FROM
	(
		-- TO GET Access_Key with coressponding values
		SELECT HQ_User_Access_ID,Access_Key,Access_Value FROM 
		(
			SELECT HQ_User_Access_ID,
				ISNULL(HQ_Admin,0) AS HQ_Admin, ISNULL(HQ_Admin_Customers,0) AS HQ_Admin_Customers, ISNULL(HQ_Admin_Customers_Company,0) AS HQ_Admin_Customers_Company,
				ISNULL(HQ_Admin_Customers_Company_New,0) AS HQ_Admin_Customers_Company_New, ISNULL(HQ_Admin_Customers_Company_Edit,0) AS HQ_Admin_Customers_Company_Edit,
				ISNULL(HQ_Admin_Customers_Sub,0) AS HQ_Admin_Customers_Sub, ISNULL(HQ_Admin_Customers_Sub_New,0) AS HQ_Admin_Customers_Sub_New,
				ISNULL(HQ_Admin_Customers_Sub_Edit,0) AS HQ_Admin_Customers_Sub_Edit, ISNULL(HQ_Admin_Customers_Site,0) AS HQ_Admin_Customers_Site,
				ISNULL(HQ_Admin_Customers_Site_New,0) AS HQ_Admin_Customers_Site_New, ISNULL(HQ_Admin_Customers_Site_Edit,0) AS HQ_Admin_Customers_Site_Edit,
				ISNULL(HQ_Admin_Customers_Zone,0) AS HQ_Admin_Customers_Zone, ISNULL(HQ_Admin_Customers_Zone_New,0) AS HQ_Admin_Customers_Zone_New,
				ISNULL(HQ_Admin_Customers_Zone_Edit,0) AS HQ_Admin_Customers_Zone_Edit, ISNULL(HQ_Admin_Customers_Bar,0) AS HQ_Admin_Customers_Bar,
				ISNULL(HQ_Admin_Customers_Bar_New,0) AS HQ_Admin_Customers_Bar_New, ISNULL(HQ_Admin_Customers_Bar_Edit,0) AS HQ_Admin_Customers_Bar_Edit,
				ISNULL(HQ_Admin_Customers_Bar_BulkCopyPositions,0) AS HQ_Admin_Customers_Bar_BulkCopyPositions, ISNULL(HQ_Admin_Access,0) AS HQ_Admin_Access,
				ISNULL(HQ_Admin_Groups,0) AS HQ_Admin_Groups, ISNULL(HQ_Admin_Groups_Edit,0) AS HQ_Admin_Groups_Edit, ISNULL(HQ_Admin_Users,0) AS HQ_Admin_Users,
				ISNULL(HQ_Admin_Users_ViewAll,0) AS HQ_Admin_Users_ViewAll, ISNULL(HQ_Admin_Users_Edit,0) AS HQ_Admin_Users_Edit, ISNULL(HQ_Admin_Users_ResetPassword,0) AS HQ_Admin_Users_ResetPassword, ISNULL(HQ_Admin_Users_Unlock,0) AS HQ_Admin_Users_Unlock, ISNULL(HQ_Admin_Users_EditAll,0) AS HQ_Admin_Users_EditAll,
				ISNULL(HQ_Admin_Settings,0) AS HQ_Admin_Settings, ISNULL(HQ_Admin_Settings_Edit,0) AS HQ_Admin_Settings_Edit, ISNULL(HQ_Admin_Depot,0) AS HQ_Admin_Depot,
				ISNULL(HQ_Admin_Depot_Edit,0) AS HQ_Admin_Depot_Edit, ISNULL(HQ_Admin_Operator,0) AS HQ_Admin_Operator, ISNULL(HQ_Admin_Operator_Edit,0) AS HQ_Admin_Operator_Edit,
				ISNULL(HQ_Admin_Calendar,0) AS HQ_Admin_Calendar, ISNULL(HQ_Calendar_Edit,0) AS HQ_Calendar_Edit, ISNULL(HQ_Admin_OpenHours,0) AS HQ_Admin_OpenHours,
				ISNULL(HQ_Admin_SiteSettings,0) AS HQ_Admin_SiteSettings, ISNULL(HQ_Admin_Declaration,0) AS HQ_Admin_Declaration, ISNULL(HQ_Admin_StackerFeature,0) AS HQ_Admin_StackerFeature,
				ISNULL(HQ_Admin_DropSchedule,0) AS HQ_Admin_DropSchedule, ISNULL(HQ_Sites,0) AS HQ_Sites, ISNULL(HQ_Stock_Machine_Control,0) AS HQ_Stock_Machine_Control,
				ISNULL(HQ_Collections,0) AS HQ_Collections, ISNULL(HQ_CashierTransactions,0) AS HQ_CashierTransactions, ISNULL(HQ_CashierTransactions_ViewNumberTickets,0) AS HQ_CashierTransactions_ViewNumberTickets,
				ISNULL(HQ_Gamelibrary,0) AS HQ_Gamelibrary, 
				ISNULL(HQ_Engineers,0) AS HQ_Engineers,
				ISNULL(HQ_Engineers_CreateCall,0) AS HQ_Engineers_CreateCall,
				ISNULL(HQ_Engineers_CreateCall_Edit,0) AS HQ_Engineers_CreateCall_Edit,
				ISNULL(HQ_Engineers_Current,0) AS HQ_Engineers_Current,
				ISNULL(HQ_Engineers_Current_Edit,0) AS HQ_Engineers_Current_Edit, 
				ISNULL(HQ_Engineers_Current_Close,0) AS HQ_Engineers_Current_Close,
				ISNULL(HQ_Engineers_Closed,0) AS HQ_Engineers_Closed, 
				ISNULL(HQ_Engineers_Closed_Edit,0) AS HQ_Engineers_Closed_Edit,
				ISNULL(HQ_Engineers_Engineers,0) AS HQ_Engineers_Engineers, 
				ISNULL(HQ_Engineers_Engineers_Edit,0) AS HQ_Engineers_Engineers_Edit,
				ISNULL(HQ_Engineers_Schedule,0) AS HQ_Engineers_Schedule, 
				 ISNULL(HQ_Engineer_EmailAlerts,0) as HQ_Engineer_EmailAlerts,
				 ISNULL(HQ_Engineer_AlertAudit,0) as HQ_Engineer_AlertAudit,
				ISNULL(HQ_Stock,0) AS HQ_Stock, ISNULL(HQ_Stock_BuyMachine,0) AS HQ_Stock_BuyMachine,
				ISNULL(HQ_Stock_TerminateMachine,0) AS HQ_Stock_TerminateMachine, ISNULL(HQ_Stock_Edit,0) AS HQ_Stock_Edit, ISNULL(HQ_Stock_Machine_History,0) AS HQ_Stock_Machine_History,
				ISNULL(HQ_Stock_Depreciation,0) AS HQ_Stock_Depreciation, ISNULL(HQ_Stock_Manufacturer,0) AS HQ_Stock_Manufacturer, ISNULL(HQ_Stock_Supplier,0) AS HQ_Stock_Supplier,
       ISNULL(HQ_stock_machine_type, 0) AS hq_stock_machine_type,
				 ISNULL(HQ_Financial,0) AS HQ_Financial, 
				 --ISNULL(HQ_Financial_Terms,0) AS HQ_Financial_Terms,
				--ISNULL(HQ_Financial_Shares,0) AS HQ_Financial_Shares, 
				--ISNULL(HQ_Financial_Shares_Edit,0) AS HQ_Financial_Shares_Edit, 
				--ISNULL(HQ_Financial_Terms_Summary,0) AS HQ_Financial_Terms_Summary,
				--ISNULL(HQ_Financial_Period_End,0) AS HQ_Financial_Period_End, 
				ISNULL(HQ_Reports,0) AS HQ_Reports, ISNULL(HQ_DataSheet,0) AS HQ_DataSheet,
				ISNULL(HQ_Analysis,0) AS HQ_Analysis, ISNULL(HQ_AuditViewer,0) AS HQ_AuditViewer, ISNULL(HQ_Guardian,0) AS HQ_Guardian,
       ISNULL(HQ_Customer_Access_View_Entire_Database, 0) AS HQ_Customer_Access_View_Entire_Database,
       ISNULL(HQ_Admin_SiteLicensing, 0) AS HQ_Admin_SiteLicensing,
       ISNULL(HQ_Admin_SiteLicensing_LicenseGen, 0) AS HQ_Admin_SiteLicensing_LicenseGen,
       ISNULL(HQ_Admin_SiteLicensing_LicenseGen_RuleInfo, 0) AS HQ_Admin_SiteLicensing_LicenseGen_RuleInfo,
       ISNULL(HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule,0) AS HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule,
       ISNULL(HQ_Admin_SiteLicensing_LicenseGen_KeyGen, 0) AS HQ_Admin_SiteLicensing_LicenseGen_KeyGen,
       ISNULL(HQ_Admin_SiteLicensing_ViewCancelLicense, 0) AS HQ_Admin_SiteLicensing_ViewCancelLicense,
       ISNULL(HQ_Admin_SiteLicensing_LicenseHistory, 0) AS HQ_Admin_SiteLicensing_LicenseHistory,
       ISNULL(HQ_Asset_Template, 0) AS HQ_Asset_Template,
       ISNULL(HQ_Template_Create, 0) AS HQ_Template_Create,
       ISNULL(HQ_Template_Edit, 0) AS HQ_Template_Edit,
       ISNULL(HQ_Template_Delete, 0) AS HQ_Template_Delete,
       ISNULL(HQ_Admin_AGSCombination, 0) AS HQ_Admin_AGSCombination,
       ISNULL(HQ_Stock_Asset_Export, 0) AS HQ_Stock_Asset_Export,
       ISNULL(HQ_Stock_Asset_Import, 0) AS HQ_Stock_Asset_Import,
       ISNULL(HQ_Financial_ShareHolder,0) AS HQ_Financial_ShareHolder,
       ISNULL(HQ_Financial_ProfitShare,0) AS HQ_Financial_ProfitShare,
       ISNULL(HQ_Financial_ExpenseShare,0) AS HQ_Financial_ExpenseShare,
       ISNULL(HQ_Financial_ReadLiquidation,0) AS HQ_Financial_ReadLiquidation,
       ISNULL(HQ_Financial_CollectionLiquidation,0) AS HQ_Financial_CollectionLiquidation,
       ISNULL(HQ_Admin_Routes, 0) AS HQ_Admin_Routes,
       ISNULL(HQ_Admin_Routes_Edit, 0) AS HQ_Admin_Routes_Edit,
       ISNULL(HQ_Admin_EmployeeCard, 0) AS HQ_Admin_EmployeeCard,
   				--ISNULL(HQ_Admin_VaultInterface,0) AS HQ_Admin_VaultInterface,
       ISNULL(HQ_Financial_ReadLiquidationReport, 0) AS [HQ_Financial_ReadLiquidationReport],
       ISNULL(HQ_SystemAudit, 0) AS HQ_SystemAudit,
       ISNULL(HQ_DataCommsAudit, 0) AS HQ_DataCommsAudit,
       ISNULL(HQ_Admin_VaultInterface, 0) AS HQ_Admin_VaultInterface,
       ISNULL(HQ_Admin_CreateVault, 0) AS HQ_Admin_CreateVault,
       ISNULL(HQ_Admin_EditCreateVault, 0) AS HQ_Admin_EditCreateVault,
       ISNULL(HQ_Admin_VaultDeclaration, 0) AS HQ_Admin_VaultDeclaration,
       ISNULL(HQ_Admin_EditVaultDeclaration, 0) AS HQ_Admin_EditVaultDeclaration,
       ISNULL(HQ_Admin_AuditVaultDeclaration, 0) AS HQ_Admin_AuditVaultDeclaration,
       ISNULL(HQ_Admin_EditAuditVaultDeclaration, 0) AS HQ_Admin_EditAuditVaultDeclaration,
       ISNULL(HQ_Admin_TerminateVault,0) AS HQ_Admin_TerminateVault,
       ISNULL(HQ_Admin_AuthorizeProfitShare,0) AS HQ_Admin_AuthorizeProfitShare,
       ISNULL(HQ_Admin_SiteUserAccess ,0) AS  HQ_Admin_SiteUserAccess,
       ISNULL(HQ_Admin_MeterAdjustment ,0) AS  HQ_Admin_MeterAdjustment,
	   ISNULL(HQ_Admin_Customers_Bar_EnableDisable, 0) AS HQ_Admin_Customers_Bar_EnableDisable,
	   ISNULL(HQ_Financial_TermsProfiles, 0) AS HQ_Financial_TermsProfiles,
	   ISNULL(HQ_Financial_ShareSchedules, 0) AS HQ_Financial_ShareSchedules,
	   ISNULL(HQ_Financial_TermsSummary, 0) AS HQ_Financial_TermsSummary,
       ISNULL(HQ_Financial_PeriodEndTermsProcessor, 0) AS HQ_Financial_PeriodEndTermsProcessor ,
      
       ISNULL(HQ_Engineers_EventViewer,0) as HQ_Engineers_EventViewer,
       --ISNULL( HQ_Site_Drop_Sequencing,0) AS  HQ_Site_Drop_Sequencing,
       --ISNULL( HQ_Site_Drop_Sequencing_Add,0) AS  HQ_Site_Drop_Sequencing_Add,
       --ISNULL( HQ_Site_Drop_Sequencing_Search,0) AS HQ_Site_Drop_Sequencing_Search,
       ISNULL( HQ_GMU_Events, 0) AS HQ_GMU_Events,
       ISNULL( HQ_GMU_Events_Add, 0) AS HQ_GMU_Events_Add,
       ISNULL( HQ_GMU_Events_Edit, 0) AS HQ_GMU_Events_Edit
FROM   HQ_User_Access
		)HQ_User_Access
	 UNPIVOT
	(Access_Value FOR Access_Key IN
	 (
		HQ_Admin,HQ_Admin_Customers,HQ_Admin_Customers_Company,HQ_Admin_Customers_Company_New,HQ_Admin_Customers_Company_Edit,
		HQ_Admin_Customers_Sub,HQ_Admin_Customers_Sub_New,HQ_Admin_Customers_Sub_Edit,HQ_Admin_Customers_Site,
		HQ_Admin_Customers_Site_New,HQ_Admin_Customers_Site_Edit,HQ_Admin_Customers_Zone,HQ_Admin_Customers_Zone_New,
		HQ_Admin_Customers_Zone_Edit,HQ_Admin_Customers_Bar,HQ_Admin_Customers_Bar_New,HQ_Admin_Customers_Bar_Edit,
		HQ_Admin_Customers_Bar_BulkCopyPositions,HQ_Admin_Access,HQ_Admin_Groups,HQ_Admin_Groups_Edit,HQ_Admin_Users,
		HQ_Admin_Users_ViewAll,HQ_Admin_Users_Edit,HQ_Admin_Users_ResetPassword, HQ_Admin_Users_Unlock, HQ_Admin_Users_EditAll,HQ_Admin_Settings,HQ_Admin_Settings_Edit,
		HQ_Admin_Depot,HQ_Admin_Depot_Edit,HQ_Admin_Operator,HQ_Admin_Operator_Edit,HQ_Admin_Calendar,HQ_Calendar_Edit,
		HQ_Admin_OpenHours,HQ_Admin_SiteSettings,HQ_Admin_Declaration,HQ_Admin_StackerFeature,HQ_Admin_DropSchedule,
		HQ_Sites,HQ_Stock_Machine_Control,HQ_Collections,HQ_CashierTransactions,HQ_CashierTransactions_ViewNumberTickets,
		HQ_Gamelibrary,
		HQ_Engineers,
		HQ_Engineers_CreateCall,HQ_Engineers_CreateCall_Edit,
		HQ_Engineers_Current,HQ_Engineers_Current_Edit,HQ_Engineers_Current_Close,HQ_Engineers_Closed,
		HQ_Engineers_Closed_Edit,
		HQ_Engineers_Engineers,HQ_Engineers_Engineers_Edit,
		HQ_Engineers_Schedule,
			HQ_Engineer_EmailAlerts,
		HQ_Engineer_AlertAudit,
		HQ_Stock,HQ_Stock_BuyMachine,
		HQ_Stock_TerminateMachine,HQ_Stock_Edit,HQ_Stock_Machine_History,HQ_Stock_Depreciation,HQ_Stock_Manufacturer,
		HQ_Stock_Supplier,hq_stock_machine_type,
		HQ_Financial,
		--HQ_Financial_Terms,
		--HQ_Financial_Shares,
		--HQ_Financial_Shares_Edit,
		--HQ_Financial_Terms_Summary,
		--HQ_Financial_Period_End,
		HQ_Reports,HQ_DataSheet,HQ_Analysis,HQ_AuditViewer,HQ_Guardian,
		HQ_Customer_Access_View_Entire_Database,HQ_Admin_SiteLicensing,HQ_Admin_SiteLicensing_LicenseGen,
		HQ_Admin_SiteLicensing_LicenseGen_RuleInfo,HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule,
		HQ_Admin_SiteLicensing_LicenseGen_KeyGen,HQ_Admin_SiteLicensing_ViewCancelLicense,HQ_Admin_SiteLicensing_LicenseHistory,
		HQ_Asset_Template,
		HQ_Template_Create,
		HQ_Template_Edit,
		HQ_Template_Delete,
		HQ_Admin_AGSCombination,
		HQ_Stock_Asset_Export,
	    HQ_Stock_Asset_Import,
	    HQ_Financial_ShareHolder,
	    HQ_Financial_ProfitShare,
	    HQ_Financial_ExpenseShare,
	    HQ_Financial_ReadLiquidation,
		HQ_Financial_CollectionLiquidation,
		HQ_Admin_Routes,
		HQ_Admin_Routes_Edit,
        HQ_Admin_EmployeeCard,
       -- HQ_Admin_VaultInterface,
        HQ_Financial_ReadLiquidationReport,
        HQ_SystemAudit,
        HQ_DataCommsAudit,
		HQ_Admin_VaultInterface, 
		HQ_Admin_CreateVault, 
		HQ_Admin_EditCreateVault, 
		HQ_Admin_VaultDeclaration, 
		HQ_Admin_EditVaultDeclaration, 
		HQ_Admin_AuditVaultDeclaration, 
		HQ_Admin_EditAuditVaultDeclaration,
		HQ_Admin_TerminateVault,
		HQ_Admin_AuthorizeProfitShare,
	    HQ_Admin_SiteUserAccess,
	    HQ_Admin_MeterAdjustment,
		HQ_Admin_Customers_Bar_EnableDisable,
		HQ_Financial_TermsProfiles,
		HQ_Financial_ShareSchedules,
		HQ_Financial_TermsSummary,
		HQ_Financial_PeriodEndTermsProcessor,
		HQ_Engineers_EventViewer,
	 --   HQ_Site_Drop_Sequencing,
     --   HQ_Site_Drop_Sequencing_Add,
     --   HQ_Site_Drop_Sequencing_Search,
        HQ_GMU_Events,
	    HQ_GMU_Events_Add,
	    HQ_GMU_Events_Edit   
	    )
	    ) AS UP_HQ_User_Access
	 ) UP_HQ_UA
	-- TO GET Access_Key with coressponding values
	INNER JOIN User_Group UG ON UP_HQ_UA.HQ_User_Access_ID = UG.HQ_User_Access_ID
	INNER JOIN User_Access UA ON UA.Access_Key = UP_HQ_UA.Access_Key
	WHERE UG.User_Group_ID = @User_Group 
	-- TO DISABLE BASED ON SETTTING AND THE DATA IS UPDATED IN tExcludedUserAccessBasedOnSetting TABLE-----
	AND UP_HQ_UA.Access_Key NOT IN  
		( SELECT DISTINCT te.User_Access_Key FROM   tExcludedUserAccessBasedOnSetting te
			INNER JOIN dbo.[Setting] s
				ON  s.Setting_Name COLLATE DATABASE_DEFAULT = te.Setting_Name COLLATE DATABASE_DEFAULT
				AND Setting_Value = 'False'
		)
		
		ORDER BY ua.Access_Id
	-- TO DISABLE BASED ON SETTTING AND THE DATA IS UPDATED IN tExcludedUserAccessBasedOnSetting TABLE-----

GO

