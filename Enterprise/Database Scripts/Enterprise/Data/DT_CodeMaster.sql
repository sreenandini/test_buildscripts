/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [CodeMaster]WHERE Code = 'MAINCA')
    INSERT [CodeMaster] ( Code, DESCRIPTION, ParentID )
    SELECT 'MAINCA', 'Maintenance Category', NULL
ELSE
    UPDATE [CodeMaster]
    SET    DESCRIPTION = 'Maintenance Category', ParentID = NULL
    WHERE  Code = 'MAINCA'

IF NOT EXISTS(SELECT 1 FROM [CodeMaster]WHERE Code = 'MAINRE')
    INSERT [CodeMaster] ( Code, DESCRIPTION, ParentID )
    SELECT 'MAINRE', 'Maintenance Reason', (SELECT TOP 1 ID FROM [CodeMaster]WHERE Code = 'MAINCA')
ELSE
    UPDATE [CodeMaster]
    SET    DESCRIPTION = 'Maintenance Reason', ParentID = (SELECT TOP 1 ID FROM [CodeMaster]WHERE Code = 'MAINCA')
    WHERE  Code = 'MAINRE'

GO