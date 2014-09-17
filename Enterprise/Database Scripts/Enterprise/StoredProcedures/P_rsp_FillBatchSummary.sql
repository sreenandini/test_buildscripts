 /*  
 * this stored procedure is to Fetch the details regarding the Collection Batch  
 *  
 * Change History:  
 *  
 * Sudarsan S   20-07-2009  created  
 * GBabu    29-11-2010  Modified  
*/  
USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FillBatchSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FillBatchSummary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_FillBatchSummary] 
	@Batch_ID INT
AS
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
	
	
	
	SELECT B.Batch_ID,
	       MAX(B.Batch_Ref) AS BatchRef,
	       MAX(B.Batch_Date) AS BatchDate,
	       MAX(B.Batch_Date_Performed) AS Batch_Date_Performed,
	       MAX(B.Batch_Time) AS Batch_Time,
	       MAX(B.Batch_User_Name) AS Batch_User_Name,
	       SUM(CC.Collection_Declared_Coins) AS Declared_Coins,
	       SUM(CC.Collection_Defloat) AS Defloat,
	       SUM(CC.Collection_Refills) AS Refills,
	       SUM(VW.RDC_Notes) AS RDC_Notes,
	       SUM(
	           CAST(
	               COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Refund'), 0) 
	               AS REAL
	           )
	       ) AS Refunds,
	       SUM(COALESCE(VW.Progressive_Value_Declared, 0)) AS 
	       Progressive_Value_Declared,
	       (
	           SUM(
	               COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Shortpay'), 0)
	           ) 
	           + SUM(
	               COALESCE(
	                   dbo.fnGetTreasuryAmount(C.Collection_ID, 'Offline Voucher-ShortPay'),
	                   0
	               )
	           )
	       ) AS Shortpay,
	       SUM(VW.Declared_Notes) AS Declared_Notes,
	       SUM(
	              VW.DecTicketBalance + (
	                  CASE 
	                       WHEN ISNULL(@sAddShortpay, 'True') = 'True' THEN 0
	                       ELSE (COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Shortpay'), 0)) 
	           + (COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Offline Voucher-ShortPay'),0))
	                  END
	              )
	          ) AS DecTicketBalance,
	       SUM(VW.DecHandpay) AS DecHandpay,
	       SUM(VW.Net_Coin) AS Net_Coin,
	       SUM(COALESCE(CC.Collection_Ticket_Balance, 0)) AS Ticket_Balance,
	       SUM(VW.Ticket_Var + (
	                  CASE 
	                       WHEN ISNULL(@sAddShortpay, 'True') = 'True' THEN 0
	                       ELSE (COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Shortpay'), 0)) 
	           + (COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Offline Voucher-ShortPay'),0))
	                  END
	              )) AS Ticket_Var,
	       SUM(VW.RDCHandpay) AS RDCHandpay,
	       SUM(VW.RDC_Coins) AS RDC_Coins,
	       SUM(VW.RDC_Coins_Out) AS RDC_Coins_Out,
	       SUM(VW.Handpay_Var) Handpay_Var,
	       SUM(
	           (
	               (CC.Collection_Coin_Var) 
	               - 
	               CAST(
	                   COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Refund'), 0) 
	                   AS REAL
	               )
	           )
	       ) AS Coin_Var,
	       SUM(CC.Collection_Note_Var) AS Note_Var,
	       SUM(
	           COALESCE(VW.Progressive_Value_Declared, 0) 
	           - 
	           COALESCE(
	               (
	                   (
	                       CAST(C.progressive_win_handpay_value AS FLOAT) * I.Installation_Price_Per_Play
	                   ) / 100
	               ),
	               0
	           )
	       ) AS Progressive_Value_Variance,
	       SUM(CC.Collection_PercentageIn) AS PercentageIn,
	       SUM(CC.Collection_PercentageOut) AS PercentageOut,
	       SUM((CC.Collection_VTP) / 10) AS Handle,
	       COUNT(C.Collection_ID) AS BatchCount,
	       SUM(VW.EftIn) AS EftIn,
	       SUM(VW.DecEftIn) AS DecEftIn,
	       SUM(VW.Eftout) AS EftOut,
	       SUM(VW.DecEftout) AS DecEftOut,
	       SUM(ISNULL(C.Mystery_Attendant_Paid, 0)) AS Mystery_Attendant_Paid ,
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
	       SUM(RDC_Notes + (DecTicketBalance- Ticket_Var) -
	       RDCHandpay + RDC_Coins + (
	           CASE 
	                WHEN @IsAFTIncludedInCalculation = 1 THEN (EftIn / 100.00) 
	                     -(EftOut / 100.00)
	                ELSE 0
	           END
	       )) AS MeterWinOrLoss,
	       --nCasino ( (HANDLE-MeterWinOrLoss)/HANDLE  ) *100)
	       AVG(CASE 
	            WHEN VW.Handle > 0 THEN (
	                     (
	                         VW.Handle -
	                         --START MeterWinOrLoss
	                         (
	                             RDC_Notes + (DecTicketBalance + (
	                  CASE 
	                       WHEN ISNULL(@sAddShortpay, 'True') = 'True' THEN 0
	                       ELSE (COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Shortpay'), 0)) 
	           + (COALESCE(dbo.fnGetTreasuryAmount(C.Collection_ID, 'Offline Voucher-ShortPay'),0))
	                  END
	                             )-Ticket_Var) -
	                             RDCHandpay + RDC_Coins + (
	                                 CASE 
	                                      WHEN @IsAFTIncludedInCalculation = 1 THEN (VW.EftIn / 100.00) 
	                                           -(VW.EftOut / 100.00)
	                                      ELSE 0
	                                 END
	                             )
	                         )--END MeterWinOrLoss
	                     ) / VW.Handle
	                 ) 
	                 * 100.00
	            ELSE 0.00
	       END ) AS nCasino, 
	       MAX(B.Batch_Name) AS RouteName
	       
	FROM   dbo.Collection C
	       INNER JOIN dbo.Collection_Calcs CC
	            ON  C.Collection_ID = CC.Collection_ID
	       INNER JOIN dbo.VW_CollectionData VW
	            ON  C.Collection_ID = VW.Collection_ID
	       INNER JOIN dbo.Installation I
	            ON  C.Installation_ID = I.Installation_ID
	       INNER JOIN dbo.Batch B
	            ON  C.Batch_ID = B.Batch_ID
	WHERE  B.Batch_ID = @Batch_ID
	       AND VW.Batch_ID = @Batch_ID
	GROUP BY
	       B.Batch_ID
END
GO