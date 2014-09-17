USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetZones_BySubCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetZones_BySubCompany]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- OUTPUT --Get Company details -- exec rsp_GetZones_BySubCompany 0,0       
-- Revision History    
-- 
-- ======================================================================= 
CREATE PROCEDURE [dbo].rsp_GetZones_BySubCompany
(@site INT = 0, @subCompany INT = 0,@Company INT=0)  
AS
BEGIN
	IF @site = 0  
     SET @site = NULL  
   
 IF @subCompany = 0  
     SET @subCompany = NULL  
   
   IF @Company = 0  
     SET @Company = NULL  
   
    
 SELECT Zone_ID,  
        Zone_Name  
 FROM   Zone Z(NOLOCK)  
 
 INNER JOIN Site S ON S.Site_ID = Z.Site_ID
 INNER JOIN Sub_Company SC ON SC.Sub_Company_ID = S.Sub_Company_ID
 INNER JOIN Company C ON C.Company_ID=SC.Company_ID
 
 WHERE  zone_name IS NOT NULL  
        AND zone_name <> ''  
        --AND Z.Site_ID IN (ISNULL(@site,S.Site_Id))
        --AND S.Sub_Company_ID IN (ISNULL(@subCompany,SC.Sub_Company_ID))
        AND
        (
          @site  IS NULL
            OR (@site <> 0 AND Z.Site_ID = @site)  
        )
       AND
       (  
            @subCompany is null 
            OR (@subCompany <> 0 AND S.Sub_Company_ID = @subCompany)  
        )  
        AND (  
                @Company is null
                OR (@Company <> 0 AND C.Company_ID = @Company)  
            )  
       ORDER BY  
        Zone_Name  
END

GO

