USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMachineNamesForServiceCall]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMachineNamesForServiceCall]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetMachineNamesForServiceCall
(
@IsShowHistory BIT,
@SiteID INT,
@MachineTypeID INT = 0
)
AS
BEGIN

DECLARE @WhereClause NVARCHAR(MAX)
DECLARE @OrderByClause NVARCHAR(100)
DECLARE @TopClause NVARCHAR(10)
DECLARE @MainSQL NVARCHAR(MAX)

	IF (@IsShowHistory = 1) 
		BEGIN
			SELECT  					 
				Bar_Position.Bar_Position_Name, 
				Machine_Class.Machine_Name, 
				Site.Site_Name,
				Site.Site_Code,
				Site.Site_Address_1,
				Site.Site_Address_2, 
				Site.Site_Address_3,
				 Site.Site_Address_4, 
				 Site.Site_Address_5, 
				 Site.Site_Postcode, 
				 Site.Site_Manager, 
				 Site.Site_Phone_No, 
				 Installation.Installation_ID, 
				 Machine_Class.Machine_Name, 
				 Machine.Machine_Stock_No, 
				 Machine.Machine_Manufacturers_Serial_No,
				Bar_Position.Bar_Position_Name, 
				Depot.Depot_Name, 
				Service_Areas.Service_Area_Name,
				 Sub_Company.Sub_Company_Name,
				  Installation.Installation_Start_Date
				 FROM 
				(((((((	 Site
					LEFT JOIN Bar_Position 
						ON Site.Site_ID = Bar_Position.Site_ID) 
						LEFT JOIN Installation
						 ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID) 
						 LEFT JOIN Machine 
						 ON Installation.Machine_ID = Machine.Machine_ID) 
						 LEFT JOIN Machine_Class 
						 ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID) 
						 LEFT JOIN Machine_Type 
						 ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID) 
						 LEFT JOIN Depot 
						 ON Site.Depot_ID = Depot.Depot_ID) 
						 LEFT JOIN Service_Areas 
						 ON Site.Service_Area_ID = Service_Areas.Service_Area_ID) 
						 LEFT JOIN Sub_Company 
						 ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID 
						WHERE Site.Site_ID  = @SiteId  AND Installation.Installation_ID > 0 
						AND ((@MachineTypeID = 0) OR (@MachineTypeID<>0 AND Machine_Class.Machine_Type_ID =@MachineTypeID))
						AND (Installation.Installation_End_Date IS NULL)
						ORDER BY Machine_Class.Machine_Name ASC
		END
	ELSE
		BEGIN
				SELECT  		 
				Bar_Position.Bar_Position_Name, 
				Machine_Class.Machine_Name, 
				Site.Site_Name,
				Site.Site_Code,
				Site.Site_Address_1,
				Site.Site_Address_2, 
				Site.Site_Address_3,
				 Site.Site_Address_4, 
				 Site.Site_Address_5, 
				 Site.Site_Postcode, 
				 Site.Site_Manager, 
				 Site.Site_Phone_No, 
				 Installation.Installation_ID, 
				 Machine_Class.Machine_Name, 
				 Machine.Machine_Stock_No, 
				 Machine.Machine_Manufacturers_Serial_No,
				Bar_Position.Bar_Position_Name, 
				Depot.Depot_Name, 
				Service_Areas.Service_Area_Name,
				 Sub_Company.Sub_Company_Name,
				  Installation.Installation_Start_Date
				 FROM 
				(((((((	 Site
					LEFT JOIN Bar_Position 
						ON Site.Site_ID = Bar_Position.Site_ID) 
						LEFT JOIN Installation
						 ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID) 
						 LEFT JOIN Machine 
						 ON Installation.Machine_ID = Machine.Machine_ID) 
						 LEFT JOIN Machine_Class 
						 ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID) 
						 LEFT JOIN Machine_Type 
						 ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID) 
						 LEFT JOIN Depot 
						 ON Site.Depot_ID = Depot.Depot_ID) 
						 LEFT JOIN Service_Areas 
						 ON Site.Service_Area_ID = Service_Areas.Service_Area_ID) 
						 LEFT JOIN Sub_Company 
						 ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID 
						WHERE Site.Site_ID  = @SiteId  AND Installation.Installation_ID > 0 
						AND ((@MachineTypeID = 0) OR (@MachineTypeID<>0 AND Machine_Class.Machine_Type_ID =@MachineTypeID))
						AND (Installation.Installation_End_Date IS NULL)
						ORDER BY  Machine_Class.Machine_Name ASC,Cast(Installation_Start_Date AS DATETIME) DESC 
		END	
END

