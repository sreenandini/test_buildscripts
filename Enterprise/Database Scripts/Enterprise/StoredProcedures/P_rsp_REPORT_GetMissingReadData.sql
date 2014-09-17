USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_GetMissingReadData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_GetMissingReadData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------     
-- EXEC dbo.rsp_REPORT_GetMissingReadData 0,0,0,0,0,0,0,'01 Jan 2008', '01 Jan 2009'
-- Description: Checks for any missing read records for the given set of conditions.    
--    
-- Inputs:     Company, SubCompany, Region, Area, District, Site, Category, StartDate, EndDate
-- Outputs:    The Site Name, Bar Pos Name and Read Date for the missing read data.
--    
-- =======================================================================    
--     
-- Revision History    
--     
-- Renjish   28/07/09   Created    
-- C.Taylor  29/07/09   removed cursors
-- C.Taylor  30/07/09   reworked to use asset, instead of installation.
-- Vineetha  04/08/09   Drop temp tables added
-- Vineetha	10/08/09	Left join removed from the shouldbe table and done cosmetic changes to the code
---------------------------------------------------------------------------     

CREATE PROCEDURE [dbo].[rsp_REPORT_GetMissingReadData]
@Company    INT = 0,
@SubCompany INT = 0,
@Region     INT = 0,
@Area       INT = 0,  
@District   INT = 0,  
@Site       INT = 0,  
@Category   INT = 0,   
@StartDate  Datetime,  
@EndDate   Datetime

AS
SET DATEFORMAT dmy 

  IF @Company = 0    SET @Company = NULL
  IF @SubCompany = 0 SET @SubCompany = NULL
  IF @Region = 0     SET @Region = NULL
  IF @Area = 0       SET @Area = NULL
  IF @District = 0   SET @District = NULL
  IF @Site = 0       SET @Site = NULL
  IF @Category = 0   SET @Category = NULL

  declare @date datetime

  -- if the date is greater than today, we should only have reads for today
  if @EndDate > getdate()
	set @enddate =getdate() 

  if @StartDate > getdate()
	set @StartDate =getdate() 

-- create list of all reads which could be created for an installation
  create table #shouldbe_read_dates ( sb_stock_no varchar(20), sb_date datetime, sb_installation_no int)

  set @date = @startdate
  while @date <= @enddate
  begin   
	insert into #shouldbe_read_dates ( sb_stock_no, sb_date, sb_installation_no ) 
	select m.machine_stock_no, @date, i.installation_id
	from installation i
		INNER JOIN machine m ON m.machine_id = i.machine_id
		INNER JOIN dbo.Bar_Position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
		INNER JOIN dbo.Site S ON BP.Site_ID = S.Site_ID
--		LEFT JOIN [read] r ON ( cast( R.Read_Date as datetime) = @date and i.installation_id = r.installation_id ) 
	where 
			(
				@enddate >= CONVERT(DATETIME,I.Installation_Start_Date,103)      
				AND ((@date <= CONVERT(DATETIME,I.Installation_End_Date,103) OR ISNULL(I.Installation_End_Date, '') = ''))
			)
		AND
			(
				@enddate >= CONVERT(DATETIME,M.Machine_Start_Date,103)      
				AND ((@date <= CONVERT(DATETIME,M.Machine_End_Date,103) OR ISNULL(M.Machine_End_Date, '') = ''))
			)	
 
		AND ((@Site IS NULL ) OR (@Site IS NOT NULL AND S.Site_ID = @Site ))

	 
    set @date = dateadd( day, 1, @date )

  end

  -- create table to hold all reads which have happened
  create table #actual_read_dates ( ac_stock_no varchar(20), ac_date datetime, ac_Days int, ac_installation_no int)

  set @date = @startdate
  while @date <= @enddate
  begin   
    insert into #actual_read_dates ( ac_stock_no, ac_date, ac_Days, ac_installation_no) 
    select  machine_stock_no, @date, read_days, i.installation_id
      from installation i
		JOIN machine m on i.machine_id = m.machine_id
		JOIN dbo.Bar_Position BP on I.Bar_Position_ID = BP.Bar_Position_ID
		JOIN dbo.Site S on BP.Site_ID = S.Site_ID
		JOIN [read] r  on ( cast( R.Read_Date as datetime) = @date and i.installation_id = r.installation_id ) 
where 
	
			(@enddate  >= CONVERT(DATETIME,I.Installation_Start_Date,103)      
			AND 
				(@date <= CONVERT(DATETIME,I.Installation_End_Date,103) OR ISNULL(I.Installation_End_Date, '') = ''))
			AND
			(@enddate  >= CONVERT(DATETIME,M.Machine_Start_Date,103) AND (@date <= CONVERT(DATETIME,M.Machine_End_Date,103) OR ISNULL(M.Machine_End_Date, '') = ''))
			AND 
			(( @Site IS NULL )OR ( @Site IS NOT NULL AND S.Site_ID = @Site ))
	

    set @date = dateadd( day, 1, @date )
  end

  -- remove some days from shouldbe where we have a covering number of days in actual
  --
  while exists ( select 1 from #actual_read_dates where ac_days > 1 ) 
  begin
 
		insert into #actual_read_dates ( ac_stock_no, ac_date, ac_Days, ac_installation_no ) 

		select  ac_stock_no, dateadd ( day, -(ac_days-1), ac_date), 1, ac_installation_no 
			from #actual_read_dates    where ac_days > 1

		update #actual_read_dates set ac_days = ac_days -1 where ac_days > 1

  end

  -- now return the dates from shouldbe, which don't have a match in actual
  --
   select distinct 
			site_name,
			installation_id = 0,
			bar_pos_name = bar_position_name,
			read_date = #shouldbe_read_dates.sb_date ,
			stock_no = sb_stock_no,
			MC.machine_name,
			M.Machine_Start_Date, 
			M.Machine_End_Date,
			#shouldbe_read_dates.sb_installation_no
     from #shouldbe_read_dates 

			LEFT JOIN #actual_read_dates  on ( sb_Date = ac_date and sb_stock_no = ac_stock_no and sb_installation_no = ac_installation_no)
			JOIN dbo.Machine M  on m.machine_stock_no = sb_stock_no
			join installation i    on i.machine_id = m.machine_id
			JOIN dbo.Bar_Position BP on I.Bar_Position_ID = BP.Bar_Position_ID    
			JOIN dbo.Machine_Class MC on M.Machine_Class_ID = MC.Machine_Class_ID    
			LEFT JOIN dbo.Machine_Type MT on M.Machine_Category_ID = MT.Machine_Type_ID
			JOIN dbo.Site S on BP.Site_ID = S.Site_ID
			LEFT JOIN dbo.Zone Z on BP.Zone_ID = Z.Zone_ID 

    WHERE ac_date is null

      AND ( ( @SubCompany IS NULL ) OR  ( @SubCompany IS NOT NULL AND S.Sub_Company_ID = @SubCompany ) )  
	  AND ( ( @Region IS NULL ) OR ( @Region IS NOT NULL AND S.Sub_Company_Region_ID = @Region  ))    
	  AND ( ( @Area IS NULL ) OR ( @Area IS NOT NULL AND S.Sub_Company_Area_ID = @Area ))    
	  AND ( ( @District IS NULL )OR ( @District IS NOT NULL AND S.Sub_Company_District_ID = @District ))    
	  AND ( ( @Site IS NULL )OR ( @Site IS NOT NULL AND S.Site_ID = @Site ))
      AND
--			(
--			 (#shouldbe_read_dates.sb_date between Machine_Start_Date and Machine_End_Date)
--			 or
--			 ( #shouldbe_read_dates.sb_date >= Machine_Start_Date and ISNULL(Machine_End_Date, '') = '')
--			)			
			(
				#shouldbe_read_dates.sb_date >= CONVERT(DATETIME,M.Machine_Start_Date,103)      
				AND (#shouldbe_read_dates.sb_date <= CONVERT(DATETIME,M.Machine_End_Date,103) OR ISNULL(M.Machine_End_Date, '') = '')
			)	
	  AND              
			(#shouldbe_read_dates.sb_date  >= CONVERT(DATETIME,I.Installation_Start_Date,103)      
			AND 
				(#shouldbe_read_dates.sb_date <= CONVERT(DATETIME,I.Installation_End_Date,103) OR ISNULL(I.Installation_End_Date, '') = ''))

 order by site_name,bar_position_name,sb_installation_no,sb_date desc

drop table #shouldbe_read_dates
drop table #actual_read_dates


GO

