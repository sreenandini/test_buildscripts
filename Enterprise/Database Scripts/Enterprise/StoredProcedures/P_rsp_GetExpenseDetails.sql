USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetExpenseDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetExpenseDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--              
-- Description: Get Expense Details for Reports      
-- Inputs:      See Inputs               
--              
-- Outputs:     Result Set - Expense Details, Summarized Expense Details & Summ Expense Details                  
--                                
-- ======================================================================================================================================              
--               
-- Revision History              
--              
-- Yoganandh P  12/03/2010  Created  
-- Yoganandh P	29/03/2010	Modified logic to fetch TicketOut information
-- Yoganandh P	31/03/2010	Modified to fetch data for Current Installation of Slot or for LifeTime of Slot based on Setting
-- Yoganandh P	09/04/2010	Fetch Ticket out information from Ticketing Voucher table instead of Exchange DB - Collection table
-- Yoganandh P  28/04/2010	Modified to fetch Cashable and NonCashable TicketOut Information based on Ticket_Type flag in Voucher table
-- Yoganandh P	28/05/2010	Modified to fetch Asset No in result set and modified data fetching logic for MTD, QTD & YTD
-- Yoganandh P	08/07/2010	Modified PTD Logic & Dropped #Temp table once finished working with it
-- Yoganandh P	20/07/2010	Added Shorpay to the expense
-- Yoganandh P	26/09/2010	Referred to field Collection_Date_Of_Collection instead of Collection_Date in Collection table for PTD
-- Yoganandh P	03/01/2011	Referred to field Collection_Time_Of_Collection, 
--							Previous_Collection_Date_Of_Collection,  Previous_Collection_Time_Of_Collection for PTD
-- Jisha Lenu	05/04/2011	Modified the sp to match the data between DAY,PTD,MTD,QTD,YTD,LTD with ALL - Fix for #99204
-- Jisha Lenu	12/04/2011	Added the 'Where' clause for #MachineTempTable - Fix for #99651	 
-- A.Vinod Kumar 08/08/2012 Modified Completely rewritten the query to improve the performance and fixing the duplicate records.
--									 CR's fixed : 140,939

-----------------------------------------------------------------------------------------------------------------------------------------
-- EXEC rsp_GetExpenseDetails 2,0,0,'2012-08-07', 'ALL'
CREATE PROCEDURE rsp_GetExpenseDetails           
(    
	@Company		INT =0,   
	@SubCompany		INT =0, 
	@Region			INT =0,
	@Area			INT =0,
	@District       INT =0,         	
	@Site			INT =0,         
	@ReportDate		DateTime,              
	@ReportPeriod	Varchar(3),
	@SiteIDList     VARCHAR(MAX),
	@IsGamingDayBasedReport    BIT
)              
AS
BEGIN
	SET ANSI_NULLS ON
	SET NOCOUNT ON
	
	-- Default Values
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
	
	
	
	--BEGIN TRY
		DECLARE @SlotLifeToDate         VARCHAR(10)
		DECLARE @Installation_End_Date  DATETIME
		
		DECLARE @DailyAutoReadTime      VARCHAR(50)
		
		EXEC rsp_GetSetting 0,
		     'SlotLifeToDate',
		     'False',
		     @SlotLifeToDate OUTPUT
		
		EXEC [rsp_GetSiteSetting] @Site, 'DailyAutoReadTime', @DailyAutoReadTime OUTPUT
		
		--When all site selected set DailyAutoReadTime to Midnight to Midnight
		IF @Site IS NULL
		BEGIN
			SET @DailyAutoReadTime = '0:00'
		END
		
		--SET @SlotLifeToDate = 'True'
		-- Report Period
		IF (@ReportPeriod = 'ALL' OR @ReportPeriod='--All--')
		    SET @ReportPeriod = NULL
		
		-- Report Start and End Date		
		DECLARE @ReportDateStart     DATETIME
		DECLARE @ReportDateEnd       DATETIME
		
		DECLARE @MTDReportStartDate  DATETIME
		DECLARE @QTDReportStartDate  DATETIME
		DECLARE @YTDReportStartDate  DATETIME
		
		
		IF @IsGamingDayBasedReport = 1
		BEGIN
			SET @ReportDateStart = DATEDIFF(dd, 1, @ReportDate) + CAST(@DailyAutoReadTime AS DATETIME)
			SET @ReportDateEnd = DATEDIFF(dd, 0, @ReportDate) + CAST(@DailyAutoReadTime AS DATETIME)
			SET @MTDReportStartDate = DATEADD(mm, DATEDIFF(mm, 0, @ReportDate), 0) + CAST(@DailyAutoReadTime AS DATETIME)
			SET @QTDReportStartDate = DATEADD(qq, DATEDIFF(qq, 0, @ReportDate), 0) + CAST(@DailyAutoReadTime AS DATETIME)
			SET @YTDReportStartDate = DATEADD(yy, DATEDIFF(yy, 0, @ReportDate), 0) + CAST(@DailyAutoReadTime AS DATETIME)
			
		END
		ELSE
		BEGIN
			SET @ReportDateStart = DATEADD(dd, DATEDIFF(dd, 0, @ReportDate), 0)
			SET @ReportDateEnd = DATEADD(ms, -3, DATEADD(dd, DATEDIFF(dd, 0, @ReportDate) + 1, 0))
			SET @MTDReportStartDate = DATEADD(mm, DATEDIFF(mm, 0, @ReportDate), 0)
			SET @QTDReportStartDate = DATEADD(qq, DATEDIFF(qq, 0, @ReportDate), 0)
			SET @YTDReportStartDate = DATEADD(yy, DATEDIFF(yy, 0, @ReportDate), 0)			
		END		
		
		-- Final Table
		DECLARE @Result        TABLE (
		            RowNo INT IDENTITY(1, 1),
		            SortOrder INT,
		            SequenceNo INT,
		            Period VARCHAR(10),
		            Slot VARCHAR(50),
		            AssetNo VARCHAR(50),
		            GMUNo VARCHAR(50),
		            AttendantPaidJackpot DECIMAL(18, 2) DEFAULT(0),
		            AttendantPaidCancelledCredits DECIMAL(18, 2) DEFAULT(0),
		            ProgressiveJackpot DECIMAL(18, 2) DEFAULT(0),
		            TicketOut DECIMAL(18, 2) DEFAULT(0),
		            NonCashableTicketOut DECIMAL(18, 2) DEFAULT(0),
		            Shortpay DECIMAL(18, 2) DEFAULT(0),
		            Site_Code VARCHAR(50),
		            Sub_Company_Name VARCHAR(50),
		            Company_Name VARCHAR(50),
		            Site_Name VARCHAR(50),
		            CollectionDate DATETIME NULL,
		            PreviousCollectionDate DATETIME NULL
		        )
		
		-- Installation Records	
		DECLARE @Installation  TABLE (
		            RowNo INT NOT NULL,
		            Installation_ID INT,
		            Company_Name VARCHAR(50),
		            Sub_Company_Name VARCHAR(50),
		            Machine_ID INT,
		            Bar_Position_ID INT,
		            Site_ID INT,
		            Site_Code VARCHAR(50),
		            Site_Name VARCHAR(50),
		            Slot VARCHAR(50) COLLATE DATABASE_DEFAULT,
		            AssetNo VARCHAR(50),
		            GMUNo VARCHAR(50),
		            START_DATE DATETIME,
		            End_Date DATETIME,
		            CollectionNo INT,
		            CollectionDate DATETIME NULL,
		            PreviousCollectionDate DATETIME NULL
		        )
		
		-- Filter Table
		DECLARE @Periods       TABLE(
		            PeriodId INT,
		            PeriodName VARCHAR(10),
		            StartDate DATETIME NULL,
		            EndDate DATETIME NULL
		        )
		
		INSERT INTO @Periods
		VALUES
		  (
		    1,
		    'DAY',
		    @ReportDateStart,
		    @ReportDateEnd
		  )
		INSERT INTO @Periods
		VALUES
		  (
		    2,
		    'PTD',
		    @ReportDateStart,
		    @ReportDateEnd
		  )
		INSERT INTO @Periods
		VALUES
		  (
		    3,
		    'MTD',
		    @MTDReportStartDate,
		    @ReportDateEnd
		  )
		INSERT INTO @Periods
		VALUES
		  (
		    4,
		    'QTD',
		    @QTDReportStartDate,
		    @ReportDateEnd
		  )
		INSERT INTO @Periods
		VALUES
		  (
		    5,
		    'YTD',
		    @YTDReportStartDate,
		    @ReportDateEnd
		  )
		INSERT INTO @Periods
		VALUES
		  (
		    6,
		    'LTD',
		    NULL,
		    @ReportDateEnd
		  )
		
		/******************************************************************************
		* INSTALLATION DETAILS
		******************************************************************************/
		INSERT INTO @Installation
		SELECT ROW_NUMBER() OVER(ORDER BY tI.Installation_ID) AS 
		       'InstallationRowNumber',
		       tI.Installation_ID,
		       CM.Company_Name,
		       SC.Sub_Company_Name,
		       tI.Machine_ID,
		       tI.Bar_Position_ID,
		       tS.Site_ID,
		       tS.Site_Code,
		       tS.Site_Name,
		       tM.Machine_Manufacturers_Serial_No AS 'Slot',
		       tM.Machine_Stock_No AS 'AssetNo',
		       tM.GMUNo AS 'GMUNo',
		       CAST(
		           tI.Installation_Start_Date + ' ' + tI.Installation_Start_Time 
		           AS DATETIME
		       ) AS 'Start_Date',
		       (
		           CASE 
		                WHEN tI.Installation_End_Date IS NULL THEN NULL
		                ELSE CAST(
		                         tI.Installation_END_Date + ' ' + tI.Installation_END_Time 
		                         AS DATETIME
		                     )
		           END
		       ) AS 'End_Date',
		       0,
		       NULL,
		       NULL
		FROM   dbo.Installation tI WITH(NOLOCK)
		       INNER JOIN MACHINE tM WITH(NOLOCK)
		            ON  tI.Machine_ID = tM.Machine_ID
		       INNER JOIN dbo.Bar_Position tBP WITH(NOLOCK)
		            ON  tbp.Bar_Position_ID = tI.Bar_Position_ID
		       INNER JOIN dbo.Site tS WITH(NOLOCK)
		            ON  tS.Site_Id = tBP.Site_ID
		       INNER JOIN dbo.Sub_Company SC WITH(NOLOCK)
		            ON  SC.Sub_Company_ID = tS.Sub_Company_ID
		       INNER JOIN dbo.Company CM WITH(NOLOCK)
		            ON  CM.Company_ID = SC.Company_ID
		WHERE  
		       ISNULL(@Site, tS.Site_ID) = tS.Site_ID		      
		       AND (
		               @SiteIDList IS NOT NULL
		               AND tS.Site_ID IN (SELECT DATA
		                                  FROM   fnSplit(@SiteIDList, ','))
		           )
		       AND (
		               @SlotLifeToDate = 'True'
		               OR (
		                      @SlotLifeToDate = 'False'
		                      AND tI.Installation_End_Date IS NULL
		                  )
		           )
			   AND ISNULL(@Company, CM.Company_Id) = CM.Company_Id
			   AND ISNULL(@SubCompany, tS.Sub_Company_Id) = tS.Sub_Company_Id
			   AND ISNULL(@Region, tS.Sub_Company_Region_ID) = tS.Sub_Company_Region_ID
			   AND ISNULL(@Area, tS.Sub_Company_Area_ID) = tS.Sub_Company_Area_ID
			   AND ISNULL(@District, tS.Sub_Company_District_ID) = tS.Sub_Company_District_ID
		       AND (
		               DATEADD(d, DATEDIFF(d, 0, @ReportDate), 0) BETWEEN 
		               CAST(
		                   CONVERT(VARCHAR(12), tI.Installation_Start_Date) AS DATETIME
		               ) AND 
		               CAST(
		                   CONVERT(VARCHAR(12), ISNULL(tI.Installation_END_Date, GETDATE())) 
		                   AS DATETIME
		               )
		           )
		
		/******************************************************************************
		* TREASURY DETAILS
		******************************************************************************/
		SELECT Treasury_Date,
		       Slot,
		       AssetNo,
		       GMUNo,
		       COALESCE([Attendantpay Jackpot], [Mystery Jackpot], 0) AS 'HandpayJackpot',
		       COALESCE([Attendantpay Credit], 0) AS 'HandpayCredit',
		       COALESCE([Progressive], 0) AS 'ProgressiveJackpot',
		       COALESCE([ShortPay], [Offline Voucher-Shortpay], 0) AS 'ShortPay' INTO #TreasuryDetails
		FROM   (
		           SELECT tT.Treasury_ID,
		                  CAST(tT.Treasury_Date + ' ' + tT.Treasury_Time AS DATETIME) AS 'Treasury_Date',
		                  tI.Slot,
		                  tI.AssetNo,
		                  tI.GMUNo,
		                  tT.Treasury_Type,
		                  tT.Treasury_Amount
		           FROM   Treasury_Entry tT WITH (NOLOCK)
		                  INNER JOIN @Installation tI
		                       ON  tI.Installation_ID = tT.Installation_ID
		           WHERE  UPPER(tT.Treasury_Type) <> 'DEFLOAT'
		       ) v PIVOT(
		           SUM(Treasury_Amount) FOR Treasury_Type IN ([Attendantpay Jackpot], [Attendantpay Credit], 
		                                                     [Progressive], [ShortPay], [Offline Voucher-Shortpay],[Mystery Jackpot])
		       ) Pvt
		ORDER BY
		       AssetNo ASC
		
		/******************************************************************************
		* VOUCHER DETAILS
		******************************************************************************/
		SELECT Slot,
		       AssetNo,
		       GMUNo,
		       dtPrinted,
		       COALESCE([0], 0) AS 'TicketOut',
		       COALESCE([1], 0) AS 'NonCashableTicketOut' INTO #VoucherDetails
		FROM   (
		           SELECT tV.iVoucherID,
		                  tM.Machine_ID,
		                  tM.Slot,
		                  tM.AssetNo,
		                  tM.GMUNo,
		                  tV.dtPrinted,
		                  tV.Ticket_Type,
		                  dbo.compute_decimal(COALESCE(tV.iAmount, 0)) AS iAmount
		           FROM   dbo.Voucher tV WITH(NOLOCK)
		                  INNER JOIN dbo.Device tD WITH(NOLOCK)
		                       ON  tV.iDeviceID = tD.iDeviceID
		                       AND tV.iSiteId = tD.Site_Code
		                  INNER JOIN (
		                           SELECT tM.Machine_ID,
		                                  tM.Slot,
		                                  tM.AssetNo,
		                                  tM.GMUNo,
		                                  tM.Site_ID,
		                                  MIN(START_DATE) AS START_DATE
		                           FROM   @Installation tM
		                           GROUP BY
		                                  tM.Machine_ID,
		                                  tM.Slot,
		                                  tM.AssetNo,
		                                  tM.GMUNo,
		                                  tM.Site_ID
		                       ) tM
		                       ON  tD.strSerial COLLATE DATABASE_DEFAULT = tM.AssetNo COLLATE 
		                           DATABASE_DEFAULT
		                  INNER JOIN dbo.Site tS WITH(NOLOCK)
		                       ON  tS.Site_Code = tD.Site_Code
		                       AND tS.Site_ID = tM.Site_ID
		                       AND (@SiteIDList IS NOT NULL AND tS.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
		           WHERE  tV.dtPrinted >= tM.[Start_Date]
		       ) v PIVOT(SUM(iAmount) FOR Ticket_Type IN ([0], [1])) AS Pvt
		ORDER BY
		       AssetNo ASC,
		       dtPrinted DESC
		
		/******************************************************************************
		* COLLECTION DETAILS
		******************************************************************************/
		SELECT DISTINCT tI.Slot,
		       tI.AssetNo,
		       tI.GMUNo,
		       tC.Collection_ID AS 'CollectionNo',
		       CAST(
		           tC.Collection_Date_Of_Collection + ' ' + tC.Collection_Time_Of_Collection AS DATETIME
		       ) AS 'CollectionDate',
		       CAST(
		           tC.Previous_Collection_Date_Of_Collection + ' ' + tC.Previous_Collection_Time_Of_Collection AS 
		           DATETIME
		       ) AS 'PreviousCollectionDate' INTO #CollectionDetails
		FROM   [Collection] tC WITH (NOLOCK)
		       INNER JOIN Treasury_Entry tT WITH (NOLOCK)
		            ON  tC.Collection_ID = tT.Collection_ID
		       INNER JOIN @Installation tI
		            ON  tI.Installation_ID = tT.Installation_ID
		WHERE  CAST(
		           tC.Collection_Date_Of_Collection + ' ' + tC.Collection_Time AS DATETIME
		       ) <= @ReportDateEnd
		
		-- 1. Collection exists for report date
		UPDATE tI
		SET    tI.CollectionNo = tC.CollectionNo,
		       tI.CollectionDate = tC.CollectionDate,
		       tI.PreviousCollectionDate = tC.PreviousCollectionDate
		FROM   @Installation tI
		       INNER JOIN (
		                SELECT ST.Slot,
		                       ST.AssetNo,
		                       ST.GMUNo,
		                       tC.CollectionNo,
		                       tC.CollectionDate,
		                       tC.PreviousCollectionDate
		                FROM   (
		                           SELECT tI.Slot,
		                                  tI.AssetNo,
		                                  tI.GMUNo,
		                                  MAX(tC.CollectionNo) AS CollectionNo
		                           FROM   #CollectionDetails tC
		                                  INNER JOIN @Installation tI
		                                       ON  tI.Slot = tC.Slot
		                                       AND tI.AssetNo = tC.AssetNo
		                                       AND tI.GMUNo = tC.GMUNo
		                           WHERE  DATEADD(d, 0, DATEDIFF(d, 0, tC.CollectionDate)) = @ReportDateStart
		                           GROUP BY
		                                  tI.Slot,
		                                  tI.AssetNo,
		                                  tI.GMUNo
		                       ) ST
		                       INNER JOIN #CollectionDetails tC
		                            ON  ST.CollectionNo = tC.CollectionNo
		            ) tC
		            ON  tI.Slot = tC.Slot
		            AND tI.AssetNo = tC.AssetNo
		            AND tI.GMUNo = tC.GMUNo
		
		-- 2. Collection exists lesser than report date
		UPDATE tI
		SET    tI.CollectionNo = tC.CollectionNo,
		       tI.CollectionDate = tC.CollectionDate,
		       tI.PreviousCollectionDate = tC.PreviousCollectionDate
		FROM   @Installation tI
		       INNER JOIN (
		                SELECT ST.Slot,
		                       ST.AssetNo,
		                       ST.GMUNo,
		                       tC.CollectionNo,
		                       @ReportDateEnd AS 'CollectionDate',
		                       tC.CollectionDate AS 'PreviousCollectionDate'
		                FROM   (
		                           SELECT tI.Slot,
		                                  tI.AssetNo,
		                                  tI.GMUNo,
		                                  MAX(tC.CollectionNo) AS CollectionNo
		                           FROM   #CollectionDetails tC
		                                  INNER JOIN @Installation tI
		                                       ON  tI.Slot = tC.Slot
		                                       AND tI.AssetNo = tC.AssetNo
		                                       AND tI.GMUNo = tC.GMUNo
		                           WHERE  DATEADD(d, 0, DATEDIFF(d, 0, tC.CollectionDate)) <= @ReportDateStart
		                           GROUP BY
		                                  tI.Slot,
		                                  tI.AssetNo,
		                                  tI.GMUNo
		                       ) ST
		                       INNER JOIN #CollectionDetails tC
		                            ON  ST.CollectionNo = tC.CollectionNo
		            ) tC
		            ON  tI.Slot = tC.Slot
		            AND tI.AssetNo = tC.AssetNo
		            AND tI.GMUNo = tC.GMUNo
		
		-- 3. Remaining collection dates
		UPDATE tI
		SET    tI.CollectionNo = 0,--tC.CollectionNo,
		       tI.CollectionDate = @ReportDateEnd,
		       tI.PreviousCollectionDate = tC.START_DATE
		FROM   @Installation tI
		       INNER JOIN (
		                SELECT t1.Machine_ID,
		                       t1.Installation_ID,
		                       t1.START_DATE
		                FROM   @Installation t1
		                       INNER JOIN (
		                                SELECT Machine_ID,
		                                       MIN(Installation_ID) AS Installation_ID
		                                FROM   @Installation
		                                GROUP BY
		                                       Machine_ID
		                            ) t2
		                            ON  t1.Machine_ID = t2.Machine_ID
		                            AND t1.Installation_ID = t2.Installation_ID
		            ) tC
		            ON  tI.Machine_ID = tC.Machine_ID
		WHERE  tI.CollectionNo = 0
		
		/******************************************************************************
		* START RECORDS
		******************************************************************************/
		INSERT INTO @Result
		  (
		    SortOrder,
		    SequenceNo,
		    Period,
		    Slot,
		    AssetNo,
		    GMUNo,
		    Site_Code,
		    Sub_Company_Name,
		    Company_Name,
		    Site_Name,
		    CollectionDate,
		    PreviousCollectionDate
		  )
		SELECT  DISTINCT 1,
		       T2.PeriodId,
		       T2.PeriodName,
		       Slot,
		       AssetNo,
		       GMUNo,
		       Site_Code,
		       Sub_Company_Name,
		       Company_Name,
		       Site_Name,
		       CollectionDate,
		       PreviousCollectionDate
		FROM   @Installation INS
		       OUTER APPLY(
		    SELECT PeriodId,
		           PeriodName
		    FROM   @Periods
		) T2
		ORDER BY
		       Company_Name,
		       Sub_Company_Name,
		       Site_Name,
		       AssetNo,
		       T2.PeriodId
		
		/******************************************************************************
		* 1st Level (Except PTD)
		******************************************************************************/
		
		UPDATE RS
		SET    RS.AttendantPaidJackpot = COALESCE(ST.HandpayJackpot, 0),
		       RS.AttendantPaidCancelledCredits = COALESCE(ST.HandpayCredit, 0),
		       RS.ProgressiveJackpot = COALESCE(ST.ProgressiveJackpot, 0),
		       RS.Shortpay = COALESCE(ST.ShortPay, 0),
		       RS.TicketOut = COALESCE(ST.TicketOut, 0),
		       RS.NonCashableTicketOut = COALESCE(ST.NonCashableTicketOut, 0)
		FROM   @Result RS
		       INNER JOIN (
		                SELECT T1.Slot,
		                       T1.AssetNo,
		                       T1.GMUNo,
		                       T1.SortOrder,
		                       T1.SequenceNo,
		                       T2.*,
		                       T3.*
		                FROM   (
		                           SELECT RS.*,
		                                  PS.StartDate,
		                                  PS.EndDate
		                           FROM   @Result RS
		                                  INNER JOIN @Periods PS
		                                       ON  Rs.SequenceNo = PS.PeriodId
		                           WHERE  RS.SortOrder = 1
		                                  AND PS.EndDate IS NOT NULL
		                                      --AND RS.AssetNo = 'LC0032'
		                       ) T1
		                       OUTER APPLY(
		                    SELECT SUM(HandpayJackpot) AS HandpayJackpot,
		                           SUM(HandpayCredit) AS HandpayCredit,
		                           SUM(ProgressiveJackpot) AS ProgressiveJackpot,
		                           SUM(ShortPay) AS ShortPay
		                    FROM   #TreasuryDetails ST
		                    WHERE  ST.Slot = T1.Slot
		                           AND ST.AssetNo = T1.AssetNo
		                           AND ST.GMUNo = T1.GMUNo
		                           AND (
		                                   (
		                                       ST.Treasury_Date >= (
		                                           CASE T1.SequenceNo
		                                                WHEN 2 THEN T1.PreviousCollectionDate
		                                                ELSE COALESCE(T1.StartDate, ST.Treasury_Date)
		                                           END
		                                       )
		                                   )
		                                   AND (
		                                           ST.Treasury_Date <= (
		                                               CASE T1.SequenceNo
		                                                    WHEN 2 THEN T1.CollectionDate
		                                                    ELSE T1.EndDate
		                                               END
		                                           )
		                                       )
		                               )
		                ) T2 OUTER APPLY(
		                    SELECT SUM(TicketOut) AS TicketOut,
		                           SUM(NonCashableTicketOut) AS NonCashableTicketOut
		                    FROM   #VoucherDetails ST
		                    WHERE  ST.Slot = T1.Slot
		                           AND ST.AssetNo = T1.AssetNo
		                           AND ST.GMUNo = T1.GMUNo
		                           AND (
		                                   (
		                                       ST.dtPrinted >= (
		                                           CASE T1.SequenceNo
		                                                WHEN 2 THEN T1.PreviousCollectionDate
		                                                ELSE COALESCE(T1.StartDate, ST.dtPrinted)
		                                           END
		                                       )
		                                   )
		                                   AND (
		                                           ST.dtPrinted <= (
		                                               CASE T1.SequenceNo
		                                                    WHEN 2 THEN T1.CollectionDate
		                                                    ELSE T1.EndDate
		                                               END
		                                           )
		                                       )
		                               )
		                ) T3
		            ) ST
		            ON  ST.Slot = RS.Slot
		            AND ST.AssetNo = RS.AssetNo
		            AND ST.GMUNo = RS.GMUNo
		            AND ST.SortOrder = RS.SortOrder
		            AND ST.SequenceNo = RS.SequenceNo
		
		/******************************************************************************
		* 2nd Level (By Company, SubCompany, Site, SequenceNo, Period)
		******************************************************************************/
		INSERT INTO @Result
		  (
		    SortOrder,
		    SequenceNo,
		    Period,
		    Slot,
		    AssetNo,
		    GMUNo,
		    AttendantPaidJackpot,
		    AttendantPaidCancelledCredits,
		    ProgressiveJackpot,
		    TicketOut,
		    NonCashableTicketOut,
		    Shortpay,
		    Site_Code,
		    Sub_Company_Name,
		    Company_Name,
		    Site_Name
		  )
		SELECT 2,
		       SequenceNo,
		       Period,
		       '',
		       '',
		       '',
		       SUM(AttendantPaidJackpot),
		       SUM(AttendantPaidCancelledCredits),
		       SUM(ProgressiveJackpot),
		       SUM(TicketOut),
		       SUM(NonCashableTicketOut),
		       SUM(Shortpay),
		       Site_Code,
		       Sub_Company_Name,
		       Company_Name,
		       Site_Name
		FROM   @Result
		WHERE  SortOrder = 1
		GROUP BY
		       Company_Name,
		       Sub_Company_Name,
		       Site_Code,
		       Site_Name,
		       SequenceNo,
		       Period
		ORDER BY
		       Company_Name,
		       Sub_Company_Name,
		       Site_Code,
		       Site_Name,
		       SequenceNo,
		       Period
		
		
		/******************************************************************************
		* 3rd Level (SubCompany, SequenceNo, Period)
		******************************************************************************/
		INSERT INTO @Result
		  (
		    SortOrder,
		    SequenceNo,
		    Period,
		    Slot,
		    AssetNo,
		    GMUNo,
		    AttendantPaidJackpot,
		    AttendantPaidCancelledCredits,
		    ProgressiveJackpot,
		    TicketOut,
		    NonCashableTicketOut,
		    Shortpay,
		    Site_Code,
		    Sub_Company_Name,
		    Company_Name,
		    Site_Name
		  )
		SELECT 3,
		       SequenceNo,
		       Period,
		       '',
		       '',
		       '',
		       SUM(AttendantPaidJackpot),
		       SUM(AttendantPaidCancelledCredits),
		       SUM(ProgressiveJackpot),
		       SUM(TicketOut),
		       SUM(NonCashableTicketOut),
		       SUM(Shortpay),
		       '',
		       Sub_Company_Name,
		       '',
		       ''
		FROM   @Result
		WHERE  SortOrder = 1
		GROUP BY
		       Sub_Company_Name,
		       SequenceNo,
		       Period
		ORDER BY
		       Sub_Company_Name,
		       SequenceNo,
		       Period
		
		
		/******************************************************************************
		* 4th Level (Company, SequenceNo, Period)
		******************************************************************************/
		INSERT INTO @Result
		  (
		    SortOrder,
		    SequenceNo,
		    Period,
		    Slot,
		    AssetNo,
		    GMUNo,
		    AttendantPaidJackpot,
		    AttendantPaidCancelledCredits,
		    ProgressiveJackpot,
		    TicketOut,
		    NonCashableTicketOut,
		    Shortpay,
		    Site_Code,
		    Sub_Company_Name,
		    Company_Name,
		    Site_Name
		  )
		SELECT 4,
		       SequenceNo,
		       Period,
		       '',
		       '',
		       '',
		       SUM(AttendantPaidJackpot),
		       SUM(AttendantPaidCancelledCredits),
		       SUM(ProgressiveJackpot),
		       SUM(TicketOut),
		       SUM(NonCashableTicketOut),
		       SUM(Shortpay),
		       '',
		       '',
		       Company_Name,
		       ''
		FROM   @Result
		WHERE  SortOrder = 1
		GROUP BY
		       Company_Name,
		       SequenceNo,
		       Period
		ORDER BY
		       Company_Name,
		       SequenceNo,
		       Period
		
		
		/******************************************************************************
		* 5th Level (SequenceNo, Period)
		******************************************************************************/
		INSERT INTO @Result
		  (
		    SortOrder,
		    SequenceNo,
		    Period,
		    Slot,
		    AssetNo,
		    GMUNo,
		    AttendantPaidJackpot,
		    AttendantPaidCancelledCredits,
		    ProgressiveJackpot,
		    TicketOut,
		    NonCashableTicketOut,
		    Shortpay,
		    Site_Code,
		    Sub_Company_Name,
		    Company_Name,
		    Site_Name
		  )
		SELECT 5,
		       SequenceNo,
		       Period,
		       '',
		       '',
		       '',
		       SUM(AttendantPaidJackpot),
		       SUM(AttendantPaidCancelledCredits),
		       SUM(ProgressiveJackpot),
		       SUM(TicketOut),
		       SUM(NonCashableTicketOut),
		       SUM(Shortpay),
		       '',
		       '',
		       '',
		       ''
		FROM   @Result
		WHERE  SortOrder = 1
		GROUP BY
		       SequenceNo,
		       Period
		ORDER BY
		       SequenceNo,
		       Period
		
		SELECT SortOrder,
		       SequenceNo,
		       Period,
		       Slot,
		       AssetNo,
		       AttendantPaidJackpot,
		       AttendantPaidCancelledCredits,
		       ProgressiveJackpot,
		       TicketOut,
		       NonCashableTicketOut,
		       Shortpay,
		       Site_Code,
		       Sub_Company_Name,
		       Company_Name,
		       Site_Name
		FROM   @Result
		WHERE  Period = COALESCE(@ReportPeriod, Period)
		ORDER BY
		       RowNo
	--END TRY
	--BEGIN CATCH
		/* 
		SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_STATE() AS ErrorState,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage
		*/
	--END CATCH
	
	--SET ANSI_NULLS OFF
	SET NOCOUNT OFF
END

GO

