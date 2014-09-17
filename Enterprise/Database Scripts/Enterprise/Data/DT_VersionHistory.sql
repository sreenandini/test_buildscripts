/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/12 5:43:30 PM
 ************************************************************/
USE [Enterprise]
GO

IF NOT EXISTS(SELECT 1 FROM [VersionHistory])
    INSERT [VersionHistory] ( VersionName, VersionDate )
    SELECT '12.5.0', GETDATE()
ELSE
    UPDATE [VersionHistory]
    SET    VersionName = '12.5.0', VersionDate = GETDATE()
GO