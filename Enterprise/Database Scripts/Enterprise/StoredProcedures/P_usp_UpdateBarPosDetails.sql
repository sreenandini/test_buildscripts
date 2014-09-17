USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateBarPosDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateBarPosDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateBarPosDetails
@BarPositionName VARCHAR(50),
@ZoneID INT,
@Supplier_AMEDIS_Code VARCHAR(4),
@DepotID INT,
@CollectionDay VARCHAR(30),
@Machine_Type_AMEDIS_Code INT
AS
BEGIN
	
UPDATE Bar_Position
SET Zone_ID = @ZoneID,
Bar_Position_Supplier_AMEDIS_Code = @Supplier_AMEDIS_Code,
Depot_ID = @DepotID,
Bar_Position_Collection_Day = @CollectionDay,
Bar_Position_Machine_Type_AMEDIS_Code = @Machine_Type_AMEDIS_Code
WHERE Bar_Position_Name=@BarPositionName
END


GO

