USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAllUserRoleLinkinXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAllUserRoleLinkinXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =======================================================================  
-- OUTPUT    To get all users assigned roles based on site id
-- =======================================================================  
-- Revision History  --  Exec  rsp_GetSiteAllUserRoleLinkinXML 1012  
-- Vineetha Mathew 07/11/09  Created
-- Vineetha Mathew 16/11/09   Modified  To fix the issue when XML failed to convert fully to string 
---------------------------------------------------------------------------  
CREATE PROCEDURE [dbo].rsp_GetSiteAllUserRoleLinkinXML  
    
	@Site_Code VARCHAR(50)	
AS   
BEGIN	
	DECLARE @Siteid INT
	DECLARE @XMLData XML

	SELECT @Siteid=site_id FROM SITE WHERE site_code=@Site_Code
	
		IF @Siteid>0	
			BEGIN
				SET @XMLData=(	SELECT 
						URL.SecurityUserID
						,URL.SecurityRoleID 
					FROM Userrole_lnk   URL
					JOIN VW_Enterprise_usersite_lnk  US ON URL.Securityuserid = US.securityuserid
					WHERE US.SiteID = @Siteid

					FOR XML PATH('UserRole'),ROOT('UserRoles') ) 
			
					SELECT CONVERT(VARCHAR(MAX),@XMLData) 	
			 END
		ELSE
			PRINT 'No Such site id '
END  





GO

