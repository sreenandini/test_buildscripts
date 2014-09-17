USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetDepotList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetDepotList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: SP for Getting Depots List
--
-- Inputs:      Supplier ID 
--
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Madhu A    13/05/08     Created
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[usp_GetDepotList]
(
	@SupplierID int 
)
AS
--------------------------------------------------
--- If supplier ID is passed get related records 
--- else get all recrods
--------------------------------------------------
IF  @SupplierID = 0
BEGIN
	SELECT	Depot_ID, 
			Depot_Name 
	FROM Depot 
	

END
ELSE
BEGIN
	SELECT	Depot_ID, 
			Depot_Name 
	FROM Depot 
	WHERE Supplier_ID = @SupplierID 
	order by Depot_Name
END


GO

