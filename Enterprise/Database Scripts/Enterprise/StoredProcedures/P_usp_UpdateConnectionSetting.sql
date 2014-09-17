USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateConnectionSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateConnectionSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: Updates the Connectionstring for all the sites (for Wireless laptop changes)
--
-- Inputs:     NIL
-- Outputs:    NIL
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	15-07-2008		Created
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[usp_UpdateConnectionSetting]
@data	VARCHAR(MAX),
@IsSuccess	INT OUTPUT
AS

BEGIN

	DECLARE @idoc INT
	DECLARE @value VARCHAR(400)
	DECLARE @Site_Code CHAR(5)

	SET @IsSuccess = 0

	SET @data = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @data

	EXEC sp_xml_Preparedocument @idoc OUTPUT, @data

	SELECT @Site_Code = SiteCode,
			@value = Connection 
	FROM OPENXML(@idoc, './Root', 2) WITH (Connection VARCHAR(400) './Conn', SiteCode VARCHAR(5) './SiteCode')

	EXEC sp_xml_removedocument @idoc

	
	IF EXISTS(SELECT 1 FROM dbo.Site WHERE Site_Code = @Site_Code)
	BEGIN
		UPDATE dbo.Site SET ConnectionString = @value WHERE Site_Code = @Site_Code
	END
	
	IF @@Error <> 0
	BEGIN
		SET @IsSuccess = -1
	END

END

GO

