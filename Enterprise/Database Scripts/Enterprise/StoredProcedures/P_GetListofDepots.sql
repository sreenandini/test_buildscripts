USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetListofDepots]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetListofDepots]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

	Create procedure [dbo].[GetListofDepots]
	as
	SELECT Depot_ID as ItemID, Depot_Name as Item FROM DEPOT

GO

