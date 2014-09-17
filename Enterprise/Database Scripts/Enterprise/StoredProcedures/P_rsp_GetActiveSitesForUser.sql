USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetActiveSitesForUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetActiveSitesForUser]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetActiveSitesForUser 1
-- -----------------------------------------------------------------
-- 
-- To populate acive site for user.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 03/06/2013 Dinesh Rathinavel Created
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetActiveSitesForUser] 
	@UserID INT
AS 
BEGIN
	SET NOCOUNT ON
	
	SELECT DISTINCT
                Site_ID ,
                Site_Code ,
                Site_Name ,
                Site_Code + ' - ' + Site_Name AS DisplayName
        FROM    [Site] S
                INNER JOIN [UserSite_lnk] USL ON USL.[SiteID] = S.[Site_ID]
                INNER JOIN [USER] U ON U.[SecurityUserID] = USL.[SecurityUserID]
        WHERE   S.[Site_Closed] = 0
				AND SiteStatus = 'FULLYCONFIGURED'
                AND U.[SecurityUserID] = @UserId
                
	ORDER by site_name asc
END
GO