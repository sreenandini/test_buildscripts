USE Enterprise
GO

-- 
-- Description: To generate reports for the Promotions 
-- 
-- 
--			
-- ======================================================================= 
    
-- Object: StoredProcedure [dbo].[rsp_SelectPromotionHistoryReport]
-- Created By: Durga Devi
---------------------------------------------------------------------------  

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_SelectPromotionHistoryReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_SelectPromotionHistoryReport]
GO

USE Enterprise
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rsp_SelectPromotionHistoryReport]
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
	
	
	SELECT * INTO #tmpPromoHistory
	FROM   (
	           SELECT DISTINCT 
	                  ISNULL(S.Site_Name, '') AS SiteName,
	                  ISNULL(Prom.PromotionalID, 0) AS PromotionalID,
	                  ISNULL(Prom.PromotionalName, '') AS PromotionalName,
	                  CASE Prom.PromotionalTicketType
	                       WHEN 1 THEN 'Non Cashable'
	                       WHEN 0 THEN 'Cashable'
	                  END AS PromotionalTicketType,
	                  ISNULL(Prom.TotalTickets, 0) AS TotalTickets,
	                  CAST(
	                      (ISNULL(Prom.PromotionalTicketAmount, 0) / 100.00) AS 
	                      DECIMAL(18, 2)
	                  ) AS PromotionalTicketAmount,
	                  CAST(
	                      (ISNULL(Prom.TotalTicketAmount, 0) / 100.00) AS 
	                      DECIMAL(18, 2)
	                  ) AS TotalTicketAmount,
	                  Prom.dtPromoCreation AS dtPrinted,
	                  Prom.dtExpire AS dtExpire,
	                  ISNULL(PRT.NoOfRedeemed, 0) AS NoOfRedeemed,
	                  CAST((ISNULL(PRT.RedeemedAmount, 0)) AS DECIMAL(18, 2)) AS RedeemedAmount,
	                  ISNULL(PET.NoOfTicketExpired, 0) AS NoOfTicketExpired,
	                  CAST((ISNULL(PET.ExpiredAmount, 0)) AS DECIMAL(18, 2)) AS ExpiredAmount,
	                  ISNULL(PVT.NoOfTicketsVoid, 0) AS NoOfTicketsVoid,
	                  CAST((ISNULL(PVT.VoidAmount, 0)) AS DECIMAL(18, 2)) AS VoidAmount
	           FROM   Promotions Prom
	                  OUTER APPLY dbo.udf_GetPromoRedeemedTickets(Prom.HQ_ID) AS 
	           PRT
	           OUTER APPLY dbo.udf_GetPromoExpiredTickets(Prom.HQ_ID) AS PET
	           OUTER APPLY dbo.udf_GetPromoVoidTickets(Prom.HQ_ID) AS PVT
	           INNER JOIN PromotionalTickets PT
	                       ON  PT.PromotionalID = Prom.HQ_ID
	                  INNER JOIN Voucher V
	                       ON  V.iVoucherID = PT.VoucherID
	                  INNER JOIN [SITE] S
	                       ON  S.Site_Code = V.iSiteID
	                       AND V.iSiteID = Prom.SiteID
	                  INNER JOIN Sub_Company SC
	                       ON  S.Sub_Company_ID = SC.Sub_Company_ID
	                  INNER JOIN Company C
	                       ON  SC.Company_ID = C.Company_ID
	                           -- convert(varchar(20),@@EndDate, 105)
	           WHERE  CONVERT(DATETIME, CONVERT(VARCHAR(20), Prom.dtPromoCreation, 120)) 
	                  BETWEEN CONVERT(DATETIME, CONVERT(VARCHAR(20), @StartDate, 120)) 
	                  AND CONVERT(VARCHAR(20), @EndDate, 120)
	                  -- convert(varchar(20),@StartDate, 120) and convert(varchar(20),@EndDate, 120)
	                  
	                  --BETWEEN convert(varchar(20),@StartDate, 120)  AND convert(varchar(20),@EndDate, 120)
	                  AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	                  AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	                  AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	                  AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	                  AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	                  AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	                  AND Prom.SourceName IS NULL
	                  AND (
	                          @SiteIDList IS NOT NULL
	                          AND S.Site_ID IN (SELECT DATA
	                                            FROM   fnSplit(@SiteIDList, ','))
	                      )
	       ) tmpPromHist
	
	SELECT DISTINCT
	       ROW_NUMBER() OVER(ORDER BY PromotionalID) AS SrNo,
	       SiteName,
	       PromotionalID,
	       PromotionalName,
	       PromotionalTicketType,
	       TotalTickets,
	       PromotionalTicketAmount,
	       TotalTicketAmount,
	       dtPrinted,
	       dtExpire,
	       NoOfRedeemed,
	       RedeemedAmount,
	       NoOfTicketExpired,
	       ExpiredAmount,
	       NoOfTicketsVoid,
	       VoidAmount
	FROM   #tmpPromoHistory
	GROUP BY
	       SiteName,
	       PromotionalID,
	       PromotionalName,
	       PromotionalTicketType,
	       TotalTickets,
	       PromotionalTicketAmount,
	       TotalTicketAmount,
	       dtPrinted,
	       dtExpire,
	       NoOfRedeemed,
	       RedeemedAmount,
	       NoOfTicketExpired,
	       ExpiredAmount,
	       NoOfTicketsVoid,
	       VoidAmount
END
GO


 
/*

exec [rsp_SelectPromotionHistoryReport] 1,0,0,0,0,'2012-10-03 17:08:58.630','2013-10-03 17:08:58.630' 
*/