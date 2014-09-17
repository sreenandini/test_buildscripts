USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_MeterDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_MeterDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*      
-- ===================================================================================================================================
-- Stored Procedure [dbo].rsp_REPORT_MeterDetail
--  rsp_REPORT_MeterDetail 0,0,0,0,0,'13-Oct-2008','13-Oct-2012'
-- -----------------------------------------------------------------------------------------------------------------------------------
-- To display read data for active installations for the selected periods
-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History 
	Rakesh Marwaha		18/06/08	Made it to run for SSRS    
	Vineetha Mathew	25/05/09	Modified to display installation details in report even no entry fall in read table  
	Vineetha Mathew	19/06/09	Added columns  Machine_Stock_No & Read_Date
	Vineetha Mathew	27/02/2010	 used Cast() instead of Convert(datetime)
	Yoganandh P		08/07/2010	 Modified to display Game Name as 'Multi Game' if the game is 'Multi Game and
								 Auto Detected' else display actual 'Game Name'
-- ===================================================================================================================================   
 */  
CREATE PROCEDURE [dbo].[rsp_REPORT_MeterDetail]    
  @Company INT = 0,    
  @SubCompany INT = 0,    
  @Region     INT = 0,    
  @Area       INT = 0,    
  @District   INT = 0,    
  @Site       INT = 0,    
  @StartDate  datetime,    
  @EndDate    datetime,
  @SiteIDList varchar(MAX)    
    
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
	installation.installation_id ,
	installation.installation_percentage_payout ,
	bar_position.bar_position_id ,
	bar_position.bar_position_name ,
	vw_read.installation_no ,
	vw_read.rdccashin ,
	vw_read.rdccashout,
	vw_read.rdccash ,
	vw_read.read_forced ,
	VW_Read.Read_Date, 
	--CASE machine_class.machine_name WHEN 'Auto Detected' THEN 'Multi Game' ELSE machine_class.machine_name END AS machine_name, 
    machine_class.machine_name AS machine_name,
	zone.zone_name ,
	machine_type_category.machine_type_id ,
	machine_type_category.machine_type_code ,
	site.site_name,
	Machine.Machine_Stock_No
FROM    
	dbo.installation installation   WITH (NOLOCK) 
	INNER JOIN dbo.bar_position bar_position WITH (NOLOCK) on installation.bar_position_id = bar_position.bar_position_id    
	INNER JOIN dbo.vw_read vw_read WITH (NOLOCK) on ( installation.installation_id = vw_read.installation_no AND CAST ( vw_read.read_date AS DATETIME ) BETWEEN @startdate AND @enddate )
	INNER JOIN dbo.machine MACHINE WITH (NOLOCK)  on installation.machine_id = machine.machine_id    
	INNER JOIN dbo.machine_class machine_class WITH (NOLOCK)  on machine.machine_class_id = machine_class.machine_class_id    
	LEFT OUTER join dbo.machine_type machine_type_category WITH (NOLOCK)  on machine.machine_category_id = machine_type_category.machine_type_id    
	INNER JOIN dbo.site site WITH (NOLOCK) on bar_position.site_id = site.site_id    
	INNER JOIN sub_company sc on sc.sub_company_id=site.sub_company_id
	INNER JOIN company c on c.company_id=sc.company_id
	LEFT OUTER JOIN dbo.zone zone on ISNULL(bar_position.zone_id,0) = ISNULL(zone.zone_id,0)

WHERE  
	--( 
  (cast(installation.installation_Start_Date as datetime)<=@EndDate)      
  --AND     
  --(cast(installation.installation_End_Date as datetime)>=@EndDate  OR installation.installation_End_Date  IS NULL))    
     
	AND ( (@company IS NULL ) OR (@company IS NOT NULL AND  c.company_id = @company ) ) 
	AND ( ( @subcompany IS NULL ) OR  ( @subcompany IS NOT NULL AND SITE.SUB_COMPANY_ID = @subcompany ) )
	AND ( ( @region IS NULL ) OR ( @region IS NOT NULL AND SITE.SUB_COMPANY_REGION_ID = @region  ))    
	AND ( ( @area IS NULL ) OR ( @area IS NOT NULL AND SITE.SUB_COMPANY_AREA_ID = @area ))    
	AND ( ( @district IS NULL )OR ( @district IS NOT NULL AND SITE.SUB_COMPANY_DISTRICT_ID = @district ))    
	--AND ( ( @site IS NULL )OR ( @site IS NOT NULL AND SITE.SITE_ID = @site )) 
	AND @SiteIDList IS NOT NULL AND SITE.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
ORDER BY
    Site.Site_Name ASC, 
	Machine_Type_Category.Machine_Type_Code ASC,
	Bar_Position.Bar_Position_ID ASC,
	Installation.Installation_Start_Date Desc,
	Machine_Type_Category.Machine_Type_ID ASC   

GO

