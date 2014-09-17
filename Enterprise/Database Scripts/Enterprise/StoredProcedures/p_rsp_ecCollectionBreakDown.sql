USE Enterprise
GO 

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_ecCollectionBreakDown'
   )
    DROP PROCEDURE dbo.rsp_ecCollectionBreakDown
GO

--rsp_ecCollectionBreakDown 4,1
CREATE PROCEDURE dbo.rsp_ecCollectionBreakDown
	@Collection_ID INT,
	@Site_id INT
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------
Durga Devi M		Add Promo Columns								Dec 20 2013
*****************************************************************************************************/

BEGIN
	DECLARE @bSVGIEnabled                VARCHAR(20) 
	DECLARE @Region                      VARCHAR(20)
	DECLARE @sAddShortpay                VARCHAR(20)
	DECLARE @Auto_Declare_Monies         VARCHAR(20)
	DECLARE @IsAFTIncludedInCalculation  VARCHAR(20) 
	
	
	DECLARE @nTicketVoid                 FLOAT
	DECLARE @nShortPay                   FLOAT
	DECLARE @Prog                        FLOAT
	DECLARE @nEftIn                      FLOAT
	DECLARE @nEFTOut                     FLOAT
	
	DECLARE @PromoCashableIn AS MONEY
	DECLARE @PromoNonCashableIn AS MONEY
	
	
	--SETTINGS-------------------------------------------------------------------------------------
	
	EXEC rsp_GetSetting 0,
	     'SGVI_Enabled',
	     'False',
	     @bSVGIEnabled OUT 
	
	EXEC rsp_GetSetting 0,
	     'Auto_Declare_Monies',
	     'False',
	     @Auto_Declare_Monies OUT
	
	EXEC rsp_GetSetting 0,
	     'IsAFTIncludedInCalculation',
	     'False',
	     @IsAFTIncludedInCalculation OUT
	
	
	
	SELECT @Region = Region
	FROM   [Site] s
	WHERE  s.Site_ID = @Site_id
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'
	
	--SELECT @sAddShortpay setting 
	
	--SETTINGS-------------------------------------------------------------------------------------- 
	
	-- DECLARE @Installation_Price_Per_Play  INT
	-- DECLARE @Installation_Token_Value     INT
	
SELECT @PromoCashableIn=SUM(ISNULL(CT.CT_Value	,0.0)) 
FROM Collection_Ticket CT where CT.CT_Inserted_Collection_ID=@Collection_ID
AND ISNULL(CT.CT_IsPromotionalTicket,0) = 1
AND  CT.CT_TicketType=0
	     
	 
	 --print @PromoCashableIn
	     
	     
SELECT @PromoNonCashableIn=SUM(ISNULL(CT.CT_Value	,0.0)) 
FROM Collection_Ticket CT where CT.CT_Inserted_Collection_ID=@Collection_ID
AND ISNULL(CT.CT_IsPromotionalTicket,0) = 1
AND  CT.CT_TicketType=1	
	
	--print @PromoNonCashableIn
	
	
	
	--EFT
	SELECT @nEFTOut = dbo.GetEftOUT(@Collection_ID) /100.00
	SELECT @nEftIn = dbo.GetEftIN(@Collection_ID)/100.00
	
	--SHORT PAY 
	SELECT @nShortPay = ISNULL(SUM(Treasury_Amount), 0)
	FROM   Treasury_Entry
	WHERE  Collection_ID = @Collection_ID
	       AND Treasury_Type IN ('Shortpay', 'Offline Voucher-Shortpay')
	
	--VOID 
	SELECT @nTicketVoid = ISNULL(SUM(Treasury_Amount), 0)
	FROM   Treasury_Entry
	WHERE  Collection_ID = @Collection_ID
	       AND Treasury_Type = 'Void'
	
	--PROG 
	SELECT @Prog = SUM(Treasury_Amount)
	FROM   Treasury_Entry
	WHERE  (Treasury_Type IN ('Prog', 'Progressive'))
	       AND Collection_ID = @Collection_ID
	
	-- SELECT @Installation_Price_Per_Play= Installation_Price_Per_Play FROM Installation i WHERE i.Installation_ID = 1
	
	--Float recovered
	
	DECLARE @FR_COL_TOTAL       FLOAT 
	DECLARE @FR_COL_20          FLOAT
	DECLARE @FR_COL_10          FLOAT
	DECLARE @FR_COL_5           FLOAT
	DECLARE @FR_COL_2           FLOAT
	DECLARE @FR_COL_1           FLOAT
	DECLARE @FR_COL_TOTALCOINS  FLOAT
	
	SELECT @FR_COL_TOTAL = ABS(ISNULL(T.Treasury_Amount, 0)),
	       @FR_COL_20 = ABS(ISNULL(T.Treasury_Breakdown_2000p, 0)),
	       @FR_COL_10 = ABS(ISNULL(T.Treasury_Breakdown_1000p, 0)),
	       @FR_COL_5 = ABS(ISNULL(T.Treasury_Breakdown_500p, 0)),
	       @FR_COL_2 = ABS(ISNULL(T.Treasury_Breakdown_200p, 0)),
	       @FR_COL_1 = ABS(ISNULL(T.Treasury_Breakdown_100p, 0)),
	       @FR_COL_TOTALCOINS = ABS(ISNULL(T.Treasury_Breakdown_50p, 0)) +
	       ABS(ISNULL(T.Treasury_Breakdown_20p, 0)) +
	       ABS(ISNULL(T.Treasury_Breakdown_10p, 0)) +
	       ABS(
	           ISNULL(T.Treasury_Breakdown_5p, 0) + ISNULL(T.Treasury_Breakdown_2p, 0)
	       )
	FROM   Treasury_Entry T
	WHERE  Collection_ID = @Collection_ID
	       AND Treasury_Type = 'Defloat'
	
	
	--Refills and Refunds
	DECLARE @RF_Amount  FLOAT 
	DECLARE @RF_t2000p  FLOAT
	DECLARE @RF_t1000p  FLOAT
	DECLARE @RF_t500p   FLOAT
	DECLARE @RF_t200p   FLOAT
	DECLARE @RF_t100p   FLOAT
	DECLARE @RF_t50p    FLOAT
	DECLARE @RF_t20p    FLOAT
	DECLARE @RF_t10p    FLOAT
	DECLARE @RF_t5p     FLOAT
	DECLARE @RF_t2p     FLOAT
	
	SELECT @RF_Amount = SUM(Treasury_Amount),
	       @RF_t2000p = SUM(Treasury_Breakdown_2000p),
	       @RF_t1000p = SUM(Treasury_Breakdown_1000p),
	       @RF_t500p = SUM(Treasury_Breakdown_500p),
	       @RF_t200p = SUM(Treasury_Breakdown_200p),
	       @RF_t100p = SUM(Treasury_Breakdown_100p),
	       @RF_t50p = SUM(Treasury_Breakdown_50p),
	       @RF_t20p = SUM(Treasury_Breakdown_20p),
	       @RF_t10p = SUM(Treasury_Breakdown_10p),
	       @RF_t5p = SUM(Treasury_Breakdown_5p),
	       @RF_t2p = SUM(Treasury_Breakdown_2p)
	FROM   Treasury_Entry
	WHERE  (Treasury_Type = 'Refill')
	       AND Collection_ID = @Collection_ID
	GROUP BY
	       Collection_ID

	SELECT DISTINCT
		  
		   Batch.Batch_User_Name,
	       Batch.Batch_Date,
	       Batch.Batch_Time,
	       Batch.Batch_Date_Performed,
	       Batch.Batch_Ref,
	       c.Collection_Date_Of_Collection,
	       CASE 
	            WHEN C.Collection_Defloat_Collection = 1 THEN 'Final'
	            ELSE 'Standard'
	       END 
	       AS Collection_Defloat_Collection
	       ,
	       Bp.Bar_Position_Name,
	       ISNULL(I.Installation_ID,0) Installation_ID, -- Will not be null 
	       I.Installation_Price_Per_Play,
	       ISNULL(I.Installation_Counter_Cash_In_Units, 0) 
	       Installation_Counter_Cash_In_Units,
	       ISNULL(I.Installation_Counter_Cash_Out_Units, 0) 
	       Installation_Counter_Cash_Out_Units,
	       ISNULL(I.Installation_Counter_Refill_Units, 0) 
	       Installation_Counter_Refill_Units,
	       ISNULL(MC.Machine_Class_SP_Features, 0) Machine_Class_SP_Features,
	       ISNULL(MC.Machine_Name, '') Machine_Name,
	       ISNULL(MC.Machine_Name, '') AS GameName,
	       ISNULL(z.Zone_Name, '') Zone_Name,
	       CAST(C.Collection_Treasury_Handpay AS FLOAT) AS 
	       Collection_Treasury_Handpay_float,
	       ---
	       -------nCoinsOut-----------------------------------------------------------------
	       CASE 
	            WHEN @Region = 'UK' THEN ISNULL(C.CASH_OUT_200p, 0) * 2 +
	                 ISNULL(C.CASH_OUT_100p, 0) +
	                 ISNULL(C.CASH_OUT_50p, 0) / 2.00 +
	                 ISNULL(C.CASH_OUT_20p, 0) / 5.00 +
	                 ISNULL(C.CASH_OUT_10p, 0) / 10.00 +
	                 ISNULL(C.CASH_OUT_5p, 0) / 20.00 +
	                 ISNULL(C.CASH_OUT_2p, 0) / 50.00 +
	                 ISNULL(C.CASH_OUT_1p, 0) /100.00
	            WHEN @Region = 'AR' THEN ISNULL(C.CASH_OUT_100p, 0) +
	                 ISNULL(C.CASH_OUT_50p, 0) / 2 +
	                 ISNULL(C.CASH_OUT_20p, 0) / 5 +
	                 ISNULL(C.CASH_OUT_10p, 0) / 10 +
	                 ISNULL(C.CASH_OUT_5p, 0) / 20 +
	                 ISNULL(C.CASH_OUT_2p, 0) / 50 +
	                 ISNULL(C.CASH_OUT_1p, 0)
	            ELSE ISNULL(C.CASH_OUT_50p, 0) / 2.00 +
	                 ISNULL(C.CASH_OUT_20p, 0) / 5.00 +
	                 ISNULL(C.CASH_OUT_10p, 0) / 10.00 +
	                 ISNULL(C.CASH_OUT_5p, 0) / 20.00 +
	                 ISNULL(C.CASH_OUT_2p, 0) / 50.00 +
	                 ISNULL(C.CASH_OUT_1p, 0) /100.00
	       END AS nCoinsOut,
	       -------------------------------------------------------------------------------------
	       --RDCCash
	       (ISNULL(C.CASH_IN_1P, 0) - ISNULL(C.CASH_OUT_1P, 0))/100.00 + (
	           (ISNULL(C.CASH_IN_2P, 0) - ISNULL(C.CASH_OUT_2P, 0)) / 50.00
	       ) + (
	           (ISNULL(C.CASH_IN_5P, 0) - ISNULL(C.CASH_OUT_5P, 0)) / 20.00
	       ) + (
	           (ISNULL(C.CASH_IN_10P, 0) - ISNULL(C.CASH_OUT_10P, 0)) / 10.00
	       ) + (
	           (ISNULL(C.CASH_IN_20P, 0) - ISNULL(C.CASH_OUT_20P, 0)) / 5.00
	       ) + (
	           (ISNULL(C.CASH_IN_50P, 0) - ISNULL(C.CASH_OUT_50P, 0)) / 2.00
	       ) + ((ISNULL(C.CASH_IN_100P, 0) - ISNULL(C.CASH_OUT_100P, 0))) + (
	           (ISNULL(C.CASH_IN_200P, 0) - ISNULL(C.CASH_OUT_200P, 0)) * 2
	       ) + (
	           (ISNULL(C.CASH_IN_500P, 0) - ISNULL(C.CASH_OUT_500P, 0)) * 5
	       ) + (
	           (ISNULL(C.CASH_IN_1000P, 0) - ISNULL(C.CASH_OUT_1000P, 0)) * 10
	       ) + (
	           (ISNULL(C.CASH_IN_2000P, 0) - ISNULL(C.CASH_OUT_2000P, 0)) * 20
	       ) + (
	           (ISNULL(C.CASH_IN_5000P, 0) - ISNULL(C.CASH_OUT_5000P, 0)) * 50
	       ) + (
	           (ISNULL(C.CASH_IN_10000P, 0) - ISNULL(C.CASH_OUT_10000P, 0)) *
	           100
	       ) + (
	           (ISNULL(C.CASH_IN_20000P, 0) - ISNULL(C.CASH_OUT_20000P, 0)) *
	           200
	       ) + (
	           (ISNULL(C.CASH_IN_50000P, 0) - ISNULL(C.CASH_OUT_50000P, 0)) *
	           500
	       ) + (
	           (
	               ISNULL(C.CASH_IN_100000P, 0) - ISNULL(C.CASH_OUT_100000P, 0)
	           ) * 1000
	       ) AS RDCCash,
	       -------------------------------------------------------------------------------------
	       --RDCCashIn 
	       (ISNULL(C.CASH_IN_1P,0)/100.00) + (ISNULL(C.CASH_IN_2P, 0) / 50.00) + (ISNULL(C.CASH_IN_5P, 0) / 20.00) 
	       + (ISNULL(C.CASH_IN_10P, 0) / 10.00) + (ISNULL(C.CASH_IN_20P, 0) / 5.00) 
	       + (ISNULL(C.CASH_IN_50P, 0) / 2.00) 
	       + (ISNULL(C.CASH_IN_100P, 0)) + (ISNULL(C.CASH_IN_200P, 0) * 2) + (ISNULL(C.CASH_IN_500P, 0) * 5) 
	       + (ISNULL(C.CASH_IN_1000P, 0) * 10) + (ISNULL(C.CASH_IN_2000P, 0) * 20) 
	       + (ISNULL(C.CASH_IN_5000P, 0) * 50) 
	       + (ISNULL(C.CASH_IN_10000P, 0) * 100) + (ISNULL(C.CASH_IN_20000P, 0) * 200) 
	       + (ISNULL(C.CASH_IN_50000P, 0) * 500) + (ISNULL(C.CASH_IN_100000P, 0) * 1000) AS 
	       RDCCashIn,
	       -------------------------------------------------------------------------------------
	       --RDCCashOut
	       (ISNULL(C.CASH_OUT_1P, 0) /100.00) + (ISNULL(C.CASH_OUT_2P, 0) / 50.00) + (ISNULL(C.CASH_OUT_5P, 0) / 20.00) 
	       + (ISNULL(C.CASH_OUT_10P, 0) / 10.00) + (ISNULL(C.CASH_OUT_20P, 0) / 5.00) 
	       + (ISNULL(C.CASH_OUT_50P, 0) / 2.00) +
	       ISNULL(C.CASH_OUT_100P, 0) + (ISNULL(C.CASH_OUT_200P, 0) * 2) + (ISNULL(C.CASH_OUT_500P, 0) * 5) 
	       + (ISNULL(C.CASH_OUT_1000P, 0) * 10) + (ISNULL(C.CASH_OUT_2000P, 0) * 20) 
	       + (ISNULL(C.CASH_OUT_5000P, 0) * 50) 
	       + (ISNULL(C.CASH_OUT_10000P, 0) * 100) + (ISNULL(C.CASH_OUT_20000P, 0) * 200) 
	       + (ISNULL(C.CASH_OUT_50000P, 0) * 500) + (ISNULL(C.CASH_OUT_100000P, 0) * 1000) AS 
	       RDCCashOut,
	       -------------------------------------------------------------------------------------
	       --Collections
	       (ISNULL(Cash_Collected_100000p, 0) / 100.00) + (ISNULL(Cash_Collected_50000p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_20000p, 0) / 100.00) + (ISNULL(Cash_Collected_10000p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_5000p, 0) / 100.00) + (ISNULL(Cash_Collected_2000p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_1000p, 0) / 100.00) + (ISNULL(Cash_Collected_500p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_200p, 0) / 100.00) + (ISNULL(Cash_Collected_100p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_50p, 0) / 100.00) + (ISNULL(Cash_Collected_20p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_10p, 0) / 100.00) + (ISNULL(Cash_Collected_5p, 0) / 100.00)
	       + (ISNULL(Cash_Collected_2p, 0) / 100.00) +(ISNULL(Cash_Collected_1p, 0) / 100.00) + (ISNULL(DeclaredTicketValue, 0)) AS 
	       Collections,
	       C.CashCollected,
	       ISNULL(Cash_Collected_100000p, 0) / 100.00 Cash_Collected_100000p,
	       ISNULL(Cash_Collected_50000p, 0) / 100.00 Cash_Collected_50000p,
	       ISNULL(Cash_Collected_20000p, 0) / 100.00 Cash_Collected_20000p,
	       ISNULL(Cash_Collected_10000p, 0) / 100.00 Cash_Collected_10000p,
	       ISNULL(Cash_Collected_5000p, 0) / 100 Cash_Collected_5000p,
	       ISNULL(Cash_Collected_2000p, 0) / 100 Cash_Collected_2000p,
	       ISNULL(Cash_Collected_1000p, 0) / 100 Cash_Collected_1000p,
	       ISNULL(Cash_Collected_500p, 0) / 100.00 Cash_Collected_500p,
	       ------------------------------------------------------------------------------------- 
	       CASE 
	            WHEN @Region = 'UK' THEN 0
	            WHEN @Region = 'AR' THEN ISNULL(Cash_Collected_200p, 0) / 100.00
	            ELSE ISNULL(Cash_Collected_200p, 0) / 100.00
	       END AS Cash_Collected_200p,
	       CASE 
	            WHEN @Region = 'UK' THEN 0
	            WHEN @Region = 'AR' THEN 0
	            ELSE ISNULL(Cash_Collected_100p, 0) / 100.00
	       END AS Cash_Collected_100p,
	       ------------------------------------------------------------------------------------- 
	       
	       --COL_COINSIN
	       CASE 
	            WHEN @Region = 'UK' THEN (ISNULL(Cash_Collected_200p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_100p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_50p, 0) / 100.00) + (ISNULL(Cash_Collected_20p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_10p, 0) / 100.00) + (ISNULL(Cash_Collected_5p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_2p, 0) / 100.00) +(ISNULL(Cash_Collected_1p, 0) / 100.00)
	            WHEN @Region = 'AR' THEN (ISNULL(Cash_Collected_100p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_50p, 0) / 100.00) + (ISNULL(Cash_Collected_20p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_10p, 0) / 100.00) + (ISNULL(Cash_Collected_5p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_2p, 0) / 100.00) + (ISNULL(Cash_Collected_1p, 0) / 100.00)
	            ELSE (ISNULL(Cash_Collected_50p, 0) / 100.00) + (ISNULL(Cash_Collected_20p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_10p, 0) / 100.00) + (ISNULL(Cash_Collected_5p, 0) / 100.00) 
	                 + (ISNULL(Cash_Collected_2p, 0) / 100.00)+ (ISNULL(Cash_Collected_1p, 0) / 100.00)
	       END AS COL_COINSIN,
	       -------------------------------------------------------------------------------------    
	       C.DeclaredTicketValue,
	       -------------------------------------------------------------------------------------    
	       CASE 
	            WHEN @sAddShortpay = 'true' THEN C.DeclaredTicketPrintedValue +
	                 @nShortPay 
	                 - @nTicketVoid
	            ELSE C.DeclaredTicketPrintedValue - @nTicketVoid
	       END AS COL_TICKETSOUT,
	       -------------------------------------------------------------------------------------
	       --COL_TICKETS
	       CASE 
	            WHEN @sAddShortpay = 'true' THEN C.DeclaredTicketValue -(C.DeclaredTicketPrintedValue + @nShortPay -@nTicketVoid)
	            ELSE C.DeclaredTicketValue -(C.DeclaredTicketPrintedValue -@nTicketVoid)
	       END AS  
	       COL_TICKETS,
	       -------------------------------------------------------------------------------------
	       --COL_PromoCashableIn
	       
	--       ISNULL((CASE WHEN ISNULL(CT.CT_IsPromotionalTicket,0) = 1 THEN 
	--        CASE WHEN CT.CT_TicketType=0 THEN
	--			SUM(CAST(ISNULL(CT.CT_Value,0.0) AS DECIMAL(18,2)))
	--		END
	--END),0)
	@PromoCashableIn AS COL_PromoCashableIn,
	---------------------------------------------------------------------------------------------
	--COL_PromoNonCashableIn
	
	--ISNULL((CASE WHEN ISNULL(CT.CT_IsPromotionalTicket,0) = 1 THEN 
	--        CASE WHEN CT.CT_TicketType=1 THEN
	--			SUM(CAST(ISNULL(CT.CT_Value,0.0) AS DECIMAL(18,2)))
	--		END
	--END),0) 
  @PromoNonCashableIn  AS COL_PromoNonCashableIn,
	       --------------------------------------------------------------------------------------
	       
	       
	       
	       
	       --COL_PROG
	       CASE 
	            WHEN (@bSVGIEnabled = 'True' AND @Auto_Declare_Monies = 'TRUE') THEN 
	                 ISNULL(C.Progressive_Value_Declared, 0.00)
	            ELSE ISNULL(@Prog, 0.00)
	       END COL_PROG,
	       -------------------------------------------------------------------------------------    
	       
	       @nEFTOut COL_EFTOUT,
	       @nEFTIn COL_EFT,
	       -- FLOAT REC 
	       ISNULL(@FR_COL_TOTAL, 0) FR_COL_TOTAL,
	       ISNULL(@FR_COL_20, 0) FR_COL_20,
	       ISNULL(@FR_COL_10, 0) FR_COL_10,
	       ISNULL(@FR_COL_5, 0) FR_COL_5,
	       ISNULL(@FR_COL_2, 0) FR_COL_2,
	       ISNULL(@FR_COL_1, 0) FR_COL_1,
	       ISNULL(@FR_COL_TOTALCOINS, 0) AS FR_COL_TOTALCOINS,
	       -------------------------------------------------------------------------------------
	       --Refills 	
	       (ISNULL(CASH_REFILL_1000P, 0) * 10.00) + (ISNULL(CASH_REFILL_500P, 0) * 5.00) 
	       + (ISNULL(CASH_REFILL_200P, 0) * 2.00)
	       + (ISNULL(CASH_REFILL_100P, 0)) + (ISNULL(CASH_REFILL_50P, 0) / 2.00) 
	       + (ISNULL(CASH_REFILL_20P, 0) / 5.00)
	       + (ISNULL(CASH_REFILL_10P, 0) / 10.00) + (ISNULL(CASH_REFILL_5P, 0) / 20.00) AS 
	       Refills,
	       (ISNULL(C.CASH_REFILL_100000P, 0) * 1000) AS RF_COL_1000,
	       (ISNULL(C.CASH_REFILL_50000P, 0) * 500) AS RF_COL_500,
	       (ISNULL(C.CASH_REFILL_20000P, 0) * 200) AS RF_COL_200,
	       (ISNULL(C.CASH_REFILL_10000P, 0) * 100) AS RF_COL_100,
	       (ISNULL(C.CASH_REFILL_5000P, 0) * 50) AS RF_COL_50,
	       ISNULL(@RF_t2000p, 0.00) + (C.CASH_REFILL_2000P * 20.00) AS RF_COL_20,
	       ISNULL(@RF_t1000p, 0.00) + (C.CASH_REFILL_1000P * 10.00) AS RF_COL_10,
	       ISNULL(@RF_t500p, 0.00) + (C.CASH_REFILL_500P * 5.00) AS RF_COL_5,
	       --
	       --     DECLARE @RF_Amount FLOAT
	       -- DECLARE @RF_t2000p FLOAT
	       -- DECLARE @RF_t1000p FLOAT
	       -- DECLARE @RF_t500p FLOAT
	       -- DECLARE @RF_t200p FLOAT
	       -- DECLARE @RF_t100p FLOAT
	       -- DECLARE @RF_t50p FLOAT
	       -- DECLARE @RF_t20p FLOAT
	       -- DECLARE @RF_t10p FLOAT
	       -- DECLARE @RF_t5p FLOAT
	       -- DECLARE @RF_t2p FLOAT
	       -- 
	       @nShortPay Short_Pay,
	       @nTicketVoid TicketVoid,
	       -------------------------------------------------------------------------------------
	       -- METER
	       -------------------------------------------------------------------------------------    	
	       (
	           (
	               ISNULL(C.CASH_IN_100000P, 0) - ISNULL(C.CASH_OUT_100000P, 0)
	           )
	       ) * 1000 AS RDC_COL_1000,
	       (
	           (ISNULL(C.CASH_IN_50000P, 0) - ISNULL(C.CASH_OUT_50000P, 0))
	       ) * 500 AS COL_500,
	       (
	           (ISNULL(C.CASH_IN_20000P, 0) - ISNULL(C.CASH_OUT_20000P, 0))
	       ) * 200 AS COL_200,
	       (
	           (ISNULL(C.CASH_IN_10000P, 0) - ISNULL(C.CASH_OUT_10000P, 0))
	       ) * 100 AS COL_100,
	       ((ISNULL(C.CASH_IN_5000P, 0) - ISNULL(C.CASH_OUT_5000P, 0))) * 50 AS 
	       COL_50,
	       ((ISNULL(C.CASH_IN_2000P, 0) - ISNULL(C.CASH_OUT_2000P, 0))) * 20 AS 
	       COL_20,
	       ((ISNULL(C.CASH_IN_1000P, 0) - ISNULL(C.CASH_OUT_1000P, 0))) * 10 AS 
	       COL_10,
	       ((ISNULL(C.CASH_IN_500P, 0) - ISNULL(C.CASH_OUT_500P, 0))) * 5 AS 
	       COL_5,
	       CASE 
	            WHEN @Region = 'UK' THEN 0
	            WHEN @Region = 'AR' THEN (ISNULL(CASH_IN_200P, 0) - ISNULL(CASH_OUT_200P, 0)) 
	                 * 2
	            ELSE (ISNULL(CASH_IN_200P, 0) - ISNULL(CASH_OUT_200P, 0)) * 2
	       END AS RDC__COL_2,
	       CASE 
	            WHEN @Region = 'UK' THEN 0
	            WHEN @Region = 'AR' THEN 0
	            ELSE (ISNULL(CASH_IN_100P, 0) - ISNULL(CASH_OUT_100P, 0))
	       END AS RDC_COL_1,
	       CASE ---RDC_COL_TOTALCOINS
	            WHEN @Region = 'UK' THEN (
	                     (ISNULL(C.CASH_IN_200P, 0) - ISNULL(C.CASH_OUT_200P, 0)) 
	                     *
	                     2.00 + (ISNULL(C.CASH_IN_100P, 0) - ISNULL(C.CASH_OUT_100P, 0))*1.00 
	                     + (ISNULL(C.CASH_IN_50P, 0) - ISNULL(C.CASH_OUT_50P, 0)) 
	                     / 2.00 
	                     + (ISNULL(C.CASH_IN_20P, 0) - ISNULL(C.CASH_OUT_20P, 0)) 
	                     / 5.00 
	                     + (ISNULL(C.CASH_IN_10P, 0) - ISNULL(C.CASH_OUT_10P, 0)) 
	                     /
	                     10.00 + (ISNULL(C.CASH_IN_5P, 0) - ISNULL(C.CASH_OUT_5P, 0)) 
	                     / 20.00 + (ISNULL(C.CASH_IN_2P, 0) - ISNULL(C.CASH_OUT_2P, 0)) 
	                     / 50.00 + (ISNULL(C.CASH_IN_1P, 0) - ISNULL(C.CASH_OUT_1P, 0))/100.00
	                 )
	            WHEN @Region = 'AR' THEN (
	                     ISNULL(C.CASH_IN_100P, 0) - ISNULL(C.CASH_OUT_100P, 0) /1.00
	                     + (ISNULL(C.CASH_IN_50P, 0) - ISNULL(C.CASH_OUT_50P, 0)) 
	                     / 2.00 + (ISNULL(C.CASH_IN_20P, 0) - ISNULL(C.CASH_OUT_20P, 0)) 
	                     / 5.00 + (ISNULL(C.CASH_IN_10P, 0) - ISNULL(C.CASH_OUT_10P, 0)) 
	                     / 10.00 + (ISNULL(C.CASH_IN_5P, 0) - ISNULL(C.CASH_OUT_5P, 0)) 
	                     / 20.00 + (ISNULL(C.CASH_IN_2P, 0) - ISNULL(C.CASH_OUT_2P, 0)) 
	                     / 50.00 + (ISNULL(C.CASH_IN_1P, 0) - ISNULL(C.CASH_OUT_1P, 0)) /100.00
	                 )
	            ELSE (
	                     (ISNULL(C.CASH_IN_50P, 0) - ISNULL(C.CASH_OUT_50P, 0)) 
	                     / 2.00 
	                     + (ISNULL(C.CASH_IN_20P, 0) - ISNULL(C.CASH_OUT_20P, 0)) 
	                     / 5.00 
	                     + (ISNULL(C.CASH_IN_10P, 0) - ISNULL(C.CASH_OUT_10P, 0)) 
	                     /
	                     10.00 + (ISNULL(C.CASH_IN_5P, 0) - ISNULL(C.CASH_OUT_5P, 0)) 
	                     / 20.00 + (ISNULL(C.CASH_IN_2P, 0) - ISNULL(C.CASH_OUT_2P, 0)) 
	                     / 50.00 + (ISNULL(C.CASH_IN_1P, 0) - ISNULL(C.CASH_OUT_1P, 0))/100.00
	                 )
	       END AS RDC_COL_TOTALCOINS,
	       CASE --COL_COINSIN
	            WHEN @Region = 'UK' THEN ISNULL(C.CASH_IN_200P, 0) / 1.00 +
	                 ISNULL(C.CASH_IN_100P, 0) / 1.00 +
	                 ISNULL(C.CASH_IN_50p, 0) / 2.00 +
	                 ISNULL(C.CASH_IN_20p, 0) / 5.00 +
	                 ISNULL(C.CASH_IN_10p, 0) / 10.00 +
	                 ISNULL(C.CASH_IN_5p, 0) / 20.00 +
	                 ISNULL(C.CASH_IN_2p, 0) / 50.00 +
	                 ISNULL(C.CASH_IN_1P, 0)/100.00
	            WHEN @Region = 'AR' THEN ISNULL(C.CASH_IN_100P, 0) +
	                 ISNULL(C.CASH_IN_50p, 0) / 2.00 +
	                 ISNULL(C.CASH_IN_20p, 0) / 5.00 +
	                 ISNULL(C.CASH_IN_10p, 0) / 10.00 +
	                 ISNULL(C.CASH_IN_5p, 0) / 20.00 +
	                 ISNULL(C.CASH_IN_2p, 0) / 50.00 +
	                 ISNULL(C.CASH_IN_1p, 0)/100.00
	            ELSE ISNULL(C.CASH_IN_50p, 0) / 2.00 +
	                 ISNULL(C.CASH_IN_20p, 0) / 5.00 +
	                 ISNULL(C.CASH_IN_10p, 0) / 10.00 +
	                 ISNULL(C.CASH_IN_5p, 0) / 20.00 +
	                 ISNULL(C.CASH_IN_2p, 0) / 50.00 +
	                 ISNULL(C.CASH_IN_1p, 0)/100.00
	       END AS RDC_COL_COINSIN,
	       (
	           ISNULL(C.COLLECTION_RDC_TICKETS_INSERTED_VALUE, 0) / 100.00 +
	           ISNULL(C.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE, 0)
	           / 100.00
	       ) AS RDC_COL_TICKETSIN,
	       (
	           ISNULL(C.COLLECTION_RDC_TICKETS_PRINTED_VALUE, 0) / 100.00 +
	           ISNULL(C.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE, 0)
	           / 100.00
	       ) AS RDC_COL_TICKETSOUT,
	       
	       --RDC_COL_PromoCashableIn
	       
	       CAST(0 AS DECIMAL) As RDC_COL_PromoCashableIn,
	        CAST(0 AS DECIMAL) As RDC_COL_PromoNonCashableIn,
	       
	       
	       
	       -- RDC_COL_TICKETS
	       (
	           ISNULL(C.COLLECTION_RDC_TICKETS_INSERTED_VALUE, 0) / 100.00 +
	           ISNULL(C.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE, 0)
	           / 100.00
	       ) -(
	           ISNULL(C.COLLECTION_RDC_TICKETS_PRINTED_VALUE, 0) / 100.00		  
	           + ISNULL(C.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE, 0)
	           / 100.00
	       ) AS RDC_COL_TICKETS,
	       (
	           (
	               ISNULL(c.COLLECTION_RDC_HANDPAY, 0) + ISNULL(COLLECTION_RDC_JACKPOT, 0)
	           ) * I.Installation_Price_Per_Play
	       ) / 100.00 AS RDC_COL_HANDPAY,
	       (
	           ISNULL(progressive_win_Handpay_value, 0) * I.Installation_Price_Per_Play
	       ) / 100.00 AS RDC_COL_PROG,
	       --AFT 
	       CASE 
	            WHEN @IsAFTIncludedInCalculation = 'True' THEN (
	                     ISNULL(Promo_Cashable_EFT_IN, 0) / 100.00 +
	                     ISNULL(NonCashable_EFT_IN, 0) / 100.00 +
	                     ISNULL(Cashable_EFT_IN, 0) / 100.00
	                 )
	            ELSE 0
	       END AS RDC_COL_EFTIN,
	       CASE 
	            WHEN @IsAFTIncludedInCalculation = 'True' THEN (
	                     ISNULL(Promo_Cashable_EFT_OUT, 0) / 100.00 +
	                     ISNULL(NonCashable_EFT_OUT, 0) / 100.00 +
	                     ISNULL(Cashable_EFT_OUT, 0) / 100.00
	                 )
	            ELSE 0
	       END AS RDC_COL_EFTOUT,
	       CASE 
	            WHEN @IsAFTIncludedInCalculation = 'True' THEN (
	                     ISNULL(Promo_Cashable_EFT_IN, 0) / 100.00 +
	                     ISNULL(NonCashable_EFT_IN, 0) / 100.00 +
	                     ISNULL(Cashable_EFT_IN, 0) / 100.00
	                 ) -(
	                     ISNULL(Promo_Cashable_EFT_OUT, 0) / 100.00 +
	                     ISNULL(NonCashable_EFT_OUT, 0) / 100.00 +
	                     ISNULL(Cashable_EFT_OUT, 0) / 100.00
	                 )
	            ELSE 0
	       END AS RDC_COL_EFT,
	       -------------------------------------------------------------------------------------
	       -- C.*,
	       CAST(ISNULL(C.CASH_IN_1P, 0) / 100 AS FLOAT) AS CASH_IN_1P,
	       CAST(ISNULL(CASH_OUT_1P, 0) / 100 AS FLOAT) AS CASH_OUT_1P,
	       --SETTINGS       
	       UPPER(ISNULL(@bSVGIEnabled, 'False')) AS Setting_SVGIEnabled,
	       UPPER(ISNULL(@Region, 'False')) AS Setting_Region,
	       UPPER(ISNULL(@sAddShortpay, 'False')) AS Setting_AddShortpay,
	       UPPER(ISNULL(@Auto_Declare_Monies, 'False')) AS 
	       Setting_Auto_Declare_Monies,
	       UPPER(ISNULL(@IsAFTIncludedInCalculation, 'False')) AS 
	       Setting_IsAFTIncludedInCalculation,
	       m.Machine_Stock_No As Asset,
           C.[User_Name] As DeclaredBy
	FROM   COLLECTION C WITH (NOLOCK)
	       INNER JOIN BAtch WITH (NOLOCK)
	            ON  batch.Batch_ID = c.Batch_ID
	       INNER JOIN Installation i
	            ON  i.Installation_ID = c.Installation_ID
	       INNER JOIN MACHINE m WITH(NOLOCK)
	            ON  m.Machine_ID = i.Machine_ID
	       LEFT JOIN Bar_Position BP
	            ON  i.Bar_Position_ID = Bp.Bar_Position_ID
	       LEFT JOIN [Zone] Z
	            ON  BP.Zone_ID = Z.Zone_ID
	       LEFT JOIN Machine_Class MC
	            ON  M.Machine_Class_ID = MC.Machine_Class_ID
	       LEFT JOIN Machine_Type MT
	            ON  MC.Machine_Type_ID = MT.Machine_Type_ID
	            
	       LEFT OUTER JOIN Collection_Ticket CT
				ON CT.CT_Inserted_Collection_ID=C.Collection_ID
	WHERE  Collection_ID = @Collection_ID
END
GO


/*
declare @p5 int
set @p5=0
exec sp_executesql N'EXEC @RETURN_VALUE = [dbo].[rsp_ecCollectionBreakDown] @Collection_ID = @p0, @Site_id = @p1',N'@p0 int,@p1 int,@RETURN_VALUE int output',@p0=1,@p1=1,@RETURN_VALUE=@p5 output
select @p5



select * from Collection_Ticket
*/