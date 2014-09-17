USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExpiredCalendarList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetExpiredCalendarList]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetExpiredCalendarList] (@To AS DATETIME, @DateFormat nvarchar(50))
as  
-- Revision History
--        
--  Anuradha	Created		13 May 2008
--  Gets the expired calendar details based on the date.
--exec GetExpiredCalendarList @to=N'31 March 2007',@DateFormat=N'dd/MM/yyyy'

DECLARE @ToDate DATETIME
SET @ToDate = CONVERT(DATETIME, @To,103)

SELECT Sub_Company.Sub_Company_Name, Company.Company_Name, 
dbo.fnFormatDate(CONVERT(DATETIME,Calendar.Calendar_Year_Start_Date,103),@DateFormat) as Calendar_Year_Start_Date,
 dbo.fnFormatDate(CONVERT(DATETIME,Calendar.Calendar_Year_End_Date,103),@DateFormat) as Calendar_Year_End_Date 
FROM { oj ((Sub_Company 
INNER JOIN  Company ON Sub_Company.Company_ID = Company.Company_ID)
LEFT OUTER JOIN Sub_Company_Calendar ON Sub_Company.Sub_Company_ID 
  = Sub_Company_Calendar.Sub_Company_ID) 
LEFT OUTER JOIN 
 Calendar ON Sub_Company_Calendar.Calendar_ID = Calendar.Calendar_ID} 
 WHERE 
 Datediff(d, CONVERT(DATETIME, Calendar.Calendar_Year_End_Date ,103) , @ToDate)>=0 
AND Sub_Company_Calendar.Sub_Company_Calendar_Active <> 0
GO

