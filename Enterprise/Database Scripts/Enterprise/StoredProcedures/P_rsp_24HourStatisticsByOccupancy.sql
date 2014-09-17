/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/19/2013 12:47:59 PM
 ************************************************************/

USE Enterprise
GO


IF EXISTS (
       SELECT 1
       FROM   sys.objects
       WHERE  NAME = 'rsp_24HourStatisticsByOccupancy'
              AND TYPE = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_24HourStatisticsByOccupancy
END

GO


 ---
 --- Description: creates a gaming day view of hourly data for specific data types
 ---
 --- Inputs:      see inputs
 ---
 --- Outputs:     (0)   - no error ..
 ---              OTHER - SQL error
 ---
 --- =======================================================================
 ---
 --- Revision History
 ---
 ---
 ---------------------------------------------------------------------------         
/*        
 exec rsp_24HourStatisticsByOccupancy @DataType=N'Occupancy',@rows=6,@starthour=6,@category=NULL,@zone=NULL,@position=NULL,@date=NULL        
*/        
--        
CREATE PROCEDURE rsp_24HourStatisticsByOccupancy
	@starthour INT,
	@rows INT = NULL,
	@DataType VARCHAR(50),
	@category INT = NULL,
	@zone INT = NULL,
	@position INT = NULL,
	@date DATETIME = NULL,
	@site INT = NULL
AS
	/**        
set @rows = 17        
set @DataType = 'CANCELLED_CREDITS'        
set @category  = null        
set @zone      = null        
set @position  = null        
set @date      = null -- '13 apr 2009'        
**/        
BEGIN
	SET NOCOUNT ON        
	SET DATEFORMAT dmy        
	
	DECLARE @CreditInd INT 
	
	-- now do the grouping.
	--        
	SET ROWCOUNT 0 
	
	-- set back to known default values          
	IF (@category = 0)
	    SET @category = NULL          
	
	IF (@zone = 0)
	    SET @zone = NULL          
	
	IF (@position = 0)
	    SET @position = NULL          
	
	DECLARE @StartDate  DATETIME        
	DECLARE @EndDate    DATETIME        
	DECLARE @MaxDays    DATETIME        
	DECLARE @CreditInt  INT        
	DECLARE @moneyInd   INT        
	
	SET @MaxDays = GETDATE() - 60 
	
	--Testing
	--SET @Date = '2011-10-01 00:00:00.000'        
	
	IF @date IS NOT NULL
	BEGIN
	    SET @StartDate = DATEADD(d, 0, DATEDIFF(d, 0, @date))        
	    SET @EndDate = DATEADD(d, 0, DATEDIFF(d, 0, @date) + 1)
	END        
	
	
	
	CREATE TABLE #tmpOccupancy
	(
		ID                 INT,
		[Date]             DATETIME,
		Bar_Position_Name  VARCHAR(50),
		Machine_Name       VARCHAR(50),
		Machine_Category   VARCHAR(50),
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
	     @site = @site 
	
	SELECT MCC.Machine_Class_Occupancy_Games_Per_Hour AS OccupancyHour,
	       (
	           LEN(
	               REPLACE(
	                   ISNULL((
	                       CASE DATENAME(dw, [Date])
	                            WHEN 'Monday' THEN ST.Site_Open_Monday
	                            WHEN 'Tuesday' THEN ST.Site_Open_Tuesday
	                            WHEN 'Wednesday' THEN ST.Site_Open_Wednesday
	                            WHEN 'Thursday' THEN ST.Site_Open_Thursday
	                            WHEN 'Friday' THEN ST.Site_Open_Friday
	                            WHEN 'Saturday' THEN ST.Site_Open_Saturday
	                            ELSE Site_Open_Sunday
	                       END
	                   ), 0),
	                   '0',
	                   ''
	               )
	           ) / 4
	       ) AS oHours,
	       occ.*
	       INTO #tOccupancy
	FROM   hourly_statistics HS WITH(NOLOCK)
	       INNER JOIN #tmpOccupancy Occ
	            ON  HS.Hs_ID = occ.ID
	       INNER JOIN Installation INS WITH(NOLOCK)
	            ON  HS.HS_Installation_No = INS.Installation_ID
	       INNER JOIN Bar_Position BP WITH(NOLOCK)
	            ON  INS.Bar_Position_ID = BP.Bar_Position_ID
	       INNER JOIN [Site] ST
	            ON  St.Site_ID = BP.Site_ID
	       INNER JOIN MACHINE MC WITH(NOLOCK)
	            ON  INS.Machine_ID = MC.Machine_ID
	       INNER JOIN Machine_Class MCC WITH(NOLOCK)
	            ON  MC.Machine_Class_ID = MCC.Machine_Class_ID
	       INNER JOIN Machine_Type MT WITH(NOLOCK)
	            ON  MC.Machine_Category_ID = MT.Machine_Type_ID
	
	
	UPDATE #tOccupancy
	SET    Total = (
	           (
	               HS_Hour1_Value +
	               HS_Hour2_Value +
	               HS_Hour3_Value +
	               HS_Hour4_Value +
	               HS_Hour5_Value +
	               HS_Hour6_Value +
	               HS_Hour7_Value +
	               HS_Hour8_Value +
	               HS_Hour9_Value +
	               HS_Hour10_Value +
	               HS_Hour11_Value +
	               HS_Hour12_Value +
	               HS_Hour13_Value +
	               HS_Hour14_Value +
	               HS_Hour15_Value +
	               HS_Hour16_Value +
	               HS_Hour17_Value +
	               HS_Hour18_Value +
	               HS_Hour19_Value +
	               HS_Hour20_Value +
	               HS_Hour21_Value +
	               HS_Hour22_Value +
	               HS_Hour23_Value +
	               HS_Hour24_Value
	           ) / ((CASE ISNULL(OccupancyHour,0) WHEN 0 THEN 1 ELSE OccupancyHour END) * 
				    (CASE ISNULL(oHours,0) WHEN 0 THEN 1 ELSE oHours END))
	       ) * 100 
	
	
	SELECT *
	FROM   #tOccupancy
END 