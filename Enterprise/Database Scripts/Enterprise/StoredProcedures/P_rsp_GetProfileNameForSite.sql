USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetProfileNameForSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetProfileNameForSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetProfileNameForSite
@Site_ID Varchar(10)
AS
SELECT 
[ProfileName] = SP.SettingsProfile_Description
FROM [Site] S
INNER JOIN SettingsProfile SP
ON SP.SettingsProfile_ID = S.Site_Setting_Profile_ID
WHERE S.Site_Code =  @Site_ID

GO

