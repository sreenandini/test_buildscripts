USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDetailsForMeterAdjustment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDetailsForMeterAdjustment]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
 *	this stored procedure is to Get the list of sites based on User Access
 *
 *	Change History:
 *	
 *	Venkatesh Kumar.J		29/04/2014		
 *	
 *  
 *  
 * EXEC [rsp_GetSiteDetailsForMeterAdjustment]  NULL,NULL,1

*/  
CREATE PROCEDURE [dbo].[rsp_GetSiteDetailsForMeterAdjustment] 
(@Site_Name VARCHAR(100) = NULL, @Site_Status INT = NULL, @UserID INT)
AS
	SET NOCOUNT ON  
  
DECLARE @SettingValue VARCHAR(10)  
 SET @SettingValue = 'False'  
 
 SELECT @SettingValue = Setting_Value  
 FROM   Setting   
 WHERE  Setting_Name = 'VIEWPARTIALLYCONFIGUREDSITES'    
 
 IF ISNULL(@Site_Name, '') = ''  
    AND ISNULL(@Site_Status, -1) = -1  
 BEGIN  
     SELECT SITE.Site_ID,  
            SITE.Site_Name + ' [' + Site_Code + ']' AS Site_Name,  
            ISNULL(Site_Status, '') AS Site_Status,  
            WEBUrl,  
            Site_code,  
            CASE   
                 WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                      -1  
                 ELSE ISNULL(Site_Status_ID, 0)  
            END AS Site_Status_ID,  
            ISNULL(SettingsProfile_Description, 'Default Profile') AS   
            SettingsProfile_Description,
            sc.Site_DB_ConnectionString  
          FROM     
		SecurityProfile A  
        INNER JOIN SITE
             ON  A.SecurityProfileType_Value = SITE.Site_ID  
             AND AllowUser = 1  
             AND A.SecurityProfileType_ID = 2  
		LEFT JOIN settingsProfile sp           
             ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID  
        LEFT JOIN SiteDBConnectionStrings sc   
			 ON sc.Site_ID =SITE.Site_ID 
        INNER JOIN Staff_Customer_Access C  
			 ON  A.Customer_Access_ID = C.Customer_Access_ID  
        INNER JOIN STAFF D  
             ON  D.staff_id = C.staff_id        
        INNER JOIN [USER] U  
             ON  U.SecurityUserID = D.UserTableID  
        INNER JOIN [UserRole_lnk] URL  
             ON  URL.SecurityUserID = U.SecurityUserID  
        INNER JOIN [ROLE] R  
             ON  R.SecurityRoleID = URL.SecurityRoleID  
        LEFT JOIN VW_Enterprise_usersite_lnk uSL  
             ON  uSL.SecurityUserID = U.SecurityUserID  AND uSL.SiteID = Site.Site_ID
      WHERE ((@SettingValue = 'False' AND SiteStatus in ('FULLYCONFIGURED')) OR  @SettingValue = 'True')   
			AND ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @UserID )
					OR  (  
							R.RoleName <> 'SUPER USER'  
							AND uSL.SecurityUserID = @UserID
							AND usL.SiteID = SITE.Site_ID  
						))
		ORDER BY    
            site_name 
 END  
 ELSE   
 IF ISNULL(@Site_Name, '') = ''  
    AND @Site_Status IS NOT NULL  
 BEGIN  
    
     SELECT SITE.Site_ID,  
            CASE   
                 WHEN Site_Code IS NULL THEN SITE.Site_Name  
                 ELSE SITE.Site_Name + ' [' + Site_Code + ']'  
            END AS Site_Name,  
            ISNULL(Site_Status, '') AS Site_Status,  
            WEBUrl,  
            Site_code,  
            CASE   
                 WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                      -1  
                 ELSE ISNULL(Site_Status_ID, 0)  
            END AS Site_Status_ID,  
            ISNULL(SettingsProfile_Description, 'Default Profile') AS   
            SettingsProfile_Description,
            sc.Site_DB_ConnectionString    
      FROM  
        SecurityProfile A  
        INNER JOIN SITE
             ON  A.SecurityProfileType_Value = SITE.Site_ID  
             AND AllowUser = 1  
             AND A.SecurityProfileType_ID = 2  
		LEFT JOIN settingsProfile sp           
             ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID  
        LEFT JOIN SiteDBConnectionStrings sc   
			 ON sc.Site_ID =SITE.Site_ID 
        INNER JOIN Staff_Customer_Access C  
			 ON  A.Customer_Access_ID = C.Customer_Access_ID  
        INNER JOIN STAFF D  
             ON  D.staff_id = C.staff_id        
        INNER JOIN [USER] U  
             ON  U.SecurityUserID = D.UserTableID  
        INNER JOIN [UserRole_lnk] URL  
             ON  URL.SecurityUserID = U.SecurityUserID  
        INNER JOIN [ROLE] R  
             ON  R.SecurityRoleID = URL.SecurityRoleID  
        LEFT JOIN VW_Enterprise_usersite_lnk uSL  
             ON  uSL.SecurityUserID = U.SecurityUserID  AND uSL.SiteID = Site.Site_ID
     WHERE  (    
                ISNULL(Site_Status_ID, 0) = @Site_Status    
            )
            AND ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @UserID )
					OR  (  
							R.RoleName <> 'SUPER USER'  
							AND  uSL.SecurityUserID = @UserID
							AND usL.SiteID = SITE.Site_ID  
						))      
     ORDER BY    
            site_name     
 END  
 ELSE  
 BEGIN  
     DECLARE @Name VARCHAR(100)  
     SET @Name = @Site_Name  
       
     SET @Site_Name = RTRIM(  
             SUBSTRING(  
                 @Site_Name,  
                 0,  
                 CASE   
                      WHEN CHARINDEX('[', @Site_Name) > 0 THEN CHARINDEX('[', @Site_Name)  
                      ELSE LEN(@Site_Name) + 1  
                 END  
             )  
         )  
       
     DECLARE @Count INT  
     SELECT @Count = COUNT(*)  
     FROM   dbo.Site  
     WHERE  Site_Name = @Site_Name   
       
     IF (ISNULL(@Count, 0) > 1)  
     BEGIN  
         SET @Site_Name = LTRIM(RTRIM(SUBSTRING(@Name, CHARINDEX('[', @Name) + 1, 4)))  
           
         SELECT SITE.Site_ID,  
                SITE.Site_Name,  
                ISNULL(Site_Status, '') AS Site_Status,  
                WEBUrl,  
                Site_code,  
                CASE   
                     WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                          -1  
                     ELSE ISNULL(Site_Status_ID, 0)  
                END AS Site_Status_ID,  
                ISNULL(SettingsProfile_Description, 'Default Profile') AS   
                SettingsProfile_Description,
                  sc.Site_DB_ConnectionString    
                  FROM   
		SecurityProfile A  
        INNER JOIN SITE
             ON  A.SecurityProfileType_Value = SITE.Site_ID  
             AND AllowUser = 1  
             AND A.SecurityProfileType_ID = 2  
		LEFT JOIN settingsProfile sp           
             ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID  
        LEFT JOIN SiteDBConnectionStrings sc   
			 ON sc.Site_ID =SITE.Site_ID 
        INNER JOIN Staff_Customer_Access C  
			 ON  A.Customer_Access_ID = C.Customer_Access_ID  
        INNER JOIN STAFF D  
             ON  D.staff_id = C.staff_id        
        INNER JOIN [USER] U  
             ON  U.SecurityUserID = D.UserTableID  
        INNER JOIN [UserRole_lnk] URL  
             ON  URL.SecurityUserID = U.SecurityUserID  
        INNER JOIN [ROLE] R  
             ON  R.SecurityRoleID = URL.SecurityRoleID  
        LEFT JOIN VW_Enterprise_usersite_lnk uSL  
             ON  uSL.SecurityUserID = U.SecurityUserID  AND uSL.SiteID = Site.Site_ID
         WHERE  site_code = @Site_Name    
                AND (    
                        @Site_Status IS NULL    
                        OR (    
                               ISNULL(Site_Status_ID, 0) = @Site_Status    
                               OR ISNULL(Site_Inactive_Date,GETDATE()) > GETDATE()    
                           )    
                    )
                AND ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @UserID )  
				OR  (  
						R.RoleName <> 'SUPER USER'  
						AND uSL.SecurityUserID = @UserID
						AND usL.SiteID = SITE.Site_ID  
					))  
     END  
     ELSE  
     BEGIN  
         SELECT SITE.Site_ID,  
                SITE.Site_Name,  
                ISNULL(Site_Status, '') AS Site_Status,  
                WEBUrl,  
                Site_code,  
                CASE   
                     WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                          -1  
                     ELSE ISNULL(Site_Status_ID, 0)  
                END AS Site_Status_ID,  
                ISNULL(SettingsProfile_Description, 'Default Profile') AS   
                SettingsProfile_Description  ,
                  sc.Site_DB_ConnectionString  
		FROM   
		SecurityProfile A  
        INNER JOIN SITE
             ON  A.SecurityProfileType_Value = SITE.Site_ID  
             AND AllowUser = 1  
             AND A.SecurityProfileType_ID = 2  
		LEFT JOIN settingsProfile sp           
             ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID  
        LEFT JOIN SiteDBConnectionStrings sc   
			 ON sc.Site_ID =SITE.Site_ID 
        INNER JOIN Staff_Customer_Access C  
			 ON  A.Customer_Access_ID = C.Customer_Access_ID  
        INNER JOIN STAFF D  
             ON  D.staff_id = C.staff_id        
        INNER JOIN [USER] U  
             ON  U.SecurityUserID = D.UserTableID  
        INNER JOIN [UserRole_lnk] URL  
             ON  URL.SecurityUserID = U.SecurityUserID  
        INNER JOIN [ROLE] R  
             ON  R.SecurityRoleID = URL.SecurityRoleID  
        LEFT JOIN VW_Enterprise_usersite_lnk uSL  
             ON  uSL.SecurityUserID = U.SecurityUserID  AND uSL.SiteID = Site.Site_ID  
         WHERE  SITE.site_Name = @Site_Name    
                AND (    
                        @Site_Status IS NULL    
                        OR (    
                               ISNULL(Site_Status_ID, 0) = @Site_Status    
                                 OR ISNULL(Site_Inactive_Date,GETDATE()) > GETDATE()    
                           )    
                    ) 
				AND  ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @UserID )  
				OR  (  
						R.RoleName <> 'SUPER USER'  
						AND uSL.SecurityUserID = @UserID 
						AND usL.SiteID = SITE.Site_ID  
					))
     END  
 END  

GO

