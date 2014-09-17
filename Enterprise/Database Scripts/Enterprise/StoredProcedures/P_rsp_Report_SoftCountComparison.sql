USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_SoftCountComparison]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_SoftCountComparison]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*--------------------------------------------------------------------------       
---      
--- Description: retrieve information required for SDS_SoftCountComparison report      
---               sp can be called a number of different ways, to either give line by line data, sub total or grand total figures      
---      
---      
--- Inputs:      see inputs      
---      
--- Outputs:     (0)   - no error ..       
---              OTHER - SQL error       
---      
--------------------------------------------------------------------------       
-- to USE      
--SELECT * FROM ZONE WHERE ZONE_NAME LIKE '%ALPHINES%'  
   exec rsp_Report_SoftCountComparison @company=4,@subcompany=0,@site=0,@Zone=6570,@startdate='2013-01-01 00:00:00',@enddate='2013-01-03 00:00:00',@GROUPBYZONE=1   
-- EXEC rsp_Report_SoftCountComparison @startdate = '01 Dec 2010', @enddate = '16 Dec 2010', @runtype = 1, @site=2,@Zone='',@company=2,@subcompany=0, @denom=0      
-- EXEC rsp_Report_SoftCountComparison @startdate = '01 jan 2011', @enddate = '01 jan 2011', @runtype = 1, @site=0,@Zone=''       
-- EXEC rsp_Report_SoftCountComparison @runtype = 2, @Zone = 'ER', @denom=1,@startdate = '01 jan 2010', @enddate = '08 dec 2010',@site=0,@company=0,@subcompany=0      
-- EXEC rsp_Report_SoftCountComparison @runtype = 3,@startdate = '01 jan 2010', @enddate = '01 jan 2010'      
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
---                           31/03/10    added a return line if no data returned, report will then show blank      
---                           10/04/10    added company/sub company grouping      
---                           12/04/10    added sum() for multi day handling      
--- Sudarsan S      15/06/10   for Bills 200 and 500      
--- Jisha Lenu George    20/12/10    Updated the criteria. Fix for #92412      
--------------------------------------------------------------------------- */      
  
CREATE PROCEDURE [dbo].[rsp_Report_SoftCountComparison]
	@Company	INT = 0,
	@SubCompany INT = 0,
	@Region		INT = 0 ,
	@Area		INT = 0,
	@District	INT = 0,
	@Site		INT = 0,
	@Zone		INT = 0, -- grouping fields, set as -1
	@Startdate	DATETIME ,
	@Enddate	DATETIME ,
	@GroupByZone BIT,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	
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
	
	DECLARE @SysRegion VARCHAR(2)                                      
	
	SELECT @SysRegion = UPPER(RIGHT(System_Parameter_Region_Culture, 2))
	FROM   System_Parameters WITH(NOLOCK)                   
	
	SELECT SUM(CT_Value) AS iAmount,
	       COUNT(CT_BArcode) AS BarcodeCount,
	       CT_Inserted_Installation_ID,
	       CT_Inserted_Collection_ID 
	       INTO #CollectionVouchersType
	FROM   Collection_Ticket
	WHERE  CONVERT(VARCHAR(13), DATEADD(DAY, -1, CT_Declared_Date), 106) BETWEEN 
	       @StartDate AND @EndDate
	       AND CT_TicketType = 0
	       AND CT_VoucherStatus <> 'LT' 
	           -- AND ISNULL(CT_IsPromotionalTicket,0)=0
	GROUP BY
	       CT_Inserted_Collection_ID,
	       CT_Inserted_Installation_ID                
	
	
	SELECT SUM(CT_Value) AS iAmount,
	       COUNT(CT_BArcode) AS BarcodeCount,
	       CT_Inserted_Installation_ID,
	       CT_Inserted_Collection_ID 
	       INTO #CollectionVouchers
	FROM   COLLECTION_TICKET
	WHERE  CONVERT(VARCHAR(13), DATEADD(DAY, -1, CT_Declared_Date), 106) BETWEEN 
	       @StartDate AND @EndDate
	       AND CT_TicketType = 1
	       AND CT_VoucherStatus <> 'LT' 
	           --AND ISNULL(CT_IsPromotionalTicket,0)=0
	GROUP BY
	       CT_Inserted_Collection_ID,
	       CT_Inserted_Installation_ID 
	
	
	
	--PromoCashable  
	SELECT SUM(CT_Value) AS iAmount,
	       COUNT(CT_BArcode) AS BarcodeCount,
	       CT_Inserted_Installation_ID,
	       CT_Inserted_Collection_ID 
	       INTO #PromoCollectionVouchersType
	FROM   COLLECTION_TICKET
	WHERE  CONVERT(VARCHAR(13), DATEADD(DAY, -1, CT_Declared_Date), 106) BETWEEN 
	       @StartDate AND @EndDate
	       AND CT_TicketType = 0
	       AND CT_VoucherStatus <> 'LT'
	       AND ISNULL(CT_IsPromotionalTicket, 0) = 1
	GROUP BY
	       CT_Inserted_Collection_ID,
	       CT_Inserted_Installation_ID 
	
	--PromoNonCashable   
	SELECT SUM(CT_Value) AS iAmount,
	       COUNT(CT_BArcode) AS BarcodeCount,
	       CT_Inserted_Installation_ID,
	       CT_Inserted_Collection_ID INTO #PromoCollectionVouchers
	FROM   COLLECTION_TICKET
	WHERE  CONVERT(VARCHAR(13), DATEADD(DAY, -1, CT_Declared_Date), 106) BETWEEN 
	       @StartDate AND @EndDate
	       AND CT_TicketType = 1
	       AND CT_VoucherStatus <> 'LT'
	       AND ISNULL(CT_IsPromotionalTicket, 0) = 1
	GROUP BY
	       CT_Inserted_Collection_ID,
	       CT_Inserted_Installation_ID 
	
	;WITH SOFTCOUNT(
	    Installation_id,
	    Collection_ID,
	    [METER_bill_1],
	    [METER_bill_5],
	    [METER_bill_10],
	    [METER_bill_20],
	    [METER_bill_50],
	    [METER_bill_100],
	    [METER_bill_200],
	    [METER_bill_500],
	    [Meter_bill_total],
	    [METER_Ticket_Amount],
	    [METER_Ticket_Count],
	    [METER_Cashable_Amount],
	    [METER_Cashable_Count],
	    [METER_NonCashable_Amount],
	    [METER_NonCashable_Count],
	    [METER_PromoCashable_Amount],
	    [METER_PromoCashable_Count],
	    [METER_PromoNonCashable_Amount],
	    [METER_PromoNonCashable_Count],
	    [Meter_Total],
	    [MAN_Bill_1],
	    [MAN_Bill_5],
	    [MAN_bill_10],
	    [MAN_bill_20],
	    [MAN_bill_50],
	    [MAN_bill_100],
	    [MAN_bill_200],
	    [MAN_bill_500],
	    [MAN_bill_total],
	    [MAN_Ticket_Amount],
	    [MAN_Ticket_Count],
	    [MAN_Cashable_Amount],
	    [MAN_Cashable_Count],
	    [MAN_NonCashable_Amount],
	    [MAN_NonCashable_Count],
	    [MAN_PromoCashable_Amount],
	    [MAN_PromoCashable_Count],
	    [MAN_PromoNonCashable_Amount],
	    [MAN_PromoNonCashable_Count],
	    [MAN_Total]
	) 
	
	AS 
	(
	    SELECT c.installation_id,
	           c.Collection_ID,
	           CASE @SysRegion
	                WHEN 'US' THEN c.cash_in_100p
	                WHEN 'AR' THEN c.cash_in_200p * 2 --[es-AR]
	                ELSE c.cash_in_100p
	           END,
	           c.cash_in_500p * 5,
	           c.cash_in_1000p * 10,
	           c.cash_in_2000p * 20,
	           c.cash_in_5000p * 50,
	           c.cash_in_10000p * 100,
	           c.cash_in_20000p * 200,
	           c.cash_in_50000p * 500,
	           cc.Collection_RDC_Notes,
	           cc.Collection_RDC_Tickets_In,
	           ISNULL(c.TICKETS_INSERTED_QTY, 0),
	           0,
	           0,
	           CAST(c.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT) / 100,
	           ISNULL(c.TICKETS_INSERTED_NONCASHABLE_QTY, 0),
	           0,
	           0,
	           0,
	           0,
	           cc.Collection_RDC_Tickets_In + cc.Collection_RDC_Notes + (
	               CAST(c.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS FLOAT) / 100
	           ),
	           CASE @SysRegion
	                WHEN 'US' THEN CAST(c.Cash_Collected_100P AS FLOAT) / 100
	                WHEN 'AR' THEN CAST(c.cash_collected_200p AS FLOAT) / 100 --[es-AR]
	                ELSE CAST(c.cash_collected_100P AS FLOAT) / 100
	           END,
	           CAST(c.cash_collected_500P AS FLOAT) / 100,
	           CAST(c.cash_collected_1000P AS FLOAT) / 100,
	           CAST(c.cash_collected_2000P AS FLOAT) / 100,
	           CAST(c.cash_collected_5000P AS FLOAT) / 100,
	           CAST(c.cash_collected_10000P AS FLOAT) / 100,
	           CAST(c.cash_collected_20000P AS FLOAT) / 100,
	           CAST(c.cash_collected_50000P AS FLOAT) / 100,
	           cc.Collection_Declared_Notes,
	           ISNULL(Cvt.iAmount, 0),
	           ISNULL(Cvt.BarcodeCount, 0),
	           0,
	           0,
	           ISNULL(Cv.iAmount, 0),
	           ISNULL(Cv.BarcodeCount, 0),
	           ISNULL(PCvt.iAmount, 0),
	           ISNULL(PCvt.BarcodeCount, 0),
	           ISNULL(PCv.iAmount, 0),
	           ISNULL(PCv.BarcodeCount, 0),
	           ISNULL(cc.Collection_Declared_Tickets, 0) + cc.Collection_Declared_Notes
	    FROM   COLLECTION c WITH(NOLOCK)
	           INNER JOIN dbo.Collection_Calcs cc WITH(NOLOCK)
	                ON  c.collection_id = cc.collection_id
	           LEFT OUTER JOIN #CollectionVouchers Cv WITH(NOLOCK)
	                ON  cc.collection_id = CV.CT_Inserted_Collection_ID
	           LEFT OUTER JOIN #CollectionVouchersType CvT WITH(NOLOCK)
	                ON  cc.collection_id = CvT.CT_Inserted_Collection_ID
	           LEFT OUTER JOIN #PromoCollectionVouchers PCvt WITH(NOLOCK)
	                ON  cc.collection_id = PCvt.CT_Inserted_Collection_ID
	           LEFT OUTER JOIN #PromoCollectionVouchersType PCv WITH(NOLOCK)
	                ON  cc.collection_id = PCv.CT_Inserted_Collection_ID
	    WHERE  c.collection_date BETWEEN @StartDate AND @EndDate
	)                    
	SELECT Installation_id,
	       Collection_ID,
	       [Order] = 1,
	       [Type] = 'METER',
	       [METER_bill_1] AS bill_1,
	       [METER_bill_5] AS bill_5,
	       [METER_bill_10] AS bill_10,
	       [METER_bill_20] AS bill_20,
	       [METER_bill_50] AS bill_50,
	       [METER_bill_100] AS bill_100,
	       [METER_bill_200] AS bill_200,
	       [METER_bill_500] AS bill_500,
	       [Meter_bill_total] AS bill_total,
	       [METER_Ticket_Amount] AS Ticket_Amount,
	       [METER_Ticket_Count] AS Ticket_Count,
	       [METER_Cashable_Amount] AS Cashable_Amount,
	       [METER_Cashable_Count] AS Cashable_Count,
	       [METER_NonCashable_Amount] AS NonCashable_Amount,
	       [METER_NonCashable_Count] AS NonCashable_Count,
	       [METER_PromoCashable_Amount] AS PromoCashableAmount,
	       [METER_PromoCashable_Count] AS PromoCashableCount,
	       [METER_PromoNonCashable_Amount] AS PromoNonCashableAmount,
	       [METER_PromoNonCashable_Count] AS PromoNonCashableCount,
	       [Meter_Total] AS Total 
	       INTO #tmp
	FROM   SoftCount 
	UNION                    
	SELECT Installation_id,
	       Collection_ID,
	       [Order] = 2,
	       [Type] = 'MAN',
	       [MAN_Bill_1] AS bill_1,
	       [MAN_Bill_5] AS bill_5,
	       [MAN_bill_10] AS bill_10,
	       [MAN_bill_20] AS bill_20,
	       [MAN_bill_50] AS bill_50,
	       [MAN_bill_100] AS bill_100,
	       [MAN_bill_200] AS bill_200,
	       [MAN_bill_500] AS bill_500,
	       [MAN_bill_total] AS bill_total,
	       [MAN_Ticket_Amount] AS Ticket_Amount,
	       [MAN_Ticket_Count] AS Ticket_Count,
	       [MAN_Cashable_Amount] AS Cashable_Amount,
	       [MAN_Cashable_Count] AS Cashable_Count,
	       [MAN_NonCashable_Amount] AS NonCashable_Amount,
	       [MAN_NonCashable_Count] AS NonCashable_Count,
	       [MAN_PromoCashable_Amount] AS PromoCashableAmount,
	       [MAN_PromoCashable_Count] AS PromoCashableCount,
	       [MAN_PromoNonCashable_Amount] AS PromoNonCashableAmount,
	       [MAN_PromoNonCashable_Count] AS PromoNonCashableCount,
	       [MAN_Total] AS Total
	FROM   SoftCount 
	UNION                    
	SELECT Installation_id,
	       Collection_ID,
	       [Order] = 3,
	       [Type] = 'VAR',
	       [MAN_Bill_1] - [METER_bill_1] AS bill_1,
	       [MAN_Bill_5] - [METER_bill_5] AS bill_5,
	       [MAN_bill_10] - [METER_bill_10] AS bill_10,
	       [MAN_bill_20] - [METER_bill_20] AS bill_20,
	       [MAN_bill_50] - 
	       [METER_bill_50] AS bill_50,
	       [MAN_bill_100] - [METER_bill_100] AS bill_100,
	       [MAN_bill_200] - [METER_bill_200] AS bill_200,
	       [MAN_bill_500] - [METER_bill_500] AS bill_500,
	       [MAN_bill_total] - [METER_bill_total] AS bill_total,
	       [MAN_Ticket_Amount] - [METER_Ticket_Amount] AS Ticket_Amount,
	       [MAN_Ticket_Count] - [METER_Ticket_Count] AS Ticket_Count,
	       [MAN_Cashable_Amount] - [METER_Cashable_Amount] AS Cashable_Amount,
	       [MAN_Cashable_Count] - [METER_Cashable_Count] AS Cashable_Count,
	       [MAN_NonCashable_Amount] - [METER_NonCashable_Amount] AS 
	       NonCashable_Amount,
	       [MAN_NonCashable_Count] - [METER_NonCashable_Count] AS 
	       NonCashable_Count,
	       [MAN_PromoCashable_Amount] - [METER_PromoCashable_Amount] AS 
	       PromoCashableAmount,
	       [MAN_PromoCashable_Count] - [METER_PromoCashable_Count] AS 
	       PromoCashableCount,
	       [MAN_PromoNonCashable_Amount] - [METER_PromoNonCashable_Amount] AS 
	       PromoNonCashableAmount,
	       [MAN_PromoNonCashable_Count] - [METER_PromoNonCashable_Count] AS 
	       PromoNonCashableCount,
	       [MAN_Total] - [Meter_Total] AS Total
	FROM   SoftCount 
	
	-- not get all fields required                            
	SELECT gmi.Bar_Position_ID,
	       gmi.Bar_Position_Name,	-- stand                            
	       gmi.Machine_Type_ID,
	       gmi.Machine_Type_Code,
	       gmi.Site_ID,
	       gmi.Site_Name,
	       gmi.Sub_Company_ID,
	       gmi.Sub_Company_Name,
	       gmi.Company_ID,
	       gmi.Company_Name,
	       gmi.Machine_Stock_No,	-- slot                            
	       ISNULL(gmi.Zone_Name, 'NOT SET') AS 'Zone_Name',	-- area             
	       gmi.Zone_ID,
	       #tmp.installation_id,
	       #tmp.Collection_ID,
	       #tmp.[Order],
	       #tmp.[Type],
	       #tmp.[bill_1],
	       #tmp.[bill_5],
	       #tmp.[bill_10],
	       #tmp.[bill_20],
	       #tmp.[bill_50],
	       #tmp.[bill_100],
	       #tmp.[bill_200],
	       #tmp.[bill_500],
	       #tmp.[bill_total],
	       #tmp.[Ticket_Amount],
	       #tmp.[Ticket_Count],
	       #tmp.[Cashable_Amount],
	       #tmp.[Cashable_Count],
	       #tmp.[NonCashable_Amount],
	       #tmp.[NonCashable_Count],
	       #tmp.[PromoCashableAmount],
	       #tmp.[PromoCashableCount],
	       #tmp.[PromoNonCashableAmount],
	       #tmp.[PromoNonCashableCount],
	       #tmp.[Total] 
	       
	       INTO #preGrouping
	FROM   vw_genericmachineinformation gmi WITH(NOLOCK)
	       INNER JOIN #tmp WITH(NOLOCK)
	            ON  #tmp.installation_id = gmi.installation_ID
	GROUP BY
	       gmi.Bar_Position_ID,
	       gmi.Bar_Position_Name,
	       gmi.Machine_Type_ID,
	       gmi.Machine_Type_Code,
	       gmi.Site_ID,
	       gmi.Site_Name,
	       gmi.Sub_Company_ID,
	       gmi.Sub_Company_Name,
	       gmi.Company_ID,
	       gmi.Company_Name,
	       gmi.Machine_Stock_No,
	       gmi.Zone_Name,
	       gmi.Zone_ID,
	       #tmp.installation_id,
	       #tmp.Collection_ID,
	       #tmp.[Order],
	       #tmp.[Type],
	       #tmp.[bill_1],
	       #tmp.[bill_5],
	       #tmp.[bill_10],
	       #tmp.[bill_20],
	       #tmp.[bill_50],
	       #tmp.[bill_100],
	       #tmp.[bill_200],
	       #tmp.[bill_500],
	       #tmp.[bill_total],
	       #tmp.[Ticket_Amount],
	       #tmp.[Ticket_Count],
	       #tmp.[Cashable_Amount],
	       #tmp.[Cashable_Count],
	       #tmp.[NonCashable_Amount],
	       #tmp.[NonCashable_Count],
	       #tmp.[PromoCashableAmount],
	       #tmp.[PromoCashableCount],
	       #tmp.[PromoNonCashableAmount],
	       #tmp.[PromoNonCashableCount],
	       #tmp.[Total]
	ORDER BY
	       collection_id,
	       [order]                            
	
	CREATE TABLE #CalculatedTable
	(
		Bar_Position_ID           INT,
		Bar_Position_Name         VARCHAR(100),
		Machine_Type_ID           INT,
		Machine_Type_Code         VARCHAR(100),
		Site_Name                 VARCHAR(100),
		Site_ID                   INT,
		Sub_Company_Name          VARCHAR(100),
		Sub_Company_ID            INT,
		Company_Name              VARCHAR(100),
		Company_ID                INT,
		Machine_Stock_No          VARCHAR(50),
		Zone_Name                 VARCHAR(100),
		zone_Id                   INT,
		[Type]                    VARCHAR(100),
		[Order]                   INT,
		[bill_1]                  FLOAT,
		[bill_5]                  FLOAT,
		[bill_10]                 FLOAT,
		[bill_20]                 FLOAT,
		[bill_50]                 FLOAT,
		[bill_100]                FLOAT,
		[bill_200]                FLOAT,
		[bill_500]                FLOAT,
		[bill_total]              FLOAT,
		[Ticket_Amount]           FLOAT,
		[Ticket_Count]            FLOAT,
		[Cashable_Amount]         FLOAT,
		[Cashable_Count]          FLOAT,
		[NonCashable_Amount]      FLOAT,
		[NonCashable_Count]       FLOAT,
		[PromoCashableAmount]     FLOAT,
		[PromoCashableCount]      FLOAT,
		[PromoNonCashableAmount]  FLOAT,
		[PromoNonCashableCount]   FLOAT,
		[Total]                   FLOAT,
		SORT_ORDER                INT
	) 
	-- do the grouping incase we are used as part of the subreport
	--                              
	IF EXISTS (
	       SELECT 1
	       FROM   #preGrouping
	   )
	    -- all records as created above                       
	    INSERT INTO #CalculatedTable
	    SELECT 
	          Bar_Position_ID,
	           Bar_Position_Name,
	           Machine_Type_ID,
	           Machine_Type_Code,
	           #preGrouping. Site_Name,
	           #preGrouping.Site_ID,
	           #preGrouping.Sub_Company_Name,
	           #preGrouping.Sub_Company_ID,
	           #preGrouping.Company_Name,
	           #preGrouping.Company_ID,
	           Machine_Stock_No,	-- slot                           
	           Zone_Name,
	           zone_id,
	           [Type],
	           [Order],
	           [bill_1] = SUM([bill_1]),
	           [bill_5] = SUM([bill_5]),
	           [bill_10] = SUM([bill_10]),
	           [bill_20] = SUM([bill_20]),
	           [bill_50] = SUM([bill_50]),
	           [bill_100] = SUM([bill_100]),
	           [bill_200] = SUM([bill_200]),
	           [bill_500] = SUM([bill_500]),
	           [bill_total] = SUM([bill_total]),
	           [Ticket_Amount] = SUM([Ticket_Amount]),
	           [Ticket_Count] = SUM([Ticket_Count]),
	           [Cashable_Amount] = SUM([Cashable_Amount]),
	           [Cashable_Count] = SUM([Cashable_Count]),
	           [NonCashable_Amount] = ISNULL(SUM([NonCashable_Amount]), 0),
	           [NonCashable_Count] = SUM([NonCashable_Count]),
	           [PromoCashableAmount] = SUM([PromoCashableAmount]),
	           [PromoCashableCount] = SUM([PromoCashableCount]),
	           [PromoNonCashableAmount] = SUM([PromoNonCashableAmount]),
	           [PromoNonCashableCount] = SUM([PromoNonCashableCount]),
	           [Total] = ISNULL(SUM([Total]), 0),
	           MAX(1)
	    FROM   #preGrouping WITH(NOLOCK)
	           INNER JOIN dbo.SITE S WITH(NOLOCK)
	                ON  s.Site_ID = #preGrouping.Site_ID
	    WHERE  ((@Zone IS NULL) OR (@Zone IS NOT NULL AND Zone_ID = @Zone))
	          -- AND 
	           --((ISNULL(@Site, 0) <> 0 AND #preGrouping.Site_ID = @Site))
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND #preGrouping.Site_ID IN (SELECT DATA
	                                                FROM   fnSplit(@SiteIDList, ','))
	               )
	           AND ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	           AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	           AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	           AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	           AND ISNULL(@Company, company_id) = company_id
	    GROUP BY
	           Bar_Position_ID,
	           Bar_Position_Name,
	           Machine_Type_ID,
	           Machine_Type_Code,
	           #preGrouping.Site_ID,
	           #preGrouping.Site_Name,
	           #preGrouping.Sub_Company_ID,
	           #preGrouping.Sub_Company_Name,
	           #preGrouping.Company_ID,
	           #preGrouping.Company_Name,
	           Machine_Stock_No,
	           Zone_Name,
	           zone_id,
	           [Type],
	           [Order]
	    ORDER BY
	           [Site_ID],
	           [Bar_Position_Name],
	           [order]                            
	
	
	--select * from #CalculatedTable
	--return
	
	UPDATE #CalculatedTable
	SET    SORT_ORDER = 1 
	
	--all records grouped for a grand total  
	IF (@GROUPBYZONE = 1)
	BEGIN
	    INSERT INTO #CalculatedTable
	      (
	        Site_ID,
	        Site_Name,
	        Zone_Name,
	        [Order],
	        [Type],
	        [bill_1],
	        [bill_5],
	        [bill_10],
	        [bill_20],
	        [bill_50],
	        [bill_100],
	        [bill_200],
	        [bill_500],
	        [bill_total],
	        [Ticket_Amount],
	        [Ticket_Count],
	        [Cashable_Amount],
	        [Cashable_Count],
	        [NonCashable_Amount],
	        [NonCashable_Count],
	        [PromoCashableAmount],
	        [PromoCashableCount],
	        [PromoNonCashableAmount],
	        [PromoNonCashableCount],
	        [Total]
	      )
	    SELECT Site_ID,
	           Site_Name,
	           Zone_Name,
	           [Order],
	           [Type],
	           SUM([bill_1]),
	           SUM([bill_5]),
	           SUM([bill_10]),
	           SUM([bill_20]),
	           SUM([bill_50]),
	           SUM([bill_100]),
	           SUM([bill_200]),
	           SUM([bill_500]),
	           SUM([bill_total]),
	           SUM([Ticket_Amount]),
	           SUM([Ticket_Count]),
	           SUM([Cashable_Amount]),
	           SUM([Cashable_Count]),
	           SUM([NonCashable_Amount]),
	           SUM([NonCashable_Count]),
	           SUM([PromoCashableAmount]),
	           SUM([PromoCashableCount]),
	           SUM([PromoNonCashableAmount]),
	           SUM([PromoNonCashableCount]),
	           SUM([Total])
	    FROM   #CalculatedTable
	    GROUP BY
	           Site_Id,
	           Site_Name,
	           Zone_Name,
	           [order],
	           [Type]
	    
	    UPDATE #CalculatedTable
	    SET    SORT_ORDER = 2
	    WHERE  SORT_ORDER IS NULL
	END
	
	INSERT INTO #CalculatedTable
	  (
	    Site_ID,
	    Site_Name,
	    [Order],
	    [Type],
	    [bill_1],
	    [bill_5],
	    [bill_10],
	    [bill_20],
	    [bill_50],
	    [bill_100],
	    [bill_200],
	    [bill_500],
	    [bill_total],
	    [Ticket_Amount],
	    [Ticket_Count],
	    [Cashable_Amount],
	    [Cashable_Count],
	    [NonCashable_Amount],
	    [NonCashable_Count],
	    [PromoCashableAmount],
	    [PromoCashableCount],
	    [PromoNonCashableAmount],
	    [PromoNonCashableCount],
	    [Total]
	  )
	SELECT Site_ID,
	       Site_Name,
	       [Order],
	       [Type],
	       SUM([bill_1]),
	       SUM([bill_5]),
	       SUM([bill_10]),
	       SUM([bill_20]),
	       SUM([bill_50]),
	       SUM([bill_100]),
	       SUM([bill_200]),
	       SUM([bill_500]),
	       SUM([bill_total]),
	       SUM([Ticket_Amount]),
	       SUM([Ticket_Count]),
	       SUM([Cashable_Amount]),
	       SUM([Cashable_Count]),
	       SUM([NonCashable_Amount]),
	       SUM([NonCashable_Count]),
	       SUM([PromoCashableAmount]),
	       SUM([PromoCashableCount]),
	       SUM([PromoNonCashableAmount]),
	       SUM([PromoNonCashableCount]),
	       SUM([Total])
	FROM   #CalculatedTable
	WHERE  SORT_ORDER = 1
	GROUP BY
	       Site_Id,
	       Site_Name,
	       [order],
	       [Type]
	
	UPDATE #CalculatedTable
	SET    SORT_ORDER = 3
	WHERE  SORT_ORDER IS NULL                    
	
	INSERT INTO #CalculatedTable
	  (
	    [Order],
	    [Type],
	    [bill_1],
	    [bill_5],
	    [bill_10],
	    [bill_20],
	    [bill_50],
	    [bill_100],
	    [bill_200],
	    [bill_500],
	    [bill_total],
	    [Ticket_Amount],
	    [Ticket_Count],
	    [Cashable_Amount],
	    [Cashable_Count],
	    [NonCashable_Amount],
	    [NonCashable_Count],
	    [PromoCashableAmount],
	    [PromoCashableCount],
	    [PromoNonCashableAmount],
	    [PromoNonCashableCount],
	    [Total]
	  )
	SELECT [Order],
	       [Type],
	       SUM([bill_1]),
	       SUM([bill_5]),
	       SUM([bill_10]),
	       SUM([bill_20]),
	       SUM([bill_50]),
	       SUM([bill_100]),
	       SUM([bill_200]),
	       SUM([bill_500]),
	       SUM([bill_total]),
	       SUM([Ticket_Amount]),
	       SUM([Ticket_Count]),
	       SUM([Cashable_Amount]),
	       SUM([Cashable_Count]),
	       SUM([NonCashable_Amount]),
	       SUM([NonCashable_Count]),
	       SUM([PromoCashableAmount]),
	       SUM([PromoCashableCount]),
	       SUM([PromoNonCashableAmount]),
	       SUM([PromoNonCashableCount]),
	       SUM([Total])
	FROM   #CalculatedTable
	WHERE  SORT_ORDER = 1
	GROUP BY
	       [order],
	       [Type]                                       
	
	UPDATE #CalculatedTable
	SET    SORT_ORDER = 4
	WHERE  SORT_ORDER IS NULL                    
	
	SELECT *
	FROM   #CalculatedTable
	ORDER BY
	       SORT_ORDER,
	       [order],
	       [Type] 
	
	DROP TABLE #CalculatedTable 
	DROP TABLE #PreGrouping
END
GO

