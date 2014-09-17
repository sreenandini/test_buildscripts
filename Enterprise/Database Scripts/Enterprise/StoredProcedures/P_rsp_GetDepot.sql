USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDepot]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetDepot(@Operator_ID AS INT)
AS
BEGIN
	SELECT Depot_ID,
	       Depot_Name
	FROM   DEPOT WITH(NOLOCK)
	WHERE  Supplier_ID = @Operator_ID
	ORDER BY  Depot_Name
END

GO

