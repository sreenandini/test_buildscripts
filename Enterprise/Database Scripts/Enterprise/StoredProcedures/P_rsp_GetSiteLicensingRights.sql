USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteLicensingRights]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteLicensingRights]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetSiteLicensingRights    
-- -----------------------------------------------------------------    
--    
-- To get site licensing rights.     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 05/04/2012 Dinesh Rathinavel Created
--    
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_GetSiteLicensingRights]
	@StaffID INT  
AS  
BEGIN  
	SET NOCOUNT ON

	 SELECT HQ.[HQ_Admin_SiteLicensing],
			  HQ.[HQ_Admin_SiteLicensing_LicenseGen],
			  HQ.[HQ_Admin_SiteLicensing_LicenseGen_RuleInfo],
			  HQ.[HQ_Admin_SiteLicensing_LicenseGen_RuleInfo_AddorEditRule],
			  HQ.[HQ_Admin_SiteLicensing_LicenseGen_KeyGen],
			  HQ.[HQ_Admin_SiteLicensing_ViewCancelLicense],
			  HQ.[HQ_Admin_SiteLicensing_LicenseHistory]
			
	 FROM  dbo.[HQ_User_Access] HQ
		INNER JOIN dbo.[USER_GROUP] UG ON UG.[HQ_User_Access_ID] = HQ.[HQ_User_Access_ID]
		INNER JOIN dbo.[STAFF] ST ON ST.[User_Group_ID] = UG.[User_Group_ID]
	WHERE ST.[Staff_ID] = @StaffID

END

GO

