USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RemoveSiteClassifonSiteStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RemoveSiteClassifonSiteStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_RemoveSiteClassifonSiteStatus] 
(
	@SiteClassifID int,
	@SiteClassifStatusOUT int = 0 OUTPUT
)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM Site WHERE Site_Classification_ID = @SiteClassifID )
	BEGIN 
		DELETE from Site_Classification WHERE Site_Classification_ID = @SiteClassifID 
		SET @SiteClassifStatusOUT = 1
	END
END

--exec usp_RemoveSiteClassifonSiteStatus 22


GO

