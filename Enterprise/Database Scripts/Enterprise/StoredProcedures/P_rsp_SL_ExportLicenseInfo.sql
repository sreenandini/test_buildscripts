USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_ExportLicenseInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_ExportLicenseInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_ExportLicenseInfo
-- -----------------------------------------------------------------    
--    
-- Generate License info XML     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 10/04/12 Venkatesan Haridass Created          
-- =================================================================
  

CREATE PROCEDURE [dbo].[rsp_SL_ExportLicenseInfo]
	@LicenseInfoID INT,
	@ExpiryDate VARCHAR(100),
	@LicenseKey VARCHAR(100)
AS                    
BEGIN 

	SET NOCOUNT ON  

	DECLARE @SiteLicenseDisableGame XML
	DECLARE @TodayDate DATETIME
	DECLARE @SiteID INT
	DECLARE @DisableGame BIT
	SET @TodayDate = GETDATE()
	SELECT @SiteID = L.[Site_ID] FROM [dbo].[SL_LicenseInfo] L WHERE L.[LicenseInfoID] = @LicenseInfoID

	SELECT TOP 1  
		L.[LicenseInfoID],  
		0 as DisableGames,  
		LTRIM(RTRIM(S.[Site_Code])) AS Site_Code  
	INTO #CurrentLicense  
	FROM [dbo].[SL_LicenseInfo] L  WITH (NOLOCK)
		INNER JOIN [dbo].[SL_Rules] R WITH (NOLOCK) ON R.[RuleID] = L.[RuleID]  
		INNER JOIN [dbo].[Site] S WITH (NOLOCK) ON S.[Site_ID] = L.[Site_ID]  
	WHERE (([StartDate] <= @TodayDate   
		AND [ExpireDate] >= @TodayDate  
		AND [KeyStatusID] = 2) OR (UPPER(LTRIM(RTRIM(ISNULL((SELECT [Setting_Value] FROM [dbo].[Setting] WHERE UPPER(LTRIM(RTRIM([Setting_Name]))) = UPPER('IsSiteLicensingEnabled')),'FALSE')))) = UPPER('FALSE'))) AND L.[Site_ID] = @SiteID

	INSERT INTO #CurrentLicense
	SELECT TOP 1
	       L.[LicenseInfoID],
	       R.[DisableGames],
	       LTRIM(RTRIM(S.[Site_Code])) AS Site_Code
	FROM   [dbo].[SL_LicenseInfo] L WITH (NOLOCK)
	       INNER JOIN [dbo].[SL_Rules] R WITH (NOLOCK)
	            ON  R.[RuleID] = L.[RuleID]
	       INNER JOIN [dbo].[Site] S WITH (NOLOCK)
	            ON  S.[Site_ID] = L.[Site_ID]
	       LEFT JOIN #CurrentLicense CL
	            ON  CL.[Site_Code] = LTRIM(RTRIM(S.[Site_Code]))
	WHERE  CL.[LicenseInfoID] IS NULL
	       AND (
	               (L.KeyStatusID = 3)
	               OR (L.KeyStatusID = 4 AND L.StartDate < GETDATE())

	           )
	       AND L.ActivatedDateTime IS NOT NULL
	ORDER BY
	       L.UpdatedDateTime DESC

	SET @SiteLicenseDisableGame = (SELECT [DisableGames] AS DisableGames FROM #CurrentLicense FOR XML PATH('SITELICENSEDISABLEGAME'), TYPE, ELEMENTS, ROOT('SITELICENSEDISABLEGAME'))

	SELECT @DisableGame = ISNULL([DisableGames],0) FROM #CurrentLicense	
	IF EXISTS (SELECT 1 FROM dbo.Site S INNER JOIN #CurrentLicense CL ON S.Site_Code = CL.Site_Code AND ISNULL(S.SiteLicensing_DisableGames,0) <> @DisableGame)
	BEGIN
		UPDATE dbo.Site 
		SET SiteLicensing_DisableGames = @DisableGame 

		UPDATE tBP SET tBP.Bar_Position_Machine_Enabled = ~@DisableGame
		FROM dbo.Bar_Position tBP INNER JOIN dbo.Site s ON s.Site_ID = tBP.Site_ID 
		AND s.Site_ID = @SiteID
		AND tBP.Bar_Position_End_Date IS NULL

	END

	SELECT 
		  [LicenseInfo].[LicenseInfoID],
		  [LicenseInfo].[StartDate],
		  --[LicenseInfo].[ExpireDate],
		  @ExpiryDate AS [ExpireDate],
		  [LicenseInfo].[RuleID],
		  --[LicenseInfo].[LicenseKey],
		  @LicenseKey AS LicenseKey,
		  [LicenseInfo].[AlertBeforeDays],
		  [LicenseInfo].[KeyStatusID],
	      [LicenseInfo].[CreatedStaffID],
		  [LicenseInfo].[ModifiedStaffID],
		  [LicenseInfo].[CreatedDateTime],
		  [LicenseInfo].[UpdatedDateTime],
		  [LicenseInfo].[ActivatedDateTime],
		  [Rule].[RuleName],
		  [Rule].[ValidationRequired],
		  [Rule].[LockSite],
		  [Rule].[DisableGames],
		  [Rule].[WarningOnly],
		  [Rule].[AlertRequired],
		  [Rule].[CreatedStaffID],
		  [Rule].[ModifiedStaffID],
		  [Rule].[CreatedDateTime],
		  [Rule].[UpdatedDateTime],
		  ISNULL(@SiteLicenseDisableGame,'<SITELICENSEDISABLEGAME><SITELICENSEDISABLEGAME><DisableGames>0</DisableGames></SITELICENSEDISABLEGAME></SITELICENSEDISABLEGAME>')
	  FROM [dbo].[SL_LicenseInfo] LicenseInfo WITH (NOLOCK)
	  INNER JOIN dbo.[SL_Rules] [Rule] WITH (NOLOCK) ON [LicenseInfo].[RuleID] = [Rule].[RuleID] 
	WHERE [LicenseInfo].[LicenseInfoID] = @LicenseInfoID
	 FOR XML AUTO, ELEMENTS, ROOT('LicenseDetails')

END
GO
