USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteDBConnectionString]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteDBConnectionString]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_UpdateSiteDBConnectionString
(@Site_ID INT, @Site_DB_ConnectionString VARBINARY(MAX))
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
	    UPDATE dbo.SiteDBConnectionStrings
	    SET    Site_DB_ConnectionString = @Site_DB_ConnectionString
	    WHERE  Site_ID = @Site_ID
	END
	ELSE
	BEGIN
	    INSERT INTO dbo.SiteDBConnectionStrings
	      (
	        Site_ID,
	        Site_DB_ConnectionString
	      )
	    VALUES
	      (
	        @Site_ID,
	        @Site_DB_ConnectionString
	      )
	END
	-- END
	SET NOCOUNT OFF
END

GO

