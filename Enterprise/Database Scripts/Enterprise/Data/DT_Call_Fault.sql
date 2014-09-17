/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 05/04/13 10:06:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [Call_Group]WHERE Call_Group_Reference = 'door')
    INSERT [Call_Group] ( Call_Group_Description, Call_Group_Reference, Call_Group_Downtime, Call_Group_End_Date, Call_Group_Log_Engineer_Change )
    SELECT 'door event', 'door', 1, NULL, 0
ELSE
    UPDATE [Call_Group]
    SET    Call_Group_Description = 'door event', Call_Group_Downtime = 1, Call_Group_End_Date = NULL, Call_Group_Log_Engineer_Change = 0
    WHERE  Call_Group_Reference = 'door'

IF NOT EXISTS(SELECT 1 FROM [Call_Group]WHERE Call_Group_Reference = 'Power')
    INSERT [Call_Group] ( Call_Group_Description, Call_Group_Reference, Call_Group_Downtime, Call_Group_End_Date, Call_Group_Log_Engineer_Change )
    SELECT 'Power event', 'Power', 1, NULL, 0
ELSE
    UPDATE [Call_Group]
    SET    Call_Group_Description = 'Power event', Call_Group_Downtime = 1, Call_Group_End_Date = NULL, Call_Group_Log_Engineer_Change = 0
    WHERE  Call_Group_Reference = 'Power'

IF NOT EXISTS(SELECT 1 FROM [Call_Group]WHERE Call_Group_Reference = 'Fault')
    INSERT [Call_Group] ( Call_Group_Description, Call_Group_Reference, Call_Group_Downtime, Call_Group_End_Date, Call_Group_Log_Engineer_Change )
    SELECT 'General Fault', 'Fault', 1, NULL, 0
ELSE
    UPDATE [Call_Group]
    SET    Call_Group_Description = 'General Fault', Call_Group_Downtime = 1, Call_Group_End_Date = NULL, Call_Group_Log_Engineer_Change = 0
    WHERE  Call_Group_Reference = 'Fault'

IF NOT EXISTS(SELECT 1 FROM [Call_Group]WHERE Call_Group_Reference = 'Comms')
    INSERT [Call_Group] ( Call_Group_Description, Call_Group_Reference, Call_Group_Downtime, Call_Group_End_Date, Call_Group_Log_Engineer_Change )
    SELECT 'Comms Fault', 'Comms', 1, '18 Jul 2008', 0
ELSE
    UPDATE [Call_Group]
    SET    Call_Group_Description = 'Comms Fault', Call_Group_Downtime = 1, Call_Group_End_Date = '18 Jul 2008', Call_Group_Log_Engineer_Change = 0
    WHERE  Call_Group_Reference = 'Comms'

GO



TRUNCATE TABLE Call_Fault
GO

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Door open event', 'Door', '18 Jul 2008'
   
INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Power'), 'Power down event', 'Down', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Power'), 'Power Up', 'Up', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Door close', 'Close', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Stacker Door Open', 'Stack Open', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Stacker Close', 'Close', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'GMU Data Comms Resumed', 'GMU Up', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Comms Up', 'Comms Up', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'GMU Data Comms Down', 'GMU Down', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Comms Down', 'Comms Down', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'belly door open', 'belly door', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), '[New Fault]', '[New Ref]', '17 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Comms'), 'Cosmetic', 'CDO', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Comms'), 'Out of Order', 'CDO', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Slot Door Open', 'Slot Open', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Front Door Open', 'Front Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Front Door Closed', 'Front Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Cash Door Open', 'Cash Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Cash Door Closed', 'Cash Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Slot Door Open', 'Slot Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Slot Door Closed', 'Slot Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Stacker Door Open', 'Stacker', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Stacker Door closed', 'Stacker', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Drop Door Open', 'Drop Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Drop Door Closed', 'Drop Door ', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Card Cage Open', 'Card Cage ', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Card Cage Closed', 'Card Cage', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Belly Door Open', 'Belly Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Belly Door Closed', 'Belly Door', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Stacker Removed', 'Stacker', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Stacker Replaced', 'Stacker', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Event Log Full', 'Event Log', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Hourly Log Full', 'Hourly Log', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Novo Ticket Printed', 'Ticket', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Meter Change Event', 'Meter', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Zero Credit Fired', 'Credit', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'handpay Event Created', 'Handpay', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Handpay Event Keyed Off', 'Handpay', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Ram Reset', 'Ram Reset', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Card Out', 'Card Out', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Ticket Print Process Started', 'Ticket', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Ticket Print Process Started', 'Ticket', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Ticket Redeem Process Started', 'Ticket', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Ticket Redeem Process Complete', 'Ticket', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'door'), 'Ticket Print Process Complete', 'Ticket', '18 Jul 2008'

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Ticket Redeem Process Started', 'Ticket', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Ticket Redeem Process Complete', 'Ticket', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Ticket Print Process Complete', 'Ticket', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Card In', 'Card In', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Undefined', 'Undefined', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Reason 2', 'CDO', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Reason 3', 'CDO', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Reason 4', 'CDO', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Other', 'CDO', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Comms'), 'site comms failure / 1', 'Site comms down', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Comms'), 'site comms failure/2 - site connection resumed', 'Site comms up', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Machine Auto Disabled', 'Machine Disabled', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Machine Auto Enabled', 'Machine Enabled', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Comms'), 'Machine Data Comms Down', 'Machine Data Comms', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'UnRecognised', 'UnRecognised', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), '** UNKNOWN **', '** UNKNOWN **', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Front Door Open', 'Front Door', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Front Door Closed', 'Front Door', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'GMU Compartment Opened', 'GMU Compartment', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'GMU Compartment Closed', 'GMU Compartment', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Game MPU Removed', 'Game MPU', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Aux fill door Opened', 'Aux fill door', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Aux fill door Closed', 'Aux fill door', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Acceptor Removed', 'Acceptor', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Acceptor door Opened', 'Acceptor', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'Acceptor door Closed', 'Acceptor', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'MPU compartment Opened', 'MPU compartment', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Door'), 'MPU compartment Closed', 'MPU compartment', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Power'), 'Power Off Card Cage Access', 'Power Off', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Power'), 'Power Off Slot Door Access', 'Power Off', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Power'), 'Power Off Cash Box Door Access', 'Power Off', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Power'), 'Power Off Drop Door Access', 'Power Off', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Employee Card In', 'Employee Card', NULL


INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Fault'), 'Employee Card Out', 'Employee Card', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Comms'), 'Comms Failure', 'Comms Failure', NULL

INSERT [Call_Fault] ( Call_Group_ID, Call_Fault_Description, Call_Fault_Reference, Call_Fault_End_Date )
SELECT (SELECT TOP 1 Call_Group_ID FROM Call_Group WHERE Call_Group_Reference = 'Other'), 'Other', 'Other', NULL
GO
   
   
   