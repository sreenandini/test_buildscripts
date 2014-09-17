USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_ValidateLicenseDates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_ValidateLicenseDates]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_SL_ValidateLicenseDates
-- -----------------------------------------------------------------    
--    
-- Verify dates for avoiding duplication in site licenses      
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 30/03/12 Venkatesan Haridass Created          
-- 19/04/12 Venkatesan Haridass Modified for date alone check
-- ================================================================= 
 
CREATE PROCEDURE [dbo].[rsp_SL_ValidateLicenseDates] (
@SiteID INT, 
@NewStartDate DATETIME, 
@NewExpiryDate DATETIME)                
AS                            
BEGIN           
		SET NOCOUNT ON
		--SET @NewStartDate = DATEADD(D, 0, DATEDIFF(D, 0, @NewStartDate))
		--SET @NewExpiryDate = DATEADD(D, 0, DATEDIFF(D, 0, @NewExpiryDate))
		IF NOT EXISTS (SELECT 1 AS RESULT 
			FROM   [dbo].[SL_LicenseInfo]
			WHERE  Site_ID = @SiteID 
				 AND ( ([StartDate] BETWEEN @NewStartDate AND @NewExpiryDate)
					OR ([ExpireDate]  BETWEEN @NewStartDate AND @NewExpiryDate)
					OR ([StartDate] < @NewStartDate AND [ExpireDate] > @NewExpiryDate)) 
				 AND KeystatusID != 4)
			RETURN 0
		ELSE
			RETURN 1
END

GO

