USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_RemoveUserToSiteMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_RemoveUserToSiteMapping]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[ASPNET_SP_RemoveUserToSiteMapping] (@UserID INT, @SiteID INT)    
AS    
BEGIN    
    
	IF EXISTS (SELECT * FROM UserSite_lnk WHERE  SecurityUserID = @UserID AND SiteID = @SiteID)     
	BEGIN    
	 DELETE FROM UserSite_lnk WHERE  SecurityUserID = @UserID AND SiteID = @SiteID  
	 SELECT 'SUCCESS' AS RESULT     
	END    
END  

GO

