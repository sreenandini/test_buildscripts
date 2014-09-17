USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateProfileForSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateProfileForSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateProfileForSite
@Site_ID VARCHAR(10),
@SettingsProfile_Description VARCHAR(200)
AS
UPDATE [SITE]
SET Site_Setting_Profile_ID = (SELECT SettingsProfile_ID FROM SettingsProfile WHERE SettingsProfile_Description = @SettingsProfile_Description)
WHERE Site_Code =  @Site_ID

GO

