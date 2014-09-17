USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SelectSiteByCodeAndURL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SelectSiteByCodeAndURL]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
      
CREATE PROCEDURE [dbo].[rsp_SelectSiteByCodeAndURL] 
@SiteCode INT,
@WebURL VARCHAR(2000)
AS
BEGIN
	SELECT * FROM dbo.Site 
	WHERE Site_Code !=@SiteCode AND WebURL = @WebURL
END


GO

