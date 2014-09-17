USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Report_DailyElectronicCashRevenue_Pivot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Report_DailyElectronicCashRevenue_Pivot]
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
---              used by the grand total part of the respective report
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
---
-------------------------------------------------------------------------- 
-- to USE
--
-- EXEC rsp_Report_DailyElectronicCashRevenue_Pivot @gamingdate = '01 jan 2010'
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
--- C.Taylor  ( Contractor )  16/04/10    Created
--- Vineetha M				  30/04/10    Modified	Updated read columns to the dummy fields
--------------------------------------------------------------------------- */
CREATE PROCEDURE [dbo].rsp_Report_DailyElectronicCashRevenue_Pivot
 
  @company          int         = 0,
  @subcompany       int         = 0,
  @site             int         = 0,  
  @zone             VARCHAR(50) ='',           -- grouping fields, set as -1
  @gamingdate       datetime    ,
  
  @Period           nvarchar(50) = 'DAY,LTD,MTD',           -- comma list of allowed periods DAY, LTD, MTD, PTD, QTD, YTD
  @ExcludeZero      bit         = 0  OUT, 
  @OnlyeFundEnabled bit         = 0  OUT   -- 1 = only efund machines, 0 = all

AS

BEGIN

  set nocount on

  declare @currencyformatting varchar(20)
	SELECT @currencyFormatting =   
	CASE  
		WHEN setting_value ='es-AR' THEN 'it-IT'  
		ELSE setting_value  
	END  
	FROM Setting WHERE Setting_Name = 'BMC_Reports_Language'  

  -- create quarter jan-mar, apr-jun, jul-sep,oct-dec
  DECLARE @qstartmonth INT,
          @qtdstart    datetime,
          @mtdstart    datetime,
          @ytdstart    datetime,
          @wtdstart    datetime

  SET @qstartmonth = CASE WHEN MONTH(@gamingdate) BETWEEN 1 AND 3 THEN 1
                          WHEN MONTH(@gamingdate) BETWEEN 4 AND 6 THEN 4
                          WHEN MONTH(@gamingdate) BETWEEN 7 AND 9 THEN 7
                          ELSE 10
                     END
                     
  SET @qtdstart = '01/' + CAST (@qstartmonth AS VARCHAR(4)) + '/' + CAST(YEAR(@gamingdate) AS VARCHAR(4))
  -- create YTD
  SET @ytdstart = '01 jan ' + CAST (YEAR(@gamingdate) AS VARCHAR(4))
  -- create mtd
  SET @mtdstart = '01/' + CAST ( DATEPART( MONTH, @gamingdate ) AS VARCHAR(3)) + '/' + CAST(YEAR(@gamingdate) AS VARCHAR(4))
  -- create ptd ??

  -- create wtd,  
  set datefirst 1	-- use monday as week start
  SET @wtdstart = DATEADD(DD, 1 - DATEPART(DW, CONVERT(VARCHAR(10), @gamingdate, 111)), @gamingdate)

  create table #Periods
   ( 
     [name]     varchar(3),
     [ordering] int,
     [start]    datetime,
     [end]      datetime
   )

  insert into #periods ( [name] )
  SELECT data from dbo.fnSplit(@Period,',')

  -- create our list of periods, complete with start and end dates
  update #Periods 
     set ordering = 1,
         [start] = @gamingdate, [end] = @gamingdate
   where [name] = 'DAY'

  update #Periods
     set ordering = 2,
         [start] = @wtdstart, [end] = @gamingdate
   where [name] = 'WTD'

  update #Periods
     set ordering = 3,
         [start] = @mtdstart, [end] = @gamingdate
   where [name] = 'MTD'

  update #Periods
     set ordering = 4,
         [start] = @qtdstart, [end] = @gamingdate
   where [name] = 'QTD'

  update #Periods
     set ordering = 5,
         [start] = @gamingdate, [end] = @gamingdate	-- what does period mean
   where [name] = 'PTD'

  update #Periods
     set ordering = 6,
         [start] = @ytdstart, [end] = @gamingdate
   where [name] = 'YTD'

  update #Periods
     set ordering = 7,
         [start] = '01 jan 2000', [end] = @gamingdate	-- arbitary date for start.
   where [name] = 'LTD'

  
  -- get the raw information, and append site, company etc information to it.
  --
    SELECT Site_ID = CASE WHEN @site != '' THEN gmi.Site_ID ELSE NULL end,
	       Site_Name = CASE WHEN @site != '' THEN gmi.Site_Name ELSE NULL end,
	       Sub_Company_ID = CASE WHEN @subcompany != '' THEN gmi.Sub_Company_ID ELSE NULL end,
	       Sub_Company_Name = CASE WHEN @subcompany != '' THEN gmi.Sub_Company_Name ELSE NULL end,
	       Company_ID = CASE WHEN @company != '' THEN gmi.Company_ID ELSE NULL end,
 	       Company_Name = CASE WHEN @company != '' THEN gmi.Company_Name ELSE NULL end,

           Zone_Name = CASE WHEN ISNULL(gmi.Zone_Name,'') = '' THEN 'NOT SET' ELSE gmi.Zone_Name END,

           [Order] = #Periods.ordering,
           [Period] = #Periods.Name,
			[WAT_In] = SUM(Cashable_EFT_IN),
            [WAT_Out] = SUM(Cashable_EFT_OUT),
            [Cashable_ePromo_In] = SUM(Promo_Cashable_EFT_IN),		            
            [Cashable_ePromo_Out] = SUM(Promo_Cashable_EFT_OUT),	    
            [NCashable_ePromo_In] = SUM(NonCashable_EFT_IN),
            [NCashable_ePromo_Out] = SUM(NonCashable_EFT_OUT),
            [eFund_Drop] =  SUM(Cashable_EFT_IN)+SUM(Promo_Cashable_EFT_IN)+ SUM(NonCashable_EFT_IN),		-- [WAT_In] + [Cashable_ePromo_In] + [NCashable_ePromo_In]
            [eFund_Expense] =  SUM(Cashable_EFT_OUT)+SUM(Promo_Cashable_EFT_OUT)+SUM(NonCashable_EFT_OUT),    -- [WAT_Out] + [Cashable_ePromo_Out] + [NCashable_ePromo_Out]
            [eFund_Net] =(SUM(Cashable_EFT_IN)+SUM(Promo_Cashable_EFT_IN)+ SUM(NonCashable_EFT_IN))-(SUM(Cashable_EFT_OUT)+SUM(Promo_Cashable_EFT_OUT)+SUM(NonCashable_EFT_OUT)),		-- drop - expense  
            currencyformatting = @currencyformatting

      into #preGrouping

      from #Periods, [read] r

      JOIN vw_genericmachineinformation gmi
        on r.installation_id = gmi.installation_ID

     where r.read_date between #periods.[start] and #periods.[end]

       AND ( ( @zone <> '' AND zone_name = @zone  )
              OR      
              @zone = ''
            )   
        AND ( ( @site <> '' AND site_id = @site  )
                OR      
                @site = 0
            )           
        AND ( ( @Company <> '' AND Company_id = @Company  )
                OR      
                @Company = 0
            )           
        AND ( ( @Subcompany <> '' AND Sub_Company_id = @Subcompany  )
                OR      
                @Subcompany = 0
            )  

    -- add eFunds enabled check here

   GROUP BY CASE WHEN ISNULL(gmi.Zone_Name,'') = '' THEN 'NOT SET' ELSE gmi.Zone_Name END,
 	        CASE WHEN @site != '' THEN gmi.Site_ID ELSE NULL end,
	        CASE WHEN @site != '' THEN gmi.Site_Name ELSE NULL end,
	        CASE WHEN @subcompany != '' THEN gmi.Sub_Company_ID ELSE NULL end,
	        CASE WHEN @subcompany != '' THEN gmi.Sub_Company_Name ELSE NULL end,
	        CASE WHEN @company != '' THEN gmi.Company_ID ELSE NULL end,
	        CASE WHEN @company != '' THEN gmi.Company_Name ELSE NULL end,
            #Periods.ordering,
            #Periods.name,
            #Periods.start,
            #Periods.[end]


  -- create a list ready for a pivoting
  --
  SELECT RowText = 'Total WAT In', ordering = 1, ColumnName = [Period], ColumnValue = SUM(WAT_In)
    into #forPivot
    from #preGrouping group by [Period]

   UNION
    SELECT RowText = 'Total WAT Out', ordering = 2, ColumnName = [Period], ColumnValue = SUM(WAT_Out)
      from #preGrouping group by [Period]

   UNION
    SELECT RowText = 'Total Cashable ePromo In', ordering = 3, ColumnName = [Period], ColumnValue = SUM([Cashable_ePromo_In])
       from #preGrouping group by [Period]
    
   UNION
    SELECT RowText = 'Total Cashable ePromo Out', ordering = 4, ColumnName = [Period], ColumnValue = SUM([Cashable_ePromo_Out])
       from #preGrouping group by [Period]

   UNION
    SELECT RowText = 'Total Non-Cashable ePromo In', ordering = 5, ColumnName = [Period], ColumnValue = SUM([NCashable_ePromo_In])
       from #preGrouping group by [Period]
    
   UNION
    SELECT RowText = 'Total Non-Cashable ePromo Out', ordering = 6, ColumnName = [Period], ColumnValue = SUM([NCashable_ePromo_Out])
       from #preGrouping group by [Period]

   UNION
    SELECT RowText = 'Total Elec Transfers Drop', ordering = 7, ColumnName = [Period], ColumnValue = SUM([eFund_Drop])
       from #preGrouping group by [Period]

   UNION
    SELECT RowText = 'Total Elec Transfers Expense', ordering = 8, ColumnName = [Period], ColumnValue = SUM([eFund_Expense])
       from #preGrouping group by [Period]

   UNION
    SELECT RowText = 'Elec Transfers Net', ordering = 9, ColumnName = [Period], ColumnValue = SUM([eFund_Net])
       from #preGrouping group by [Period]

  -- create a pivot
    SELECT RowText, ordering,
                    isnull([DAY],0) AS [DAY], 
                    isnull([WTD],0) AS [WTD],
                    isnull([MTD],0) AS [MTD],
                    isnull([QTD],0) AS [QTD],
                    isnull([PTD],0) AS [PTD],
                    isnull([YTD],0) AS [YTD],
                    isnull([LTD],0) AS [LTD]
      FROM ( SELECT RowText, ordering, ColumnName, ColumnValue FROM #forPivot ) ps
     PIVOT ( SUM (ColumnValue) FOR ColumnName IN ( [DAY], [WTD], [MTD], [QTD], [PTD], [YTD], [LTD] ) ) AS pvt
  order by ordering

END

GO

