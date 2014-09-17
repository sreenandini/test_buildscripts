USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetActiveSiteDetailsBySetting]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetActiveSiteDetailsBySetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetActiveSiteDetailsBySetting]
	@UserID INT,
	@SettingName VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON
	
	;WITH SITE_CTE AS(
	    SELECT Site_ID,
	           Site_Code,
	           Site_Name,
	           Site_Code + ' - ' + Site_Name AS DisplayName
	    FROM   [Site] S
	           INNER JOIN [UserSite_lnk] USL
	                ON  USL.[SiteID] = S.[Site_ID]
	           INNER JOIN [USER] U
	                ON  U.[SecurityUserID] = USL.[SecurityUserID]
	           INNER JOIN [Staff] ST
	                ON  ST.UserTableID = U.[SecurityUserID]
	           INNER JOIN SettingsProfileItems SPI
	                ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID
	           INNER JOIN dbo.SettingsMaster SM
	                ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
	    WHERE  S.[Site_Closed] = 0
	           AND U.[SecurityUserID] = @UserID
	           AND SM.SettingsMaster_Name = 'CentralizedReadLiquidation'
	           AND spi.SettingsProfileItems_SettingsMaster_Values = 'True'
	           AND S.[Site_ID] IN (SELECT SI.[Site_Id]
	                               FROM   [dbo].[SettingsProfileItems] SPI
	                                      INNER JOIN [dbo].[SettingsMaster] SM
	                                           ON  SM.[SettingsMaster_ID] = SPI.[SettingsProfileItems_SettingsMaster_ID]
	                                      INNER JOIN [dbo].[SettingsProfile] SP
	                                           ON  SP.[SettingsProfile_ID] = SPI.[SettingsProfileItems_SettingsProfile_ID]
	                                      INNER JOIN [dbo].[Site] SI
	                                           ON  SI.[Site_Setting_Profile_ID] = 
	                                               SP.[SettingsProfile_ID]
	                               WHERE  UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name]))) = 
	                                      UPPER(LTRIM(RTRIM(@SettingName)))
	                                      AND UPPER(LTRIM(RTRIM(@SettingName))) = 
	                                          UPPER('LiquidationType')
	                                      AND UPPER(
	                                              LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values]))
	                                          ) = LTRIM(RTRIM('READ')))
	)
	
	SELECT MAX(ISNULL(R.Read_ID, 0)) AS Read_ID,
	       SC.Site_ID,
	       SC.Site_Code,
	       SC.Site_Name,
	       SC.DisplayName
	       INTO #TmpSite
	FROM   SITE_CTE SC
	       INNER JOIN [Site] S WITH (NOLOCK)
	            ON  S.Site_ID = SC.Site_ID
	       INNER JOIN [Bar_Position] BP WITH (NOLOCK)
	            ON  BP.[Site_ID] = S.[Site_ID]
	       INNER JOIN [Installation] I WITH (NOLOCK)
	            ON  I.Bar_Position_ID = BP.Bar_Position_ID
	       INNER JOIN [Read] R
	            ON  R.Installation_ID = I.Installation_ID
	WHERE  R.Read_ID > ISNULL(
	           (
	               SELECT TOP 1 ISNULL(LD.[ReadId], 0)
	               FROM   [LiquidationDetails] LD WITH(NOLOCK)
	               WHERE  LD.[SiteId] = SC.Site_ID
	               ORDER BY
	                      LD.[ReadId] DESC
	           ),
	           0
	       )
	GROUP BY
	       SC.Site_ID,
	       SC.Site_Code,
	       SC.Site_Name,
	       SC.DisplayName
	
	SELECT DISTINCT Site_ID,
	       Site_Code,
	       Site_Name,
	       DisplayName
	FROM   #TmpSite
	WHERE  Read_ID > 0
	
	DROP TABLE #TmpSite
END
GO

