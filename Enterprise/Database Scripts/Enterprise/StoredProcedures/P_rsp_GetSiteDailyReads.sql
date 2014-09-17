USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDailyReads]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDailyReads]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
 * this stored procedure is to export the Daily Read details for X weeks to the corresponding Exchange  
 * Change History:  --EXEC dbo.rsp_GetSiteDailyReads 1003,10   
 * Vineetha M  19 Sep 2009  Created
 * Vineetha M  18 Jan 2010  Modified	alias name given as Installation_no for recovery.
 * Sudarsan S	11/03/2010	EFT/Mystery/Non cashable tkt
 * Sudarsan S	15/06/2010	Bills 200 and 500
*/  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

CREATE PROCEDURE rsp_GetSiteDailyReads
	@Site_Code VARCHAR(50) ,
	@XDays int
 
AS  
  
BEGIN  

	DECLARE @dttopdate  DATETIME	
	DECLARE @fetchdate DATETIME
	DECLARE @siteid INT

	SELECT @Siteid = site_id FROM SITE WHERE site_code = @Site_Code

		IF @Siteid > 0
	
			BEGIN

				SELECT  @dtTopDate = CONVERT(DATETIME, Read_Date, 101) FROM [READ] WHERE Read_ID=(SELECT MAX(Read_ID) FROM [READ] R 
				INNER JOIN Installation I ON R.Installation_ID = I.Installation_ID 
				INNER JOIN Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
				INNER JOIN [Site] S ON BP.Site_ID = S.Site_ID WHERE S.Site_ID = @Siteid)

				SET @FetchDate=DATEADD(dd,-@XDays,@dtTopDate)

					SELECT  
					   Read_ID, 
					   --Collection_ID, 
 					   Read_Session,  
					   --Read_Interrogator,  
					   --Read_Machine_Code,  
					   PAYOUT_FLOAT_TOKEN,  
					   PAYOUT_FLOAT_10P,  
					   PAYOUT_FLOAT_20P,  
					   PAYOUT_FLOAT_50P,  
					   PAYOUT_FLOAT_100P,  
					   CASH_IN_1P, 
					   CASH_IN_2P,  
					   CASH_IN_5P,  
					   CASH_IN_10P,  
					   CASH_IN_20P,  
					   CASH_IN_50P,  
					   CASH_IN_100P,  
					   CASH_IN_200P,  
					   CASH_IN_500P,  
					   CASH_IN_1000P,  
					   CASH_IN_2000P,  
					   CASH_IN_5000P,  
					   CASH_IN_10000P,  
					   CASH_IN_20000P,  
					   CASH_IN_50000P,  
					   CASH_IN_100000P,  
					   --CASH_IN_200000P,  
					   --CASH_IN_500000P,  
					   --CASH_IN_1000000P,  
					   TOKEN_IN_5P,  
					   TOKEN_IN_10P,  
					   TOKEN_IN_20P,  
					   TOKEN_IN_50P,  
					   TOKEN_IN_100P,  
					   TOKEN_IN_200P,  
					   TOKEN_IN_500P,  
					   TOKEN_IN_1000P,  
					   CASH_OUT_1P,
					   CASH_OUT_2P,  
					   CASH_OUT_5P,  
					   CASH_OUT_10P,  
					   CASH_OUT_20P,  
					   CASH_OUT_50P,  
					   CASH_OUT_100P,  
					   CASH_OUT_200P,  
					   CASH_OUT_500P,  
					   CASH_OUT_1000P,  
					   CASH_OUT_2000P,  
					   CASH_OUT_5000P,  
					   CASH_OUT_10000P,  
					   CASH_OUT_20000P,  
					   CASH_OUT_50000P,  
					   CASH_OUT_100000P,  
					   --CASH_OUT_200000P,  
					   --CASH_OUT_500000P,  
					   --CASH_OUT_1000000P,  
					   TOKEN_OUT_5P,  
					   TOKEN_OUT_10P,  
					   TOKEN_OUT_20P,  
					   TOKEN_OUT_50P,  
					   TOKEN_OUT_100P,  
					   TOKEN_OUT_200P,  
					   TOKEN_OUT_500P,  
					   TOKEN_OUT_1000P,  
					   CASH_REFILL_5P,  
					   CASH_REFILL_10P,  
					   CASH_REFILL_20P,  
					   CASH_REFILL_50P,  
					   CASH_REFILL_100P,  
					   CASH_REFILL_200P,  
					   CASH_REFILL_500P,  
					   CASH_REFILL_1000P,  
					   CASH_REFILL_2000P,  
					   CASH_REFILL_5000P,  
					   CASH_REFILL_10000P,  
					   CASH_REFILL_20000P,  
					   CASH_REFILL_50000P,  
					   CASH_REFILL_100000P,  
					   --CASH_REFILL_200000P,  
					   --CASH_REFILL_500000P,  
					   --CASH_REFILL_1000000P,  
					   TOKEN_REFILL_5P,  
					   TOKEN_REFILL_10P,  
					   TOKEN_REFILL_20P,  
					   TOKEN_REFILL_50P,  
					   TOKEN_REFILL_100P,  
					   TOKEN_REFILL_200P,  
					   TOKEN_REFILL_500P,  
					   TOKEN_REFILL_1000P,  
				--       CREDITS_TAKEN,  
				--       Hours_On,  
				--       Read_Amedis_Uploaded,  
				--       Read_honeyframe_EDI_Uploaded,  
				--       Read_Declaration_AWPSerialNumber,  
				--       Read_Declaration_MachineDays,  
				--       Read_Declaration_TokenFloatPos,  
				--       Read_Declaration_TokenFloatNeg,  
				--       Read_Declaration_Rent,  
				--       Read_Declaration_Cash50p,  
				--       Read_Declaration_Cash100p,  
				--       Read_Declaration_DisposableCash,  
				--       Read_Declaration_TokensRedeemed,  
				--       Read_Declaration_Sundries,  
				--       Read_Declaration_NetBalance,  
				--       Read_Declaration_TypeOfTrade,  
				--       Read_Declaration_Faults,  
				--       Datapak_Serial,  
				--       Diary_Entry_ID,  
				--       Read_RDC_VTP,  
				--       Read_RDC_Secondary_Machine_Code,  
				--       Read_RDC_SiteCode,  
					   READ_COINS_IN,  
					   READ_COINS_OUT,  
					   READ_COIN_DROP,  
					   READ_HANDPAY,  
					   READ_EXTERNAL_CREDIT,  
					   READ_GAMES_BET,  
					   READ_GAMES_WON,  
					   READ_NOTES,  
					   VTP,  
					   READ_RDC_CANCELLED_CREDITS,  
					   READ_RDC_GAMES_LOST,  
					   READ_RDC_GAMES_SINCE_POWER_UP,  
					   READ_RDC_TRUE_COIN_IN,  
					   READ_RDC_TRUE_COIN_OUT,  
					   READ_RDC_CURRENT_CREDITS,  
					   READ_RDC_BILL_1,  
					   READ_RDC_BILL_2,  
					   READ_RDC_BILL_5,  
					   READ_RDC_BILL_10,  
					   READ_RDC_BILL_20,  
					   READ_RDC_BILL_50,  
					   READ_RDC_BILL_100,  
					   READ_RDC_BILL_250,  
					   READ_RDC_BILL_10000,  
					   READ_RDC_BILL_20000,  
					   READ_RDC_BILL_50000,  
					   READ_RDC_BILL_100000,  
					   1,
					   R.Installation_ID AS Installation_No,  
					   CONVERT(DATETIME, Read_Date + ' ' + Read_Time, 101) AS Read_Date,
--					   Read_Time,
					   READ_TICKET,  
					   READ_TICKET_VALUE,  
					   READ_TICKET_IN_SUSPENSE,  
					   READ_TICKET_IN_SUSPENSE_VALUE,  
					   Read_Forced, 
					   CONVERT(DATETIME, Previous_Read_Date + ' ' + Previous_Read_Time, 101) AS Previous_Read_Date,
--					   Previous_Read_Time,  
					   --READ_RDC_Datapak_Type,  
					   --READ_RDC_Datapak_Version,  
					   --Read_Occurrence, 
					   READ_RDC_JACKPOT,
					   Week_ID,  
					   Period_ID,  
					   Operator_Week_ID,  
					   Operator_Period_ID, 					   
					   progressive_win_value,  
					   progressive_win_Handpay_value,
						Mystery_Machine_Paid,
						Mystery_Attendant_Paid,
						TICKETS_INSERTED_NONCASHABLE_VALUE,
						TICKETS_PRINTED_NONCASHABLE_VALUE,
						Promo_Cashable_EFT_IN,
						Promo_Cashable_EFT_OUT,
						NonCashable_EFT_IN,
						NonCashable_EFT_OUT,
						Cashable_EFT_IN,
						Cashable_EFT_OUT,
					   READ_RDC_BILL_200,
					   READ_RDC_BILL_500
     
						FROM	[READ]  R
						JOIN Installation i on i.Installation_ID=R.Installation_ID
						JOIN Bar_Position b on i.Bar_Position_ID=b.Bar_Position_ID
						JOIN Site S on b.Site_ID=S.Site_ID
					WHERE R.Read_Date >= @FetchDate and S.Site_id=@Siteid ORDER BY 1 DESC
			 END

END


GO

