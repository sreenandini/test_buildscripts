USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepotDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepotDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE rsp_GetDepotDetails(@Depot_ID AS INT = NULL, @Operator_ID AS INT = NULL)
AS
BEGIN
	SELECT *
	FROM   Depot
	WHERE  Depot_ID = COALESCE(@Depot_ID, Depot_ID)
	       AND (@Operator_ID IS NULL OR Supplier_ID = @Operator_ID)
	       ORDER BY Depot_Name
END

GO

