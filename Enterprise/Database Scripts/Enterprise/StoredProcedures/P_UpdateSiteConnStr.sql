USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateSiteConnStr]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateSiteConnStr]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UpdateSiteConnStr]    
(@ConnectionString AS VARCHAR(200),@SiteID AS INT)    
AS    
BEGIN    
Update Site set ConnectionString = @ConnectionString    
WHERE Site_ID = @SiteID    
END    
GO

