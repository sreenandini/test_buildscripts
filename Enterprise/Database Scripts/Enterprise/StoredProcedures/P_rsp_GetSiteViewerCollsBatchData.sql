USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteViewerCollsBatchData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteViewerCollsBatchData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetSiteViewerCollsBatchData]
	@BatchId INT,
	@site INT
AS
BEGIN
	SET NOCOUNT ON  
	
	
	
	DECLARE @SGVI_Enabled      VARCHAR(10)      
	
	DECLARE @SGVI_autodeclare  VARCHAR(10)    
	
	DECLARE @sAddShortpay               VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'
	
	
	SELECT @SGVI_Enabled = Setting_Value
	FROM   Setting WITH(NOLOCK)
	WHERE  Setting_Name = 'SGVi_Enabled'   
	
	
	
	SELECT @SGVI_autodeclare = Setting_Value
	FROM   Setting WITH(NOLOCK)
	WHERE  Setting_Name = 'Auto_Declare_Monies'
	
	DECLARE @CollectionID INT
	
	
	
	;WITH FilteredInstallation 
	
	
	
	AS 
	
	
	
	(
	    SELECT I.Installation_ID,
	           s.Region,
	           I.Installation_Price_Per_Play
	    FROM   Installation I WITH(NOLOCK)
	           INNER JOIN Bar_Position BP WITH(NOLOCK)
	                ON  BP.Bar_Position_ID = I.Bar_Position_ID
	           INNER JOIN [Site] S WITH(NOLOCK)
	                ON  S.Site_ID = BP.Site_ID
	                AND S.Site_id = @Site
	), 
	
	
	
	CollectionDetails AS(
	    SELECT C.Collection_ID,
	           Batch_Date AS BatchDate,
	           Batch_Date_performed AS Batch_Date_performed,
	           Batch_Time AS Batch_Time_performed,
	           Batch_Name AS Batch_Name,
	           B.Batch_ID AS Batch_ID,
	           Batch_Ref AS BatchRef,
	           ISNULL(CAST(C.CashCollected AS FLOAT), 0) AS CashCollected,
	           CC.Collection_Declared_Notes AS Declared_Notes,
	           CC.Collection_Declared_Coins AS Coins,
	           COALESCE(C.DeclaredTicketValue, 0) AS TicktesIn,
	           COALESCE(C.DeclaredTicketPrintedValue, 0) AS TicktesOut,
	           (CC.Collection_Refills) AS Refills,
	           (CC.Collection_Note_Var) AS NotesVar,
	           (CC.Collection_Handpay_Var) 
	           
	           - COALESCE(
	               (
	                   (
	                       CAST(C.collection_rdc_jackpot AS FLOAT) 
	                       
	                       * FI.Installation_Price_Per_Play
	                   ) / 100
	               ),
	               0
	           ) AS Handpay_Var,
	           (CC.Collection_RDCRefill) AS RDCRefill,
	           (CC.Collection_RDCVar) AS RDCVar,
	           (CC.Collection_MeterCash) AS MeterCash,
	           (CC.Collection_MeterRefill) AS MeterRefill,
	           (CC.Collection_MeterVar) AS MeterVar,
	           (CC.Collection_RDC_Notes) AS RDC_Notes,
	           (B.Batch_Company_Error) AS BatchAdj,
	           (CC.Collection_RDCHandpay) 
	           
	           + COALESCE(
	               (
	                   (
	                       CAST(C.collection_rdc_jackpot AS FLOAT) 
	                       
	                       * FI.Installation_Price_Per_Play
	                   ) / 100
	               ),
	               0
	           ) AS RDCHandpay,
	           (CC.Collection_RDC_Tickets_In) AS RDC_Tickets_In,
	           (CC.Collection_RDC_Tickets_Out) AS RDC_Tickets_Out,
	           (CC.Collection_MeterHandpay) AS MeterHandpay,
	           (CC.Collection_Ticket) AS Ticket,
	           (
	               CC.Collection_RDC_Take - COALESCE(
	                   (
	                       (
	                           CAST(C.collection_rdc_jackpot AS FLOAT) 
	                           
	                           * FI.Installation_Price_Per_Play
	                       ) / 100
	                   ),
	                   0
	               )
	           ) AS RDC_Take,
	           CC.Collection_RDC_Coins AS RDC_Coins,
	           CC.Collection_HopperChange AS Hopperchange,
	           CC.Collection_RDC_Coins_out AS RDC_Coins_Out,
	           (
	               CAST(ISNULL(C.Promo_Cashable_EFT_In, 0) AS FLOAT) 
	               
	               +
	               
	               CAST(ISNULL(C.NonCashable_EFT_In, 0) AS FLOAT) 
	               
	               +
	               
	               CAST(ISNULL(C.Cashable_EFT_In, 0) AS FLOAT)
	           ) AS EftIn,
	           (
	               CAST(ISNULL(C.Promo_Cashable_EFT_Out, 0) AS FLOAT) 
	               
	               +
	               
	               CAST(ISNULL(C.NonCashable_EFT_Out, 0) AS FLOAT) 
	               
	               +
	               
	               CAST(ISNULL(C.Cashable_EFT_Out, 0) AS FLOAT)
	           ) AS EFTOut,
	           CC.Collection_Net_Coin AS Net_Coin,
	           CC.Collection_Coin_Var AS Coin_Var,
	           CAST(
	               ISNULL(
	                   CAST(C.COLLECTION_RDC_TICKETS_PRINTED_VALUE AS FLOAT) / 
	                   100,
	                   0
	               ) AS FLOAT
	           ) AS COLLECTION_RDC_TICKETS_PRINTED_VALUE,
	           CAST(
	               ISNULL(
	                   CAST(C.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE AS FLOAT) / 
	                   100,
	                   0
	               ) AS FLOAT
	           ) AS RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
	           CC.Collection_Cash_Take,
	           CC.Collection_RDC_Take,
	           COALESCE(
	               (
	                   (
	                       CAST(C.collection_rdc_jackpot AS FLOAT) * FI.Installation_Price_Per_Play
	                   ) / 100
	               ),
	               0
	           ) AS Jackpot,
	           CC.Collection_Refills,
	           COALESCE(
	               (
	                   (
	                       CAST(C.progressive_win_handpay_value AS FLOAT) * FI.Installation_Price_Per_Play
	                   ) / 100
	               ),
	               0
	           ) AS progressive_win_handpay_value,
	           (
	               COALESCE(
	                   (
	                       (
	                           CAST(C.collection_rdc_jackpot AS FLOAT) * FI.Installation_Price_Per_Play
	                       ) / 100
	                   ),
	                   0
	               )
	           ) +(
	               COALESCE(
	                   (
	                       (
	                           CAST(C.collection_rdc_handpay AS FLOAT) * FI.Installation_Price_Per_Play
	                       ) / 100
	                   ),
	                   0
	               )
	           ) AS JackPotHandPay,
	           CAST(ISNULL(CAST(C.COLLECTION_RDC_TICKETS_INSERTED_VALUE AS FLOAT)/100, 0) AS FLOAT) AS COLLECTION_RDC_TICKETS_INSERTED_VALUE,
	           CAST(ISNULL(CAST(C.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT)/100, 0) AS FLOAT) AS RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
	    FROM   [COLLECTION] C WITH(NOLOCK)
	           INNER JOIN COLLECTION_CALCS CC WITH(NOLOCK)
	                ON  CC.COLLECTION_ID = C.COLLECTION_ID
	           INNER JOIN Batch b
	                ON  b.Batch_ID = c.Batch_ID
	           INNER JOIN FilteredInstallation FI
	                ON  FI.installation_id = C.Installation_ID
	                AND b.Batch_ID = @BatchId
	), 
	
	 --PromoCashable 
	
	PromoCashable AS
	
   (SELECT CAST(SUM(CT.CT_Value) AS FLOAT) AS iAmount ,
   COUNT(CT.CT_Barcode) As TicketCount,  
   
  CT.CT_Inserted_Collection_ID           
 
  FROM COLLECTION_TICKET CT 
  
  WHERE 
  CT.CT_TicketType = 0    
  AND CT.CT_VoucherStatus <> 'LT'                   
 
  AND ISNULL(CT.CT_IsPromotionalTicket,0)=1
  GROUP BY CT_Inserted_Collection_ID),          
              
   --PromoNonCashable 
   PromoNonCashable AS
  (SELECT CAST(SUM(CT.CT_Value) AS FLOAT) AS iAmount ,
   COUNT(CT.CT_Barcode) As TicketCount,  
   
  CT.CT_Inserted_Collection_ID          
 
  FROM COLLECTION_TICKET CT 
  
  WHERE 
  CT.CT_TicketType = 1    
  AND CT.CT_VoucherStatus <> 'LT'                   
 
  AND ISNULL(CT.CT_IsPromotionalTicket,0)=1
  GROUP BY CT_Inserted_Collection_ID),  
	
	VOID AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type = 'VOID'
	    GROUP BY
	           CD.Collection_ID
	), 
	
	Refund AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type = 'Refund'
	    GROUP BY
	           CD.Collection_ID
	), 
	
	EXPIRED AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type = 'EXPIRED'
	    GROUP BY
	           CD.Collection_ID
	), 
	
	PROGRESSIVE AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('PROGRESSIVE', 'PROG')
	    GROUP BY
	           CD.Collection_ID
	), 
	
	ShortPay AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('Shortpay', 'Offline Voucher-Shortpay')
	    GROUP BY
	           CD.Collection_ID
	), 
	
	AttendantPay AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('handpay credit', 'handpay jackpot', 
	                            'mystery jackpot', 'Attendantpay Credit', 
	                            'Attendantpay Jackpot', 'PROGRESSIVE', 'PROG')
	    GROUP BY
	           CD.Collection_ID
	), 
	
	DecEftIn AS 
	
	
	
	(
	    SELECT Aft.Collection_No,
	           SUM(
	               ISNULL(Aft.Promo_Cashable_EFT_OUT, 0) + ISNULL(Aft.NonCashable_EFT_OUT, 0) 
	               + ISNULL(Aft.WAT_Out, 0)
	           ) AS Amount
	    FROM   AFT_TRANSACTIONS Aft WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = Aft.Collection_No
	    WHERE  Transaction_Type = 'WithDrawal Complete'
	    GROUP BY
	           Aft.Collection_No
	), 
	
	DecEftOut AS 
	
	
	
	(
	    SELECT Aft.Collection_No,
	           SUM(
	               ISNULL(Aft.Promo_Cashable_EFT_OUT, 0) + ISNULL(Aft.NonCashable_EFT_OUT, 0) 
	               + ISNULL(Aft.WAT_Out, 0)
	           ) AS Amount
	    FROM   AFT_TRANSACTIONS Aft WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = Aft.Collection_No
	    WHERE  Transaction_Type = 'Deposit Complete'
	    GROUP BY
	           Aft.Collection_No
	)
	
	SELECT MAX(BatchDate) AS BatchDate,
	       MAX(Batch_Date_performed) AS Batch_Date_performed,
	       MAX(Batch_Time_performed) AS Batch_Time_performed,
	       MAX(Batch_Name) AS Batch_Name,
	       MAX(Batch_ID) AS Batch_ID,
	       MAX(BatchRef) AS BatchRef,
	       COUNT(CD.Collection_ID) AS BatchCount,
	       SUM(CD.CashCollected) AS CashCollected,
	       SUM(Declared_Notes) AS Notes,
	       SUM(
	           (CAST(ISNULL(TicktesIn, 0) AS FLOAT)) 
	           
	           -(
	               (CAST(ISNULL(TicktesOut, 0) AS FLOAT)) 
               
	               -ISNULL(VD.Amount, 0)
	           )
	       ) AS DecTicketBalance,
	       SUM(Coins) AS Coins,
	       SUM(TicktesIn) AS TicktesIn,
	       SUM(TicktesOut + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN ISNULL(SP.Amount, 0) ELSE 0 END)) AS TicktesOut,
	       SUM((Net_Coin - CAST(ISNULL(Rf.Amount, 0) AS REAL))) AS Net_Coin,
	       SUM(Refills) AS Refills,
	       SUM(CAST(ISNULL(Rf.Amount, 0) AS REAL)) AS Refunds,
	       SUM(
	           CASE 
	                WHEN (@SGVI_Enabled = 'True') THEN (JackPotHandPay)
	                ELSE ISNULL(AP.Amount, 0)
	           END
	       ) AS Handpay,
	       SUM(CAST(ISNULL(SP.Amount, 0) AS REAL)) AS Shortpay,
	       SUM(NotesVar) AS NotesVar,
	       SUM(Coin_Var - CAST(ISNULL(Rf.Amount, 0) AS REAL)) AS CoinVar,
	       
	       (SUM((CAST(ISNULL(TicktesIn, 0) AS FLOAT)) - 
        ((COLLECTION_RDC_TICKETS_INSERTED_VALUE) + (CD.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE)))               
      - SUM((CAST(ISNULL(TicktesOut, 0) AS FLOAT)  + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN ISNULL(SP.Amount, 0) ELSE 0 END)   
      - ISNULL(VD.Amount, 0)-(COLLECTION_RDC_TICKETS_PRINTED_VALUE  + RDC_TICKETS_PRINTED_NONCASHABLE_VALUE ))))  AS TicketVar,     
      
	       
	     
	       SUM(Handpay_Var) AS HandpayVar,
	       SUM(
	           (
	               Collection_Cash_Take -((Collection_RDC_Take) - Jackpot) + 
	               Collection_Refills 
	               
	               + ISNULL(VD.Amount, 0) 
	               
	               - ISNULL(SP.Amount, 0) 
	               
	               - CAST(ISNULL(Rf.Amount, 0) AS REAL)
	           )
	       ) AS Take_Var,
	       SUM(RDCRefill) AS RDCRefill,
	       SUM(MeterCash) AS MeterCash,
	       SUM(MeterRefill) AS MeterRefill,
	       SUM(MeterVar) AS MeterVar,
	       SUM(RDC_Notes) AS RDC_Notes,
	       MAX(BatchAdj) AS BatchAdj,
	       SUM(
	           CASE 
	                WHEN (@SGVI_Enabled = 'True') THEN (JackPotHandPay)
	                ELSE ISNULL(AP.Amount, 0)
	           END
	       ) AS DecHandpay,
	       SUM(RDCHandpay) AS RDCHandpay,
	       SUM(RDC_Tickets_In) AS RDC_Tickets_In,
	       SUM(RDC_Tickets_Out) AS RDC_Tickets_Out,
	       SUM(MeterHandpay) AS MeterHandpay,
	       SUM(Ticket) AS Ticket,
	       SUM(RDC_Take) AS RDC_Take,
	       SUM(RDC_Coins) AS RDC_Coins,
	       SUM(Hopperchange) AS Hopperchange,
	       SUM(RDC_Coins_Out) AS RDC_Coins_Out,
	       SUM(ISNULL(VD.Amount, 0)) AS Void,
	       SUM(ISNULL(Ex.Amount, 0)) AS Expired,
	       SUM(ISNULL(P.Amount, 0)) AS Progressive_Value_Declared,
	       SUM(ISNULL(P.Amount, 0) - progressive_win_handpay_value) AS 
	       Progressive_Value_Variance,
	       SUM(EftIn) AS EftIn,
	       SUM(EftOut) AS EftOut,
	       SUM(Declared_Notes) + SUM(
	           (CAST(ISNULL(TicktesIn, 0) AS FLOAT)) 
	           
	           -(
	               (CAST(ISNULL(TicktesOut, 0) AS FLOAT)) 
	               
	               + ISNULL(SP.Amount, 0) 
	               
	               -ISNULL(VD.Amount, 0)
	           )
	       ) 
	       
	       - SUM(
	           CASE 
	                WHEN (@SGVI_Enabled = 'True') THEN (JackPotHandPay)
	                ELSE ISNULL(AP.Amount, 0)
	           END
	       ) 
	       
	       + SUM(Net_Coin) AS Cash_Take,
	       (
	           SUM(Declared_Notes) + SUM(
	               (CAST(ISNULL(TicktesIn, 0) AS FLOAT)) 
	               
	               -(
	                   (CAST(ISNULL(TicktesOut, 0) AS FLOAT)) 
	                   
	                   + ISNULL(SP.Amount, 0) 
	                   
	                   -ISNULL(VD.Amount, 0)
	               )
	           ) - SUM(
	               CASE 
	                    WHEN (@SGVI_Enabled = 'True') THEN (JackPotHandPay)
	                    ELSE ISNULL(AP.Amount, 0)
	               END
	           ) + SUM(Net_Coin)
	       ) -(
	           SUM(RDC_Notes) + (
	               SUM(
	                   (CAST(ISNULL(TicktesIn, 0) AS FLOAT)) 
	                   
	                   -(
	                       (CAST(ISNULL(TicktesOut, 0) AS FLOAT)) 
	                       
	                       + ISNULL(SP.Amount, 0) 
	                       
	                       -ISNULL(VD.Amount, 0)
	                   )
	               ) - SUM(
	                   (
	                       CAST(ISNULL(TicktesOut, 0) AS FLOAT) 
	                       
	                       + ISNULL(SP.Amount, 0) 
	                       
	                       - ISNULL(VD.Amount, 0) 
	                       
	                       -(
	                           COLLECTION_RDC_TICKETS_PRINTED_VALUE 
	                           
	                           + RDC_TICKETS_PRINTED_NONCASHABLE_VALUE
	                       )
	                   )
	               )
	           ) - SUM(RDCHandpay) + SUM(RDC_Coins)
	       ) AS WinLossVar,
	       SUM(ISNULL(EftIn.Amount, 0)) AS DecEftIn,
	       SUM(ISNULL(EftOut.Amount, 0)) AS DecEftOut,
	       SUM(ISNULL(PC.iAmount,0)) AS PromoCashableAmount,
	      -- MAX(ISNULL(PC.BarcodeCount,0)) AS PromoCashableCount,
	       SUM(ISNULL(PNC.iAmount,0)) AS PromoNonCashableAmount
--MAX(ISNULL(PNC.BarcodeCount,0)) AS PromoCashableCount
	FROM   CollectionDetails CD
	       LEFT OUTER JOIN VOID VD
	            ON  CD.Collection_ID = VD.Collection_ID
	       LEFT OUTER JOIN Refund Rf
	            ON  CD.Collection_ID = Rf.Collection_ID
	       LEFT OUTER JOIN Expired Ex
	            ON  CD.Collection_ID = Ex.Collection_ID
	       LEFT OUTER JOIN ShortPay SP
	            ON  CD.Collection_ID = SP.Collection_ID
	       LEFT OUTER JOIN AttendantPay AP
	            ON  CD.Collection_ID = AP.Collection_ID
	       LEFT OUTER JOIN PROGRESSIVE P
	            ON  CD.Collection_ID = P.Collection_ID
	       LEFT OUTER JOIN DecEftIn EftIn
	            ON  CD.Collection_ID = EftIn.Collection_No
	       LEFT OUTER JOIN DecEftOut EftOut
	            ON  CD.Collection_ID = EftOut.Collection_No
	       LEFT OUTER JOIN PromoCashable PC
				ON PC.CT_Inserted_Collection_ID=CD.Collection_ID
		   LEFT OUTER JOIn PromoNonCashable PNC
				ON PNC.CT_Inserted_Collection_ID=CD.Collection_ID	
END  

GO

