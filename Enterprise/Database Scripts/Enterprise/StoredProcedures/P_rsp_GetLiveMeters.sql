USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetLiveMeters]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetLiveMeters]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------       
--      
-- Description: To get the live meters at enterprise
--      
-- Inputs:      @Installation_No INT - Installation No 
--      
-- Return:      Set of meters
--      
-- =======================================================================      
--       
-- Revision History      
--       
-- P Saravanakumar     24/08/2010     Created   
---------------------------------------------------------------------------       
  
        
CREATE PROCEDURE [dbo].[rsp_GetLiveMeters] (@Installation_No INT)                
AS                            
BEGIN                            
		DECLARE @COINS_OUT AS INT
		DECLARE @COIN_DROP AS INT
		DECLARE @HANDPAY AS INT
		DECLARE @RDC_CANCELLED_CREDITS AS INT
		DECLARE @RDC_TICKETS_INSERTED_CASHABLE_VALUE AS INT
		DECLARE @RDC_TICKETS_PRINTED_CASHABLE_VALUE AS INT


Declare @Bill500Meter as INT
Declare @Bill200Meter as INT
Declare @Bill100Meter as INT
Declare @Bill50Meter as INT
Declare @Bill20Meter as INT
Declare @Bill10Meter as INT
Declare @Bill5Meter as INT
Declare @Bill1Meter as INT
Declare @TrueCoinIn as INT
Declare @TrueCoinOut as INT

Declare @Bets as INT
Declare @Wins as INT
Declare @Drop as INT
Declare @Jackpot as INT
Declare @CancelledCredits as INT
Declare @AttCancelledCredits as INT
Declare @CashableVoucherInValue as INT
Declare @CashableVoucherOutValue as INT
Declare @CashableVoucherInQty as INT
Declare @CashableVoucherOutQty as INT

Declare @NonCashableVoucherInValue as INT
Declare @NonCashableVoucherOutValue as INT
Declare @NonCashableVoucherInQty as INT
Declare @NonCashableVoucherOutQty as INT
Declare @ProgressiveAttendantpay as INT
Declare @GamesPlayed as INT
Declare @PromoCashableEFTIn as INT
Declare @PromoCashableEFOut as INT
Declare @NonCashableEFTIn as INT
Declare @NonCashableEFTOut as INT

Declare @CashableEFTIn as INT
Declare @CashableEFTOut INT
			
		SELECT                         
			Machine_Class.Machine_Class_Is_Ticket,   
			Installation.Datapak_Variant,   
			Machine_Class.Machine_Class_SP_Features,   
			Manufacturer.Manufacturer_Name,   
			Manufacturer.Manufacturer_Handpay_Added_To_Coin_Out,   
			Machine_Class.Machine_Class_RecreateCancelledCredits,   
			Machine_Class.Machine_Class_JackpotAddedToCancelledCredits,   
			Machine_Class_AddTrueCoinInToDrop,   
			Machine_Class_UseCancelledCreditsAsTicketsPrinted,  
			Machine_Class_RecreateTicketsInsertedfromDrop,  
			Installation.Installation_Token_Value,  
			Installation.Installation_Price_per_Play,  
			'7' AS Datapak_Type  
		INTO #Config
		FROM Installation (NOLOCK)  
		INNER JOIN Machine (NOLOCK) ON Installation.Machine_ID = Machine.Machine_ID  
		INNER JOIN Machine_Class (NOLOCK) ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID  
		INNER JOIN Manufacturer (NOLOCK) ON Machine_Class.Manufacturer_ID = Manufacturer.Manufacturer_ID  
		LEFT JOIN Datapak ON Installation.Datapak_ID = Datapak.Datapak_Serial  
		WHERE Installation.Installation_ID = @Installation_No
	

		SELECT 
			@Bill500Meter = RDC_BILL_500,
			@Bill200Meter = RDC_BILL_200,
			@Bill100Meter = RDC_BILL_100,
			@Bill50Meter = RDC_BILL_50,
			@Bill20Meter = RDC_BILL_20,
			@Bill10Meter = RDC_BILL_10,	
			@Bill5Meter = RDC_BILL_5,
			@Bill1Meter = RDC_BILL_1,
			@TrueCoinIn = RDC_TRUE_COIN_IN,
			@TrueCoinOut = RDC_TRUE_COIN_OUT,
			@Bets = COINS_IN ,
			@Wins = COINS_OUT,
			@Drop = COIN_DROP ,
			@Jackpot = RDC_JACKPOT ,
			@CancelledCredits = RDC_CANCELLED_CREDITS ,
			@AttCancelledCredits = HANDPAY,
			@CashableVoucherInValue = RDC_TICKETS_INSERTED_CASHABLE_VALUE ,
			@CashableVoucherOutValue = RDC_TICKETS_PRINTED_CASHABLE_VALUE ,
			@CashableVoucherInQty = RDC_TICKETS_INSERTED_CASHABLE_QTY	,
			@CashableVoucherOutQty = RDC_TICKETS_PRINTED_CASHABLE_QTY	,
			@NonCashableVoucherInValue = RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
			@NonCashableVoucherOutValue = RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
			@NonCashableVoucherInQty = RDC_TICKETS_INSERTED_NONCASHABLE_QTY,
			@NonCashableVoucherOutQty = RDC_TICKETS_PRINTED_NONCASHABLE_QTY,
			@ProgressiveAttendantpay = Progressive_Win_Handpay_Value,
			@GamesPlayed = GAMES_BET,
			@PromoCashableEFTIn = Promo_Cashable_EFT_IN	,
			@PromoCashableEFOut = Promo_Cashable_EFT_OUT	,
			@NonCashableEFTIn = NonCashable_EFT_IN	,
			@NonCashableEFTOut = NonCashable_EFT_OUT	,
			@CashableEFTIn = Cashable_EFT_IN	,
			@CashableEFTOut = Cashable_EFT_OUT	
		FROM Floor_Financials (NOLOCK)
		WHERE Installation_No = @Installation_No

		IF ( (SELECT Manufacturer_Handpay_Added_To_Coin_Out FROM #Config) = 1 )  
			SELECT @COINS_OUT = (@Wins + @CancelledCredits)  
		ELSE 
			SELECT @COINS_OUT = @Wins 

		SELECT @COIN_DROP = @Drop
		IF ( (SELECT Machine_Class_AddTrueCoinInToDrop FROM #Config) = 1 )  
			SET @COIN_DROP = @Drop + ((@TrueCoinIn * (SELECT Installation_Token_Value FROM #Config))/(SELECT Installation_Price_per_Play FROM #Config)) - @TrueCoinIn

		SELECT @HANDPAY = @AttCancelledCredits
		IF ( (SELECT Machine_Class_UseCancelledCreditsAsTicketsPrinted FROM #Config) = 1 )  
			SET @HANDPAY = @HANDPAY + @Jackpot

		SELECT @RDC_CANCELLED_CREDITS = @CancelledCredits
		IF ( (SELECT Machine_Class_RecreateCancelledCredits FROM #Config) = 1 )  
			SET @RDC_CANCELLED_CREDITS =  @Drop - @Bets - @Wins
		ELSE IF ( (SELECT Machine_Class_JackpotAddedToCancelledCredits FROM #Config) = 1 )  
			SET @RDC_CANCELLED_CREDITS = @RDC_CANCELLED_CREDITS - @Jackpot


		SELECT @RDC_TICKETS_INSERTED_CASHABLE_VALUE = @CashableVoucherInValue
		IF ( (SELECT Machine_Class_RecreateTicketsInsertedfromDrop FROM #Config) = 1 )  
		BEGIN
			DECLARE @cashvalue float
			SELECT @cashvalue = (CAST(@Bill1Meter as float) * 100) +   
			 (cast(@Bill5Meter as float) * 500) +  
			 (CAST(@Bill10Meter as float) * 1000) +   
			 (CAST(@Bill20Meter as float) * 2000) +   
			 (cast(@Bill50Meter as float) * 5000) +  
			 (cast(@Bill100Meter as float) * 10000) +  
			 (cast(@Bill200Meter as float) * 10000) +  
			 (cast(@Bill500Meter as float) * 10000)

			SET @RDC_TICKETS_INSERTED_CASHABLE_VALUE = (@COIN_DROP * (SELECT Installation_Price_per_Play FROM #Config) ) - @cashvalue
		END	

		SELECT @RDC_TICKETS_PRINTED_CASHABLE_VALUE = @CashableVoucherOutValue
		IF ( (SELECT Machine_Class_UseCancelledCreditsAsTicketsPrinted FROM #Config) = 1 )  
			SET @RDC_TICKETS_PRINTED_CASHABLE_VALUE = (@CancelledCredits * (SELECT Installation_Price_per_Play FROM #Config)) - @AttCancelledCredits

		SELECT 
			@Bill500Meter as [Bill 500 Meter],
			@Bill200Meter as [Bill 200 Meter],
			@Bill100Meter as [Bill 100 Meter],
			@Bill50Meter as [Bill 50 Meter],
			@Bill20Meter as [Bill 20 Meter],
			@Bill10Meter as [Bill 10 Meter],	
			@Bill5Meter as [Bill 5 Meter],
			@Bill1Meter as [Bill 1 Meter],
			@TrueCoinIn as [True Coin In],
			@TrueCoinOut as [True Coin Out],
			@Bets as [Bets],
			@COINS_OUT AS [Wins],
			@COIN_DROP AS [Drop],
			@Jackpot as [Jackpot],
			@RDC_CANCELLED_CREDITS AS [Cancelled Credits],
			@HANDPAY AS [Attendantpaid Cancelled Credits],
			@RDC_TICKETS_INSERTED_CASHABLE_VALUE AS	[Cashable Voucher In - Value],
			@RDC_TICKETS_PRINTED_CASHABLE_VALUE AS	[Cashable Voucher Out - Value],
			@CashableVoucherInQty as [Cashable Voucher In - Qty],
			@CashableVoucherOutQty as [Cashable Voucher Out - Qty],
			@NonCashableVoucherInValue as [Non Cashable Voucher In - Value],
			@NonCashableVoucherOutValue as [Non Cashable Voucher Out - Value],
			@NonCashableVoucherInQty as [Non Cashable Voucher In - Qty],
			@NonCashableVoucherOutQty as [Non Cashable Voucher Out - Qty],
			@ProgressiveAttendantpay as [Progressive Attendantpay],
			@GamesPlayed as [Games Played],
			@PromoCashableEFTIn as [Promo Cashable EFT In],
			@PromoCashableEFOut as [Promo Cashable EFT Out],
			@NonCashableEFTIn as [Non Cashable EFT In],
			@NonCashableEFTOut as [Non Cashable EFT Out],
			@CashableEFTIn as [Cashable EFT In],
			@CashableEFTOut as [Cashable EFT Out]
END   


GO

