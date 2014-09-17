USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_CheckAccountingWinlossRecordExists]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_CheckAccountingWinlossRecordExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_Report_CheckAccountingWinlossRecordExists
	@company INT = 0,
	@subcompany INT = 0,
	@region INT = 0,
	@area INT = 0,
	@district INT = 0,
	@site INT = 0,
	@zone INT = 0,
	@category INT = 0,
	@startdate DATETIME,
	@enddate DATETIME,
	@SiteIDList VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	
	
	
	
	
	
	
	IF @company = 0
	    SET @company = NULL  
	
	
	
	IF @subcompany = 0
	    SET @subcompany = NULL  
	
	
	
	IF @region = 0
	    SET @region = NULL  
	
	
	
	IF @area = 0
	    SET @area = NULL  
	
	
	
	IF @district = 0
	    SET @district = NULL  
	
	
	
	IF @site = 0
	    SET @site = NULL  
	
	
	
	IF @zone = 0
	    SET @zone = NULL  
	
	
	
	IF @category = 0
	    SET @category = NULL 
	
	IF EXISTS (
	       SELECT *
	       FROM   [COLLECTION ] Coll
	              INNER JOIN Installation I WITH(NOLOCK)
	                   ON  coll.installation_id = i.Installation_ID
	              INNER JOIN Bar_Position BP WITH(NOLOCK)
	                   ON  BP.Bar_Position_ID = I.Bar_Position_ID
	              INNER JOIN MACHINE M WITH(NOLOCK)
	                   ON  M.machine_id = I.machine_id
	              LEFT JOIN Machine_Type MT WITH(NOLOCK)
	                   ON  MT.machine_type_id = M.machine_category_id
	              INNER JOIN [Site] S WITH(NOLOCK)
	                   ON  S.Site_ID = BP.Site_ID
	              INNER JOIN Sub_Company SC WITH(NOLOCK)
	                   ON  S.sub_company_id = SC.sub_company_id
	              INNER JOIN Company C WITH(NOLOCK)
	                   ON  SC.company_id = C.company_id
	              LEFT JOIN zone Z WITH(NOLOCK)
	                   ON  Z.Zone_ID = BP.Zone_ID
	       WHERE  CAST(coll.collection_date AS DATETIME) BETWEEN CONVERT(VARCHAR(30), @startdate, 106) 
	              AND CONVERT(VARCHAR(30), @enddate, 106)
	              AND ISNULL(@company, c.company_id) = c.company_id
	              AND ISNULL(@subcompany, S.sub_company_id) = S.sub_company_id
	              AND ISNULL(@region, S.sub_company_region_id) = S.sub_company_region_id
	              AND ISNULL(@area, S.sub_company_area_id) = S.sub_company_area_id
	              AND ISNULL(@district, S.sub_company_district_id) = S.sub_company_district_id
	                  --AND ISNULL(@site, S.site_id) = S.site_id
	              AND (
	                      @SiteIDList IS NOT NULL
	                      AND s.Site_Id IN (SELECT DATA
	                                        FROM   dbo.fnSplit (@SiteIDList, ','))
	                  )
	              AND ISNULL(@zone, ISNULL(Z.Zone_ID, 0)) = ISNULL(Z.Zone_ID, 0)
	              AND ISNULL(@category, MT.machine_type_id) = MT.machine_type_id
	   )
	    SELECT 1
	ELSE
	    SELECT 0
END
GO

