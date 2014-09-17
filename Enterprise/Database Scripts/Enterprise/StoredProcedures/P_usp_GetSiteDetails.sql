USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Madhu A    13/05/08     Created
--------------------------------------------------------------------------- 

CREATE PROCEDURE [dbo].[usp_GetSiteDetails]
(
	@SiteID int,
	@DistrictID int,
	@AreaID int,
	@RegionID int,
	@SubCompanyID int,
	@CompanyID int
)
AS
--------------------------------------------------
--- Create SQL statement
--------------------------------------------------
DECLARE @SQL VARCHAR(5000)

SET @SQL  = 'SELECT DISTINCT Company.Company_Name, Sub_Company.Sub_Company_Name, Site.Site_Name, 
            Sub_Company_Area.Sub_Company_Area_Name, Sub_Company_District.Sub_Company_District_Name, Sub_Company_Region.Sub_Company_Region_Name 
            FROM Site 
            JOIN Sub_Company ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID 
            JOIN Company ON Sub_Company.Company_ID = Company.Company_ID 
            LEFT JOIN Sub_Company_Area ON Site.Sub_Company_Area_ID = Sub_Company_Area.Sub_Company_Area_ID 
            LEFT JOIN Sub_Company_District ON Site.Sub_Company_District_ID = Sub_Company_District.Sub_Company_District_ID 
            LEFT JOIN Sub_Company_Region ON Site.Sub_Company_Region_ID = Sub_Company_Region.Sub_Company_Region_ID '

		
IF @SiteID >0 
	SET @SQL = @SQL + ' WHERE Site.Site_ID = '+ CONVERT(VARCHAR, @SiteID)
ELSE IF @DistrictID >0
	SET @SQL = @SQL + ' WHERE Site.Sub_Company_District_ID = '+ CONVERT(VARCHAR, @DistrictID)
ELSE IF @AreaID >0
	SET @SQL = @SQL + ' WHERE Site.Sub_Company_Area_ID =  '+ CONVERT(VARCHAR, @AreaID)
ELSE IF @RegionID >0 
	SET @SQL = @SQL + ' WHERE Site.Sub_Company_Region_ID = '+ CONVERT(VARCHAR, @RegionID)
ELSE IF @SubCompanyID >0
	SET @SQL = @SQL + ' WHERE Sub_Company.Sub_Company_ID =  '+ CONVERT(VARCHAR, @SubCompanyID)
ELSE 
	SET @SQL = @SQL + ' WHERE Company.Company_ID = '+ CONVERT(VARCHAR, @CompanyID)

EXEC(@SQL)


GO

