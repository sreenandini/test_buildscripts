USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCalendarListForCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCalendarListForCompany]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetCalendarListForCompany
AS
/*****************************************************************************************************
DESCRIPTION : To list the Calendar name for the company  
CREATED DATE: 4.2.2013
MODULE : Calendars      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------

                                                              
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      SELECT Calendar_ID,Calendar_Description,Calendar_Year_Start_Date,Calendar_Year_End_Date FROM Calendar c
END

GO

