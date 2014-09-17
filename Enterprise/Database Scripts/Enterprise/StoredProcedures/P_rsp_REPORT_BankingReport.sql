USE Enterprise
GO


IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  NAME = 'rsp_REPORT_BankingReport'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_REPORT_BankingReport
END


GO 

  /**    
  C.Taylor   20/09/06 LEFT joined machine_type .. was missing machines from report  
        any machines not assigned to a machine type, displayed as UNCATEGORISED  
  Rakesh Marwaha 18/06/08 Added @calcStartDate and @calcEndDate in case of SSRS  
  Rakesh Marwaha 19/08/08 Added 10000 and 100 column value for US setting  
  Anuradha      09/04/09    Added Progressive Win Declared Value to handpay  
  Vineetha Mathew 27/02/2010 used Cast() instead of Convert(datetime)  
  Sudarsan S  15/06/2010 for Bills 200 and 500  
  Jisha Lenu George 08/12/2010  Added EFTIn & EFTOut in the select query  
  A.Vinod Kumar  14/12/2010  Modified EFTIn & EFTOut for CR92027  
        Non cashable Tickets In and Out also retrieved.  
  A.Vinod Kumar  22/12/2010  Installation_Price_Per_Play removed from EFTIn and EFTOut calculation  
  Jisha Lenu George 10/01/2011 Modified the EFTIn to get the values from declared. Fix for #93429  
    exec rsp_REPORT_BankingReport @company=4,@subcompany=0,@region=0,@area=0,@district=0,@site=0,@zone=0,@category=0,@startdate=N'2013-5-20 13:28:34',@enddate=N'2013-8-21 13:28:34',@IncludeNonCashable=0,@GROUPBYZONE=0
    
   
**/ 
    
CREATE PROCEDURE [dbo].[rsp_REPORT_BankingReport]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Zone VARCHAR(50) = NULL,
	@Category INT = 0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@IncludeNonCashable BIT,
	@GroupByZone BIT,
	@SiteIDList Varchar(MAX)
AS
BEGIN
	DECLARE @AddShortpay VARCHAR(500)        
	SELECT @AddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'      
	
	DECLARE @eRegion VARCHAR(2)          
	SELECT @eRegion = UPPER(RIGHT(System_Parameter_Region_Culture, 2))
	FROM   System_Parameters          
	
	
	DECLARE @calcStartDate  DATETIME,
	        @calcEndDate    DATETIME          
	
	SET DATEFORMAT dmy                  
	SET @calcStartDate = CAST(@startdate AS DATETIME)                  
	SET @calcEndDate = CAST(@enddate AS DATETIME)           
	
	DECLARE @SGVI_Enabled VARCHAR(10)              
	SELECT @SGVI_Enabled = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'SGVi_Enabled'           
	
	DECLARE @SGVI_autodeclare VARCHAR(10)              
	SELECT @SGVI_autodeclare = Setting_Value
	FROM   Setting
	WHERE  Setting_Name = 'Auto_Declare_Monies'          
	
	DECLARE @AddPromoCashable VARCHAR(10)              
	SET @AddPromoCashable = 'False'              
	SELECT @AddPromoCashable = (COALESCE(RTRIM(LTRIM(Setting_Value)), ''))
	FROM   Setting
	WHERE  Setting_Name = 'Add_PromoCashable_In_WinLoss'         
	
	DECLARE @IncludeRareBills VARCHAR(10)              
	SET @IncludeRareBills = 'False'              
	SELECT @IncludeRareBills = (COALESCE(RTRIM(LTRIM(Setting_Value)), ''))
	FROM   Setting
	WHERE  Setting_Name = 'IncludeRareBills'        
	
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
	
	IF @zone =0  SET @zone =NULL
	
	IF @category = 0
	    SET @category = NULL 
	
	;WITH FilteredInstallation 
	AS 
	(
	    SELECT I.installation_ID,
	           I.Machine_ID,
	           c.company_id,
	           s.sub_company_id,
	           s.sub_company_region_id,
	           s.sub_company_area_id,
	           s.sub_company_district_id,
	           s.site_id,
	           s.site_name,
	           s.Region,
	           ISNULL(MT.Machine_Type_Id, 0) AS Machine_Type_Id,
	           ISNULL(MT.machine_Type_Code, @NOTSET) AS Machine_Type_Code,
	           ISNULL(MT.Machine_Type_Description, @NOTSET) AS 
	           Machine_Type_Description,
	           I.Installation_Price_Per_Play,
	           BP.Bar_Position_Name AS PosName,
	           M.machine_stock_no,
	           ISNULL(Z.Zone_Name, 'NOT SET') AS Zone_Name
	    FROM   Installation I WITH(NOLOCK)
	           INNER JOIN Bar_Position BP WITH(NOLOCK)
	                ON  BP.Bar_Position_ID = I.Bar_Position_ID
	           INNER JOIN MACHINE M WITH(NOLOCK)
	                ON  M.machine_id = I.machine_id
	           LEFT JOIN Machine_Type MT WITH(NOLOCK)
	                ON  MT.machine_type_id = M.machine_category_id
	           INNER JOIN [Site] S WITH(NOLOCK)
	                ON  S.Site_ID = BP.Site_ID
	           INNER JOIN Sub_Company SC WITH(NOLOCK)
	                ON  S.sub_company_id = SC.sub_company_id
	           INNER JOIN Company C WITH(NOLOCK)
	                ON  SC.company_id = C.company_id
	           LEFT JOIN zone Z WITH(NOLOCK)
	                ON  Z.Zone_ID = BP.Zone_ID
	    WHERE  ISNULL(@company, c.company_id) = c.company_id
	           AND ISNULL(@subcompany, S.sub_company_id) = S.sub_company_id
	           AND ISNULL(@region, S.sub_company_region_id) = S.sub_company_region_id
	           AND ISNULL(@area, S.sub_company_area_id) = S.sub_company_area_id
	           AND ISNULL(@district, S.sub_company_district_id) = S.sub_company_district_id	           
	           AND @SiteIDList IS NOT NULL AND S.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	           AND 
				( 
					( @zone IS NULL )               
					OR            
					( @zone IS NOT NULL             
					AND            
					Z.Zone_ID = @zone
					)            
				)  
	           AND ISNULL(@category, MT.machine_type_id) = MT.machine_type_id
	), 
	GameTitleDetails AS (
	    SELECT I.installation_ID,
	           --CASE
	           --	 WHEN COUNT(*) > 1 THEN 'Multi Game'
	           --	 ELSE ISNULL(MAX(GT.Game_Title), 'Unassigned GameTitle')
	           --END 
(
	    CASE 
	    WHEN mc.Machine_Name   = 'MULTI GAME' THEN               
        ISNULL(MGMP.Multigamename, 'MULTI GAME')
	    ELSE  mc.Machine_Name  
	    END
	     ) 
     AS GameTitle
	    FROM   FilteredInstallation I 
	           --INNER JOIN dbo.Installation_game_info IGI
	           --	 ON  IGI.Installation_No = I.Installation_ID
	           --INNER JOIN dbo.Game_Library GL
	           --	 ON  IGI.Game_Name = GL.MG_Game_Name
	           --	 AND GL.Game_Part_Number = IGI.Game_Part_Number
	           --INNER JOIN dbo.Game_Title GT
	           --	 ON  GT.Game_Title_ID = GL.MG_Group_ID    
	           
	           INNER JOIN MACHINE M
	                ON  M.Machine_ID = I.Machine_ID
	           INNER JOIN Machine_Class mc
	                ON  mc.Machine_Class_ID = M.Machine_Class_ID
LEFT JOIN MultiGameMapping MGMP ON MGMP.Machineid=M.Machine_ID      
	    GROUP BY
	           I.installation_ID,
               MGMP.Multigamename,
	           mc.Machine_Name
	), 
	CollectionDetails AS(
	    SELECT FI.installation_ID,
	           FI.Company_Id,
	           FI.Sub_Company_Id,
	           FI.Sub_Company_Region_Id,
	           FI.Sub_Company_Area_Id,
	           FI.Sub_Company_District_Id,
	           FI.Site_Id,
	           FI.Site_Name,
	           FI.Machine_Type_Id,
	           FI.Machine_Type_Code,
	           FI.Machine_Type_Description,
	           FI.Machine_Stock_No,
	           FI.PosName,
	           ISNULL(FI.Zone_Name, 'NOT SET') AS Zone_Name,
	           C.Collection_ID,
	           ISNULL(CAST(C.CashCollected AS decimal(12,2)), 0) AS CashCollected,
	           ISNULL(CAST(C.Cash_Collected_50000p AS FLOAT), 0) / 100 AS 
	           Cash_Collected_50000p,
	           ISNULL(CAST(C.Cash_Collected_20000p AS FLOAT), 0) / 100 AS 
	           Cash_Collected_20000p,
	           ISNULL(CAST(C.Cash_Collected_10000p AS FLOAT), 0) / 100 AS 
	           Cash_Collected_10000p,
	           ISNULL(CAST(C.Cash_Collected_5000P AS FLOAT), 0) / 100 AS 
	           Cash_Collected_5000P,
	           ISNULL(CAST(C.Cash_Collected_2000P AS FLOAT), 0) / 100 AS 
	           Cash_Collected_2000P,
	           ISNULL(CAST(C.Cash_Collected_1000P AS FLOAT), 0) / 100 AS 
	           Cash_Collected_1000P,
	           ISNULL(CAST(C.Cash_Collected_500P AS FLOAT), 0) / 100 AS 
	           Cash_Collected_500P,
	           CASE 
	                WHEN FI.Region = 'AR' THEN ISNULL(CAST(C.Cash_Collected_200P AS FLOAT), 0) 
	                     / 100
	                ELSE ISNULL(CAST(C.Cash_Collected_100P AS FLOAT), 0) / 100
	           END AS Cash_collected_100p,
	           ISNULL(CAST(CC.Collection_Declared_Tickets AS DECIMAL(12,2)), 0) AS 
	           Declared_Total_Tickets,
	           --Modified for Data mismatch issue        
	           
	           
	           
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN CAST(
	                         ISNULL(CAST(C.DecCashableInsertedValue AS FLOAT), 0) 
	                         AS FLOAT
	                     ) +
	                     CAST(
	                         ISNULL(CAST(C.DecNonCashableInsertedValue AS FLOAT), 0) 
	                         AS FLOAT
	                     )
	                ELSE CAST(
	                         ISNULL(CAST(C.DecCashableInsertedValue AS FLOAT), 0) 
	                         AS FLOAT
	                     )
	           END AS Declared_Tickets,
	           (CC.Collection_ManualRefills) AS ManualRefills,
	           (
	               CASE 
	                    WHEN @IncludeNonCashable = 1 THEN CAST(
	                             ISNULL(CAST(C.DecCashablePrintedValue AS FLOAT), 0) 
	                             AS FLOAT
	                         ) +
	                         CAST(
	                             ISNULL(CAST(C.DecNonCashablePrintedValue AS FLOAT), 0) 
	                             AS FLOAT
	                         )
	                    ELSE CAST(
	                             ISNULL(CAST(C.DecCashablePrintedValue AS FLOAT), 0) 
	                             AS FLOAT
	                         )
	               END
	           ) AS Tickets_Printed,
	           (CC.Collection_Handpay_Var) AS Collection_Handpay_Var,
	           (
	               COALESCE(
	                   (
	                       CAST(C.Collection_rdc_jackpot AS FLOAT) *
	                       Installation_Price_Per_Play
	                   ),
	                   0
	               ) / 100
	           ) AS Collection_RDC_Jackpot,
	           COALESCE(
	               CAST(C.Collection_RDC_Handpay AS FLOAT) *
	               Installation_Price_Per_Play,
	               0
	           ) / 100 AS Collection_RDC_Handpay,
	           (CC.Collection_Note_Var) AS Note_Var,
	           (CC.Collection_Coin_Var) AS Collection_Coin_Var,
	           (CC.Collection_RDC_Tickets_In) AS RDC_Tickets_In,
	           (CC.Collection_RDC_Tickets_Out) AS RDC_Tickets_Out,
	           (CC.Collection_Cash_Take) AS Collection_Cash_Take,
	           (CC.Collection_RDC_Take) AS Collection_RDC_Take,
	           (CC.Collection_Refills) AS Collection_Refills,
	           (CC.Collection_Declared_Notes) AS Collection_Declared_Notes,
	           (CC.Collection_DecHandpay) AS Collection_DecHandpay,
	           (CAST(ISNULL(C.DeclaredTicketValue, 0) AS FLOAT)) AS 
	           DeclaredTicketValue,
	           (CAST(ISNULL(C.DeclaredTicketPrintedValue, 0) AS FLOAT)) AS 
	           DeclaredTicketPrintedValue,
	           (CC.Collection_Net_Coin) AS Collection_Net_Coin,
	           (CC.Collection_RDC_Notes) AS Collection_RDC_Notes,
	           (
	               CAST(
	                   ISNULL(CAST(C.COLLECTION_RDC_TICKETS_INSERTED_VALUE AS FLOAT), 0) 
	                   / 100 AS FLOAT
	               )
	           ) AS COLLECTION_RDC_TICKETS_INSERTED_VALUE,
	           (
	               CASE 
	                    WHEN @IncludeNonCashable = 1 THEN CAST(
	                             ISNULL(CAST(C.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT), 0) 
	                             / 100 AS FLOAT
	                         )
	                    ELSE 0
	               END
	           ) AS RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
	           (
	               CAST(
	                   ISNULL(CAST(C.COLLECTION_RDC_TICKETS_PRINTED_VALUE AS FLOAT), 0) 
	                   / 100 AS FLOAT
	               )
	           ) AS COLLECTION_RDC_TICKETS_PRINTED_VALUE,
	           (
	               CASE 
	                    WHEN @IncludeNonCashable = 1 THEN CAST(
	                             ISNULL(CAST(C.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE AS FLOAT), 0) 
	                             / 100 AS FLOAT
	                         )
	                    ELSE 0
	               END
	           ) AS RDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
	           (CC.Collection_RDCHandpay) AS Collection_RDCHandpay,
	           (CC.Collection_RDC_Coins) AS Collection_RDC_Coins,
	           (CC.Collection_RDC_Coins_Out) AS CoinsOut,
	           CASE 
	                WHEN @AddPromoCashable = 'True' THEN (
	                         CAST(ISNULL(C.Promo_Cashable_EFT_In, 0) AS DECIMAL(18, 2)) 
	                         / 100
	                     )
	                ELSE 0
	           END AS Promo_Cashable_EFT_In,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN (
	                         CAST(ISNULL(C.NonCashable_EFT_In, 0) AS DECIMAL(18, 2)) 
	                         / 100
	                     )
	                ELSE 0
	           END AS NonCashable_EFT_In,
	           (CAST(ISNULL(C.Cashable_EFT_In, 0) AS DECIMAL(18, 2)) / 100) AS 
	           Cashable_EFT_In,
	           CASE 
	                WHEN @AddPromoCashable = 'True' THEN (
	                         CAST(ISNULL(C.Promo_Cashable_EFT_Out, 0) AS DECIMAL(18, 2)) 
	                         / 100
	                     )
	                ELSE 0
	           END AS Promo_Cashable_EFT_Out,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN (
	                         CAST(ISNULL(C.NonCashable_EFT_Out, 0) AS DECIMAL(18, 2)) 
	                         / 100
	                     )
	                ELSE 0
	           END AS NonCashable_EFT_Out,
	           (
	               CAST(ISNULL(C.Cashable_EFT_Out, 0) AS DECIMAL(18, 2)) / 100
	           ) AS Cashable_EFT_Out,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN CAST(
	                         ISNULL(CAST(C.DecNonCashableInsertedValue AS FLOAT), 0) 
	                         AS FLOAT
	                     )
	                ELSE 0
	           END AS DecNonCashableInsertedValue,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN CAST(
	                         ISNULL(CAST(C.DecNonCashablePrintedValue AS FLOAT), 0) 
	                         AS FLOAT
	                     )
	                ELSE 0
	           END AS DecNonCashablePrintedValue,
	           CAST(
	               ISNULL(CAST(C.DecCashableInsertedValue AS FLOAT), 0) AS FLOAT
	           ) AS DecCashableInsertedValue,
	           CAST(
	               ISNULL(CAST(C.DecCashablePrintedValue AS FLOAT), 0) AS FLOAT
	           ) AS DecCashablePrintedValue,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN C.DecNonCashableInsertedQty
	                ELSE 0
	           END AS DecNonCashableInsertedQty,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN C.DecNonCashablePrintedQty
	                ELSE 0
	           END AS DecNonCashablePrintedQty,
	           C.DecCashableInsertedQty AS DecCashableInsertedQty,
	           C.DecCashablePrintedQty AS DecCashablePrintedQty,
	           CAST(ISNULL(C.progressive_win_value, 0) AS DECIMAL(18, 2)) AS 
	           Progressive_Win_Value,
	           CAST(ISNULL(C.Progressive_Value_Declared, 0) AS DECIMAL(18, 2)) 
	           Progressive_Value_Declared,
	           CC.Collection_Take_Var AS Take_var,
	           C.Collection_Treasury_Handpay AS Collection_Handpay,
	           C.Collection_RDC_Handpay AS RDC_HandPay,
	           C.COLLECTION_RDC_JACKPOT AS RDC_Jackpot,
	           C.progressive_win_value AS RDC_Progressive
	    FROM   COLLECTION C WITH(NOLOCK)
	           INNER JOIN COLLECTION_CALCS CC WITH(NOLOCK)
	                ON  CC.COLLECTION_ID = C.COLLECTION_ID
	           INNER JOIN FilteredInstallation FI WITH(NOLOCK)
	                ON  FI.installation_ID = C.installation_ID
	    WHERE  CAST(C.Collection_Date AS DATETIME) BETWEEN CONVERT(VARCHAR(30), @calcStartDate, 106) 
	           AND CONVERT(VARCHAR(30), @calcEndDate, 106)
	), 
	
	
	VOID AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type = 'VOID'
	    GROUP BY
	           CD.Collection_ID
	), 
	
	Refund AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type = 'Refund'
	    GROUP BY
	           CD.Collection_ID
	), 
	
	ShortPay AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('Shortpay', 'Offline Voucher-Shortpay')
	    GROUP BY
	           CD.Collection_ID
	), 
	
	AttendantPay AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('handpay credit', 'handpay jackpot', 
	                            'mystery jackpot', 'Attendantpay Credit', 
	                            'Attendantpay Jackpot', 'PROGRESSIVE', 'PROG')
	    GROUP BY
	           CD.Collection_ID
	), 
	
	
	ManualAttendantPay AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('handpay credit', 'Attendantpay Credit')
	           AND IsManualAttendantPay = 1
	    GROUP BY
	           CD.Collection_ID
	), 
	
	
	
	MachineAttendantPay AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('handpay credit', 'Attendantpay Credit')
	           AND IsManualAttendantPay = 0
	    GROUP BY
	           CD.Collection_ID
	), 
	
	
	ManualJackpot AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('handpay jackpot', 'mystery jackpot', 
	                            'Attendantpay Jackpot')
	           AND IsManualAttendantPay = 1
	    GROUP BY
	           CD.Collection_ID
	), 
	
	MachineJackpot AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('handpay jackpot', 'mystery jackpot', 
	                            'Attendantpay Jackpot')
	           AND IsManualAttendantPay = 0
	    GROUP BY
	           CD.Collection_ID
	), 
	
	ManualProgressive AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('PROGRESSIVE', 'PROG')
	           AND IsManualAttendantPay = 1
	    GROUP BY
	           CD.Collection_ID
	), 
	
	MachineProgressive AS (
	    SELECT CD.Collection_ID,
	           SUM(Treasury_Amount) AS Amount
	    FROM   Treasury_Entry TE WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = TE.Collection_ID
	    WHERE  Treasury_Type IN ('PROGRESSIVE', 'PROG')
	           AND IsManualAttendantPay = 0
	    GROUP BY
	           CD.Collection_ID
	), 
	
	
	DecEftIn AS 
	
	
	(
	    SELECT Aft.Collection_No,
	           SUM(
	               CASE 
	                    WHEN @AddPromoCashable = 'True' THEN ISNULL(Aft.Promo_Cashable_EFT_OUT, 0) 
	                         / 100
	                    ELSE 0
	               END +
	               CASE 
	                    WHEN @IncludeNonCashable = 1 THEN ISNULL(Aft.NonCashable_EFT_OUT, 0) 
	                         / 100
	                    ELSE 0
	               END 
	               + ISNULL(Aft.WAT_Out, 0) / 100
	           ) AS Amount
	    FROM   AFT_TRANSACTIONS Aft WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = Aft.Collection_No
	    WHERE  Transaction_Type = 'WithDrawal Complete'
	    GROUP BY
	           Aft.Collection_No
	), 
	
	DecEftOut AS 
	
	
	(
	    SELECT Aft.Collection_No,
	           SUM(
	               CASE 
	                    WHEN @AddPromoCashable = 'True' THEN ISNULL(Aft.Promo_Cashable_EFT_OUT, 0) 
	                         / 100
	                    ELSE 0
	               END +
	               CASE 
	                    WHEN @IncludeNonCashable = 1 THEN ISNULL(Aft.NonCashable_EFT_OUT, 0) 
	                         / 100
	                    ELSE 0
	               END +
	               ISNULL(Aft.WAT_Out, 0) / 100
	           ) AS Amount
	    FROM   AFT_TRANSACTIONS Aft WITH(NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = Aft.Collection_No
	    WHERE  Transaction_Type = 'Deposit Complete'
	    GROUP BY
	           Aft.Collection_No
	) , 
	
	PromoCashableTickets AS
	
	(
	    SELECT CT.CT_Inserted_Collection_ID,
	           CAST(SUM(ISNULL(CT.CT_Value, 0)) AS DECIMAL(18, 2)) AS 
	           PromoCashableAmount
	    FROM   Collection_Ticket CT WITH (NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = CT.CT_Inserted_Collection_ID
	    WHERE  CT.CT_TicketType = 0
	           AND CT.CT_IsPromotionalTicket = 1
	    GROUP BY
	           CT.CT_Inserted_Collection_ID
	),
	
	
	
	PromoNonCashableTickets AS
	(
	    SELECT CT.CT_Inserted_Collection_ID,
	           CASE 
	                WHEN @IncludeNonCashable = 1 THEN CAST(SUM(ISNULL(CT.CT_Value, 0)) AS DECIMAL(18, 2))
	                ELSE 0
	           END AS PromoNonCashableAmount
	    FROM   Collection_Ticket CT WITH (NOLOCK)
	           INNER JOIN CollectionDetails CD
	                ON  CD.Collection_ID = CT.CT_Inserted_Collection_ID
	    WHERE  CT.CT_TicketType = 1
	           AND CT.CT_IsPromotionalTicket = 1
	    GROUP BY
	           CT.CT_Inserted_Collection_ID
	)   
	
	
	SELECT CD.Company_Id,
	       CD.Sub_Company_Id,
	       CD.Sub_Company_Region_Id,
	       CD.Sub_Company_Area_Id,
	       CD.Sub_Company_District_Id,
	       CD.Site_Id,
	       CD.Site_Name,
	       CD.Machine_Type_Id,
	       CD.Machine_Type_Code,
	       CD.Machine_Type_Description,
	       CD.Machine_Stock_No,
	       ISNULL(GT.GameTitle, 'Unassigned GameTitle') AS MachineName,
	       CD.PosName,
	       CD.CashCollected,
	       -- Setting added for UK client - To exclude extra bills       
	       CASE 
	            WHEN ISNULL(@IncludeRareBills, 'False') = 'False' THEN 0
	            ELSE CD.Cash_Collected_50000p
	       END Cash_Collected_50000p,
	       CASE 
	            WHEN ISNULL(@IncludeRareBills, 'False') = 'False' THEN 0
	            ELSE CD.Cash_Collected_20000p
	       END Cash_Collected_20000p,
	       CASE 
	            WHEN ISNULL(@IncludeRareBills, 'False') = 'False' THEN 0
	            ELSE CD.Cash_Collected_10000p
	       END Cash_Collected_10000p,
	       CD.Cash_Collected_5000P,
	       CD.Cash_Collected_2000P,
	       CD.Cash_Collected_1000P,
	       CD.Cash_Collected_500P,
	       CD.DecCashablePrintedValue,
	       CASE 
	            WHEN ISNULL(@IncludeRareBills, 'False') = 'False' THEN 0
	            ELSE CD.Cash_collected_100p
	       END Cash_collected_100p,
	       CD.Declared_Tickets,
	       CD.Declared_Total_Tickets,
	       ISNULL(SP.Amount, 0) AS Shortpay,
	       CASE 
	            WHEN (@SGVI_Enabled = 'True' AND @SGVI_autodeclare = 'True') THEN (CD.Collection_RDC_Handpay + CD.Collection_RDC_Jackpot)
	            ELSE ISNULL(Ap.Amount, 0)
	       END AS DecHandpay,
	       CD.ManualRefills,
	       ISNULL(Rf.Amount, 0) AS Refunds,
	       (
	           ISNULL(CD.Tickets_Printed, 0) + 
	           (
	               CASE 
	                    WHEN ISNULL(@AddShortpay, 'True') = 'True' THEN ISNULL(SP.Amount, 0)
	                    ELSE 0
	               END
	           )
	       ) AS Tickets_Printed,
	       ((CD.Collection_Handpay_Var) - CD.Collection_RDC_Jackpot) AS 
	       Handpay_Var,
	       CD.Note_Var,
	       ISNULL(
	           (
	               CD.Collection_Coin_Var - CAST(ISNULL(Rf.Amount, 0) AS REAL)
	           ),
	           0
	       ) AS Coin_Var,
	       CD.RDC_Tickets_In,
	       CD. RDC_Tickets_Out,
	       --Modified for Data mismatch issue for LCI      
	       
	       CAST(
	           (
	               (CD.Collection_Declared_Notes) +
	               CD.Declared_Tickets 
	               + ISNULL(VD.Amount, 0) 
	               + (
	                   (
	                       (CD.Collection_Net_Coin) + CAST(ISNULL(CD.ManualRefills, 0)AS REAL)
	                   )
	               ) 
	               + ISNULL(EftIn.Amount, 0)
	           ) 
	           
	           -
	           ISNULL(
	               (
	                      
	                   
	           --ISNULL(CD.Tickets_Printed, 0) + 
	           --(
	           --    CASE 
	           --         WHEN ISNULL(@AddShortpay, 'True') = 'True' THEN ISNULL(SP.Amount, 0)
	           --         ELSE 0
	           --    END
	           --)
	       
	                   ISNULL(CD.Tickets_Printed, 0)
	                   
	                   + ISNULL(SP.Amount, 0) 
	                   
	                   + ISNULL(Rf.Amount, 0) 
	                   
	                   
	                   
	                   + ISNULL(
	                       CASE 
	                            WHEN (@SGVI_Enabled = 'True' AND @SGVI_autodeclare = 'True') THEN (CD.Collection_RDC_Handpay + CD.Collection_RDC_Jackpot)
	                            ELSE (ISNULL(CD.Collection_Handpay, 0) + ISNULL(CD.Progressive_Value_Declared, 0))
	                       END,
	                       0
	                   ) 
	                   
	                   + ISNULL(EftOut.Amount, 0)
	               ),
	               0.00
	           ) 
	           
	           
	           AS DECIMAL(18, 2)
	       ) AS Cash_Take,	-- Win/loss  
	       
	       VWCD.DecWinLoss - VWCD.MeterWinLoss AS Take_Var,
	       (ISNULL(CD.Collection_Handpay, 0) + ISNULL(CD.Progressive_Value_Declared, 0)) AS AttendPay,
	       ISNULL(ManAP.Amount, 0) AS ManAttPay,
	       ISNULL(MacAP.Amount, 0) AS MacAttPay,
	       ISNULL(ManJP.Amount, 0) AS ManJackpot,
	       ISNULL(MacJP.Amount, 0) AS MacJackpot,
	       ISNULL(ManProg.Amount, 0) AS ManProgressive,
	       ISNULL(MacProg.Amount, 0) AS MacProgressive,
	       ISNULL(VD.Amount, 0) AS Void,
	       CAST(
	           ISNULL(CD.Promo_Cashable_EFT_IN, 0) + ISNULL(CD.NonCashable_EFT_In, 0)
	           + ISNULL(CD.Cashable_EFT_In, 0) AS DECIMAL(18, 2)
	       ) AS EftIn,
	       CAST(
	           ISNULL(CD.Promo_Cashable_EFT_Out, 0) + ISNULL(CD.NonCashable_EFT_Out, 0)
	           + ISNULL(CD.Cashable_EFT_Out, 0) AS DECIMAL(18, 2)
	       ) AS EFTOut,
	       CD.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS NonCashableVoucherIn,
	       CD.RDC_TICKETS_PRINTED_NONCASHABLE_VALUE AS NonCashableVoucherOut,
	       CD.CoinsOut,
	       CD.Zone_Name,
	       CAST(ISNULL(PNCT.PromoNonCashableAmount, 0) AS FLOAT) AS PromoNonCashAmount,
	       CAST(ISNULL(PCT.PromoCashableAmount, 0) AS FLOAT) AS PromoCashAmount
	FROM   CollectionDetails CD
	       JOIN VW_CollectionData VWCD
	            ON  VWCD.Collection_ID = CD.Collection_ID
	       LEFT OUTER JOIN VOID VD
	            ON  CD.Collection_ID = VD.Collection_ID
	       LEFT OUTER JOIN Refund Rf
	            ON  CD.Collection_ID = Rf.Collection_ID
	       LEFT OUTER JOIN ShortPay SP
	            ON  CD.Collection_ID = SP.Collection_ID
	       LEFT OUTER JOIN AttendantPay AP
	            ON  CD.Collection_ID = AP.Collection_ID
	       LEFT OUTER JOIN ManualAttendantPay ManAP
	            ON  CD.Collection_ID = ManAP.Collection_ID
	       LEFT OUTER JOIN MachineAttendantPay MacAP
	            ON  CD.Collection_ID = MacAP.Collection_ID
	       LEFT OUTER JOIN ManualJackpot ManJP
	            ON  CD.Collection_ID = ManJP.Collection_ID
	       LEFT OUTER JOIN MachineJackpot MacJP
	            ON  CD.Collection_ID = MacJP.Collection_ID
	       LEFT OUTER JOIN ManualProgressive ManProg
	            ON  CD.Collection_ID = ManProg.Collection_ID
	       LEFT OUTER JOIN MachineProgressive MacProg
	            ON  CD.Collection_ID = MacProg.Collection_ID
	       LEFT OUTER JOIN DecEftIn EftIn
	            ON  CD.Collection_ID = EftIn.Collection_No
	       LEFT OUTER JOIN DecEftOut EftOut
	            ON  CD.Collection_ID = EftOut.Collection_No
	       LEFT OUTER JOIN GameTitleDetails GT
	            ON  CD.Installation_ID = GT.Installation_ID
	       LEFT OUTER JOIN Installation I
	            ON  I.Installation_ID = GT.Installation_ID
	       LEFT OUTER JOIN PromoCashableTickets PCT
	            ON  PCT.CT_Inserted_Collection_ID = CD.Collection_ID
	       LEFT OUTER JOIN PromoNonCashableTickets PNCT
	            ON  PNCT.CT_Inserted_Collection_ID = CD.Collection_ID
	ORDER BY
	       CD.Company_Id,
	       CD.Sub_Company_Id,
	       CD.Sub_Company_Region_Id,
	       CD.Sub_Company_Area_Id,
	       CD.Sub_Company_District_Id,
	       CD.Site_Id,
	       CD.Site_Name,
	       CD.Machine_Type_Id,
	       CD.PosName
END

GO