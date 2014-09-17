USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSiteDetailsGuardian]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSiteDetailsGuardian]
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
Select * from site
 * exec  [rsp_GetSiteDetailsGuardian] 'New site [1300]'
exec rsp_GetSiteDetailsGuardian @Site_Name='',@Site_Status=0,@SecurityUserID=0,@Status=0,@StatusInterval=0
*/  
CREATE PROCEDURE [dbo].[rsp_GetSiteDetailsGuardian]
(
    @Site_Name       VARCHAR(100) = NULL,
    @Site_Status     INT = NULL,-- 1 Means active sites alone, 0 means all sites
    @SecurityUserID  INT = 0, --0 for ALL
    @Status INT = 0, -- 0 for all, 1 for Running, 2 for Stopped,3 for unknown,4 for Terminated  
    @StatusInterval INT = 10
)
AS
BEGIN
	SET NOCOUNT ON  
	
	DECLARE @SettingValue VARCHAR(10)  
	SET @SettingValue = 'False'  
	DECLARE @temp VARCHAR(150)  
	SET @temp =''
	
	SELECT @SettingValue = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'VIEWPARTIALLYCONFIGUREDSITES'    
	
	SET @SettingValue = UPPER(LTRIM(RTRIM(@SettingValue)))
	SET @Site_Name = ISNULL(@Site_Name,'') 
	SET @Site_Name = LTRIM(RTRIM(SUBSTRING(@Site_Name, CHARINDEX('[', @Site_Name) + 1, 4)))
	
	SELECT SITE_ID,
				CASE 
	                WHEN Site_Code IS NULL THEN Site_Name
	                ELSE Site_Name + ' [' + Site_Code + ']'
	           END AS Site_Name,
	               ISNULL(Site_Status, '') AS Site_Status,
	               WEBUrl,
	               Site_code,
	               CASE 
						WHEN(Site_Closed = 1) THEN
							-1
	                    WHEN (Site_Inactive_Date IS NOT NULL) THEN 
	                         1
	                    ELSE ISNULL(Site_Status_ID, 0)
	               END AS Site_Status_ID,
	               ISNULL(SettingsProfile_Description, 'Default Profile') AS 
	               SettingsProfile_Description,
	               ISNULL(HourlyNotRun,@temp) AS HourlyNotRun
	               FROM SITE SI LEFT JOIN  settingsProfile sp
	                    ON  SI.Site_Setting_Profile_ID = sp.SettingsProfile_ID
	                    WHERE SI.Site_ID IN (
	SELECT DISTINCT Site_ID
	        FROM   SITE S
	               INNER JOIN UserSite_lnk lnk
	                    ON  S.Site_ID = lnk.SiteID
	               INNER JOIN [USER] U
	                    ON  lnk.SecurityUserID = U.SecurityUserID
	                    AND (@SecurityUserID = 0 OR U.SecurityUserID = @SecurityUserID)
	        WHERE  
	        (ISNULL(@Site_Name, '') = '' OR  site_Code = @Site_Name)
	         AND (
	                       @Site_Status IS NULL
	                       OR (
	                              (ISNULL(@Site_Status,0) = 0 OR ISNULL(Site_Status_ID, 0) = 0)
	                              OR Site_Inactive_Date IS NULL
	                          )
	                   )
	                   AND
	            (
					@SettingValue = 'TRUE' 
					OR
	               (
	                   @SettingValue = 'FALSE'
	                   AND SiteStatus IN ('FULLYCONFIGURED')
	               )
	           )
	           AND
	           
	           (
	           @Status = 0 
	           OR  
	           (
	            @Status = 1 AND Site_Inactive_Date IS NULL
	            AND ISNULL(Site_Status_ID, 0) = 0 AND
	            ISNULL(Site_Closed, 0) = 0 AND
	            (
					DATEDIFF(SECOND, ISNULL(Site_Status.value('(/Site//Status//DateTime/node())[1]', 'Datetime'),GetDate()), GETDATE()) != 0) AND
					(DATEDIFF(MI, ISNULL(Site_Status.value('(/Site//Status//DateTime/node())[1]', 'Datetime'),GetDate()), GETDATE()) BETWEEN 0 and 10)
	           )
	           OR
	           (
				 @Status = 2 AND ISNULL(Site_Closed, 0) <> 1 AND (Site_Inactive_Date IS NOT NULL OR Site_Status IS NULL OR
	           (DATEDIFF(SECOND, ISNULL(Site_Status.value('(/Site//Status//DateTime/node())[1]', 'Datetime'),GetDate()), GETDATE()) != 0) AND
					(DATEDIFF(MI, ISNULL(Site_Status.value('(/Site//Status//DateTime/node())[1]', 'Datetime'),GetDate()), GETDATE()) BETWEEN 0 and 10))
	           )
	           OR
	           (
	           @Status = 3 AND Site_Inactive_Date IS NULL AND  ISNULL(Site_Status_ID, 0) = 0 AND ISNULL(Site_Closed, 0) = 0 
	           AND (DATEDIFF(MI, ISNULL(Site_Status.value('(/Site//Status//DateTime/node())[1]', 'Datetime'),GetDate()), GETDATE()) >= 10
	           OR DATEDIFF(SECOND, ISNULL(Site_Status.value('(/Site//Status//DateTime/node())[1]', 'Datetime'),GetDate()), GETDATE()) = 0)
	           )
	           OR
	           (
				 @Status = 4 AND ISNULL(Site_Closed, 0) = 1
		       )
		)
		)
		ORDER BY Site_Name	           
	           
	END
GO
