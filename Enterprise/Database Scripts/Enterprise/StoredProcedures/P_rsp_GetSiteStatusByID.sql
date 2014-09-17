/************************************************************  
 * Code formatted by SoftTree SQL Assistant © v4.8.29  
 * Time: 03/02/2013 5:42:43 PM  
 ************************************************************/  
USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteStatusByID]    Script Date: 01/31/2013 06:54:34 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetSiteStatusByID]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetSiteStatusByID]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetSiteStatusByID]    Script Date: 01/31/2013 06:54:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO 
CREATE PROCEDURE rsp_GetSiteStatusByID  
 @siteID INT = 0  
AS  
BEGIN  
 SELECT Site_ID,  
        Site_Status  
 FROM   SITE  
 WHERE  site_id = @siteID  
        OR  @siteID = 0  
END 