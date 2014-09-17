USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchConnectionStrings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchConnectionStrings]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: Fetches the Connectionstring for all the sites (for Wireless laptop changes)
--
-- Inputs:     NIL
-- Outputs:    NIL
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	15-07-2008		Created
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[rsp_FetchConnectionStrings]
AS

SELECT Site.Site_Name AS SiteName, Site.Site_Code AS SiteCode, Site.ConnectionString As Connection FROM dbo.Site Site FOR XML AUTO, ELEMENTS, ROOT('SiteDetails')




GO

