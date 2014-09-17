USE [Enterprise] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.rsp_GetReadLiquidationReportRecords'))
	DROP PROCEDURE [dbo].[rsp_GetReadLiquidationReportRecords]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetReadLiquidationReportRecords
-- -----------------------------------------------------------------
-- 
-- To Read Liquidation details for Report
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 31/05/2013 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetReadLiquidationReportRecords]
	@Site_Id INT = NULL,
	@NumberOfRecords INT = 0
AS
BEGIN
	
	SET NOCOUNT ON

	SELECT 
		TOP (CASE WHEN @NumberOfRecords <> 0 THEN @NumberOfRecords ELSE 3000 END)
		LD.LiquidationId,
		CAST(CASE WHEN ISNULL(LD.HQ_ID, 0) = 0 THEN '-' ELSE CAST(LD.HQ_ID AS VARCHAR(20)) END AS VARCHAR(20)) AS SiteLiquidationId,
		LD.ReadId,
		DATEADD(DD, 0, DATEDIFF(DD, 0, R.[ReadDate])) AS [Read_Date],		
		LD.LiquidationPerformedDate AS  LiquidationDate,
		ISNULL(CONVERT(nvarchar(30), CP.Calendar_Period_Start_Date, 120)+ ' - ' + 
				CONVERT(nvarchar(30), CP.Calendar_Period_End_Date , 120), '') AS Calendar_Period,
		MeterIn AS Gross,
		MeterOut AS TicketsExpected,
		NetAmount AS Net,
		RetailerNegativeNet,
		TicketPaid,
		AdvanceToRetailer,
		Retailer,
		BalanceDue,
		RetailerNetRevenue
	FROM LiquidationDetails LD
		LEFT JOIN Calendar_Period CP ON CP.Calendar_Period_ID = LD.PayPeriodId
		INNER JOIN [Site] S ON S.Site_Id = LD.SiteId
		INNER JOIN [Read] R WITH (NOLOCK) ON R.Read_ID = LD.ReadId  
	WHERE S.Site_Id = @Site_Id
		AND LD.ReadId IS NOT NULL
		AND ISNULL(LD.ProfitShareGroupId, 0) > 0
	ORDER BY LD.LiquidationId DESC
END

GO

