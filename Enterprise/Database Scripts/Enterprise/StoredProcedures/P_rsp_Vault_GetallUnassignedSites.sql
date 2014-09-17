USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'rsp_Vault_GetallUnassignedSites'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetallUnassignedSites
GO

CREATE PROCEDURE dbo.rsp_Vault_GetallUnassignedSites
	@User_ID INT
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: 26-aug-2012
MODULE		: PROC used in vault creation screen to get all unassigned sites and vault
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------


*****************************************************************************************************/
BEGIN
	SELECT td.NGADevice_ID,
	       td.[Name]
	FROM   tVault_Devices td WITH(NOLOCK)
	WHERE  td.End_Date IS NULL
		   AND td.SoldDate IS NULL
	       AND td.NGADevice_ID NOT IN (SELECT NGADevice_ID
	                                   FROM   tNGAInstallations ti
	                                   WHERE  ti.Assigned_To_Site IS NOT NULL
	                                   AND ti.End_Date IS NULL
	                                   )
		   AND 
			(td.IsWebServiceEnabled = 0
			OR   
			(td.IsWebServiceEnabled = 1 
			AND EXISTS (SELECT ''
						FROM   tVault_Cassettes tvc
						 INNER JOIN tVault_CassetteTypes tt   WITH (NOLOCK)
							ON tvc.Type=tt.CassetteType_ID
						WHERE  tvc.IsActive = 1 AND tt.CassetteType_Name<>'Rejection'
	                      AND tvc.Vault_ID = td.Vault_ID )))
	ORDER by td.[Name] 
	
	SELECT s.Site_ID,
	       s.Site_Name,
	       s.Site_Code
	FROM   SITE s WITH(NOLOCK)
	       INNER JOIN VW_Enterprise_usersite_lnk us WITH(NOLOCK)
	            ON  s.Site_ID = us.SiteID
	       INNER JOIN Staff sf WITH(NOLOCK)
	            ON  us.SecurityUserID = sf.UserTableID
	WHERE  sf.UserTableID = @User_ID
	       AND s.Site_ID NOT IN (SELECT site_id
	                             FROM   tNGAInstallations ti
	                             WHERE  ti.End_Date IS NULL)
	ORDER BY  s.Site_Name
END
GO
