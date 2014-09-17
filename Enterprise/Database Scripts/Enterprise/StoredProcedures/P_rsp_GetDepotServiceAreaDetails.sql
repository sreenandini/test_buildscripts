USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotServiceAreaDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotServiceAreaDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepotServiceAreaDetails(@Depotid AS INT, @Service_Area_ID AS INT)
AS
BEGIN
	SELECT *
	FROM   Service_Areas WITH(NOLOCK)
	WHERE  Depot_ID = @Depotid
	       AND Service_Area_ID = @Service_Area_ID
END

GO

