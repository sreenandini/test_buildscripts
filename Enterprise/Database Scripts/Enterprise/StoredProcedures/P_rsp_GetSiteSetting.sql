USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetSettingFromSettingsMaster
-- -----------------------------------------------------------------
-- 
-- To get site setting based on the sitecode and site profile
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 15/05/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetSiteSetting]
	@Site_Id INT,
	@SettingMaster_Name VARCHAR(100),
	@Setting_Value VARCHAR(500) OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON

	SELECT
		@Setting_Value = SPI.[SettingsProfileItems_SettingsMaster_Values]
		--SPI.[SettingsProfileItems_SettingsMaster_Values]
	FROM [dbo].[SettingsProfileItems] SPI
		INNER JOIN [dbo].[SettingsMaster] SM ON SM.[SettingsMaster_ID] = SPI.[SettingsProfileItems_SettingsMaster_ID]
		INNER JOIN [dbo].[SettingsProfile] SP ON SP.[SettingsProfile_ID] = SPI.[SettingsProfileItems_SettingsProfile_ID]
		INNER JOIN [dbo].[Site] S ON S.[Site_Setting_Profile_ID] = SP.[SettingsProfile_ID]
		WHERE UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name]))) = UPPER(LTRIM(RTRIM(@SettingMaster_Name)))
				AND S.[Site_Id] = @Site_Id

END

GO

