/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.3.171
 * Time: 10/17/2013 5:56:16 PM
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
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Vault_GetSitesForDrop]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Vault_GetSitesForDrop]
GO
-- =============================================
-- Author:		<SriHari Jogaraj>
-- Create date: <9th July 2013>
-- Description:	<Get the Site Details in Enterprise - Vault Declaration Screen>
-- =============================================
CREATE PROCEDURE dbo.rsp_Vault_GetSitesForDrop
	@User_id INT
AS
	/*****************************************************************************************************  
DESCRIPTION : Get site and vaults    
CREATED DATE: 10-Jul-2013  
MODULE            : Enterprise client vault declaration        
CHANGE HISTORY :  

rsp_Vault_GetSitesForDrop 1
------------------------------------------------------------------------------------------------------  
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE  
------------------------------------------------------------------------------------------------------  

*****************************************************************************************************/  

BEGIN
	SET NOCOUNT ON   
	
	SELECT DISTINCT 
	       s.Site_ID,
	       s.Site_Name,
	       s.Site_Code
	FROM   SITE s WITH (NOLOCK)
	       INNER JOIN (
	                SELECT DISTINCT MAX(tvd.Drop_id) Drop_id,
	                       AVG(tvd.Site_id) Site_id
	                FROM   tVault_Drops tvd
	                WHERE  tvd.IsDeclared = 1
	                GROUP BY
	                       tvd.site_id
	            ) TI
	            ON  ti.site_id = s.Site_ID
	       INNER JOIN VW_Enterprise_usersite_lnk SL WITH(NOLOCK)
	            ON  s.Site_ID = sl.SiteID
	       INNER JOIN Staff sf WITH(NOLOCK)
	            ON  sl.SecurityUserID = sf.UserTableID
	WHERE  sf.UserTableID = @User_id
	ORDER BY
	       s.Site_Name
END
GO

