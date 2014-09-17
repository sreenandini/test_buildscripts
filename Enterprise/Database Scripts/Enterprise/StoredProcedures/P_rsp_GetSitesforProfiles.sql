USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSitesforProfiles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSitesforProfiles]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*
 *	this stored procedure is to fetch the site details for displaying in a tree to apply profiles
 *
 *	Change History:
 *	
 *	Sudarsan S		16-02-2009		created
*/
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[rsp_GetSitesforProfiles]
AS
SELECT DISTINCT Company.Company_ID, 
Company.Company_Name, 
Sub_Company.Sub_Company_ID, 
Sub_Company.Sub_Company_Name, 
Site.Site_ID, Site.Site_Name, 
Site.Site_Code, 
Site.Site_Address_2, 
Site.Site_Address_3  
FROM Site 
INNER JOIN Sub_Company ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID
INNER JOIN Company ON Sub_Company.Company_ID = Company.Company_ID
WHERE Site.Site_End_Date IS NULL 
ORDER BY Company.Company_Name ASC, Company.Company_ID ASC, Sub_Company.Sub_Company_Name ASC, Sub_Company.Sub_Company_ID ASC, Site.Site_Name ASC, Site.Site_Code ASC, Site.Site_ID ASC 



GO

