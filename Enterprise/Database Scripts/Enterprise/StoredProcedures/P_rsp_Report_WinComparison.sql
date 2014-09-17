USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_WinComparison]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_WinComparison]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE rsp_Report_WinComparison
	@Company		INT = 0,
	@SubCompany		INT = 0,
	@Region			INT = 0,
	@Area			INT = 0,
	@District		INT = 0,
	@Site			INT = 0,
	@Zone			INT = 0,
	@Slot			VARCHAR(50), --Asset Number
	@GamingDate		DATETIME,
	@IncludeNonCashable BIT,
	@UsePhysicalWin BIT,
	@Period			NVARCHAR(50),
	@SiteIDList		VARCHAR(MAX)
AS
BEGIN
	
	SET NOCOUNT ON                                  
	SET DATEFORMAT DMY       
	                    
	
	DECLARE @mtdStart       DATETIME,
	        @qtdStart       DATETIME,
	        @ytdStart       DATETIME,
	        @Criteria       VARCHAR(200),
	        @Denom          FLOAT,
	        @MachineTypeID  INT,
	        @RunType        INT                
	
	
	SET @MachineTypeID = 0                        
	SET @RunType = 1 
	--SET @UsePhysicalWin = 0                         
	SET @GamingDate = CONVERT(DATETIME, @GamingDate, 106) 
	
	-- create YTD                                  
	SET @ytdStart = '01 jan ' + CAST(YEAR(@GamingDate) AS VARCHAR(4)) 
	-- create mtd                                  
	SET @mtdStart = '01/' + CAST(DATEPART(MONTH, @GamingDate) AS VARCHAR(3)) +
	    '/' + CAST(YEAR(@GamingDate) AS VARCHAR(4)) 
	--Create QTD
	-- Quarter Jan-Mar, Apr-Jun, Jul-Sep,Oct-Dec                                      
	DECLARE @qStartMonth INT                                      
	SET @qStartMonth = CASE 
	                        WHEN MONTH(@GamingDate) BETWEEN 1 AND 3 THEN 1
	                        WHEN MONTH(@GamingDate) BETWEEN 4 AND 6 THEN 4
	                        WHEN MONTH(@GamingDate) BETWEEN 7 AND 9 THEN 7
	                        ELSE 10
	                   END    
	
	SET @qtdStart = '01/' + CAST(@qStartMonth AS VARCHAR(4)) + '/' + CAST(YEAR(CAST(@GamingDate AS DATETIME)) AS VARCHAR(4))                                      
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
	       [start] = CAST(@mtdStart AS DATETIME),
	       [end] = DATEADD(DD, 0, DATEDIFF(DD, 0, DATEADD(DAY, 1, @GamingDate)))
	WHERE  [name] = 'MTD'                     
	
	
	UPDATE #Periods
	SET    ordering = 2,
	       [start] = CAST(@qtdStart AS DATETIME),
	       [end] = DATEADD(DD, 0, DATEDIFF(DD, 0, DATEADD(DAY, 1, @GamingDate)))
	WHERE  [name] = 'QTD'                  
	
	UPDATE #Periods
	SET    ordering = 3,
	       [start] = CAST(@ytdStart AS DATETIME),
	       [end] = DATEADD(DD, 0, DATEDIFF(DD, 0, DATEADD(DAY, 1, @GamingDate)))
	WHERE  [name] = 'YTD'                                    
	
	UPDATE #Periods
	SET    ordering = 4,
	       [start] = CAST('01 jan 2000' AS DATETIME),
	       [end] = DATEADD(DD, 0, DATEDIFF(DD, 0, DATEADD(DAY, 1, @GamingDate))) -- arbitary date for start.
	WHERE  [name] = 'LTD' 
	
	--select * from #Periods                         
	
	SET @GamingDate = CONVERT(DATETIME, @GamingDate, 106)                     
	
	
	
	
	
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
	
	
	
	                                      
	
	SET @mtdStart = '01/' + CAST(
	        DATEPART(MONTH, CAST(@GamingDate AS DATETIME)) AS VARCHAR(3)
	    ) + '/' + CAST(YEAR(CAST(@GamingDate AS DATETIME)) AS VARCHAR(4))    
	
	SET @ytdStart = '01 Jan ' + CAST(YEAR(CAST(@GamingDate AS DATETIME)) AS VARCHAR(4))                                            
	
	DECLARE @Val VARCHAR(100)                      
	SELECT TOP 1 @Val = Setting_Value
	FROM   Setting(NOLOCK)
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'    
	
	
	CREATE TABLE #SLOT_MACHINE
	(
		[ORDER]                      VARCHAR(20),
		[Period]                     VARCHAR(20),
		Site_ID                      INT,
		Site_Name                    VARCHAR(50),
		ReadDate                     DATETIME,
		Area                         VARCHAR(MAX),
		Denom                        VARCHAR(20),
		Assets                       VARCHAR(50),
		Machine_Name                 VARCHAR(50),
		RDCCashIn                    FLOAT,
		Bets                         FLOAT,
		Wins                         FLOAT,
		ActualWin                    FLOAT,
		[Days]                       INT,
		GamesPlayed                  INT,
		TotalCashIn                  FLOAT,
		READ_COINS_IN                INT,
		Installation_Price_Per_Play  INT,
		TheoreticalPayout            FLOAT,
		MeterDrop                    FLOAT,
		TotalExpenses                FLOAT,
		ManufacturerName             VARCHAR(50),
		TotalJackpots                FLOAT,
		WinPerDevice                 FLOAT,
		GameIdentifier               VARCHAR(50),
		Slot                         VARCHAR(50),
		Position                     VARCHAR(50),
		Gametype                     VARCHAR(50)
	)     
	
	INSERT INTO #SLOT_MACHINE
	SELECT DISTINCT 
	       
	       
	       [Order] = #Periods.ordering,
	       [Period] = #Periods.[Name],
	       --Site ID                
	       S.Site_ID,
	       --Site Name                
	       S.Site_Name,
	       --Read Date                  
	       R.ReadDate AS ReadDate,
	       --Finding Area                  
	       ISNULL(Z.Zone_Name, 'NOTSET') AS Area,
	       --Finding Denom                  
	       
	       CASE 
	            WHEN M.IsMultiGame = 1 THEN 'Multi'
	            ELSE CAST(
	                     (CAST(ISNULL(I.Installation_Token_Value, 0) AS FLOAT) / 100) 
	                     AS VARCHAR(20)
	                 )
	       END AS Denom,
	       --Finding Asset                
	       
	       M.Machine_Stock_No AS Assets,
	       MC.Machine_Name,
	       --- Finding Actual hold Percentage                    
	       (
	           CAST(ISNULL(READ_COINS_IN, 0) AS FLOAT) * I.Installation_Price_Per_Play
	       ) / 100.0 AS RDCCashIn,
	       ---Finding Bets                    
	       (
	           CAST(ISNULL(READ_COINS_IN, 0) AS FLOAT) * I.Installation_Price_Per_Play
	       ) / 100.0 AS Bets,
	       (
	           CAST(ISNULL(READ_COINS_OUT, 0) AS FLOAT) * I.Installation_Price_Per_Play
	       ) / 100.0 AS Wins,
	       CASE 
	            WHEN @IncludeNonCashable = 0 THEN CASE 
	                                                   WHEN @UsePhysicalWin =
	                                                        1 THEN (
	                                                            (
	                                                                CAST(ISNULL(R.Cashable_EFT_IN, 0) AS FLOAT) 
	                                                                * I.Installation_Price_Per_Play
	                                                            ) / 100.0
	                                                        ) + (
	                                                            (
	                                                                CAST(ISNULL(READ_COINS_IN, 0) AS FLOAT) 
	                                                                * I.Installation_Price_Per_Play
	                                                            ) / 100.0
	                                                        ) + (
	                                                            (
	                                                                CAST(ISNULL(READ_TICKET_VALUE, 0) AS FLOAT) 
	                                                                * I.Installation_Price_Per_Play
	                                                            ) / 100.0
	                                                        ) 
	                                                        
	                                                        -(
	                                                            (
	                                                                CAST(ISNULL(READ_COINS_OUT, 0) AS FLOAT) 
	                                                                * I.Installation_Price_Per_Play
	                                                            ) / 100.0
	                                                        ) -(
	                                                            (
	                                                                CAST(ISNULL(R.Cashable_EFT_OUT, 0) AS FLOAT) 
	                                                                * I.Installation_Price_Per_Play
	                                                            ) / 100.0
	                                                        )
	                                                   ELSE --Bet-(Win+Jackpots)                  
	                                                        (
	                                                            (
	                                                                CAST(ISNULL(SUM(CAST(READ_COINS_IN AS FLOAT)), 0) AS FLOAT) 
	                                                                * I.Installation_Price_Per_Play
	                                                            ) / 100.0
	                                                        ) -(
	                                                            (
	                                                                (
	                                                                    CAST(ISNULL(READ_COINS_OUT, 0) AS FLOAT) 
	                                                                    * I.Installation_Price_Per_Play
	                                                                ) / 100.0
	                                                            ) + (
	                                                                (
	                                                                    CAST(ISNULL(READ_RDC_JACKPOT, 0) AS FLOAT) 
	                                                                    * I.Installation_Price_Per_Play
	                                                                ) / 100.0
	                                                            )
	                                                        )
	                                              END
	            ELSE CASE 
	                      WHEN @UsePhysicalWin = 1 THEN (
	                               (
	                                   CAST(ISNULL(R.NonCashable_EFT_IN, 0) AS FLOAT) 
	                                   * I.Installation_Price_Per_Play
	                               ) / 100.0
	                           ) + (
	                               (
	                                   CAST(ISNULL(READ_COINS_IN, 0) AS FLOAT) 
	                                   * I.Installation_Price_Per_Play
	                               ) / 100.0
	                           ) + (
	                               (
	                                   CAST(ISNULL(R.TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                                   * I.Installation_Price_Per_Play
	                               ) / 100.0
	                           ) -(
	                               (
	                                   CAST(ISNULL(READ_COINS_OUT, 0) AS FLOAT) 
	                                   * I.Installation_Price_Per_Play
	                               ) / 100.0
	                           ) -(
	                               (
	                                   CAST(ISNULL(R.NonCashable_EFT_OUT, 0) AS FLOAT) 
	                                   * I.Installation_Price_Per_Play
	                               ) / 100.0
	                           )
	                      ELSE --Bet-(Win+Jackpots)                  
	                           (
	                               (
	                                   CAST(ISNULL(SUM(CAST(READ_COINS_IN AS FLOAT)), 0) AS FLOAT) 
	                                   * I.Installation_Price_Per_Play
	                               ) / 100.0
	                           ) -(
	                               (
	                                   (
	                                       CAST(ISNULL(READ_COINS_OUT, 0) AS FLOAT) 
	                                       * I.Installation_Price_Per_Play
	                                   ) / 100.0
	                               ) + (
	                                   (
	                                       CAST(ISNULL(READ_RDC_JACKPOT, 0) AS FLOAT) 
	                                       * I.Installation_Price_Per_Play
	                                   ) / 100.0
	                               )
	                           )
	                 END
	       END AS ActualWin,	--(cashin-cashout)                      
	       
	       
	       --Finding Days                    
	       
	       CASE 
	            WHEN ISNULL(R.Read_Days, 0) = 0 THEN 1
	            ELSE ISNULL(R.Read_Days, 0)
	       END AS [Days],
	       --Finding Games Played                    
	       ISNULL(R.READ_GAMES_BET, 0) AS GamesPlayed,
	       --Finding Win Per Device                  
	       CASE 
	            WHEN @IncludeNonCashable = 1 THEN (
	                     (
	                         CAST(ISNULL(R.READ_RDC_TRUE_COIN_IN, 0) AS FLOAT) 
	                         * I.Installation_Price_Per_Play
	                     ) / 100.0 +
	                     CAST(ISNULL(R.READ_RDC_BILL_1, 0) AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_2, 0) * 2.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_5, 0) * 5.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_10, 0) * 10.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_20, 0) * 20.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_50, 0) * 50.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_100, 0) * 100.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_200, 0) * 200.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_250, 0) * 250.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_500, 0) * 500.0 AS FLOAT) +
	                     CAST(ISNULL(R.READ_RDC_BILL_10000, 0) * 10000.0 AS FLOAT) 
	                     +
	                     CAST(ISNULL(R.READ_RDC_BILL_20000, 0) * 20000.0 AS FLOAT) 
	                     +
	                     CAST(ISNULL(R.READ_RDC_BILL_50000, 0) * 50000.0 AS FLOAT) 
	                     +
	                     CAST(ISNULL(R.READ_RDC_BILL_100000, 0) * 100000.0 AS FLOAT) 
	                     + (
	                         CAST(ISNULL(R.READ_TICKET_IN_SUSPENSE, 0) AS FLOAT) 
	                         / 100.0
	                     ) 
	                     + (
	                         CAST(ISNULL(R.TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                         / 100.0
	                     ) + (
	                         CASE 
	                              WHEN @Val = 'True' THEN (
	                                       (CAST(ISNULL(R.Promo_Cashable_EFT_IN, 0) AS FLOAT)) 
	                                       + (CAST(ISNULL(R.NonCashable_EFT_IN, 0) AS FLOAT)) 
	                                       + (CAST(ISNULL(R.Cashable_EFT_IN, 0) AS FLOAT))
	                                   ) / 100.0
	                              ELSE 0
	                         END
	                     )
	                 )
	            ELSE (
	                     CAST(ISNULL(R.READ_RDC_TRUE_COIN_IN, 0) AS FLOAT) * I.Installation_Price_Per_Play
	                 ) / 100.0 +
	                 CAST(ISNULL(R.READ_RDC_BILL_1, 0) AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_2, 0) * 2.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_5, 0) * 5.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_10, 0) * 10.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_20, 0) * 20.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_50, 0) * 50.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_100, 0) * 100.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_200, 0) * 200.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_250, 0) * 250.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_500, 0) * 500.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_10000, 0) * 10000.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_20000, 0) * 20000.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_50000, 0) * 50000.0 AS FLOAT) +
	                 CAST(ISNULL(R.READ_RDC_BILL_100000, 0) * 100000.0 AS FLOAT) 
	                 + (
	                     CAST(ISNULL(R.READ_TICKET_IN_SUSPENSE, 0) AS FLOAT) / 
	                     100.0
	                 ) 
	                 + (CAST(ISNULL(R.READ_TICKET, 0)AS FLOAT) / 100.0) + (
	                     CASE 
	                          WHEN @Val = 'True' THEN (
	                                   (CAST(ISNULL(R.Promo_Cashable_EFT_IN, 0) AS FLOAT)) 
	                                   + (CAST(ISNULL(R.NonCashable_EFT_IN, 0) AS FLOAT)) 
	                                   + (CAST(ISNULL(R.Cashable_EFT_IN, 0) AS FLOAT))
	                               ) / 100.0
	                          ELSE 0
	                     END
	                 )
	       END AS TotalCashIn,
	       R.READ_COINS_IN AS READ_COINS_IN,
	       I.Installation_Price_Per_Play AS Installation_Price_Per_Play,
	       (100.0 - I.Installation_Percentage_Payout) AS TheoreticalPayout,
	       --- Findind Meter Drop                     
	       CASE 
	            WHEN @IncludeNonCashable = 1 THEN (CAST(ISNULL(R.READ_COIN_DROP, 0) AS FLOAT) / 100.0)
	            ELSE (CAST(ISNULL(R.READ_COIN_DROP, 0) AS FLOAT) / 100.0) -(
	                     (
	                         (
	                             CAST(ISNULL(R.TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                             / 100.0
	                         )
	                     ) 
	                     + (CAST(ISNULL(R.NonCashable_EFT_IN, 0) AS FLOAT) / 100.0)
	                 )
	       END AS MeterDrop,
	       ---Finding Total Expenses                    
	       CASE 
	            WHEN @IncludeNonCashable = 1 THEN (
	                     (
	                         CAST(ISNULL(R.READ_HANDPAY, 0) AS FLOAT) * I.Installation_Price_Per_Play
	                     ) / 100.0
	                 ) + (CAST(ISNULL(R.READ_TICKET, 0) AS FLOAT) / 100.0) + (
	                     CAST(ISNULL(R.TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                     / 100.0
	                 ) + (
	                     CASE 
	                          WHEN @Val = 'True' THEN (
	                                   (CAST(ISNULL(R.Promo_Cashable_EFT_OUT, 0) AS FLOAT)) 
	                                   + (CAST(ISNULL(R.NonCashable_EFT_OUT, 0) AS FLOAT)) 
	                                   + (CAST(ISNULL(R.Cashable_EFT_OUT, 0)AS FLOAT))
	                               ) / 100.0
	                          ELSE 0
	                     END
	                 )
	            ELSE (
	                     (
	                         (
	                             CAST(ISNULL(R.READ_HANDPAY, 0) AS FLOAT) * I.Installation_Price_Per_Play
	                         ) / 100.0
	                     ) + (CAST(ISNULL(R.READ_TICKET, 0) AS FLOAT) / 100.0) 
	                     + (
	                         CASE 
	                              WHEN @Val = 'True' THEN (
	                                       (CAST(ISNULL(R.Promo_Cashable_EFT_OUT, 0) AS FLOAT)) 
	                                       + (CAST(ISNULL(R.Cashable_EFT_OUT, 0) AS FLOAT))
	                                   ) / 100.0
	                              ELSE 0
	                         END
	                     )
	                 )
	       END AS TotalExpenses,
	       -- Finding Manufacturer Name                       
	       MR.Manufacturer_Name AS ManufacturerName,
	       --Finding Total Number of Jackpots                       
	       (CAST(ISNULL(R.READ_RDC_JACKPOT, 0) AS FLOAT) / 100.0) AS 
	       TotalJackpots,
	       --Winperdevice         
	       CASE 
	            WHEN (@IncludeNonCashable = 0 AND @UsePhysicalWin = 0) THEN (CAST(ISNULL(R.READ_COIN_DROP, 0) AS FLOAT) / 100.0) 
	                 -(
	                     (
	                         (
	                             CAST(ISNULL(R.READ_HANDPAY, 0) AS FLOAT) * I.Installation_Price_Per_Play
	                         ) / 100.0
	                     ) + (CAST(ISNULL(R.READ_TICKET, 0) AS FLOAT) / 100.0) 
	                     + (
	                         CAST(ISNULL(R.TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                         / 100.0
	                     ) + (
	                         CASE 
	                              WHEN @Val = 'True' THEN (
	                                       (CAST(ISNULL(R.Promo_Cashable_EFT_OUT, 0) AS FLOAT)) 
	                                       + (CAST(ISNULL(R.NonCashable_EFT_OUT, 0) AS FLOAT)) 
	                                       + (CAST(ISNULL(R.Cashable_EFT_OUT, 0) AS FLOAT))
	                                   ) / 100.0
	                              ELSE 0
	                         END
	                     )
	                 )
	            ELSE (
	                     (
	                         CASE 
	                              WHEN @IncludeNonCashable = 1 THEN (CAST(ISNULL(R.READ_COIN_DROP, 0) AS FLOAT) / 100.0)
	                              ELSE (CAST(ISNULL(R.READ_COIN_DROP, 0) AS FLOAT) / 100.0) 
	                                   -(
	                                       (
	                                           (
	                                               CAST(ISNULL(R.TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                                               / 100.0
	                                           )
	                                       ) 
	                                       + (CAST(ISNULL(R.NonCashable_EFT_IN, 0) AS FLOAT) / 100.0)
	                                   )
	                         END
	                     ) -(
	                         CASE 
	                              WHEN @IncludeNonCashable = 1 THEN (
	                                       (
	                                           CAST(ISNULL(R.READ_HANDPAY, 0) AS FLOAT) 
	                                           * I.Installation_Price_Per_Play
	                                       ) / 100.0
	                                   ) + (CAST(ISNULL(R.READ_TICKET, 0) AS FLOAT) / 100.0) 
	                                   + (
	                                       CAST(ISNULL(R.TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT) 
	                                       / 100.0
	                                   ) + (
	                                       CASE 
	                                            WHEN @Val = 'True' THEN (
	                                                     (CAST(ISNULL(R.Promo_Cashable_EFT_OUT, 0) AS FLOAT)) 
	                                                     + (CAST(ISNULL(R.NonCashable_EFT_OUT, 0) AS FLOAT)) 
	                                                     + (CAST(ISNULL(R.Cashable_EFT_OUT, 0) AS FLOAT))
	                                                 ) / 100
	                                            ELSE 0
	                                       END
	                                   )
	                              ELSE (
	                                       (
	                                           (
	                                               CAST(ISNULL(R.READ_HANDPAY, 0) AS FLOAT) 
	                                               * I.Installation_Price_Per_Play
	                                           ) / 100.0
	                                       ) + (CAST(ISNULL(R.READ_TICKET, 0) AS FLOAT) / 100.0) 
	                                       + (
	                                           CASE 
	                                                WHEN @Val = 'True' THEN (
	                                                         (CAST(ISNULL(R.Promo_Cashable_EFT_OUT, 0) AS FLOAT)) 
	                                                         + (CAST(ISNULL(R.Cashable_EFT_OUT, 0) AS FLOAT))
	                                                     ) / 100
	                                                ELSE 0
	                                           END
	                                       )
	                                   )
	                         END
	                     )
	                 )
	       END AS WinPerDevice,
	       ---Finding GameIdentifier                      
	       M.ActAssetNo AS GameIdentifier,
	       --Finding Slot                  
	       M.Machine_Stock_No AS Slot,
	       --Finding Stand                    
	       BP.Bar_Position_Name AS Position,
	       --Finding Game Type    
	       ISNULL(MC.Machine_Name, '') AS Gametype
	FROM   #periods,
	       dbo.Installation I
	       INNER JOIN dbo.[Read] R WITH(NOLOCK)
	            ON  R.Installation_ID = I.Installation_ID
	       INNER JOIN dbo.Bar_Position BP WITH (NOLOCK)
	            ON  I.Bar_Position_ID = BP.Bar_Position_ID
	       INNER JOIN dbo.MACHINE M WITH (NOLOCK)
	            ON  I.Machine_ID = M.Machine_ID
	       INNER  JOIN dbo.Machine_Class MC WITH (NOLOCK)
	            ON  MC.Machine_Class_ID = M.Machine_Class_ID
	       INNER JOIN dbo.Manufacturer MR WITH (NOLOCK)
	            ON  MR.Manufacturer_ID = MC.Manufacturer_ID
	       INNER JOIN dbo.[Site] S WITH (NOLOCK)
	            ON  BP.Site_ID = S.Site_ID
	       INNER JOIN dbo.Sub_Company SC WITH (NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN dbo.Company CO WITH (NOLOCK)
	            ON  CO.Company_ID = SC.Company_ID
	       LEFT OUTER JOIN dbo.Zone Z WITH (NOLOCK)
	            ON  BP.Zone_ID = Z.Zone_ID
	WHERE  (
	           (
	               R.ReadDate >= #Periods.start
	               AND R.ReadDate < #Periods.[end]
	           ) 
	           
	           --(R.Read_Date >= #Periods.start AND R.Read_Date < #Periods.[end])
	           AND ISNULL(@Company, CO.company_id) = CO.company_id
	           AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	           AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	           AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	           AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	           AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND S.Site_ID IN (SELECT DATA
	                                     FROM   fnSplit(@SiteIDList, ','))
	               )
	           AND ISNULL(@Zone, Z.Zone_ID) = Z.Zone_ID
	           AND (
	                   (@Slot <> '' AND M.Machine_Stock_No = @Slot)
	                   OR @Slot = '--ALL--' OR @Slot = 'All'
	               )
	       )
	GROUP BY
	       R.ReadDate,
	       S.Site_ID,
	       S.Site_Name,
	       Z.Zone_Name,
	       i.Installation_Percentage_Payout,
	       mc.Machine_Name,
	       M.IsMultiGame,
	       R.READ_COINS_IN,
	       I.Installation_Price_Per_Play,
	       READ_COINS_OUT,
	       R.Read_Days,
	       R.READ_GAMES_BET,
	       R.READ_RDC_TRUE_COIN_IN,
	       R.READ_RDC_BILL_1,
	       R.READ_RDC_BILL_2,
	       R.READ_RDC_BILL_5,
	       R.READ_RDC_BILL_10,
	       R.READ_RDC_BILL_20,
	       R.READ_RDC_BILL_50,
	       R.READ_RDC_BILL_100,
	       R.READ_RDC_BILL_200,
	       R.READ_RDC_BILL_250,
	       R.READ_RDC_BILL_500,
	       R.READ_RDC_BILL_10000,
	       R.READ_RDC_BILL_20000,
	       R.READ_RDC_BILL_50000,
	       R.READ_RDC_BILL_100000,
	       R.READ_COIN_DROP,
	       R.READ_TICKET_IN_SUSPENSE,
	       R.READ_TICKET_VALUE,
	       R.TICKETS_INSERTED_NONCASHABLE_VALUE,
	       R.Promo_Cashable_EFT_IN,
	       R.NonCashable_EFT_IN,
	       R.Cashable_EFT_IN,
	       R.READ_RDC_TRUE_COIN_OUT,
	       R.READ_HANDPAY,
	       R.READ_TICKET,
	       R.TICKETS_PRINTED_NONCASHABLE_VALUE,
	       R.Promo_Cashable_EFT_OUT,
	       R.NonCashable_EFT_OUT,
	       R.Cashable_EFT_OUT,
	       MR.Manufacturer_Name,
	       R.READ_RDC_JACKPOT,
	       M.ActAssetNo,
	       M.Machine_Stock_No,
	       BP.Bar_Position_Name,
	       i.Installation_Token_Value,
	       #Periods.ordering,
	       #Periods.[Name],
	       #Periods.start,
	       #Periods.[end]    
	
	SELECT [order],
	       [Period],
	       Site_ID,
	       Site_Name,
	       Area,
	       Denom,
	       Assets,
	       TheoreticalPayout AS ActualTheoreticalPayout,
	       SUM(TheoreticalPayout) AS TheoreticalPayout,
	       SUM(Bets) AS Bets,
	       SUM(Wins) AS Wins,
	       SUM(MeterDrop) AS MeterDrop,
	       SUM(TotalExpenses) AS TotalExpenses,
	       SUM([Days]) AS [days],
	       SUM(CAST(GamesPlayed AS FLOAT)) AS GamesPlayed,
	       SUM(WinPerDevice) AS WinPerDevice,
	       WinPerDevicePer = SUM(WinPerDevice / [Days]),
	       ManufacturerName,
	       SUM(TotalJackpots) AS TotalJackpots,
	       GameIdentifier,
	       Slot,
	       Position,
	       GameType 
	       INTO #tmp
	FROM   #SLOT_MACHINE
	GROUP BY
	       [order],
	       [Period],
	       Site_ID,
	       Site_Name,
	       Area,
	       Denom,
	       Assets,
	       TheoreticalPayout,
	       ActualWin,
	       RDCCashIn,
	       Bets,
	       Installation_Price_Per_Play,
	       TotalCashIn,
	       ManufacturerName,
	       GameIdentifier,
	       Slot,
	       Position,
	       GameType     
	
	SELECT 1 AS SortOrder,
	       [Order],
	       [Period],
	       0 AS Site_ID,
	       Site_Name,
	       Area,
	       Denom,
	       Assets,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (
	                         (100 -((SUM(Wins) / SUM(Bets)) * 100)) - 
	                         AVG(ActualTheoreticalPayout)
	                     )
	           END
	       ) AS ActTheoVarPercentage,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (100 -((SUM(Wins) / SUM(Bets)) * 100))
	           END
	       ) AS ActualHoldPercentage,
	       SUM(Bets) AS Bets,
	       SUM(MeterDrop) AS MeterDrop,
	       SUM(TotalExpenses) AS TotalExpenses,
	       SUM([Days]) AS [Days],
	       SUM(CAST(GamesPlayed AS FLOAT)) AS GamesPlayed,
	       SUM(WinPerDevice) AS WinPerDevice,
	       WinPerDevicePer = SUM(WinPerDevice) / SUM([Days]),
	       ManufacturerName,
	       SUM(TotalJackpots) AS TotalJackpots,
	       GameIdentifier,
	       Slot,
	       Position,
	       GameType 
	       INTO 
	       #tmpActual
	FROM   #tmp
	GROUP BY
	       Site_Name,
	       Area,
	       Denom,
	       Assets,
	       ManufacturerName,
	       GameIdentifier,
	       Slot,
	       GameType,
	       [Order],
	       [Period],
	       Position 
	
	
	-- Group by denom                
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_ID,
	    Site_Name,
	    Area,
	    Denom,
	    Assets,
	    ActTheoVarPercentage,
	    ActualHoldPercentage,
	    Bets,
	    MeterDrop,
	    TotalExpenses,
	    [Days],
	    GamesPlayed,
	    WinPerDevice,
	    WinPerDevicePer,
	    TotalJackpots,
	    GameType
	  )
	SELECT 2 AS SortOrder,
	       [order],
	       [Period],
	       Site_ID,
	       Site_Name,
	       ISNULL(Area, 'NOTSET') AS Area,
	       Denom,
	       '' AS assets,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (
	                         (100 -((SUM(Wins) / SUM(Bets)) * 100)) - 
	                         AVG(ActualTheoreticalPayout)
	                     )
	           END
	       ) AS ActTheoVarPercentage,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (100 -((SUM(Wins) / SUM(Bets)) * 100))
	           END
	       ) AS ActualHoldPercentage,
	       Bets = SUM(Bets),
	       MeterDrop = SUM(MeterDrop),
	       TotalExpenses = SUM(TotalExpenses),
	       SUM([Days]) AS [Days],
	       NoOfGamesPlayed = SUM(CAST(GamesPlayed AS FLOAT)),
	       WinPerDevice = SUM(WinPerDevice),
	       WinPerDevicePer = SUM(WinPerDevice) / SUM([Days]),
	       TotalJackpots = SUM(TotalJackpots),
	       ''
	FROM   #tmp
	GROUP BY
	       Site_Name,
	       denom,
	       area,
	       Site_ID,
	       [Order],
	       [period] 
	
	
	-- Group by Zone              
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_ID,
	    Site_Name,
	    Area,
	    Denom,
	    Assets,
	    ActTheoVarPercentage,
	    ActualHoldPercentage,
	    Bets,
	    MeterDrop,
	    TotalExpenses,
	    [Days],
	    GamesPlayed,
	    WinPerDevice,
	    WinPerDevicePer,
	    TotalJackpots,
	    GameType
	  )
	SELECT 3 AS SortOrder,
	       [order],
	       [Period],
	       Site_ID,
	       Site_Name,
	       ISNULL(Area, 'NOTSET') AS Area,
	       '' AS Denom,
	       '' AS assets,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (
	                         (100 -((SUM(Wins) / SUM(Bets)) * 100)) - 
	                         AVG(ActualTheoreticalPayout)
	                     )
	           END
	       ) AS ActTheoVarPercentage,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (100 -((SUM(Wins) / SUM(Bets)) * 100))
	           END
	       ) AS ActualHoldPercentage,
	       Bets = SUM(Bets),
	       MeterDrop = SUM(MeterDrop),
	       TotalExpenses = SUM(TotalExpenses),
	       SUM([Days]) AS [Days],
	       NoOfGamesPlayed = SUM(CAST(GamesPlayed AS FLOAT)),
	       WinPerDevice = SUM(WinPerDevice),
	       WinPerDevicePer = SUM(WinPerDevice) / SUM([Days]),
	       TotalJackpots = SUM(TotalJackpots),
	       ''
	FROM   #tmp
	GROUP BY
	       area,
	       Site_ID,
	       Site_Name,
	       [Order],
	       [period] 
	--,      ActualTheoreticalPayout    
	
	--Group by site                
	
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_ID,
	    Site_Name,
	    Area,
	    Denom,
	    Assets,
	    ActTheoVarPercentage,
	    ActualHoldPercentage,
	    Bets,
	    MeterDrop,
	    TotalExpenses,
	    [Days],
	    GamesPlayed,
	    WinPerDevice,
	    WinPerDevicePer,
	    TotalJackpots,
	    GameType
	  )
	SELECT 4 AS SortOrder,
	       [order],
	       [Period],
	       Site_ID,
	       Site_Name,
	       '' AS Area,
	       '' AS Denom,
	       '' AS assets,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (
	                         (100 -((SUM(Wins) / SUM(Bets)) * 100)) 
	                         - AVG(ActualTheoreticalPayout)
	                     )
	           END
	       ) AS ActTheoVarPercentage,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (100 -((SUM(Wins) / SUM(Bets)) * 100))
	           END
	       ) AS ActualHoldPercentage,
	       Bets = SUM(Bets),
	       MeterDrop = SUM(MeterDrop),
	       TotalExpenses = SUM(TotalExpenses),
	       SUM([Days]) AS [days],
	       NoOfGamesPlayed = SUM(CAST(GamesPlayed AS FLOAT)),
	       WinPerDevice = SUM(WinPerDevice),
	       WinPerDevicePer = SUM(WinPerDevice) / SUM([Days]),
	       TotalJackpots = SUM(TotalJackpots),
	       ''
	FROM   #tmp
	GROUP BY
	       Site_ID,
	       Site_Name,
	       [Order],
	       [period] 
	
	
	--Grand Total                
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Order],
	    [Period],
	    Site_ID,
	    Site_Name,
	    Area,
	    Denom,
	    Assets,
	    ActTheoVarPercentage,
	    ActualHoldPercentage,
	    Bets,
	    MeterDrop,
	    TotalExpenses,
	    [Days],
	    GamesPlayed,
	    WinPerDevice,
	    WinPerDevicePer,
	    TotalJackpots,
	    GameType
	  )
	SELECT 5 AS SortOrder,
	       [order],
	       [Period],
	       '' AS Site_Id,
	       '' AS Site_name,
	       '' AS Area,
	       '' AS Denom,
	       '' AS assets,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (
	                         (100 -((SUM(Wins) / SUM(Bets)) * 100)) - 
	                         AVG(ActualTheoreticalPayout)
	                     )
	           END
	       ) AS ActTheoVarPercentage,
	       (
	           CASE SUM(Bets)
	                WHEN 0 THEN 0
	                ELSE (100 -((SUM(Wins) / SUM(Bets)) * 100))
	           END
	       ) AS ActualHoldPercentage,
	       Bets = SUM(Bets),
	       MeterDrop = SUM(MeterDrop),
	       TotalExpenses = SUM(TotalExpenses),
	       SUM([Days]) AS [days],
	       NoOfGamesPlayed = SUM(CAST(GamesPlayed AS FLOAT)),
	       WinPerDevice = SUM(WinPerDevice),
	       WinPerDevicePer = SUM(WinPerDevice) / SUM([Days]),
	       TotalJackpots = SUM(TotalJackpots),
	       ''
	FROM   #tmp
	GROUP BY
	       [period],
	       [Order]  
	
	
	SELECT *
	FROM   #tmpActual
	ORDER BY
	       SortOrder 
	
	DROP TABLE #tmp 
	DROP TABLE #tmpActual 
	DROP TABLE #Periods 
	DROP TABLE #SLOT_MACHINE
END
GO

