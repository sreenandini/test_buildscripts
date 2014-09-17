GO
USE ENTERPRISE 
GO
 
IF OBJECT_ID('usp_ExportUnassignedManufacturers') IS NOT NULL
    DROP PROC usp_ExportUnassignedManufacturers
GO
 --Export all manufacturers which are not associated with machien class
CREATE PROC usp_ExportUnassignedManufacturers
	@Site_id INT 
	AS 
BEGIN
	INSERT INTO export_history
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       M.Manufacturer_ID,
	       'MANUFACTURER_DETAILS',
	       s.site_code
	FROM   manufacturer M WITH (NOLOCK),
	       SITE S WITH (NOLOCK)
	WHERE  Manufacturer_ID NOT IN (SELECT DISTINCT Manufacturer_ID
	                               FROM   Machine_Class)
	       --AND S.SiteStatus = 'FULLYCONFIGURED'
	       AND s.Site_ID = @Site_ID
END 
GO 