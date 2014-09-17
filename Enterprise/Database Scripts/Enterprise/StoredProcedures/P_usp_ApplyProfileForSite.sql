USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ApplyProfileForSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ApplyProfileForSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
 *	this stored procedure is to apply the selected to profile to settings/Jobs
 *
 *	Change History:
 *	
 *	Sudarsan S		16-02-2009		created
*/
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE usp_ApplyProfileForSite --'3,4,5,2', '', 'Sudarsan', 'SetProfile'
@Site_IDs VARCHAR(5000),
@RemSites	VARCHAR(5000),
@SettingsProfile_Description VARCHAR(200),
@Type	VARCHAR(20)
AS

BEGIN

	DECLARE @SiteCode	TABLE(Sno INT IDENTITY(1,1), Site_ID INT)
	DECLARE @sql	VARCHAR(8000)

	IF @Type = 'SetProfile'
	BEGIN
		SET @sql = CASE WHEN @RemSites <> '' THEN 'UPDATE S SET S.Site_Setting_Profile_ID = 1 FROM dbo.Site S 
					INNER JOIN dbo.SettingsProfile SP ON S.Site_Setting_Profile_ID = SP.SettingsProfile_ID
					WHERE SP.SettingsProfile_Description = ''' + LTRIM(RTRIM(@SettingsProfile_Description))
					+ ''' AND S.Site_ID IN (' + @RemSites + ');' ELSE '' END

		SET @sql = @sql + 'UPDATE dbo.Site SET Site_Setting_Profile_ID = 
				(SELECT SettingsProfile_ID FROM dbo.SettingsProfile WHERE SettingsProfile_Description = ''' 
					+ LTRIM(RTRIM(@SettingsProfile_Description)) + ''') WHERE Site_ID IN (' + @Site_IDs + ')'
	END
	ELSE
	BEGIN
		SET @sql = CASE WHEN @RemSites <> '' THEN 'UPDATE S SET S.Site_Jobs_Profile_ID = 1 FROM dbo.Site S 
					INNER JOIN dbo.ScheduleProfile SP ON S.Site_Jobs_Profile_ID = SP.ID
					WHERE SP.[Name] = ''' + LTRIM(RTRIM(@SettingsProfile_Description))
					+ ''' AND S.Site_ID IN (' + @RemSites + ');' ELSE '' END

		SET @sql = @sql + 'UPDATE dbo.Site SET Site_Jobs_Profile_ID = 
				(SELECT [ID] FROM dbo.ScheduleProfile WHERE [Name] = ''' + LTRIM(RTRIM(@SettingsProfile_Description))
					+ ''') WHERE Site_ID IN (' + @Site_IDs + ')'
	END

--	SELECT @sql

	EXEC (@sql)

END


GO

