USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetRegionDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetRegionDetails]
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

CREATE PROCEDURE [dbo].rsp_GetRegionDetails 
(@subcompany INT = 0, @companyID INT = 0)
AS
BEGIN
	IF @subcompany = 0
	    SET @subcompany = NULL  
	
	IF @companyID = 0
	    SET @companyID = NULL    
	
	SELECT SR.Sub_Company_Region_ID,
	       SR.Sub_Company_Region_Name
	FROM   Sub_Company_Region SR
	       INNER JOIN Sub_Company SC
	            ON  SC.Sub_Company_ID = SR.Sub_Company_ID
	WHERE  SC.Company_ID = ISNULL(@companyID, SC.Company_ID)
	       AND SC.Sub_Company_ID = ISNULL(@subcompany, SC.Sub_Company_ID)
	          
	ORDER BY
	       Sub_Company_Region_Name
END
  

 
GO

