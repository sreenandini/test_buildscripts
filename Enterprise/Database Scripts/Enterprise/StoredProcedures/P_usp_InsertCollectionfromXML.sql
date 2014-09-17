USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCollectionfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCollectionfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- ===================================================================================================================================
-- usp_InsertCollectionBatchfromXML
-- -----------------------------------------------------------------------------------------------------------------------------------
--
-- inserts into Collection batch, collection table from XML
-- 

-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 13/05/2008	Sudarsan S	Created
-- 25/06/2008	Sudarsan S	passed Batch_Advance to the inner SP to be inserted in to the Batch table
-- 09/07/2008	Sudarsan S	insert the user_name into the tables instead of numbers
-- 10/07/2008	Sudarsan S	For multiplying 100 for all cash_collected data
-- 11/07/2008	Renjish N   Modified sp for 'Rerequest of Collection By Date' Requirement.
-- 17/07/2008	C.Taylor    Added call to esp_AssignCollectionToPeriodEnd, which sets the period end flag automatically, based on the 
--							next calendar_period end
-- 24/07/2008	Sudarsan S	to add the Treasury_refills to the Cash_Refills
-- 18/09/2008   Siva		Added changes for adding new column Progressive_Value_Declared
-- 24/09/08	    Anuradha    Added code for Retailer Negative Net
-- 18/06/09		Sudarsan	modified the code
-- 29/12/09     Anuradha	Modified to include new column Collection_Defloat_COllection.
-- 09/03/2010	Sudarsan S	EFT/Mystery/Non cashable tkt
-- 03/01/2011	Yoganandh P Modified to inculde new columns Previous_Collection_Date_Of_Collection & Previous_Collection_Time_Of_Collection
-- ===================================================================================================================================
CREATE PROCEDURE [dbo].[usp_InsertCollectionfromXML]
  @doc VARCHAR(MAX),
  @IsSuccess VARCHAR(500) output
AS

BEGIN

-- idoc is the document handle of the internal representation of an XML document which is created by calling sp_xml_preparedocument.
DECLARE @idoc INT
DECLARE @liCountBatch	INT
DECLARE @lvcSite_Code	VARCHAR(50)
DECLARE @liBatch_No	INT
DECLARE @lvcBatch_Date	VARCHAR(30)
DECLARE @lvcBatch_Time	VARCHAR(50)
DECLARE @lrBatch_Adjustment	REAL
DECLARE @lvcUser_Name	VARCHAR(50)
DECLARE @lvcBatch_Date_Performed	VARCHAR(30)
DECLARE @lvcBatch_Name	VARCHAR(50)
DECLARE @lfBatch_Advance	FLOAT
DECLARE @liHQ_Batch_ID	INT
DECLARE @liCount	INT
DECLARE @liStartPos	INT
DECLARE @liCollection_ID	INT
DECLARE @liInstallation_ID	INT
--DECLARE @Route_ID	INT
DECLARE @BatchNegativeNet	FLOAT
DECLARE @currentCollID INT
DECLARE @prevCollID INT
DECLARE @userId int

SET @liStartPos = 1
SET @IsSuccess = 'SUCCESS'

SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc

/*	the below temp table is to fetch the Collection data from the parsed XML	*/
CREATE TABLE dbo.#TempCollection
(
		Sno	INT IDENTITY(1,1),
		Collection_No	INT,
		Collection_Batch_No	INT,
		Installation_No	INT,
		Collection_Date	DATETIME,
--		Collection_Time	VARCHAR(50),
		Collection_Date_Performed DATETIME,
		PreviousCollectionDate	DATETIME,
--		PreviousCollectionTime	VARCHAR(50),
		--Route_ID INT,		
		CASH_IN_2P	INT,
		CASH_IN_5P	INT,
		CASH_IN_10P	INT,
		CASH_IN_20P	INT,
		CASH_IN_50P	INT,
		CASH_IN_100P	INT,
		CASH_IN_200P	INT,
		CASH_IN_500P	INT,
		CASH_IN_1000P	INT,
		CASH_IN_2000P	INT,
		CASH_IN_5000P	INT,
		CASH_IN_10000P	INT,
		CASH_IN_20000P	INT,
		CASH_IN_50000P	INT,
		CASH_IN_100000P	INT,
		TOKEN_IN_5P	INT,
		TOKEN_IN_10P	INT,
		TOKEN_IN_20P	INT,
		TOKEN_IN_50P	INT,
		TOKEN_IN_100P	INT,
		TOKEN_IN_200P	INT,
		TOKEN_IN_500P	INT,
		TOKEN_IN_1000P	INT,		
		CASH_OUT_2P	INT,
		CASH_OUT_5P	INT,
		CASH_OUT_10P	INT,
		CASH_OUT_20P	INT,
		CASH_OUT_50P	INT,
		CASH_OUT_100P	INT,
		CASH_OUT_200P	INT,
		CASH_OUT_500P	INT,
		CASH_OUT_1000P	INT,
		CASH_OUT_2000P	INT,	
		CASH_OUT_5000P	INT,
		CASH_OUT_10000P	INT,
		CASH_OUT_20000P	INT,
		CASH_OUT_50000P	INT,
		CASH_OUT_100000P	INT,
		TOKEN_OUT_5P	INT,
		TOKEN_OUT_10P	INT,
		TOKEN_OUT_20P	INT,
		TOKEN_OUT_50P	INT,
		TOKEN_OUT_100P	INT,
		TOKEN_OUT_200P	INT,
		TOKEN_OUT_500P	INT,
		TOKEN_OUT_1000P	INT,
		CASH_REFILL_5P	INT,
		CASH_REFILL_10P	INT,
		CASH_REFILL_20P	INT,
		CASH_REFILL_50P	INT,
		CASH_REFILL_100P	INT,
		CASH_REFILL_200P	INT,
		CASH_REFILL_500P	INT,
		CASH_REFILL_1000P	INT,
		CASH_REFILL_2000P	INT,
		CASH_REFILL_5000P	INT,
		CASH_REFILL_10000P	INT,
		CASH_REFILL_20000P	INT,
		CASH_REFILL_50000P	INT,
		CASH_REFILL_100000P	INT,
		Treasury_Refills	REAL,
		TOKEN_REFILL_5P	INT,
		TOKEN_REFILL_10P	INT,
		TOKEN_REFILL_20P	INT,
		TOKEN_REFILL_50P	INT,
		TOKEN_REFILL_100P	INT,
		TOKEN_REFILL_200P	INT,
		TOKEN_REFILL_500P	INT,
		TOKEN_REFILL_1000P	INT,
		Declaration		BIT,
		CounterCashIn	INT,
		CounterCashOut	INT,
		CounterTokensIn	INT,
		CounterTokensOut	INT,
		CounterRefills	INT,
		CounterPrize	INT,
		CashCollected	REAL,
		TokensCollected	REAL,
		Cash_Collected_1p	REAL,
		Cash_Collected_2p	REAL,
		Cash_Collected_5p	REAL,
		Cash_Collected_10p	REAL,
		Cash_Collected_20p	REAL,
		Cash_Collected_50p	REAL,
		Cash_Collected_100p	REAL,
		Cash_Collected_200p	REAL,
		Cash_Collected_500p	REAL,
		Cash_Collected_1000p	REAL,
		Cash_Collected_2000p	REAL,
		Cash_Collected_5000p	REAL,
		Cash_Collected_10000p	REAL,
		Cash_Collected_20000p	REAL,
		Cash_Collected_50000p	REAL,
		Cash_Collected_100000p	REAL,
		CashRefills	REAL,
		TokenRefills	REAL,
		Cash_Refills_2p	REAL,
		Cash_Refills_5p	REAL,
		Cash_Refills_10p	REAL,
		Cash_Refills_20p	REAL,
		Cash_Refills_50p	REAL,
		Cash_Refills_100p	REAL,
		Cash_Refills_200p	REAL,
		Cash_Refills_500p	REAL,
		Cash_Refills_1000p	REAL,
		Cash_Refills_2000p	REAL,
		Cash_Refills_5000p	REAL,
		Cash_Refills_10000p	REAL,
		Cash_Refills_20000p	REAL,
		Cash_Refills_50000p	REAL,
		Cash_Refills_100000p	REAL,
		CounterCashInElectronic	INT,
		CounterCashOutElectronic	INT,
		CounterTokensInElectronic	INT,
		CounterTokensOutElectronic	INT,
		JackpotsOut	INT,
		PreviousCounterCashIn	INT,
		PreviousCounterCashOut	INT,
		PreviousCounterTokensIn	INT,
		PreviousCounterTokensOut	INT,
		PreviousCounterCashInElectronic	INT,
		PreviousCounterCashOutElectronic	INT,
		PreviousCounterTokensInElectronic	INT,
		PreviousCounterTokensOutElectronic	INT,
		COLLECTION_RDC_COINS_IN	INT,
		COLLECTION_RDC_COINS_OUT	INT,
		COLLECTION_RDC_COIN_DROP	INT,
		COLLECTION_RDC_HANDPAY	INT,
		COLLECTION_RDC_EXTERNAL_CREDIT	INT,
		COLLECTION_RDC_GAMES_BET	INT,
		COLLECTION_RDC_GAMES_WON	INT,
		COLLECTION_RDC_NOTES	INT,
		Collection_Meters_Coins_In	INT,
		Collection_Meters_Coins_Out	INT,
		Collection_Meters_Coin_Drop	INT,
		Collection_Meters_Handpay	INT,
		Collection_Meters_External_Credit	INT,
		Collection_Meters_Games_Bet	INT,
		Collection_Meters_Games_Won	INT,
		Collection_Meters_Notes	INT,
		Collection_Treasury_Defloat	REAL,
		Previous_Meters_Coins_In	INT,
		Previous_Meters_Coins_Out	INT,
		Previous_Meters_Coin_Drop	INT,
		Previous_Meters_Handpay	INT,
		Previous_Meters_External_Credit	INT,
		Previous_Meters_Games_Bet	INT,
		Previous_Meters_Games_Won	INT,
		Previous_Meters_Notes	INT,
		PreviousCounterPrize	INT,
		PreviousCounterJackpotsOut	INT,
		PreviousCounterTournament	INT,
		PreviousCounterJukebox	INT,
		PreviousCounterRefills	INT,
		Treasury_Handpay	FLOAT,
		COLLECTION_RDC_VTP	INT,
		Collection_RDC_Machine_Code	VARCHAR(10),
		Collection_RDC_Secondary_Machine_Code VARCHAR(20),
		Collection_EDC_Status	INT,
		CASH_FLOAT_CHANGE_1p	INT,
		CASH_FLOAT_CHANGE_2p	INT,
		CASH_FLOAT_CHANGE_5p	INT,
		CASH_FLOAT_CHANGE_10p	INT,
		CASH_FLOAT_CHANGE_20p	INT,
		CASH_FLOAT_CHANGE_50p	INT,
		CASH_FLOAT_CHANGE_100p	INT,
		CASH_FLOAT_CHANGE_200p	INT,
		CASH_FLOAT_CHANGE_500p	INT,
		CASH_FLOAT_CHANGE_1000p	INT,
		CASH_FLOAT_TOTAL	REAL,
		COLLECTION_RDC_CANCELLED_CREDITS	INT,
		COLLECTION_RDC_GAMES_LOST	INT,
		COLLECTION_RDC_GAMES_SINCE_POWER_UP	INT,
		COLLECTION_RDC_TRUE_COIN_IN	INT,
		COLLECTION_RDC_TRUE_COIN_OUT	INT,
		COLLECTION_RDC_CURRENT_CREDITS	INT,
		DeclaredTicketQty	INT,
		DeclaredTicketValue	REAL,
		COLLECTION_RDC_JACKPOT	INT,
		DeclaredTicketPrintedQty	INT,
		DeclaredTicketPrintedValue	REAL,
		COLLECTION_RDC_TICKETS_INSERTED_VALUE	INT,
		COLLECTION_RDC_TICKETS_PRINTED_VALUE	INT,
		progressive_win_value	INT,
		progressive_win_Handpay_value		INT,
		Progressive_Value_Declared FLOAT,
		Mystery_Machine_Paid	INT,
		Mystery_Attendant_Paid	INT,
		RDC_TICKETS_INSERTED_NONCASHABLE_VALUE	INT,
		RDC_TICKETS_PRINTED_NONCASHABLE_VALUE	INT,
		Promo_Cashable_EFT_IN	INT,
		Promo_Cashable_EFT_OUT	INT,
		NonCashable_EFT_IN	INT,
		NonCashable_EFT_OUT	INT,
		Cashable_EFT_IN		INT,
		Cashable_EFT_OUT	INT,
		Treasury_Repayments	VARCHAR(50),
		ExpectedBaggedCash	VARCHAR(50),
		ActualBaggedCash	VARCHAR(50),
		CollectionNoDoorEvents	INT,
		CollectionNoPowerEvents	INT,
		CollectionNoFaultEvents	INT,
		CollectionTotalDurationPower	INT,
		Collection_Defloat_Collection BIT,
		PreviousCollectionDatePerformed DATETIME,
		TICKETS_INSERTED_NONCASHABLE_QTY INT,
		TICKETS_INSERTED_QTY INT

        -- Adding the Collection table columns which are not there in Enterprise Collection Table - Begin
		-- Enterprise Collection table doesn't have some columns and those columns sre added to that table. So in XML including those columns too.
		,[Machine_Serial] VARCHAR(50)
		,[Datapak_Read_Occurrence] INT
		,[Float_Level] INT
		,[Period_ID] INT
		,[Week_ID] INT
		,[Treasury_Total] REAL
		,[PreviousCollectionNo] INT
		,[Treasury_Tokens] REAL
		,[Operator_Week_ID] INT
		,[Operator_Period_ID] INT
		,[CollectionHandHeldMetersReceived] INT
		,[CollectionTotalDurationDoor] INT
		,[Collection_NetEx] REAL
		,[Collection_VAT_Rate] REAL
		,[Collection_PoP_Actual] INT
		,[Collection_PoP_Configured] INT
		,[Collection_Meter_Status] INT
		,[Collection_Cash_Status] INT
		,[CASH_IN_1P] INT
		,[CASH_OUT_1P] INT
		,[User_Name] varchar(50) 
		,DecCashableInsertedValue    real  
		,DecCashablePrintedValue     real  
		,DecCashableInsertedQty      int  
		,DecCashablePrintedQty       int  
		,DecNonCashableInsertedValue real  
		,DecNonCashablePrintedValue  real  
		,DecNonCashableInsertedQty   int  
		,DecNonCashablePrintedQty    int  
		-- Adding the Collection table columns which are not there in Enterprise Collection Table - End
		
)

--------------- this block is to insert into the temp table for inserting into collection table	----------

--Create an internal representation of the XML document.

	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc

	INSERT INTO dbo.#TempCollection
	SELECT * FROM OPENXML(@idoc, './Batch/collection_batch/Installation/collection', 2) WITH dbo.#TempCollection

	IF @@Error <> 0
	BEGIN
		SET @IsSuccess = 'insert from XML to temp table failed'---1	--	insert from XML to temp table failed
		GOTO ErrorHandler
	END

	UPDATE dbo.#TempCollection SET Installation_No = NULL

	SELECT @liCount = COUNT(*) FROM dbo.#TempCollection 

---------------	code block ends	----------------------------------

--------------- this block is to obtain the Batch_ID for updating collection table	----------

	SELECT @lvcSite_Code = Site_Code,
		@liBatch_No = Batch_No,
		@lvcBatch_Date = CONVERT(VARCHAR, Batch_Date, 106),
		@lvcBatch_Time = CONVERT(VARCHAR, Batch_Date, 108),
		@lrBatch_Adjustment = Batch_Adjustment,
		@lvcUser_Name = [User_Name],
		@lvcBatch_Date_Performed = CONVERT(VARCHAR, Batch_Date_Performed, 106),
		@lvcBatch_Name = Batch_Name,
		@lfBatch_Advance = Batch_Advance,
		@BatchNegativeNet= Batch_Negative_Net--,
		,@userId=[User_No]
		--@Route_ID = Route_ID

		FROM OPENXML(@idoc, './Batch/collection_batch', 2)
				WITH (
						Batch_No	INT	'./Collection_Batch_No',
						Batch_Date	DATETIME	'./Collection_Batch_Date',
--						Batch_Time	VARCHAR(50)	'./Collection_Batch_Time',
						Batch_Adjustment	REAL	'./Collection_Batch_Adjustment',
						Batch_Date_Performed	DATETIME	'./Collection_Batch_Date_Performed',
						Batch_Name	VARCHAR(50)	'./Collection_Batch_Name',
						Batch_Advance	FLOAT	'./Collection_Batch_Advance',
						Site_Code	VARCHAR(50)	'./Installation/Site_Code',
						[User_Name]	VARCHAR(50)	'./Installation/User_Name',
						Batch_Negative_Net FLOAT './Batch_Negative_Net'--,
						,[User_No] int './User_No'
						--Route_ID INT './Route_ID'
					)

	EXEC dbo.usp_CheckBatchRef @lvcSite_Code, @liBatch_No, @lvcBatch_Date, @lvcBatch_Time, @lrBatch_Adjustment, @lvcUser_Name, @lvcBatch_Date_Performed, @lvcBatch_Name, @lfBatch_Advance,@BatchNegativeNet, @liHQ_Batch_ID OUTPUT
	--EXEC dbo.usp_CheckBatchRef @lvcSite_Code, @liBatch_No, @lvcBatch_Date, @lvcBatch_Time, @lrBatch_Adjustment, @lvcUser_Name, @lvcBatch_Date_Performed, @lvcBatch_Name, @lfBatch_Advance,@BatchNegativeNet,@Route_ID, @liHQ_Batch_ID OUTPUT

	IF ISNULL(@liHQ_Batch_ID, 0) = 0
	BEGIN
		SET @IsSuccess = 'No Batch is recognized'---2	-- this batch is already processed
		GOTO ErrorHandler
	END
	
	UPDATE dbo.#TempCollection SET Collection_Batch_No = @liHQ_Batch_ID,[User_Name]=(select [username] from [user] where SecurityUserID=@userId)

---------------	code block ends	----------------------------------

	UPDATE TC
		SET TC.Installation_No = HQ_Installation_No
	FROM OPENXML(@idoc, './Batch/collection_batch/Installation/collection', 2) 
		WITH (HQ_Installation_No	INT	'../HQ_Installation_No', Collection_No	INT	'./Collection_No') A
	INNER JOIN dbo.#TempCollection TC ON A.Collection_No = TC.Collection_No
	
		
	--Commented this to fix issue in importing multiple collections for single installation
	--UPDATE TC
	--	SET TC.Installation_No = HQ_Installation_No
	--FROM OPENXML(@idoc, './Batch/collection_batch/Installation', 2) 
	--	WITH (HQ_Installation_No	INT	'./HQ_Installation_No', Collection_No	INT	'./collection/Collection_No') A
	--INNER JOIN dbo.#TempCollection TC ON A.Collection_No = TC.Collection_No

	EXEC sp_xml_removedocument @idoc

	IF EXISTS(SELECT 1 FROM [Merged_Batch_Details] M INNER JOIN dbo.#TempCollection TC ON M.Merged_Batch_ID = TC.Collection_Batch_No OR M.Deleted_Batch_ID = TC.Collection_Batch_No)
		RETURN 0

	BEGIN TRAN

	------------------------------------
	----------Insert Code Start---------
	------------------------------------	

	INSERT INTO dbo.Collection(
								Batch_ID,
								Collection_Ref,
								Installation_ID,
								Collection_Date,
								Collection_Time,
								Collection_Date_Of_Collection,
								Collection_Time_Of_Collection,
								Previous_Collection_Date,
								Previous_Collection_Time,
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
								TOKEN_IN_5P,
								TOKEN_IN_10P,
								TOKEN_IN_20P,
								TOKEN_IN_50P,
								TOKEN_IN_100P,
								TOKEN_IN_200P,
								TOKEN_IN_500P,
								TOKEN_IN_1000P,
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
								TOKEN_REFILL_5P,
								TOKEN_REFILL_10P,
								TOKEN_REFILL_20P,
								TOKEN_REFILL_50P,
								TOKEN_REFILL_100P,
								TOKEN_REFILL_200P,
								TOKEN_REFILL_500P,
								TOKEN_REFILL_1000P,
								Declaration,
								CounterCashIn,
								CounterCashOut,
								CounterTokensIn,
								CounterTokensOut,
								CounterRefill,
								CounterPrize,
								CashCollected,
								TokensCollected,
								Cash_Collected_1p,
								Cash_Collected_2p,
								Cash_Collected_5p,
								Cash_Collected_10p,
								Cash_Collected_20p,
								Cash_Collected_50p,
								Cash_Collected_100p,
								Cash_Collected_200p,
								Cash_Collected_500p,
								Cash_Collected_1000p,
								Cash_Collected_2000p,
								Cash_Collected_5000p,
								Cash_Collected_10000p,
								Cash_Collected_20000p,
								Cash_Collected_50000p,
								Cash_Collected_100000p,
								CashRefills,
								TokenRefills,
								Cash_Refills_2p,
								Cash_Refills_5p,
								Cash_Refills_10p,
								Cash_Refills_20p,
								Cash_Refills_50p,
								Cash_Refills_100p,
								Cash_Refills_200p,
								Cash_Refills_500p,
								Cash_Refills_1000p,
								Cash_Refills_2000p,
								Cash_Refills_5000p,
								Cash_Refills_10000p,
								Cash_Refills_20000p,
								Cash_Refills_50000p,
								Cash_Refills_100000p,
								CounterCashInElectronic,
								CounterCashOutElectronic,
								CounterTokensInElectronic,
								CounterTokensOutElectronic,
								JackpotsOut,
								PreviousCounterCashIn,
								PreviousCounterCashOut,
								PreviousCounterTokensIn,
								PreviousCounterTokensOut,
								PreviousCounterCashInElectronic,
								PreviousCounterCashOutElectronic,
								PreviousCounterTokensInElectronic,
								PreviousCounterTokensOutElectronic,
								Collection_RDC_CoinsIn,
								Collection_RDC_CoinsOut,
								Collection_RDC_CoinsDrop,
								Collection_RDC_Handpay,
								Collection_RDC_ExternalCredit,
								Collection_RDC_GamesBet,
								Collection_RDC_GamesWon,
								Collection_RDC_Notes,
								Collection_Meters_CoinsIn,
								Collection_Meters_CoinsOut,
								Collection_Meters_CoinsDrop,
								Collection_Meters_Handpay,
								Collection_Meters_ExternalCredit,
								Collection_Meters_GamesBet,
								Collection_Meters_GamesWon,
								Collection_Meters_Notes,
								Collection_Treasury_Defloat,
								Previous_Meters_Coins_In,
								Previous_Meters_Coins_Out,
								Previous_Meters_Coin_Drop,
								Previous_Meters_Handpay,
								Previous_Meters_External_Credit,
								Previous_Meters_Games_Bet,
								Previous_Meters_Games_Won,
								Previous_Meters_Notes,
								PreviousCounterPrize,
								PreviousCounterJackpotsOut,
								PreviousCounterTournament,
								PreviousCounterJukebox,
								PreviousCounterRefills,
								Collection_Treasury_Handpay,
								Collection_RDC_VTP,
								Collection_RDC_Machine_Code,
								Collection_RDC_Secondary_Machine_Code,
								Collection_EDC_Status,
								CASH_FLOAT_CHANGE_1p,
								CASH_FLOAT_CHANGE_2p,
								CASH_FLOAT_CHANGE_5p,
								CASH_FLOAT_CHANGE_10p,
								CASH_FLOAT_CHANGE_20p,
								CASH_FLOAT_CHANGE_50p,
								CASH_FLOAT_CHANGE_100p,
								CASH_FLOAT_CHANGE_200p,
								CASH_FLOAT_CHANGE_500p,
								CASH_FLOAT_CHANGE_1000p,
								CASH_FLOAT_TOTAL,
								COLLECTION_RDC_CANCELLED_CREDITS,
								COLLECTION_RDC_GAMES_LOST,
								COLLECTION_RDC_GAMES_SINCE_POWER_UP,
								COLLECTION_RDC_TRUE_COIN_IN,
								COLLECTION_RDC_TRUE_COIN_OUT,
								COLLECTION_RDC_CURRENT_CREDITS,
								DeclaredTicketQty,
								DeclaredTicketValue,
								COLLECTION_RDC_JACKPOT,
								DeclaredTicketPrintedQty,
								DeclaredTicketPrintedValue,
								COLLECTION_RDC_TICKETS_INSERTED_VALUE,
								COLLECTION_RDC_TICKETS_PRINTED_VALUE,
								progressive_win_value,
								progressive_win_Handpay_value,
								Progressive_Value_Declared ,  
								Collection_Defloat_Collection,
								Mystery_Machine_Paid,
								Mystery_Attendant_Paid,
								RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
								RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
								Promo_Cashable_EFT_IN,
								Promo_Cashable_EFT_OUT,
								NonCashable_EFT_IN,
								NonCashable_EFT_OUT,
								Cashable_EFT_IN,
								Cashable_EFT_OUT,
								Previous_Collection_Date_Of_Collection,
								Previous_Collection_Time_Of_Collection,TICKETS_INSERTED_NONCASHABLE_QTY,TICKETS_INSERTED_QTY,
                                [Machine_Serial]
								,[Datapak_Read_Occurrence]
								,[Float_Level]
								,[Period_ID]
								,[Week_ID]
								,[Treasury_Total]
								,[PreviousCollectionNo]
								,[Treasury_Tokens]
								,[Operator_Week_ID]
								,[Operator_Period_ID]
								,[CollectionHandHeldMetersReceived]
								,[CollectionTotalDurationDoor]
								,[Collection_NetEx]
								,[Collection_VAT_Rate]
								,[Collection_PoP_Actual]
								,[Collection_PoP_Configured]
								,[Collection_Meter_Status]
								,[Collection_Cash_Status]
								,[CASH_IN_1P]
							    ,[CASH_OUT_1P]  
							    ,[User_Name]
							    ,DecCashableInsertedValue     
								,DecCashablePrintedValue      
								,DecCashableInsertedQty       
								,DecCashablePrintedQty        
								,DecNonCashableInsertedValue  
								,DecNonCashablePrintedValue   
								,DecNonCashableInsertedQty    
								,DecNonCashablePrintedQty     
	
       				)      
                                   
				SELECT			TC.Collection_Batch_No,
								@lvcSite_Code + ',' + Convert(VARCHAR, TC.Collection_No),
								TC.Installation_No,
								CONVERT(VARCHAR, TC.Collection_Date, 106),
								CONVERT(VARCHAR, TC.Collection_Date, 108),
								CONVERT(VARCHAR, TC.Collection_Date_Performed, 106),
								CONVERT(VARCHAR, TC.Collection_Date_Performed, 108),
								CONVERT(VARCHAR, TC.PreviousCollectionDate, 106),
								CONVERT(VARCHAR, TC.PreviousCollectionDate, 108),
								TC.CASH_IN_2P,
								TC.CASH_IN_5P,
								TC.CASH_IN_10P,
								TC.CASH_IN_20P,
								TC.CASH_IN_50P,
								TC.CASH_IN_100P,
								TC.CASH_IN_200P,
								TC.CASH_IN_500P,
								TC.CASH_IN_1000P,
								TC.CASH_IN_2000P,
								TC.CASH_IN_5000P,
								TC.CASH_IN_10000P,
								TC.CASH_IN_20000P,
								TC.CASH_IN_50000P,
								TC.CASH_IN_100000P,
								TC.TOKEN_IN_5P,
								TC.TOKEN_IN_10P,
								TC.TOKEN_IN_20P,
								TC.TOKEN_IN_50P,
								TC.TOKEN_IN_100P,
								TC.TOKEN_IN_200P,
								TC.TOKEN_IN_500P,
								TC.TOKEN_IN_1000P,
								TC.CASH_OUT_2P,
								TC.CASH_OUT_5P,
								TC.CASH_OUT_10P,
								TC.CASH_OUT_20P,
								TC.CASH_OUT_50P,
								TC.CASH_OUT_100P,
								TC.CASH_OUT_200P,
								TC.CASH_OUT_500P,
								TC.CASH_OUT_1000P,
								TC.CASH_OUT_2000P,	
								TC.CASH_OUT_5000P,
								TC.CASH_OUT_10000P,
								TC.CASH_OUT_20000P,
								TC.CASH_OUT_50000P,
								TC.CASH_OUT_100000P,
								TC.TOKEN_OUT_5P,
								TC.TOKEN_OUT_10P,
								TC.TOKEN_OUT_20P,
								TC.TOKEN_OUT_50P,
								TC.TOKEN_OUT_100P,
								TC.TOKEN_OUT_200P,
								TC.TOKEN_OUT_500P,
								TC.TOKEN_OUT_1000P,
								TC.CASH_REFILL_5P,
								TC.CASH_REFILL_10P,
								TC.CASH_REFILL_20P,
								TC.CASH_REFILL_50P,
								TC.CASH_REFILL_100P,
								TC.CASH_REFILL_200P,
								TC.CASH_REFILL_500P,
								TC.CASH_REFILL_1000P,
								TC.CASH_REFILL_2000P,
								TC.CASH_REFILL_5000P,
								TC.CASH_REFILL_10000P,
								TC.CASH_REFILL_20000P,
								TC.CASH_REFILL_50000P,
								TC.CASH_REFILL_100000P,
								TC.TOKEN_REFILL_5P,
								TC.TOKEN_REFILL_10P,
								TC.TOKEN_REFILL_20P,
								TC.TOKEN_REFILL_50P,
								TC.TOKEN_REFILL_100P,
								TC.TOKEN_REFILL_200P,
								TC.TOKEN_REFILL_500P,
								TC.TOKEN_REFILL_1000P,
								TC.Declaration,
								TC.CounterCashIn,
								TC.CounterCashOut,
								TC.CounterTokensIn,
								TC.CounterTokensOut,
								TC.CounterRefills,
								TC.CounterPrize,
								TC.CashCollected,
								TC.TokensCollected,
								ISNULL(TC.Cash_Collected_1p, 0) * 100,
								ISNULL(TC.Cash_Collected_2p, 0) * 100,
								ISNULL(TC.Cash_Collected_5p, 0) * 100,
								ISNULL(TC.Cash_Collected_10p, 0) * 100,
								ISNULL(TC.Cash_Collected_20p, 0) * 100,
								ISNULL(TC.Cash_Collected_50p, 0) * 100,
								ISNULL(TC.Cash_Collected_100p, 0) * 100,
								ISNULL(TC.Cash_Collected_200p, 0) * 100,
								ISNULL(TC.Cash_Collected_500p, 0) * 100,
								ISNULL(TC.Cash_Collected_1000p, 0) * 100,
								ISNULL(TC.Cash_Collected_2000p, 0) * 100,
								ISNULL(TC.Cash_Collected_5000p, 0) * 100,
								ISNULL(TC.Cash_Collected_10000p, 0) * 100,
								ISNULL(TC.Cash_Collected_20000p, 0) * 100,
								ISNULL(TC.Cash_Collected_50000p, 0) * 100,
								ISNULL(TC.Cash_Collected_100000p, 0) * 100,
								ISNULL(TC.CashRefills, 0) + ISNULL(TC.Treasury_Refills, 0),
								TC.TokenRefills,
								TC.Cash_Refills_2p,
								TC.Cash_Refills_5p,
								TC.Cash_Refills_10p,
								TC.Cash_Refills_20p,
								TC.Cash_Refills_50p,
								TC.Cash_Refills_100p,
								TC.Cash_Refills_200p,
								TC.Cash_Refills_500p,
								TC.Cash_Refills_1000p,
								TC.Cash_Refills_2000p,
								TC.Cash_Refills_5000p,
								TC.Cash_Refills_10000p,
								TC.Cash_Refills_20000p,
								TC.Cash_Refills_50000p,
								TC.Cash_Refills_100000p,
								TC.CounterCashInElectronic,
								TC.CounterCashOutElectronic,
								TC.CounterTokensInElectronic,
								TC.CounterTokensOutElectronic,
								TC.JackpotsOut,
								TC.PreviousCounterCashIn,
								TC.PreviousCounterCashOut,
								TC.PreviousCounterTokensIn,
								TC.PreviousCounterTokensOut,
								TC.PreviousCounterCashInElectronic,
								TC.PreviousCounterCashOutElectronic,
								TC.PreviousCounterTokensInElectronic,
								TC.PreviousCounterTokensOutElectronic,
								TC.COLLECTION_RDC_COINS_IN,
								TC.COLLECTION_RDC_COINS_OUT,
								TC.COLLECTION_RDC_COIN_DROP,
								TC.COLLECTION_RDC_HANDPAY,
								TC.COLLECTION_RDC_EXTERNAL_CREDIT,
								TC.COLLECTION_RDC_GAMES_BET,
								TC.COLLECTION_RDC_GAMES_WON,
								TC.COLLECTION_RDC_NOTES,
								TC.Collection_Meters_Coins_In,
								TC.Collection_Meters_Coins_Out,
								TC.Collection_Meters_Coin_Drop,
								TC.Collection_Meters_Handpay,
								TC.Collection_Meters_External_Credit,
								TC.Collection_Meters_Games_Bet,
								TC.Collection_Meters_Games_Won,
								TC.Collection_Meters_Notes,
								TC.Collection_Treasury_Defloat,
								TC.Previous_Meters_Coins_In,
								TC.Previous_Meters_Coins_Out,
								TC.Previous_Meters_Coin_Drop,
								TC.Previous_Meters_Handpay,
								TC.Previous_Meters_External_Credit,
								TC.Previous_Meters_Games_Bet,
								TC.Previous_Meters_Games_Won,
								TC.Previous_Meters_Notes,
								TC.PreviousCounterPrize,
								TC.PreviousCounterJackpotsOut,
								TC.PreviousCounterTournament,
								TC.PreviousCounterJukebox,
								TC.PreviousCounterRefills,
								TC.Treasury_Handpay,
								TC.COLLECTION_RDC_VTP,
								TC.Collection_RDC_Machine_Code,
								TC.Collection_RDC_Secondary_Machine_Code,
								TC.Collection_EDC_Status,
								TC.CASH_FLOAT_CHANGE_1p,
								TC.CASH_FLOAT_CHANGE_2p,
								TC.CASH_FLOAT_CHANGE_5p,
								TC.CASH_FLOAT_CHANGE_10p,
								TC.CASH_FLOAT_CHANGE_20p,
								TC.CASH_FLOAT_CHANGE_50p,
								TC.CASH_FLOAT_CHANGE_100p,
								TC.CASH_FLOAT_CHANGE_200p,
								TC.CASH_FLOAT_CHANGE_500p,
								TC.CASH_FLOAT_CHANGE_1000p,
								TC.CASH_FLOAT_TOTAL,
								TC.COLLECTION_RDC_CANCELLED_CREDITS,
								TC.COLLECTION_RDC_GAMES_LOST,
								TC.COLLECTION_RDC_GAMES_SINCE_POWER_UP,
								TC.COLLECTION_RDC_TRUE_COIN_IN,
								TC.COLLECTION_RDC_TRUE_COIN_OUT,
								TC.COLLECTION_RDC_CURRENT_CREDITS,
								TC.DeclaredTicketQty,
								TC.DeclaredTicketValue,
								TC.COLLECTION_RDC_JACKPOT,
								TC.DeclaredTicketPrintedQty,
								TC.DeclaredTicketPrintedValue,
								TC.COLLECTION_RDC_TICKETS_INSERTED_VALUE,
								TC.COLLECTION_RDC_TICKETS_PRINTED_VALUE,
								TC.progressive_win_value,
								TC.progressive_win_Handpay_value,
								TC.Progressive_Value_Declared,
								TC.Collection_Defloat_Collection,
								TC.Mystery_Machine_Paid,
								TC.Mystery_Attendant_Paid,
								TC.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
								TC.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
								TC.Promo_Cashable_EFT_IN,
								TC.Promo_Cashable_EFT_OUT,
								TC.NonCashable_EFT_IN,
								TC.NonCashable_EFT_OUT,
								TC.Cashable_EFT_IN,
								TC.Cashable_EFT_OUT,
								CONVERT(VARCHAR, TC.PreviousCollectionDatePerformed, 106),
								CONVERT(VARCHAR, TC.PreviousCollectionDatePerformed, 108),
								TC.TICKETS_INSERTED_NONCASHABLE_QTY,
								TC.TICKETS_INSERTED_QTY,

---***************************************** bEGIN
					         	TC.[Machine_Serial],
								TC.[Datapak_Read_Occurrence],
								TC.[Float_Level],
								TC.[Period_ID],
								TC.[Week_ID],
								TC.[Treasury_Total],
								TC.[PreviousCollectionNo],
								TC.[Treasury_Tokens],
								TC.[Operator_Week_ID],
								TC.[Operator_Period_ID],
								TC.[CollectionHandHeldMetersReceived],
								TC.[CollectionTotalDurationDoor],
								TC.[Collection_NetEx],
								TC.[Collection_VAT_Rate],
								TC.[Collection_PoP_Actual],
								TC.[Collection_PoP_Configured],
								TC.[Collection_Meter_Status],
								TC.[Collection_Cash_Status],
								TC.[CASH_IN_1P],
							        TC.[CASH_OUT_1P] ,
							        TC.[User_Name] , 
							        TC.DecCashableInsertedValue     
								,TC.DecCashablePrintedValue      
								,TC.DecCashableInsertedQty       
								,TC.DecCashablePrintedQty        
								,TC.DecNonCashableInsertedValue,  
								TC.DecNonCashablePrintedValue   
								,TC.DecNonCashableInsertedQty    
								,TC.DecNonCashablePrintedQty      


					FROM		dbo.#TempCollection TC
				LEFT JOIN		dbo.Collection C ON TC.Installation_No = C.Installation_ID AND CONVERT(VARCHAR, TC.Collection_Date, 106) = C.Collection_Date AND CONVERT(VARCHAR, TC.Collection_Date, 108) = C.Collection_Time AND TC.Collection_Batch_No = C.Batch_ID
					WHERE		C.Installation_ID IS NULL AND C.Collection_Date IS NULL AND C.Collection_Time IS NULL AND C.Batch_ID IS NULL
--					WHERE		ISNULL(Installation_No, 0) <> 0 AND 
--					Convert(Varchar(10),Installation_No) + ' ' + Convert(Varchar(20),Collection_Date) + ' ' + Convert(Varchar(10),Collection_Time) + ' ' + Convert(Varchar, Collection_Batch_No) NOT IN 
--					(SELECT Convert(Varchar(10),Installation_Id) + ' ' + Convert(Varchar(20),Collection_Date) + ' ' + Convert(Varchar(10),Collection_Time) + ' ' + Convert(Varchar, Batch_ID) FROM Collection)


					IF @@Error <> 0
					BEGIN
						SET @IsSuccess = 'not all records are inserted from XML to Collection table'---3		-- not all records are inserted
						GOTO ErrorHandler
					END

	------------------------------------
	----------Insert Code End---------
	------------------------------------

	------------------------------------
	----------Update Code Start---------
	------------------------------------


	UPDATE dbo.Collection
	SET Collection.Collection_Date = CONVERT(VARCHAR, TC.Collection_Date, 106),
		Collection.Collection_Time = CONVERT(VARCHAR, TC.Collection_Date, 108),
		Collection.Collection_Date_Of_Collection = CONVERT(VARCHAR, TC.Collection_Date_Performed, 106),
		Collection.Collection_Time_Of_Collection = CONVERT(VARCHAR, TC.Collection_Date_Performed, 108),
		Collection.Previous_Collection_Date = CONVERT(VARCHAR, TC.PreviousCollectionDate, 106),
		Collection.Previous_Collection_Time = CONVERT(VARCHAR, TC.PreviousCollectionDate, 108),
		Collection.CASH_IN_2P = TC.CASH_IN_2P,	
		Collection.CASH_IN_5P = TC.CASH_IN_5P,	
		Collection.CASH_IN_10P = TC.CASH_IN_10P,	
		Collection.CASH_IN_20P = TC.CASH_IN_20P,	
		Collection.CASH_IN_50P = TC.CASH_IN_50P,	
		Collection.CASH_IN_100P = TC.CASH_IN_100P,	
		Collection.CASH_IN_200P = TC.CASH_IN_200P,	
		Collection.CASH_IN_500P = TC.CASH_IN_500P,	
		Collection.CASH_IN_1000P = TC.CASH_IN_1000P,	
		Collection.CASH_IN_2000P = TC.CASH_IN_2000P,	
		Collection.CASH_IN_5000P = TC.CASH_IN_5000P,	
		Collection.CASH_IN_10000P = TC.CASH_IN_10000P,	
		Collection.CASH_IN_20000P = TC.CASH_IN_20000P,	
		Collection.CASH_IN_50000P = TC.CASH_IN_50000P,	
		Collection.CASH_IN_100000P = TC.CASH_IN_100000P,	
		Collection.TOKEN_IN_5P = TC.TOKEN_IN_5P,	
		Collection.TOKEN_IN_10P = TC.TOKEN_IN_10P,	
		Collection.TOKEN_IN_20P = TC.TOKEN_IN_20P,	
		Collection.TOKEN_IN_50P = TC.TOKEN_IN_50P,	
		Collection.TOKEN_IN_100P = TC.TOKEN_IN_100P,	
		Collection.TOKEN_IN_200P = TC.TOKEN_IN_200P,	
		Collection.TOKEN_IN_500P = TC.TOKEN_IN_500P,	
		Collection.TOKEN_IN_1000P = TC.TOKEN_IN_1000P,	
		Collection.CASH_OUT_2P = TC.CASH_OUT_2P,	
		Collection.CASH_OUT_5P = TC.CASH_OUT_5P,	
		Collection.CASH_OUT_10P = TC.CASH_OUT_10P,	
		Collection.CASH_OUT_20P = TC.CASH_OUT_20P,	
		Collection.CASH_OUT_50P = TC.CASH_OUT_50P,	
		Collection.CASH_OUT_100P = TC.CASH_OUT_100P,	
		Collection.CASH_OUT_200P = TC.CASH_OUT_200P,	
		Collection.CASH_OUT_500P = TC.CASH_OUT_500P,	
		Collection.CASH_OUT_1000P = TC.CASH_OUT_1000P,	
		Collection.CASH_OUT_2000P = TC.CASH_OUT_2000P,	
		Collection.CASH_OUT_5000P = TC.CASH_OUT_5000P,	
		Collection.CASH_OUT_10000P = TC.CASH_OUT_10000P,	
		Collection.CASH_OUT_20000P = TC.CASH_OUT_20000P,	
		Collection.CASH_OUT_50000P = TC.CASH_OUT_50000P,	
		Collection.CASH_OUT_100000P = TC.CASH_OUT_100000P,	
		Collection.TOKEN_OUT_5P = TC.TOKEN_OUT_5P,	
		Collection.TOKEN_OUT_10P = TC.TOKEN_OUT_10P,	
		Collection.TOKEN_OUT_20P = TC.TOKEN_OUT_20P,	
		Collection.TOKEN_OUT_50P = TC.TOKEN_OUT_50P,	
		Collection.TOKEN_OUT_100P = TC.TOKEN_OUT_100P,	
		Collection.TOKEN_OUT_200P = TC.TOKEN_OUT_200P,	
		Collection.TOKEN_OUT_500P = TC.TOKEN_OUT_500P,	
		Collection.TOKEN_OUT_1000P = TC.TOKEN_OUT_1000P,	
		Collection.CASH_REFILL_5P = TC.CASH_REFILL_5P,	
		Collection.CASH_REFILL_10P = TC.CASH_REFILL_10P,	
		Collection.CASH_REFILL_20P = TC.CASH_REFILL_20P,	
		Collection.CASH_REFILL_50P = TC.CASH_REFILL_50P,	
		Collection.CASH_REFILL_100P = TC.CASH_REFILL_100P,	
		Collection.CASH_REFILL_200P = TC.CASH_REFILL_200P,	
		Collection.CASH_REFILL_500P = TC.CASH_REFILL_500P,	
		Collection.CASH_REFILL_1000P = TC.CASH_REFILL_1000P,	
		Collection.CASH_REFILL_2000P = TC.CASH_REFILL_2000P,	
		Collection.CASH_REFILL_5000P = TC.CASH_REFILL_5000P,	
		Collection.CASH_REFILL_10000P = TC.CASH_REFILL_10000P,	
		Collection.CASH_REFILL_20000P = TC.CASH_REFILL_20000P,	
		Collection.CASH_REFILL_50000P = TC.CASH_REFILL_50000P,	
		Collection.CASH_REFILL_100000P = TC.CASH_REFILL_100000P,	
		Collection.TOKEN_REFILL_5P = TC.TOKEN_REFILL_5P,	
		Collection.TOKEN_REFILL_10P = TC.TOKEN_REFILL_10P,	
		Collection.TOKEN_REFILL_20P = TC.TOKEN_REFILL_20P,	
		Collection.TOKEN_REFILL_50P = TC.TOKEN_REFILL_50P,	
		Collection.TOKEN_REFILL_100P = TC.TOKEN_REFILL_100P,	
		Collection.TOKEN_REFILL_200P = TC.TOKEN_REFILL_200P,	
		Collection.TOKEN_REFILL_500P = TC.TOKEN_REFILL_500P,	
		Collection.TOKEN_REFILL_1000P = TC.TOKEN_REFILL_1000P,	
		Collection.Declaration = TC.Declaration,	
		Collection.CounterCashIn = TC.CounterCashIn,	
		Collection.CounterCashOut = TC.CounterCashOut,	
		Collection.CounterTokensIn = TC.CounterTokensIn,	
		Collection.CounterTokensOut = TC.CounterTokensOut,	
		Collection.CounterRefill = TC.CounterRefills,	
		Collection.CounterPrize = TC.CounterPrize,	
		Collection.CashCollected = TC.CashCollected,	
		Collection.TokensCollected = TC.TokensCollected,
		Collection.Cash_Collected_1p = ISNULL(TC.Cash_Collected_1p, 0) * 100,		
		Collection.Cash_Collected_2p = ISNULL(TC.Cash_Collected_2p, 0) * 100,	
		Collection.Cash_Collected_5p = ISNULL(TC.Cash_Collected_5p, 0) * 100,	
		Collection.Cash_Collected_10p = ISNULL(TC.Cash_Collected_10p, 0) * 100,	
		Collection.Cash_Collected_20p = ISNULL(TC.Cash_Collected_20p, 0) * 100,	
		Collection.Cash_Collected_50p = ISNULL(TC.Cash_Collected_50p, 0) * 100,	
		Collection.Cash_Collected_100p = ISNULL(TC.Cash_Collected_100p, 0) * 100,	
		Collection.Cash_Collected_200p = ISNULL(TC.Cash_Collected_200p, 0) * 100,	
		Collection.Cash_Collected_500p = ISNULL(TC.Cash_Collected_500p, 0) * 100,	
		Collection.Cash_Collected_1000p = ISNULL(TC.Cash_Collected_1000p, 0) * 100,	
		Collection.Cash_Collected_2000p = ISNULL(TC.Cash_Collected_2000p, 0) * 100,	
		Collection.Cash_Collected_5000p = ISNULL(TC.Cash_Collected_5000p, 0) * 100,	
		Collection.Cash_Collected_10000p = ISNULL(TC.Cash_Collected_10000p, 0) * 100,	
		Collection.Cash_Collected_20000p = ISNULL(TC.Cash_Collected_20000p, 0) * 100,	
		Collection.Cash_Collected_50000p = ISNULL(TC.Cash_Collected_50000p, 0) * 100,	
		Collection.Cash_Collected_100000p = ISNULL(TC.Cash_Collected_100000p, 0) * 100,	
		Collection.CashRefills = ISNULL(TC.CashRefills, 0) + ISNULL(TC.Treasury_Refills, 0),	
		Collection.TokenRefills = TC.TokenRefills,	
		Collection.Cash_Refills_2p = TC.Cash_Refills_2p,	
		Collection.Cash_Refills_5p = TC.Cash_Refills_5p,	
		Collection.Cash_Refills_10p = TC.Cash_Refills_10p,	
		Collection.Cash_Refills_20p = TC.Cash_Refills_20p,	
		Collection.Cash_Refills_50p = TC.Cash_Refills_50p,	
		Collection.Cash_Refills_100p = TC.Cash_Refills_100p,	
		Collection.Cash_Refills_200p = TC.Cash_Refills_200p,	
		Collection.Cash_Refills_500p = TC.Cash_Refills_500p,	
		Collection.Cash_Refills_1000p = TC.Cash_Refills_1000p,	
		Collection.Cash_Refills_2000p = TC.Cash_Refills_2000p,	
		Collection.Cash_Refills_5000p = TC.Cash_Refills_5000p,	
		Collection.Cash_Refills_10000p = TC.Cash_Refills_10000p,	
		Collection.Cash_Refills_20000p = TC.Cash_Refills_20000p,	
		Collection.Cash_Refills_50000p = TC.Cash_Refills_50000p,	
		Collection.Cash_Refills_100000p = TC.Cash_Refills_100000p,	
		Collection.CounterCashInElectronic = TC.CounterCashInElectronic,	
		Collection.CounterCashOutElectronic = TC.CounterCashOutElectronic,	
		Collection.CounterTokensInElectronic = TC.CounterTokensInElectronic,	
		Collection.CounterTokensOutElectronic = TC.CounterTokensOutElectronic,	
		Collection.JackpotsOut = TC.JackpotsOut,	
		Collection.PreviousCounterCashIn = TC.PreviousCounterCashIn,	
		Collection.PreviousCounterCashOut = TC.PreviousCounterCashOut,	
		Collection.PreviousCounterTokensIn = TC.PreviousCounterTokensIn,	
		Collection.PreviousCounterTokensOut = TC.PreviousCounterTokensOut,	
		Collection.PreviousCounterCashInElectronic = TC.PreviousCounterCashInElectronic,	
		Collection.PreviousCounterCashOutElectronic = TC.PreviousCounterCashOutElectronic,	
		Collection.PreviousCounterTokensInElectronic = TC.PreviousCounterTokensInElectronic,	
		Collection.PreviousCounterTokensOutElectronic = TC.PreviousCounterTokensOutElectronic,	
		Collection.Collection_RDC_CoinsIn = TC.COLLECTION_RDC_COINS_IN,	
		Collection.Collection_RDC_CoinsOut = TC.COLLECTION_RDC_COINS_OUT,	
		Collection.Collection_RDC_CoinsDrop = TC.COLLECTION_RDC_COIN_DROP,	
		Collection.Collection_RDC_Handpay = TC.COLLECTION_RDC_HANDPAY,	
		Collection.Collection_RDC_ExternalCredit = TC.COLLECTION_RDC_EXTERNAL_CREDIT,	
		Collection.Collection_RDC_GamesBet = TC.COLLECTION_RDC_GAMES_BET,	
		Collection.Collection_RDC_GamesWon = TC.COLLECTION_RDC_GAMES_WON,	
		Collection.Collection_RDC_Notes = TC.COLLECTION_RDC_NOTES,	
		Collection.Collection_Meters_CoinsIn = TC.Collection_Meters_Coins_In,	
		Collection.Collection_Meters_CoinsOut = TC.Collection_Meters_Coins_Out,	
		Collection.Collection_Meters_CoinsDrop = TC.Collection_Meters_Coin_Drop,	
		Collection.Collection_Meters_Handpay = TC.Collection_Meters_Handpay,	
		Collection.Collection_Meters_ExternalCredit = TC.Collection_Meters_External_Credit,	
		Collection.Collection_Meters_GamesBet = TC.Collection_Meters_Games_Bet,	
		Collection.Collection_Meters_GamesWon = TC.Collection_Meters_Games_Won,	
		Collection.Collection_Meters_Notes = TC.Collection_Meters_Notes,	
		Collection.Collection_Treasury_Defloat = TC.Collection_Treasury_Defloat,	
		Collection.Previous_Meters_Coins_In = TC.Previous_Meters_Coins_In,	
		Collection.Previous_Meters_Coins_Out = TC.Previous_Meters_Coins_Out,	
		Collection.Previous_Meters_Coin_Drop = TC.Previous_Meters_Coin_Drop,	
		Collection.Previous_Meters_Handpay = TC.Previous_Meters_Handpay,	
		Collection.Previous_Meters_External_Credit = TC.Previous_Meters_External_Credit,	
		Collection.Previous_Meters_Games_Bet = TC.Previous_Meters_Games_Bet,	
		Collection.Previous_Meters_Games_Won = TC.Previous_Meters_Games_Won,	
		Collection.Previous_Meters_Notes = TC.Previous_Meters_Notes,	
		Collection.PreviousCounterPrize = TC.PreviousCounterPrize,	
		Collection.PreviousCounterJackpotsOut = TC.PreviousCounterJackpotsOut,	
		Collection.PreviousCounterTournament = TC.PreviousCounterTournament,	
		Collection.PreviousCounterJukebox = TC.PreviousCounterJukebox,	
		Collection.PreviousCounterRefills = TC.PreviousCounterRefills,	
		Collection.Collection_Treasury_Handpay = TC.Treasury_Handpay,	
		Collection.Collection_RDC_VTP = TC.COLLECTION_RDC_VTP,	
		Collection.Collection_RDC_Machine_Code = TC.Collection_RDC_Machine_Code,	
		Collection.Collection_RDC_Secondary_Machine_Code = TC.Collection_RDC_Secondary_Machine_Code,	
		Collection.Collection_EDC_Status = TC.Collection_EDC_Status,	
		Collection.CASH_FLOAT_CHANGE_1p = TC.CASH_FLOAT_CHANGE_1p,	
		Collection.CASH_FLOAT_CHANGE_2p = TC.CASH_FLOAT_CHANGE_2p,	
		Collection.CASH_FLOAT_CHANGE_5p = TC.CASH_FLOAT_CHANGE_5p,	
		Collection.CASH_FLOAT_CHANGE_10p = TC.CASH_FLOAT_CHANGE_10p,	
		Collection.CASH_FLOAT_CHANGE_20p = TC.CASH_FLOAT_CHANGE_20p,	
		Collection.CASH_FLOAT_CHANGE_50p = TC.CASH_FLOAT_CHANGE_50p,	
		Collection.CASH_FLOAT_CHANGE_100p = TC.CASH_FLOAT_CHANGE_100p,	
		Collection.CASH_FLOAT_CHANGE_200p = TC.CASH_FLOAT_CHANGE_200p,	
		Collection.CASH_FLOAT_CHANGE_500p = TC.CASH_FLOAT_CHANGE_500p,	
		Collection.CASH_FLOAT_CHANGE_1000p = TC.CASH_FLOAT_CHANGE_1000p,	
		Collection.CASH_FLOAT_TOTAL = TC.CASH_FLOAT_TOTAL,	
		Collection.COLLECTION_RDC_CANCELLED_CREDITS = TC.COLLECTION_RDC_CANCELLED_CREDITS,	
		Collection.COLLECTION_RDC_GAMES_LOST = TC.COLLECTION_RDC_GAMES_LOST,	
		Collection.COLLECTION_RDC_GAMES_SINCE_POWER_UP = TC.COLLECTION_RDC_GAMES_SINCE_POWER_UP,	
		Collection.COLLECTION_RDC_TRUE_COIN_IN = TC.COLLECTION_RDC_TRUE_COIN_IN,	
		Collection.COLLECTION_RDC_TRUE_COIN_OUT = TC.COLLECTION_RDC_TRUE_COIN_OUT,	
		Collection.COLLECTION_RDC_CURRENT_CREDITS = TC.COLLECTION_RDC_CURRENT_CREDITS,	
		Collection.DeclaredTicketQty = TC.DeclaredTicketQty,	
		Collection.DeclaredTicketValue = TC.DeclaredTicketValue,	
		Collection.COLLECTION_RDC_JACKPOT = TC.COLLECTION_RDC_JACKPOT,	
		Collection.DeclaredTicketPrintedQty = TC.DeclaredTicketPrintedQty,	
		Collection.DeclaredTicketPrintedValue = TC.DeclaredTicketPrintedValue,	
		Collection.COLLECTION_RDC_TICKETS_INSERTED_VALUE = TC.COLLECTION_RDC_TICKETS_INSERTED_VALUE,	
		Collection.COLLECTION_RDC_TICKETS_PRINTED_VALUE = TC.COLLECTION_RDC_TICKETS_PRINTED_VALUE,	
		Collection.progressive_win_value = TC.progressive_win_value,	
		Collection.progressive_win_Handpay_value = TC.progressive_win_Handpay_value	,
		Collection.Progressive_Value_Declared = TC.Progressive_Value_Declared,
		Collection.Collection_Defloat_Collection=TC.Collection_Defloat_Collection,
		Collection.Mystery_Machine_Paid = TC.Mystery_Machine_Paid,
		Collection.Mystery_Attendant_Paid = TC.Mystery_Attendant_Paid,
		Collection.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE = TC.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
		Collection.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE = TC.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
		Collection.Promo_Cashable_EFT_IN = TC.Promo_Cashable_EFT_IN,
		Collection.Promo_Cashable_EFT_OUT = TC.Promo_Cashable_EFT_OUT,
		Collection.NonCashable_EFT_IN = TC.NonCashable_EFT_IN,
		Collection.NonCashable_EFT_OUT = TC.NonCashable_EFT_OUT,
		Collection.Cashable_EFT_IN = TC.Cashable_EFT_IN,
		Collection.Cashable_EFT_OUT = TC.Cashable_EFT_OUT,
		Collection.Previous_Collection_Date_Of_Collection = CONVERT(VARCHAR, TC.PreviousCollectionDatePerformed, 106),
		Collection.Previous_Collection_Time_Of_Collection = CONVERT(VARCHAR, TC.PreviousCollectionDatePerformed, 108),
		Collection.TICKETS_INSERTED_QTY = TC.TICKETS_INSERTED_QTY,
        Collection.TICKETS_INSERTED_NONCASHABLE_QTY = TC.TICKETS_INSERTED_NONCASHABLE_QTY,

       ----*********************************************************---------
								
    Collection.[Machine_Serial] =   	TC.[Machine_Serial],
	Collection.[Datapak_Read_Occurrence] = 	TC.[Datapak_Read_Occurrence],
	Collection.[Float_Level]=	TC.[Float_Level],
	Collection.[Period_ID] = TC.[Period_ID],
	Collection.[Week_ID] = TC.[Week_ID],
	Collection.[Treasury_Total] = TC.[Treasury_Total],
	Collection.[PreviousCollectionNo] = TC.[PreviousCollectionNo],
	Collection.[Treasury_Tokens]=TC.[Treasury_Tokens],
	Collection.[Operator_Week_ID]=TC.[Operator_Week_ID],
	Collection.[Operator_Period_ID]=TC.[Operator_Period_ID],
	Collection.[CollectionHandHeldMetersReceived]=TC.[CollectionHandHeldMetersReceived],
	Collection.[CollectionTotalDurationDoor]=	TC.[CollectionTotalDurationDoor],
	Collection.[Collection_NetEx]=TC.[Collection_NetEx],
	Collection.[Collection_VAT_Rate]=TC.[Collection_VAT_Rate],
	Collection.[Collection_PoP_Actual]=TC.[Collection_PoP_Actual],
	Collection.[Collection_PoP_Configured]=TC.[Collection_PoP_Configured],
	Collection.[Collection_Meter_Status]=TC.[Collection_Meter_Status],
	Collection.[Collection_Cash_Status]=TC.[Collection_Cash_Status],
	Collection.[CASH_IN_1P]=TC.[CASH_IN_1P],
 Collection.[CASH_OUT_1P]= TC.[CASH_OUT_1P]  ,  
 Collection.[User_Name]=TC.[User_Name],
 collection.DecCashableInsertedValue=TC.DecCashableInsertedValue     
,collection.DecCashablePrintedValue = TC.DecCashablePrintedValue      
,collection.DecCashableInsertedQty = TC.DecCashableInsertedQty       
,collection.DecCashablePrintedQty =TC.DecCashablePrintedQty        
,collection.DecNonCashableInsertedValue = TC.DecNonCashableInsertedValue,  
collection.DecNonCashablePrintedValue = TC.DecNonCashablePrintedValue   
,collection.DecNonCashableInsertedQty =TC.DecNonCashableInsertedQty    
,collection.DecNonCashablePrintedQty = TC.DecNonCashablePrintedQty      


		FROM		dbo.#TempCollection TC INNER JOIN  Collection CO
		ON TC.Installation_No = CO.Installation_Id AND CONVERT(VARCHAR, TC.Collection_Date, 106) = CO.Collection_Date AND CONVERT(VARCHAR, TC.Collection_Date, 108) = CO.Collection_Time AND TC.Collection_Batch_No = CO.Batch_ID
		WHERE ISNULL(Installation_No, 0) <> 0  
		
		IF @@RowCount <> @liCount
		BEGIN
			SET @IsSuccess = 'not all records are updated from XML to Collection table'---3		-- not all records are inserted
			GOTO ErrorHandler
		END

	------------------------------------
	----------Update Code End---------
	------------------------------------

			WHILE @liStartPos <= @liCount
			BEGIN
					SELECT @liCollection_ID = C.Collection_ID, 
							@liInstallation_ID = C.Installation_ID 
					FROM dbo.Collection C 
				INNER JOIN dbo.#TempCollection TC 
					ON (C.Batch_ID = TC.Collection_Batch_No AND C.Installation_ID = TC.Installation_No

					AND C.Collection_Date = CONVERT(VARCHAR, TC.Collection_Date, 106)
					AND C.Collection_Time = CONVERT(VARCHAR, TC.Collection_Date, 108))
					AND TC.Sno = @liStartPos
					
			-- Get Previous & Current Meter History ID for Updating MH_LinkReference with Collection_ID
			SELECT TOP 1 @prevCollID = MH_ID FROM Meter_history MH WHERE MH.MH_Installation_No = @liInstallation_ID
			AND MH.MH_Process = 'COLL' AND MH.MH_Type = 'P' AND MH_LinkReference IS NULL ORDER BY MH_ID ASC

			SELECT TOP 1 @currentCollID = MH_ID FROM Meter_history WHERE MH_Installation_No = @liInstallation_ID
			AND MH_Process = 'COLL' AND MH_Type = 'C' AND MH_LinkReference IS NULL ORDER BY MH_ID ASC

			IF(ISNULL(@prevCollID,0) <> 0 AND ISNULL(@currentCollID,0) <> 0)
			BEGIN
				UPDATE Meter_History SET MH_LinkReference = @liCollection_ID
				WHERE MH_Installation_No = @liInstallation_ID AND MH_Process = 'COLL' 
				AND MH_ID IN (@prevCollID, @currentCollID) AND MH_LinkReference IS NULL
			END

			EXEC dbo.usp_InsertCollDetailfromCollection @lvcSite_Code, @liCollection_ID, @liInstallation_ID, @IsSuccess OUTPUT

			select @liStartPos, @lvcSite_Code, @liCollection_ID, @liInstallation_ID, @IsSuccess 

				IF @IsSuccess <> 'SUCCESS'
					GOTO ErrorHandler

        -- set current period end flag on the collection details record
        EXEC esp_AssignCollectionToPeriodEnd @liCollection_ID, @liInstallation_ID


				UPDATE BP
					SET Bar_Position_Last_Collection_Date = CONVERT(VARCHAR, TC.Collection_Date, 106),
						Bar_Position_Last_Collection_ID = @liCollection_ID
				FROM dbo.Bar_Position BP
			INNER JOIN dbo.Installation I ON I.Bar_Position_ID = BP.Bar_Position_ID
			INNER JOIN dbo.#TempCollection TC ON I.Installation_ID = TC.Installation_No
				WHERE I.Installation_ID = @liInstallation_ID
				
				SET @liStartPos = @liStartPos + 1

			END

    -- recalculate the negative net value from this batch
    EXEC esp_Calculate_Batch_Negative_Net @liHQ_Batch_ID 

--	PRINT @@TRANCOUNT

	IF @@TRANCOUNT > 0
		COMMIT TRAN

RETURN 0
					
ErrorHandler:

IF @@TRANCOUNT > 0
	ROLLBACK TRAN

RETURN -99


END

GO

