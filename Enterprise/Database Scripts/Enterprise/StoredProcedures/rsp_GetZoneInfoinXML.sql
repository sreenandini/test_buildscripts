USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetZoneInfoinXML]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetZoneInfoinXML] 
GO

CREATE PROCEDURE [dbo].[rsp_GetZoneInfoinXML](@ZoneID INT)
AS
BEGIN
	SELECT EH_Reference1 AS [ZoneName]
	FROM   Export_History
	WHERE  EH_ID = @ZoneID
	       FOR
	       XML PATH('Zone')
END
GO
