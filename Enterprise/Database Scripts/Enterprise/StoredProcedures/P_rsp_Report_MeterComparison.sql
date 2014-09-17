USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_Report_MeterComparison]    Script Date: 05/12/2014 19:25:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_Report_MeterComparison'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_Report_MeterComparison
END
GO

/*-------------------------------------------------------------------------- 
---
--- Description: retrieve information required for SDS_MeterComparison report
---               sp can be called a number of different ways, to either give line by line data, sub total or grand total figures
---
---
	req							read				meter history
	--------------------------------------------------------------------
	bets						read_games_bet				mh_games_bet
	wins						read_games_won				mh_games_won
	coin drop					read_coin_drop				mh_coins_drop
	coins inserted				read_coins_in				mh_coins_in
	coins collected				read_coins_out				mh_coins_out
	attendent paid jp			?							mh_jackpot			** mh_handpay
	attendant paid cc			read_rdc_cancelled_credits	mh_cancelled_credits
	attendant paid prog payout	progressive_win_hp_value	mh_progressive_win_handpay_value
    machine paid prog			progressive_win_value		mh_progressive_win_value
    
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
---
-------------------------------------------------------------------------- 
-- to USE
--
-- EXEC [rsp_Report_MeterComparison] 0,0,0,0,'01 jan 2011','02 jan 2011',''  
-- EXEC [rsp_Report_MeterComparison] @runtype = 1, @startdate='01 dec 2099',@enddate='01 dec 2099',@site=0,@Zone=''
-- EXEC [rsp_Report_MeterComparison] @runtype = 2, @Zone='ER'	-- sub total
-- EXEC [rsp_Report_MeterComparison] @runtype = 2, @site=55	-- sub total
-- EXEC [rsp_Report_MeterComparison] @runtype = 3, @startdate='01 jan 2010',@enddate='02 jan 2010',@site=0,@Zone=''
-----------------------------------------------------------------------------
-- SDS comparable naming conventions
--  Area = zone
--  denom = pop
--  site
--  stand = bar_pos_name
--- slot = asset number
--- =======================================================================
--- 
--- Revision History
--- 
--- C.Taylor  ( Contractor )  27/03/10    Created
---                           30/03/10    not handling start and end meters when spanning multiple days
---                           31/03/10    added a return line if no data returned, report will then show blank
---                           10/04/10    added company/sub company grouping
--- GBabu                 14/12/10    Modified the meter comparison sp for crystal reports
--- Jisha Lenu George		  15/12/10	  Uncommented the where condition
--- GBabu		  15/12/10    Modified the order by 
--- Jisha Lenu George		  20/12/10    Updated the criteria. Fix for #92412  
--- GBabu		17/12/10 Removed the unwanted params
---Melvi Miranda  changed logic to improve performance. Used single cte and reduced number of joins.
--------------------------------------------------------------------------- */
  
CREATE PROCEDURE [dbo].[rsp_Report_MeterComparison]
	@Company INT = 0,
	@Subcompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0, -- set as zero for all
	@Zone INT = 0, -- set as blank for all
	@StartDate DATETIME ,
	@EndDate DATETIME ,
	@GroupByZONE BIT,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	DECLARE @SYSRegion VARCHAR(2)       
	SELECT @SYSRegion = UPPER(RIGHT(System_Parameter_Region_Culture, 2))
	FROM   System_Parameters 
	
	DECLARE @Client VARCHAR(20)
	SELECT @Client = setting_value
	FROM   dbo.Setting
	WHERE  Setting_Name = 'Client'     
	
	DECLARE @calcStartDate  DATETIME,
	        @calcEndDate    DATETIME        
	
	DECLARE @Denom          FLOAT     
	SET @Denom = 0  
	
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
	
	
	SET DATEFORMAT dmy
	
	SET @calcStartDate = CAST(@StartDate AS DATETIME)        
	SET @calcEndDate = CAST(@EndDate AS DATETIME) 
	-- generic information,
	-- get read linked to the normal machine / site level information.       
	
	--start #tblGeneric defn       
	
	--End #tmpGeneric table defn  
	CREATE TABLE #tempGeneric
	(
		Bar_Position_ID                     INT,
		Bar_Position_Name                   VARCHAR(50),
		Machine_Type_ID                     INT,
		Machine_Type_Code                   VARCHAR(50),
		Site_ID                             INT,
		Site_Name                           VARCHAR(50),
		Sub_Company_ID                      INT,
		Sub_Company_Name                    VARCHAR(50),
		Company_ID                          INT,
		Company_Name                        VARCHAR(50),
		Machine_Stock_No                    VARCHAR(50),	-- slot                  
		installdate                         DATETIME,
		Zone_Name                           VARCHAR(50),
		Zone_ID                             INT,	-- area                
		installation_price_per_play         INT,
		Token                               INT,
		installation_id                     INT,
		Read_ID                             INT,
		read_games_bet                      INT,
		read_games_won                      INT,
		READ_RDC_BILL_100                   INT,
		READ_RDC_BILL_50                    INT,
		READ_RDC_BILL_20                    INT,
		READ_RDC_BILL_10                    INT,
		READ_RDC_BILL_5                     INT,
		READ_RDC_BILL_2                     INT,
		READ_RDC_BILL_1                     INT,
		READ_TICKET_IN_SUSPENSE             REAL,
		READ_TICKET                         REAL,
		TICKETS_INSERTED_NONCASHABLE_VALUE  INT,
		TICKETS_PRINTED_NONCASHABLE_VALUE   INT,
		read_coin_drop                      INT,
		READ_RDC_TRUE_COIN_IN               INT,
		READ_RDC_TRUE_COIN_OUT              INT,
		read_coins_in                       INT,
		read_coins_out                      INT,
		READ_RDC_JACKPOT                    INT,
		read_Handpay                        INT,
		progressive_win_handpay_value       INT,
		progressive_win_value               INT,
		Promo_Cashable_EFT_IN               INT,
		Promo_Cashable_EFT_OUT              INT,
		NonCashable_EFT_IN                  INT,
		NonCashable_EFT_OUT                 INT,
		Cashable_EFT_IN                     INT,
		Cashable_EFT_OUT                    INT
	) 
	
	CREATE NONCLUSTERED INDEX _IXtmp ON #tempGeneric(installation_id, Read_id)
	INCLUDE(
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
	    installdate,
	    Zone_Name,
	    Zone_ID,	-- area                    
	    installation_price_per_play,
	    Token,
	    read_games_bet,
	    read_games_won,
	    READ_RDC_BILL_100,
	    READ_RDC_BILL_50,
	    READ_RDC_BILL_20,
	    READ_RDC_BILL_10,
	    READ_RDC_BILL_5,
	    READ_RDC_BILL_2,
	    READ_RDC_BILL_1,
	    READ_TICKET_IN_SUSPENSE,
	    READ_TICKET,
	    TICKETS_INSERTED_NONCASHABLE_VALUE,
	    TICKETS_PRINTED_NONCASHABLE_VALUE,
	    read_coin_drop,
	    READ_RDC_TRUE_COIN_IN,
	    READ_RDC_TRUE_COIN_OUT,
	    read_coins_in,
	    read_coins_out,
	    READ_RDC_JACKPOT,
	    read_Handpay,
	    progressive_win_handpay_value,
	    progressive_win_value,
	    Promo_Cashable_EFT_IN,
	    Promo_Cashable_EFT_OUT,
	    NonCashable_EFT_IN,
	    NonCashable_EFT_OUT,
	    Cashable_EFT_IN,
	    Cashable_EFT_OUT
	)
	
	INSERT INTO #tempGeneric
	SELECT Bar_Position.Bar_Position_ID,
	       Bar_Position.Bar_Position_Name,	-- stand                    
	       Machine_Type_Category.Machine_Type_ID,
	       Machine_Type_Category.Machine_Type_Code,
	       [SITE].Site_ID,
	       [SITE].Site_Name,
	       Sub_Company.Sub_Company_ID,
	       Sub_Company.Sub_Company_Name,
	       Company.Company_ID,
	       Company.Company_Name,
	       MACHINE.Machine_Stock_No,	-- slot                  
	       CONVERT(VARCHAR(15), i.Installation_End_Date, 103) + ' ' + CONVERT(VARCHAR(15), i.Installation_End_Time, 114) AS 
	       installdate,
	       Zone.Zone_Name,
	       Zone.Zone_ID,	-- area                    
	       i.installation_price_per_play,
	       i.Installation_Token_Value AS Token,
	       i.installation_id,
	       r.Read_ID,
	       r.read_games_bet,
	       r.read_games_won,
	       r.READ_RDC_BILL_100,
	       r.READ_RDC_BILL_50,
	       r.READ_RDC_BILL_20,
	       r.READ_RDC_BILL_10,
	       r.READ_RDC_BILL_5,
	       r.READ_RDC_BILL_2,
	       r.READ_RDC_BILL_1,
	       r.READ_TICKET_IN_SUSPENSE,
	       r.READ_TICKET,
	       r.TICKETS_INSERTED_NONCASHABLE_VALUE,
	       r.TICKETS_PRINTED_NONCASHABLE_VALUE,
	       r.read_coin_drop,
	       r.READ_RDC_TRUE_COIN_IN,
	       r.READ_RDC_TRUE_COIN_OUT,
	       r.read_coins_in,
	       r.read_coins_out,
	       r.READ_RDC_JACKPOT,
	       r.read_Handpay,
	       r.progressive_win_handpay_value,
	       r.progressive_win_value,
	       r.Promo_Cashable_EFT_IN,
	       r.Promo_Cashable_EFT_OUT,
	       r.NonCashable_EFT_IN,
	       r.NonCashable_EFT_OUT,
	       r.Cashable_EFT_IN,
	       r.Cashable_EFT_OUT 
	       --	INTO #tmpGeneric
	FROM   dbo.Installation i WITH (NOLOCK)
	       JOIN dbo.Bar_Position Bar_Position WITH (NOLOCK)
	            ON  i.Bar_Position_ID = Bar_Position.Bar_Position_ID
	       LEFT JOIN dbo.Zone Zone WITH (NOLOCK)
	            ON  bar_position.Zone_ID = Zone.Zone_ID
	       JOIN MACHINE WITH (NOLOCK)
	            ON  i.Machine_ID = MACHINE.Machine_ID
	       JOIN [SITE] WITH (NOLOCK)
	            ON  Bar_Position.Site_ID = [SITE].Site_ID
	       JOIN Sub_Company WITH (NOLOCK)
	            ON  Sub_Company.Sub_Company_ID = [SITE].Sub_Company_ID
	       JOIN Company WITH (NOLOCK)
	            ON  Company.Company_ID = Sub_Company.Company_ID
	       LEFT JOIN dbo.Machine_Type AS Machine_Type_Category WITH (NOLOCK)
	            ON  MACHINE.Machine_Category_ID = Machine_Type_Category.Machine_Type_ID
	       INNER JOIN [READ] r WITH(NOLOCK)
	            ON  (i.installation_id = r.installation_id)
	WHERE  r.ReadDate BETWEEN @calcStartDate AND @calcEndDate
	       AND installation_price_per_play <> 0 
	
	---------------------------------------------------------Tempgeneric--------------------  
	
	---new code  
	
	SELECT g.read_id,
	       g.installation_id,
	       meter.mh_type,
	       MH_ID,
	       mh_datetime 
	       -- Bets    
	       ,
	       meter.mh_games_bet,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.read_games_bet
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) read_games_bet 
	       -- Wins    
	       ,
	       meter.mh_games_won,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.read_games_won
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) read_games_won 
	       -- Bill 100    
	       ,
	       meter.MH_BILL_100,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_100
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_100 
	       -- Bill 50    
	       ,
	       meter.MH_BILL_50,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_50
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_50 
	       --Bill 20    
	       ,
	       meter.MH_BILL_20,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_20
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_20 
	       --Bill 10    
	       ,
	       meter.MH_BILL_10,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_10
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_10 
	       --Bill 5    
	       ,
	       meter.MH_BILL_5,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_5
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_5 
	       --Bill 2    
	       ,
	       meter.MH_BILL_2,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_2
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_2 
	       --Bill 1    
	       ,
	       meter.MH_BILL_1,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_BILL_1
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_BILL_1 
	       --Cashable Voucher In    
	       ,
	       meter.mh_ticket_inserted_value,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_TICKET_IN_SUSPENSE
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_TICKET_IN_SUSPENSE 
	       --Cashable Voucher Out    
	       ,
	       meter.mh_ticket_printed_value,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_TICKET
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_TICKET 
	       --Non Cashable Voucher In    
	       ,
	       meter.MH_TICKETS_INSERTED_NONCASHABLE_VALUE,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.TICKETS_INSERTED_NONCASHABLE_VALUE
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) 
	       TICKETS_INSERTED_NONCASHABLE_VALUE 
	       --Non Cashable Voucher Out    
	       ,
	       meter.MH_TICKETS_PRINTED_NONCASHABLE_VALUE,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.TICKETS_PRINTED_NONCASHABLE_VALUE
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) 
	       TICKETS_PRINTED_NONCASHABLE_VALUE 
	       --coin drop    
	       ,
	       meter.mh_coin_drop,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.read_coin_drop
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) read_coin_drop,
	       g.Installation_Price_Per_Play 
	       --TRUE_COIN_IN    
	       ,
	       meter.mh_true_coin_in,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_TRUE_COIN_IN
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_TRUE_COIN_IN,
	       g.Token 
	       --TRUE_COIN_OUT    
	       ,
	       meter.mh_true_coin_out,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_TRUE_COIN_OUT
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_TRUE_COIN_OUT 
	       --COIN_INSERTED    
	       ,
	       meter.mh_coins_in,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.read_coins_in
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) read_coins_in 
	       --COIN_COLLECTED    
	       ,
	       meter.mh_coins_out,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.read_coins_out
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) read_coins_out,
	       --g.READ_RDC_JACKPOT cc_READ_RDC_JACKPOT    
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_JACKPOT
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) cc_READ_RDC_JACKPOT 
	       --JACKPOT    
	       ,
	       meter.mh_jackpot,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.READ_RDC_JACKPOT
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) READ_RDC_JACKPOT 
	       --ATTND_PAID_CC    
	       ,
	       meter.mh_Handpay,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.read_Handpay
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) read_Handpay 
	       --ATTND_PAID_PROG_PAYOUT    
	       ,
	       meter.mh_progressive_win_handpay_value,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.progressive_win_handpay_value
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) progressive_win_handpay_value 
	       --MACHINE_PAID_PROG_PAYOUT    
	       ,
	       meter.mh_progressive_win_value,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.progressive_win_value
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) progressive_win_value 
	       --PromoCashableEFTIN    
	       ,
	       meter.MH_Promo_Cashable_EFT_IN,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.Promo_Cashable_EFT_IN
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) Promo_Cashable_EFT_IN 
	       --PromoCashableEFTOut    
	       ,
	       meter.MH_Promo_Cashable_EFT_OUT,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.Promo_Cashable_EFT_OUT
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) Promo_Cashable_EFT_OUT 
	       --NonCashableEFTIN    
	       ,
	       meter.MH_NonCashable_EFT_IN,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.NonCashable_EFT_IN
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) NonCashable_EFT_IN 
	       --NonCashableEFTOUT    
	       ,
	       meter.MH_NonCashable_EFT_OUT,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.NonCashable_EFT_OUT
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) NonCashable_EFT_OUT 
	       --CashableEFTIN    
	       ,
	       meter.MH_Cashable_EFT_IN,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.Cashable_EFT_IN
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) Cashable_EFT_IN 
	       --CashableEFTOUT    
	       ,
	       meter.MH_Cashable_EFT_OUT,
	       SUM(
	           CASE 
	                WHEN meter.mh_type = 'P' THEN g.Cashable_EFT_OUT
	                ELSE 0
	           END
	       ) OVER(PARTITION BY installation_id) Cashable_EFT_OUT 
	       --    
	       ,
	       ROW_NUMBER() OVER(PARTITION BY installation_id ORDER BY mh_datetime ASC) 
	       asc_rnum,
	       ROW_NUMBER() OVER(PARTITION BY installation_id ORDER BY mh_datetime DESC) 
	       desc_rnum
	       INTO #tmpMH
	FROM   #tempGeneric g
	       JOIN meter_history meter
	            ON  g.installation_id = meter.Mh_Installation_No
	            AND g.Read_id = meter.mh_linkreference
	            AND meter.mh_process = 'READ'
	            AND meter.mh_type IN ('P', 'C')
	
	--WHERE installation_id in (2,11)  
	
	SELECT t2.read_id,
	       t1.installation_id 
	       -- Bets  
	       ,
	       t1.mh_games_bet start_mh_games_bet,
	       t2.mh_games_bet end_mh_games_bet,
	       Bets_Meter_Delta = t1.read_games_bet --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'Mh_Games_Bet')  
	       ,
	       Bets_BMC_Value = t1.read_games_bet 
	       -- Wins  
	       ,
	       t1.mh_games_won start_mh_games_won,
	       t2.mh_games_won end_mh_games_won,
	       Win_Meter_Delta = t1.read_games_won --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_games_won')  
	       ,
	       Win_BMC_Value = t1.read_games_won 
	       -- Bill 100   
	       ,
	       t1.MH_BILL_100 start_MH_BILL_100,
	       t2.MH_BILL_100 end_MH_BILL_100,
	       BILL_100_Meter_Delta = t1.READ_RDC_BILL_100 --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'Mh_Bill_100')  
	       ,
	       BILL_100_BMC_Value = t1.READ_RDC_BILL_100 
	       --Bill 50  
	       ,
	       t1.MH_BILL_50 start_MH_BILL_50,
	       t2.MH_BILL_50 end_MH_BILL_50,
	       BILL_50_Meter_Delta = t1.READ_RDC_BILL_50 -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'MH_BILL_50')  
	       ,
	       BILL_50_BMC_Value = t1.READ_RDC_BILL_50 
	       -- Bill 20  
	       ,
	       t1.MH_BILL_20 start_MH_BILL_20,
	       t2.MH_BILL_20 end_MH_BILL_20,
	       BILL_20_Meter_Delta = t1.READ_RDC_BILL_20 --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'MH_BILL_20')  
	       ,
	       BILL_20_BMC_Value = t1.READ_RDC_BILL_20
	       -- Bill 10  
	       ,
	       t1.MH_BILL_10 start_MH_BILL_10,
	       t2.MH_BILL_10 end_MH_BILL_10,
	       BILL_10_Meter_Delta = t1.READ_RDC_BILL_10 -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'MH_BILL_10')  
	       ,
	       BILL_10_BMC_Value = t1.READ_RDC_BILL_10
	       -- Bill 5  
	       ,
	       t1.MH_BILL_5 start_MH_BILL_5,
	       t2.MH_BILL_5 end_MH_BILL_5,
	       BILL_5_Meter_Delta = t1.READ_RDC_BILL_5 --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'Mh_Bill_5')  
	       ,
	       BILL_5_BMC_Value = t1.READ_RDC_BILL_5
	       --Bill 2  
	       ,
	       t1.MH_BILL_2 start_MH_BILL_2,
	       t2.MH_BILL_2 end_MH_BILL_2,
	       BILL_2_Meter_Delta = t1.READ_RDC_BILL_2 -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'Mh_Bill_2')  
	       ,
	       BILL_2_BMC_Value = t1.READ_RDC_BILL_2 
	       --Bill 1  
	       ,
	       t1.MH_BILL_1 start_MH_BILL_1,
	       t2.MH_BILL_1 end_MH_BILL_1,
	       BILL_1_Meter_Delta = t1.READ_RDC_BILL_1 -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'Mh_Bill_1')  
	       ,
	       BILL_1_BMC_Value = t1.READ_RDC_BILL_1 
	       --Cashable Voucher In  
	       ,
	       t1.mh_ticket_inserted_value start_CASHABLE_VOUCHER_IN,
	       t2.mh_ticket_inserted_value end_CASHABLE_VOUCHER_IN 
	       --,CASHABLE_VOUCHER_IN_Meter_Delta = dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,
	       --null,null,null,null,'mh_ticket_inserted_value')  
	       ,
	       CASHABLE_VOUCHER_IN_Meter_Delta = t1.READ_TICKET_IN_SUSPENSE,
	       CASHABLE_VOUCHER_IN_BMC_Value = t1.READ_TICKET_IN_SUSPENSE 
	       --Cashable Voucher Out  
	       ,
	       t1.mh_ticket_printed_value start_CASHABLE_VOUCHER_OUT,
	       t2.mh_ticket_printed_value end_CASHABLE_VOUCHER_OUT,
	       CASHABLE_VOUCHER_OUT_Meter_Delta = t1.READ_TICKET -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_ticket_printed_value')  
	       ,
	       CASHABLE_VOUCHER_OUT_BMC_Value = t1.READ_TICKET 
	       --Non Cashable Voucher In  
	       ,
	       t1.MH_TICKETS_INSERTED_NONCASHABLE_VALUE start_NONCASHABLE_VOUCHER_IN,
	       t2.MH_TICKETS_INSERTED_NONCASHABLE_VALUE end_NONCASHABLE_VOUCHER_IN,
	       NONCASHABLE_VOUCHER_IN_Meter_Delta = t1.TICKETS_INSERTED_NONCASHABLE_VALUE -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_tickets_inserted_noncashable_value')  
	       ,
	       NONCASHABLE_VOUCHER_IN_BMC_Value = t1.TICKETS_INSERTED_NONCASHABLE_VALUE
	       --Non Cashable Voucher Out    
	       ,
	       t1.MH_TICKETS_PRINTED_NONCASHABLE_VALUE start_NONCASHABLE_VOUCHER_OUT,
	       t2.MH_TICKETS_PRINTED_NONCASHABLE_VALUE end_NONCASHABLE_VOUCHER_OUT,
	       NONCASHABLE_VOUCHER_OUT_Meter_Delta = t1.TICKETS_PRINTED_NONCASHABLE_VALUE --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_tickets_printed_noncashable_value')  
	       ,
	       NONCASHABLE_VOUCHER_OUT_BMC_Value = t1.TICKETS_PRINTED_NONCASHABLE_VALUE
	       -- coin drop      
	       ,
	       t1.mh_coin_drop start_COIN_DROP,
	       t2.mh_coin_drop end_COIN_DROP,
	       COIN_DROP_Meter_Delta = CAST(t1.read_coin_drop AS FLOAT) * t1.Installation_Price_Per_Play --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_coin_drop')*t1.Installation_Price_Per_Play    
	       ,
	       COIN_DROP_BMC_Value = CAST(t1.read_coin_drop AS FLOAT) * t1.Installation_Price_Per_Play
	       -- TRUE_COIN_IN    
	       ,
	       t1.mh_true_coin_in start_TRUE_COIN_IN,
	       t2.mh_true_coin_in end_TRUE_COIN_IN,
	       TRUE_COIN_IN_Meter_Delta = CAST(t1.READ_RDC_TRUE_COIN_IN AS FLOAT) * t1.Token --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_true_coin_in')* t1.Token    
	       ,
	       TRUE_COIN_IN_BMC_Value = CAST(t1.READ_RDC_TRUE_COIN_IN AS FLOAT) * t1.Token
	       --TRUE_COIN_OUT    
	       ,
	       t1.mh_true_coin_out start_TRUE_COIN_OUT,
	       t2.mh_true_coin_out end_TRUE_COIN_OUT,
	       TRUE_COIN_OUT_Meter_Delta = (CAST(t1.READ_RDC_TRUE_COIN_OUT AS FLOAT) * t1.Token) -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_true_coin_out')* t1.Token    
	       ,
	       TRUE_COIN_OUT_BMC_Value = (CAST(t1.READ_RDC_TRUE_COIN_OUT AS FLOAT) * t1.Token) 
	       --COIN_INSERTED    
	       ,
	       t1.mh_coins_in start_COIN_INSERTED,
	       t2.mh_coins_in end_COIN_INSERTED,
	       COIN_INSERTED_Meter_Delta = (CAST(t1.read_coins_in AS FLOAT) * t1.Installation_Price_Per_Play) -- dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_coins_in')* t1.Installation_Price_Per_Play    
	       ,
	       COIN_INSERTED_BMC_Value = (CAST(t1.read_coins_in AS FLOAT) * t1.Installation_Price_Per_Play) 
	       --COIN_COLLECTED    
	       ,
	       t1.mh_coins_out start_COIN_COLLECTED,
	       t2.mh_coins_out end_COIN_COLLECTED,
	       COIN_COLLECTED_Meter_Delta = CAST((t1.read_coins_out -t1.cc_READ_RDC_JACKPOT) AS FLOAT)
	       * t1.Installation_Price_Per_Play --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_coins_out')* t1.Installation_Price_Per_Play    
	       ,
	       COIN_COLLECTED_BMC_Value = CAST((t1.read_coins_out -t1.cc_READ_RDC_JACKPOT) AS FLOAT)
	       * t1.Installation_Price_Per_Play 
	       --JACKPOT    
	       ,
	       t1.mh_jackpot start_JACKPOT,
	       t2.mh_jackpot end_JACKPOT,
	       JACKPOT_Meter_Delta = (CAST(ISNULL(t1.read_rdc_jackpot, 0) AS FLOAT))
	       * t1.Installation_Price_Per_Play --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_jackpot')* t1.Installation_Price_Per_Play    
	       ,
	       JACKPOT_BMC_Value = (CAST(ISNULL(t1.read_rdc_jackpot, 0) AS FLOAT)) *
	       t1.Installation_Price_Per_Play 
	       --ATTND_PAID_CC    
	       ,
	       t1.mh_Handpay start_ATTND_PAID_CC,
	       t2.mh_Handpay end_ATTND_PAID_CC,
	       ATTND_PAID_CC_Meter_Delta = CAST(
	           (
	               ISNULL(t1.read_Handpay, 0) -ISNULL(t1.cc_READ_RDC_JACKPOT, 0)
	           ) AS FLOAT
	       ) * t1.Installation_Price_Per_Play --dbo.fsp_CalculateMeterDeltas(t1.MH_ID,t2.MH_ID,t1.Installation_ID,null,null,null,null,'mh_Handpay')* t1.Installation_Price_Per_Play    
	       ,
	       ATTND_PAID_CC_BMC_Value = CAST(
	           (
	               ISNULL(t1.read_Handpay, 0) -ISNULL(t1.cc_READ_RDC_JACKPOT, 0)
	           ) AS FLOAT
	       ) * t1.Installation_Price_Per_Play 
	       --ATTND_PAID_PROG_PAYOUT    
	       ,
	       t1.mh_progressive_win_handpay_value start_ATTND_PAID_PROG_PAYOUT,
	       t2.mh_progressive_win_handpay_value end_ATTND_PAID_PROG_PAYOUT,
	       ATTND_PAID_PROG_PAYOUT_Meter_Delta = CAST(
	           (
	               t2.mh_progressive_win_handpay_value - t1.mh_progressive_win_handpay_value
	           ) AS FLOAT
	       ) * t1.Installation_Price_Per_Play,
	       ATTND_PAID_PROG_PAYOUT_BMC_Value = CAST((t1.progressive_win_handpay_value) AS FLOAT)
	       * t1.Installation_Price_Per_Play 
	       --MACHINE_PAID_PROG_PAYOUT    
	       ,
	       t1.mh_progressive_win_value start_MACHINE_PAID_PROG_PAYOUT,
	       t2.mh_progressive_win_value end_MACHINE_PAID_PROG_PAYOUT,
	       MACHINE_PAID_PROG_PAYOUT_Meter_Delta = CAST(
	           (t2.mh_progressive_win_value - t1.mh_progressive_win_value) AS 
	           FLOAT
	       ) * t1.Installation_Price_Per_Play,
	       MACHINE_PAID_PROG_PAYOUT_BMC_Value = CAST((t1.progressive_win_value) AS FLOAT)
	       * t1.Installation_Price_Per_Play 
	       --PromoCashableEFTIN    
	       ,
	       t1.MH_Promo_Cashable_EFT_IN start_PromoCashableEFTIN,
	       t2.MH_Promo_Cashable_EFT_IN end_PromoCashableEFTIN,
	       PromoCashableEFTIN_Meter_Delta = t1.Promo_Cashable_EFT_IN,
	       PromoCashableEFTIN_BMC_Value = t1.Promo_Cashable_EFT_IN
	       --PromoCashableEFTOut  
	       ,
	       t1.MH_Promo_Cashable_EFT_OUT start_PromoCashableEFTOut,
	       t2.MH_Promo_Cashable_EFT_OUT end_PromoCashableEFTOut,
	       PromoCashableEFTOut_Meter_Delta = t1.Promo_Cashable_EFT_OUT,
	       PromoCashableEFTOut_BMC_Value = t1.Promo_Cashable_EFT_OUT
	       --NonCashableEFTIN  
	       ,
	       t1.MH_NonCashable_EFT_IN start_NonCashableEFTIN,
	       t2.MH_NonCashable_EFT_IN end_NonCashableEFTIN,
	       NonCashableEFTIN_Meter_Delta = t1.NonCashable_EFT_IN,
	       NonCashableEFTIN_BMC_Value = t1.NonCashable_EFT_IN
	       --NonCashableEFTOUT  
	       ,
	       t1.MH_NonCashable_EFT_OUT start_NonCashableEFTOUT,
	       t2.MH_NonCashable_EFT_OUT end_NonCashableEFTOUT,
	       NonCashableEFTOUT_Meter_Delta = t1.NonCashable_EFT_OUT,
	       NonCashableEFTOUT_BMC_Value = t1.NonCashable_EFT_OUT
	       --CashableEFTIN  
	       ,
	       t1.MH_Cashable_EFT_IN start_CashableEFTIN,
	       t2.MH_Cashable_EFT_IN end_CashableEFTIN,
	       CashableEFTIN_Meter_Delta = t1.Cashable_EFT_IN,
	       CashableEFTIN_BMC_Value = t1.Cashable_EFT_IN
	       --CashableEFTOUT  
	       ,
	       t1.MH_Cashable_EFT_OUT start_CashableEFTOUT,
	       t2.MH_Cashable_EFT_OUT end_CashableEFTOUT,
	       CashableEFTOUT_Meter_Delta = t1.Cashable_EFT_OUT,
	       CashableEFTOUT_BMC_Value = t1.Cashable_EFT_OUT
	       --  
	       INTO #temp
	FROM   #tmpMH t1
	       JOIN #tmpMH t2
	            ON  t1.installation_id = t2.installation_id
	WHERE  t1.asc_rnum = 1
	       AND t2.desc_rnum = 1 
	
	-- Bets  
	SELECT read_id,
	       [Meter Type] = 'Games Played',
	       [Meter Order] = 1,
	       installation_id,
	       [Start Meter] = start_mh_games_bet,
	       [End Meter] = end_mh_games_bet,
	       [Meter Delta] = CAST(Bets_Meter_Delta AS FLOAT),
	       [BMC Value] = CAST(Bets_BMC_Value AS FLOAT) 
	       INTO #temp1
	FROM   #temp 
	
	UNION ALL 
	-- Wins  
	SELECT read_id,
	       'Games Won',
	       2,
	       installation_id,
	       [Start Meter] = start_mh_games_won,
	       [End Meter] = end_mh_games_won,
	       [Meter Delta] = CAST(Win_Meter_Delta AS FLOAT) ,
	       [BMC Value] = CAST(Win_BMC_Value AS FLOAT)
	FROM   #temp 
	
	UNION ALL 
	-- Bill 100  
	SELECT read_id,
	       'Bill 100',
	       3,
	       installation_id,
	       [Start Meter] = start_MH_BILL_100,
	       [End Meter] = end_MH_BILL_100,
	       [Meter Delta] = CAST(BILL_100_Meter_Delta AS FLOAT)  * 100.0,
	       [BMC Value] = CAST(BILL_100_BMC_Value AS FLOAT)  * 100.0
	FROM   #temp
	WHERE  @SYSRegion IN ('US', 'AR')
	
	UNION ALL 
	-- Bill 50  
	SELECT read_id,
	       'Bill 50',
	       4,
	       installation_id,
	       [Start Meter] = start_MH_BILL_50,
	       [End Meter] = end_MH_BILL_50,
	       [Meter Delta] = CAST(BILL_50_Meter_Delta AS FLOAT) * 50.0,
	       [BMC Value] = CAST(BILL_50_BMC_Value AS FLOAT) * 50.0
	FROM   #temp 
	
	UNION ALL 
	-- Bill 20  
	SELECT read_id,
	       'Bill 20',
	       5,
	       installation_id,
	       [Start Meter] = start_MH_BILL_20,
	       [End Meter] = end_MH_BILL_20,
	       [Meter Delta] = CAST(BILL_20_Meter_Delta AS FLOAT) * 20.0,
	       [BMC Value] = CAST(BILL_20_BMC_Value AS FLOAT) * 20.0
	FROM   #temp 
	
	UNION ALL 
	-- Bill 10  
	SELECT read_id,
	       'Bill 10',
	       6,
	       installation_id,
	       [Start Meter] = start_MH_BILL_10,
	       [End Meter] = end_MH_BILL_10,
	       [Meter Delta] = CAST(BILL_10_Meter_Delta AS FLOAT) * 10.0,
	       [BMC Value] = CAST(BILL_10_BMC_Value AS FLOAT) * 10.0
	FROM   #temp 
	
	UNION ALL 
	-- Bill 5  
	SELECT read_id,
	       'Bill 5',
	       7,
	       installation_id,
	       [Start Meter] = start_MH_BILL_5,
	       [End Meter] = end_MH_BILL_5,
	       [Meter Delta] = CAST(BILL_5_Meter_Delta AS FLOAT) * 5.0,
	       [BMC Value] = CAST(BILL_5_BMC_Value AS FLOAT) * 5.0
	FROM   #temp 
	
	UNION ALL 
	-- Bill 2  
	SELECT read_id,
	       'Bill 2',
	       8,
	       installation_id,
	       [Start Meter] = start_MH_BILL_2,
	       [End Meter] = end_MH_BILL_2,
	       [Meter Delta] = CAST(BILL_2_Meter_Delta AS FLOAT) * 2.0,
	       [BMC Value] = CAST(BILL_2_BMC_Value AS FLOAT) * 2.0
	FROM   #temp
	WHERE  @SYSRegion = 'AR' 
	
	UNION ALL 
	-- Bill 1  
	SELECT read_id,
	       'Bill 1',
	       8,
	       installation_id,
	       [Start Meter] = start_MH_BILL_1,
	       [End Meter] = end_MH_BILL_1,
	       [Meter Delta] = CAST(BILL_1_Meter_Delta AS FLOAT),
	       [BMC Value] = CAST(BILL_1_BMC_Value AS FLOAT)
	FROM   #temp
	WHERE  @SYSRegion = 'US' 
	
	UNION ALL 
	-- CASHABLE_VOUCHER_IN  
	SELECT read_id,
	       'Cashable Voucher In',
	       9,
	       installation_id,
	       [Start Meter] = start_CASHABLE_VOUCHER_IN,
	       [End Meter] = end_CASHABLE_VOUCHER_IN,
	       [Meter Delta] = (CAST(CASHABLE_VOUCHER_IN_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(CASHABLE_VOUCHER_IN_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- CASHABLE_VOUCHER_OUT  
	SELECT read_id,
	       'Cashable Voucher Out',
	       10,
	       installation_id,
	       [Start Meter] = start_CASHABLE_VOUCHER_OUT,
	       [End Meter] = end_CASHABLE_VOUCHER_OUT,
	       [Meter Delta] = (CAST(CASHABLE_VOUCHER_OUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(CASHABLE_VOUCHER_OUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- NONCASHABLE_VOUCHER_IN  
	SELECT read_id,
	       'Non Cashable Voucher In',
	       11,
	       installation_id,
	       [Start Meter] = start_NONCASHABLE_VOUCHER_IN,
	       [End Meter] = end_NONCASHABLE_VOUCHER_IN,
	       [Meter Delta] = (CAST(NONCASHABLE_VOUCHER_IN_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(NONCASHABLE_VOUCHER_IN_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- NONCASHABLE_VOUCHER_OUT  
	SELECT read_id,
	       'Non Cashable Voucher Out',
	       12,
	       installation_id,
	       [Start Meter] = start_NONCASHABLE_VOUCHER_OUT,
	       [End Meter] = end_NONCASHABLE_VOUCHER_OUT,
	       [Meter Delta] = (CAST(NONCASHABLE_VOUCHER_OUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(NONCASHABLE_VOUCHER_OUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- COIN_DROP  
	SELECT read_id,
	       'Game Drop',
	       13,
	       installation_id,
	       [Start Meter] = start_COIN_DROP,
	       [End Meter] = end_COIN_DROP,
	       [Meter Delta] = (CAST(COIN_DROP_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(COIN_DROP_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- TRUE_COIN_IN  
	SELECT read_id,
	       'True Coin in',
	       14,
	       installation_id,
	       [Start Meter] = start_TRUE_COIN_IN,
	       [End Meter] = end_TRUE_COIN_IN,
	       [Meter Delta] = (CAST(TRUE_COIN_IN_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(TRUE_COIN_IN_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- TRUE_COIN_OUT  
	SELECT read_id,
	       'True Coin Out',
	       15,
	       installation_id,
	       [Start Meter] = start_TRUE_COIN_OUT,
	       [End Meter] = end_TRUE_COIN_OUT,
	       [Meter Delta] = (CAST(TRUE_COIN_OUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(TRUE_COIN_OUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- COIN_INSERTED  
	SELECT read_id,
	       'Bets',
	       16,
	       installation_id,
	       [Start Meter] = start_COIN_INSERTED,
	       [End Meter] = end_COIN_INSERTED,
	       [Meter Delta] = (CAST(COIN_INSERTED_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(COIN_INSERTED_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- COIN_COLLECTED  
	SELECT read_id,
	       'Wins',
	       17,
	       installation_id,
	       [Start Meter] = start_COIN_COLLECTED,
	       [End Meter] = end_COIN_COLLECTED,
	       [Meter Delta] = (CAST(COIN_COLLECTED_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(COIN_COLLECTED_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- JACKPOT  
	SELECT read_id,
	       'Jackpot',
	       18,
	       installation_id,
	       [Start Meter] = start_JACKPOT,
	       [End Meter] = end_JACKPOT,
	       [Meter Delta] = (CAST(JACKPOT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(JACKPOT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- ATTND_PAID_CC  
	SELECT read_id,
	       'Attendant Paid CC',
	       19,
	       installation_id,
	       [Start Meter] = start_ATTND_PAID_CC,
	       [End Meter] = end_ATTND_PAID_CC,
	       [Meter Delta] = (CAST(ATTND_PAID_CC_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(ATTND_PAID_CC_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- ATTND_PAID_PROG_PAYOUT  
	SELECT read_id,
	       'Attendant Paid Progressive Payout',
	       20,
	       installation_id,
	       [Start Meter] = start_ATTND_PAID_PROG_PAYOUT,
	       [End Meter] = end_ATTND_PAID_PROG_PAYOUT,
	       [Meter Delta] = (CAST(ATTND_PAID_PROG_PAYOUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(ATTND_PAID_PROG_PAYOUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- MACHINE_PAID_PROG_PAYOUT  
	SELECT read_id,
	       'Machine Paid Progressive',
	       21,
	       installation_id,
	       [Start Meter] = start_MACHINE_PAID_PROG_PAYOUT,
	       [End Meter] = end_MACHINE_PAID_PROG_PAYOUT,
	       [Meter Delta] = (CAST(MACHINE_PAID_PROG_PAYOUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(MACHINE_PAID_PROG_PAYOUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- PromoCashableEFTIN  
	SELECT read_id,
	       'PromoCashableEFTIN',
	       22,
	       installation_id,
	       [Start Meter] = start_PromoCashableEFTIN,
	       [End Meter] = end_PromoCashableEFTIN,
	       [Meter Delta] = (CAST(PromoCashableEFTIN_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(PromoCashableEFTIN_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- PromoCashableEFTOut  
	SELECT read_id,
	       'PromoCashableEFTOUT',
	       22,
	       installation_id,
	       [Start Meter] = start_PromoCashableEFTOut,
	       [End Meter] = end_PromoCashableEFTOut,
	       [Meter Delta] = (CAST(PromoCashableEFTOut_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(PromoCashableEFTOut_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	
	UNION ALL 
	-- NonCashableEFTIN  
	SELECT read_id,
	       'NonCashableEFTIN',
	       23,
	       installation_id,
	       [Start Meter] = start_NonCashableEFTIN,
	       [End Meter] = end_NonCashableEFTIN,
	       [Meter Delta] = (CAST(NonCashableEFTIN_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(NonCashableEFTIN_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	
	UNION ALL 
	-- NonCashableEFTOUT  
	SELECT read_id,
	       'NonCashableEFTOUT',
	       23,
	       installation_id,
	       [Start Meter] = start_NonCashableEFTOUT,
	       [End Meter] = end_NonCashableEFTOUT,
	       [Meter Delta] = (CAST(NonCashableEFTOUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(NonCashableEFTOUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- NonCashableEFTOUT  
	SELECT read_id,
	       'CashableEFTIN',
	       24,
	       installation_id,
	       [Start Meter] = start_CashableEFTIN,
	       [End Meter] = end_CashableEFTIN,
	       [Meter Delta] = (CAST(CashableEFTIN_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(CashableEFTIN_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	UNION ALL 
	-- CashableEFTOUT  
	SELECT read_id,
	       'CashableEFTOUT',
	       24,
	       installation_id,
	       [Start Meter] = start_CashableEFTOUT,
	       [End Meter] = end_CashableEFTOUT,
	       [Meter Delta] = (CAST(CashableEFTOUT_Meter_Delta AS FLOAT) / 100.0),
	       [BMC Value] = (CAST(CashableEFTOUT_BMC_Value AS FLOAT) / 100.0)
	FROM   #temp 
	
	------------------------------------new code end------------------------------------------
	-- get value from tmp into a pre grouping temp table,
	-- incase we want to do some formatting and checking before returning to caller                  
	
	SELECT #temp1.Installation_ID,
	       Bar_Position_ID,
	       Bar_Position_Name,	-- stand                  
	       Machine_Type_ID,
	       Machine_Type_Code,
	       Site_Name,
	       Site_ID,
	       Sub_Company_Name,
	       Sub_Company_ID,
	       Company_Name,
	       Company_ID,
	       Machine_Stock_No,	-- slot                  
	       Zone_Name = CASE 
	                        WHEN ISNULL(Zone_name, '') = '' THEN 'NOT SET'
	                        ELSE Zone_Name
	                   END,	-- area  
	       Zone_ID = zone_id,
	       --                  
	       installation_price_per_play = CAST(
	           CAST(installation_price_per_play AS FLOAT) / 100 AS DECIMAL(5, 2)
	       ),	-- denom                   
	       [Meter Type],
	       [Meter Order],
	       [Start Meter],
	       [End Meter],
	       [Meter Delta],
	       [BMC Value],
	       [Variance_Value] = [BMC Value] - [Meter Delta],
	       [Variance_per] = dbo.fnPercentOf ([Meter Delta], [BMC Value]),
	       InstallationDate = #tempGeneric.InstallDate 
	       
	       INTO #preGrouping
	FROM   #temp1
	       JOIN #tempGeneric
	            ON  #temp1.Read_ID = #tempGeneric.Read_ID
	ORDER BY
	       installation_id,
	       [meter order]                  
	
	
	
	CREATE TABLE #CalculatedTable
	(
		Installation_ID              INT,
		Bar_Position_ID              INT,
		Bar_Position_Name            VARCHAR(100),
		Machine_Type_ID              INT,
		Machine_Type_Code            VARCHAR(100),
		Site_Name                    VARCHAR(100),
		Site_ID                      INT,
		Sub_Company_Name             VARCHAR(100),
		Sub_Company_ID               INT,
		Company_Name                 VARCHAR(100),
		Company_ID                   INT,
		Machine_Stock_No             VARCHAR(50),
		Zone_Name                    VARCHAR(100),
		Zone_ID                      INT,
		installation_price_per_play  FLOAT,
		[Meter Type]                 VARCHAR(100),
		[Meter Order]                FLOAT,
		[Start Meter]                FLOAT,
		[End Meter]                  FLOAT,
		[Meter Delta]                FLOAT,
		[BMC Value]                  FLOAT,
		[Variance_Value]             FLOAT,
		[Variance_per]               FLOAT,
		InstallationDate             DATETIME,
		SORT_ORDER                   INT
	) 
	
	-- do the grouping incase we are used as part of the subreport                  
	
	
	IF EXISTS (
	       SELECT TOP 1 *
	       FROM   #preGrouping
	   )
	    INSERT INTO #CalculatedTable
	    SELECT p.*,
	           SORT_ORDER = 1
	    FROM   #preGrouping p
	           JOIN [Site] S
	                ON  S.Site_ID = p.Site_Id
	    WHERE  (
	               (@Denom <> 0 AND installation_price_per_play = @Denom)
	               OR @Denom = 0
	           )
	           AND ((@zone IS NULL) OR (@zone IS NOT NULL AND Zone_ID=@zone))
	           AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	           AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	           AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	           AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	           AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND p.Site_ID IN (SELECT DATA
	                                     FROM   fnSplit(@SiteIDList, ','))
	               )
	           AND ISNULL(@Company, company_id) = company_id                           
	
	
	SELECT Site_name,
	       zone_id,
	       zone_name,
	       [Meter Type] AS [Meter Type],
	       [meter order] AS [meter order],
	       SUM([Meter Delta]) AS [Meter Delta],
	       SUM([BMC Value]) AS [BMC Value],
	       SUM([Variance_Value]) AS [Variance_Value],
	       SUM([Variance_per]) AS [Variance_per] 
	       INTO #TempZoneSubTotal
	FROM   #CalculatedTable
	WHERE  zone_name IS NOT NULL
	GROUP BY
	       zone_id,
	       Zone_Name,
	       site_name,
	       [Meter Type],
	       [meter order]       
	
	IF (@GROUPBYZONE = 1)
	BEGIN
	    INSERT INTO #CalculatedTable
	      (
	        site_name,
	        zone_id,
	        Zone_Name,
	        [Meter Type],
	        [meter order],
	        [Meter Delta],
	        [BMC Value],
	        [Variance_Value],
	        [Variance_per],
	        SORT_ORDER
	      )
	    SELECT *,
	           2
	    FROM   #TempZoneSubTotal
	END
	
	SELECT site_name,
	       site_id,
	       [Meter Type] AS [Meter Type],
	       [meter order] AS [meter order],
	       SUM([Meter Delta]) AS [Meter Delta],
	       SUM([BMC Value]) AS [BMC Value],
	       SUM([Variance_Value]) AS [Variance_Value],
	       SUM([Variance_per]) AS [Variance_per]
	       INTO #TempSiteSubTotal
	FROM   #CalculatedTable
	WHERE  site_id IS NOT NULL
	GROUP BY
	       site_id,
	       site_name,
	       [Meter Type],
	       [meter order] 
	
	INSERT INTO #CalculatedTable
	  (
	    site_name,
	    site_id,
	    [Meter Type],
	    [meter order],
	    [Meter Delta],
	    [BMC Value],
	    [Variance_Value],
	    [Variance_per],
	    SORT_ORDER
	  )
	SELECT *,
	       3
	FROM   #TempSiteSubTotal 
	
	SELECT [Meter Type] AS [Meter Type],
	       [meter order] AS [meter order],
	       SUM([Meter Delta]) AS [Meter Delta],
	       SUM([BMC Value]) AS [BMC Value],
	       SUM([Variance_Value]) AS [Variance_Value],
	       SUM([Variance_per]) AS [Variance_per] 
	       INTO #TempGrandTotal
	FROM   #TempSiteSubTotal
	GROUP BY
	       [Meter Type],
	       [meter order]       
	
	INSERT INTO #CalculatedTable
	  (
	    [Meter Type],
	    [meter order],
	    [Meter Delta],
	    [BMC Value],
	    [Variance_Value],
	    [Variance_per],
	    SORT_ORDER
	  )
	SELECT *,
	       4
	FROM   #TempGrandTotal 
	
	DROP TABLE #TempSiteSubTotal
	DROP TABLE #TempZoneSubTotal 
	DROP TABLE #TempGrandTotal      
	
	SELECT Installation_ID,
	       Bar_Position_ID,
	       Bar_Position_Name,
	       Machine_Type_ID,
	       Machine_Type_Code,
	       Site_Name,
	       Site_ID,
	       Sub_Company_Name,
	       Sub_Company_ID,
	       Company_Name,
	       Company_ID,
	       Machine_Stock_No,
	       Zone_Name,
	       Zone_ID,
	       installation_price_per_play,
	       [Meter Type],
	       [Meter Order],
	       ISNULL([Start Meter], 0) AS [Start Meter],
	       ISNULL([End Meter], 0) AS [End Meter],
	       ISNULL([Meter Delta], 0) AS [Meter Delta],
	       ISNULL([BMC Value], 0) AS [BMC Value],
	       ISNULL([Variance_Value], 0) AS [Variance_Value],
	       [Variance_per],
	       InstallationDate,
	       SORT_ORDER
	FROM   #CalculatedTable
	WHERE  SORT_ORDER IN (1, 2, 3, 4)
	ORDER BY
	       SORT_ORDER,
	       [Site_id],
	       zone_id,
	       [bar_position_name],
	       InstallationDate,
	       [meter order] 
	
	DROP TABLE #CalculatedTable 
	DROP TABLE #preGrouping
END
GO

