/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
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

IF NOT EXISTS(SELECT 1 FROM [Call_Group]WHERE Call_Group_Reference = 'Other')
    INSERT [Call_Group] ( Call_Group_Description, Call_Group_Reference, Call_Group_Downtime, Call_Group_End_Date, Call_Group_Log_Engineer_Change )
    SELECT 'Other', 'Other', 0, NULL, 0
ELSE
    UPDATE [Call_Group]
    SET    Call_Group_Description = 'Other', Call_Group_Downtime = 0, Call_Group_End_Date = NULL, Call_Group_Log_Engineer_Change = 0
    WHERE  Call_Group_Reference = 'Other'
GO
