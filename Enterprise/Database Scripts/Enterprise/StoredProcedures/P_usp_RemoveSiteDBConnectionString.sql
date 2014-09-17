USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RemoveSiteDBConnectionString]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RemoveSiteDBConnectionString]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_RemoveSiteDBConnectionString
(@Site_ID INT)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	IF EXISTS(
	       SELECT 1
	       FROM   dbo.SiteDBConnectionStrings
	       WHERE  Site_ID = @Site_ID
	   )
	BEGIN
	    DELETE FROM dbo.SiteDBConnectionStrings 
	    WHERE  Site_ID = @Site_ID
	END
	
	-- END
	SET NOCOUNT OFF
END

GO

