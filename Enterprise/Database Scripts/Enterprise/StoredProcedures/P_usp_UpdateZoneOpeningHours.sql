USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateZoneOpeningHours]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateZoneOpeningHours]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_UpdateZoneOpeningHours] 
(
	@SiteID int,
	@ZoneOpenMonday varchar(96),
	@ZoneOpenTuesday varchar(96),
	@ZoneOpenWednesday varchar(96),
	@ZoneOpenThursday varchar(96),
	@ZoneOpenFriday varchar(96),
	@ZoneOpenSaturday varchar(96),
	@ZoneOpenSunday varchar(96)	
)

AS

BEGIN

	IF EXISTS (SELECT 1 FROM ZONE WHERE Site_ID = @SiteID)
	BEGIN
	UPDATE ZONE
	SET
		Zone_Open_Monday = @ZoneOpenMonday,
		Zone_Open_Tuesday = @ZoneOpenTuesday,
		Zone_Open_Wednesday = @ZoneOpenWednesday,
		Zone_Open_Thursday = @ZoneOpenThursday,
		Zone_Open_Friday = @ZoneOpenFriday,
		Zone_Open_Saturday = @ZoneOpenSaturday,
		Zone_Open_Sunday = @ZoneOpenSunday
	WHERE
		Site_ID = @SiteID
	END
END

GO

