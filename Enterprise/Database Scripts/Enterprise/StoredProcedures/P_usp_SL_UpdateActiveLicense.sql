USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_SL_UpdateActiveLicense]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_SL_UpdateActiveLicense]
GO
--[usp_SL_UpdateActiveLicense] 27,2,1,0
USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SL_UpdateActiveLicense]
	@LicenseInfoId INT,
	@StatusKey INT,
	@UserId INT
AS
BEGIN
	DECLARE @Result INT
 DECLARE @STMAlertForSiteLicense VARCHAR(500)        
 SELECT @STMAlertForSiteLicense = setting_value    
 FROM   setting    
 WHERE  setting_name = 'STMAlertForSiteLicensing' 
	SET NOCOUNT ON
	IF EXISTS (
	       SELECT 1
	       FROM   [dbo].[SL_LicenseInfo]
	       WHERE  [LicenseInfoID] = @LicenseInfoId
	              AND KeyStatusID = @StatusKey
	   )
	BEGIN
	    SET @Result = 2
	END
	ELSE 
	IF EXISTS(
	       SELECT 1
	       FROM   [dbo].[SL_LicenseInfo]
	       WHERE  [LicenseInfoID] = @LicenseInfoId
	              AND KeyStatusID = 4
	   )
	BEGIN
	    SET @Result = 3
	END
	ELSE 
	IF EXISTS (
	       SELECT 1
	       FROM   [dbo].[SL_LicenseInfo]
	       WHERE  [LicenseInfoID] = @LicenseInfoId
	              AND [ExpireDate] > GETDATE()
	   )
	BEGIN
	    UPDATE [dbo].[SL_LicenseInfo]
	    SET    [KeyStatusID] = @StatusKey,
	           [ActivatedDateTime] = GETDATE(),
	           [ActivatedStaffID] = @UserId,
	           [UpdatedDateTime] = GETDATE()
	    WHERE  [LicenseInfoID] = @LicenseInfoId
	           AND [ExpireDate] > GETDATE()
	    
	    DECLARE @SiteID INT
	    SELECT @SiteID = Site_ID
	    FROM   [dbo].[SL_LicenseInfo]
	    WHERE  [LicenseInfoID] = @LicenseInfoId
	    
	    EXEC [dbo].[usp_Export_History] @LicenseInfoId,
	         'ACTIVELICENSE',
	         @SiteID
	         
	IF(UPPER(ISNULL(@STMAlertForSiteLicense,'False'))='TRUE')
    EXEC usp_SendSiteLicenseAlert @UserId,@LicenseInfoId,@StatusKey
	    
	    SET @Result = 0
	END
	ELSE
	BEGIN
	    EXEC usp_SL_UpdateLicenseStatus @LicenseInfoId,
	         3,
	         1
	    
	    SET @Result = 1
	END
	RETURN @Result
END
GO