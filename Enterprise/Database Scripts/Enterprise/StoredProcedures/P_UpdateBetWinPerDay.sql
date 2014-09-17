USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBetWinPerDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateBetWinPerDay]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





Create  PROCEDURE UpdateBetWinPerDay
AS
BEGIN

--Truncate the table before Refilling
Truncate table tbl_Bet_Win_Per_Day 

Set dateformat dmy 
--Inserting the COLL Record


---READ

insert into tbl_Bet_Win_Per_Day (Site_Id,Read_Date,Bet_value,Bet_Count,Win_Value,Win_Count,Record_Type) 
   Select 
       Site.Site_Id,
	cast(R.Read_Date as datetime) as Read_Date,
         sum(R.READ_COINS_IN) as Bet_Value,
         sum(R.READ_GAMES_BET) as Bet_Count,
         sum(R.READ_COINS_OUT)as Win_Value,
         sum(R.READ_GAMES_WON) as WIn_Count,
         'READ'		 
    from [Read] R
	Inner join Installation ON
	Installation.Installation_ID = R.Installation_ID  
	Inner join 
	Machine ON
        Installation.Machine_ID = Machine.Machine_ID
     	INNER JOIN Bar_Position ON
        Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
	INNER JOIN Site ON
        Bar_Position.Site_ID = Site.Site_ID
	                           
  where R.READ_COINS_IN< 16000000

	group by Site.Site_Id,cast(R.Read_Date as datetime) order by read_date

    
END





GO

