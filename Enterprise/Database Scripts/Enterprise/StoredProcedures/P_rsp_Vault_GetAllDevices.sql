USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetAllDevices'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetAllDevices
GO

CREATE PROCEDURE dbo.rsp_Vault_GetAllDevices
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
rsp_Vault_GetAllDevices 1
select * from tVault_Devices
*****************************************************************************************************/

BEGIN
	SELECT td.Name Vault,
	       td.Vault_ID,
	       ISNULL(s.Site_ID, '-') Site_ID,
	       ISNULL(s.Site_Name, '-') Site_Name,
	       ISNULL(s.Site_Code, '-') Site_Code,
	       td.Serial_NO,
	       td.[Active],
	       CASE 
	            WHEN td.[active] = 1 THEN 'Active' --Site is Active
	            WHEN ISNULL(s.Site_ID, 0) <> 0 and td.[active] = 0 THEN 'Assigned' --Site is Assigned
	            ELSE 'Unassigned' --Site is UnAssigned
	       END AS [Status]
	FROM   tVault_Devices td WITH(NOLOCK)
	       LEFT JOIN tNGAInstallations ti WITH(NOLOCK)
	            ON  ti.NGADevice_ID = td.NGADevice_ID
	            AND ti.End_Date IS NULL
	       LEFT JOIN SITE s
	            ON  s.Site_ID = ti.Site_ID
	       LEFT JOIN (
	                VW_Enterprise_usersite_lnk SL WITH(NOLOCK) 
	                INNER JOIN Staff sf WITH(NOLOCK) 
	                ON sl.SecurityUserID = sf.UserTableID
	                AND sf.Staff_ID = @User_id
	            )
	            ON  s.Site_ID = sl.SiteID
	WHERE  td.End_Date IS NULL
	       AND td.SoldDate IS NULL
	ORDER BY
	       td.[Name]
END
GO