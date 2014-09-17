USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertManufacturerName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertManufacturerName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[esp_InsertManufacturerName]
      @Manufacturer_Name varchar(50) = ''
AS
/*****************************************************************************************************
DESCRIPTION : Inserts data into Manufacturer table  
CREATED DATE: 01 Jun 2012
MODULE            : Manufacturer      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      INSERT INTO Manufacturer (Manufacturer_Name) VALUES(@Manufacturer_Name) 
      INSERT INTO MeterAnalysis.dbo.Manufacturer (Manufacturer_Name) VALUES(@Manufacturer_Name) 
      DECLARE @Manufacturer_ID INT
      SET @Manufacturer_ID = SCOPE_IDENTITY()
      
      Select SCOPE_IDENTITY() AS Manufacturer_ID
      
      EXEC [dbo].usp_EBS_UpdateManufacturerDetails @Manufacturer_ID 
END
--esp_InsertManufacturerName 'dfg'

GO

