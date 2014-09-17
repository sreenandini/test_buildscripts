USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_GetUserSiteMapping]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_GetUserSiteMapping]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ASPNET_SP_GetUserSiteMapping](@USERID INT)    
AS    
BEGIN    
	 SELECT     
	 us.SecurityUserID,  
	 us.SiteID  
	 FROM     
	  [UserSite_lnk] us   
	 WHERE     
	  us.SecurityUserID = @USERID     
END    

GO

