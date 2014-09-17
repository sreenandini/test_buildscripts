USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSite_Code_Name]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSite_Code_Name]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***
Version History
---------------------------------------
Kirubakar	Created		03 Jun 2010 20:35:45
---------------------------------------
***/
CREATE PROCEDURE rsp_GetSite_Code_Name    
@SiteID int    
AS    
BEGIN    
SELECT Site_Code,Site_Name     
 FROM SITE    
 WHERE SITE_ID=@SiteID    
END

GO

