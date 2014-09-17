
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_PerUnitPerDayPerformanceReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_PerUnitPerDayPerformanceReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--exec rsp_Report_PerUnitPerDayPerformanceReport @basedon = 1, @company =0,@subcompany=0,@site=0,@zone=0,@startdate ='01 Jan 2014',@enddate='01 May 2014'
  
CREATE PROCEDURE [dbo].[rsp_Report_PerUnitPerDayPerformanceReport]
	@BasedOn INT,
	@Company INT,
	@SubCompany INT,
	@Site INT,
	@Zone INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@SiteIDList VARCHAR(MAX) = null 
AS
BEGIN
	SET NOCOUNT ON  
	
	IF @BasedOn = 1
	BEGIN
	    SELECT CASE 
	                WHEN CAST(tI.Installation_Start_Date AS DATETIME) > CAST(@StartDate AS DATETIME) THEN 
	                     CAST(tI.Installation_Start_Date AS DATETIME)
	                ELSE @StartDate
	           END AS Installation_Start_Date,
	           CAST(
	               (
	                   CASE 
	                        WHEN tI.Installation_End_Date IS NULL THEN @EndDate
	                        WHEN tI.Installation_End_Date > @EndDate THEN @EndDate
	                        ELSE tI.Installation_End_Date
	                   END
	               )AS DATETIME
	           ) AS Installation_End_Date,
	           CASE 
	                WHEN (tMI.MGMD_Denom_Value = 0) THEN CAST((tI.Installation_Price_Per_Play) / 100.0 AS FLOAT)
	                ELSE CAST(tMI.MGMD_Denom_Value / 100.0 AS FLOAT)
	           END AS Denom,
	           tPT.Payout AS RTP,	--Percentage Payout    
	           COALESCE(Z.Zone_Name, '[NOT SET]') AS Zone_Name,
	           S.Site_ID,
	           S.Site_Name,
	           SC.Sub_Company_ID,
	           SC.Sub_Company_Name,
	           C.Company_ID,
	           C.Company_Name,
	           tM.Machine_Stock_No AS AssetNo,
	           BP.Bar_Position_Name AS Bar_Position_Name,
	           Machine_Type_Code AS GameType,
	           tGT.Game_Title AS GameName,
            SUM( CAST(  
                    (tMS.MGMD_Coins_IN * tI.Installation_Price_Per_Play) /   
                    100.0   
                    AS FLOAT  
                )  ) AS CoinIN,  
	           SUM(tMS.MGMD_Games_Bet) AS HandlePulls,	--Games Played    
	           SUM(
	               CAST(
	                   (tMS.MGMD_Coins_IN * tI.Installation_Price_Per_Play) / 
	                   100.0 
	                   AS FLOAT
	               )
	           ) AS Bets,	--     
	           
	           (
	               (
	                   SUM(
	                       CAST(
	                           tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play 
	                           AS 
	                           FLOAT
	                       )
	                   ) / 100
	               ) -(
	                   SUM(
	                       CAST(
	                           tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play 
	                           AS FLOAT
	                       )
	                   ) / 100 + SUM(
								CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
								) / 100
	               )
	           ) AS Wins,
	           (
	                   SUM(
	                       CAST(
	                           tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play 
	                           AS FLOAT
	                       )
	                   ) / 100 + 
							SUM(
							CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
							) / 100
	               )
	            AS CoinOut,
	           (
	               SUM(
	                   CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
	               ) / 100
	           ) AS Jackpots,
	           --TheoHoldPercent          
	           (100 - tPT.Payout) AS TheoHoldPercent 
	           INTO #BASETABLE
	    FROM   --#Periods,    
	           Installation tI
	           INNER JOIN Bar_Position BP
	                ON  tI.Bar_Position_ID = BP.Bar_Position_ID
	           LEFT JOIN ZONE Z
	                ON  BP.Zone_ID = Z.Zone_ID
	           INNER JOIN SITE S
	                ON  S.Site_ID = BP.SITE_ID
	           INNER JOIN Sub_Company SC
	                ON  SC.Sub_Company_ID = S.Sub_Company_ID
	           INNER JOIN COMPANY C
	                ON  C.Company_ID = SC.Company_ID
	           INNER JOIN MACHINE tM
	                ON  tI.Machine_ID = tM.Machine_ID
	           INNER JOIN Machine_Type MT
	                ON  MT.Machine_Type_ID = tM.machine_category_id
	           INNER JOIN Machine_Class MC
	                ON  tM.Machine_Class_ID = MC.Machine_Class_ID
	           INNER JOIN manufacturer
	                ON  manufacturer.Manufacturer_ID = MC.Manufacturer_ID
	           INNER JOIN MGMD_Installation tMI
	                ON  tI.Installation_ID = tMI.MGMD_Installation_ID
	           LEFT JOIN Game_Library tGL
	                ON  tMI.MGMD_Game_ID = tGL.MG_Game_ID
	           LEFT JOIN PayTable tPT
	                ON  tMI.MGMD_Paytable_ID = tPT.Paytable_ID
	           LEFT JOIN Game_Title tGT
	                ON  tGL.MG_Group_ID = tGT.Game_Title_ID
	           LEFT JOIN Game_Category tGC
	                ON  tGC.Game_Category_ID = tGT.Game_Category_ID
	           LEFT JOIN MGMD_SessionDelta tMS
	                ON  tMI.MGMD_ID = tMS.MGMD_Combination_ID
	    WHERE  --(tMS.MGMD_Start_DateTime >= @StartDate AND tMS.MGMD_End_DateTime <= @EndDate)  
	           DATEADD(DD, 0, DATEDIFF(DD, 0, tMS.MGMD_Start_DateTime)) >= 
	           DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	           AND DATEADD(DD, 0, DATEDIFF(DD, 0, tMS.MGMD_Start_DateTime)) <= 
	               DATEADD(DD, 1, DATEDIFF(DD, 0, @EndDate))
	           AND (@Company = 0 OR C.Company_ID = @Company)
	           AND (@SubCompany = 0 OR SC.Sub_Company_ID = @SubCompany)
	           AND 
				  (
					 S.Site_ID =  BP.Site_ID 
					AND S.site_id IN (SELECT DATA
					FROM   dbo.fnSplit (@SiteIDList,','))
				   )
	           AND ((@Zone <> 0 AND Z.Zone_ID = @Zone) OR @Zone = 0) 
	               --  AND tI.Bar_Position_ID IN (SELECT Bar_Position_ID FROM BAR_POSITION WHERE (@SITE <> 0 AND Site_ID = @SITE)
	               --  OR
	               --  (
	               --  @Site = 0 AND Site_ID IN
	               --  (
	               --   SELECT Site_ID FROM Site
	               --   WHERE
	               --   (
	               --    (@SubCompany <> 0 AND Sub_Company_ID = @SubCompany)
	               --   OR
	               --    (@SubCompany = 0 AND Sub_Company_ID IN (SELECT Sub_Company_ID FROM Sub_Company WHERE Company_ID = @Company))
	               --   )
	               --  )
	               --   ))
	    GROUP BY
	           C.Company_ID,
	           C.Company_Name,
	           SC.Sub_Company_ID,
	           SC.Sub_Company_Name,
	           S.Site_ID,
	           S.Site_Name,
	           Z.Zone_ID,
	           Z.Zone_Name,
	           BP.Bar_Position_Name,
	           tI.Installation_ID,
	           tI.Installation_Start_Date,
	           tI.Installation_End_Date,
	           CASE 
	                WHEN (tMI.MGMD_Denom_Value = 0) THEN CAST((tI.Installation_Price_Per_Play) / 100.0 AS FLOAT)
	                ELSE CAST(tMI.MGMD_Denom_Value / 100.0 AS FLOAT)
	           END,
	           Machine_Type_Code,
	           tPT.PT_Description,
	           tPT.Payout,
	           Game_Category_Name,
	           Machine_Type_Code,
	           tGT.Game_Title,
	           tM.Machine_Stock_No,
	           tI.Installation_Start_Date
	    HAVING SUM(
	               CAST(
	                   (tMS.MGMD_Coins_IN * tI.Installation_Price_Per_Play) / 
	                   100.0 
	                   AS FLOAT
	               )
	           ) > 0         
	    
	    
	    SELECT Installation_Start_Date,
	           Installation_End_Date,
	           Denom,
	           CASE SUM(
	                    DATEDIFF(DAY, Installation_Start_Date, Installation_End_Date)
	                )
	                WHEN 0 THEN 1
	                ELSE SUM(
	                         DATEDIFF(DAY, Installation_Start_Date, Installation_End_Date)
	                     )
	           END AS DaysOnline,
	           COALESCE(Zone_Name, '[NOT SET]') AS Zone_Name,
	           Site_ID,
	           Site_Name,
	           Sub_Company_ID,
	           Sub_Company_Name,
	           Company_ID,
	           Company_Name,
	           AssetNo,
	           Bar_Position_Name,
	           GameType,
	           GameName,
	           RTP,
	           ISNULL(SUM(CoinIN), 0) AS CoinIN,
	           ISNULL(SUM(HandlePulls), 0) AS HandlePulls,
	           ISNULL(SUM(Bets), 0) AS Bets,	--     
	           ISNULL(SUM(Wins), 0) AS Wins,
	           ISNULL(SUM(CoinOut), 0) AS CoinOut, 
	           SUM(Jackpots) AS Jackpots,
               AVG(TheoHoldPercent) AS TheoHoldPercent   
	           INTO 
	           #RAWVALUES
	    FROM   #BASETABLE
	    GROUP BY
	           Company_ID,
	           Company_Name,
	           Sub_Company_ID,
	           Sub_Company_Name,
	           Zone_Name,
	           Site_ID,
	           Site_Name,
	           AssetNo,
	           Bar_Position_Name,
	           GameType,
	           GameName,
	           Denom,
	           RTP,
	           Installation_Start_Date,
	           Installation_End_Date    
	    
	    SELECT Company_Name Company,
			   Sub_Company_Name SubCompany,
			   Site_Name Site,
	           Zone_Name Zone,
	           AssetNo [Asset No],
	           Bar_Position_Name [Position No],
	           GameType [Game Type],
	           GameName [Game Name],
	           Denom,
	           DaysOnline [Days Online],
	           CAST(Bets AS DECIMAL(20, 2)) AS Bet,
	           CAST(CoinOut AS DECIMAL(20, 2)) AS Win,
	           CAST(Wins AS DECIMAL(20, 2)) AS [Meter Result],
	           --Theoretical Win= (Bets * Hold% / 100)    
	           TheoWin = CAST(((Bets * TheoHoldPercent) / 100) AS DECIMAL(20, 2)),
	           HandlePulls AS [Games Played],	--GamesPlayed
	                                      	--  Bets, 
	           [Actual Payout %] = 100- CASE BETS
	                                 WHEN 0 THEN 0
	                                 ELSE CAST(
	                                          (
	                                              (
	                                                  (
	                                                      Bets / DaysOnline -(CoinOut / DaysOnline) 
	                                                  ) / Bets
	                                              ) * 100
	                                          ) AS DECIMAL(20, 2)
	                                      )
	                            END,
	           RTP [Theo Payout %],
	           Variance = CAST(
	               (
	                   (100-(
	                       CASE BETS
	                            WHEN 0 THEN 0
	                            ELSE (
	                                     (
	                                         Bets / DaysOnline -(CoinOut / DaysOnline) 
	                                     ) / Bets
	                                 ) * 100
	                       END)
	                   ) -RTP
	               ) AS DECIMAL(20, 2)
	           ),
	              
	           
	           CAST(Jackpots AS DECIMAL(20, 2)) AS Jackpots,
	           --BetsPerDay=Bets/DaysOnline,    
	           BetPerDay = CAST(
	               CAST((CoinIN / DaysOnline) AS DECIMAL(20, 2)) AS 
	               FLOAT
	           ),
	            WinPerDay = CAST(
	               CAST((CoinOut / DaysOnline) AS DECIMAL(20, 2)) AS 
	               FLOAT
	           ), 
	           [Meter ResultPerDay] = CAST((Wins / DaysOnline)AS DECIMAL(20, 2)),
	           JackpotsPerDay = CAST((Jackpots / DaysOnline) AS DECIMAL(20, 2)),
	           --Actual Win/Day = (bets/day) ? (wins/day)? (JP/day)    
	           ActWinPerDay = CAST(
	               (
	                   Bets / DaysOnline -(CoinOut  / DaysOnline) 
	               )AS DECIMAL(20, 2)
	           ),
	           --Theoretical Win/Day = (Bets * Hold% / 100)/Day    
	           TheoWinPerDay = CAST(
	               (((Bets * TheoHoldPercent) / 100) / DaysOnline) AS DECIMAL(20, 2)
	           ),
	           --GP/Day    
	           GPPerDay = ROUND( HandlePulls / DaysOnline,0),
	           AvgBetsPerGP = CASE HandlePulls
	                               WHEN 0 THEN 0
	                               ELSE CAST((Bets / HandlePulls) AS DECIMAL(20, 2))
	                          END
	           --Theoretical Hold % = Hold % from the Slot File     
	           --CAST(TheoHoldPercent AS DECIMAL(20, 2)) AS TheoHoldPercent
	           --Actual Hold% = (Actual Win/Day) / Bets * 100    
	           
	    FROM   #RAWVALUES
	    WHERE  Bets > 0 AND DaysOnline>0
	     order by Company_Name,
	              Sub_Company_Name,
	             Site_Name,CAST(Bar_Position_Name AS INT)
	END
	ELSE 
	IF @BasedOn = 2
	BEGIN
	    WITH Base_CTE AS (
	        SELECT --CASE
	               --WHEN CAST(tI.Installation_Start_Date AS DATETIME) > CAST(@StartDate AS DATETIME) THEN
	               --  CAST(tI.Installation_Start_Date AS DATETIME)
	               --ELSE @StartDate
	               -- END AS Installation_Start_Date,
	               -- CAST(
	               --  (
	               --   CASE
	               --  WHEN tI.Installation_End_Date IS NULL THEN @EndDate
	               --  WHEN tI.Installation_End_Date > @EndDate THEN @EndDate
	               --  ELSE tI.Installation_End_Date
	               --   END
	               --  )AS DATETIME
	               -- ) AS Installation_End_Date,  
	               
	               
	               CAST(tI.Installation_Price_Per_Play / 100.00 AS FLOAT) AS Denom,
	               tI.Installation_Percentage_Payout AS Payout,
	               COALESCE(Z.Zone_Name, '[NOT SET]') AS Zone_Name,
	               S.Site_ID,
	               S.Site_Name,
	               SC.Sub_Company_ID,
	               SC.Sub_Company_Name,
	               C.Company_ID,
	               C.Company_Name,
	               M.Machine_Stock_No AS AssetNo,
	               BP.Bar_Position_Name AS Bar_Position_Name,
	               MT.Machine_Type_Code AS GameType,
	               isnull(mgm.MultiGameName,MC.Machine_Name) AS GameName,
                SUM(CAST(  
                        (  
                            CAST(R.READ_COINS_IN AS FLOAT) * CAST(tI.Installation_Price_Per_Play AS FLOAT)  
                        ) / 100.0   
                        AS FLOAT  
                    )  ) AS CoinIN,  
	               SUM(CAST(R.READ_GAMES_BET AS FLOAT)) AS HandlePulls,	--Games Played    
	               SUM(
	                   CAST(
	                       (
	                           CAST(R.READ_COINS_IN AS FLOAT) * CAST(tI.Installation_Price_Per_Play AS FLOAT)
	                       ) / 100.0 
	                       AS FLOAT
	                   )
	               ) AS Bets,	--     
	               
	               (
	                   (
	                       SUM(
	                           CAST(
	                               CAST(R.READ_COINS_IN AS FLOAT) * CAST(tI.Installation_Price_Per_Play AS FLOAT) 
	                               AS 
	                               FLOAT
	                           )
	                       ) / 100
	                   ) -(
	                       SUM(
	                           CAST(
	                               CAST(R.READ_COINS_OUT AS FLOAT) * CAST(tI.Installation_Price_Per_Play AS FLOAT) 
	                               AS FLOAT
	                           )
	                       ) / 100
	                   )
	               ) AS Wins,
	               
	               (
	                       SUM(
	                           CAST(
	                               CAST(R.READ_COINS_OUT AS FLOAT) * CAST(tI.Installation_Price_Per_Play AS FLOAT) 
	                               AS FLOAT
	                           )
	                       ) / 100
	                   )
	                AS CoinOut,
	               
	               (
	                   SUM(
	                       CAST(
	                           CAST(R.READ_RDC_JACKPOT AS FLOAT) * CAST(tI.Installation_Price_Per_Play AS FLOAT) 
	                           AS FLOAT
	                       )
	                   ) / 100
	               ) AS Jackpots,
	               (100 - tI.Installation_Percentage_Payout) AS TheoHoldPercent,
	               R.ReadDate AS ReadDate,
	               COUNT(DISTINCT M.Machine_Stock_No) AS Qty,
	               --COUNT (DISTINCT (CAST(R.Read_Date AS VARCHAR(20)) + ' ' + CAST(BP.Bar_Position_Name AS VARCHAR(10)) + ' ' + CAST(S.Site_ID AS VARCHAR(20)))) AS ReadDays  
	               COUNT(DISTINCT(CAST(R.ReadDate AS VARCHAR(20)))) AS ReadDays
	        FROM   [Read] R
	               INNER JOIN Installation tI
	                    ON  tI.Installation_ID = R.Installation_ID
	               INNER JOIN Bar_Position BP
	                    ON  tI.Bar_Position_ID = BP.Bar_Position_ID
	               INNER JOIN [Machine] M
	                    ON  M.Machine_ID = tI.Machine_ID
	               LEFT JOIN MultiGameMapping mgm
	                    ON  mgm.MachineID = m.Machine_ID
	               INNER JOIN [Machine_Class] MC
	                    ON  MC.Machine_Class_ID = M.Machine_Class_ID
	               INNER JOIN [Machine_Type] MT
	                    ON  MT.Machine_Type_ID = MC.Machine_Type_ID
	               LEFT JOIN ZONE Z
	                    ON  BP.Zone_ID = Z.Zone_ID
	               INNER JOIN SITE S
	                    ON  S.Site_ID = BP.SITE_ID
	               INNER JOIN Sub_Company SC
	                    ON  SC.Sub_Company_ID = S.Sub_Company_ID
	               INNER JOIN COMPANY C
	                    ON  C.Company_ID = SC.Company_ID
	        WHERE  DATEADD(DD, 0, DATEDIFF(DD, 0, R.ReadDate)) >= DATEADD(DD, 0, DATEDIFF(DD, 0, @StartDate))
	               AND DATEADD(DD, 0, DATEDIFF(DD, 0, R.ReadDate)) <= DATEADD(DD, 0, DATEDIFF(DD, 0, @EndDate))
	               AND (@Company = 0 OR C.Company_ID = @Company)
	               AND (@SubCompany = 0 OR SC.Sub_Company_ID = @SubCompany)
	               AND  
				  (
					 S.Site_ID =  BP.Site_ID 
					AND S.site_id IN (SELECT DATA
					FROM   dbo.fnSplit (@SiteIDList,','))
				   )
	               AND ((@Zone <> 0 AND Z.Zone_ID = @Zone) OR @Zone = 0)
	        GROUP BY
	               C.Company_ID,
	               C.Company_Name,
	               SC.Sub_Company_ID,
	               SC.Sub_Company_Name,
	               S.Site_ID,
	               S.Site_Name,
	               Z.Zone_ID,
	               Z.Zone_Name,
	               BP.Bar_Position_Name,
	               --tI.Installation_ID,
	               --tI.Installation_Start_Date,
	               --tI.Installation_End_Date,  
	               CAST(tI.Installation_Price_Per_Play / 100.00 AS FLOAT),
	               tI.Installation_Percentage_Payout,
	               M.Machine_Stock_No,
	               MT.Machine_Type_Code,
	               isnull(mgm.MultiGameName,MC.Machine_Name),
	               R.ReadDate
	    ), 
	    
	    Raw_CTE AS (
	        SELECT Denom,
	               SUM(ReadDays) AS DaysOnline,
	               COALESCE(Zone_Name, '[NOT SET]') AS Zone_Name,
	               Site_ID,
	               Site_Name,
	               Sub_Company_ID,
	               Sub_Company_Name,
	               Company_ID,
	               Company_Name,
	               AssetNo,
	               Bar_Position_Name,
	               GameType,
	               GameName,
	               Payout,
	               ISNULL(SUM(CoinIN), 0) AS CoinIN,
	               ISNULL(SUM(HandlePulls), 0) AS HandlePulls,
	               ISNULL(SUM(Bets), 0) AS Bets,	--     
	               ISNULL(SUM(Wins), 0) AS Wins,
	               ISNULL(SUM(CoinOut) ,0) AS CoinOut, 
	               ISNULL(SUM(Jackpots), 0) AS Jackpots,
                ISNULL(AVG(TheoHoldPercent), 0) AS TheoHoldPercent  
	        FROM   Base_CTE
	        GROUP BY
	               Company_ID,
	               Company_Name,
	               Sub_Company_ID,
	               Sub_Company_Name,
	               Zone_Name,
	               Site_ID,
	               Site_Name,
	               AssetNo,
	               Bar_Position_Name,
	               GameType,
	               GameName,
	               Denom,
	               Payout--,
	    )
	    
	    SELECT 
	           Company_Name Company,
			   Sub_Company_Name SubCompany,
			   Site_Name Site,
	           Zone_Name Zone,
	           AssetNo [Asset No],
	           Bar_Position_Name [Position No],
	           GameType [Game Type],
	           GameName [Game Name],
	           Denom,
	           DaysOnline [Days Online],
	           CAST(Bets AS DECIMAL(20, 2)) AS Bet,
	           CAST(CoinOut AS DECIMAL(20, 2)) AS Win,
	           CAST(Wins AS DECIMAL(20, 2)) AS [Meter Result],
	            
	           --Theoretical Win= (Bets * Hold% / 100)    
	           TheoWin = CAST(((Bets * TheoHoldPercent) / 100) AS DECIMAL(20, 2)),
	           
	           
	           
	           HandlePulls AS [Games Played],	--GamesPlayed
	                                      	--  Bets, 
	            [Actual Payout %] =100- CASE BETS
	                                 WHEN 0 THEN 0
	                                 ELSE CAST(
	                                          (
	                                              (
	                                                  (
	                                                      Bets / DaysOnline -(Coinout / DaysOnline) 
	                                             
	                                                  ) / Bets
	                                              ) * 100
	                                          ) AS DECIMAL(20, 2)
	                                      )
	                            END,
	           Payout [Theo Payout %],                          	                    
	           Variance = CAST(
	               (
	                   (100-
	                       (CASE BETS
	                            WHEN 0 THEN 0
	                            ELSE (
	                                     (
	                                         Bets / DaysOnline -(Coinout / DaysOnline) 
	                                         
	                                     ) / Bets
	                                 ) * 100
	                       END)
	                   ) - Payout
	               ) AS DECIMAL(20, 2)
	           ),
	  
	           CAST(Jackpots AS DECIMAL(20, 2)) AS Jackpots,
	           --BetsPerDay=Bets/DaysOnline,    
	           BetPerDay = CAST(
                CAST((CoinIN / DaysOnline) AS DECIMAL(20, 2))  AS   
	               DECIMAL(18, 2)
	           ),
	           WinPerDay = CAST(
                CAST((CoinOut / DaysOnline) AS DECIMAL(20, 2))  AS   
	               DECIMAL(18, 2)
	           ),
	           
	           [Meter ResultPerDay] = CAST((Wins / DaysOnline)AS DECIMAL(20, 2)),
	           JackpotsPerDay = CAST((Jackpots / DaysOnline) AS DECIMAL(20, 2)),
	           --Actual Win/Day = (bets/day) ? (wins/day)? (JP/day)    
	           ActWinPerDay = CAST(
	               (
	                   Bets / DaysOnline -(Coinout / DaysOnline)
	               )AS DECIMAL(20, 2)
	           ),
	           --Theoretical Win/Day = (Bets * Hold% / 100)/Day    
	           TheoWinPerDay = CAST(
	               (((Bets * TheoHoldPercent) / 100) / DaysOnline) AS DECIMAL(20, 2)
	           ),
	           --GP/Day    
	           GPPerDay = ROUND( HandlePulls / DaysOnline,0),
	           AvgBetsPerGP = CASE HandlePulls
	                               WHEN 0 THEN 0
	                               ELSE CAST((Bets / HandlePulls) AS DECIMAL(20, 2))
	                          END
	           --Theoretical Hold % = Hold % from the Slot File     
	           --CAST(TheoHoldPercent AS DECIMAL(20, 2)) AS TheoHoldPercent
	           --Actual Hold% = (Actual Win/Day) / Bets * 100    
	          
	    FROM   Raw_CTE 
	           --WHERE  Bets > 0
	    ORDER BY
	         Company_Name,
	          Sub_Company_Name,
	          Site_Name,
	          CAST(Bar_Position_Name AS INT)
	END
END  