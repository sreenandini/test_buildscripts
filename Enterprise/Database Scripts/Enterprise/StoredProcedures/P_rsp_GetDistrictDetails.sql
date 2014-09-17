USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDistrictDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDistrictDetails]
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
CREATE PROCEDURE [dbo].rsp_GetDistrictDetails 
  (
      @region         INT = 0,
      @area           INT = 0,
      @subcompany_id  INT = 0,
      @companyID      INT = 0
  )
  AS
  BEGIN
  	IF @area = 0
  	    SET @area = NULL    
  	
  	IF @region = 0
  	    SET @region = NULL
  	
  	IF @subcompany_id = 0
  	    SET @subcompany_id = NULL
  	
  	IF @companyID = 0
  	    SET @companyID = NULL  
  	
  	SELECT Sub_Company_District_ID,
  	       Sub_Company_District_Name,
  	       sc.Company_ID,
  	       sc.Sub_Company_ID
  	FROM   Sub_Company_District scd
  	       INNER JOIN dbo.Sub_Company_Area sca
  	            ON  scd.Sub_Company_Area_ID = sca.Sub_Company_Area_ID
  	       INNER JOIN dbo.Sub_Company_Region scr
  	            ON  sca.Sub_Company_Region_ID = scr.Sub_Company_Region_ID
  	       INNER JOIN dbo.Sub_Company sc
  	            ON  sc.sub_company_id = scr.Sub_Company_ID
  	       INNER JOIN Company c
  	            ON  c.company_id = sc.Company_ID
  	WHERE  sc.sub_company_id = ISNULL(@subcompany_id, sc.Sub_Company_ID)
  	       AND sca.Sub_Company_Area_ID = ISNULL(@area, sca.Sub_Company_Area_ID)
  	       AND scr.Sub_Company_Region_ID = ISNULL(@region, scr.Sub_Company_Region_ID)
  	       AND c.company_id = ISNULL(@companyID, c.company_id)
  	ORDER BY
  	       Sub_Company_District_Name
  END



 
GO

