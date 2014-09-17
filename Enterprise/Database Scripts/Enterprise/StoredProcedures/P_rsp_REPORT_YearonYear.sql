USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_YearonYear]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_YearonYear]
GO

USE [Enterprise]
GO
/*
DBCC FREEPROCCACHE
DBCC DROPCLEANBUFFERS
exec rsp_REPORT_YearonYear @subcompany=0,@region=0,@area=0,@district=0,@site=0,@startdate='2010-09-17 16:41:00',@enddate='2011-09-17 16:41:00',@Company=4
*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_REPORT_YearonYear]
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@Company INT = 0,
	@SiteIDList VARCHAR(MAX)
AS

	SET NOCOUNT ON
	--DECLARE @startdateprev  char(20)
	--DECLARE @Enddateprev  char(20)       
	
	--Fix for report not showing up when same date in chosen for start and end date.
	SET @startdate = DATEADD(D, 0, DATEDIFF(D, 0, @startdate)) --converts '2012-02-15 13:53:23.000' to '2012-02-15 00:00:00.000'
	SET @enddate = DATEADD(D, 0, DATEDIFF(D, 0, @enddate)) --converts '2012-02-15 13:53:23.000' to '2012-02-15 00:00:00.000'
	
	DECLARE @startdateprev  DATETIME       
	DECLARE @Enddateprev    DATETIME      
	
	
	SET DATEFORMAT dmy 
	--get the previous year for the given start date and end date     
	SET @startdateprev = DATEADD(YEAR, -1, @startdate)        
	SET @Enddateprev = DATEADD(YEAR, -1, @enddate)    
	
	IF @subcompany = 0
	    SET @subcompany = NULL
	
	IF @region = 0
	    SET @region = NULL
	
	IF @area = 0
	    SET @area = NULL
	
	IF @district = 0
	    SET @district = NULL
	
	IF @site = 0
	    SET @site = NULL 
	
	--Create temptable to hold current and previous year data    
	CREATE TABLE #temptable1
	(
		RDCCashInCur          FLOAT,
		RDCCashOutCur         FLOAT,
		RDCCashCur            FLOAT,
		Read_Games_BetCur     FLOAT,
		READ_COINS_DROPCur    FLOAT,
		Site_NameCur          VARCHAR(50),
		Machine_Type_CodeCur  VARCHAR(50),
		Bar_Position_IDCur    INT,
		Machine_Type_IDCur    INT,
		Bar_Position_NameCur  VARCHAR(50),
		Installation_NoCur    INT,
		Zone_NameCur          VARCHAR(50),
		machine_nameCur       VARCHAR(50),
		Read_DateCur          DATETIME,
		Machine_Stock_NoCur   VARCHAR(50),
		RDCCashInPrev         FLOAT,
		RDCCashOutPrev        FLOAT,
		RDCCashPrev           FLOAT,
		Read_Games_BetPrev    FLOAT,
		READ_COINS_DROPPrev   FLOAT,
		Bar_Position_IDPrev   INT,
		Installation_NoPrev   INT,
		Site_NamePrev         VARCHAR(50),
		Read_DatePrev         DATETIME,
		Machine_Stock_NoPrev  VARCHAR(50)
	) 
	--create temp table for previous year data      
	CREATE TABLE #temptable2
	(
		Site_NamePrev          VARCHAR(50),
		Machine_Type_CodePrev  VARCHAR(50),
		Machine_Type_IDPrev    INT,
		Bar_Position_NamePrev  VARCHAR(50),
		Zone_NamePrev          VARCHAR(50),
		machine_namePrev       VARCHAR(50),
		Read_DatePrev          DATETIME,
		RDCCashInPrev          FLOAT,
		RDCCashOutPrev         FLOAT,
		RDCCashPrev            FLOAT,
		Read_Games_BetPrev     INT,
		READ_COINS_DROPPrev    INT,
		Bar_Position_IDPrev    INT,
		Installation_NoPrev    INT,
		Machine_Stock_NoPrev   VARCHAR(50)
	) 
	
	CREATE TABLE  #temptable3 
	(
		Read_No int,
		Installation_No int null,
		Read_Date dateTime null,
		Read_Games_Bet int null,
		READ_COIN_DROP int null,
		HandPay float null,
		TicketsIN float null,
		RDCCashIn float null,
		RDCCashOut float null,
		RDCCash  float null,
		VTP  float null,
		Read_Forced BIT null,
		Hold float null,
		Value float null,
		SuspendedTicketCount float null, 
		Read_Days float null
	) 
	
	CREATE TABLE  #temptable4 
	(
		Read_No int,
		Installation_No int null,
		Read_Date dateTime null,
		Read_Games_Bet int null,
		READ_COIN_DROP int null,
		HandPay float null,
		TicketsIN float null,
		RDCCashIn float null,
		RDCCashOut float null,
		RDCCash  float null,
		VTP  float null,
		Read_Forced BIT null,
		Hold float null,
		Value float null,
		SuspendedTicketCount float null, 
		Read_Days float null
	) 
	
	SELECT i.installation_id,
	i.Bar_Position_ID,
	i.machine_id,
	i.Installation_Price_Per_Play,
	i.Installation_Token_Value,
	Machine_Class.Machine_Class_Sp_Features,
	machine_type_category.Machine_Type_Code,
	machine_type_category.Machine_Type_ID,
	Zone.Zone_Name,
	CASE machine_class.machine_name
	WHEN 'Auto Detected' THEN 'Multi Game'
	ELSE machine_class.machine_name
	END AS machine_name,
	MACHINE.Machine_Stock_No,
	Site.Site_Name,
	Bar_Position.Bar_Position_Name
	
	INTO #InstallationTempTable1
	FROM Installation I
	INNER JOIN dbo.Bar_Position (NOLOCK)
	            ON  I.Bar_Position_ID = Bar_Position.Bar_Position_ID
	       INNER JOIN dbo.machine MACHINE WITH (NOLOCK)
	            ON  I.machine_id = MACHINE.machine_id
	       INNER JOIN dbo.machine_class machine_class WITH (NOLOCK)
	            ON  MACHINE.machine_class_id = machine_class.machine_class_id
	       LEFT OUTER JOIN dbo.machine_type machine_type_category WITH (NOLOCK)
	            ON  MACHINE.machine_category_id = machine_type_category.machine_type_id
	       INNER JOIN dbo.Site (NOLOCK)
	            ON  Bar_Position.Site_ID = SITE.Site_ID
	       LEFT OUTER JOIN dbo.Zone (NOLOCK)
	            ON  Bar_Position.Zone_ID = Zone.Zone_ID
	WHERE (
	           (
	               CAST(i.installation_Start_Date AS DATETIME) <= @EndDate
	               AND (
	                       CAST(i.installation_End_Date AS DATETIME) >= @EndDate
	                       OR i.installation_End_Date IS NULL
	                   )
	           )
	       )
	       AND 
	       (
	               ISNULL(@subcompany, 0) = 0
	               OR SITE.sub_company_id = @subcompany
	           )
	       AND (
	               ISNULL(@region, 0) = 0
	               OR SITE.sub_company_region_id = @region
	           )
	       AND (ISNULL(@area, 0) = 0 OR SITE.sub_company_area_id = @area)
	       AND (
	               ISNULL(@district, 0) = 0
	               OR SITE.sub_company_district_id = @district
	           )
	       --AND (ISNULL(@site, 0) = 0 OR SITE.site_id = @site)
	       AND @SiteIDList IS NOT NULL AND SITE.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	       AND (
	               ISNULL(@COMPANY, 0) = 0
	               OR Sub_Company_ID IN (SELECT Sub_Company_ID
	                                     FROM   Sub_Company
	                                     WHERE  Company_ID = @COMPANY)
	           )
	ORDER BY i.installation_id
	
	INSERT INTO #temptable3
	SELECT             
		            
		Max([Read].Read_ID) AS Read_No,            
		            
		Max(Installation.Installation_ID) AS Installation_No,            
		            
		Max([Read].ReadDate) AS Read_Date,      
		[Read].Read_Games_Bet ,        
		[Read].READ_COIN_DROP ,              
		ISNULL(SUM(dbo.[Read].READ_HANDPAY),0) AS HandPay,             
		            
		ISNULL(SUM(dbo.[Read].READ_TICKET), 0) AS TicketsIN,             
		            
		SUM((CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		             
		 WHEN 12 THEN            
		              
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		            
		 WHEN 20 THEN            
		              
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_IN_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_IN_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_IN_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_IN_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_IN_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_IN_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)) AS RDCCashIn,            
		            
		            
		SUM((CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_OUT_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_OUT_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_OUT_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_OUT_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_OUT_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_OUT_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_OUT_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_OUT_500p,0) AS FLOAT) * 5) +           
		  (CAST(ISNULL(CASH_OUT_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_OUT_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_OUT_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_OUT_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_OUT_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_OUT_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_OUT_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)) AS RDCCashOut,            
		            
		SUM((            
		            
		(CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_IN_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_IN_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_IN_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_IN_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_IN_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_IN_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)            
		            
		-            
		            
		            
		(CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_OUT_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_OUT_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_OUT_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_OUT_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_OUT_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_OUT_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_OUT_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_OUT_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_OUT_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_OUT_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_OUT_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_OUT_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_OUT_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_OUT_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_OUT_100000p,0) AS FLOAT) * 1000)
		              
		  )                
		            
		END)            
		            
		)) AS RDCCash,           
		            
		SUM((CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 WHEN 10 THEN            
		             
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		             
		 WHEN 12 THEN            
		              
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 WHEN 20 THEN            
		              
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(VTP,0) AS FLOAT))            
		            
		              
		  )            
		            
		END)) AS VTP,            
		            
		MAX(CASE Read_Forced            
		            
		 WHEN 0 THEN 0            
		            
		 ELSE 1            
		            
		END) AS Read_Forced,          
		            
		SUM(CASE ISNULL(READ_COINS_IN,0)            
		             
		 WHEN 0 THEN 0            
		            
		 ELSE             
		              
		 100 - ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) / CAST(ISNULL(READ_COINS_IN,0) AS FLOAT))  * 100)            
		            
		END) AS Hold,             
		            
		SUM(ISNULL([Read].READ_TICKET_IN_SUSPENSE_VALUE,0)) AS Value,            
		            
		            
		SUM(ISNULL([Read].Read_Ticket_In_Suspense,0)) AS SuspendedTicketCount,
		
		ISNULL([Read].Read_Days,0) as Read_Days	            
            
FROM ((([Read] WITH (NOLOCK)             
INNER JOIN #InstallationTempTable1 Installation WITH (NOLOCK) ON ReadDate between @StartDate and @EndDate 
AND [Read].Installation_ID = Installation.Installation_ID)))                       
--LEFT JOIN Machine_Type ON Machine.Machine_Category_ID = Machine_Type.Machine_Type_ID)                 
--WHERE
--(
--	               CAST(Installation.installation_Start_Date AS DATETIME) <= @EndDate
--	               AND (
--	                       CAST(Installation.installation_End_Date AS DATETIME) >= @EndDate
--	                       OR Installation.installation_End_Date IS NULL
--	                   )
--	           )
GROUP BY 
[Read].Installation_ID, 
[Read].ReadDate, 
[Read].Previous_Read_Date,      
--dbo.[Read].Week_ID,      
--dbo.[Read].Period_ID,      
[Read].Read_Games_Bet ,        
--Installation.Bar_Position_ID,       
[Read].READ_COIN_DROP  ,      
--Machine_Type.Machine_Type_Code ,        
--Machine_Type.Machine_Type_ID,      
--Machine_Class.Machine_Name,    
--Installation.Installation_Start_Date,      
--Installation.Installation_End_Date,
[Read].Read_Days
	
	
	
	--insert into temp table only current data values      
	INSERT INTO #temptable1
	  (
	    RDCCashInCur,
	    RDCCashOutCur,
	    RDCCashCur,
	    Read_Games_BetCur,
	    READ_COINS_DROPCur,
	    Site_NameCur,
	    Machine_Type_CodeCur,
	    Bar_Position_IDCur,
	    Machine_Type_IDCur,
	    Bar_Position_NameCur,
	    Installation_NoCur,
	    Zone_NameCur,
	    machine_nameCur,
	    Read_DateCur,
	    Machine_Stock_NoCur
	  )
	--select data for current year       
	SELECT TT.RDCCashIn,
	       TT.RDCCashOut,
	       TT.RDCCash,
	       TT.Read_Games_Bet,
	       (TT.READ_COIN_DROP) READ_COIN_DROP,
	       Installation.Site_Name,
	       Installation.Machine_Type_Code,
	       Installation.Bar_Position_ID,
	       Installation.Machine_Type_ID,
	       Installation.Bar_Position_Name,
	       TT.Installation_No,
	       Installation.Zone_Name,
	       Installation.machine_name,
	       TT.Read_Date,
	       Installation.Machine_Stock_No
	FROM   #InstallationTempTable1 Installation WITH (NOLOCK)
	       LEFT JOIN #temptable3 TT(NOLOCK)
	            ON  (
	                    Installation.installation_id = TT.installation_no
	                )
	       
	--SELECT i.installation_id,
	--i.Bar_Position_ID,
	--i.machine_id,
	--i.Installation_Price_Per_Play,
	--i.Installation_Token_Value,
	--Machine_Class.Machine_Class_Sp_Features
	--INTO #temptable6
	--FROM Installation I
	--LEFT JOIN Machine WITH (NOLOCK) ON I.Machine_ID = Machine.Machine_ID            
	--LEFT JOIN Machine_Class WITH (NOLOCK) ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID
	--WHERE (
	--           (
	--               CAST(i.installation_Start_Date AS DATETIME) <= @Enddateprev
	--               AND (
	--                       CAST(i.installation_End_Date AS DATETIME) >= @Enddateprev
	--                       OR i.installation_End_Date IS NULL
	--                   )
	--           )
	--       )
	
	SELECT i.installation_id,
	i.Bar_Position_ID,
	i.machine_id,
	i.Installation_Price_Per_Play,
	i.Installation_Token_Value,
	Machine_Class.Machine_Class_Sp_Features,
	machine_type_category.Machine_Type_Code,
	machine_type_category.Machine_Type_ID,
	Zone.Zone_Name,
	CASE machine_class.machine_name
	WHEN 'Auto Detected' THEN 'Multi Game'
	ELSE machine_class.machine_name
	END AS machine_name,
	MACHINE.Machine_Stock_No,
	Site.Site_Name,
	Bar_Position.Bar_Position_Name
	
	INTO #InstallationTempTable2
	FROM Installation I
	INNER JOIN dbo.Bar_Position (NOLOCK)
	            ON  I.Bar_Position_ID = Bar_Position.Bar_Position_ID
	       INNER JOIN dbo.machine MACHINE WITH (NOLOCK)
	            ON  I.machine_id = MACHINE.machine_id
	       INNER JOIN dbo.machine_class machine_class WITH (NOLOCK)
	            ON  MACHINE.machine_class_id = machine_class.machine_class_id
	       LEFT OUTER JOIN dbo.machine_type machine_type_category WITH (NOLOCK)
	            ON  MACHINE.machine_category_id = machine_type_category.machine_type_id
	       INNER JOIN dbo.Site (NOLOCK)
	            ON  Bar_Position.Site_ID = SITE.Site_ID
	       LEFT OUTER JOIN dbo.Zone (NOLOCK)
	            ON  Bar_Position.Zone_ID = Zone.Zone_ID
	        
	WHERE (
	           (
	               CAST(i.installation_Start_Date AS DATETIME) <= @Enddateprev
	               AND (
	                       CAST(i.installation_End_Date AS DATETIME) >= @Enddateprev
	                       OR i.installation_End_Date IS NULL
	                   )
	           )
	       )
	       AND 
	       (
	               ISNULL(@subcompany, 0) = 0
	               OR SITE.sub_company_id = @subcompany
	           )
	       AND (
	               ISNULL(@region, 0) = 0
	               OR SITE.sub_company_region_id = @region
	           )
	       AND (ISNULL(@area, 0) = 0 OR SITE.sub_company_area_id = @area)
	       AND (
	               ISNULL(@district, 0) = 0
	               OR SITE.sub_company_district_id = @district
	           )
	       --AND (ISNULL(@site, 0) = 0 OR SITE.site_id = @site)
	       AND @SiteIDList IS NOT NULL AND SITE.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	       AND (
	               ISNULL(@COMPANY, 0) = 0
	               OR Sub_Company_ID IN (SELECT Sub_Company_ID
	                                     FROM   Sub_Company
	                                     WHERE  Company_ID = @COMPANY)
	           )
	           ORDER BY i.installation_id
	
	
	
	
	INSERT INTO #temptable4
	SELECT             
		            
		Max([Read].Read_ID) AS Read_No,            
		            
		Max(Installation.Installation_ID) AS Installation_No,            
		            
		Max([Read].ReadDate) AS Read_Date,      
		[Read].Read_Games_Bet ,        
		[Read].READ_COIN_DROP ,              
		ISNULL(SUM(dbo.[Read].READ_HANDPAY),0) AS HandPay,             
		            
		ISNULL(SUM(dbo.[Read].READ_TICKET), 0) AS TicketsIN,             
		            
		SUM((CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		             
		 WHEN 12 THEN            
		              
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		            
		 WHEN 20 THEN            
		              
		  ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		  +            
		  ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)              
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_IN_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_IN_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_IN_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_IN_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_IN_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_IN_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)) AS RDCCashIn,            
		            
		            
		SUM((CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_OUT_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_OUT_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_OUT_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_OUT_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_OUT_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_OUT_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_OUT_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_OUT_500p,0) AS FLOAT) * 5) +           
		  (CAST(ISNULL(CASH_OUT_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_OUT_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_OUT_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_OUT_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_OUT_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_OUT_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_OUT_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)) AS RDCCashOut,            
		            
		SUM((            
		            
		(CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_IN_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_IN_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_IN_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_IN_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_IN_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_IN_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_IN_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_IN_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_IN_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_IN_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_IN_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_IN_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_IN_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_IN_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_IN_100000p,0) AS FLOAT) * 1000)
		              
		  )            
		            
		            
		END)            
		            
		-            
		            
		            
		(CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  (CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100            
		            
		 WHEN 10 THEN            
		             
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		             
		 WHEN 12 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 WHEN 20 THEN            
		              
		  (            
		   ((CAST(ISNULL(READ_COIN_DROP,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		   +            
		   ((CAST(ISNULL(READ_RDC_TRUE_COIN_IN,0) AS FLOAT) * Installation.Installation_Token_Value) / 100)            
		  )            
		            
		  -            
		            
		  (((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100)            
		             
		  -            
		            
		  ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100))            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(CASH_OUT_2p,0) AS FLOAT) / 50) +            
		  (CAST(ISNULL(CASH_OUT_5p,0) AS FLOAT) / 20) +            
		  (CAST(ISNULL(CASH_OUT_10p,0) AS FLOAT) / 10) +            
		  (CAST(ISNULL(CASH_OUT_20p,0) AS FLOAT) / 5) +            
		  (CAST(ISNULL(CASH_OUT_50p,0) AS FLOAT) / 2) +            
		  (CAST(ISNULL(CASH_OUT_100p,0) AS FLOAT))  +            
		  (CAST(ISNULL(CASH_OUT_200p,0) AS FLOAT) * 2) +            
		  (CAST(ISNULL(CASH_OUT_500p,0) AS FLOAT) * 5) +            
		  (CAST(ISNULL(CASH_OUT_1000p,0) AS FLOAT) * 10) +            
		  (CAST(ISNULL(CASH_OUT_2000p,0) AS FLOAT) * 20) +            
		  (CAST(ISNULL(CASH_OUT_5000p,0) AS FLOAT) * 50) +
		  (CAST(ISNULL(CASH_OUT_10000p,0) AS FLOAT) * 100) +
		  (CAST(ISNULL(CASH_OUT_20000p,0) AS FLOAT) * 200) +
		  (CAST(ISNULL(CASH_OUT_50000p,0) AS FLOAT) * 500) +
		  (CAST(ISNULL(CASH_OUT_100000p,0) AS FLOAT) * 1000)
		              
		  )                
		            
		END)            
		            
		)) AS RDCCash,           
		            
		SUM((CASE Installation.Machine_Class_Sp_Features            
		            
		 WHEN 1 THEN            
		            
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 WHEN 10 THEN            
		             
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		             
		 WHEN 12 THEN            
		              
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 WHEN 20 THEN            
		              
		  ((CAST(ISNULL(READ_COINS_IN,0) AS FLOAT) * Installation.Installation_Price_Per_Play) / 100) * 10            
		            
		 ELSE            
		              
		  (            
		              
		  (CAST(ISNULL(VTP,0) AS FLOAT))            
		            
		              
		  )            
		            
		END)) AS VTP,            
		            
		MAX(CASE Read_Forced            
		            
		 WHEN 0 THEN 0            
		            
		 ELSE 1            
		            
		END) AS Read_Forced,          
		            
		SUM(CASE ISNULL(READ_COINS_IN,0)            
		             
		 WHEN 0 THEN 0            
		            
		 ELSE             
		              
		 100 - ((CAST(ISNULL(READ_COINS_OUT,0) AS FLOAT) / CAST(ISNULL(READ_COINS_IN,0) AS FLOAT))  * 100)            
		            
		END) AS Hold,             
		            
		SUM(ISNULL([Read].READ_TICKET_IN_SUSPENSE_VALUE,0)) AS Value,            
		            
		            
		SUM(ISNULL([Read].Read_Ticket_In_Suspense,0)) AS SuspendedTicketCount,
		
		ISNULL([Read].Read_Days,0) as Read_Days	            
            
FROM ((([Read] WITH (NOLOCK)             
INNER JOIN #InstallationTempTable2 Installation WITH (NOLOCK) ON ReadDate between 
@startdateprev and @Enddateprev AND [Read].Installation_ID = Installation.Installation_ID)))                       
--LEFT JOIN Machine_Type ON Machine.Machine_Category_ID = Machine_Type.Machine_Type_ID)                 
--WHERE
--(
--	               CAST(Installation.installation_Start_Date AS DATETIME) <= @EndDate
--	               AND (
--	                       CAST(Installation.installation_End_Date AS DATETIME) >= @EndDate
--	                       OR Installation.installation_End_Date IS NULL
--	                   )
--	           )
GROUP BY 
[Read].Installation_ID, 
[Read].ReadDate, 
[Read].Previous_Read_Date,      
--dbo.[Read].Week_ID,      
--dbo.[Read].Period_ID,      
[Read].Read_Games_Bet ,        
--Installation.Bar_Position_ID,       
[Read].READ_COIN_DROP  ,      
--Machine_Type.Machine_Type_Code ,        
--Machine_Type.Machine_Type_ID,      
--Machine_Class.Machine_Name,    
--Installation.Installation_Start_Date,      
--Installation.Installation_End_Date,
[Read].Read_Days
	
	--INSERT INTO #temptable4
	--EXEC [rsp_ReadYearonYear] @startdateprev,@Enddateprev
	
	--SELECT i.installation_id,
	--i.Bar_Position_ID,
	--i.machine_id
	--INTO #temptable6
	--FROM Installation I
	--WHERE (
	--           (
	--               CAST(i.installation_Start_Date AS DATETIME) <= @Enddateprev
	--               AND (
	--                       CAST(i.installation_End_Date AS DATETIME) >= @Enddateprev
	--                       OR i.installation_End_Date IS NULL
	--                   )
	--           )
	--       )
	
	INSERT INTO #temptable2
	  (
	    Site_NamePrev,
	    Machine_Type_CodePrev,
	    Machine_Type_IDPrev,
	    Bar_Position_NamePrev,
	    Zone_NamePrev,
	    machine_namePrev,
	    Read_DatePrev,
	    RDCCashInPrev,
	    RDCCashOutPrev,
	    RDCCashPrev,
	    Read_Games_BetPrev,
	    READ_COINS_DROPPrev,
	    Bar_Position_IDPrev,
	    Installation_NoPrev,
	    Machine_Stock_NoPrev
	  )
	SELECT i.Site_Name,
	       i.Machine_Type_Code,
	       i.Machine_Type_ID,
	       i.Bar_Position_Name,
	       i.Zone_Name,
			i.machine_name,
	       TT.Read_Date,
	       TT.RDCCashIn,
	       TT.RDCCashOut,
	       TT.RDCCash,
	       TT.Read_Games_Bet,
	       (TT.READ_COIN_DROP) READ_COIN_DROP,
	       i.Bar_Position_ID,
	       TT.Installation_No,
	       i.Machine_Stock_No
	FROM   --dbo.TT_ReadYearonYear  TT (nolock)  
	       #InstallationTempTable2 i WITH (NOLOCK)
	       LEFT JOIN #temptable4 TT(NOLOCK)
	            ON  (
	                    i.installation_id = TT.installation_no
	                )
	--       INNER JOIN dbo.Bar_Position (NOLOCK)
	--            ON  i.Bar_Position_ID = Bar_Position.Bar_Position_ID
	--       INNER JOIN dbo.machine MACHINE WITH (NOLOCK)
	--            ON  i.machine_id = MACHINE.machine_id
	--       INNER JOIN dbo.machine_class machine_class WITH (NOLOCK)
	--            ON  MACHINE.machine_class_id = machine_class.machine_class_id
	--       LEFT OUTER JOIN dbo.machine_type machine_type_category WITH (NOLOCK)
	--            ON  MACHINE.machine_category_id = machine_type_category.machine_type_id
	--       INNER JOIN dbo.Site (NOLOCK)
	--            ON  Bar_Position.Site_ID = SITE.Site_ID
	--       LEFT OUTER JOIN dbo.Zone (NOLOCK)
	--            ON  Bar_Position.Zone_ID = Zone.Zone_ID
	--WHERE  --CONVERT(DATETIME, TT_ReadYearonYear.Read_Date, 101) BETWEEN @startdateprev AND @Enddateprev
	--       --(
	--       --		(CONVERT(DATETIME,i.installation_Start_Date,103)<CONVERT(DATETIME,@Enddateprev,103)
	--       --		AND
	--       --		(CONVERT(DATETIME,i.installation_End_Date,103)>CONVERT(DATETIME,@Enddateprev,103) OR CONVERT(DATETIME,i.installation_End_Date,103) IS NULL))
	--       --		 )
	--       --(
	--       --    (
	--       --        CAST(i.installation_Start_Date AS DATETIME) <= @Enddateprev
	--       --        AND (
	--       --                CAST(i.installation_End_Date AS DATETIME) >= @Enddateprev
	--       --                OR i.installation_End_Date IS NULL
	--       --            )
	--       --    )
	--       --)
	--       --AND
	--        (
	--               ISNULL(@subcompany, 0) = 0
	--               OR SITE.sub_company_id = @subcompany
	--           )
	--       AND (
	--               ISNULL(@region, 0) = 0
	--               OR SITE.sub_company_region_id = @region
	--           )
	--       AND (ISNULL(@area, 0) = 0 OR SITE.sub_company_area_id = @area)
	--       AND (
	--               ISNULL(@district, 0) = 0
	--               OR SITE.sub_company_district_id = @district
	--           )
	--       AND (ISNULL(@site, 0) = 0 OR SITE.site_id = @site)
	--       AND (
	--               ISNULL(@COMPANY, 0) = 0
	--               OR Sub_Company_ID IN (SELECT Sub_Company_ID
	--                                     FROM   Sub_Company
	--                                     WHERE  Company_ID = @COMPANY)
	--           )
	--Update the first temptable previous year values with the second temptable which is having only previous year data      
	UPDATE #temptable1
	SET    #temptable1.RDCCashInPrev = #temptable2.RDCCashInPrev,
	       #temptable1.RDCCashOutPrev = #temptable2.RDCCashOutPrev,
	       #temptable1.RDCCashPrev = #temptable2.RDCCashPrev,
	       #temptable1.Read_Games_BetPrev = #temptable2.Read_Games_BetPrev,
	       #temptable1.READ_COINS_DROPPrev = #temptable2.READ_COINS_DROPPrev,
	       #temptable1.Installation_NoPrev = #temptable2.Installation_NoPrev,
	       #temptable1.Bar_Position_IDPrev = #temptable2.Bar_Position_IDPrev,
	       #temptable1.Site_NamePrev = #temptable2.Site_NamePrev,
	       #temptable1.Read_dateprev = #temptable2.Read_dateprev,
	       #temptable1.Machine_Stock_NoPrev = #temptable2.Machine_Stock_NoPrev
	FROM   #temptable2
	       INNER JOIN #temptable1
	       ON #temptable2.Machine_Stock_NoPrev=#temptable1.Machine_Stock_NoCur
	            --ON  #temptable2.Bar_Position_IDPrev = #temptable1.Bar_Position_IDCur 
	            --    AND #temptable2.Installation_NoPrev=#temptable1.Installation_NoCur
	            AND #temptable2.Read_DatePrev = DATEADD(YEAR, -1, #temptable1.Read_DateCur) 
	
	--Inser into first temptable with previous year values which is not matching data with the current year.      
	INSERT INTO #temptable1
	SELECT DISTINCT 
	       NULL,
	       NULL,
	       NULL,
	       NULL,
	       NULL,
	       T2.Site_NamePrev,
	       T2.Machine_Type_CodePrev,
	       NULL,
	       T2.Machine_Type_IDPrev,
	       T2.Bar_Position_NamePrev,
	       NULL,
	       T2.Zone_NamePrev,
	       T2.machine_namePrev,
	       NULL,
	       NULL,
	       T2.RDCCashInPrev,
	       T2.RDCCashOutPrev,
	       T2.RDCCashPrev,
	       T2.Read_Games_BetPrev,
	       T2.READ_COINS_DROPPrev,
	       T2.Bar_Position_IDPrev,
	       T2.Installation_NoPrev,
	       T2.Site_NamePrev,
	       T2.Read_DatePrev,
	       T2.Machine_Stock_NoPrev
	FROM   #temptable2 T2
	       LEFT JOIN #temptable1 T1
	            ON  T2.Bar_Position_IDPrev = T1.Bar_Position_IDCur 
	                --AND T2.Installation_NoPrev = T1.Installation_NoCur
	            AND T2.Read_DatePrev = T1.Read_DatePrev
	WHERE    
	       T1.Bar_Position_IDCur IS NULL
	       AND T1.Read_DatePrev IS NULL 
	       
	
	--select data from temptable1 for meter period report      
	SELECT ROUND(CurYear.RDCCashInCur, 2) AS RDCCashInCur,
	       ROUND(CurYear.RDCCashOutCur, 2) AS RDCCashOutCur,
	       ROUND(CurYear.RDCCashCur, 2) AS RDCCashCur,
	       CurYear.Read_Games_BetCur,
	       ROUND(CurYear.READ_COINS_DROPCur, 2) AS READ_COINS_DROPCur,
	       CurYear.Site_NameCur,
	       CurYear.Machine_Type_CodeCur AS Machine_Type_Code,
	       CurYear.Bar_Position_IDCur AS Bar_Position_ID,
	       CurYear.Machine_Type_IDCur AS Machine_Type_ID,
	       CurYear.Bar_Position_NameCur AS Bar_Position_Name,
	       CurYear.Installation_NoCur AS Installation_No,
	       CurYear.Zone_NameCur AS Zone_Name,
	       CurYear.machine_nameCur AS machine_name,
	       CurYear.RDCCashInPrev,
	       CurYear.RDCCashOutPrev,
	       CurYear.RDCCashPrev,
	       CurYear.Read_Games_BetPrev,
	       CurYear.READ_COINS_DROPPrev,
	       CurYear.Installation_NoPrev,
	       CurYear.Bar_Position_IDPrev,
	       CurYear.Read_DatePrev,
	       CurYear.Read_DateCur,
	       CurYear.Site_NamePrev,
	       CurYear.Machine_Stock_NoCur,
	       CurYear.Machine_Stock_NoPrev
	FROM   #temptable1 AS CurYear
	ORDER BY
	       CurYear.Site_NameCur ASC,
	       CurYear.Machine_Type_CodeCur ASC,
	       CurYear.Bar_Position_IDCur ASC,
	       CurYear.Machine_Type_IDCur ASC 
	
	--drop the temptables       
	DROP TABLE #temptable1 
	DROP TABLE #temptable2
	DROP TABLE #temptable3 
	DROP TABLE #temptable4
GO

