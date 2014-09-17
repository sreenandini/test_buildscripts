USE Enterprise
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'usp_InsertOtherCollectionDetailsfromXML_114'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE usp_InsertOtherCollectionDetailsfromXML_114
END
GO

CREATE PROCEDURE [dbo].[usp_InsertOtherCollectionDetailsfromXML_114]
  @doc VARCHAR(MAX),
  @IsSuccess	VARCHAR(500)	OUTPUT
AS

BEGIN

SET NOCOUNT ON

DECLARE @iDoc	INT
DECLARE @iCount	INT
DECLARE @iBatch_No	INT
DECLARE @vcSite_Code	VARCHAR(50)
DECLARE @iCollection_ID	INT


DECLARE @iTreasury_No	INT
DECLARE @iPart_Collection_No	INT
DECLARE @iDoor_Event_No	INT
DECLARE @iPower_Event_No	INT
DECLARE @iFault_Event_No	INT

DECLARE @CollTable	TABLE
(
	Collection_ID	INT,
	Installation_ID	INT
)


--DECLARE #Treasury	TABLE
--(
--	Installation_ID	INT,
--	[User_Name]	VARCHAR(50),
--	Treasury_Date	VARCHAR(30),
--	Treasury_Time	VARCHAR(50),
--	Treasury_Amount	REAL,
--	Treasury_Reason	VARCHAR(200),
--	Treasury_Allocated	BIT,
--	Treasury_Type	VARCHAR(50),
--	Treasury_Temp	BIT,
--	Treasury_Docket_No	VARCHAR(50),
--	Treasury_Breakdown_2000p	REAL,
--	Treasury_Breakdown_1000p	REAL,
--	Treasury_Breakdown_500p	REAL,
--	Treasury_Breakdown_200p	REAL,
--	Treasury_Breakdown_100p	REAL,
--	Treasury_Breakdown_50p	REAL,
--	Treasury_Breakdown_20p	REAL,
--	Treasury_Breakdown_10p	REAL,
--	Treasury_Breakdown_5p	REAL,
--	Treasury_Breakdown_2p	REAL,
--	Treasury_Float_Issued_By	INT,
--	Treasury_Float_Recovered_Total	REAL,
--	Treasury_Float_Recovered_2000p	REAL,
--	Treasury_Float_Recovered_1000p	REAL,
--	Treasury_Float_Recovered_500p	REAL,
--	Treasury_Float_Recovered_200p	REAL,
--	Treasury_Float_Recovered_100p	REAL,
--	Treasury_Float_Recovered_50p	REAL,
--	Treasury_Float_Recovered_20p	REAL,
--	Treasury_Float_Recovered_10p	REAL,
--	Treasury_Float_Recovered_5p	REAL,
--	Treasury_Float_Recovered_2p	REAL,
--	Treasury_Membership_No	VARCHAR(50),
--	Treasury_Actual_Date DATETIME
--)

CREATE TABLE #Treasury 
(
	Installation_ID	INT,
	Treasury_Date	VARCHAR(30),
	Treasury_Time	VARCHAR(50),
	Treasury_Actual_Date	DATETIME
--	Treasury_Type	VARCHAR(50)
)


CREATE TABLE #Door
(
	Collection_ID	INT,
	Installation_ID	INT,
	Duration	FLOAT,
	Door_Date	VARCHAR(30),
	Door_Time	VARCHAR(8),
	Door_Event_Type	VARCHAR(50),
	Key_Owner	VARCHAR(50),
	Door_Polled	BIT,
	Door_Cleared_By	VARCHAR(50),
	Door_Cleared_Date	VARCHAR(30),
	Door_Cleared_Time	VARCHAR(50)
)

CREATE TABLE #Power
(
	Collection_ID	INT,
	Installation_ID	INT,
	Duration	FLOAT,
	Power_Date	VARCHAR(30),
	Power_Time	VARCHAR(50),
	VTP	INT,
	Power_Polled	BIT,
	Power_Cleared_By	INT,
	Power_Cleared_Date	VARCHAR(30),
	Power_Cleared_Time	VARCHAR(50)
)

CREATE TABLE #Fault
(
	Collection_ID	INT,
	Installation_ID	INT,
	Fault_Source	INT,
	Fault_ID	INT,
	Fault_Notes	VARCHAR(100),
	Fault_Description	VARCHAR(50),
	Fault_Date	VARCHAR(30),
	Fault_Time	VARCHAR(50),
	Fault_Cleared_By	INT,
	Fault_Cleared_Date	VARCHAR(30),
	Fault_Cleared_Time	VARCHAR(50)
)

	SET @IsSuccess = 'SUCCESS'

	SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc

	EXEC sp_xml_PrepareDocument @idoc OUTPUT, @doc

	SELECT @iBatch_No = Collection_Batch_No, 
			@vcSite_Code = Site_Code FROM OPENXML(@idoc, './CollectionDetails/Site', 2) WITH
			(Collection_Batch_No	INT	'./Collection_Batch_No',
			 Site_Code	VARCHAR(20)	'./Site_Code'
			)

/*	the below select statement is written just to know if there
	are any records corresponding to them.	*/

	SELECT @iTreasury_No = Treasury_No, 
			@iPart_Collection_No = Part_Collection_No,
			@iDoor_Event_No = Door_Event_No,
			@iPower_Event_No = Power_Event_No,
			@iFault_Event_No = Fault_Event_No
	FROM OPENXML(@idoc, './CollectionDetails', 2)	WITH
		(
			Treasury_No	INT	'./TreasuryDetails/Treasury_Detail/Treasury_No',
			Part_Collection_No	INT	'./PartCollections/Part_Collection/Part_Collection_No',
			Door_Event_No	INT	'./DoorEvents/Door_Event/Door_Event_No',
			Power_Event_No	INT	'./PowerEvents/Power_Event/Power_Event_No',
			Fault_Event_No	INT	'./FaultEvents/Fault_Event/Fault_Event_No'
		)
	--SELECT @iPart_Collection_No 
--	SELECT @iCollection_ID = Collection_ID 
--			FROM dbo.Collection C 
--	INNER JOIN dbo.Batch B ON C.Batch_ID = B.Batch_ID 
--	WHERE SUBSTRING(B.Batch_Ref, CHARINDEX(',', B.Batch_Ref) + 1, LEN(B.Batch_Ref) - CHARINDEX(',', B.Batch_Ref)) = @iBatch_No

	INSERT INTO @CollTable
	SELECT C.Collection_ID, C.Installation_ID
	FROM	dbo.Collection C
	INNER JOIN dbo.Batch B ON C.Batch_ID = B.Batch_ID 
	WHERE B.Batch_Ref = @vcSite_Code + ',' + CAST(@iBatch_No AS VARCHAR)

--	IF @iCollection_ID IS NULL
	IF NOT EXISTS(SELECT * FROM @CollTable)
	BEGIN
		SET @IsSuccess = 'No corresponding collection_id exists'---8	-- No collection_id exists
		GOTO Err
	END

	IF @iTreasury_No IS NOT NULL
	BEGIN

--		INSERT INTO #Treasury
--		SELECT * FROM OPENXML(@idoc, './CollectionDetails/TreasuryDetails/Treasury_Detail', 2) WITH
--			(
--				Installation_ID	INT	'./HQ_Installation_No',
--				[User_Name]	VARCHAR(50)	'./User_Name',
--				Treasury_Date	VARCHAR(30)	'./Treasury_Date',
--				Treasury_Time	VARCHAR(50)	'./Treasury_Time',
--				Treasury_Amount	REAL	'./Treasury_Amount',
--				Treasury_Reason	VARCHAR(200)	'./Treasury_Reason',
--				Treasury_Allocated	BIT	'./Treasury_Allocated',
--				Treasury_Type	VARCHAR(50)	'./Treasury_Type',
--				Treasury_Temp	BIT	'./Treasury_Temp',
--				Treasury_Docket_No	VARCHAR(50)	'./Treasury_Docket_No',
--				Treasury_Breakdown_2000p	REAL	'./Treasury_Breakdown_2000p',
--				Treasury_Breakdown_1000p	REAL	'./Treasury_Breakdown_1000p',
--				Treasury_Breakdown_500p	REAL	'./Treasury_Breakdown_500p',
--				Treasury_Breakdown_200p	REAL	'./Treasury_Breakdown_200p',
--				Treasury_Breakdown_100p	REAL	'./Treasury_Breakdown_100p',
--				Treasury_Breakdown_50p	REAL	'./Treasury_Breakdown_50p',
--				Treasury_Breakdown_20p	REAL	'./Treasury_Breakdown_20p',
--				Treasury_Breakdown_10p	REAL	'./Treasury_Breakdown_10p',
--				Treasury_Breakdown_5p	REAL	'./Treasury_Breakdown_5p',
--				Treasury_Breakdown_2p	REAL	'./Treasury_Breakdown_2p',
--				Treasury_Float_Issued_By	INT	'./Treasury_Float_Issued_By',
--				Treasury_Float_Recovered_Total	REAL	'./Treasury_Float_Recovered_Total',
--				Treasury_Float_Recovered_2000p	REAL	'./Treasury_Float_Recovered_2000p',
--				Treasury_Float_Recovered_1000p	REAL	'./Treasury_Float_Recovered_1000p',
--				Treasury_Float_Recovered_500p	REAL	'./Treasury_Float_Recovered_500p',
--				Treasury_Float_Recovered_200p	REAL	'./Treasury_Float_Recovered_200p',
--				Treasury_Float_Recovered_100p	REAL	'./Treasury_Float_Recovered_100p',
--				Treasury_Float_Recovered_50p	REAL	'./Treasury_Float_Recovered_50p',
--				Treasury_Float_Recovered_20p	REAL	'./Treasury_Float_Recovered_20p',
--				Treasury_Float_Recovered_10p	REAL	'./Treasury_Float_Recovered_10p',
--				Treasury_Float_Recovered_5p	REAL	'./Treasury_Float_Recovered_5p',
--				Treasury_Float_Recovered_2p	REAL	'./Treasury_Float_Recovered_2p',
--				Treasury_Membership_No	VARCHAR(50)	'./Treasury_Membership_No',
--				Treasury_Actual_Date	DATETIME	'./Treasury_Actual_Date'
--			)	--AS A
--			INNER JOIN	@CollTable C	ON A.Installation_ID = C.Installation_ID

		INSERT INTO #Treasury
		SELECT * FROM OPENXML(@idoc, './CollectionDetails/TreasuryDetails/Treasury_Detail', 2) WITH
			(
				Installation_ID	INT	'./HQ_Installation_No',
				Treasury_Date	VARCHAR(30)	'./Treasury_Date',
				Treasury_Time	VARCHAR(50)	'./Treasury_Time',
				Treasury_Actual_Date	DATETIME	'./Treasury_Actual_Date'
--				Treasury_Type	VARCHAR(50)	'./Treasury_Type'
			)

			UPDATE TE
			   SET TE.Collection_ID = C.Collection_ID
			  FROM dbo.Treasury_Entry TE
		INNER JOIN #Treasury T ON T.Installation_ID = TE.Installation_ID AND T.Treasury_Date = TE.Treasury_Date AND T.Treasury_Time = TE.Treasury_Time AND ISNULL(TE.Treasury_Actual_Date, '') = ISNULL(T.Treasury_Actual_Date, '')
		INNER JOIN @CollTable C ON TE.Installation_ID = C.Installation_ID

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while updating Collection_ID in Treasury_Entry'---9		-- error while inserting into Trerasury_Entry
				GOTO	Err
			END

/*	no need to insert as all the treasuries would have reached enterprise long before

		INSERT INTO dbo.Treasury_Entry
			SELECT	C.Collection_ID, T.*
			  FROM  #Treasury T --dbo.#TempTreasury T
		INNER JOIN  @CollTable C ON C.Installation_ID = T.Installation_ID
		 LEFT JOIN  dbo.Treasury_Entry TE ON T.Installation_ID = TE.Installation_ID AND T.Treasury_Date = TE.Treasury_Date AND T.Treasury_Time = TE.Treasury_Time AND T.Treasury_Type = TE.Treasury_Type
			 WHERE  ISNULL(TE.Installation_ID, 0) = 0 AND ISNULL(TE.Treasury_Date, '') = '' AND ISNULL(TE.Treasury_Time, '') = '' AND ISNULL(TE.Treasury_Type, '') = ''

--			 WHERE  CONVERT(VARCHAR(20), T.Installation_ID) + ',' + T.Treasury_Date + ',' + T.Treasury_Time + ',' + T.Treasury_Type NOT IN
--						(SELECT CONVERT(VARCHAR(20), Installation_ID) + ',' + Treasury_Date + ',' + Treasury_Time + ',' + Treasury_Type FROM dbo.Treasury_Entry)

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while inserting into Treasury_Entry'---9		-- error while inserting into Trerasury_Entry
				GOTO	Err
			END

	change ends		*/

	END

	IF @iPart_Collection_No IS NOT NULL
	BEGIN
		
--		INSERT INTO dbo.Part_Collection
		SELECT C.Collection_ID, A.* INTO dbo.#TempPart_Collection FROM OPENXML(@idoc, './CollectionDetails/PartCollections/Part_Collection', 2)	WITH
			(
				Installation_ID	INT	'./HQ_Installation_No',
				User_No	VARCHAR(50)	'./User_Name',
				Part_Collection_Date	VARCHAR(30)	'./Part_Collection_Date',
				Part_Collection_Time	VARCHAR(50)	'./Part_Collection_Time',
				Part_Collection_Declared	BIT	'./Part_Collection_Declared',
				Part_Collection_CashCollected	REAL	'./Part_Collection_CashCollected',
				Part_Collection_Cash_Collected_2p	REAL	'./Part_Collection_Cash_Collected_2p',
				Part_Collection_Cash_Collected_5p	REAL	'./Part_Collection_Cash_Collected_5p',
				Part_Collection_Cash_Collected_10p	REAL	'./Part_Collection_Cash_Collected_10p',
				Part_Collection_Cash_Collected_20p	REAL	'./Part_Collection_Cash_Collected_20p',
				Part_Collection_Cash_Collected_50p	REAL	'./Part_Collection_Cash_Collected_50p',
				Part_Collection_Cash_Collected_100p	REAL	'./Part_Collection_Cash_Collected_100p',
				Part_Collection_Cash_Collected_200p	REAL	'./Part_Collection_Cash_Collected_200p',
				Part_Collection_Cash_Collected_500p	REAL	'./Part_Collection_Cash_Collected_500p',
				Part_Collection_Cash_Collected_1000p	REAL	'./Part_Collection_Cash_Collected_1000p',
				Part_Collection_Cash_Collected_2000p	REAL	'./Part_Collection_Cash_Collected_2000p',
				Part_Collection_Cash_Collected_5000p	REAL	'./Part_Collection_Cash_Collected_5000p',
				Part_Collection_Cash_Collected_10000p	REAL	'./Part_Collection_Cash_Collected_10000p',
				Part_Collection_Cash_Collected_20000p	REAL	'./Part_Collection_Cash_Collected_20000p',
				Part_Collection_Cash_Collected_50000p	REAL	'./Part_Collection_Cash_Collected_50000p',
				Part_Collection_Cash_Collected_100000p	REAL	'./Part_Collection_Cash_Collected_100000p',
				Part_Collection_CashRefills	REAL	'./Part_Collection_CashRefills',
				Part_Collection_Cash_Refills_2p	REAL	'./Part_Collection_Cash_Refills_2p',
				Part_Collection_Cash_Refills_5p	REAL	'./Part_Collection_Cash_Refills_5p',
				Part_Collection_Cash_Refills_10p	REAL	'./Part_Collection_Cash_Refills_10p',
				Part_Collection_Cash_Refills_20p	REAL	'./Part_Collection_Cash_Refills_20p',
				Part_Collection_Cash_Refills_50p	REAL	'./Part_Collection_Cash_Refills_50p',
				Part_Collection_Cash_Refills_100p	REAL	'./Part_Collection_Cash_Refills_100p',
				Part_Collection_Cash_Refills_200p	REAL	'./Part_Collection_Cash_Refills_200p',
				Part_Collection_Cash_Refills_500p	REAL	'./Part_Collection_Cash_Refills_500p',
				Part_Collection_Cash_Refills_1000p	REAL	'./Part_Collection_Cash_Refills_1000p',
				Part_Collection_Cash_Refills_2000p	REAL	'./Part_Collection_Cash_Refills_2000p',
				Part_Collection_Cash_Refills_5000p	REAL	'./Part_Collection_Cash_Refills_5000p',
				Part_Collection_Cash_Refills_10000p	REAL	'./Part_Collection_Cash_Refills_10000p',
				Part_Collection_Cash_Refills_20000p	REAL	'./Part_Collection_Cash_Refills_20000p',
				Part_Collection_Cash_Refills_50000p	REAL	'./Part_Collection_Cash_Refills_50000p',
				Part_Collection_Cash_Refills_100000p	REAL	'./Part_Collection_Cash_Refills_100000p',
				Part_Collection_TokensCollected	REAL	'./Part_Collection_TokensCollected',
				Part_Collection_TokenRefills	REAL	'./Part_Collection_TokenRefills',
				Part_Collection_CounterCashIn	INT	'./Part_Collection_CounterCashIn',
				Part_Collection_CounterCashOut	INT	'./Part_Collection_CounterCashOut',
				Part_Collection_CounterTokensIn	INT	'./Part_Collection_CounterTokensIn',
				Part_Collection_CounterTokensOut	INT	'./Part_Collection_CounterTokensOut',
				Part_Collection_CounterJackpots	INT	'./Part_Collection_CounterJackpots',
				Part_Collection_PreviousCounterCashIn	INT	'./Part_Collection_PreviousCounterCashIn',
				Part_Collection_PreviousCounterCashOut	INT	'./Part_Collection_PreviousCounterCashOut',
				Part_Collection_PreviousCounterTokensIn	INT	'./Part_Collection_PreviousCounterTokensIn',
				Part_Collection_PreviousCounterTokensOut	INT	'./Part_Collection_PreviousCounterTokensOut',
				Part_Collection_PreviousCollectionDate	VARCHAR(30)	'./Part_Collection_PreviousCollectionDate'
			)	AS A
			INNER JOIN	@CollTable C	ON A.Installation_ID = C.Installation_ID

			IF @@Error <> 0
			BEGIN
				SET @ISSuccess = 'error while inserting in to TempPart_Collection table'---10	--	error while inserting in to Part_Collection table
				GOTO	Err
			END

			INSERT INTO dbo.Part_Collection
			  (
			    Installation_ID,
			    Part_Collection_User,
			    Part_Collection_Date,
			    Part_Collection_Time,
			    Part_Collection_Declared,
			    Part_Collection_CashCollected,
			    Part_Collection_Cash_Collected_2p,
			    Part_Collection_Cash_Collected_5p,
			    Part_Collection_Cash_Collected_10p,
			    Part_Collection_Cash_Collected_20p,
			    Part_Collection_Cash_Collected_50p,
			    Part_Collection_Cash_Collected_100p,
			    Part_Collection_Cash_Collected_200p,
			    Part_Collection_Cash_Collected_500p,
			    Part_Collection_Cash_Collected_1000p,
			    Part_Collection_Cash_Collected_2000p,
			    Part_Collection_Cash_Collected_5000p,
			    Part_Collection_Cash_Collected_10000p,
			    Part_Collection_Cash_Collected_20000p,
			    Part_Collection_Cash_Collected_50000p,
			    Part_Collection_Cash_Collected_100000p,
			    Part_Collection_CashRefills,
			    Part_Collection_Cash_Refills_2p,
			    Part_Collection_Cash_Refills_5p,
			    Part_Collection_Cash_Refills_10p,
			    Part_Collection_Cash_Refills_20p,
			    Part_Collection_Cash_Refills_50p,
			    Part_Collection_Cash_Refills_100p,
			    Part_Collection_Cash_Refills_200p,
			    Part_Collection_Cash_Refills_500p,
			    Part_Collection_Cash_Refills_1000p,
			    Part_Collection_Cash_Refills_2000p,
			    Part_Collection_Cash_Refills_5000p,
			    Part_Collection_Cash_Refills_10000p,
			    Part_Collection_Cash_Refills_20000p,
			    Part_Collection_Cash_Refills_50000p,
			    Part_Collection_Cash_Refills_100000p,
			    Part_Collection_TokensCollected,
			    Part_Collection_TokenRefills,
			    Part_Collection_CounterCashIn,
			    Part_Collection_CounterCashOut,
			    Part_Collection_CounterTokensIn,
			    Part_Collection_CounterTokensOut,
			    Part_Collection_CounterJackpots,
			    Part_Collection_PreviousCounterCashIn,
			    Part_Collection_PreviousCounterCashOut,
			    Part_Collection_PreviousCounterTokensIn,
			    Part_Collection_PreviousCounterTokensOut,
			    Part_Collection_PreviousCollectionDate
			  )
			SELECT Installation_ID,
			       User_No,
			       Part_Collection_Date,
			       Part_Collection_Time,
			       Part_Collection_Declared,
			       Part_Collection_CashCollected,
			       Part_Collection_Cash_Collected_2p,
			       Part_Collection_Cash_Collected_5p,
			       Part_Collection_Cash_Collected_10p,
			       Part_Collection_Cash_Collected_20p,
			       Part_Collection_Cash_Collected_50p,
			       Part_Collection_Cash_Collected_100p,
			       Part_Collection_Cash_Collected_200p,
			       Part_Collection_Cash_Collected_500p,
			       Part_Collection_Cash_Collected_1000p,
			       Part_Collection_Cash_Collected_2000p,
			       Part_Collection_Cash_Collected_5000p,
			       Part_Collection_Cash_Collected_10000p,
			       Part_Collection_Cash_Collected_20000p,
			       Part_Collection_Cash_Collected_50000p,
			       Part_Collection_Cash_Collected_100000p,
			       Part_Collection_CashRefills,
			       Part_Collection_Cash_Refills_2p,
			       Part_Collection_Cash_Refills_5p,
			       Part_Collection_Cash_Refills_10p,
			       Part_Collection_Cash_Refills_20p,
			       Part_Collection_Cash_Refills_50p,
			       Part_Collection_Cash_Refills_100p,
			       Part_Collection_Cash_Refills_200p,
			       Part_Collection_Cash_Refills_500p,
			       Part_Collection_Cash_Refills_1000p,
			       Part_Collection_Cash_Refills_2000p,
			       Part_Collection_Cash_Refills_5000p,
			       Part_Collection_Cash_Refills_10000p,
			       Part_Collection_Cash_Refills_20000p,
			       Part_Collection_Cash_Refills_50000p,
			       Part_Collection_Cash_Refills_100000p,
			       Part_Collection_TokensCollected,
			       Part_Collection_TokenRefills,
			       Part_Collection_CounterCashIn,
			       Part_Collection_CounterCashOut,
			       Part_Collection_CounterTokensIn,
			       Part_Collection_CounterTokensOut,
			       Part_Collection_CounterJackpots,
			       Part_Collection_PreviousCounterCashIn,
			       Part_Collection_PreviousCounterCashOut,
			       Part_Collection_PreviousCounterTokensIn,
			       Part_Collection_PreviousCounterTokensOut,
			       Part_Collection_PreviousCollectionDate
			FROM   dbo.#TempPart_Collection
			WHERE  CAST(Collection_ID AS VARCHAR) + ',' + Part_Collection_Date + 
			       Part_Collection_Time
			       NOT IN (SELECT CAST(Collection_ID AS VARCHAR) + ',' + 
			                      Part_Collection_Date + Part_Collection_Time
			               FROM   dbo.Part_Collection)

			IF @@Error <> 0
			BEGIN
				SET @ISSuccess = 'error while inserting in to Part_Collection table'---10	--	error while inserting in to Part_Collection table
				GOTO	Err
			END

			UPDATE PC
				SET PC.Installation_ID = T.Installation_ID,
					PC.Part_Collection_User = T.User_No,
					PC.Part_Collection_Date = T.Part_Collection_Date,
					PC.Part_Collection_Time = T.Part_Collection_Time,
					PC.Part_Collection_Declared = T.Part_Collection_Declared,
					PC.Part_Collection_CashCollected = T.Part_Collection_CashCollected,
					PC.Part_Collection_Cash_Collected_2p = T.Part_Collection_Cash_Collected_2p,
					PC.Part_Collection_Cash_Collected_5p = T.Part_Collection_Cash_Collected_5p,
					PC.Part_Collection_Cash_Collected_10p = T.Part_Collection_Cash_Collected_10p,
					PC.Part_Collection_Cash_Collected_20p = T.Part_Collection_Cash_Collected_20p,
					PC.Part_Collection_Cash_Collected_50p = T.Part_Collection_Cash_Collected_50p,
					PC.Part_Collection_Cash_Collected_100p = T.Part_Collection_Cash_Collected_100p,
					PC.Part_Collection_Cash_Collected_200p = T.Part_Collection_Cash_Collected_200p,
					PC.Part_Collection_Cash_Collected_500p = T.Part_Collection_Cash_Collected_500p,
					PC.Part_Collection_Cash_Collected_1000p = T.Part_Collection_Cash_Collected_1000p,
					PC.Part_Collection_Cash_Collected_2000p = T.Part_Collection_Cash_Collected_2000p,
					PC.Part_Collection_Cash_Collected_5000p = T.Part_Collection_Cash_Collected_5000p,
					PC.Part_Collection_Cash_Collected_10000p = T.Part_Collection_Cash_Collected_10000p,
					PC.Part_Collection_Cash_Collected_20000p = T.Part_Collection_Cash_Collected_20000p,
					PC.Part_Collection_Cash_Collected_50000p = T.Part_Collection_Cash_Collected_50000p,
					PC.Part_Collection_Cash_Collected_100000p = T.Part_Collection_Cash_Collected_100000p,
					PC.Part_Collection_CashRefills = T.Part_Collection_CashRefills,
					PC.Part_Collection_Cash_Refills_2p = T.Part_Collection_Cash_Refills_2p,
					PC.Part_Collection_Cash_Refills_5p = T.Part_Collection_Cash_Refills_5p,
					PC.Part_Collection_Cash_Refills_10p = T.Part_Collection_Cash_Refills_10p,
					PC.Part_Collection_Cash_Refills_20p = T.Part_Collection_Cash_Refills_20p,
					PC.Part_Collection_Cash_Refills_50p = T.Part_Collection_Cash_Refills_50p,
					PC.Part_Collection_Cash_Refills_100p = T.Part_Collection_Cash_Refills_100p,
					PC.Part_Collection_Cash_Refills_200p = T.Part_Collection_Cash_Refills_200p,
					PC.Part_Collection_Cash_Refills_500p = T.Part_Collection_Cash_Refills_500p,
					PC.Part_Collection_Cash_Refills_1000p = T.Part_Collection_Cash_Refills_1000p,
					PC.Part_Collection_Cash_Refills_2000p = T.Part_Collection_Cash_Refills_2000p,
					PC.Part_Collection_Cash_Refills_5000p = T.Part_Collection_Cash_Refills_5000p,
					PC.Part_Collection_Cash_Refills_10000p = T.Part_Collection_Cash_Refills_10000p,
					PC.Part_Collection_Cash_Refills_20000p = T.Part_Collection_Cash_Refills_20000p,
					PC.Part_Collection_Cash_Refills_50000p = T.Part_Collection_Cash_Refills_50000p,
					PC.Part_Collection_Cash_Refills_100000p = T.Part_Collection_Cash_Refills_100000p,
					PC.Part_Collection_TokensCollected = T.Part_Collection_TokensCollected,
					PC.Part_Collection_TokenRefills = T.Part_Collection_TokenRefills,
					PC.Part_Collection_CounterCashIn = T.Part_Collection_CounterCashIn,
					PC.Part_Collection_CounterCashOut = T.Part_Collection_CounterCashOut,
					PC.Part_Collection_CounterTokensIn = T.Part_Collection_CounterTokensIn,
					PC.Part_Collection_CounterTokensOut = T.Part_Collection_CounterTokensOut,
					PC.Part_Collection_CounterJackpots = T.Part_Collection_CounterJackpots,
					PC.Part_Collection_PreviousCounterCashIn = T.Part_Collection_PreviousCounterCashIn,
					PC.Part_Collection_PreviousCounterCashOut = T.Part_Collection_PreviousCounterCashOut,
					PC.Part_Collection_PreviousCounterTokensIn = T.Part_Collection_PreviousCounterTokensIn,
					PC.Part_Collection_PreviousCounterTokensOut = T.Part_Collection_PreviousCounterTokensOut,
					PC.Part_Collection_PreviousCollectionDate = T.Part_Collection_PreviousCollectionDate
			FROM	dbo.Part_Collection PC 
      INNER JOIN dbo.#TempPart_Collection T ON PC.Collection_ID = T.Collection_ID AND PC.Part_Collection_Date = T.Part_Collection_Date AND PC.Part_Collection_Time = T.Part_Collection_Time

			IF @@Error <> 0
			BEGIN
				SET @ISSuccess = 'error while updating the Part_Collection table'---10	--	error while inserting in to Part_Collection table
				GOTO	Err
			END

	END

	IF @iDoor_Event_No IS NOT NULL
	BEGIN
--		INSERT INTO dbo.Door_Event(
--								Collection_ID,
--								Installation_ID,
--								Door_Event_Duration,
--								Door_Event_Date,
--								Door_Event_Time,
--								Door_Event_Type,
--								Door_Event_Key_Owner,
--								Door_Event_Polled,
--								Door_Cleared_By,
--								Door_Cleared_Date,
--								Door_Cleared_Time
--									)
		INSERT INTO #Door
		SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/DoorEvents/Door_Event')	WITH
					(
							Installation_ID	INT	'./HQ_Installation_No',
							Duration	FLOAT	'./Duration',
							Door_Date	VARCHAR(30)	'./Door_Date',
							Door_Time	VARCHAR(8)	'./Door_Time',
							Door_Event_Type	VARCHAR(50)	'./Door_Event_Type',
							Key_Owner	VARCHAR(50)	'./Key_Owner',
							Door_Polled	BIT	'./Door_Polled',
							Door_Cleared_By	VARCHAR(50)	'./Door_Cleared_By',
							Door_Cleared_Date	VARCHAR(30)	'./Door_Cleared_Date',
							Door_Cleared_Time	VARCHAR(50)	'./Door_Cleared_Time'
					)	AS A
			INNER JOIN	@CollTable C	ON A.Installation_ID = C.Installation_ID

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while inserting in to TempDoor table'---11	--	error while inserting in to Door_Event table
				GOTO	Err
			END

		INSERT INTO dbo.Door_Event(
								Collection_ID,
								Installation_ID,
								Door_Event_Duration,
								Door_Event_Date,
								Door_Event_Time,
								Door_Event_Type,
								Door_Event_Key_Owner,
								Door_Event_Polled,
								Door_Cleared_By,
								Door_Cleared_Date,
								Door_Cleared_Time
									)

		SELECT D.* FROM #Door D
		LEFT JOIN dbo.Door_Event DE ON D.Installation_ID = DE.Installation_ID AND D.Door_Date = DE.Door_Event_Date AND D.Door_Time = DE.Door_Event_Time AND D.Duration = DE.Door_Event_Duration AND D.Door_Event_Type = DE.Door_Event_Type
		    WHERE ISNULL(DE.Collection_ID, 0) = 0 AND ISNULL(DE.Door_Event_Date, '') = '' AND ISNULL(DE.Door_Event_Time, '') = '' AND ISNULL(DE.Door_Event_Duration, 0) = 0 AND ISNULL(DE.Door_Event_Type, '') = ''

--		SELECT * FROM dbo.#TempDoor 
--		WHERE CAST(Collection_ID AS VARCHAR) + ',' + Door_Date + ',' + Door_Time + ',' + CAST(Duration AS VARCHAR) + ',' + Door_Event_Type
--			NOT IN (SELECT CAST(Collection_ID AS VARCHAR) + ',' + Door_Event_Date + ',' + Door_Event_Time + ',' + CAST(Door_Event_Duration AS VARCHAR) + ',' + Door_Event_Type FROM dbo.Door_Event)

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = 'error while inserting in to Door_Event table'---11	--	error while inserting in to Door_Event table
			GOTO	Err
		END

		UPDATE D
			SET D.Installation_ID = T.Installation_ID,
				D.Door_Event_Duration = T.Duration,
				D.Door_Event_Date = T.Door_Date,
				D.Door_Event_Time = T.Door_Time,
				D.Door_Event_Type = T.Door_Event_Type,
				D.Door_Event_Key_Owner = T.Key_Owner,
				D.Door_Event_Polled = T.Door_Polled,
				D.Door_Cleared_By = T.Door_Cleared_By,
				D.Door_Cleared_Date = T.Door_Cleared_Date,
				D.Door_Cleared_Time = T.Door_Cleared_Time
		FROM dbo.Door_Event D
  INNER JOIN #Door T ON D.Installation_ID = T.Installation_ID AND D.Door_Event_Duration = T.Duration
				AND D.Door_Event_Date = T.Door_Date AND D.Door_Event_Time = T.Door_Time AND D.Door_Event_Type = T.Door_Event_Type

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = 'error while updating Door_Event table'---11	--	error while inserting in to Door_Event table
			GOTO	Err
		END

	END

	IF @iPower_Event_No IS NOT NULL
	BEGIN
--			INSERT INTO dbo.Power_Event
--							(
--								Collection_ID,
--								Installation_ID,
--								Power_Event_Duration,
--								Power_Event_Date,
--								Power_Event_Time,
--								Power_Event_VTP,
--								Power_Event_Polled,
--								Power_Cleared_By,
--								Power_Cleared_Date,
--								Power_Cleared_Time
--							)
			INSERT INTO #Power
			SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/PowerEvents/Power_Event')	WITH
					(
							Installation_ID	INT	'./HQ_Installation_No',
							Duration	FLOAT	'./Duration',
							Power_Date	VARCHAR(30)	'./Power_Date',
							Power_Time	VARCHAR(50)	'./Power_Time',
							VTP	INT	'./VTP',
							Power_Polled	BIT	'./Power_Polled',
							Power_Cleared_By	INT	'./Power_Cleared_By',
							Power_Cleared_Date	VARCHAR(30)	'./Power_Cleared_Date',
							Power_Cleared_Time	VARCHAR(50)	'./Power_Cleared_Time'
					)	AS A
			INNER JOIN	@CollTable C	ON A.Installation_ID = C.Installation_ID

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while inserting in to TempPower table'---12	--	error while inserting in to Power_Event table
				GOTO	Err
			END

			INSERT INTO dbo.Power_Event
							(
								Collection_ID,
								Installation_ID,
								Power_Event_Duration,
								Power_Event_Date,
								Power_Event_Time,
								Power_Event_VTP,
								Power_Event_Polled,
								Power_Cleared_By,
								Power_Cleared_Date,
								Power_Cleared_Time
							)

--			SELECT * FROM dbo.#TempPower WHERE CAST(Collection_ID AS VARCHAR) + ',' + CAST(Duration AS VARCHAR) + ',' + Power_Date + ',' + Power_Time
--						NOT IN (SELECT CAST(Collection_ID AS VARCHAR) + ',' + CAST(Power_Event_Duration AS VARCHAR) + ',' + Power_Event_Date + ',' + Power_Event_Time FROM dbo.Power_Event)

			SELECT P.* FROM #Power P
			LEFT JOIN dbo.Power_Event PE ON P.Installation_ID = PE.Installation_ID AND P.Duration = PE.Power_Event_Duration AND P.Power_Date = PE.Power_Event_Date AND P.Power_Time = PE.Power_Event_Time

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while inserting in to Power_Event table'---12	--	error while inserting in to Power_Event table
				GOTO	Err
			END

			UPDATE P
				SET P.Installation_ID = T.Installation_ID,
					P.Power_Event_Duration = T.Duration,
					P.Power_Event_Date = T.Power_Date,
					P.Power_Event_Time = T.Power_Time,
					P.Power_Event_VTP = T.VTP,
					P.Power_Event_Polled = T.Power_Polled,
					P.Power_Cleared_By = T.Power_Cleared_By,
					P.Power_Cleared_Date = T.Power_Cleared_Date,
					P.Power_Cleared_Time = T.Power_Cleared_Time
			FROM dbo.Power_Event P 
	  INNER JOIN #Power T ON P.Installation_ID = T.Installation_ID AND P.Power_Event_Date = T.Power_Date
						AND P.Power_Event_Time = T.Power_Time AND P.Power_Event_Duration = T.Duration

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while updating the Power_Event table'---12	--	error while inserting in to Power_Event table
				GOTO	Err
			END


	END

	IF @iFault_Event_No IS NOT NULL
	BEGIN
--			INSERT INTO dbo.Fault_Event
--							(
--								Collection_ID,
--								Installation_ID,
--								Fault_Event_Source,
--								Fault_Event_Code,
--								Fault_Event_Notes,
--								Fault_Event_Description,
--								Fault_Event_Date,
--								Fault_Event_Time,
--								Fault_Cleared_By,
--								Fault_Cleared_Date,
--								Fault_Cleared_Time
--
--							)
			INSERT INTO #Fault
			SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/FaultEvents/Fault_Event')	WITH
					(
							Installation_ID	INT	'./HQ_Installation_No',
							Fault_Source	INT	'./Fault_Source',
							Fault_ID	INT	'./Fault_ID',
							Fault_Notes	VARCHAR(100)	'./Fault_Notes',
							Fault_Description	VARCHAR(50)	'./Fault_Description',
							Fault_Date	VARCHAR(30)	'./Fault_Date',
							Fault_Time	VARCHAR(50)	'./Fault_Time',
							Fault_Cleared_By	INT	'./Fault_Cleared_By',
							Fault_Cleared_Date	VARCHAR(30)	'./Fault_Cleared_Date',
							Fault_Cleared_Time	VARCHAR(50)	'./Fault_Cleared_Time'
					)	AS A
			INNER JOIN	@CollTable C	ON A.Installation_ID = C.Installation_ID

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while inserting in to TempFault table'---13	--	error while inserting in to Fault_Event table
				GOTO	Err
			END

			INSERT INTO dbo.Fault_Event
							(
								Collection_ID,
								Installation_ID,
								Fault_Event_Source,
								Fault_Event_Code,
								Fault_Event_Notes,
								Fault_Event_Description,
								Fault_Event_Date,
								Fault_Event_Time,
								Fault_Cleared_By,
								Fault_Cleared_Date,
								Fault_Cleared_Time
							)

			SELECT F.* FROM #Fault F
			LEFT JOIN dbo.Fault_Event FE ON F.Installation_ID = FE.Installation_ID AND F.Fault_Date = FE.Fault_Event_Date AND F.Fault_Time = FE.Fault_Event_Time
			WHERE ISNULL(FE.Installation_ID, 0) = 0 AND ISNULL(FE.Fault_Event_Date, '') = '' AND ISNULL(FE.Fault_Event_Time, '') = ''

--			SELECT * FROM dbo.#TempFault WHERE CAST(Collection_ID AS VARCHAR) + ',' + Fault_Date + ',' + Fault_Time
--						NOT IN (SELECT CAST(Collection_ID AS VARCHAR) + ',' + Fault_Event_Date + ',' + Fault_Event_Time FROM dbo.Fault_Event)

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while inserting in to Fault_Table table'---13	--	error while inserting in to Fault_Event table
				GOTO	Err
			END

			UPDATE  F
				SET	F.Installation_ID = T.Installation_ID,
					F.Fault_Event_Source = T.Fault_Source,
					F.Fault_Event_Code = T.Fault_ID,
					F.Fault_Event_Notes = T.Fault_Notes,
					F.Fault_Event_Description = T.Fault_Description,
					F.Fault_Event_Date = T.Fault_Date,
					F.Fault_Event_Time = T.Fault_Time,
					F.Fault_Cleared_By = T.Fault_Cleared_By,
					F.Fault_Cleared_Date = T.Fault_Cleared_Date,
					F.Fault_Cleared_Time = T.Fault_Cleared_Time
			FROM dbo.Fault_Event F 
	  INNER JOIN #Fault T ON F.Installation_ID = T.Installation_ID AND F.Fault_Event_Date = T.Fault_Date AND F.Fault_Event_Time = T.Fault_Time

			IF @@Error <> 0
			BEGIN
				SET @IsSuccess = 'error while updating the Fault_Table table'---13	--	error while inserting in to Fault_Event table
				GOTO	Err
			END

	END

	EXEC sp_xml_RemoveDocument @idoc

RETURN 0

Err:
RETURN -99

END
GO
