USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_SiteAddressList]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[rsp_REPORT_SiteAddressList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/**
  Rakesh Marwaha  06/05/08  This SP returns data which will be displayed in Site Address List Report
**/
CREATE PROCEDURE [dbo].[rsp_REPORT_SiteAddressList]

	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Supplier INT = 0,
	@Depot INT = 0,
  @orderby	  VarChar(10)
AS
BEGIN
	SET DATEFORMAT dmy
	
  IF @company = 0    SET @company = NULL
  IF @subcompany = 0 SET @subcompany = NULL
  IF @region = 0     SET @region = NULL
  IF @area = 0       SET @area = NULL
  IF @district = 0   SET @district = NULL
  IF @supplier = 0   SET @supplier = NULL
  IF @depot = 0		 SET @depot = NULL
	
SELECT
    Company.Company_Logo_Reference,
    Sub_Company.Sub_Company_Name, Sub_Company.Sub_Company_Logo_Reference,
    Site.Site_ID, Site.Site_Code, Site.Site_Name, Site.Site_Postcode, Site.Site_Phone_No, Site.Site_Manager, Site.Site_Address_1, Site.Site_Address_2, Site.Site_Address_3, Site.Site_Address_4,
    Depot.Depot_Name,
    Operator.Operator_Logo_Reference
FROM Site
	       INNER JOIN Sub_Company
	            ON  SITE.Sub_Company_ID = Sub_Company.Sub_Company_ID
	       INNER JOIN Company
	            ON  Sub_Company.Company_ID = Company.Company_ID
	       LEFT JOIN Sub_Company_Region
	            ON  SITE.Sub_Company_Region_ID = Sub_Company_Region.Sub_Company_Region_ID
	       LEFT JOIN Sub_Company_District
	            ON  SITE.Sub_Company_District_ID = Sub_Company_District.Sub_Company_District_ID
	       LEFT JOIN Sub_Company_Area
	            ON  SITE.Sub_Company_Area_ID = Sub_Company_Area.Sub_Company_Area_ID
	       LEFT JOIN depot
	            ON  depot.Depot_ID = SITE.Depot_ID
	       LEFT JOIN Operator
	            ON  Operator.Operator_ID = depot.Supplier_ID
	WHERE  company.company_id = ISNULL(@company, company.company_id)
	       AND SITE.sub_company_id = ISNULL(@subcompany, SITE.sub_company_id)
	       AND SITE.sub_company_region_id = ISNULL(@region, SITE.sub_company_region_id)
	       AND SITE.sub_company_area_id = ISNULL(@area, SITE.sub_company_area_id)
	       AND SITE.sub_company_district_id = ISNULL(@district, SITE.sub_company_district_id)
	       AND Operator.Operator_ID = ISNULL(@supplier, Operator.Operator_ID)
	       AND Depot.Depot_ID = ISNULL(@depot, Depot.Depot_ID)
	       AND site_id IN (SELECT DISTINCT bp.Site_ID
	                       FROM   Installation i
	                              INNER JOIN Bar_Position bp
	                                   ON  i.Bar_Position_ID = bp.Bar_Position_ID
	                       WHERE  i.Installation_End_Date IS NULL)
	ORDER BY
	       CASE @orderby
	            WHEN '0' THEN SITE.Site_Name
	            WHEN '1' THEN SITE.Site_Code
	            WHEN '2' THEN SITE.Site_PostCode
	       END
END
GO