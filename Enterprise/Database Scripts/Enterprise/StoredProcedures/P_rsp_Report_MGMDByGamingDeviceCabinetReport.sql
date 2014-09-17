USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Report_MGMDByGamingDeviceCabinetReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Report_MGMDByGamingDeviceCabinetReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[rsp_Report_MGMDByGamingDeviceCabinetReport]
	@Company INT =0,
	@SubCompany INT =0,
	@Region INT=0,
	@Area INT=0,
	@District INT=0,
	@Site INT=0,
	@Zone INT=0,
	@GamingDate DATETIME,
	@Period NVARCHAR(50),
	@GROUPBYZONE BIT
AS
BEGIN
	
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
	    SET @subcompany = NULL   
	    
	IF @Region = 0
	    SET @Region = NULL 
	
	IF @Area = 0
	    SET @Area = NULL 
	
	IF @District = 0
	    SET @District = NULL                    
	
	IF @Site = 0
	    SET @site = NULL                      
	
	IF @Zone = 0
	    SET @zone = NULL  
	
	--select @GamingDate='2011-12-02 00:00:00.000'          
	
	DECLARE @DaysOnline   INT 
	
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
	
	
	--Select * from #Periods          
	
	SELECT '' AS DaysOnline,
	       CASE 
	            WHEN CAST(tI.Installation_Start_Date AS DATETIME) > CAST(#periods.start AS DATETIME) THEN 
	                 CAST(tI.Installation_Start_Date AS DATETIME)
	            ELSE #periods.start
	       END AS Installation_Start_Date,
	       CAST(
	           (
	               CASE 
	                    WHEN tI.Installation_End_Date IS NULL THEN #periods.[end]
	                    WHEN tI.Installation_End_Date > #periods.[end] THEN 
	                         #periods.[end]
	                    ELSE tI.Installation_End_Date
	               END
	           )AS DATETIME
	       ) AS Installation_End_Date,
	       CAST(tMI.MGMD_Denom_Value / 100.0 AS FLOAT) AS Denom,
	       --tGT.Game_Title_ID AS GameId,            
	       Manufacturer_Name,
	       z.zone_id AS Zone_Id,
	       COALESCE(Z.Zone_Name, '[NOTSET]') AS Zone_Name,
	       S.Site_ID,
	       S.Site_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       C.Company_ID,
	       C.Company_Name,
	       CAST(tpt.Paytable_ID AS VARCHAR(50)) AS PaytableID,
	       CAST(tpt.PT_Description AS VARCHAR(50)) AS PayoutDescription,
	       tgc.Game_Category_Name AS Game_Category_Name,
	       tM.Machine_Stock_No AS AssetNo,
	       SUM(tMS.MGMD_Games_Bet) AS HandlePulls,
	       SUM(
	           CAST(
	               (tMS.MGMD_Coins_IN * tI.Installation_Price_Per_Play) / 100.0 
	               AS 
	               FLOAT
	           )
	       ) AS Bets,	          
	             
	       
	       SUM(
	           CAST(
	               tMS.MGMD_COINS_OUT * tI.Installation_Price_Per_Play AS FLOAT
	           ) / 100
	       ) AS Wins,
	       SUM(
	           CAST(tMS.MGMD_JACKPOT * tI.Installation_Price_Per_Play AS FLOAT)
	       ) / 100 AS Jackpots,	            
	       (100 - tI.Installation_Percentage_Payout) AS TheoHoldPercent,
	       [Order] = #Periods.ordering,
	       [Period] = #Periods.[Name],
	       Bp.Bar_Position_Name AS PosName 
	       INTO #BASETABLE
	FROM   #Periods,
	       Installation tI WITH(NOLOCK)
	       INNER JOIN Bar_Position BP WITH(NOLOCK)
	            ON  tI.Bar_Position_ID = BP.Bar_Position_ID 	                
	       LEFT JOIN ZONE Z WITH(NOLOCK)
	            ON  BP.Zone_ID = Z.Zone_ID
	       INNER JOIN SITE S WITH(NOLOCK)
	            ON  S.Site_ID = BP.SITE_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN COMPANY C
	            ON  C.Company_ID = SC.Company_ID
	       INNER JOIN MACHINE tM
	            ON  tI.Machine_ID = tM.Machine_ID
	       INNER JOIN Machine_Class MC
	            ON  tM.Machine_Class_ID = MC.Machine_Class_ID
	       INNER JOIN manufacturer
	            ON  manufacturer.Manufacturer_ID = MC.Manufacturer_ID
	       INNER JOIN MGMD_Installation tMI
	            ON  tI.Installation_ID = tMI.MGMD_Installation_ID
	       INNER JOIN Game_Library tGL
	            ON  tMI.MGMD_Game_ID = tGL.MG_Game_ID
	       INNER JOIN PayTable tPT
	            ON  tMI.MGMD_Paytable_ID = tPT.Paytable_ID
	       INNER JOIN Game_Title tGT
	            ON  tGL.MG_Group_ID = tGT.Game_Title_ID
	       INNER JOIN Game_Category tGC
	            ON  tGC.Game_Category_ID = tGT.Game_Category_ID
	       INNER JOIN MGMD_SessionDelta tMS
	            ON  tMI.MGMD_ID = tMS.MGMD_Combination_ID
	WHERE      
	       
	       (
	           DATEADD(DD, 0, DATEDIFF(DD, 0, tMS.Read_Date)) >= DATEADD(DD, 0, DATEDIFF(DD, 0, #Periods.start))
	           AND DATEADD(DD, 0, DATEDIFF(DD, 0, tMS.Read_Date)) <
	               DATEADD(DD, 0, DATEDIFF(DD, 0, #Periods.[end]))
	       )
	       AND tMs.MGMD_COINS_IN > 0 	           
	      AND ISNULL(@Site, S.Site_id) = S.Site_ID
	      AND ISNULL(@Company, C.Company_Id) = C.Company_Id
          AND ISNULL(@SubCompany, S.Sub_Company_Id) =S.Sub_Company_Id
          AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
          AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
          AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID	      
	      --AND ISNULL(@Zone, Z.Zone_id) = Z.Zone_ID 
	       AND (@Zone IS NULL OR Z.Zone_id = @Zone )
	GROUP BY
	       C.Company_ID,
	       C.Company_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       S.Site_ID,
	       S.Site_Name,
	       Z.Zone_ID,
	       Z.Zone_Name,
	       tMI.MGMD_Denom_Value,
	       Manufacturer_Name,
	       tPT.PT_Description,
	       tpt.Paytable_ID,
	       Game_Category_Name,
	       tI.Installation_Start_Date,
	       tI.Installation_End_Date,
	       tI.Installation_Percentage_Payout,
	       tM.Machine_Stock_No,
	       #Periods.ordering,
	       #Periods.[Name],
	       #Periods.start,
	       #Periods.[end],
	       bp.Bar_Position_Name      
     
       
 SELECT Installation_Start_Date,      
        Installation_End_Date,      
        CAST(Denom AS FLOAT) AS Denom,      
        CASE       
             WHEN DATEDIFF(DAY, Installation_Start_Date, Installation_End_Date)       
                  <= 1 THEN 1      
             ELSE DATEDIFF(DAY, Installation_Start_Date, Installation_End_Date)      
        END AS DaysOnline,      
        Manufacturer_Name,      
        Zone_ID,      
        COUNT(assetno) AS SlotCount,      
        COALESCE(Zone_Name, '[NOTSET]') AS Zone_Name,      
        Site_ID,      
        Site_Name,      
        Sub_Company_ID,      
        Sub_Company_Name,      
        Company_ID,      
        Company_Name,      
        PayoutDescription,      
        Game_Category_Name,      
        AssetNo,      
        SUM(HandlePulls) AS handlepulls,      
        SUM(Bets) AS Bets, --           
        SUM(Wins) AS Wins,      
        SUM(Jackpots) AS Jackpots,
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        [Order],      
        [Period],      
        PaytableID,
        PosName            
        INTO       
        #RAWVALUES      
 FROM   #BASETABLE      
 GROUP BY      
        [order],      
        [Period],      
        Company_ID,      
        Company_Name,      
        Sub_Company_ID,      
        Sub_Company_Name,      
        Zone_ID,      
        Zone_Name,      
        Site_ID,      
        Site_Name,      
        AssetNo,      
        Denom,      
        Manufacturer_Name,      
        PayoutDescription,      
        Game_Category_Name,      
        Installation_Start_Date,      
        Installation_End_Date,      
        daysonline,      
        PaytableID ,
        PosName             
       
      
 SELECT Site_ID,      
        Site_Name,      
        Sub_Company_ID,      
        Sub_Company_Name,      
        Company_ID,      
        Company_Name,      
        Zone_ID,      
        Zone_Name,      
        DaysOnline,      
        Slotcount,      
        CAST(Denom AS FLOAT) AS Denom,      
        Manufacturer_Name,      
        PaytableID,      
        PayoutDescription,      
        Game_Category_Name,      
        AssetNo,      
        HandlePulls,
        BetsPerDay = Bets / DaysOnline,      
        WinsPerDay = Wins / DaysOnline,      
        JackpotsPerDay = Jackpots / DaysOnline,      
        --Actual Win/Day = (bets/day) � (wins/day)� (JP/day)          
        ActWinPerDay = Bets / DaysOnline -(Wins / DaysOnline) -(Jackpots / DaysOnline),      
        --Actual Hold% = (Actual Win/Day) / Bets * 100          
        ActHoldPercent = CASE BETS      
                              WHEN 0 THEN 0      
                              ELSE (      
                                       (      
                                           Bets / DaysOnline -(Wins / DaysOnline)       
                                           -(Jackpots / DaysOnline)      
                                       ) / Bets      
                                   ) * 100      
                         END,      
        --Theoretical Hold % = Hold % from the Slot File           
        TheoHoldPercent,      
        --Theoretical Win/Day = (Bets * Hold% / 100)/Day          
        TheoWinPerDay = ((Bets * TheoHoldPercent) / 100) / DaysOnline,      
        AvgBetsPerHP = CASE HandlePulls      
                            WHEN 0 THEN 0      
                            ELSE Bets / HandlePulls      
                       END,      
        [Order],      
        Period ,
        PosName       
        INTO #CALCULATEDTABLE      
 FROM   #RAWVALUES       
       
 DROP TABLE #BASETABLE       
 DROP TABLE #Periods       
       

 --Details Section(1)        
 SELECT 1 AS SortOrder,      
        Site_ID,      
        Site_Name,      
        Sub_Company_ID,      
        Sub_Company_Name,      
        Company_ID,      
        Company_Name,      
        AssetNo,      
        Zone_Name,      
        [Order],      
        [Period],      
        DaysOnline,      
        slotcount,      
        Denom,      
        Manufacturer_Name,      
        PaytableID,      
        PayoutDescription,      
        Game_Category_Name,      
        HandlePulls,      
        BetsPerDay,      
        WinsPerDay,      
        JackpotsPerDay,      
        ActWinPerDay,      
        ActHoldPercent,      
        TheoHoldPercent,      
        TheoWinPerDay,      
        AvgBetsPerHP ,
        PosName       
        INTO #ResultSet      
 FROM   #CALCULATEDTABLE       
       
       
 --Group By Denom(2)          
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     SlotCount,      
     Denom,      
     Manufacturer_Name,      
     paytableId,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP,
     PosName       
   )      
 SELECT 2 AS SortOrder,      
        '',      
        Site_Name,      
        '',      
        Sub_Company_Name,      
        '',      
        Company_Name,      
        COUNT(assetno) AS AssetNo,      
        COALESCE(Zone_Name, 'NOTSET') AS Zone_Name,      
        [Order],      
        [Period],      
        SUM(DaysOnline) AS DaysOnline,      
        SUM(Slotcount) AS SlotCount,      
        Denom,      
       COALESCE(Manufacturer_Name, 'NOTSET') AS Manufacturer_Name,          
        --'',  
        '' AS paytableid,      
        '',      
        '',      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP   ,
        ''    
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        Company_Name,      
        Sub_Company_Name,      
        Site_Name,      
        Zone_Name,    
        Manufacturer_Name,    
        Denom,      
        [order],      
        [Period]--, 
       -- PosName 
       
       
 --Group By Manufacturer(3)          
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     SlotCount,      
     Denom,      
     Manufacturer_Name,      
     PaytableId,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP  ,
     PosName     
   )      
 SELECT 3 AS SortOrder,      
        '',      
        Site_Name,      
        '',      
        Sub_Company_Name,      
        '',      
        Company_Name,      
         COUNT(assetno) AS AssetNo,     
        COALESCE(Zone_Name, 'NOTSET') AS Zone_Name,      
        [Order],      
        [Period],      
         SUM(DaysOnline) AS DaysOnline,        
        SUM(Slotcount) AS SlotCount,      
        '',      
        COALESCE(Manufacturer_Name, 'NOTSET') AS Manufacturer_Name,      
        '' AS paytableid,      
        '',      
        '',      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP  ,
        ''     
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        Company_Name,      
        Sub_Company_Name,      
        Site_Name,      
        Zone_Name,      
        --Denom,      
        Manufacturer_Name,      
        [order],      
        [Period]
       
 ORDER BY      
        [Order]       
          
           IF(@GROUPBYZONE=1)  
  BEGIN  
        --Group By Zone(4)        
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     slotcount,      
     Denom,      
     Manufacturer_Name,      
     PaytableID,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,         
       AvgBetsPerHP  ,
       PosName     
   )      
 SELECT 4 AS SortOrder,      
        '' AS Site_Id,      
        Site_Name,      
        '' AS Sub_Company_Id,      
        Sub_Company_Name,      
        '' AS Company_ID,      
        Company_Name,      
         COUNT(assetno) AS AssetNo,      
        COALESCE(Zone_Name, 'NOTSET') AS Zone_Name,      
        [Order],      
        [Period],      
        SUM(DaysOnline) AS DaysOnline,       
        SUM(Slotcount) AS SlotCount,      
        '' AS denom,      
        '' AS manufacturer_name,      
        '' AS paytableid,      
        '' AS payoutdescription,      
        '' AS Game_Category_Name,      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP  ,
        ''      
        --INTO #ResultSet      
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        Company_Name,      
        Sub_Company_Name,      
        Site_Name,      
        Zone_Name,      
        [order],      
        [Period] 
        
 END    
       
 ----Group By Site(5)           
       
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     slotcount,      
     Denom,      
     Manufacturer_Name,      
     paytableid,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP   ,
     PosName    
   )      
 SELECT 5 AS SortOrder,      
        '',      
        Site_Name,      
        '',      
        Sub_Company_Name,      
        '',      
        Company_Name,      
        COUNT(assetno) AS AssetNo,      
        '' AS Zone_Name,      
        [Order],      
        [Period],      
        SUM(DaysOnline) AS DaysOnline,       
        SUM(Slotcount) AS SlotCount,      
        '',      
        '',      
        '',      
        '',      
        '',      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP ,
        ''      
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        Company_Name,      
        Sub_Company_Name,      
      Site_Name,      
        [order],      
        [Period]     
            
       
 ----Group By Sub Company(6)           
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     Slotcount,      
     Denom,      
     Manufacturer_Name,      
     paytableId,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP    ,
     PosName   
   )      
 SELECT 6 AS SortOrder,      
        '',      
        '',      
        '',      
        Sub_Company_Name,      
        '',      
        Company_Name,      
        COUNT(assetno) AS AssetNo,      
        '',      
        [Order],      
        [Period],      
         SUM(DaysOnline) AS DaysOnline,         
        SUM(Slotcount) AS SlotCount,      
        '',      
        '',      
        '',      
        '',      
        '',      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP   ,
        '' 
           
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        Company_Name,      
        Sub_Company_Name,      
        [order],      
        [Period]    
        
       
 --Group By Company (7)            
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     Slotcount,      
     Denom,      
     Manufacturer_Name,      
     paytableid,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP ,
     PosName 
          
   )      
 SELECT 7 AS SortOrder,      
        '',      
        '',      
        '',      
        '',      
        '',      
        Company_Name,      
         COUNT(assetno) AS AssetNo,      
        '',      
        [Order],      
        [Period],      
         SUM(DaysOnline) AS DaysOnline,         
        SUM(Slotcount) AS SlotCount,      
        '',      
        '',      
        '',      
        '',      
   '',      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP    ,
        ''   
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        Company_Name,      
        [order],      
        [Period] 
          
       
 --Grand Total  (8)          
 INSERT INTO #ResultSet      
   (      
     SortOrder,      
     Site_ID,      
     Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     Slotcount,      
     Denom,      
     Manufacturer_Name,      
     paytableid,      
     PayoutDescription,      
     Game_Category_Name,      
     HandlePulls,      
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP      ,
     PosName 
   )      
 SELECT 8 AS SortOrder,      
        '',      
        '',      
        '',      
        '',      
        '',      
        '',      
         COUNT(assetno) AS AssetNo,      
        '',      
        [Order],      
        [Period],      
         SUM(DaysOnline) AS DaysOnline,         
        SUM(Slotcount) AS SlotCount,      
        '',      
        '',      
        '',      
        '',      
        '',      
        SUM(HandlePulls) AS HandlePulls,      
        AVG(BetsPerDay) AS BetsPerDay,      
        AVG(WinsPerDay) AS WinsPerDay,      
        AVG(JackpotsPerDay) AS JackpotsPerDay,      
        AVG(ActWinPerDay) AS ActWinPerDay,      
        AVG(ActHoldPercent) AS ActHoldPercent,      
        AVG(TheoHoldPercent) AS TheoHoldPercent,      
        AVG(TheoWinPerDay) AS TheoWinPerDay,      
        AVG(AvgBetsPerHP) AS AvgBetsPerHP   ,
        ''    
 FROM   #CALCULATEDTABLE      
 GROUP BY      
        [order],      
        [Period]     
            
       
 SELECT       
  SortOrder,      
     Site_ID,      
  Site_Name,      
     Sub_Company_ID,      
     Sub_Company_Name,      
     Company_ID,      
     Company_Name,      
     AssetNo,      
     Zone_Name,      
     [Order],      
     [Period],      
     DaysOnline,      
     CAST(slotcount AS Varchar(50)) As slotcount,      
     Denom,      
     Manufacturer_Name,      
     paytableid,      
     PayoutDescription,      
     Game_Category_Name,      
  CAST(HandlePulls AS Varchar(50)) As HandlePulls,         
     BetsPerDay,      
     WinsPerDay,      
     JackpotsPerDay,      
     ActWinPerDay,      
     ActHoldPercent,      
     TheoHoldPercent,      
     TheoWinPerDay,      
     AvgBetsPerHP   ,
     PosName    
 FROM   #Resultset     
 --where SortOrder <> 1   
 ORDER BY      
        SORTORDER,      
        [order],      
        [period]  ,
         PosName      
       
         
 DROP TABLE #CALCULATEDTABLE       
END 

Go


