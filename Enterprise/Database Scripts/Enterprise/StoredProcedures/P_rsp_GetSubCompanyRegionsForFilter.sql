USE [Enterprise]
GO

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetSubCompanyRegionsForFilter'
   )
    DROP PROCEDURE dbo.rsp_GetSubCompanyRegionsForFilter
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------     
-- EXEC dbo.rsp_GetSubCompanyRegionDetails 6,7
-- Description: Select Region Id and name for View site filter.    
--    
-- Inputs:     SubCompany, Company
-- Outputs:    Region Id, Region Name
--    
-- =======================================================================    
--     
-- Revision History    
--     
---------------------------------------------------------------------------  


CREATE PROCEDURE rsp_GetSubCompanyRegionsForFilter
	@SubCompanyId INT = NULL,
	@CompanyId INT = NULL
AS
BEGIN
	
	SET NOCOUNT ON
	
	SELECT Sub_Company_Region_ID,
	       Sub_Company_Region_Name
	FROM Company C
		INNER JOIN Sub_Company SC ON SC.Company_ID = C.Company_ID
		INNER JOIN Sub_Company_Region SCR ON SCR.Sub_Company_ID = SC.Sub_Company_ID
	WHERE  (SC.Sub_Company_ID = ISNULL(@SubCompanyId, SC.Sub_Company_ID))
	       AND  ((C.Company_ID = ISNULL(@CompanyId, C.Company_ID)))
	ORDER BY
	       Sub_Company_Region_Name
END
GO
