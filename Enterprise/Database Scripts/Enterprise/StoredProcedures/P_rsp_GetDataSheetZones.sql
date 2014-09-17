GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDataSheetZones]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDataSheetZones]
GO
-- =======================================================================
-- Revision History      
-- Rajkumar R 22/08/2013  Created      
-- =======================================================================   
CREATE PROCEDURE [dbo].rsp_GetDataSheetZones 
(
	@CompanyId INT=0,
	@SubCompanyId INT=0,
	@site INT = 0  
)  
AS  
BEGIN  
  
IF @site = 0            SET @site = NULL   
IF @SubCompanyId = 0    SET @SubCompanyId = NULL  
IF @CompanyId = 0       SET @CompanyId = NULL  
  
 SELECT Zone_ID,Zone_Name
 FROM Zone  zc
 INNER JOIN [dbo].[Site] SC ON SC.Site_ID = zc.Site_ID   
 INNER JOIN [dbo].[Sub_Company] SCB ON SCB.[Sub_Company_ID] = SC.[Sub_Company_ID]
 INNER JOIN [dbo].[Company] C ON SCB.[Company_ID] =C.Company_ID
 WHERE
 zc.zone_name IS NOT NULL AND  zone_name <> ''  
 AND   
 ( ( @site IS NULL )OR( @site IS NOT NULL AND zc.Site_ID= @site ))  
 AND 
  SC.Sub_Company_ID=ISNULL(@SubCompanyId ,SC.Sub_Company_ID)
 AND
 C.Company_ID=ISNULL(@CompanyId,C.Company_ID)
 ORDER BY zc.Zone_Name  
  
END  