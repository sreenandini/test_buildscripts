USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateSiteOpeningHours]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateSiteOpeningHours]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_UpdateSiteOpeningHours] 
(
	@SiteID int,
	@SiteOpenMonday varchar(96),
	@SiteOpenTuesday varchar(96),
	@SiteOpenWednesday varchar(96),
	@SiteOpenThursday varchar(96),
	@SiteOpenFriday varchar(96),
	@SiteOpenSaturday varchar(96),
	@SiteOpenSunday varchar(96),
	@SiteStatusOUT int = 0 Output
)

AS

BEGIN

	IF EXISTS (SELECT 1 FROM SITE WHERE Site_ID = @SiteID) 	
	BEGIN
	
	SET @SiteStatusOUT = 1	
	
	UPDATE Site
	SET
		Site_Open_Monday = @SiteOpenMonday,
		Site_Open_Tuesday = @SiteOpenTuesday,
		Site_Open_Wednesday = @SiteOpenWednesday,
		Site_Open_Thursday = @SiteOpenThursday,
		Site_Open_Friday = @SiteOpenFriday,
		Site_Open_Saturday = @SiteOpenSaturday,
		Site_Open_Sunday = @SiteOpenSunday
	WHERE
		Site_ID = @SiteID
	END
END

GO

