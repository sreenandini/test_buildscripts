USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotServiceArea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotServiceArea]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepotServiceArea(@Depot_ID AS INT)
AS
BEGIN
	SELECT *
	FROM   Service_Areas WITH(NOLOCK)
	WHERE  Depot_ID = @Depot_ID
	order by Service_Area_Name
END

GO

