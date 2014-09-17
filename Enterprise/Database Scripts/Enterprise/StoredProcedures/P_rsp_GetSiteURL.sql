USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteURL]    Script Date: 01/30/2013 04:59:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteURL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteURL]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteURL]    Script Date: 01/30/2013 04:59:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 -- rsp_GetSettingFromSettingsMaster  
-- -----------------------------------------------------------------  
--   
-- To get site setting based on the sitecode and site profile  
--   
-- -----------------------------------------------------------------  
-- Revision History  
--   
-- 15/05/2012 Dinesh Rathinavel Created  
--   
-- =================================================================  
  
CREATE PROCEDURE [dbo].[rsp_GetSiteURL]  
 @Site_Id INT = 0  
AS  
BEGIN  
   
 SET NOCOUNT ON 
 DECLARE @CertificateIssuer VARCHAR(50),
 @IsCertificateRequired BIT 
 Select @IsCertificateRequired= (CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(Setting_value,0)))) = 'TRUE' THEN 1 ELSE 0 END) from setting Where Setting_Name = 'IsCertificateRequired'
 Select @CertificateIssuer=Setting_value from setting Where Setting_Name = 'CertificateIssuer'
 
 
 SELECT DISTINCT
  S.Site_ID,
  S.Site_Code,
  S.WebURL,
  @IsCertificateRequired IsCertificateRequired,
  @CertificateIssuer CertificateIssuer,
  SPI.[SettingsProfileItems_SettingsMaster_Values]  ReadTime
  --SPI.[SettingsProfileItems_SettingsMaster_Values]  
 FROM [dbo].[SettingsProfileItems] SPI  
  INNER JOIN [dbo].[SettingsMaster] SM ON SM.[SettingsMaster_ID] = SPI.[SettingsProfileItems_SettingsMaster_ID]  
  INNER JOIN [dbo].[SettingsProfile] SP ON SP.[SettingsProfile_ID] = SPI.[SettingsProfileItems_SettingsProfile_ID]  
  INNER JOIN [dbo].[Site] S ON S.[Site_Setting_Profile_ID] = SP.[SettingsProfile_ID]  
  WHERE UPPER(LTRIM(RTRIM(SM.[SettingsMaster_Name]))) = UPPER(LTRIM(RTRIM('DailyAutoReadTime')))  
     AND (S.[Site_Id] = @Site_Id  OR @Site_Id = 0)  AND S.[Site_Closed] = 0 AND ISNULL(S.WebURL,'')<>''
  
END  

GO


