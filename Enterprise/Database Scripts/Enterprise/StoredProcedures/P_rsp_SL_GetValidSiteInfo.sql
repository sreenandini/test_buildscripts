USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SL_GetValidSiteInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SL_GetValidSiteInfo] 
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_SL_GetValidSiteInfo]
	@SubCompanyId INT = 0,
	@UserId       INT = 0
AS  
BEGIN
	SET NOCOUNT ON;
	
		SELECT DISTINCT
				S.[Site_ID],
				S.[Site_Name]
	FROM [dbo].[Site] S
	INNER JOIN [dbo].[Sub_Company] SC ON SC.[Sub_Company_ID] = S.[Sub_Company_ID]
	INNER JOIN [dbo].[Company] C ON SC.[Company_ID] = C.[Company_ID]
	INNER JOIN SecurityProfile SP ON  
	SP.SecurityProfileType_Value = S.Site_ID AND AllowUser = 1 AND SecurityProfileType_ID = 2
INNER JOIN STAFF_CUSTOMER_ACCESS SCA ON  
SP.Customer_Access_ID = SCA. Customer_Access_ID AND (@UserId = 0 OR SCA.Staff_ID = @UserId)
INNER JOIN SettingsProfileItems SPI ON  
SPI.SettingsProfileItems_SettingsProfile_ID = S.Site_Setting_Profile_ID 
AND UPPER(LTRIM(RTRIM(SPI.[SettingsProfileItems_SettingsMaster_Values])))='TRUE'
INNER JOIN dbo.SettingsMaster SM ON  
SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID AND 
UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name])))='ISSITELICENSINGENABLED'
WHERE (@SubCompanyId = 0 OR SC.[Sub_Company_ID] = @SubCompanyId)
ORDER BY S.Site_Name
END

GO
