USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetServiceAreaonDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetServiceAreaonDepot]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetServiceAreaonDepot] 
(
	@DepotID INT
)
AS

BEGIN

SELECT 
	Service_Area_Name, 
	Service_Area_ID 
FROM 
	Service_Areas 
WHERE 
	Depot_ID = @DepotID 
ORDER BY 
	Service_Area_Name 
ASC

END

GO

