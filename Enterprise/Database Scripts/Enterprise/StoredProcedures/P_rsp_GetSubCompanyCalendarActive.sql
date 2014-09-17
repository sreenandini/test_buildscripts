USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCompanyCalendarActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCompanyCalendarActive]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetSubCompanyCalendarActive
	@SubCompanyId INT
AS
/*****************************************************************************************************
DESCRIPTION : To get the SubCompanyCalendarActive Field  
CREATED DATE: 06.2.2013
MODULE            :Calendar      
CHANGE HISTORY :
*****************************************************************************************************/

BEGIN

	SELECT scc.Sub_Company_Calendar_ID,
		   Calendar.Calendar_ID,  
	       scc.Sub_Company_Calendar_Active,
	       Calendar.Calendar_Description,
	       Calendar.Calendar_Year_Start_Date,
	       Calendar.Calendar_Year_End_Date,
	       CASE 
	            WHEN scc.Sub_Company_Calendar_Active = 0 THEN 
	            	CAST(Calendar.Calendar_Description AS VARCHAR(50)) 
	                 + CAST(Calendar.Calendar_Year_Start_Date AS VARCHAR(30)) 
	                + ' to ' + CAST(Calendar.Calendar_Year_End_Date AS VARCHAR(30))
	            ELSE CAST(Calendar.Calendar_Description AS VARCHAR(50))    +  '[Active]'   +
	                 + CAST(Calendar.Calendar_Year_Start_Date AS VARCHAR(30)) 
	                + ' to ' + CAST(Calendar.Calendar_Year_End_Date AS VARCHAR(30))
	       END AS Calendar_History
	FROM   Sub_Company_Calendar scc
	       INNER JOIN Calendar
	            ON  scc.Calendar_ID = Calendar.Calendar_ID
	WHERE  Sub_Company_ID = @SubCompanyId
	ORDER BY
	       scc.Sub_Company_Calendar_ID DESC    
END

GO

