USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGamingSystemAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGamingSystemAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetGamingSystemAAMSDetails  
-- -----------------------------------------------------------------  
--  
-- Returns the Gaming System details.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 18/11/09 Renjish Created        
-- =================================================================  

CREATE PROCEDURE dbo.rsp_GetGamingSystemAAMSDetails

AS

DECLARE @Date DATETIME  
DECLARE @Setting_Value VARCHAR(20)  
DECLARE @MaxRead_Date DATETIME  
DECLARE @TotBet FLOAT(25)
DECLARE @HourlyTotBet FLOAT(25)
DECLARE @HourlyTotBetMaxReadDate FLOAT(25)
DECLARE @SenderCode VARCHAR(20)

SELECT @SenderCode = Setting_Value FROM Setting WHERE Setting_Name = 'SenderCode'

SELECT @Setting_Value = ISNULL(SPI.SettingsProfileItems_SettingsMaster_Values, '00:00')  
FROM dbo.SettingsProfileItems SPI  
INNER JOIN dbo.SettingsMaster SM ON SPI.SettingsProfileItems_SettingsMaster_ID = SM.SettingsMaster_ID  
WHERE SPI.SettingsProfileItems_SettingsProfile_ID = 1 AND SM.SettingsMaster_Name = 'DailyAutoReadTime'  

SELECT @MaxRead_Date = MAX(CAST(R.Read_Date AS DATETIME)) + ' ' + @Setting_Value FROM dbo.[Read] R  

SET @Date = CAST(DATEPART(YEAR, GETDATE()) AS VARCHAR) + '-01-01 ' + @Setting_Value  

-- Tot Bet for which read data is available.
SELECT @TotBet = SUM(CAST(R.READ_COINS_IN * I.Installation_Price_Per_Play AS FLOAT(25)) / 100)    
FROM dbo.[Read] R  
INNER JOIN  dbo.Installation I ON R.Installation_ID = I.Installation_ID  
WHERE  R.Read_Date BETWEEN @Date AND @MaxRead_Date  

-- Tot Bet for which read data is not available taken from hourly data.
--(Excludes Data for Max Read Date, after the Read has run.)
SELECT @HourlyTotBet = SUM(CAST(HS.HS_Hour1 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour2 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour3 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour4 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour5 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour6 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour7 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour8 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour9 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour10 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour11 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour12 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour13 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour14 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour15 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour16 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour17 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour18 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour19 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour20 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour21 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour22 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour23 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100) + 
SUM(CAST(HS.HS_Hour24 * I.Installation_Price_Per_Play AS FLOAT(25)) / 100)
FROM Hourly_Statistics HS
INNER JOIN  dbo.Installation I ON HS.HS_Installation_No = I.Installation_ID  
WHERE HS.HS_Type = 'CREDITS_WAGERED' AND HS.HS_Date > @MaxRead_Date

--Tot bet data for Max Read Date alone, after the Read has run.

	SELECT I.Installation_ID, MAX(I.Installation_Price_Per_Play) AS POP, CASE SUBSTRING(@Setting_Value, 1, 2) 
	WHEN '1' THEN SUM(HS.HS_Hour2 + HS.HS_Hour3 + HS.HS_Hour4 + HS.HS_Hour5 + HS.HS_Hour6 + HS.HS_Hour7 + 
	HS.HS_Hour8 + HS.HS_Hour9 + HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + 
	HS.HS_Hour15 + HS.HS_Hour16 + 	HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + 
	HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '2' THEN SUM(HS.HS_Hour3 + HS.HS_Hour4 + HS.HS_Hour5 + HS.HS_Hour6 + HS.HS_Hour7 + HS.HS_Hour8 + 
	HS.HS_Hour9 + HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + 
	HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + 
	HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '3' THEN SUM(HS.HS_Hour4 + HS.HS_Hour5 + HS.HS_Hour6 + HS.HS_Hour7 + HS.HS_Hour8 + HS.HS_Hour9 + 
	HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + 
	HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + 
	HS.HS_Hour24)
	WHEN '4' THEN SUM(HS.HS_Hour5 + HS.HS_Hour6 + HS.HS_Hour7 + HS.HS_Hour8 + HS.HS_Hour9 + HS.HS_Hour10 + 
	HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + 
	HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24) 
	WHEN '5' THEN SUM(HS.HS_Hour6 + HS.HS_Hour7 + HS.HS_Hour8 + HS.HS_Hour9 + HS.HS_Hour10 + HS.HS_Hour11 + 
	HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + 
	HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24) 
	WHEN '6' THEN SUM(HS.HS_Hour7 + HS.HS_Hour8 + HS.HS_Hour9 + HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + 
	HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + 
	HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24) 
	WHEN '7' THEN SUM(HS.HS_Hour8 + HS.HS_Hour9 + HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + 
	HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + 
	HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '8' THEN SUM(HS.HS_Hour9 + HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + 
	HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + 
	HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '9' THEN SUM(HS.HS_Hour10 + HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + 
	HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + 
	HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '10' THEN SUM(HS.HS_Hour11 + HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 +
	HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + 
	HS.HS_Hour24)
	WHEN '11' THEN SUM(HS.HS_Hour12 + HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + 
	HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '12' THEN SUM(HS.HS_Hour13 + HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + 
	HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24) 
	WHEN '13' THEN SUM(HS.HS_Hour14 + HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + 
	HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24) 
	WHEN '14' THEN SUM(HS.HS_Hour15 + HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + 
	HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '15' THEN SUM(HS.HS_Hour16 + HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + 
	HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '16' THEN SUM(HS.HS_Hour17 + HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + 
	HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '17' THEN SUM(HS.HS_Hour18 + HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + 
	HS.HS_Hour24)
	WHEN '18' THEN SUM(HS.HS_Hour19 + HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '19' THEN SUM(HS.HS_Hour20 + HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '20' THEN SUM(HS.HS_Hour21 + HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '21' THEN SUM(HS.HS_Hour22 + HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '22' THEN SUM(HS.HS_Hour23 + HS.HS_Hour24)
	WHEN '23' THEN SUM(HS.HS_Hour24)
	ELSE 0
	END AS TotBet
INTO #TempHS
FROM Hourly_Statistics HS
INNER JOIN  dbo.Installation I ON HS.HS_Installation_No = I.Installation_ID  
WHERE HS.HS_Type = 'CREDITS_WAGERED' AND HS.HS_Date = @MaxRead_Date
GROUP BY I.Installation_ID

SELECT @HourlyTotBetMaxReadDate = CAST(SUM(TotBet * POP) AS FLOAT(25)) / 100 FROM #TempHS

SELECT ISNULL(BAD.BAD_AAMS_Code, '') AS BAD_AAMS_Code, ISNULL(BAD.BAD_AAMS_Status, '') AS BAD_AAMS_Status,
ISNULL(@TotBet,0) + ISNULL(@HourlyTotBet,0) + ISNULL(@HourlyTotBetMaxReadDate,0) AS TotBet, ISNULL(@SenderCode, '') AS SenderCode
FROM dbo.BMC_AAMS_Details BAD WHERE BAD.BAD_AAMS_Entity_Type = 1


GO

