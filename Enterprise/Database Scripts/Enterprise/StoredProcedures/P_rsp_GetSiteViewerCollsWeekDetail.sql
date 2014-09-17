USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'rsp_GetSiteViewerCollsWeekDetail' 
)
   DROP PROCEDURE dbo.rsp_GetSiteViewerCollsWeekDetail
GO

CREATE PROCEDURE dbo.rsp_GetSiteViewerCollsWeekDetail
    @WeekId INT,
	@Site INT
AS
/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------
										  
*****************************************************************************************************/

BEGIN
	DECLARE @IsAFTIncludedInCalculation BIT
	
	DECLARE @sAddShortpay               VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut' 
	
	SELECT @IsAFTIncludedInCalculation = CASE 
	                                          WHEN Setting_value = 'True' THEN 1
	                                          ELSE 0
	                                     END
	FROM   Setting
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'  
	
	SELECT --MAX(Calendar_Week_Start_Date) AS StartDate,
	       --MAX(Calendar_Week_End_Date) AS EndDate,
	       MAX(CAST(convert(datetime,Calendar_Week_Start_Date,103) AS VARCHAR(30))) AS StartDate,  
		   MAX(CAST(convert(datetime,Calendar_Week_End_Date,103) AS VARCHAR(30)) ) AS EndDate,  
        
	       MAX(Calendar_Week_Number) AS WeekNumber,
	       COUNT(Calendar_Week_Number) AS WeekCount,
	       MAX(Batch_ID) AS Batch_ID,
	       MAX(BatchRef) AS BatchRef,
	       MAX(BatchDate) AS BatchDate,
	       MAX(BatchAdj) AS BatchAdj,
	       Week_ID AS Week_ID,
	       Site_ID AS Site_ID,
	       '' AS Batch_Memo,
	       '' AS Batch_User_Name,
	       '' AS Batch_Time,
	       COUNT(Collection_ID) AS BatchCount,
	       SUM(CashCollected) AS CashCollected,
	       SUM(Defloat) AS Defloat,
	       SUM(GrossCash) AS GrossCash,
	       SUM(Refills) AS Refills,
	       SUM(Refunds) AS Refunds,
	       SUM(Ticket) AS Ticket,
	       SUM(NetCash) AS NetCash,
	       SUM(CashTake) AS CashTake,
	       SUM(RDCCash) AS RDCCash,
	       SUM(RDCRefill) AS RDCRefill,
	       SUM(RDCVar) AS RDCVar,
	       SUM(MeterCash) AS MeterCash,
	       SUM(MeterRefill) AS MeterRefill,
	       SUM(MeterVar) AS MeterVar,
	       SUM(VTP) AS VTP,
	       SUM(VTP / 10) AS Handle,
	       SUM(DecTicketBalance + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE Shortpay END)) AS DecTicketBalance,
	       SUM(PercentageIn) AS PercentageIn,
	       SUM(PercentageOut) AS PercentageOut,
	       SUM(DecHandpay) AS DecHandpay,
	       SUM(RDCHandpay) AS RDCHandpay,
	       SUM(DecHandpay - RDCHandpay) AS RDCHandpayVar,
	       SUM(MeterHandpay) AS MeterHandpay,
	       SUM(DecHandpay - MeterHandpay) AS MeterHandpayVar,
	       SUM(HopperChange) AS HopperChange,
	       SUM(Handpay_Var) AS Handpay_Var,
	       SUM(Declared_Coins) AS Declared_Coins,
	       SUM(Net_Coin) AS Net_Coin,
	       SUM(RDC_Coins) AS RDC_Coins,
	       SUM(Coin_Var) AS Coin_Var,
	       SUM(RDC_TICKETS_INSERTED_NONCASHABLE_VALUE) AS 
	       RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
	       SUM(RDC_TICKETS_PRINTED_NONCASHABLE_VALUE) AS 
	       RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
	       SUM(Declared_Notes) AS Declared_Notes,
	       SUM(RDC_Notes) AS RDC_Notes,
	       SUM(Note_Var) AS Note_Var,
	       SUM(RDC_Coins_Out) AS RDC_Coins_Out,
	       SUM(Tickets_Printed) AS Tickets_Printed,
	       SUM(Ticket_Balance) AS Ticket_Balance,
	       SUM(RDC_Ticket_Balance) AS RDC_Ticket_Balance,
	       SUM(Ticket_Var + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE Shortpay END)) AS Ticket_Var,
	       SUM(Cash_Take) AS Cash_Take,
	       SUM(RDC_Take) AS RDC_Take,
	       SUM(Take_Var) AS Take_Var,
	       SUM(Shortpay) AS Shortpay,
	       SUM(Expired) AS Expired,
	       SUM(Void) AS Void,
	       SUM(Progressive_Value_Meter) AS Progressive_Value_Meter,
	       SUM(Progressive_Value_Declared) AS Progressive_Value_Declared,
	       SUM(Progressive_Value_Variance) AS Progressive_Value_Variance,
	       SUM(EftIn) AS EftIn,
	       SUM(EftOut) AS EftOut,
	       SUM(DecEftIn) AS DecEftIn,
	       SUM(DecEftOut) AS DecEftOut,
	       @IsAFTIncludedInCalculation IsAFTIncluded,
	         --DecWinOrLoss----------------------------------------
	      SUM( Declared_Notes + DecTicketBalance - DecHandpay + Net_Coin + (
	           CASE 
	                WHEN @IsAFTIncludedInCalculation = 1 THEN (DecEftIn / 100.00)
	                     -(DecEftOut / 100.00)
	                ELSE 0
	           END
	       ) )AS DecWinOrLoss,
	       --MeterWinOrLoss----------------------------------------
	       SUM(RDC_Notes + (DecTicketBalance -Ticket_Var) -
	       RDCHandpay + RDC_Coins + (
	           CASE 
	                WHEN @IsAFTIncludedInCalculation = 1 THEN (EftIn / 100.00) 
	                     -(EftOut / 100.00)
	                ELSE 0
	           END
	       )) AS MeterWinOrLoss,
	        --nCasino ( (HANDLE-MeterWinOrLoss)/HANDLE  ) *100)
	       sum(CASE 
	            WHEN Handle > 0 THEN (
	                     (
	                         Handle -
	                         --START MeterWinOrLoss
	                         (
	                             RDC_Notes + (DecTicketBalance -Ticket_Var) -
	                             RDCHandpay + RDC_Coins + (
	                                 CASE 
	                                      WHEN @IsAFTIncludedInCalculation = 1 THEN (EftIn / 100.00) 
	                                           -(EftOut / 100.00)
	                                      ELSE 0
	                                 END
	                             )
	                         )--END MeterWinOrLoss
	                     ) / Handle
	                 ) 
	                 * 100.00
	            ELSE 0.00
	       END ) AS nCasino,
	       SUM( ISNULL(PromoCashableIn,0)) AS PromoCashableIn,
	       SUM( ISNULL(PromoNonCashableIn,0)) AS PromoNonCashableIn
	      --ISNULL( CASE WHEN CT.CT_IsPromotionalTicket=1 THEN 
	      -- (
	      -- CASE WHEN CT.CT_TicketType=0 THEN
	      -- (
	      --   SUM(CAST(ISNULL(CT.CT_Value,0)AS FLOAT)) 
	      --   ) END 
	      --  )END,0) 
	      -- AS PromoCashableIn,
	       
	      --ISNULL( CASE WHEN CT.CT_IsPromotionalTicket=1 THEN 
	      -- (
	      -- CASE WHEN CT.CT_TicketType=1 THEN
	      -- (
	      --    SUM(CAST(ISNULL(CT.CT_Value,0)AS FLOAT)) 
	      --   ) END 
	      --   )END,0)
	      -- AS PromoNonCashableIn
	       
	FROM   dbo.VW_CollectionData 
	--LEFT OUTER JOIN Collection_Ticket CT ON CT.CT_Inserted_Collection_ID= VW_CollectionData.Collection_ID
	WHERE  Week_ID = @WeekId
	       AND Site_ID = @Site
	GROUP BY
	       Site_ID,
	       Week_ID
	       --CT.CT_IsPromotionalTicket,
	       --CT.CT_TicketType
END
GO

