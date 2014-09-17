USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetTreasuryItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetTreasuryItems]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****
Version History
----------------------------------------
Kirubakar S		28 May 2010		Created
----------------------------------------
***/

CREATE PROCEDURE rsp_GetTreasuryItems      
@StartDate DATETIME,      
@EndDate DATETIME,      
@Type varchar(20),  
@SITE int=0  
      
AS      
 Select Treasury_Date+' '+Treasury_Time as Treasury_Date,     
   isnull(Treasury_Reason,'')as Treasury_Reason,     
   zone_name,       
   Bar_Position_Name,       
   treasury_reason_code,       
   machine_class.Machine_Name as machine_name,       
   Treasury_Amount       
 from   
   treasury_entry       
   inner join installation       
   on treasury_entry.Installation_ID = installation.Installation_ID       
   inner join bar_position on installation.Bar_Position_ID = bar_position.Bar_Position_ID  
   AND (@SITE=0 OR  bar_position.Site_ID=@SITE)      
   left join [zone] on bar_position.Zone_ID = [zone].Zone_ID       
   inner join machine on installation.Machine_ID = machine.Machine_ID       
   inner join machine_class on machine.Machine_Class_ID = machine_class.Machine_Class_ID       
 where   
  treasury_date+' '+Treasury_Time BETWEEN @StartDate AND @EndDate   
   and treasury_type = @Type      
 order by   
   treasury_date DESC      


GO

