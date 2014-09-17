USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStatusDet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetStatusDet]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[GetStatusDet](@machineID as int)
as
	SELECT Machine_Status_Flag, Machine_Status, Machine_Original_Purchase_Price, 
	Machine_Start_Date, Machine_Alternative_Serial_Numbers, Machine_Manufacturers_Serial_No, 
	Machine_Stock_No, Depot.Depot_ID, Depot_Name, Machine_Class.Machine_Class_ID, 
	Machine_Name, Machine_Type.Machine_Type_ID, Machine_Type_Code,Machine_end_Date 
	FROM ((Machine
 INNER JOIN Machine_Class ON Machine.Machine_Class_ID = 
	Machine_Class.Machine_Class_ID)
 INNER JOIN Machine_Type ON Machine_Class.Machine_Type_ID
	 = Machine_Type.Machine_Type_ID) 
 LEFT JOIN Depot ON Machine.Depot_ID = 
	Depot.Depot_ID
 WHERE MAchine_ID = @machineID
	
GO

