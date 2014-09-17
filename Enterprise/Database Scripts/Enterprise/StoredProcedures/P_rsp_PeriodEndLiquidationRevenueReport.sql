USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_PeriodEndLiquidationRevenueReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_PeriodEndLiquidationRevenueReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_PeriodEndLiquidationRevenueReport] 
(
    @Company     AS INT,
    @SubCompany  AS INT,
    @Region      AS INT,
    @Area        AS INT,
    @District    AS INT,
    @Site        AS INT,
    @Startdate   AS DATETIME,
    @Enddate     AS DATETIME,
    @SiteIDList  AS VARCHAR(MAX)
)
AS
BEGIN
	SELECT s.Site_Code,
	       ld.LiquidationPerformedDate,
	       LD.CollectionBatchId AS BatchId,
	       SUBSTRING(
	           Batch_ref,
	           CHARINDEX(',', Batch_ref, 2) + 1,
	           LEN(Batch_ref)
	       ) AS Site_Batch_No,
	       SH.ShareHolderName AS ShareHolderName,
	       S.SITE_NAME,
	       ld.MeterIn,
	       ld.MeterOut,
	       ld.NetAmount,
	       ps.ProfitSharePercentage / 100 AS PercentageShare,
	       CAST(
	           (
	               CASE 
	                    WHEN CAST(ps.ProfitSharePercentage AS DECIMAL(18, 2)) =
	                         0.00 THEN 0
	                    ELSE ld.NetAmount * (ps.ProfitSharePercentage / 100)
	               END
	           ) AS DECIMAL(18, 2)
	       ) AS Amount,
	       (REPLACE(CONVERT(VARCHAR(12),Convert(Datetime,cp.Calendar_Period_Start_Date,105),106),' ','-') + '--' +
	      REPLACE(CONVERT(VARCHAR(12),Convert(Datetime,cp.Calendar_Period_End_Date,105),106),' ','-') )AS PayPeriod,
	       ps. ProfitShareGroupId
	FROM   LiquidationDetails ld
	       INNER JOIN Batch B
	            ON  B.Batch_ID = ld.CollectionBatchId
	       INNER JOIN [Site] s
	            ON  s.Site_ID = ld.SiteId
	       INNER JOIN Sub_Company sc
	            ON  sc.Sub_Company_ID = s.Sub_Company_ID
	       INNER JOIN Company c
	            ON  c.Company_ID = sc.Company_ID
	       INNER JOIN dbo.ProfitShare PS
	            ON  PS.ProfitShareGroupId = ld.ProfitShareGroupId
	            AND PS.SysDelete = 0
	       INNER JOIN dbo.ShareHolders SH
	            ON  SH.ShareHolderId = PS.ShareHolderId
	            AND SH.SysDelete = 0
	       LEFT  JOIN Calendar_Period cp
	            ON  cp.Calendar_Period_ID = ld.PayPeriodId
	WHERE  CAST(ld.LiquidationPerformedDate AS DATETIME) BETWEEN @Startdate 
	       AND 
	       @Enddate
	       AND ld.CollectionBatchId IS NOT NULL
	       AND (
	               (@company = 0)
	               OR (@company <> 0 AND c.company_id = @company)
	           )
	       AND (
	               (@subcompany = 0)
	               OR (@subcompany <> 0 AND s.sub_company_id = @subcompany)
	           )
	       AND (
	               (@region = 0)
	               OR (@region <> 0 AND S.Sub_Company_Region_ID = @region)
	           )
	       AND (
	               (@area = 0)
	               OR (@area <> 0 AND S.Sub_Company_Area_ID = @area)
	           )
	       AND (
	               (@district = 0)
	               OR (@district <> 0 AND S.Sub_Company_District_ID = @district)
	           )
	       AND ((@Site = 0) OR (@Site <> 0 AND s.site_id = @Site))
	       AND (@SiteIDList IS NOT NULL AND s.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
	ORDER BY
	       SH.ShareHolderId DESC
END
GO

