USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetActiveSiteDetailsForDeclaration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetActiveSiteDetailsForDeclaration]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetActiveSiteDetailsForDeclaration
-- -----------------------------------------------------------------
-- 
-- To populate site lists for Declaration.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 15/05/2012 Dinesh Rathinavel Created
-- 19/11/2012 Durga Mayavathar Modified - Declaration User Rights in Enterprise(SiteController rights should not be used in Enterprise).
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetActiveSiteDetailsForDeclaration] @UserID INT
AS 
    BEGIN
        SET NOCOUNT ON    
	
        ;WITH Site_CTE AS(
        SELECT DISTINCT
                Site_ID ,
                Site_Code ,
                S.Site_Name ,
                Site_Code + ' - ' + S.Site_Name AS DisplayName
        FROM    [Site] S
                INNER JOIN VW_Enterprise_usersite_lnk USL ON USL.[SiteID] = S.[Site_ID]
                INNER JOIN [USER] U ON U.[SecurityUserID] = USL.[SecurityUserID]
                INNER JOIN [Staff] ST ON ST.UserTableID = U.[SecurityUserID]
                INNER JOIN SettingsProfileItems SPI ON SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID
                INNER JOIN dbo.SettingsMaster SM ON SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
        WHERE   S.[Site_Closed] = 0
                AND ST.[Staff_ID] = @UserId
                AND SM.SettingsMaster_Name = 'CentralizedDeclaration'
                AND spi.SettingsProfileItems_SettingsMaster_Values = 'True')
                
       SELECT 
			S.*
       FROM Site_CTE S
       WHERE 
			S.Site_ID IN(SELECT DISTINCT Site_ID FROM VW_CollectionData VCD WITH(NOLOCK) WHERE VCD.Declaration = 0)
	   ORDER BY DisplayName
	       
    END

GO

