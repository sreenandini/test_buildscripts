USE [ENTERPRISE]
GO
IF EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE name LIKE 'Export_History_Types' AND TYPE ='U')
BEGIN
TRUNCATE TABLE dbo.Export_History_Types

INSERT INTO Export_History_Types VALUES ('ADDUSER', 'New User')
INSERT INTO Export_History_Types VALUES ('AFTENABLEDISABLE', 'Aft Enable Disable')
INSERT INTO Export_History_Types VALUES ('AFTSETTINGS', 'Aft Settings')
INSERT INTO Export_History_Types VALUES ('AUTOINSTALLATION', 'Auto Installation')
INSERT INTO Export_History_Types VALUES ('CHANGEPASSWORD', 'Change Password')
INSERT INTO Export_History_Types VALUES ('CMPGAMETYPE', 'CMP Game Type')
INSERT INTO Export_History_Types VALUES ('CODEMASTER', 'Code Master')
INSERT INTO Export_History_Types VALUES ('CROSSTICKETING', 'Cross Ticketing')
INSERT INTO Export_History_Types VALUES ('DECCOLLBATCH', 'Drop Declaration')
INSERT INTO Export_History_Types VALUES ('DISABLESITE', 'Disable Site')
INSERT INTO Export_History_Types VALUES ('ENABLESITE', 'Enable Site')
INSERT INTO Export_History_Types VALUES ('EXPENSESHARE', 'Expense Share')
INSERT INTO Export_History_Types VALUES ('EXPENSESHAREGROUP', 'Expense Share Group')
INSERT INTO Export_History_Types VALUES ('FACTORYRESET_STATUS', 'Factory Reset Status')
INSERT INTO Export_History_Types VALUES ('GAMECATEGORY', 'Game Category')
INSERT INTO Export_History_Types VALUES ('GAMELIBRARY', 'Add/Modify Game')
INSERT INTO Export_History_Types VALUES ('GAMELIBRARY_MAPPING', 'Game Library Mapping')
INSERT INTO Export_History_Types VALUES ('GAMETITLE', 'Game Title')
INSERT INTO Export_History_Types VALUES ('GETCOLLBYDATE', 'Get Drop By Date')
INSERT INTO Export_History_Types VALUES ('LANGUAGELOOKUP', 'Language Look Up')
INSERT INTO Export_History_Types VALUES ('LIQUIDATIONDETAILS', 'Liquidation Details')
INSERT INTO Export_History_Types VALUES ('LIQUIDATIONSHAREDETAILS', 'Liquidation Share Details')
INSERT INTO Export_History_Types VALUES ('LOOKUPMASTER', 'Look Up Master')
INSERT INTO Export_History_Types VALUES ('MACHINEDISABLE', 'Disable Machine')
INSERT INTO Export_History_Types VALUES ('MACHINEENABLE', 'Enable Machine')
INSERT INTO Export_History_Types VALUES ('MACHINEUPDATE', 'Update Machine')
INSERT INTO Export_History_Types VALUES ('MANUFACTURER_DETAILS', 'Manufacturer Details')
INSERT INTO Export_History_Types VALUES ('MASTERCARDENABLE', 'Employee Card')
INSERT INTO Export_History_Types VALUES ('MODEL', 'Model')
INSERT INTO Export_History_Types VALUES ('NOTEACCEPTORDISABLE', 'Note Acceptor Disable')
INSERT INTO Export_History_Types VALUES ('NOTEACCEPTORENABLE', 'Note Acceptor Enable')
INSERT INTO Export_History_Types VALUES ('O-CALENDAR', 'Operator Calendar')
INSERT INTO Export_History_Types VALUES ('PAYTABLE', 'Pay Table')
INSERT INTO Export_History_Types VALUES ('PROFITSHARE', 'Profit Share')
INSERT INTO Export_History_Types VALUES ('PROFITSHAREGROUP', 'Profit Share Group')
INSERT INTO Export_History_Types VALUES ('REMOVEUSER', 'Delete User')
INSERT INTO Export_History_Types VALUES ('RESETMASTERCARDFLAG', 'Reset Master Card Flag')
INSERT INTO Export_History_Types VALUES ('ROLEACCESSLINK', 'User Access')
INSERT INTO Export_History_Types VALUES ('ROUTE', 'Route')
INSERT INTO Export_History_Types VALUES ('S-CALENDAR', 'Site Calendar')
INSERT INTO Export_History_Types VALUES ('SHAREHOLDER', 'Share Holder')
INSERT INTO Export_History_Types VALUES ('SITELICENSING', 'Site Licensing')
INSERT INTO Export_History_Types VALUES ('ACTIVELICENSE', 'Activate License')
INSERT INTO Export_History_Types VALUES ('SITESETTINGS', 'Site Settings')
INSERT INTO Export_History_Types VALUES ('SITESETUP', 'Site Setup')
INSERT INTO Export_History_Types VALUES ('STACKER', 'Stacker')
INSERT INTO Export_History_Types VALUES ('USERDETAILS', 'Employee Card Details')
INSERT INTO Export_History_Types VALUES ('USERROLE', 'User Role')
INSERT INTO Export_History_Types VALUES ('VAULTDEVICE', 'Vault Device')
INSERT INTO Export_History_Types VALUES ('TERMINATEUSER', 'Terminate User')
INSERT INTO Export_History_Types VALUES ('UPDATEUSER','Update User')

IF EXISTS(SELECT 1 FROM DBO.Export_History_Types WHERE EH_Type_Ref = 'REMOVEUSER')
BEGIN 
UPDATE Export_History_Types  SET EH_Type_Desc = 'Remove User' WHERE EH_Type_Ref = 'REMOVEUSER'
END 


END
GO