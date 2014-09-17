USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_CheckLicenseKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_CheckLicenseKey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_CheckLicenseKey    
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

CREATE PROCEDURE [dbo].[rsp_SL_CheckLicenseKey]
	@LicenseKey VARCHAR(30),
	@SiteCode VARCHAR(50),
	@UserName VARCHAR(50),
	@Result INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	SET @LicenseKey = lTRIM(RTRIM(@LicenseKey))
	SET @SiteCode = LTRIM(RTRIM(@SiteCode))
	SET @UserName = LTRIM(RTRIM(@UserName))
	SET @Result = 0

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

		-- Update License status from 'Created' to 'Active'
		EXEC [dbo].[usp_SL_UpdateLicenseStatus] @LId, 2, @UserId
	END
	ELSE IF @KeyStatus = 2 -- Active 
	BEGIN
		SET @Result = -3
	END
	ELSE IF @KeyStatus = 3 -- Expired
	BEGIN
		SET @Result = -4
	END
	ELSE IF @KeyStatus = 4 -- Cancelled
	BEGIN
		SET @Result = -5
	END

END

GO

