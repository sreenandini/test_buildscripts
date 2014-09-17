USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMachines]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMachines]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--Machines
Create Proc [dbo].[usp_GetMachines](@Site_Id int)
--With ENCRYPTION
As
Begin
Select DISTINCT
Machine_Stock_No As	Stock_No,
CONVERT(DATETIME, Machine_Start_Date, 101) As	Machine_Start_Date,
CONVERT(DATETIME, Machine_End_Date, 101) As Machine_End_Date,
Machine_Manufacturers_Serial_No As Machine_Manufacturers_Serial_No,
Machine.Machine_Class_ID As Machine_Class_No,
Machine_Class.Machine_Name As [Name],
Machine_Type.Machine_Type_code As Machine_Type_Code,
Machine_Category_ID as Machine_Category_No,
IsMultiGame as IsMultiGame,
Manufacturer.Manufacturer_Name as Manufacturer_Name,
IsAFTEnabled
from Machine
Join Machine_Class on Machine.Machine_Class_Id = Machine_Class.Machine_Class_Id
Join Manufacturer on Manufacturer.Manufacturer_ID = Machine_Class.Manufacturer_ID
Join Machine_Type  on Machine_Class.Machine_Type_Id=Machine_Type.Machine_Type_Id
join Installation on Installation.machine_id=machine.machine_id
join Bar_Position on Bar_Position.Bar_Position_Id = installation.Bar_Position_Id
Join Site on Bar_Position.Site_Id = Site.Site_Id
Where
Site.Site_Code=@Site_Id and coalesce(Machine_End_Date,'')='' 
	For XML Path('Machine'),ROOT('Machines'),TYPE, ELEMENTS XSINIL

End



GO

