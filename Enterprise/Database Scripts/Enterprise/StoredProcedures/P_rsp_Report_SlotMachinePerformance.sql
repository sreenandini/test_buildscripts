USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_SlotMachinePerformance]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_SlotMachinePerformance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_Report_SlotMachinePerformance]
	@Company		INT = 0,
	@SubCompany		INT = 0,
	@Region			INT = 0 ,
	@Area			INT = 0,
	@District		INT = 0,
	@Site			INT = 0,
	@GamingDate		DATETIME,
	@IncludeNonCashable BIT,
	@SiteIDList		VARCHAR(MAX)
AS
BEGIN
	
	SET DATEFORMAT DMY                     
	
	DECLARE @mtdStart          DATETIME,
	        @qtdStart          DATETIME,
	        @ytdStart          DATETIME,
	        @Denom             FLOAT,
	        @MachineTypeID     INT,
	        @RunType           INT,
	        @UsePhysicalWin    BIT,
	        @Zone              INT -- Hardcoded Zone ID as 0 to show all zone.Bcoz to avoid problem in Zone Based Grouping.
	
	              
	DECLARE @AddPromoCashable  VARCHAR(10)
	SET @AddPromoCashable = 'False'
	SELECT @AddPromoCashable = (COALESCE(RTRIM(LTRIM(Setting_Value)), ''))
	FROM   Setting
	WHERE  Setting_Name = 'Add_PromoCashable_In_WinLoss'
	
	SET @Denom = 0  
	SET @MachineTypeID = 0  
	SET @RunType = 1  
	SET @UsePhysicalWin = 0   
	SET @GamingDate = CAST(
	        CONVERT(VARCHAR(30), CAST(@GamingDate AS DATETIME), 106) AS DATETIME
	    )
	
	
	
	CREATE TABLE #tmp
	(
		SortOrder                    INT,
		[Order]                      INT,
		[Type]                       CHAR(1),
		ReadDays                     FLOAT,
		CoinIn                       FLOAT,
		TotalDrop                    FLOAT,
		Expenses                     FLOAT,
		NetWin                       FLOAT,
		NetWinForActWinPer           FLOAT,
		Bar_Position_ID              INT,
		Bar_Position_Name            VARCHAR(100),
		Machine_Type_ID              INT,
		Machine_Type_Code            VARCHAR(100),
		Site_ID                      INT,
		Site_Name                    VARCHAR(500),
		Sub_Company_ID               INT,
		Sub_Company_Name             VARCHAR(500),
		Company_ID                   INT,
		Company_Name                 VARCHAR(500),
		Machine_Stock_No             VARCHAR(50),
		Zone_Name                    VARCHAR(500),
		Region_ID                    INT,
		Region_Name                  VARCHAR(500),
		Area_ID                      INT,
		Area_Name                    VARCHAR(500),
		District_ID                  INT,
		District_Name                VARCHAR(500),
		HoldPer                      FLOAT,
		ActWinPer                    DECIMAL(18,2),
		PerVar                       DECIMAL(18,2),
		WeightedAvgPer               FLOAT,
		installation_price_per_play  FLOAT,
		AvgCoinInPerDayPerMachine    FLOAT,
		AvgNetWinPerDayPerMachine    FLOAT,
		TheoWin                      FLOAT,
		Criteria                     VARCHAR(1000),
		currencyformatting           VARCHAR(10),
		Machine_Class_Percent_Cash_Payout INT,
		WinPer						 FLOAT
	) 
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

	SET @Zone = NULL
	                    
	DECLARE @qStartMonth INT                    
	SET @qStartMonth = CASE 
	                        WHEN MONTH(@GamingDate) BETWEEN 1 AND 3 THEN 1
	                        WHEN MONTH(@GamingDate) BETWEEN 4 AND 6 THEN 4
	                        WHEN MONTH(@GamingDate) BETWEEN 7 AND 9 THEN 7
	                        ELSE 10
	                   END                   
	
	SET @mtdStart = '01/' + CAST(
	        DATEPART(MONTH, CAST(@GamingDate AS DATETIME)) AS VARCHAR(3)
	    ) + '/' + CAST(YEAR(CAST(@GamingDate AS DATETIME)) AS VARCHAR(4))
	
	SET @qtdStart = '01/' + CAST(@qstartmonth AS VARCHAR(4)) + '/' + CAST(YEAR(CAST(@GamingDate AS DATETIME)) AS VARCHAR(4))                    
	SET @ytdStart = '01 Jan ' + CAST(YEAR(CAST(@GamingDate AS DATETIME)) AS VARCHAR(4)) 
	
	

	INSERT INTO #tmp
	  (
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    NetWinForActWinPer,
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
	    holdper,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    installation_price_per_play,
	    Machine_Class_Percent_Cash_Payout,
	    WinPer
	  )
	SELECT ReadDays = CASE 
	                       WHEN COUNT(ISNULL(VWR.Read_Days, 1)) = 0 THEN 1
	                       ELSE COUNT(ISNULL(VWR.Read_Days, 1))
	                  END,
	       CoinIn = SUM(VWR.RDCCashIn),
	       TotalDrop = CASE 
	                        WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn)
	                        ELSE (
	                                 SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                 SUM(VWR.READ_RDC_BILL_1) +
	                                 SUM(VWR.READ_RDC_BILL_2) +
	                                 SUM(VWR.READ_RDC_BILL_5) +
	                                 SUM(VWR.READ_RDC_BILL_10) +
	                                 SUM(VWR.READ_RDC_BILL_20) +
	                                 SUM(VWR.READ_RDC_BILL_50) +
	                                 SUM(VWR.READ_RDC_BILL_100) +
	                                 SUM(VWR.READ_RDC_BILL_200) +
	                                 SUM(VWR.READ_RDC_BILL_250) +
	                                 SUM(VWR.READ_RDC_BILL_500) +
	                                 SUM(VWR.READ_RDC_BILL_10000) +
	                                 SUM(VWR.READ_RDC_BILL_20000) +
	                                 SUM(VWR.READ_RDC_BILL_50000) +
	                                 SUM(VWR.READ_RDC_BILL_100000) +
	                                 SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                 CASE 
	                                      WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                      ELSE 0
	                                 END +(
	                                     CASE 
	                                          WHEN @AddPromoCashable = 'True' THEN 
	                                               SUM(VWR.Promo_Cashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     CASE 
	                                          WHEN @IncludeNonCashable = 1 THEN 
	                                               SUM(VWR.NonCashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     SUM(VWR.Cashable_EFT_IN)
	                                 )
	                             )
	                   END,
	       Expenses = CASE 
	                       WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut)
	                       ELSE (
	                                SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                SUM(VWR.READ_HANDPAY) +
	                                SUM(VWR.READ_TICKET) +
	                                CASE 
	                                     WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                     ELSE 0
	                                END +(
	                                    CASE 
	                                         WHEN @AddPromoCashable = 'True' THEN 
	                                              SUM(VWR.Promo_Cashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    CASE 
	                                         WHEN @IncludeNonCashable = 1 THEN 
	                                              SUM(VWR.NonCashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    SUM(VWR.Cashable_EFT_OUT)
	                                )
	                            )
	                  END,
	       NetWin = CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END,
	       NetWinForActWinPer = CASE 
	                                 WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                                 ELSE SUM(VWR.TotalCash)
	                            END,
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                    
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                    
	       VWGMI.Zone_Name,	-- area  
	       VWGMI.holdper,
	       ActWinPer = ((SUM(VWR.RDCCashIn)- (CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END))/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100,
	       PerVar = (VWGMI.Machine_Class_Percent_Cash_Payout) - ((SUM(VWR.RDCCashIn)- (CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END)/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100),
	       WeightedAvgPer = 0,
	       installation_price_per_play = CAST(
	           CAST(VWGMI.installation_price_per_play AS FLOAT) / 100 AS FLOAT
	       ),
	      VWGMI.Machine_Class_Percent_Cash_Payout,
	      VWGMI.WinPer
	FROM   vw_GenericMachineInformation VWGMI
	       INNER JOIN VW_Read VWR
	            ON  VWGMI.Installation_ID = VWR.Installation_No
	       INNER JOIN [Site] s WITH(NOLOCK)
	            ON  s.site_id = VWGMI.site_id 
	   
	            AND (
	                    CAST(VWR.ReadDate AS DATETIME) = CAST(@GamingDate AS DATETIME)
	                )
	WHERE  (
	           (@Denom <> 0 AND installation_price_per_play = @Denom)
	           OR @Denom = 0
	       )
	     
           AND ISNULL(@Zone, Zone_Id) =  Zone_Id  
	       AND ISNULL(@Site, VWGMI.Site_Id) = S.Site_Id
	       AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	       AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND (
	               @SiteIDList IS NOT NULL
	               AND VWGMI.Site_ID IN (SELECT DATA
	                                     FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, VWGMI.Company_id) = VWGMI.Company_id
	GROUP BY
	       VWGMI.HoldPer,
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                    
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	                    
	       VWGMI.Zone_Name,
	     
	       installation_price_per_play,
	       VWGMI.Machine_Class_Percent_Cash_Payout,
		   VWGMI.WinPer  
	

	
	UPDATE #tmp
	SET    [ORDER] = 1,
	       [TYPE] = 'D'
	WHERE  [ORDER] IS NULL 
	
	

	INSERT INTO #tmp
	  (
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    NetWinForActWinPer,
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
	    holdper,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    installation_price_per_play,
	    Machine_Class_Percent_Cash_Payout,
	    WinPer
	  )
	SELECT
	 ReadDays = CASE 
	                       WHEN COUNT(ISNULL(VWR.Read_Days, 1)) = 0 THEN 1
	                       ELSE COUNT(ISNULL(VWR.Read_Days, 1))
	                  END,
	       
	       CoinIn = SUM(VWR.RDCCashIn),
	       TotalDrop = CASE 
	                        WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn)
	                        ELSE (
	                                 SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                 SUM(VWR.READ_RDC_BILL_1) +
	                                 SUM(VWR.READ_RDC_BILL_2) +
	                                 SUM(VWR.READ_RDC_BILL_5) +
	                                 SUM(VWR.READ_RDC_BILL_10) +
	                                 SUM(VWR.READ_RDC_BILL_20) +
	                                 SUM(VWR.READ_RDC_BILL_50) +
	                                 SUM(VWR.READ_RDC_BILL_100) +
	                                 SUM(VWR.READ_RDC_BILL_200) +
	                                 SUM(VWR.READ_RDC_BILL_250) +
	                                 SUM(VWR.READ_RDC_BILL_500) +
	                                 SUM(VWR.READ_RDC_BILL_10000) +
	                                 SUM(VWR.READ_RDC_BILL_20000) +
	                                 SUM(VWR.READ_RDC_BILL_50000) +
	                                 SUM(VWR.READ_RDC_BILL_100000) +
	                                 SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                 CASE 
	                                      WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                      ELSE 0
	                                 END +(
	                                     CASE 
	                                          WHEN @AddPromoCashable = 'True' THEN 
	                                               SUM(VWR.Promo_Cashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     CASE 
	                                          WHEN @IncludeNonCashable = 1 THEN 
	                                               SUM(VWR.NonCashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     SUM(VWR.Cashable_EFT_IN)
	                                 )
	                             )
	                   END,
	       Expenses = CASE 
	                       WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut)
	                       ELSE (
	                                SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                SUM(VWR.READ_HANDPAY) +
	                                SUM(VWR.READ_TICKET) +
	                                CASE 
	                                     WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                     ELSE 0
	                                END +(
	                                    CASE 
	                                         WHEN @AddPromoCashable = 'True' THEN 
	                                              SUM(VWR.Promo_Cashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    CASE 
	                                         WHEN @IncludeNonCashable = 1 THEN 
	                                              SUM(VWR.NonCashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    SUM(VWR.Cashable_EFT_OUT)
	                                )
	                            )
	                  END,
	       NetWin = CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END,
	       NetWinForActWinPer = CASE 
	                                 WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                                 ELSE SUM(VWR.TotalCash)
	                            END,
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                    
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                    
	       VWGMI.Zone_Name,	-- area
	       VWGMI.holdper,
	       ActWinPer = ((SUM(VWR.RDCCashIn)- (CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END))/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100,
	       PerVar = (VWGMI.Machine_Class_Percent_Cash_Payout) - ((SUM(VWR.RDCCashIn)- (CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END)/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100),
	       WeightedAvgPer = 0,
	       installation_price_per_play = CAST(
	           CAST(VWGMI.installation_price_per_play AS FLOAT) / 100 AS FLOAT
	       ),
	       VWGMI.Machine_Class_Percent_Cash_Payout,
	       VWGMI.WinPer
	FROM   vw_GenericMachineInformation VWGMI
	       INNER JOIN VW_Read VWR
	            ON  VWGMI.Installation_ID = VWR.Installation_No
	       INNER JOIN [Site] s WITH(NOLOCK)
	            ON  s.site_id = VWGMI.site_id
	WHERE  (
	           (@Denom <> 0 AND installation_price_per_play = @Denom)
	           OR @Denom = 0
	       )
	           AND ISNULL(@Zone, Zone_Id) =  Zone_Id  
	       AND ISNULL(@Site, VWGMI.Site_Id) = S.Site_Id
	       AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	       AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND (
	               @SiteIDList IS NOT NULL
	               AND VWGMI.Site_ID IN (SELECT DATA
	                                     FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, VWGMI.Company_id) = VWGMI.Company_id
	GROUP BY
	       VWGMI.HoldPer,
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                    
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                    
	       VWGMI.Zone_Name,	-- area 
	       installation_price_per_play ,
	       VWGMI.Machine_Class_Percent_Cash_Payout,
	       VWGMI.WinPer
	
	--SELECT * FROM #TMP
--	RETURN
	
	UPDATE #tmp
	SET    [ORDER] = 2,
	       [TYPE] = 'M'
	WHERE  [ORDER] IS NULL 
	
	
	-- Get quarterly values   
	INSERT INTO #tmp
	  (
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    NetWinForActWinPer,
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
	    holdper,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    installation_price_per_play,
	    Machine_Class_Percent_Cash_Payout,
	    WinPer
	  )
	SELECT ReadDays = CASE 
	                       WHEN COUNT(ISNULL(VWR.Read_Days, 1)) = 0 THEN 1
	                       ELSE COUNT(ISNULL(VWR.Read_Days, 1))
	                  END,
	       CoinIn = SUM(VWR.RDCCashIn),
	       TotalDrop = CASE 
	                        WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn)
	                        ELSE (
	                                 SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                 SUM(VWR.READ_RDC_BILL_1) +
	                                 SUM(VWR.READ_RDC_BILL_2) +
	                                 SUM(VWR.READ_RDC_BILL_5) +
	                                 SUM(VWR.READ_RDC_BILL_10) +
	                                 SUM(VWR.READ_RDC_BILL_20) +
	                                 SUM(VWR.READ_RDC_BILL_50) +
	                                 SUM(VWR.READ_RDC_BILL_100) +
	                                 SUM(VWR.READ_RDC_BILL_200) +
	                                 SUM(VWR.READ_RDC_BILL_250) +
	                                 SUM(VWR.READ_RDC_BILL_500) +
	                                 SUM(VWR.READ_RDC_BILL_10000) +
	                                 SUM(VWR.READ_RDC_BILL_20000) +
	                                 SUM(VWR.READ_RDC_BILL_50000) +
	                                 SUM(VWR.READ_RDC_BILL_100000) +
	                                 SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                 CASE 
	                                      WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                      ELSE 0
	                                 END +(
	                                     CASE 
	                                          WHEN @AddPromoCashable = 'True' THEN 
	                                               SUM(VWR.Promo_Cashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     CASE 
	                                          WHEN @IncludeNonCashable = 1 THEN 
	                                               SUM(VWR.NonCashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     SUM(VWR.Cashable_EFT_IN)
	                                 )
	                             )
	                   END,
	       Expenses = CASE 
	                       WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut)
	                       ELSE (
	                                SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                SUM(VWR.READ_HANDPAY) +
	                                SUM(VWR.READ_TICKET) +
	                                CASE 
	                                     WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                     ELSE 0
	                                END +(
	                                    CASE 
	                                         WHEN @AddPromoCashable = 'True' THEN 
	                                              SUM(VWR.Promo_Cashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    CASE 
	                                         WHEN @IncludeNonCashable = 1 THEN 
	                                              SUM(VWR.NonCashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    SUM(VWR.Cashable_EFT_OUT)
	                                )
	                            )
	                  END,
	       NetWin = CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END,
	       NetWinForActWinPer = CASE 
	                                 WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                                 ELSE SUM(VWR.TotalCash)
	                            END,
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                    
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                    
	       VWGMI.Zone_Name,
	       -- area  
	       VWGMI.holdper,
	       ActWinPer = ((SUM(VWR.RDCCashIn)- CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END)/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100,
	       PerVar = (VWGMI.Machine_Class_Percent_Cash_Payout) - ((SUM(VWR.RDCCashIn)- CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100),
	       WeightedAvgPer = 0,
	       installation_price_per_play = CAST(
	           CAST(VWGMI.installation_price_per_play AS FLOAT) / 100 AS FLOAT
	       ),
	       VWGMI.Machine_Class_Percent_Cash_Payout,
	       VWGMI.WinPer
	FROM   vw_GenericMachineInformation VWGMI
	       INNER JOIN VW_Read VWR
	            ON  VWGMI.Installation_ID = VWR.Installation_No
	       INNER JOIN [Site] s WITH(NOLOCK)
	            ON  s.site_id = VWGMI.site_id
	            AND (
	                    CAST(VWR.ReadDate AS DATETIME) BETWEEN CAST(@qtdStart AS DATETIME) 
	                    AND CAST(@GamingDate AS DATETIME)
	                )
	WHERE  (
	           (@Denom <> 0 AND installation_price_per_play = @Denom)
	           OR @Denom = 0
	       )
	       AND ISNULL(@Zone, Zone_Id) =  Zone_Id  
	       AND ISNULL(@Site, VWGMI.Site_Id) = S.Site_Id
	       AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	       AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND (
	               @SiteIDList IS NOT NULL
	               AND VWGMI.Site_ID IN (SELECT DATA
	                                     FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, VWGMI.Company_id) = VWGMI.Company_id
	GROUP BY
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                  
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                  
	       VWGMI.Zone_Name,
	       -- area                  
	       VWGMI.holdper,
	       VWGMI.installation_price_per_play,
	       VWGMI.Machine_Class_Percent_Cash_Payout,
	       VWGMI.WinPer   
	
	UPDATE #tmp
	SET    [ORDER] = 3,
	       [TYPE] = 'Q'
	WHERE  [ORDER] IS NULL 
	
	-- Get yearly values   
	INSERT INTO #tmp
	  (
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    NetWinForActWinPer,
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
	    holdper,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    installation_price_per_play,
	    Machine_Class_Percent_Cash_Payout,
	    WinPer
	  )
	SELECT ReadDays = CASE 
	                       WHEN COUNT(ISNULL(VWR.Read_Days, 1)) = 0 THEN 1
	                       ELSE COUNT(ISNULL(VWR.Read_Days, 1))
	                  END,
	       CoinIn = SUM(VWR.RDCCashIn),
	       TotalDrop = CASE 
	                        WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn)
	                        ELSE (
	                                 SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                 SUM(VWR.READ_RDC_BILL_1) +
	                                 SUM(VWR.READ_RDC_BILL_2) +
	                                 SUM(VWR.READ_RDC_BILL_5) +
	                                 SUM(VWR.READ_RDC_BILL_10) +
	                                 SUM(VWR.READ_RDC_BILL_20) +
	                                 SUM(VWR.READ_RDC_BILL_50) +
	                                 SUM(VWR.READ_RDC_BILL_100) +
	                                 SUM(VWR.READ_RDC_BILL_200) +
	                                 SUM(VWR.READ_RDC_BILL_250) +
	                                 SUM(VWR.READ_RDC_BILL_500) +
	                                 SUM(VWR.READ_RDC_BILL_10000) +
	                                 SUM(VWR.READ_RDC_BILL_20000) +
	                                 SUM(VWR.READ_RDC_BILL_50000) +
	                                 SUM(VWR.READ_RDC_BILL_100000) +
	                                 SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                 CASE 
	                                      WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                      ELSE 0
	                                 END +(
	                                     CASE 
	                                          WHEN @AddPromoCashable = 'True' THEN 
	                                               SUM(VWR.Promo_Cashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     CASE 
	                                          WHEN @IncludeNonCashable = 1 THEN 
	                                               SUM(VWR.NonCashable_EFT_IN)
	                                          ELSE 0
	                                     END +
	                                     SUM(VWR.Cashable_EFT_IN)
	                                 )
	                             )
	                   END,
	       Expenses = CASE 
	                       WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut)
	                       ELSE (
	                                SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                SUM(VWR.READ_HANDPAY) +
	                                SUM(VWR.READ_TICKET) +
	                                CASE 
	                                     WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                     ELSE 0
	                                END +(
	                                    CASE 
	                                         WHEN @AddPromoCashable = 'True' THEN 
	                                              SUM(VWR.Promo_Cashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    CASE 
	                                         WHEN @IncludeNonCashable = 'True' THEN 
	                                              SUM(VWR.NonCashable_EFT_OUT)
	                                         ELSE 0
	                                    END +
	                                    SUM(VWR.Cashable_EFT_OUT)
	                                )
	                            )
	                  END,
	       NetWin = CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END,
	       NetWinForActWinPer = CASE 
	                                 WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                                 ELSE SUM(VWR.TotalCash)
	                            END,
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                    
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                    
	       VWGMI.Zone_Name,
	       VWGMI.holdper,
	       ActWinPer = ((SUM(VWR.RDCCashIn)- CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END)/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100,
	       PerVar = (VWGMI.Machine_Class_Percent_Cash_Payout) - ((SUM(VWR.RDCCashIn)- CASE 
	                     WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash)
	                     ELSE (
	                              (
	                                  SUM(VWR.READ_RDC_TRUE_COIN_IN) +
	                                  SUM(VWR.READ_RDC_BILL_1) +
	                                  SUM(VWR.READ_RDC_BILL_2) +
	                                  SUM(VWR.READ_RDC_BILL_5) +
	                                  SUM(VWR.READ_RDC_BILL_10) +
	                                  SUM(VWR.READ_RDC_BILL_20) +
	                                  SUM(VWR.READ_RDC_BILL_50) +
	                                  SUM(VWR.READ_RDC_BILL_100) +
	                                  SUM(VWR.READ_RDC_BILL_200) +
	                                  SUM(VWR.READ_RDC_BILL_250) +
	                                  SUM(VWR.READ_RDC_BILL_500) +
	                                  SUM(VWR.READ_RDC_BILL_10000) +
	                                  SUM(VWR.READ_RDC_BILL_20000) +
	                                  SUM(VWR.READ_RDC_BILL_50000) +
	                                  SUM(VWR.READ_RDC_BILL_100000) +
	                                  SUM(VWR.READ_TICKET_IN_SUSPENSE) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_INSERTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_IN)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_IN)
	                              )
	                              -(
	                                  SUM(VWR.READ_RDC_TRUE_COIN_OUT) +
	                                  SUM(VWR.READ_HANDPAY) +
	                                  SUM(VWR.READ_TICKET) +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.TICKETS_PRINTED_NONCASHABLE_VALUE)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @AddPromoCashable = 'True' THEN 
	                                            SUM(VWR.Promo_Cashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  CASE 
	                                       WHEN @IncludeNonCashable = 1 THEN SUM(VWR.NonCashable_EFT_OUT)
	                                       ELSE 0
	                                  END +
	                                  SUM(VWR.Cashable_EFT_OUT)
	                              )
	                          )
	                END/CASE SUM(VWR.RDCCashIn) WHEN 0 THEN 1 ELSE SUM(VWR.RDCCashIn) END) * 100),
	       WeightedAvgPer = 0,
	       installation_price_per_play = CAST(
	           CAST(VWGMI.installation_price_per_play AS FLOAT) / 100 AS FLOAT
	       ),
	       VWGMI.Machine_Class_Percent_Cash_Payout,
	       VWGMI.WinPer
	FROM   vw_GenericMachineInformation VWGMI
	       INNER JOIN VW_Read VWR
	            ON  VWGMI.Installation_ID = VWR.Installation_No
	       INNER JOIN [Site] s WITH(NOLOCK)
	            ON  s.site_id = VWGMI.site_id
	            AND (
	                    CAST(VWR.ReadDate AS DATETIME) BETWEEN CAST(@ytdStart AS DATETIME) 
	                    AND CAST(@GamingDate AS DATETIME)
	                )
	WHERE  (
	           (@Denom <> 0 AND installation_price_per_play = @Denom)
	           OR @Denom = 0
	)
	      AND ISNULL(@Zone, Zone_Id) = Zone_Id
	       AND ISNULL(@Site, VWGMI.Site_Id) = S.Site_Id
	       AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	       AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND (
	               @SiteIDList IS NOT NULL
	               AND VWGMI.Site_ID IN (SELECT DATA
	                                     FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, VWGMI.Company_id) = VWGMI.Company_id
	GROUP BY
	       VWGMI.Bar_Position_ID,
	       VWGMI.Bar_Position_Name,	-- stand                  
	       VWGMI.Machine_Type_ID,
	       VWGMI.Machine_Type_Code,
	       VWGMI.Site_ID,
	       VWGMI.Site_Name,
	       VWGMI.Sub_Company_ID,
	       VWGMI.Sub_Company_Name,
	       VWGMI.Company_ID,
	       VWGMI.Company_Name,
	       VWGMI.Machine_Stock_No,	-- slot                  
	       VWGMI.Zone_Name,	-- area    
	       VWGMI.holdper,
	       VWGMI.installation_price_per_play,
	       VWGMI.Machine_Class_Percent_Cash_Payout,
	       VWGMI.WinPer
	
	
	UPDATE #tmp
	SET    [ORDER] = 4,
	       [TYPE] = 'Y'
	WHERE  [ORDER] IS NULL 
	-- Select Final Result Set
	-- Details  
	SELECT 1 AS 'SortOrder',
	       [Order],
	       [Type],
	       ReadDays,
	       CoinIn,
	       TotalDrop,
	       Expenses,
	       NetWin,
	       NetWinForActWinPer,
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
	       HoldPer,
	       ActWinPer,
	       (CAST(Machine_Class_Percent_Cash_Payout AS FLOAT)) - (ActWinPer) AS PerVar,
	       --PerVar,
	       WeightedAvgPer = (
	           (
	               CAST(
	                   ((CoinIn * HoldPer) / 100) / CASE 
	                                                     WHEN ReadDays = 0 THEN 
	                                                          1
	                                                     ELSE ReadDays
	                                                END AS FLOAT
	               )
	           ) /
	           CASE 
	                WHEN (
	                         CAST(
	                             CoinIn / CASE 
	                                           WHEN ReadDays = 0 THEN 1
	                                           ELSE ReadDays
	                                      END AS FLOAT
	                         )
	                     ) = 0 THEN 1
	                ELSE (
	                         CAST(
	                             CoinIn / CASE 
	                                           WHEN ReadDays = 0 THEN 1
	                                           ELSE ReadDays
	                                      END AS FLOAT
	                         )
	                     )
	           END
	       ) * 100,
	       installation_price_per_play,
	       AvgCoinInPerDayPerMachine = CAST(
	           CoinIn / CASE 
	                         WHEN ReadDays = 0 THEN 1
	                         ELSE ReadDays
	                    END AS FLOAT
	       ),
	       AvgNetWinPerDayPerMachine = CAST(
	           NetWin / CASE 
	                         WHEN ReadDays = 0 THEN 1
	                         ELSE ReadDays
	                    END AS FLOAT
	       ),
	       TheoWin = WinPer
	       
	       INTO 
	       #tmpActual
	FROM   #tmp
	--ORDER BY Bar_Position_ID, [Order]
	-- Sub Total for Site & Denom  
	SELECT Site_Name,
	       [Type],
	       ReadDays = SUM(ReadDays),
	       CoinIn = SUM(CoinIn),
	       TotalDrop = SUM(TotalDrop),
	       Expenses = SUM(Expenses),
	       NetWin = SUM(NetWin),
	       HoldPer = AVG(HoldPer),
	       --ActWinPer = (SUM(CoinIn)- SUM(NetWin)/CASE SUM(NetWin) WHEN 0 THEN 1 ELSE SUM(CoinIn) END) * 100,  
			ActWinPer = (CASE SUM(CoinIn) WHEN 0 THEN 0 
							  ELSE 
								((SUM(CoinIn)- SUM(NetWin))/CASE SUM(NetWin) WHEN 0 THEN 1 ELSE SUM(CoinIn) END)
						END) * 100, 
			--PerVar              = dbo.fnSDSActualWinPercentage ( SUM(NetWinForActWinPer), SUM(CoinIn)) - AVG(HoldPer),      
			--PerVar =  ((SUM(CAST(Machine_Class_Percent_Cash_Payout AS FLOAT)) / COUNT(Bar_Position_ID))) - ((SUM(CoinIn)- SUM(NetWin)/CASE SUM(NetWin) WHEN 0 THEN 1 ELSE SUM(CoinIn) END) * 100),  
			PerVar =  ((SUM(CAST(Machine_Class_Percent_Cash_Payout AS FLOAT)) / COUNT(Bar_Position_ID))) - 
					   (CASE SUM(CoinIn) WHEN 0 THEN 0 
							  ELSE 
								((SUM(CoinIn)- SUM(NetWin))/CASE SUM(NetWin) WHEN 0 THEN 1 ELSE SUM(CoinIn) END)
						END) * 100,  
	       WeightedAvgPer = (
	           (
	               CAST(
	                   ((SUM(CoinIn) * AVG(HoldPer)) / 100) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	                   AS FLOAT
	               ) / COUNT(DISTINCT Bar_Position_ID)
	           ) 
	           / CASE 
	                  WHEN (
	                           (
	                               CAST(
	                                   SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) AS FLOAT
	                               ) / COUNT(DISTINCT Bar_Position_ID)
	                           )
	                       ) = 0 THEN 1
	                  ELSE (
	                           CAST(
	                               SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	                               AS FLOAT
	                           ) / COUNT(DISTINCT Bar_Position_ID)
	                       )
	             END
	       ) * 100,
	       installation_price_per_play = installation_price_per_play,
	       AvgCoinInPerDayPerMachine = CAST(
	           SUM(CoinIn) / SUM(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	           AS FLOAT
	       ),
	       AvgNetWinPerDayPerMachine = CAST(
	           SUM(NetWin) / SUM(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	           AS FLOAT
	       ),
	       TheoWin = (SUM(WinPer) / COUNT(Bar_Position_ID))
	       INTO #tmpSubTotalDenom
	FROM   #tmp
	GROUP BY
	       #tmp.Site_Name,
	       #tmp.[Type],
	       #tmp.installation_price_per_play
	--ORDER BY Bar_Position_ID, [Order]    
		
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Type],
	    Site_Name,
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    HoldPer,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    installation_price_per_play,
	    AvgCoinInPerDayPerMachine,
	    AvgNetWinPerDayPerMachine,
	    TheoWin
	  )
	SELECT 2,
	       [Type],
	       Site_Name,
	       ReadDays,
	       CoinIn,
	       TotalDrop,
	       Expenses,
	       NetWin,
	       HoldPer,
	       ActWinPer,
	       PerVar,
	       WeightedAvgPer,
	       installation_price_per_play,
	       AvgCoinInPerDayPerMachine,
	       AvgNetWinPerDayPerMachine,
	       TheoWin
	FROM   #tmpSubTotalDenom 
	
	-- Sub Total for Site  
	SELECT Site_Name,
	       [Type],
	       ReadDays = SUM(ReadDays),
	       CoinIn = SUM(CoinIn),
	       TotalDrop = SUM(TotalDrop),
	       Expenses = SUM(Expenses),
	       NetWin = SUM(NetWin),
	       HoldPer = AVG(HoldPer),
	       ActWinPer = ((SUM(CoinIn)- SUM(NetWin))/CASE SUM(CoinIn) WHEN 0 THEN 1 ELSE SUM(CoinIn) END) * 100,
	       --PerVar              = dbo.fnSDSActualWinPercentage ( SUM(NetWinForActWinPer), SUM(CoinIn)) - AVG(HoldPer),    
	       PerVar = ((SUM(CAST(Machine_Class_Percent_Cash_Payout AS FLOAT)) / COUNT(Bar_Position_ID)))- ((SUM(CoinIn)- SUM(NetWin)/CASE SUM(CoinIn) WHEN 0 THEN 1 ELSE SUM(CoinIn) END) * 100),
	       WeightedAvgPer = (
	           (
	               CAST(
	                   ((SUM(CoinIn) * AVG(HoldPer)) / 100) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	                   AS FLOAT
	               ) / COUNT(DISTINCT Bar_Position_ID)
	           ) 
	           / CASE 
	                  WHEN (
	                           (
	                               CAST(
	                                   SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) AS FLOAT
	                               ) / COUNT(DISTINCT Bar_Position_ID)
	                           )
	                       ) = 0 THEN 1
	                  ELSE (
	                           CAST(
	                               SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	                               AS FLOAT
	                           ) / COUNT(DISTINCT Bar_Position_ID)
	                       )
	             END
	       ) * 100,
	       AvgCoinInPerDayPerMachine = CAST(
	           SUM(CoinIn) / SUM(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	           AS FLOAT
	       ),
	       AvgNetWinPerDayPerMachine = CAST(
	           SUM(NetWin) / SUM(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	           AS FLOAT
	       ),
	       TheoWin = (SUM(WinPer) / COUNT(Bar_Position_ID))
	       INTO #tmpSubTotalSite
	FROM   #tmp
	GROUP BY
	       #tmp.Site_Name,
	       [Type] 
	--ORDER BY Bar_Position_ID, [Order]          
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Type],
	    Site_Name,
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    HoldPer,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    installation_price_per_play,
	    AvgCoinInPerDayPerMachine,
	    AvgNetWinPerDayPerMachine,
	    TheoWin
	  )
	SELECT 3,
	       [Type],
	       Site_Name,
	       ReadDays,
	       CoinIn,
	       TotalDrop,
	       Expenses,
	       NetWin,
	       HoldPer,
	       ActWinPer,
	       PerVar,
	       WeightedAvgPer,
	       99999,
	       AvgCoinInPerDayPerMachine,
	       AvgNetWinPerDayPerMachine,
	       TheoWin
	FROM   #tmpSubTotalSite 
	-- Grand Total  
	INSERT INTO #tmpActual
	  (
	    SortOrder,
	    [Type],
	    ReadDays,
	    CoinIn,
	    TotalDrop,
	    Expenses,
	    NetWin,
	    HoldPer,
	    ActWinPer,
	    PerVar,
	    WeightedAvgPer,
	    AvgCoinInPerDayPerMachine,
	    AvgNetWinPerDayPerMachine,
	    TheoWin
	  )
	SELECT 4,
	       [Type],
	       ReadDays = SUM(ReadDays),
	       CoinIn = SUM(CoinIn),
	       TotalDrop = SUM(TotalDrop),
	       Expenses = SUM(Expenses),
	       NetWin = SUM(NetWin),
	       HoldPer = AVG(HoldPer),
	       ActWinPer = ((SUM(CoinIn) - SUM(NetWin))/CASE SUM(CoinIn) WHEN 0 THEN 1 ELSE SUM(CoinIn) END) * 100,
	       --PerVar              = dbo.fnSDSActualWinPercentage ( SUM(NetWinForActWinPer), SUM(CoinIn)) - AVG(HoldPer),    
	       PerVar = ((SUM(CAST(Machine_Class_Percent_Cash_Payout AS FLOAT)) / COUNT(Bar_Position_ID))) - ((SUM(CoinIn) - SUM(NetWin)/CASE SUM(CoinIn) WHEN 0 THEN 1 ELSE SUM(CoinIn) END) * 100),
	       WeightedAvgPer = (
	           (
	               CAST(
	                   ((SUM(CoinIn) * AVG(HoldPer)) / 100) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	                   AS FLOAT
	               ) / COUNT(DISTINCT Bar_Position_ID)
	           ) 
	           / CASE 
	                  WHEN (
	                           (
	                               CAST(
	                                   SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) AS FLOAT
	                               ) / COUNT(DISTINCT Bar_Position_ID)
	                           )
	                       ) = 0 THEN 1
	                  ELSE (
	                           CAST(
	                               SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	                               AS FLOAT
	                           ) / COUNT(DISTINCT Bar_Position_ID)
	                       )
	             END
	       ) * 100,
	       AvgCoinInPerDayPerMachine = CAST(
	           SUM(CoinIn) / SUM(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	           AS FLOAT
	       ),
	       AvgNetWinPerDayPerMachine = CAST(
	           SUM(NetWin) / SUM(CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END) 
	           AS FLOAT
	       ),
	       TheoWin = (SUM(WinPer) / COUNT(Bar_Position_ID))	       
	FROM   #tmp
	GROUP BY
	       [Type] 
	--ORDER BY Bar_Position_ID, [Order]   
	
	SELECT *
	FROM   #tmpActual
	ORDER BY
	       SortOrder,
	       Bar_Position_ID,
	       [Order] 
	
 DROP TABLE #tmpActual  
 DROP TABLE #tmpSubTotalSite
 DROP TABLE #tmpSubTotalDenom
 DROP TABLE #tmp 
END       

GO
