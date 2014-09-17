/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 26/02/13 12:30:56 PM
 ************************************************************/

/* ================================================================================= 
 * Purpose	:	rsp_GetVSSiteDetails
 * Author	:	A.Vinod Kumar
 * Created  :	25/02/2013
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 25/02/2013  	A.Vinod Kumar    Initial Version
 * ================================================================================= 
*/
-- Use the database
USE Enterprise
GO

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetVSSiteDetails'
   )
    DROP PROCEDURE dbo.rsp_GetVSSiteDetails
GO

-- EXEC dbo.rsp_GetVSSiteDetails 1
CREATE PROCEDURE dbo.rsp_GetVSSiteDetails
(@SiteID INT)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	SELECT SITE.Sub_Company_ID,
	       SITE.Site_Address_1,
	       SITE.Site_Address_2,
	       SITE.Site_Address_3,
	       SITE.Site_Address_4,
	       SITE.Site_Address_5,
	       SITE.Site_Code,
	       SITE.Site_Name,
	       Company.Company_Name,
	       Sub_Company.Sub_Company_Name,
	       SITE.Site_Manager,
	       SITE.Site_Postcode,
	       SITE.Site_Reference,
	       SITE.Site_Phone_No,
	       Sub_Company_Area.Sub_Company_Area_Name,
	       Sub_Company_Region.Sub_Company_Region_Name,
	       Sub_Company_District.Sub_Company_District_Name,
	       Site_Trade_Type,
	       SITE.Site_Memo,
	       Sec_Brewery.Sub_Company_Name AS Sec_Brewery_Name,
	       Staff_Last_Name,
	       Staff_First_Name,
	       Site_Start_Date
	FROM   (
	           (
	               (
	                   (
	                       (
	                           (
	                               SITE INNER JOIN Sub_Company ON SITE.Sub_Company_ID = Sub_Company.Sub_Company_ID
	                           ) INNER JOIN Company ON Sub_Company.Company_ID = Company.Company_ID
	                       ) LEFT JOIN Sub_Company_Region ON SITE.Sub_Company_Region_ID = Sub_Company_Region.Sub_Company_Region_ID
	                   ) LEFT JOIN Sub_Company_Area ON SITE.Sub_Company_Area_ID = Sub_Company_Area.Sub_Company_Area_ID
	               ) LEFT JOIN Sub_Company_District ON SITE.Sub_Company_District_ID = Sub_Company_District.Sub_Company_District_ID
	           ) LEFT JOIN Sub_Company AS Sec_Brewery ON SITE.Secondary_Sub_Company_ID = Sec_Brewery.Sub_Company_ID
	       )
	       LEFT JOIN Staff
	            ON  SITE.Staff_ID = Staff.Staff_ID
	WHERE  SITE.Site_ID = @SiteID
	
	-- END
	SET NOCOUNT OFF
END
GO