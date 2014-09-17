USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDepotDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDepotDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

	create procedure [dbo].[GetDepotDetails](@supplierID as int)
	as
	SELECT Depot_ID as ItemID, Depot_Name as Item FROM Depot WHERE 
	Supplier_ID = @supplierID

GO

