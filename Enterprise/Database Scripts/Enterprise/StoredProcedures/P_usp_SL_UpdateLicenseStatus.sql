USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_UpdateLicenseStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_UpdateLicenseStatus]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SL_UpdateLicenseStatus    
-- -----------------------------------------------------------------    
--    
-- To update site license key status.
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 26/03/2012 Dinesh Rathinavel Created
-- 10/04/2012 Venkatesan Haridass updated for Export History
-- 17/01/2013 Durga Devi - Updated for STM alert
-- =================================================================  

CREATE PROCEDURE [dbo].[usp_SL_UpdateLicenseStatus]
	@LicenseInfoId INT,
	@StatusKey INT,
	@UserId INT
AS  
BEGIN         
	SET NOCOUNT ON
    
   DECLARE @STMAlertForSiteLicense VARCHAR(500)        
 SELECT @STMAlertForSiteLicense = setting_value    
 FROM   setting    
 WHERE  setting_name = 'STMAlertForSiteLicensing' 
    
	IF @StatusKey = 2 
	BEGIN
		UPDATE [dbo].[SL_LicenseInfo] 
		SET 
			[KeyStatusID] = @StatusKey, 
			[ActivatedDateTime] =  GETDATE(),
			[ActivatedStaffID] = @UserId,
			[UpdatedDateTime] =  GETDATE()
		WHERE [LicenseInfoID] = @LicenseInfoId
    END
	ELSE IF @StatusKey = 4
	BEGIN
		UPDATE [dbo].[SL_LicenseInfo] 
		SET 
			[KeyStatusID] = @StatusKey, 
			[CancelledDateTime] =  GETDATE(),
			[UpdatedDateTime] =  GETDATE(),
			[CancelledStaffID] = @UserId
		WHERE [LicenseInfoID] = @LicenseInfoId
	END
	ELSE 
	BEGIN
		UPDATE [dbo].[SL_LicenseInfo] 
		SET 
			[KeyStatusID] = @StatusKey, 
			[UpdatedDateTime] =  [ExpireDate],
			[ModifiedStaffID] = @UserId
		WHERE [LicenseInfoID] = @LicenseInfoId
	END

	DECLARE @SiteID INT
    SELECT @SiteID = Site_ID FROM [dbo].[SL_LicenseInfo] WHERE [LicenseInfoID] = @LicenseInfoId
    EXEC [dbo].[usp_Export_History] @LicenseInfoId, 'SITELICENSING', @SiteID
    
    
    IF(UPPER(ISNULL(@STMAlertForSiteLicense,'False'))='TRUE')
    EXEC usp_SendSiteLicenseAlert @UserId,@LicenseInfoId,@StatusKey

END
GO

