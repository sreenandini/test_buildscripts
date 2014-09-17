USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportManufacturerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportManufacturerDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_ExportManufacturerDetails
@ManufacturerID INT 
AS                    
BEGIN    
select (SELECT * FROM dbo.Manufacturer WHERE Manufacturer_ID = @ManufacturerID
 FOR XML AUTO, ELEMENTS XSINIL, ROOT('Manufacturer_Details'))   AS [A]   
END
GO

