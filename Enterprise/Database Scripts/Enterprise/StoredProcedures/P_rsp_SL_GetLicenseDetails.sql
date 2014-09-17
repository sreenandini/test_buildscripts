USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetLicenseDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetLicenseDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_GetLicenseDetails    
-- -----------------------------------------------------------------    
--    
-- To populate license for site licensing     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 26/03/2012 Dinesh Rathinavel Created
--    
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_SL_GetLicenseDetails]
	@SiteID INT = 0
AS  
BEGIN
	SET NOCOUNT ON

	SELECT  
			LI.[LicenseInfoID],
			S.[Site_Name] AS [SiteName],
			S.[Site_Code] AS [SiteCode],
			LI.[Licensekey],
			LI.[StartDate] AS [StartDate],
			LI.[ExpireDate] AS [ExpiryDate],
			LI.[KeyStatusID],
			KS.[KeyText],
			R.[ValidationRequired],
			R.[LockSite],
			R.[DisableGames],
			R.[WarningOnly],
			R.[AlertRequired],
			CASE WHEN R.[AlertRequired] = 1
				THEN LI.[AlertBeforeDays] 
				ELSE NULL 
			END AS [AlertBefore],
			ST.[Staff_First_Name] + ', ' + ST.[Staff_Last_Name] AS [GeneratedBy],
			LI.[CreatedDateTime] AS [GeneratedDateTime],
			ST1.[Staff_First_Name] + ', ' + ST1.[Staff_Last_Name] AS [ActivatedBy],
			LI.[ActivatedDateTime] AS [ActivatedDateTime],
			ST2.[Staff_First_Name] + ', ' + ST2.[Staff_Last_Name] AS [CancelledBy],
			LI.[CancelledDateTime] AS [CancelledDateTime]
	FROM [dbo].[Site] S
			INNER JOIN [dbo].[Sub_Company] SC ON S.[Sub_Company_ID] = SC.[Sub_Company_ID] 
			INNER JOIN [dbo].[Company] C ON SC.[Company_ID] = C.[Company_ID]
			INNER JOIN [dbo].[SL_LicenseInfo] LI ON LI.[Site_ID] = S.[Site_ID]
			INNER JOIN [dbo].[SL_Rules] R ON R.[RuleID] = LI.[RuleID]
			INNER JOIN [dbo].[SL_KeyStatus] KS ON KS.[KeyStatusID] = LI.[KeyStatusID]
			INNER JOIN [dbo].[Staff] ST ON ST.[Staff_ID] = LI.[CreatedStaffID]
			LEFT OUTER JOIN [dbo].[Staff] ST1 ON ST1.[Staff_ID] = LI.[ActivatedStaffID] 
			LEFT OUTER JOIN [dbo].[Staff] ST2 ON ST2.[Staff_ID] = LI.[CancelledStaffID]
	WHERE (S.[Site_ID] = @SiteID)
END

GO

