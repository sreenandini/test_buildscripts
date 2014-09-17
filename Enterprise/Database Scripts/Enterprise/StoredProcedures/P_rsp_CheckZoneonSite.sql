USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckZoneonSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckZoneonSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_CheckZoneonSite] 
(
	@SiteID int,
	@ZoneStatusOUT int = 0 Output
)
AS

BEGIN

	IF EXISTS (SELECT 1 FROM ZONE WHERE Site_ID = @SiteID) 	
	BEGIN
		SET @ZoneStatusOUT = 1
	END
END


GO

