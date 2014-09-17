USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SL_UpdateExpiryDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SL_UpdateExpiryDate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SL_UpdateExpiryDate    
-- -----------------------------------------------------------------    
--    
-- To update expiry status for the expired licenses. 
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 19/04/2012 Dinesh Rathinavel Created
-- 09/10/12 Venkatesan Haridass Modified for Date and Time Check   
-- =================================================================  

CREATE PROCEDURE [dbo].[usp_SL_UpdateExpiryDate]
AS
BEGIN
	SET NOCOUNT ON


	DECLARE @DateTimeNow DATETIME
	SET @DateTimeNow = GETDATE()
DECLARE @ModifiedStaffID INT
 DECLARE @STMAlertForSiteLicense VARCHAR(500)        
 SELECT @STMAlertForSiteLicense = setting_value    
 FROM   setting    
 WHERE  setting_name = 'STMAlertForSiteLicensing' 

DECLARE @Inc INT
DECLARE @LIId INT
DECLARE @Count INT
SET @Inc=1
CREATE TABLE #tmpSiteLicense(RowID INT IDENTITY(1,1) , LicenseInfoID INT,ModifiedStaffID INT)

INSERT INTO #tmpSiteLicense(LicenseInfoID,ModifiedStaffID) 
SELECT SL.LicenseInfoID,SL.ModifiedStaffID
From SL_LicenseInfo SL 
WHERE [StartDate] < @DateTimeNow 
			AND [ExpireDate] < @DateTimeNow -- Expiry Check
			AND [KeyStatusID] <> 3 -- Ignore Expired Records
			AND [KeyStatusID] <> 4 -- Ignore Cancelled Records
			
			
	IF (UPPER(LTRIM(RTRIM(ISNULL((SELECT [Setting_Value] FROM [dbo].[Setting] WHERE UPPER(LTRIM(RTRIM([Setting_Name]))) = UPPER('IsSiteLicensingEnabled')),'FALSE')))) = UPPER('FALSE'))
		RETURN

	Declare @tempAffectedData table (UpdatedDateTime DATETIME);

	UPDATE [dbo].[SL_LicenseInfo] 
			SET [KeyStatusID] = 3, 
				[ModifiedStaffID] = (SELECT Staff_ID FROM Staff WHERE Staff_UserName ='admin'),
				[UpdatedDateTime] = [ExpireDate]
	OUTPUT INSERTED.[UpdatedDateTime] INTO @tempAffectedData		
	WHERE [StartDate] < @DateTimeNow 
			AND [ExpireDate] < @DateTimeNow
			AND [KeyStatusID] <> 3 -- Ignore Expired Records
			AND [KeyStatusID] <> 4 -- Ignore Cancelled Records


	 INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)
	 SELECT
		@DateTimeNow,	
		SL.[LicenseInfoID],
		'SITELICENSING',
		S.[Site_Code]		
	FROM [dbo].[SL_LicenseInfo] SL 
	INNER JOIN [dbo].[Site] S ON S.Site_ID = SL.Site_ID 
		WHERE [UpdatedDateTime] IN (SELECT UpdatedDateTime FROM @tempAffectedData)
		

SELECT @Count=COUNT(*) FROM #tmpSiteLicense 
IF(UPPER(ISNULL(@STMAlertForSiteLicense,'False'))='TRUE')
BEGIN		
IF @Count>0 
BEGIN
WHILE @Inc<=@Count
BEGIN
SELECT @LIId=LicenseInfoID,@ModifiedStaffID=ModifiedStaffID FROM #tmpSiteLicense WHERE RowID=@Inc

EXEC usp_SendSiteLicenseAlert @ModifiedStaffID,@LIId,3

SET @Inc=@Inc+1 
END

END
	END
		

   

END

GO

