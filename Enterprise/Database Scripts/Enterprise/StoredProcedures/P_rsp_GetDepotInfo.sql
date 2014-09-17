USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetDepotInfo] (@SupplierID INT = 0)
AS

BEGIN

SELECT Depot_Name, 
	   Depot_ID 
FROM Depot 
WHERE Supplier_ID = @SupplierID 
ORDER BY Depot_Name 
ASC

END

GO

