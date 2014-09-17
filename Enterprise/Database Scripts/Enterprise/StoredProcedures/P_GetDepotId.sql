USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDepotId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDepotId]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[GetDepotId](@supplierID as int)
	as
	if (@supplierID = 0)
		Begin
		  Select Depot_ID From Depot
		End
	else
  		Begin
			Select Depot_ID From Depot WHERE Supplier_ID =@supplierID
  		End

GO

