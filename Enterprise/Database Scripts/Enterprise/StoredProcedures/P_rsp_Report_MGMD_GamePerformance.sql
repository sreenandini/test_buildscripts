USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Report_MGMD_GamePerformance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Report_MGMD_GamePerformance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Inputs:      See inputs                     
--                    
-- Outputs:                             
-- rsp_Report_MGMD_GamePerformance 2,0,0, 0,   '2011-12-10 12:44:02.340', '2011-12-17 13:15:30.810'                                 
-- =======================================================================                    
--                     
-- Revision History                    
--                    
-- Anuradha             16/07/2010  Created         
-- Jisha Lenu George    08/10/2010  Updated the calculation for WinLoss 
-- Jisha Lenu George	13/12/2010	Updated AverageBet    
-- Yoganandh P			22/01/2011	Updated Net Win, Average Win Formula
-- Anil					23/02/2011	Changed Net WIN = (Bet - Win - Jackpot) 
-- Anil					01/03/2011	Changed Average Win   =  (Net Win / Paytable Count), Average Net % =  (Net Win / Total Bet) * 100 
-- Anil					11/03/2011	Changed Net WIN = (Bet - Win - Jackpot)

------------------------------------------------------------------------------------------------------                    
CREATE PROCEDURE rsp_Report_MGMD_GamePerformance                         
@Company INT =0,                                
@SubCompany INT =0,   
@Region INT =0,
@Area INT =0,
@District INT =0,                             
@Site INT =0,             
@Zone  INT =0,                            
@StartDate  DATETIME,                                
@EndDate  DATETIME,
@SiteIDList VARCHAR(MAX)         
                                
AS                                                               
                              
DECLARE @iDaysOnline INT        
                                 
SELECT @iDaysOnline = DATEDIFF(DAY, @StartDate, @EndDate) + 1            
            
DECLARE @isAFTCalculationEnabled BIT            
SELECT @isAFTCalculationEnabled = Setting_Value FROM Setting WHERE Setting_Name = 'IsAFTIncludedInCalculation'

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

IF @zone = 0
    SET @zone = NULL

                              
DECLARE @GamePerf  TABLE 
        (
            Denom FLOAT,
            GameName VARCHAR(100),
            Paytable_Description VARCHAR(500),
            PaytableCount INT,
            TotalBet FLOAT,
            AverageBet FLOAT,
            NetWin FLOAT,
            TheoWin DECIMAL(10, 2),
            AverageWin FLOAT,
            Installation_Percentage_Payout FLOAT,
            ZoneName VARCHAR(50),
            Bar_Position_Name VARCHAR(50)
        )                              

DECLARE @PayTable  TABLE 
        (
            Denom FLOAT,
            GameName VARCHAR(100),
            Paytable_Description VARCHAR(500),
            PaytableCount INT,
            Installation_Percentage_Payout FLOAT,
            Paytable_ID INT,
            Bar_Position_Name VARCHAR(50)
        );            

WITH PaytableCTE(
                    Denom,
                    GameName,
                    Paytable_Description,
                    PaytableId,
                    Installation_Percentage_Payout,
                    Bar_Position_Name
                )             
AS 
(
    SELECT CAST(tMI.MGMD_Denom_Value AS FLOAT) / 100,
           tGT.Game_Title,
           tPT.PT_Description,
           tPT.Paytable_ID,
           tI.Installation_Percentage_Payout,
           Bar_Position.Bar_Position_Name
    FROM   Installation tI WITH (NOLOCK)
           INNER JOIN MGMD_Installation tMI WITH (NOLOCK)
                ON  tI.Installation_ID = tMI.MGMD_Installation_ID
           INNER JOIN Game_Library tGL WITH (NOLOCK)
                ON  tMI.MGMD_Game_ID = tGL.MG_Game_ID
           INNER JOIN PayTable tPT WITH (NOLOCK)
                ON  tMI.MGMD_Paytable_ID = tPT.Paytable_ID
           INNER JOIN Game_Title tGT WITH (NOLOCK)
                ON  tGL.MG_Group_ID = tGT.Game_Title_ID
           INNER JOIN MGMD_SessionDelta tMS WITH (NOLOCK)
                ON  tMI.MGMD_ID = tMS.MGMD_Combination_ID
           INNER JOIN Bar_Position WITH (NOLOCK)
                ON  Bar_Position.Bar_Position_ID = tI.Bar_Position_ID
           LEFT JOIN Zone Z WITH (NOLOCK)
                ON  Bar_Position.Zone_Id = Z.Zone_ID
           INNER JOIN [Site] S
                ON  Bar_Position.Site_ID = S.Site_ID
           INNER JOIN Sub_Company SC
                ON  S.Sub_Company_ID = SC.Sub_Company_ID
           INNER JOIN Company C
                ON  SC.Company_ID = C.Company_ID
    WHERE  (
               tMS.MGMD_Start_DateTime >= @StartDate
               AND tMS.MGMD_End_DateTime <= @EndDate
           )
           AND (
                   ISNULL(@Site, S.Site_id) = S.Site_ID 
                   AND (
                           @SiteIDList IS NOT NULL
                           AND S.Site_ID IN (SELECT DATA
                                             FROM   fnSplit(@SiteIDList, ','))
                       )
                   AND (@Company IS NULL OR (ISNULL(@Company,0) <> 0 AND C.Company_ID = @Company))
                   AND (@SubCompany IS NULL OR (ISNULL(@SubCompany,0) <> 0 AND SC.Sub_Company_ID = @SubCompany))
                   AND (@Region IS NULL OR (ISNULL(@Region,0) <> 0 AND S.Sub_Company_Region_ID = @Region))
                   AND (@Area IS NULL OR (ISNULL(@Area,0) <> 0 AND S.Sub_Company_Area_ID = @Area))
                   AND (@District IS NULL OR (ISNULL(@District,0) <> 0 AND S.Sub_Company_District_ID = @District))
                   AND (@Zone IS NULL OR (ISNULL(@Zone,0) <> 0 AND Z.Zone_ID = @Zone))
               )   
         
                 
GROUP BY tMI.MGMD_Denom_Value,tGT.Game_Title,tPT.PT_Description, tPT.Paytable_ID, tI.Installation_Percentage_Payout , CONVERT(VARCHAR(20),MGMD_Start_DateTime,103),Bar_Position.Bar_Position_Name            
)            
INSERT INTO @PayTable  
 SELECT Denom,  
        GameName,  
        Paytable_Description,  
        0 PaytableCount,  
        Installation_Percentage_Payout,  
        PaytableID,
        Bar_Position_Name  
 FROM   PaytableCTE  
 GROUP BY  
        Denom,  
        GameName,  
        Paytable_Description,  
        PaytableId,  
        Installation_Percentage_Payout,
        Bar_Position_Name  
  
 UPDATE PT SET PaytableCount = PtCount   
 FROM @PayTable PT  
 INNER JOIN     
 (SELECT GameName,  
        COUNT('Paytable_ID') PtCount  
 FROM   (  
            SELECT DISTINCT GameName,  
                   Paytable_ID  
            FROM   @PayTable  
        ) X  
 GROUP BY  
        GameName,Paytable_ID) Y  
  ON PT.GameName=Y.GameName
            
INSERT INTO @GamePerf                              
 SELECT              
  CAST(tMI.MGMD_Denom_Value AS FLOAT)/100,             
  CASE WHEN ISNULL(tGT.Game_Title, '') <> '' THEN tGT.Game_Title ELSE '[N/a]' END AS Game_Title,                
  tPT.PT_Description AS PayoutDescription,               
  tPT.Paytable_ID AS PaytableCount,              
  SUM(CAST(tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS FLOAT))/100 AS TotalBet,              
             
  CASE WHEN SUM(tMS.MGMD_GAMES_BET) > 0 THEN              
(SUM(CAST(tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS FLOAT))/100)  ELSE 0 END AS AverageBet,                
(             
(SUM(CAST(tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS FLOAT))/100)-            
(SUM(CAST(tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play AS FLOAT))/100)-            
(SUM(CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT))/100)            
) AS NetWin,            
                
  CAST((100 -tI.Installation_Percentage_Payout) as decimal(10,2))as TheoWin,              
              
                          
                     
CASE 
     WHEN ISNULL(@iDaysOnline, 0) > 0 THEN (
              (
                  SUM(
                      CAST(
                          tMS.MGMD_COINS_IN * tI.Installation_Price_Per_Play AS 
                          FLOAT
                      )
                  ) / 100
              ) -(
                  SUM(
                      CAST(
                          tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play AS 
                          FLOAT
                      )
                  ) / 100
              ) -(
                  SUM(
                      CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
                  ) / 100
              )
          ) / @iDaysOnline
     ELSE 0
END AS AverageWin ,-- Net Win/ No of Day Online (Not in a use now)            
tI.Installation_Percentage_Payout,        
Z.Zone_Name,
Bar_Position.Bar_Position_Name              

FROM Installation tI                  
INNER JOIN MGMD_Installation tMI ON tI.Installation_ID = tMI.MGMD_Installation_ID                    
INNER JOIN Game_Library tGL ON tMI.MGMD_Game_ID = tGL.MG_Game_ID                
INNER JOIN PayTable tPT ON tMI.MGMD_Paytable_ID = tPT.Paytable_ID                
INNER JOIN Game_Title tGT ON tGL.MG_Group_ID = tGT.Game_Title_ID                
INNER JOIN MGMD_SessionDelta tMS ON tMI.MGMD_ID = tMS.MGMD_Combination_ID             
INNER JOIN Bar_Position(NOLOCK)  ON Bar_Position.Bar_Position_ID = tI.Bar_Position_ID          
LEFT JOIN Zone  Z(NOLOCK)  ON Z.Zone_ID = Bar_Position.Zone_ID          
INNER JOIN [Site] S ON Bar_Position.Site_ID = S.Site_ID
INNER JOIN Sub_Company SC ON S.Sub_Company_ID = SC.Sub_Company_ID
INNER JOIN Company C ON SC.Company_ID = C.Company_ID
WHERE(
         tMS.MGMD_Start_DateTime >= @StartDate
         AND tMS.MGMD_End_DateTime <= @EndDate
     )
AND(
       ISNULL(@Site, S.Site_id) = S.Site_ID 
       AND (
               @SiteIDList IS NOT NULL
               AND S.Site_ID IN (SELECT DATA
                                 FROM   fnSplit(@SiteIDList, ','))
           )
       AND (@Company IS NULL OR (ISNULL(@Company,0) <> 0 AND C.Company_ID = @Company))
       AND (@SubCompany IS NULL OR (ISNULL(@SubCompany,0) <> 0 AND SC.Sub_Company_ID = @SubCompany))
       AND (@Region IS NULL OR (ISNULL(@Region,0) <> 0 AND S.Sub_Company_Region_ID = @Region))
       AND (@Area IS NULL OR (ISNULL(@Area,0) <> 0 AND S.Sub_Company_Area_ID = @Area))
       AND (@District IS NULL OR (ISNULL(@District,0) <> 0 AND S.Sub_Company_District_ID = @District))
       AND (@Zone IS NULL OR (ISNULL(@Zone,0) <> 0 AND Z.Zone_ID = @Zone))
	)               
  GROUP BY               
  tMI.MGMD_Denom_Value,tGT.Game_Title, tPT.Paytable_ID, tPT.PT_Description, tI.Installation_Percentage_Payout,Z.Zone_Name,Bar_Position.Bar_Position_Name         
         
SELECT             
 G.Denom,                            
 G.GameName,        
 G.ZoneName,
 G.Bar_Position_Name,                           
 G.Paytable_Description,                            
 SUM(P.PaytableCount) as paytablecount,                                     
 G.TotalBet,                   
 Round(G.AverageBet/ coalesce(SUM(P.PaytableCount),1),2) as AverageBet,                             
 G.NetWin,                         
 G.TheoWin,                    
    ROUND(G.NetWin/SUM(P.PaytableCount),2) as AverageWin,                   
 CASE WHEN Round(G.AverageBet/ coalesce(SUM(P.PaytableCount),1),2) > 0 THEN                  
  (ROUND(G.NetWin/SUM(P.PaytableCount),2) / Round(G.AverageBet/ coalesce(SUM(P.PaytableCount),1),2)) * 100  --(Net Win/Total Bet) * 100            
 ELSE 0               
 END AS AverageNetPerc                         
FROM                   
 @GamePerf G INNER JOIN @PayTable P ON G.Denom = P.Denom AND G.GameName = P.GameName             
 AND G.Paytable_Description = P.Paytable_Description               
 AND G.Bar_Position_Name= P.Bar_Position_Name
WHERE G.TotalBet>0                          
Group by               
     G.Denom,                          
  G.GameName,         
  G.ZoneName,
  G.Bar_Position_Name,                          
  G.Paytable_Description,                            
  G.NetWin,                         
  G.TheoWin,                     
  G.AverageWin,                  
  G.TotalBet,                            
  G.AverageBet 

GO

