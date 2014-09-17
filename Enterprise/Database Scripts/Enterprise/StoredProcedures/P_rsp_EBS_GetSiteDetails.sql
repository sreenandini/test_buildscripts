/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/11/2014 8:45:30 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_EBS_GetSiteDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_EBS_GetSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------    
---    
--- Description: Fetches the details from Site table
---        
--- Inputs:         
--- Outputs:     
--- ======================================================================================================================    
---     
--- Revision History    
---     
--- Sudarsan S     10/11/08     Created     
--- Selva Kumar S	27th Aug '13 Modified
------------------------------------------------------------------------------------------------------------------------    
-- exec [rsp_EBS_GetSiteDetails] '1005'
CREATE PROCEDURE [dbo].[rsp_EBS_GetSiteDetails]
(@SiteCode VARCHAR(50) = NULL)
AS
BEGIN
	SELECT s.Site_Code AS SiteId,
	       s.Site_Code AS SiteCode,
	       s.[Site_Name] AS SiteName,
	       CAST(
	           (CASE s.SiteStatus WHEN 'FULLYCONFIGURED' THEN 1 ELSE 0 END) AS 
	           BIT
	       ) AS IsActive
	FROM   dbo.[Site] s
	WHERE  s.Site_Code = COALESCE(@SiteCode, s.Site_Code)
END
GO

