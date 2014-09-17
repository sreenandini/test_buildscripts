USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameSessionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameSessionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: Inserts/updates the session details
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		12/10/2009		Created
-- Sudarsan S		11/03/2010		EFT/Mystery
-- Yoganandh P		21/01/2011		True Coin In/True Coin Out
------------------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.usp_UpdateGameSessionDetails
	@doc		XML
AS

BEGIN
	
DECLARE @idoc	INT
DECLARE @Session_ID	INT
DECLARE @Read_Date	DATETIME
DECLARE @Installation_ID	INT

	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	SELECT * INTO #TempSession FROM OPENXML(@idoc, './SessionDetails/Session', 2) WITH
		(
			MGMD_ID				INT	'./HQ_MGMD_ID',
			Installation_ID		INT	'./HQ_Installation_No',
			StartDate			DATETIME	'./MGMD_Start_DateTime',
			EndDate				DATETIME	'./MGMD_End_DateTime',
			MGMD_COINS_IN		INT	'./MGMD_COINS_IN',
			MGMD_COINS_OUT		INT	'./MGMD_COINS_OUT',
			MGMD_COIN_DROP		INT	'./MGMD_COIN_DROP',
			MGMD_HANDPAY		INT	'./MGMD_HANDPAY',
			MGMD_GAMES_BET		INT	'./MGMD_GAMES_BET',
			MGMD_GAMES_WON		INT './MGMD_GAMES_WON',
			MGMD_CANCELLED_CREDITS	INT './MGMD_CANCELLED_CREDITS',
			MGMD_GAMES_LOST		INT './MGMD_GAMES_LOST',
			MGMD_CURRENT_CREDITS	INT './MGMD_CURRENT_CREDITS',
			MGMD_JACKPOT	INT './MGMD_JACKPOT',
			MGMD_BILL_1	INT './MGMD_BILL_1',
			MGMD_BILL_5	INT './MGMD_BILL_5',
			MGMD_BILL_10	INT './MGMD_BILL_10',
			MGMD_BILL_20	INT './MGMD_BILL_20',
			MGMD_BILL_50	INT './MGMD_BILL_50',
			MGMD_BILL_100	INT './MGMD_BILL_100',
			MGMD_progressive_win_value	INT './MGMD_progressive_win_value',
			MGMD_progressive_win_Handpay_value	INT './MGMD_progressive_win_Handpay_value',
			Read_Date		DATETIME	'./Read_Date',
			MGMD_Mystery_Machine_Paid	INT	'./MGMD_Mystery_Machine_Paid',
			MGMD_Mystery_Attendant_Paid	INT	'./MGMD_Mystery_Attendant_Paid',
			MGMD_Promo_Cashable_EFT_IN	INT	'./MGMD_Promo_Cashable_EFT_IN',
			MGMD_Promo_Cashable_EFT_OUT	INT	'./MGMD_Promo_Cashable_EFT_OUT',
			MGMD_NonCashable_EFT_IN	INT	'./MGMD_NonCashable_EFT_IN',
			MGMD_NonCashable_EFT_OUT	INT	'./MGMD_NonCashable_EFT_OUT',
			MGMD_Cashable_EFT_IN	INT	'./MGMD_Cashable_EFT_IN',
			MGMD_Cashable_EFT_OUT	INT	'./MGMD_Cashable_EFT_OUT',
			MGMD_BILL_200	INT './MGMD_BILL_200',
			MGMD_BILL_500	INT './MGMD_BILL_500',
			MGMD_TICKET_PRINTED_QTY INT './MGMD_TICKET_PRINTED_QTY',
		    MGMD_TICKET_PRINTED_VALUE INT './MGMD_TICKET_PRINTED_VALUE',
		    MGMD_TICKET_INSERTED_QTY INT './MGMD_TICKET_INSERTED_QTY',
		    MGMD_TICKET_INSERTED_VALUE INT './MGMD_TICKET_INSERTED_VALUE',
		    MGMD_TICKETS_PRINTED_NONCASHABLE_QTY INT './MGMD_TICKETS_PRINTED_NONCASHABLE_QTY',
		    MGMD_TICKETS_PRINTED_NONCASHABLE_VALUE INT './MGMD_TICKETS_PRINTED_NONCASHABLE_VALUE',
		    MGMD_TICKETS_INSERTED_NONCASHABLE_QTY INT './MGMD_TICKETS_INSERTED_NONCASHABLE_QTY',
		    MGMD_TICKETS_INSERTED_NONCASHABLE_VALUE  INT './MGMD_TICKETS_INSERTED_NONCASHABLE_VALUE',
			MGMD_TRUE_COIN_IN INT './MGMD_TRUE_COIN_IN',
			MGMD_TRUE_COIN_OUT INT './MGMD_TRUE_COIN_OUT'
		)

	EXEC sp_xml_removedocument @idoc

	SELECT @Session_ID = S.MGMD_Session_ID FROM dbo.MGMD_SessionDelta S 
				INNER JOIN #TempSession T ON S.MGMD_Combination_ID = T.MGMD_ID AND S.MGMD_Start_DateTime = T.StartDate
						AND S.MGMD_End_DateTime = T.EndDate

	IF @Session_ID IS NULL
	BEGIN

		INSERT INTO dbo.MGMD_SessionDelta
		SELECT * FROM dbo.#TempSession 

		SELECT @Session_ID = SCOPE_IDENTITY()

		SELECT @Installation_ID = MGMD_Installation_ID, @Read_Date = Read_Date
				FROM dbo.MGMD_SessionDelta WHERE MGMD_Session_ID = @Session_ID

		IF @Read_Date IS NOT NULL
		BEGIN
			UPDATE dbo.MGMD_SessionDelta SET Read_Date = @Read_Date
			 WHERE MGMD_Installation_ID = @Installation_ID AND Read_Date IS NULL 
			   AND MGMD_Session_ID < @Session_ID
		END

	END
	ELSE
	BEGIN
		UPDATE S
			SET S.MGMD_COINS_IN = T.MGMD_COINS_IN,
				S.MGMD_COINS_OUT = T.MGMD_COINS_OUT,
				S.MGMD_COIN_DROP = T.MGMD_COIN_DROP,
				S.MGMD_HANDPAY = T.MGMD_HANDPAY,
				S.MGMD_GAMES_BET = T.MGMD_GAMES_BET,
				S.MGMD_GAMES_WON = T.MGMD_GAMES_WON,
				S.MGMD_CANCELLED_CREDITS = T.MGMD_CANCELLED_CREDITS,
				S.MGMD_GAMES_LOST = T.MGMD_GAMES_LOST,
				S.MGMD_CURRENT_CREDITS = T.MGMD_CURRENT_CREDITS,
				S.MGMD_JACKPOT = T.MGMD_JACKPOT,
				S.MGMD_BILL_1 = T.MGMD_BILL_1,
				S.MGMD_BILL_5 = T.MGMD_BILL_5,
				S.MGMD_BILL_10 = T.MGMD_BILL_10,
				S.MGMD_BILL_20 = T.MGMD_BILL_20,
				S.MGMD_BILL_50 = T.MGMD_BILL_50,
				S.MGMD_BILL_100 = T.MGMD_BILL_100,
				S.MGMD_progressive_win_value = T.MGMD_progressive_win_value,
				S.MGMD_progressive_win_Handpay_value = T.MGMD_progressive_win_Handpay_value,
				S.MGMD_Mystery_Machine_Paid = T.MGMD_Mystery_Machine_Paid,
				S.MGMD_Mystery_Attendant_Paid = T.MGMD_Mystery_Attendant_Paid,
				S.MGMD_Promo_Cashable_EFT_IN = T.MGMD_Promo_Cashable_EFT_IN,
				S.MGMD_Promo_Cashable_EFT_OUT = T.MGMD_Promo_Cashable_EFT_OUT,
				S.MGMD_NonCashable_EFT_IN = T.MGMD_NonCashable_EFT_IN,
				S.MGMD_NonCashable_EFT_OUT = T.MGMD_NonCashable_EFT_OUT,
				S.MGMD_Cashable_EFT_IN = T.MGMD_Cashable_EFT_IN,
				S.MGMD_Cashable_EFT_OUT = T.MGMD_Cashable_EFT_OUT,
				S.MGMD_BILL_200 = T.MGMD_BILL_200,
				S.MGMD_BILL_500 = T.MGMD_BILL_500,
				S.MGMD_TICKET_PRINTED_QTY = T.MGMD_TICKET_PRINTED_QTY,
				S.MGMD_TICKET_PRINTED_VALUE = T.MGMD_TICKET_PRINTED_VALUE,
				S.MGMD_TICKET_INSERTED_QTY = T.MGMD_TICKET_INSERTED_QTY,
				S.MGMD_TICKET_INSERTED_VALUE = T.MGMD_TICKET_INSERTED_VALUE,
				S.MGMD_TICKETS_PRINTED_NONCASHABLE_QTY = T.MGMD_TICKETS_PRINTED_NONCASHABLE_QTY,
				S.MGMD_TICKETS_PRINTED_NONCASHABLE_VALUE = T.MGMD_TICKETS_PRINTED_NONCASHABLE_VALUE,
				S.MGMD_TICKETS_INSERTED_NONCASHABLE_QTY = T.MGMD_TICKETS_INSERTED_NONCASHABLE_QTY,
				S.MGMD_TICKETS_INSERTED_NONCASHABLE_VALUE = T.MGMD_TICKETS_INSERTED_NONCASHABLE_VALUE,
				S.MGMD_TRUE_COIN_IN = T.MGMD_TRUE_COIN_IN,
				S.MGMD_TRUE_COIN_OUT = T.MGMD_TRUE_COIN_OUT


		  FROM  dbo.MGMD_SessionDelta S
	INNER JOIN	dbo.#TempSession T ON S.MGMD_Combination_ID = T.MGMD_ID 
		AND S.MGMD_Start_DateTime = T.StartDate AND S.MGMD_End_DateTime = T.EndDate
	END

	RETURN @Session_ID
	
END


GO

