USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLocation600]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLocation600]
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
-- Description: Export Location specific data for AAMS
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
CREATE PROCEDURE [dbo].[rsp_ExportLocation600]
	@LocationType	VARCHAR(20),
	@Location_ID	INT,
	@DurationType	VARCHAR(10)
AS

BEGIN

	DECLARE @GameXML	XML
	DECLARE @ResultXML	XML
	DECLARE @Date DATETIME
	DECLARE @Setting_Value	VARCHAR(20)
	DECLARE @MaxRead_Date	DATETIME
	DECLARE @SecondMaxDate	DATETIME
	DECLARE @MaxRedeem_Date	DATETIME
	DECLARE @SecondRedeemDate	DATETIME
	DECLARE @Read_Run	BIT
--	DECLARE @MinSessionID	INT
	DECLARE @MaxSessionID	INT
	DECLARE @YearStartID	INT
	DECLARE @Day	INT
	DECLARE @Month	INT
	DECLARE @Year	INT

	SELECT @Setting_Value = ISNULL(SPI.SettingsProfileItems_SettingsMaster_Values, '00:00')
	FROM dbo.SettingsProfileItems SPI
	INNER JOIN dbo.SettingsMaster SM ON SPI.SettingsProfileItems_SettingsMaster_ID = SM.SettingsMaster_ID
	WHERE SPI.SettingsProfileItems_SettingsProfile_ID = 1 AND SM.SettingsMaster_Name = 'DailyAutoReadTime'

--	Get the max read date (the last gaming day)

--	SELECT @MaxRead_Date = MAX(CAST(R.Read_Date AS DATETIME)) + ' ' + @Setting_Value FROM dbo.[Read] R
	SELECT @MaxRead_Date = MAX(CAST(R.Read_Date AS DATETIME)), @MaxRedeem_Date = MAX(CAST(R.Read_Date + ' ' + R.Read_Time AS DATETIME))
		FROM dbo.[Read] R
		INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID
		INNER JOIN dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
		INNER JOIN dbo.Site S ON S.Site_ID = B.Site_ID
		WHERE ((@LocationType = 'Parlour' AND S.Site_ID = @Location_ID) OR (@LocationType = ''))

--	get the next max read date, from which the data need to be captured

	SELECT @SecondMaxDate = MAX(CAST(R.Read_Date AS DATETIME)), @SecondRedeemDate = MAX(CAST(R.Read_Date + ' ' + R.Read_Time AS DATETIME))
		FROM dbo.[Read] R
		INNER JOIN dbo.Installation I ON R.Installation_ID = I.Installation_ID
		INNER JOIN dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
		INNER JOIN dbo.Site S ON S.Site_ID = B.Site_ID
		WHERE ((@LocationType = 'Parlour' AND S.Site_ID = @Location_ID) OR (@LocationType = ''))
		  AND CAST(R.Read_Date AS DATETIME) < @MaxRead_Date

-- if the max read date is not the last gaming day, read was not run in which case, need to send -1 for all fields
-- if max read date is NULL, no read records available, send -1 in all fields

	IF DATEADD(DAY, 1, @MaxRead_Date) <> CONVERT(DATETIME, CONVERT(VARCHAR, GETDATE(), 106), 101)
		SET @Read_Run = 0
	ELSE IF @MaxRead_Date IS NULL	
		SET @Read_Run = -1
	ELSE
		SET @Read_Run = 1

--	SELECT @MinSessionID = MAX(MGMD_Session_ID) FROM dbo.MGMD_SessionDelta WHERE Read_Date = @SecondMaxDate
	SELECT @MaxSessionID = MAX(MGMD_Session_ID) 
	  FROM dbo.MGMD_SessionDelta MG
INNER JOIN dbo.Installation I ON MG.MGMD_Installation_ID = I.Installation_ID
INNER JOIN dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
INNER JOIN dbo.Site S ON S.Site_ID = B.Site_ID
	 WHERE MG.Read_Date = @MaxRead_Date AND ((@LocationType = 'Parlour' AND S.Site_ID = @Location_ID) OR (@LocationType = ''))

-- if @MinSessionID is NULL, then only once the read might have run.....

--	IF @MinSessionID IS NULL
--		SET @MinSessionID = 0

	IF DATEPART(MONTH, GETDATE()) = 1 AND DATEPART(DAY, GETDATE()) = 1
	BEGIN
		SET @Date = CAST(DATEPART(YEAR, GETDATE() - 1) AS VARCHAR) + '-01-01' --+ @Setting_Value
		SELECT @Year = DATEPART(YEAR, GETDATE() - 1), @Month = 12, @Day = 31

		IF @DurationType = 'MTD'
		BEGIN
			SET @SecondMaxDate = CAST(DATEPART(YEAR, GETDATE() - 1) AS VARCHAR) + '-12-01'
			--SET @SecondRedeemDate = @SecondRedeemDate - (DATEPART(DAY, @SecondRedeemDate) - 1)
			SET @SecondRedeemDate = CAST(DATEPART(YEAR, GETDATE() - 1) AS VARCHAR) + '-11-30' + CONVERT(VARCHAR, @SecondMaxDate, 108)
		END
	END
	ELSE
	BEGIN
		SET @Date = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-01-01' --+ @Setting_Value

		IF @DurationType = 'MTD'
		BEGIN
			IF DATEPART(DAY, GETDATE()) <> 1
				SET @SecondMaxDate = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-' + CAST(DATEPART(MONTH, GETDATE()) AS VARCHAR) + '-01'
			ELSE
				SET @SecondMaxDate = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-' + CAST(DATEPART(MONTH, GETDATE() - 1) AS VARCHAR) + '-01'		

			SET @SecondRedeemDate = DATEADD(DAY, -1, @SecondMaxDate) + @Setting_Value
		END
		ELSE
		BEGIN
			IF DATEDIFF(DAY, @SecondMaxDate, @MaxRead_Date) > 1 AND @SecondMaxDate IS NOT NULL
			BEGIN
				SET @SecondMaxDate = DATEADD(DAY, -1, @MaxRead_Date)
				SET @SecondRedeemDate = @SecondMaxDate + CONVERT(VARCHAR, @MaxRedeem_Date, 108)
			END
		END

		IF DATEPART(DAY, GETDATE()) <> 1
			SELECT @Year = DATEPART(YEAR, GETDATE()), @Month = DATEPART(MONTH, GETDATE()), @Day = DATEPART(DAY, GETDATE() - 1)
		ELSE
			SELECT @Year = DATEPART(YEAR, GETDATE()), @Month = DATEPART(MONTH, GETDATE() - 1), @Day = DATEPART(DAY, GETDATE() - 1)

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
			TotPaid		BIGINT,
			TotIn		BIGINT,
			TotOut		BIGINT,
			BetNum		INT,
			TotBet		BIGINT,
			TotWin		BIGINT,
			TotTotPaid	BIGINT,
			TotTotIn	BIGINT,
			TotTotOut	BIGINT,
			TotBetNum	INT,
			[Year]		INT,
			[Month]		INT,
			[Day]		INT
		)

	CREATE TABLE #TempGame600
		(
			IdComponent	INT,
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
		SELECT  @Location_ID AS IdComponent, 
				GL.MG_Game_ID,
				GL.MG_Game_Name, 
				CASE WHEN @Read_Run <> -1 THEN SUM(S.MGMD_COINS_IN * I.Installation_Price_Per_Play) ELSE -1 END AS Bet,
				CASE WHEN @Read_Run <> -1 THEN SUM((S.MGMD_COINS_OUT - (S.MGMD_progressive_win_value + S.MGMD_progressive_win_Handpay_value)) * I.Installation_Price_Per_Play) ELSE -1 END AS Win,
				CASE WHEN @Read_Run <> -1 THEN SUM(MGMD_GAMES_BET) ELSE -1 END AS BetNum, 0, 0, 0
		  FROM  dbo.MGMD_SessionDelta S
	INNER JOIN  dbo.MGMD_Installation MI ON S.MGMD_Combination_ID = MI.MGMD_ID
	INNER JOIN  dbo.Game_Library GL ON GL.MG_Game_ID = MI.MGMD_Game_ID
	INNER JOIN  dbo.Installation I ON S.MGMD_Installation_ID = I.Installation_ID
	INNER JOIN  dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
	INNER JOIN  dbo.Site St ON St.Site_ID = B.Site_ID
		 WHERE  ((@LocationType = 'Parlour' AND St.Site_ID = @Location_ID) OR (@LocationType = ''))
--						AND S.MGMD_Start_DateTime BETWEEN @MaxRead_Date AND DATEADD(DAY, 1, @MaxRead_Date) AND S.MGMD_End_DateTime BETWEEN @MaxRead_Date AND DATEADD(DAY, 1, @MaxRead_Date)
--						AND S.MGMD_Session_ID > @MinSessionID AND S.MGMD_Session_ID <= @MaxSessionID
				AND ((S.Read_Date = @MaxRead_Date AND @DurationType = 'PTD') 
						OR (S.Read_Date BETWEEN @SecondMaxDate AND @MaxRead_Date AND @DurationType = 'MTD')
						OR (S.MGMD_Session_ID BETWEEN @YearStartID AND @MaxSessionID AND @DurationType = 'YTD'))
	  GROUP BY  GL.MG_Game_ID, GL.MG_Game_Name
		

		;WITH CTEGame AS
			(
				SELECT  GL.MG_Game_ID,
						SUM(S.MGMD_COINS_IN * I.Installation_Price_Per_Play) AS TotBet,
						SUM((S.MGMD_COINS_OUT - (S.MGMD_progressive_win_Handpay_value + S.MGMD_progressive_win_value)) * I.Installation_Price_Per_Play) AS TotWin,
						SUM(MGMD_GAMES_BET) AS TotBetNum
				  FROM dbo.MGMD_SessionDelta S
			INNER JOIN  dbo.MGMD_Installation MI ON S.MGMD_Combination_ID = MI.MGMD_ID
			INNER JOIN  dbo.Game_Library GL ON GL.MG_Game_ID = MI.MGMD_Game_ID
			INNER JOIN  dbo.Installation I ON S.MGMD_Installation_ID = I.Installation_ID
			INNER JOIN  dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
			INNER JOIN  dbo.Site St ON St.Site_ID = B.Site_ID
				 WHERE  ((@LocationType = 'Parlour' AND St.Site_ID = @Location_ID) OR (@LocationType = ''))
			--				AND S.MGMD_Start_DateTime BETWEEN @Date AND DATEADD(DAY, 1, @MaxRead_Date) AND S.MGMD_End_DateTime BETWEEN @Date AND DATEADD(DAY, 1, @MaxRead_Date)
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
		SELECT	@Location_ID AS IdComponent,
				CASE WHEN @Read_Run <> -1 THEN SUM(R.READ_COINS_IN * I.Installation_Price_Per_Play) ELSE -1 END AS Bet,
				CASE WHEN @Read_Run <> -1 THEN SUM((R.READ_COINS_OUT - (R.progressive_win_Handpay_value + R.progressive_win_value)) * I.Installation_Price_Per_Play) ELSE -1 END AS Win,
				CASE WHEN @Read_Run <> -1 THEN 0 ELSE -1 END,
				CASE WHEN @Read_Run <> -1 THEN (SUM(R.READ_RDC_BILL_1) * 100) + (SUM(R.READ_RDC_BILL_2) * 200) + (SUM(R.READ_RDC_BILL_5) * 500) + (SUM(R.READ_RDC_BILL_10) * 1000)
					+ (SUM(R.READ_RDC_BILL_20) * 2000) + (SUM(R.READ_RDC_BILL_50) * 5000) + (SUM(R.READ_RDC_BILL_100) * 10000)
					+ SUM(R.READ_RDC_TRUE_COIN_IN * I.Installation_Price_Per_Play)
					+ SUM(R.READ_TICKET_IN_SUSPENSE) ELSE -1 END AS TotIn,
				CASE WHEN @Read_Run <> -1 THEN SUM(R.READ_TICKET) ELSE -1 END AS TotOut,
				CASE WHEN @Read_Run <> -1 THEN SUM(R.Read_Games_Bet) ELSE -1 END AS BetNum, 0,0,0,0,0,0,
				@Year AS [Year], --DATEPART(YEAR, GETDATE()) AS [Year],
				CASE WHEN @DurationType <> 'YTD' THEN @Month ELSE NULL END AS [Month],
				CASE WHEN @DurationType = 'PTD' THEN @Day ELSE NULL END AS [Day]
		FROM dbo.[Read] R
		INNER JOIN  dbo.Installation I ON R.Installation_ID = I.Installation_ID
		INNER JOIN  dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
		INNER JOIN  dbo.Site St ON St.Site_ID = B.Site_ID
			 WHERE  ((@LocationType = 'Parlour' AND St.Site_ID = @Location_ID) OR (@LocationType = ''))
					AND ((R.Read_Date = CONVERT(VARCHAR, @MaxRead_Date, 106) AND @DurationType = 'PTD')
							OR (CONVERT(DATETIME, R.Read_Date, 101) BETWEEN @SecondMaxDate AND @MaxRead_Date AND @DurationType = 'MTD')
							OR (CONVERT(DATETIME, R.Read_Date, 101) BETWEEN @Date AND @MaxRead_Date AND @DurationType = 'YTD'))

		;WITH CTEVoucherDay AS
			(	SELECT
				SUM(R.READ_COINS_IN * I.Installation_Price_Per_Play) AS TotBet,
				SUM((R.READ_COINS_OUT - (R.progressive_win_Handpay_value + R.progressive_win_value)) * I.Installation_Price_Per_Play) AS TotWin,
				(SUM(R.READ_RDC_BILL_1) * 100) + (SUM(READ_RDC_BILL_2) * 200) + (SUM(READ_RDC_BILL_5) * 500) + (SUM(READ_RDC_BILL_10) * 1000)
					+ (SUM(READ_RDC_BILL_20) * 2000) + (SUM(READ_RDC_BILL_50) * 5000) + (SUM(READ_RDC_BILL_100) * 10000)
					+ SUM(READ_RDC_TRUE_COIN_IN * I.Installation_Price_Per_Play) 
					+ SUM(R.READ_TICKET_IN_SUSPENSE) AS TotTotIn,
				SUM(R.READ_TICKET) AS TotTotOut,
				SUM(R.Read_Games_Bet) AS TotBetNum
		FROM dbo.[Read] R
		INNER JOIN  dbo.Installation I ON R.Installation_ID = I.Installation_ID
		INNER JOIN  dbo.Bar_Position B ON I.Bar_Position_ID = B.Bar_Position_ID
		INNER JOIN  dbo.Site St ON St.Site_ID = B.Site_ID
			 WHERE  ((@LocationType = 'Parlour' AND St.Site_ID = @Location_ID) OR (@LocationType = ''))
					AND CONVERT(DATETIME, R.Read_Date, 101) BETWEEN @Date AND @MaxRead_Date
			)
		UPDATE #Temp600
		SET TotBet = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotBet ELSE -1 END,
			TotWin = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotWin ELSE -1 END,
			TotTotIn = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotTotIn ELSE -1 END,
			TotTotOut = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotTotOut ELSE -1 END,
			TotBetNum = CASE WHEN @Read_Run = 1 THEN CTEVoucherDay.TotBetNum ELSE -1 END
		FROM CTEVoucherDay

-- Fetch data for TotPaid from Voucher table
		;WITH CTERedeem AS
			(
				SELECT  SUM(V.iAmount) AS RedeemAmt
				  FROM dbo.Voucher V
			INNER JOIN dbo.Device D ON V.iPayDeviceID = D.iDeviceID AND V.iSiteID = D.Site_Code
			INNER JOIN dbo.SiteWorkstations SW ON SW.Site_Workstation = D.strSerial
			 LEFT JOIN dbo.Site S ON S.Site_ID = SW.Site_ID
--				 WHERE V.dtPaid BETWEEN @MaxRead_Date - 1 AND @MaxRead_Date 
				 WHERE ((V.dtPaid BETWEEN DATEADD(DAY, 1, @SecondRedeemDate) AND DATEADD(DAY, 1, @MaxRedeem_Date) AND @SecondRedeemDate IS NOT NULL AND @DurationType <> 'YTD')
					OR (V.dtPaid BETWEEN @Date + @Setting_Value AND DATEADD(DAY, 1, @MaxRedeem_Date) AND @DurationType = 'YTD' AND @SecondRedeemDate IS NOT NULL)
					OR (V.dtPaid <= DATEADD(DAY, 1, @MaxRedeem_Date) AND @SecondRedeemDate IS NULL))
				   AND V.strVoucherStatus = 'PD'
				   AND ((@LocationType = 'Parlour' AND S.Site_ID = @Location_ID) OR (@LocationType = ''))
			)
		UPDATE #Temp600
		   SET TotPaid = CASE WHEN @Read_Run = 1 THEN CTERedeem.RedeemAmt ELSE -1 END
		 FROM CTERedeem

		;WITH CTERedeemYear AS
			(
				SELECT  SUM(V.iAmount) AS RedeemAmt
				  FROM dbo.Voucher V
			INNER JOIN dbo.Device D ON V.iPayDeviceID = D.iDeviceID AND V.iSiteID = D.Site_Code
			INNER JOIN dbo.SiteWorkstations SW ON SW.Site_Workstation = D.strSerial
			 LEFT JOIN dbo.Site S ON S.Site_ID = SW.Site_ID
--				 WHERE V.dtPaid BETWEEN @Date AND @MaxRead_Date 
				 WHERE V.dtPaid BETWEEN @Date + @Setting_Value AND DATEADD(DAY, 1, @MaxRedeem_Date) 
				   AND V.strVoucherStatus = 'PD'
				   AND ((@LocationType = 'Parlour' AND S.Site_ID = @Location_ID) OR (@LocationType = ''))
			)
		UPDATE #Temp600
		   SET TotTotPaid = CASE WHEN @Read_Run <> -1 THEN CTERedeemYear.RedeemAmt ELSE -1 END
		  FROM CTERedeemYear

		SET @ResultXML = CASE WHEN @LocationType = 'Parlour' THEN
								(SELECT *, @GameXML FROM #Temp600 AS ParlourSystem FOR XML AUTO, ELEMENTS, ROOT('Message600'))
							ELSE
								(SELECT *, @GameXML FROM #Temp600 AS GamingSystem FOR XML AUTO, ELEMENTS, ROOT('Message600'))
							END
		IF @ResultXML IS NOT NULL
		BEGIN
			INSERT INTO dbo.BMC_BAS_Export_History(BBEH_Reference, BBEH_AAMS_Entity_Type, BBEH_Message_Type, BBEH_Status, BBEH_Received_Date, BBEH_Financial_Data)
			SELECT CASE WHEN @LocationType = 'Parlour' THEN @Location_ID ELSE 1 END, CASE WHEN @LocationType = 'Parlour' THEN 2 ELSE 1 END, 600, 0, GETDATE(), @ResultXML
		END

END


GO

