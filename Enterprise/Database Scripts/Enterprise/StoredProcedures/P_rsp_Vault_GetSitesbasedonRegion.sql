/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.3.171
 * Time: 10/17/2013 5:50:17 PM
 ************************************************************/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Vault_GetSitesbasedonRegion]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Vault_GetSitesbasedonRegion]
GO
-- =============================================
-- Author:		<SriHari Jogaraj>
-- Create date: <9th July 2013>
-- Description:	<Get the Site Details in Enterprise - Vault Declaration Screen>
-- =============================================
CREATE PROCEDURE dbo.rsp_Vault_GetSitesbasedonRegion
	@Region_Id INT,
	@User_id INT
AS
	/*****************************************************************************************************  
DESCRIPTION : Get site and vaults    
CREATED DATE: 10-Jul-2013  
MODULE            : Enterprise client vault declaration        
CHANGE HISTORY :  
EXAMPLE : EXEC rsp_Vault_GetSitesbasedonRegion 0,1
select * from site
------------------------------------------------------------------------------------------------------  
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE  
------------------------------------------------------------------------------------------------------  

*****************************************************************************************************/  

BEGIN
	SET NOCOUNT ON  
		
	IF ISNULL(@Region_Id, 0) = 0
	BEGIN
	    SELECT DISTINCT 
	           s.Site_ID,
	           s.Site_Name,
	           s.Site_Code
	    FROM   SITE s WITH (NOLOCK)
	           INNER JOIN (
	                    SELECT DISTINCT MAX(tvd.Drop_id) Drop_id,AVG(tvd.Site_id) Site_id
	                    FROM   tVault_Drops tvd
	                    WHERE  tvd.IsDeclared = 0
	                    AND tvd.IsDropComplete = 1
	                    GROUP BY tvd.site_id
	                ) TD
	                ON  td.site_id = s.Site_ID
	           INNER JOIN SettingsProfileItems spi
	                ON  s.Site_Setting_Profile_ID = spi.SettingsProfileItems_SettingsProfile_ID
	           INNER JOIN SettingsMaster SM
	                ON  spi.SettingsProfileItems_SettingsMaster_ID = sm.SettingsMaster_ID
	                AND sm.SettingsMaster_Name = 'CentralizedVaultDeclaration'
	           INNER JOIN VW_Enterprise_usersite_lnk SL WITH(NOLOCK)
	                ON  s.Site_ID = sl.SiteID
	           INNER JOIN Staff sf WITH(NOLOCK)
	                ON  sl.SecurityUserID = sf.UserTableID
	    WHERE  spi.SettingsProfileItems_SettingsMaster_Values = 'true'
	           AND sf.UserTableID = @User_id
	    ORDER BY
	           s.Site_Name
	END
	ELSE
	BEGIN
	    SELECT DISTINCT 
	           s.Site_ID,
	           s.Site_Name,
	           s.Site_Code
	    FROM   SITE s WITH (NOLOCK)
	           INNER JOIN (
	                   SELECT DISTINCT MAX(tvd.Drop_id) Drop_id,AVG(tvd.Site_id) Site_id
	                    FROM   tVault_Drops tvd
	                    WHERE  tvd.IsDeclared = 0
	                     AND tvd.IsDropComplete = 1
	                    GROUP BY tvd.site_id 
	                ) TD
	                ON  td.site_id = s.Site_ID
	           INNER JOIN SettingsProfileItems spi
	                ON  s.Site_Setting_Profile_ID = spi.SettingsProfileItems_SettingsProfile_ID
	           INNER JOIN SettingsMaster SM
	                ON  spi.SettingsProfileItems_SettingsMaster_ID = sm.SettingsMaster_ID
	                AND sm.SettingsMaster_Name = 'CentralizedVaultDeclaration'
	           INNER JOIN VW_Enterprise_usersite_lnk SL WITH(NOLOCK)
	                ON  s.Site_ID = sl.SiteID
	           INNER JOIN Staff sf WITH(NOLOCK)
	                ON  sl.SecurityUserID = sf.UserTableID
	           INNER JOIN Sub_Company_Region scr
	                ON  scr.Sub_Company_Region_ID = s.Sub_Company_Region_ID
	    WHERE  spi.SettingsProfileItems_SettingsMaster_Values = 'true'
	           AND sf.UserTableID = @User_id
	           AND scr.Sub_Company_Region_ID = @Region_Id
	    ORDER BY
	           s.Site_Name
	END
END
GO
