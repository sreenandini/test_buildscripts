USE [Enterprise]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SendSiteLicenseExpiryWarning]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SendSiteLicenseExpiryWarning]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[usp_SendSiteLicenseExpiryWarning]
@LastUpdatedSTMAlertHour INT
AS
BEGIN



DECLARE @Inc INT
DECLARE @LIId INT
DECLARE @Count INT
SET @Inc=1
CREATE TABLE #tmpSiteLicense(RowID INT IDENTITY(1,1) , LicenseInfoID INT)

INSERT INTO #tmpSiteLicense (LicenseInfoID) 

SELECT SL.LicenseInfoID 
from SL_LicenseInfo SL
INNER JOIN SL_Rules SLR ON SL.RuleId=SLR.RuleID
INNER JOIN Site S ON S.Site_ID = SL.Site_ID
INNER JOIN SettingsProfileItems SPI
	ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
INNER JOIN dbo.SettingsMaster SM
	ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'
WHERE  
SLR.AlertRequired=1 -- Alert Required
AND SL.KeyStatusID=2 --Active License
AND GetDate() between  SL.StartDate AND SL.ExpireDate
AND DateDiff(d,SL.ExpireDate,GetDate())<=SL.AlertBeforeDays
AND SL.LicenseInfoID NOT IN(SELECT TOP 1 SL.LicenseInfoID from SL_LicenseInfo SL1 WHERE SL1.KeyStatusID = 2 
AND SL1.Site_ID = SL.Site_ID AND SL1.StartDate > SL.ExpireDate 
--AND SL1.ExpireDate>=GETDATE()
AND SL1.StartDate between SL.ExpireDate and DATEADD(ss,5,SL.ExpireDate)
ORDER BY SL1.StartDate)



SELECT @Count=Count(*) FROM #tmpSiteLicense

IF @Count>0 
BEGIN
WHILE @Inc<=@Count
BEGIN	
SELECT @LIId=LicenseInfoID FROM #tmpSiteLicense WHERE RowID=@Inc

EXEC usp_SendSiteLicenseAlert NULL,@LIId,5

SET @Inc=@Inc+1 
END

IF EXISTS(Select 1 FROM Setting WHERE Setting_Name='STMAlertLastUpdatedTime') 
BEGIN
 UPDATE Setting set Setting_Value=@LastUpdatedSTMAlertHour where Setting_Name='STMAlertLastUpdatedTime'
END

END



DROP TABLE #tmpSiteLicense

END