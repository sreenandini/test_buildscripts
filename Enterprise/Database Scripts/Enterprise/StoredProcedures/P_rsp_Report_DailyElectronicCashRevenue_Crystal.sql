USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Report_DailyElectronicCashRevenue_Crystal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Report_DailyElectronicCashRevenue_Crystal]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_Report_DailyElectronicCashRevenue_Crystal]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Zone INT = 0, -- grouping fields, set as -1
	@GamingDate DATETIME ,
	@Period NVARCHAR(50) = 'DAY,LTD,MTD',
	@SiteIDList VARCHAR(MAX)
	 ---- comma list of allowed periods DAY, LTD, MTD, PTD, QTD, YTD
AS
BEGIN
	SET NOCOUNT ON              
	SET DATEFORMAT DMY 
	
	
	-- create quarter jan-mar, apr-jun, jul-sep,oct-dec              
	DECLARE @qstartmonth            INT,
	        @qtdstart               DATETIME,
	        @mtdstart               DATETIME,
	        @ytdstart               DATETIME
	-- Create Day Record
	DECLARE @GamingDayStartTimeVar  VARCHAR(10)
	DECLARE @GamingDayStartTime     INT
	
	
	EXEC rsp_GetSetting 0,
	     'GAMING_DAY_START_HOUR',
	     '0',
	     @GamingDayStartTimeVar OUTPUT
	
	IF @GamingDayStartTimeVar = '0'
	    SET @GamingDayStartTime = 0
	ELSE
	    SET @GamingDayStartTime = CAST(@GamingDayStartTimeVar AS INT)
	
	
	
	DECLARE @DayStartDate DATETIME
	SET @DayStartDate = DATEADD(
	        DAY,
	        -1,
	        CAST(CONVERT(VARCHAR(MAX), @GamingDate, 103) AS DATETIME)
	    )
	
	SET @DayStartDate = DATEADD(
	        HOUR,
	        @GamingDayStartTime,
	        CAST(FLOOR(CAST(@DayStartDate AS FLOAT)) AS DATETIME)
	)
	
	-- Create Period Start Date	
	DECLARE @prdStart          DATETIME
	DECLARE @CalendarIDForPrd  INT
	IF (@SubCompany > 0)
	BEGIN
	    IF EXISTS (
	           SELECT 1
	           FROM   Sub_Company_Calendar
	           WHERE  Sub_Company_Id = @SubCompany
	       )
	    BEGIN
	        SELECT TOP 1 @prdStart = CP.Calendar_Period_Start_Date
	        FROM   Calendar C
	               INNER JOIN Calendar_Period CP
	                    ON  C.Calendar_ID = CP.Calendar_ID
	               INNER JOIN Sub_Company_Calendar SCC
	                    ON  C.Calendar_ID = SCC.Calendar_ID
	        WHERE  SCC.Sub_Company_ID = @SubCompany
	               AND @GamingDate BETWEEN CONVERT(DATETIME, Calendar_Period_Start_Date, 103) 
	                   AND CONVERT(DATETIME, Calendar_Period_End_Date, 103)
	    END
	END
	ELSE 
	IF (@SubCompany = 0)
	BEGIN
	    IF EXISTS (
	           SELECT TOP 1 *
	           FROM   Sub_Company_Calendar
	       )
	        SELECT TOP 1 @prdStart = CP.Calendar_Period_Start_Date
	        FROM   Calendar C
	               INNER JOIN Calendar_Period CP
	                    ON  C.Calendar_ID = CP.Calendar_ID
	               INNER JOIN Sub_Company_Calendar SCC
	                    ON  C.Calendar_ID = SCC.Calendar_ID
	               INNER JOIN Sub_Company SC
	                    ON  SC.Sub_Company_ID = SCC.Sub_Company_ID
	               INNER JOIN Company Com
	                    ON  Com.Company_ID = SC.Company_ID
	        WHERE  @GamingDate BETWEEN CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) 
	               AND CONVERT(DATETIME, CP.Calendar_Period_End_Date, 103)
	    ELSE
	        SET @prdStart = @GamingDate
	END
	ELSE
	BEGIN
	    SET @prdStart = @GamingDate
	END
	
	-- Create Calendar Week Start Date	
	DECLARE @wtdstart DATETIME
	
	IF (@subcompany > 0)
	BEGIN
	    IF EXISTS (
	           SELECT 1
	           FROM   Sub_Company_Calendar
	           WHERE  Sub_Company_Id = @subcompany
	       )
	    BEGIN
	        SELECT TOP 1 @wtdstart = CW.Calendar_Week_Start_Date
	        FROM   Calendar C
	               INNER JOIN Calendar_Week CW
	                    ON  C.Calendar_ID = CW.Calendar_ID
	               INNER JOIN Sub_Company_Calendar SCC
	                    ON  C.Calendar_ID = SCC.Calendar_ID
	        WHERE  SCC.Sub_Company_ID = @subcompany
	               AND @GamingDate BETWEEN CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) 
	                   AND CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
	    END
	END
	ELSE 
	IF (@subcompany = 0)
	BEGIN
	    IF EXISTS (
	           SELECT 1
	           FROM   Sub_Company_Calendar
	           WHERE  Sub_Company_Id = @subcompany
	       )
	        SELECT TOP 1 @wtdstart = CW.Calendar_Week_Start_Date
	        FROM   Calendar C
	               INNER JOIN Calendar_Week CW
	                    ON  C.Calendar_ID = CW.Calendar_ID
	               INNER JOIN Sub_Company_Calendar SCC
	                    ON  C.Calendar_ID = SCC.Calendar_ID
	        WHERE  @GamingDate BETWEEN CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) 
	               AND CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)
	    ELSE
	        SET @wtdstart = @GamingDate
	END
	ELSE
	BEGIN
	    SET @wtdstart = @GamingDate
	END
	
	
	
	
	SET @qstartmonth = CASE 
	                        WHEN MONTH(@GamingDate) BETWEEN 1 AND 3 THEN 1
	                        WHEN MONTH(@GamingDate) BETWEEN 4 AND 6 THEN 4
	                        WHEN MONTH(@GamingDate) BETWEEN 7 AND 9 THEN 7
	                        ELSE 10
	                   END              
	
	SET @qtdstart = '01/' + CAST(@qstartmonth AS VARCHAR(4)) + '/' + CAST(YEAR(@GamingDate) AS VARCHAR(4)) 
	-- create YTD              
	SET @ytdstart = '01 jan ' + CAST(YEAR(@GamingDate) AS VARCHAR(4)) 
	-- create mtd              
	SET @mtdstart = '01/' + CAST(DATEPART(MONTH, @GamingDate) AS VARCHAR(3)) +
	    '/' + CAST(YEAR(@GamingDate) AS VARCHAR(4)) 
	-- create ptd ??              
	
	-- create wtd,                
	SET DATEFIRST 1 -- use monday as week start              
	
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
	
	IF @Zone = 0
	    SET @Zone = NULL
	
	
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
	       [start] = @DayStartDate,
	       [end] = @gamingdate
	WHERE  [name] = 'DAY'    
	
	UPDATE #Periods
	SET    ordering = 2,
	       [start] = @prdStart,
	       [end] = @gamingdate -- what does period mean
	WHERE  [name] = 'PTD'                
	
	UPDATE #Periods
	SET    ordering = 3,
	       [start] = CAST(@wtdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'WTD'                
	
	UPDATE #Periods
	SET    ordering = 4,
	       [start] = CAST(@mtdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'MTD'                
	
	UPDATE #Periods
	SET    ordering = 5,
	       [start] = CAST(@qtdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'QTD'                
	
	UPDATE #Periods
	SET    ordering = 6,
	       [start] = CAST(@ytdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'YTD'                
	
	UPDATE #Periods
	SET    ordering = 7,
	       [start] = CAST('01 jan 2000' AS DATETIME),
	       [end] = @gamingdate -- arbitary date for start.
	WHERE  [name] = 'LTD' 
	
	
	-- get the raw information, and append site, company etc information to it.
	--              
	SELECT Bar_Position.Bar_Position_ID,
	       Bar_Position.Bar_Position_Name,	-- stand              
	       Machine_Type_Category.Machine_Type_ID,
	       Machine_Type_Category.Machine_Type_Code,
	       S.Site_ID,
	       S.Site_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       C.Company_ID,
	       C.Company_Name,
	       MACHINE.Machine_Stock_No,	-- slot              
	       Zone_Name = CASE 
	                        WHEN ISNULL(Z.Zone_Name, '') = '' THEN 'NOT SET'
	                        ELSE Z.Zone_Name
	                   END,
	       ISNULL(Z.ZONE_ID, 0) AS ZONE_ID,
	       [Order] = #Periods.ordering,
	       [Period] = #Periods.Name,
	       [WAT_In] = SUM(CAST(ISNULL(Cashable_EFT_IN, 0) AS DECIMAL(10, 2)) / 100),
	       [WAT_Out] = SUM(CAST(ISNULL(Cashable_EFT_OUT, 0) AS DECIMAL(10, 2)) / 100),
	       [Cashable_ePromo_In] = SUM(
	           CAST(ISNULL(Promo_Cashable_EFT_IN, 0) AS DECIMAL(10, 2)) / 100
	       ),
	       [Cashable_ePromo_Out] = SUM(
	           CAST(ISNULL(Promo_Cashable_EFT_OUT, 0) AS DECIMAL(10, 2)) / 100
	       ),
	       [NCashable_ePromo_In] = SUM(
	           CAST(ISNULL(NonCashable_EFT_IN, 0) AS DECIMAL(10, 2)) / 100
	       ),
	       [NCashable_ePromo_Out] = SUM(
	           CAST(ISNULL(NonCashable_EFT_OUT, 0) AS DECIMAL(10, 2)) / 100
	       ),
	       [eFund_Drop] = SUM(CAST(ISNULL(Cashable_EFT_IN, 0) AS DECIMAL(10, 2)) / 100) 
	       + SUM(
	           CAST(ISNULL(Promo_Cashable_EFT_IN, 0) AS DECIMAL(10, 2)) / 100
	       ) 
	       + (
	           SUM(CAST(ISNULL(NonCashable_EFT_IN, 0) AS DECIMAL(10, 2))) / 100
	       ),	
	       
	       [eFund_Expense] = SUM(CAST(ISNULL(Cashable_EFT_OUT, 0) AS DECIMAL(10, 2)) / 100) 
	       + SUM(
	           CAST(ISNULL(Promo_Cashable_EFT_OUT, 0) AS DECIMAL(10, 2)) / 100
	       ) 
	       + (
	           SUM(CAST(ISNULL(NonCashable_EFT_OUT, 0) AS DECIMAL(10, 2))) / 100
	       ),	
	       
	       [eFund_Net] = (
	           SUM(CAST(ISNULL(Cashable_EFT_IN, 0) AS DECIMAL(10, 2)) / 100) 
	           + SUM(
	               CAST(ISNULL(Promo_Cashable_EFT_IN, 0) AS DECIMAL(10, 2)) /
	               100
	           ) 
	           + (
	               SUM(CAST(ISNULL(NonCashable_EFT_IN, 0) AS DECIMAL(10, 2))) /
	               100
	           )
	       ) 
	       -(
	           SUM(CAST(ISNULL(Cashable_EFT_OUT, 0) AS DECIMAL(10, 2)) / 100) 
	           + SUM(
	               CAST(ISNULL(Promo_Cashable_EFT_OUT, 0) AS DECIMAL(10, 2)) /
	               100
	           ) 
	           + (
	               SUM(CAST(ISNULL(NonCashable_EFT_OUT, 0) AS DECIMAL(10, 2))) /
	               100
	           )
	       ) -- drop - expense                   
	       INTO #preGrouping
	FROM   #Periods,
	       [read] r WITH (NOLOCK)
	       INNER JOIN dbo.Installation i WITH (NOLOCK)
	            ON  i.Installation_ID = r.Installation_ID
	       JOIN MACHINE WITH (NOLOCK)
	            ON  MACHINE.Machine_ID = i.Machine_ID
	       JOIN dbo.Bar_Position Bar_Position WITH (NOLOCK)
	            ON  Bar_Position.Bar_Position_ID = i.Bar_Position_ID
	       JOIN SITE S WITH (NOLOCK)
	            ON  S.Site_ID = Bar_Position.Site_ID
	       JOIN Sub_Company SC WITH (NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       JOIN Company C WITH (NOLOCK)
	            ON  C.Company_ID = SC.Company_ID
	       LEFT JOIN dbo.Zone Z WITH (NOLOCK)
	            ON  Z.Zone_ID = Bar_Position.Zone_ID
	       LEFT JOIN dbo.Machine_Type AS Machine_Type_Category WITH (NOLOCK)
	            ON  Machine_Type_Category.Machine_Type_ID = MACHINE.Machine_Category_ID
	WHERE  r.ReadDate BETWEEN 
	       #periods.[start] AND #periods.[end]
	       AND installation_price_per_play <> 0	          
	       AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Z.Zone_ID = @Zone))	          
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	           
 -- add eFunds enabled check here
	GROUP BY
	       Bar_Position.Bar_Position_ID,
	       Bar_Position.Bar_Position_Name,	-- stand              
	       Machine_Type_Category.Machine_Type_ID,
	       Machine_Type_Category.Machine_Type_Code,
	       S.Site_ID,
	       S.Site_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       C.Company_ID,
	       C.Company_Name,
	       MACHINE.Machine_Stock_No,	-- slot              
	       Z.Zone_Name,
	       Z.ZONE_ID,	-- area              
	       #Periods.ordering,
	       #Periods.name,
	       #Periods.start,
	       #Periods.[end] 
	--Details Section(1) 	
	
	SELECT 1 AS SortOrder,
	       * 
	       INTO #ResultSet
	FROM   #preGrouping 
	
	
	
	--Group By Zone(2)      
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    Bar_Position_ID,
	    Bar_Position_Name,
	    Machine_Type_ID,
	    Machine_Type_Code,
	    Site_ID,
	    Site_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Company_ID,
	    Company_Name,
	    Machine_Stock_No,
	    Zone_Name,
	    ZONE_ID,
	    [Order],
	    [Period],
	    [WAT_In],
	    [WAT_Out],
	    [Cashable_ePromo_In],
	    [Cashable_ePromo_Out],
	    [NCashable_ePromo_In],
	    [NCashable_ePromo_Out],
	    [eFund_Drop],
	    [eFund_Expense],
	    [eFund_Net]
	  )
	SELECT 2 AS SortOrder,
	       '',
	       '',
	       '',
	       '',
	       '',
	       Site_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       Company_Name,
	       '',
	       Zone_Name,
	       MAX(ISNULL(ZONE_ID, 0)) AS ZONE_ID,
	       [Order],
	       [Period],
	       [WAT_In] = CAST(SUM([WAT_In]) AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(SUM([WAT_Out]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(SUM([Cashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(SUM([Cashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(SUM([NCashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(SUM([NCashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [eFund_Drop] = CAST(SUM([eFund_Drop]) AS DECIMAL(10, 2)),
	       [eFund_Expense] = CAST(SUM([eFund_Expense]) AS DECIMAL(10, 2)),
	       [eFund_Net] = CAST(SUM([eFund_Net]) AS DECIMAL(10, 2))--,
	FROM   #preGrouping
	WHERE  (
	           (@Zone IS NOT NULL AND #preGrouping.ZONE_ID = @Zone)
	           OR @Zone IS NULL
	       )
	      
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       Site_Name,
	       Zone_Name,
	     
	       -- zone_id, /*Groping based on Zone Name Alone.If Grouping is based on Zone ID Records are Repeated*/            
	       [order],
	       [Period]
	ORDER BY
	       [Order] 
	--
	----Group By Site(3)    
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    Bar_Position_ID,
	    Bar_Position_Name,
	    Machine_Type_ID,
	    Machine_Type_Code,
	    Site_ID,
	    Site_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Company_ID,
	    Company_Name,
	    Machine_Stock_No,
	    Zone_Name,
	    Zone_ID,
	    [Order],
	    [Period],
	    [WAT_In],
	    [WAT_Out],
	    [Cashable_ePromo_In],
	    [Cashable_ePromo_Out],
	    [NCashable_ePromo_In],
	    [NCashable_ePromo_Out],
	    [eFund_Drop],
	    [eFund_Expense],
	    [eFund_Net]
	  )
	SELECT 3 AS SortOrder,
	       '',
	       '',
	       '',
	       '',
	       '',
	       Site_Name,
	       '',
	       Sub_Company_Name,
	       '',
	       Company_Name,
	       '',
	       '',
	       '',
	       [Order],
	       [Period],
	       [WAT_In] = CAST(SUM([WAT_In]) AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(SUM([WAT_Out]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(SUM([Cashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(SUM([Cashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(SUM([NCashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(SUM([NCashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [eFund_Drop] = CAST(SUM([eFund_Drop]) AS DECIMAL(10, 2)),
	       [eFund_Expense] = CAST(SUM([eFund_Expense]) AS DECIMAL(10, 2)),
	       [eFund_Net] = CAST(SUM([eFund_Net]) AS DECIMAL(10, 2))--,
	FROM   #preGrouping
	WHERE  ISNULL(@Site, #preGrouping.Site_ID) = #preGrouping.Site_ID 
	      
	       AND (
	               @SiteIDList IS NOT NULL
	               AND #preGrouping.Site_ID IN (SELECT DATA
	                                            FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND (
	               (@Zone IS NOT NULL AND #preGrouping.ZONE_ID = @Zone)
	               OR @Zone IS NULL
	           )
	         
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       Site_Name,
	       [order],
	       [Period] 
	
	----Group By Sub Company(4)    
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    Bar_Position_ID,
	    Bar_Position_Name,
	    Machine_Type_ID,
	    Machine_Type_Code,
	    Site_ID,
	    Site_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Company_ID,
	    Company_Name,
	    Machine_Stock_No,
	    Zone_Name,
	    Zone_ID,
	    [Order],
	    [Period],
	    [WAT_In],
	    [WAT_Out],
	    [Cashable_ePromo_In],
	    [Cashable_ePromo_Out],
	    [NCashable_ePromo_In],
	    [NCashable_ePromo_Out],
	    [eFund_Drop],
	    [eFund_Expense],
	    [eFund_Net]
	  )
	SELECT 4 AS SortOrder,
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       Sub_Company_Name,
	       '',
	       Company_Name,
	       '',
	       '',
	       '',
	       [Order],
	       [Period],
	       [WAT_In] = CAST(SUM([WAT_In]) AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(SUM([WAT_Out]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(SUM([Cashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(SUM([Cashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(SUM([NCashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(SUM([NCashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [eFund_Drop] = CAST(SUM([eFund_Drop]) AS DECIMAL(10, 2)),
	       [eFund_Expense] = CAST(SUM([eFund_Expense]) AS DECIMAL(10, 2)),
	       [eFund_Net] = CAST(SUM([eFund_Net]) AS DECIMAL(10, 2))
	FROM   #preGrouping
	WHERE  ISNULL(@SubCompany, #preGrouping.Sub_Company_ID) = #preGrouping.Sub_Company_ID
	      
	       AND (
	               (@Zone IS NOT NULL AND #preGrouping.ZONE_ID = @Zone)
	               OR @Zone IS NULL
	           )
	          
	GROUP BY
	       Company_Name,
	       Sub_Company_Name,
	       [order],
	       [Period] 
	
	--Group By Company (5)    
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    Bar_Position_ID,
	    Bar_Position_Name,
	    Machine_Type_ID,
	    Machine_Type_Code,
	    Site_ID,
	    Site_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Company_ID,
	    Company_Name,
	    Machine_Stock_No,
	    Zone_Name,
	    Zone_ID,
	    [Order],
	    [Period],
	    [WAT_In],
	    [WAT_Out],
	    [Cashable_ePromo_In],
	    [Cashable_ePromo_Out],
	    [NCashable_ePromo_In],
	    [NCashable_ePromo_Out],
	    [eFund_Drop],
	    [eFund_Expense],
	    [eFund_Net]
	  )
	SELECT 5 AS SortOrder,
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       '',
	       Company_Name,
	       '',
	       '',
	       '',
	       [Order],
	       [Period],
	       [WAT_In] = CAST(SUM([WAT_In]) AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(SUM([WAT_Out]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(SUM([Cashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(SUM([Cashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(SUM([NCashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(SUM([NCashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [eFund_Drop] = CAST(SUM([eFund_Drop]) AS DECIMAL(10, 2)),
	       [eFund_Expense] = CAST(SUM([eFund_Expense]) AS DECIMAL(10, 2)),
	       [eFund_Net] = CAST(SUM([eFund_Net]) AS DECIMAL(10, 2))
	FROM   #preGrouping
	WHERE  ISNULL(@Company, #preGrouping.Company_ID) = #preGrouping.Company_ID
	       AND (
	               (@Zone IS NOT NULL AND #preGrouping.ZONE_ID = @Zone)
	               OR @Zone IS NULL
	           )
	         
	GROUP BY
	       Company_Name,
	       [order],
	       [Period] 
	--Grand Total    
	
	INSERT INTO #ResultSet
	  (
	    SortOrder,
	    Bar_Position_ID,
	    Bar_Position_Name,
	    Machine_Type_ID,
	    Machine_Type_Code,
	    Site_ID,
	    Site_Name,
	    Sub_Company_ID,
	    Sub_Company_Name,
	    Company_ID,
	    Company_Name,
	    Machine_Stock_No,
	    Zone_Name,
	    Zone_ID,
	    [Order],
	    [Period],
	    [WAT_In],
	    [WAT_Out],
	    [Cashable_ePromo_In],
	    [Cashable_ePromo_Out],
	    [NCashable_ePromo_In],
	    [NCashable_ePromo_Out],
	    [eFund_Drop],
	    [eFund_Expense],
	    [eFund_Net]
	  )
	SELECT 6 AS SortOrder,
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
	       '',
	       '',
	       '',
	       [Order],
	       [Period],
	       [WAT_In] = CAST(SUM([WAT_In]) AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(SUM([WAT_Out]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(SUM([Cashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(SUM([Cashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(SUM([NCashable_ePromo_In]) AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(SUM([NCashable_ePromo_Out]) AS DECIMAL(10, 2)),
	       [eFund_Drop] = CAST(SUM([eFund_Drop]) AS DECIMAL(10, 2)),
	       [eFund_Expense] = CAST(SUM([eFund_Expense]) AS DECIMAL(10, 2)),
	       [eFund_Net] = CAST(SUM([eFund_Net]) AS DECIMAL(10, 2))
	FROM   #preGrouping
	WHERE  (
	           (@Zone IS NOT NULL AND #preGrouping.ZONE_ID = @Zone)
	           OR @Zone IS NULL
	       )
	      
	GROUP BY
	       [order],
	       [Period]        
	
	
	SELECT *
	FROM   #Resultset
	ORDER BY
	       SORTORDER,
	       [order],
	       [period],
	       Zone_Name,
	       site_name,
	       sub_company_name,
	       company_name
END
GO

