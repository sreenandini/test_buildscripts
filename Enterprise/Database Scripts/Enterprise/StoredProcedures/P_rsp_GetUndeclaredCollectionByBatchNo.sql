USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUndeclaredCollectionByBatchNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUndeclaredCollectionByBatchNo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetUndeclaredCollectionByBatchNo](  
 @Site_ID   INT,  
    @BatchNo      INT,    
    @FilterBy     INT = NULL,    
    @FilterValue  VARCHAR(50) = NULL    
)    
AS      
BEGIN      
 SET @FilterBy = COALESCE(@FilterBy, 0)    
 DECLARE @Type INT    
 SET @Type = 0    
 IF (@FilterBy = 3 AND ISNULL(@FilterValue, '')<>'')    
 BEGIN    
  SET @Type = CAST(ISNULL(@FilterValue, '0') AS INT)    
 END    
     
 DECLARE @Region AS VARCHAR(2)      
 EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'Region', @Region OUTPUT  
    
 DECLARE @DeclareMethod AS VARCHAR(MAX)        
 EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'TicketDeclarationMethod', @DeclareMethod OUTPUT  
     
 DECLARE @Client AS VARCHAR(500)  
 EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'CLIENT', @Client OUTPUT  
  
SELECT Zone.Zone_ID,    
 Zone.Zone_Name,           
 Bar_Position.Bar_Position_Name,         
 Machine.Machine_Stock_No As AssetNo,       
 Machine_Class.Machine_Name,          
 Machine.Machine_Uses_Meters,          
 Installation.Installation_ID,          
 Installation.Installation_Token_Value,      
 --[Event].Evt_Datetime,          
 CAST(Collection.Collection_Date AS DATETIME) AS Evt_Datetime,
 Collection.Collection_ID,          
 Collection.Batch_ID,          
 Collection.Collection_Defloat_Collection,          
 Collection.CollectionHandHeldMetersReceived,          
  isnull(Collection.Cash_Collected_100000P/100,0) as Cash_Collected_100000P,          
   isnull(Collection.Cash_Collected_50000P/100,0) as Cash_Collected_50000P,          
   isnull(Collection.Cash_Collected_20000P/100,0) as Cash_Collected_20000P,       
   isnull(Collection.Cash_Collected_10000P/100,0) as Cash_Collected_10000P,       
   isnull(Collection.Cash_Collected_5000P/100,0) as Cash_Collected_5000P,       
   isnull(Collection.Cash_Collected_2000p/100,0) as Cash_Collected_2000p,       
   isnull(Collection.Cash_Collected_1000P/100,0) as Cash_Collected_1000P,       
   isnull(Collection.Cash_Collected_500P/100,0) as Cash_Collected_500P,      
   isnull(Collection.Cash_Collected_200P/100,0) as Cash_Collected_200P,       
   isnull(Collection.Cash_Collected_100P/100,0)as   Cash_Collected_100P,     
   ISNULL(CAST(Collection.Cash_Collected_50p/100.0 AS DECIMAL(20,2)), 0) as  Cash_Collected_50p,
   ISNULL(CAST(Collection.Cash_Collected_20p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_20p,
   ISNULL(CAST(Collection.Cash_Collected_10p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_10p,
   isnull(CAST(Collection.Cash_Collected_5p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_5p,
   isnull(CAST(Collection.Cash_Collected_2p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_2p,
   isnull(CAST(Collection.Cash_Collected_1p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_1p,
        TicketsIn =  (CASE WHEN UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'METER'   
     THEN CAST((ISNULL(CAST(Collection.COLLECTION_RDC_TICKETS_INSERTED_VALUE AS FLOAT)/100, 0)   
      + ISNULL(CAST(Collection.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT)/100, 0)) AS MONEY)   
     WHEN UPPER(LTRIM(RTRIM(@Client))) = 'SGVI' AND UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'AUTO'  
     THEN CAST((ISNULL(CAST(Collection.COLLECTION_RDC_TICKETS_INSERTED_VALUE AS FLOAT)/100, 0)   
      + ISNULL(CAST(Collection.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT)/100, 0)) AS MONEY)  
     ELSE ISNULL(      
            dbo.fn_GetTicketsInByMachineBatch(      
                COLLECTION.Batch_ID,      
                Installation.Installation_ID      
            ),      
            0      
        ) END),      
 TicketsOut = CASE WHEN UPPER(LTRIM(RTRIM(@Client))) = 'SGVI' AND UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'AUTO'  
     THEN CAST((ISNULL(CAST(Collection.COLLECTION_RDC_TICKETS_PRINTED_VALUE AS FLOAT)/100, 0)   
      + ISNULL(CAST(Collection.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE AS FLOAT)/100, 0)) AS MONEY)   
    ELSE  
   ISNULL(dbo.fn_GetTicketsOutByMachineBatch(Collection.Collection_ID, Installation.Installation_ID),0)  
    END,   
 ShortPay = ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Shortpay'), 0)+    
 ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Offline Voucher-Shortpay'), 0),        
 Handpay = ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Handpay Credit'), 0)      
   + ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'AttendantPay Credit'), 0),      
        
 Refills = ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Refill'),0),        
 Refunds = ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Refund'),0),        
 HandpayJackpot = CASE WHEN UPPER(LTRIM(RTRIM(@Client))) = 'SGVI' AND UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'AUTO'  
     THEN  
      CAST((ISNULL(CAST(Collection.COLLECTION_RDC_JACKPOT AS FLOAT)* Installation.Installation_Price_Per_Play/100, 0)) AS REAL)  
     ELSE  
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Handpay Jackpot'),0) +      
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Prog'),0)  +     
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Mystery Jackpot'),0) +    
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'PROGRESSIVE'),0)  +     
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'AttendantPay Jackpot'),0)  
     END,   
 DeclaredHandpay = CASE WHEN UPPER(LTRIM(RTRIM(@Client))) = 'SGVI' AND UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'AUTO'  
     THEN  
      CAST((ISNULL(CAST(Collection.COLLECTION_RDC_HANDPAY AS FLOAT)* Installation.Installation_Price_Per_Play/100, 0)) AS REAL)  
     ELSE  
     ISNULL(dbo.fn_GetDeclaredHandpayFromCollection(Collection.Collection_ID),0)    
     END,  
Case When @Region = 'US' Then                  
(ISNULL(dbo.fn_IncludeMissingCoinOut1P('COLL',Collection.Installation_ID,Collection_ID),0) +
(CAST(ISNULL(Collection.CASH_OUT_1P/100,0) AS FLOAT) / 100) +            
(CAST(ISNULL(Collection.CASH_OUT_2P/100,0) AS FLOAT) / 50) +                   
(CAST(ISNULL(Collection.CASH_OUT_5P/100,0) AS FLOAT) / 20) +                  
(CAST(ISNULL(Collection.CASH_OUT_10P/100,0) AS FLOAT) / 10) +                  
(CAST(ISNULL(Collection.CASH_OUT_20P/100,0) AS FLOAT) / 5) +                  
(CAST(ISNULL(Collection.CASH_OUT_50P/100,0) AS FLOAT) / 2))         
When @Region = 'AR' Then                  
(ISNULL(dbo.fn_IncludeMissingCoinOut1P('COLL',Collection.Installation_ID,Collection_ID),0) +           
(CAST(ISNULL(Collection.CASH_OUT_100P,0) AS FLOAT)) +       
(CAST(ISNULL(Collection.CASH_OUT_2P/100,0) AS FLOAT) / 50) +               
(CAST(ISNULL(Collection.CASH_OUT_5P/100,0) AS FLOAT) / 20) +              
(CAST(ISNULL(Collection.CASH_OUT_10P/100,0) AS FLOAT) / 10) +              
(CAST(ISNULL(Collection.CASH_OUT_20P/100,0) AS FLOAT) / 5) +              
(CAST(ISNULL(Collection.CASH_OUT_50P/100,0) AS FLOAT) / 2))           
Else          
(ISNULL(dbo.fn_IncludeMissingCoinOut1P('COLL',Collection.Installation_ID,Collection_ID),0) +   
(CAST(ISNULL(Collection.CASH_OUT_1P/100,0) AS FLOAT) / 100) +     
(CAST(ISNULL(Collection.CASH_OUT_2P/100,0) AS FLOAT) / 50) +               
(CAST(ISNULL(Collection.CASH_OUT_5P/100,0) AS FLOAT) / 20) +              
(CAST(ISNULL(Collection.CASH_OUT_10P/100,0) AS FLOAT) / 10) +              
(CAST(ISNULL(Collection.CASH_OUT_20P/100,0) AS FLOAT) / 5) +              
(CAST(ISNULL(Collection.CASH_OUT_50P/100,0) AS FLOAT) / 2) +          
(CAST(ISNULL(Collection.CASH_OUT_100P/100,0) AS FLOAT) ) +          
(CAST(ISNULL(Collection.CASH_OUT_200P/100,0) AS FLOAT) * 2)          
)           
End          
AS CoinOut ,    
-- Fix for SENTHIL    
--AttendantPay = ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Handpay Credit'), 0)      
--   + ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'AttendantPay Credit'), 0)   
  
--  
AttendantPay = CAST(CASE WHEN UPPER(LTRIM(RTRIM(@Client))) = 'SGVI' AND UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'AUTO'   
     THEN ISNULL( ((CAST(Collection.collection_rdc_jackpot AS FLOAT)   
         * Installation.Installation_Price_Per_Play) / 100),0)     
       +ISNULL(((CAST(Collection.collection_rdc_handpay AS FLOAT)   
         * Installation.Installation_Price_Per_Play) / 100),0)   
     ELSE    
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID,   
          Installation.Installation_ID, 'Handpay Credit'), 0)      
     + ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID,   
          Installation.Installation_ID, 'AttendantPay Credit'), 0)   
     + (CASE WHEN UPPER(LTRIM(RTRIM(@Client))) = 'SGVI' AND UPPER(LTRIM(RTRIM(@DeclareMethod))) = 'AUTO'  
     THEN  
      CAST((ISNULL(CAST(Collection.COLLECTION_RDC_JACKPOT AS FLOAT)* Installation.Installation_Price_Per_Play/100, 0)) AS REAL)  
     ELSE  
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Handpay Jackpot'),0) +      
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Prog'),0)  +     
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'Mystery Jackpot'),0) +    
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'PROGRESSIVE'),0)  +     
     ISNULL(dbo.fn_GetTreasuryEntriesInCollection(Collection.Collection_ID, Installation.Installation_ID, 'AttendantPay Jackpot'),0)  
     END)    
     END AS FLOAT)  
FROM           
Collection
--[Event]      
--INNER JOIN Collection ON Event.Evt_Fault_Details = Collection.Collection_ID           
INNER JOIN Installation ON Collection.Installation_ID = Installation.Installation_ID           
INNER JOIN Bar_Position ON Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID           
INNER JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID           
INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID           
LEFT JOIN Zone ON Bar_Position.Zone_ID = Zone.Zone_ID        
INNER JOIN dbo.[Site] ON Bar_Position.Site_ID = Site.Site_ID         
WHERE           
 --Evt_Fault_Type = 2000           
 --AND 
 (Declaration = 0 OR Declaration IS NULL)           
 AND Batch_ID = @BatchNo          
        AND Bar_Position.Bar_Position_Name LIKE (CASE @FilterBy WHEN 1 THEN ('%' + @FilterValue + '%') ELSE Bar_Position.Bar_Position_Name END)    
        AND MACHINE.Machine_Stock_No LIKE (CASE @FilterBy WHEN 2 THEN ('%' + @FilterValue + '%') ELSE MACHINE.Machine_Stock_No END)    
        AND COLLECTION.Collection_Defloat_Collection LIKE (CASE @FilterBy WHEN 3 THEN (@Type) ELSE COLLECTION.Collection_Defloat_Collection END)  
  AND [Site].Site_ID = @Site_Id  
 ORDER BY    
        Bar_Position.Bar_Position_Name    
      
END    

GO

