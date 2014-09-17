USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportVLT600]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportVLT600]
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
-- Description: Export VLT specific data for AAMS
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		03/12/2009		Created
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[rsp_ExportVLT600]
	@Machine_ID	INT,
	@DurationType	VARCHAR(10)
AS

BEGIN

	DECLARE @GameXML	XML
	DECLARE @ResultXML	XML
--	DECLARE @Site_ID	INT
	DECLARE @Setting_Value	VARCHAR(20)
	DECLARE @Date DATETIME
	DECLARE @MaxRead_Date	DATETIME
	DECLARE @SecondMaxDate	DATETIME
	DECLARE @Read_Run	INT
--	DECLARE @MinSessionID	INT
	DECLARE @MaxSessionID	INT
	DECLARE @YearStartID	INT
	DECLARE @Day	INT
	DECLARE @Month	INT
	DECLARE @Year	INT
	DECLARE @TotBet	BIGINT

--	Get the max read date (the last gaming day)
--	SELECT @MaxRead_Date = MAX(CAST(R.Read_Date AS DATETIME)) + ' ' + @Setting_Value 

	SELECT @MaxRead_Date = MAX(CAST(R.Read_Date AS DATETIME))
							FROM dbo.[Read] R 
							INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID
							INNER JOIN dbo.Machine M ON M.Machine_ID = I.Machine_ID
							WHERE M.Machine_ID = @Machine_ID

--	get the next max read date, from which the data need to be captured

--	SELECT @SecondMaxDate = MAX(CAST(R.Read_Date AS DATETIME))
--							FROM dbo.[Read] R 
--							INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID
--							INNER JOIN dbo.Machine M ON M.Machine_ID = I.Machine_ID
--							WHERE M.Machine_ID = @Machine_ID AND CAST(R.Read_Date AS DATETIME) < @MaxRead_Date


-- if the max read date is not the last gaming day, read was not run in which case, need to send -1 for all fields
-- if max read date is NULL, no read records available, send -1 in all fields
	IF DATEADD(DAY, 1, @MaxRead_Date) <> CONVERT(DATETIME, CONVERT(VARCHAR, GETDATE(), 106), 101)
		SET @Read_Run = 0
	ELSE IF @MaxRead_Date IS NULL	
		SET @Read_Run = -1
	ELSE
		SET @Read_Run = 1

--	SELECT @MinSessionID = MAX(MGMD_Session_ID) FROM dbo.MGMD_SessionDelta WHERE Read_Date = @SecondMaxDate
	SELECT @MaxSessionID = MAX(MGMD_Session_ID) FROM dbo.MGMD_SessionDelta WHERE Read_Date = @MaxRead_Date

-- if @MinSessionID is NULL, then only once the read might have run.....

--	IF @MinSessionID IS NULL
--		SET @MinSessionID = 0


	IF DATEPART(MONTH, GETDATE()) = 1 AND DATEPART(DAY, GETDATE()) = 1
	BEGIN
		SET @Date = CAST(DATEPART(YEAR, GETDATE() - 1) AS VARCHAR) + '-01-01' --+ @Setting_Value
		SET @SecondMaxDate = CAST(DATEPART(YEAR, GETDATE() - 1) AS VARCHAR) + '-12-01'
		SELECT @Year = DATEPART(YEAR, GETDATE() - 1), @Month = 12, @Day = 31
	END
	ELSE
	BEGIN
		SET @Date = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-01-01' --+ @Setting_Value
		IF DATEPART(DAY, GETDATE()) <> 1
		BEGIN
			SET @SecondMaxDate = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-' + CAST(DATEPART(MONTH, GETDATE()) AS VARCHAR) + '-01'
			SELECT @Year = DATEPART(YEAR, GETDATE()), @Month = DATEPART(MONTH, GETDATE()), @Day = DATEPART(DAY, GETDATE() - 1)
		END
		ELSE
		BEGIN
			SET @SecondMaxDate = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-' + CAST(DATEPART(MONTH, GETDATE() - 1) AS VARCHAR) + '-01'
			SELECT @Year = DATEPART(YEAR, GETDATE()), @Month = DATEPART(MONTH, GETDATE() - 1), @Day = DATEPART(DAY, GETDATE() - 1)
		END
	END

	SELECT @YearStartID = MIN(MGMD_Session_ID) FROM dbo.MGMD_SessionDelta WHERE Read_Date >= @Date

-- if First record for the year is NULL, consider the ID 1 as the starting point	
	IF @YearStartID IS NULL
		SET @YearStartID = 1

	CREATE TABLE #Temp600
		(
			IdComponent	INT,
			Bet			BIGINT,
			Win			BIGINT,
--			TotPaid		FLOAT(25),
			TotIn		BIGINT,
			TotOut		BIGINT,
			BetNum		INT,
			TotBet		BIGINT,
			TotWin		BIGINT,
--			TotTotPaid	FLOAT(25),
			TotTotIn	BIGINT,
			TotTotOut	BIGINT,
			TotBetNum	INT,
			[Year]		INT,
			[Month]		INT,
			[Day]		INT
		)

	CREATE TABLE #TempGame600
		(
			Game_ID		INT,
			Game_Name	VARCHAR(500),
			Bet			BIGINT,
			Win			BIGINT,
			BetNum		INT,
			TotBet		BIGINT,
			TotWin		BIGINT,
			TotBetNum	INT
		)

-- First populate the cumulative data from 1st of the year till last gaming day for all games
	
		INSERT INTO #TempGame600
		SELECT  GL.MG_Game_ID,
				GL.MG_Game_Name,
				CASE WHEN @Read_Run = 1 THEN SUM(S.MGMD_COINS_IN * I.Installation_Price_Per_Play) ELSE -1 END AS Bet,
				CASE WHEN @Read_Run = 1 THEN SUM((S.MGMD_COINS_OUT - (S.MGMD_Mystery_Machine_Paid + S.MGMD_Mystery_Attendant_Paid)) * I.Installation_Price_Per_Play) ELSE -1 END AS Win,
				CASE WHEN @Read_Run = 1 THEN SUM(MGMD_GAMES_BET) ELSE -1 END AS BetNum, 0, 0, 0
		  FROM  dbo.MGMD_SessionDelta S
	INNER JOIN  dbo.MGMD_Installation MI ON S.MGMD_Combination_ID = MI.MGMD_ID
	INNER JOIN  dbo.Game_Library GL ON GL.MG_Game_ID = MI.MGMD_Game_ID
	INNER JOIN  dbo.Installation I ON S.MGMD_Installation_ID = I.Installation_ID
	INNER JOIN  dbo.Machine M ON I.Machine_ID = M.Machine_ID
		 WHERE  M.Machine_ID = @Machine_ID 
--					AND S.MGMD_Start_DateTime BETWEEN @MaxRead_Date AND DATEADD(DAY, 1, @MaxRead_Date) AND S.MGMD_End_DateTime BETWEEN @MaxRead_Date AND DATEADD(DAY, 1, @MaxRead_Date)
--					AND S.MGMD_Session_ID > @MinSessionID AND S.MGMD_Session_ID <= @MaxSessionID
			AND ((S.Read_Date = @MaxRead_Date AND @DurationType = 'PTD')
				OR (S.Read_Date BETWEEN @SecondMaxDate AND @MaxRead_Date AND @DurationType = 'MTD')
				OR (S.Read_Date BETWEEN @Date AND @MaxRead_Date AND @DurationType = 'YTD'))
	  GROUP BY  GL.MG_Game_ID, GL.MG_Game_Name

-- Now, update last gaming day data for the particular VLT, all games
		;WITH CTEGame AS
			(
				SELECT  GL.MG_Game_ID,
						SUM(S.MGMD_COINS_IN * I.Installation_Price_Per_Play) AS TotBet,
						SUM((S.MGMD_COINS_OUT - (S.MGMD_Mystery_Machine_Paid + S.MGMD_Mystery_Attendant_Paid)) * I.Installation_Price_Per_Play) AS TotWin,
						SUM(MGMD_GAMES_BET) AS TotBetNum
				  FROM  dbo.MGMD_SessionDelta S
			INNER JOIN  dbo.MGMD_Installation MI ON S.MGMD_Combination_ID = MI.MGMD_ID
			INNER JOIN  dbo.Game_Library GL ON GL.MG_Game_ID = MI.MGMD_Game_ID
			INNER JOIN  dbo.Installation I ON S.MGMD_Installation_ID = I.Installation_ID
			INNER JOIN  dbo.Machine M ON I.Machine_ID = M.Machine_ID
				 WHERE  M.Machine_ID = @Machine_ID 
		--			AND S.MGMD_Start_DateTime BETWEEN @Date AND DATEADD(DAY, 1, @MaxRead_Date) AND S.MGMD_End_DateTime BETWEEN @Date AND DATEADD(DAY, 1, @MaxRead_Date)
					AND S.MGMD_Session_ID BETWEEN @YearStartID AND @MaxSessionID
			  GROUP BY  GL.MG_Game_ID
			)
		UPDATE T
			SET T.TotBet = CASE WHEN @Read_Run = 1 THEN CTEGame.TotBet ELSE -1 END,
				T.TotWin = CASE WHEN @Read_Run = 1 THEN CTEGame.TotWin ELSE -1 END,
				T.TotBetNum = CASE WHEN @Read_Run = 1 THEN CTEGame.TotBetNum ELSE -1 END
			FROM #TempGame600 T INNER JOIN CTEGame ON T.Game_ID = CTEGame.MG_Game_ID

		SET @GameXML = (SELECT * FROM #TempGame600 AS Game600 FOR XML AUTO, ELEMENTS)

-- follow similar steps for VLTs also, as done for games

			INSERT INTO #Temp600
			SELECT  M.Machine_ID,
					CASE WHEN @Read_Run = 1 THEN SUM(R.READ_COINS_IN * I.Installation_Price_Per_Play) ELSE -1 END AS Bet,
					CASE WHEN @Read_Run = 1 THEN SUM((R.READ_COINS_OUT - (R.Mystery_Machine_Paid + R.Mystery_Attendant_Paid)) * I.Installation_Price_Per_Play) ELSE -1 END AS Win,
					CASE WHEN @Read_Run = 1 THEN (SUM(R.READ_RDC_BILL_1) * 100) + (SUM(R.READ_RDC_BILL_2) * 200) + (SUM(R.READ_RDC_BILL_5) * 500) + (SUM(R.READ_RDC_BILL_10) * 1000)
						+ (SUM(R.READ_RDC_BILL_20) * 2000) + (SUM(R.READ_RDC_BILL_50) * 5000) + (SUM(R.READ_RDC_BILL_100) * 10000)
						+ (SUM(R.READ_RDC_BILL_200) * 20000) + (SUM(R.READ_RDC_BILL_500) * 50000)
						+ SUM(R.READ_RDC_TRUE_COIN_IN * I.Installation_Price_Per_Play)
						+ SUM(R.READ_TICKET_IN_SUSPENSE) ELSE -1 END AS TotIn,
					CASE WHEN @Read_Run = 1 THEN SUM(R.READ_TICKET) + SUM(R.READ_HANDPAY) ELSE -1 END AS TotOut,
					CASE WHEN @Read_Run = 1 THEN SUM(R.Read_Games_Bet) ELSE -1 END AS BetNum, 0, 0, 0, 0, 0, 
					@Year AS [Year], --DATEPART(YEAR, GETDATE()) AS [Year],
					CASE WHEN @DurationType <> 'YTD' THEN @Month ELSE NULL END AS [Month],
					CASE WHEN @DurationType = 'PTD' THEN @Day ELSE NULL END AS [Day]
			FROM dbo.[Read] R
			INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID
			INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
			WHERE M.Machine_ID = @Machine_ID 
			  AND ((R.Read_Date = CONVERT(VARCHAR, @MaxRead_Date, 106) AND @DurationType = 'PTD')
					OR (CONVERT(DATETIME, R.Read_Date, 101) BETWEEN @SecondMaxDate AND @MaxRead_Date AND @DurationType = 'MTD')
					OR (CONVERT(DATETIME, R.Read_Date, 101) BETWEEN @Date AND @MaxRead_Date AND @DurationType = 'YTD'))
			GROUP BY M.Machine_ID

		;WITH CTEVoucherDay AS
			(
				SELECT  M.Machine_ID,
						SUM(R.READ_COINS_IN * I.Installation_Price_Per_Play) AS TotBet,
						SUM((R.READ_COINS_OUT - (R.Mystery_Machine_Paid + R.Mystery_Attendant_Paid)) * I.Installation_Price_Per_Play) AS TotWin,
						(SUM(R.READ_RDC_BILL_1) * 100) + (SUM(READ_RDC_BILL_2) * 200) + (SUM(READ_RDC_BILL_5) * 500) + (SUM(READ_RDC_BILL_10) * 1000)
							+ (SUM(READ_RDC_BILL_20) * 2000) + (SUM(READ_RDC_BILL_50) * 5000) + (SUM(READ_RDC_BILL_100) * 10000)
							+ (SUM(READ_RDC_BILL_200) * 20000) + (SUM(READ_RDC_BILL_500) * 50000)
							+ SUM(READ_RDC_TRUE_COIN_IN * I.Installation_Price_Per_Play)
							+ SUM(R.READ_TICKET_IN_SUSPENSE) AS TotTotIn,
						SUM(R.READ_TICKET) + SUM(R.READ_HANDPAY) AS TotTotOut,
						SUM(R.Read_Games_Bet) AS TotBetNum
				  FROM dbo.[Read] R
			INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID
			INNER JOIN dbo.Machine M ON I.Machine_ID = M.Machine_ID
				 WHERE CONVERT(DATETIME, R.Read_Date, 101) BETWEEN @Date AND @MaxRead_Date 
				   AND M.Machine_ID = @Machine_ID
				GROUP BY M.Machine_ID
			)
		UPDATE #Temp600
		SET TotBet = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotBet ELSE -1 END,
			TotWin = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotWin ELSE -1 END,
			TotTotIn = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotTotIn ELSE -1 END,
			TotTotOut = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotTotOut ELSE -1 END,
			TotBetNum = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotBetNum ELSE -1 END
		FROM CTEVoucherDay

		SET @ResultXML = (SELECT *, @GameXML FROM #Temp600 AS VLT FOR XML AUTO, ELEMENTS, ROOT('Message600'))

		SELECT @TotBet = TotBet FROM #Temp600

		IF @ResultXML IS NOT NULL
		BEGIN
			INSERT INTO dbo.BMC_BAS_Export_History(BBEH_Reference, BBEH_AAMS_Entity_Type, BBEH_Message_Type, BBEH_Status, BBEH_Received_Date, BBEH_Financial_Data, BBEH_Process_Type, BBEH_Process_Type_Comments, BBEH_TotalBet)
			SELECT @Machine_ID, 3, 600, 0, GETDATE(), CASE WHEN @Read_Run = 1 THEN @ResultXML ELSE 'Read not Run/No Read Record' END, 
				CASE @DurationType WHEN 'YTD' THEN 3 ELSE 0 END, CASE WHEN @Read_Run = 1 THEN '1' ELSE '0' END, @TotBet
		END

END

GO

