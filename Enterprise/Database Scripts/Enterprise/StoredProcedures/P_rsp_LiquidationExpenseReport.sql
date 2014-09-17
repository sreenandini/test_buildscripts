USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_LiquidationExpenseReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_LiquidationExpenseReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---  EXEC rsp_LiquidationExpenseReport 2, 2, 0, 0, 0, '28 Nov 2012','28 Nov 2012'
       
--- Revision History        
---         
--- Aishwarrya V S 12/12/12  Created         
------------------------------------------------------------------------------------------------------------------------      
CREATE PROCEDURE rsp_LiquidationExpenseReport(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList  VARCHAR(MAX)
)
AS
BEGIN
	SELECT CONVERT(VARCHAR(10), C.Calendar_Period_Start_Date, 101) + ' -- ' 
	       + CONVERT(VARCHAR(10), C.Calendar_Period_End_Date, 101) AS PayPeriod,
	       S.Site_Code AS SiteID,
	       S.Site_Name AS Site_Name,
	       CASE WHEN LD.ReadId IS NULL THEN 1
				ELSE 0
		   END AS IsCollection,
		   CAST(CASE WHEN LD.ReadId IS NULL THEN CAST(SUBSTRING(B.Batch_Ref, CHARINDEX(',', B.Batch_Ref) + 1, LEN(B.Batch_Ref) - CHARINDEX(',', B.Batch_Ref)) AS VARCHAR(30))
				ELSE CAST(R.Read_Date AS VARCHAR(30))
		   END AS VARCHAR(30)) AS CollectionBatchId,
	       SH.ShareHolderName AS ShareHolderName,
	       CAST(
	           CASE 
	                WHEN SH.ShareHolderId = 3 THEN LD.PrevCarriedForwardExpense
	                ELSE 0
	           END AS DECIMAL(18, 2)
	       ) AS CarriedForward,
	       CAST(LD.ExpenseShareAmount AS DECIMAL(18, 2)) AS FixedExpense,
	       CAST(
	           CASE 
	                WHEN SH.ShareHolderId = 3 THEN LD.PrevCarriedForwardExpense 
	                     + LD.ExpenseShareAmount
	                ELSE LD.ExpenseShareAmount
	           END AS DECIMAL(18, 2)
	       ) AS TotalExpense,
	       CAST(
	           CASE 
	                WHEN SH.ShareHolderId = 3 THEN LD.PrevCarriedForwardExpense 
	                     + LD.ExpenseShareAmount
	                ELSE 0
	           END AS DECIMAL(18, 2)
	       ) AS TotalExpenseForSum,
	       (ES.ExpenseSharePercentage / 100) AS PercentageShare,
	       CAST(LD.ExpenseShareAmount * ((ES.ExpenseSharePercentage / 100)) AS DECIMAL(18, 2)) AS Amount,
	       CAST(
	           CASE 
	                WHEN SH.ShareHolderId = 3 THEN LD.WriteOffAmount
	                ELSE 0
	           END AS DECIMAL(18, 2)
	       ) AS WriteOff,
	       ES.ExpenseShareGroupId,
	       LD.LiquidationId
	FROM   dbo.LiquidationDetails LD
	       LEFT JOIN dbo.Calendar_Period C
	            ON  C.Calendar_Period_ID = LD.PayPeriodId
	       LEFT JOIN Batch B 
				ON B.Batch_ID = LD.CollectionBatchId
		   LEFT JOIN [Read] R
				ON R.Read_Id = LD.ReadId
	       INNER JOIN SITE S
	            ON  S.Site_ID = LD.SiteId
	       INNER JOIN ExpenseShare ES
	            ON  ES.ExpenseShareGroupId = LD.ExpenseShareGroupId
					AND ES.SysDelete = 0
	       INNER JOIN dbo.ShareHolders SH
	            ON  SH.ShareHolderId = ES.ShareHolderId
	            AND SH.SysDelete = 0
	       INNER JOIN [dbo].Sub_Company SC
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN [dbo].Company
	            ON  Company.Company_ID = SC.Company_ID	       
	            AND (
	                    (@company = 0)
	                    OR (@company <> 0 AND Company.Company_ID = @company)
	                )
	            AND (
	                    (@subcompany = 0)
	                    OR (@subcompany <> 0 AND S.Sub_Company_ID = @subcompany)
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
	            AND ((@site = 0) OR (@site <> 0 AND S.Site_ID = @site))
	            
	            AND (@SiteIDList IS NOT NULL AND S.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
	            
	            AND (LiquidationPerformedDate BETWEEN @StartDate AND @EndDate)

END
GO
