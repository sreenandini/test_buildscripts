USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAreaDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAreaDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================    
-- OUTPUT --Get Company details -- exec rsp_GetCompanyDetails 2         
-- Revision History    
-- Vineetha Mathew 22/03/2010  Created    
-- ======================================================================= 
  

CREATE PROCEDURE [dbo].rsp_GetAreaDetails    
(@region INT = 0  ,   
@companyID INT=0,   
@subcompany  INT=0   
)    
AS    
BEGIN    
    
IF @region = 0         SET @region = NULL    
IF @subcompany  = 0 SET  @subcompany  = NULL   
IF @companyID = 0      SET @companyID = NULL   
    
 SELECT Sub_Company_Area_ID,Sub_Company_Area_Name  ,sc.Company_ID,sc.Sub_Company_ID   
 FROM Sub_Company_Area sa    
    
 INNER JOIN dbo.Sub_Company_Region sr   
             ON  sa.Sub_Company_Region_ID = sr.Sub_Company_Region_ID   
        INNER JOIN dbo.Sub_Company sc   
             ON  sc.sub_company_id = sr.Sub_Company_ID   
        INNER JOIN Company c   
             ON  c.company_id = sc.Company_ID   
 WHERE  sc.sub_company_id = ISNULL(@subcompany, sc.Sub_Company_ID)   
        AND sr.Sub_Company_Region_ID = ISNULL(@region, sr.Sub_Company_Region_ID)   
        AND c.company_id = ISNULL(@companyID, c.company_id)   
         ORDER BY  Sub_Company_Area_Name    
   
          
END   
 
GO

