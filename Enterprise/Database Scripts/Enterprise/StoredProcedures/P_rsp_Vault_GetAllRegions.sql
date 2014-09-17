/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 23/07/13 9:27:32 PM
 ************************************************************/

USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetAllRegions'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetAllRegions
GO

CREATE PROCEDURE dbo.rsp_Vault_GetAllRegions
AS
	/*****************************************************************************************************
DESCRIPTION	: For loading regions in Vault-OutstandingDrop Screen  
CREATED DATE: 23rd July 2013
MODULE		: Vault	
CHANGE HISTORY :
Example : EXEC rsp_Vault_GetAllRegions
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SELECT DISTINCT scr.Sub_Company_Region_ID,
	       scr.Sub_Company_Region_Name
	FROM   [Site] s WITH (NOLOCK)
	       INNER JOIN Sub_Company_Region scr WITH (NOLOCK)
	            ON  scr.Sub_Company_Region_ID = s.Sub_Company_Region_ID
END
GO