USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_InsertLicenseDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_InsertLicenseDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SL_InsertLicenseDetails
-- -----------------------------------------------------------------    
--    
-- To Insert License information for site licensing      
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 02/04/12 Venkatesan Haridass Created          
-- 10/04/12 Venkatesan Haridass Edited for Export History Entry
-- 19/04/12 Venkatesan Haridass Edited for Date alone saving
-- =================================================================  
CREATE PROCEDURE [dbo].[usp_SL_InsertLicenseDetails](
			@StartDate DATETIME
           ,@ExpireDate DATETIME
           ,@RuleID INT
           ,@SiteID INT
           ,@LicenseKey VARCHAR(50)
           ,@AlertBeforeDays SMALLINT
           ,@KeyStatusID INT
           ,@StaffID INT)
AS
BEGIN
    SET NOCOUNT ON
	--SET @StartDate = DATEADD(D, 0, DATEDIFF(D, 0, @StartDate))
	--SET @ExpireDate = DATEADD(D, 0, DATEDIFF(D, 0, @ExpireDate))
    INSERT INTO [dbo].[SL_LicenseInfo]
           ([StartDate]
           ,[ExpireDate]
           ,[RuleID]
           ,[Site_ID]
           ,[LicenseKey]
           ,[AlertBeforeDays]
           ,[KeyStatusID]
	       ,[CreatedStaffID]
		   ,[ModifiedStaffID]
		   ,[CreatedDateTime]
		   ,[UpdatedDateTime])
    VALUES
		  (@StartDate
		  ,@ExpireDate
		  ,@RuleID
		  ,@SiteID
		  ,@LicenseKey
		  ,@AlertBeforeDays
		  ,@KeyStatusID
		  ,@StaffID
		  ,@StaffID
		  ,GETDATE()
		  ,GETDATE())
	
	IF @@ERROR <> 0
		RETURN 1
	ELSE
	BEGIN
            DECLARE @LicenseID INT
	        SET @LicenseID = SCOPE_IDENTITY()
            IF @LicenseID > 0
            BEGIN
				EXEC [dbo].[usp_Export_History] @LicenseID, 'SITELICENSING', @SiteID

				IF @@ERROR <> 0
					RETURN 1
				ELSE
					RETURN 0
            END
	END
	RETURN 1
END

GO

