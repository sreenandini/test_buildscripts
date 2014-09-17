USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_getSubCompayRegions'
   )
    DROP PROCEDURE dbo.rsp_getSubCompayRegions
GO

CREATE PROCEDURE dbo.rsp_getSubCompayRegions
	@Site_ID INT
AS
/*****************************************************************************************************
DESCRIPTION    : Retrives all the districts  
CREATED DATE   : 
MODULE		   : Org -> SubComp -> Site -> Area Tab
-- =============================================
-- Example to execute the stored procedure
-- =============================================
-- EXECUTE dbo.rsp_getSubCompayRegions 2
-- GO      

CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR						MODIFIED DATE		DESCRIPTON
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SET NOCOUNT ON
	
	SELECT Sub_Company_Region.Sub_Company_Region_ID,
	       Sub_Company_Region.Sub_Company_Region_Name,
	       Sub_Company_Region.Sub_Company_Region_Description,
	       Staff.Staff_ID,
	       Staff.Staff_Last_Name,
	       Staff.Staff_First_Name
	FROM   (
	           SITE INNER JOIN Sub_Company_Region ON SITE.Sub_Company_ID =
	           Sub_Company_Region.Sub_Company_ID
	       )
	       LEFT JOIN Staff
	            ON  Sub_Company_Region.Staff_ID = Staff.Staff_ID
	WHERE  SITE.Site_ID = @Site_ID
	ORDER BY
	       Sub_Company_Region.Sub_Company_Region_Name ASC
	
	
	/*SELECT Sub_Company_Region.Sub_Company_Region_ID,
	       Sub_Company_Region.Sub_Company_Region_Name,
	       Sub_Company_Region.Sub_Company_Region_Description,
	       Staff.Staff_ID,
	       Staff.Staff_Last_Name,
	       Staff.Staff_First_Name
	FROM   (
	           (
	               SITE INNER JOIN Sub_Company ON SITE.Sub_Company_ID = 
	               Sub_Company.Sub_Company_ID
	           ) INNER JOIN Sub_Company_Region ON Sub_Company.Company_ID =
	           Sub_Company_Region.Company_ID
	       )
	       LEFT JOIN Staff
	            ON  Sub_Company_Region.Staff_ID = Staff.Staff_ID
	WHERE  SITE.Site_ID = @Site_ID
	ORDER BY
	       Sub_Company_Region.Sub_Company_Region_Name ASC*/
END
GO
