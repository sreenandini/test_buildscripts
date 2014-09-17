USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteViewerColls]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteViewerColls]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---
--- Description: returns collection details for site viewer screen
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
--- C.Taylor   Jan 2008     Created 
--- C.Taylor   16/03/09     Speeded up, removing query on collectiondetails, 
---                              replaced with query on collectiondata
--- C.Taylor   Jun 2009     removed * from select, selected only required columns
--- Vineetha M 01/07/2009    Added SET NOCOUNT ON for the issue in View sites for the week & period selection
--- Anuradha J 07/07/09      Changed condition based on Period_Id from collection Details rather than week when checking based on periods
--- Sudarsan S 20/07/09      Changed condition based on Week_Id from collection Details 
--- GBabu 	29/11/10      Added the Eft values 
------------------------------------------------------------------------------------------------- ----------------------  
CREATE procedure [dbo].[rsp_GetSiteViewerColls]    
    
  @records      int,    
  @weeks        int,    
  @periods      int,    
  @bar_position int,    
  @site         int    
    
AS    
    
SET NOCOUNT ON    
  DECLARE @tmpweeks TABLE ( week_id int )    
  DECLARE @tmpperiods TABLE ( period_id int )
  
  DECLARE @sAddShortpay               VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'    

  
     DECLARE @PromoCashable TABLE (Amount float,TicketCount int,CollectionID int) 
  DECLARE @PromoNonCashable TABLE (Amount float,TicketCount int,CollectionID int)   
  -- create a list of weeks to use    
  if @weeks > 0     
  BEGIN    
    SET rowcount @weeks    
      
    INSERT INTO @tmpweeks    
    SELECT Calendar_Week_ID     
      FROM Site     
      JOIN Sub_Company_Calendar     
        ON Site.Sub_Company_ID = Sub_Company_Calendar.Sub_Company_ID    
      JOIN Calendar_Week     
        ON Sub_Company_Calendar.Calendar_ID = Calendar_Week.Calendar_ID    
     WHERE Site.Site_ID = @Site     
       AND CONVERT(DATETIME, Calendar_Week.Calendar_Week_Start_Date, 103) <= getdate()    
  ORDER BY CONVERT(DATETIME, Calendar_Week.Calendar_Week_Start_Date, 103) DESC    
  END    
    
  if @periods > 0     
  BEGIN    
    SET rowcount @periods    
      
    INSERT INTO @tmpperiods    
    SELECT Calendar_Period_ID     
      FROM Site      
      JOIN Sub_Company_Calendar     
        ON Site.Sub_Company_ID = Sub_Company_Calendar.Sub_Company_ID    
      JOIN Calendar_Period     
        ON Sub_Company_Calendar.Calendar_ID = Calendar_Period.Calendar_ID    
     WHERE Site.Site_ID = @Site     
       AND CONVERT(DATETIME, Calendar_Period.Calendar_Period_Start_Date, 103) <= getdate()    
  ORDER BY CONVERT(DATETIME, Calendar_Period.Calendar_Period_Start_Date, 103) DESC    
  END    
    
    
  

 
    
  if @records > 0    
     set rowcount @records    
    
 
     
    
 SELECT Collection_Source,        
         Batch_Received_All,        
         VW_CollectionData.Batch_ID,        
         VW_CollectionDetails.Period_End_ID,        
         Collection_Terms_Invalid,        
         COLLECTION_REPLACEMENT,        
         Collection_Terms_Invalid,        
         Collection_Terms_Invalid_Ignore,        
        
         Collection_Processed_Through_Terms,        
         VW_CollectionDetails.EDI_Import_Log_ID,        
        
   VW_CollectionData.Collection_ID,        
     VW_CollectionData.Collection_Date,        
     Machine_Name,      
      (
	    CASE 
	    WHEN GameName   = 'MULTI GAME' THEN               
        ISNULL(MGMP.Multigamename, 'MULTI GAME')
	    ELSE  GameName  
	    END
	   ) As
     GameName,       
     Operator_Name,        
     Depot_Name,        
     VW_CollectionData.Collection_Days,        
          
     (Declared_Notes + DecTicketBalance - DecHandpay + Net_Coin ) AS Cash_Take,        
          
     VW_CollectionData.CashCollected,        
     VW_CollectionData.PercentageIn,        
     VW_CollectionData.PercentageOut,        
     Collection_declared_Notes,        
     VW_CollectionData.Refunds,        
     Net_Coin,        
     Collection_Ticket_Balance,        
     DecHandpay,        
     Shortpay,        
     VW_CollectionData.Declared_Notes,    
     Note_Var,        
     Coin_Var,        
     VW_CollectionData.Net_Coin,    
     VW_CollectionData.DecHandpay,    
     VW_CollectionData.RDC_Notes,    
     VW_CollectionData.RDCHandpay,    
     Ticket_Var + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE Shortpay END) Ticket_Var,        
     Handpay_Var,        
     Take_Var,        
     DecTicketBalance + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE Shortpay END) DecTicketBalance,       
     RDC_Coins,        
     Void,        
     RDC_Ticket_Balance,        
     (Declared_Notes + DecTicketBalance - DecHandpay + Net_Coin+(VW_CollectionData.DecEftIn/100- VW_CollectionData.DecEftOut/100)) -         
     (RDC_Notes + (DecTicketBalance - Ticket_var) - RDCHandpay + RDC_Coins+(VW_CollectionData.EftIn/100 - VW_CollectionData.EftOut/100)) AS WinLossVar,        
     Collection_Handpay_Var,        
     RDC_Take,        
     VW_CollectionData.Refills,        
     VW_CollectionDetails.Collection_Supplier_Share,        
     VW_CollectionDetails.Collection_Company_Share,        
     VW_CollectionDetails.Collection_Location_Share,        
     VW_CollectionDetails.Collection_Other_Share,        
     VW_CollectionDetails.Collection_AMLD,        
     Remarks,        
          
     Collection_Details.Period_Id,      
     VW_CollectionDetails.Period_End_ID,        
     Lag,        
     TargetVariance,        
     RDCIn,        
     RDCOut,        
     vw_collectiondata.RDCCash,        
     VW_CollectionData.RDCVar,        
     VW_CollectionData.EftIn As EftIn,     
     VW_CollectionData.DecEftIn As DecEftIn,     
  VW_CollectionData.Eftout AS EftOut,    
  VW_CollectionData.DecEftout AS DecEftOut,    
     Secondary_Brewery_Name,        
     VW_CollectionDetails.Secondary_Sub_Company_Period_End_ID,        
         VW_CollectionData.RDC_Coins_Out AS RDC_Coins_Out,    
           VW_CollectionDetails.Week_End_ID,        
     VW_CollectionDetails.VTP/10 AS VTP,    
            Sub_Company_Name,        
            Company_Name,        
                    
            MeterCashIn,        
            MeterCashOut,        
            VW_CollectionDetails.Handle,        
        
            PIndex,        
                    
            PacePrev9Days,        
            PacePrev9Cash,        
   PaceDays,        
            PaceCash,        
        
            VW_CollectionDetails.Collection_GPT,        
            VW_CollectionDetails.Collection_FOBT_Stakes,        
            VW_CollectionDetails.Collection_FOBT_Payout,        
            VW_CollectionDetails.Collection_Transactions ,
            VW_CollectionDetails.Machine_Start_Date    
        
    FROM VW_CollectionDetails         
    JOIN VW_CollectionData         
      ON VW_CollectionDetails.Collection_ID = VW_CollectionData.Collection_ID          
 JOIN Collection_Details       
   On VW_CollectionData.Collection_ID=Collection_Details.Collection_ID      
     LEFT OUTER JOIN MultiGameMapping MGMP 
	
   	on VW_CollectionData.Machine_ID = mgmp.MachineID
        
   WHERE VW_CollectionDetails.Bar_Position_ID = @bar_position         
     AND VW_CollectionData.Bar_Position_ID = @bar_position      
     AND VW_CollectionDetails.Collection_Report_Status <> 2 -- !!!        
        
     AND ( ( @weeks > 0         
           AND         
           Collection_Details.Week_ID IN ( SELECT Week_ID FROM @tmpweeks )         
         )         
         OR @weeks = -1 )         
        
     AND ( ( @periods > 0         
           AND         
           --VW_CollectionData.Week_ID IN ( SELECT Period_ID FROM @tmpperiods )        
   Collection_Details.Period_ID IN ( SELECT Period_ID FROM @tmpperiods )        
         ) OR @periods = - 1 )        
        
  ORDER BY CAST(VW_CollectionData.Collection_Date AS DATETIME) DESC,         
           VW_CollectionData.Collection_ID DESC
           
           
GO

