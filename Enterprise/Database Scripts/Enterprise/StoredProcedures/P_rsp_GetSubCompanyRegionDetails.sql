/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 06/05/13 2:13:14 PM
 ************************************************************/

-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetSubCompanyRegionDetails'
   )
    DROP PROCEDURE dbo.rsp_GetSubCompanyRegionDetails
GO

-- exec dbo.rsp_GetSubCompanyRegionDetails
CREATE PROCEDURE dbo.rsp_GetSubCompanyRegionDetails
	@SubCompanyId INT = NULL,
	@CompanyId INT = NULL
AS
BEGIN
	SELECT Sub_Company_Region_ID,
	       Sub_Company_Region_Name
	FROM   Sub_Company_Region
	WHERE  (Sub_Company_ID = ISNULL(@SubCompanyId, Sub_Company_ID))
	       OR  ((Company_ID = ISNULL(@CompanyId, Company_ID)))
	ORDER BY
	       Sub_Company_Region_Name
END
GO
