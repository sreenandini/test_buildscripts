
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_DailyAccounting]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_DailyAccounting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_REPORT_DailyAccounting]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Zone VARCHAR(50) = NULL,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@GroupByZone BIT,
	@SiteIDList varchar(MAX)
AS
	DECLARE @calcStartDate            DATETIME,
	        @calcEndDate              DATETIME,
	        @Zone_id                  INT,
	        @currencyFormat           VARCHAR(20),
	        @isAFTCalculationEnabled  BIT,   
	        @Include_AllTreasuryItems_In_Handpay	BIT   
	
	SET DATEFORMAT dmy        
	
	SET @calcStartDate = CAST(CONVERT(VARCHAR(12), @startdate) AS DATETIME)            
	SET @calcEndDate = CAST(CONVERT(VARCHAR(12), @enddate) AS DATETIME)  
	
	
	DECLARE @NOTSET VARCHAR(20)        
	
	SET @NOTSET = 'UN-DEFINED'
	
	IF @company = 0
		SET @company = NULL
	
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
	
	IF @zone = 'ALL'
	   OR @zone = '0'
	   OR @zone = '--None--'
	    SET @zone = NULL    
	
	SELECT @currencyFormat = CASE 
	                              WHEN setting_value = 'es-AR' THEN 'it-IT'
	                              ELSE setting_value
	                         END
	FROM   Setting
	WHERE  Setting_Name = 'BMC_Reports_Language'       
	
	SELECT @isAFTCalculationEnabled = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'
	
	SELECT @Include_AllTreasuryItems_In_Handpay = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'Include_AllTreasuryItems_In_Handpay'
	
	DECLARE @HandpayTable TABLE
	(
		HP_Installation_ID INT,
		HP_Amount FLOAT,
		HP_Date DATETIME
	)
	IF @Include_AllTreasuryItems_In_Handpay = 1
	BEGIN
		INSERT INTO @HandpayTable
			SELECT
				Installation_ID AS HP_Installation_ID,
				SUM(CAST(ISNULL(dbo.Treasury_Entry.Treasury_Amount,0) AS FLOAT)) AS HP_Amount,
				DBO.[fnCalcGamingDt](CONVERT(DATETIME, CONVERT(VARCHAR(20), Treasury_Date) + ' ' + Treasury_Time, 106)) as HP_Date
			FROM
				Treasury_Entry
			WHERE
				DBO.[fnCalcGamingDt](CONVERT(DATETIME, CONVERT(VARCHAR(20), Treasury_Date) + ' ' + Treasury_Time, 106))
				BETWEEN  @calcStartDate and @calcEndDate
			GROUP BY
				Installation_ID, DBO.[fnCalcGamingDt](CONVERT(DATETIME, CONVERT(VARCHAR(20), Treasury_Date) + ' ' + Treasury_Time, 106))
	END
	ELSE
	BEGIN
		INSERT INTO @HandpayTable
			SELECT
				0 AS HP_Installation_ID,
				CAST(0 AS FLOAT) AS HP_Amount,
				GETDATE() AS HP_Date
	END
	
	DECLARE @temptable TABLE 
	        (
	            Installation_ID INT,
	            Bar_Position_ID INT,
	            Bar_Position_Name VARCHAR(50),
	            Machine_Name VARCHAR(50),
	            Zone_Name VARCHAR(50),
	            Machine_Type_ID INT,
	            Machine_Type_Code VARCHAR(50),
	            Site_Name VARCHAR(50),
	            CoinsIn FLOAT,
	            CoinsOut FLOAT,
	            BillsIn FLOAT,
	            TicketsIn FLOAT,
	            NonCashableVouchersIn FLOAT,
	            EFTIn FLOAT,
	            TotalCashIn FLOAT,
	            TicketsOut FLOAT,
	            NonCashableVouchersOut FLOAT,
	            Handpay FLOAT,
	            EFTOut FLOAT,
	            TotalCashOut FLOAT,
	            Net FLOAT,
	            CurrencyFormat VARCHAR(20),
	            Machine_Stock_No VARCHAR(50)
	        )
	
	INSERT INTO @temptable
	SELECT Installation.Installation_ID,
	       Installation.Bar_Position_ID,
	       Bar_Position.Bar_Position_Name,
	    
	       
	   (
	    CASE 
	    WHEN Machine_Name = 'MULTI GAME' THEN               
        ISNULL(MGMP.Multigamename, 'MULTI GAME')
	    ELSE  Machine_Name 
	    END
	     )    
         AS machine_name,
	       isnull(Zone.Zone_Name,'NOT SET') AS Zone_Name,
	       Machine_Type_Category.Machine_Type_ID,
	       Machine_Type_Category.Machine_Type_Code,
	       SITE.Site_Name,
	       (
	           (
	               CAST(ISNULL(READ_RDC_TRUE_COIN_IN, 0) AS FLOAT) *
	               Installation.Installation_Token_Value
	           ) / 100
	       ) AS CoinsIn,	--Coins In        
	       
	       (
	           (
	               CAST(ISNULL(READ_RDC_TRUE_COIN_Out, 0) AS FLOAT) *
	               Installation.Installation_Token_Value
	           ) / 100
	       ) AS CoinsOut,	--Coins Out        
	       CASE 
	            WHEN SITE.Region = 'US' THEN CAST(ISNULL(READ_RDC_BILL_1, 0) AS FLOAT) 
	                 + (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                 + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                 + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                 + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                 --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	            WHEN SITE.Region = 'AR' THEN (CAST(ISNULL(READ_RDC_BILL_2, 0) AS FLOAT) * 2) 
	                 + (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                 + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                 + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                 + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                 --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	            ELSE (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                 + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                 + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                 + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                 --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	       END AS BillsIn,	--Cash In
	       
	       ISNULL(dbo.[Read].Read_Ticket_In_Suspense, 0) / 100 AS TicketsIn,	--Tickets In        
	       
	       CAST(
	           ISNULL([Read].TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT
	       ) / 100 AS NonCashableVouchersIn,	--Non Cashable Vouchers In        
	       
	       (
	           CAST(ISNULL([Read].Promo_Cashable_EFT_IN, 0) AS FLOAT) +
	           CAST(ISNULL([Read].NonCashable_EFT_IN, 0) AS FLOAT) +
	           CAST(ISNULL([Read].Cashable_EFT_IN, 0) AS FLOAT)
	       ) / 100 AS EFTIn,	-- EFT In      
	       
	       (
	           (
	               (
	                   CAST(ISNULL(dbo.[Read].READ_RDC_TRUE_COIN_IN, 0) AS FLOAT) 
	                   * Installation.Installation_Token_Value
	               ) / 100
	           ) 
	           +
	           CASE 
	                WHEN SITE.Region = 'US' THEN CAST(ISNULL(READ_RDC_BILL_1, 0) AS FLOAT) 
	                     + (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                     + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                     + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                     + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                     --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	                WHEN SITE.Region = 'AR' THEN (CAST(ISNULL(READ_RDC_BILL_2, 0) AS FLOAT) * 2) 
	                     + (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                     + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                     + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                     + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                     --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	                ELSE (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                     + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                     + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                     + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                     --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	           END 
	           +
	           
	           CASE 
	                WHEN @isAFTCalculationEnabled = 1 THEN (
	                         CAST(ISNULL([Read].Promo_Cashable_EFT_IN, 0) AS FLOAT) 
	                         +
	                         CAST(ISNULL([Read].NonCashable_EFT_IN, 0) AS FLOAT) 
	                         +
	                         CAST(ISNULL([Read].Cashable_EFT_IN, 0) AS FLOAT)
	                     ) / 100
	                ELSE 0
	           END 
	           
	           +
	           ISNULL(dbo.[Read].Read_Ticket_In_Suspense, 0) / 100 
	           +
	           CAST(
	               ISNULL([Read].TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT
	           ) / 100
	       ) AS TotalCashIn,
	       ISNULL(dbo.[Read].READ_TICKET, 0) / 100 AS TicketsOut,
	       CAST(
	           ISNULL([Read].TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT
	       ) / 100 AS NonCashableVouchersOut,	--Non Cashable Vouchers Out        
	       
	       CASE WHEN @Include_AllTreasuryItems_In_Handpay = 1 THEN
				CAST(ISNULL(HPTable.HP_Amount,0) AS FLOAT)
		   ELSE
			   ((
				   CAST(ISNULL(dbo.[Read].READ_HANDPAY, 0) AS FLOAT) * Installation.Installation_Price_Per_Play
			   ) / 100) 
		   END AS Handpay,
	       (
	           CAST(ISNULL([Read].Promo_Cashable_EFT_OUT, 0) AS FLOAT) +
	           CAST(ISNULL([Read].NonCashable_EFT_OUT, 0) AS FLOAT) +
	           CAST(ISNULL([Read].Cashable_EFT_OUT, 0) AS FLOAT)
	       ) / 100 AS EFTOut,	-- EFT Out      
	       
	       (
		   (
	           (
	               CAST(ISNULL(dbo.[Read].READ_RDC_TRUE_COIN_OUT, 0) AS FLOAT) *
	               Installation.Installation_Token_Value
	           ) 
	           
	           +
	           
	           CASE 
	                WHEN @isAFTCalculationEnabled = 1 THEN CAST(ISNULL([Read].Promo_Cashable_EFT_OUT, 0) AS FLOAT) 
	                     +
	                     CAST(ISNULL([Read].NonCashable_EFT_OUT, 0) AS FLOAT) +
	                     CAST(ISNULL([Read].Cashable_EFT_OUT, 0) AS FLOAT)
	                ELSE 0
	           END 
	           
	           +
	           ISNULL(dbo.[Read].READ_TICKET, 0) 
	           
	           +
	           CAST(
	               ISNULL([Read].TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT
	           )
	       ) / 100
	       
	       + 
	           
	           CASE WHEN @Include_AllTreasuryItems_In_Handpay = 1 THEN
					CAST(ISNULL(HPTable.HP_Amount,0) AS FLOAT)
				ELSE
					((
						CAST(ISNULL(dbo.[Read].READ_HANDPAY, 0) AS FLOAT) * Installation.Installation_Price_Per_Play
					)/100.00 ) 
				END
	       )AS TotalCashOut,	-- Total CashOut      
	       
	       (
	           (
	               CAST(ISNULL(READ_RDC_TRUE_COIN_IN, 0) AS FLOAT) *
	               Installation.Installation_Token_Value
	           ) / 100
	       ) 
	       
	       +
	       CASE 
	            WHEN SITE.Region = 'US' THEN CAST(ISNULL(READ_RDC_BILL_1, 0) AS FLOAT) 
	                 + (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                 + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                 + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                 + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                 --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	            WHEN SITE.Region = 'AR' THEN (CAST(ISNULL(READ_RDC_BILL_2, 0) AS FLOAT) * 2) 
	                 + (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                 + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                 + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                 + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                 --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	            ELSE (CAST(ISNULL(READ_RDC_BILL_5, 0) AS FLOAT) * 5) + (CAST(ISNULL(READ_RDC_BILL_10, 0) AS FLOAT) * 10) 
	                 + (CAST(ISNULL(READ_RDC_BILL_20, 0) AS FLOAT) * 20) + (CAST(ISNULL(READ_RDC_BILL_50, 0) AS FLOAT) * 50) 
	                 + (CAST(ISNULL(READ_RDC_BILL_100, 0) AS FLOAT) * 100) + (CAST(ISNULL(READ_RDC_BILL_200, 0) AS FLOAT) * 200) 
	                 + (CAST(ISNULL(READ_RDC_BILL_500, 0) AS FLOAT) * 500) 
	                 --(CAST(ISNULL(READ_RDC_BILL_1000,0) AS FLOAT) * 1000)
	       END 
	       +
	       
	       CASE 
	            WHEN @isAFTCalculationEnabled = 1 THEN (
	                     CAST(ISNULL([Read].Promo_Cashable_EFT_IN, 0) AS FLOAT) 
	                     +
	                     CAST(ISNULL([Read].NonCashable_EFT_IN, 0) AS FLOAT) +
	                     CAST(ISNULL([Read].Cashable_EFT_IN, 0) AS FLOAT)
	                 ) / 100
	            ELSE 0
	       END 
	       
	       +
	       ISNULL(dbo.[Read].Read_Ticket_In_Suspense, 0) / 100 
	       
	       +
	       CAST(
	           ISNULL([Read].TICKETS_INSERTED_NONCASHABLE_VALUE, 0) AS FLOAT
	       ) / 100 
	       -(
			(
			(
	           (
	               CAST(ISNULL(dbo.[Read].READ_RDC_TRUE_COIN_OUT, 0) AS FLOAT) *
	               Installation.Installation_Token_Value
	           ) +
	           ISNULL(dbo.[Read].READ_TICKET, 0) +
	           CAST(
	               ISNULL([Read].TICKETS_PRINTED_NONCASHABLE_VALUE, 0) AS FLOAT
	           ) +
	           
	           CASE 
	                WHEN @isAFTCalculationEnabled = 1 THEN CAST(ISNULL([Read].Promo_Cashable_EFT_OUT, 0) AS FLOAT) 
	                     +
	                     CAST(ISNULL([Read].NonCashable_EFT_OUT, 0) AS FLOAT) +
	                     CAST(ISNULL([Read].Cashable_EFT_OUT, 0) AS FLOAT)
	                ELSE 0
	           END
	       ) / 100)
	       +
	       (
	               CASE WHEN @Include_AllTreasuryItems_In_Handpay = 1 THEN
						CAST(ISNULL(HPTable.HP_Amount,0) AS FLOAT)
					ELSE
						((
						CAST(ISNULL(dbo.[Read].READ_HANDPAY, 0) AS FLOAT) * Installation.Installation_Price_Per_Play
						)/100.00 ) 
					END
	           ) 
	       ) AS Net,	--(TotalCashin-TotalCashout)        
	       @currencyFormat,
	       MACHINE.Machine_Stock_No 
	       --INTO #temptable1
	FROM   dbo.Installation Installation WITH (NOLOCK)
	       INNER JOIN dbo.Bar_Position Bar_Position WITH (NOLOCK)
	            ON  Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
	       INNER JOIN dbo.[Read] WITH (NOLOCK)
	            ON  Installation.Installation_ID = [Read].Installation_ID
	       INNER JOIN dbo.Machine MACHINE WITH (NOLOCK)
	            ON  Installation.Machine_ID = MACHINE.Machine_ID
	       LEFT JOIN MultiGameMapping MGMP ON
	            MGMP.Machineid=MACHINE.Machine_ID
	       INNER JOIN dbo.Site SITE WITH (NOLOCK)
	            ON  Bar_Position.Site_ID = SITE.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SITE.sub_company_id = SC.sub_company_id
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	       INNER JOIN dbo.Machine_Class Machine_Class WITH (NOLOCK)
	            ON  MACHINE.Machine_Class_ID = Machine_Class.Machine_Class_ID
	       LEFT OUTER JOIN @HandpayTable HPTable ON Installation.Installation_ID = HPTable.HP_Installation_ID 
				AND [Read].ReadDate  = HPTable.HP_Date
	       LEFT OUTER JOIN dbo.Machine_Type Machine_Type_Category WITH (NOLOCK)
	            ON  MACHINE.Machine_Category_ID = Machine_Type_Category.Machine_Type_ID
	       LEFT OUTER JOIN dbo.Zone Zone WITH (NOLOCK)
	            ON  Bar_Position.Zone_ID = Zone.Zone_ID
	WHERE  [Read].ReadDate BETWEEN @calcStartDate AND @calcEndDate
	       AND ISNULL(@company, c.company_id) = c.company_id
	       AND (SC.sub_company_id = ISNULL(@subcompany, SC.sub_company_id))
	       AND (
	               sub_company_region_id = ISNULL(@region, sub_company_region_id)
	           )
	       AND (sub_company_area_id = ISNULL(@area, sub_company_area_id))
	       AND (
	               sub_company_district_id = ISNULL(@district, sub_company_district_id)
	           )
	       --AND (SITE.site_id = ISNULL(@site, SITE.site_id))
	       AND @SiteIDList IS NOT NULL AND SITE.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	       AND (
	               (@zone IS NULL)
	               OR (
	                      @zone IS NOT NULL
	                      AND Zone.Zone_ID IN (
	                              SELECT Zone_Id
	                              FROM   Zone Z
	                              WHERE  Z.Zone_Id = @Zone
	                          )
	                  )
	           )
	ORDER BY
	       SITE.site_name,
	       Machine_Type_Category.Machine_Type_Code,
	       Bar_Position.Bar_Position_ID,
	       Machine_Type_Category.Machine_Type_ID,
	       Installation.installation_start_date 
	       DESC        
	
	
	
	
	SELECT Installation_ID,
	       Bar_Position_ID,
	       Bar_Position_Name,
	       Machine_Name,
	       Zone_Name,
	       Machine_Type_ID,
	       Machine_Type_Code,
	       Site_Name,
	       CoinsIn,
	       CoinsOut,
	       BillsIn,
	       TicketsIn,
	       NonCashableVouchersIn,
	       EFTIn,
	       TotalCashIn,
	       TicketsOut,
	       NonCashableVouchersOut,
	       EFTOut,
	       Handpay,
	       TotalCashOut,
	       Net,
	       CurrencyFormat,
	       Machine_Stock_No
	FROM   @temptable
GO

