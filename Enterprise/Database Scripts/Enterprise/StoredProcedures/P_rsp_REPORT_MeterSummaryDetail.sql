USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_MeterSummaryDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_MeterSummaryDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_REPORT_MeterSummaryDetail]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region     INT = 0,
	@Area       INT = 0,
	@District   INT = 0,
	@Site       INT = 0,
	@StartDate  DateTime,
	@EndDate    DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
	SET DATEFORMAT dmy
  IF @company = 0 SET @company = NULL
  IF @subcompany = 0 SET @subcompany = NULL
  IF @region = 0     SET @region = NULL
  IF @area = 0       SET @area = NULL
  IF @district = 0   SET @district = NULL
  IF @site = 0       SET @site = NULL
 
  SET @startdate = CAST(CONVERT(VARCHAR(12), CAST(@startdate AS DATETIME), 106)  AS DATETIME) 
  SET @enddate = CAST(CONVERT(VARCHAR(12), CAST(@enddate AS DATETIME), 106) AS DATETIME) 

  SELECT
	i.Installation_ID,
	i.Bar_Position_ID,
	VW_Read.Read_Date,
	VW_Read.Installation_No,
	VW_Read.RDCCashIn,
	VW_Read.RDCCashOut,
	VW_Read.RDCCash,
	VW_Read.Read_Forced,
	Machine_Type_Category.Machine_Type_ID,
	Machine_Type_Category.Machine_Type_Code,
	Site.Site_Name,
	Machine.Machine_Stock_No
FROM
    dbo.Installation  i WITH (NOLOCK)
	INNER JOIN dbo.Bar_Position Bar_Position WITH (NOLOCK) ON i.Bar_Position_ID = Bar_Position.Bar_Position_ID
	INNER JOIN dbo.vw_read vw_read WITH (NOLOCK) on ( i.installation_id = vw_read.installation_no AND CAST ( vw_read.read_date AS DATETIME ) BETWEEN @startdate AND @enddate )  
    INNER JOIN dbo.Machine MACHINE WITH (NOLOCK) ON i.Machine_ID = Machine.Machine_ID
    INNER JOIN dbo.Site Site  WITH (NOLOCK) ON Bar_Position.Site_ID = Site.Site_ID
	LEFT OUTER JOIN dbo.Machine_Type Machine_Type_Category WITH (NOLOCK) ON Machine.Machine_Category_ID = Machine_Type_Category.Machine_Type_ID  
	INNER JOIN Sub_Company SC on SC.Sub_Company_ID=Site.Sub_Company_ID
	INNER JOIN company c on c.company_id=sc.company_id
WHERE
(
  	CAST(i.installation_Start_Date AS DateTime)<=@EndDate      
) 
 AND ( (@company IS NULL ) OR (@company IS NOT NULL AND  c.Company_ID = @company ) ) 
 AND ( ( @subcompany IS NULL ) OR  ( @subcompany IS NOT NULL AND SITE.SUB_COMPANY_ID = @subcompany ) )  
 AND ( ( @region IS NULL ) OR ( @region IS NOT NULL AND SITE.SUB_COMPANY_REGION_ID = @region  ))      
 AND ( ( @area IS NULL ) OR ( @area IS NOT NULL AND SITE.SUB_COMPANY_AREA_ID = @area ))      
 AND ( ( @district IS NULL )OR ( @district IS NOT NULL AND SITE.SUB_COMPANY_DISTRICT_ID = @district ))      
 AND ( ( @site IS NULL )OR ( @site IS NOT NULL AND SITE.SITE_ID = @site ))   
 AND (@SiteIDList IS NOT NULL AND SITE.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
 

 order by site.site_name,
		  Machine_Type_Category.Machine_Type_Code,
          Bar_Position.Bar_Position_ID,
		  Machine_Type_Category.Machine_Type_ID,
	      i.installation_start_date desc

GO

