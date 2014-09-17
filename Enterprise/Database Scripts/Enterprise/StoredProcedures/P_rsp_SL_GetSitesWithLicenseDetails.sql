USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetSitesWithLicenseDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetSitesWithLicenseDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_GetSitesWithLIcenseDetails 1
-- -----------------------------------------------------------------    
--    
-- To populate site lists for site licensing.     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 26/03/2012 Dinesh Rathinavel Created
-- 09/10/2012 Venkatesan Haridass Modified for Key status  
-- =================================================================  

CREATE PROCEDURE [dbo].[rsp_SL_GetSitesWithLicenseDetails]
(@UserId       INT = 0)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @TodayDate DATETIME
	SET @TodayDate = GETDATE()
	
	
	EXEC usp_SL_UpdateExpiryDate
	
	SELECT S.[Site_ID],
	       L.[KeyStatusID],
	       LTRIM(RTRIM(S.[Site_Code])) AS Site_Code
	       INTO #CurrentLicense
	FROM   [dbo].[SL_LicenseInfo] L WITH (NOLOCK)
	       INNER JOIN [dbo].[Site] S WITH (NOLOCK)
	            ON  S.[Site_ID] = L.[Site_ID]
	WHERE  ([KeyStatusID] = 2
	       OR  [KeyStatusID] = 1)
	       AND [StartDate] <= @TodayDate
	       AND [ExpireDate] >= @TodayDate


	INSERT INTO #CurrentLicense
	SELECT 
	       S.[Site_ID],
	       L.[KeyStatusID],
	       LTRIM(RTRIM(S.[Site_Code])) AS Site_Code
	FROM   [dbo].[SL_LicenseInfo] L WITH (NOLOCK)
	       INNER JOIN [dbo].[SL_Rules] R WITH (NOLOCK)
	            ON  R.[RuleID] = L.[RuleID]
	       INNER JOIN [dbo].[Site] S WITH (NOLOCK)
	            ON  S.[Site_ID] = L.[Site_ID]
	       LEFT JOIN #CurrentLicense CL
	            ON  CL.[Site_Code] = LTRIM(RTRIM(S.[Site_Code]))
	WHERE  CL.[Site_Code] IS NULL
	       AND (
	               (L.KeyStatusID = 3)
	               OR (L.KeyStatusID = 4 AND L.StartDate < GETDATE())
	           )
	       AND L.ActivatedDateTime IS NOT NULL
	ORDER BY
	       L.UpdatedDateTime DESC




	SELECT DISTINCT 
	       C.[Company_Name],
	       SC.[Sub_Company_Name],
	       S.[Site_Name],
	       S.[Site_ID],
	       C.[Company_ID],
	       SC.[Sub_Company_ID],
	       S.[Site_Code],
	       (
	           SELECT TOP 1 KS.[KeyStatusID]
	           FROM   [dbo].#CurrentLicense KS
	           WHERE  KS.[Site_ID] = S.[Site_ID]
	       ) AS [SiteStatus]
	FROM   [dbo].[Site] S WITH (NOLOCK)
	       INNER JOIN [dbo].[Sub_Company] SC WITH (NOLOCK)
	            ON  S.[Sub_Company_ID] = SC.[Sub_Company_ID]
	       INNER JOIN [dbo].[Company] C WITH (NOLOCK)
	            ON  SC.[Company_ID] = C.[Company_ID]
	       INNER JOIN SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = S.Site_ID
	            AND AllowUser = 1
	            AND SecurityProfileType_ID = 2
	       INNER JOIN STAFF_CUSTOMER_ACCESS SCA
	            ON  SP.Customer_Access_ID = SCA. Customer_Access_ID
	            AND (@UserId = 0 OR SCA.Staff_ID = @UserId)	            
		   INNER JOIN SettingsProfileItems SPI 
		   ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID 
		   AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
		   INNER JOIN dbo.SettingsMaster SM 
		   ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND 
		   UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'
	WHERE  (
	           (S.[Site_Inactive_Date] IS NULL
	           OR S.[Site_Inactive_Date] > @TodayDate)
	           AND S.[Site_Closed] = 0
	       )
	    

	       
	ORDER BY
	       C.[Company_Name] ASC,
	       C.[Company_ID] ASC,
	       SC.[Sub_Company_Name] ASC,
	       SC.[Sub_Company_ID] ASC,
	       S.[Site_Name] ASC,
	       S.[Site_ID] ASC

END

GO

