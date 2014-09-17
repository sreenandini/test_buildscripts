USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllSiteForVerification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllSiteForVerification]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--------------------------------------------------------------------------  
--------------------------------------------------------------------------   
---  
--- Description: Get Get All Sit eFor Verification.  
---  
--- Inputs:      see inputs  
---  
--- Outputs:       
---   
--- =======================================================================  
---   
--- Revision History  
---   
--- Senthil  07/06/10     Created   
---------------------------------------------------------------------------   
CREATE PROCEDURE dbo.rsp_GetAllSiteForVerification  
AS  
BEGIN  
  
 SELECT DISTINCT SITE_ID As 'Site ID',Site_Name As 'Site Name'
 FROM   dbo.Site
END  
  

GO

