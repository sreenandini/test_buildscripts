/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO
IF EXISTS (SELECT 1 FROM SYS.OBJECTS WHERE name LIKE 'Import_History_Types' AND TYPE ='U')
BEGIN
TRUNCATE TABLE dbo.Import_History_Types
INSERT INTO Import_History_Types VALUES ('BATCHEXPCOMPLETE', 'Batch Export Complete')
INSERT INTO Import_History_Types VALUES ('CHANGEPASSWORD', 'Password Change')
INSERT INTO Import_History_Types VALUES ('COLLECTION', 'Drop')
INSERT INTO Import_History_Types VALUES ('COLLECTIONDETAILS', 'Drop Details')
INSERT INTO Import_History_Types VALUES ('DAILY', 'Daily Read')
INSERT INTO Import_History_Types VALUES ('EVENT', 'Event Details')
INSERT INTO Import_History_Types VALUES ('FACTORYRESET', 'Factory Reset Event')
INSERT INTO Import_History_Types VALUES ('FUND', 'Fund Transfer')
INSERT INTO Import_History_Types VALUES ('GAMEPAYTABLEDETAILS', 'Game Pay Table Details')
INSERT INTO Import_History_Types VALUES ('GAMESESSION', 'Game Session')
INSERT INTO Import_History_Types VALUES ('GLORYAUDIT', 'Glory Audit Data')
INSERT INTO Import_History_Types VALUES ('HOURLY', 'Hourly Data')
INSERT INTO Import_History_Types VALUES ('LIQUIDATIONDETAILS', 'Liquidation Details')
INSERT INTO Import_History_Types VALUES ('LIQUIDATIONSHAREDETAILS', 'Liquidation Share Details')
INSERT INTO Import_History_Types VALUES ('LOGSITEEVENT', 'Machine Events')
INSERT INTO Import_History_Types VALUES ('MACHINECLASS', 'New Machine Class')
INSERT INTO Import_History_Types VALUES ('MACHINEMAINTENANCE', 'Machine Maintenance')
INSERT INTO Import_History_Types VALUES ('MAINTENANCEHISTORY', 'Maintenance History')
INSERT INTO Import_History_Types VALUES ('MAINTENANCEREASONCATEGORY', 'Maintenance Reason Category')
INSERT INTO Import_History_Types VALUES ('MAINTENANCESESSION', 'Maintenance Session')
INSERT INTO Import_History_Types VALUES ('METER_HISTORY', 'Meter Details')
INSERT INTO Import_History_Types VALUES ('PAYTABLE', 'Pay Table')
INSERT INTO Import_History_Types VALUES ('REINSTATE', 'Reinstate')
INSERT INTO Import_History_Types VALUES ('STACKERLEVEL', 'Stacker Level')
INSERT INTO Import_History_Types VALUES ('VAULTBALANCE', 'Vault Balance')
END
GO