USE Enterprise
GO

EXEC usp_InsertUser_Access '', 'HQ_Admin', 'Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Customers', 'Organisation'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers', 'HQ_Admin_Customers_Company', 'CompanyName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Company', 'HQ_Admin_Customers_Company_New', 'New Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Company', 'HQ_Admin_Customers_Company_Edit', 'Edit Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Company_Edit', 'HQ_Admin_Customers_Sub', 'SubCompanyName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Sub', 'HQ_Admin_Customers_Sub_New', 'New SubCompanyName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Sub', 'HQ_Admin_Customers_Sub_Edit', 'Edit SubCompanyName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Sub_Edit', 'HQ_Admin_Customers_Site', 'SiteName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Site', 'HQ_Admin_Customers_Site_New', 'New SiteName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Site', 'HQ_Admin_Customers_Site_Edit', 'Edit SiteName Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Site_Edit', 'HQ_Admin_Customers_Zone', 'ZoneName'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Zone', 'HQ_Admin_Customers_Zone_New', 'New ZoneName'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Zone', 'HQ_Admin_Customers_Zone_Edit', 'Edit ZoneName'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Site_Edit', 'HQ_Admin_Customers_Bar', 'Position Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Bar', 'HQ_Admin_Customers_Bar_New', 'New Position Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Bar', 'HQ_Admin_Customers_Bar_Edit', 'Edit Position Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Bar', 'HQ_Admin_Customers_Bar_BulkCopyPositions', 'Bulk Copy Position Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Access', 'Access'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Groups', 'Groups'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Groups', 'HQ_Admin_Groups_Edit', 'Edit Groups'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Users', 'Users'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Users', 'HQ_Admin_Users_ViewAll', 'View All'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Users', 'HQ_Admin_Users_Edit', 'Edit Current User Only'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Users', 'HQ_Admin_Users_ResetPassword', 'Reset Password'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Users', 'HQ_Admin_Users_Unlock', 'Unlock'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Users', 'HQ_Admin_Users_EditAll', 'Edit Other Users Only'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Settings', 'Settings'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Settings', 'HQ_Admin_Settings_Edit', 'Edit'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Depot', 'Depot'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Depot', 'HQ_Admin_Depot_Edit', 'Edit'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Operator', 'Operator'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Operator', 'HQ_Admin_Operator_Edit', 'Edit'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Calendar', 'Calendar'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Calendar', 'HQ_Calendar_Edit', 'Edit'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_OpenHours', 'Opening Hours'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_SiteSettings', 'Site Settings'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_Declaration', 'Drop Declaration'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_StackerFeature', 'Stacker Feature'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_DropSchedule', 'Drop Schedule'
GO
Exec usp_InsertUser_Access '','HQ_Sites', 'Sites'
GO
Exec usp_InsertUser_Access 'HQ_Sites', 'HQ_Stock_Machine_Control', 'Machine Control'
GO
Exec usp_InsertUser_Access 'HQ_Sites', 'HQ_Collections', 'Modify Drop'
GO
Exec usp_InsertUser_Access 'HQ_Sites', 'HQ_CashierTransactions', 'CashierTransactions'
GO
Exec usp_InsertUser_Access 'HQ_CashierTransactions', 'HQ_CashierTransactions_ViewNumberTickets', 'View Voucher Numbers'
GO
Exec usp_InsertUser_Access '', 'HQ_Gamelibrary', 'Game Library'
GO
Exec usp_InsertUser_Access '', 'HQ_Engineers', 'Service Calls'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineers_CreateCall', 'Create Call'
GO
Exec usp_InsertUser_Access 'HQ_Engineers_CreateCall', 'HQ_Engineers_CreateCall_Edit', 'Edit'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineers_Current', 'Current Call'
GO
Exec usp_InsertUser_Access 'HQ_Engineers_Current', 'HQ_Engineers_Current_Edit', 'Edit'
GO
Exec usp_InsertUser_Access 'HQ_Engineers_Current', 'HQ_Engineers_Current_Close', 'Close Open Call'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineers_Closed', 'Closed Call'
GO
Exec usp_InsertUser_Access 'HQ_Engineers_Closed', 'HQ_Engineers_Closed_Edit', 'Edit'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineers_Engineers', 'Service Admin'
GO
Exec usp_InsertUser_Access 'HQ_Engineers_Engineers', 'HQ_Engineers_Engineers_Edit', 'Edit'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineers_Schedule', 'M/c Protocol Fault Codes'
GO
Exec usp_InsertUser_Access '', 'HQ_Stock', 'Stock'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_BuyMachine', 'Buy Machine'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_TerminateMachine', 'Terminate Machine'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Edit', 'Edit Machine'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Machine_History', 'Machine History'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Depreciation', 'Depreciation'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Manufacturer', 'Manufacturer Admin'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Supplier', 'Operator Admin'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'hq_stock_machine_type', 'Machine Type Admin'
GO
Exec usp_InsertUser_Access '', 'HQ_Financial', 'Financial'
GO
--Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_Terms', 'Terms'
--GO
--Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_Shares', 'Shares'
--GO
--Exec usp_InsertUser_Access 'HQ_Financial_Shares', 'HQ_Financial_Shares_Edit', 'Edit'
--GO
--Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_Terms_Summary', 'Terms Summary'
--GO
--Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_Period_End', 'Period End'
GO
Exec usp_InsertUser_Access '', 'HQ_Reports', 'Reports'
GO
Exec usp_InsertUser_Access '', 'HQ_DataSheet', 'DataSheet'
GO
Exec usp_InsertUser_Access '', 'HQ_Analysis', 'Analysis'
GO
Exec usp_InsertUser_Access '', 'HQ_AuditViewer', 'Audit & Monitoring'
GO
Exec usp_InsertUser_Access 'HQ_AuditViewer', 'HQ_Guardian', 'Guardian Website'
GO
Exec usp_InsertUser_Access '', 'HQ_Customer_Access_View_Entire_Database', 'View All Customers (Customer access override)'
GO
Exec usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_AGSCombination', 'AGS Combination'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Asset_Template', 'Asset Template'
GO
Exec usp_InsertUser_Access 'HQ_Asset_Template', 'HQ_Template_Create', 'Create Template'
GO
Exec usp_InsertUser_Access 'HQ_Asset_Template', 'HQ_Template_Edit', 'Edit Template'
GO
Exec usp_InsertUser_Access 'HQ_Asset_Template', 'HQ_Template_Delete', 'Delete Template'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Asset_Export', 'Export Template Details'
GO
Exec usp_InsertUser_Access 'HQ_Stock', 'HQ_Stock_Asset_Import', 'Import Template Details'
GO
Exec usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_SiteLicensing', 'Site Licensing'
GO
Exec usp_InsertUser_Access 'HQ_Admin_SiteLicensing', 'HQ_Admin_SiteLicensing_LicenseGen', 'License Generation'
GO
Exec usp_InsertUser_Access 'HQ_Admin_SiteLicensing_LicenseGen', 'HQ_Admin_SiteLicensing_LicenseGen_RuleInfo', 'Rule Information'
GO
Exec usp_InsertUser_Access 'HQ_Admin_SiteLicensing_LicenseGen_RuleInfo', 'HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule', 'Add/Edit Rule'
GO
Exec usp_InsertUser_Access 'HQ_Admin_SiteLicensing_LicenseGen_RuleInfo', 'HQ_Admin_SiteLicensing_LicenseGen_KeyGen', 'Key Generation'
GO
Exec usp_InsertUser_Access 'HQ_Admin_SiteLicensing', 'HQ_Admin_SiteLicensing_ViewCancelLicense', 'View/Cancel/Active License'
GO
Exec usp_InsertUser_Access 'HQ_Admin_SiteLicensing', 'HQ_Admin_SiteLicensing_LicenseHistory', 'License History'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_ShareHolder', 'Share Holder'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_ProfitShare', 'Profit Share'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_ExpenseShare', 'Expense Share'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_ReadLiquidation', 'Read Based Liquidation'
GO
Exec usp_InsertUser_Access 'HQ_Financial_ReadLiquidation', 'HQ_Financial_ReadLiquidationReport', 'Read Liquidation Report'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_CollectionLiquidation', 'Collection Based Liquidation'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Bar', 'HQ_Admin_Routes', 'Route Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Bar', 'HQ_Admin_Routes_Edit', 'Edit Route Admin'
GO
EXEC usp_InsertUser_Access 'HQ_Admin', 'HQ_Admin_EmployeeCard', 'View Employee card tracking'
GO
Exec usp_InsertUser_Access 'HQ_AuditViewer', 'HQ_SystemAudit', 'System Audit'
GO
Exec usp_InsertUser_Access 'HQ_AuditViewer', 'HQ_DataCommsAudit', 'Data Comms Audit'
GO
EXEC usp_InsertUser_Access '', 'HQ_Admin_VaultInterface', 'Vault'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_VaultInterface', 'HQ_Admin_CreateVault', 'Create Vault'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_CreateVault', 'HQ_Admin_EditCreateVault', 'Edit Vault'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_VaultInterface', 'HQ_Admin_VaultDeclaration', 'Vault Declaration'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_VaultDeclaration', 'HQ_Admin_EditVaultDeclaration', 'Edit Vault Declaration'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_VaultInterface', 'HQ_Admin_AuditVaultDeclaration', 'Vault Audit'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_AuditVaultDeclaration', 'HQ_Admin_EditAuditVaultDeclaration', 'Edit Vault Audit'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_CreateVault', 'HQ_Admin_TerminateVault', 'Terminate Vault'
GO
EXEC usp_InsertUser_Access 'HQ_Financial','HQ_Admin_AuthorizeProfitShare','Authorize Profit Share'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Users','HQ_Admin_SiteUserAccess','Site User Access'
GO 
EXEC usp_InsertUser_Access '', 'HQ_Admin_MeterAdjustment', 'Meter Adjustment'
GO
EXEC usp_InsertUser_Access 'HQ_Admin_Customers_Bar', 'HQ_Admin_Customers_Bar_EnableDisable', 'Enable/Disable Bar Position'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_TermsProfiles', 'Terms Profiles'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_ShareSchedules', 'Share Schedules'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_TermsSummary', 'Terms Summary'
GO
Exec usp_InsertUser_Access 'HQ_Financial', 'HQ_Financial_PeriodEndTermsProcessor', 'Period End Terms Processor'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineer_EmailAlerts', 'Email Alert'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineer_AlertAudit', 'Audit for Alerts'
GO
Exec usp_InsertUser_Access 'HQ_Engineers', 'HQ_Engineers_EventViewer', 'Event Viewer'

EXEC usp_InsertUser_Access 'HQ_Engineers','HQ_GMU_Events','GMU Events'
GO 

EXEC usp_InsertUser_Access 'HQ_GMU_Events','HQ_GMU_Events_Add','Add GMU Events'
GO 

EXEC usp_InsertUser_Access 'HQ_GMU_Events','HQ_GMU_Events_Edit','Edit GMU Events'
GO 