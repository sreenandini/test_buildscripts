USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'rsp_Vault_GetAllManufacturers' 
)
   DROP PROCEDURE dbo.rsp_Vault_GetAllManufacturers
GO

CREATE PROCEDURE dbo.rsp_Vault_GetAllManufacturers
AS
/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------
										  
*****************************************************************************************************/

BEGIN
		SELECT Manufacturer_ID,
		       Manufacturer_Name
		FROM   Manufacturer WITH(NOLOCK)
		ORDER BY
		       Manufacturer_Name
END
GO

