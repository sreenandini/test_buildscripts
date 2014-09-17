USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetSiteDetails]        
as        
begin        
 Select Site_ID,Site_Name,ConnectionString from Site      
WHERE Site_End_Date is NUll      
end  
  
GO

