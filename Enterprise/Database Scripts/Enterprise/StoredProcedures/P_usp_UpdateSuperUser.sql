USE Enterprise
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSuperUser]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[usp_UpdateSuperUser]
GO




/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 11/05/13 1:29:06 PM
 ************************************************************/
CREATE PROCEDURE usp_UpdateSuperUser(@UserID INT, @IsSuperUser BIT = 0 OUTPUT)
AS
BEGIN
	DECLARE @User_Access_ID INT  
	SET @User_Access_ID = 0  
	SELECT @User_Access_ID = hua.HQ_User_Access_ID
	FROM   HQ_User_Access hua
	       INNER JOIN User_Group ug
	            ON  ug.HQ_User_Access_ID = hua.HQ_User_Access_ID
	       INNER JOIN Staff s
	            ON  s.User_Group_ID = ug.User_Group_ID
	WHERE  s.UserTableID = @UserID
	       AND ug.User_Group_Name = 'Super User'  
	
	IF @User_Access_ID <> 0
	BEGIN
	    SET @IsSuperUser = 1	
	    UPDATE HQ_User_Access
	    SET    HQ_Admin = 1,
	           HQ_Admin_Customers = 1,
	           HQ_Admin_Customers_Company = 1,
	           HQ_Admin_Customers_Company_Edit = 1,
	           HQ_Admin_Customers_Sub = 1,
	           HQ_Admin_Customers_Sub_Edit = 1,
	           HQ_Admin_Customers_Site = 1,
	           HQ_Admin_Customers_Site_Edit = 1,
	           HQ_Admin_Customers_Zone = 1,
	           HQ_Admin_Customers_Zone_Edit = 1,
	           HQ_Admin_Customers_Bar = 1,
	           HQ_Admin_Customers_Bar_Edit = 1,
	           HQ_Admin_Groups = 1,
	           HQ_Admin_Groups_Edit = 1,
	           HQ_Admin_Users = 1,
	           HQ_Admin_Users_ViewAll = 1,
	           HQ_Admin_Users_Edit = 1,
	           HQ_Admin_Users_EditAll = 1,
	           HQ_Admin_Settings = 1,
	           HQ_Admin_Settings_Edit = 1,
	           HQ_Admin_Routes = 1,
	           HQ_Admin_Routes_Edit = 1,
	           HQ_Admin_Depot = 1,
	           HQ_Admin_Depot_Edit = 1,
	           HQ_Admin_Operator = 1,
	           HQ_Admin_Operator_Edit = 1,
	           HQ_Admin_Accessories = 1,
	           HQ_Admin_Calendar = 1,
	           HQ_Admin_Exception = 1,
	           HQ_Admin_Op_Cost = 1,
	           HQ_Admin_Licence = 1,
	           HQ_Admin_OpenHours = 1,
	           HQ_Admin_SiteUpdate = 1,
	           HQ_Browse = 1,
	           HQ_Search = 1,
	           HQ_Sites = 1,
	           HQ_Engineers = 1,
	           HQ_Engineers_CreateCall = 1,
	           HQ_Engineers_CreateCall_Edit = 1,
	           HQ_Engineers_Current = 1,
	           HQ_Engineers_Current_Edit = 1,
	           HQ_Engineers_Current_Close = 1,
	           HQ_Engineers_Closed = 1,
	           HQ_Engineers_Closed_Edit = 1,
	           HQ_Engineers_Engineers = 1,
	           HQ_Engineers_Engineers_Edit = 1,
	           HQ_Engineers_Schedule = 1,
	           HQ_Engineers_Schedule_Edit = 1,
	           HQ_Engineers_Notes = 1,
	           HQ_Engineers_Notes_History = 1,
	           HQ_Stock = 1,
	           HQ_Stock_Edit = 1,
	           HQ_Stock_MachinesInUse = 1,
	           HQ_Stock_MachinesInUse_Edit = 1,
	           HQ_Stock_MachinesUnderRepair = 1,
	           HQ_Stock_MachinesUnderRepair_Edit = 1,
	           HQ_Stock_UsableStock = 1,
	           HQ_Stock_UsableStock_Edit = 1,
	           HQ_Stock_MachinesOnOrder = 1,
	           HQ_Stock_MachinesOnOrder_Edit = 1,
	           HQ_Stock_Depreciation = 1,
	           HQ_Stock_Model_Sets = 1,
	           HQ_Stock_Component = 1,
	           HQ_Stock_Supplier = 1,
	           HQ_Stock_Manufacturer = 1,
	           HQ_Stock_Machine_Type = 1,
	           HQ_Stock_Machine_Model = 1,
	           HQ_Stock_Movement_History = 1,
	           HQ_Stock_Machine_History = 1,
	           HQ_Stock_Convert_Machine = 1,
	           HQ_Logistics = 1,
	           HQ_Logistics_Create = 1,
	           HQ_Logistics_Positions = 1,
	           HQ_Logistics_Models = 1,
	           HQ_Logistics_Assets = 1,
	           HQ_Logistics_Authorise = 1,
	           HQ_Logistics_Complete = 1,
	           HQ_Licensing = 1,
	           HQ_Licensing_Purchase = 1,
	           HQ_Licensing_Sell = 1,
	           HQ_CDS = 1,
	           HQ_CDS_Edit = 1,
	           HQ_CDS_Programs_View_Tab = 1,
	           HQ_CDS_Programs_View_Tab_Edit = 1,
	           HQ_CDS_Programs_Edit_Tab = 1,
	           HQ_CDS_Programs_Edit_Tab_Edit = 1,
	           HQ_CDS_Stock_Tab = 1,
	           HQ_CDS_Stock_Tab_Edit = 1,
	           HQ_CDS_Label_Tab = 1,
	           HQ_CDS_Label_Tab_Edit = 1,
	           HQ_Comms = 1,
	           HQ_Comms_CE = 1,
	           HQ_Comms_Site = 1,
	           HQ_Comms_Site_Accept = 1,
	           HQ_Comms_Site_Accept_Edit = 1,
	           HQ_Comms_Site_Inbox = 1,
	           HQ_Comms_Site_Inbox_Edit = 1,
	           HQ_Comms_Site_Outbox = 1,
	           HQ_Comms_Site_Outbox_Edit = 1,
	           HQ_Comms_Mail = 1,
	           HQ_Comms_Messages = 1,
	           HQ_Comms_Move_Rej = 1,
	           HQ_Comms_Col_Rejects = 1,
	           HQ_Comms_New_Move_Rej = 1,
	           HQ_Comms_New_Col_Rej = 1,
	           HQ_Comms_Amedis = 1,
	           HQ_Comms_Export = 1,
	           HQ_Shedules = 1,
	           HQ_Shedules_Edit = 1,
	           HQ_Collections = 1,
	           HQ_Collections_docket = 1,
	           HQ_Collections_Collection_Manager = 1,
	           HQ_Collections_Col_Mismatch = 1,
	           HQ_Collections_Edit_Remarks = 1,
	           HQ_Collections_Royalty = 1,
	           HQ_Financial = 1,
	           --HQ_Financial_Edit = 1,
	           --HQ_Financial_Terms = 1,
	           --HQ_Financial_Terms_Edit = 1,
	           --HQ_Financial_Terms_Summary = 1,
	           --HQ_Financial_Shares = 1,
	           --HQ_Financial_Shares_Edit = 1,
	           --HQ_Financial_Rent = 1,
	           --HQ_Financial_Rent_Edit = 1,
	           HQ_Financial_Export = 1,
	           HQ_Financial_Site_Budget = 1,
	           HQ_Financial_Giro_Rec = 1,
	           HQ_Reports = 1,
	           HQ_Reports_Edit = 1,
	           HQ_Analysis = 1,
	           HQ_Analysis_Edit = 1,
	           HQ_Analysis_Collection = 1,
	           HQ_Analysis_Collection_Edit = 1,
	           HQ_Analysis_Sessional = 1,
	           HQ_Analysis_Sessional_Edit = 1,
	           HQ_Analysis_Comparison = 1,
	           HQ_Analysis_Comparison_Edit = 1,
	           HQ_Analysis_Advanced = 1,
	           HQ_Analysis_Advanced_Edit = 1,
	           HQ_SUPER = 1,
	           HQ_Collections_Edit = 1,
	           HQ_Customer_Access_View_Entire_Database = 1,
	           HQ_Collections_Site_Viewer_Input = 1,
	           HQ_Security = 1,
	           HQ_Security_Edit_EDC_Machine_Code = 1,
	           HQ_Security_Edit_Meter_Readings = 1,
	           HQ_Comms_CE_Reject = 1,
	           HQ_Comms_Site_Reject = 1,
	           HQ_Collections_Manager_Input = 1,
	           HQ_Collections_Manager_Var_Rec = 1,
	           HQ_Collections_Manager_Col_Rec = 1,
	           HQ_Collections_Site_Viewer_Input_New = 1,
	           HQ_Movements_Site_Viewer_Fudge = 1,
	           hq_stock_machine_control = 1,
	           HQ_EventViewer = 1,
	           HQ_Calendar_Edit = 1,
	           HQ_Sites_MC_Control = 1,
	           HQ_Admin_SiteSettings = 1,
	           HQ_Admin_AAMSConfig = 1,
	           HQ_Gamelibrary = 1,
	           HQ_Stock_BuyMachine = 1,
	           HQ_Stock_NewInstall = 1,
	           HQ_Stock_EditInstall = 1,
	           HQ_Stock_CloseInstall = 1,
	           HQ_Stock_GameLibrary = 1,
	           HQ_Stock_TerminateMachine = 1,
	           HQ_AuditViewer = 1,
	           HQ_CashierTransactions = 1,
	           HQ_Admin_Customers_Company_New = 1,
	           HQ_Admin_Customers_Sub_New = 1,
	           HQ_Admin_Customers_Site_New = 1,
	           HQ_Admin_Customers_Zone_New = 1,
	           HQ_Admin_Customers_Bar_New = 1,
	           HQ_Admin_Customers_Bar_BulkCopyPositions = 1,
	           HQ_Guardian = 1,
	           HQ_CashierTransactions_ViewNumberTickets = 1,
	           HQ_Financial_Period_End = 1,
	           HQ_DataSheet = 1,
	           HQ_Admin_Declaration = 1,
	           HQ_Admin_StackerLevelAlert = 1,
	           HQ_Admin_DropSchedule = 1,
	           HQ_Admin_Access = 1,
	           HQ_Admin_StackerFeature = 1,
	           HQ_Admin_SiteLicensing = 1,
	           HQ_Admin_SiteLicensing_LicenseGen = 1,
	           HQ_Admin_SiteLicensing_LicenseGen_RuleInfo = 1,
	           HQ_Admin_SiteLicensing_LicenseGen_KeyGen = 1,
	           HQ_Admin_SiteLicensing_ViewCancelLicense = 1,
	           HQ_Admin_SiteLicensing_LicenseHistory = 1,
	           HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule = 1,
	           HQ_Admin_ExpenseShare = 1,
	           HQ_Admin_WriteOffExpense = 1,
	           HQ_Admin_PayPeriod = 1,
	           HQ_DeMerge_Batch = 1,
	           HQ_Financial_ShareHolder = 1,
	           HQ_Financial_ProfitShare = 1,
	           HQ_Financial_ExpenseShare = 1,
	           HQ_Admin_Groups_EmployeeCardTracking = 1,
	           HQ_Financial_Liquidation = 1,
	           HQ_Admin_Liquidation = 1,
	           HQ_Financial_ReadLiquidation = 1,
	           HQ_Stock_Asset_Export = 1,
	           HQ_Stock_Asset_Import = 1,
	           HQ_Asset_Template = 1,
	           HQ_Template_Create = 1,
	           HQ_Template_Edit = 1,
	           HQ_Template_Delete = 1,
	           HQ_Admin_Customers_Bar_Route = 1,
	           HQ_Admin_AGSCombination = 1,
               HQ_Financial_CollectionLiquidation = 1,
               HQ_Admin_VaultInterface=1,
               HQ_Admin_EmployeeCard =1,
               HQ_Financial_ReadLiquidationReport = 1,
               HQ_SystemAudit = 1,
			   HQ_DataCommsAudit = 1,
			   HQ_Admin_CreateVault=1,
			   HQ_Admin_EditCreateVault=1,
			   HQ_Admin_VaultDeclaration=1,
			   HQ_Admin_EditVaultDeclaration=1,
			   HQ_Admin_AuditVaultDeclaration=1,
			   HQ_Admin_EditAuditVaultDeclaration=1,
			   HQ_Admin_TerminateVault=1,
			   HQ_Admin_AuthorizeProfitShare = 1,
			   HQ_Admin_SiteUserAccess = 1,
			   HQ_Admin_MeterAdjustment = 1,
			   HQ_Admin_Customers_Bar_EnableDisable = 1,
			   HQ_Financial_TermsProfiles=1,
			   HQ_Financial_ShareSchedules=1,
			   HQ_Financial_TermsSummary=1,
			   HQ_Financial_PeriodEndTermsProcessor=1,
 			   HQ_Engineer_EmailAlerts = 1,
			   HQ_Engineer_AlertAudit = 1,
			   HQ_Engineers_EventViewer=1,
			   HQ_Admin_Users_ResetPassword=1,
			   HQ_Admin_Users_Unlock=1,
	           HQ_GMU_Events =1,
			   HQ_GMU_Events_Add = 1,
			   HQ_GMU_Events_Edit =1 
	    WHERE  HQ_User_Access_ID = @User_Access_ID
	END
	ELSE
	BEGIN
	    SET @IsSuperUser = 0
	END  
	DECLARE @SecurityRoleID INT
	SELECT @SecurityRoleID = SecurityRoleID
	FROM   [ROLE] r
	WHERE  r.RoleName = 'Super User'
	
	DELETE 
	FROM   RoleAccessRole_lnk
	WHERE  SecurityRoleID = @SecurityRoleID
	
	INSERT INTO RoleAccessRole_lnk
	SELECT @SecurityRoleID,
	       RoleAccessID
	FROM   RoleAccess
	WHERE  IsEnabled = 'Y'
	
	DECLARE @client AS VARCHAR(100)      
	SET @client = ''      
	EXEC rsp_GetSetting NULL,
	     'CLIENT',
	     '',
	     @client OUTPUT      
	
	DECLARE @powerpromo AS VARCHAR(10)      
	SET @powerpromo = NULL      
	EXEC rsp_GetSetting NULL,
	     'ISPOWERPROMOREPORTSREQUIRED',
	     '',
	     @powerpromo OUTPUT      
	
	DECLARE @SGVISetting AS VARCHAR(100)      
	SET @SGVISetting = ''      
	EXEC rsp_GetSetting NULL,
	     'SGVI_Enabled',
	     '',
	     @SGVISetting OUTPUT      
	
	
	DECLARE @IsEmployeeTrackingEnabled AS VARCHAR(100)  
	SET @IsEmployeeTrackingEnabled = ''  
	EXEC rsp_GetSetting NULL,
	     'IsEmployeeCardTrackingEnabled',
	     '',
	     @IsEmployeeTrackingEnabled OUTPUT  
	
	
	DECLARE @IsStackerFeatureEnabled AS VARCHAR(100)  
	SET @IsStackerFeatureEnabled = ''  
	EXEC rsp_GetSetting NULL,
	     'StackerFeature',
	     '',
	     @IsStackerFeatureEnabled OUTPUT  
	
	DECLARE @IsDropScheduleAlertEnabled AS VARCHAR(100)  
	SET @IsDropScheduleAlertEnabled = ''  
	EXEC rsp_GetSetting NULL,
	     'DropScheduleAlert',
	     '',
	     @IsDropScheduleAlertEnabled OUTPUT  
	
	DECLARE @IsSiteLicensingEnabled AS VARCHAR(100)  
	SET @IsSiteLicensingEnabled = ''  
	EXEC rsp_GetSetting NULL,
	     'IsSiteLicensingEnabled',
	     '',
	     @IsSiteLicensingEnabled OUTPUT  
	     
	DECLARE @IsVaultEnabled AS VARCHAR(100)
	SET @IsVaultEnabled = ''
	EXEC rsp_GetSetting NULL,
	     'IsVaultEnabled',
	     '',
	     @IsVaultEnabled OUTPUT
	
	DECLARE @IsGameCappingEnabled as varchar(10)
	SET @IsGameCappingEnabled=''
	EXEC rsp_GetSetting NULL, 'IsGameCappingEnabled', '', @IsGameCappingEnabled OUTPUT
	     
	DECLARE @LiquidationProfitShare AS VARCHAR(10)      
	SET @LiquidationProfitShare = ''      
	EXEC rsp_GetSetting NULL, 'LiquidationProfitShare', '', @LiquidationProfitShare OUTPUT
	
	IF (ISNULL(@client, '') = '')
	    SET @client = NULL
	ELSE 
	IF @client = '0'
	    SET @client = NULL      
	
	
	
	DELETE 
	FROM   ReportsMenuAccess
	WHERE  SecurityRoleID = @SecurityRoleID
	
	INSERT INTO ReportsMenuAccess
	SELECT @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       LEFT  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  ISNULL(RM.Client, '') = '' 
	       --AND ISNULL(UPPER(RM.MS_ProcedureUsed),'') <> UPPER('rsp_REPORT_BankingReport')
	       AND ISNULL(RMParent.Client, '') = ''
	       AND LTRIM(RTRIM(RMParent.ReportName)) <> 'Main Menu'
	       AND (
	               UPPER(LTRIM(RTRIM(RM.ShowPowerPromoReports))) = 'FALSE'
	               OR RM.ShowPowerPromoReports IS NULL
	           )
	       AND UPPER(ISNULL(RMParent.Client, '')) <> UPPER(ISNULL(@client, ''))
	       AND LTRIM(RTRIM(RM.ReportName)) 
	           NOT IN ('Employee Card List Report', 
	                  'Employee Card Sessions Report', 
	                  'Stacker Level Details Report', 
	                  'Drop Schedule Details Report', 'License History Report', 
	                  'Site Licensing Reports',  
	                  'License Expiry Report',
					  'Total Funds In Summary Report',
					  'Total Funds In Detail Report',
					  'Cash Dispenser Cassettes Inventory Status',
					  'Cash Dispenser Configuration Details',		
					  'Cash Dispenser Cassette Accounting Detail',			  
	                  'Cash Dispenser Drop Report', 
	                  'Cash Dispenser Inventory Status Report',
	                  'Cash Dispenser Level Details',
	                  'Cash Dispenser Transaction Details',
	                  'Cash Dispenser Variance Report',
	                  'Capped Game Summary Report',
	                  'Capped Game List Report',
					  'Fixed Expense Details Report',
					  'Period-End Liquidation Revenue Report',
					  'Cash Dispenser Accounting Report') 
	
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       LEFT  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  ISNULL(UPPER(RM.Client), '') = ISNULL(UPPER(@client), '')
	       AND LTRIM(RTRIM(RMParent.ReportName)) <> 'Main Menu'
	       AND (
	               ISNULL(RM.ShowPowerPromoReports, '') = ISNULL(UPPER(@powerpromo), '')
	               AND UPPER(@powerpromo) = 'TRUE'
	           ) 
	
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       LEFT  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  ISNULL(UPPER(RM.Client), '') = ISNULL(UPPER(@client), '')
	       AND LTRIM(RTRIM(RMParent.ReportName)) <> 'Main Menu'
	       AND ISNULL(RMParent.Client, '') = @Client
	       AND @client = 'SGVI'
	       AND @SGVISetting = 'TRUE' 
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) = 'Accounting Machine Win/Loss Report' 
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Employee Card Sessions Report', 
	                                      'Employee Card List Report')
	       AND @IsEmployeeTrackingEnabled = 'True'
	       
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Capped Game Summary Report','Capped Game List Report')
	       AND @IsGameCappingEnabled = 'True' 
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Fixed Expense Details Report','Period-End Liquidation Revenue Report')
	       AND @LiquidationProfitShare = 'True'  
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) = 'Stacker Level Details Report'
	       AND @IsStackerFeatureEnabled = 'True' 
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) = 'Drop Schedule Details Report'
	       AND @IsStackerFeatureEnabled = 'True'
	       AND @IsDropScheduleAlertEnabled = 'True' 
	
	UNION
	SELECT DISTINCT 
	       @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Site Licensing Reports', 
	                                      'License History Report', 	                                      
	                                      'License Expiry Report')
	       AND @IsSiteLicensingEnabled = 'True'
	
	UNION
	SELECT DISTINCT
		   @SecurityRoleID,
	       RM.ReportID
	FROM   ReportsMenu RM
	       INNER  JOIN ReportsMenu RMParent
	            ON  ISNULL(RMParent.ReportID, '') = ISNULL(RM.ReportMenuID, '')
	WHERE  LTRIM(RTRIM(RM.ReportName)) IN ('Cash Dispenser Configuration Details', 
	                                      'Cash Dispenser Cassettes Inventory Status', 
	                                      'Cash Dispenser Drop Report', 
	                                      'Cash Dispenser Inventory Status Report',
	                                      'Cash Dispenser Level Details',
	                                      'Cash Dispenser Transaction Details',
	                                      'Cash Dispenser Variance Report',
	                                      'Total Funds In Detail Report',
	                                      'Total Funds In Summary Report',
										  'Cash Dispenser Cassette Accounting Detail',
	                                      'Cash Dispenser Accounting Report')
	       AND @IsVaultEnabled = 'True'
		
END  
GO 

