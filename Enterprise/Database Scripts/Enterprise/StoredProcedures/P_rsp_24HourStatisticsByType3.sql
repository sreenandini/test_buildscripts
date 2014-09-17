/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/29/2013 8:10:13 PM
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_24HourStatisticsByType3]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_24HourStatisticsByType3]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec rsp_24HourStatisticsByType3 @starthour=6,@rows=48,@dataType=N'OCCUPANCY',@category=NULL,@zone=NULL,@position=NULL,@date=NULL,@site=57
CREATE PROCEDURE rsp_24HourStatisticsByType3
	@starthour INT,
	@rows INT = NULL,
	@DataType VARCHAR(50),
	@category INT = NULL,
	@zone INT = NULL,
	@position INT = NULL,
	@date DATETIME = NULL,
	@site INT = NULL,
	@IsDetails INT = 0
WITH RECOMPILE
AS
BEGIN
	SET DATEFORMAT dmy
	SET NOCOUNT ON -- <<< ADO likes this when using temp tables 
	
	IF (@DataType = 'OCCUPANCY(%)')
	    SET @IsDetails = 1
	
	DECLARE @DataType2 VARCHAR(50)
	SET @DataType2 = @DataType 	 
	
	DECLARE @StartDate  DATETIME
	DECLARE @EndDate    DATETIME
	DECLARE @MaxDays    INT
	DECLARE @CreditInt  INT
	DECLARE @moneyInd   INT
	DECLARE @Date2      DATETIME
	
	SET @MaxDays = NULL
	
	--Testing
	--SET @Date = '2011-10-01 00:00:00.000'
	
	IF @date IS NOT NULL
	BEGIN
	    SET @StartDate = DATEADD(d, 0, DATEDIFF(d, 0, @date))
	    SET @EndDate = DATEADD(d, 0, DATEDIFF(d, 0, @date) + 1)
	    SET @IsDetails = 1
	END
	ELSE
	BEGIN
	    IF @IsDetails = 1
	    BEGIN
	        
	        SET @Date2 = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))
	        
	        IF (@rows > 0)
	        BEGIN
	            SET @StartDate = DATEADD(d, 0, DATEDIFF(d, 0, @Date2) - @rows)
	            SET @EndDate = DATEADD(d, 0, DATEDIFF(d, 0, @Date2))
	        END
	        ELSE
	        BEGIN
	            SET @StartDate = NULL
	            SET @EndDate = NULL
	        END
	        
	        --SELECT @StartDate,
	        --       @EndDate
	        
	        --RETURN
	    END
	END
	
	-- Occupancy fix
	IF (@DataType = 'OCCUPANCY(%)')
	BEGIN
	    CREATE TABLE #tmpOccupancy
	    (
	    	ID                 INT,
	    	[Date]             DATETIME,
	    	Bar_Position_Name  VARCHAR(50),
	    	Machine_Name       VARCHAR(50),
	    	Machine_Category   VARCHAR(50),
      		Stock_No		   VARCHAR(50),  
	    	HS_Hour1_Value     FLOAT,
	    	HS_Hour2_Value     FLOAT,
	    	HS_Hour3_Value     FLOAT,
	    	HS_Hour4_Value     FLOAT,
	    	HS_Hour5_Value     FLOAT,
	    	HS_Hour6_Value     FLOAT,
	    	HS_Hour7_Value     FLOAT,
	    	HS_Hour8_Value     FLOAT,
	    	HS_Hour9_Value     FLOAT,
	    	HS_Hour10_Value    FLOAT,
	    	HS_Hour11_Value    FLOAT,
	    	HS_Hour12_Value    FLOAT,
	    	HS_Hour13_Value    FLOAT,
	    	HS_Hour14_Value    FLOAT,
	    	HS_Hour15_Value    FLOAT,
	    	HS_Hour16_Value    FLOAT,
	    	HS_Hour17_Value    FLOAT,
	    	HS_Hour18_Value    FLOAT,
	    	HS_Hour19_Value    FLOAT,
	    	HS_Hour20_Value    FLOAT,
	    	HS_Hour21_Value    FLOAT,
	    	HS_Hour22_Value    FLOAT,
	    	HS_Hour23_Value    FLOAT,
	    	HS_Hour24_Value    FLOAT,
	    	Total              FLOAT
	    )    
	    
	    INSERT INTO #tmpOccupancy
	    EXEC rsp_24HourStatisticsByType3 @DataType = N'Games_Bet',
	         @rows = @rows,
	         @starthour = @starthour,
	         @category = @category,
	         @zone = @zone,
	         @position = @position,
	         @date = @date,
	         @site = @site,
	         @IsDetails = @IsDetails
	    
	    -- Site Opening Hour
	    SELECT Site_ID, DAY_0 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Sunday
	                    ELSE Standard_Opening_Hours_Open_Sunday
	               END
	           ),
	           DAY_1 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Monday
	                    ELSE Standard_Opening_Hours_Open_Monday
	               END
	           ),
	           DAY_2 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Tuesday
	                    ELSE Standard_Opening_Hours_Open_Tuesday
	               END
	           ),
	           DAY_3 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Wednesday
	                    ELSE Standard_Opening_Hours_Open_Wednesday
	               END
	           ),
	           DAY_4 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Thursday
	                    ELSE Standard_Opening_Hours_Open_Thursday
	               END
	           ),
	           DAY_5 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Friday
	                    ELSE Standard_Opening_Hours_Open_Friday
	               END
	           ),
	           DAY_6 = (
	               CASE ISNULL(ST.Standard_Opening_Hours_ID, 0)
	                    WHEN 0 THEN ST.Site_Open_Saturday
	                    ELSE Standard_Opening_Hours_Open_Saturday
	               END
	           )
	           INTO #Site
	    FROM   dbo.[Site] ST
	           LEFT JOIN dbo.Standard_Opening_Hours SOH
	                ON  sT.Standard_Opening_Hours_ID = SOH.Standard_Opening_Hours_ID
	    WHERE  ST.Site_ID = @site
	    
	    SELECT occ.*,
	           MC.Machine_Occupancy_Hour AS OccupancyHour,
	           (
	               LEN(
	                   REPLACE(
	                       ISNULL(
	                           (
	                               CASE DATENAME(dw, [Date])
	                                    WHEN 'Monday' THEN ST.Day_1
	                                    WHEN 'Tuesday' THEN ST.Day_2
	                                    WHEN 'Wednesday' THEN ST.Day_3
	                                    WHEN 'Thursday' THEN ST.Day_4
	                                    WHEN 'Friday' THEN ST.Day_5
	                                    WHEN 'Saturday' THEN ST.Day_6
	                                    ELSE ST.Day_0
	                               END
	                           ),
	                           0
	                       ),
	                       '0',
	                       ''
	                   )
	               ) / 4
	           ) AS oHours
	           INTO #tOccupancy
	    FROM   hourly_statistics HS WITH(NOLOCK)
	           INNER JOIN #tmpOccupancy Occ
	                ON  HS.Hs_ID = occ.ID
	           INNER JOIN Installation INS WITH(NOLOCK)
	                ON  HS.HS_Installation_No = INS.Installation_ID
	           INNER JOIN Bar_Position BP WITH(NOLOCK)
	                ON  INS.Bar_Position_ID = BP.Bar_Position_ID
	           INNER JOIN #Site ST
	                ON  St.Site_ID = BP.Site_ID
	           INNER JOIN MACHINE MC WITH(NOLOCK)
	                ON  INS.Machine_ID = MC.Machine_ID
	           INNER JOIN Machine_Class MCC WITH(NOLOCK)
	                ON  MC.Machine_Class_ID = MCC.Machine_Class_ID
	           INNER JOIN Machine_Type MT WITH(NOLOCK)
	                ON  MC.Machine_Category_ID = MT.Machine_Type_ID
	    
	    SELECT ID,
	           Date,
	           Bar_Position_Name,
	           Machine_Name,
	           Machine_Category,
	           Stock_No,
	           HS_Hour1_Value,
	           HS_Hour2_Value,
	           HS_Hour3_Value,
	           HS_Hour4_Value,
	           HS_Hour5_Value,
	           HS_Hour6_Value,
	           HS_Hour7_Value,
	           HS_Hour8_Value,
	           HS_Hour9_Value,
	           HS_Hour10_Value,
	           HS_Hour11_Value,
	           HS_Hour12_Value,
	           HS_Hour13_Value,
	           HS_Hour14_Value,
	           HS_Hour15_Value,
	           HS_Hour16_Value,
	           HS_Hour17_Value,
	           HS_Hour18_Value,
	           HS_Hour19_Value,
	           HS_Hour20_Value,
	           HS_Hour21_Value,
	           HS_Hour22_Value,
	           HS_Hour23_Value,
	           HS_Hour24_Value,
	           Total,
	           OccupancyHour = (
	               CASE ISNULL(OccupancyHour, 0)
	                    WHEN 0 THEN 1
	                    ELSE ISNULL(OccupancyHour, 0)
	               END
	           ),
	           SiteOpeningHour = (
	               CASE ISNULL(oHours, 0)
	                    WHEN 0 THEN 1
	                    ELSE ISNULL(oHours, 0)
	               END
	           )
	    FROM   #tOccupancy
     ORDER BY
      [Date] DESC,  
            Bar_Position_Name ASC   
	    
	    RETURN
	END
	
	-- set back to known default values
	IF (@category = 0)
	    SET @category = NULL
	
	IF (@zone = 0)
	    SET @zone = NULL
	
	IF (@position = 0)
	    SET @position = NULL
	
	IF (@site = 0)
	    SET @site = NULL	 
	
	SET @CreditInt = 0
	SET @moneyInd = 1
	IF (
	       @DataType2 IN ('AVG_BET', 'CANCELLED_CREDITS', 'CASINO_WIN', 
	                     'CREDITS_WAGERED', 'CREDITS_WON', 'DROP', 'HANDPAY', 
	                     'PROGRESSIVE_WIN', 'PROGRESSIVE_HANDPAY', 'JACKPOT', 
	                     'MYSTERY_MACHINE_PAID', 'MYSTERY_ATTENDANT_PAID')
	   )
	BEGIN
	    SET @CreditInt = 1
	END
	
	IF (
	       @DataType2 IN ('GAMES_BET', 'GAMES_WON', 'GAMES_LOST', 
	                     'TICKETS_PRINTED_QTY', 'TICKETS_INSERTED_QTY', 
	                     'OCCUPANCY(%)', 'NON_CASHABLE_VOUCHERS_IN_QTY', 
	                     'NON_CASHABLE_VOUCHERS_OUT_QTY','BILLS_IN')
	   )
	BEGIN
	    SET @moneyInd = 0
	END
	
	IF (@Datatype <> 'AVG_BET')
	BEGIN
	    SELECT INS.Installation_ID,
	           INS.Bar_Position_ID,
	           CAST(
	               CASE 
	                    WHEN (@CreditInt = 1) THEN INS.Installation_Price_Per_Play
	                    ELSE 1
	               END AS FLOAT
	           ) AS Installation_Price_Of_Play,
	           CAST(CASE WHEN (@moneyInd = 1) THEN 100 ELSE 1 END AS FLOAT) AS 
	           MoneyInd,
	           CASE 
	                WHEN @IsDetails = 0 THEN ''
	                ELSE BP.Bar_Position_Name
	           END AS Bar_Position_Name,
	           CASE 
	                WHEN @IsDetails = 0 THEN ''
	                WHEN ISNULL(MC.IsMultiGame, 0) <> 1 THEN 
	                MCC.Machine_Name
	                ELSE
	                ISNULL(MGMN.MultiGameName,'MULTI GAME')
	           END AS Machine_Name,
	           CASE 
	                WHEN @IsDetails = 0 THEN ''
	                ELSE MT.Machine_Type_Code
	           END AS Machine_Category,
	           CASE 
	                WHEN @IsDetails = 0 THEN ''
                 	ELSE Machine_Stock_No      
                END AS Stock_No,
	           CASE 
	                WHEN @IsDetails = 0 THEN ''
	                ELSE BP.Zone_ID
	           END AS zone,
	           CASE 
	                WHEN @IsDetails = 0 THEN ''
	                ELSE MC.Machine_Category_ID
	           END AS category,
	               
            CASE   
                 WHEN (@IsDetails = 0) THEN ''  
	                ELSE INS.Bar_Position_ID
	           END AS position
	           INTO #TempTable
	    FROM   Installation INS WITH(NOLOCK)
	           LEFT JOIN Bar_Position BP WITH(NOLOCK)
	                ON  INS.Bar_Position_ID = BP.Bar_Position_ID
	           LEFT JOIN MACHINE MC WITH(NOLOCK)
	                ON  INS.Machine_ID = MC.Machine_ID
	           LEFT JOIN Machine_Class MCC WITH(NOLOCK)
	                ON  MC.Machine_Class_ID = MCC.Machine_Class_ID
                    LEFT JOIN MultiGameMapping MGMN WITH(NOLOCK)
	                ON MGMN.machineid=MC.Machine_ID
	           LEFT JOIN Machine_Type MT WITH(NOLOCK)
	                ON  MC.Machine_Category_ID = MT.Machine_Type_ID
	           LEFT JOIN [Site] ST
	                ON  St.Site_ID = BP.Site_ID
	    WHERE  (@Category IS NULL OR MC.Machine_Category_ID = @Category)
	           AND (@position IS NULL OR INS.Bar_Position_ID = @position)
	           AND (@zone IS NULL OR BP.Zone_ID = @zone)
	           AND (@Site IS NULL OR BP.Site_ID = @site)
	    
	    IF (@IsDetails = 0)
	    BEGIN
	        IF (COALESCE(@rows, 0) > 0)
	            SET ROWCOUNT @rows
	        ELSE 
	        IF ISNULL(@MaxDays, 0) > 0
	            SET ROWCOUNT @MaxDays
	    END
	    
	    SELECT [ID] = MAX(HS_ID),
	           [Date] = HS_Date,
	           Bar_Position_Name,
	           Machine_Name,
	           Machine_Category,
	           Stock_No,
	           HS_Hour1_Value = SUM(
	               (
	                   (ISNULL(HS_Hour1, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour2_Value = SUM(
	               (
	                   (ISNULL(HS_Hour2, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour3_Value = SUM(
	               (
	                   (ISNULL(HS_Hour3, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour4_Value = SUM(
	               (
	                   (ISNULL(HS_Hour4, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour5_Value = SUM(
	               (
	                   (ISNULL(HS_Hour5, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour6_Value = SUM(
	               (
	                   (ISNULL(HS_Hour6, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour7_Value = SUM(
	               (
	                   (ISNULL(HS_Hour7, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour8_Value = SUM(
	               (
	                   (ISNULL(HS_Hour8, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour9_Value = SUM(
	               (
	                   (ISNULL(HS_Hour9, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour10_Value = SUM(
	               (
	                   (ISNULL(HS_Hour10, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour11_Value = SUM(
	               (
	                   (ISNULL(HS_Hour11, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour12_Value = SUM(
	               (
	                   (ISNULL(HS_Hour12, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour13_Value = SUM(
	               (
	                   (ISNULL(HS_Hour13, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour14_Value = SUM(
	               (
	                   (ISNULL(HS_Hour14, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour15_Value = SUM(
	               (
	                   (ISNULL(HS_Hour15, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour16_Value = SUM(
	               (
	                   (ISNULL(HS_Hour16, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour17_Value = SUM(
	               (
	                   (ISNULL(HS_Hour17, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour18_Value = SUM(
	               (
	                   (ISNULL(HS_Hour18, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour19_Value = SUM(
	               (
	                   (ISNULL(HS_Hour19, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour20_Value = SUM(
	               (
	                   (ISNULL(HS_Hour20, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour21_Value = SUM(
	               (
	                   (ISNULL(HS_Hour21, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour22_Value = SUM(
	               (
	                   (ISNULL(HS_Hour22, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour23_Value = SUM(
	               (
	                   (ISNULL(HS_Hour23, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           HS_Hour24_Value = SUM(
	               (
	                   (ISNULL(HS_Hour24, 0) * TT.Installation_Price_Of_Play) /
	                   TT.MoneyInd
	               )
	           ),
	           CAST(0.0 AS FLOAT) AS Total
	    FROM   dbo.Hourly_Statistics AS HS WITH(NOLOCK)
	           INNER JOIN #TempTable TT
	                ON  HS.HS_Installation_No = TT.Installation_ID
	    WHERE  HS_Type = @DataType2
	           AND (
	                   @IsDetails = 0
	                   OR (
	                          HS_Date >= ISNULL(@StartDate, HS_DATE)
	                          AND HS_Date <= ISNULL(@EndDate, HS_DATE)
	                      )
	               )
	    GROUP BY
	           HS_Date,
	           Bar_Position_Name,
	           Machine_Name,
	           Machine_Category,
	           category,
	           Zone,
	           Position,
	           Stock_No
	    ORDER BY
	           CAST(HS_Date AS DATETIME) DESC,
	           Bar_Position_Name ASC
	END
	
	IF (@Datatype = 'AVG_BET')
	BEGIN
	    --DECLARE @TEMP_Wagred TABLE(
	    --            ID INT,
	    --            Date DATETIME,
	    --            Bar_Position_Name VARCHAR(50),
	    --            Machine_Name VARCHAR(50),
	    --            Machine_Category VARCHAR(50),
	    --            HS_Hour1_Value FLOAT,
	    --            HS_Hour2_Value FLOAT,
	    --            HS_Hour3_Value FLOAT,
	    --            HS_Hour4_Value FLOAT,
	    --            HS_Hour5_Value FLOAT,
	    --            HS_Hour6_Value FLOAT,
	    --            HS_Hour7_Value FLOAT,
	    --            HS_Hour8_Value FLOAT,
	    --            HS_Hour9_Value FLOAT,
	    --            HS_Hour10_Value FLOAT,
	    --            HS_Hour11_Value FLOAT,
	    --            HS_Hour12_Value FLOAT,
	    --            HS_Hour13_Value FLOAT,
	    --            HS_Hour14_Value FLOAT,
	    --            HS_Hour15_Value FLOAT,
	    --            HS_Hour16_Value FLOAT,
	    --            HS_Hour17_Value FLOAT,
	    --            HS_Hour18_Value FLOAT,
	    --            HS_Hour19_Value FLOAT,
	    --            HS_Hour20_Value FLOAT,
	    --            HS_Hour21_Value FLOAT,
	    --            HS_Hour22_Value FLOAT,
	    --            HS_Hour23_Value FLOAT,
	    --            HS_Hour24_Value FLOAT,
	    --            Total FLOAT
	    --        )
	    ----Games Wagere
	    --INSERT INTO @TEMP_Wagred
	    EXEC rsp_24HourStatisticsByType3 @DataType = 'CREDITS_WAGERED',
	         @starthour = @starthour,
	         @rows = @rows,
	         @category = @category,
	         @zone = @zone,
	         @position = @position,
	         @date = @date,
	         @site = @site
	    
	    --DECLARE @TEMP_Gamebet TABLE(
	    --            ID INT,
	    --            Date DATETIME,
	    --            Bar_Position_Name VARCHAR(50),
	    --            Machine_Name VARCHAR(50),
	    --            Machine_Category VARCHAR(50),
	    --            HS_Hour1_Value FLOAT,
	    --            HS_Hour2_Value FLOAT,
	    --            HS_Hour3_Value FLOAT,
	    --            HS_Hour4_Value FLOAT,
	    --            HS_Hour5_Value FLOAT,
	    --            HS_Hour6_Value FLOAT,
	    --            HS_Hour7_Value FLOAT,
	    --            HS_Hour8_Value FLOAT,
	    --            HS_Hour9_Value FLOAT,
	    --            HS_Hour10_Value FLOAT,
	    --            HS_Hour11_Value FLOAT,
	    --            HS_Hour12_Value FLOAT,
	    --            HS_Hour13_Value FLOAT,
	    --            HS_Hour14_Value FLOAT,
	    --            HS_Hour15_Value FLOAT,
	    --            HS_Hour16_Value FLOAT,
	    --            HS_Hour17_Value FLOAT,
	    --            HS_Hour18_Value FLOAT,
	    --            HS_Hour19_Value FLOAT,
	    --            HS_Hour20_Value FLOAT,
	    --            HS_Hour21_Value FLOAT,
	    --            HS_Hour22_Value FLOAT,
	    --            HS_Hour23_Value FLOAT,
	    --            HS_Hour24_Value FLOAT,
	    --            Total FLOAT
	    --        )
	    
	    -- GamesBet
	    --INSERT INTO @TEMP_Gamebet
	    EXEC rsp_24HourStatisticsByType3 @DataType = 'GAMES_BET',
	         @starthour = @starthour,
	         @rows = @rows,
	         @category = @category,
	         @zone = @zone,
	         @position = @position,
	         @date = @date,
	         @site = @site
	    --AVG BET 
	    --SELECT A.* -- ,
	    --           --			ROUND((A.HS_Hour1_Value + A.HS_Hour2_Value + A.HS_Hour3_Value +
	    --           --				A.HS_Hour4_Value + A.HS_Hour5_Value + A.HS_Hour6_Value + A.HS_Hour7_Value  + A.HS_Hour8_Value  + A.HS_Hour9_Value +
	    --           --				A.HS_Hour10_Value+ A.HS_Hour11_Value+ A.HS_Hour12_Value+ A.HS_Hour13_Value + A.HS_Hour14_Value + A.HS_Hour15_Value+
	    --           --				A.HS_Hour16_Value+ A.HS_Hour17_Value+ A.HS_Hour18_Value+ A.HS_Hour19_Value + A.HS_Hour20_Value + A.HS_Hour21_Value +
	    --           --				A.HS_Hour22_Value+ A.HS_Hour23_Value+ A.HS_Hour24_Value),2,1)
	    --           --				AS Total
	    --FROM   (
	    --           SELECT B.ID AS ID,
	    --                  B.Date AS Date,
	    --                  B.Bar_Position_Name,
	    --                  B.Machine_Name,
	    --                  B.Machine_Category,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour1_Value <> 0 AND c.HS_Hour1_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour1_Value / C.HS_Hour1_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour1_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour2_Value <> 0 AND c.HS_Hour2_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour2_Value / C.HS_Hour2_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour2_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour3_Value <> 0 AND c.HS_Hour3_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour3_Value / C.HS_Hour3_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour3_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour4_Value <> 0 AND c.HS_Hour4_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour4_Value / C.HS_Hour4_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour4_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour5_Value <> 0 AND c.HS_Hour5_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour5_Value / C.HS_Hour5_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour5_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour6_Value <> 0 AND c.HS_Hour6_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour6_Value / C.HS_Hour6_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour6_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour7_Value <> 0 AND c.HS_Hour7_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour7_Value / C.HS_Hour7_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour7_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour8_Value <> 0 AND c.HS_Hour8_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour8_Value / C.HS_Hour8_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour8_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour9_Value <> 0 AND c.HS_Hour9_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour9_Value / C.HS_Hour9_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour9_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour10_Value <> 0 AND c.HS_Hour10_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour10_Value / C.HS_Hour10_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour10_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour11_Value <> 0 AND c.HS_Hour11_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour11_Value / C.HS_Hour11_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour11_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour12_Value <> 0 AND c.HS_Hour12_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour12_Value / C.HS_Hour12_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour12_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour13_Value <> 0 AND c.HS_Hour13_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour13_Value / C.HS_Hour13_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour13_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour14_Value <> 0 AND c.HS_Hour14_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour14_Value / C.HS_Hour14_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour14_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour15_Value <> 0 AND c.HS_Hour15_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour15_Value / C.HS_Hour15_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour15_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour16_Value <> 0 AND c.HS_Hour16_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour16_Value / C.HS_Hour16_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour16_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour17_Value <> 0 AND c.HS_Hour17_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour17_Value / C.HS_Hour17_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour17_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour18_Value <> 0 AND c.HS_Hour18_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour18_Value / C.HS_Hour18_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour18_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour19_Value <> 0 AND c.HS_Hour19_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour19_Value / C.HS_Hour19_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour19_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour20_Value <> 0 AND c.HS_Hour20_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour20_Value / C.HS_Hour20_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour20_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour21_Value <> 0 AND c.HS_Hour21_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour21_Value / C.HS_Hour21_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour21_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour22_Value <> 0 AND c.HS_Hour22_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour22_Value / C.HS_Hour22_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour22_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour23_Value <> 0 AND c.HS_Hour23_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour23_Value / C.HS_Hour23_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour23_Value,
	    --                  CASE 
	    --                       WHEN (B.HS_Hour24_Value <> 0 AND c.HS_Hour24_Value <> 0) THEN 
	    --                            ROUND(B.HS_Hour24_Value / C.HS_Hour24_Value, 2, 1)
	    --                       ELSE 0
	    --                  END AS HS_Hour24_Value,
	    --                  B.Total AS TotalCreditsWagered,
	    --                  C.Total AS TotalGamesBet,
	    --                  CASE 
	    --                       WHEN (B.Total > 0 AND C.Total > 0) THEN ROUND((B.Total / C.Total), 2, 1)
	    --                       ELSE 0.00
	    --                  END AS Total
	    --           FROM   @TEMP_Wagred B
	    --                  INNER JOIN @TEMP_Gamebet C
	    --                       ON  B.Date = C.Date	                           
	    --                       AND B.Bar_Position_Name = C.Bar_Position_Name
	    --       ) A
	END
END
GO

