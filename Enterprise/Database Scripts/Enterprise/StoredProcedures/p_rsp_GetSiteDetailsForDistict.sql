
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDetailsForDistrict]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].rsp_GetSiteDetailsForDistrict
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--rsp_GetSiteDetailsForDistrict 2,2,5,2,2,1
CREATE PROCEDURE [dbo].rsp_GetSiteDetailsForDistrict   
  (  
	  @company      INT = 0,
	  @subcompany  INT = 0,
	  @region         INT = 0,        
      @area           INT = 0,
      @district       INT = 0,    
      @Userid         INT = null  
  )  
  AS  
  BEGIN  
   IF @district = 0
		SET @district = NULL
   IF @area = 0  
       SET @area = NULL      
     
   IF @region = 0  
       SET @region = NULL  
     
   IF @subcompany = 0  
       SET @subcompany = NULL  
     
   IF @company = 0  
       SET @company = NULL    
     
 IF    @district > 0 AND  @region > 0 AND @area > 0 AND @subcompany > 0
 BEGIN
 SELECT DISTINCT
 B.Site_ID,B.Site_Code,B.Site_Name,B.Region   
 FROM Site B   
 INNER JOIN SecurityProfile   A ON A.SecurityProfileType_Value = B.Site_ID AND AllowUser = 1 AND A.SecurityProfileType_ID = 2  
 LEFT JOIN  Staff_Customer_Access CA ON A.Customer_Access_ID =CA.Customer_Access_ID  
 LEFT JOIN STAFF D ON D.staff_id=CA.staff_id      
 
 INNER JOIN [USER] U
	            ON  U.SecurityUserID = D.UserTableID
	       INNER JOIN [UserRole_lnk] URL
	            ON  URL.SecurityUserID = U.SecurityUserID
	       INNER JOIN [ROLE] R
	            ON  R.SecurityRoleID = URL.SecurityRoleID
	       LEFT JOIN VW_Enterprise_usersite_lnk uSL
	            ON  uSL.SecurityUserID = U.SecurityUserID	  
 
 INNER JOIN   Sub_Company_District scd  ON scd.Sub_Company_District_ID = B.Sub_Company_District_ID 
 INNER JOIN dbo.Sub_Company_Area sca  ON  scd.Sub_Company_Area_ID = B.Sub_Company_Area_ID  
 INNER JOIN dbo.Sub_Company_Region scr  ON  sca.Sub_Company_Region_ID = B.Sub_Company_Region_ID  
 INNER JOIN dbo.Sub_Company sc  ON  sc.sub_company_id = B.Sub_Company_ID  
 INNER JOIN Company c  ON  c.company_id = sc.Company_ID  
 WHERE  B.sub_company_id = ISNULL(@subcompany, sc.Sub_Company_ID)  
          AND B.Sub_Company_Area_ID = ISNULL(@area, sca.Sub_Company_Area_ID)  
          AND B.Sub_Company_Region_ID = ISNULL(@region, scr.Sub_Company_Region_ID)  
          AND c.company_id = ISNULL(@company, c.company_id)  
          AND B.Sub_Company_District_ID = ISNULL(@district, scd.Sub_Company_District_ID)
          AND ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @Userid)
	       OR  (
	               R.RoleName <> 'SUPER USER'
	               AND uSL.SecurityUserID = @Userid
	               AND usL.SiteID = B.Site_ID
	           )) 
          --AND ISNULL(USERTABLEID,0)=@Userid 
   ORDER BY  
          Site_Name  
 END
 ELSE    
     BEGIN
			 SELECT DISTINCT
		 B.Site_ID,B.Site_Code,B.Site_Name,B.Region   
		 FROM Site B   
		 INNER JOIN SecurityProfile   A ON A.SecurityProfileType_Value = B.Site_ID AND AllowUser = 1 AND A.SecurityProfileType_ID = 2  
		 LEFT JOIN  Staff_Customer_Access CA ON A.Customer_Access_ID =CA.Customer_Access_ID  
		 LEFT JOIN STAFF D ON D.staff_id=CA.staff_id      		
		 
		 INNER JOIN [USER] U
	            ON  U.SecurityUserID = D.UserTableID
	       INNER JOIN [UserRole_lnk] URL
	            ON  URL.SecurityUserID = U.SecurityUserID
	       INNER JOIN [ROLE] R
	            ON  R.SecurityRoleID = URL.SecurityRoleID
	       LEFT JOIN VW_Enterprise_usersite_lnk uSL
	            ON  uSL.SecurityUserID = U.SecurityUserID
		  
		 INNER JOIN dbo.Sub_Company sc  ON  sc.sub_company_id = B.Sub_Company_ID  
		 INNER JOIN Company c  ON  c.company_id = sc.Company_ID  
		 WHERE  B.sub_company_id = ISNULL(@subcompany, sc.Sub_Company_ID)  
				  AND c.company_id = ISNULL(@company, c.company_id)
				  AND ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @Userid)
	       OR  (
	               R.RoleName <> 'SUPER USER'
	               AND uSL.SecurityUserID = @Userid
	               AND usL.SiteID = B.Site_ID
	           ))   				  
				  --AND ISNULL(USERTABLEID,0)=@Userid 
   ORDER BY  
          Site_Name  
     END
          
  END  