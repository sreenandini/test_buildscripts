USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchSitesforSelectedProfile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchSitesforSelectedProfile]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
 *	this stored procedure is to fetch the sites which have been set the profile selected
 *
 *	Change History:
 *	
 *	Sudarsan S		20-05-2008		created
*/
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[rsp_FetchSitesforSelectedProfile]
	@Profile_Name	VARCHAR(100),
	@Type VARCHAR(10)
AS

BEGIN

	SET @Profile_Name = LTRIM(RTRIM(@Profile_Name))

	IF @Type = 'SetProfile'
	BEGIN
		SELECT S.Site_ID FROM dbo.Site S 
		INNER JOIN dbo.SettingsProfile SP ON S.Site_Setting_Profile_ID = SP.SettingsProfile_ID
		WHERE SP.SettingsProfile_Description = @Profile_Name
	END
	ELSE
	BEGIN
		SELECT S.Site_ID FROM dbo.Site S 
		INNER JOIN dbo.ScheduleProfile SP ON S.Site_Jobs_Profile_ID = SP.ID
		WHERE SP.Name = @Profile_Name
	END

END


GO

