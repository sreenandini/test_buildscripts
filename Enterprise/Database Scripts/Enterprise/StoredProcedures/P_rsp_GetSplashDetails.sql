USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSplashDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSplashDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
   
/*    
 * this stored procedure is to fetch the setting details  
 *    
 * Change History:   
 *     
 * GBabu  22-03-2011  created     
*/    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
CREATE PROCEDURE [dbo].[rsp_GetSplashDetails]   
AS  
BEGIN  
BEGIN TRY
	SET NOCOUNT ON 

	SELECT [COPYRIGTINFO] AS COPYRIGTINFO,[PRODUCTVERSION] AS PRODUCTVERSION,
	[PRODUCTDESC] AS PRODUCTDESC, [COMPANYNAME] AS COMPANYNAME, 
	[PRODUCTNAME] AS PRODUCTNAME
	FROM 
	(SELECT SettingsProfileItems_SettingsMaster_Values ,SettingsMaster_Name
	FROM SettingsProfileItems INNER JOIN SettingsMaster ON 
	SettingsProfileItems_SettingsMaster_ID = SettingsMaster_ID WHERE 
	SettingsMaster_Name IN ('COPYRIGTINFO', 'PRODUCTVERSION', 'PRODUCTDESC', 'COMPANYNAME',  'PRODUCTNAME') ) p
	PIVOT
	(
	MAX([SettingsProfileItems_SettingsMaster_Values])
	FOR SettingsMaster_Name IN
	([COPYRIGTINFO],
	[PRODUCTVERSION],
	[PRODUCTDESC],
	[COMPANYNAME],
	[PRODUCTNAME])
	) AS pvt 

END TRY
BEGIN CATCH
	RETURN -1
END CATCH
END



GO

