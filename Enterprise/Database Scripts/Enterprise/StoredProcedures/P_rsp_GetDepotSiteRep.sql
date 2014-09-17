USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotSiteRep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotSiteRep]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepotSiteRep(@iDepotid AS INT)
AS
BEGIN
	SELECT s.Staff_ID,
	       s.Staff_Last_Name + ' ' + s.Staff_First_Name AS [Name],
	       sd.Depot_ID
	FROM   Staff s WITH(NOLOCK)
	       LEFT OUTER JOIN Staff_Depot sd
	            ON  s.Staff_ID = sd.Staff_ID
	            AND sd.Depot_ID = @iDepotid
	WHERE  s.Staff_IsARepresentative = 1
	order by Staff_Last_Name
	
	
END

GO

