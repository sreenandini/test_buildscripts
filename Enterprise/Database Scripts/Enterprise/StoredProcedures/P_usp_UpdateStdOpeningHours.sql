USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateStdOpeningHours]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateStdOpeningHours]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_UpdateStdOpeningHours] 
(
	@StdOpenHRID int,
	@StdOpenMonday varchar(96),
	@StdOpenTuesday varchar(96),
	@StdOpenWednesday varchar(96),
	@StdOpenThursday varchar(96),
	@StdOpenFriday varchar(96),
	@StdOpenSaturday varchar(96),
	@StdOpenSunday varchar(96),
	@StdOpeningHoursTotal int,
	@StdStatusOUT int = 0 Output
)

AS

BEGIN

	IF EXISTS (SELECT 1 FROM Standard_Opening_Hours WHERE Standard_Opening_Hours_ID = @StdOpenHRID) 	
	BEGIN
	
	SET @StdStatusOUT = 1	
	
	UPDATE Standard_Opening_Hours
	SET
		Standard_Opening_Hours_Open_Monday = @StdOpenMonday,
		Standard_Opening_Hours_Open_Tuesday = @StdOpenTuesday,
		Standard_Opening_Hours_Open_Wednesday = @StdOpenWednesday,
		Standard_Opening_Hours_Open_Thursday = @StdOpenThursday,
		Standard_Opening_Hours_Open_Friday = @StdOpenFriday,
		Standard_Opening_Hours_Open_Saturday = @StdOpenSaturday,
		Standard_Opening_Hours_Open_Sunday = @StdOpenSunday,
		Standard_Opening_Hours_Total = @StdOpeningHoursTotal
	WHERE
		Standard_Opening_Hours_ID = @StdOpenHRID
	END
END

GO

