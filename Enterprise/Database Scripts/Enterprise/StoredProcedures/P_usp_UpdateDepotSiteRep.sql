USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateDepotSiteRep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateDepotSiteRep]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_UpdateDepotSiteRep(@DepotID INT, @StaffIDs VARCHAR(2000))
AS
BEGIN
	DELETE 
	FROM   dbo.Staff_Depot
	WHERE  Depot_ID = @DepotID 
	
	INSERT INTO dbo.Staff_Depot
	SELECT IntItem,
	       @DepotID
	FROM   dbo.Fn_GetIntTableFromStringList(@StaffIDs)
END

GO

