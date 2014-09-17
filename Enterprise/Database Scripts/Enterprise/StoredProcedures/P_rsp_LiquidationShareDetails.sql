USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_LiquidationShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_LiquidationShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_LiquidationShareDetails
@SiteId INT,
@BatchId INT
AS
BEGIN
SELECT
CollectionPerformedDate,
SiteName,
MeterIn,
NetAmount,
RetailerSharePercentage,
BalanceDue,
Retailer,
RetailerNegativeNet,
MeterOut,
TicketPaid,
AdvanceToRetailer,
RetailerShareAfterAdjustment,
RetailerNetRevenue
FROM LiquidationDetails 
WHERE 
SiteId = @SiteId AND CollectionBatchId=@BatchId 
END

GO

