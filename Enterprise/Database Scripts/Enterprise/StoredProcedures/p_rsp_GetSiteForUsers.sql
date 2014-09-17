USE ENTERPRISE
GO
 
IF EXISTS (
       SELECT *
       FROM   DBO.SYSOBJECTS
       WHERE  id = OBJECT_ID(N'[dbo].[rsp_GetSiteForUsers]')
              AND OBJECTPROPERTY(id, N'IsProcedure') = 1
   )
BEGIN
    DROP PROCEDURE [dbo].[rsp_GetSiteForUsers]
END
GO

CREATE PROCEDURE rsp_GetSiteForUsers
AS
BEGIN
	DECLARE @SettingValue VARCHAR(10)
	SET @SettingValue = 'False'
	
	SELECT @SettingValue = s.Setting_Value
	FROM   Setting s
	WHERE  s.Setting_Name = 'VIEWPARTIALLYCONFIGUREDSITES'
	
	IF (@SettingValue = 'True')
	BEGIN
	    SELECT s.Site_ID,
	           s.Site_Name
	    FROM   [Site] s WITH(NOLOCK)
	    WHERE  s.SiteStatus IN ('FULLYCONFIGURED','PARTIALLYCONFIGURED')
	           AND Site_Inactive_Date IS NULL
	           OR  s.Site_Inactive_Date > GETDATE()
	    ORDER BY
	           s.Site_Name
	END
	ELSE
	BEGIN
	    SELECT s.Site_ID,
	           s.Site_Name
	    FROM   [Site] s WITH(NOLOCK)
	    WHERE  s.SiteStatus = 'FULLYCONFIGURED'
	           AND Site_Inactive_Date IS NULL
	           OR  s.Site_Inactive_Date > GETDATE()
	    ORDER BY
	           s.Site_Name
	END
END
GO