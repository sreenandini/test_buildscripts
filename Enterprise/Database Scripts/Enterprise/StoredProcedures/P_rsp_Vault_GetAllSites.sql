USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetAllSites'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetAllSites
GO

CREATE PROCEDURE dbo.rsp_Vault_GetAllSites
	@User_id INT
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
	
	SELECT S.Site_ID,
	       S.Site_Name
	FROM   [Site] S WITH(NOLOCK) 
	INNER JOIN VW_Enterprise_usersite_lnk SL WITH(NOLOCK)
	ON s.Site_ID = sl.SiteID
	INNER JOIN Staff sf WITH(NOLOCK) 
	ON sl.SecurityUserID=sf.UserTableID 
	WHERE sf.Staff_ID= @User_id  

END
GO


