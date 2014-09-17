USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_getCrossPropertyTicketAnalysis]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_getCrossPropertyTicketAnalysis]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_getCrossPropertyTicketAnalysis]
	@Company	INT = 0,
	@SubCompany INT = 0,
	@Region		INT = 0,
	@Area		INT = 0,
	@District	INT = 0,
	@Site		INT = 0,
	@StartDate	DATETIME,
	@EndDate	DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	
	IF @Company = 0
	    SET @Company = NULL          
	
	IF @SubCompany = 0
	    SET @SubCompany = NULL
	
	IF @Region = 0
	    SET @Region = NULL
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL
	
	IF @Site = 0
	    SET @Site = NULL           
	
	SELECT Site_code,
	       Site_Name 
	       INTO #tmpSite
	FROM   [SITE]    
	
	SELECT SITE1 AS FromSite,
	       SITE2 AS ToSite,
	       dbo.FnGetSiteName(SITE2) AS SiteName,
	       CompanyID,
	       CompanyName,
	       SubCompanyID,
	       SubCompanyName,
	       ISNULL(PROMOCASHABLEIN, 0) AS PROMOCASHABLEIN,
	       ISNULL(PROMOCASHABLEOUT, 0) AS PROMOCASHABLEOUT,
	       ISNULL(CASHABLEIN, 0) AS CASHABLEIN,
	       ISNULL(CASHABLEOUT, 0) AS CASHABLEOUT,
	       (ISNULL(CASHABLEIN, 0) -ISNULL(CASHABLEOUT, 0)) AS CASHABLENET,
	       (ISNULL(PROMOCASHABLEIN, 0) -ISNULL(PROMOCASHABLEOUT, 0)) AS PROMOCASHABLENET,
	       (ISNULL(CASHABLEIN, 0) -ISNULL(CASHABLEOUT, 0)) + (ISNULL(PROMOCASHABLEIN, 0) -ISNULL(PROMOCASHABLEOUT, 0)) AS TotalNet
	FROM   (
	           SELECT #tmpSite.Site_code AS Site1,
	                  CASE 
	                       WHEN #tmpSite.Site_code = ipaySiteid THEN iSITEID
	                       ELSE ipaysiteid
	                  END AS SITE2,
	                  --strbarcode,    
	                  SUM(
	                      CAST(CAST(iAmount AS DECIMAL(10, 2)) / 100 AS DECIMAL(10, 2))
	                  ) AS Amount,
	                  CASE 
	                       WHEN ticket_type = 0
	           AND #tmpSite.Site_code = ipaySiteid THEN 'CASHABLEOUT' 
	               WHEN ticket_type = 2
	           AND #tmpSite.Site_code = ipaySiteid THEN 'PROMOCASHABLEOUT' 
	               WHEN ticket_type = 0
	           AND #tmpSite.Site_code = iSiteid THEN 'CASHABLEIN' 
	               WHEN ticket_type = 2
	           AND #tmpSite.Site_code = iSiteid THEN 'PROMOCASHABLEIN' 
	               END AS TicketType,
	           C.Company_ID AS CompanyID,
	           C.Company_Name AS CompanyName,
	           SC.Sub_Company_ID AS SubCompanyID,
	           SC.Sub_Company_Name AS SubCompanyName 
	           FROM voucher V 
	           INNER JOIN #tmpSite ON (
	               #tmpSite.Site_code = V.iSITEID
	               OR #tmpSite.Site_code = V.ipaySiteid
	           ) 
	           INNER JOIN [SITE] SI ON V.iSiteid = SI.Site_Code 
	           INNER JOIN Sub_Company SC ON SI.Sub_Company_ID = SC.Sub_Company_ID 
	           INNER JOIN Company C ON SC.Company_ID = C.Company_ID 
	           WHERE (dtPaid BETWEEN @StartDate AND @EndDate)
	           AND (strvoucherstatus = 'LT' OR strvoucherstatus = 'PD')
	           AND iSiteid <> ipaysiteid
	           AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	           AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	           AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	           AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	           AND ISNULL(@Site, SI.Site_Id) = SI.Site_Id
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND SI.Site_ID IN (SELECT DATA
	                                      FROM   fnSplit(@SiteIDList, ','))
	               )
	               
	               GROUP BY C.Company_ID,
	           C.Company_Name,
	           SC.Sub_Company_ID,
	           SC.Sub_Company_Name,
	           #tmpSite.Site_code,
	           isiteid,
	           ipaysiteid,
	           strvoucherstatus,
	           ticket_type
	       ) CTTable 
	       
	       PIVOT(
	           MAX(Amount) 
	           FOR TicketType  IN (PROMOCASHABLEIN, PROMOCASHABLEOUT, CASHABLEIN, CASHABLEOUT)
	       ) AS pvt
END
GO

