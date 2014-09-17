USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(
                  N'[dbo].[rsp_GetActiveSiteDetailsForCollectionLiquidation]'
              )
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetActiveSiteDetailsForCollectionLiquidation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetActiveSiteDetailsForCollectionLiquidation]
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
	           AND SM.SettingsMaster_Name = 'CentralizedDeclaration'
	           AND SPI.SettingsProfileItems_SettingsMaster_Values = 'True'
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
	                                          UPPER('LIQUIDATIONTYPE')
	                                      AND UPPER(
	                                              LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values]))
	                                          ) = LTRIM(RTRIM('COLLECTION')))
	)
	
	SELECT DISTINCT SC.Site_ID,
	       SC.Site_Code,
	       SC.Site_Name,
	       SC.DisplayName
	FROM   SITE_CTE SC
	       INNER JOIN Batch B
	            ON  SC.Site_Code = SUBSTRING(B.Batch_ref, 1, 4)
	       INNER JOIN COLLECTION C
	            ON  C.Batch_ID = B.Batch_ID
	       LEFT OUTER JOIN LiquidationDetails LD
	            ON  B.Batch_ID = LD.CollectionBatchId
	WHERE  LD.CollectionBatchId IS NULL
			AND B.Batch_ID > ISNULL((SELECT TOP 1 COALESCE(LD.CollectionBatchId, 0) AS CollectionBatchId
								FROM   LiquidationDetails LD
								WHERE  
										LD.SiteId = SC.Site_ID
										AND LD.CollectionBatchId IS NOT NULL
			                  ORDER BY
									   LD.LiquidationId DESC), 0)
	       AND B.Batch_ID > ISNULL((SELECT TOP 1 COALESCE(Batch_ID, 0) AS Batch_ID
								FROM   Batch B
								WHERE  B.Liquidation_StartBatch = 1
									AND SUBSTRING(B.Batch_ref, 1, CHARINDEX(',', B.Batch_ref) -1) = SC.Site_Code
								ORDER BY Batch_ID DESC), 0)
			AND ISNULL(B.Batch_Declared, 0) = 1
END
GO

