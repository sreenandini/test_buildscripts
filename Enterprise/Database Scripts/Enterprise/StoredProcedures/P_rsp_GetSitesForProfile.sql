USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSitesForProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSitesForProfile]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetSitesForProfile
@SettingsProfile_Description VARCHAR(200)
AS
SELECT 
Site_ID, Site_Code, Site_Name
FROM [Site] S
INNER JOIN SettingsProfile SP
ON  S.Site_Setting_Profile_ID = (SELECT SettingsProfile_ID FROM SettingsProfile WHERE SettingsProfile_Description = @SettingsProfile_Description)
where SP.SettingsProfile_Description = @SettingsProfile_Description

GO

