/************************************************************
 * Created by Bally Technologies © 2011
 * Time: 26/12/2012 6:45:03 PM
 ************************************************************/

USE [Enterprise]
GO
SET IDENTITY_INSERT SettingsProfile ON
GO

IF NOT EXISTS(SELECT 1 FROM [SettingsProfile]WHERE SettingsProfile_ID = '1')
    INSERT [SettingsProfile] ( SettingsProfile_ID, SettingsProfile_Description )
    SELECT 1, 'Default Profile'
ELSE
    UPDATE [SettingsProfile]
    SET    SettingsProfile_Description = 'Default Profile'
    WHERE  SettingsProfile_ID = '1'

GO
SET IDENTITY_INSERT SettingsProfile OFF
GO