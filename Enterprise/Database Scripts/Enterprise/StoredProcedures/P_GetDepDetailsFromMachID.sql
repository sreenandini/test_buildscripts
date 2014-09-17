USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDepDetailsFromMachID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDepDetailsFromMachID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[GetDepDetailsFromMachID](@machineID as int)
as
	SELECT Machine_End_Date, Machine_Original_Purchase_Price, 
		Machine_Depreciation_Start_Date, Machine.Depreciation_Policy_ID, 
	Machine.Machine_Class_ID, Depreciation_Policy_Details_ID, 
	Depreciation_Policy_Residual_Value, Depreciation_Policy_Details_Duration, 
	Depreciation_Policy_Details_Percentage FROM
	((Machine 
    LEFT JOIN Depreciation_Policy ON Machine.Depreciation_Policy_ID 
	= Depreciation_Policy.Depreciation_Policy_ID) 
    LEFT JOIN 
	Depreciation_Policy_Details ON Depreciation_Policy.Depreciation_Policy_ID 
	= Depreciation_Policy_Details.Depreciation_Policy_ID)
	WHERE
  Machine.Machine_ID = @machineID 
ORDER BY Depreciation_Policy_Details_Period
GO

