

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_ManufacturerPerformanceReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_ManufacturerPerformanceReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[rsp_Report_ManufacturerPerformanceReport]
	@Company INT =0,
	@SubCompany INT =0,
	@Region INT =0,
	@Area INT =0,
	@District INT =0,
	@Site INT =0,
	@GamingDate DATETIME,
	@Period NVARCHAR(50),
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	SET DATEFORMAT dmy
	
	IF @Site = 0 
	BEGIN
	    SELECT @GamingDate = DATEADD(
	               minute,
	               30,
	               DATEADD(hour, 6, DATEADD(D, 0, DATEDIFF(D, 0, @GamingDate)))
	           )
	END
	ELSE
	BEGIN
	    DECLARE @SettingValue VARCHAR(50) 
	    
	    SELECT @SettingValue = SettingsProfileItems_SettingsMaster_Values
	    FROM   SettingsMaster SM
	           INNER JOIN SettingsProfileItems SPI
	                ON  SM.SettingsMaster_ID = SPI.SettingsProfileItems_SettingsMaster_ID
	           INNER JOIN SettingsProfile SP
	                ON  SPI.SettingsProfileItems_SettingsProfile_ID = SP.SettingsProfile_ID
	    WHERE  SP.SettingsProfile_ID = (
	               SELECT Site_Setting_Profile_ID
	               FROM   [Site]
	               WHERE  Site_ID = @Site
	           )--@Site
	           AND SettingsMaster_Name = 'DailyAutoReadTime'--@SettingName    
	    
	    --SELECT @SettingValue
	    
	    SELECT DATA 
	           INTO #ReadTime
	    FROM   dbo.fnSplit(@SettingValue, ':')  
	    
	    DECLARE @hour INT
	    SELECT TOP 1 @hour = DATA
	    FROM   #ReadTime
	    
	    DELETE 
	    FROM   #ReadTime
	    WHERE  DATA = (
	               SELECT TOP 1 DATA
	               FROM   #ReadTime
	           )
	    
	    DECLARE @Minute INT
	    
	    SELECT TOP 1 @Minute = DATA
	    FROM   #ReadTime
	    
	    SELECT @GamingDate = DATEADD(
	               minute,
	               @Minute,
	               DATEADD(hour, @hour, DATEADD(D, 0, DATEDIFF(D, 0, @GamingDate)))
	           )
	    
	    --SELECT @GamingDate
	    
	    DROP TABLE #ReadTime
	END
	
	
	IF @Company = 0
	    SET @Company = NULL
	
	IF @SubCompany = 0
	    SET @SubCompany = NULL
	
	IF @Region = 0
	    SET @Region = NULL
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL 
	
	IF @Site = 0
	    SET @Site = NULL       
	
	-- create quarter jan-mar, apr-jun, jul-sep,oct-dec              
	DECLARE @qstartmonth  INT,
	        @qtdstart     DATETIME,
	        @mtdstart     DATETIME,
	        @ytdstart     DATETIME,
	        @wtdstart     DATETIME              
	
	SET @qstartmonth = CASE 
	                        WHEN MONTH(@gamingdate) BETWEEN 1 AND 3 THEN 1
	                        WHEN MONTH(@gamingdate) BETWEEN 4 AND 6 THEN 4
	                        WHEN MONTH(@gamingdate) BETWEEN 7 AND 9 THEN 7
	                        ELSE 10
	                   END              
	
	SET @qtdstart = '01/' + CAST(@qstartmonth AS VARCHAR(4)) + '/' + CAST(YEAR(@gamingdate) AS VARCHAR(4)) 
	--  -- create YTD              
	SET @ytdstart = '01 jan ' + CAST(YEAR(@gamingdate) AS VARCHAR(4)) 
	-- create mtd              
	SET @mtdstart = '01/' + CAST(DATEPART(MONTH, @gamingdate) AS VARCHAR(3)) +
	    '/' + CAST(YEAR(@gamingdate) AS VARCHAR(4))              
	
	
	CREATE TABLE #Periods
	(
		[name]      VARCHAR(3),
		[ordering]  INT,
		[start]     DATETIME,
		[end]       DATETIME
	)              
	
	INSERT INTO #periods
	  (
	    [name]
	  )
	SELECT DATA
	FROM   dbo.fnSplit(@Period, ',') 
	
	-- create our list of periods, complete with start and end dates                
	UPDATE #Periods
	SET    ordering = 1,
	       [start] = @gamingdate,
	       [end] = DATEADD(DAY, 1, @gamingdate)
	WHERE  [name] = 'DAY'    
	
	UPDATE #Periods
	SET    ordering = 2,
	       [start] = CAST(@mtdstart AS DATETIME),
	       [end] = DATEADD(DAY, 1, @gamingdate)
	WHERE  [name] = 'MTD'                
	
	UPDATE #Periods
	SET    ordering = 3,
	       [start] = CAST(@qtdstart AS DATETIME),
	       [end] = DATEADD(DAY, 1, @gamingdate)
	WHERE  [name] = 'QTD'                
	
	UPDATE #Periods
	SET    ordering = 4,
	       [start] = CAST(@ytdstart AS DATETIME),
	       [end] = DATEADD(DAY, 1, @gamingdate)
	WHERE  [name] = 'YTD' 
	
	--Select * from #periods
	
	SELECT S.Site_ID,
	       S.Site_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       C.Company_ID,
	       C.Company_Name,
	       BP.Bar_Position_Name,
	       M.Machine_Stock_No,
	       Manufacturer_Name,
	       Machine_Type_Code AS [Machine_Type_Description],
	       CASE 
	            WHEN CAST(I.Installation_Start_Date AS DATETIME) > CAST(#periods.start AS DATETIME) THEN 
	                 CAST(I.Installation_Start_Date AS DATETIME)
	            ELSE #periods.start
	       END AS Installation_Start_Date,
	       CAST(
	           (
	               CASE 
	                    WHEN I.Installation_End_Date IS NULL THEN #periods.[end]
	                    WHEN I.Installation_End_Date > #periods.[end] THEN 
	                         #periods.[end]
	                    ELSE I.Installation_End_Date
	               END
	           )AS DATETIME
	       ) AS Installation_End_Date,
	       (
	           SUM(
	               CAST(MS.MGMD_JACKPOT * I.Installation_Price_Per_Play AS FLOAT)
	           ) / 100
	       ) AS Jackpots,
	       (
	           SUM(
	               CAST(MS.MGMD_COINS_IN * I.Installation_Price_Per_Play AS FLOAT)
	           ) / 100
	       ) AS CoinsIn,
	       (
	           SUM(
	               CAST(MS.MGMD_COINS_OUT * I.Installation_Price_Per_Play AS FLOAT)
	           ) / 100
	       ) AS CoinsOut,
	       SUM(MS.MGMD_Games_Bet) AS HandlePull,
	       CAST(MI.MGMD_Denom_Value / 100.0 AS FLOAT) AS MGMD_Denom_Value,
	       SUM(I.Installation_Percentage_Payout) AS 
	       Installation_Percentage_Payout,
	       100 -Installation_Percentage_Payout AS SlotHold,
	       [Order] = #Periods.ordering,
	       [Period] = #Periods.[Name] 
	       INTO #BASETABLE
	FROM   #periods,
	       Installation I
	       INNER JOIN Bar_Position BP
	            ON  I.Bar_Position_ID = BP.Bar_Position_ID
	       INNER JOIN SITE S
	            ON  S.Site_ID = BP.SITE_ID
	       INNER JOIN Sub_Company SC
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN COMPANY C
	            ON  C.Company_ID = SC.Company_ID
	       INNER JOIN MACHINE M
	            ON  I.Machine_ID = M.Machine_ID
	       INNER JOIN Machine_Class MC
	            ON  M.Machine_Class_ID = MC.Machine_Class_ID
	       INNER JOIN Machine_Type MT
	            ON  MT.Machine_Type_ID = MC.Machine_Type_ID
	       INNER JOIN manufacturer
	            ON  manufacturer.Manufacturer_ID = MC.Manufacturer_ID
	       INNER JOIN MGMD_Installation MI
	            ON  I.Installation_ID = MI.MGMD_Installation_ID
	       INNER JOIN MGMD_SessionDelta MS
	            ON  MI.MGMD_ID = MS.MGMD_Combination_ID --AND MS.MGMD_Installation_ID=I.Installation_ID
	WHERE  ISNULL(@Site, S.Site_ID) = S.Site_ID
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND S.Site_ID IN (SELECT DATA
	                                 FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND (
	               MS.MGMD_Start_DateTime >= #Periods.start
	               AND MS.MGMD_End_DateTime <= #Periods.[end]
	           )
	GROUP BY
	       C.Company_ID,
	       C.Company_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       S.Site_ID,
	       S.Site_Name,
	       I.Installation_Percentage_Payout,
	       I.Installation_Price_Per_Play,
	       I.Installation_Start_Date,
	       I.Installation_Start_Time,
	       I.Installation_End_Date,
	       I.Installation_End_Time,
	       Machine_Type_Code,
	       Manufacturer_Name,
	       M.Machine_Stock_No,
	       BP.Bar_Position_ID,
	       BP.Bar_Position_Name,
	       MI.MGMD_Denom_Value,
	       #Periods.ordering,
	       #Periods.[Name],
	       #Periods.start,
	       #Periods.[end]  
	
	
	SELECT [order],
	       [Period],
	       Company_ID,
	       Company_Name,
	       Sub_Company_ID,
	       Sub_Company_Name,
	       Site_ID,
	       Site_Name,
	       Bar_Position_Name,
	       Machine_Stock_No,
	       COUNT(Machine_Stock_No) AS MachineCount,
	       Manufacturer_Name,
	       Machine_Type_Description,
	       MGMD_Denom_Value,
	       SUM(
	           CASE 
	                WHEN DATEDIFF(DAY, Installation_Start_Date, Installation_End_Date) 
	                     <= 0 THEN 1
	                ELSE DATEDIFF(DAY, Installation_Start_Date, Installation_End_Date)
	           END
	       ) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       SUM(CoinsIn) AS CoinsIn,
	       SUM(CoinsOut) AS CoinsOut,
	       SUM(HandlePull) AS HandlePull,
	       SUM(SlotHold) AS SlotHold
	       INTO #RawValues
	FROM   #BASETABLE
	GROUP BY
	       [order],
	       [Period],
	       Company_ID,
	       Company_Name,
	       Sub_Company_ID,
	       Sub_Company_Name,
	       Site_ID,
	       Site_Name,
	       Bar_Position_Name,
	       Machine_Stock_No,
	       MGMD_Denom_Value,
	       Manufacturer_Name,
	       Machine_Type_Description
	
	SELECT 1 AS SortOrder,
	       [order],
	       [Period],
	       Company_ID,
	       Company_Name,
	       Sub_Company_ID,
	       Sub_Company_Name,
	       Site_ID,
	       Site_Name,
	       Manufacturer_Name,
	       Machine_Type_Description,
	       MGMD_Denom_Value,
	       Bar_Position_Name,
	       Machine_Stock_No,
	       DaysOnline,
	       Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = ((CoinsIn -CoinsOut -Jackpots) / MachineCount) /
	       DaysOnline,
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (CoinsIn / MachineCount) / DaysOnline,
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN HandlePull > 0 THEN CoinsIn / HandlePull
	                     ELSE 0
	                END,
	       SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN CoinsIn > 0 THEN (
	                               (
	                                   ((CoinsIn -CoinsOut -Jackpots) / MachineCount)
	                                   / DaysOnline
	                               ) / CoinsIn
	                           ) * 100
	                      ELSE 0
	                 END
	       --		CoinsIn,
	       --		CoinsOut,
	       --		HandlePull
	       INTO #ResultSet
	FROM   #RawValues 
	
	--
	
	
	--2 (Machine Type Description)
	
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 2 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       Company_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       Site_Name,
	       Manufacturer_Name,
	       Machine_Type_Description,
	       MGMD_Denom_Value,
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END 
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       Site_Name,
	       Manufacturer_Name,
	       MGMD_Denom_Value,
	       Machine_Type_Description,
	       [order],
	       [Period]
	
	
	--3 (Denom)
	
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 3 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       Company_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       Site_Name,
	       Manufacturer_Name,
	       '',
	       MGMD_Denom_Value,
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END 
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       Site_Name,
	       Manufacturer_Name,
	       MGMD_Denom_Value,
	       [order],
	       [Period]
	
	
	----4 (Manufacturer)
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 4 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       Company_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       Site_Name,
	       Manufacturer_Name,
	       '',
	       '',
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END 
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       Site_Name,
	       Manufacturer_Name,
	       [order],
	       [Period]
	
	
	--5 (Site)
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 5 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       Company_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       Site_Name,
	       '',
	       '',
	       '',
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END 
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       Site_Name,
	       [order],
	       [Period]
	
	
	
	--6 (Sub Company)
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 6 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       Company_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END 
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       [order],
	       [Period]
	
	--7 ( Company)
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 7 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       Company_Name,
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       [order],
	       [Period]
	
	--8 ( Grand Total)
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    [order],
	    [Period],
	    Company_ID,
	    Company_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Site_ID,
	    Site_Name,
	    Manufacturer_Name,
	    Machine_Type_Description,
	    MGMD_Denom_Value,
	    Bar_Position_Name,
	    Machine_Stock_No,
	    DaysOnline,
	    Jackpots,
	    WinPerUnitPerDay,
	    CIPerUnitPerDay,
	    AvgBet,
	    SlotHold,
	    ActHold
	    --						CoinsIn,
	    --						CoinsOut,
	    --						HandlePull
	  )
	SELECT 8 AS SortOrder,
	       [order],
	       [Period],
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       CAST(COUNT(Machine_Stock_No) AS VARCHAR(10)) AS Machine_Stock_No,
	       SUM(DaysOnline) AS DaysOnline,
	       SUM(Jackpots) AS Jackpots,
	       --W/U/D ? Actual NET win value/# OF UNITS)/# of days online
	       WinPerUnitPerDay = (
	           (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots)) / SUM(MachineCount)
	       ) / SUM(DaysOnline),
	       --CI/U/D - (Coin IN or Bets)/# of days online
	       CIPerUnitPerDay = (SUM(CoinsIn) / SUM(MachineCount)) / SUM(DaysOnline),
	       --Avg Bet - Bets / HP (Handle Pulls)
	       AvgBet = CASE 
	                     WHEN SUM(HandlePull) > 0 THEN SUM(CoinsIn) / SUM(HandlePull)
	                     ELSE 0
	                END,
	       AVG(SlotHold) AS SlotHold,
	       --Act Hold Day - ("W/U/D" / Bets) * 100	
	       ActHold = CASE 
	                      WHEN SUM(CoinsIn) > 0 THEN (
	                               (
	                                   (
	                                       (SUM(CoinsIn) -SUM(CoinsOut) -SUM(Jackpots))
	                                       / SUM(MachineCount)
	                                   ) / SUM(DaysOnline)
	                               ) / SUM(CoinsIn)
	                           ) * 100
	                      ELSE 0
	                 END 
	       --					CoinsIn,
	       --					CoinsOut,
	       --					HandlePull
	FROM   #RawValues
	GROUP BY
	       Company_Name,
	       [order],
	       [Period]
	
	DROP TABLE #BASETABLE
	DROP TABLE #Periods 
	DROP TABLE #RawValues
	
	SELECT *
	FROM   #ResultSet
	ORDER BY
	       SORTORDER,
	       [order],
	       [period]
END
GO

