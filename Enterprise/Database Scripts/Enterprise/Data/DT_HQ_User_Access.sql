USE [Enterprise]
GO
IF NOT EXISTS(SELECT 1 FROM [HQ_User_Access] WHERE HQ_Admin_Users = '1')
BEGIN

    INSERT [HQ_User_Access] ( HQ_Admin, HQ_Admin_Customers, HQ_Admin_Customers_Company, HQ_Admin_Customers_Company_Edit, HQ_Admin_Customers_Sub, HQ_Admin_Customers_Sub_Edit, HQ_Admin_Customers_Site, HQ_Admin_Customers_Site_Edit, HQ_Admin_Customers_Zone, HQ_Admin_Customers_Zone_Edit, HQ_Admin_Customers_Bar, HQ_Admin_Customers_Bar_Edit, HQ_Admin_Groups, HQ_Admin_Groups_Edit, HQ_Admin_Users, HQ_Admin_Users_ViewAll, HQ_Admin_Users_Edit, HQ_Admin_Users_EditAll, HQ_Admin_Settings, HQ_Admin_Settings_Edit, HQ_Admin_Routes, HQ_Admin_Routes_Edit, HQ_Admin_Depot, HQ_Admin_Depot_Edit, HQ_Admin_Operator, HQ_Admin_Operator_Edit, HQ_Admin_Accessories, HQ_Admin_Calendar, HQ_Admin_Exception, HQ_Admin_Op_Cost, HQ_Admin_Licence, HQ_Admin_OpenHours, HQ_Admin_SiteUpdate, HQ_Browse, HQ_Search, HQ_Sites, HQ_Engineers, HQ_Engineers_Current, HQ_Engineers_Current_Edit, HQ_Engineers_Current_Close, HQ_Engineers_Closed, HQ_Engineers_Closed_Edit, HQ_Engineers_Engineers, HQ_Engineers_Engineers_Edit, 
           HQ_Engineers_Schedule, HQ_Engineers_Schedule_Edit, HQ_Engineers_Notes, HQ_Engineers_Notes_History, HQ_Stock, HQ_Stock_Edit, HQ_Stock_MachinesInUse, HQ_Stock_MachinesInUse_Edit, HQ_Stock_MachinesUnderRepair, HQ_Stock_MachinesUnderRepair_Edit, HQ_Stock_UsableStock, HQ_Stock_UsableStock_Edit, HQ_Stock_MachinesOnOrder, HQ_Stock_MachinesOnOrder_Edit, HQ_Stock_Depreciation, HQ_Stock_Model_Sets, HQ_Stock_Component, HQ_Stock_Supplier, HQ_Stock_Manufacturer, HQ_Stock_Machine_Type, HQ_Stock_Machine_Model, HQ_Stock_Movement_History, HQ_Stock_Machine_History, HQ_Stock_Convert_Machine, HQ_Logistics, HQ_Logistics_Create, HQ_Logistics_Positions, HQ_Logistics_Models, HQ_Logistics_Assets, HQ_Logistics_Authorise, HQ_Logistics_Complete, HQ_Licensing, HQ_Licensing_Purchase, HQ_Licensing_Sell, HQ_CDS, HQ_CDS_Edit, HQ_CDS_Programs_View_Tab, HQ_CDS_Programs_View_Tab_Edit, HQ_CDS_Programs_Edit_Tab, HQ_CDS_Programs_Edit_Tab_Edit, HQ_CDS_Stock_Tab, HQ_CDS_Stock_Tab_Edit, HQ_CDS_Label_Tab, 
           HQ_CDS_Label_Tab_Edit, HQ_Comms, HQ_Comms_CE, HQ_Comms_Site, HQ_Comms_Site_Accept, HQ_Comms_Site_Accept_Edit, HQ_Comms_Site_Inbox, HQ_Comms_Site_Inbox_Edit, HQ_Comms_Site_Outbox, HQ_Comms_Site_Outbox_Edit, HQ_Comms_Mail, HQ_Comms_Messages, HQ_Comms_Move_Rej, HQ_Comms_Col_Rejects, HQ_Comms_New_Move_Rej, HQ_Comms_New_Col_Rej, HQ_Comms_Amedis, HQ_Comms_Export, HQ_Shedules, HQ_Shedules_Edit, HQ_Collections, HQ_Collections_docket, HQ_Collections_Collection_Manager, HQ_Collections_Col_Mismatch, HQ_Collections_Edit_Remarks, HQ_Collections_Royalty, HQ_Financial, HQ_Financial_Edit, HQ_Financial_Terms, HQ_Financial_Terms_Edit, HQ_Financial_Terms_Summary, HQ_Financial_Shares, HQ_Financial_Shares_Edit, HQ_Financial_Rent, HQ_Financial_Rent_Edit, HQ_Financial_Export, HQ_Financial_Site_Budget, HQ_Financial_Giro_Rec, HQ_Reports, HQ_Reports_Edit, HQ_Analysis, HQ_Analysis_Edit, HQ_Analysis_Collection, HQ_Analysis_Collection_Edit, HQ_Analysis_Sessional, HQ_Analysis_Sessional_Edit, 
           HQ_Analysis_Comparison, HQ_Analysis_Comparison_Edit, HQ_Analysis_Advanced, HQ_Analysis_Advanced_Edit, HQ_SUPER, HQ_Collections_Edit, HQ_Customer_Access_View_Entire_Database, HQ_Collections_Site_Viewer_Input, HQ_Security, HQ_Security_Edit_EDC_Machine_Code, HQ_Security_Edit_Meter_Readings, HQ_Comms_CE_Reject, HQ_Comms_Site_Reject, HQ_Collections_Manager_Input, HQ_Collections_Manager_Var_Rec, HQ_Collections_Manager_Col_Rec, HQ_Collections_Site_Viewer_Input_New, HQ_Movements_Site_Viewer_Fudge, hq_stock_machine_control, HQ_EventViewer, HQ_Calendar_Edit, HQ_Sites_MC_Control, HQ_Admin_SiteSettings, HQ_Admin_AAMSConfig, HQ_Gamelibrary, HQ_Stock_BuyMachine, HQ_Stock_NewInstall, HQ_Stock_EditInstall, HQ_Stock_CloseInstall, HQ_Stock_GameLibrary, HQ_Stock_TerminateMachine, HQ_AuditViewer, HQ_CashierTransactions, HQ_Admin_Customers_Company_New, HQ_Admin_Customers_Sub_New, HQ_Admin_Customers_Site_New, HQ_Admin_Customers_Zone_New, HQ_Admin_Customers_Bar_New, 
		   HQ_Admin_Customers_Bar_BulkCopyPositions, HQ_Guardian, HQ_CashierTransactions_ViewNumberTickets, HQ_Financial_Period_End, HQ_DataSheet, HQ_Admin_Declaration, HQ_Admin_StackerLevelAlert, HQ_Admin_DropSchedule, HQ_Admin_Access, HQ_DeMerge_Batch, HQ_Admin_StackerFeature, HQ_Admin_SiteLicensing, HQ_Admin_SiteLicensing_LicenseGen, HQ_Admin_SiteLicensing_LicenseGen_RuleInfo, HQ_Admin_SiteLicensing_LicenseGen_KeyGen, HQ_Admin_SiteLicensing_ViewCancelLicense, HQ_Admin_SiteLicensing_LicenseHistory, HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule, HQ_Admin_ExpenseShare, HQ_Admin_WriteOffExpense, HQ_Admin_PayPeriod, HQ_Financial_ShareHolder, HQ_Financial_ProfitShare, HQ_Financial_ExpenseShare, HQ_Admin_Groups_EmployeeCardTracking, HQ_Financial_Liquidation, HQ_Admin_Liquidation, HQ_Financial_ReadLiquidation, HQ_Stock_Asset_Export, HQ_Stock_Asset_Import, HQ_Asset_Template, HQ_Template_Create, HQ_Template_Edit, HQ_Template_Delete, HQ_Admin_Customers_Bar_Route
           ,HQ_Admin_VaultInterface,HQ_Admin_CreateVault,HQ_Admin_EditCreateVault,HQ_Admin_VaultDeclaration,HQ_Admin_EditVaultDeclaration,HQ_Admin_AuditVaultDeclaration,HQ_Admin_EditAuditVaultDeclaration, HQ_Admin_Customers_Bar_EnableDisable)
    SELECT NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, 
           NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
		   ,NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
END
GO

IF  EXISTS(SELECT 1 FROM SYS.columns  WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[HQ_User_Access]') AND name = 'HQ_GMU_Events')
	UPDATE [DBO].[HQ_User_Access] SET HQ_GMU_Events =  NULL
	
IF  EXISTS(SELECT 1 FROM SYS.columns  WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[HQ_User_Access]') AND name = 'HQ_GMU_Events_Add')
	UPDATE [DBO].[HQ_User_Access] SET HQ_GMU_Events_Add =  NULL
	
IF  EXISTS(SELECT 1 FROM SYS.columns  WHERE OBJECT_ID = OBJECT_ID(N'[DBO].[HQ_User_Access]') AND name = 'HQ_GMU_Events_Edit')
	UPDATE [DBO].[HQ_User_Access] SET HQ_GMU_Events_Edit =  NULL	
GO	

   