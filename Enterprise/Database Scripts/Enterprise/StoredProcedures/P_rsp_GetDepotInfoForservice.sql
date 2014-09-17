USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotInfoForservice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotInfoForservice]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[rsp_GetDepotInfoForservice] (@SupplierID INT = 0)
AS

BEGIN

SELECT Depot_Name, 
	   Depot_ID 
FROM Depot 
WHERE Supplier_ID = @SupplierID 
AND Depot_Service=1  
ORDER BY Depot_Name 
ASC

END

GO
