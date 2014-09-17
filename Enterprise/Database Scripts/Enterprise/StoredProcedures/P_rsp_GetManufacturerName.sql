USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetManufacturerName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetManufacturerName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetManufacturerName]  
AS  
BEGIN  
SELECT DISTINCT 
Manufacturer_Name 
FROM Manufacturer WITH(NOLOCK)
ORDER BY  Manufacturer_Name ASC
END 

