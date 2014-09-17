/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 05/04/13 8:37:14 PM
 ************************************************************/

USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'User Group/Site Admin')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 500, 'User Group/Site Admin'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'User Site Admin')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 501, 'User Site Admin'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Centralised Settings')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 502, 'Centralised Settings'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'General')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 503, 'General'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Model Type')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 504, 'Model Type'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'GMU Events')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 505, 'GMU Events'

DELETE FROM [Audit_Modules] WHERE Audit_Module_ID = '506'
IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'ServiceCalls')
BEGIN	
	INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 506, 'ServiceCalls'   
END
    

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'CentralDeclaration')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 533, 'CentralDeclaration'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Sub Company')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 508, 'Sub Company'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Site')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 560, 'Site'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Position')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 559, 'Position'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Company')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 511, 'Company'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Calendars')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 512, 'Calendars'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Operators')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 513, 'Operators'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Depot')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 514, 'Depot'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Settings')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 515, 'Settings'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Users')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 516, 'Users'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Customer Access')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 517, 'Customer Access'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'User Group')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 518, 'User Group'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Game Library')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 519, 'Game Library'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Game Model')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 520, 'Game Model'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Purchase Machine')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 521, 'Purchase Machine'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'DropAlert')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 534, 'DropAlert'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Machine Type')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 523, 'Machine Type'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Manufacturer')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 524, 'Manufacturer'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'User Reports Admin')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 525, 'User Reports Admin'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'ComponentVerification')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 530, 'ComponentVerification'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Shortpay')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 531, 'Shortpay'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'OfflineVoucher_Shortpay')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 532, 'OfflineVoucher_Shortpay'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Stacker')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 535, 'Stacker'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'SiteLicensing')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 536, 'SiteLicensing'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'MergeBatch')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 537, 'MergeBatch'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'DeMergeBatch')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 538, 'DeMergeBatch'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Share Holder')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 539, 'Share Holder'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Profit Share')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 540, 'Profit Share'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Expense Share')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 541, 'Expense Share'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Asset Template')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 543, 'Asset Template'
    
IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Collection Based Liquidation')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 544, 'Collection Based Liquidation'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Read Based Liquidation')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 545, 'Read Based Liquidation'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'ExportDetail')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 546, 'ExportDetail'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Unlock')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 547, 'Unlock'

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Route')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 549, 'Route'

GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'VaultManager')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 551, 'VaultManager'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'FillVault')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 710, 'FillVault'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Import/Export Asset File')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 552, 'Import/Export Asset File'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'UpdateGmuNo')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 555, 'UpdateGmuNo'
GO
IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'RebootGMU')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 556, 'RebootGMU'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'EmployeeCard')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 557, 'EmployeeCard'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'EnableDisableMachine')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 558, 'EnableDisableMachine'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'AUDIT_GAMECAPPING')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 561, 'AUDIT_GAMECAPPING'
GO


IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'AUDIT_TERMINATEMACHINE')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 562, 'AUDIT_TERMINATEMACHINE'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Login')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 5, 'Login'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Logout')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 6, 'Logout'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'ChangePassword')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 7, 'ChangePassword'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'Maintenance')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 563, 'Maintenance'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'MultipleVoucherRedeem')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 714, 'MultipleVoucherRedeem'
GO

IF NOT EXISTS(SELECT 1 FROM [Audit_Modules]WHERE Audit_Module_Name = 'SGVI Financial')
    INSERT [Audit_Modules] ( Audit_Module_ID, Audit_Module_Name )
    SELECT 718, 'SGVI Financial'
GO

--TRUNCATE TABLE  [Audit_Modules]

