USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSettingProfileItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSettingProfileItem]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_UpdateSettingProfile
-- -----------------------------------------------------------------
-- 
-- To update setting profile values
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 14/07/2012 Dinesh Rathinavel Created
-- 
-- =================================================================


CREATE PROCEDURE [dbo].[usp_UpdateSettingProfileItem]
	@SettingsMaster_Name VARCHAR(100),
	@SettingsProfileItems_SettingsMaster_Value VARCHAR(500) OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON

	UPDATE [dbo].[SettingsProfileItems]
		SET [SettingsProfileItems].[SettingsProfileItems_SettingsMaster_Values] = @SettingsProfileItems_SettingsMaster_Value
	FROM [dbo].[SettingsProfileItems]
		INNER JOIN [dbo].[SettingsMaster] SM ON SM.[SettingsMaster_ID] = [SettingsProfileItems].[SettingsProfileItems_SettingsMaster_ID]
		INNER JOIN [dbo].[SettingsProfile] SP ON SP.[SettingsProfile_ID] = [SettingsProfileItems].[SettingsProfileItems_SettingsProfile_ID]
		INNER JOIN [dbo].[Site] S ON S.[Site_Setting_Profile_ID] = SP.[SettingsProfile_ID]
	WHERE UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name]))) = UPPER(LTRIM(RTRIM(@SettingsMaster_Name)))

END

GO

