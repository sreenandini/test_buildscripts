USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetLicenseHistoryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetLicenseHistoryDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_GetLicenseHistoryDetails    
-- -----------------------------------------------------------------    
--    
-- License History Search     
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 26/03/2012 Dinesh Rathinavel Created
--    
-- ================================================================= 

CREATE PROCEDURE [rsp_SL_GetLicenseHistoryDetails] 
	@CompanyId INT = 0,
	@SubCompanyId INT = 0,
	@SiteId INT = 0,
	@FromStartDate DATETIME = NULL,
	@ToStartDate DATETIME = NULL,
	@FromExpiryDate DATETIME = NULL,
	@ToExpiryDate DATETIME = NULL,
	@KeyStatusId INT = 0,
	@ValidationReq BIT = NULL,
	@LockSite BIT = NULL,
	@DisableEGM BIT = NULL,
	@WarningOnly BIT = NULL,
	@AlertRequired BIT = NULL,
	@UserId       INT = 0,
	@CreatedStaffID INT=0,
    @ActivatedStaffID INT=0,
    @CancelledStaffID INT=0
	
AS
BEGIN

	SET NOCOUNT ON;
	SET @FromStartDate = DATEADD(D, 0, DATEDIFF(D, 0, @FromStartDate))
	SET @ToStartDate = DATEADD(D, 0, DATEDIFF(D, 0, @ToStartDate))+cast('11:59PM' as datetime)
	SET @FromExpiryDate = DATEADD(D, 0, DATEDIFF(D, 0, @FromExpiryDate))
	SET @ToExpiryDate = DATEADD(D, 0, DATEDIFF(D, 0, @ToExpiryDate))+cast('11:59PM' as datetime)
	
	
	--SET @FromStartDate = DATEDIFF(DAY, @FromStartDate, GETDATE())
	--SET @ToStartDate = DATEDIFF(DAY, @ToStartDate, GETDATE())
	--SET @FromExpiryDate = DATEDIFF(DAY, @FromExpiryDate, GETDATE())
	--SET @ToExpiryDate = DATEDIFF(DAY, @ToExpiryDate, GETDATE())

	SELECT 
		C.Company_Name AS [CompanyName],
		SC.Sub_Company_Name AS [SubCompanyName],
		SI.Site_Name AS [SiteName],
		SI.Site_Code AS [SiteCode],
		LI.[LicenseInfoID],
		LI.[LicenseKey],
		LI.[KeyStatusID],
		LI.[StartDate] AS [StartDate],
		LI.[ExpireDate] AS [ExpireDate],
		KS.[KeyText],
		R.[RuleName],
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
	FROM [dbo].[Site] SI
		INNER JOIN [dbo].[Sub_Company] SC ON SC.[Sub_Company_ID] = SI.[Sub_Company_ID] 
		INNER JOIN [dbo].[Company] C ON C.[Company_ID] = SC.[Company_ID]
		INNER JOIN [dbo].[SL_LicenseInfo] LI ON LI.[Site_ID] = SI.[Site_ID]
		INNER JOIN [dbo].[SL_Rules] R ON R.[RuleID] = LI.[RuleID]
		INNER JOIN [dbo].[SL_KeyStatus] KS ON KS.[KeyStatusID] = LI.[KeyStatusID]
		INNER JOIN [dbo].[Staff] ST ON ST.[Staff_ID] = LI.[CreatedStaffID]
		LEFT OUTER JOIN [dbo].[Staff] ST1 ON ST1.[Staff_ID] = LI.[ActivatedStaffID] 
		LEFT OUTER JOIN [dbo].[Staff] ST2 ON ST2.[Staff_ID] = LI.[CancelledStaffID]
		INNER JOIN SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = SI.Site_ID
	            AND AllowUser = 1
	            AND SecurityProfileType_ID = 2
	    INNER JOIN STAFF_CUSTOMER_ACCESS SCA
	            ON  SP.Customer_Access_ID = SCA. Customer_Access_ID
	            AND (@UserId = 0 OR SCA.Staff_ID = @UserId)
	WHERE (@SiteId = 0 OR SI.[Site_ID] = @SiteId)
		AND (@CompanyId = 0 OR C.[Company_ID] = @CompanyId)
		AND (@SubCompanyId = 0 OR SC.[Sub_Company_ID] = @SubCompanyId)
		AND LI.[StartDate] >= @FromStartDate AND LI.[StartDate] <=  @ToStartDate  
		AND LI.[ExpireDate] >= @FromExpiryDate AND LI.[ExpireDate] <= @ToExpiryDate
		AND (@KeyStatusId = 0 OR KS.[KeyStatusId] = @KeyStatusId)
		AND (@ValidationReq IS NULL OR  R.[ValidationRequired] = @ValidationReq)
		AND (@LockSite IS NULL OR R.[LockSite] = @LockSite)
		AND (@DisableEGM IS NULL OR R.[DisableGames] = @DisableEGM)
		AND (@WarningOnly IS NULL OR R.[WarningOnly] = @WarningOnly)
		AND (@AlertRequired IS NULL OR R.[AlertRequired] = @AlertRequired)
		AND (@CreatedStaffID =0 OR LI.CreatedStaffID =@CreatedStaffID)
		AND (@ActivatedStaffID =0 OR LI.ActivatedStaffID=@ActivatedStaffID)
		AND (@CancelledStaffID =0 OR LI.CancelledStaffID=@CancelledStaffID)

END

GO





