USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteViewerCollsBatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteViewerCollsBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---      
--- Inputs:      see inputs       
---      
--- Outputs:     (0)   - no error ..       
---              OTHER - SQL error       
---       
--- =====================================================================================================================      
---       
--- Revision History      
---       
--- Anuradha 03 July 2009    Created      
--- Sudarsan S 20 July 2009 picked Week ID from Collection Details instead of view  
--- Gabu 	29/11/10      Added the Eft values 
------------------------------------------------------------------------------------------------ ----------------------        
----------------------------------------------------------------------------------------------- ----------------------          
        
CREATE procedure [dbo].[rsp_GetSiteViewerCollsBatch]    
  @Weeks        int,        
  @Periods      int,        
  @Site         int        
        
AS         
        
  DECLARE @tmpweeks TABLE ( week_id int )        
  DECLARE @tmpperiods TABLE ( period_id int ) 
  DECLARE @PromoCashable TABLE (Amount float,TicketCount int,CollectionID int) 
  DECLARE @PromoNonCashable TABLE (Amount float,TicketCount int,CollectionID int)       
        
SET NOCOUNT ON      
if @Weeks > 0         
  BEGIN        
    --SET rowcount @Weeks        
          
    INSERT INTO @tmpweeks        
    SELECT TOP(@Weeks) Calendar_Week_ID         
      FROM Site         
      JOIN Sub_Company_Calendar         
        ON Site.Sub_Company_ID = Sub_Company_Calendar.Sub_Company_ID        
      JOIN Calendar_Week         
        ON Sub_Company_Calendar.Calendar_ID = Calendar_Week.Calendar_ID        
     WHERE Site.Site_ID = @site         
       AND CONVERT(DATETIME, Calendar_Week.Calendar_Week_Start_Date, 103) <= getdate()        
  ORDER BY CONVERT(DATETIME, Calendar_Week.Calendar_Week_Start_Date, 103) DESC        
  END        
        
  if @Periods > 0         
  BEGIN        
    --SET rowcount @Periods        
          
    INSERT INTO @tmpperiods        
    SELECT TOP(@Periods) Calendar_Period_ID         
      FROM Site          
      JOIN Sub_Company_Calendar         
        ON Site.Sub_Company_ID = Sub_Company_Calendar.Sub_Company_ID        
      JOIN Calendar_Period         
        ON Sub_Company_Calendar.Calendar_ID = Calendar_Period.Calendar_ID        
     WHERE Site.Site_ID = @site         
       AND CONVERT(DATETIME, Calendar_Period.Calendar_Period_Start_Date, 103) <= getdate()        
  ORDER BY CONVERT(DATETIME, Calendar_Period.Calendar_Period_Start_Date, 103) DESC        
  END        

INSERT INTO @PromoCashable 

SELECT SUM(CT.CT_Value) AS Amount ,
   COUNT(CT.CT_Barcode) As TicketCount,  
   
  CT.CT_Inserted_Collection_ID As CollectionID             
 
  FROM COLLECTION_TICKET CT 
  
  WHERE 
  CT.CT_TicketType = 0    
  AND CT.CT_VoucherStatus <> 'LT'                   
 
  AND ISNULL(CT.CT_IsPromotionalTicket,0)=1
  GROUP BY CT_Inserted_Collection_ID
              
   --PromoNonCashable 
   INSERT INTO @PromoNonCashable 
  SELECT SUM(CT.CT_Value) AS Amount ,
   COUNT(CT.CT_Barcode) As TicketCount,  
   
  CT.CT_Inserted_Collection_ID As CollectionID             
 
  FROM COLLECTION_TICKET CT 
  
  WHERE 
  CT.CT_TicketType = 1    
  AND CT.CT_VoucherStatus <> 'LT'                   
 
  AND ISNULL(CT.CT_IsPromotionalTicket,0)=1
  GROUP BY CT_Inserted_Collection_ID
 
        
SELECT          
  MAX(BatchDate) AS BatchDate,         
  MAX(Batch_Date_performed) AS Batch_Date_performed,         
  MAX(Batch_Time_performed) AS Batch_Time_performed,         
  MAX(Batch_Name) AS Batch_Name,         
  MAX(Vw_CollectionData.Batch_ID) AS Batch_ID,         
  MAX(BatchRef) AS BatchRef,         
  COUNT(Vw_CollectionData.Collection_ID) AS BatchCount,           
  SUM(Vw_CollectionData.CashCollected) AS CashCollected,         
  SUM(Declared_Notes) AS Notes,      
  SUM(Vw_CollectionData.DecTicketBalance) AS DecTicketBalance,     
  SUM(Declared_Coins) AS Coins,   
  SUM(DeclaredTicketValue) AS TicktesIn,  
  SUM(DeclaredTicketPrintedValue) AS TicktesOut, 
  SUM(PC.Amount) AS PromoCashableIn,
  SUM(PNC.Amount) AS PromoNonCashableIn,      
  SUM(Vw_CollectionData.Net_Coin) AS Net_Coin,     
  SUM(Vw_CollectionData.Refills) AS Refills,         
  SUM(Vw_CollectionData.Refunds) AS Refunds,         
  SUM(DecHandpay) AS Handpay,         
  SUM(Shortpay) AS Shortpay,   
  SUM(Note_var) as NotesVar,  
  SUM(Coin_Var) as CoinVar,        
  SUM(Ticket_var) as TicketVar,  
  SUM(Handpay_Var) as HandpayVar,   
  SUM(Take_Var) as TakeVar,         
  SUM(RDCRefill) AS RDCRefill,         
  SUM(Vw_CollectionData.RDCVar) AS RDCVar,         
  SUM(Vw_CollectionData.MeterCash) AS MeterCash,         
  SUM(MeterRefill) AS MeterRefill,         
  SUM(Vw_CollectionData.MeterVar) AS MeterVar,      
  SUM(RDC_Notes) AS RDC_Notes,  
  MAX(BatchAdj) AS BatchAdj,         
  SUM(DecHandpay) AS DecHandpay,         
  SUM(RDCHandpay) AS RDCHandpay,   
  SUM(RDC_tickets_In) AS RDC_Tickets_In,   
  SUM(RDC_tickets_Out) AS RDC_Tickets_Out,         
  SUM(MeterHandpay) AS MeterHandpay,         
  SUM(Ticket) AS Ticket,   
  SUM(RDC_Take) AS RDC_Take,        
  SUM(Declared_Notes) + SUM(DecTicketBalance) - SUM(DecHandpay) + SUM(Net_Coin)  AS Cash_Take,   
  (SUM(Declared_Notes) + SUM(DecTicketBalance) - SUM(DecHandpay) + SUM(Net_Coin)) -   
  (SUM(RDC_Notes) + (SUM(DecTicketBalance) - SUM(Ticket_var))- SUM(RDCHandpay) + SUM(RDC_Coins)) AS WinLossVar,  
  SUM(RDC_Coins) AS RDC_Coins,         
  SUM(HopperChange) AS HopperChange ,  
  SUM(RDC_Coins_Out) AS RDC_Coins_Out,  
  SUM(Void) as Void,   
  SUM(expired) as Expired,        
  SUM(Progressive_Value_Declared) AS Progressive_Value_Declared,        
  SUM(Progressive_Value_Variance) AS Progressive_Value_Variance,  
  SUM(VW_CollectionData.EftIn ) AS EftIn,  
  SUM(VW_CollectionData.DecEftIn ) AS DecEftIn ,
  SUM(VW_CollectionData.EftOUT ) AS EftOut,  
  SUM(VW_CollectionData.DecEftout ) AS DecEftOut 
  FROM VW_CollectionData          
  JOIN Collection_Details    
   ON Collection_Details.Collection_ID = VW_CollectionData.Collection_ID  
   LEFT OUTER JOIN @PromoCashable PC ON PC.CollectionID=VW_CollectionData.Collection_ID
   LEFT OUTER JOIN @PromoNonCashable PNC ON PNC.CollectionID=VW_CollectionData.Collection_ID      
WHERE         
 Site_ID = @Site         
        
 AND ( ( @Weeks > 0         
           AND         
           Collection_Details.Week_ID IN ( SELECT Week_ID FROM @tmpweeks )         
         )         
         OR @Weeks = -1 )         
        
        
     AND ( ( @Periods > 0         
           AND         
           Collection_Details.Period_ID IN ( SELECT Period_ID FROM @tmpperiods )        
         ) OR @Periods = - 1 )        
        
        
GROUP BY         
 VW_CollectionData.Batch_ID         
ORDER BY        
 MAX(CAST(BatchDate AS DATETIME)) DESC, MAX(BatchRef) DESC, MAX(Vw_CollectionData.Batch_ID) ASC     

GO

