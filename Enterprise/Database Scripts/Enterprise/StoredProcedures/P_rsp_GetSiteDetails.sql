USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
 *	this stored procedure is to Get the list of sittes
 *
 *	Change History:
 *	
 *	Madhu		20-08-2007		created
 *	Madhu		2-4-2009		Modified to get site Status as parameter
 *  Yoganandh	26-10-2010		Modified - Do not fetch Site if the Site is Inactive
 *  
 * exec  [rsp_GetSiteDetails] 'New site [1300]'

*/  
CREATE PROCEDURE [dbo].[rsp_GetSiteDetails] 
(@Site_Name VARCHAR(100) = NULL, @Site_Status INT = NULL)
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
            Site_Name + ' [' + Site_Code + ']' AS Site_Name,  
            ISNULL(Site_Status, '') AS Site_Status,  
            WEBUrl,  
            Site_code,  
            CASE   
                 WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                      -1  
                 ELSE ISNULL(Site_Status_ID, 0)  
            END AS Site_Status_ID,  
            ISNULL(SettingsProfile_Description, 'UnAssigned') AS   
            SettingsProfile_Description,
            sc.Site_DB_ConnectionString  
     FROM   SITE  
            LEFT JOIN settingsProfile sp         
                 ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID
            LEFT JOIN SiteDBConnectionStrings sc 
				 ON	sc.Site_ID =SITE.Site_ID  
						WHERE ((@SettingValue = 'False' AND SiteStatus in ('FULLYCONFIGURED')) OR  @SettingValue = 'True') 
                     -- WHERE (@Site_Status IS NULL  
                     -- AND (Site_Inactive_Date IS NULL OR Site_Inactive_Date > GETDATE())  
                     -- OR (ISNULL(Site_Status_ID,0) = @Site_Status OR Site_Inactive_Date > GETDATE()))  
     ORDER BY  
            site_name  
 END  
 ELSE   
 IF ISNULL(@Site_Name, '') = ''  
    AND @Site_Status IS NOT NULL  
 BEGIN  
    
     SELECT SITE.Site_ID,  
            CASE   
                 WHEN Site_Code IS NULL THEN Site_Name  
                 ELSE Site_Name + ' [' + Site_Code + ']'  
            END AS Site_Name,  
            ISNULL(Site_Status, '') AS Site_Status,  
            WEBUrl,  
            Site_code,  
            CASE   
                 WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                      -1  
                 ELSE ISNULL(Site_Status_ID, 0)  
            END AS Site_Status_ID,  
            ISNULL(SettingsProfile_Description, 'UnAssigned') AS   
            SettingsProfile_Description,
            sc.Site_DB_ConnectionString    
     FROM   SITE  
            LEFT JOIN settingsProfile sp  
                 ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID 
           LEFT JOIN SiteDBConnectionStrings sc 
				 ON	sc.Site_ID =SITE.Site_ID   
     WHERE  (  
                ISNULL(Site_Status_ID, 0) = @Site_Status  
--                OR ISNULL(Site_Inactive_Date,GETDATE()) > GETDATE()  
            )  
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
                Site_Name,  
                ISNULL(Site_Status, '') AS Site_Status,  
                WEBUrl,  
                Site_code,  
                CASE   
                     WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                          -1  
                     ELSE ISNULL(Site_Status_ID, 0)  
                END AS Site_Status_ID,  
                ISNULL(SettingsProfile_Description, 'UnAssigned') AS   
                SettingsProfile_Description,
                  sc.Site_DB_ConnectionString    
         FROM   SITE  
                LEFT JOIN settingsProfile sp  
                     ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID
               LEFT JOIN SiteDBConnectionStrings sc 
				 ON	sc.Site_ID =SITE.Site_ID    
         WHERE  site_code = @Site_Name  
                AND (  
                        @Site_Status IS NULL  
                        OR (  
                               ISNULL(Site_Status_ID, 0) = @Site_Status  
                               OR ISNULL(Site_Inactive_Date,GETDATE()) > GETDATE()  
                           )  
                    )  
     END  
     ELSE  
     BEGIN  
         SELECT SITE.Site_ID,  
                Site_Name,  
                ISNULL(Site_Status, '') AS Site_Status,  
                WEBUrl,  
                Site_code,  
                CASE   
                     WHEN (ISNULL(Site_Inactive_Date, GETDATE()) < GETDATE()) THEN   
                          -1  
                     ELSE ISNULL(Site_Status_ID, 0)  
                END AS Site_Status_ID,  
                ISNULL(SettingsProfile_Description, 'UnAssigned') AS   
                SettingsProfile_Description  ,
                  sc.Site_DB_ConnectionString  
         FROM   SITE  
                LEFT JOIN settingsProfile sp  
                     ON  SITE.Site_Setting_Profile_ID = sp.SettingsProfile_ID 
                LEFT JOIN SiteDBConnectionStrings sc 
				 ON	sc.Site_ID =SITE.Site_ID   
         WHERE  site_Name = @Site_Name  
                AND (  
                        @Site_Status IS NULL  
                        OR (  
                               ISNULL(Site_Status_ID, 0) = @Site_Status  
                                 OR ISNULL(Site_Inactive_Date,GETDATE()) > GETDATE()  
                           )  
                    )  
     END  
 END  

GO

