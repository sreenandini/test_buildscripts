USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetZoneOpeningHours]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetZoneOpeningHours]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetZoneOpeningHours] (@ZoneID int)
AS

BEGIN

SELECT  
		Zone_Name,
		Zone_Open_Monday,
		Zone_Open_Tuesday,
		Zone_Open_Wednesday,
		Zone_Open_Thursday,
		Zone_Open_Friday,
		Zone_Open_Saturday,
		Zone_Open_Sunday
FROM Zone
WHERE Zone_ID = @ZoneID

END

GO

