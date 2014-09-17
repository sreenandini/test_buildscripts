USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetLiabilityTransferSummaryReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetLiabilityTransferSummaryReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /****** Object:  StoredProcedure [dbo].[rsp_GetLiabilityTransferSummaryReport] 
  Script Date: 04/05/2012 22:21:21 
  Created By: Kirubakar S ******/
CREATE PROCEDURE [dbo].[rsp_GetLiabilityTransferSummaryReport]
	@Company	INT = 0,
	@SubCompany INT = 0,
	@Region		INT = 0,
	@Area		INT = 0,
	@District	INT = 0,
	@Site		INT = 0,
	@StartDate	DATETIME,
	@EndDate	DATETIME ,
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
	
	SELECT isiteid AS SiteCode,
	       dbo.FnGetSiteName(ipaysiteid) AS SiteName,
	       CompanyID,
	       CompanyName,
	       SubCompanyID,
	       SubCompanyName,
	       ipaysiteid,
	       ISNULL(CASHABLE, 0) AS CashableAmount,
	       ISNULL(PROMOCASHABLE, 0) AS PromoAmount,
	       ISNULL(CASHABLE, 0) + ISNULL(PROMOCASHABLE, 0) AS Total
	FROM   (
	           SELECT isiteid,
	                  ipaysiteid,
	                  SUM(
	                      CAST(CAST(iAmount AS DECIMAL(10, 2)) / 100 AS DECIMAL(10, 2))
	                  ) AS Amount,
	                  CASE ticket_type
	                       WHEN 0 THEN 'CASHABLE'
	                       WHEN 2 THEN 'PROMOCASHABLE'
	                  END AS TicketType,
	                  C.Company_ID AS CompanyID,
	                  C.Company_Name AS CompanyName,
	                  SC.Sub_Company_ID AS SubCompanyID,
	                  SC.Sub_Company_Name AS SubCompanyName
	           FROM   voucher VR
	                  INNER JOIN [SITE] SI
	                       ON  VR.iSiteid = SI.Site_Code
	                  INNER JOIN Sub_Company SC
	                       ON  SI.Sub_Company_ID = SC.Sub_Company_ID
	                  INNER JOIN Company C
	                       ON  SC.Company_ID = C.Company_ID
	                           --LEFT OUTER JOIN bar_position BAR ON SI.Site_ID=BAR.Site_ID
	           WHERE  strvoucherstatus = 'LT'
	                  AND (dtPaid BETWEEN @StartDate AND @EndDate)
	                  AND ISNULL(@Site, SI.Site_Id) = SI.Site_Id
	                  AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	                  AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	                  AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	                  AND ISNULL(@SubCompany, SI.Sub_Company_Id) = SI.Sub_Company_Id
	                  AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	                  AND (
	                          @SiteIDList IS NOT NULL
	                          AND SI.Site_ID IN (SELECT DATA
	                                             FROM   fnSplit(@SiteIDList, ','))
	                      )
	           GROUP BY
	                  C.Company_ID,
	                  C.Company_Name,
	                  SC.Sub_Company_ID,
	                  SC.Sub_Company_Name,
	                  isiteid,
	                  ipaysiteid,
	                  ticket_type
	       ) CTTable 
	       PIVOT(MAX(Amount) FOR TicketType IN (CASHABLE, PROMOCASHABLE)) AS pvt
END
GO
