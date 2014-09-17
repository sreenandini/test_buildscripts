USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getAnalysisPerformanceData_Week_Read]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getAnalysisPerformanceData_Week_Read]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------- 
---
--- Description: Returns a dataset for PopulatePerformanceMachineandManufacturerVW. This SP gets data from DailyRead. 
---				 Initially the data was taken from VW_MachineAnalysis view. As per comment from Rob (Requirement dated
---				 14th Nov 2006), we are need to take data from VW_Read instead. So, this SP is trying to take all values
---				 which we initially took from VW_MachineAnalysis + some additional values.
--- Inputs:      @FilterCondition (varchar)		site		- Filter by site
---												company		- Filter by Company
---												subcomp		- Filter by Sub_Company
---												region		- filter by Sub_Company_Region
---												area		- filter by Sub_Company_Area
---												dist		- filter by Sub_Company_Area
---												zone		- filter by Zone_Name, Zone_ID
---												barpos		- filter by Bar_Position_Name, Bar_Position_ID
---				 @StaffID    					Integer		- filter based on Staff_ID/Staff_Name
---				 @ManufacturerID				Integer		- Manufacturer drop down is selected
---				 @MachineTypeID					Integer     - MachineType drop down is selected
---				 @WeekOrPeriodList    (varchar)	values for in clause ->Week_ID or Period_ID values
---				 @PerformanceDataType(varchar)	general		- Performance General
---												machine		- Performance Machine
---												machinetype	- Performance Machine Type
---												manufac		- Performance Manufacturer
---				 @IsWeek			  (bit)     1			- Week
---												0			- Period
---				 @ID							int			- This ID is based on @FilterCondition. For eg., it could be
---															- Site_ID, Company_ID etc
---				 @Groupby					    varchar	    - optional subcomp,region,area,dist,site,zone,barpos,rep,none
--- Outputs:    dataset
----Execute:	exec rsp_getAnalysisPerformanceData_Week_Read 'site',0,0,0,'334,347,336,337,338,346,345,344,343,342,341,340','general',1,'barpos',48
--- ======================================================================================================================
--- 
--- Revision History
--- 
--- N.Siva     27/11/06     Created 
---------------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[rsp_getAnalysisPerformanceData_Week_Read]
 
	@FilterCondition		varchar(10),
	@StaffID				int,
	@MachineTypeID			int,
	@ManufacturerID			int,		
	@WeekOrPeriodList		varchar(2000), --as of now this is only weeklist
	@PerformanceDataType	varchar	(15),
	@IsWeek					bit,		  -- not used as of now. Need to work on this.
	@Groupby				varchar(10),
	@ID						int
AS
declare @TotalRead int
/***************************************************************************************
Please remove the below comment and start running form here if you are testing this SP
declare @PerformanceDataType varchar(40)
declare @FilterCondition varchar(20)
declare @ID int
declare @StaffID int
declare @ManufacturerID int
declare @MachineTypeID int
declare @WeekOrPeriodList  varchar(1000)
declare @Index int
declare @Delimiter char(1)
declare @String  varchar(1000)
declare @Slice varchar(1000)
set @Index=1
set @Delimiter = ','
set @String = @WeekOrPeriodList
set @PerformanceDataType = 'general'
set @FilterCondition     = 'zone'
Set @ID=348
Set @StaffID = 0
Set @ManufacturerID = 0
Set @MachineTypeID = 0
Set @Groupby = 'none'
SET @WeekOrPeriodList = '334,347,346,345,344,343,342,341,340,339,338,337,336,335'
***************************************************************************************/
SET NOCOUNT ON			--ADO likes this when using temp tables 
SET DATEFORMAT dmy		--UK date format. Otherwise Casting to dd/mm format will not work

---This logic below is a customized in clause. Inserting commaa sepearated values into
--temp table and in the 'IN clause' we use this way - select * from (temp table)
declare @tempCalc int
declare @Index int
declare @Delimiter char(1)
declare @String  varchar(1000)
declare @Slice varchar(1000)
select  @Index=1
set @Delimiter = ','
set @String = @WeekOrPeriodList
SET NOCOUNT ON			--ADO likes this when using temp tables 
SET DATEFORMAT dmy		--UK date format. Otherwise Casting to dd/mm format will not work


--This logic below is a customized in clause. Inserting commaa sepearated values into
--temp table and in the 'IN clause' we use this way - select * from (temp table)
create table #temptableWeekID (valuesWeekIDs varchar(5))

while (@Index !=0)
  begin
	 --Get the Index of first occurence of split character (',' here)
	 Select @Index=charindex(@delimiter,@String)
	 --Now push everthing to left of it into the slice variable
	 if (@Index !=0)
		select @Slice = left(@String,@Index-1)
	 else
		select @Slice = @String
	 --put item into the result set
	print @Slice
	insert into #temptableWeekID(valuesWeekIDs) values(@Slice)
	--Chop the item removed off the main string
	set @tempCalc = len(@String) - @Index
	select @String = Right(@String,@tempcalc)
	--break out if we are done
	if len(@String)=0 break
  end
--General tab under 'Performance tab' has different group by clause based on condition
SELECT @PerformanceDataType=
CASE WHEN @PerformanceDataType = 'general' THEN (@PerformanceDataType + ':' + @FilterCondition)
    ELSE @PerformanceDataType
END


Select VW_Read.Read_No as Read_Count,Week_Id AS Calendar_Week_Ref,(VTP) as VTP,(Hold) as Hold,(RDCCashIn) as RDCCashIN,
(RDCCashOut) as RDCCashOut,(TicketsIN) as TicketsIN,isnull(HandPay,0) as HandPay,
Machine_class_id			= case when (@PerformanceDataType	= 'machine'			or @Groupby='barpos')									  THEN Machine_class.Machine_Class_ID else ''  end, 
Manufacturer_ID				= case when (@PerformanceDataType	= 'manufac'			or @Groupby='barpos')									  THEN Manufacturer.Manufacturer_ID else ''  end,
Machine_Type_ID				= case when (@PerformanceDataType	= 'machinetype'		or @Groupby='barpos')									  THEN Machine_Type.Machine_Type_ID else ''  end, 
Sub_Company_ID				= case when (@PerformanceDataType	= 'general:subcomp'	or @Groupby='subcomp')									  THEN Sub_Company.Sub_Company_ID else ''  end,
Sub_Company_Region_ID		= case when (@PerformanceDataType	= 'general:region'	or @Groupby='region')									  THEN Sub_Company_Region.Sub_Company_Region_ID else ''  end, 
Sub_Company_Area_ID			= case when (@PerformanceDataType	= 'general:area'    or @Groupby='area')										  THEN Sub_Company_Area.Sub_Company_Area_ID else ''  end, 
Sub_Company_District_ID		= case when (@PerformanceDataType	= 'general:dist'    or @Groupby='dist')										  THEN Sub_Company_District.Sub_Company_District_ID else ''  end, 
Zone_Id						= case when (@PerformanceDataType	= 'general:zone'    or @Groupby='zone' or @Groupby='barpos')				  THEN Zone.Zone_Id else '' end, 
Site_ID						= case when (@PerformanceDataType	= 'general:site'	or @Groupby='site')										  THEN Site.Site_ID  else '' end,
Bar_Position_ID				= case when (@PerformanceDataType	= 'general:barpos'  or @Groupby='barpos')									  THEN Bar_Position.Bar_Position_ID else '' end,
Staff_ID					= case when @StaffID >0																							  THEN Site.Staff_ID else '' end,
Machine_Name				= case when (@PerformanceDataType	= 'machine'	        or @Groupby='barpos')									  THEN Machine_Class.Machine_Name else '0' end,
Manufacturer_Name			= case when (@PerformanceDataType	= 'manufac'			or @PerformanceDataType	= 'machine')					  THEN Manufacturer.Manufacturer_Name else '0' end, 
Machine_Type_Code			= case when (@PerformanceDataType	= 'machinetype'		or @PerformanceDataType	= 'machine' or @Groupby='barpos') THEN Machine_Type.Machine_Type_Code else '0' end,
Sub_Company_Name			= case when (@PerformanceDataType	= 'general:subcomp'	or @Groupby='subcomp')									  THEN Sub_Company.Sub_Company_Name else '0' end, 
Sub_Company_Region_Name		= case when (@PerformanceDataType	= 'general:region'	or @Groupby='region')									  THEN Sub_Company_Region.Sub_Company_Region_Name else '0' end,
Sub_Company_Area_Name		= case when (@PerformanceDataType	= 'general:area'    or @Groupby='area')										  THEN Sub_Company_Area.Sub_Company_Area_Name else '0' end, 
Sub_Company_District_Name	= case when (@PerformanceDataType	= 'general:dist'    or @Groupby='dist')										  THEN Sub_Company_District.Sub_Company_District_Name else '0' end, 
Zone_Name					= case when (@PerformanceDataType	= 'general:zone'    or @Groupby='zone' or @Groupby='barpos')				  THEN Zone.Zone_Name else '0' end,  
Bar_Position_Name			= case when (@PerformanceDataType	= 'general:barpos'  or @Groupby='barpos')									  THEN Bar_Position.Bar_Position_Name else '0' end,
Site_Name					= case when (@PerformanceDataType	= 'general:site'	or @Groupby='site')										  THEN Site.Site_Name else '0' end, 
SiteRep						= case when @StaffID >0																							  THEN (Staff.Staff_First_Name + ' ' + Staff.Staff_Last_Name) else '0' end,
(Installation.Installation_Price_Per_Play) as Installation_Price_Of_Play,
(Installation.Installation_Jackpot_Value) as Jackpot, 
(Installation.Installation_Price_Per_Play) as POP, --MAX(Installation.Installation_Start_Date ) as Installation_Start_Date,

(select isnull(SUM(treasury_amount),0)
 from treasury_entry 
 where treasury_entry.installation_id = Installation.Installation_ID
   and treasury_type = 'Shortpay' 
   and treasury_date between CONVERT(DATETIME, calendar_week.calendar_week_start_date, 103) and CONVERT(DATETIME, calendar_week.calendar_week_end_date, 103)) as Shortpay,

(select isnull(SUM(treasury_amount),0)
 from treasury_entry 
 where treasury_entry.installation_id = Installation.Installation_ID
   and treasury_type = 'Refill' 
   and treasury_date between CONVERT(DATETIME, calendar_week.calendar_week_start_date, 103) and CONVERT(DATETIME, calendar_week.calendar_week_end_date, 103)) as Refill,

(select isnull(SUM(treasury_amount),0)
 from treasury_entry 
 where treasury_entry.installation_id = Installation.Installation_ID
   and treasury_type = 'Refunds' 
   and treasury_date between CONVERT(DATETIME, calendar_week.calendar_week_start_date, 103) and CONVERT(DATETIME, calendar_week.calendar_week_end_date, 103)) as Refunds,
1 as counttTreasury


INTO #tmpgroupbydata
from VW_Read 
INNER JOIN Installation ON Installation.Installation_ID = VW_Read.Installation_No 
INNER JOIN Bar_Position ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID 
INNER JOIN Site ON Site.Site_ID = Bar_Position.Site_ID 
LEFT JOIN  Zone ON Bar_Position.Zone_ID = Zone.Zone_ID 
INNER JOIN Calendar_Period ON VW_Read.Period_ID = Calendar_Period.Calendar_Period_ID 
INNER JOIN Calendar_Week ON VW_Read.Week_ID = Calendar_Week.Calendar_Week_ID 
INNER JOIN Sub_Company ON Sub_Company.Sub_Company_ID = Site.Sub_Company_ID 
INNER JOIN Company ON Company.Company_ID = Sub_Company.Company_ID 
INNER JOIN Calendar ON Calendar.Calendar_ID = Sub_Company.Calendar_ID 
LEFT JOIN Sub_Company_Region ON Sub_Company_Region.Sub_Company_Region_ID = Site.Sub_Company_Region_ID 
LEFT JOIN Sub_Company_Area ON Sub_Company_Area.Sub_Company_Area_ID = Site.Sub_Company_Area_ID 
LEFT JOIN Sub_Company_District ON Sub_Company_District.Sub_Company_District_ID = Site.Sub_Company_District_ID 
LEFT JOIN Staff ON Site.Staff_ID = Staff.Staff_ID 
INNER JOIN Machine ON Machine.Machine_ID = Installation.Machine_ID 
INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID 
INNER JOIN Machine_Type  ON Machine.Machine_Category_ID = Machine_Type.Machine_Type_ID 
INNER JOIN Manufacturer ON Machine_Class.Manufacturer_ID = Manufacturer.Manufacturer_ID 

--This is the customised where clause. Again, courtesy Carl :-)
WHERE VW_Read.Week_ID IN (select * from #temptableWeekID)
 AND (
				(
				   @FilterCondition = 'site' 
				   and Site.Site_ID =@id
				)
				OR
				(
				  @FilterCondition = 'company'
				  AND
				  Company.Company_ID = @id
				)
					
				OR (
						  @FilterCondition = 'subcomp' 
						   and Sub_Company.Sub_Company_ID =@id
				 )
				 OR(
						   @FilterCondition = 'region'
						  AND
						  Sub_Company_Region.Sub_Company_Region_ID =@id
			  
				    )
                 OR(
						   @FilterCondition = 'area'
						  AND
						  Sub_Company_Area.Sub_Company_Area_ID = @id
				  
				    )
				 OR(
						   @FilterCondition = 'dist'
						  AND
						  Sub_Company_District.Sub_Company_District_ID = @id
				  
				    )
				 OR(
    					  @FilterCondition = 'zone'
						  AND
						  [Zone].Zone_ID = @id
		  
				    )
	     )
		 AND (
				(
				  @StaffID = 0
				)
				OR
				(
				  @StaffID >0
				  AND
				  Staff.Staff_ID = @StaffID
				)
            )
		AND (
				(
				  @ManufacturerID = 0
				)
				OR
				(
				  @ManufacturerID >0
				  AND
				  Manufacturer.Manufacturer_ID = @ManufacturerID
				)
            )
		AND (
				(
				  @MachineTypeID = 0
				)
				OR
				(
				  @MachineTypeID >0
				  AND
				  Machine_Type.Machine_Type_ID = @MachineTypeID
				)
            )


--These temp tables are required as the sum(treasuryamount) that we are doing down will add up similar rows having same amount
--Hence we need to divide by the No.of times the same values are added.
select count(counttTreasury) as trcountrefund into #tmptblrefunds from #tmpgroupbydata where Refunds <>0 group by Refunds
select count(counttTreasury) as trcountrefill into #tmptblrefill from #tmpgroupbydata where Refill <>0 group by Refill
select count(counttTreasury) as trcountshortpay into #tmptblshortpay from #tmpgroupbydata where ShortPay <>0 group by Shortpay

--Here is the actual dataset returned from this SP
SELECT Calendar_Week_Ref, 
       count(distinct Read_Count) as ReadCount,
       SUM(VTP) as VTP,
       AVG(Hold) as Hold,
       SUM(RDCCashIn) as RDCCashIN, 
       isnull((SUM(Shortpay)  /  (select MAX(trcountshortpay) from #tmptblshortpay) ),0) As shortpay,
       isnull((sum(refunds)   /  (select MAX(trcountrefund) from #tmptblrefunds) ),0)    as refunds,
       isnull((sum(refill)    / (select MAX(trcountrefill) from #tmptblrefill) ),0) as refill,

SUM(TicketsIN) as TicketsIN,
SUM(RDCCashOut) as RDCCashOut, 
SUM(HandPay) as HandPay,
Machine_class_id			= case when (@PerformanceDataType	= 'machine'			or @Groupby='barpos')									  THEN Machine_Class_ID else ''  end, 
Manufacturer_ID				= case when (@PerformanceDataType	= 'manufac'			or @Groupby='barpos')									  THEN Manufacturer_ID else ''  end,
Machine_Type_ID				= case when (@PerformanceDataType	= 'machinetype'		or @Groupby='barpos')									  THEN Machine_Type_ID else ''  end, 
Sub_Company_ID				= case when (@PerformanceDataType	= 'general:subcomp'	or @Groupby='subcomp')									  THEN Sub_Company_ID else ''  end,
Sub_Company_Region_ID		= case when (@PerformanceDataType	= 'general:region'	or @Groupby='region')									  THEN Sub_Company_Region_ID else ''  end, 
Sub_Company_Area_ID			= case when (@PerformanceDataType	= 'general:area'    or @Groupby='area')										  THEN Sub_Company_Area_ID else ''  end, 
Sub_Company_District_ID		= case when (@PerformanceDataType	= 'general:dist'    or @Groupby='dist')										  THEN Sub_Company_District_ID else ''  end, 
Zone_Id						= case when (@PerformanceDataType	= 'general:zone'    or @Groupby='zone' or @Groupby='barpos')				  THEN Zone_Id else '' end, 
Site_ID						= case when (@PerformanceDataType	= 'general:site'	or @Groupby='site')										  THEN Site_ID  else '' end,
Bar_Position_ID				= case when (@PerformanceDataType	= 'general:barpos'  or @Groupby='barpos')									  THEN Bar_Position_ID else '' end,
Staff_ID					= case when  @StaffID >0																						  THEN Staff_ID else '' end,
Machine_Name				= case when (@PerformanceDataType	= 'machine'	        or @Groupby='barpos')									  THEN Machine_Name else '0' end,
Manufacturer_Name			= case when (@PerformanceDataType	= 'manufac'			or @PerformanceDataType	= 'machine')					  THEN Manufacturer_Name else '0' end, 
Machine_Type_Code			= case when (@PerformanceDataType	= 'machinetype'		or @PerformanceDataType	= 'machine' or @Groupby='barpos') THEN Machine_Type_Code else '0' end,
Sub_Company_Name			= case when (@PerformanceDataType	= 'general:subcomp'	or @Groupby='subcomp')									  THEN Sub_Company_Name else '0' end, 
Sub_Company_Region_Name		= case when (@PerformanceDataType	= 'general:region'	or @Groupby='region')									  THEN Sub_Company_Region_Name else '0' end,
Sub_Company_Area_Name		= case when (@PerformanceDataType	= 'general:area'    or @Groupby='area')										  THEN Sub_Company_Area_Name else '0' end, 
Sub_Company_District_Name	= case when (@PerformanceDataType	= 'general:dist'    or @Groupby='dist')										  THEN Sub_Company_District_Name else '0' end, 
Zone_Name					= case when (@PerformanceDataType	= 'general:zone'    or @Groupby='zone' or @Groupby='barpos')				  THEN Zone_Name else '0' end,  
Bar_Position_Name			= case when (@PerformanceDataType	= 'general:barpos'  or @Groupby='barpos')									  THEN Bar_Position_Name else '0' end,
Site_Name					= case when (@PerformanceDataType	= 'general:site'	or @Groupby='site')										  THEN Site_Name else '0' end, 
SiteRep						= case when @StaffID >0																							  THEN SiteRep else '' end,
SUM(Installation_Price_Of_Play) as Installation_Price_Of_Play, SUM(Jackpot) as Jackpot, 
SUM(POP) as POP--, MAX(Installation_Start_Date ) as Installation_Start_Date,

from #tmpgroupbydata 

group by 
		Calendar_Week_Ref,
		Sub_Company_ID,
		Sub_Company_Name,
		Sub_Company_Region_ID, 
		Sub_Company_Region_Name,
		Sub_Company_Area_ID, 
		Sub_Company_Area_Name, 
		Sub_Company_District_ID, 
		Sub_Company_District_Name, 
		Zone_Id, 
		Zone_Name, 
		Site_ID,
		Site_Name,
		Machine_class_id, 
		Manufacturer_ID,
		Machine_Type_ID, 
		Machine_Name,
		Manufacturer_Name, 
		Machine_Type_Code,
		Bar_Position_ID,
		Bar_Position_Name, 
		Staff_ID,
		SiteRep
 



--This is a customized order by clause
ORDER BY CASE WHEN (@PerformanceDataType = 'general:subcomp' or @Groupby='subcomp') THEN 
					cast(Sub_Company_ID AS Varchar(20)) 
			  WHEN (@PerformanceDataType = 'general:region'  or @Groupby='region')  THEN
					cast(Sub_Company_Region_ID AS Varchar(20))  
			  WHEN (@PerformanceDataType = 'general:area' or @Groupby='area') THEN
					cast(Sub_Company_Area_ID AS Varchar(20)) 
			  WHEN (@PerformanceDataType = 'general:dist' or  @Groupby='dist') THEN
					cast(Sub_Company_District_ID AS Varchar(20)) 
			  WHEN (@PerformanceDataType = 'general:site' or @Groupby='site')  THEN
 					cast(Site_Name AS Varchar(20))
			  WHEN (@PerformanceDataType = 'general:zone' or @Groupby='zone')  THEN
					cast(Zone_Name AS Varchar(20)) 
			  WHEN (@PerformanceDataType = 'general:barpos' or @Groupby='barpos')  THEN
					cast(Zone_Name AS Varchar(20))  + cast(Bar_Position_Name AS Varchar(20)) + cast(Machine_Name AS Varchar(20)) + cast(Machine_type_ID AS Varchar(20)) 
 			  WHEN (@PerformanceDataType = 'rep' or @Groupby='rep')  THEN
					cast(SiteRep AS Varchar(20)) + cast(Max(Staff_ID) AS Varchar(20))
			  WHEN (@PerformanceDataType = 'machine' and @Groupby<>'barpos')  THEN 
					cast(Machine_Name AS Varchar(20)) + cast(Machine_Class_ID AS Varchar(20)) + cast(Manufacturer_ID AS Varchar(20)) + cast( Manufacturer_Name AS Varchar(20)) 
			  WHEN (@PerformanceDataType = 'machinetype' and @Groupby<>'barpos') THEN 
					cast(Machine_type_ID AS Varchar(20)) +  cast(Manufacturer_ID AS Varchar(20)) 
              WHEN @PerformanceDataType = 'manufac' THEN 
                    cast(Manufacturer_ID AS Varchar(20)) + cast(Machine_Class_ID AS Varchar(20)) 
			  ELSE ''
         END


--Finally drop the temp tables
drop table #tmpgroupbydata
drop table #temptableWeekID 
drop table #tmptblrefill
drop table #tmptblrefunds
drop table #tmptblshortpay
--Return error if any
RETURN @@ERROR






GO

