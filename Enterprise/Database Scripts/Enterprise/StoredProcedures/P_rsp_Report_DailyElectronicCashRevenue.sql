USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_DailyElectronicCashRevenue]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_DailyElectronicCashRevenue]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------------------------------------------------   
---  
--- Description: retrieve information required for SDS_DailyElectronicCashRevenue report  
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
--  
-- EXEC rsp_Report_DailyElectronicCashRevenue @gamingdate = '27 Sep 2010', @runtype = 1  
-- EXEC rsp_Report_DailyElectronicCashRevenue @gamingdate = '01 jan 2010', @runtype = 2, @site = 55, @zone=''  
exec rsp_Report_DailyElectronicCashRevenue @company=0,@subcompany=0,@site=0,@zone=347,@gamingdate='2013-07-18 19:42:14',@Period=N'DAY, LTD, MTD, PTD, QTD, YTD'
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
--- C.Taylor  ( Contractor )	13/04/10    Created  
--- Vineetha M					30/04/10    Modified Updated read columns to the dummy fields  
--- Jisha Lenu George			20/12/10    Updated the criteria. Fix for #92412 
--- 
--------------------------------------------------------------------------- */  
CREATE PROCEDURE [dbo].[rsp_Report_DailyElectronicCashRevenue]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Zone VARCHAR(50) = '', -- grouping fields, set as -1
	@GamingDate DATETIME ,
	@Period NVARCHAR(50) = 'DAY,LTD,MTD', -- comma list of allowed periods DAY, LTD, MTD, PTD, QTD, YTD
	@SiteIDList VARCHAR(MAX),
	@ExcludeZero BIT = 0 OUT,
	@OnlyeFundEnabled BIT = 0 OUT, -- 1 = only efund machines, 0 = all
	@Runtype INT = 1 OUT -- (1), normal, (2) sub total
AS
BEGIN
	SET NOCOUNT ON        
	
	SET DATEFORMAT dmy 
	
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
	-- create YTD        
	SET @ytdstart = '01 jan ' + CAST(YEAR(@gamingdate) AS VARCHAR(4)) 
	-- create mtd        
	SET @mtdstart = '01/' + CAST(DATEPART(MONTH, @gamingdate) AS VARCHAR(3)) +
	    '/' + CAST(YEAR(@gamingdate) AS VARCHAR(4)) 
	-- create ptd ??        
	
	-- create wtd,          
	SET DATEFIRST 1 -- use monday as week start
	                --  SET @wtdstart = DATEADD(DD, 1 - DATEPART(DW, CONVERT(VARCHAR(10), @gamingdate, 111)), @gamingdate)    
	SET @wtdstart = DATEADD(
	        DD,
	        1 - DATEPART(DW, CONVERT(DATETIME, @gamingdate, 103)),
	        @gamingdate
	    )          
	
	
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
	       [end] = @gamingdate
	WHERE  [name] = 'DAY'        
	
	UPDATE #Periods
	SET    ordering = 2,
	       [start] = CAST(@wtdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'WTD'        
	
	UPDATE #Periods
	SET    ordering = 3,
	       [start] = CAST(@mtdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'MTD'        
	
	UPDATE #Periods
	SET    ordering = 4,
	       [start] = CAST(@qtdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'QTD'        
	
	UPDATE #Periods
	SET    ordering = 5,
	       [start] = @gamingdate,
	       [end] = @gamingdate -- what does period mean
	WHERE  [name] = 'PTD'        
	
	UPDATE #Periods
	SET    ordering = 6,
	       [start] = CAST(@ytdstart AS DATETIME),
	       [end] = @gamingdate
	WHERE  [name] = 'YTD'        
	
	UPDATE #Periods
	SET    ordering = 7,
	       [start] = CAST('01 jan 2000' AS DATETIME),
	       [end] = @gamingdate -- arbitary date for start.
	WHERE  [name] = 'LTD' 
	
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
	
	-- get the raw information, and append site, company etc information to it.
	--        
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
	       Zone_Name = CASE 
	                        WHEN ISNULL(gmi.Zone_Name, '') = '' THEN 'NOT SET'
	                        ELSE gmi.Zone_Name
	                   END,
	       [Order] = #Periods.ordering,
	       [Period] = #Periods.Name,
	       [WAT_In] = SUM(CAST(Cashable_EFT_IN AS DECIMAL(10, 2)) / 100),
	       [WAT_Out] = SUM(CAST(Cashable_EFT_OUT AS DECIMAL(10, 2)) / 100),
	       [Cashable_ePromo_In] = SUM(CAST(Promo_Cashable_EFT_IN AS DECIMAL(10, 2)) / 100),
	       [Cashable_ePromo_Out] = SUM(CAST(Promo_Cashable_EFT_OUT AS DECIMAL(10, 2)) / 100),
	       [NCashable_ePromo_In] = SUM(CAST(NonCashable_EFT_IN AS DECIMAL(10, 2)) / 100),
	       [NCashable_ePromo_Out] = SUM(CAST(NonCashable_EFT_OUT AS DECIMAL(10, 2)) / 100),
	       [eFund_Drop] = SUM(CAST(Cashable_EFT_IN AS DECIMAL(10, 2)) / 100) +
	       SUM(CAST(Promo_Cashable_EFT_IN AS DECIMAL(10, 2)) / 100) + (SUM(CAST(NonCashable_EFT_IN AS DECIMAL(10, 2))) / 100),	-- [WAT_In] + [Cashable_ePromo_In] + [NCashable_ePromo_In]  
	       
	       
	       [eFund_Expense] = SUM(CAST(Cashable_EFT_OUT AS DECIMAL(10, 2)) / 100)
	       + SUM(CAST(Promo_Cashable_EFT_OUT AS DECIMAL(10, 2)) / 100) + (SUM(CAST(NonCashable_EFT_OUT AS DECIMAL(10, 2))) / 100),	-- [WAT_Out] + [Cashable_ePromo_Out] + [NCashable_ePromo_Out]              
	       [eFund_Net] = (
	           SUM(CAST(Cashable_EFT_IN AS DECIMAL(10, 2)) / 100) + SUM(CAST(Promo_Cashable_EFT_IN AS DECIMAL(10, 2)) / 100) 
	           + (SUM(CAST(NonCashable_EFT_IN AS DECIMAL(10, 2))) / 100)
	       ) 
	       -(
	           SUM(CAST(Cashable_EFT_OUT AS DECIMAL(10, 2)) / 100) + SUM(CAST(Promo_Cashable_EFT_OUT AS DECIMAL(10, 2)) / 100) 
	           + (SUM(CAST(NonCashable_EFT_OUT AS DECIMAL(10, 2))) / 100)
	       ) -- drop - expense             
	       
	       
	       
	       INTO #preGrouping
	FROM   #Periods,
	       [read] r
	       JOIN vw_genericmachineinformation gmi
	            ON  r.installation_id = gmi.installation_ID
	       JOIN SITE S
	            ON  gmi.Site_ID = S.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.Company_ID = C.Company_ID
	WHERE  r.ReadDate BETWEEN DATEADD(d, 0, DATEDIFF(d, 0, #periods.[start])) 
	       AND DATEADD(d, 0, DATEDIFF(d, 0, #periods.[end]))
	       AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID = @Zone))
	       AND ISNULL(@Site, S.Site_ID) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND gmi.Site_ID IN (SELECT DATA
	                                   FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	       AND ISNULL(@SubCompany, S.Sub_Company_ID) = S.Sub_Company_ID
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	           
	           -- add eFunds enabled check here
	GROUP BY
	       gmi.Bar_Position_ID,
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
	       gmi.Zone_Name,	-- area        
	       #Periods.ordering,
	       #Periods.name,
	       #Periods.start,
	       #Periods.[end] 
	
	--order by Company_ID, sub_company_id, site_id, gmi.Bar_Position_name, #Periods.ordering        
	
	-- return relevant report type, detail, sub and grandtotal
	--         
	IF @runtype = 1 -- details
	BEGIN
	    IF EXISTS (
	           SELECT *
	           FROM   #preGrouping
	       )
	        -- get data        
	        SELECT *
	        FROM   #preGrouping
	    ELSE
	        -- create dummy row        
	        SELECT Bar_Position_ID = NULL,
	               Bar_Position_Name = NULL,	-- stand        
	               Machine_Type_ID = NULL,
	               Machine_Type_Code = NULL,
	               Site_ID = NULL,
	               Site_Name = NULL,
	               Sub_Company_ID = NULL,
	               Sub_Company_Name = NULL,
	               Company_ID = NULL,
	               Company_Name = NULL,
	               Machine_Stock_No = NULL,	-- slot        
	               Zone_Name = NULL,	-- area   
	               [Order] = NULL,
	               [Period] = NULL,
	               [WAT_In] = NULL,
	               [WAT_Out] = NULL,
	               [Cashable_ePromo_In] = NULL,
	               [Cashable_ePromo_Out] = NULL,
	               [NCashable_ePromo_In] = NULL,
	               [NCashable_ePromo_Out] = NULL,
	               [eFund_Drop] = NULL,
	               [eFund_Expense] = NULL,
	               [eFund_Net] = NULL
	END
	ELSE 
	IF @runtype = 2
	    SELECT Bar_Position_ID = NULL,
	           Bar_Position_Name = NULL,
	           Machine_Type_ID = NULL,
	           Machine_Type_Code = NULL,
	           Site_ID = CASE 
	                          WHEN @site != '' THEN Site_ID
	                          ELSE NULL
	                     END,
	           Site_Name = CASE 
	                            WHEN @site != '' THEN Site_Name
	                            ELSE NULL
	                       END,
	           Sub_Company_ID = CASE 
	                                 WHEN @subcompany != '' THEN Sub_Company_ID
	                                 ELSE NULL
	                            END,
	           Sub_Company_Name = CASE 
	                                   WHEN @subcompany != '' THEN 
	                                        Sub_Company_Name
	                                   ELSE NULL
	                              END,
	           Company_ID = CASE 
	                             WHEN @company != '' THEN Company_ID
	                             ELSE NULL
	                        END,
	           Company_Name = CASE 
	                               WHEN @company != '' THEN Company_Name
	                               ELSE NULL
	                          END,
	           Machine_Stock_No = NULL,
	           Zone_Name = CASE 
	                            WHEN @zone != '' THEN Zone_Name
	                            ELSE NULL
	                       END,
	           [Order],
	           [Period],
	           [WAT_In] = CAST(SUM([WAT_In]) AS DECIMAL(10, 2)),
	           [WAT_Out] = CAST(SUM([WAT_Out]) AS DECIMAL(10, 2)),
	           [Cashable_ePromo_In] = CAST(SUM([Cashable_ePromo_In]) AS DECIMAL(10, 2)),
	           [Cashable_ePromo_Out] = CAST(SUM([Cashable_ePromo_Out]) AS DECIMAL(10, 2)),
	           [NCashable_ePromo_In] = CAST(SUM([NCashable_ePromo_In]) AS DECIMAL(10, 2)),
	           [NCashable_ePromo_Out] = CAST(SUM([NCashable_ePromo_Out]) AS DECIMAL(10, 2)),
	           [eFund_Drop] = CAST(SUM([eFund_Drop]) AS DECIMAL(10, 2)),
	           [eFund_Expense] = CAST(SUM([eFund_Expense]) AS DECIMAL(10, 2)),
	           [eFund_Net] = CAST(SUM([eFund_Net]) AS DECIMAL(10, 2))
	    FROM   #preGrouping
	    GROUP BY
	           CASE 
	                WHEN @zone != '' THEN Zone_Name
	                ELSE NULL
	           END,
	           CASE 
	                WHEN @site != '' THEN Site_ID
	                ELSE NULL
	           END,
	           CASE 
	                WHEN @site != '' THEN Site_Name
	                ELSE NULL
	           END,
	           CASE 
	                WHEN @subcompany != '' THEN Sub_Company_ID
	                ELSE NULL
	           END,
	           CASE 
	                WHEN @subcompany != '' THEN Sub_Company_Name
	                ELSE NULL
	           END,
	           CASE 
	                WHEN @company != '' THEN Company_ID
	                ELSE NULL
	           END,
	           CASE 
	                WHEN @company != '' THEN Company_Name
	                ELSE NULL
	           END,
	           [order],
	           [Period]
	    ORDER BY
	           [Order]
END
GO

