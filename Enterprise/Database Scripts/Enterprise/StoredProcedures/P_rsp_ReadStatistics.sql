USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ReadStatistics]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ReadStatistics]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_ReadStatistics
 
  @StartDate 		datetime,
  @EndDate 		datetime,

  @Company_ID 		int = NULL,
  @Sub_Company_ID 	int = NULL,
  @Site_ID 		int = NULL,
  @Operator_ID		int = NULL,
  @Depot_ID		int = NULL,

  @Type                 int = NULL,	-- m9000, s21 etc
  @Category		int = NULL,	-- sub category
  @Class		int = NULL,	-- Game name

  @GroupBy		varchar(10)

AS

 SET DATEFORMAT DMY
 SET NOCOUNT ON

 DECLARE @ASSET 	varchar(10),
 	 @GAMETITLE	varchar(10)

 SET @ASSET = 'ASSET'
 SET @GAMETITLE = 'GAMETITLE'


  IF @Company_ID = 0 SET @Company_ID = NULL
  IF @Sub_Company_ID = 0 SET @Sub_Company_ID = NULL
  IF @Site_ID = 0 SET @Site_ID = NULL
  IF @Operator_ID = 0 SET @Operator_ID = NULL
  IF @Depot_ID = 0 SET @Depot_ID = NULL
  IF @Type = 0 SET @Type = NULL
  IF @Category = 0 SET @Category = NULL
  IF @Class = 0 SET @Class = NULL
  IF @Company_ID = 0 SET @Company_ID = NULL

 SELECT Installation_ID = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE i.Installation_ID END, 
	I.Installation_Percentage_Payout,  
        Installation_Start_Date = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE Cast(I.Installation_Start_Date AS DATETIME) END,
	Bar_Position_ID = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE BP.Bar_Position_ID END,
	Bar_Position_Name = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE BP.Bar_Position_Name END,
	Zone_Name = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE z.Zone_Name END, 

	MC.Machine_Name, 
	Machine_ID = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE M.Machine_ID END, 
	Machine_Stock_No = CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE M.Machine_Stock_No END,  
	MAN.Manufacturer_Name,  
	Category_Code = CAT.Machine_Type_Code,
	MT.Machine_Type_Code,  

	Bet = SUM(VW_Read.RDCCashIn), 
	Win = SUM(VW_Read.RDCCashOut), 
	NetWin = SUM(VW_Read.RDCCash), 
	ReadDays = COUNT(DISTINCT VW_Read.Read_Date), 
	Read_Games_Bet = SUM(r.Read_Games_Bet),  

	Sub_Company_Name = CASE WHEN @GroupBy = @GAMETITLE AND @Sub_Company_ID IS NULL THEN '' ELSE sc.Sub_Company_Name END, 
	Sub_Company_ID = CASE WHEN @GroupBy = @GAMETITLE AND @Sub_Company_ID IS NULL THEN '' ELSE sc.sub_company_id END,

	Company_Name = CASE WHEN @GroupBy = @GAMETITLE AND @Company_ID IS NULL THEN '' ELSE c.Company_Name END, 
	Company_ID = CASE WHEN @GroupBy = @GAMETITLE AND @Company_ID IS NULL THEN '' ELSE c.company_id END, 

	Site_Name = CASE WHEN @GroupBy = @GAMETITLE AND @Site_ID IS NULL THEN '' ELSE S.Site_Name END, 
	Site_ID = CASE WHEN @GroupBy = @GAMETITLE AND @Site_ID IS NULL THEN '' ELSE s.site_id END, 

        Operator_Name = CASE WHEN @GroupBy = @GAMETITLE AND @Operator_ID IS NULL THEN '' ELSE o.Operator_Name END, 
        Operator_ID = CASE WHEN @GroupBy = @GAMETITLE AND @Operator_ID IS NULL THEN '' ELSE o.Operator_ID END,

        Depot_Name = CASE WHEN @GroupBy = @GAMETITLE AND @Depot_ID IS NULL THEN '' ELSE d.Depot_Name END,
        Depot_ID = CASE WHEN @GroupBy = @GAMETITLE AND @Depot_ID IS NULL THEN '' ELSE d.Depot_ID END,

	Qty = count(distinct i.installation_id),

        OrgBetValue = ( SELECT SUM(Bet_Value/100) 
                     		FROM tbl_Bet_Win_Per_Day 
                      		WHERE tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),

        OrgWinValue = ( SELECT SUM(Win_Value/100) 
                     		FROM tbl_Bet_Win_Per_Day 
                      		WHERE tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),

        CompanyBetValue = ( SELECT SUM(Bet_Value/100) 
                     		FROM tbl_Bet_Win_Per_Day 
                     		JOIN Site tmpS
                       		ON tmpS.Site_ID = tbl_Bet_Win_Per_Day.Site_ID
                     		JOIN Sub_Company tmpSC
                       		ON tmpSC.Sub_Company_ID = tmpS.Sub_Company_ID
                     		JOIN Company tmpC
                       		ON tmpSC.Company_ID = tmpC.Company_ID
                    		WHERE tmpC.Company_ID = c.Company_ID
                      		AND tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),

        CompanyWinValue = ( SELECT SUM(Win_Value/100) 
                     		FROM tbl_Bet_Win_Per_Day 
                     		JOIN Site tmpS
                       		ON tmpS.Site_ID = tbl_Bet_Win_Per_Day.Site_ID
                     		JOIN Sub_Company tmpSC
                       		ON tmpSC.Sub_Company_ID = tmpS.Sub_Company_ID
                     		JOIN Company tmpC
                       		ON tmpSC.Company_ID = tmpC.Company_ID
                    		WHERE tmpC.Company_ID = c.Company_ID 
                      		AND tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),

        SiteBetValue = ( SELECT SUM(Bet_Value/100) 
				FROM tbl_Bet_Win_Per_Day 
				WHERE tbl_Bet_Win_Per_Day.Site_ID = s.Site_ID 
	                      	AND tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),

        SiteWinValue = ( SELECT SUM(Win_Value/100) 
				FROM tbl_Bet_Win_Per_Day 
				WHERE tbl_Bet_Win_Per_Day.Site_ID = s.Site_ID 
                      		AND tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),
	OperatorBetValue = 0,
	OperatorWinValue = 0

/**

        OperatorBetValue = ( SELECT SUM(Bet_Value/100) 
                     		FROM tbl_Bet_Win_Per_Day 
				JOIN bar_position tmpBS
				ON tmpBS.Site_ID = tbl_Bet_Win_Per_Day.Site_ID                 		
				JOIN Installation tmpI
				ON tmpBS.Bar_Position_ID = tmpI.bar_Position_ID
				JOIN Machine tmpM
				ON tmpI.Machine_ID = tmpM.Machine_ID
				WHERE tmpM.Operator_ID = o.Operator_ID
                      		AND tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate),

        OperatorWinValue = ( SELECT SUM(Win_Value/100) 
                     		FROM tbl_Bet_Win_Per_Day 
				JOIN bar_position tmpBS
				ON tmpBS.Site_ID = tbl_Bet_Win_Per_Day.Site_ID                 		
				JOIN Installation tmpI
				ON tmpBS.Bar_Position_ID = tmpI.bar_Position_ID
				JOIN Machine tmpM
				ON tmpI.Machine_ID = tmpM.Machine_ID
				WHERE tmpM.Operator_ID = o.Operator_ID
                      		AND tbl_Bet_Win_Per_Day.Read_Date BETWEEN @Startdate AND @EndDate)
**/

  INTO #tmpPreCollated

  FROM  VW_Read  
  JOIN [Read] r ON VW_Read.Read_No = r.Read_ID  
  JOIN Installation I ON VW_Read.Installation_no = I.Installation_ID  
  JOIN Machine M ON I.Machine_ID = M.Machine_ID  
  JOIN Machine_class MC ON M.Machine_Class_ID = MC.Machine_Class_ID  
  JOIN Machine_Type MT ON MC.Machine_Type_ID = MT.Machine_Type_ID  
  LEFT JOIN Machine_Type CAT ON M.Machine_Category_ID = CAT.Machine_Type_ID  
  JOIN Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID  
  JOIN Site S ON BP.Site_ID = S.Site_ID  
  JOIN Sub_Company sc ON sc.Sub_Company_ID = S.Sub_company_ID  
  JOIN Company c ON c.Company_ID = sc.company_ID  
  JOIN Operator o ON o.Operator_ID = m.Operator_ID
  JOIN Depot d ON d.Depot_ID = m.Depot_ID
  LEFT JOIN [Zone] Z ON BP.Zone_ID = Z.Zone_ID  
  JOIN Manufacturer MAN ON MC.Manufacturer_ID = MAN.Manufacturer_ID  

 WHERE Cast(VW_Read.Read_Date AS DateTime) BETWEEN @Startdate AND @EndDate

   AND ( ( @Company_ID IS NULL ) OR @Company_ID IS NOT NULL AND c.Company_ID = @Company_ID )
   AND ( ( @Sub_Company_ID IS NULL ) OR @Sub_Company_ID IS NOT NULL AND sc.Sub_Company_ID = @Sub_Company_ID )
   AND ( ( @Site_ID IS NULL ) OR @Site_ID IS NOT NULL AND s.Site_ID = @Site_ID )
   AND ( ( @Operator_ID IS NULL ) OR @Operator_ID IS NOT NULL AND o.Operator_ID = @Operator_ID )
   AND ( ( @Depot_ID IS NULL ) OR @Depot_ID IS NOT NULL AND d.Depot_ID = @Depot_ID )

   AND ( ( @type IS NULL ) OR @type IS NOT NULL AND mt.Machine_Type_ID = @type )
   AND ( ( @Category IS NULL ) OR @Category IS NOT NULL AND CAT.Machine_Type_ID = @Category )
   AND ( ( @Class IS NULL ) OR @Class IS NOT NULL AND mc.machine_class_ID = @Class )

GROUP BY I.Installation_ID, 
	I.Installation_Percentage_Payout,  
	BP.Bar_Position_ID, 
	BP.Bar_Position_Name, 
	Z.Zone_Name, 

	MC.Machine_Name, 
	M.Machine_ID, 
	M.Machine_Stock_No, 
	MAN.Manufacturer_Name,  
	CAT.Machine_Type_Code,
	MT.Machine_Type_Code,  
	sc.Sub_Company_Name, 
	sc.sub_company_id,
	c.Company_Name,
	c.company_id,
	S.Site_Name,  
	s.site_id,
        o.Operator_Name,
        o.Operator_ID,
        d.Depot_Name,
        d.Depot_ID,
        CASE WHEN @GroupBy = @GAMETITLE THEN '' ELSE Cast(I.Installation_Start_Date AS DATETIME) END

-- collate all info ..
SELECT Installation_Start_Date,
   	Installation_ID, 
	Bar_Position_ID, 
	Bar_Position_Name, 
	Zone_Name, 

	Machine_Name, 
	Machine_ID, 
	Machine_Stock_No, 
	Manufacturer_Name,  
	Category_Code,
	Machine_Type_Code,  

	Bet = SUM(Bet), 
	Win = SUM(Win),
	NetWin= SUM(NetWin),
	ReadDays = SUM(ReadDays), 
	GamesBet = SUM(Read_Games_Bet),  
	DaysInPeriod = 0, --( SELECT COUNT(DISTINCT Read_Date) FROM #tmpPreCollated ),
	Sub_Company_Name, 
	sub_company_id,
	Company_Name,
	company_id,
	Site_Name,  
	site_id,
        Operator_Name,
        Operator_ID,
        Depot_Name,
        Depot_ID,

	Qty = SUM(Qty),

	OrgBetValue = MAX(OrgBetValue),
	OrgWinValue = MAX(OrgWinValue),
	OperatorBetValue = MAX(0),
	OperatorWinValue = MAX(0),
        CompanyBetValue = SUM(DISTINCT CompanyBetValue),
        CompanyWinValue = SUM(DISTINCT CompanyWinValue),
        SiteBetValue = SUM(DISTINCT SiteBetValue),
        SiteWinValue = SUM(DISTINCT SiteWinValue),

	-- calculations

	-- payout percentage calculations
	Payout_Perc = AVG(Installation_Percentage_Payout),  

	-- hold percentage calculations
	Hold_Perc = 100 - AVG(Installation_Percentage_Payout)

INTO #tmpCollated

FROM #tmpPreCollated

group by Installation_Start_Date,
	Installation_ID, 
	Bar_Position_ID, 
	Bar_Position_Name, 
	Zone_Name, 

	Machine_Name, 
	Machine_ID, 
	Machine_Stock_No, 
	Manufacturer_Name,  
	Category_Code,
	Machine_Type_Code,

	Sub_Company_Name, 
	sub_company_id,
	Company_Name,
	company_id,
	Site_Name,  
	site_id,
        Operator_Name,
        Operator_ID,
        Depot_Name,
        Depot_ID


-- do calculations
--
SELECT Installation_ID, 
	Bar_Position_ID, 
	Bar_Position_Name, 
	Zone_Name, 

	Machine_Name, 
	Machine_ID, 
	Machine_Stock_No, 
	Manufacturer_Name,  
	Category_Code,
	Machine_Type_Code,  

	Bet, 
	Win,
	NetWin,
	ReadDays, 
	GamesBet,  
        DaysInPeriod,

	Sub_Company_Name, 
	sub_company_id,
	Company_Name,
	company_id,
	Site_Name,  
	site_id,
        Operator_Name,
        Operator_ID,
        Depot_Name,
        Depot_ID,

	Qty,

	OrgBetValue = CAST ( OrgBetValue AS DECIMAL(15,2)),
	OrgWinValue = CAST ( OrgWinValue AS DECIMAL(15,2)),
	OperatorBetValue = CAST ( OperatorBetValue AS DECIMAL(15,2)),
	OperatorWinValue = CAST ( OperatorWinValue AS DECIMAL(15,2)),
        CompanyBetValue = CAST ( CompanyBetValue AS DECIMAL(15,2)),
        CompanyWinValue = CAST ( CompanyWinValue AS DECIMAL(15,2)),
        SiteBetValue = CAST ( SiteBetValue AS DECIMAL(15,2)),
        SiteWinValue = CAST ( SiteWinValue AS DECIMAL(15,2)),

	-- calculations
	CasinoWin = CAST ( NetWin AS DECIMAL(10,2)),
	AvgBet = CAST ( Bet / CASE WHEN GamesBet = 0 THEN 1 ELSE GamesBet END AS DECIMAL(10,2)),
	AvgDailyWin = CAST ( NetWin / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END AS DECIMAL(10,2)),

	-- payout percentage calculations
	Payout_Perc,  
	Payout_ActPerc = CAST ( ((Bet - NetWin) 
                           / CASE WHEN Bet = 0 THEN 1 ELSE Bet END ) * 100 as decimal(10,2)),
	Payout_PercVar = cast ( Payout_Perc - ((Bet - NetWin) 
                       / CASE WHEN Bet = 0 THEN 1 ELSE Bet END ) * 100 as decimal(10,2)),

	-- hold percentage calculations
	Hold_Perc,  
	Hold_ActPerc = CAST ( 100 - ( ((Bet - NetWin) / CASE WHEN Bet = 0 THEN 1 ELSE Bet END ) * 100 ) as decimal(10,2)),
	Hold_PercVar = CAST ( ( 100 - (((Bet - NetWin) / CASE WHEN Bet = 0 THEN 1 ELSE Bet END )* 100 ) ) - ( 100 - Payout_Perc ) as decimal(10,2)),

	Net_Win_Day = CAST ( NetWin / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END AS DECIMAL(10,2)),

	Theo_Net_Win = CAST ( Bet * ( Payout_Perc / 100 ) AS DECIMAL(10,2) ),

	Theo_Net_Win_Day = CAST ( ( Bet / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END )
                                  * 
                                  ( Payout_Perc / 100 ) 
                                AS DECIMAL(10,2) ),

	test = ( SELECT SUM(Qty) FROM #tmpCollated ),

	Org_Index_Handle = ( OrgBetValue / ( SELECT SUM(Qty) FROM #tmpCollated ) ) / ( CASE WHEN Bet = 0 THEN 1 ELSE (Bet/qty) END ), 
	Org_Index_NetWin = ( (OrgBetValue-OrgWinValue) / ( SELECT SUM(Qty) FROM #tmpCollated ) ) / ( CASE WHEN NetWin = 0 THEN 1 ELSE (NetWin/QTY) END ),

	Org_Index_TheoNetWin = ( ( OrgBetValue * ( ( SELECT AVG(Payout_Perc) FROM #tmpCollated ) / 100 ) ) / ( SELECT SUM(Qty) FROM #tmpCollated ) ) 
                                        / ( CASE WHEN Bet = 0 THEN 1 ELSE Bet/qty END * ( Payout_Perc / 100 ) ),

	Company_Index_Handle = ( CompanyBetValue / ( SELECT SUM(Qty) FROM #tmpCollated tc WHERE tc.Company_id = #tmpCollated.Company_id ) ) / CASE WHEN Bet = 0 THEN 1 ELSE Bet END, 
	Company_Index_NetWin = ( (CompanyBetValue-CompanyWinValue) / ( SELECT SUM(Qty) FROM #tmpCollated tc WHERE tc.Company_id = #tmpCollated.Company_id ) ) / CASE WHEN NetWin = 0 THEN 1 ELSE NetWin END,

	Company_Index_TheoNetWin = ( CompanyBetValue * ( ( SELECT AVG(Payout_Perc) FROM #tmpCollated tc WHERE tc.Company_id = #tmpCollated.Company_id ) / 100 )
                                        / ( SELECT SUM(Qty) FROM #tmpCollated tc WHERE tc.Company_id = #tmpCollated.Company_id ) ) 
                                        / ( CASE WHEN Bet = 0 THEN 1 ELSE Bet END * ( Payout_Perc / 100 ) ),

	Site_Index_Handle = ( SiteBetValue / ( SELECT SUM(Qty) FROM #tmpCollated tc WHERE tc.Site_id = #tmpCollated.site_id ) ) / CASE WHEN Bet = 0 THEN 1 ELSE Bet END,
	Site_Index_NetWin = ( (SiteBetValue-SiteWinValue) / ( SELECT SUM(Qty) FROM #tmpCollated tc WHERE tc.Site_id = #tmpCollated.site_id ) ) / CASE WHEN NetWin = 0 THEN 1 ELSE NetWin END ,

	Site_Index_TheoNetWin = ( SiteBetValue * ( ( SELECT AVG(Payout_Perc) FROM #tmpCollated tc WHERE tc.Site_id = #tmpCollated.Site_id ) / 100 )
                                        / ( SELECT SUM(Qty) FROM #tmpCollated tc WHERE tc.Site_id = #tmpCollated.Site_id ) ) 
                                        / ( CASE WHEN Bet = 0 THEN 1 ELSE Bet END * ( Payout_Perc / 100 ) )

FROM #tmpCollated

ORDER BY Site_Name, 
	Machine_Type_Code, 
	Machine_Stock_No, 
        Installation_Start_Date, 
	Installation_ID 


GO

