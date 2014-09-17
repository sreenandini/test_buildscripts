USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateManufacturerDetails]    Script Date: 03/09/2014 00:53:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EBS_UpdateManufacturerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EBS_UpdateManufacturerDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[usp_EBS_UpdateManufacturerDetails]    Script Date: 03/09/2014 00:53:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
[usp_EBS_UpdateManufacturerDetails] 1
Select * from EBS_Export_History
usp_EBS_InsertExportHistory
*/
CREATE PROCEDURE [dbo].[usp_EBS_UpdateManufacturerDetails](@ManufacturerId INT)  
AS  
BEGIN  
SET NOCOUNT ON
 DECLARE @Manufacturer  TABLE (  
             ManufacturerID INT,  
             ManufacturerName VARCHAR(50),
             ManufacturerValue VARCHAR(50),  
             IsActive BIT 
         )  
   
 DECLARE @Value    XML   
 
 INSERT INTO @Manufacturer  
 EXEC [dbo].[rsp_EBS_GetManufacturerDetails] @Manufacturer_ID = @ManufacturerId  
   
 SELECT @Value = (  
            SELECT 
            ManufacturerID,
            ManufacturerName,
            ManufacturerValue,
            IsActive
            FROM  @Manufacturer M  
                   FOR XML PATH('Manufacturer'),  
                   TYPE,  
                   ELEMENTS,  
                   ROOT('Manufacturers')  
        )  

	 EXEC [dbo].[usp_EBS_InsertExportHistory] @EH_Type = 'Manufacturer', @EH_Value = @Value,  @EH_SiteCode = 0

SET NOCOUNT OFF
END  

GO


