USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_UpdateLicenseActivation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_UpdateLicenseActivation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SL_UpdateLicenseActivation    
-- -----------------------------------------------------------------    
--    
-- To check the license key and its status
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 26/03/2012 Dinesh Rathinavel Created
--    
-- =================================================================  

CREATE PROCEDURE [dbo].[usp_SL_UpdateLicenseActivation]
	@LicenseKey VARCHAR(30),
	@SiteCode VARCHAR(50),
	@UserName VARCHAR(50),
	@ActivatedDateTime DATETIME,
	@Result INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	SET @LicenseKey = lTRIM(RTRIM(@LicenseKey))
	SET @SiteCode = LTRIM(RTRIM(@SiteCode))
	SET @UserName = LTRIM(RTRIM(@UserName))
	SET @Result = 0
	DECLARE @TodayDate DATETIME
	SET @TodayDate = GETDATE()
	IF NOT EXISTS (SELECT 1	FROM [dbo].[SL_LicenseInfo] LI
					INNER JOIN [dbo].[Site] S ON S.[Site_ID] = LI.[Site_ID]
				WHERE LTRIM(RTRIM(S.[Site_Code])) = @SiteCode AND LI.[LicenseKey] = @LicenseKey)
	BEGIN
		RETURN
	END

	DECLARE @LId INT
	SET @LId = 0

	DECLARE @KeyStatus INT
	SET @KeyStatus = 0

	SELECT @KeyStatus = LI.[KeyStatusID], @LId = LI.[LicenseInfoID]
	FROM [dbo].[SL_LicenseInfo] LI
		INNER JOIN [dbo].[Site] S ON S.[Site_ID] = LI.[Site_ID]
		INNER JOIN [dbo].[SL_KeyStatus] K ON K.[KeyStatusID] = LI.[KeyStatusID]
	WHERE LTRIM(RTRIM(S.[Site_Code])) = @SiteCode
		AND LI.[LicenseKey] = @LicenseKey

	IF @KeyStatus = 1
	BEGIN
		SET @Result = 1

		DECLARE @UserId INT
		SET @UserId = 0
		SET @UserId = (SELECT Staff_ID FROM Staff WHERE Staff_Username = @UserName)

		UPDATE [dbo].[SL_LicenseInfo] 
			SET [KeyStatusID] = 2, 
			[ActivatedStaffID] = @UserId, 
			ActivatedDateTime =@ActivatedDateTime,
			UpdatedDateTime =  @ActivatedDateTime
			WHERE LTRIM(RTRIM([LicenseKey])) = @LicenseKey  

		UPDATE [dbo].[SL_LicenseInfo] 
			SET [KeyStatusID] = 3, 
				[ModifiedStaffID] = (SELECT Staff_ID FROM Staff WHERE Staff_UserName ='admin'),
				[UpdatedDateTime] = [ExpireDate]
		WHERE [StartDate] < @TodayDate 
			AND [ExpireDate] < @TodayDate
		AND LTRIM(RTRIM([LicenseKey])) = @LicenseKey

	END
	ELSE IF @KeyStatus = 2 -- Active 
	BEGIN
		UPDATE [dbo].[SL_LicenseInfo] 
			SET [KeyStatusID] = 3, 
				[ModifiedStaffID] = (SELECT Staff_ID FROM Staff WHERE Staff_UserName ='admin'),
				[UpdatedDateTime] = [ExpireDate]
		WHERE [StartDate] < @TodayDate 
			AND [ExpireDate] < @TodayDate
		AND LTRIM(RTRIM([LicenseKey])) = @LicenseKey
		SET @Result = -3
	END
	ELSE IF @KeyStatus = 3 -- Expired
	BEGIN
		UPDATE [dbo].[SL_LicenseInfo] SET [ActivatedStaffID] = @UserId, ActivatedDateTime =@ActivatedDateTime WHERE LTRIM(RTRIM([LicenseKey])) = @LicenseKey
		SET @Result = -4
	END
	ELSE IF @KeyStatus = 4 -- Cancelled
	BEGIN
		UPDATE [dbo].[SL_LicenseInfo] SET [ActivatedStaffID] = @UserId, ActivatedDateTime =@ActivatedDateTime WHERE LTRIM(RTRIM([LicenseKey])) = @LicenseKey
		SET @Result = -5
	END

	DECLARE @SiteID INT
	DECLARE @LicenseInfoId INT
    SELECT @SiteID = Site_ID, @LicenseInfoId = LicenseInfoId FROM [dbo].[SL_LicenseInfo] WHERE LTRIM(RTRIM([LicenseKey])) = @LicenseKey
    EXEC [dbo].[usp_Export_History] @LicenseInfoId, 'SITELICENSING', @SiteID

END

GO

