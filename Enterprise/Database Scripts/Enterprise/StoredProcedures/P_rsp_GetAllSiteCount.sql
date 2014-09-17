USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAllSiteCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAllSiteCount]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].rsp_GetAllSiteCount
AS
/* =======================================================================    
USED IN :  Reports Main Form [ReportsMain]
Desc	: To get number of active sites
 ======================================================================= */
BEGIN

	SELECT COUNT('D') TotalSites FROM dbo.SITE  WHERE Site_Enabled=1

END



GO

