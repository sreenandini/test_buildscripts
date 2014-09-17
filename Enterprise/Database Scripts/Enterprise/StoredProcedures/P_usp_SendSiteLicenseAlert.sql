USE [Enterprise]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SendSiteLicenseAlert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SendSiteLicenseAlert]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
@Type

2	Active
3	Expired
4	Cancelled
5   License Expiry - Alert before Expiry Date using configuration
*/

Create PROCEDURE [dbo].[usp_SendSiteLicenseAlert]
@LoginUserID INT=NULL,
@LicenseInfoID INT,
@Type INT
AS
BEGIN
	DECLARE @VersionName  VARCHAR(20)    
	DECLARE @temp        NVARCHAR(MAX)   
	DECLARE @XML XML   
	DECLARE @MessageDateTime DATETIME  
	DECLARE @siteCode VARCHAR(50)
	DECLARE @AlertInDays INT
	DECLARE @ExpiryDate DateTime
	DECLARE @ExpiryDays INT
	
	SELECT @siteCode=Site_Code From [Site] Where Site_ID In  (Select Site_ID from SL_LicenseInfo where LicenseInfoID=@LicenseInfoID)
	
	               
	SELECT TOP 1 @VersionName = VersionName
	FROM   VersionHistory
	ORDER BY
	       1 DESC
	
	SELECT @MessageDateTime = GETDATE()    
	
	SET @temp = ''    
	
	IF (@Type=3) -- Expired
 	BEGIN
	
	SET @XML = (
	SELECT TOP 1 
	       'LICENSEEXPIRED' AS [Source],
	       @VersionName AS [BMCVersion],
	       '3001' AS ExceptionCode, 
	       '000' AS OperatorId,
	       @temp AS [SubCode],
	       C.Company_Name AS Company,
	       ISNULL(SCR.Sub_Company_Region_Name, '') AS [Region],
	       ISNULL(SCA.Sub_Company_Area_Name, '') AS [Area],
	       S.Site_Code AS [SiteId],
	       S.Site_Name AS [SiteName],
	       --S.Site_Code AS [Asset],
	       SL.StartDate AS [LicenseStartDate],
	       SL.ExpireDate AS [LicenseExpireDate],
	       ST.Staff_First_Name+' '+ST.Staff_Last_Name AS [CreatedStaffName],
	       SR.RuleName AS [RuleName],
	       convert(VARCHAR, @MessageDateTime , 120) AS [MessageDateTime]
	FROM   SL_LicenseInfo SL
	       INNER JOIN SL_Rules SR
	            ON SL.RuleID=Sr.RuleID
	       INNER JOIN Staff ST
				ON ST.Staff_ID = SL.CreatedStaffID
	       INNER JOIN Site S
				ON S.Site_ID=SL.Site_ID
     	   INNER JOIN SettingsProfileItems SPI
				ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
		   INNER JOIN dbo.SettingsMaster SM
				ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'
		   INNER JOIN Sub_Company SC
				ON S.Sub_Company_ID=SC.Sub_Company_ID					  
	       INNER JOIN dbo.Company C
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN Sub_Company_Region SCR
	            ON  SCR.SUB_COMPANY_REGION_ID = S.SUB_COMPANY_REGION_ID
	       LEFT JOIN Sub_Company_Area SCA
	            ON  SCA.SUB_COMPANY_AREA_ID = S.SUB_COMPANY_AREA_ID
	       
	WHERE  SL.LicenseInfoID=@LicenseInfoID AND SL.ActivatedDateTime IS NOT NULL
	       
	
	       
	       FOR XML PATH('') ,TYPE, ROOT('BMCRequest'))
	 
	 IF @XML IS NULL 
	 RETURN
	 
	 INSERT INTO STM_Export_History ([Type],ClientID,Site_Code,[Message],Received_Date) VALUES
	 ('LICENSEEXPIRED',1,@siteCode,@XML,GETDATE())
	 
	
	END
	
	ELSE IF (@Type=4) -- Cancelled
	BEGIN
		SET @XML = (
	SELECT TOP 1 
	       'LICENSECANCELLED' AS [Source],
	       @VersionName AS [BMCVersion],
	       '3002' AS ExceptionCode,
	       '000' AS OperatorId,
	       @temp AS [SubCode],
	       C.Company_Name AS Company,
	       ISNULL(SCR.Sub_Company_Region_Name, '') AS [Region],
	       ISNULL(SCA.Sub_Company_Area_Name, '') AS [Area],
	       S.Site_Code AS [SiteId],
	       S.Site_Name AS [SiteName],
	        --  S.Site_Code AS [Asset],
	       SL.StartDate AS [LicenseStartDate],
	       SL.ExpireDate AS [LicenseExpireDate],
	       SL.CancelledDateTime AS [LicenseCancelledDate],
	       STC.Staff_First_Name+' '+STC.Staff_Last_Name AS [CreatedStaffName],
	       STCan.Staff_First_Name+' '+STCan.Staff_Last_Name AS [CancelledStaffName],
	       SR.RuleName AS [RuleName],
	       convert(VARCHAR, @MessageDateTime , 120) AS [MessageDateTime]
	FROM   SL_LicenseInfo SL
	       INNER JOIN SL_Rules SR
	            ON SL.RuleID=Sr.RuleID
	       INNER JOIN Staff STC
				ON STC.Staff_ID = SL.CreatedStaffID
		   INNEr JOIN Staff STCan
				ON SL.CancelledStaffID=STCan.Staff_ID	
	       INNER JOIN Site S
				ON S.Site_ID=SL.Site_ID
    	   INNER JOIN SettingsProfileItems SPI
				ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
		   INNER JOIN dbo.SettingsMaster SM
				ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'

		   INNER JOIN Sub_Company SC
				ON S.Sub_Company_ID=SC.Sub_Company_ID					  
	       INNER JOIN dbo.Company C
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN Sub_Company_Region SCR
	            ON  SCR.SUB_COMPANY_REGION_ID = S.SUB_COMPANY_REGION_ID
	       LEFT JOIN Sub_Company_Area SCA
	            ON  SCA.SUB_COMPANY_AREA_ID = S.SUB_COMPANY_AREA_ID	      
				WHERE  SL.LicenseInfoID=@LicenseInfoID AND SL.ActivatedDateTime IS NOT NULL
	       
	       FOR XML PATH('') ,TYPE, ROOT('BMCRequest'))
	 
	 
	 IF @XML IS NULL 
	 RETURN
	 
	 INSERT INTO STM_Export_History ([Type],ClientID,Site_Code,[Message],Received_Date) VALUES
	 ('LICENSECANCELLED',1,@siteCode,@XML,GETDATE())
	END
	
	
	ELSE IF (@Type=2) --Activated
	BEGIN
	
	SET @XML = (
	SELECT TOP 1 
	       'LICENSEACTIVATED' AS [Source],
	       @VersionName AS [BMCVersion],
	       '3003' AS ExceptionCode, 
	       '000' AS OperatorId,
	       @temp AS [SubCode],
	       C.Company_Name AS Company,
	       ISNULL(SCR.Sub_Company_Region_Name, '') AS [Region],
	       ISNULL(SCA.Sub_Company_Area_Name, '') AS [Area],
	       S.Site_Code AS [SiteId],
	       S.Site_Name AS [SiteName],
	      --    S.Site_Code AS [Asset],
	       SL.StartDate AS [LicenseStartDate],
	       SL.ExpireDate AS [LicenseExpireDate],
	       SL.ActivatedDateTime AS [LicenseActivatedDate],
	       ST.Staff_First_Name+' '+ST.Staff_Last_Name AS [CreatedStaffName],
	       STA.Staff_First_Name+' '+STA.Staff_Last_Name AS [ActivatedStaffName],
	       SR.RuleName AS [RuleName],
	       convert(VARCHAR, @MessageDateTime , 120) AS [MessageDateTime]
	FROM   SL_LicenseInfo SL
	       INNER JOIN SL_Rules SR
	            ON SL.RuleID=Sr.RuleID
	       INNER JOIN Staff ST
				ON ST.Staff_ID = SL.CreatedStaffID
		   INNER JOIN Staff STA
		        ON STA.Staff_ID=SL.ActivatedStaffID
	       INNER JOIN Site S
				ON S.Site_ID=SL.Site_ID
		   INNER JOIN SettingsProfileItems SPI
				ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
		   INNER JOIN dbo.SettingsMaster SM
				ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'

		   INNER JOIN Sub_Company SC
				ON S.Sub_Company_ID=SC.Sub_Company_ID					  
	       INNER JOIN dbo.Company C
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN Sub_Company_Region SCR
	            ON  SCR.SUB_COMPANY_REGION_ID = S.SUB_COMPANY_REGION_ID
	       LEFT JOIN Sub_Company_Area SCA
	            ON  SCA.SUB_COMPANY_AREA_ID = S.SUB_COMPANY_AREA_ID
	       
	WHERE  SL.LicenseInfoID=@LicenseInfoID --AND SL.ActivatedDateTime IS NOT NULL
	       FOR XML PATH('') ,TYPE, ROOT('BMCRequest'))
	 
	 IF @XML IS NULL 
	 RETURN
	 
	 INSERT INTO STM_Export_History ([Type],ClientID,Site_Code,[Message],Received_Date) VALUES
	 ('LICENSEACTIVATED',1,@siteCode,@XML,GETDATE())
	 
	
	END
	
	
	ELSE IF (@Type=5)	-- Expiry alert using configuration
	BEGIN
	SELECT @ExpiryDate=[ExpireDate] FROM  SL_LicenseInfo WHERE LicenseInfoID=@LicenseInfoID
	SET @ExpiryDays=DATEDIFF(d,GETDATE(),@ExpiryDate)
	SET @XML = (
	SELECT TOP 1 
	       'LICENSEEXPIREDWARNING' AS [Source],
	       @VersionName AS [BMCVersion],
	       '3004' AS ExceptionCode, 
	       '000' AS OperatorId,
	       @temp AS [SubCode],
	       C.Company_Name AS Company,
	       ISNULL(SCR.Sub_Company_Region_Name, '') AS [Region],
	       ISNULL(SCA.Sub_Company_Area_Name, '') AS [Area],
	       S.Site_Code AS [SiteId],
	       S.Site_Name AS [SiteName],
	      --    S.Site_Code AS [Asset],
	       SL.StartDate AS [LicenseStartDate],
	       SL.ExpireDate AS [LicenseExpireDate],
	       @ExpiryDays AS [LicenseExpireInDays],
	       ST.Staff_First_Name+' '+ST.Staff_Last_Name AS [CreatedStaffName],
	       SR.RuleName AS [RuleName],
	       convert(VARCHAR, @MessageDateTime , 120) AS [MessageDateTime]
	FROM   SL_LicenseInfo SL
	       INNER JOIN SL_Rules SR
	            ON SL.RuleID=Sr.RuleID
	       INNER JOIN Staff ST
				ON ST.Staff_ID = SL.CreatedStaffID
	       INNER JOIN Site S
				ON S.Site_ID=SL.Site_ID
				 INNER JOIN SettingsProfileItems SPI
				ON  SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
		   INNER JOIN dbo.SettingsMaster SM
				ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'

		   INNER JOIN Sub_Company SC
				ON S.Sub_Company_ID=SC.Sub_Company_ID					  
	       INNER JOIN dbo.Company C
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN Sub_Company_Region SCR
	            ON  SCR.SUB_COMPANY_REGION_ID = S.SUB_COMPANY_REGION_ID
	       LEFT JOIN Sub_Company_Area SCA
	            ON  SCA.SUB_COMPANY_AREA_ID = S.SUB_COMPANY_AREA_ID
	       
	WHERE  SL.LicenseInfoID=@LicenseInfoID AND SL.ActivatedDateTime IS NOT NULL
	       FOR XML PATH('') ,TYPE, ROOT('BMCRequest'))
	       
	       
IF @XML IS NULL 
	 RETURN
	 
	 INSERT INTO STM_Export_History ([Type],ClientID,Site_Code,[Message],Received_Date) VALUES
	 ('LICENSEEXPIREDWARNING',1,@siteCode,@XML,GETDATE())
	
	
	END
END 

GO

