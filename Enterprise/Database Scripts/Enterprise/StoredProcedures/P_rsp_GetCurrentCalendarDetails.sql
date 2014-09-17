USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCurrentCalendarDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCurrentCalendarDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetCurrentCalendarDetails
	@calendarId INT
AS
/*****************************************************************************************************
DESCRIPTION : To get the Current Week Number and Current Period Number  
CREATED DATE: 06.2.2013
MODULE            :Calendar      
CHANGE HISTORY :
*****************************************************************************************************/
BEGIN
	SELECT CW.Calendar_Week_Number,
	       CP.Calendar_Period_Number,CP.Calendar_Period_End_Date
	FROM   Calendar_Week CW WITH(NOLOCK)
	       INNER JOIN Calendar_Period CP WITH(NOLOCK)
	            ON  CW.Calendar_ID = CP.Calendar_ID
	WHERE  cw.Calendar_ID = @calendarId
	       AND DATEDIFF(d, CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103), GETDATE()) >= 0
	       AND DATEDIFF(d, GETDATE(), CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103)) >= 0
	       AND DATEDIFF(d, CONVERT(DATETIME, Calendar_Period_Start_Date, 103), GETDATE()) >= 0
	       AND DATEDIFF(d, GETDATE(), CONVERT(DATETIME,Calendar_Period_End_Date, 103)) >= 0
END

GO

