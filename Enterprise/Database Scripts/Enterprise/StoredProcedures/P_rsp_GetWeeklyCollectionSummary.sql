USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetWeeklyCollectionSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetWeeklyCollectionSummary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                             
--                            
-- Description: Gets the weekly collection summary for the given site code       
--              
-- Inputs:     @Site_Code
--				@Noof Records
--                            
--                            
-- RETURN:      NONE                      
--                            
-- =======================================================================                            
--                             
-- Revision History                            
--                             
-- Madhu		19/09/2008     Created      
---------------------------------------------------------------------------  
CREATE proc [dbo].[rsp_GetWeeklyCollectionSummary]     
(    
 @Site_Code varchar(10),    
 @NoOfRecords int    
)    
as    
    
SELECT top (@NoOfRecords )  MAX(Week_ID) AS Week_ID,     
MAX(Calendar_Week_Number) AS WeekNumber,     
MAX(Calendar_Week_Start_Date) AS StartDate,     
MAX(Calendar_Week_End_Date) AS EndDate,     
COUNT(Collection_ID) AS WeekCount,     
SUM(CashCollected) AS CashCollected,     
SUM(Declared_Notes) AS Notes,     
SUM(Declared_Coins) AS Coins, SUM(Declared_Tickets) AS TicktesIn,SUM(Tickets_Printed) AS TicktesOut,      
SUM(Refills) AS Refills,     
SUM(Refunds) AS Refunds,     
SUM(DecHandpay) AS Handpay,     
SUM(Shortpay) AS Shortpay, sum(Note_var) as NotesVar,sum(Coin_Var) as CoinVar,sum(Ticket_var) as TicketVar,sum(Handpay_Var) as HandpayVar, SUM(DecWinloss -MeterWinloss) as TakeVar,     
SUM(RDCRefill) AS RDCRefill,     
SUM(RDCVar) AS RDCVar,     
SUM(MeterCash) AS MeterCash,     
SUM(MeterRefill) AS MeterRefill,     
SUM(MeterVar) AS MeterVar,     
MAX(BatchAdj) AS BatchAdj,     
SUM(DecHandpay) AS DecHandpay,     
SUM(RDCHandpay) AS RDCHandpay, SUM(RDC_tickets_In) AS RDC_Tickets_In, SUM(RDC_tickets_Out) AS RDC_Tickets_Out,     
SUM(MeterHandpay) AS MeterHandpay,     
SUM(Ticket) AS Ticket, SUM(MeterWinloss) AS RDC_Take,    
SUM(DecWinloss) AS Cash_Take, SUM(RDC_Coins) AS RDC_Coins,     
SUM(HopperChange) AS HopperChange , SUM(Void) as Void, SUM(expired) as Expired,    
SUM(Progressive_Value_Declared) AS Progressive_Value_Declared,    
SUM(Progressive_Value_Variance) AS Progressive_Value_Variance,    
SUM(EftIn/100) AS  EFTIN,    
SUM(EFTOut/100) AS EFTOut    
FROM VW_CollectionData       
JOIN Site on VW_CollectionData.Site_Id = site.Site_ID    
WHERE Site.Site_Code = @Site_Code     
--AND Week_ID<>0 /*This SPs is used only when Week_ID=0*/
GROUP BY Week_ID     
ORDER BY Max(CONVERT(DATETIME,Calendar_Week_Start_Date, 103)) DESC, Week_ID ASC 

GO

